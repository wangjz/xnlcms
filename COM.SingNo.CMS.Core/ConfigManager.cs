using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using System.Reflection;
using System.Data;
using System.Xml;
using System.Web;
using System.Collections;
using COM.SingNo.DAL;
using COM.SingNo.Common;
namespace COM.SingNo.CMS.Core
{
 public   class SiteConfigManager
    {
        private static volatile SiteConfigManager _instance;
        private static object syncRoot = new Object();
        public SafeDictionary<int,Cchannel> siteDataColls; //存储站点集合
        private SiteConfigManager()
        {
            siteDataColls = new SafeDictionary<int, Cchannel>();
        }
        public static SiteConfigManager createInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)_instance = new SiteConfigManager();
                }
            }
            return _instance;
        }
    }

 public  class ChannelConfigManager
    {
        private static volatile ChannelConfigManager _instance;
        private static object syncRoot = new Object();
        public SafeDictionary<int, Cchannel> channelDataColls; //存储节点集合
        private ChannelConfigManager()
        {
            channelDataColls = new SafeDictionary<int, Cchannel>();
            loadChannel();
        }
        public static ChannelConfigManager createInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null) _instance = new ChannelConfigManager();
                }
            }
            return _instance;
        }
        private void loadChannel()
        {
            setGlobalModel();
            DataTable channelDT = DataHelper.ExecuteDataTable("select * from sn_nodes"); //DSMethod.Invoke(null,new object[]{"select * from sn_nodes"}) as DataTable; //查询所有节点
            foreach (DataRow row in channelDT.Rows)
            {
                Cchannel newChannel = setChannel(row);
                channelDataColls.Add(Convert.ToInt32(row["nodeid"]), newChannel);
            }
            //设置站点节点
            DataTable siteDT = DataHelper.ExecuteDataTable("select * from sn_sites");//DSMethod.Invoke(null, new object[] { "select * from sn_sites" }) as DataTable; //查询所有站点
            foreach (DataRow row in siteDT.Rows)
            {
                setSite(row);
                SiteConfigManager.createInstance().siteDataColls.Add(Convert.ToInt32(row["siteid"]), channelDataColls[Convert.ToInt32(row["nodeid"])]);
            }
            //设置节点子节点列表，父节点，此节点站点节点,匹配方案，模板等
            foreach (DataRow dr in channelDT.Rows)
            {
                Cchannel curChannel = channelDataColls[Convert.ToInt32(dr["nodeid"])];
                if (Convert.ToInt32(dr["ChildsNum"]) > 0)  //有子节点
                {
                    string ArrChildID_str = dr["ArrChildID"].ToString();
                    string[] ArrChildID_arr = ArrChildID_str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in ArrChildID_arr)  //设置子节点
                    {
                        Cchannel subChannel = channelDataColls[Convert.ToInt32(str)];
                        curChannel.addSubNode(Convert.ToInt32(str),subChannel);
                        subChannel.parentNode=curChannel;
                        if (curChannel.isSite && !subChannel.isSite)
                        {
                            subChannel.siteNode = curChannel;
                        }
                        else if (!curChannel.isSite && !subChannel.isSite)
                        {
                            subChannel.siteNode = curChannel.siteNode;
                        }
                    }
                }
            }//end foreach
            channelDT.Clear();
            channelDT.Dispose();
            matchTemplate();
        }
        private Cchannel setChannel(DataRow row)
        {
            Cchannel tmpChannel = new Cchannel();
            tmpChannel.nodeID =Convert.ToInt32(row["nodeid"]);
            tmpChannel.nodeIndexName = row["IndexName"].ToString();
            tmpChannel.state = Convert.ToInt32(row["state"]);
            tmpChannel.nodeName = row["NodeName"].ToString();
            tmpChannel.addDate = row["adddate"].ToString();
            tmpChannel.depth =Convert.ToInt32(row["depth"]) ;
            tmpChannel.imageUrl = Convert.ToString(row["imageurl"]);
            //设置节点模型信息
            setNodeModel(row, tmpChannel);
           //设置节点配置项
            XmlDocument channelConfigXml = new XmlDocument();
            channelConfigXml.LoadXml(row["SettingsXML"].ToString());
            XmlNode baseConfigNode = channelConfigXml.SelectSingleNode("/ChannelConfig/BaseConfig");
            ChannelBaseConfig config = tmpChannel.theChannelConfig.baseConfig;
            config.prePageInfoNum = Convert.ToInt32(baseConfigNode.SelectSingleNode("PrePageItem").InnerText);
            config.countFileDownload = Convert.ToBoolean(baseConfigNode.SelectSingleNode("CountDownload").InnerText);
            config.countContentClick = Convert.ToBoolean(baseConfigNode.SelectSingleNode("CountContentClick").InnerText);
            config.hitsOfHot = Convert.ToInt32(baseConfigNode.SelectSingleNode("HitsOfHot").InnerText);
            config.showOnMap = Convert.ToBoolean(baseConfigNode.SelectSingleNode("ShowOnMap").InnerText);
            config.showOnPath = Convert.ToBoolean(baseConfigNode.SelectSingleNode("ShowOnPath").InnerText);
            config.openType = baseConfigNode.SelectSingleNode("OpenType").InnerText;
            config.itemOpenType = baseConfigNode.SelectSingleNode("ItemOpenType").InnerText;
            config.autoWordNums = Convert.ToInt32(baseConfigNode.SelectSingleNode("PartPage/WordNum").InnerText);
            config.canAddChannel = Convert.ToBoolean(baseConfigNode.SelectSingleNode("CanAddChannel").InnerText);
            config.canAddContent = Convert.ToBoolean(baseConfigNode.SelectSingleNode("CanAddContent").InnerText);
            config.editor = baseConfigNode.SelectSingleNode("Editor").InnerText;
            config.info = baseConfigNode.SelectSingleNode("Info").InnerText;
            config.metaDesc = baseConfigNode.SelectSingleNode("MetaDesc").InnerText;
            config.metaKeyword = baseConfigNode.SelectSingleNode("MetaKeyword").InnerText;
            config.setPartPage(baseConfigNode.SelectSingleNode("PartPage/@type").Value);
            config.SaveExtImage = Convert.ToBoolean(baseConfigNode.SelectSingleNode("AutoSaveImage").InnerText);
            //config.useChannelGroup = Convert.ToBoolean(baseConfigNode.SelectSingleNode("ChannelGroup").InnerText);
            config.useContentGroup = Convert.ToBoolean(baseConfigNode.SelectSingleNode("ContentGroup").InnerText);
            config.useContentTag = Convert.ToBoolean(baseConfigNode.SelectSingleNode("Tag").InnerText); //设置添加内容页是否显示内容tag
            XmlNode commentsConfigNode = channelConfigXml.SelectSingleNode("/ChannelConfig/CommentsConfig");
            ChannelCommentsConfig config2 = tmpChannel.theChannelConfig.commentsConfig;
            config2.isContentComments =Convert.ToBoolean(commentsConfigNode.SelectSingleNode("Enabled").InnerText);
            config2.isNeedLogin = Convert.ToBoolean(commentsConfigNode.SelectSingleNode("Login").InnerText);
            config2.isNeedAudit = Convert.ToBoolean(commentsConfigNode.SelectSingleNode("Audit").InnerText);
            config2.successTipMsg = commentsConfigNode.SelectSingleNode("SuccessMsg").InnerText;
            config2.FailTipMsg = commentsConfigNode.SelectSingleNode("FailMsg").InnerText;
            ChannelCreateConfig createConfig = tmpChannel.theChannelConfig.createConfig;
            XmlNode createConfigNode = channelConfigXml.SelectSingleNode("/ChannelConfig/CreateConfig");
            createConfig.createChannelChange = Convert.ToBoolean(createConfigNode.SelectSingleNode("ChannelChange").InnerText);
            createConfig.createContentChange = Convert.ToBoolean(createConfigNode.SelectSingleNode("ContentChange").InnerText);
            XmlNode contributeConfigNode = channelConfigXml.SelectSingleNode("/ChannelConfig/ContributeConfig");
            ContributeConfig contributeConfig = tmpChannel.theChannelConfig.contributeConfig;
            contributeConfig.enabled = Convert.ToBoolean(contributeConfigNode.SelectSingleNode("Enabled").InnerText);
            contributeConfig.needAudit = Convert.ToBoolean(contributeConfigNode.SelectSingleNode("Audit").InnerText);
            return tmpChannel;
        }
        private void setSite(DataRow row)
        {
            Cchannel curChannel = channelDataColls[Convert.ToInt32(row["nodeid"])];
            curChannel.siteID = Convert.ToInt32(row["siteid"]);
            curChannel.isSite = true;
            curChannel.theSiteConfig = new siteConfig();
            //设置站点配置项
            XmlDocument siteConfigXml = new XmlDocument();
            siteConfigXml.LoadXml(row["SettingsXML"].ToString());
            XmlNode siteBaseConfigNode = siteConfigXml.SelectSingleNode("/SiteConfig/BsaeConfig");
            siteConfig curSiteConfig = curChannel.theSiteConfig;
            curSiteConfig.baseConfig.encoder = Encoding.GetEncoding(siteBaseConfigNode.SelectSingleNode("CharSet").InnerText);
            curSiteConfig.baseConfig.title = siteBaseConfigNode.SelectSingleNode("title").InnerText;
            curSiteConfig.baseConfig.ico = siteBaseConfigNode.SelectSingleNode("ico").InnerText;
            curSiteConfig.baseConfig.info = siteBaseConfigNode.SelectSingleNode("info").InnerText;
            curSiteConfig.baseConfig.mateDesc = siteBaseConfigNode.SelectSingleNode("metaDesc").InnerText;
            curSiteConfig.baseConfig.mateKeyWord = siteBaseConfigNode.SelectSingleNode("metaKeyword").InnerText;
            curSiteConfig.baseConfig.auditLevel = Convert.ToInt32(siteBaseConfigNode.SelectSingleNode("Audit").InnerText);
            curSiteConfig.baseConfig.AccessType = 0;
            XmlNode createConfigNode = siteConfigXml.SelectSingleNode("/SiteConfig/CreateConfig");
            //curChannel.theSiteConfig.baseConfig.AccessType = Convert.ToInt32(createConfigNode.SelectSingleNode("AccessType").InnerText);
            curSiteConfig.baseConfig.TemplateSaveType = Convert.ToInt32(createConfigNode.SelectSingleNode("TemplateSaveType").InnerText);
            //站点生成配置
            siteCreateConfig createConfig = curSiteConfig.createConfig;
            createConfig.setSiteErrorHandle(createConfigNode.SelectSingleNode("SiteErrorHandle").InnerText);
            createConfig.setLabelErrorHandle(createConfigNode.SelectSingleNode("LabelErrorHandle").InnerText);
            createConfig.PreCompiled = Convert.ToBoolean(createConfigNode.SelectSingleNode("PreCompiled").InnerText);
            //设置站点上传配置
            XmlNode siteUploadConfig = siteConfigXml.SelectSingleNode("/SiteConfig/UploadConfig");
            curSiteConfig.uploadConfig.UploadFolder = siteUploadConfig.SelectSingleNode("Folder").InnerText;
            curSiteConfig.uploadConfig.FileSaveStyle = siteUploadConfig.SelectSingleNode("FileSaveStyle").InnerText;
            curSiteConfig.uploadConfig.IsRenameByTime = Convert.ToBoolean(siteUploadConfig.SelectSingleNode("FileRenameByTime").InnerText);
            curSiteConfig.uploadConfig.ImageUploadInfo.SizeType = siteUploadConfig.SelectSingleNode("ImageType/@sizeType").Value;
            curSiteConfig.uploadConfig.ImageUploadInfo.MaxSize = Convert.ToInt32(siteUploadConfig.SelectSingleNode("ImageType/@maxSize").Value);
            curSiteConfig.uploadConfig.ImageUploadInfo.FileTypes = siteUploadConfig.SelectSingleNode("ImageType").InnerText;
            curSiteConfig.uploadConfig.MediaUploadInfo.SizeType = siteUploadConfig.SelectSingleNode("MediaType/@sizeType").Value;
            curSiteConfig.uploadConfig.MediaUploadInfo.MaxSize = Convert.ToInt32(siteUploadConfig.SelectSingleNode("MediaType/@maxSize").Value);
            curSiteConfig.uploadConfig.MediaUploadInfo.FileTypes = siteUploadConfig.SelectSingleNode("MediaType").InnerText;
            curSiteConfig.uploadConfig.DocUploadInfo.SizeType = siteUploadConfig.SelectSingleNode("DocType/@sizeType").Value;
            curSiteConfig.uploadConfig.DocUploadInfo.MaxSize = Convert.ToInt32(siteUploadConfig.SelectSingleNode("DocType/@maxSize").Value);
            curSiteConfig.uploadConfig.DocUploadInfo.FileTypes = siteUploadConfig.SelectSingleNode("DocType").InnerText;
            XmlNode gbSetNode = siteConfigXml.SelectSingleNode("/SiteConfig/GBConfig");
            curSiteConfig.guestBookConfig.IsEnabled = Convert.ToBoolean(gbSetNode.SelectSingleNode("Enabled").InnerText);
            curSiteConfig.guestBookConfig.IsNeedLogin = Convert.ToBoolean(gbSetNode.SelectSingleNode("NeedLogin").InnerText);
            curSiteConfig.guestBookConfig.IsNeedAudit = Convert.ToBoolean(gbSetNode.SelectSingleNode("Audit").InnerText);
            curSiteConfig.guestBookConfig.IsNeedAudit = Convert.ToBoolean(gbSetNode.SelectSingleNode("Audit").InnerText);
            curSiteConfig.guestBookConfig.IsNeedAudit = Convert.ToBoolean(gbSetNode.SelectSingleNode("VerifyCode").InnerText);
            //设置水印
            WaterMarkConfig waterMarkConfig = curSiteConfig.waterMarkConfig;
            XmlNode waterSetNode = siteConfigXml.SelectSingleNode("/SiteConfig/WaterMarkConfig");
            waterMarkConfig.enabled = Convert.ToBoolean(waterSetNode.SelectSingleNode("Enabled").InnerText);
            waterMarkConfig.position = Convert.ToInt32(waterSetNode.SelectSingleNode("Position").InnerText);
            waterMarkConfig.alpha = Convert.ToInt32(waterSetNode.SelectSingleNode("alpha").InnerText);
            waterMarkConfig.imageMinWidth = Convert.ToInt32(waterSetNode.SelectSingleNode("ImageMinSize/@W").Value);
            waterMarkConfig.imageMinHeight = Convert.ToInt32(waterSetNode.SelectSingleNode("ImageMinSize/@H").Value);
            waterMarkConfig.markType = waterSetNode.SelectSingleNode("WaterMarkType/@type").Value;
            waterMarkConfig.imagePath = waterSetNode.SelectSingleNode("WaterMarkType/ImageType/ImagePath").InnerText;
            waterMarkConfig.content = waterSetNode.SelectSingleNode("WaterMarkType/TextType/Content").InnerText;
            waterMarkConfig.font = waterSetNode.SelectSingleNode("WaterMarkType/TextType/Font").InnerText;
            waterMarkConfig.fontSize =Convert.ToInt32(waterSetNode.SelectSingleNode("WaterMarkType/TextType/Size").InnerText);
            //XmlNode siteMailConfigNode = siteConfigXml.SelectSingleNode("/SiteConfig/MailConfig");
            //curChannel.theSiteConfig.mailConfig.SMTPServer = siteMailConfigNode.SelectSingleNode("SMTPServer").InnerText;
            //curChannel.theSiteConfig.mailConfig.SMTPPort = siteMailConfigNode.SelectSingleNode("SMTPProt").InnerText;
            //curChannel.theSiteConfig.mailConfig.SystemMail = siteMailConfigNode.SelectSingleNode("SystemMail").InnerText;
            //curChannel.theSiteConfig.mailConfig.SystemMailPass = siteMailConfigNode.SelectSingleNode("SystemMailPass").InnerText;
            //设置站点路径等
            curChannel.siteWebPath = row["SiteUrl"].ToString();
            /*查找此站点的所有模板方案*/
            DataTable projectDT = DataHelper.ExecuteDataTable("select * from SN_TemplateProject  where SiteID=" + curChannel.siteID); //DSMethod.Invoke(null, new object[] { "select * from SN_TemplateProject  where SiteID=" + curChannel.siteID }) as DataTable; //查询模板方案
            foreach (DataRow pr in projectDT.Rows)
            {
                int projectID = Convert.ToInt32(pr["ProjectID"]);
                string projectName = pr["ProjectName"].ToString();
                if (Convert.ToInt32(pr["IsDefault"]) == 1) curChannel.siteNode.defaultProjectId = projectID;
                TemplateProject templateProject = new TemplateProject();
                templateProject.templateProjectName = projectName;
                curChannel.siteNode.addTemplateProject(projectID, templateProject);
            }
            projectDT.Clear();
            projectDT.Dispose();
            curChannel.indexPageCreateThreadInfo = new SinglePageCreateThreadInfo();
            curChannel.singlePageCreateThreadInfo = new SinglePageCreateThreadInfo();
            curChannel.channelPageCreateThreadInfo = new SinglePageCreateThreadInfo();
            curChannel.contentPageCreateThreadInfo = new ContentPageCreateThreadInfo();
        }
        private void setNodeModel(DataRow row, Cchannel curChannel)  //设置站点节点模型
        {
            curChannel.model = DataModelManager.createInstance().getModel(Convert.ToInt32(row["ModelId"]));
        }
     /*
        private int setTemplate(Cchannel curChannel,int templateID,int templateStyle)
        {
            
            DataTable templateDT = DataHelper.ExecuteDataTable("select * from SN_Template  where TemplateID=" + templateID);//DSMethod.Invoke(null, new object[] { "select * from SN_Template  where TemplateID=" + templateID }) as DataTable;
            DataRow templateRow = templateDT.Rows[0];
           // string templateName = templateRow["TemplateName"].ToString()+"_" + templateRow["Templateid"].ToString();
            int templateId = Convert.ToInt32(templateRow["Templateid"]);
            if (curChannel.siteNode.templateColls == null || !curChannel.siteNode.templateColls.ContainsKey(templateId))
            {
                Template template = new Template();
                template.templateProjectId = Convert.ToInt32(templateRow["TemplateProjectID"]);
                template.theSite = curChannel.siteNode;
                template.templateStyle = templateStyle;
                template.encoder = Encoding.GetEncoding(templateRow["Charset"].ToString());
                template.TemplatePath = HttpContext.Current.Server.MapPath("~" + templateRow["TemplateFilePath"].ToString().Replace("@", curChannel.siteNode.siteWebPath));
                if (curChannel.siteNode.theSiteConfig.baseConfig.TemplateSaveType == 1)
                {
                    template.TemplateContent = Utils.loadTempleteByPath(template.TemplatePath, template.encoder);
                }
                curChannel.siteNode.addTemplate(templateId, template);
            }
            templateDT.Clear();
            templateDT.Dispose();
            return templateId;
        }
       
        private int  setTemplate(int templateId,int templateProjectId,string TemplateFilePath,string Charset,int templateStyle,Cchannel curChannel)
        {
            if (curChannel.siteNode.templateColls == null || !curChannel.siteNode.templateColls.ContainsKey(templateId))
            {
                Template template = new Template();
                template.templateProjectId = templateProjectId;
                template.theSite = curChannel.siteNode;
                template.templateStyle = templateStyle;
                template.encoder = Encoding.GetEncoding(Charset);
                template.TemplatePath = HttpContext.Current.Server.MapPath("~" + TemplateFilePath.Replace("@", curChannel.siteNode.siteWebPath));
                if (curChannel.siteNode.theSiteConfig.baseConfig.TemplateSaveType == 1)
                {
                    template.TemplateContent = Utils.loadTempleteByPath(template.TemplatePath, template.encoder);
                }
                curChannel.siteNode.addTemplate(templateId, template);
            }
            return templateId;
        }
      */ 
        private void matchTemplate()
        {
            //查找模板匹配
            DataTable MatchDT = DataHelper.ExecuteDataTable("select NodeID,ProjectID,ChannelFilePath,ContentFilePath,ChannelFilePathRule,ContentFilePathRule from SN_TemplateMatch"); //DSMethod.Invoke(null, new object[] { "select NodeID,ProjectID,ChannelTemplateID,ContentTemplateID,ChannelFilePath,ContentFilePath from SN_TemplateMatch" }) as DataTable; //查询模板匹配方案
            foreach (DataRow MatchRow in MatchDT.Rows)
            {
                int nodeId = Convert.ToInt32(MatchRow["NodeID"]);
                int ProjectID = Convert.ToInt32(MatchRow["ProjectID"]);
               // int ChannelTemplateID =Convert.ToInt32(MatchRow["ChannelTemplateID"]);
                //int ContentTemplateID =Convert.ToInt32(MatchRow["ContentTemplateID"]);
                string ChannelFilePath = MatchRow["ChannelFilePath"].ToString();
                string ContentFilePath = MatchRow["ContentFilePath"].ToString();
                string ChannelFilePathRule = MatchRow["ChannelFilePathRule"].ToString();
                string ContentFilePathRule = MatchRow["ContentFilePathRule"].ToString();
                Cchannel curChannel = channelDataColls[nodeId];
                //设置模板方案栏目地址
                string channelUrl = (CMSUtils.getPathByPathRule(ChannelFilePathRule,curChannel.nodeID,curChannel.nodeIndexName,curChannel.nodeName,1,null).Replace("//", "/"));
                curChannel.templateProjectColls[ProjectID].setChannelUrl(curChannel.nodeID,channelUrl);
                //设置模板方案内容页地址规则
                ContentFilePathRule = (CMSUtils.getPathByPathRule(ContentFilePathRule, curChannel.nodeID, curChannel.nodeIndexName, curChannel.nodeName, 1, null).Replace("//", "/"));
                curChannel.templateProjectColls[ProjectID].setContentUrlRule(curChannel.nodeID, ContentFilePathRule);
                /*此代码无效
                //添加栏目模板,内容模板
                if (!string.IsNullOrEmpty(ChannelFilePath))
                {
                    if(Utils.getExtName(ChannelFilePath).ToLower()==".aspx")
                    {
                        string newPath = Utils.getAbsolutePath(ChannelFilePath).Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                        int channelTID = setTemplate(curChannel, ChannelTemplateID,1);
                        singlePage channelPage = new singlePage();
                        channelPage.template = curChannel.templateColls[channelTID];
                        channelPage.theChannel = curChannel;
                        SinglePageConfigManager.createInstance().addSinglePage(newPath, channelPage);
                    }
                }
                //内容模板
                if (!string.IsNullOrEmpty(ContentFilePath))
                {
                    if (Utils.getExtName(ContentFilePath).ToLower() == ".aspx")
                    {
                        string newPath = Utils.getAbsolutePath(ContentFilePath).Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                        int contentTID = setTemplate(curChannel, ContentTemplateID,2);
                        singlePage contentPage = new singlePage();
                        contentPage.template = curChannel.templateColls[contentTID];
                        contentPage.theChannel = curChannel;
                        SinglePageConfigManager.createInstance().addSinglePage(newPath, contentPage);
                    }
                }
                 */ 
            }
            MatchDT.Clear();
            MatchDT.Dispose();
            //DataTable indexAndSingleDT = DataHelper.ExecuteDataTable("select TemplateID,TemplateName,TemplateStyle,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,Charset,SiteID,TemplateProjectID from SN_Template where ((TemplateStyle=0 and IsDefault=1) or TemplateStyle=3)"); //DSMethod.Invoke(null, new object[] { "select TemplateID,TemplateName,TemplateStyle,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,Charset,SiteID  from SN_Template where ((TemplateStyle=0 and IsDefault=1) or TemplateStyle=3) and CreatedFileExtName='.aspx'" }) as DataTable;
            DataTable indexAndSingleDT = DataHelper.ExecuteDataTable("select TemplateStyle,CreatedFileFullName,CreatedFileExtName,SiteID,TemplateProjectID from SN_Template where (TemplateStyle=0 and IsDefault=1)");
            //添加栏目单页，栏目模板方案，内容模板方案
            foreach (DataRow pageRow in indexAndSingleDT.Rows)
            {
                int templateProjectId = Convert.ToInt32(pageRow["TemplateProjectID"]);
                int SiteID = Convert.ToInt32(pageRow["siteid"]);
                Cchannel curChannel = SiteConfigManager.createInstance().siteDataColls[SiteID];
                string CreatedFileExtName = Convert.ToString(pageRow["CreatedFileExtName"]);
                string CreatedFileFullName = Convert.ToString(pageRow["CreatedFileFullName"]);
                if (Convert.ToInt32(pageRow["TemplateStyle"]) == 0)
                {
                    string indexUrl = CMSUtils.getAbsolutePath(CreatedFileFullName + CreatedFileExtName);
                    curChannel.siteNode.templateProjectColls[templateProjectId].indexUrl = indexUrl;
                }
                /*
                if (CreatedFileExtName.ToLower().Equals(".aspx"))
                {
                    int templateId = Convert.ToInt32(pageRow["TemplateID"]);
                    //string templateName = Convert.ToString(pageRow["TemplateName"]);
                    int TemplateStyle = Convert.ToInt32(pageRow["TemplateStyle"]);
                    string TemplateFilePath = Convert.ToString(pageRow["TemplateFilePath"]);
                    string Charset = Convert.ToString(pageRow["Charset"]);
                    int TID = setTemplate(templateId,templateProjectId, TemplateFilePath, Charset, TemplateStyle, curChannel);
                    singlePage page = new singlePage();
                    page.template = curChannel.templateColls[TID];
                    page.theChannel = curChannel;
                    string newPath = Utils.getAbsolutePath(CreatedFileFullName + CreatedFileExtName).Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                    SinglePageConfigManager.createInstance().addSinglePage(newPath, page);
                }
                 */ 
            }
        }

     /// <summary>
     /// 设置模型缓存
     /// </summary>
     /// <param name="DSMethod"></param>
        private void setGlobalModel()
        {
            DataTable modelDT = DataHelper.ExecuteDataTable("select * from SN_Model"); // DSMethod.Invoke(null, new object[] { "select * from SN_Model" }) as DataTable; //查询所有模型
            foreach (DataRow row in modelDT.Rows)
            {
                DataModel model = new DataModel();
                model.ModelId = Convert.ToInt32(row["ModelID"]);
                model.ModelName = Convert.ToString(row["ModelName"]);
                model.TableName= Convert.ToString(row["TableName"]);
                model.ItemName = Convert.ToString(row["ItemName"]);
                model.ItemUnit = Convert.ToString(row["ItemUnit"]);
                model.ItemIcon = Convert.ToString(row["ItemIcon"]);
                model.State = Convert.ToInt32(row["State"]);
                //model.UseNumber = Convert.ToInt32(row["UseNumber"]);
                DataTable modelFieldDT = DataHelper.ExecuteDataTable("select * from SN_ModelDescript where ModelName='" + model.ModelName.Replace("'","''")+"' order by indexid desc");//DSMethod.Invoke(null, new object[] { "select * from SN_ModelDescript where ModelName='" + model.ModelName.Replace("'","''")+"' order by indexid desc" }) as DataTable;
                foreach (DataRow fieldRow in modelFieldDT.Rows)
                {
                    ModelField field = new ModelField();
                    field.FieldName = Convert.ToString(fieldRow["FieldName"]);
                    model.addField(field);
                }
                DataModelManager.createInstance().addModel(model.ModelId, model);
            }
        } 
    }

  public  class SinglePageConfigManager
  {
        private static volatile SinglePageConfigManager _instance;
        private static object syncRoot = new Object();
        private SafeDictionary<string, SafeDictionary<string, singlePage>> singlePageIndexColls;
        private SinglePageConfigManager()
        {
            singlePageIndexColls = new SafeDictionary<string, SafeDictionary<string, singlePage>>();
        }
        public static SinglePageConfigManager createInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null) _instance = new SinglePageConfigManager();
                }
            }
            return _instance;
        }
        public singlePage getSinglePage(string path)
        {
            path = path.Trim().ToLower();
            var indexString = path.Substring((int)(path.Length * 0.2), 1) + path.Substring((int)(path.Length * 0.4), 1) + path.Substring((int)(path.Length * 0.6), 1) + path.Substring((int)(path.Length * 0.8), 1);
            try
            {
                return singlePageIndexColls[indexString][path];
            }
            catch
            {
                return null;
            }
        }
        public void removeSinglePage(string path)
        {
            path = path.Trim().ToLower();
            //根据path的2,4,6,8位创建索引
            var indexString = path.Substring((int)(path.Length * 0.2), 1) + path.Substring((int)(path.Length * 0.4), 1) + path.Substring((int)(path.Length * 0.6), 1) + path.Substring((int)(path.Length * 0.8), 1);
            if (singlePageIndexColls.ContainsKey(indexString) && singlePageIndexColls[indexString].ContainsKey(path))
            {
                //singlePageIndexColls[indexString][path].template = null;
                singlePageIndexColls[indexString][path] = null;
                singlePageIndexColls[indexString].Remove(path);
                if (singlePageIndexColls[indexString].Count == 0)
                {
                    singlePageIndexColls.Remove(indexString);
                }
            }
        }
        public void addSinglePage(string path, singlePage page)
        {
            path = path.Trim().ToLower();
            //根据path的2,4,6,8位创建索引
            var indexString = path.Substring((int)(path.Length * 0.2), 1) + path.Substring((int)(path.Length * 0.4), 1) + path.Substring((int)(path.Length * 0.6), 1) + path.Substring((int)(path.Length * 0.8), 1);
            SafeDictionary<string, singlePage> tempSinglePageColls;
            if (!singlePageIndexColls.TryGetValue(indexString, out tempSinglePageColls))
            {
                tempSinglePageColls = new SafeDictionary<string, singlePage>();
                singlePageIndexColls.Add(indexString, tempSinglePageColls);
            }
            addToSinglePageColl(path, page, tempSinglePageColls);
        }
        private void addToSinglePageColl(string path,singlePage page,SafeDictionary<string, singlePage> singlePageColls)
        {
            if (singlePageColls.ContainsKey(path))
            {
                singlePageColls[path] = page;
            }
            else
            {
                singlePageColls.Add(path, page);
            }
        }
   }
}
