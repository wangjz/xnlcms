using System;
using System.Collections.Generic;
using System.Web;
using COM.SingNo.XNLCore;
namespace COM.SingNo.CMS.Core
{
    public enum AccessType { Static, Dynamic, PseudoStatic };
    public class WebContext : XNLContext
    {
        public Cchannel curChannel { get; set; }//当前页所属节点
        public bool hasJquery { get; set; } //是否当前模板包含jquery
        public string pagePath { get; set; } //当前页地址
        public AccessType accessType { get; set; }
        public HttpContext Context { get; set; }
        public int contentId { get; set; }
        public int templateProjectId { get; set; }
    }
}
