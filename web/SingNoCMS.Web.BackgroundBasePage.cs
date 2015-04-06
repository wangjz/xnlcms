using System;
using System.Collections.Generic;
using System.Web;
using COM.SingNo.Common;
using System.Configuration;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
    public partial class BackgroundBasePage : System.Web.UI.Page
    {
        public BackgroundBasePage()
        {
            Init += new EventHandler(Page_Init);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            string _path = Request.FilePath.ToLower();
            BackgroundSiteConfigManage bgSiteManage = BackgroundSiteConfigManage.loadConfig(Request.PhysicalApplicationPath + "GlobalFiles\\config\\backGroupSite.config");
            singlePage singlepage = null;
            try
            {
                singlepage = bgSiteManage.bgSiteSinglePageDataColls[_path];
            }
            catch
            {
                Response.Write("找不到此页");
                Response.End();
            }
            //try
            //{
            BasePage basePage = new BasePage();
            basePage.Context = this.Context;
            basePage.pagePath = Request.RawUrl;
            basePage.curChannel = singlepage.theChannel;
            string templateFileStr = singlepage.template.TemplateContent;
            string pageStr =ParserEngine<WebContext>.xnlParser.parse(templateFileStr, basePage);
            Response.Write(pageStr);
            //}
            //catch (Exception e)
            //{
            //    context.Response.Write(e.Message);
            //    context.Response.End();
            //}
            //_pagePath = Request.RawUrl;
            //string _path = Request.FilePath.ToLower();
            //BackgroundSiteConfigManage bgSiteManage = BackgroundSiteConfigManage.loadConfig(Request.PhysicalApplicationPath + "GlobalFiles\\config\\backGroupSite.config");
            //try
            //{
            //    singlePage singlepage = bgSiteManage.bgSiteSinglePageDataColls[_path];
            //    _curChannel = singlepage.theChannel;
            //    string templateFileStr = singlepage.template.TemplateContent;
            //    pageStr = XNLParseEngine.Parse(templateFileStr, true, this);
            //    Response.Write(pageStr);
            //}
            //catch
            //{
            //    Response.Write("没有找到此页面的模板文件。");
            //    Response.End();
            //}            
        }
    }
}
