using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using COM.SingNo.Common;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
    public  class ValidatorTest:IXNLTag<WebContext>
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
            labelContentStr = XNLPage.xnlParser.replaceAttribleVariable(labelContentStr,labelParams,XNLPage);
            MatchCollection valTestColl = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "XNL", "validatorTest");
            labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, valTestColl);
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "validatorTest.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "validatorTest.Error");
            bool responseEnd = false;
            XNLParam responseEndParam;
            if (labelParams.TryGetValue("responseend", out responseEndParam))
            {
                responseEnd = string.Compare(responseEndParam.value.ToString(),"true",true)==0 ? true : false;
            }
            if (XNLPage.Context.Request["XNL__VIEWSTATE"] == null)
            {
                Dictionary<string,string> errorList=new Dictionary<string,string>();
                errorList.Add("1","参数错误！");
                labelContentStr=XNLWebCommon.setValidatorErrorItem(labelContentStr,matchSuccessItem,matchFailItem,new ValidatorInfos(errorList));
                labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, valTestColl);
                if(responseEnd)
                {
                    XNLPage.Context.Response.Write(labelContentStr);
                    XNLPage.Context.Response.End();
                }
                return labelContentStr;
            }
            string XNl_viewStateStr = XNLPage.Context.Request["XNL__VIEWSTATE"].Trim();
            try
            {
                XNl_viewStateStr = EncrypString.Decrypto(EncrypString.MD5("singnocms"), XNl_viewStateStr).Trim();
            }
            catch
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "参数错误！");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, valTestColl);
                if (responseEnd)
                {
                    XNLPage.Context.Response.Write(labelContentStr);
                    XNLPage.Context.Response.End();
                }
                return labelContentStr;
            }
            ValidatorInfos vInfos = ValidatorCommon.getValidatorInfos(XNl_viewStateStr);
            if (vInfos.errorsCount > 0)
            {
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, vInfos);
                labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, valTestColl);
                if (responseEnd)
                {
                    XNLPage.Context.Response.Write(labelContentStr);
                    XNLPage.Context.Response.End();
                }
            }
            else
            {
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, valTestColl);
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
