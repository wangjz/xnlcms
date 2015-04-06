using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Web;
using LitJson;
using COM.SingNo.Common;
using System.Web.UI;
using COM.SingNo.DAL;
using System.Text.RegularExpressions;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class HandleForm:IXNLTag<WebContext>
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
             if (XNLPage.Context.Request["XNL__VIEWSTATE"] == null) return "";
             string XNl_viewStateStr = XNLPage.Context.Request["XNL__VIEWSTATE"].Trim();
             MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "HandleForm.Success");
             MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "HandleForm.Error");
              try
              {
                    XNl_viewStateStr=EncrypString.Decrypto(EncrypString.MD5("singnocms"),XNl_viewStateStr).Trim();
               }
               catch
               {
                   Dictionary<string, string> errorList = new Dictionary<string, string>();
                   errorList.Add("1", "参数错误!");
                   labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                   return labelContentStr;
               }
                 JsonData xnlFromData = JsonMapper.ToObject(XNl_viewStateStr);              
                 if (! Util.jsonHasAttriable(xnlFromData.ToJson(),"sql"))
                 {
                     Dictionary<string, string> errorList = new Dictionary<string, string>();
                     errorList.Add("1", "参数错误!");
                     labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                     return labelContentStr;
                 }

                 ValidatorInfos vinfos = ValidatorCommon.getValidatorInfos(XNl_viewStateStr, labelParams);
                 labelParams = vinfos.Params;
                 //服务端验证 end
                 string sqlCommandStr = xnlFromData["sql"].ToString();
                 string[] splits_arr = {xnlFromData["sp"].ToString()};
                 string[] sqlStrs = sqlCommandStr.Split(splits_arr, StringSplitOptions.RemoveEmptyEntries);
                 Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                 for (var i = 0; i < sqlStrs.Length; i++)
                 {
                     string temSql = RegxpEngineCommon.replaceReqFormVariable(sqlStrs[i]);
                     temSql = RegxpEngineCommon.replaceSessionVariable(temSql);
                     temSql = RegxpEngineCommon.replaceApplicationVariable(temSql);
                     temSql = RegxpEngineCommon.replaceAttribleVariable(labelParams, temSql);
                     sqlColls.Add(Convert.ToString(i), temSql);
                 }
                 DbResultInfo dbResultInfo = DataHelper.ExrcuteScalarSomeSqlWithTransaction(sqlColls, labelParams);
                 if (dbResultInfo.isSuccess)
                 {
                     labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                 }
                 else
                 {
                     Dictionary<string, string> errorList = new Dictionary<string, string>();
                     errorList.Add("1", dbResultInfo.exception.Message);
                     labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                 }
                 labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
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
