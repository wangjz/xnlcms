using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COM.SingNo.Common;
using System.Data;
using COM.SingNo.DAL;
namespace COM.SingNo.Web
{
    public partial class testLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = DataHelper.ExecuteDataTable("select * from sn_log");
            foreach (DataRow r in dt.Rows)
            {
                Response.Write(r["log_msg"].ToString() + "</br>");
            }
        }
    }
}
