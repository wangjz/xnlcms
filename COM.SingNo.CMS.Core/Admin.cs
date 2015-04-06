using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.DAL;
using System.Data.Common;
using COM.SingNo.XNLCore;
using System.Data;
namespace COM.SingNo.CMS.Core
{
    /// <summary>
    /// 管理员
    /// </summary>
  public class Admin
  {
      /// <summary>
      /// 管理员登陆名称
      /// </summary>
      public string loginName;
      /// <summary>
      /// 管理员显示名称
      /// </summary>
      public string displayName;
      /// <summary>
      /// 管理员角色
      /// </summary>
      public AdminRole role;
      public bool IsLocked;
      /// <summary>
      /// 返回1，成功  返回0没找到 返回-1没找到角色,-2被锁定
      /// </summary>
      /// <param name="adminName"></param>
      /// <returns></returns>
      public int fillInfoByName(string adminName,string passWord)
      {
          Dictionary<string, XNLParam> _params = new Dictionary<string, XNLParam>(2);
          _params.Add("user", new XNLParam( XNLType.String, adminName));
          _params.Add("pass", new XNLParam( XNLType.String, passWord));
          DbDataReader dbReader = DataHelper.ExecuteReader("select [LoginName],[DisplayName],[IsLocked],[RoleId] from sn_admin where [LoginName]=@User and password=@pass", _params);
          if(dbReader.HasRows)
          {
              dbReader.Read();
              this.loginName = adminName;
              this.displayName =Convert.ToString(dbReader["DisplayName"]);
              if (this.displayName.Trim().Equals("")) this.displayName = this.loginName;
              this.IsLocked = Convert.ToInt32(dbReader["IsLocked"])==0?false:true;
              int roleId = Convert.ToInt32(dbReader["RoleId"]);
              dbReader.Close();
              if (IsLocked) return -2;
              AdminRole role = new AdminRole();
              if (!role.fileInfoById(roleId)) return -1;
              this.role = role;
              return 1;
          }
          dbReader.Close();
          return 0;
      }

      public void loginIn()
      {
          System.Web.HttpContext.Current.Session["admin"] = this;
      }

      public void loginOut()
      {
          System.Web.HttpContext.Current.Session["admin"] = null;
          System.Web.HttpContext.Current.Session.Remove("admin");
          System.Web.HttpContext.Current.Session.Remove("siteNodeId");
      }
      public static bool loginCheck()
      {
          if(System.Web.HttpContext.Current.Session["admin"]==null)
          {
              return false;
          }
          return true;
      }
    }
}
