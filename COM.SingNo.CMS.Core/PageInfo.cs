using System;
using System.Collections.Generic;
using System.Text;
namespace COM.SingNo.CMS.Core
{
   public class PageInfo
    {
       private Dictionary<string, string> _pageColls;
       public Dictionary<string, string> pageColls
       {
           get
           {
               return _pageColls;
           }
       }
       private Dictionary<string, string> _myTagColls;
       public Dictionary<string, string> myTagColls
       {
           get
           {
               return _myTagColls;
           }
       }
      public void addPageInfo(string key,string pageNum)
       {
           if (_pageColls == null) _pageColls = new Dictionary<string, string>();
           if (_pageColls.ContainsKey(key))
           {
               _pageColls[key] = pageNum;
           }
           else
           {
               _pageColls.Add(key, pageNum);
           }
       }
      public void addMyTag(string tagName, string tagStr)
      {
          if (_myTagColls == null) _myTagColls = new Dictionary<string, string>();
          if (!_myTagColls.ContainsKey(tagName))
          {
              _myTagColls.Add(tagName, tagStr);
          }
      }
      public string parseStr { get; set; }
      public PageInfo(string str)
      {
          parseStr = str;
      }
    }
}
