using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Manage;
using System.Data;
using COM.SingNo.Common;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
    public class CreatePage : IXNLTag<WebContext>
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
            return "";
            /*
            MatchCollection matchSuccessItem ;
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "CreatePage.Error");;
            string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
            try
            {
                if (actionStr.Equals("create"))
                {
                    labelParams.Add("createpage.progress",new XNLParam(XNLType.String,Create(labelParams, labelContentStr, XNLPage)));
                }
                else if (actionStr.Equals("cancle"))
                {
                    Cancle(labelParams, "", XNLPage);
                    labelParams.Add("createpage.progress", new XNLParam(XNLType.String, "生成任务已取消!"));
                }
                labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
                matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "CreatePage.Success");
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (Exception e)
            {
                Cancle(labelParams, labelContentStr, XNLPage);
                matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "CreatePage.Success");
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
            */
        }
        /*
        private string Create(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            string pageStyle = labelParams["pagestyle"].value.ToString();
            string nodeId = Convert.ToString(labelParams["nodeid"].value);
            int ProgectId = Convert.ToInt32(labelParams["projectid"].value);
            bool debug = Convert.ToString(labelParams["debug"].value).ToLower().Equals("true") ? true : false;
            string nodeListStr = labelParams["nodelist"].value.ToString();
            ParseAndCreat parseProcess;
            switch (pageStyle)
            {
                case "0":
                    parseProcess = new ParseAndCreat(nodeId, pageStyle, ProgectId, debug, XNLPage,labelParams);
                    return parseProcess.toParseCreat();
                case "1":  //节点生成
                case "2":
                    parseProcess = new ParseAndCreat(nodeListStr, pageStyle, ProgectId, debug, XNLPage,labelParams);
                    return parseProcess.toParseCreat();
                default:
                    string pageListStr = labelParams["pagelist"].value.ToString();
                    parseProcess = new ParseAndCreat(pageListStr, pageStyle, ProgectId, debug, XNLPage, labelParams);
                    return parseProcess.toParseCreat();
            }
        }

        private void Cancle(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            string pageStyle = labelParams["pagestyle"].value.ToString();
            Cchannel site = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
            switch (pageStyle)
            {
                case "0":
                    site.indexPageCreateThreadInfo.cancle();
                    break;
                case "1":  //节点生成
                    site.channelPageCreateThreadInfo.cancle();
                    break;
                case "2":
                    site.contentPageCreateThreadInfo.cancle();
                    break;
                default:
                    site.singlePageCreateThreadInfo.cancle();
                    break;
            }
        }
        */
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
