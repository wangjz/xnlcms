using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.DAL;
using System.Data;
using System.Text.RegularExpressions;
using COM.SingNo.XNLEngine;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class Content : IXNLTag<WebContext>
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
        /*
         * wordNum="显示字符的数目，0代表不限制字数" ellipsis="文字超出部分显示的文字" isClearTags="是否清除HTML标签" isReturnToBR="是否将回车替换为HTML换行标签"
        <XNL:Content id="" channelid="" channelindex channelname="" siteid="" sitename="" pagename="">
        * {@Content.Id} {@Content.title} {@Content.adddate} {@Content.linkUrl}  {@Content.InputUser} {@Content.LastEditUser} {@Content.LastEditDate}  {@Content.SubTitle} {@Content.Summary}
         * {@Content.ImageUrl} {@Content.FileUrl} {@Content.Author} {@Content.Source}  {@Content.Content}    {@Content.KeyWord} {@Content.Hits} {@Content.Dayhits} {@Content.WeekHits}
         * {@Content.MonthHits} {@Content.LastHitsDate}  {@Content.ItemIndex} {@Content.channelId} {@content.pagecontent}{@content.styletitle}
         * {@content.url}{@content.path}{@content.dir} {@content.dirpath}{@content.dirurl} {@content.pagefilename} {@content.pagefileext} {@content.pagefile}
        * </XNL:Content>
        */
       public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
           /*
            XNLParam IdParam=null;
            Cchannel curChannel=XNLPage.curChannel;
            int channelid;
            //首先判断channelid
            if (labelParams.TryGetValue("id", out IdParam)&&Convert.ToInt32(IdParam.value)!=XNLPage.contentId)
            {
                if (IdParam.value.ToString().Equals("0")) return "";
                XNLParam channelIdParam;
                XNLParam channelindexParam;
                XNLParam channelnameParam;
                XNLParam siteidParam;
                XNLParam sitenameParam;
                XNLParam xnlpageParam = null;
                string pagename = "page";
                if (labelParams.TryGetValue("pagename", out xnlpageParam))
                {
                    pagename = xnlpageParam.value.ToString();
                }
                if (labelParams.TryGetValue("channelid", out channelIdParam)) //有channelid参数
                {
                    channelid = Convert.ToInt32(channelIdParam.value);
                    if (channelid != XNLPage.curChannel.nodeID)
                    {
                        curChannel = ChannelConfigManager.createInstance().channelDataColls[channelid];
                    }
                }
                else if (labelParams.TryGetValue("siteid", out siteidParam)) //有siteid参数
                {
                    if (labelParams.TryGetValue("channelindex", out channelindexParam)) //如果有channelindex参数，则优先查找
                    {
                        curChannel = XNLUtils.getChannelByIndexFromChannel(channelindexParam.value.ToString(), SiteConfigManager.createInstance().siteDataColls[Convert.ToInt32(siteidParam.value)]);
                    }
                    else if (labelParams.TryGetValue("channelname", out channelnameParam))//没有指定channelindex参数，则查找 channelname
                    {
                        curChannel = XNLUtils.getChannelByNameFromChannel(channelnameParam.value.ToString(), SiteConfigManager.createInstance().siteDataColls[Convert.ToInt32(siteidParam.value)]);
                    }
                }
                else if (labelParams.TryGetValue("sitename", out sitenameParam))//有sitename参数
                {
                    if (labelParams.TryGetValue("channelindex", out channelindexParam)) //如果有channelindex参数，则优先查找
                    {
                        curChannel = XNLUtils.getChannelByIndexFromChannel(channelindexParam.value.ToString(), XNLUtils.getSiteByName(sitenameParam.value.ToString()));
                    }
                    else if (labelParams.TryGetValue("channelname", out channelnameParam))//没有指定channelindex参数，则查找 channelname
                    {
                        curChannel = XNLUtils.getChannelByNameFromChannel(channelnameParam.value.ToString(), XNLUtils.getSiteByName(sitenameParam.value.ToString()));
                    }
                }
                //跟当前内容id不一样
                if (curChannel == null) curChannel = XNLPage.curChannel;
                channelid = curChannel.nodeID;
                //查找所有的属性，根据指定的属性提取数据
                Dictionary<string,List<string>> allVarList=XNLUtils.getContentVarList(curChannel,labelContentStr);
                if (allVarList == null)
                {
                    return labelContentStr;
                }
                else
                {
                    string fieldStr = "";
                    List<string> fieldList=allVarList["f"];
                    for (int i = 0; i < fieldList.Count; i++)
                    {
                        fieldStr += (i == 0 ? fieldList[i] : "," + fieldList[i]);
                    }
                    DataTable dt = DataHelper.ExecuteDataTable("select " + fieldStr + " from " + curChannel.model.TableName + " where id=@id", labelParams);
                    if (dt.Rows.Count == 0)
                    {
                        throw (new Exception("内容不存在"));
                    }
                    DataRow row = dt.Rows[0];
                    labelParams.Add("content.channelid", new XNLParam(row["nodeid"]));
                    int titleWord = 0;
                    if (labelParams.TryGetValue("titleword", out IdParam))
                    {
                        if (!int.TryParse(IdParam.value.ToString(), out titleWord))
                        {
                            titleWord = 0;
                        }
                    }
                    string ellipsis = "...";
                    if (labelParams.TryGetValue("ellipsis", out IdParam))
                    {
                        ellipsis = IdParam.value.ToString();
                    }
                    foreach (string str in fieldList)
                    {
                        string attrStr = Convert.ToString(row[str]);
                        if (str == "title")
                        {
                            if (titleWord > 0 && attrStr.Length > titleWord)
                            {
                                attrStr = attrStr.Substring(0, titleWord) + ellipsis;
                            }
                        }
                        labelParams.Add("content." + str, new XNLParam(attrStr));
                    }
                    List<string> extList=allVarList["e"];
                    string contentId=row["id"].ToString();
                    foreach (string str in extList)
                    {
                        switch (str)
                        {
                            case "url":
                                string linkUrl = row["linkurl"].ToString().Trim();//getContentData("linkurl", XNLPage);
                                if(string.IsNullOrEmpty(linkUrl.Trim()))
                                {
                                    //得到内容页路径
                                    string url=SystemConfig.systemUrl + (curChannel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(curChannel.nodeID, contentId).Replace("@", curChannel.siteWebPath)).Replace("//", "/");
                                    labelParams.Add("content.url", new XNLParam(url));
                                }
                                else
                                {
                                    labelParams.Add("content.url", new XNLParam(linkUrl));
                                }
                                break;
                            case "pagecontent":
                                string contentStr = row["content"].ToString();
                                string pageType=row["pagetype"].ToString();
                                if(pageType.Equals("1"))
                                {
                                    string pageWords=row["pagewords"].ToString();
                                    labelParams.Add("content.pagecontent", new XNLParam("<xnl:pagetext type=\"auto\" words=\""+pageWords+"\" pagename=\""+pagename+"\">"+contentStr+"</xnl:pagetext>"));
                                }
                                else
                                {
                                    string _type=pageType.Equals("2")?"tag":"no";
                                    labelParams.Add("content.pagecontent", new XNLParam("<xnl:pagettext type=\"" + _type + "\" pagename=\"" + pagename + "\">" + contentStr + "</xnl:pagetext>"));
                                }
                                break;
                            case "path":
                                string _linkUrl = row["linkurl"].ToString();
                                if (string.IsNullOrEmpty(_linkUrl.Trim()))
                                {
                                    //得到内容页路径
                                    string url = (curChannel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(curChannel.nodeID, contentId).Replace("@", curChannel.siteWebPath)).Replace("//", "/");
                                    labelParams.Add("content.path", new XNLParam(url));
                                }
                                else
                                {
                                    labelParams.Add("content.path", new XNLParam(_linkUrl));
                                }
                                break;
                            case "dir":
                                labelParams.Add("content.dir", new XNLParam(getContentDir(curChannel, XNLPage, contentId)));
                                break;
                            case "dirpath":
                                labelParams.Add("content.dirpath", new XNLParam(getContentDirPath(curChannel, XNLPage, contentId)));
                                break;
                            case "dirurl":
                                labelParams.Add("content.dirurl", new XNLParam(getContentDirUrl(curChannel, XNLPage, contentId)));
                                break;
                            case "pagefilename":
                                labelParams.Add("content.pagefilename", new XNLParam(getContentPageFileName(curChannel, XNLPage, contentId)));
                                break;
                            case "pagefileext":
                                labelParams.Add("content.pagefileext", new XNLParam(getContentPageFileExt(curChannel, XNLPage, contentId)));
                                break;
                            case "pagefile":
                                labelParams.Add("content.pagefile", new XNLParam(getContentPageFile(curChannel, XNLPage, contentId)));
                                break;
                            case "channelid":
                                break;
                            case "channelname":
                                break;
                            case "channelindex":
                                break;
                        }
                    }
                    labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
                    return labelContentStr;
                }
            }
            else if (IdParam==null&&XNLPage.contentId == 0) //没指定id，则当前页id为0
            {
                return "";
            }
            else //替换当前页的内容变量
            {
                if (curChannel == null) curChannel = XNLPage.curChannel;
                channelid = curChannel.nodeID;
                string contentId = Convert.ToString(IdParam.value);
                string pagename = "page";
                XNLParam xnlpageParam;
                if (labelParams.TryGetValue("pagename", out xnlpageParam))
                {
                    pagename = xnlpageParam.value.ToString();
                }
                MatchCollection matchColls = Regex.Matches(labelContentStr, "{@content\\.(.+?)}", XNLBaseCommon.XNL_RegexOptions);
                if(matchColls.Count>0)
                {
                    StringBuilder contentSb = new StringBuilder(labelContentStr);
                    foreach (Match m in matchColls)
                    {
                        string varName=m.Groups[1].Value.ToLower();
                        switch (varName)
                        {
                            case "url":
                                string linkUrl = getContentData("linkurl", XNLPage);
                                if(string.IsNullOrEmpty(linkUrl.Trim()))
                                {
                                    //得到内容页路径
                                    string url=SystemConfig.systemUrl + (curChannel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(curChannel.nodeID, contentId).Replace("@", curChannel.siteWebPath)).Replace("//", "/");
                                    contentSb.Replace(m.Value, url);
                                }
                                else
                                {
                                    contentSb.Replace(m.Value, linkUrl);
                                }
                                break;
                            case "pagecontent":
                                string contentStr = getContentData("content", XNLPage);
                                string pageType=getContentData("pagetype", XNLPage);
                                if(pageType.Equals("1"))
                                {
                                    string pageWords=getContentData("pagewords", XNLPage);
                                    contentSb.Replace(m.Value, "<xnl:pagecontent type=\"auto\" words=\"" + pageWords + "\" pagename=\"" + pagename + "\">" + contentStr + "</xnl:pagecontent>");
                                }
                                else
                                {
                                    string _type=pageType.Equals("2")?"tag":"no";
                                    contentSb.Replace(m.Value, "<xnl:pagecontent type=\"" + _type + "\" pagename=\"" + pagename + "\">" + contentStr + "</xnl:pagecontent>");
                                }
                                break;
                            case "path":
                                string _linkUrl = getContentData("linkurl", XNLPage);
                                if (string.IsNullOrEmpty(_linkUrl.Trim()))
                                {
                                    //得到内容页路径
                                    string url = (curChannel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(curChannel.nodeID, contentId).Replace("@", curChannel.siteWebPath)).Replace("//", "/");
                                    contentSb.Replace(m.Value, url);
                                }
                                else
                                {
                                    contentSb.Replace(m.Value, _linkUrl);
                                }
                                break;
                            case "dir":
                                contentSb.Replace(m.Value, getContentDir(curChannel,XNLPage,contentId));
                                break;
                            case "dirpath":
                                contentSb.Replace(m.Value, getContentDirPath(curChannel, XNLPage, contentId));
                                break;
                            case "dirurl":
                                contentSb.Replace(m.Value, getContentDirUrl(curChannel, XNLPage, contentId));
                                break;
                            case "pagefilename":
                                contentSb.Replace(m.Value, getContentPageFileName(curChannel, XNLPage, contentId));
                                break;
                            case "pagefileext":
                                contentSb.Replace(m.Value, getContentPageFileExt(curChannel, XNLPage, contentId));
                                break;
                            case "pagefile":
                                contentSb.Replace(m.Value, getContentPageFile(curChannel, XNLPage, contentId));
                                break;
                            case "channelid":
                                break;
                            case "channelname":
                                break;
                            case "channelindex":
                                break;
                            default:
                               contentSb.Replace(m.Value, "{[content." + varName + "]}");
                                break;

                        }
                        
                    }
                    return contentSb.ToString();
                }
                return labelContentStr;
            }
           */
            return "";
        }
        #endregion
       private string getContentData(string field, WebContext XNLPage)
       {
           DataTable dt=XNLUtils.setContentVariableData(field, XNLPage);
           try
           {
               return Convert.ToString(dt.Rows[0][field]); 
           }
           catch //(System.Exception ex)
           {
               return "";
           }
       }
       private string getContentDir(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string Path = channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId);
           string[] path_arr = Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
           if (path_arr.Length > 1) return path_arr[path_arr.Length - 2].Replace("@", "");
           return "";
       }
       private string getContentDirPath(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string Path = channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId);
           int pIndex = Path.LastIndexOf('/');
           if (pIndex != -1) return Path.Remove(pIndex).Replace("@", "/").Replace("//", "/");
           return "/";
       }
       private string getContentDirUrl(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string Path = channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId);
           int pInd = Path.LastIndexOf('/');
           if (pInd != -1) return SystemConfig.systemUrl + (Path.Remove(pInd).Replace("@", "/").Replace("//", "/"));
           return SystemConfig.systemUrl;
       }
       private string getContentPageFileExt(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string url = (channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId).Replace("@", channel.siteWebPath)).Replace("//", "/");
           string[] path_arr0 = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
           return "." + (path_arr0[path_arr0.Length - 1].Split('?')[0].Split('.')[1]);
       }
       private string getContentPageFileName(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string url = (channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId).Replace("@", channel.siteWebPath)).Replace("//", "/");
           string[] path_arr = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
           return path_arr[path_arr.Length - 1].Split('?')[0].Split('.')[0];
       }
       private string getContentPageFile(Cchannel channel, WebContext XNLPage, string contentId)
       {
           string url = (channel.templateProjectColls[XNLPage.templateProjectId].getContentUrl(channel.nodeID, contentId).Replace("@", channel.siteWebPath)).Replace("//", "/");
           string[] path_arr1 = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
           return path_arr1[path_arr1.Length - 1].Split('?')[0];
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
