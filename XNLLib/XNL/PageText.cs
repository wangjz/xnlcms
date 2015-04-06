using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class PageText:IXNLTag<WebContext>
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
       //auto,tag,no
       //type,words,pagename
       public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
           /*
            labelParams.Add("perpagerecordsnum",new XNLParam(1));
            labelParams.Add("curpagenum", new XNLParam(1));  //当前页
            labelParams.Add("totalrecordsnum", new XNLParam(1)); 
            //totalrecordsnum,curpagenum
            XNLParam _param;
            string _type = "no";
            if (labelParams.TryGetValue("type",out _param))
            {
                _type = _param.value.ToString();
            }
            if (_type.Equals("no"))
            {
                return labelContentStr.Replace("[#xnl_page#]","");
            }
            else
            {
                string pagename = "";
                int curpage = 1;
                int allpage = 1;
                switch (_type)
                {
                    case "auto":
                        labelContentStr = labelContentStr.Replace("[#xnl_page#]", "");
                        int autonum;
                        if (labelParams.TryGetValue("auto", out _param))
                        {
                            autonum = Convert.ToInt32(_param.value);
                        }
                        else
                        {
                            autonum = XNLPage.curChannel.theChannelConfig.baseConfig.autoWordNums;
                        }
                        if (labelContentStr.Length <= autonum)
                        {
                            return labelContentStr;
                        }
                        else
                        {
                            pagename = getPageName(labelParams, _param);
                            //计算可以分几页
                            double allnum = labelContentStr.Length / autonum;
                            int znum = Convert.ToInt32(Math.Floor(allnum));
                            allpage = (znum + (allnum - znum > 0 ? 1 : 0));
                            labelParams["totalrecordsnum"].value = allpage;
                            //得到当前页
                            curpage = getCurPageNum(labelParams, _param, XNLPage, pagename,allpage);
                            //得到当前内容
                            return labelContentStr.Substring(autonum * (curpage - 1), autonum * curpage);
                        }
                    case "tag":
                        string[] content_arr = labelContentStr.Split(new string[] { "[#xnl_page#]" }, StringSplitOptions.RemoveEmptyEntries);
                        if (content_arr.Length > 0)
                        {
                            pagename = getPageName(labelParams, _param);
                            labelParams["totalrecordsnum"].value = content_arr.Length;
                            curpage = getCurPageNum(labelParams, _param, XNLPage, pagename,content_arr.Length);
                            return content_arr[curpage - 1];
                        }
                        else
                        {
                            return labelContentStr;
                        }
                }
            }
            return labelContentStr.Replace("[#xnl_page#]", "");
            */
            return "";
        }

        #endregion
        public string getPageName(Dictionary<string, XNLParam> labelParams,XNLParam _params)
        {
            if (labelParams.TryGetValue("pagename", out _params))
            {
                return _params.value.ToString();
            }
            else
            {
                throw new Exception("没有pagename属性");
            }
        }
        public int getCurPageNum(Dictionary<string, XNLParam> labelParams, XNLParam _params, WebContext XNLPage, string pagename, int allpage)
        {
            int curpage=1;
            if (XNLPage.accessType == AccessType.Static)
            {
                ParseInfo parseInfo = (ParseInfo)labelParams["_parseinfo_"].value;
                if (parseInfo != null && parseInfo.pageRequestColls != null)
                {
                    curpage = Convert.ToInt32(parseInfo.pageRequestColls[pagename]);
                }
            }
            else
            {
                if (!(XNLPage.Context.Request[pagename] == null))
                {
                    int.TryParse(XNLPage.Context.Request[pagename], out curpage);
                }
            }
            if (curpage < 1)
            {
                curpage = 1;
            }
            else if(curpage>allpage)
            {
                curpage = allpage;
            }
            labelParams["curpagenum"].value = curpage;
            return curpage;
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
