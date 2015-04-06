using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
    // {@Projectid} {@projectname}
   public class Site:IXNLTag<WebContext>
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

       public string main(XNLTagStruct tagStruct, WebContext xnlContext) //where T:WebContext
       {
           throw new NotImplementedException();
       }

       #endregion

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
