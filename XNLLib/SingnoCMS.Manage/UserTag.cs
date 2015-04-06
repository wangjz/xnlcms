using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Web;
using System.Text.RegularExpressions;
using COM.SingNo.Common;
using COM.SingNo.DAL;
using System.Data;
using COM.SingNo.XNLCore;
using System.Data.Common;
using System.IO;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public class UserTag:IXNLTag<WebContext>
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
      public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
            /*
              labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
              MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "UserTag.Success");
              MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "UserTag.Error");
              string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
              try
              {
                  if (actionStr.Equals("add"))
                  {
                      addUserTag(labelParams, XNLPage);
                  }
                  else if (actionStr.Equals("modify"))
                  {
                      modifyUserTag(labelParams, XNLPage);
                  }
                  else if (actionStr.Equals("delete"))
                  {
                      deleteUserTag(labelParams, XNLPage);
                  }
                  else if (actionStr.Equals("copy"))
                  {
                      copyUserTag(labelParams, XNLPage);
                  }
                  else if (actionStr.Equals("loadcontent"))
                  {
                     return loadTagContent(labelParams, XNLPage);
                  }
                  labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
              }
              catch (Exception e)
              {
                  Dictionary<string, string> errorList = new Dictionary<string, string>();
                  errorList.Add("1", e.Message);
                  labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
              }
             return labelContentStr;
             */
            return "";
        }
      private void addUserTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            /*
              string tagName = labelParams["tagname"].value.ToString().Trim().ToLower();
              if(tagName.Equals(""))
              {
                  throw (new Exception("标签名称不能为空"));
              }
              //检查标签内容是否嵌套用户标签
              string tagContent = XNLPage.Context.Request.Form["tagContent"];
              if(Regex.IsMatch(tagContent,"<XNL\\.UserTag:[^<>\\.]+>",XNLBaseCommon.XNL_RegexOptions))
              {
                  throw (new Exception("用户标签不允许嵌套"));
              }
              //查找是否已有此标签名称
              DbConnection dbConn = DataHelper.CreateConnection();
              dbConn.Open();
              DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
              try
              {
                  Cchannel siteObj =ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)];
                  int siteId = siteObj.siteID;
                  DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TagID from SN_UserTag where TagName=@tagName and SiteId="+siteId);
                  dbCmd.Connection = dbConn;
                  dbCmd.Transaction = dbTran;
                  DataHelper.SetParameterValue(dbCmd, labelParams);
                  object idObj = dbCmd.ExecuteScalar();
                  int id = Convert.ToInt32((Convert.IsDBNull(idObj) ? 0 : idObj));
                  if(id>0)
                  {
                      throw (new Exception("此标签名称已存在"));
                  }
                  string filePath = "@/Template/UserTag/" + "UT_"+tagName+".ascx";
                  labelParams.Add("siteid", new XNLParam(XNLType.Int32, siteId));
                  labelParams.Add("filepath", new XNLParam(XNLType.String, filePath));
                  dbCmd.CommandText = "insert into SN_UserTag(TagName,TagDesc,SiteId,TagFilePath)values(@TagName,@TagDesc,@SiteId,@FilePath)";
                  dbCmd.Parameters.Clear();
                  DataHelper.SetParameterValue(dbCmd, labelParams);
                  dbCmd.ExecuteNonQuery();
                  //写文件
                 filePath = XNLPage.Context.Server.MapPath("~" + filePath.Replace("@", siteObj.siteWebPath));
                 using (FileStream fs = File.Create(filePath))
                  {
                      byte[] info = Encoding.UTF8.GetBytes(tagContent);
                      // Add some information to the file.
                      fs.Write(info, 0, info.Length);
                  }
                  dbTran.Commit();
         
              }
              catch (System.Exception ex)
              {
                  try
                  {
                      dbTran.Rollback();
                  }
                  catch{}
                  throw (ex);
              }
              finally
              {
                  dbConn.Close();
                  dbTran.Dispose();
              } */
        }
      /*
        private void modifyUserTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            string tagName = labelParams["tagname"].value.ToString().Trim().ToLower();
            if (tagName.Equals(""))
            {
                throw (new Exception("标签名称不能为空"));
            }
            //检查标签内容是否嵌套用户标签
            string tagContent = XNLPage.Context.Request.Form["tagContent"];
            if (Regex.IsMatch(tagContent, "<XNL\\.UserTag:[^<>\\.]+>", XNLBaseCommon.XNL_RegexOptions))
            {
                throw (new Exception("用户标签不允许嵌套"));
            }
            //查找是否已有此标签名称
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
            try
            {
                Cchannel siteObj = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)];
                int siteId = siteObj.siteID;
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TagID from SN_UserTag where TagName=@tagName and tagId<>@tagId and SiteId=" + siteId);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                object idObj = dbCmd.ExecuteScalar();
                int id = Convert.ToInt32((Convert.IsDBNull(idObj) ? 0 : idObj));
                if (id > 0)
                {
                    throw (new Exception("此标签名称已存在"));
                }
                dbCmd.CommandText = "select TagName from SN_UserTag where TagID=@TagID";
                dbCmd.Parameters.Clear();
                DataHelper.SetParameterValue(dbCmd, labelParams);
                string srcTagName = dbCmd.ExecuteScalar().ToString();
                string filePath = "@/Template/UserTag/UT_" + tagName + ".ascx";
                labelParams.Add("siteid", new XNLParam(XNLType.Int32, siteId));
                labelParams.Add("filepath", new XNLParam(XNLType.String, filePath));
                dbCmd.CommandText = "update SN_UserTag set TagName=@TagName,TagDesc=@TagDesc,TagFilePath=@FilePath where TagID=@TagID";
                dbCmd.Parameters.Clear();
                DataHelper.SetParameterValue(dbCmd, labelParams);
                dbCmd.ExecuteNonQuery();
                //写文件
              filePath = XNLPage.Context.Server.MapPath("~" + filePath.Replace("@", siteObj.siteWebPath));
               using (FileStream fs = File.Open(filePath,FileMode.Create))
                {
                    byte[] info = Encoding.UTF8.GetBytes(tagContent);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
               if (string.Compare(tagName, srcTagName, true) != 0)
                {
                    //删除原来的文件
                    string srcFilePath = "@/Template/UserTag/UT_" + srcTagName + ".ascx";
                    File.Delete(XNLPage.Context.Server.MapPath("~" + srcFilePath.Replace("@", siteObj.siteWebPath)));
                }
                dbTran.Commit();
            }
            catch (System.Exception ex)
            {
                try
                {
                    dbTran.Rollback();
                }
                catch { }
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                dbTran.Dispose();
            }
        }
        private void deleteUserTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
            try
            {
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TagFilePath from SN_UserTag where TagID=@TagID");
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                string filePath = dbCmd.ExecuteScalar().ToString();
                dbCmd.CommandText = "delete from SN_UserTag where TagID=@TagID";
                dbCmd.ExecuteNonQuery();
                dbTran.Commit();
                Cchannel siteObj = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)];
                filePath = XNLPage.Context.Server.MapPath("~" + filePath.Replace("@", siteObj.siteWebPath));
                File.Delete(filePath);  
            }
            catch (System.Exception ex)
            {
                try
                {
                    dbTran.Rollback();
                }
                catch { }
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                dbTran.Dispose();
            }
        }
        private void copyUserTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            string tagName = labelParams["tagname"].value.ToString().Trim().ToLower();
            if (tagName.Equals(""))
            {
                throw (new Exception("标签名称不能为空"));
            }
            //查找是否已有此标签名称
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
            try
            {
                Cchannel siteObj = ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)];
                int siteId = siteObj.siteID;
                DbCommand dbCmd = DataHelper.GetSqlStringCommand("select TagID from SN_UserTag where TagName=@tagName and SiteId=" + siteId);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                object idObj = dbCmd.ExecuteScalar();
                int id = Convert.ToInt32((Convert.IsDBNull(idObj) ? 0 : idObj));
                if (id > 0)
                {
                    throw (new Exception("此标签名称已存在"));
                }
                string filePath = "@/Template/UserTag/" + "UT_" + tagName + ".ascx";
                labelParams.Add("siteid", new XNLParam(XNLType.Int32, siteId));
                labelParams.Add("filepath", new XNLParam(XNLType.String, filePath));
                dbCmd.CommandText = "select TagDesc,TagFilePath from SN_UserTag where TagID=@TagID";
                dbCmd.Parameters.Clear();
                DataHelper.SetParameterValue(dbCmd, labelParams);
                DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                string TagDesc = dt.Rows[0]["TagDesc"].ToString();
                string srcFilePath = dt.Rows[0]["TagFilePath"].ToString();
                labelParams.Add("tagdesc", new XNLParam(XNLType.String, TagDesc));
                dbCmd.CommandText = "insert into SN_UserTag(TagName,TagDesc,SiteId,TagFilePath)values(@TagName,@TagDesc,@SiteId,@FilePath)";
                dbCmd.Parameters.Clear();
                DataHelper.SetParameterValue(dbCmd, labelParams);
                dbCmd.ExecuteNonQuery();
                //写文件
                srcFilePath = XNLPage.Context.Server.MapPath("~" + srcFilePath.Replace("@", siteObj.siteWebPath));
                filePath = XNLPage.Context.Server.MapPath("~" + filePath.Replace("@", siteObj.siteWebPath));
                File.Copy(srcFilePath, filePath);
                dbTran.Commit();
            }
            catch (System.Exception ex)
            {
                try
                {
                    dbTran.Rollback();
                }
                catch { }
                throw (ex);
            }
            finally
            {
                dbConn.Close();
                dbTran.Dispose();
            }
        }
        private string loadTagContent(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
        {
            //由站点id得到站点路径
            int siteID = Convert.ToInt32(labelParams["siteid"].value);
            string path = labelParams["filepath"].value.ToString();
            return ManageUtil.getUserTagContent(siteID,path);
        }
      */
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
