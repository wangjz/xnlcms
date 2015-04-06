using System;
using System.Collections.Generic;
using System.Web;
namespace COM.SingNo.CMS.Core
{
    //public enum AccessType { Static, Dynamic, PseudoStatic };
    //public  interface IBasePage 
    //{
    //    Cchannel curChannel { get; set; }//当前页所属节点
    //    bool hasJquery { get; set; } //是否当前模板包含jquery
    //    string pagePath {get; set; } //当前页地址
    //    AccessType accessType{get;set;}
    //    HttpContext Context { get; }
    //    int contentId { get; set; }
    //    Dictionary<string, string> pageAttriableColls { get; }
    //    Dictionary<string, string> myTagColls{get;set;}
    //    void addPageAttriable(string key, string value);
    //    int templateProjectId { get; set; }
    //    void addMyTag(string tagName, string tagStr);
    //    Dictionary<string, object> Items { get; set; }
    //    //string charset{get;set;}
    //}
   public class ParseInfo
    {
        public string fileExtName { get; set; }
        public string templateContent { get; set; }
        public string charset { get; set; }
        public string pageName { get; set; }
        public string createPath { get; set; }
        public string pageMsg { get; set; }
        public string pageStyle { get; set; }
        public Cchannel curChannel { get; set; }
        public WebContext XNLPage { get; set; }
        public int contentId;
        public string pagePath;
        public Dictionary<string, string> pageRequestColls;
        public Dictionary<string, string> myTagColls;
        public System.Security.Principal.WindowsIdentity windowsIdentity;
        public void addMyTag(string tagName, string tagStr)
        {
            if (myTagColls == null) myTagColls = new Dictionary<string, string>();
            if (!myTagColls.ContainsKey(tagName))
            {
                myTagColls.Add(tagName, tagStr);
            }
        }
    }
}
