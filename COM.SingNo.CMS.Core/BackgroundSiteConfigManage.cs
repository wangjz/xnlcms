using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using COM.SingNo.XNLCore;
namespace COM.SingNo.CMS.Core
{
  public  class BackgroundSiteConfigManage
    {
      private volatile static BackgroundSiteConfigManage _instance;
      private static object syncRoot = new Object(); 
      public Cchannel backGroupSite; //后台站点
      public Dictionary<string, singlePage> bgSiteSinglePageDataColls; //存储后台单页集合
      private BackgroundSiteConfigManage(string configFile)
      {
              XmlDocument siteXml = new XmlDocument();
              siteXml.Load(configFile);
              string bgSitepath = siteXml.SelectSingleNode("/site/@rootPath").Value; //后台站点路径
              backGroupSite = new Cchannel();
              backGroupSite.isSite = true;
              backGroupSite.nodeID = 0;
             // string bgSiteWebPath = HttpContext.Current.Server.MapPath(bgSitepath).Replace(HttpContext.Current.Request.PhysicalApplicationPath, "/");// bgSitepath.Replace("~", HttpContext.Current.Request.ApplicationPath); ;
              string bgSiteWebPath = "/"+bgSitepath;
              backGroupSite.siteWebPath = bgSiteWebPath;
              backGroupSite.theSiteConfig = new siteConfig();
              backGroupSite.theSiteConfig.baseConfig.TemplateSaveType = 0;
              bgSiteSinglePageDataColls = new Dictionary<string, singlePage>();
              LoadConfig(siteXml);
      }

      public static BackgroundSiteConfigManage loadConfig(string configFile)
      {
          if (_instance == null)
          {
              lock (syncRoot)
              {
                  if (_instance == null)
                  {
                      _instance = new BackgroundSiteConfigManage(configFile);
                  }
              }
          } 
          return _instance;
      }

      private void LoadConfig(XmlDocument siteXml)
      {
          string siteName = siteXml.SelectSingleNode("/site/@name").Value;  //站点名称
          string templeteProjectName = siteXml.SelectSingleNode("/site/@template").Value; //站点模板方案名称
          backGroupSite.nodeName = siteName;
          backGroupSite.nodeIndexName = siteXml.SelectSingleNode("/site/templates/template[@name='" + templeteProjectName + "']/@folder").Value;
          XmlNodeList siteNS = siteXml.SelectNodes("/site/templates/template[@name='" + templeteProjectName + "']/file");
          int templateId = 0;
          foreach (XmlNode j in siteNS)
          {
              string path = j.Attributes["pagePath"].Value.ToLower();
              if (path.Substring(0, 1).Equals("@"))
              {
                  path = path.Replace("@",backGroupSite.siteWebPath).ToLower();
              }
              string temFileName = j.Attributes["name"].Value.ToLower();
              string temFilePath = j.Attributes["path"].Value;
              Template bgPageTemplate = new Template();
              bgPageTemplate.templateStyle = 3;
              bgPageTemplate.theSite = backGroupSite;
              bgPageTemplate.encoder = backGroupSite.theSiteConfig.baseConfig.encoder;
              bgPageTemplate.TemplatePath = temFilePath.Replace("@", HttpContext.Current.Server.MapPath("~" + backGroupSite.siteWebPath));
              //backGroupSite.addTemplate(templateId, bgPageTemplate);
              singlePage singlepageobj = new singlePage();
              singlepageobj.theChannel = backGroupSite;
             // singlepageobj.templateName = temFileName;
              singlepageobj.template = bgPageTemplate;
              //singlepageobj.template = backGroupSite.templateColls[templateId];
              bgSiteSinglePageDataColls.Add(path, singlepageobj);
              templateId++;
          }
      }

      public void reSet()
      {
          lock (syncRoot)
          {
              _instance = null;
          }
      }
    }
}
