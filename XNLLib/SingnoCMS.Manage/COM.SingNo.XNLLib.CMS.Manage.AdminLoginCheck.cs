using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
    /// <summary>
    /// 管理员登陆检查
    /// </summary>
   public class AdminLoginCheck:IXNLTag<WebContext>
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
        //<AdminLoginCheck.yesItem></AdminLoginCheck.yesItem> <AdminLoginCheck.noItem></AdminLoginCheck.noItem>
       public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
            /*
             MatchCollection yesMatchs=RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminLoginCheck.yesItem");
             Match yesMatch=null;
             if(yesMatchs.Count>0)yesMatch=yesMatchs[0];
             Match noMatch = null;
             MatchCollection noMatchs=RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminLoginCheck.noItem");
             if (noMatchs.Count > 0) noMatch = noMatchs[0];
             XNLParam responseEndParam;
             bool responseEnd = true;
             if (labelParams.TryGetValue("responseend", out responseEndParam))
             {
                 responseEnd = string.Compare(responseEndParam.value.ToString(), "true", true) == 0 ? true : false;
             }
             if(Admin.loginCheck())
             {
                 if(noMatch!=null)labelContentStr = labelContentStr.Replace(noMatch.Value, "");
                 if (yesMatch != null) labelContentStr = labelContentStr.Replace(yesMatch.Value, yesMatch.Groups[3].Value);
             }
             else
             {
                 if (yesMatch != null)
                 {
                     labelContentStr = labelContentStr.Replace(yesMatch.Value, "");
                 }
                 if (noMatch == null)
                 {
                     labelContentStr = "未登录";
                 }
                 else
                 {
                      labelContentStr = labelContentStr.Replace(noMatch.Value, noMatch.Groups[3].Value);
                 }
                 XNLPage.Context.Response.Write(labelContentStr);
                 if (responseEnd) XNLPage.Context.Response.End();
             }
              return labelContentStr;
             */
            return "";
        }
        #endregion

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
