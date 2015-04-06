using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
namespace COM.SingNo.CMS.Core
{
    public class ValidatorInfos
    {
        public int errorsCount = 0;
        public XNLParams Params;
        public Dictionary<string, string> errorMsgList = new Dictionary<string, string>();
        public ValidatorInfos()
        {

        }

        public ValidatorInfos(XNLParams Params, Dictionary<string, string> errorMsgList)
        {
            this.Params = Params;
            if (errorMsgList != null)
            {
                this.errorsCount = errorMsgList.Count;
            }
        }

        public ValidatorInfos(XNLParams Params)
        {
            this.Params = Params;
        }

        public ValidatorInfos(Dictionary<string, string> errorMsgList)
        {
            this.errorMsgList = errorMsgList;
            if (errorMsgList != null)
            {
                this.errorsCount = errorMsgList.Count;
            }
        }
    }
}
