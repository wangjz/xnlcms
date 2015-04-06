using System;
using System.Collections.Generic;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using System.Text;
using System.IO;
using System.Threading;
using COM.SingNo.XNLEngine;
using System.Web;
using COM.SingNo.DAL;
using System.Data;
using System.Data.Common;
using COM.SingNo.XNLLib.CMS.Manage;
using COM.SingNo.Web;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Manage
{
   public class ParseAndCreat
    {
        private string fileExtName;
        private string pageStyle;
        private string templatePath;
        private string charset;
        private string nodeList;
        string templateContent;
        private WebContext XNLPage;
        private bool isDebug;
        private string rulePath;//生成规则路径
        private Cchannel _curChannel;
        private string fileName;//要生成的文件名称
        private string ruleTrueFullPath;//生成规则路径的物理全地址
        private string ruleTruePath;//生成路径规则的当前站点路径
        int projectId;
        string[] nodeArr;
        Dictionary<string, XNLParam> labelParams;
        public ParseAndCreat()
        {
            labelParams = new Dictionary<string, XNLParam>();
        }
        public ParseAndCreat(string nodeList, string pageStyle, int projectId, bool debug, WebContext basePage, Dictionary<string, XNLParam> labelParams)
        {
            this.nodeList = nodeList;  //生成单页时表示单页列表
            this.pageStyle = pageStyle;
            this.projectId = projectId;
            isDebug = debug;
            XNLPage = basePage;
            XNLPage.templateProjectId = projectId;
            //由节点列表得到各个节点
            int _pageStyle = Convert.ToInt32(pageStyle);
            if(debug)
            {
                int _nodeId = COM.SingNo.XNLLib.CMS.Manage.ManageUtil.getCurSiteID(XNLPage);
                _curChannel = ChannelConfigManager.createInstance().channelDataColls[_nodeId];
            }else
            {
                if (_pageStyle == 1 || _pageStyle == 2)
                {
                    nodeArr = nodeList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int _nodeId = Convert.ToInt32(nodeArr[0]);
                    _curChannel = ChannelConfigManager.createInstance().channelDataColls[_nodeId];
                }
                else
                {
                    int _nodeId = COM.SingNo.XNLLib.CMS.Manage.ManageUtil.getCurSiteID(XNLPage);
                    _curChannel = ChannelConfigManager.createInstance().channelDataColls[_nodeId];
                }
            }
            XNLPage.curChannel=_curChannel;
            this.labelParams = labelParams;
        }
        public string toParseCreat()
        {
            #region //调试得到当前状态，不真正解析
            if (isDebug)
            {
                switch (pageStyle)
                {
                    case "0":
                        return "{\"state\":\"" + _curChannel.siteNode.indexPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.indexPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.indexPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    case "1":
                        return "{\"state\":\"" + _curChannel.siteNode.channelPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.channelPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.channelPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    case "2":
                        return "{\"state\":\"" + _curChannel.siteNode.contentPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.contentPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.contentPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    default:
                        return "{\"state\":\"" + _curChannel.siteNode.singlePageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.singlePageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.singlePageCreateThreadInfo.createdPageCount.ToString() + "}";
                }
            }
            #endregion
            //得到页面类型
            switch (pageStyle)
            {
                case "0":
                    if (_curChannel.siteNode.indexPageCreateThreadInfo.state == CreateThreadState.YES)
                    {
                        return "{\"state\":\"" + _curChannel.siteNode.indexPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.indexPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.indexPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    }
                    DataTable dt = getTemplateTable();
                    DataRow row = dt.Rows[0];
                    templatePath= Convert.ToString(row["TemplateFilePath"]);
                    rulePath = Convert.ToString(row["CreatedFileFullName"]);
                    charset = Convert.ToString(row["charset"]);
                    fileExtName = Convert.ToString(row["CreatedFileExtName"]);
                    rulePath = CMSUtils.getAbsolutePath(rulePath);
                    string[] path_array = rulePath.Split('/');
                    fileName = path_array[path_array.Length - 1];
                    ruleTruePath = rulePath.Replace("@", _curChannel.siteNode.siteWebPath).Replace("//", "/");
                    ruleTrueFullPath = XNLPage.Context.Server.MapPath("~" + ruleTruePath);
                    string dirPath = (ruleTrueFullPath + ".tempext").Replace(fileName + ".tempext", "");
                    CMSUtils.CreateDirectory(dirPath);
                    string templateTruePath = XNLPage.Context.Server.MapPath("~" + templatePath.Replace("@", _curChannel.siteNode.siteWebPath));
                    templateContent = XNLBaseCommon.loadTemplete(templateTruePath, charset);
                    XNLPage.pagePath = ruleTruePath + fileExtName;
                    if (fileExtName.ToLower() != ".aspx") //静态
                    {
                        PageInfo pageinfo = getPageInfo(XNLPage);
                        //检查是否建立生成文件的目录
                        XNLPage.accessType = AccessType.Static;
                        if (pageinfo.pageColls == null || pageinfo.pageColls.Count == 0)
                        {
                           _curChannel.indexPageCreateThreadInfo.pageCount = 1;
                           createPage(pageinfo);
                           return "{\"state\":\"YES\",\"all\":1,\"cur\":1}";
                        }
                        else if (pageinfo.pageColls != null && pageinfo.pageColls.Count > 0)
                        {
                             int _pageCount=createPage(pageinfo);
                             return "{\"state\":\"" + _curChannel.siteNode.indexPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _pageCount + ",\"cur\":" + _curChannel.siteNode.indexPageCreateThreadInfo.createdPageCount.ToString() + "}";
                        }
                    }
                    else //生成动态页
                    {
                        ParseInfo parseinfo = new ParseInfo();
                        BasePage basepage = new BasePage();
                        basepage.Context = XNLPage.Context;
                        basepage.curChannel = _curChannel;
                        basepage.templateProjectId = projectId;
                        basepage.pagePath = XNLPage.pagePath;
                        ParseInfo _parseinfo = new ParseInfo();
                        _parseinfo.charset = charset;
                        _parseinfo.createPath = (this.ruleTrueFullPath + fileExtName);
                        _parseinfo.fileExtName = fileExtName;
                        _parseinfo.pageName = this.fileName;
                        _parseinfo.templateContent = templateContent;
                        _parseinfo.pageStyle = pageStyle;
                        _parseinfo.curChannel = _curChannel;
                        _parseinfo.pagePath = basepage.pagePath;
                        _parseinfo.XNLPage = basepage;
                        string dynamicTemplate = XNLWebCommon.loadDynamicTemplate();
                        string templateStr = XNLWebCommon.getDynamicPage(dynamicTemplate, templateContent, _parseinfo);
                        XNLWebCommon.writeDynamicPage(_parseinfo.createPath, templateStr);
                        return "{\"state\":\"YES\",\"all\":1,\"cur\":1}";
                    }
                    break;
                case "1":
                    if (_curChannel.siteNode.channelPageCreateThreadInfo.state == CreateThreadState.YES)
                    {
                        return "{\"state\":\"" + _curChannel.siteNode.channelPageCreateThreadInfo.state.ToString() + ",\"all\":" + _curChannel.siteNode.channelPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.channelPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    }
                    int _channelPageCount=toSetChannelCreate();
                    return "{\"state\":\"" + _curChannel.siteNode.channelPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _channelPageCount + ",\"cur\":" + _curChannel.siteNode.channelPageCreateThreadInfo.createdPageCount.ToString() + "}";
                case "2":
                    if (_curChannel.siteNode.contentPageCreateThreadInfo.state == CreateThreadState.YES)
                    {
                        return "{\"state\":\"" + _curChannel.siteNode.contentPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.contentPageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.contentPageCreateThreadInfo.createdPageCount.ToString() + "}";
                    }
                   int _contentPageCount=toSetContentCreate();
                   return "{\"state\":\"" + _curChannel.siteNode.contentPageCreateThreadInfo.state.ToString() + "\",\"all\":" + _contentPageCount + ",\"cur\":" + _curChannel.siteNode.contentPageCreateThreadInfo.createdPageCount.ToString() + "}";
                default:
                    if (_curChannel.siteNode.singlePageCreateThreadInfo.state == CreateThreadState.YES)
                    {
                        return "{\"state\":\"" + _curChannel.siteNode.singlePageCreateThreadInfo.state.ToString() + "\",\"all\":" + _curChannel.siteNode.singlePageCreateThreadInfo.pageCount.ToString() + ",\"cur\":" + _curChannel.siteNode.singlePageCreateThreadInfo.createdPageCount.ToString() + "}";
                    }
                    int _createCount=toSetSinglePageCreate();
                    return "{\"state\":\"" + _curChannel.siteNode.singlePageCreateThreadInfo.state.ToString() + "\",\"all\":" + _createCount + ",\"cur\":" + _curChannel.siteNode.singlePageCreateThreadInfo.createdPageCount.ToString() + "}";
            }            
            return "";
        }
        private DataTable getTemplateTable()
        {
            string sql = null;
            switch (pageStyle)
            {
                case "0":
                    sql = "select  template.TemplateFilePath,template.CreatedFileFullName ,template.CreatedFileExtName,template.Charset  from SN_Template as template,SN_TemplateProject as project  where template.TemplateProjectID=@projectid and  project.IndexTemplateID=template.TemplateID";
                    break;
                case "1":
                    sql = " select templateMatch.nodeid,template.TemplateFilePath, template.charset,templateMatch.ChannelFilePathRule,templateMatch.ChannelTemplateID from SN_Template as template,SN_TemplateMatch as templateMatch where templateMatch.ProjectID=@ProjectID and templateMatch.nodeid in( " + nodeList + " ) and templateMatch.ChannelTemplateID=template.TemplateID";
                    break;
                case "2":
                    sql = "select templateMatch.nodeid,template.TemplateFilePath, template.charset,templateMatch.ContentFilePathRule,templateMatch.ContentTemplateID  from SN_Template as template,SN_TemplateMatch as templateMatch where templateMatch.ProjectID=@ProjectID and templateMatch.nodeid in( " + nodeList + " ) and templateMatch.ContentTemplateID=template.TemplateID";
                    break;
                default:
                    sql = " select templateId,TemplateFilePath,CreatedFileFullName, charset,CreatedFileExtName from SN_Template  where TemplateProjectID=@ProjectID and TemplateID in( " + nodeList + " ) and TemplateStyle>=3";
                    break;

            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction tran = dbConn.BeginTransaction();
            DbCommand cmd = DataHelper.GetSqlStringCommand(sql);
            DataHelper.SetParameterValue(cmd, labelParams);
            cmd.Connection = dbConn;
            cmd.Transaction = tran;
            DataTable dt = DataHelper.ExecuteDataTable(cmd);
            tran.Commit();
            tran.Dispose();
            dbConn.Close();
            return dt;
        }
        private PageInfo getPageInfo(WebContext XNLPage)
        {
            PageInfo pageinfo=null;
            XNLPage.accessType = AccessType.Static;
            //pageinfo = ParseEngine.ParseAndPageInfo(templateContent, true, XNLPage); //先注释 通过编译
            return pageinfo;
        }
        private void createPageTest(PageInfo pageinfo)
        {
            if (pageinfo.pageColls != null && pageinfo.pageColls.Count > 0)
            {
                //有其它页要生成
                List<int> pageList = new List<int>();
                string requestList = "";
                int i = 0;
                foreach (KeyValuePair<string, string> pinfo in pageinfo.pageColls)
                {
                    pageList.Add(Convert.ToInt32(pinfo.Value));
                    if (i == 0)
                    {
                        requestList += pinfo.Key;
                    }
                    else
                    {
                        requestList += "," + pinfo.Key;
                    }
                    i++;
                }
                List<string> pagesList = setPageNum(0, pageList.Count - 1, pageList);
                //遍历生成文件
                switch (pageStyle)
                {
                    case "1":
                        _curChannel.channelPageCreateThreadInfo.pageCount += pagesList.Count;
                        break;
                    case "2":
                        _curChannel.contentPageCreateThreadInfo.pageCount += pagesList.Count;
                        break;
                    default:
                        _curChannel.singlePageCreateThreadInfo.pageCount += pagesList.Count;
                        break;
                }
            }
            else
            {
                switch (pageStyle)
                {
                    case "1":
                        _curChannel.channelPageCreateThreadInfo.pageCount +=1;
                        break;
                    case "2":
                        _curChannel.contentPageCreateThreadInfo.pageCount += 1;
                        break;
                   default:
                        _curChannel.singlePageCreateThreadInfo.pageCount += 1;
                        break;
                }
            }
        }
        private int createPage(PageInfo pageinfo)
        {
            if (pageinfo.pageColls != null && pageinfo.pageColls.Count > 0)
            {
                //有其它页要生成
                List<int> pageList = new List<int>();
                string requestList = "";
                int i = 0;
                foreach (KeyValuePair<string, string> pinfo in pageinfo.pageColls)
                {
                    pageList.Add(Convert.ToInt32(pinfo.Value));
                    if (i == 0)
                    {
                        requestList += pinfo.Key;
                    }
                    else
                    {
                        requestList += "," + pinfo.Key;
                    }
                    i++;
                }
                List<string> pagesList = setPageNum(0, pageList.Count - 1, pageList);
                //遍历生成文件
                  switch (pageStyle)
                    {
                        case "0":
                            _curChannel.indexPageCreateThreadInfo.reSet();
                            _curChannel.indexPageCreateThreadInfo.setState(CreateThreadState.YES);
                            _curChannel.indexPageCreateThreadInfo.pageCount = pagesList.Count;
                            break;
                    }
                for (int k = 0; k < pagesList.Count; k++)
                {
                    string pageMsg = pagesList[k].Replace(",", "-");
                    string createPath = this.ruleTrueFullPath + "_" + pageMsg;
                    string[] t_page = pagesList[k].Split(',');
                    string[] tn_page = requestList.Split(',');
                    int pageAddNum = 0;
                    Dictionary<string, string> pageRequestColls = new Dictionary<string, string>();
                    for (int t = 0; t < t_page.Length; t++)
                    {
                        pageAddNum += Convert.ToInt32(t_page[t]) - 1;
                        pageRequestColls.Add(tn_page[t], t_page[t]);
                    }
                    if (pageAddNum == 0) createPath = this.ruleTrueFullPath;
                    ParseInfo parseinfo = new ParseInfo();
                    parseinfo.charset = charset;
                    parseinfo.createPath = (createPath + fileExtName);
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.pageRequestColls = pageRequestColls;
                    parseinfo.pageName = this.fileName;
                    parseinfo.templateContent = templateContent;
                    parseinfo.pageMsg = pageMsg;
                    parseinfo.pageStyle = pageStyle;
                    parseinfo.curChannel = _curChannel;
                    parseinfo.myTagColls = pageinfo.myTagColls;
                    parseinfo.XNLPage = XNLPage;
                    string _tmpPageNumMsg = (k > 0 ? pageMsg : "");
                    parseinfo.pagePath = (ruleTruePath + (k > 0 ? "_" : "") + (k > 0 ? pageMsg : "") + fileExtName);
                    parseinfo.XNLPage = XNLPage;
                    parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    switch (pageStyle)
                    {
                        case "0":
                            _curChannel.indexPageCreateThreadInfo.addParseInfo(parseinfo);
                            break;
                        case "1":
                            _curChannel.channelPageCreateThreadInfo.addParseInfo(parseinfo);
                            break;
                        default:
                            _curChannel.singlePageCreateThreadInfo.addParseInfo(parseinfo);
                            break;
                    }
                }
                switch (pageStyle)
                {
                    case "0":
                        ParseInfo parseinfo = _curChannel.indexPageCreateThreadInfo.getWhileCreateParseInfo();
                        if (parseinfo != null)AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo);
                        //if (parseinfo != null) _curChannel.indexPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo));
                        break;
                    case "1":
                        ParseInfo parseinfo2 = _curChannel.channelPageCreateThreadInfo.getWhileCreateParseInfo();
                        if (parseinfo2 != null)AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo2);
                        //if (parseinfo2 != null) _curChannel.channelPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo2));
                        break;
                    default:
                       ParseInfo parseinfo3 = _curChannel.singlePageCreateThreadInfo.getWhileCreateParseInfo();
                       if (parseinfo3 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3);
                       //if (parseinfo3 != null) _curChannel.singlePageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3));
                       break;
                }
                return pagesList.Count;
            }
            else  //只有一页
            {
                ParseInfo parseinfo = new ParseInfo();
                parseinfo.charset = charset;
                parseinfo.createPath = (this.ruleTrueFullPath + fileExtName);
                parseinfo.fileExtName = fileExtName;
                parseinfo.pageName = this.fileName;
                parseinfo.templateContent = templateContent;
                parseinfo.pageStyle = pageStyle;
                parseinfo.curChannel = _curChannel;
                parseinfo.XNLPage = XNLPage;
                parseinfo.pagePath = XNLPage.pagePath;
                parseinfo.myTagColls = pageinfo.myTagColls;
                parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                var _pageStyle=Convert.ToInt32(pageStyle);
                if (_pageStyle > 0)
                {
                    switch(_pageStyle)
                    {
                        case 1:
                            AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo);
                            //_curChannel.channelPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo));
                            break;
                        default:
                            AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo);
                            //_curChannel.contentPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo));
                            break;
                    }
                }
                else
                {
                    RequestParse.parse(parseinfo);
                }
                return 1;
            }
        }
        private List<string> setPageNum(int pageId, int maxPageId, List<int> pageList)
        {
            List<string> pagesList = new List<string>();
            if (pageId == maxPageId)
            {
                for (int i = 1; i <= pageList[pageId]; i++)
                {
                    pagesList.Add(i.ToString());
                }
                return pagesList;
            }
            else
            {
                List<string> tmp_array = setPageNum(pageId + 1, maxPageId, pageList);
                for (var i = 1; i <= pageList[pageId]; i++)
                {
                    for (var j = 0; j < tmp_array.Count; j++)
                    {
                        pagesList.Add(i.ToString() + "," + tmp_array[j]);
                    }
                }
                return pagesList;
            }
        }
        private DataTable getContentList(Cchannel tmpChannel,string whereStr)
        {
            string sql = "select ID from " + tmpChannel.model.TableName + " where NodeID=" + tmpChannel.nodeID+whereStr;
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction tran = dbConn.BeginTransaction();
            DbCommand cmd = DataHelper.GetSqlStringCommand(sql);
            cmd.Connection = dbConn;
            cmd.Transaction = tran;
            DataTable dt = DataHelper.ExecuteDataTable(cmd);
            tran.Commit();
            tran.Dispose();
            dbConn.Close();
            return dt;
        }
        private DataTable getContentList(Cchannel tmpChannel, string whereStr,int topNum)
        {
            string sql = "select top "+topNum.ToString()+" ID from " + tmpChannel.model.TableName + " where NodeID=" + tmpChannel.nodeID + whereStr;
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction tran = dbConn.BeginTransaction();
            DbCommand cmd = DataHelper.GetSqlStringCommand(sql);
            cmd.Connection = dbConn;
            cmd.Transaction = tran;
            DataTable dt = DataHelper.ExecuteDataTable(cmd);
            tran.Commit();
            tran.Dispose();
            dbConn.Close();
            return dt;
        }
        private DataTable getContentList(Cchannel tmpChannel, string whereStr, int pageNum,int pageRecoder)
        {
            if (!labelParams.ContainsKey("primarykey"))labelParams.Add("primarykey", new XNLParam(XNLType.String, "id"));
            if (!labelParams.ContainsKey("perpagerecordsnum"))
            {
                labelParams.Add("perpagerecordsnum", new XNLParam(XNLType.Int32, pageRecoder));
            }
            else
            {
                labelParams["perpagerecordsnum"].value = pageRecoder;
            }
            if (!labelParams.ContainsKey("curpagenum")) labelParams.Add("curpagenum", new XNLParam(XNLType.Int32, pageNum));
            string sql = "select ID from " + tmpChannel.model.TableName + " where NodeID=" + tmpChannel.nodeID + whereStr+ " order by id desc";
            string countsql = "select count(ID) from " + tmpChannel.model.TableName + " where NodeID=" + tmpChannel.nodeID + whereStr ;
            if(!labelParams.ContainsKey("countsql"))
            {
                labelParams.Add("countsql", new XNLParam(XNLType.String, countsql));
            }else
            {
                labelParams["countsql"].value = countsql;
            }
            
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction tran = dbConn.BeginTransaction();
            DbCommand cmd = DataHelper.GetSqlStringCommand(sql);
            cmd.Connection = dbConn;
            cmd.Transaction = tran;
            DataTable dt = DataHelper.GetDataTableBySqlWithPage(cmd, labelParams);
            tran.Commit();
            tran.Dispose();
            dbConn.Close();
            return dt;
        }
        private int toSetChannelCreate()
        {
            DataTable t_dt = getTemplateTable();
            Dictionary<int, PageInfo> pageInfoColls = new Dictionary<int, PageInfo>();
            Dictionary<int, string> templateColls = new Dictionary<int, string>();
            int _pageCount = 0;
            _curChannel.channelPageCreateThreadInfo.reSet();
            _curChannel.channelPageCreateThreadInfo.setState(CreateThreadState.YES);
            foreach (DataRow row2 in t_dt.Rows)
            {
                int nodeId = Convert.ToInt32(row2["nodeid"]);
                _curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
                XNLPage.curChannel = _curChannel;
                int templateId = Convert.ToInt32(row2["ChannelTemplateID"]);
                rulePath = Convert.ToString(row2["ChannelFilePathRule"]);
                fileExtName = CMSUtils.getExtName(rulePath);
                if (!templateColls.ContainsKey(templateId))
                {
                    templatePath = Convert.ToString(row2["TemplateFilePath"]);
                    charset = Convert.ToString(row2["charset"]);
                    string templateTruePath = XNLPage.Context.Server.MapPath("~" + templatePath.Replace("@", _curChannel.siteNode.siteWebPath));
                    templateContent = XNLBaseCommon.loadTemplete(templateTruePath, charset);
                    templateColls.Add(templateId, templateContent);
                }
                else
                {
                    templateContent = templateColls[templateId];
                }
                if (fileExtName.ToLower() != ".aspx") //静态
                {
                    XNLPage.accessType = AccessType.Static;
                    XNLPage.pagePath = ruleTruePath + fileExtName;
                    PageInfo pageinfo = getPageInfo(XNLPage);
                    createPageTest(pageinfo);
                    pageInfoColls.Add(nodeId, pageinfo);
                }
                else
                {
                    _curChannel.channelPageCreateThreadInfo.pageCount += 1;
                }
            }
            foreach (DataRow row2 in t_dt.Rows)
            {
                int nodeId = Convert.ToInt32(row2["nodeid"]);
                _curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
                XNLPage.curChannel = _curChannel;
                rulePath = Convert.ToString(row2["ChannelFilePathRule"]);
                int templateId = Convert.ToInt32(row2["ChannelTemplateID"]);
                rulePath = CMSUtils.getAbsolutePath(rulePath).Replace("[#ChannelID]", nodeId.ToString());
                charset = Convert.ToString(row2["charset"]);
                fileExtName = CMSUtils.getExtName(rulePath);
                rulePath = rulePath.Replace(fileExtName, "");
                string[] path_array2 = rulePath.Split('/');
                fileName = path_array2[path_array2.Length - 1];
                ruleTruePath = rulePath.Replace("@", _curChannel.siteNode.siteWebPath).Replace("//", "/");
                ruleTrueFullPath = XNLPage.Context.Server.MapPath("~" + ruleTruePath);
                string dirPath = (ruleTrueFullPath + ".tempext").Replace(fileName + ".tempext", "");
                CMSUtils.CreateDirectory(dirPath);
                templateContent = templateColls[templateId];
                XNLPage.pagePath = ruleTruePath + fileExtName;
                if (fileExtName.ToLower() != ".aspx") //静态
                {
                    XNLPage.accessType = AccessType.Static;
                    PageInfo pageinfo = pageInfoColls[nodeId];
                    _pageCount+=createPage(pageinfo);
                }
                else //动态页
                {
                    ParseInfo parseinfo = new ParseInfo();
                    parseinfo.charset = charset;
                    parseinfo.createPath = (this.ruleTrueFullPath + fileExtName);
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.pageName = this.fileName;
                    parseinfo.templateContent = templateContent;
                    parseinfo.pageStyle = pageStyle;
                    parseinfo.curChannel = _curChannel;
                    parseinfo.pagePath = XNLPage.pagePath;
                    parseinfo.XNLPage = XNLPage;
                    parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    _curChannel.channelPageCreateThreadInfo.addParseInfo(parseinfo);
                    _pageCount += 1;
                    if(_pageCount==1)
                    {
                        ParseInfo parseinfo2 = _curChannel.channelPageCreateThreadInfo.getWhileCreateParseInfo();
                        if (parseinfo2 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo2);// _curChannel.channelPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo2));
                    }
                }
            }
            if (_pageCount == 0) _pageCount = 1;
            return _pageCount;
        }
        private int toSetContentCreate()
        {
            int _pageCount = 0;
            DataTable t_dt = getTemplateTable();
            string typeStr = labelParams["type"].value.ToString();
            string startDate = labelParams["startdate"].value.ToString();
            string endDate = labelParams["enddate"].value.ToString();
            string startId = labelParams["startid"].value.ToString();
            string endId = labelParams["endid"].value.ToString();
            string contendId = labelParams["contendid"].value.ToString();
            string whereStr = getContentWhereStr(typeStr, startDate, endDate, startId, endId, contendId);
            _curChannel.contentPageCreateThreadInfo.reSet();
            _curChannel.contentPageCreateThreadInfo.setState(CreateThreadState.YES);
            _curChannel.contentPageCreateThreadInfo.whereStr = whereStr;
            _curChannel.contentPageCreateThreadInfo.templateColls = new Dictionary<int, string>();
            Dictionary<int, string> templateColls = _curChannel.contentPageCreateThreadInfo.templateColls;
            foreach (DataRow row2 in t_dt.Rows)
            {
                int nodeId = Convert.ToInt32(row2["nodeid"]);
                _curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
                NodeContentCreateSet contentCreateSet = new NodeContentCreateSet();
                contentCreateSet.nodeId = nodeId;
                contentCreateSet.charset = Convert.ToString(row2["charset"]);
                int templateId = Convert.ToInt32(row2["ContentTemplateID"]);
                string _tmpContentPathRule = Convert.ToString(row2["ContentFilePathRule"]);
                fileExtName = CMSUtils.getExtName(_tmpContentPathRule);
                contentCreateSet.fileExtName = fileExtName;
                if (!templateColls.ContainsKey(templateId))
                {
                    templatePath = Convert.ToString(row2["TemplateFilePath"]);
                    string _tmpTemplatePath = XNLPage.Context.Server.MapPath("~" + templatePath.Replace("@", _curChannel.siteNode.siteWebPath));
                    templateContent = XNLBaseCommon.loadTemplete(_tmpTemplatePath,contentCreateSet.charset);
                    templateColls.Add(templateId, templateContent);
                }
                else
                {
                    templateContent = templateColls[templateId];
                }
                contentCreateSet.templateId = templateId;
                contentCreateSet.contentPathRule = (CMSUtils.getAbsolutePath(_tmpContentPathRule).Replace("[#ChannelID]", nodeId.ToString())).Replace("@", _curChannel.siteNode.siteWebPath).Replace("//", "/"); ;
                contentCreateSet.contentDirPathRule = XNLPage.Context.Server.MapPath("~" + contentCreateSet.contentPathRule);
                XNLPage.curChannel = _curChannel;
                XNLPage.accessType = AccessType.Static;
                XNLPage.pagePath = "test"+ fileExtName;
                if (fileExtName.ToLower() != ".aspx") //静态
                {
                    DataTable contentDt = getContentList(_curChannel, whereStr, 1,600);
                    if (contentDt.Rows.Count > 0)
                    {
                        //if (_pageCount == 0)
                        //{
                        XNLPage.contentId = Convert.ToInt32(contentDt.Rows[0]["id"]);
                        XNLPage.items = null;
                        PageInfo pageinfo = null;//ParseEngine.ParseAndPageInfo(templateContent, true, XNLPage);  //先注释 通过编译
                        contentCreateSet.myTagColls = pageinfo.myTagColls;
                        if (XNLPage.items != null && XNLPage.items.ContainsKey("@_c_f")) contentCreateSet.contentFieldList = (List<string>)XNLPage.items["@_c_f"];//@_c_f,设置内容页字段列表
                        //}
                        int allPage=Convert.ToInt32(labelParams["totalpagesnum"].value);
                        contentCreateSet.allPage =(allPage>0?allPage:1) ;
                        contentCreateSet.allContentPage = Convert.ToInt32(labelParams["totalrecordsnum"].value);
                        _pageCount += contentCreateSet.allContentPage;
                    }
                 }
                 else //生成动态页
                 {
                     DataTable contentDt = getContentList(_curChannel, whereStr, 1);
                     if (contentDt.Rows.Count > 0)
                     {
                         XNLPage.contentId = Convert.ToInt32(contentDt.Rows[0]["id"]);
                         XNLPage.items = null;
                         PageInfo pageinfo = null; //ParseEngine.ParseAndPageInfo(templateContent, true, XNLPage); //先注释 通过编译
                         contentCreateSet.myTagColls = pageinfo.myTagColls;
                         if (XNLPage.items != null && XNLPage.items.ContainsKey("@_c_f")) contentCreateSet.contentFieldList = (List<string>)XNLPage.items["@_c_f"];//@_c_f,设置内容页字段列表
                         _pageCount += 1;
                         contentCreateSet.allPage = 1;
                         contentCreateSet.allContentPage = 1;
                     }
                  }
                if (contentCreateSet.allContentPage > 0)
                {
                    contentCreateSet.addCurPage();
                    _curChannel.contentPageCreateThreadInfo.addNodeCreateAttrs(nodeId, contentCreateSet);
                    _curChannel.contentPageCreateThreadInfo.addNodeToQueue(nodeId);
                }
            }
            if (_pageCount > 0)
            {
                _curChannel.contentPageCreateThreadInfo.XNLPage = XNLPage;
                _curChannel.contentPageCreateThreadInfo.pageCount = _pageCount;
                toSetCreateContentPage(_curChannel.contentPageCreateThreadInfo.getCurCreateNode());
            }
            else
            {
                _curChannel.contentPageCreateThreadInfo.reSet();
            }
            return _pageCount;
        }
        public void toSetCreateContentPage(int nodeId)
        {
            _curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
            ContentPageCreateThreadInfo threadInfo = _curChannel.contentPageCreateThreadInfo;
            XNLPage = threadInfo.XNLPage;
            XNLPage.curChannel = _curChannel;
            NodeContentCreateSet nodeSetting = threadInfo.getNodeSetting(nodeId);
            templateContent = threadInfo.templateColls[nodeSetting.templateId];
            if(nodeSetting.fileExtName!=".aspx")
            {
                DataTable dt = getContentList(_curChannel, threadInfo.whereStr, nodeSetting.curPage,600);
                foreach (DataRow contentRow in dt.Rows)
                {
                    rulePath = nodeSetting.contentPathRule;
                    fileExtName = nodeSetting.fileExtName;
                    string _tmpPagePath = rulePath.Replace("[#ContentID]", contentRow["id"].ToString());
                    string ruleTrueFullPath = nodeSetting.contentDirPathRule.Replace("[#ContentID]", contentRow["id"].ToString());
                    if (!fileExtName.Equals(""))
                    {
                        _tmpPagePath = _tmpPagePath.Replace(fileExtName, "");
                        ruleTrueFullPath = ruleTrueFullPath.Replace(fileExtName, "");
                    }
                    string[] path_array2 = _tmpPagePath.Split('/');
                    fileName = path_array2[path_array2.Length - 1];
                    //检查是否建立生成文件的目录
                    string dirPath = (ruleTrueFullPath + ".tempext").Replace(fileName + ".tempext", "");
                    CMSUtils.CreateDirectory(dirPath);
                    XNLPage.contentId = Convert.ToInt32(contentRow["id"]);
                    XNLPage.accessType = AccessType.Static;
                    XNLPage.pagePath = ruleTruePath + fileExtName;
                    ParseInfo parseinfo = new ParseInfo();
                    parseinfo.charset = nodeSetting.charset;
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.createPath = (ruleTrueFullPath + fileExtName);
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.pageName = this.fileName;
                    parseinfo.templateContent = templateContent;
                    parseinfo.pageStyle = "2";
                    parseinfo.contentId = Convert.ToInt32(contentRow["id"]);
                    parseinfo.pagePath = _tmpPagePath + fileExtName;
                    parseinfo.curChannel = _curChannel;
                    parseinfo.XNLPage = XNLPage;
                    parseinfo.myTagColls = nodeSetting.myTagColls;
                    parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    _curChannel.contentPageCreateThreadInfo.addParseInfo(parseinfo);
                }
            }
            else if (nodeSetting.fileExtName==".aspx")
            {
                rulePath = nodeSetting.contentPathRule;
                fileExtName = ".aspx";
                string _tmpPagePath = rulePath.Replace("[#ContentID]", "content");
                string ruleTrueFullPath = nodeSetting.contentDirPathRule.Replace("[#ContentID]", "content");
                if (!fileExtName.Equals(""))
                {
                    _tmpPagePath = _tmpPagePath.Replace(fileExtName, "");
                    ruleTrueFullPath = ruleTrueFullPath.Replace(fileExtName, "");
                }
                string[] path_array2 = _tmpPagePath.Split('/');
                fileName = path_array2[path_array2.Length - 1];
                //检查是否建立生成文件的目录
                string dirPath = (ruleTrueFullPath + ".tempext").Replace(fileName + ".tempext", "");
                CMSUtils.CreateDirectory(dirPath);
                XNLPage.accessType = AccessType.Static;
                XNLPage.pagePath = _tmpPagePath + fileExtName;
                ParseInfo parseinfo = new ParseInfo();
                parseinfo.charset = nodeSetting.charset;
                parseinfo.fileExtName = fileExtName;
                parseinfo.createPath = (ruleTrueFullPath + fileExtName);
                parseinfo.fileExtName = fileExtName;
                parseinfo.pageName = this.fileName;
                parseinfo.templateContent = templateContent;
                parseinfo.pageStyle = "2";
                parseinfo.pagePath = _tmpPagePath + fileExtName;
                parseinfo.curChannel = _curChannel;
                parseinfo.XNLPage = XNLPage;
                parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                _curChannel.contentPageCreateThreadInfo.addParseInfo(parseinfo);
            }
            ParseInfo parseinfo3 = _curChannel.contentPageCreateThreadInfo.getWhileCreateParseInfo();
            if (parseinfo3 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3);// _curChannel.contentPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3));
        }
        private int toSetSinglePageCreate()
        {
            var _pageCount = 0;
            _curChannel.singlePageCreateThreadInfo.reSet();
            _curChannel.singlePageCreateThreadInfo.setState(CreateThreadState.YES);
            DataTable t_dt = getTemplateTable();
            Dictionary<int, PageInfo> pageInfoColls = new Dictionary<int, PageInfo>();
            Dictionary<int, string> templateColls = new Dictionary<int, string>();
            foreach (DataRow row2 in t_dt.Rows)
            {
                int templateId = Convert.ToInt32(row2["TemplateID"]);
                templatePath = Convert.ToString(row2["TemplateFilePath"]);
                rulePath = Convert.ToString(row2["CreatedFileFullName"]);
                charset = Convert.ToString(row2["charset"]);
                fileExtName = Convert.ToString(row2["CreatedFileExtName"]);
                string templateTruePath = XNLPage.Context.Server.MapPath("~" + templatePath.Replace("@", _curChannel.siteNode.siteWebPath));
                templateContent = XNLBaseCommon.loadTemplete(templateTruePath, charset);
                templateColls.Add(templateId, templateContent);
                if (fileExtName.ToLower() != ".aspx") //静态
                {
                    XNLPage.accessType = AccessType.Static;
                    XNLPage.pagePath = rulePath + fileExtName;
                    PageInfo pageinfo = getPageInfo(XNLPage);
                    createPageTest(pageinfo);
                    pageInfoColls.Add(templateId, pageinfo);
                }
                else
                {
                    _curChannel.singlePageCreateThreadInfo.pageCount += 1;
                }
            }
            foreach (DataRow row2 in t_dt.Rows)
            {
                int templateId = Convert.ToInt32(row2["TemplateID"]);
                rulePath = Convert.ToString(row2["CreatedFileFullName"]);
                rulePath = CMSUtils.getAbsolutePath(rulePath).Replace("[#ChannelID]", _curChannel.nodeID.ToString());
                charset = Convert.ToString(row2["charset"]);
                fileExtName = Convert.ToString(row2["CreatedFileExtName"]);
                string[] path_array2 = rulePath.Split('/');
                fileName = path_array2[path_array2.Length - 1];
                ruleTruePath = rulePath.Replace("@", _curChannel.siteNode.siteWebPath).Replace("//", "/");
                ruleTrueFullPath = XNLPage.Context.Server.MapPath("~" + ruleTruePath);
                string dirPath = (ruleTrueFullPath + ".tempext").Replace(fileName + ".tempext", "");
                CMSUtils.CreateDirectory(dirPath);
                templateContent = templateColls[templateId];
                if (fileExtName.ToLower() != ".aspx") //静态
                {
                    XNLPage.accessType = AccessType.Static;
                    XNLPage.pagePath = ruleTruePath + fileExtName;
                    PageInfo pageinfo = pageInfoColls[templateId];
                   _pageCount+= createPage(pageinfo);
                }
                else
                {
                    _pageCount += 1;
                    ParseInfo parseinfo = new ParseInfo();
                    parseinfo.charset = charset;
                    parseinfo.createPath = (this.ruleTrueFullPath + fileExtName);
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.pageName = this.fileName;
                    parseinfo.templateContent = templateContent;
                    parseinfo.pageStyle = pageStyle;
                    parseinfo.curChannel = _curChannel;
                    parseinfo.pagePath = ruleTruePath + fileExtName;
                    parseinfo.XNLPage = XNLPage;
                    parseinfo.windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    _curChannel.singlePageCreateThreadInfo.addParseInfo(parseinfo);
                    if(_pageCount==1)
                    {
                        ParseInfo parseinfo3 = _curChannel.singlePageCreateThreadInfo.getWhileCreateParseInfo();
                        if (parseinfo3 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3);// _curChannel.singlePageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3));
                    }
                }
            }
            if (_pageCount == 0) _pageCount = 1;
            return _pageCount;
        }
        private string getContentWhereStr(string typeStr,string startDate,string endDate,string startId,string endId,string someId)
        {
            string whereStr = " and isRecycle=0 and State=99 ";
            switch(typeStr)
            {
                case "date":
                    string dateTypeStr = "'";
                    if (DataHelper.getDataBaseType().ToLower() == "access") dateTypeStr = "#";
                    
                    DateTime fromDate;
                    DateTime toDate;
                    var isStart=DateTime.TryParse(startDate, out fromDate);
                    var isEnd=DateTime.TryParse(endDate, out toDate);
                    if (isStart && isEnd)
                    {
                        if(startDate.CompareTo(toDate)>0)
                        {
                            whereStr += "and ([AddDate]>="+dateTypeStr+startDate+dateTypeStr+" and [AddDate]<="+dateTypeStr+ endDate+dateTypeStr+")";
                        }else
                        {
                            whereStr += "and ([AddDate]>=" + dateTypeStr + endDate + dateTypeStr + " and [AddDate]<=" + dateTypeStr + startDate + dateTypeStr + ")";
                        }
                        
                    }
                    else if (isStart)
                    {
                        whereStr += "and [AddDate]=" + dateTypeStr+startDate+dateTypeStr;
                    }
                    else if (isEnd)
                    {
                        whereStr += "and [AddDate]=" + dateTypeStr + endDate + dateTypeStr;
                    }
                    break;
                case "id":
                    int fromId;
                    int toId;
                    var isStartId = int.TryParse(startId, out fromId);
                    var isEndId = int.TryParse(endId, out toId);
                    if (isStartId && isEndId)
                    {
                        if (toId > fromId)
                        {
                            whereStr += "and ([id]>=" + fromId + " and [id]<=" + toId + ")";
                        }
                        else
                        {
                            whereStr += "and ([id]>=" + toId + " and [id]<=" + fromId + ")";
                        }

                    }
                    else if (isStartId)
                    {
                        whereStr += "and [id]=" + fromId;
                    }
                    else if (isEndId)
                    {
                        whereStr += "and [id]=" + toId;
                    }
                    break;
                case "someid":
                    someId = someId.Trim();
                    if(!someId.Equals(""))
                    {
                        string _tmpSomeId = someId.Replace(",", "");
                        if(UtilsCode.IsInt(_tmpSomeId))
                        {
                            whereStr += "and [id] in(" + someId + ")";
                        }
                    }
                    break;
            }
            return whereStr;
        }
    }
  public class RequestParse
   {
       public static void parse(object info)
       {
           ParseInfo _info = (ParseInfo)info;
           try
           {
               System.Security.Principal.WindowsIdentity.Impersonate(_info.windowsIdentity.Token);
               COM.SingNo.Web.BasePage XNLPage = new COM.SingNo.Web.BasePage();
               XNLPage.curChannel = _info.curChannel;
               XNLPage.Context = _info.XNLPage.Context;
               XNLPage.contentId = _info.contentId;
               XNLPage.pagePath = _info.pagePath;
               XNLPage.templateProjectId = _info.XNLPage.templateProjectId;
               _info.XNLPage = XNLPage;
               if (_info.fileExtName.Equals(".aspx")) //动态页
               {
                   string dynamicTemplate = XNLWebCommon.loadDynamicTemplate();
                   if (_info.pageStyle.Equals("2"))
                   {
                       NodeContentCreateSet _createSet = _info.curChannel.contentPageCreateThreadInfo.getNodeSetting(_info.curChannel.contentPageCreateThreadInfo.getCurCreateNode());
                       if (_createSet.contentFieldList != null)
                       {
                           _info.XNLPage.items = new Dictionary<string, object>(1);
                           _info.XNLPage.items.Add("@_c_f", _createSet.contentFieldList);
                       }
                   }
                   string templateStr = XNLWebCommon.getDynamicPage(dynamicTemplate, _info.templateContent, _info);
                   XNLWebCommon.writeDynamicPage(_info.createPath, templateStr);
               }
               else
               {
                   XNLPage.accessType = AccessType.Static;
                   if (!_info.pageStyle.Equals("2"))
                   {
                       //暂时注释，先通过编译
                       //string responseStr = XNLParseEngine.Parse(_info.templateContent, true, _info.XNLPage, _info);
                       //UtilsCode.writeFile(_info.createPath, _info.charset, responseStr);
                   }
               }
               switch (_info.pageStyle)
               {
                   case "0":
                       _info.curChannel.indexPageCreateThreadInfo.addCreatePage();
                       if (_info.curChannel.indexPageCreateThreadInfo.pageCount == _info.curChannel.indexPageCreateThreadInfo.createdPageCount)
                       {
                           //生成完成
                           _info.curChannel.indexPageCreateThreadInfo.reSet();
                       }
                       Thread.Sleep(35);
                       ParseInfo parseInfo = _info.curChannel.indexPageCreateThreadInfo.getWhileCreateParseInfo();
                       if (parseInfo != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo);// _info.curChannel.indexPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo));
                       break;
                   case "1":
                       _info.curChannel.channelPageCreateThreadInfo.addCreatePage();
                       if (_info.curChannel.channelPageCreateThreadInfo.pageCount == _info.curChannel.channelPageCreateThreadInfo.createdPageCount)
                       {
                           //生成完成
                           _info.curChannel.channelPageCreateThreadInfo.reSet();
                       }
                       Thread.Sleep(35);
                       ParseInfo parseInfo2 = _info.curChannel.channelPageCreateThreadInfo.getWhileCreateParseInfo();
                       if (parseInfo2 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo2);// _info.curChannel.channelPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo2));
                       break;
                   case "2":
                       ContentPageCreateThreadInfo threadInfo = _info.curChannel.contentPageCreateThreadInfo;
                       NodeContentCreateSet createSet = threadInfo.getNodeSetting(threadInfo.getCurCreateNode());
                       if (!_info.fileExtName.Equals(".aspx")) //静态页
                       {
                           if (createSet.contentFieldList != null)
                           {
                               _info.XNLPage.items = new Dictionary<string, object>(2);
                               _info.XNLPage.items.Add("@_c_f", createSet.contentFieldList);
                           }
                           //得到生成页共几个分页的信息
                           PageInfo pageInfo = null;//ParseEngine.ParseAndPageInfo(_info.templateContent, true, _info.XNLPage);  //先注释 通过编译
                           if (pageInfo.pageColls != null && pageInfo.pageColls.Count > 0) //
                           {
                               ///////////////
                               //有其它页要生成
                               List<int> pageList = new List<int>();
                               string requestList = "";
                               int i = 0;
                               foreach (KeyValuePair<string, string> pinfo in pageInfo.pageColls)
                               {
                                   pageList.Add(Convert.ToInt32(pinfo.Value));
                                   if (i == 0)
                                   {
                                       requestList += pinfo.Key;
                                   }
                                   else
                                   {
                                       requestList += "," + pinfo.Key;
                                   }
                                   i++;
                               }
                               List<string> pagesList = setPageNum(0, pageList.Count - 1, pageList);
                               //遍历生成文件
                               string createPath = _info.createPath.Replace(_info.fileExtName, "");
                               string _pagePath = _info.pagePath.Replace(_info.fileExtName, "");
                               for (int k = 0; k < pagesList.Count; k++)
                               {
                                   string pageMsg = pagesList[k].Replace(",", "-");
                                   string _createPath = createPath + "_" + pageMsg;
                                   string[] t_page = pagesList[k].Split(',');
                                   string[] tn_page = requestList.Split(',');
                                   int pageAddNum = 0;
                                   Dictionary<string, string> pageRequestColls = new Dictionary<string, string>();
                                   for (var t = 0; t < t_page.Length; t++)
                                   {
                                       pageAddNum += Convert.ToInt32(t_page[t]) - 1;
                                       pageRequestColls.Add(tn_page[t], t_page[t]);
                                   }
                                   _info.pageRequestColls = pageRequestColls;
                                   _info.pageMsg = pageMsg;
                                   BasePage XNLPage2 = new BasePage();
                                   if (k > 0)
                                   {
                                       _info.createPath = (_createPath + _info.fileExtName);
                                       XNLPage2.pagePath = _pagePath + "_" + pageMsg + _info.fileExtName;
                                   }
                                   else
                                   {
                                       XNLPage2.pagePath = _info.pagePath;
                                   }
                                   XNLPage2.curChannel = _info.curChannel;
                                   XNLPage2.accessType = AccessType.Static;
                                   XNLPage2.Context = threadInfo.XNLPage.Context;
                                   XNLPage2.contentId = _info.contentId;
                                   XNLPage2.templateProjectId = _info.XNLPage.templateProjectId;
                                   if (createSet.contentFieldList != null)
                                   {
                                       XNLPage2.items = new Dictionary<string, object>(2);
                                       XNLPage2.items.Add("@_c_f", createSet.contentFieldList);
                                   }
                                   _info.XNLPage = XNLPage2;
                                   //暂时注释，先通过编译
                                   //string responseStr = XNLParseEngine.Parse(_info.templateContent, true, XNLPage2, _info);
                                   //UtilsCode.writeFile(_info.createPath, _info.charset, responseStr);
                               }
                           }
                           else
                           {
                               UtilsCode.writeFile(_info.createPath, _info.charset, pageInfo.parseStr);
                           }
                       }
                       threadInfo.addCreatePage();
                       Queue<int> _nodeQueue = threadInfo.nodeQueue;
                       if (_nodeQueue.Count > 0)
                       {
                           int curNode = _nodeQueue.Peek();
                           var createAttr = threadInfo.nodeCreateAttrs[curNode];
                           if (createAttr.curPage == createAttr.allPage && createAttr.curCreatedPage == createAttr.allContentPage)//这栏目全部页生成完成,开始生成下一栏目
                           {
                               _nodeQueue.Dequeue();
                                 if (_nodeQueue.Count > 0)
                                 {
                                     //开始下一栏目生成
                                     new ParseAndCreat().toSetCreateContentPage(threadInfo.getCurCreateNode());
                                }
                            }
                           else if (createAttr.curPage < createAttr.allPage && createAttr.curPageCreatedPage == 600) //此栏目当前分页全部页生成，开始生成下一分页
                            {
                                   createAttr.addCurPage();
                                   new ParseAndCreat().toSetCreateContentPage(threadInfo.getCurCreateNode());
                            }
                           else
                           {
                               Thread.Sleep(35);
                               ParseInfo parseinfo3 = threadInfo.getWhileCreateParseInfo();
                               if (parseinfo3 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3);// _info.curChannel.contentPageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo3));
                            }
                       }
                       if (threadInfo.pageCount == threadInfo.createdPageCount)
                       {
                           //生成完成
                           threadInfo.reSet();
                       }
                       break;
                   default:
                       _info.curChannel.singlePageCreateThreadInfo.addCreatePage();
                       if (_info.curChannel.singlePageCreateThreadInfo.pageCount == _info.curChannel.singlePageCreateThreadInfo.createdPageCount)
                       {
                           //生成完成
                           _info.curChannel.singlePageCreateThreadInfo.reSet();
                       }
                       Thread.Sleep(35);
                       ParseInfo parseInfo3 = _info.curChannel.singlePageCreateThreadInfo.getWhileCreateParseInfo();
                       if (parseInfo3 != null) AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo3);// _info.curChannel.singlePageCreateThreadInfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseInfo3));
                       break;
               }
           }
           catch //(Exception e)
           {
               switch (_info.pageStyle)
               {
                   case "0":
                       if (_info != null && _info.curChannel != null) _info.curChannel.indexPageCreateThreadInfo.cancle();
                       break;
                   case "1":
                       if (_info != null && _info.curChannel != null) _info.curChannel.channelPageCreateThreadInfo.cancle();
                       break;
                   case "2":
                       if (_info != null && _info.curChannel != null) _info.curChannel.contentPageCreateThreadInfo.cancle();
                       break;
                   default:
                       if (_info != null && _info.curChannel != null) _info.curChannel.singlePageCreateThreadInfo.cancle();
                       break;
               }
           }
           finally
           {
               try
               {
                   _info.windowsIdentity.Dispose();
                   _info = null;
               }
               catch {} 
           }
       }
       private static List<string> setPageNum(int pageId, int maxPageId, List<int> pageList)
       {
           List<string> pagesList = new List<string>();
           if (pageId == maxPageId)
           {
               for (int i = 1; i <= pageList[pageId]; i++)
               {
                   pagesList.Add(i.ToString());
               }
               return pagesList;
           }
           else
           {
               List<string> tmp_array = setPageNum(pageId + 1, maxPageId, pageList);
               for (var i = 1; i <= pageList[pageId]; i++)
               {
                   for (var j = 0; j < tmp_array.Count; j++)
                   {
                       pagesList.Add(i.ToString() + "," + tmp_array[j]);
                   }
               }
               return pagesList;
           }
       }
   }
}
