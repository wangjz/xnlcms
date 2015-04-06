using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.DAL;
using System.Data;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
	public class AdminUser:IXNLTag<WebContext>
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
        public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            return "";
            /*
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminUser.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "AdminUser.Error");
            string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
            try
            {
                if (actionStr.Equals("addadmin"))
                {
                    addAdmin(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("modifyadminrole"))
                {
                    modifyAdminRole(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("deleteadmin"))
                {
                    deleteAdmin(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("lockadmin"))
                {
                    lockAdmin(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("unlockadmin"))
                {
                    unlockAdmin(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("createrole"))
                {
                    createRole(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("modifyadminpass"))
                {
                    modifyAdminPass(labelParams, labelContentStr, XNLPage);
                }
                else if(actionStr.Equals("modifyrole"))
                {
                    modifyRole(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("deleterole"))
                {
                    deleteRole(labelParams, labelContentStr, XNLPage);
                }
                else if(actionStr.Equals("modifyrole"))
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
            return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            */
        }
        #endregion
        private void addAdmin(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            string userName = labelParams["loginname"].value.ToString().ToLower();
            if(userName.Equals(""))
            {
                throw (new Exception("管理员登陆名不能为空。"));
            }

            string displayName = labelParams["displayname"].value.ToString().ToLower();
            if (displayName.Equals(""))
            {
                throw (new Exception("管理员显示名不能为空。"));
            }
            string passWord = labelParams["password"].value.ToString().Trim();
            if(passWord.Equals(""))
            {
                throw (new Exception("管理员密码不能为空。"));
            }
            else if (passWord.Length < 6)
            {
                throw (new Exception("密码长度必须大于等于6。"));
            }
            string confirmPass = labelParams["confirmpass"].value.ToString().Trim();
            if(!confirmPass.Equals(passWord))
            {
                throw (new Exception("确认密码与密码不一致。"));
            }
            string email = labelParams["email"].value.ToString().Trim();
            if(email.Equals(""))
            {
                throw (new Exception("管理员电子邮件地址不能为空。"));
            }
            else
            {
                string regex = "^\\w+[-+.\'\\w]*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regex);
                if(!reg.IsMatch(email))
                {
                    throw (new Exception("管理员电子邮件地址格式不正确。"));
                }
            }

            string question = labelParams["question"].value.ToString().Trim();
            if (question.Equals(""))
            {
                throw (new Exception("找回密码问题不能为空。"));
            }
            string answer = labelParams["answer"].value.ToString().Trim();
            if (answer.Equals(""))
            {
                throw (new Exception("找回密码答案不能为空。"));
            }
            DataHelper.ExecuteNonQuery("insert into sn_admin([LoginName],[Password],[DisplayName],[Question],[Answer],[Email],[RoleId])values(@LoginName,@Password,@DisplayName,@Question,@Answer,@Email,@RoleId)", labelParams);
        }
        private void createRole(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            string roleName = labelParams["rolename"].value.ToString();
            if (roleName.Trim().Equals(""))
            {
                throw (new Exception("角色名称不能为空。"));
            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran = dbConn.BeginTransaction();
            DbCommand cmd = DataHelper.GetSqlStringCommand("select ID from  SN_AdminRole where RoleName=@RoleName");
            DataHelper.SetParameterValue(cmd, labelParams);
            cmd.Connection = dbConn;
            cmd.Transaction = dbTran;
            object roleObj = DataHelper.ExecuteScalar(cmd);
            int roleId = Convert.IsDBNull(roleObj) ? 0 : Convert.ToInt32(roleObj);
            if (roleId > 0)
            {
                dbTran.Rollback();
                dbTran.Dispose();
                dbConn.Close();
                throw (new Exception("角色名称已存在。"));
            }
            try
            {
                labelParams["siterights"].value = UtilsCode.decodeHtml(labelParams["siterights"].value.ToString());
                cmd.CommandText = "insert into SN_AdminRole(RoleName,Descript,SysRights,SiteNodeRights,PluginsRights,OthersRights)values(@roleName,@desc,@systemRights,@siteRights,@pulginsRight,@otherRights)";
                cmd.Parameters.Clear();
                DataHelper.SetParameterValue(cmd, labelParams);
                DataHelper.ExecuteNonQuery(cmd);
                dbTran.Commit();
                dbTran.Dispose();
                dbConn.Close();
            }
            catch (Exception e)
            {
                dbTran.Rollback();
                dbTran.Dispose();
                dbConn.Close();
                throw (e);
            }
        }
       /// <summary>
       /// 删除管理员
       /// </summary>
       /// <param name="labelParams"></param>
       /// <param name="labelContentStr"></param>
       /// <param name="XNLPage"></param>
        private void deleteAdmin(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            Admin curAdmin = XNLPage.Context.Session["admin"] as Admin;
            string adminName = labelParams["loginname"].value.ToString();
            if(curAdmin.loginName.Equals(adminName))
            {
                throw (new Exception("当前管理员不能操作"));
            }
            DataHelper.ExecuteNonQuery("delete from sn_admin where loginName=@loginname",labelParams);
        }
        /// <summary>
        /// 锁定管理员
        /// </summary>
        /// <param name="labelParams"></param>
        /// <param name="labelContentStr"></param>
        /// <param name="XNLPage"></param>
        private void lockAdmin(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            Admin curAdmin = XNLPage.Context.Session["admin"] as Admin;
            string adminName = labelParams["loginname"].value.ToString();
            if (curAdmin.loginName.Equals(adminName))
            {
                throw (new Exception("当前管理员不能操作"));
            }
            DataHelper.ExecuteNonQuery("update sn_admin set IsLocked=1 where loginName=@loginname", labelParams);
        }
        /// <summary>
        /// 解锁管理员
        /// </summary>
        /// <param name="labelParams"></param>
        /// <param name="labelContentStr"></param>
        /// <param name="XNLPage"></param>
        private void unlockAdmin(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            Admin curAdmin = XNLPage.Context.Session["admin"] as Admin;
            string adminName = labelParams["loginname"].value.ToString();
            if (curAdmin.loginName.Equals(adminName))
            {
                throw (new Exception("当前管理员不能操作"));
            }
            DataHelper.ExecuteNonQuery("update sn_admin set IsLocked=0 where loginName=@loginname", labelParams);
        }
        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="labelParams"></param>
        /// <param name="labelContentStr"></param>
        /// <param name="XNLPage"></param>
        private void modifyAdmin(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {

        }
        private void modifyAdminPass(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            string passWord = labelParams["password"].value.ToString().Trim();
            if (passWord.Equals(""))
            {
                throw (new Exception("管理员新密码不能为空。"));
            }
            else if (passWord.Length < 6)
            {
                throw (new Exception("新密码长度必须大于等于6。"));
            }
            string confirmPass = labelParams["confirmpass"].value.ToString().Trim();
            if (!confirmPass.Equals(passWord))
            {
                throw (new Exception("确认密码与密码不一致。"));
            }
            DataHelper.ExecuteNonQuery("update  sn_admin set [Password]=@Password where [loginName]=@loginname", labelParams);
        }
        private void modifyAdminRole(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            DataHelper.ExecuteNonQuery("update sn_admin set [roleid]=@roleid where loginName=@loginName", labelParams);
        }
        private void deleteRole(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
            sqlColls.Add("1", "delete from sn_adminRole where id=@roleId");
            sqlColls.Add("2", "delete from sn_admin where roleId=@roleId");
            DbResultInfo resultInfo= DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
            if(!resultInfo.isSuccess)
            {
                throw (resultInfo.exception);
            }
        }
        private void modifyRole(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            labelParams["siterights"].value = UtilsCode.decodeHtml(labelParams["siterights"].value.ToString());
            DataHelper.ExecuteNonQuery("update sn_adminRole set Descript=@desc,SysRights=@systemRights,SiteNodeRights=@siteRights,PluginsRights=@pulginsRight,OthersRights=@otherRights where id=@roleId", labelParams);
        }

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
