using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.Common;
using COM.SingNo.DAL;
using System.Data;
using System.Web;
using COM.SingNo.XNLCore;
using System.IO;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
 public   class Site:IXNLTag<WebContext>
    {
        public bool _isTagEnd = false;
        //标签是否解析结束
        public bool isTagEnd
        {
            get
            {
                return _isTagEnd;
            }
            set
            {
                _isTagEnd = value;
            }
        }
        // //标签开始,初始化参数等 抛异常初始化失败原因
        public void onInit(params object[] args)
        {
        }
        //子标签开始,初始化参数等 返回空初始化成功 非空或抛异常初始化失败原因
        public void onSubInit(params object[] args)
        {

        }
        //子标签解析
        public string onSubTag(OnTagDelegate<WebContext> tagDelegate)
        {
            return "";
        }
        public void setParam(string paramName, object value)
        {

        }
        public object getParam(string paramName)
        {
            return "";
        }
        public void setParam(string subTagName, string paramName, object value)
        {

        }
        public object getParam(string subTagName, string paramName)
        {
            return "";
        }
        //创建 
        public IXNLTag<WebContext> createNew()
        {
            return null;
        }
        #region IXNLBase 成员
     public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            /*
               labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
               MatchCollection labelColl = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "CMS.Manage", "Site");
               labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, labelColl);
               MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Site.Success");
               MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Site.Error");
               string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
               if (actionStr.Equals("add"))
               {
                   labelContentStr = addSite(labelParams, labelContentStr, matchSuccessItem, matchFailItem);
                   labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
               }
               else if (actionStr.Equals("modify"))
               {
                   //labelContentStr = modelModify(labelParams, labelContentStr, matchSuccessItem, matchFailItem);
               }
               else if (actionStr.Equals("delete"))
               {
                   //labelContentStr = modelDelete(labelParams, labelContentStr, matchSuccessItem, matchFailItem);
               }
               else if (actionStr.Equals("checksite"))
               {
                   labelContentStr = checkSite(labelParams, labelContentStr, matchSuccessItem, matchFailItem);
                   labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
               }
               else
               {
                   Dictionary<string, string> errorList = new Dictionary<string, string>();
                   errorList.Add("1", "参数错误!");
                   labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                   labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
                   return labelContentStr;
               }
             return labelContentStr;
             */
            return "";
        }
        #endregion
     /// <summary>
     /// 添加站点
     /// </summary>
     /// <param name="labelParams"></param>
     /// <param name="labelContentStr"></param>
     /// <param name="matchSuccessItem"></param>
     /// <param name="matchFailItem"></param>
     /// <returns></returns>
        private string addSite(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            if (labelParams["sitename"].value.ToString().Trim().Equals(""))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "站点名称不能为空!");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            string dirName=labelParams["dirname"].value.ToString();
            string siteDirPath = "/" + dirName;
            if (Convert.ToInt32(labelParams["parentsiteid"].value) == 0) //无上级站点
            {
                labelParams.Add("siteurl", new XNLParam(siteDirPath));
                labelParams.Add("nodesettings", new XNLParam(ManageUtil.getChannelConfig()));
                if (labelParams["isroot"].value.ToString().Equals("1"))
                {
                    siteDirPath = "/";
                    labelParams["siteurl"].value = "/";
                    //主站不用检查站点文件夹
                }
                else
                {
                    if (dirName.Trim().Equals(""))
                    {
                        Dictionary<string, string> errorList = new Dictionary<string, string>();
                        errorList.Add("1", "站点文件夹名称不能为空!");
                        labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                        return labelContentStr;
                    }
                    else
                    {
                        //检查站点文件夹格式
                        if(!Regex.Match(dirName,"^[A-Za-z0-9-_]+$").Success)
                        {
                            Dictionary<string, string> errorList = new Dictionary<string, string>();
                            errorList.Add("1", "站点文件夹名称格式不正确,站点文件夹名仅限于字母、数字、_-等字符!");
                            labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                            return labelContentStr;
                        }
                    }
                    //检查此站点文件夹是否已存在
                    if (checkDirName(siteDirPath))
                    {
                        Dictionary<string, string> errorList = new Dictionary<string, string>();
                        errorList.Add("1", "此站点文件夹名称已存在!");
                        labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                        return labelContentStr;
                    }
                }
                //checkLevel
                if (labelParams["checklevel"].value.ToString().Equals("0"))
                {
                    labelParams["checklevelnum"].value = 1;
                }
                string checkLevelNum = labelParams["checklevelnum"].value.ToString();
                string siteSetXml=ManageUtil.getSiteConfig().Replace("@CharSet", labelParams["charset"].value.ToString());
                siteSetXml = siteSetXml.Replace("@Audit", checkLevelNum);
                //事务处理添加站点
                labelParams.Add("sitesettings", new XNLParam(siteSetXml));
                string addNodeSql = "insert into sn_nodes(NodeName,IndexName,ModelId,ParentID,ParentPath,Depth,RootID,ChildsNum,indexID,SettingsXML,sortid)values(@siteName,'首页',@ModelId,0,'0',0,-1,0,0,@nodesettings,'0000')";
                string selectNodeSql = "select nodeid from sn_nodes where rootid=-1";
                string updateNodeSql = "update sn_nodes set rootid=@_dbresult1 where nodeid=@_dbresult1";
                string addSiteSql = "insert into sn_sites(nodeId,siteDir,siteUrl,parentSiteId,SettingsXML,isroot)values(@_dbresult1,@dirName,@siteurl,0,@sitesettings,@isroot)";
                string updateModelSql = "update sn_model set useNumber=useNumber+1 where ModelID=@ModelID";
                Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                sqlColls.Add("1", addNodeSql);
                sqlColls.Add("2", selectNodeSql);
                sqlColls.Add("3", updateNodeSql);
                sqlColls.Add("4", addSiteSql);
                sqlColls.Add("5", updateModelSql);
                using (DbConnection dbConnection = DataHelper.CreateConnection())
                {
                    dbConnection.Open();
                    DbTransaction transaction = dbConnection.BeginTransaction();
                    try
                    {
                        DataHelper.ExrcuteScalarSomeSql(sqlColls, labelParams, transaction);
                        labelParams.Add("nodeid", new XNLParam( XNLType.Int32, labelParams["_dbresult1"].value));
                        //建站点目录，复制相关文件，建默认模板方案，系统默认模板文件
                        setGlobalChannel(labelParams, transaction);
                        setSiteTemplate(labelParams, transaction);
                        setSiteDirectory(labelParams);
                        transaction.Commit();
                        labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Dictionary<string, string> errorList = new Dictionary<string, string>();
                        errorList.Add("1", e.Message);
                        labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                    }
                    transaction.Dispose();
                }
                return labelContentStr;
            }
            else //有上级站点
            {
                 if (dirName.Trim().Equals(""))
                 {
                        Dictionary<string, string> errorList = new Dictionary<string, string>();
                        errorList.Add("1", "站点文件夹名称不能为空!");
                        labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                        return labelContentStr;
                 }
                 else
                 {
                      //检查站点文件夹格式
                        if(!Regex.Match(dirName,"^[A-Za-z0-9-_]+$").Success)
                        {
                            Dictionary<string, string> errorList = new Dictionary<string, string>();
                            errorList.Add("1", "站点文件夹名称格式不正确,站点文件夹名仅限于字母、数字、_-等字符!");
                            labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                            return labelContentStr;
                        }
                 }
                DbConnection dbConnection = DataHelper.CreateConnection();
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
               // DbCommand dbCmd = DataHelper.GetSqlStringCommand("select b.nodeid as nodeid,b.ChildsNum as ChildsNum,a.siteurl as siteurl,b.RootID as rootid,b.Depth as depth,b.ParentPath as parentpath from sn_sites as a ,sn_nodes as b where a.siteid=@parentsiteid and a.nodeid=b.nodeid");
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select b.nodeid as nodeid,b.ChildsNum as ChildsNum,a.siteurl as siteurl,b.RootID as rootid,b.ParentPath as parentpath from sn_sites as a ,sn_nodes as b where a.siteid=@parentsiteid and a.nodeid=b.nodeid");
                dbCmd.Connection = dbConnection;
                dbCmd.Transaction = transaction;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                DataTable dt1 = DataHelper.ExecuteDataTable(dbCmd);
                int parentNodeId = Convert.ToInt32(dt1.Rows[0]["nodeid"]);
                string parentDirPath = Convert.ToString(dt1.Rows[0]["siteurl"]);
               // int depth = Convert.ToInt32(dt1.Rows[0]["depth"]);
               // int depth = 0;// Convert.ToInt32(dt1.Rows[0]["depth"]);
                string parentNodePath = Convert.ToString(dt1.Rows[0]["parentpath"]);
                int ChildsNum = Convert.ToInt32(dt1.Rows[0]["ChildsNum"]);
                //添加新节点
                labelParams.Add("parentnodeid", new XNLParam(parentNodeId));
                labelParams.Add("parentpath", new XNLParam(parentNodePath)); //需要更新
               // labelParams.Add("depth", new XNLParam(depth + 1));
              //  labelParams.Add("depth", new XNLParam(XNLType.XNL_Object, 0));
                string curDirPath = parentDirPath + "/" + dirName;
                if (parentDirPath.Substring(parentDirPath.Length - 1, 1).Equals("/"))
                {
                    curDirPath = parentDirPath + dirName;
                }
                //检查此站点文件夹是否已存在
                if (checkDirName(curDirPath))
                 {
                     transaction.Rollback();
                     dbConnection.Close();
                     transaction.Dispose(); 
                     Dictionary<string, string> errorList = new Dictionary<string, string>();
                     errorList.Add("1", "此站点文件夹名称已存在!");
                     labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                     return labelContentStr;
                }
                labelParams.Add("siteurl", new XNLParam(curDirPath));
               // DbCommand dbCmd2 = DataHelper.GetSqlStringCommand("select max(indexid) as maxindex from sn_nodes where ParentID=@parentNodeId");
               // dbCmd2.Connection = dbConnection;
               // dbCmd2.Transaction = transaction;
               // DataHelper.SetParameterValue(dbCmd2, labelParams);
               // object maxIndex = DataHelper.ExecuteScalar(dbCmd2);
               // int newIndexNum = Convert.ToInt32(Convert.IsDBNull(maxIndex) ? 0 : maxIndex) + 1;
               // labelParams.Add("indexid", new XNLParam(newIndexNum));
                if (labelParams["checklevel"].value.ToString().Equals("0"))
                {
                    labelParams["checklevelnum"].value = 1;
                }
                string checkLevelNum = labelParams["checklevelnum"].value.ToString();
                string siteSetXml = ManageUtil.getSiteConfig().Replace("@CharSet", labelParams["charset"].value.ToString());
                siteSetXml = siteSetXml.Replace("@Audit", checkLevelNum);
                labelParams.Add("nodesettings", new XNLParam( XNLType.String, ManageUtil.getChannelConfig()));
                labelParams.Add("sitesettings", new XNLParam( XNLType.String, siteSetXml));
                //string addNodeSql = "insert into sn_nodes(NodeName,IndexName,ModelId,ParentID,ParentPath,Depth,RootID,ChildsNum,indexID,SettingsXML)values(@siteName,'首页',@ModelId,@parentNodeId,@parentpath,@depth,-1,0,@indexid,@nodesettings)";
                string addNodeSql = "insert into sn_nodes(NodeName,IndexName,ModelId,ParentID,ParentPath,Depth,RootID,ChildsNum,indexID,SettingsXML,sortid)values(@siteName,'首页',@ModelId,@parentNodeId,@parentpath,0,-1,0,0,@nodesettings,'0000')";
                string selectNodeSql = "select nodeid from sn_nodes where rootid=-1";
                string addSiteSql = "insert into sn_sites(nodeId,siteDir,siteUrl,parentSiteId,SettingsXML,isroot)values(@_dbresult1,@dirname,@siteurl,@parentsiteid,@sitesettings,@isroot)";
                string updateModelSql = "update sn_model set useNumber=useNumber+1 where ModelID=@ModelID";
                Dictionary<string, string> sqlColl = new Dictionary<string, string>();
                sqlColl.Add("addnode", addNodeSql);
                sqlColl.Add("selectnode", selectNodeSql);
                sqlColl.Add("addSite", addSiteSql);
                sqlColl.Add("setmodel", updateModelSql);
                    try
                    {
                        DataHelper.ExrcuteScalarSomeSql(sqlColl, labelParams, transaction);
                        labelParams.Add("nodeid",new XNLParam(XNLType.Int32,labelParams["_dbresult1"].value));
                        labelParams.Add("rootid", new XNLParam( XNLType.Int32, labelParams["_dbresult1"].value));
                        labelParams["parentpath"].value = parentNodePath + "," + Convert.ToString(labelParams["nodeid"].value);
                        string updateNodeSql = "update sn_nodes set ParentPath=@ParentPath,rootid=@nodeid where nodeid=@nodeid";
                        string updateParentSql = string.Empty;
                        if (ChildsNum == 0)
                        {
                            updateParentSql = "update sn_nodes set ChildsNum=ChildsNum+1,arrChildID='" + Convert.ToString(labelParams["nodeid"].value) + "'  where nodeid=@parentnodeid";
                        }
                        else
                        {
                            updateParentSql = "update sn_nodes set ChildsNum=ChildsNum+1,arrChildID=arrChildID+'," + Convert.ToString(labelParams["nodeid"].value) + "'  where nodeid=@parentnodeid";
                        }
                        Dictionary<string, string> sqlColl2 = new Dictionary<string, string>();
                        sqlColl2.Add("updatenode", updateNodeSql);
                        sqlColl2.Add("updateparent", updateParentSql);
                        DataHelper.ExrcuteScalarSomeSql(sqlColl2, labelParams, transaction); 
                        //建站点目录，复制相关文件，建默认模板方案，系统默认模板文件
                        setGlobalChannel(labelParams, transaction);
                        setSiteTemplate(labelParams, transaction);
                        setSiteDirectory(labelParams);
                        transaction.Commit();
                        labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Dictionary<string, string> errorList = new Dictionary<string, string>();
                        errorList.Add("1", e.Message);
                        labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                    }
                    dbConnection.Close();
                    transaction.Dispose(); 
                return labelContentStr;
            }
        }
     /// <summary>
     /// 检查站点
     /// </summary>
     /// <param name="labelParams"></param>
     /// <param name="labelContentStr"></param>
     /// <param name="matchSuccessItem"></param>
     /// <param name="matchFailItem"></param>
     /// <returns></returns>
        private string checkSite(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            string checkSql = "select a.siteId from sn_sites as a,sn_nodes as b  where a.parentSiteId=@parentsiteid and b.nodename=@siteName";
            object siteidObj = DataHelper.ExecuteScalar(checkSql, labelParams);
            if (Convert.ToInt32(Convert.IsDBNull(siteidObj) ? 0 :siteidObj)> 0)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "站点已存在");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
            else
            {
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            return labelContentStr;
        }
        private void setSiteDirectory( Dictionary<string, XNLParam> labelParams)
        {
            string siteDirPath;
            if (labelParams["isroot"].value.ToString().Equals("0"))
            {
                siteDirPath = HttpContext.Current.Server.MapPath("~" + labelParams["siteurl"].value.ToString());
                Directory.CreateDirectory(siteDirPath);
            }
            else
            {
                siteDirPath = HttpContext.Current.Server.MapPath("~/");
            }
             if(!(siteDirPath.Substring(siteDirPath.Length-1,1).Equals("\\")||siteDirPath.Substring(siteDirPath.Length-1,1).Equals("/")))
             {
                 siteDirPath = siteDirPath + "/";
             }
            Directory.CreateDirectory(siteDirPath + "Template");
            Directory.CreateDirectory(siteDirPath + "Template/默认模板方案");
            FileStream fs = File.Create(siteDirPath + "Template/默认模板方案/T_系统首页模板.ascx");
            FileStream fs1 = File.Create(siteDirPath + "Template/默认模板方案/T_系统栏目模板.ascx");
            FileStream fs2 = File.Create(siteDirPath + "Template/默认模板方案/T_系统内容模板.ascx");
            fs.Close();
            fs1.Close();
            fs2.Close();
            string commonPath=HttpContext.Current.Server.MapPath("~/GlobalFiles/Common");
            CopyDirectory( commonPath,siteDirPath + "/Common");    
        }
        ///   <summary>   
        ///   复制文件夹   
        ///   </summary>   
        ///   <param   name="sourceDirName">源文件夹</param>   
        ///   <param   name="destDirName">目标文件夹</param>     
        //复制文件夹   
        public void CopyDirectory(string sourceDirName, string destDirName)
        {

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                File.Copy(file, destDirName + Path.GetFileName(file), true);
            }


            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }
        public static int GetFilesCount(System.IO.DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            totalFile += dirInfo.GetFiles().Length;
            foreach (System.IO.DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetFilesCount(subdir);
            }
            return totalFile;
        }
        private void setGlobalChannel(Dictionary<string, XNLParam> labelParams, DbTransaction transaction)
        {
            int nodeID=Convert.ToInt32(labelParams["nodeid"].value);
            ChannelConfigManager channelConfig = ChannelConfigManager.createInstance();
            if (channelConfig.channelDataColls.ContainsKey(nodeID)) //已存在此站点
            {

            }
            else  //新建节点
            {
                Cchannel newChannel = new Cchannel();
                newChannel.nodeID = nodeID;
                newChannel.nodeIndexName = "首页";
                newChannel.nodeName = labelParams["sitename"].value.ToString();
                newChannel.isSite = true;
                newChannel.addDate = DateTime.Now.ToLongDateString();
                newChannel.depth = 0;// Convert.ToInt32(labelParams["depth"].theValue);
                if (Convert.ToInt32(labelParams["parentsiteid"].value) != 0)//有上级站点
                {
                    newChannel.parentNode = channelConfig.channelDataColls[Convert.ToInt32(labelParams["parentnodeid"].value)];
                    newChannel.parentNode.addSubNode(nodeID, newChannel);
                }
                //设置模型
                newChannel.model = DataModelManager.createInstance().getModel(Convert.ToInt32(labelParams["modelid"].value));
                //ManageUtil.setNodeModel(newChannel, labelParams, transaction);
                newChannel.siteWebPath = labelParams["siteurl"].value.ToString();
                newChannel.theSiteConfig = new siteConfig();
                newChannel.theSiteConfig.baseConfig.encoder = Encoding.GetEncoding(labelParams["charset"].value.ToString());
                channelConfig.channelDataColls.Add(nodeID, newChannel);
                //设置首页跟单页生成线程信息
                newChannel.indexPageCreateThreadInfo = new SinglePageCreateThreadInfo();
                newChannel.singlePageCreateThreadInfo = new SinglePageCreateThreadInfo();
                newChannel.channelPageCreateThreadInfo = new SinglePageCreateThreadInfo();
                newChannel.contentPageCreateThreadInfo = new ContentPageCreateThreadInfo();
            }
        }
        private void setSiteTemplate(Dictionary<string, XNLParam> labelParams,DbTransaction transaction)
        {
            //建模板方案
            labelParams.Add("sysindex", new XNLParam("@/Template/默认模板方案/T_系统首页模板.ascx"));
            labelParams.Add("syschannel", new XNLParam("@/Template/默认模板方案/T_系统栏目模板.ascx"));
            labelParams.Add("syscontent", new XNLParam("@/Template/默认模板方案/T_系统内容模板.ascx"));
            labelParams.Add("createfilepath", new XNLParam("@/index"));
            //建模板表
            string selectSiteSql = "select SiteID from sn_sites where nodeid=@nodeid";
            string templateProjectSql = "insert into SN_TemplateProject(ProjectName,SiteID,ChannelFilePathRule,ContentFilePathRule,IsDefault)values('默认模板方案',@_dbresult0,'channels/[#ChannelID].html','contents/[#ChannelID]/[#ContentID].html',1)";
            string selectProjectSql = "select ProjectID from SN_TemplateProject where siteid=@_dbresult0";
            string indexTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统首页模板',0,'T_系统首页模板.ascx',@sysindex,@createfilepath,'.html',1,@charset,1,@_dbresult0,@_dbresult2)";
            string channelTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统栏目模板',1,'T_系统栏目模板.ascx',@syschannel,1,@charset,1,@_dbresult0,@_dbresult2)";
            string contentTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统内容模板',2,'T_系统内容模板.ascx',@syscontent,1,@charset,1,@_dbresult0,@_dbresult2)";
            string selectIndexTemplateSql = "select TemplateID from SN_Template where SiteID=@_dbresult0 and TemplateProjectID=@_dbresult2 and TemplateStyle=0";
            string selectChannelTemplateSql = "select TemplateID from SN_Template where SiteID=@_dbresult0 and TemplateProjectID=@_dbresult2 and TemplateStyle=1";
            string selectContentTemplateSql = "select TemplateID from SN_Template where SiteID=@_dbresult0 and TemplateProjectID=@_dbresult2 and TemplateStyle=2";
            string updateTemProSql = "update SN_TemplateProject set IndexTemplateID=@_dbresult6,ChannelTemplateID=@_dbresult7,ContentTemplateID=@_dbresult8 where ProjectID=@_dbresult2";
            string addTemMatchSql = "insert into SN_TemplateMatch (NodeID,ProjectID,SiteID,ChannelTemplateID,ContentTemplateID,ChannelFilePathRule,ContentFilePathRule)values(@NodeID,@_dbresult2,@_dbresult0,@_dbresult7,@_dbresult8,'channels/[#ChannelID].html','contents/[#ChannelID]/[#ContentID].html')";
            Dictionary<string, string> sqlColls = new Dictionary<string, string>();
            sqlColls.Add("selectsite", selectSiteSql);
            sqlColls.Add("Project", templateProjectSql);
            sqlColls.Add("selectproject", selectProjectSql);
            sqlColls.Add("indexTemplate", indexTemplateSql);
            sqlColls.Add("channelTemplate", channelTemplateSql);
            sqlColls.Add("contentTemplate", contentTemplateSql);
            sqlColls.Add("selectindext", selectIndexTemplateSql);
            sqlColls.Add("selectct", selectChannelTemplateSql);
            sqlColls.Add("selectcot", selectContentTemplateSql);
            sqlColls.Add("uptp", updateTemProSql);
            sqlColls.Add("addtm", addTemMatchSql);
            DataHelper.ExrcuteScalarSomeSql(sqlColls, labelParams,transaction);
            labelParams.Add("siteid", new XNLParam( XNLType.Int32, labelParams["_dbresult0"].value));
            int siteID = Convert.ToInt32(labelParams["siteid"].value);
            SiteConfigManager siteConfig = SiteConfigManager.createInstance();
            int nodeID = Convert.ToInt32(labelParams["nodeid"].value);
            ChannelConfigManager channelConfig = ChannelConfigManager.createInstance();
            Cchannel newChannel = channelConfig.channelDataColls[nodeID];
            newChannel.siteID = siteID;
            siteConfig.siteDataColls.Add(siteID, newChannel);
           int projectID = Convert.ToInt32(labelParams["_dbresult2"].value);
           string projectName = "默认模板方案";
           newChannel.defaultProjectId = projectID;
           TemplateProject templateProject = new TemplateProject();
           templateProject.templateProjectName = projectName;
           templateProject.indexUrl = ("@/index.html");
           //设置模板方案栏目地址
           string channelUrl = CMSUtils.getPathByPathRule("channels/[#ChannelID].html", newChannel.nodeID, newChannel.nodeIndexName, newChannel.nodeName, 1, null);
           templateProject.setChannelUrl(newChannel.nodeID, channelUrl);
           //设置模板方案内容页地址规则
           string ContentFilePathRule = CMSUtils.getPathByPathRule("contents/[#ChannelID]/[#ContentID].html", newChannel.nodeID, newChannel.nodeIndexName, newChannel.nodeName, 1, null);
           templateProject.setContentUrlRule(newChannel.nodeID, ContentFilePathRule);
           newChannel.addTemplateProject(projectID, templateProject);
        }
        private bool checkDirName(string siteDirPath)
        {
            if (Directory.Exists(HttpContext.Current.Server.MapPath(siteDirPath)))
            {
                return true;
            }
            return false;
        }

        #region IXNLTagObj<WebContext> 成员


        public string subTagNames
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IXNLTagObj<WebContext> 成员


        public string getSubTagNames(string parentTagName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
