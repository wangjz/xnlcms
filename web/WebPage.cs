using System;
using System.Collections.Generic;
using System.Web;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
using System.Text;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
    public partial class WebPage : System.Web.UI.Page
    {
        public int nodeId;
        public int projectId;
        public int pageStyle;
        public string template;
        public Dictionary<string, string> myTagColls;
        public BasePage XNLPage=new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetExpires(DateTime.Now.AddMinutes(300));
            //Response.Cache.SetCacheability(HttpCacheability.Public);
            XNLPage.templateProjectId = projectId;
            XNLPage.Context = this.Context;
            XNLPage.curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
            if(pageStyle==2)
            {
                if(Request.QueryString["id"]==null)
                {
                    //出错处理
                }
                else
                {
                    int id;
                    if(int.TryParse(Request.QueryString["id"],out id))
                    {
                        XNLPage.contentId = id;
                    }
                    else
                    {
                        //出错处理
                    }
                }
            }
            //ParseEngine.Parse(template, XNLPage);
            XNLPage.Dispose();
        }
        protected void addMyTag(string tagName,string tagContent)
        {
            XNLContext.setCustomTag(XNLPage, tagName, tagContent.Replace("<XNL۞script>", "</script>"));
            //XNLPage.addCustomTag(tagName, tagContent.Replace("<XNL۞script>", "</script>"));
        }
        protected void setTemplate(string templateStr)
        {
            template = templateStr.Replace("<XNL۞script>", "</script>");
        }
       
        protected void setContentField(string fields)
        {
            XNLContext.setGlobalAttriable(XNLPage, "$_cf_$", fields);
            //XNLPage.addGlobalAttriable("$_cf_$", fields);
        }
    }
}
