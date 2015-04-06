using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.Web
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    public class CAPTCHA : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            VerifyCode.WriteVerifyCode(SystemConfig.verifyCode_Length, SystemConfig.verifyCode_FontSize, SystemConfig.verifyCode_StringMode, SystemConfig.verifyCode_FontColor, SystemConfig.verifyCode_BgColor);
        }
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
