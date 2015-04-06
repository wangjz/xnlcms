using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.DAL;
using System.Xml;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class ChannelConfig:IXNLTag<WebContext>
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
           string actionStr = labelParams["action"].value.ToString().ToLower();
           string type = labelParams["type"].value.ToString();
           try
           {
               switch (actionStr)
               {
                   case "view":
                       labelContentStr = setViewControlerByType(labelParams, labelContentStr, XNLPage, type);
                       break;
                   case "modify":
                       labelContentStr = setModifyControlerByType(labelParams, labelContentStr, XNLPage, type);
                       break;
                   default:
                       return "controler error";
               }
           }
           catch (Exception e)
           {
               return e.Message;
           }
           return labelContentStr;
           */
           return "";
       }
       #endregion
       /*
       private string setViewControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, string type)
       {
           int channelId = Convert.ToInt32(labelParams["channelid"].value);
           Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[channelId];
           switch (type)
           {
               case "base":
                   labelContentStr = viewBaseConfig(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
               case "comments":
                   labelContentStr = viewCommentsConfig(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
               case "create":
                   labelContentStr = viewCreateConfig(labelParams, labelContentStr, XNLPage, curChannel);
                   break;
               case "contribute":
                   labelContentStr = viewContributeConfig(labelParams, labelContentStr, XNLPage, curChannel);
                   break;
           }
           return labelContentStr;
       }
       private string setModifyControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, string type)
       {
           int channelId = Convert.ToInt32(labelParams["channelid"].value);
           Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[channelId];
           switch (type)
           {
               case "base":
                   labelContentStr = modifyBase(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
               case "comments":
                   labelContentStr = modifyComments(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
               case "create":
                   labelContentStr = modifyCreate(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
               case "contribute":
                   labelContentStr = modifyContribute(labelParams, labelContentStr, XNLPage,curChannel);
                   break;
           }
           return labelContentStr;
       }
       private string viewBaseConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           //@AutoSaveImage,PartPageType,WordNum,Editor,PrePageItem,CountContentClick,CountDownload,HitsOfHot,CanAddChannel,CanAddContent,ChannelGroup,ContentGroup,OpenType,ItemOpenType,OrderType,Info,MetaKeyword,MetaDesc,ShowOnMap,ShowOnPath
           ChannelBaseConfig config=curChannel.theChannelConfig.baseConfig;
           labelParams.Add("autosaveimage", new XNLParam(XNLType.String, config.SaveExtImage));
           labelParams.Add("partpagetype", new XNLParam(XNLType.String, config.partPage.ToString()));
           labelParams.Add("wordnum", new XNLParam(XNLType.String, config.autoWordNums));
           labelParams.Add("editor", new XNLParam(XNLType.String, config.editor));
           labelParams.Add("prepageitem", new XNLParam(XNLType.String, config.prePageInfoNum));
           labelParams.Add("countcontentclick", new XNLParam(XNLType.String, config.countContentClick));
           labelParams.Add("countdownload", new XNLParam(XNLType.String, config.countFileDownload));
           labelParams.Add("hitsofhot", new XNLParam(XNLType.String, config.hitsOfHot));
           labelParams.Add("canaddchannel", new XNLParam(XNLType.String, config.canAddChannel));
           labelParams.Add("canaddcontent", new XNLParam(XNLType.String, config.canAddContent));
           labelParams.Add("contenttag", new XNLParam(XNLType.String, config.useContentTag));
           labelParams.Add("contentgroup", new XNLParam(XNLType.String, config.useContentGroup));
           labelParams.Add("opentype", new XNLParam(XNLType.String, config.openType));
           labelParams.Add("itemopentype", new XNLParam(XNLType.String, config.itemOpenType));
           labelParams.Add("ordertype", new XNLParam(XNLType.String, config.itemOrderType));
           labelParams.Add("info", new XNLParam(XNLType.String, config.info));
           labelParams.Add("metakeyword", new XNLParam(XNLType.String, config.metaKeyword));
           labelParams.Add("metadesc", new XNLParam(XNLType.String, config.metaDesc));
           labelParams.Add("showonmap", new XNLParam(XNLType.String, config.showOnMap));
           labelParams.Add("showonpath", new XNLParam(XNLType.String, config.showOnPath));
           return RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
       }
       private string viewCommentsConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           //@enabled,needlogin,needaudit,successmsg,failmsg
           ChannelCommentsConfig config = curChannel.theChannelConfig.commentsConfig;
           labelParams.Add("enabled", new XNLParam(XNLType.String, config.isContentComments));
           labelParams.Add("needlogin", new XNLParam(XNLType.String, config.isNeedLogin));
           labelParams.Add("needaudit", new XNLParam(XNLType.String, config.isNeedAudit));
           labelParams.Add("successmsg", new XNLParam(XNLType.String, config.successTipMsg));
           labelParams.Add("failmsg", new XNLParam(XNLType.String, config.FailTipMsg));
           return RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
       }
       private string viewCreateConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           //@channelchange,contentchange
           ChannelCreateConfig config = curChannel.theChannelConfig.createConfig;
           labelParams.Add("channelchange", new XNLParam(XNLType.String, config.createChannelChange));
           labelParams.Add("contentchange", new XNLParam(XNLType.String, config.createContentChange));
           return RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
       }
       private string viewContributeConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           //@enabled,needlogin,needaudit,successmsg,failmsg
           ContributeConfig config = curChannel.theChannelConfig.contributeConfig;
           labelParams.Add("enabled", new XNLParam(XNLType.String, config.enabled));
           labelParams.Add("needaudit", new XNLParam(XNLType.String, config.needAudit));
           return RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
       }
       private string modifyBase(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage,Cchannel curChannel)
       {
           MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Success");
           MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Error");
           try
           {
               bool autoSaveImage = Convert.ToBoolean(labelParams["autosaveimage"].value);
               string partPageType = labelParams["partpagetype"].value.ToString();
               int wordNum =Convert.ToInt32(labelParams["wordnum"].value);
               int prePageItem =Convert.ToInt32(labelParams["prepageitem"].value);
               //bool channelGroup=Convert.ToBoolean(labelParams["channelgroup"].theValue);
               bool contentGroup = Convert.ToBoolean(labelParams["contentgroup"].value);
               bool contentTag = Convert.ToBoolean(labelParams["contenttag"].value);
               bool countContentClick = Convert.ToBoolean(labelParams["countcontentclick"].value);
               bool countDownload = Convert.ToBoolean(labelParams["countdownload"].value);
               int hitsofhot = Convert.ToInt32(labelParams["hitsofhot"].value);
               string openType = labelParams["opentype"].value.ToString();
               string itemOpenType = labelParams["itemopentype"].value.ToString();
               bool canAddChannel = Convert.ToBoolean(labelParams["canaddchannel"].value);
               bool canAddContent = Convert.ToBoolean(labelParams["canaddcontent"].value);
               string info = labelParams["info"].value.ToString();
               string keyword = labelParams["metakeyword"].value.ToString();
               string desc = labelParams["metadesc"].value.ToString();
               object SetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_nodes where nodeid=" + curChannel.nodeID);
               string SetXml = Convert.ToString(SetObj);
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.LoadXml(SetXml);
               XmlNode baseSetNode = xmlDoc.SelectSingleNode("/ChannelConfig/BaseConfig");
               baseSetNode.SelectSingleNode("PartPage/@type").Value = partPageType;
               baseSetNode.SelectSingleNode("PartPage/WordNum").InnerText = wordNum.ToString();
               baseSetNode.SelectSingleNode("PrePageItem").InnerText = prePageItem.ToString();
               baseSetNode.SelectSingleNode("CountContentClick").InnerText = countContentClick.ToString();
               baseSetNode.SelectSingleNode("CountDownload").InnerText = countDownload.ToString();
               baseSetNode.SelectSingleNode("HitsOfHot").InnerText = hitsofhot.ToString();
               baseSetNode.SelectSingleNode("CanAddChannel").InnerText = canAddChannel.ToString();
               baseSetNode.SelectSingleNode("CanAddContent").InnerText = canAddContent.ToString();
               //baseSetNode.SelectSingleNode("ChannelGroup").InnerText = channelGroup.ToString();
               baseSetNode.SelectSingleNode("ContentGroup").InnerText = contentGroup.ToString();
               baseSetNode.SelectSingleNode("Tag").InnerText = contentTag.ToString();
               baseSetNode.SelectSingleNode("AutoSaveImage").InnerText = autoSaveImage.ToString();
               baseSetNode.SelectSingleNode("OpenType").InnerText = openType;
               baseSetNode.SelectSingleNode("ItemOpenType").InnerText = itemOpenType;
               baseSetNode.SelectSingleNode("Info").InnerText = info;
               baseSetNode.SelectSingleNode("MetaKeyword").InnerText = keyword;
               baseSetNode.SelectSingleNode("MetaDesc").InnerText = desc;
               labelParams.Add("xmlset", new XNLParam(XNLType.String, xmlDoc.OuterXml));
               DataHelper.ExecuteNonQuery("update sn_nodes set SettingsXML=@xmlset where nodeid=" + curChannel.nodeID,labelParams);
               ChannelBaseConfig config = curChannel.theChannelConfig.baseConfig;
               config.autoWordNums = wordNum;
               config.canAddChannel = canAddChannel;
               config.canAddContent = canAddContent;
               config.countContentClick = countContentClick;
               config.countFileDownload = countDownload;
               config.hitsOfHot = hitsofhot;
               config.info = info;
               config.itemOpenType = itemOpenType;
               config.metaDesc = desc;
               config.metaKeyword = keyword;
               config.openType = openType;
               config.setPartPage(partPageType);
               config.prePageInfoNum = prePageItem;
               config.SaveExtImage = autoSaveImage;
               config.useContentGroup = contentGroup;
               config.useContentTag = contentTag;
               return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
           }
           catch (System.Exception ex)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>(1);
               errorList.Add("1", ex.Message);
               return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
           }
       }
       private string modifyComments(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Success");
           MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Error");
           try
           {
               bool enabled = Convert.ToBoolean(labelParams["enabled"].value);
               bool needlogin = Convert.ToBoolean(labelParams["needlogin"].value);
               bool needaudit = Convert.ToBoolean(labelParams["needaudit"].value);
               string successmsg = labelParams["successmsg"].value.ToString();
               string failmsg = labelParams["failmsg"].value.ToString();
               object SetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_nodes where nodeid=" + curChannel.nodeID);
               string SetXml = Convert.ToString(SetObj);
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.LoadXml(SetXml);
               XmlNode SetNode = xmlDoc.SelectSingleNode("/ChannelConfig/CommentsConfig");
               SetNode.SelectSingleNode("Enabled").InnerText = enabled.ToString();
               SetNode.SelectSingleNode("Login").InnerText = needlogin.ToString();
               SetNode.SelectSingleNode("Audit").InnerText = needaudit.ToString();
               SetNode.SelectSingleNode("SuccessMsg").InnerText = successmsg.ToString();
               SetNode.SelectSingleNode("FailMsg").InnerText = failmsg.ToString();
               labelParams.Add("xmlset", new XNLParam(XNLType.String, xmlDoc.OuterXml));
               DataHelper.ExecuteNonQuery("update sn_nodes set SettingsXML=@xmlset where nodeid=" + curChannel.nodeID, labelParams);
               ChannelCommentsConfig config = curChannel.theChannelConfig.commentsConfig;
               config.isContentComments = enabled;
               config.isNeedLogin= needlogin;
               config.isNeedAudit = needaudit;
               config.successTipMsg =successmsg;
               config.FailTipMsg = failmsg;
               return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
           }
           catch (System.Exception ex)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>(1);
               errorList.Add("1", ex.Message);
               return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
           }
       }
       private string modifyCreate(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Success");
           MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Error");
           try
           {
               bool contentchange = Convert.ToBoolean(labelParams["contentchange"].value);
               bool channelchange = Convert.ToBoolean(labelParams["channelchange"].value);
               object SetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_nodes where nodeid=" + curChannel.nodeID);
               string SetXml = Convert.ToString(SetObj);
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.LoadXml(SetXml);
               XmlNode SetNode = xmlDoc.SelectSingleNode("/ChannelConfig/CreateConfig");
               SetNode.SelectSingleNode("ContentChange").InnerText = contentchange.ToString();
               SetNode.SelectSingleNode("ChannelChange").InnerText = channelchange.ToString();
               labelParams.Add("xmlset", new XNLParam(XNLType.String, xmlDoc.OuterXml));
               DataHelper.ExecuteNonQuery("update sn_nodes set SettingsXML=@xmlset where nodeid=" + curChannel.nodeID, labelParams);
               ChannelCreateConfig config = curChannel.theChannelConfig.createConfig;
               config.createChannelChange = channelchange;
               config.createContentChange = contentchange;
               return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
           }
           catch (System.Exception ex)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>(1);
               errorList.Add("1", ex.Message);
               return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
           }
       }
       private string modifyContribute(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage, Cchannel curChannel)
       {
           MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Success");
           MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ChannelConfig.Error");
           try
           {
               bool enabled = Convert.ToBoolean(labelParams["enabled"].value);
               bool needaudit = Convert.ToBoolean(labelParams["needaudit"].value);
               object SetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_nodes where nodeid=" + curChannel.nodeID);
               string SetXml = Convert.ToString(SetObj);
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.LoadXml(SetXml);
               XmlNode SetNode = xmlDoc.SelectSingleNode("/ChannelConfig/ContributeConfig");
               SetNode.SelectSingleNode("Enabled").InnerText = enabled.ToString();
               SetNode.SelectSingleNode("Audit").InnerText = needaudit.ToString();
               labelParams.Add("xmlset", new XNLParam(XNLType.String, xmlDoc.OuterXml));
               DataHelper.ExecuteNonQuery("update sn_nodes set SettingsXML=@xmlset where nodeid=" + curChannel.nodeID, labelParams);
               ContributeConfig config = curChannel.theChannelConfig.contributeConfig;
               config.enabled= enabled;
               config.needAudit = needaudit;
               return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
           }
           catch (System.Exception ex)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>(1);
               errorList.Add("1", ex.Message);
               return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
           }
       }
       */

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
