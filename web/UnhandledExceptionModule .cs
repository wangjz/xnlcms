using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
    /**//// <summary>
    /// Summary description for UnhandledExceptionModule
    /// </summary>
    public class UnhandledExceptionModule : IHttpModule
    {
        static object _initLock = new object();
        static bool _initialized = false;

        public UnhandledExceptionModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        void OnUnhandledException(object o, UnhandledExceptionEventArgs e)
        {
            //Do some thing you wish to do when the Unhandled Exception raised.
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(SystemConfig.systemDir+"log.txt",System.IO.FileMode.Append, System.IO.FileAccess.Write))
                 {
                    using (System.IO.StreamWriter w = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
                     {
                        w.WriteLine(e.ExceptionObject);
                    }
                }
            }
            catch
            {
            }
        }
       // IHttpModule Members
            #region IHttpModule Members

        public void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Init(HttpApplication context)
        {
            // Do this one time for each AppDomain.
            lock (_initLock)
            {
                if (!_initialized)
                {
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
                    _initialized = true;
                }
            }
        }

        #endregion
    }
}

