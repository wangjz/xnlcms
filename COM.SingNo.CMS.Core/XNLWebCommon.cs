using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Data;
using COM.SingNo.Common;
using COM.SingNo.XNLCore;
using LitJson;
namespace COM.SingNo.CMS.Core
{
    public class XNLWebCommon
    {
        internal const RegexOptions XNL_RegexOptions = (((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
        ////public const string MatchContentVarString = @"{\[content\.(\w+?)\]}";

        /// <summary>
        /// 得到栏目变量值
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string getChannelVariable(Cchannel channel, WebContext XNLPage, string varName)
        {
            switch (varName.ToLower())
            {
                case "id":
                    return channel.nodeID.ToString();
                case "name":
                    return channel.nodeName;
                case "path":
                    return (channel.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(channel.nodeID).Replace("@", channel.siteWebPath)).Replace("//", "/");
                case "url":
                    return SystemConfig.systemUrl + (channel.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(channel.nodeID).Replace("@", channel.siteWebPath)).Replace("//", "/");
                case "summary":
                    return channel.theChannelConfig.baseConfig.info;
                case "matekeywords":
                    return channel.theChannelConfig.baseConfig.metaKeyword;
                case "matedesc":
                    return channel.theChannelConfig.baseConfig.metaDesc;
                case "logo":
                    break;
                case "modelname":
                    return channel.model.ModelName;
                case "modeid":
                    return channel.model.ModelId.ToString();
                case "modelicon":
                    return channel.model.ItemIcon;
                case "itemname":
                    return channel.model.ItemName;
                case "itemunit":
                    return channel.model.ItemUnit;
                case "indexname":
                    return channel.nodeIndexName;
                case "dir":
                    string channelPath = (channel.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(channel.nodeID));
                    string[] path_arr = channelPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (path_arr.Length > 1) return path_arr[path_arr.Length - 2].Replace("@", "");
                    return "";
                case "dirpath":
                    string channelPath2 = (channel.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(channel.nodeID));
                    int pIndex = channelPath2.LastIndexOf('/');
                    if (pIndex != -1) return channelPath2.Remove(pIndex).Replace("@", "/").Replace("//", "/");
                    return "/";
                case "dirurl":
                    string channelPath3 = (channel.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(channel.nodeID));
                    int pInd = channelPath3.LastIndexOf('/');
                    if (pInd != -1) return SystemConfig.systemUrl + (channelPath3.Remove(pInd).Replace("@", "/").Replace("//", "/"));
                    return SystemConfig.systemUrl;
                case "adddate":
                    return XNLPage.curChannel.addDate.ToString();
                case "imgurl":
                    return XNLUtils.getChannelImageUrl(XNLPage.curChannel);
                case "imgpath":
                    return XNLUtils.getChannelImagePath(XNLPage.curChannel);
                case "childcount":
                    return (channel.subNodeColls != null ? channel.subNodeColls.Count.ToString() : "0");
                case "allchildcount":
                    return "";
                case "contentcount":
                    return "";
                case "allcontentcount":
                    return "";
                case "imgcontentcount":
                    return "";
                case "allimgcontentcount":
                    return "";
                case "indexnum":
                    return "0";
                case "depth":
                    return channel.depth.ToString();
            }
            return "";

        }

        /// <summary>
        /// 得到站点变量值
        /// </summary>
        /// <param name="site"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string getSiteVariable(Cchannel site, string varName)
        {
            switch (varName.ToLower())
            {
                case "path":
                    return site.siteWebPath;
                case "url":
                    return (SystemConfig.systemUrl + site.siteWebPath);
                case "id":
                    return site.siteID.ToString();
                case "name":
                    return site.nodeName;
                case "title":
                    return site.theSiteConfig.baseConfig.title;
                case "ico":
                    return site.theSiteConfig.baseConfig.ico;
                case "logo":
                    return "";
                case "summary":
                    return site.theSiteConfig.baseConfig.info;
                case "matekeywords":
                    return site.theSiteConfig.baseConfig.mateKeyWord;
                case "matedesc":
                    return site.theSiteConfig.baseConfig.mateDesc;
                case "charset":
                    return site.theSiteConfig.baseConfig.encoder.WebName;
                case "indexname":
                    return site.nodeIndexName;
                case "dir":
                    if (site.siteWebPath.Equals("/")) return "/";
                    string[] path_arr = site.siteWebPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (path_arr.Length > 0) return path_arr[path_arr.Length - 1];
                    return "";
            }
            return "";

        }
        /// <summary>
        /// 得到页变量值
        /// </summary>
        /// <param name="XNLPage"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string getPageVariable(WebContext XNLPage, string varName)
        {
            //{[page.location]},{[page.url]},{[page.fileName]},{[page.title]},{[page.path]},{[page.fileExt]},{[page.file]}
            switch (varName.ToLower())
            {
                case "location":
                    if (XNLPage.curChannel.depth > XNLPage.curChannel.siteNode.depth)
                    {
                        System.Collections.Generic.Stack<Cchannel> stack = new Stack<Cchannel>();
                        Cchannel pn = XNLPage.curChannel;
                        stack.Push(pn);
                        for (int k = 1; k <= XNLPage.curChannel.depth - XNLPage.curChannel.siteNode.depth; k++)
                        {
                            pn = pn.parentNode;
                            stack.Push(pn);
                        }
                        StringBuilder locationSb = new StringBuilder();
                        while (stack.Count > 0)
                        {
                            pn = stack.Pop();
                            if (pn.theChannelConfig.baseConfig.showOnPath)
                            {
                                if (!pn.isSite)
                                {
                                    locationSb.Append("<a href=\"" + (pn.siteNode.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(pn.nodeID).Replace("@", pn.siteNode.siteWebPath)).Replace("//", "/") + "\" target=\"" + pn.theChannelConfig.baseConfig.openType + "\" title=\"" + pn.nodeName + "\">" + pn.nodeName + "</a>" + (stack.Count > 0 ? " &gt; " : ""));
                                }
                                else
                                {
                                    locationSb.Append("<a href=\"" + (pn.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", pn.siteWebPath)).Replace("//", "/") + "\" target=\"" + pn.theChannelConfig.baseConfig.openType + "\" title=\"首页\">首页</a>" + (stack.Count > 0 ? " &gt; " : ""));
                                }
                            }
                        }
                        return locationSb.ToString();
                    }
                    else
                    {
                        if (XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.showOnPath)
                        {
                            return "<a href=\"" + (XNLPage.curChannel.siteNode.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", XNLPage.curChannel.siteNode.siteWebPath)).Replace("//", "/") + "\" target=\"" + XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.openType + "\" title=\"首页\">首页</a>";
                        }
                    }
                    return "";
                case "url":
                    var url = (SystemConfig.systemUrl + XNLPage.pagePath);
                    if (url.EndsWith("?")) url = url.Remove(url.Length - 1);
                    return url;
                case "filename":
                    string[] path_arr = XNLPage.pagePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return path_arr[path_arr.Length - 1].Split('?')[0].Split('.')[0];
                case "path":
                    var path = XNLPage.pagePath;
                    if (path.EndsWith("?")) path = path.Remove(path.Length - 1);
                    return path;
                case "fileext":
                    string[] path_arr0 = XNLPage.pagePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return "." + (path_arr0[path_arr0.Length - 1].Split('?')[0].Split('.')[1]);
                case "file":
                    string[] path_arr1 = XNLPage.pagePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return path_arr1[path_arr1.Length - 1].Split('?')[0];
                case "host":
                    return SystemConfig.systemHost;
                case "query":
                    if (XNLPage.accessType == AccessType.Static)
                    {
                        return "";
                    }
                    Uri uri = XNLPage.Context.Request.Url;
                    return uri.Query;
                case "pathandquery":
                    if (XNLPage.accessType == AccessType.Static)
                    {
                        return XNLPage.pagePath;
                    }
                    Uri uri1 = XNLPage.Context.Request.Url;
                    return uri1.PathAndQuery;
                case "contentid":
                    return XNLPage.contentId.ToString();
                case "dir":
                    string[] path_arr2 = XNLPage.pagePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (path_arr2.Length > 1) return path_arr2[path_arr2.Length - 2];
                    return "";
                case "dirpath":
                    var path2 = XNLPage.pagePath;
                    if (path2.EndsWith("?")) path2 = path2.Remove(path2.Length - 1);
                    int pIndex = path2.LastIndexOf('/');
                    if (pIndex != -1) return path2.Remove(pIndex);
                    return "/";
                case "dirurl":
                    var path3 = XNLPage.pagePath;
                    if (path3.EndsWith("?")) path3 = path3.Remove(path3.Length - 1);
                    int pInd = path3.LastIndexOf('/');
                    if (pInd != -1) return SystemConfig.systemUrl + path3.Remove(pInd);
                    return SystemConfig.systemUrl;
                case "projectid":
                    return XNLPage.templateProjectId.ToString();
                case "projectname":
                    return XNLPage.curChannel.templateProjectColls[XNLPage.templateProjectId].templateProjectName;
            }
            return "";

        }
        public static string getContentVariable(WebContext XNLPage, string varName)
        {
            if (XNLPage.contentId == 0) return "";
            DataTable dt = null;
            switch (varName.ToLower())
            {
                case "id":
                    return XNLPage.contentId.ToString();
                case "styletitle":
                    dt = XNLUtils.setContentVariableDataRow("titlecolor", XNLPage);
                    dt = XNLUtils.setContentVariableDataRow("bold", XNLPage);
                    dt = XNLUtils.setContentVariableDataRow("italic", XNLPage);
                    dt = XNLUtils.setContentVariableDataRow("underline", XNLPage);
                    dt = XNLUtils.setContentVariableDataRow("title", XNLPage);
                    DataRow r = dt.Rows[0];
                    string titleColor = Convert.ToString(r["titlecolor"]);
                    int Bold = Convert.ToInt32(r["bold"]);
                    int italic = Convert.ToInt32(r["italic"]);
                    int underline = Convert.ToInt32(r["underline"]);
                    string title = Convert.ToString(r["title"]);
                    title = "<font color=\"" + titleColor + "\"" + (Bold == 1 ? " style=\"font-weight:bold;" : "") + (italic == 1 ? " font-style:italic;" : "") + ">" + (underline == 1 ? "<u>" : "") + title + (underline == 1 ? "</u>" : "") + "</font>";
                    return title;
                case "autourl":
                    return "";
                case "autopath":
                    return "";
                case "pagecontent":
                    dt = XNLUtils.setContentVariableDataRow("content", XNLPage);
                    DataRow row = dt.Rows[0];
                    object obj = row[varName];
                    string pageType = row["pagetype"].ToString();
                    if (pageType.Equals("1"))
                    {
                        string pageWords = row["pagewords"].ToString();
                        return "<xnl:pagetext type=\"auto\" words=\"" + pageWords + "\" pagename=\"_page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                    }
                    else
                    {
                        string _type = pageType.Equals("2") ? "tag" : "no";
                        return "<xnl:pagetext type=\"" + _type + "\" pagename=\"_page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                    }
                default:
                    dt = XNLUtils.setContentVariableDataRow(varName, XNLPage);
                    DataRow row1 = dt.Rows[0];
                    object obj1 = row1[varName];
                    return Convert.IsDBNull(obj1) ? "" : Convert.ToString(obj1);
            }
        }
        public static string getContentVariable(WebContext XNLPage, string varName, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "";
            switch (varName.ToLower())
            {
                //case "id":
                //    return XNLPage.contentId.ToString();
                case "styletitle":
                    DataRow r = dt.Rows[0];
                    string titleColor = Convert.ToString(r["titlecolor"]);
                    int Bold = Convert.ToInt32(r["bold"]);
                    int italic = Convert.ToInt32(r["italic"]);
                    int underline = Convert.ToInt32(r["underline"]);
                    string title = Convert.ToString(r["title"]);
                    title = "<font color=\"" + titleColor + "\"" + (Bold == 1 ? " style=\"font-weight:bold;" : "") + (italic == 1 ? " font-style:italic;" : "") + ">" + (underline == 1 ? "<u>" : "") + title + (underline == 1 ? "</u>" : "") + "</font>";
                    return title;
                case "pagecontent":
                    try
                    {
                        DataRow row1 = dt.Rows[0];
                        object obj = row1["content"];
                        string pageType = row1["pagetype"].ToString();
                        if (pageType.Equals("1"))
                        {
                            string pageWords = row1["pagewords"].ToString();
                            return "<xnl:pagetext type=\"auto\" words=\"" + pageWords + "\" pagename=\"_page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                        }
                        else
                        {
                            string _type = pageType.Equals("2") ? "tag" : "no";
                            return "<xnl:pagetext type=\"" + _type + "\" pagename=\"_page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                        }
                    }
                    catch { return ""; }
                default:
                    {
                        try
                        {
                            DataRow row = dt.Rows[0];
                            object obj = row[varName];
                            return Convert.IsDBNull(obj) ? "" : Convert.ToString(obj);
                        }
                        catch { return ""; }
                    }
            }
        }
        public static string getContentVariable(WebContext XNLPage, string varName, DataRow row, Cchannel channel, string contentPath)
        {
            if (row == null) return "";
            switch (varName.ToLower())
            {
                case "id":
                    return row["id"].ToString();
                case "styletitle":
                    string titleColor = Convert.ToString(row["titlecolor"]);
                    int Bold = Convert.ToInt32(row["bold"]);
                    int italic = Convert.ToInt32(row["italic"]);
                    int underline = Convert.ToInt32(row["underline"]);
                    string title = Convert.ToString(row["title"]);
                    title = "<font color=\"" + titleColor + "\"" + (Bold == 1 ? " style=\"font-weight:bold;" : "") + (italic == 1 ? " font-style:italic;" : "") + ">" + (underline == 1 ? "<u>" : "") + title + (underline == 1 ? "</u>" : "") + "</font>";
                    return title;
                case "autourl":
                    return "";
                case "autopath":
                    return "";
                case "target":
                    return channel.theChannelConfig.baseConfig.itemOpenType;
                case "path":
                    string link = row["linkurl"].ToString();
                    if (!string.IsNullOrEmpty(link))
                    {
                        return link;
                    }
                    else
                    {
                        return contentPath;
                    }
                case "url":
                    string link2 = row["linkurl"].ToString();
                    if (!string.IsNullOrEmpty(link2))
                    {
                        return link2;
                    }
                    else
                    {
                        return SystemConfig.systemUrl + contentPath.Replace("//", "/");
                    }
                case "dir":
                    string[] path_arr = contentPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (path_arr.Length > 1) return path_arr[path_arr.Length - 2].Replace("@", "");
                    return "";
                case "dirpath":
                    int pIndex = contentPath.LastIndexOf('/');
                    if (pIndex != -1) return contentPath.Remove(pIndex).Replace("@", "/").Replace("//", "/");
                    return "/";
                case "dirurl":
                    int pInd = contentPath.LastIndexOf('/');
                    if (pInd != -1) return SystemConfig.systemUrl + (contentPath.Remove(pInd).Replace("@", "/").Replace("//", "/"));
                    return SystemConfig.systemUrl;
                case "filename":
                    string[] path_arr2 = contentPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return path_arr2[contentPath.Length - 1].Split('?')[0].Split('.')[0];
                case "fileext":
                    string[] path_arr0 = contentPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return "." + (path_arr0[path_arr0.Length - 1].Split('?')[0].Split('.')[1]);
                case "file":
                    string[] path_arr1 = contentPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return path_arr1[path_arr1.Length - 1].Split('?')[0];
                case "channelid":
                    return channel.nodeID.ToString();
                case "channelname":
                    return channel.nodeName.ToString();
                case "channelindex":
                    return channel.nodeIndexName.ToString();
                case "pagecontent":
                    try
                    {
                        object obj = row["content"];
                        string pageType = row["pagetype"].ToString();
                        if (pageType.Equals("1"))
                        {
                            string pageWords = row["pagewords"].ToString();
                            return "<xnl:pagetext type=\"auto\" words=\"" + pageWords + "\" pagename=\"_page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                        }
                        else
                        {
                            string _type = pageType.Equals("2") ? "tag" : "no";
                            return "<xnl:pagetext type=\"" + _type + "\" pagename=\"page\">" + Convert.ToString(obj) + "</xnl:pagetext>";
                        }
                    }
                    catch { return ""; }
                default:
                    {
                        try
                        {
                            object obj = row[varName];
                            return Convert.IsDBNull(obj) ? "" : Convert.ToString(obj);
                        }
                        catch { return ""; }
                    }
            }
        }
        /// <summary>
        /// 得到页变量值,动态页生成用
        /// </summary>
        /// <param name="XNLPage"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string getDynamicPagePageVariable(WebContext XNLPage, string varName)
        {
            //{[page.location]},{[page.url]},{[page.fileName]},{[page.title]},{[page.path]},{[page.fileExt]},{[page.file]}
            switch (varName.ToLower())
            {
                case "location":
                    if (XNLPage.curChannel.depth > XNLPage.curChannel.siteNode.depth)
                    {
                        System.Collections.Generic.Stack<Cchannel> stack = new Stack<Cchannel>();
                        Cchannel pn = XNLPage.curChannel;
                        stack.Push(pn);
                        for (int k = 1; k <= XNLPage.curChannel.depth - XNLPage.curChannel.siteNode.depth; k++)
                        {
                            pn = pn.parentNode;
                            stack.Push(pn);
                        }
                        StringBuilder locationSb = new StringBuilder();
                        while (stack.Count > 0)
                        {
                            pn = stack.Pop();
                            if (pn.theChannelConfig.baseConfig.showOnPath)
                            {
                                if (!pn.isSite)
                                {
                                    locationSb.Append("<a href=\"" + (pn.siteNode.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(pn.nodeID).Replace("@", pn.siteNode.siteWebPath)).Replace("//", "/") + "\" target=\"" + pn.theChannelConfig.baseConfig.openType + "\" title=\"" + pn.nodeName + "\">" + pn.nodeName + "</a>" + (stack.Count > 0 ? " &gt; " : ""));
                                }
                                else
                                {
                                    locationSb.Append("<a href=\"" + (pn.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", pn.siteWebPath)).Replace("//", "/") + "\" target=\"" + pn.theChannelConfig.baseConfig.openType + "\" title=\"首页\">首页</a>" + (stack.Count > 0 ? " &gt; " : ""));
                                }
                            }
                        }
                        return locationSb.ToString();
                    }
                    else
                    {
                        if (XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.showOnPath)
                        {
                            return "<a href=\"" + (XNLPage.curChannel.siteNode.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", XNLPage.curChannel.siteNode.siteWebPath)).Replace("//", "/") + "\" target=\"" + XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.openType + "\" title=\"首页\">首页</a>";
                        }
                    }
                    return "";
                case "url":
                    var url = (SystemConfig.systemUrl + XNLPage.pagePath);
                    if (url.EndsWith("?")) url = url.Remove(url.Length - 1);
                    return url;
                case "filename":
                    string[] path_arr = XNLPage.pagePath.Split('/');
                    return path_arr[path_arr.Length - 1].Split('?')[0].Split('.')[0];
                case "path":
                    var path = XNLPage.pagePath;
                    if (path.EndsWith("?")) path = path.Remove(path.Length - 1);
                    return path;
                case "fileext":
                    return ".aspx";
                case "file":
                    string[] path_arr1 = XNLPage.pagePath.Split('/');
                    return path_arr1[path_arr1.Length - 1].Split('?')[0];
                case "host":
                    return SystemConfig.systemHost;
                case "dir":
                    string[] path_arr2 = XNLPage.pagePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (path_arr2.Length > 1) return path_arr2[path_arr2.Length - 2];
                    return "";
                case "dirpath":
                    var path2 = XNLPage.pagePath;
                    if (path2.EndsWith("?")) path2 = path2.Remove(path2.Length - 1);
                    int pIndex = path2.LastIndexOf('/');
                    if (pIndex != -1) return path2.Remove(pIndex);
                    return "/";
                case "dirurl":
                    var path3 = XNLPage.pagePath;
                    if (path3.EndsWith("?")) path3 = path3.Remove(path3.Length - 1);
                    int pInd = path3.LastIndexOf('/');
                    if (pInd != -1) return SystemConfig.systemUrl + path3.Remove(pInd);
                    return SystemConfig.systemUrl;
                case "projectid":
                    return XNLPage.templateProjectId.ToString();
                case "projectname":
                    return XNLPage.curChannel.templateProjectColls[XNLPage.templateProjectId].templateProjectName;
            }
            return "{[page." + varName + "]}";

        }

        /// <summary>
        ///动态页生成后的文件内容
        /// </summary>
        /// <param name="dynamicTemplate">动态页模板</param>
        /// <param name="template">页模板</param>
        /// <param name="parseinfo">解析对象</param>
        /// <returns></returns>
        public static string getDynamicPage(string dynamicTemplate, string template, ParseInfo parseInfo)
        {
            template = replaceDynamicPageCommonExpression(template, parseInfo.XNLPage);
            parseInfo.XNLPage.customTagColls = XNLBaseCommon.getTemplateUserTag(template, parseInfo.curChannel.siteWebPath);
            //替换动态模板页变量
            StringBuilder dynamicDocument = new StringBuilder(dynamicTemplate);
            dynamicDocument.Replace("@charset", parseInfo.charset);
            dynamicDocument.Replace("@nodeId", parseInfo.curChannel.nodeID.ToString());
            dynamicDocument.Replace("@pageStyle", parseInfo.pageStyle);
            dynamicDocument.Replace("@projectId", parseInfo.XNLPage.templateProjectId.ToString());

            StringBuilder templateSb = new StringBuilder(Convert.ToInt32(template.Length * 1.5));
            templateSb.Append("setTemplate(\"" +replaceSomeChar(template) + "\");");

            if (parseInfo.XNLPage.customTagColls != null && parseInfo.XNLPage.customTagColls.Count > 0)
            {
                foreach (KeyValuePair<string, string> KeyValue in parseInfo.XNLPage.customTagColls)
                {
                    templateSb.AppendLine("addMyTag(\"" + KeyValue.Key + "\",\"" + replaceSomeChar(replaceDynamicPageCommonExpression(KeyValue.Value, parseInfo.XNLPage)) + "\");");
                }
            }
            if (parseInfo.pageStyle.Equals("2"))
            {
                int i = 0;
                string t_fiels = string.Empty;
                if (parseInfo.XNLPage.items != null && parseInfo.XNLPage.items.ContainsKey("@_c_f"))
                {
                    List<string> fieldList = (List<string>)parseInfo.XNLPage.items["@_c_f"];
                    foreach (string str in fieldList)
                    {
                        t_fiels += (i == 0 ? str : ("," + str));
                        i++;
                    }
                }
                templateSb.AppendLine("setContentField(\"" + t_fiels + "\");");
            }
            dynamicDocument.Replace("@template", templateSb.ToString());
            return dynamicDocument.ToString();
        }

        //public static string replaceExtExpression(string labelStr, XNLContext xnlContext)
        //{
        //    replaceExtExpression(labelStr, (XNLWebCommon)xnlContext);
        //}
        /// <summary>
        /// 替换基本表达式
        /// </summary>
        /// <param name="labelStr"></param>
        /// <returns></returns>
        public static string replaceExtExpression(string labelStr, WebContext XNLPage)
        {
            //站点，栏目，页，系统，内容
            //{[site.+]},{[channel.+]},{[page.+]},{[system.+]},{[content.+]}
            //{[system.url]},
            //{[site.id]},{[site.path]},{[site.title]},{[site.url]},{[site.ico]},{[site.logo]},{[site.Summary]},{[site.mateKeywords]},{[site.mateDesc]},{[site.charset]},{[site.name]},
            //{[channel.id]},{[channel.path]},{[channel.url]},{[channel.Summary]},{[channel.mateKeywords]},{[channel.mateDesc]},{[channel.name]},{[channel.indexName]},{[channel.logo]},{[channel.modelName]}
            //{[page.location]},{[page.url]},{[page.fileName]},{[page.title]},{[page.path]},{[page.fileExt]},{[page.file]}
            //{[content.id]},{[content.keyWord]},{[content.state]},{[content.InputUser]},{[content.isRecycle]},{[content.Hits]},{[content.PinYinTitle]},{content.pyTitle},{[content.titleColor]},{[content.underLine]},{[content.Italic]},{[content.Bold]},{[content.IsRecommend]},{[content.IsHot]},{[content.IsColor]},{[content.IsTop]},{[content.title]},{[content.subTitle]},{[content.ImageUrl]},{[content.Summary]},{[content.linkUrl]},{[content.Author]},{[content.Source]},{[content.FileUrl]},{[content.Content]},{[content.AddDate]},{[content.formatTitle]},{[content.prev]},{[content.next]},{[content.prevTitle]},{[content.prevUrl]},{[content.nextTitle]},{[content.nextUrl]},{[content.prevId]},{[content.nextId]},{[content.prevImageUrl]},{[content.nextImageUrl]}
            MatchCollection matchColls = Regex.Matches(labelStr, "{\\[(channel|content|site|system|page)\\.(.+?)\\]}", XNL_RegexOptions);
            if (matchColls.Count > 0)
            {
                StringBuilder labelSb = new StringBuilder(labelStr);
                Dictionary<string, List<string>> colls = new Dictionary<string, List<string>>();
                foreach (Match match in matchColls)
                {
                    GroupCollection groups = match.Groups;
                    string trueName = groups[1].Value;
                    string name = trueName.ToLower();
                    string varName = groups[2].Value.ToLower();
                    List<string> coll;
                    if (!colls.TryGetValue(trueName, out coll))
                    {
                        coll = new List<string>();
                        colls.Add(trueName, coll);
                    }
                    switch (name)
                    {
                        case "channel":
                            if (!coll.Contains(varName))
                            {
                                labelSb.Replace(groups[0].Value, getChannelVariable(XNLPage.curChannel, XNLPage, varName));
                                coll.Add(varName);
                            }
                            break;
                        case "site":
                            if (!coll.Contains(varName))
                            {
                                labelSb.Replace(groups[0].Value, getSiteVariable(XNLPage.curChannel.siteNode, varName));
                                coll.Add(varName);
                            }
                            break;
                        case "system":
                            if (!coll.Contains(varName))
                            {
                                labelSb.Replace(groups[0].Value, getSystemVariable(varName));
                                coll.Add(varName);
                            }
                            break;
                        case "page":
                            if (!coll.Contains(varName))
                            {
                                labelSb.Replace(groups[0].Value, getPageVariable(XNLPage, varName));
                                coll.Add(varName);
                            }
                            break;
                        case "content":
                            {
                                if (!varName.Equals("id"))
                                {
                                    DataTable dt = null;
                                    dt = XNLUtils.setContentVariableData(varName, XNLPage);
                                    if (!coll.Contains(varName))
                                    {
                                        labelSb.Replace(groups[0].Value, getContentVariable(XNLPage, varName, dt));
                                        coll.Add(varName);
                                    }
                                }
                                else
                                {
                                    labelSb.Replace(groups[0].Value, XNLPage.contentId.ToString());
                                }
                                break;
                            }
                    }
                }
                return labelSb.ToString();
            }
            return labelStr;
        }

        public static string replaceExtExpressionByName(string expStr, WebContext XNLPage)
        {
            //站点，栏目，页，系统，内容
            //{[site.+]},{[channel.+]},{[page.+]},{[system.+]},{[content.+]}
            //{[system.url]},
            //{[site.id]},{[site.path]},{[site.title]},{[site.url]},{[site.ico]},{[site.logo]},{[site.Summary]},{[site.mateKeywords]},{[site.mateDesc]},{[site.charset]},{[site.name]},
            //{[channel.id]},{[channel.path]},{[channel.url]},{[channel.Summary]},{[channel.mateKeywords]},{[channel.mateDesc]},{[channel.name]},{[channel.indexName]},{[channel.logo]},{[channel.modelName]}
            //{[page.location]},{[page.url]},{[page.fileName]},{[page.title]},{[page.path]},{[page.fileExt]},{[page.file]}
            //{[content.id]},{[content.keyWord]},{[content.state]},{[content.InputUser]},{[content.isRecycle]},{[content.Hits]},{[content.PinYinTitle]},{content.pyTitle},{[content.titleColor]},{[content.underLine]},{[content.Italic]},{[content.Bold]},{[content.IsRecommend]},{[content.IsHot]},{[content.IsColor]},{[content.IsTop]},{[content.title]},{[content.subTitle]},{[content.ImageUrl]},{[content.Summary]},{[content.linkUrl]},{[content.Author]},{[content.Source]},{[content.FileUrl]},{[content.Content]},{[content.AddDate]},{[content.formatTitle]},{[content.prev]},{[content.next]},{[content.prevTitle]},{[content.prevUrl]},{[content.nextTitle]},{[content.nextUrl]},{[content.prevId]},{[content.nextId]},{[content.prevImageUrl]},{[content.nextImageUrl]}
            string[] strs = expStr.Split(new char[] { '.' });
            string varName = strs[1];
            switch (strs[0])
            {
                case "channel":
                   return  getChannelVariable(XNLPage.curChannel, XNLPage, varName);
                case "site":
                   return  getSiteVariable(XNLPage.curChannel.siteNode, varName);
                case "system":
                   return getSystemVariable(varName);
                case "page":
                   return getPageVariable(XNLPage, varName);
                case "content":
                    {
                        if (!varName.Equals("id"))
                        {
                            DataTable dt = null;
                            dt = XNLUtils.setContentVariableData(varName, XNLPage);
                            return getContentVariable(XNLPage, varName, dt);
                        }
                        else
                        {
                            return XNLPage.contentId.ToString();
                        }
                    }
            }
            return "";
        }

        /// <summary>
        /// 替换基本表达式,动态页生成用
        /// </summary>
        /// <param name="labelStr"></param>
        /// <returns></returns>
        public static string replaceDynamicPageCommonExpression(string labelStr, WebContext XNLPage)
        {
            //站点，栏目，页，系统，内容
            //{[site.+]},{[channel.+]},{[page.+]},{[system.+]},{[content.+]}
            //{[system.url]},
            //{[site.id]},{[site.path]},{[site.title]},{[site.url]},{[site.ico]},{[site.logo]},{[site.Summary]},{[site.mateKeywords]},{[site.mateDesc]},{[site.charset]},{[site.name]},
            //{[channel.id]},{[channel.path]},{[channel.url]},{[channel.Summary]},{[channel.mateKeywords]},{[channel.mateDesc]},{[channel.name]},{[channel.indexName]},{[channel.logo]},{[channel.modelName]}
            //{[page.location]},{[page.url]},{[page.fileName]},{[page.title]},{[page.path]},{[page.fileExt]},{[page.file]}
            //{[content.id]},{[content.keyWord]},{[content.state]},{[content.InputUser]},{[content.isRecycle]},{[content.Hits]},{[content.PinYinTitle]},{content.pyTitle},{[content.titleColor]},{[content.underLine]},{[content.Italic]},{[content.Bold]},{[content.IsRecommend]},{[content.IsHot]},{[content.IsColor]},{[content.IsTop]},{[content.title]},{[content.subTitle]},{[content.ImageUrl]},{[content.Summary]},{[content.linkUrl]},{[content.Author]},{[content.Source]},{[content.FileUrl]},{[content.Content]},{[content.AddDate]},{[content.formatTitle]},{[content.prev]},{[content.next]},{[content.prevTitle]},{[content.prevUrl]},{[content.nextTitle]},{[content.nextUrl]},{[content.prevId]},{[content.nextId]},{[content.prevImageUrl]},{[content.nextImageUrl]}
            MatchCollection matchColls = Regex.Matches(labelStr, "{\\[(channel|site|system|page)\\.(.+?)\\]}", XNL_RegexOptions);
            if (matchColls.Count > 0)
            {
                StringBuilder labelSb = new StringBuilder(labelStr);
                foreach (Match match in matchColls)
                {
                    GroupCollection groups = match.Groups;
                    string name = groups[1].Value.ToLower();
                    string varName = groups[2].Value.ToLower();
                    switch (name)
                    {
                        case "channel":
                            labelSb.Replace(groups[0].Value, getChannelVariable(XNLPage.curChannel, XNLPage, varName));
                            break;
                        case "site":
                            labelSb.Replace(groups[0].Value, getSiteVariable(XNLPage.curChannel.siteNode, varName));
                            break;
                        case "system":
                            labelSb.Replace(groups[0].Value, getSystemVariable(varName));
                            break;
                        case "page":
                            labelSb.Replace(groups[0].Value, getDynamicPagePageVariable(XNLPage, varName));
                            break;
                    }
                }
                return labelSb.ToString();
            }
            return labelStr;
        }


        /// <summary>
        /// 得到系统变量值
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static string getSystemVariable(string varName)
        {
            switch (varName.ToLower())
            {
                case "url":
                    return SystemConfig.systemUrl;
            }
            return "";
        }
        /// <summary>
        /// 得到动态页模板内容
        /// </summary>
        /// <returns></returns>
        public static string loadDynamicTemplate()
        {
            string pStr = (SystemConfig.systemDir.EndsWith("\\") ? "" : "\\");
            string pathStr = SystemConfig.systemDir + pStr + "GlobalFiles\\Page\\Template.ascx";
            StreamReader TxtReader = new StreamReader(pathStr, System.Text.Encoding.UTF8);
            string FileContent = TxtReader.ReadToEnd();
            TxtReader.Close();
            return FileContent;
        }
        public static void writeDynamicPage(string filePath, string responseStr)
        {
            //空文件路径
            string epStr = (SystemConfig.systemDir.EndsWith("\\") ? "" : "\\");
            string epathStr = SystemConfig.systemDir + epStr + "GlobalFiles\\Page\\Empty.ascx";
            File.Copy(epathStr, filePath, true);
            File.WriteAllText(filePath, responseStr, Encoding.UTF8);
        }
        public static string replaceSomeChar(string str)
        {
            StringBuilder sb = new StringBuilder(Convert.ToInt32(str.Length * 1.5));
            sb.Append(Regex.Replace(str, "</script>", "<XNL۞script>",XNL_RegexOptions));
            sb.Replace("\\", "\\\\");
            sb.Replace("\"", "\\\"");
            sb.Replace("\r", "\\r");
            sb.Replace("\t", "\\t");
            sb.Replace("\n", "\\n");
            return sb.ToString();
        }

        /// <summary>
        /// 设置验证成功后的信息
        /// </summary>
        /// <param name="labelContentStr"></param>
        /// <param name="matchSuccessItem"></param>
        /// <param name="matchfailItem"></param>
        /// <returns></returns>
        public static string setValidatorSuccessItem(string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            string successItemStr = "";
            string errorItemStr = "";
            string successContentStr = "";
            if (matchSuccessItem.Count > 0)
            {
                successItemStr = matchSuccessItem[0].Groups[0].Value;
                successContentStr = matchSuccessItem[0].Groups[3].Value;
            }
            if (matchFailItem.Count > 0)
            {
                errorItemStr = matchFailItem[0].Groups[0].Value;
            }
            labelContentStr = labelContentStr.Replace(successItemStr, successContentStr);
            labelContentStr = labelContentStr.Replace(errorItemStr, "");
            return labelContentStr;
        }
        /// <summary>
        /// 设置验证失败后的信息
        /// </summary>
        /// <param name="labelContentStr"></param>
        /// <param name="matchSuccessItem"></param>
        /// <param name="matchFailItem"></param>
        /// <param name="vInfos"></param>
        /// <returns></returns>
        public static string setValidatorErrorItem(string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem, ValidatorInfos vInfos)
        {
            string successItemStr = "";
            string errorItemStr = "";
            string errorItemContentStr = "";
            if (matchSuccessItem.Count > 0)
            {
                successItemStr = matchSuccessItem[0].Groups[0].Value;
            }
            if (matchFailItem.Count > 0)
            {
                errorItemStr = matchFailItem[0].Groups[0].Value;
                errorItemContentStr = matchFailItem[0].Groups[3].Value;
            }
            //先注释 通过编译
            //MatchCollection errorListMatch = XNLCommon.matchsItemTagByName(errorItemContentStr, matchFailItem[0].Groups[1].Value + ".Item");
            //string errorItemListMatchStr = "";
            //string errorItemListContentStr = "";
            //if (errorListMatch.Count > 0)
            //{
            //    errorItemListMatchStr = errorListMatch[0].Groups[0].Value;
            //    errorItemListContentStr = errorListMatch[0].Groups[3].Value;
            //}

            //if (!successItemStr.Equals("")) labelContentStr = labelContentStr.Replace(successItemStr, "");

            //if (!errorItemStr.Equals(""))
            //{
            //    string _tmpErrorListStr = "";
            //    if (vInfos != null && vInfos.errorMsgList.Count > 0)
            //    {
            //        foreach (KeyValuePair<string, string> i in vInfos.errorMsgList)
            //        {
            //            _tmpErrorListStr += XNLCommon.replaceAttribleVariableByName(errorItemListContentStr, matchFailItem[0].Groups[1].Value + ".Msg", i.Value);
            //        }
            //    }
            //    errorItemContentStr = errorItemContentStr.Replace(errorItemListMatchStr, _tmpErrorListStr);
            //    labelContentStr = labelContentStr.Replace(errorItemStr, errorItemContentStr);
            //}
            return labelContentStr;
        }
    }
}

