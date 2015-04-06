using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using COM.SingNo.XNLCore;
using COM.SingNo.DAL;
using System.Data.Common;
using System.Data;
using COM.SingNo.CMS.Core;
using COM.SingNo.Common;
using System.IO;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  internal  class ManageUtil
    {
      /// <summary>
        /// 根据内容页命名规则得到内容页目录路径
      /// </summary>
        /// <param name="contentPageNameRule">内容页命名规则</param>
      /// <returns></returns>
      public static string getContentDirectoryByRule(string contentPageNameRule)
      {
          string pathStr="";
          int rootid = contentPageNameRule.IndexOf('@');
          if (rootid >= 0)  
          {
              contentPageNameRule = contentPageNameRule.Substring(rootid+1, contentPageNameRule.Length);
          }
          char [] split_arr={'/'};
          string[] path_arr = contentPageNameRule.Split(split_arr, StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < path_arr.Length; i++)
          {
              if (path_arr[i].IndexOf('.') < 0)
              {
                  pathStr += path_arr[i] + "/";
              }
          }
          return pathStr;
      }
      /// <summary>
      ///根据栏目页面命名规则得到内容页目录路径  ,非站点结点有效
      /// </summary>
      /// <param name="channelPageNameRule">栏目页面命名规则</param>
      /// <returns></returns>
      public static string getChannelDirectoryByRule(string channelPageNameRule)
      {

          string pathStr = "";
          int rootid = channelPageNameRule.IndexOf('@');
          if (rootid >= 0)
          {
              channelPageNameRule = channelPageNameRule.Substring(rootid+1, channelPageNameRule.Length);
          }
          char[] split_arr = { '/' };
          string[] path_arr = channelPageNameRule.Split(split_arr, StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < path_arr.Length; i++)
          {
              if (path_arr[i].IndexOf('.') < 0)
              {
                  pathStr += path_arr[i] + "/";
              }
          }
          return pathStr;
      }
      public static string getSiteConfig()
      {
          return "<!DOCTYPE SiteConfig [<!ENTITY xnlal \"{\"><!ENTITY xnlar \"}\">]><SiteConfig><BsaeConfig><title></title><ico></ico><CharSet>@CharSet</CharSet><Audit>@Audit</Audit><info></info><metaKeyword></metaKeyword><metaDesc></metaDesc><ChannelGroup>True</ChannelGroup></BsaeConfig><UploadConfig><Folder>upload</Folder><FileSaveStyle>Y/M</FileSaveStyle><FileRenameByTime>True</FileRenameByTime><ImageType maxSize=\"5\" sizeType=\"M\">gif,jpg,jpeg,bmp,png</ImageType><MediaType maxSize=\"20\" sizeType=\"M\">rm,rmvb,mp3,flv,wav,mid,midi,ra,avi,mpg,mpeg,asf,asx,wma,mov,swf</MediaType><DocType maxSize=\"5\" sizeType=\"M\">txt,doc,docx,ppt,pptx,xls,xlsx,xml,pdf,rar,zip,7z,gz</DocType></UploadConfig><GBConfig><Enabled>False</Enabled><Audit>True</Audit><NeedLogin>True</NeedLogin><VerifyCode>True</VerifyCode></GBConfig><CreateConfig><SiteErrorHandle>writeMsgNoSql</SiteErrorHandle><LabelErrorHandle>writeMsg</LabelErrorHandle><TemplateSaveType>0</TemplateSaveType><PreCompiled>False</PreCompiled></CreateConfig><WaterMarkConfig><Enabled>False</Enabled><Position>9</Position><alpha>50</alpha><ImageMinSize W=\"200\" H=\"200\"></ImageMinSize><WaterMarkType type=\"Image\"><ImageType><ImagePath></ImagePath></ImageType><TextType><Content><![CDATA[]]></Content><Font>Arial</Font><Size>12</Size></TextType></WaterMarkType></WaterMarkConfig><PowerByConfig><ViewType>link</ViewType><PowerByInfo><![CDATA[Powered by <a href=\"http://www.singno.com\" target=\"_blank\">SingNo CMS</a>]]></PowerByInfo></PowerByConfig></SiteConfig>";
      }
      public static string getChannelConfig()
      {
          return "<!DOCTYPE ChannelConfig [<!ENTITY xnlal \"{\"><!ENTITY xnlar \"}\">]><ChannelConfig><BaseConfig><PartPage type=\"Auto\"><WordNum>1500</WordNum></PartPage><Editor>xHtmlEditor</Editor><PrePageItem>50</PrePageItem><CountContentClick>False</CountContentClick><CountDownload>False</CountDownload><HitsOfHot>200</HitsOfHot><CanAddChannel>True</CanAddChannel><CanAddContent>True</CanAddContent><ContentGroup>True</ContentGroup><Tag>True</Tag><AutoSaveImage>False</AutoSaveImage><OpenType>_self</OpenType><ItemOpenType>_self</ItemOpenType><Info></Info><MetaKeyword></MetaKeyword><MetaDesc></MetaDesc><ShowOnMap>True</ShowOnMap><ShowOnPath>True</ShowOnPath></BaseConfig><CommentsConfig><Enabled>False</Enabled><Login>True</Login><Audit>True</Audit><SuccessMsg>评论成功</SuccessMsg><FailMsg>评论失败</FailMsg></CommentsConfig><CreateConfig><ContentChange>True</ContentChange><ChannelChange>True</ChannelChange></CreateConfig><ContributeConfig><Enabled>False</Enabled><Audit>True</Audit></ContributeConfig><PurviewConfig></PurviewConfig><CustomFieldConfig></CustomFieldConfig></ChannelConfig>";
      }
      public static int getCurSiteID(WebContext XNLPage)
      {
          if (HttpContext.Current.Session["siteNodeId"] == null)
          {
              XNLPage.Context.Response.Write("<script type=\"text/javascript\">top.location.href=\"" + XNLPage.curChannel.siteNode.siteWebPath + "/default.aspx\"</script>");
              XNLPage.Context.Response.End();
              return 0;
          }
          else
          {
              return Convert.ToInt32(HttpContext.Current.Session["siteNodeId"]);
          }
      }
      public static void setNodeModel(Cchannel theChannel, Dictionary<string, XNLParam> labelParams, DbTransaction transaction)
      {
          //查询模型表，得到模型表名
          DbCommand dbCmd = DataHelper.GetSqlStringCommand("select ModelName,TableName from SN_model where ModelID=@ModelID");
          dbCmd.Connection = transaction.Connection;
          dbCmd.Transaction = transaction;
          DataHelper.SetParameterValue(dbCmd, labelParams);
          DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
          string modelName = dt.Rows[0]["ModelName"].ToString();
          string tableName = dt.Rows[0]["TableName"].ToString();
         // theChannel.modelName = modelName;
         // theChannel.modelID = Convert.ToInt32(labelParams["modelid"].theValue);
         // theChannel.modelTabelName = tableName;
          labelParams.Add("modelname", new XNLParam( modelName));
          DbCommand dbCmd2 = DataHelper.GetSqlStringCommand("select FieldName from SN_ModelDescript where ModelName=@modelName");
          dbCmd2.Connection = transaction.Connection;
          dbCmd2.Transaction = transaction;
          DataHelper.SetParameterValue(dbCmd2, labelParams);
          dt = DataHelper.ExecuteDataTable(dbCmd2);
          foreach (DataRow row in dt.Rows)
          {
              //theChannel.addModelFieldToList(row[0].ToString());
          }
      }
      public static bool checkTemplateMatch(int templateStyle,int templateID,DbTransaction transaction)
      {
          string matchSql = string.Empty;
          if (templateStyle == 1)
          {
              matchSql = "select ChannelTemplateID from SN_TemplateMatch where  ChannelTemplateID=" + templateID;
          }
          else if (templateStyle == 2)
          {
              matchSql = "select ChannelTemplateID from SN_TemplateMatch where  ContentTemplateID=" + templateID;
          }
          DbCommand dbCmd = DataHelper.GetSqlStringCommand(matchSql);
          dbCmd.Connection = transaction.Connection;
          dbCmd.Transaction = transaction;
          object matchObj = DataHelper.ExecuteScalar(dbCmd);
          int matchID = Convert.ToInt32(Convert.IsDBNull(matchObj) ? 0 : matchObj);
          if (matchID > 0) return true;
          return false;
      }
      public static string getExtName(string pathStr)
      {
          int index = pathStr.IndexOf('.');
          if (index == -1) return "";
          string extName = pathStr.Substring(index);
          return extName;
      }
      public static string getTableXML(string tableName)
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
          sb.Append("<Root>");
          sb.Append("<Table Name=\"@tableName\">");
          sb.Append("<Field Name=\"Title\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"标题\" IndexId=\"1\" IsSystem=\"1\" Description=\"标题的名称\" HelpText=\"标题的名称\" IsVisible=\"1\" IsValidator=\"1\" isShowOnList=\"1\" InputType=\"Text\">");
          sb.Append("<InputTypeSet readonly=\"false\" style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"true\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"SubTitle\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"副标题\" IndexId=\"1\" IsSystem=\"1\" Description=\"副标题的名称\" HelpText=\"副标题的名称\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Text\" >");
          sb.Append("<InputTypeSet   readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"ImageUrl\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"标题图片\" IndexId=\"1\" IsSystem=\"1\" Description=\"标题的图片\" HelpText=\"标题的图片\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Image\">");
          sb.Append("<InputTypeSet   readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"Summary\" DataType=\"NText\" DataLength=\"16\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"信息简介\" IndexId=\"1\" IsSystem=\"1\" Description=\"信息的简介\" HelpText=\"信息的简介\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"TextArea\" >");
          sb.Append("<InputTypeSet  width=\"0\" height=\"0\" readonly=\"false\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"LinkUrl\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"外部链接\" IndexId=\"1\" IsSystem=\"1\" Description=\"设置点击信息链接后需要到达的地址，默认为空，代表进入本信息页\" HelpText=\"设置点击信息链接后需要到达的地址，默认为空，代表进入本信息页\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Text\" >");
          sb.Append("<InputTypeSet   readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"Author\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"作者\" IndexId=\"1\" IsSystem=\"1\" Description=\"此信息的作者\" HelpText=\"此信息的作者\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Text\">");
          sb.Append("<InputTypeSet  readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"Source\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"来源\" IndexId=\"1\" IsSystem=\"1\" Description=\"此信息的来源\" HelpText=\"此信息的来源\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Text\">");
          sb.Append("<InputTypeSet  readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"FileUrl\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"附件\" IndexId=\"1\" IsSystem=\"1\" Description=\"内容的附件\" HelpText=\"内容的附件\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"File\" >");
          sb.Append("<InputTypeSet  readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

         // sb.Append("<Field Name=\"IsRecommend\" DataType=\"Boolean\" DataLength=\"1\" DefaultValue=\"0\" IsNull=\"N\" DisplayName=\"推荐\" IndexId=\"1\" IsSystem=\"1\" Description=\"是否为推荐内容\" HelpText=\"是否为推荐内容\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"CheckBox\">");
          //sb.Append("<InputTypeSet  style=\"\" direction=\"Horizontal\" columns=\"1\"><options><option label=\"推荐\" checked=\"false\"><value>1</value></option></options></InputTypeSet>");
          //sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

         // sb.Append("<Field Name=\"IsHot\" DataType=\"Boolean\" DataLength=\"1\" DefaultValue=\"0\" IsNull=\"N\" DisplayName=\"热点\" IndexId=\"1\" IsSystem=\"1\" Description=\"是否为热点内容\" HelpText=\"是否为热点内容\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"CheckBox\">");
         // sb.Append("<InputTypeSet  style=\"\" direction=\"Horizontal\" columns=\"1\"><options><option label=\"热点\" checked=\"false\"><value>1</value></option></options></InputTypeSet>");
         // sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

         // sb.Append("<Field Name=\"IsColor\" DataType=\"Boolean\" DataLength=\"1\" DefaultValue=\"0\" IsNull=\"N\" DisplayName=\"醒目\" IndexId=\"1\" IsSystem=\"1\" Description=\"是否为醒目内容\" HelpText=\"是否为醒目内容\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"CheckBox\">");
        //  sb.Append("<InputTypeSet  style=\"\" direction=\"Horizontal\" columns=\"1\"><options><option label=\"醒目\" checked=\"false\"><value>1</value></option></options></InputTypeSet>");
        //  sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

         // sb.Append("<Field Name=\"IsTop\" DataType=\"Boolean\" DataLength=\"1\" DefaultValue=\"0\" IsNull=\"N\" DisplayName=\"置顶\" IndexId=\"1\" IsSystem=\"1\" Description=\"是否为置顶内容\" HelpText=\"是否为置顶内容\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"CheckBox\">");
        //  sb.Append("<InputTypeSet  style=\"\" direction=\"Horizontal\" columns=\"1\"><options><option label=\"置顶\" checked=\"false\"><value>1</value></option></options></InputTypeSet>");
        //  sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"Content\" DataType=\"NText\" DataLength=\"16\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"内容\" IndexId=\"1\" IsSystem=\"1\" Description=\"内容正文\" HelpText=\"内容正文\" IsVisible=\"1\" IsValidator=\"0\"  isShowOnList=\"0\" InputType=\"TextEditor\" >");
          sb.Append("<InputTypeSet  width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"KeyWord\" DataType=\"NVarChar\" DataLength=\"255\" DefaultValue=\"\" IsNull=\"N\" DisplayName=\"关键字\" IndexId=\"1\" IsSystem=\"1\" Description=\"内容的关键字\" HelpText=\"内容关键字\" IsVisible=\"1\" IsValidator=\"0\" isShowOnList=\"0\" InputType=\"Text\">");
          sb.Append("<InputTypeSet  readonly=\"false\" style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");

          sb.Append("<Field Name=\"AddDate\" DataType=\"DateTime\" DataLength=\"8\" DefaultValue=\"[Current]\" IsNull=\"N\" DisplayName=\"添加日期\" IndexId=\"1\" IsSystem=\"1\" Description=\"内容的添加日期\" HelpText=\"内容的添加日期\" IsVisible=\"1\" IsValidator=\"1\" isShowOnList=\"0\" InputType=\"DateTime\">");
          sb.Append("<InputTypeSet  readonly=\"false\"  style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>");
          sb.Append("<ValidatorSet required=\"true\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet></Field>");
          sb.Append("</Table>");
          sb.Append("</Root>");
          return sb.ToString();
      }
      public static string setItemDefault(string itemType, string defaultStr)
      {
          if (itemType == "DateTime")
          {
              if (defaultStr == "[Current]")
              {
                  return DateTime.Now.ToString();
              }
          }
          else if (itemType == "Date")
          {
              if (defaultStr == "[Current]")
              {
                  return DateTime.Now.ToShortDateString();
              }
          }
          return defaultStr;
      }
      public static string getCurAdminName()
      {
          return (HttpContext.Current.Session["admin"] as Admin).loginName;
      }
      public static string getModelName(int nodeid)
      {
         return ChannelConfigManager.createInstance().channelDataColls[nodeid].model.ModelName;
      }
      public static string checkNodeGroup(int nodeid)
      {
          return "true";
          //return ChannelConfigManager.createInstance().channelDataColls[nodeid].theChannelConfig.baseConfig.useChannelGroup.ToString();
      }
      public static string getUserTagContent(int siteId,string tagFilePath)
      {
            //由站点id得到站点路径
          string sitePath = (SiteConfigManager.createInstance().siteDataColls[siteId] as Cchannel).siteWebPath;
            tagFilePath = tagFilePath.Replace("@", sitePath);
            tagFilePath = HttpContext.Current.Server.MapPath("~" + tagFilePath);
            string contentStr = CMSUtils.loadTempleteByPath(tagFilePath, Encoding.UTF8);
            contentStr = UtilsCode.encodeHtmlAndXnl(contentStr);
            return contentStr;
      }
      public static void writeFile(string filePath, string charset,string fileContent)
      {
          Encoding encoder = Encoding.GetEncoding(charset);
          using (FileStream fs = File.Create(filePath))
          {
              byte[] b_info = encoder.GetBytes(fileContent);
              fs.Write(b_info, 0, b_info.Length);
          }
      }
    }
}
