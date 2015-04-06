using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
using COM.SingNo.XNLEngine;
using COM.SingNo.Common;
using System.Text.RegularExpressions;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class AdminLogin:IXNLTag<WebContext>
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
       /// <summary>
       /// 管理员登陆
       /// </summary>
       /// <param name="labelParams"></param>
       /// <param name="labelContentStr"></param>
       /// <param name="XNLPage"></param>
       /// <returns></returns>
       public string main(XNLTagStruct tagStruct, WebContext XNLPage)
       {
           /*
           labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
           MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminLogin.Success");
           MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminLogin.Error");
           try
           {
               string loginName = labelParams["loginname"].value.ToString().Trim();
               if (loginName.Equals(""))
               {
                   throw (new Exception("用户名不能为空。"));
               }
               string password = labelParams["password"].value.ToString().Trim();
               if (password.Equals(""))
               {
                   throw (new Exception("密码不能为空。"));
               }
               string verifyCode = labelParams["verifycode"].value.ToString().Trim();
               if (verifyCode.Equals(""))
               {
                   throw (new Exception("验证码不能为空。"));
               }
               if (!VerifyCode.Verify(verifyCode, true))
               {
                   throw (new Exception("验证码不正确。"));
               }
               Admin admin = new Admin();
               int stateNum = admin.fillInfoByName(loginName, password);
               switch (stateNum)
               {
                   case 0:
                       throw (new Exception("用户名或密码不正确。"));
                   case 1:
                       admin.loginIn();
                       break;
                   case -1:
                       throw (new Exception("此用户所属角色已不存在。"));
                   case -2:
                       throw (new Exception("用户已被锁定。"));
               }
           }
           catch (System.Exception ex)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               errorList.Add("1", ex.Message);
               labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
           }
           return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
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
