using System;
using System.Collections.Generic;
using System.Web;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
    public class BasePage:WebContext,IDisposable
    {
        #region IBasePage 成员
       // private Cchannel _curChannel;
        //private bool _hasJquery;
        //private string _pagePath;
        //private AccessType _accessType;
        //private HttpContext _context;
        //private int _contentId;
        //public string charset{get;set;}
       // public Dictionary<string, object> Items { get; set; }
       // public Dictionary<string, string> myTagColls{get;set;}
        /// <summary>
        /// 当前页所属节点
        /// </summary>
        //public Cchannel curChannel
        //{
        //    get
        //    {
        //        return _curChannel;
        //    }
        //    set
        //    {
        //        _curChannel = value;
        //    }
        //}
        /// <summary>
        /// 模板方案id
        /// </summary>
        //public int templateProjectId { get; set; }
        /// <summary>
        /// 是否当前模板包含jquery js文件
        /// </summary>
        //public bool hasJquery
        //{
        //    get
        //    {
        //        return _hasJquery;
        //    }
        //    set
        //    {
        //        _hasJquery = value;
        //    }
        //}
        /// <summary>
        /// 当前页地址
        /// </summary>
        //public string pagePath
        //{
        //    get
        //    {
        //        return _pagePath;
        //    }
        //    set
        //    {
        //        _pagePath = value;
        //    }
        //}
        /// <summary>
        /// 访问类型
        /// </summary>
        //public AccessType accessType
        //{
        //    get
        //    {
        //        return _accessType;
        //    }
        //    set
        //    {
        //        _accessType = value;
        //    }
        //}
        /// <summary>
        /// httpContext上下文
        /// </summary>
        //public HttpContext Context
        //{
        //    get { return _context; }
        //    set
        //    {
        //        _context = value;
        //    }
        //}
        /// <summary>
        /// 当前内容id,内容页时使用
        /// </summary>
        //public int contentId
        //{
        //    get
        //    {
        //        return _contentId;
        //    }
        //    set
        //    {
        //        _contentId = value;
        //    }
        //}
        //private Dictionary<string, string> _pageAttriableColls;
        /// <summary>
        /// 当前页属性集合
        /// </summary>
        //public Dictionary<string, string> pageAttriableColls
        //{
        //    get
        //    {
        //        return _pageAttriableColls;
        //    }
        //}
        public BasePage()
        {
            //_pagePath = "";
            //_contentId = 0;
            //_hasJquery = false;
            //_accessType = AccessType.Dynamic;
        }
        /// <summary>
        /// 添加当前页属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //public void addPageAttriable(string key,string value)
        //{
        //    if (_pageAttriableColls == null) _pageAttriableColls = new Dictionary<string, string>();
        //    try
        //    {
        //        _pageAttriableColls.Add(key, value);
        //    }
        //    catch
        //    {
        //        _pageAttriableColls[key] = value;
        //    }
        //}
        //public void addMyTag(string tagName, string tagStr)
        //{
        //    if (myTagColls == null) myTagColls = new Dictionary<string, string>();
        //    if (!myTagColls.ContainsKey(tagName))
        //    {
        //        myTagColls.Add(tagName, tagStr);
        //    }
        //}
        #endregion
        #region IDisposable 成员

        public void Dispose()
        {
            //_pagePath = null;
            //_pageAttriableColls = null;
            //myTagColls = null;
            //Items = null;
        }

        #endregion
    }
}
