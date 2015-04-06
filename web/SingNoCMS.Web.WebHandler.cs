using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.IO;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
using System.Text;
using COM.SingNo.Common;
using System.Text.RegularExpressions;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
   public class WebHandler : IHttpHandler, IRequiresSessionState
    {
        public virtual void ProcessRequest(HttpContext context)
        {
            string _path = context.Request.FilePath.ToLower();
            if(_path.EndsWith("].aspx"))
            {
                Match match = Regex.Match(_path, "\\[(.*)\\]\\.aspx$");
                if(match.Success)
                {
                    _path = _path.Replace(match.Value, ".aspx");
                    context.RewritePath(_path, "", context.Request.QueryString.Count > 0 ? match.Groups[1].Value + "&" + context.Request.QueryString : match.Groups[1].Value);
                }
            }
                BasePage basePage = null;
                singlePage page = SinglePageConfigManager.createInstance().getSinglePage(_path);
                if (page != null)
                {
                    //try
                    //{
                    basePage = new BasePage();
                    basePage.Context = context;
                    basePage.pagePath = context.Request.RawUrl;
                    basePage.curChannel = page.theChannel;
                    context.Response.ContentEncoding = page.template.encoder;
                    basePage.templateProjectId = page.template.templateProjectId;
                    string templateFileStr = page.template.TemplateContent;
                    if (page.template.templateStyle == 2)
                    {
                        int cid;
                        if (Int32.TryParse(context.Request.QueryString["id"], out cid))
                        {
                            basePage.contentId = cid;
                        }
                        else
                        {
                            context.Response.Write("没有指定当前内容编号!");
                            context.Response.End();
                        }
                    }
                    //string pageStr = RegexParser.getInstance().parse(templateFileStr, basePage);
                    //context.Response.Write(pageStr);
                    //}
                    //catch (Exception e)
                    //{
                    //    context.Response.Write(e.Message);
                    //    context.Response.End();
                    //}
                }
                else
                {
                    BackgroundSiteConfigManage bgSiteManage = BackgroundSiteConfigManage.loadConfig(context.Request.PhysicalApplicationPath + "GlobalFiles\\config\\backGroupSite.config");
                    singlePage singlepage=null;
                    try
                    {
                       singlepage = bgSiteManage.bgSiteSinglePageDataColls[_path];
                    }
                    catch
                    {
                        context.Response.Write("找不到此页");
                        context.Response.End();
                    }
                    //try
                    //{
                        basePage = new BasePage();
                        basePage.Context = context;
                        basePage.pagePath = context.Request.RawUrl;
                        basePage.curChannel = singlepage.theChannel;
                        //basePage.templateProjectId = page.template.templateProjectId;
                        string templateFileStr = singlepage.template.TemplateContent;
                        string pageStr = RegexParser<WebContext>.getInstance().parse(templateFileStr, basePage);
                        context.Response.Write(pageStr);
                    //}
                    //catch (Exception e)
                    //{
                    //    context.Response.Write(e.Message);
                    //    context.Response.End();
                    //}
                }
                if (basePage != null)
                {
                    basePage.Dispose();
                    basePage = null;
                }
        }
        public Boolean IsReusable
        {
            get { return true; }
        }
    }
}
