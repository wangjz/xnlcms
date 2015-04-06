using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
using COM.SingNo.XNLEngine;
namespace COM.SingNo.XNLLib.XNL
{
    public class Page:IXNLTag<WebContext>
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
        public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
            /*
            string labelContent = labelContentStr;
            AccessType accessType = XNLPage.accessType;
            MatchCollection pageMatchs = RegxpEngineCommon.matchsXNLTagByName(labelContent, "XNL", "page");
            labelContent = RegxpEngineCommon.disableNestedXNLTag(labelContent, pageMatchs);
            MatchCollection pageListMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "pageListTemplete");
            MatchCollection pageSeparatorMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "pageSeparatorTemplete");
            if (pageListMatchs.Count > 0) labelContent = labelContent.Replace(pageListMatchs[0].Groups[0].Value, "<pageListDisableString></pageListDisableString>");
            if (pageSeparatorMatchs.Count > 0) labelContent = labelContent.Replace(pageSeparatorMatchs[0].Groups[0].Value, "<pageSeparatorDisableString></pageSeparatorDisableString>");
            MatchCollection firstPageMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "firstPageTemplete");
            MatchCollection lastPageMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "lastPageTemplete");
            MatchCollection currentPageMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "currentPageTemplete");
            MatchCollection prevPageMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "prevPageTemplete");
            MatchCollection nextPageMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "nextPageTemplete");
            MatchCollection noPageListMatchs = RegxpEngineCommon.matchsItemTagByName(labelContent, "noPageListTemplete");
            string firstPageTemStr = "";
            string lastPageTemStr = "";
            string curPageTemStr = "";
            string prevPageTemStr = "";
            string nextPageTemStr = "";
            string pageListTemStr = "";
            string pageSeparatorTemStr = "";
            string noPageListTemStr = "";

            if (firstPageMatchs.Count > 0)
            {
                firstPageTemStr = firstPageMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(firstPageMatchs[0].Groups[0].Value, "<firstPageDisableString></firstPageDisableString>");
            };

            if (lastPageMatchs.Count > 0)
            {
                lastPageTemStr = lastPageMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(lastPageMatchs[0].Groups[0].Value, "<lastPageDisableString></lastPageDisableString>");

            };

            if (currentPageMatchs.Count > 0)
            {
                curPageTemStr = currentPageMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(currentPageMatchs[0].Groups[0].Value, "<currentPageDisableString></currentPageDisableString>");

            };
            if (prevPageMatchs.Count > 0)
            {
                prevPageTemStr = prevPageMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(prevPageMatchs[0].Groups[0].Value, "<prevPageDisableString></prevPageDisableString>");
            };
            if (nextPageMatchs.Count > 0)
            {
                nextPageTemStr = nextPageMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(nextPageMatchs[0].Groups[0].Value, "<nextPageDisableString></nextPageDisableString>");
            };

            if (pageListMatchs.Count > 0)
            {
                pageListTemStr = pageListMatchs[0].Groups[3].Value;
            };

            if (pageSeparatorMatchs.Count > 0)
            {
                pageSeparatorTemStr = pageSeparatorMatchs[0].Groups[3].Value;
            };

            if (noPageListMatchs.Count > 0)
            {
                noPageListTemStr = noPageListMatchs[0].Groups[3].Value;
                labelContent = labelContent.Replace(noPageListMatchs[0].Groups[0].Value, "<noPageListDisableString></noPageListDisableString>");

            };
            labelContent = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContent); //替换属性
            labelContent = RegxpEngineCommon.enabledNestedXNLTag(labelContent, pageMatchs);
            string curPath =XNLPage.pagePath;
            int curPageNum = Convert.ToInt32(labelParams["curpagenum"].value);
            int firstPageNum = Convert.ToInt32(labelParams["firstpagenum"].value);
            int lastPageNum = Convert.ToInt32(labelParams["lastpagenum"].value);
            int prevPageNum = Convert.ToInt32(labelParams["prevpagenum"].value);
            int nextPageNum = Convert.ToInt32(labelParams["nextpagenum"].value);
            int totalPageNum = Convert.ToInt32(labelParams["totalpagesnum"].value);
            string  pageReqName =Convert.ToString(labelParams["pagename"].value);
            ParseInfo parseInfo = null;
            if (accessType == AccessType.Static)  //静态
            {
                parseInfo = (ParseInfo)labelParams["_parseinfo_"].value;
            }
            if (Convert.ToInt32(labelParams["totalpagesnum"].value) > 1)
            {
                if (noPageListMatchs.Count > 0) labelContent = labelContent.Replace("<noPageListDisableString></noPageListDisableString>", ""); 
                string matchReqStr=Regex.Escape(pageReqName + "=" )+ "[\\d]*";
                Regex matchReqReg=new Regex(matchReqStr,RegexOptions.IgnoreCase);
                Match pathMatch = matchReqReg.Match(curPath);
                if (firstPageMatchs.Count > 0)
                {
                    //{@firstpagelink}
                    if (accessType == AccessType.Static)  //静态
                    {
                        string LinkStr = setStaticLink(parseInfo, pageReqName, lastPageNum, 0, XNLPage, "first");
                        labelParams.Add("firstpagelink", new XNLParam(LinkStr));
                    }
                    else if (accessType == AccessType.PseudoStatic) //伪静态
                    {

                    }
                    else //动态
                    {
                        string firstLink = pageReqName + "=" + Convert.ToString(firstPageNum);
                        if (pathMatch.Success)
                        {
                            firstLink = curPath.Replace(pathMatch.Value, firstLink);
                        }
                        else
                        {
                            if (curPath.IndexOf("?") > 0)
                            {
                                firstLink = curPath + "&" + firstLink;
                            }
                            else
                            {
                                firstLink = curPath + "?" + firstLink;
                            }
                        }
                        labelParams.Add("firstpagelink", new XNLParam(firstLink));
                    }
                    
                    MatchCollection effectiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(firstPageTemStr, "effectiveState");
                    MatchCollection inactiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(firstPageTemStr, "inactiveState");
                    if (curPageNum > 1)
                    {
                        if (inactiveStateMatchs.Count > 0) firstPageTemStr = firstPageTemStr.Replace(inactiveStateMatchs[0].Value, "");
                        if (effectiveStateMatchs.Count > 0) firstPageTemStr = firstPageTemStr.Replace(effectiveStateMatchs[0].Value, effectiveStateMatchs[0].Groups[3].Value);
                    }
                    else
                    {
                        if (effectiveStateMatchs.Count > 0) firstPageTemStr = firstPageTemStr.Replace(effectiveStateMatchs[0].Value, "");
                        if (inactiveStateMatchs.Count > 0) firstPageTemStr = firstPageTemStr.Replace(inactiveStateMatchs[0].Value, inactiveStateMatchs[0].Groups[3].Value);
                    }
                    labelContent = labelContent.Replace("<firstPageDisableString></firstPageDisableString>", RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(firstPageTemStr, pageMatchs)));
                }

                if (lastPageMatchs.Count > 0)
                {

                    if (accessType == AccessType.Static)
                    {
                        string LinkStr = setStaticLink(parseInfo, pageReqName, lastPageNum, 0, XNLPage, "last");
                        labelParams.Add("lastpagelink", new XNLParam(LinkStr));
                    }
                    else if (accessType == AccessType.PseudoStatic)
                    {

                    }
                    else  //动态页
                    {
                        string lastLink =pageReqName + "=" + Convert.ToString(lastPageNum);
                        if (pathMatch.Success)
                        {
                            lastLink = curPath.Replace(pathMatch.Value, lastLink);
                        }
                        else
                        {
                            if (curPath.IndexOf("?") > 0)
                            {
                                lastLink = curPath + "&" + lastLink;
                            }
                            else
                            {
                                lastLink = curPath + "?" + lastLink;
                            }

                        }
                        labelParams.Add("lastpagelink", new XNLParam(lastLink));
                    }
                    MatchCollection effectiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(lastPageTemStr, "effectiveState");
                    MatchCollection inactiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(lastPageTemStr, "inactiveState");
                    if (curPageNum <totalPageNum)
                    {
                        if (inactiveStateMatchs.Count > 0) lastPageTemStr = lastPageTemStr.Replace(inactiveStateMatchs[0].Value, "");
                        if (effectiveStateMatchs.Count > 0) lastPageTemStr = lastPageTemStr.Replace(effectiveStateMatchs[0].Value, effectiveStateMatchs[0].Groups[3].Value);
                    }
                    else
                    {
                        if (effectiveStateMatchs.Count > 0) lastPageTemStr = lastPageTemStr.Replace(effectiveStateMatchs[0].Value, "");
                        if (inactiveStateMatchs.Count > 0) lastPageTemStr = lastPageTemStr.Replace(inactiveStateMatchs[0].Value, inactiveStateMatchs[0].Groups[3].Value);
                    }

                    labelContent = labelContent.Replace("<lastPageDisableString></lastPageDisableString>", RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(lastPageTemStr, pageMatchs)));
                }

                if (prevPageMatchs.Count > 0)
                {

                    if (accessType == AccessType.Static)
                   {
                       string LinkStr = setStaticLink(parseInfo, pageReqName, lastPageNum, 0, XNLPage, "prev");
                       labelParams.Add("prevpagelink", new XNLParam(LinkStr));
                   }
                    else if (accessType == AccessType.PseudoStatic)
                   {
                   }
                   else //动态
                   {
                       string prevLink =pageReqName + "=" + Convert.ToString(prevPageNum);
                       if (pathMatch.Success)
                       {
                           prevLink = curPath.Replace(pathMatch.Value, prevLink);
                       }
                       else
                       {
                           if (curPath.IndexOf("?") > 0)
                           {
                               prevLink = curPath + "&" + prevLink;
                           }
                           else
                           {
                               prevLink = curPath + "?" + prevLink;
                           }

                       }
                       labelParams.Add("prevpagelink", new XNLParam(prevLink));
                   }
                   MatchCollection effectiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(prevPageTemStr, "effectiveState");
                   MatchCollection inactiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(prevPageTemStr, "inactiveState");
                   if (curPageNum > 1)
                   {
                       if (inactiveStateMatchs.Count > 0) prevPageTemStr = prevPageTemStr.Replace(inactiveStateMatchs[0].Value, "");
                       if (effectiveStateMatchs.Count > 0) prevPageTemStr = prevPageTemStr.Replace(effectiveStateMatchs[0].Value, effectiveStateMatchs[0].Groups[3].Value);
                   }
                   else
                   {
                       if (effectiveStateMatchs.Count > 0) prevPageTemStr = prevPageTemStr.Replace(effectiveStateMatchs[0].Value, "");
                       if (inactiveStateMatchs.Count > 0) prevPageTemStr = prevPageTemStr.Replace(inactiveStateMatchs[0].Value, inactiveStateMatchs[0].Groups[3].Value);
                   }
                   labelContent = labelContent.Replace("<prevPageDisableString></prevPageDisableString>", RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(prevPageTemStr, pageMatchs)));
                }
                if (nextPageMatchs.Count > 0)
                {
                    if (accessType == AccessType.Static)
                   {
                       string LinkStr = setStaticLink(parseInfo, pageReqName, lastPageNum, 0, XNLPage, "next");
                       labelParams.Add("nextpagelink", new XNLParam(LinkStr));
                   }
                    else if (accessType == AccessType.PseudoStatic)
                   {
                   }
                   else //动态
                   {
                       string nextLink = pageReqName + "=" + Convert.ToString(nextPageNum);
                       if (pathMatch.Success)
                       {
                           nextLink = curPath.Replace(pathMatch.Value, nextLink);
                       }
                       else
                       {
                           if (curPath.IndexOf("?") > 0)
                           {
                               nextLink = curPath + "&" + nextLink;
                           }
                           else
                           {
                               nextLink = curPath + "?" + nextLink;
                           }
                       }
                       labelParams.Add("nextpagelink", new XNLParam(nextLink));
                   }
                   MatchCollection effectiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(nextPageTemStr, "effectiveState");
                   MatchCollection inactiveStateMatchs = RegxpEngineCommon.matchsItemTagByName(nextPageTemStr, "inactiveState");
                   if (curPageNum < totalPageNum)
                   {
                       if (inactiveStateMatchs.Count > 0) nextPageTemStr = nextPageTemStr.Replace(inactiveStateMatchs[0].Value, "");
                       if (effectiveStateMatchs.Count > 0) nextPageTemStr = nextPageTemStr.Replace(effectiveStateMatchs[0].Value, effectiveStateMatchs[0].Groups[3].Value);
                   }
                   else
                   {
                       if (effectiveStateMatchs.Count > 0) nextPageTemStr = nextPageTemStr.Replace(effectiveStateMatchs[0].Value, "");
                       if (inactiveStateMatchs.Count > 0) nextPageTemStr = nextPageTemStr.Replace(inactiveStateMatchs[0].Value, inactiveStateMatchs[0].Groups[3].Value);
                   }
                   labelContent = labelContent.Replace("<nextPageDisableString></nextPageDisableString>", RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(nextPageTemStr, pageMatchs)));
                }

                if (pageListMatchs.Count > 0)  //页列表
                {
                    if (!curPageTemStr.Equals(string.Empty)) labelContent = labelContent.Replace("<currentPageDisableString></currentPageDisableString>", "");
                    if (!pageSeparatorTemStr.Equals(string.Empty)) labelContent = labelContent.Replace("<pageSeparatorDisableString></pageSeparatorDisableString>", "");
                    if(!noPageListTemStr.Equals(string.Empty))labelContent = labelContent.Replace("<noPageListDisableString></noPageListDisableString>", ""); 
                    XNLParam pageNumParam;
                    StringBuilder listPageStrs = new StringBuilder();

                    if (!labelParams.TryGetValue("pagenum", out pageNumParam))
                    {
                        labelParams.Add("pagenum", new XNLParam(1));
                    }
                    int currentPageNum = Convert.ToInt32(labelParams["curpagenum"].value);
                    int totalPagesNum = Convert.ToInt32(labelParams["totalpagesnum"].value);
                    labelParams.Add("pagelink", new XNLParam());

                    for (var i = 1; i <=totalPagesNum ; i++)
                    {
                        labelParams["pagenum"].value = i;
                        //{@pageLink}
                        if (accessType == AccessType.Static)
                        {
                            string LinkStr = setStaticLink(parseInfo, pageReqName, lastPageNum, i, XNLPage, "list");
                            labelParams["pagelink"].value = LinkStr;
                        }
                        else if (accessType == AccessType.PseudoStatic)
                        {
                        }
                        else  //动态
                        {
                            string pageLink = XNLPage.Context.Server.HtmlEncode(pageReqName + "=" + Convert.ToString(i));
                            if (pathMatch.Success)
                            {
                                pageLink = curPath.Replace(pathMatch.Value, pageLink);
                            }
                            else
                            {
                                if (curPath.IndexOf("?") > 0)
                                {
                                    pageLink = curPath + "&" + pageLink;
                                }
                                else
                                {
                                    pageLink = curPath + "?" + pageLink;
                                }
                            }
                            labelParams["pagelink"].value = pageLink;
                        }
                        
                        if (i == currentPageNum)
                        {
                            if (currentPageMatchs.Count > 0)
                            {
                                listPageStrs.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(curPageTemStr, pageMatchs)));
                            }
                        }
                        else
                        {
                            listPageStrs.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(pageListTemStr, pageMatchs)));
                        }

                        if (pageSeparatorMatchs.Count > 0)
                        {
                            if (i < totalPagesNum)
                            {
                                listPageStrs.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(pageSeparatorTemStr, pageMatchs)));
                            }
                        }
                    }

                    labelContent = labelContent.Replace("<pageListDisableString></pageListDisableString>", Convert.ToString(listPageStrs));
                }
            }
            else
            {    
                    labelContent = labelContent.Replace("<firstPageDisableString></firstPageDisableString>", "");
                    labelContent = labelContent.Replace("<lastPageDisableString></lastPageDisableString>", "");
                    labelContent = labelContent.Replace("<prevPageDisableString></prevPageDisableString>", "");
                    labelContent = labelContent.Replace("<nextPageDisableString></nextPageDisableString>", ""); 
                    labelContent = labelContent.Replace("<pageListDisableString></pageListDisableString>", "");
                    labelContent = labelContent.Replace("<currentPageDisableString></currentPageDisableString>", "");
                    labelContent = labelContent.Replace("<pageSeparatorDisableString></pageSeparatorDisableString>", "");
                    labelContent = labelContent.Replace("<noPageListDisableString></noPageListDisableString>", noPageListTemStr);
            }
            
            return labelContent; 
             */
            return "";
        }
        #endregion
        private string setStaticLink(ParseInfo parseInfo, string pageReqName, int lastPageNum, int pageNum, WebContext XNLPage, string style)
        {
            if (parseInfo == null || parseInfo.pageRequestColls==null) return XNLPage.pagePath;
            string tmpPageMsg = "";
            string splitStr = "";
            int i = 0;
            foreach (KeyValuePair<string, string> req in parseInfo.pageRequestColls)
            {
                if (i > 0) splitStr = "-";
                if (req.Key.Equals(pageReqName))
                {
                    int tmpNum = Convert.ToInt32(req.Value);
                    switch (style)
                    {
                        case "first":
                            tmpPageMsg +=splitStr+ "1";
                            break;
                        case "last":
                            tmpPageMsg += splitStr + lastPageNum.ToString();
                            break;
                        case "prev":
                            tmpNum = (tmpNum - 1 <= 1 ? 1 : tmpNum - 1);
                            tmpPageMsg += splitStr+tmpNum.ToString();
                            break;
                        case "next":
                            tmpNum = (tmpNum + 1 >= lastPageNum ? lastPageNum : tmpNum + 1);
                            tmpPageMsg += splitStr + tmpNum.ToString();
                            break;
                        case "list":
                            tmpPageMsg += splitStr + pageNum.ToString();
                            break;
                    }
                }
                else
                {
                    tmpPageMsg += splitStr + req.Value;
                }
                i++;
            }
            string LinkStr =parseInfo.pageName;
            int pageAddNum = 0;
            string[] pagenum_arr = tmpPageMsg.Split('-');
            for (i = 0; i < pagenum_arr.Length; i++)
            {
                pageAddNum += Convert.ToInt32(pagenum_arr[i])-1;
            }
            if (pageAddNum==0)
            {
                LinkStr += parseInfo.fileExtName;
            }
            else
            {
                LinkStr += "_" + tmpPageMsg + parseInfo.fileExtName;
            }
            return LinkStr;
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
