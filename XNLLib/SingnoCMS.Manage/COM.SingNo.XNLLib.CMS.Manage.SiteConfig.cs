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
   public class SiteConfig:IXNLTag<WebContext>
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
       private string setViewControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage, string type)
        {
            
            switch (type)
            {
                case "base":
                    labelContentStr = viewBaseConfig(labelParams, labelContentStr, XNLPage);
                    break;
                case "upload":
                    labelContentStr = viewUploadConfig(labelParams, labelContentStr, XNLPage);
                    break;
                case "guestbook":
                    labelContentStr = viewGBConfig(labelParams, labelContentStr, XNLPage);
                    break;
                case "watermark":
                    labelContentStr = viewImageWaterConfig(labelParams, labelContentStr, XNLPage);
                    break;
                case "create":
                    labelContentStr = viewCreateConfig(labelParams, labelContentStr, XNLPage);
                    break;
            }
            return labelContentStr;
        }
       private string setModifyControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage, string type)
        {
            switch (type)
            {
                case "base":
                    labelContentStr = modifyBase(labelParams, labelContentStr, XNLPage);
                    break;
                case "upload":
                    labelContentStr = modifyUpload(labelParams, labelContentStr, XNLPage);
                    break;
                case "guestbook":
                    labelContentStr = modifyGB(labelParams, labelContentStr, XNLPage);
                    break;
                case "watermark":
                    labelContentStr = modifyImageWaterConfig(labelParams, labelContentStr, XNLPage);
                    break;
                case "create":
                    labelContentStr = modifyCreateConfig(labelParams, labelContentStr, XNLPage);
                    break;
            }
            return labelContentStr;
        }
        private string viewBaseConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
            //{@name}  {@path} {@title} {@ico} {@info} {@keyword} {@desc} {@charset} {@audit}
            /*
           Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
           labelParams.Add("name", new XNLParam( XNLType.String, curSite.nodeName));
           labelParams.Add("path", new XNLParam( XNLType.String, curSite.siteWebPath));
           labelParams.Add("title", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.title));
           labelParams.Add("ico", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.ico));
           labelParams.Add("info", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.info));
           labelParams.Add("keyword", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.mateKeyWord));
           labelParams.Add("desc", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.mateDesc));
           labelParams.Add("charset", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.encoder.WebName));
           labelParams.Add("audit", new XNLParam( XNLType.String, curSite.theSiteConfig.baseConfig.auditLevel));
           labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
             */
           return labelContentStr;
       }
        private string modifyBase(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            return "";
            /*
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Error");
            try
            {
                string siteName = labelParams["sitename"].value.ToString();
                string title = labelParams["title"].value.ToString();
                string ico = labelParams["ico"].value.ToString();
                string info = labelParams["info"].value.ToString();
                string keyword = labelParams["keyword"].value.ToString();
                string desc = labelParams["desc"].value.ToString();
                string Charset = labelParams["charset"].value.ToString();
                int audit = Convert.ToInt32(labelParams["audit"].value);
                string checkLevel = "1";
                if (audit > 0) checkLevel = labelParams["checklevel"].value.ToString();
                Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                object siteSetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_sites where siteid=" + curSite.siteID);
                string siteSetXml =Convert.ToString(siteSetObj);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(siteSetXml);
                XmlNode baseSetNode = xmlDoc.SelectSingleNode("/SiteConfig/BsaeConfig");
                baseSetNode.SelectSingleNode("title").InnerXml = title;
                baseSetNode.SelectSingleNode("ico").InnerXml = ico;
                baseSetNode.SelectSingleNode("info").InnerXml = info;
                baseSetNode.SelectSingleNode("CharSet").InnerText = Charset;
                baseSetNode.SelectSingleNode("Audit").InnerText = checkLevel;
                baseSetNode.SelectSingleNode("metaKeyword").InnerXml = keyword;
                baseSetNode.SelectSingleNode("metaDesc").InnerXml = desc;
                labelParams.Add("xmlset", new XNLParam( XNLType.String, xmlDoc.OuterXml));
                Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
                sqlColls.Add("1", "update sn_sites set SettingsXML=@xmlset where siteid=" + curSite.siteID);
                if(!curSite.nodeName.Equals(siteName))
                {
                    sqlColls.Add("2", "update sn_nodes set NodeName=@siteName where NodeId=" + curSite.nodeID);
                }
                DbResultInfo exeInfo= DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
                if(!exeInfo.isSuccess)
                {
                    throw (exeInfo.exception);
                }
                curSite.nodeName = siteName;
                curSite.theSiteConfig.baseConfig.title = title;
                curSite.theSiteConfig.baseConfig.ico = ico;
                curSite.theSiteConfig.baseConfig.info= info;
                curSite.theSiteConfig.baseConfig.mateKeyWord =keyword;
                curSite.theSiteConfig.baseConfig.mateDesc = desc;
                curSite.theSiteConfig.baseConfig.auditLevel = Convert.ToInt32(checkLevel);
                curSite.theSiteConfig.baseConfig.encoder = Encoding.GetEncoding(Charset);
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
             */ 
        }
        private string viewUploadConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            //{@folder}  {@saveType} {@renameByTime} {@imagesType}{@mediasType}{@docsType} {@imageMaxSize} {@mediaMaxSize}{@docMaxSize}
            /*
            Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
            UploadConfig uploadConfig=curSite.theSiteConfig.uploadConfig;
            labelParams.Add("folder", new XNLParam( XNLType.String, uploadConfig.UploadFolder));
            labelParams.Add("savetype", new XNLParam( XNLType.String, uploadConfig.FileSaveStyle));
            labelParams.Add("renamebytime", new XNLParam( XNLType.String, uploadConfig.IsRenameByTime));
            labelParams.Add("imagestype", new XNLParam( XNLType.String, uploadConfig.ImageUploadInfo.FileTypes));
            labelParams.Add("mediastype", new XNLParam( XNLType.String, uploadConfig.MediaUploadInfo.FileTypes));
            labelParams.Add("docstype", new XNLParam( XNLType.String, uploadConfig.DocUploadInfo.FileTypes));
            labelParams.Add("imagemaxsize", new XNLParam( XNLType.String, uploadConfig.ImageUploadInfo.MaxSize));
            labelParams.Add("mediamaxsize", new XNLParam( XNLType.String, uploadConfig.MediaUploadInfo.MaxSize));
            labelParams.Add("docmaxsize", new XNLParam( XNLType.String, uploadConfig.DocUploadInfo.MaxSize));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
             */ 
            return labelContentStr;
        }
        private string modifyUpload(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            /*
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Error");
            try
            {
                string folder = labelParams["folder"].value.ToString();
                if(folder.Trim().Equals(""))
                {
                    throw(new Exception("上传文件夹名称不能为空"));
                }
                //string fullFolder=folder;
                //if(folder.IndexOf("@/")!=0)
                //{
                //    fullFolder="@/"+folder;
                //}
                Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                //fullFolder=XNLPage.Context.Server.MapPath((fullFolder.Replace("@",curSite.siteWebPath).Replace("//","/"))) ;
                //if(!System.IO.Directory.Exists(fullFolder))
                //{
                //    System.IO.Directory.CreateDirectory(fullFolder);
                //}
                string SaveType = labelParams["savetype"].value.ToString();
                string ReNameByTime = labelParams["renamebytime"].value.ToString();
                string imageType = labelParams["imagetype"].value.ToString();
                int imageMaxSize =Convert.ToInt32(labelParams["imagemaxsize"].value);
                string mediaType = labelParams["mediatype"].value.ToString();
                int mediaMaxSize =Convert.ToInt32(labelParams["mediamaxsize"].value);
                string docsType = labelParams["docstype"].value.ToString();
                int docMaxSize =Convert.ToInt32(labelParams["docmaxsize"].value);
                object siteSetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_sites where siteid=" + curSite.siteID);
                string siteSetXml = Convert.ToString(siteSetObj);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(siteSetXml);
                XmlNode SetNode = xmlDoc.SelectSingleNode("/SiteConfig/UploadConfig");
                SetNode.SelectSingleNode("Folder").InnerText = folder;
                SetNode.SelectSingleNode("FileSaveStyle").InnerText = SaveType;
                SetNode.SelectSingleNode("FileRenameByTime").InnerText = ReNameByTime;
                SetNode.SelectSingleNode("ImageType").InnerText = imageType;
                SetNode.SelectSingleNode("ImageType/@maxSize").Value = imageMaxSize.ToString();
                SetNode.SelectSingleNode("MediaType").InnerText = mediaType;
                SetNode.SelectSingleNode("MediaType/@maxSize").Value = mediaMaxSize.ToString();
                SetNode.SelectSingleNode("DocType").InnerText = docsType;
                SetNode.SelectSingleNode("DocType/@maxSize").Value = docMaxSize.ToString();
                labelParams.Add("xmlset", new XNLParam( XNLType.String, xmlDoc.OuterXml));
                string sql="update sn_sites set SettingsXML=@xmlset where siteid=" + curSite.siteID;
                DataHelper.ExecuteNonQuery(sql, labelParams);
                UploadConfig upConfig = curSite.theSiteConfig.uploadConfig;
                upConfig.UploadFolder = folder;
                upConfig.IsRenameByTime = Convert.ToBoolean(ReNameByTime);
                upConfig.FileSaveStyle = SaveType;
                upConfig.ImageUploadInfo.FileTypes = imageType;
                upConfig.ImageUploadInfo.MaxSize = imageMaxSize;
                upConfig.MediaUploadInfo.FileTypes = mediaType;
                upConfig.MediaUploadInfo.MaxSize = mediaMaxSize;
                upConfig.DocUploadInfo.FileTypes = docsType;
                upConfig.DocUploadInfo.MaxSize = docMaxSize;
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
             */
            return "";
        }
        private string viewGBConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            //{@enabled}  {@needaudit} {@needlogin} {@needverify}
            /*
            Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
            GuestBookConfig config = curSite.theSiteConfig.guestBookConfig;
            labelParams.Add("enabled", new XNLParam( XNLType.String, config.IsEnabled));
            labelParams.Add("needaudit", new XNLParam( XNLType.String, config.IsNeedAudit));
            labelParams.Add("needlogin", new XNLParam( XNLType.String, config.IsNeedLogin));
            labelParams.Add("needverify", new XNLParam( XNLType.String, config.IsVerifyCode));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
             */
            return labelContentStr;
        }
        private string modifyGB(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            /*
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Error");
            try
            {
                bool isEnabled =Convert.ToBoolean(labelParams["isenabled"].value);
                bool isAudit = Convert.ToBoolean(labelParams["isaudit"].value);
                bool isLogin = Convert.ToBoolean(labelParams["islogin"].value);
                bool isCode = Convert.ToBoolean(labelParams["iscode"].value);
                Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                object siteSetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_sites where siteid=" + curSite.siteID);
                string siteSetXml = Convert.ToString(siteSetObj);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(siteSetXml);
                XmlNode SetNode = xmlDoc.SelectSingleNode("/SiteConfig/GBConfig");
                SetNode.SelectSingleNode("Enabled").InnerText = isEnabled.ToString();
                SetNode.SelectSingleNode("Audit").InnerText = isAudit.ToString();
                SetNode.SelectSingleNode("NeedLogin").InnerText = isLogin.ToString();
                SetNode.SelectSingleNode("VerifyCode").InnerText = isCode.ToString();
                labelParams.Add("xmlset", new XNLParam( XNLType.String, xmlDoc.OuterXml));
                string sql = "update sn_sites set SettingsXML=@xmlset where siteid=" + curSite.siteID;
                DataHelper.ExecuteNonQuery(sql, labelParams);
                GuestBookConfig config = curSite.theSiteConfig.guestBookConfig;
                config.IsEnabled = isEnabled;
                config.IsNeedAudit = isAudit;
                config.IsNeedLogin= isLogin;
                config.IsVerifyCode = isCode;
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
             */
            return "";
        }
        private string viewImageWaterConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            //{@enabled}  {@Position} {@alpha} {@ImageMinWidth} {@ImageMinHeight} {@WaterMarkType} 
            //{@ImagePath} {@Content} {@Font} {@fontSize} {fontFamilies}
            /*
            Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
            WaterMarkConfig config = curSite.theSiteConfig.waterMarkConfig;
            labelParams.Add("enabled", new XNLParam( XNLType.String, config.enabled));
            labelParams.Add("position", new XNLParam( XNLType.String, config.position));
            labelParams.Add("alpha", new XNLParam( XNLType.String, config.alpha));
            labelParams.Add("imageminwidth", new XNLParam( XNLType.String, config.imageMinWidth));
            labelParams.Add("imageminheight", new XNLParam( XNLType.String, config.imageMinHeight));
            labelParams.Add("watermarktype", new XNLParam( XNLType.String, config.markType));
            labelParams.Add("imagepath", new XNLParam( XNLType.String, config.imagePath));
            labelParams.Add("content", new XNLParam( XNLType.String, config.content));
            labelParams.Add("font", new XNLParam( XNLType.String, config.font));
            labelParams.Add("fontsize", new XNLParam( XNLType.String, config.fontSize));
            labelParams.Add("fontfamilies", new XNLParam( XNLType.String, COM.SingNo.Common.Util.getPcFontList()));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            return labelContentStr;
             */
            return "";
        }
        private string viewCreateConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            //{@siteError}  {@LabelError} {@PreCompiled}
            /*
            Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
            siteCreateConfig config = curSite.theSiteConfig.createConfig;
            labelParams.Add("siteerror", new XNLParam( XNLType.String, config.siteErrorHandle.ToString()));
            labelParams.Add("labelerror", new XNLParam( XNLType.String, config.labelErrorHandle.ToString()));
            labelParams.Add("precompiled", new XNLParam( XNLType.String, config.PreCompiled));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
             */
            return labelContentStr;
        }
        private string modifyImageWaterConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            //enabled="{%isWatermark}" position="{%pos}" alpha="{%alpha}" imageminwidth="{%imgW}" imageminheight="{%imgH}" watermarktype="{%textType}" imagepath="{%imgPath}" content="{%content}"
//font="{%font}" fontsize="{%fontsize}"
            /*
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Error");
            try
            {
                Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                bool isEnabled = Convert.ToBoolean(labelParams["enabled"].value);
                int pos = Convert.ToInt32(labelParams["position"].value);
                int alpha = Convert.ToInt32(labelParams["alpha"].value);
                int imageMinW = Convert.ToInt32(labelParams["imageminwidth"].value);
                int imageMinH = Convert.ToInt32(labelParams["imageminheight"].value);
                string markType = labelParams["watermarktype"].value.ToString();
                string imgPath = labelParams["imagepath"].value.ToString();
                string content = labelParams["content"].value.ToString();
                string font = labelParams["font"].value.ToString();
                int fontSize = Convert.ToInt32(labelParams["fontsize"].value);
                object siteSetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_sites where siteid=" + curSite.siteID);
                string siteSetXml = Convert.ToString(siteSetObj);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(siteSetXml);
                XmlNode SetNode = xmlDoc.SelectSingleNode("/SiteConfig/WaterMarkConfig");
                SetNode.SelectSingleNode("Enabled").InnerText = isEnabled.ToString();
                SetNode.SelectSingleNode("Position").InnerText = pos.ToString();
                SetNode.SelectSingleNode("alpha").InnerText = alpha.ToString();
                SetNode.SelectSingleNode("ImageMinSize/@W").Value= imageMinW.ToString();
                SetNode.SelectSingleNode("ImageMinSize/@H").Value = imageMinH.ToString();
                SetNode.SelectSingleNode("WaterMarkType/@type").Value = markType;
                SetNode.SelectSingleNode("WaterMarkType/ImageType/ImagePath").InnerText =imgPath;
                SetNode.SelectSingleNode("WaterMarkType/TextType/Content").InnerXml = "<![CDATA["+content+"]]>";
                SetNode.SelectSingleNode("WaterMarkType/TextType/Font").InnerText = font;
                SetNode.SelectSingleNode("WaterMarkType/TextType/Size").InnerText = fontSize.ToString();
                labelParams.Add("xmlset", new XNLParam( XNLType.String, xmlDoc.OuterXml));
                string sql = "update sn_sites set SettingsXML=@xmlset where siteid=" + curSite.siteID;
                DataHelper.ExecuteNonQuery(sql, labelParams);
                WaterMarkConfig config = curSite.theSiteConfig.waterMarkConfig;
                config.enabled= isEnabled;
                config.position = pos;
                config.alpha = alpha;
                config.imageMinHeight = imageMinH;
                config.imageMinWidth = imageMinW;
                config.markType = markType;
                config.imagePath = imgPath;
                config.content = content;
                config.font = font;
                config.fontSize = fontSize;
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
             */
            return "";
        }
        private string modifyCreateConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            /*
           // siteError="{%siteError}" writeSQL="{%writeSQL}" LabelError="{%LabelError}" PreCompiled="{%PreCompiled}"
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "SiteConfig.Error");
            try
            {
                Cchannel curSite = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                string siteError = labelParams["siteerror"].value.ToString();
                string writeSQL = labelParams["writesql"].value.ToString();
                string LabelError = labelParams["labelerror"].value.ToString();
                bool PreCompiled = Convert.ToBoolean(labelParams["precompiled"].value);
                object siteSetObj = DataHelper.ExecuteScalar("select SettingsXML from sn_sites where siteid=" + curSite.siteID);
                string siteSetXml = Convert.ToString(siteSetObj);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(siteSetXml);
                XmlNode SetNode = xmlDoc.SelectSingleNode("/SiteConfig/CreateConfig");
                if (siteError.Equals("writeMsg"))
                {
                    siteError = writeSQL;
                }
                SetNode.SelectSingleNode("SiteErrorHandle").InnerText = siteError;
                SetNode.SelectSingleNode("LabelErrorHandle").InnerText = LabelError;
                SetNode.SelectSingleNode("PreCompiled").InnerText = PreCompiled.ToString();
                labelParams.Add("xmlset", new XNLParam( XNLType.String, xmlDoc.OuterXml));
                string sql = "update sn_sites set SettingsXML=@xmlset where siteid=" + curSite.siteID;
                DataHelper.ExecuteNonQuery(sql, labelParams);
                siteCreateConfig config = curSite.theSiteConfig.createConfig;
                config.setSiteErrorHandle(siteError);
                config.setLabelErrorHandle(LabelError);
                config.PreCompiled= PreCompiled;
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
             */
            return "";
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
