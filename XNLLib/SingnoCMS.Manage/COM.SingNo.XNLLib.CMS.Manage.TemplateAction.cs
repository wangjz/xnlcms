using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using System.Data;
using COM.SingNo.DAL;
using System.IO;
using System.Text.RegularExpressions;
using COM.SingNo.XNLEngine;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public  class TemplateAction:IXNLTag<WebContext>
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
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "TemplateAction.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "TemplateAction.Error");
            string actionStr = labelParams["action"].value.ToString().ToLower();
            try
            {
                switch (actionStr)
                {
                    case "addtemplate":
                        return addTemplate(labelContentStr, labelParams, XNLPage, matchSuccessItem, matchFailItem);
                    case "loadcontent":
                        return loadTemplateContent(labelParams, XNLPage);
                    case "modifytemplate":
                        return modifyTemplate(labelContentStr, labelParams, XNLPage, matchSuccessItem, matchFailItem);
                    case "addproject":
                        return addProject(labelContentStr, labelParams, XNLPage, matchSuccessItem, matchFailItem);
                    case "matchtemplate":
                        matchTemplate(labelParams, XNLPage);
                        break;
                    case "modifyrule":
                        modifyRule(labelParams, XNLPage);
                        break;
                    case "delete":
                        deleteTemplate(labelParams, XNLPage);
                        break;
                    case "copy":
                        copyTemplate(labelParams, XNLPage);
                        break;
                    case "setdefault":
                        setDefaultTemplate(labelParams, XNLPage);
                        break;
                }
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                return labelContentStr;
            }
            catch (Exception e)
            {
                //XNLDebug.show(e.Source);
                //XNLDebug.show(e.StackTrace);
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
           */ 
        }
        private string loadTemplateContent(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            //由站点id得到站点路径
            int siteID =Convert.ToInt32(labelParams["siteid"].value);
            string sitePath = (SiteConfigManager.createInstance().siteDataColls[siteID] as Cchannel).siteWebPath;
            string templatePath = labelParams["templatefilepath"].value.ToString();
            templatePath = templatePath.Replace("@", sitePath);
            templatePath = XNLPage.Context.Server.MapPath("~" + templatePath);
            Encoding encoder = Encoding.GetEncoding(labelParams["charset"].value.ToString());
            string contentStr = CMSUtils.loadTempleteByPath(templatePath,encoder);
            contentStr = UtilsCode.encodeHtmlAndXnl(contentStr);
            //模板路径
            return contentStr;
        }
        private string modifyTemplate(string labelContentStr, Dictionary<string, XNLParam> labelParams, WebContext XNLPage, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            string templateName = labelParams["templatename"].value.ToString().Trim();
            //检查 templateName
            if (string.IsNullOrEmpty(templateName.Trim()))
            {
                throw (new Exception("模板名称不能为空!"));
            }
            string charset = labelParams["charset"].value.ToString();
            Encoding encoder;
            try
            {
                encoder = Encoding.GetEncoding(charset);
            }
            catch (Exception e)
            {
                throw (e);
            }
            string extName = labelParams["extname"].value.ToString().Trim();
            string userExtName = labelParams["userextname"].value.ToString().Trim();
            //检查生成文件扩展名
            string fileExtName = extName;
            if (extName.Equals("none")) fileExtName = userExtName;
            int templateID =Convert.ToInt32(labelParams["templateid"].value);
            string createFileName = labelParams["createfilename"].value.ToString().Trim();
            string templateContent = XNLPage.Context.Request[labelParams["contentrequest"].value.ToString()].ToString();
            labelParams.Add("templatecontent", new XNLParam( XNLType.Object, templateContent));
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTransaction = dbConn.BeginTransaction();
            try
            {
                //查询模板信息
                DbCommand dbTempCmd = DataHelper.GetSqlStringCommand("select a.TemplateName,a.TemplateStyle,a.SiteID,b.ProjectName,a.IsDefault,a.TemplateProjectID,a.CreatedFileFullName,a.CreatedFileExtName from SN_Template as a,SN_TemplateProject as b where a.TemplateProjectID=b.ProjectID and a.TemplateID=@templateID");
                dbTempCmd.Connection = dbConn;
                dbTempCmd.Transaction = dbTransaction;
                DataHelper.SetParameterValue(dbTempCmd, labelParams);
                DataTable dt = DataHelper.ExecuteDataTable(dbTempCmd);
                DataRow row = dt.Rows[0];
                int siteId = Convert.ToInt32(row["siteid"]);
                string prevTemplateName = row["TemplateName"].ToString();
                string projectName = row["ProjectName"].ToString();
                int projectId = Convert.ToInt32(row["TemplateProjectID"]);
                int isDefault = Convert.ToInt32(row["isDefault"]);
                int templateStyle = Convert.ToInt32(row["TemplateStyle"]);
                string CreatedFileFullName = Convert.ToString(row["CreatedFileFullName"]);
                string CreatedFileExtName = Convert.ToString(row["CreatedFileExtName"]);
                dt.Dispose();
                //检查生成文件名称
                if (templateStyle == 0 || templateStyle == 3)
                {
                    if (string.IsNullOrEmpty(createFileName.Trim()))
                    {
                        throw (new Exception("模板生成文件名不能为空!"));
                    }
                    if (!Regex.IsMatch(fileExtName, "^\\.[^\\.]+$"))
                    {
                        throw (new Exception("模板生成扩展名格式不正确!"));
                    }
                    //检查生成文件名的合法性
                }
                labelParams.Add("createdfileextname", new XNLParam( XNLType.String, fileExtName));
                if (prevTemplateName != templateName)
                {
                    labelParams.Add("projectid",new XNLParam(XNLType.Int32,projectId));
                    DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TemplateId from sn_template where TemplateProjectID=@projectId and templateName=@templateName and TemplateId<>@TemplateId");
                    dbCmd.Connection = dbConn;
                    dbCmd.Transaction = dbTransaction;
                    DataHelper.SetParameterValue(dbCmd, labelParams);
                    object isTempNameObj = DataHelper.ExecuteScalar(dbCmd);
                    int temId = Convert.ToInt32(Convert.IsDBNull(isTempNameObj) ? 0 : isTempNameObj);
                    if (temId > 0)
                    {
                        throw (new Exception("此模板名称已存在!"));
                    }
                }
                string templateFileName = "T_" + templateName + ".ascx";
                string templateFilePath = "@/Template/" + projectName+"/" + templateFileName;
                Cchannel siteObj =SiteConfigManager.createInstance().siteDataColls[siteId];
                string templateTurePath = XNLPage.Context.Server.MapPath("~" + templateFilePath.Replace("@", siteObj.siteWebPath));
                using (FileStream fs = File.Create(templateTurePath))
                {
                    byte[] info = Encoding.GetEncoding(charset).GetBytes(templateContent);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                if (prevTemplateName != templateName)
                {
                    //删除原来的文件
                    string prevTemplateFilePath = "@/Template/" + projectName + "/T_" + prevTemplateName + ".ascx";
                    File.Delete(XNLPage.Context.Server.MapPath("~" + prevTemplateFilePath.Replace("@", siteObj.siteWebPath)));
                }
                //修改数据库内容
                labelParams.Add("templatefilename", new XNLParam( XNLType.String, templateFileName));
                labelParams.Add("templatefilepath", new XNLParam( XNLType.String, templateFilePath));
                DbCommand dbCommand = DataHelper.GetSqlStringCommand("update SN_Template set TemplateName=@TemplateName,TemplateFileName=@TemplateFileName,TemplateFilePath=@TemplateFilePath,CreatedFileFullName=@createFileName,CreatedFileExtName=@CreatedFileExtName,Charset=@Charset where templateid=@templateid");
                dbCommand.Connection = dbConn;
                dbCommand.Transaction = dbTransaction;
                DataHelper.SetParameterValue(dbCommand, labelParams);
                DataHelper.ExecuteNonQuery(dbCommand);
                #region 此处代码弃用
                /*
                if (fileExtName.ToLower().Equals(".aspx")) //设置缓存
                {
                    Template curTemplate=null;
                    switch (templateStyle)
                    {
                        case 0:
                            if (isDefault == 1)
                            {
                                if (CreatedFileExtName.ToLower().Equals(".aspx"))
                                {
                                    curTemplate = siteObj.templateColls[templateID];
                                }
                                else
                                {
                                    curTemplate = new Template();
                                }
                                curTemplate.encoder = Encoding.GetEncoding(charset);
                                curTemplate.TemplatePath = templateTurePath;
                                curTemplate.templateStyle = templateStyle;
                                curTemplate.theSite = siteObj;
                                curTemplate.templateProjectId = projectId;
                                if (siteObj.theSiteConfig.baseConfig.TemplateSaveType == 1)
                                {
                                    curTemplate.TemplateContent = Utils.loadTempleteByPath(curTemplate.TemplatePath, encoder);
                                }
                                else { curTemplate.TemplateContent = null; }
                                //if (prevTemplateName != templateName) //模板名改变
                                //{
                                //    siteObj.addTemplate(templateName + "_" + templateID, curTemplate);
                                //    removeTemplateFromCache(siteObj, prevTemplateName + "_" + templateID);
                                //}
                                //else 
                                if(string.Compare(CreatedFileExtName,".aspx",true)!=0)
                                {
                                    siteObj.addTemplate(templateID, curTemplate);
                                }
                            }
                            break;
                        case 1:
                        case 2:
                            //查找是否有匹配
                            if (ManageUtil.checkTemplateMatch(templateStyle, templateID, dbTransaction))
                            {
                                curTemplate = siteObj.templateColls[templateID];
                                curTemplate.encoder = Encoding.GetEncoding(charset);
                                curTemplate.TemplatePath = templateTurePath;
                                curTemplate.templateStyle = templateStyle;
                                if (siteObj.theSiteConfig.baseConfig.TemplateSaveType == 1)
                                {
                                    curTemplate.TemplateContent = Utils.loadTempleteByPath(curTemplate.TemplatePath, encoder);
                                }
                                else { curTemplate.TemplateContent = null; }
                                //if (prevTemplateName != templateName) //模板名改变
                                //{
                                //    siteObj.addTemplate(templateName + "_" + templateID, curTemplate);
                                //    removeTemplateFromCache(siteObj, prevTemplateName + "_" + templateID);
                                //}
                                //else 
                                if (string.Compare(CreatedFileExtName, ".aspx", true) != 0)
                                {
                                    siteObj.addTemplate(templateID, curTemplate);
                                }
                            }
                            break;
                        case 3:
                            if (CreatedFileExtName.ToLower().Equals(".aspx"))
                            {
                                curTemplate = siteObj.templateColls[templateID];
                            }
                            else
                            {
                                curTemplate = new Template();
                            }
                            curTemplate.encoder = Encoding.GetEncoding(charset);
                            curTemplate.TemplatePath = templateTurePath;
                            curTemplate.templateStyle = templateStyle;
                            curTemplate.theSite = siteObj;
                            curTemplate.templateProjectId = projectId;
                            if (siteObj.theSiteConfig.baseConfig.TemplateSaveType == 1)
                            {
                                curTemplate.TemplateContent = Utils.loadTempleteByPath(curTemplate.TemplatePath, encoder);
                            }
                            else { curTemplate.TemplateContent = null; }
                            //if (prevTemplateName != templateName) //模板名改变
                            //{
                            //    siteObj.addTemplate(templateName + "_" + templateID, curTemplate);
                            //    removeTemplateFromCache(siteObj, prevTemplateName + "_" + templateID);
                            //}
                            //else 
                            if (string.Compare(CreatedFileExtName, ".aspx", true) != 0)
                            {
                                siteObj.addTemplate(templateID, curTemplate);
                            }
                            break;
                    }
                    if (string.Compare(createFileName, CreatedFileFullName, true) != 0 || string.Compare(fileExtName, CreatedFileExtName, true)!=0)
                    {
                        string webPath = Utils.getAbsolutePath(CreatedFileFullName + CreatedFileExtName).Replace("@", siteObj.siteWebPath).Trim().ToLower().Replace("//", "/");
                        SinglePageConfigManager.createInstance().removeSinglePage(webPath);
                        //添加page cache
                        webPath = Utils.getAbsolutePath(createFileName + fileExtName).Replace("@", siteObj.siteWebPath).Trim().ToLower().Replace("//", "/");
                        singlePage page = new singlePage();
                        page.theChannel = siteObj;
                        page.template = siteObj.templateColls[templateID];
                       SinglePageConfigManager.createInstance().addSinglePage(webPath,page);
                    }
                }
                else if (CreatedFileExtName.ToLower().Equals(".aspx"))
                {
                    //remove page cache
                    string webPath = Utils.getAbsolutePath(CreatedFileFullName + CreatedFileExtName).Replace("@", siteObj.siteWebPath).Trim().ToLower().Replace("//", "/");
                    SinglePageConfigManager.createInstance().removeSinglePage(webPath);
                    if (siteObj.templateColls.ContainsKey(templateID)) //remove
                    {
                        siteObj.templateColls[templateID] = null;
                        siteObj.templateColls.Remove(templateID);
                    }
                }
                //设置缓存end 
                 */  
                #endregion 此处代码弃用
                dbTransaction.Commit();
            }
            catch (Exception e)
            {
                dbTransaction.Rollback();
                dbConn.Close();
                dbTransaction.Dispose();
                //dbConn.Dispose();
                throw (e);
            }
            dbConn.Close();
            dbTransaction.Dispose();
            labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            return labelContentStr;
        }
        private string addProject(string labelContentStr, Dictionary<string, XNLParam> labelParams, WebContext XNLPage, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            string projectName=labelParams["projectname"].value.ToString();
            if (projectName.Trim().Equals(""))
            {
                throw (new Exception("模板方案名称不能为空!"));
            }
            if (labelParams["channelfilepathrule"].value.ToString().Trim().Equals(""))
            {
                throw (new Exception("栏目页默认生成路径规则不能为空!"));
            }
            if (labelParams["contentfilepathrule"].value.ToString().Trim().Equals(""))
            {
                throw (new Exception("内容页默认生成路径规则不能为空!"));
            }
            int curNodeId=ManageUtil.getCurSiteID(XNLPage);
            Cchannel curSiteObj = ChannelConfigManager.createInstance().channelDataColls[curNodeId].siteNode;
            int curManageSiteID = curSiteObj.siteID;
            bool isHave = checkProjectName(projectName, curSiteObj.templateProjectColls);
            if (isHave)
            {
               throw (new Exception("此模板方案名称已存在!"));
            }
            labelParams.Add("siteid",new XNLParam(XNLType.Int32,curManageSiteID));
            labelParams.Add("charset", new XNLParam( XNLType.String, curSiteObj.theSiteConfig.baseConfig.encoder.WebName));
            string siteDirPath = XNLPage.Context.Server.MapPath(curSiteObj.siteWebPath);
            //建立模板方案目录,模板方案初始建三个模板页，首页，栏目页，内容页
            Directory.CreateDirectory(siteDirPath + "Template/"+projectName);
            FileStream fs = File.Create(siteDirPath + "Template/"+projectName+"/T_系统首页模板.ascx");
            FileStream fs1 = File.Create(siteDirPath + "Template/" + projectName + "/T_系统栏目模板.ascx");
            FileStream fs2 = File.Create(siteDirPath + "Template/" + projectName + "/T_系统内容模板.ascx");
            fs.Close();
            fs1.Close();
            fs2.Close();
            //1.将模板方案信息写入数据库
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction transaction = dbConn.BeginTransaction();
           try
           {
               setTemplateProject(projectName, curSiteObj, labelParams, transaction,XNLPage);
               transaction.Commit();
           }
           catch (Exception e)
           {
               transaction.Rollback();
               dbConn.Close();
               transaction.Dispose();
               throw (e);
           }
           dbConn.Close();
           transaction.Dispose();
           labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
           return labelContentStr;
        }
        #endregion
        private bool checkProjectName(string projectName, SafeDictionary<int, TemplateProject> projectColls)
        {
            bool isHave = false;
            foreach (KeyValuePair<int, TemplateProject> project in projectColls)
            {
                if (project.Value.templateProjectName.Trim().Equals(projectName.Trim())) return true;
            }
            return isHave;
        }
        private string addTemplate(string labelContentStr, Dictionary<string, XNLParam> labelParams, WebContext XNLPage, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            string templateName = labelParams["templatename"].value.ToString().Trim();
             //检查 templateName
            if(string.IsNullOrEmpty(templateName.Trim()))
            {
                throw (new Exception("模板名称不能为空!"));
            }
            //检查是否已存在此文件名
            int  templateStyle =Convert.ToInt32(labelParams["templatestyle"].value);
             //检查templateStyle
            if (templateStyle<0)
            {
                throw (new Exception("模板类型值非法!"));
            }
            string createFileName = labelParams["createfilename"].value.ToString().Trim();
            string extName = labelParams["extname"].value.ToString().Trim();
            string userExtName = labelParams["userextname"].value.ToString().Trim();
            string fileExtName = extName;
            if (extName.Equals("none")) fileExtName = userExtName;
            //检查生成文件扩展名
            //检查生成文件名称
            if (templateStyle == 0 || templateStyle >= 3)
            {
                if (string.IsNullOrEmpty(createFileName.Trim()))
                {
                    throw (new Exception("模板生成文件名不能为空!"));
                }
                //检查生成文件名的合法性
                if (!Regex.IsMatch(fileExtName, "^\\.[^\\.]+$"))
                {
                    throw (new Exception("模板生成扩展名格式不正确!"));
                }
            }
           
            labelParams.Add("fileextname", new XNLParam(XNLType.String,fileExtName));
            string templateContent = XNLPage.Context.Request[labelParams["contentrequest"].value.ToString()].ToString();
            labelParams.Add("templatecontent", new XNLParam( XNLType.String, templateContent));
           //网页编码
             string charset = labelParams["charset"].value.ToString().Trim();
            Encoding encoder;
            try{
                encoder = Encoding.GetEncoding(charset);
            }catch(Exception e)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            //检查模板名是否已存在
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TemplateId from sn_template where TemplateProjectID=@projectId and templateName=@templateName");
            dbCmd.Connection = dbConn;
            dbCmd.Transaction = transaction;
            DataHelper.SetParameterValue(dbCmd, labelParams);
            try
            {
                object tempIdObj = DataHelper.ExecuteScalar(dbCmd);
                int tempId=Convert.ToInt32(Convert.IsDBNull(tempIdObj)?0:tempIdObj);
                if (tempId > 0)
                {
                    throw (new Exception("此模板名称已存在。"));
                }
                //设置模板文件路径
                string templateFileName = "T_" + templateName + ".ascx";
                int projectId =Convert.ToInt32(labelParams["projectid"].value);
                int curNodeId=ManageUtil.getCurSiteID(XNLPage);
                Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[curNodeId].siteNode;
                int curSiteId = curChannel.siteID;
                string templateFullPath = "@/Template/" + curChannel.templateProjectColls[projectId].templateProjectName + "/" + templateFileName;
                labelParams.Add("templatefilename",new XNLParam(XNLType.String,templateFileName));
                labelParams.Add("templatefilepath", new XNLParam( XNLType.String, templateFullPath));
                labelParams.Add("siteid", new XNLParam( XNLType.Int32, curSiteId));
                //将信息写入数据库
                DbCommand dbInsertCmd = DataHelper.GetSqlStringCommand("insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values(@TemplateName,@TemplateStyle,@TemplateFileName,@TemplateFilePath,@createFileName,@FileExtName,0,@Charset,0,@SiteID,@ProjectID)");
                dbInsertCmd.Connection = dbConn;
                dbInsertCmd.Transaction = transaction;
                DataHelper.SetParameterValue(dbInsertCmd, labelParams);
                dbInsertCmd.ExecuteNonQuery();
                DbCommand dbSelectCmd = DataHelper.GetSqlStringCommand("select max(templateID) from SN_Template where templateID>0");
                dbSelectCmd.Connection = dbConn;
                dbSelectCmd.Transaction = transaction;
                object theTemplateIdObj = DataHelper.ExecuteScalar(dbSelectCmd);
                //写模板文件
                string templateTruePath=XNLPage.Context.Server.MapPath("~" + templateFullPath.Replace("@", curChannel.siteWebPath));
                using (FileStream fs = File.Create(templateTruePath))
                {
                    byte[] info = encoder.GetBytes(templateContent);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
                #region 此处代码弃用
                /*
                if (templateStyle == 3)
                {
                    if (fileExtName.ToLower().Equals(".aspx") || curChannel.siteNode.theSiteConfig.baseConfig.AccessType > 0)
                    {
                        //需要将此模板放入缓存 
                        Template theTemplate = new Template();
                        theTemplate.encoder = encoder;
                        theTemplate.theSite = curChannel.siteNode;
                        theTemplate.TemplatePath = templateTruePath;
                        if (curChannel.siteNode.theSiteConfig.baseConfig.TemplateSaveType == 1)
                        {
                            theTemplate.TemplateContent =Utils.loadTempleteByPath(templateTruePath,encoder);
                        }
                        curChannel.siteNode.addTemplate(Convert.ToInt32(theTemplateIdObj),theTemplate);
                    }
                }
                 */
                #endregion
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                dbConn.Close();
                transaction.Dispose();
                throw (e);
            }
            dbConn.Close();
            transaction.Dispose();
            labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            return labelContentStr;
        }
        private void setTemplateProject(string projectName, Cchannel curSiteChannel, Dictionary<string, XNLParam> labelParams, DbTransaction transaction, WebContext XNLPage)
        {
            //建模板方案
            string indexTemplatePath = "@/Template/" + projectName + "/T_系统首页模板.ascx";
            string channelTemplatePath = "@/Template/" + projectName + "/T_系统栏目模板.ascx";
            string contentTemplatePath = "@/Template/" + projectName + "/T_系统内容模板.ascx";
            labelParams.Add("sysindex", new XNLParam(indexTemplatePath));
            labelParams.Add("syschannel", new XNLParam(channelTemplatePath));
            labelParams.Add("syscontent", new XNLParam(contentTemplatePath));
            labelParams.Add("createfilepath", new XNLParam("@/index"));
            //建模板表
            Dictionary<string, string> sqlColls = new Dictionary<string, string>();
            string templateProjectSql = "insert into SN_TemplateProject(ProjectName,SiteID,ChannelFilePathRule,ContentFilePathRule,IsDefault)values(@projectname,@siteid,@channelfilepathrule,@contentfilepathrule,0)";
            string selectProjectSql = "select max(ProjectID) from SN_TemplateProject where siteid=@siteid";
            string selectNodeCountSql = "select count(NodeId) from sn_nodes where rootid=" + curSiteChannel.nodeID;
            string indexTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统首页模板',0,'T_系统首页模板.ascx',@sysindex,@createfilepath,'.html',1,@charset,1,@siteid,@_dbresult1)";
            string channelTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统栏目模板',1,'T_系统栏目模板.ascx',@syschannel,1,@charset,@_dbresult2,@siteid,@_dbresult1)";
            string contentTemplateSql = "insert into SN_Template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values('系统内容模板',2,'T_系统内容模板.ascx',@syscontent,1,@charset,@_dbresult2,@siteid,@_dbresult1)";
            string selectIndexTemplateSql = "select TemplateID from SN_Template where SiteID=@siteid and TemplateProjectID=@_dbresult1 and TemplateStyle=0";
            string selectChannelTemplateSql = "select TemplateID from SN_Template where SiteID=@siteid and TemplateProjectID=@_dbresult1 and TemplateStyle=1";
            string selectContentTemplateSql = "select TemplateID from SN_Template where SiteID=@siteid and TemplateProjectID=@_dbresult1 and TemplateStyle=2";
            string updateTemProSql = "update SN_TemplateProject set IndexTemplateID=@_dbresult6,ChannelTemplateID=@_dbresult7,ContentTemplateID=@_dbresult8 where ProjectID=@_dbresult1";
            sqlColls.Add("Project", templateProjectSql);
            sqlColls.Add("selectproject", selectProjectSql);
            sqlColls.Add("selectnodecount", selectNodeCountSql);
            sqlColls.Add("indexTemplate", indexTemplateSql);
            sqlColls.Add("channelTemplate", channelTemplateSql);
            sqlColls.Add("contentTemplate", contentTemplateSql);
            sqlColls.Add("selectindext", selectIndexTemplateSql);
            sqlColls.Add("selectct", selectChannelTemplateSql);
            sqlColls.Add("selectcot", selectContentTemplateSql);
            sqlColls.Add("uptp", updateTemProSql);
            //查找站点所有列表
            DbCommand getNodesCmd = DataHelper.GetSqlStringCommand("select NodeId from sn_nodes where rootid=" + curSiteChannel.nodeID);
            getNodesCmd.Connection = transaction.Connection;
            getNodesCmd.Transaction = transaction;
            DataTable nodesDT = DataHelper.ExecuteDataTable(getNodesCmd);
            foreach (DataRow row in nodesDT.Rows)
            {
                string addTemMatchSql = "insert into SN_TemplateMatch (NodeID,ProjectID,SiteID,ChannelTemplateID,ContentTemplateID,ChannelFilePathRule,ContentFilePathRule)values(" + row["nodeid"].ToString() + ",@_dbresult1,@siteid,@_dbresult7,@_dbresult8,@channelfilepathrule,@contentfilepathrule)";
                sqlColls.Add("addtm" + row["nodeid"].ToString(), addTemMatchSql);
            }
            DataHelper.ExrcuteScalarSomeSql(sqlColls, labelParams, transaction);
            #region 代码已不使用
            /*
            int projectId =Convert.ToInt32(labelParams["_dbresult1"].theValue);
            TemplateProject templateProject = new TemplateProject();
            templateProject.templateProjectName = projectName;
            templateProject.indexUrl = "@/index.html";
            string ChannelFilePathRule = labelParams["channelfilepathrule"].theValue.ToString();
            string ContentFilePathRule = labelParams["contentfilepathrule"].theValue.ToString();
            foreach (DataRow row in nodesDT.Rows)
            {
                Cchannel _curChannel = ChannelConfigManager.createInstance().channelDataColls[Convert.ToInt32(row["nodeid"])];
                //设置模板方案栏目地址
                string channelUrl = (Utils.getPathByPathRule(ChannelFilePathRule, _curChannel.nodeID, _curChannel.nodeIndexName, _curChannel.nodeName, 1, null).Replace("//", "/"));
                templateProject.setChannelUrl(_curChannel.nodeID, channelUrl);
                //设置模板方案内容页地址规则
                ContentFilePathRule = (Utils.getPathByPathRule(ContentFilePathRule, _curChannel.nodeID, _curChannel.nodeIndexName, _curChannel.nodeName, 1, null).Replace("//", "/"));
                templateProject.setContentUrlRule(_curChannel.nodeID, ContentFilePathRule);
            }
            curSiteChannel.addTemplateProject(projectId, templateProject);
           
            if (curSiteChannel.theSiteConfig.baseConfig.AccessType > 0)
            {
                //将模板放入缓存
                int indexTemplateID = Convert.ToInt32(labelParams["_dbresult6"].theValue);
                int channelTemplateID = Convert.ToInt32(labelParams["_dbresult7"].theValue);
                int contentTemplateID = Convert.ToInt32(labelParams["_dbresult8"].theValue);
                Template indexTemplate = new Template();
                indexTemplate.theSite = curSiteChannel;
                indexTemplate.encoder = curSiteChannel.theSiteConfig.baseConfig.encoder;
                indexTemplate.TemplatePath = XNLPage.Context.Server.MapPath("~" + indexTemplatePath.Replace("@",curSiteChannel.siteWebPath));
                Template channelTemplate = new Template();
                channelTemplate.theSite = curSiteChannel;
                channelTemplate.encoder = curSiteChannel.theSiteConfig.baseConfig.encoder;
                channelTemplate.TemplatePath = XNLPage.Context.Server.MapPath("~" + channelTemplatePath.Replace("@", curSiteChannel.siteWebPath));
                Template contentTemplate = new Template();
                contentTemplate.theSite = curSiteChannel;
                contentTemplate.encoder = curSiteChannel.theSiteConfig.baseConfig.encoder;
                contentTemplate.TemplatePath = XNLPage.Context.Server.MapPath("~" + contentTemplatePath.Replace("@", curSiteChannel.siteWebPath));
                if (curSiteChannel.theSiteConfig.baseConfig.TemplateSaveType == 1)
                {
                    indexTemplate.TemplateContent = Utils.loadTempleteByPath(indexTemplate.TemplatePath, indexTemplate.encoder);
                    channelTemplate.TemplateContent = Utils.loadTempleteByPath(channelTemplate.TemplatePath, channelTemplate.encoder);
                    contentTemplate.TemplateContent = Utils.loadTempleteByPath(contentTemplate.TemplatePath, contentTemplate.encoder);
                }
                curSiteChannel.addTemplate(indexTemplateID, indexTemplate);
                curSiteChannel.addTemplate(channelTemplateID, channelTemplate);
                curSiteChannel.addTemplate(contentTemplateID, contentTemplate);
            }
             */
#endregion
        }
        private void matchTemplate(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            string matchStr = labelParams["match"].value.ToString().Trim();
            if (!matchStr.Equals(""))
            {
                int[, ] nodeList;
                string [] matchArr;
                matchArr = matchStr.Split('|');
                nodeList = new int[matchArr.Length, 5];  //n*5数组,0:nodeid,1:channelTemplateId,2:contentTemplateId,3:栏目模板是否改变，0，不改变1，改变 4：内容模板是否改变，0，不改变1，改变
                //得到所有节点
                //设置所有模板
                Dictionary<int, int> templatesColl = new Dictionary<int, int>();
                string nodesStr="";
                for (var i = 0; i < matchArr.Length; i++)
                {
                    string []nodeMatchArr = matchArr[i].Split(',');
                    int nodeId=Convert.ToInt32(nodeMatchArr[0]) ;
                    int channelTemplateId=Convert.ToInt32(nodeMatchArr[1]);
                    int contentTemplateId=Convert.ToInt32(nodeMatchArr[2]);
                    nodeList[i,0] =nodeId ;
                    nodeList[i, 1] = channelTemplateId;
                    nodeList[i, 2] = contentTemplateId;
                    nodeList[i, 3] = 0;
                    nodeList[i, 4] = 0;
                    nodesStr += (i == 0 ? "" : ",") + nodeMatchArr[0];
                    if (!templatesColl.ContainsKey(channelTemplateId))
                    {
                        templatesColl.Add(channelTemplateId, 0);
                    }
                    if (!templatesColl.ContainsKey(contentTemplateId))
                    {
                        templatesColl.Add(contentTemplateId, 0);
                    }
                }
                //查找所有节点匹配
                DbConnection dbConn = DataHelper.CreateConnection();
                dbConn.Open();
                DbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    string getMatchSql = "select NodeID,ChannelTemplateID,ContentTemplateID,ChannelFilePath,ContentFilePath from SN_TemplateMatch where ProjectID=@ProjectID and nodeId in (" + nodesStr + ")";
                    DbCommand dbMatchCmd = DataHelper.GetSqlStringCommand(getMatchSql);
                    dbMatchCmd.Connection = dbConn;
                    dbMatchCmd.Transaction = transaction;
                    DataHelper.SetParameterValue(dbMatchCmd, labelParams);
                    DataTable matchDt = DataHelper.ExecuteDataTable(dbMatchCmd);
                    //查找以前节点的匹配的栏目模板，内容模板
                    int[,] oldNodeMatchList = new int[matchDt.Rows.Count, 3];
                    Dictionary<int, string> channelTemplatePathColls = new Dictionary<int, string>();
                    Dictionary<int, string> contentTemplatePathColls = new Dictionary<int, string>();
                    for (int i = 0; i < matchDt.Rows.Count; i++)
                    {
                        int channelTemplateId = Convert.ToInt32(matchDt.Rows[i]["ChannelTemplateID"]);
                        int contentTemplateId = Convert.ToInt32(matchDt.Rows[i]["ContentTemplateID"]);
                        int _nodeId = Convert.ToInt32(matchDt.Rows[i]["nodeID"]);
                        channelTemplatePathColls.Add(_nodeId, Convert.ToString(matchDt.Rows[i]["ChannelFilePath"]));
                        contentTemplatePathColls.Add(_nodeId, Convert.ToString(matchDt.Rows[i]["ContentFilePath"]));
                        oldNodeMatchList[i, 0] = _nodeId;
                        oldNodeMatchList[i, 1] = channelTemplateId;
                        oldNodeMatchList[i, 2] = contentTemplateId;
                        if (!templatesColl.ContainsKey(channelTemplateId))
                        {
                            templatesColl.Add(channelTemplateId, 0);
                        }
                        if (!templatesColl.ContainsKey(contentTemplateId))
                        {
                            templatesColl.Add(contentTemplateId, 0);
                        }

                    }
                    //查找所有模板的使用情况
                    string allTemplateStr = "";
                    int k = 0;
                    foreach (KeyValuePair<int, int> tmp in templatesColl)
                    {
                        allTemplateStr += (k > 0 ? "," : "") + tmp.Key.ToString();
                        k++;
                    }
                    DbCommand dbUserCmd = DataHelper.GetSqlStringCommand("select TemplateID,UseNumber,templateName,Charset,TemplateFilePath,TemplateStyle from SN_Template where TemplateID in (" + allTemplateStr + ")");
                    dbUserCmd.Connection = dbConn;
                    dbUserCmd.Transaction = transaction;
                    DataTable allTemplateDT = DataHelper.ExecuteDataTable(dbUserCmd);
                    Dictionary<int, string> templatesCharsetColl = new Dictionary<int, string>();
                    Dictionary<int, string> templatesPathColl = new Dictionary<int, string>();
                    Dictionary<int, int> templatesStyleColl = new Dictionary<int, int>();
                    foreach (DataRow row in allTemplateDT.Rows)
                    {
                        int id = Convert.ToInt32(row["TemplateID"]);
                        templatesColl[id] = Convert.ToInt32(row["UseNumber"]);
                        templatesCharsetColl.Add(id, Convert.ToString(row["Charset"]));
                        templatesPathColl.Add(id, Convert.ToString(row["TemplateFilePath"]));
                        templatesStyleColl.Add(id, Convert.ToInt32(row["TemplateStyle"]));
                    }
                    Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                    //判断栏目模板,内容模板是否有更改
                    for (var i = 0; i < nodeList.GetLength(0); i++)
                    {
                        for (var j = 0; j < oldNodeMatchList.GetLength(0); j++)
                        {
                            if (nodeList[i, 0] == oldNodeMatchList[j, 0])
                            {
                                if (nodeList[i, 1] != oldNodeMatchList[j, 1]) //栏目模板有改变
                                {
                                    templatesColl[oldNodeMatchList[j, 1]] -= 1;
                                    templatesColl[nodeList[i, 1]] += 1;
                                    nodeList[i, 3] = 1;
                                }

                                if (nodeList[i, 2] != oldNodeMatchList[j, 2]) //内容模板有改变
                                {
                                    templatesColl[oldNodeMatchList[j, 2]] -= 1;
                                    templatesColl[nodeList[i, 2]] += 1;
                                    nodeList[i, 4] = 1;
                                }
                                break;
                            }
                        }
                        sqlColls.Add("s" + i, "update SN_TemplateMatch set ChannelTemplateID=" + nodeList[i, 1] + " , ContentTemplateID=" + nodeList[i, 2] + " where NodeID=" + nodeList[i, 0] + " and ProjectID=@ProjectID");
                    }

                    Cchannel curSiteNode = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode;
                    foreach (KeyValuePair<int, int> tmp in templatesColl)
                    {
                        sqlColls.Add("use" + tmp.Key, "update SN_Template set  UseNumber=" + tmp.Value + " where TemplateID=" + tmp.Key);
                        #region  此处代码弃用
                        /*
                        var tmpKey = tmp.Key;
                        if (tmp.Value == 0) //没有节点匹配，需从缓存删除
                        {
                            if (curSiteNode.templateColls!=null&&curSiteNode.templateColls.ContainsKey(tmpKey))
                            {
                                curSiteNode.templateColls[tmpKey] = null;
                                curSiteNode.templateColls.Remove(tmpKey);
                            }
                        }
                        else
                        {
                            int templateStyle = templatesStyleColl[tmp.Key];//得到模板类型
                            for (var i = 0; i < nodeList.GetLength(0); i++)
                            {
                                string templateCreatePath = (templateStyle == 1 ? channelTemplatePathColls[nodeList[i, 0]] : contentTemplatePathColls[nodeList[i, 0]]);
                                if (nodeList[i, templateStyle] == tmp.Key && ManageUtil.getExtName(templateCreatePath).ToLower().Equals(".aspx"))  //需放入缓存
                                {
                                    if (curSiteNode.templateColls == null || !curSiteNode.templateColls.ContainsKey(tmpKey))  //需加入缓存
                                    {
                                        string templatePath = XNLPage.Context.Server.MapPath("~" + templatesPathColl[tmp.Key].Replace("@", curSiteNode.siteWebPath));
                                        addTemplateToCache(tmpKey, templateStyle, templatesCharsetColl[tmp.Key], curSiteNode, templatePath);
                                    }
                                    //更新page  模板
                                    if (nodeList[i, 3] == 1 && templateStyle==1)
                                    {
                                        Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeList[i, 0]];
                                        string pagePath = channelTemplatePathColls[nodeList[i, 0]].Trim().Replace("@", curChannel.siteWebPath).Replace("//", "/").ToLower();
                                        SinglePageConfigManager.createInstance().getSinglePage(pagePath).template = curChannel.templateColls[tmpKey];
                                    }
                                    if (nodeList[i, 4] == 1 && templateStyle == 2)
                                    {
                                        Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeList[i, 0]];
                                        string pagePath = contentTemplatePathColls[nodeList[i, 0]].Trim().Replace("@", curChannel.siteWebPath).Replace("//", "/").ToLower();
                                        SinglePageConfigManager.createInstance().getSinglePage(pagePath).template = curChannel.templateColls[tmpKey];
                                    }
                                }
                            }
                        }
                         */
                        #endregion
                    }
                    DataHelper.ExrcuteScalarSomeSql(sqlColls, labelParams, transaction);
                    transaction.Commit();
                    dbConn.Close();
                    transaction.Dispose();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    dbConn.Close();
                    transaction.Dispose();
                    throw (e);
                }
            }
        }
      /*  
      private Template addTemplateToCache(int templateId,int templateType, string charset, Cchannel Site, string templatePath)
        {
            Template template = new Template();
            template.encoder = Encoding.GetEncoding(charset);
            template.theSite = Site;
            template.TemplatePath = templatePath;
            if (Site.theSiteConfig.baseConfig.TemplateSaveType == 1)
            {
                template.TemplateContent = Utils.loadTempleteByPath(template.TemplatePath, template.encoder);
            }
            Site.addTemplate(templateId, template);
            return template;
        }
       */ 
        private void modifyRule(Dictionary<string, XNLParam> labelParams, XNLContext XNLPage)
        {
            labelParams["nodeid"].type = XNLType.Int32;
            labelParams["projectid"].type = XNLType.Int32;
            labelParams["channelpathrule"].type = XNLType.String;
            labelParams["contentpathrule"].type = XNLType.String;
            string channelPathRule=Convert.ToString(labelParams["channelpathrule"].value);
            string contentPathRule = Convert.ToString(labelParams["contentpathrule"].value);
            int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
            int projectId = Convert.ToInt32(labelParams["projectid"].value);
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            try
            {
                Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
                string ChannelPath = getPathByPathRule(channelPathRule, nodeId, 0);
                string ContentPath = getPathByPathRule(contentPathRule, nodeId, 1);
#region 此处代码弃用

                /*
                //取得以前的页面生成信息
                DbCommand cmd = DataHelper.GetSqlStringCommand("select ChannelTemplateID,ContentTemplateID,ChannelFilePath,ContentFilePath from SN_TemplateMatch where NodeID=@nodeid and ProjectID=@ProjectID");
                cmd.Connection = dbConn;
                cmd.Transaction = transaction;
                DataHelper.SetParameterValue(cmd, labelParams);
                DataTable nodeTemplateMatchDt = DataHelper.ExecuteDataTable(cmd);
                string prevChannelPath = Convert.ToString(nodeTemplateMatchDt.Rows[0]["ChannelFilePath"]);
                string prevContentPath = Convert.ToString(nodeTemplateMatchDt.Rows[0]["ContentFilePath"]);
                int ChannelTemplateID = Convert.ToInt32(nodeTemplateMatchDt.Rows[0]["ChannelTemplateID"]);
                int ContentTemplateID = Convert.ToInt32(nodeTemplateMatchDt.Rows[0]["ContentTemplateID"]);
                
                if (!Utils.getExtName(prevChannelPath).ToLower().Equals(".aspx") && Utils.getExtName(ChannelPath).ToLower().Equals(".aspx"))  //以前的路径为空或以前的文件不为aspx，当前的路径为aspx文件 ,需要添加进page缓存
                {
                    string path = ChannelPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                    if (SinglePageConfigManager.createInstance().getSinglePage(path) == null)  //不存在，添加
                    {
                        singlePage page = new singlePage();
                        page.theChannel = curChannel;
                        if (curChannel.siteNode.templateColls.ContainsKey(ChannelTemplateID))
                        {
                            page.template = curChannel.siteNode.templateColls[ChannelTemplateID];
                        }
                        else //添加模板
                        {
                            page.template = addTemplateToCache(ChannelTemplateID, curChannel.siteNode, XNLPage,transaction);
                        }
                        SinglePageConfigManager.createInstance().addSinglePage(path, page);
                    }
                }
                else
                {
                    string path = ChannelPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                    //如果以前为apsx文件
                    if (Utils.getExtName(prevChannelPath).ToLower().Equals(".aspx") && string.Compare(prevChannelPath, ChannelPath) != 0)
                    {
                        string prevPath = prevChannelPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                        //更新缓存
                        if (Utils.getExtName(ChannelPath).ToLower().Equals(".aspx"))
                        {
                            singlePage page = SinglePageConfigManager.createInstance().getSinglePage(prevPath);
                            SinglePageConfigManager.createInstance().addSinglePage(path, page);
                            SinglePageConfigManager.createInstance().removeSinglePage(prevPath);
                        }
                        else  //清除缓存
                        {
                            SinglePageConfigManager.createInstance().removeSinglePage(prevPath);
                        }
                    }
                }
                //设置内容页
                
                if (!Utils.getExtName(prevContentPath).ToLower().Equals(".aspx") && Utils.getExtName(ContentPath).ToLower().Equals(".aspx"))  //以前的路径为空或以前的文件不为aspx，当前的路径为aspx文件 ,需要添加进page缓存
                {
                    string path = ContentPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                    if (SinglePageConfigManager.createInstance().getSinglePage(path) == null)  //不存在，添加
                    {
                        singlePage page = new singlePage();
                        page.theChannel = curChannel;
                        if (curChannel.siteNode.templateColls.ContainsKey(ContentTemplateID))
                        {
                            page.template = curChannel.siteNode.templateColls[ContentTemplateID];
                        }
                        else //添加模板
                        {
                            page.template = addTemplateToCache(ContentTemplateID, curChannel.siteNode, XNLPage,transaction);
                        }
                        SinglePageConfigManager.createInstance().addSinglePage(path, page);
                    }
                }
                else
                {
                    string path = ContentPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                    //如果以前为apsx文件
                    if (Utils.getExtName(prevContentPath).ToLower().Equals(".aspx") && string.Compare(prevContentPath, ContentPath) != 0)
                    {
                        string prevPath = prevContentPath.Replace("@", curChannel.siteWebPath).Trim().ToLower().Replace("//", "/");
                        //更新缓存
                        if (Utils.getExtName(ContentPath).ToLower().Equals(".aspx"))
                        {
                            singlePage page = SinglePageConfigManager.createInstance().getSinglePage(prevPath);
                            SinglePageConfigManager.createInstance().addSinglePage(path, page);
                            SinglePageConfigManager.createInstance().removeSinglePage(prevPath);
                        }
                        else  //清除缓存
                        {
                            SinglePageConfigManager.createInstance().removeSinglePage(prevPath);
                        }
                    }
                }
                //end
                 */
#endregion
                labelParams.Add("channelfilepath", new XNLParam( XNLType.String, ChannelPath));
                labelParams.Add("contentfilepath", new XNLParam( XNLType.String, ContentPath));
                //更新数据库
                DbCommand updateCmd = DataHelper.GetSqlStringCommand("update SN_TemplateMatch set ChannelFilePathRule=@channelPathRule,ContentFilePathRule=@contentPathRule,ChannelFilePath=@ChannelFilePath,ContentFilePath=@contentFilePath where NodeID=@NodeID and ProjectID=@ProjectID");
                updateCmd.Connection = dbConn;
                updateCmd.Transaction = transaction;
                DataHelper.SetParameterValue(updateCmd, labelParams);
                updateCmd.ExecuteNonQuery();
                transaction.Commit();
                dbConn.Close();
                dbConn.Dispose();
                transaction.Dispose();
#region 此处代码弃用

                /*
                //设置模板方案栏目地址
                TemplateProject templateProject = curChannel.templateProjectColls[projectId];
                string channelUrl = (Utils.getPathByPathRule(channelPathRule, curChannel.nodeID, curChannel.nodeIndexName, curChannel.nodeName, 1, null).Replace("//", "/"));
                templateProject.setChannelUrl(curChannel.nodeID, channelUrl);
                //设置模板方案内容页地址规则
                string ContentFilePathRule = (Utils.getPathByPathRule(contentPathRule, curChannel.nodeID, curChannel.nodeIndexName, curChannel.nodeName, 1, null).Replace("//", "/"));
                templateProject.setContentUrlRule(curChannel.nodeID, ContentFilePathRule);
                 */
#endregion
            }
            catch (Exception e)
            {
                transaction.Rollback();
                dbConn.Close();
                transaction.Dispose();
                throw (e);
            }
        }
      /*
        private void removeTemplateFromCache(Cchannel Site, int templateId)
        {
            if (Site.templateColls.ContainsKey(templateId))
            {
                //Site.templateColls[templateName] = null;
                Site.templateColls.Remove(templateId);
            }
        }
       */ 
        private string getPathByPathRule(string pathRule,int nodeId,int ruleType)
        {
            string extStr = CMSUtils.getExtName(pathRule);
            if (extStr.ToLower().Equals(".aspx"))
            {
                string fullPath = CMSUtils.getAbsolutePath(pathRule);
                if (ruleType == 0)
                {
                    return fullPath.Replace("[#ChannelID]", nodeId.ToString());
                }
                else
                {
                    return fullPath.Replace("[#ChannelID]", nodeId.ToString()).Replace("[#ContentID]", "Content");
                }
            }
            return "";
        }
      /*
        private Template addTemplateToCache(int templateId, Cchannel site,IBasePage XNLPage,DbTransaction transaction)
        {
            DbCommand cmd = DataHelper.GetSqlStringCommand("select TemplateStyle,TemplateFilePath,Charset from SN_Template where TemplateID=" + templateId);
            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            DataTable templateDt = DataHelper.ExecuteDataTable(cmd);
            DataRow row = templateDt.Rows[0];
            int templateStyle = Convert.ToInt32(row["TemplateStyle"]);
            string templatePath = Convert.ToString(row["TemplateFilePath"]);
            string charset = Convert.ToString(row["Charset"]);
            templatePath = XNLPage.Context.Server.MapPath("~" + templatePath.Replace("@", site.siteWebPath));
            return addTemplateToCache(templateId, templateStyle, charset, site, templatePath);
        }
       */ 
        private void deleteTemplate(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            //查询是默认，或是正在使用，或是错误页
            int templateId;
            string templateIdStr=labelParams["templateid"].value.ToString();
            if(!Int32.TryParse(templateIdStr,out templateId))
            {
                throw (new Exception("模板编号类型不正确"));
            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            try
            {
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TemplateStyle,UseNumber,IsDefault,TemplateFilePath,SiteID from SN_Template where TemplateID=" + templateId);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = transaction;
                DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                DataRowCollection dataRows = dt.Rows;
                if(dataRows.Count>0)
                {
                    DataRow row = dataRows[0];
                    int templateType = Convert.ToInt32(row["TemplateStyle"]);
                    int UseNumber = Convert.ToInt32(row["UseNumber"]);
                    int IsDefault = Convert.ToInt32(row["IsDefault"]);
                    if(templateType>=0&&templateType<=2)
                    {
                        if(IsDefault>=1)
                        {
                            throw (new Exception("此模板为默认模板，不能删除"));
                        }
                        if (UseNumber >0)
                        {
                            throw (new Exception("此模板正在使用，不能删除"));
                        }
                    }
                    else
                    {
                        if (templateType ==4)
                        {
                            throw (new Exception("错误页模板，不能删除"));
                        }
                    }
                    //可以删除
                    dbCmd.CommandText = "delete from SN_Template where TemplateID="+templateId;
                    dbCmd.ExecuteNonQuery();
                    //删除模板文件
                    string TemplateFilePath = row["TemplateFilePath"].ToString();
                    int siteId = Convert.ToInt32(row["SiteID"]);
                    Cchannel siteObj = SiteConfigManager.createInstance().siteDataColls[siteId];
                    TemplateFilePath = TemplateFilePath.Replace("@",XNLPage.Context.Server.MapPath(siteObj.siteWebPath)).Replace("//", "/");
                    File.Delete(TemplateFilePath);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch {}        		
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                transaction.Dispose();
            }
        }
        private void copyTemplate(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            string templateName = labelParams["templatename"].value.ToString().Trim();
            //检查 templateName
            if (string.IsNullOrEmpty(templateName.Trim()))
            {
                throw (new Exception("模板名称不能为空!"));
            }
            int templateId;
            string templateIdStr = labelParams["templateid"].value.ToString();
            if (!Int32.TryParse(templateIdStr, out templateId))
            {
                throw (new Exception("模板编号类型不正确"));
            }

            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            try
            {
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TemplateStyle,CreatedFileFullName,CreatedFileExtName,Charset,SiteID,TemplateProjectID,TemplateFilePath,TemplateFileName from SN_Template where TemplateID=" + templateId);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = transaction;
                DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                DataRowCollection dataRows = dt.Rows;
                if (dataRows.Count > 0)
                {
                    DataRow row = dataRows[0];
                    int templateType = Convert.ToInt32(row["TemplateStyle"]);
                    int projectId = Convert.ToInt32(row["TemplateProjectID"]);
                    int SiteID = Convert.ToInt32(row["SiteID"]);
                    labelParams["templatename"].type = XNLType.String;
                   // labelParams["templatename"].dbType = DbType.String;
                    dbCmd.CommandText="select TemplateId from sn_template where TemplateProjectID="+projectId+" and templateName=@templateName";
                    DataHelper.SetParameterValue(dbCmd, labelParams);
                    object tempIdObj = DataHelper.ExecuteScalar(dbCmd);
                    int tempId=Convert.ToInt32(Convert.IsDBNull(tempIdObj)?0:tempIdObj);
                    if (tempId > 0)
                    {
                        throw (new Exception("此模板名称已存在。"));
                    }
                    string srcT_name = row["TemplateFileName"].ToString();
                    string srcT_path = row["TemplateFilePath"].ToString();
                    string nowT_name="T_" + templateName + ".ascx";
                    string nowT_path = srcT_path.Replace(srcT_name, nowT_name);
                    labelParams.Add("templatefilename", new XNLParam(XNLType.String, nowT_name));
                    labelParams.Add("templatefilepath", new XNLParam(XNLType.String, nowT_path));
                    labelParams.Add("createdfile", new XNLParam(XNLType.String, row["CreatedFileFullName"].ToString()));
                    labelParams.Add("extname", new XNLParam(XNLType.String, row["CreatedFileExtName"].ToString()));
                    labelParams.Add("charset", new XNLParam(XNLType.String, row["Charset"].ToString()));
                    dbCmd.Parameters.Clear();
                    dbCmd.CommandText = "insert into sn_template(TemplateName,TemplateStyle,TemplateFileName,TemplateFilePath,CreatedFileFullName,CreatedFileExtName,UseNumber,Charset,IsDefault,SiteID,TemplateProjectID)values(@TemplateName," + templateType + ",@templatefilename,@templatefilepath,@createdfile,@extname,0,@charset,0,"+SiteID+","+projectId+")";
                    DataHelper.SetParameterValue(dbCmd, labelParams);
                    dbCmd.ExecuteNonQuery();
                    //复制模板文件
                    int siteId = Convert.ToInt32(row["SiteID"]);
                    Cchannel siteObj = SiteConfigManager.createInstance().siteDataColls[siteId];
                    string sitePath= XNLPage.Context.Server.MapPath(siteObj.siteWebPath);
                    srcT_path=srcT_path.Replace("@",sitePath).Replace("//", "/");
                    nowT_path = nowT_path.Replace("@", sitePath).Replace("//", "/");
                    File.Copy(srcT_path, nowT_path, true);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch {}
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                transaction.Dispose();
            }
        }
        private void setDefaultTemplate(Dictionary<string, XNLParam> labelParams, XNLContext XNLPage)
        {
            int templateId;
            string templateIdStr = labelParams["templateid"].value.ToString();
            if (!Int32.TryParse(templateIdStr, out templateId))
            {
                throw (new Exception("模板编号类型不正确"));
            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            try
            {
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TemplateStyle,SiteID,IsDefault,TemplateProjectID from SN_Template where TemplateID=" + templateId);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = transaction;
                DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                DataRowCollection dataRows = dt.Rows;
                if (dataRows.Count > 0)
                {
                    DataRow row = dataRows[0];
                    int IsDefault = Convert.ToInt32(row["IsDefault"]);
                    if(IsDefault==0)
                    {
                        int templateType = Convert.ToInt32(row["TemplateStyle"]);
                        int SiteID = Convert.ToInt32(row["SiteID"]);
                        int projectId = Convert.ToInt32(row["TemplateProjectID"]);
                        dbCmd.CommandText = "update SN_Template set [IsDefault]=0 where TemplateStyle=" + templateType + " and [SiteID]=" + SiteID + " and TemplateProjectID=" + projectId;
                        dbCmd.ExecuteNonQuery();
                        dbCmd.CommandText = "update SN_Template set [IsDefault]=1 where TemplateID=" + templateId;
                        dbCmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch {}
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                transaction.Dispose();
            }
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