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
using LitJson;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class Content:IXNLTag<WebContext>
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
            return "";
           /*
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Content.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Content.Error");
            string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
            try
            {
                if (actionStr.Equals("add"))
                {
                     add(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("modify"))
                {
                    modify(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("recycle"))
                {
                    deleteToRecycle(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("delete"))
                {
                    delete(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("restore"))
                {
                    restore(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("attribute"))
                {
                    setAttribute(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("audit"))
                {
                    setAuditPass(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("addgroup"))
                {
                    addGroup(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("deletegroup"))
                {
                    deleteGroup(labelParams, XNLPage);
                }
                else if (actionStr.Equals("modifygroup"))
                {
                    
                }
                else if(actionStr.Equals("deletegroupinfo"))
                {
                    deleteGroupInfo(labelParams, XNLPage);
                }
                else if (actionStr.Equals("addtag"))
                {
                    addTag(labelParams, labelContentStr, XNLPage);
                }
                else if (actionStr.Equals("deletetag"))
                {
                    deleteTag(labelParams, XNLPage);
                }
                else if (actionStr.Equals("modifytag"))
                {
                    modifyTag(labelParams, XNLPage);
                }
                else if (actionStr.Equals("deletetaginfo"))
                {
                    deleteTagInfo(labelParams, XNLPage);
                }
                else
                {
                    throw new Exception("controler error");
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
       /*
       private void add(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
          //得到模型
           int nodeId =Convert.ToInt32(labelParams["nodeid"].value);
           Cchannel curChannel=ChannelConfigManager.createInstance().channelDataColls[nodeId];
           if(!curChannel.theChannelConfig.baseConfig.canAddContent)
           {
               throw (new Exception("此栏目不允许添加内容"));
           }
           DataModel curModel = curChannel.model;
           string modelName=curModel.ModelName;
           //得到模型描述信息
           DataTable desDt = DataHelper.ExecuteDataTable("select FieldName,DataType,DefaultValue,IsVisible,InputType from SN_ModelDescript where ModelName='" + modelName + "'");
           bool isTitleShow = true;
           StringBuilder fieldSb = new StringBuilder();
           StringBuilder fieldParamSb = new StringBuilder();
           //得到所有字段
           foreach (DataRow row in desDt.Rows)
           {
               bool isVisible = Convert.ToInt32(row["isVisible"]) == 1 ? true : false;
               string DataType = Convert.ToString(row["DataType"]);
               string DefaultValue = Convert.ToString(row["DefaultValue"]);
               string fieldName = Convert.ToString(row["FieldName"]);
               string lowFieldName = fieldName.ToLower();
               string InputType = Convert.ToString(row["InputType"]);
               if (isVisible)
               {
                   switch (DataType)
                   {
                       case "NVarChar":
                       case "NText":
                           string v =(XNLPage.Context.Request.Form[fieldName] == null ? "" : XNLPage.Context.Request.Form[fieldName]);
                           if (!InputType.Equals("TextEditor"))
                           {
                               v = UtilsCode.encodeHtmlAndXnl(v);
                           }
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.String, v));
                           break;
                       case "Integer":
                       case "Boolean":
                           int v1 =Convert.ToInt32(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.Int32, v1));
                           break;
                       case "Decimal":
                           decimal v2 = Convert.ToDecimal(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam(XNLType.Decimal, v2));
                           break;
                       case "DateTime":
                           var tmpType = DataType;
                           if (InputType.Equals("Date")) tmpType = InputType;
                           string d = ManageUtil.setItemDefault(DataType, DefaultValue);
                           DateTime t;
                           if (!DateTime.TryParse(XNLPage.Context.Request.Form[fieldName] == null ? d : XNLPage.Context.Request.Form[fieldName], out t))
                           {
                               t = Convert.ToDateTime(d);
                           }
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.DateTime, t));
                           break;
                   }
               }
               else
               {
                   if (lowFieldName.Equals("title")) isTitleShow = false;
                   switch (DataType)
                   {
                       case "NVarChar":
                       case "NText":
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.String, DefaultValue));
                           break;
                       case "Integer":
                       case "Boolean":
                           int v1 = Convert.ToInt32( DefaultValue);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.Int32, v1));
                           break;
                       case "Decimal":
                           decimal v2 = Convert.ToDecimal(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam(XNLType.Decimal, v2));
                           break;
                       case "DateTime":
                           var tmpType = DataType;
                           if (InputType.Equals("Date")) tmpType = InputType;
                           string d = ManageUtil.setItemDefault(InputType, DefaultValue);
                           DateTime t = Convert.ToDateTime(d);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.DateTime, t));
                           break;
                   }
               }
               fieldSb.Append("," + fieldName);
               fieldParamSb.Append(",@" + fieldName);
           }
           //设置标题属性
           int boldStr = (XNLPage.Context.Request.Form["titleStyle_bold"] == null||XNLPage.Context.Request.Form["titleStyle_bold"].Equals("")) ? 0 :Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_bold"]);
           int italicStr = (XNLPage.Context.Request.Form["titleStyle_italic"] == null || XNLPage.Context.Request.Form["titleStyle_italic"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_italic"]);
           int uStr = (XNLPage.Context.Request.Form["titleStyle_u"] == null||XNLPage.Context.Request.Form["titleStyle_u"].Equals("")) ? 0 :Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_u"]);
           string colorStr = (XNLPage.Context.Request.Form["titleStyle_color"] == null||XNLPage.Context.Request.Form["titleStyle_color"].Equals(""))? "#000000" : XNLPage.Context.Request.Form["titleStyle_color"];
           labelParams.Add("bold", new XNLParam(XNLType.Int32, boldStr));
           labelParams.Add("italic", new XNLParam(XNLType.Int32, italicStr));
           labelParams.Add("underline", new XNLParam(XNLType.Int32, uStr));
           labelParams.Add("titlecolor", new XNLParam(XNLType.String, colorStr));
           labelParams.Add("pinyintitle", new XNLParam(XNLType.String, ""));
           labelParams.Add("pytitle", new XNLParam( XNLType.String, ""));
           if (isTitleShow)
           {
               string title = XNLPage.Context.Request.Form["title"];
               //设置pinyintitle ,pytitle
               string pinYinTitle = UtilsCode.CHS2PinYin(title, "", true);
               string pyTitle = UtilsCode.CHS2PY(title, "", true);
               labelParams["pinyintitle"].value = pinYinTitle;
               labelParams["pytitle"].value = pyTitle;
           }
           //设置状态
          // labelParams["nodeid"].dbType = DbType.Int32;
           labelParams["nodeid"].type = XNLType.Int32;
           if (labelParams["state"].value.ToString().Equals(""))
           {
               labelParams["state"].value = 0;
           }
          // labelParams["state"].dbType = DbType.Int32;
           labelParams["state"].type = XNLType.Int32;
           //设置输入用户
           string adminUser=ManageUtil.getCurAdminName();
           labelParams.Add("inputuser", new XNLParam(XNLType.String, adminUser));
           labelParams.Add("lastedituser", new XNLParam(XNLType.String, adminUser));
           Cchannel channelNode = ChannelConfigManager.createInstance().channelDataColls[nodeId];
           ChannelBaseConfig baseConfig = channelNode.theChannelConfig.baseConfig;
           int pageType = 0;
           int pageWords = 1500;
           switch (baseConfig.partPage)
           {
               case ChannelBaseConfig.PartPage.No:
                   pageType = 0;
                   break;
               case ChannelBaseConfig.PartPage.Auto:
                   pageType = 1;
                   break;
               case ChannelBaseConfig.PartPage.Manual:
                   pageType = 2;
                   pageWords = baseConfig.autoWordNums;
                   break;
           }
           
           //设置是否允许评论isComment,pageType,pageWords
           int isComment = 0; ;
           if (XNLPage.Context.Request.Form["isComment"] == null)
           {
               isComment = (channelNode.theChannelConfig.commentsConfig.isContentComments ? 1 : 0);
           }
           else
           {
               if (XNLPage.Context.Request.Form["isComment"].Equals("1"))
               {
                   isComment = 1;
               }
           }
           pageType = (XNLPage.Context.Request.Form["pageType"] == null || XNLPage.Context.Request.Form["pageType"].Equals("")) ? pageType : Convert.ToInt32(XNLPage.Context.Request.Form["pageType"]);
           pageWords = (XNLPage.Context.Request.Form["pageWords"] == null || XNLPage.Context.Request.Form["pageWords"].Equals("")) ? pageWords : Convert.ToInt32(XNLPage.Context.Request.Form["pageWords"]);
           int hits = (XNLPage.Context.Request.Form["Hits"] == null || XNLPage.Context.Request.Form["Hits"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["Hits"]);
           int IsRecommend = (XNLPage.Context.Request.Form["IsRecommend"] == null || XNLPage.Context.Request.Form["IsRecommend"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsRecommend"]);
           int IsHot = (XNLPage.Context.Request.Form["IsHot"] == null || XNLPage.Context.Request.Form["IsHot"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsHot"]);
           int IsColor = (XNLPage.Context.Request.Form["IsColor"] == null || XNLPage.Context.Request.Form["IsColor"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsColor"]);
           int IsTop = (XNLPage.Context.Request.Form["IsTop"] == null||XNLPage.Context.Request.Form["IsTop"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsTop"]);
           labelParams.Add("isrecommend", new XNLParam(XNLType.Int32, IsRecommend));
           labelParams.Add("ishot", new XNLParam(XNLType.Int32, IsHot));
           labelParams.Add("iscolor", new XNLParam(XNLType.Int32, IsColor));
           labelParams.Add("istop", new XNLParam(XNLType.Int32, IsTop));
           labelParams.Add("iscomment", new XNLParam(XNLType.Int32, isComment));
           labelParams.Add("pagetype", new XNLParam(XNLType.Int32, pageType));
           labelParams.Add("pagewords", new XNLParam(XNLType.Int32, pageWords));
           labelParams.Add("hits", new XNLParam(XNLType.Int32, hits));
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction transaction = dbConn.BeginTransaction();
           try
           {
               //设置indexid.
               //DbCommand dbIndexCmd = DataHelper.GetSqlStringCommand("select max(IndexID) from " + curModel.TableName + " where nodeid=@nodeid");
               //dbIndexCmd.Connection = dbConn;
               //dbIndexCmd.Transaction = transaction;
               //DataHelper.SetParameterValue(dbIndexCmd, labelParams);
               //object indexObj = DataHelper.ExecuteScalar(dbIndexCmd);
               //int indexId = Convert.ToInt32(Convert.IsDBNull(indexObj) ? 0 : indexObj) + 1;
               //labelParams.Add("indexid", new XNLParam( XNLType.XNL_Int32, indexId));
             //  labelParams.Add("indexid", new XNLParam(XNLType.XNL_Int32, 0));
               //StringBuilder insertSqlSb = new StringBuilder();
              // insertSqlSb.Append("insert into " + curModel.TableName + " ([IndexID],[NodeID],[InputUser],[LastEditUser],[State],[PinYinTitle],[PYTitle],[isComment],[Hits],[titleColor],[underLine],[Italic],[Bold],[IsRecommend],[IsHot],[IsColor],[IsTop],[pageType],[pageWords] " + fieldSb.ToString() + ") values (@IndexID,@NodeID,@InputUser,@LastEditUser,@State,@PinYinTitle,@PYTitle,@isComment,@Hits,@titleColor,@underLine,@Italic,@Bold,@IsRecommend,@IsHot,@IsColor,@IsTop,@pageType,@pageWords" + fieldParamSb.ToString() + ")");
               string insertSql="insert into " + curModel.TableName + " ([NodeID],[InputUser],[LastEditUser],[State],[PinYinTitle],[PYTitle],[isComment],[Hits],[titleColor],[underLine],[Italic],[Bold],[IsRecommend],[IsHot],[IsColor],[IsTop],[pageType],[pageWords] " + fieldSb.ToString() + ") values (@NodeID,@InputUser,@LastEditUser,@State,@PinYinTitle,@PYTitle,@isComment,@Hits,@titleColor,@underLine,@Italic,@Bold,@IsRecommend,@IsHot,@IsColor,@IsTop,@pageType,@pageWords" + fieldParamSb.ToString() + ")";
              // DbCommand insertCmd = DataHelper.GetSqlStringCommand(insertSqlSb.ToString());
               string datebaseType=DataHelper.getDataBaseType();
               if(datebaseType=="access"||datebaseType=="sqlserver")
               {
                   insertSql=insertSql+";Select @@Identity";
               }
               DbCommand insertCmd = DataHelper.GetSqlStringCommand(insertSql);
               DataHelper.SetParameterValue(insertCmd, labelParams);
               insertCmd.Connection = dbConn;
               insertCmd.Transaction = transaction;
               int insertid=0;
               if(datebaseType=="access"||datebaseType=="sqlserver")
               {
                   insertid=Convert.ToInt32(insertCmd.ExecuteScalar());
               }
               else
               {
                   insertCmd.ExecuteNonQuery();
                   DbCommand getIdCmd=DataHelper.GetSqlStringCommand("select max(id) from "+curModel.TableName+" where nodeid="+nodeId.ToString());
                   getIdCmd.Connection = dbConn;
                   getIdCmd.Transaction = transaction;
                   insertid=Convert.ToInt32(getIdCmd.ExecuteScalar());
               }
               insertCmd.CommandText = "update sn_nodes set ItemCount=ItemCount+1 where nodeid="+nodeId.ToString();
               insertCmd.ExecuteNonQuery();
               insertCmd.CommandText = "update sn_sites set ContentCount=ContentCount+1 where siteid="+curChannel.siteID.ToString();
               insertCmd.ExecuteNonQuery();
               ///////////////////
               if (curChannel.theChannelConfig.baseConfig.useContentGroup)
               {
                   //插入内容组
                   string groups = (XNLPage.Context.Request.Form["groupid"] == null || XNLPage.Context.Request.Form["groupid"].Equals("")) ? "" : XNLPage.Context.Request.Form["groupid"];
                   if (!groups.Trim().Equals(""))
                   {

                       string[] groups_arr = groups.Split(new char[] { ',' });
                       foreach (string str in groups_arr)
                       {
                           DbCommand insertgCmd = DataHelper.GetSqlStringCommand("insert into SN_ContentGroups (CGS_Id,CGS_ContentId,CGS_NodeId) values(" + str + "," + insertid.ToString() + "," + nodeId.ToString() + ")");
                           insertgCmd.Connection = dbConn;
                           insertgCmd.Transaction = transaction;
                           insertgCmd.ExecuteNonQuery();
                       }

                   }
               }
               if(curChannel.theChannelConfig.baseConfig.useContentTag)
               {
                   //设置内容tag
                   string groups = (XNLPage.Context.Request.Form["xnl__tag"] == null || XNLPage.Context.Request.Form["xnl__tag"].Equals("")) ? "" : XNLPage.Context.Request.Form["xnl__tag"];
                   if (!groups.Trim().Equals(""))
                   {
                       string tmpTags ="'"+(groups.Replace("'", "''").Replace(",","','"))+"'";
                       DbCommand tagCmd = DataHelper.GetSqlStringCommand("select ct_id,ct_name from sn_contenttag where ct_siteid="+curChannel.siteID.ToString()+" and ct_name in ("+tmpTags+")");
                       tagCmd.Connection = dbConn;
                       tagCmd.Transaction = transaction;
                       DataTable tag_dt = DataHelper.ExecuteDataTable(tagCmd);
                       string[] groups_arr = groups.Split(new char[] { ',' });
                       Dictionary<int, string> tagColls = new Dictionary<int, string>(groups_arr.Length);
                       List<string> tagList = new List<string>();
                       int newtagid = 0;
                       foreach (DataRow row in tag_dt.Rows)
                       {
                           string _tagname = Convert.ToString(row[1]);
                           tagColls.Add(Convert.ToInt32(row[0]), _tagname);
                           tagList.Add(_tagname);
                       }
                       foreach (string str in groups_arr)
                       {
                           if (!tagList.Contains(str))
                           {
                               //插入不存在的
                               if (datebaseType == "access" || datebaseType == "sqlserver")
                               {
                                   tagCmd.CommandText = "insert into sn_contenttag (ct_name,ct_usenum,ct_siteid) values('" + str.Replace("'", "''") + "',1," + curChannel.siteID.ToString() + ");Select @@Identity";
                                   newtagid = Convert.ToInt32(tagCmd.ExecuteScalar());
                                   tagColls.Add(newtagid, str);
                               }
                               else
                               {
                                   tagCmd.CommandText = "insert into sn_contenttag (ct_name,ct_usenum,ct_siteid) values('" + str.Replace("'", "''") + "',1," + curChannel.siteID.ToString() + ")";
                                   tagCmd.ExecuteNonQuery();
                                   tagCmd.CommandText = "select max(ct_id) from sn_contenttag";
                                   newtagid = Convert.ToInt32(tagCmd.ExecuteScalar());
                                   tagColls.Add(newtagid, str);
                               }
                           }
                           else
                           {
                               //更新已存在的
                               tagCmd.CommandText = "update sn_contenttag set ct_usenum=ct_usenum+1 where ct_name='" + str.Replace("'", "''") + "' and ct_siteid=" + curChannel.siteID.ToString();
                               tagCmd.ExecuteNonQuery();
                           }
                       }
                       foreach (KeyValuePair<int, string> kv in tagColls)
                       {
                           tagCmd.CommandText = "insert into SN_ContentTags (CTS_Id,CTS_ContentId,CTS_NodeId,CTS_siteid) values(" + kv.Key.ToString() + "," + insertid.ToString() + "," + nodeId.ToString() + ","+curChannel.siteID.ToString()+")";
                           tagCmd.ExecuteNonQuery();
                       }
                   }
               }
               ///////////////
               transaction.Commit();
               transaction.Dispose();
               dbConn.Close();
           }
           catch (Exception e)
           {
               transaction.Rollback();
               transaction.Dispose();
               dbConn.Close();
               throw (e);
           }

       }
       private void modify(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           Cchannel curChannel=ChannelConfigManager.createInstance().channelDataColls[nodeId];
           DataModel curModel = curChannel.model;
           string modelName = curModel.ModelName;
           //得到模型描述信息
           DataTable desDt = DataHelper.ExecuteDataTable("select FieldName,DataType,DefaultValue,IsVisible,InputType  from SN_ModelDescript where ModelName='" + modelName + "'");
           bool isTitleShow = true;
           StringBuilder fieldSb = new StringBuilder();
           //得到所有字段
           foreach (DataRow row in desDt.Rows)
           {
               bool isVisible = Convert.ToInt32(row["isVisible"]) == 1 ? true : false;
               string DataType = Convert.ToString(row["DataType"]);
               string DefaultValue = Convert.ToString(row["DefaultValue"]);
               string fieldName = Convert.ToString(row["FieldName"]);
               string lowFieldName = fieldName.ToLower();
               string InputType = Convert.ToString(row["InputType"]);
               if (isVisible)
               {
                   switch (DataType)
                   {
                       case "NVarChar":
                       case "NText":
                           string v = (XNLPage.Context.Request.Form[fieldName] == null ? "" : XNLPage.Context.Request.Form[fieldName]);
                           if (!InputType.Equals("TextEditor"))
                           {
                               v = UtilsCode.encodeHtmlAndXnl(v);
                           }
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.String, v));
                           break;
                       case "Integer":
                       case "Boolean":
                           int v1 = Convert.ToInt32(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.Int32, v1));
                           break;
                       case "Decimal":
                           decimal v2 = Convert.ToDecimal(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam(XNLType.Decimal, v2));
                           break;
                       case "DateTime":
                           var tmpType = DataType;
                           if (InputType.Equals("Date")) tmpType = InputType;
                           string d = ManageUtil.setItemDefault(tmpType, DefaultValue);
                           DateTime t;
                           if (!DateTime.TryParse(XNLPage.Context.Request.Form[fieldName] == null ? d : XNLPage.Context.Request.Form[fieldName], out t))
                           {
                               t = Convert.ToDateTime(d);
                           }
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.DateTime, t));
                           break;
                   }
               }
               else
               {
                   if (lowFieldName.Equals("title")) isTitleShow = false;
                   switch (DataType)
                   {
                       case "NVarChar":
                       case "NText":
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.String, DefaultValue));
                           break;
                       case "Integer":
                       case "Boolean":
                           int v1 = Convert.ToInt32(DefaultValue);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.Int32, v1));
                           break;
                       case "Decimal":
                           decimal v2 = Convert.ToDecimal(XNLPage.Context.Request.Form[fieldName] == null ? DefaultValue : XNLPage.Context.Request.Form[fieldName]);
                           labelParams.Add(lowFieldName, new XNLParam(XNLType.Decimal, v2));
                           break;
                       case "DateTime":
                           var tmpType=DataType;
                           if(InputType.Equals("Date"))tmpType=InputType;
                           string d = ManageUtil.setItemDefault(tmpType, DefaultValue);
                           DateTime t = Convert.ToDateTime(d);
                           labelParams.Add(lowFieldName, new XNLParam( XNLType.DateTime, t));
                           break;
                   }
               }
               fieldSb.Append("," + fieldName+"=@" + fieldName);
           }
           //设置标题属性
           int boldStr = (XNLPage.Context.Request.Form["titleStyle_bold"] == null || XNLPage.Context.Request.Form["titleStyle_bold"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_bold"]);
           int italicStr = (XNLPage.Context.Request.Form["titleStyle_italic"] == null || XNLPage.Context.Request.Form["titleStyle_italic"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_italic"]);
           int uStr = (XNLPage.Context.Request.Form["titleStyle_u"] == null || XNLPage.Context.Request.Form["titleStyle_u"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["titleStyle_u"]);
           string colorStr = (XNLPage.Context.Request.Form["titleStyle_color"] == null || XNLPage.Context.Request.Form["titleStyle_color"].Equals("")) ? "#000000" : XNLPage.Context.Request.Form["titleStyle_color"];
           
           labelParams.Add("bold", new XNLParam(XNLType.Int32, boldStr));
           labelParams.Add("italic", new XNLParam(XNLType.Int32, italicStr));
           labelParams.Add("underline", new XNLParam(XNLType.Int32, uStr));
           labelParams.Add("titlecolor", new XNLParam(XNLType.String, colorStr));
           labelParams.Add("pinyintitle", new XNLParam( XNLType.String));
           labelParams.Add("pytitle", new XNLParam( XNLType.String));
           if (isTitleShow)
           {
               string title = XNLPage.Context.Request.Form["title"];
               //设置pinyintitle ,pytitle
               string pinYinTitle = UtilsCode.CHS2PinYin(title, "", true);
               string pyTitle = UtilsCode.CHS2PY(title, "", true);
               labelParams["pinyintitle"].value = pinYinTitle;
               labelParams["pytitle"].value = pyTitle;
           }
           //设置状态
          // labelParams["nodeid"].dbType = DbType.Int32;
           labelParams["nodeid"].type = XNLType.Int32;
           if (labelParams["state"].value.ToString().Equals(""))
           {
               labelParams["state"].value = 0;
           }
           labelParams["state"].value = Convert.ToInt32(labelParams["state"].value);
           //labelParams["state"].dbType = DbType.Int32;
           labelParams["state"].type = XNLType.Int32;
           //设置输入用户
           string adminUser=ManageUtil.getCurAdminName();
           labelParams.Add("lasteditdate", new XNLParam(XNLType.Date, DateTime.Now.Date));
           labelParams.Add("lastedituser", new XNLParam(XNLType.String, adminUser));
           Cchannel channelNode = ChannelConfigManager.createInstance().channelDataColls[nodeId];
           ChannelBaseConfig baseConfig = channelNode.theChannelConfig.baseConfig;
           int pageType=0;
           int pageWords = 1500;
           switch(baseConfig.partPage)
           {
               case ChannelBaseConfig.PartPage.No:
                   pageType = 0;
                   break;
               case ChannelBaseConfig.PartPage.Auto:
                   pageType = 1;
                   break;
               case ChannelBaseConfig.PartPage.Manual:
                   pageType = 2;
                   pageWords = baseConfig.autoWordNums;
                   break;
           }
           //设置是否允许评论isComment,pageType,pageWords
           int isComment = 0; ;
           if(XNLPage.Context.Request.Form["isComment"] == null)
           {
               isComment=(channelNode.theChannelConfig.commentsConfig.isContentComments?1:0) ;
           }
           else
           {
               if(XNLPage.Context.Request.Form["isComment"].Equals("1"))
               {
                   isComment=1;
               }
           }
           pageType = (XNLPage.Context.Request.Form["pageType"] == null||XNLPage.Context.Request.Form["pageType"].Equals("")) ? pageType : Convert.ToInt32(XNLPage.Context.Request.Form["pageType"]);
           pageWords = (XNLPage.Context.Request.Form["pageWords"] == null||XNLPage.Context.Request.Form["pageWords"].Equals("")) ? pageWords : Convert.ToInt32(XNLPage.Context.Request.Form["pageWords"]);
           int hits = (XNLPage.Context.Request.Form["Hits"] == null || XNLPage.Context.Request.Form["Hits"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["Hits"]);
           labelParams.Add("iscomment", new XNLParam(XNLType.Int32, isComment));
           labelParams.Add("pagetype", new XNLParam(XNLType.Int32, pageType));
           labelParams.Add("pagewords", new XNLParam(XNLType.Int32, pageWords));
           labelParams.Add("hits", new XNLParam(XNLType.Int32, hits));
           int IsRecommend = (XNLPage.Context.Request.Form["IsRecommend"] == null || XNLPage.Context.Request.Form["IsRecommend"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsRecommend"]);
           int IsHot = (XNLPage.Context.Request.Form["IsHot"] == null || XNLPage.Context.Request.Form["IsHot"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsHot"]);
           int IsColor = (XNLPage.Context.Request.Form["IsColor"] == null || XNLPage.Context.Request.Form["IsColor"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsColor"]);
           int IsTop = (XNLPage.Context.Request.Form["IsTop"] == null || XNLPage.Context.Request.Form["IsTop"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsTop"]);
           labelParams.Add("isrecommend", new XNLParam(XNLType.Int32, IsRecommend));
           labelParams.Add("ishot", new XNLParam(XNLType.Int32, IsHot));
           labelParams.Add("iscolor", new XNLParam(XNLType.Int32, IsColor));
           labelParams.Add("istop", new XNLParam(XNLType.Int32, IsTop));

           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction transaction = dbConn.BeginTransaction();
           try
           {
               //设置内容组
               ///////////////////
               int contentId = Convert.ToInt32(labelParams["contentid"].value);
               if (curChannel.theChannelConfig.baseConfig.useContentGroup)
               {
                   //插入内容组
                   string groups = (XNLPage.Context.Request.Form["groupid"] == null || XNLPage.Context.Request.Form["groupid"].Equals("")) ? "" : XNLPage.Context.Request.Form["groupid"];
                   if (!groups.Trim().Equals(""))
                   {
                       DbCommand groupCmd = DataHelper.GetSqlStringCommand("delete from SN_ContentGroups where CGS_Id not in (" + groups + ") and CGS_ContentId=" + contentId.ToString() + " and CGS_NodeId=" + nodeId.ToString());
                       groupCmd.Connection = dbConn;
                       groupCmd.Transaction = transaction;
                       groupCmd.ExecuteNonQuery();
                       groupCmd.CommandText = "select CGS_Id from SN_ContentGroups  where CGS_ContentId=" + contentId.ToString() + " and CGS_NodeId=" + nodeId.ToString();
                       DataTable groupDt = DataHelper.ExecuteDataTable(groupCmd);
                       string[] groups_arr = groups.Split(new char[] { ',' });
                       if (groupDt.Rows.Count == 0)
                       {
                           foreach (string str in groups_arr)
                           {
                               DbCommand _insertCmd = DataHelper.GetSqlStringCommand("insert into SN_ContentGroups (CGS_Id,CGS_ContentId,CGS_NodeId) values(" + str + ","+contentId.ToString()+"," + nodeId + ")");
                               _insertCmd.Connection = dbConn;
                               _insertCmd.Transaction = transaction;
                               _insertCmd.ExecuteNonQuery();
                           }
                       }
                       else
                       {
                           List<string> groupList = new List<string>();
                           foreach (DataRow row in groupDt.Rows)
                           {
                               groupList.Add(row["CGS_Id"].ToString());
                           }
                           foreach (string str in groups_arr)
                           {
                               if (!groupList.Contains(str))
                               {
                                   DbCommand _insertCmd = DataHelper.GetSqlStringCommand("insert into SN_ContentGroups (CGS_Id,CGS_ContentId,CGS_NodeId) values(" + str + "," + contentId.ToString() + "," + nodeId + ")");
                                   _insertCmd.Connection = dbConn;
                                   _insertCmd.Transaction = transaction;
                                   _insertCmd.ExecuteNonQuery();
                               }
                           }
                       }
                   }
                   else
                   {
                       DbCommand insertgCmd = DataHelper.GetSqlStringCommand("delete from SN_ContentGroups where CGS_ContentId=" + contentId.ToString() + " and CGS_NodeId=" + nodeId.ToString());
                       insertgCmd.Connection = dbConn;
                       insertgCmd.Transaction = transaction;
                       insertgCmd.ExecuteNonQuery();
                   }
               }

               if (curChannel.theChannelConfig.baseConfig.useContentTag)
               {
                   //设置内容tag
                   string groups = (XNLPage.Context.Request.Form["xnl__tag"] == null || XNLPage.Context.Request.Form["xnl__tag"].Equals("")) ? "" : XNLPage.Context.Request.Form["xnl__tag"];
                   if (!groups.Trim().Equals(""))
                   {
                       string tmpTags = "'" + (groups.Replace("'", "''").Replace(",", "','")) + "'";
                       DbCommand tagCmd = DataHelper.GetSqlStringCommand("select ct_id,ct_name from sn_contenttag where ct_siteid=" + curChannel.siteID.ToString() + " and ct_name in (" + tmpTags + ")");
                       tagCmd.Connection = dbConn;
                       tagCmd.Transaction = transaction;
                       DataTable tag_dt = DataHelper.ExecuteDataTable(tagCmd);
                       string[] groups_arr = groups.Split(new char[] { ',' });
                       List<string> tagList = new List<string>();
                       int newtagid = 0;
                       string tagids = "";
                       int ti = 0;
                       List<int> newtagList = new List<int>();
                       List<int> updatetagList = new List<int>(); //要更新的tagid
                       foreach (DataRow row in tag_dt.Rows)
                       {
                           string _tagname = Convert.ToString(row[1]);
                           int t_id = Convert.ToInt32(row[0]);
                           tagids += (ti > 0 ? "," : "") + t_id.ToString();
                           tagList.Add(_tagname);
                           updatetagList.Add(t_id);
                       }
                       foreach (string str in groups_arr)
                       {
                           if (!tagList.Contains(str))
                           {
                               //插入不存在的
                               string datebaseType = DataHelper.getDataBaseType();
                               if (datebaseType == "access" || datebaseType == "sqlserver")
                               {
                                   tagCmd.CommandText = "insert into sn_contenttag (ct_name,ct_usenum,ct_siteid) values('" + str.Replace("'", "''") + "',1," + curChannel.siteID.ToString() + ");Select @@Identity";
                                   newtagid = Convert.ToInt32(tagCmd.ExecuteScalar());
                                   newtagList.Add(newtagid);
                               }
                               else
                               {
                                   tagCmd.CommandText = "insert into sn_contenttag (ct_name,ct_usenum,ct_siteid) values('" + str.Replace("'", "''") + "',1," + curChannel.siteID.ToString() + ")";
                                   tagCmd.ExecuteNonQuery();
                                   tagCmd.CommandText = "select max(ct_id) from sn_contenttag";
                                   newtagid = Convert.ToInt32(tagCmd.ExecuteScalar());
                                   newtagList.Add(newtagid);
                               }
                           }
                       }
                       ////////////////////////////
                       tagCmd.CommandText = "select CTS_Id from SN_ContentTags  where CTS_ContentId=" + contentId.ToString() + " and CTS_NodeId=" + nodeId.ToString();
                       DataTable groupDt = DataHelper.ExecuteDataTable(tagCmd);
                       if (groupDt.Rows.Count == 0)
                       {
                           foreach (int _n in updatetagList)
                           {
                               tagCmd.CommandText="insert into SN_ContentTags (CTS_Id,CTS_ContentId,CTS_NodeId,cts_siteid) values(" + _n.ToString() + "," + contentId.ToString() + "," + nodeId + ","+curChannel.siteID.ToString()+")";
                               tagCmd.ExecuteNonQuery();
                               tagCmd.CommandText = "update SN_ContentTag set ct_usenum=ct_usenum+1 where ct_id="+_n.ToString();
                               tagCmd.ExecuteNonQuery();
                           }
                       }
                       else
                       {
                           List<int> srcList = new List<int>();
                           foreach (DataRow row in groupDt.Rows)
                           {
                               int ct_id = Convert.ToInt32(row["CTS_Id"]);
                               srcList.Add(ct_id);
                               if (!updatetagList.Contains(ct_id))
                               {
                                   //删除
                                   tagCmd.CommandText = "update SN_ContentTag set ct_usenum=ct_usenum-1 where ct_id="+ct_id.ToString();
                                   tagCmd.ExecuteNonQuery();
                                   tagCmd.CommandText = "delete from SN_ContentTags where CTS_Id="+ct_id+" and CTS_ContentId=" + contentId.ToString() + " and CTS_NodeId=" + nodeId.ToString();
                                   tagCmd.ExecuteNonQuery();
                               }
                           }
                           foreach (int _n in updatetagList)
                           {
                               if (!srcList.Contains(_n))
                               {
                                   tagCmd.CommandText = "insert into SN_ContentTags (CTS_Id,CTS_ContentId,CTS_NodeId,cts_siteid) values(" + _n.ToString() + "," + contentId.ToString() + "," + nodeId + "," + curChannel.siteID.ToString() + ")";
                                   tagCmd.ExecuteNonQuery();
                                   tagCmd.CommandText = "update SN_ContentTag set ct_usenum=ct_usenum+1 where ct_id=" + _n.ToString();
                                   tagCmd.ExecuteNonQuery();
                               }
                           }
                       }
                       foreach (int _n in newtagList)
                       {
                           tagCmd.CommandText = "insert into SN_ContentTags (CTS_Id,CTS_ContentId,CTS_NodeId,cts_siteid) values(" + _n.ToString() + "," + contentId.ToString() + "," + nodeId + "," + curChannel.siteID.ToString() + ")";
                           tagCmd.ExecuteNonQuery();
                       }
                   }
                   else
                   {
                       //删除原来的
                       DbCommand cmd = DataHelper.GetSqlStringCommand("update SN_ContentTag set ct_usenum=ct_usenum-1 where ct_id in (select cts_id from SN_ContentTags where CTS_ContentId=" + contentId.ToString() + " and CTS_NodeId=" + nodeId.ToString()+")");
                       cmd.Connection = dbConn;
                       cmd.Transaction = transaction;
                       cmd.ExecuteNonQuery();
                       cmd.CommandText="delete from SN_ContentTags where CTS_ContentId=" + contentId.ToString() + " and CTS_NodeId=" + nodeId.ToString();
                       cmd.ExecuteNonQuery();
                   }
               }
               ///////////////
               StringBuilder insertSqlSb = new StringBuilder();
               insertSqlSb.Append("update " + curModel.TableName + " set [LastEditDate]=@LastEditDate,[LastEditUser]=@LastEditUser,[State]=@State,[PinYinTitle]=@PinYinTitle,[PYTitle]=@PYTitle,[isComment]=@isComment,[Hits]=@Hits,[titleColor]=@titleColor,[underLine]=@underLine,[Italic]=@Italic,[Bold]=@Bold,[IsRecommend]=@IsRecommend,[IsHot]=@IsHot,[IsColor]=@IsColor,[IsTop]=@IsTop,[pageType]=@pageType,[pageWords]=@pageWords " + fieldSb.ToString() + " where [id]=@contentId");
               DbCommand insertCmd = DataHelper.GetSqlStringCommand(insertSqlSb.ToString());
               DataHelper.SetParameterValue(insertCmd, labelParams);
               insertCmd.Connection = dbConn;
               insertCmd.Transaction = transaction;
               insertCmd.ExecuteNonQuery();
               transaction.Commit();
               transaction.Dispose();
               dbConn.Close();
           }
           catch (Exception e)
           {
               transaction.Rollback();
               transaction.Dispose();
               dbConn.Close();
               //dbConn.Dispose();
               throw (e);
           }
       }
       private void deleteToRecycle(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
       {
           string contentIdStr = labelParams["contentid"].value.ToString().Trim();
           if(contentIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容编号"));
           }
           if(!Regex.IsMatch(contentIdStr.Replace(",",""),"\\d+"))
           {
                throw (new Exception("错误的内容编号"));
           }
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           DataModel curModel = ChannelConfigManager.createInstance().channelDataColls[nodeId].model;
           string deleteSql = "update " + curModel.TableName + " set [isRecycle]=1 where id=@contentId";
           if(contentIdStr.Length>1)
           {
               deleteSql = "update " + curModel.TableName + " set [isRecycle]=1 where id in ("+contentIdStr+")";
           }
           DbCommand delCommand = DataHelper.GetSqlStringCommand(deleteSql);
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction tran = dbConn.BeginTransaction();
           DataHelper.SetParameterValue(delCommand, labelParams);
           delCommand.Connection = dbConn;
           delCommand.Transaction = tran;
           delCommand.ExecuteNonQuery();
           tran.Commit();
           tran.Dispose();
           dbConn.Close();
       }
       private void delete(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
       {
           string contentIdStr = labelParams["contentid"].value.ToString().Trim();
           if(contentIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容编号"));
           }
           if(!Regex.IsMatch(contentIdStr.Replace(",",""),"\\d+"))
           {
                throw (new Exception("错误的内容编号"));
           }
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           Cchannel curChannel=ChannelConfigManager.createInstance().channelDataColls[nodeId];
           DataModel curModel = curChannel.model;
           string deleteSql = "delete from " + curModel.TableName + " where id="+contentIdStr;
           int contentLen = 1;
           string selectSql = "select cts_id from sn_contenttags where cts_contentid="+contentIdStr;
           string delgroup = "delete from sn_contentgroups where CGS_ContentId=" + contentIdStr + " and CGS_NodeId="+curChannel.nodeID.ToString();
           string deltag = "delete from sn_contenttags where CTS_ContentId=" + contentIdStr + " and CTS_NodeId=" + curChannel.nodeID.ToString();
           contentLen = contentIdStr.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).Length;
           if (contentLen > 1)
           {
               selectSql = "select cts_id from sn_contenttags where cts_contentid in (" + contentIdStr+")";
               deleteSql = "delete from " + curModel.TableName + "  where id in ("+contentIdStr+")";
               delgroup = "delete from sn_contentgroups where CGS_ContentId in (" + contentIdStr + ") and CGS_NodeId=" + curChannel.nodeID.ToString();
               deltag = "delete from sn_contenttags where CTS_ContentId in (" + contentIdStr + ") and CTS_NodeId=" + curChannel.nodeID.ToString();
           }
           DbCommand delCommand = DataHelper.GetSqlStringCommand(deleteSql);
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction tran = dbConn.BeginTransaction();
           delCommand.Connection = dbConn;
           delCommand.Transaction = tran;
           delCommand.ExecuteNonQuery();
           delCommand.CommandText = selectSql;
           DataTable dt = DataHelper.ExecuteDataTable(delCommand);
           foreach (DataRow row in dt.Rows)
           {
               delCommand.CommandText = "update sn_contenttag set ct_usenum=ct_usenum-1 where ct_id="+Convert.ToString(row[0]);
               delCommand.ExecuteNonQuery();
           }
           delCommand.CommandText = delgroup;
           delCommand.ExecuteNonQuery();
           delCommand.CommandText = deltag;
           int delnum=delCommand.ExecuteNonQuery();
           delCommand.CommandText = "update sn_nodes set ItemCount=ItemCount-"+contentLen.ToString()+" where nodeid=" + nodeId.ToString();
           delCommand.ExecuteNonQuery();
           delCommand.CommandText = "update sn_sites set ContentCount=ContentCount-"+contentLen.ToString()+" where siteid=" + curChannel.siteID.ToString();
           delCommand.ExecuteNonQuery();
           tran.Commit();
           tran.Dispose();
           dbConn.Close();
       }
       private void restore(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
       {
           string contentIdStr = labelParams["contentid"].value.ToString().Trim();
           if(contentIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容编号"));
           }
           if (!Regex.IsMatch(contentIdStr.Replace(",", ""), "\\d+"))
           {
                throw (new Exception("错误的内容编号"));
           }
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           DataModel curModel = ChannelConfigManager.createInstance().channelDataColls[nodeId].model;
           string deleteSql = "update " + curModel.TableName + " set [isRecycle]=0 where id=@contentId";
           if(contentIdStr.Length>1)
           {
               deleteSql = "update " + curModel.TableName + " set [isRecycle]=0 where id in ("+contentIdStr+")";
           }
           DbCommand delCommand = DataHelper.GetSqlStringCommand(deleteSql);
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction tran = dbConn.BeginTransaction();
           DataHelper.SetParameterValue(delCommand, labelParams);
           delCommand.Connection = dbConn;
           delCommand.Transaction = tran;
           delCommand.ExecuteNonQuery();
           tran.Commit();
           tran.Dispose();
           dbConn.Close();
       }
       private void setAttribute(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
           string contentIdStr = labelParams["contentid"].value.ToString().Trim();
           if (contentIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容编号"));
           }
           if (!Regex.IsMatch(contentIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的内容编号"));
           }
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           DataModel curModel = ChannelConfigManager.createInstance().channelDataColls[nodeId].model;
           int isComment = 0; ;
           if (XNLPage.Context.Request.Form["isComment"] == null)
           {
               Cchannel channelNode = ChannelConfigManager.createInstance().channelDataColls[nodeId];
               isComment = (channelNode.theChannelConfig.commentsConfig.isContentComments ? 1 : 0);
           }
           else
           {
               if (XNLPage.Context.Request.Form["isComment"].Equals("1"))
               {
                   isComment = 1;
               }
           }
          // pageType = (XNLPage.Context.Request.Form["pageType"] == null || XNLPage.Context.Request.Form["pageType"].Equals("")) ? pageType : Convert.ToInt32(XNLPage.Context.Request.Form["pageType"]);
          // pageWords = (XNLPage.Context.Request.Form["pageWords"] == null || XNLPage.Context.Request.Form["pageWords"].Equals("")) ? pageWords : Convert.ToInt32(XNLPage.Context.Request.Form["pageWords"]);
           int hits = (XNLPage.Context.Request.Form["Hits"] == null || XNLPage.Context.Request.Form["Hits"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["Hits"]);
           labelParams.Add("iscomment", new XNLParam(XNLType.Int32, isComment));
          // labelParams.Add("pagetype", new XNLParam(XNLType.XNL_Int32, pageType, DbType.Int32));
          // labelParams.Add("pagewords", new XNLParam(XNLType.XNL_Int32, pageWords, DbType.Int32));
           //labelParams.Add("hits", new XNLParam(XNLType.XNL_Int32, hits, DbType.Int32));
           int IsRecommend = (XNLPage.Context.Request.Form["IsRecommend"] == null || XNLPage.Context.Request.Form["IsRecommend"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsRecommend"]);
           int IsHot = (XNLPage.Context.Request.Form["IsHot"] == null || XNLPage.Context.Request.Form["IsHot"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsHot"]);
           int IsColor = (XNLPage.Context.Request.Form["IsColor"] == null || XNLPage.Context.Request.Form["IsColor"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsColor"]);
           int IsTop = (XNLPage.Context.Request.Form["IsTop"] == null || XNLPage.Context.Request.Form["IsTop"].Equals("")) ? 0 : Convert.ToInt32(XNLPage.Context.Request.Form["IsTop"]);
           labelParams.Add("isrecommend", new XNLParam(XNLType.Int32, IsRecommend));
           labelParams.Add("ishot", new XNLParam(XNLType.Int32, IsHot));
           labelParams.Add("iscolor", new XNLParam(XNLType.Int32, IsColor));
           labelParams.Add("istop", new XNLParam(XNLType.Int32, IsTop));
           string Sql = "update " + curModel.TableName + " set [isComment]=@isComment,[state]=@state,[isRecommend]=@isRecommend,[isHot]=@isHot,[isColor]=@isColor,[isTop]=@isTop where id=@contentId";
           if (contentIdStr.Length > 1)
           {
               Sql = "update " + curModel.TableName + " set [isComment]=@isComment,[state]=@state,[isRecommend]=@isRecommend,[isHot]=@isHot,[isColor]=@isColor,[isTop]=@isTop where id in (" + contentIdStr + ")";
           }
           DbCommand delCommand = DataHelper.GetSqlStringCommand(Sql);
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction tran = dbConn.BeginTransaction();
           DataHelper.SetParameterValue(delCommand, labelParams);
           delCommand.Connection = dbConn;
           delCommand.Transaction = tran;
           delCommand.ExecuteNonQuery();
           tran.Commit();
           tran.Dispose();
           dbConn.Close();
       }
       private void setAuditPass(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
       {
           string contentIdStr = labelParams["contentid"].value.ToString().Trim();
           if (contentIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容编号"));
           }
           if (!Regex.IsMatch(contentIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的内容编号"));
           }
           //得到模型
           int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
           DataModel curModel = ChannelConfigManager.createInstance().channelDataColls[nodeId].model;
           string Sql = "update " + curModel.TableName + " set [state]=99 where id=@contentId";
           if (contentIdStr.Length > 1)
           {
               Sql = "update " + curModel.TableName + " set [state]=99 where id in (" + contentIdStr + ")";
           }
           DbCommand delCommand = DataHelper.GetSqlStringCommand(Sql);
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction tran = dbConn.BeginTransaction();
           DataHelper.SetParameterValue(delCommand, labelParams);
           delCommand.Connection = dbConn;
           delCommand.Transaction = tran;
           delCommand.ExecuteNonQuery();
           tran.Commit();
           tran.Dispose();
           dbConn.Close();
       }
       private void addGroup(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
           //检查名称是否为空
           XNLParam groupNameParam;
           if (labelParams.TryGetValue("groupname", out groupNameParam))
           {
               if (groupNameParam.value.ToString().Trim().Equals(""))
               {
                   Dictionary<string, string> errorList = new Dictionary<string, string>();
                   throw (new Exception("内容组名称不能为空!"));
               }
               groupNameParam.type = XNLType.String;
           }
           else
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("没有内容组名称属性!"));
           }
           //检查是否已存在相在名称
           int siteId = ManageUtil.getCurSiteID(XNLPage);
           object obj = DataHelper.ExecuteScalar("select CG_id from sn_contentgroup where CG_name=@groupname and CG_siteid=" + siteId.ToString(), labelParams);
           int groupId = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
           if (groupId > 0)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("内容组名称已存在!"));  
           }
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction trans = dbConn.BeginTransaction();
           try
           {
               DbCommand command = DataHelper.GetSqlStringCommand("insert into sn_contentgroup(CG_Name,CG_SiteId,CG_Desc)values(@GroupName," + siteId + ",@Description)");
               DataHelper.SetParameterValue(command, labelParams);
               command.Connection = dbConn;
               command.Transaction = trans;
               DataHelper.ExecuteNonQuery(command);
               trans.Commit();
               trans.Dispose();
               dbConn.Close();
           }
           catch (Exception e)
           {
               trans.Rollback();
               trans.Dispose();
               dbConn.Close();
               throw (e);
           }
       }
       private void deleteGroup(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
       {
           string groupIdStr = labelParams["groupid"].value.ToString().Trim();
           if (groupIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容组编号"));
           }
           if (!Regex.IsMatch(groupIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的内容组编号"));
           }
           string sql1 = "delete from SN_ContentGroup where CG_ID =" + groupIdStr;
           string sql2 = "delete from SN_ContentGroups where CGS_ID=" + groupIdStr;
           if (groupIdStr.IndexOf(',') >= 0)
           {
               sql1 = "delete from SN_ContentGroup where CG_ID in(" + groupIdStr + ")";
               sql2 = "delete from SN_ContentGroups where CGS_ID in(" + groupIdStr + ")";
           }
           Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
           sqlColls.Add("1", sql1);
           sqlColls.Add("2", sql2);
           DbResultInfo dbInfo = DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
           if (!dbInfo.isSuccess)
           {
               throw (dbInfo.exception);
           }
       }
       private void deleteGroupInfo(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
       {
           string groupIdStr = labelParams["groupid"].value.ToString().Trim();
           if (groupIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容组编号"));
           }
           else if (!Regex.IsMatch(groupIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的内容组编号"));
           }
           string nodeIdStr = labelParams["nodeid"].value.ToString().Trim();
           if (nodeIdStr.Equals(""))
           {
               throw (new Exception("没有指定栏目编号"));
           }
           else if (!Regex.IsMatch(nodeIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的栏目编号"));
           }
           string infoIdStr=labelParams["infoid"].value.ToString().Trim();
           if (infoIdStr.Equals(""))
           {
               throw (new Exception("没有指定信息编号"));
           }
           else if (!Regex.IsMatch(infoIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的信息编号"));
           }
           //string sql = "delete from SN_ContentGroups where CGS_ID =" + groupIdStr + " and CGS_nodeId=" + nodeIdStr + " and CGS_ContentId="+infoIdStr;
           string sql = "delete from SN_ContentGroups where CGS_ID =" + groupIdStr + " and CGS_nodeId";// +nodeIdStr + " and CGS_ContentId=" + infoIdStr;
           if (nodeIdStr.IndexOf(',') >= 0)
           {
               sql += " in (" + nodeIdStr + " )";
               //sql = "delete from SN_NodeGroups where NGS_ID=" + groupIdStr + " and NGS_nodeId in(" + nodeIdStr + ")";
           }
           else
           {
               sql += "=" + nodeIdStr;
           }
           if (infoIdStr.IndexOf(',') >= 0)
           {
               sql += " and CGS_ContentId in (" + infoIdStr + " )";
           }
           else
           {
               sql += " and CGS_ContentId=" + infoIdStr;
           }
           DataHelper.ExecuteNonQuery(sql);
       }
       private void addTag(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
       {
           //检查名称是否为空
           XNLParam tagNameParam;
           if (labelParams.TryGetValue("tagname", out tagNameParam))
           {
               if (tagNameParam.value.ToString().Trim().Equals(""))
               {
                   Dictionary<string, string> errorList = new Dictionary<string, string>();
                   throw (new Exception("tag名称不能为空!"));
               }
               tagNameParam.type = XNLType.String;
           }
           else
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("没有tagName属性!"));
           }
           if (tagNameParam.value.ToString().IndexOf(',') >= 0)
           {
               throw (new Exception("tag名称不允许含有\",\"字符!"));
           }
           //检查是否已存在相在名称
           int siteId = ManageUtil.getCurSiteID(XNLPage);
           object obj = DataHelper.ExecuteScalar("select Ct_id from sn_contenttag where Ct_name=@tagname and Ct_siteid=" + siteId.ToString(), labelParams);
           int groupId = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
           if (groupId > 0)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("此tag名称已存在!"));
           }
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction trans = dbConn.BeginTransaction();
           try
           {
               DbCommand command = DataHelper.GetSqlStringCommand("insert into sn_contenttag(Ct_Name,Ct_SiteId)values(@tagName," + siteId +")");
               DataHelper.SetParameterValue(command, labelParams);
               command.Connection = dbConn;
               command.Transaction = trans;
               DataHelper.ExecuteNonQuery(command);
               trans.Commit();
               trans.Dispose();
               dbConn.Close();
           }
           catch (Exception e)
           {
               trans.Rollback();
               trans.Dispose();
               dbConn.Close();
               throw (e);
           }
       }
       private void deleteTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
       {
           string tagIdStr = labelParams["tagid"].value.ToString().Trim();
           if (tagIdStr.Equals(""))
           {
               throw (new Exception("没有指定tag编号"));
           }
           if (!Regex.IsMatch(tagIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的tag编号"));
           }
           string sql1 = "delete from SN_ContentTag where CT_ID =" + tagIdStr;
           string sql2 = "delete from SN_ContentTags where CTS_ID=" + tagIdStr;
           if (tagIdStr.IndexOf(',') >= 0)
           {
               sql1 = "delete from SN_ContentTag where CT_ID in(" + tagIdStr + ")";
               sql2 = "delete from SN_ContentTags where CTS_ID in(" + tagIdStr + ")";
           }
           Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
           sqlColls.Add("1", sql1);
           sqlColls.Add("2", sql2);
           DbResultInfo dbInfo = DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
           if (!dbInfo.isSuccess)
           {
               throw (dbInfo.exception);
           }
       }
       private void modifyTag(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
       {
           //检查名称是否为空
           XNLParam tagNameParam;
           if (labelParams.TryGetValue("tagname", out tagNameParam))
           {
               if (tagNameParam.value.ToString().Trim().Equals(""))
               {
                   Dictionary<string, string> errorList = new Dictionary<string, string>();
                   throw (new Exception("tag名称不能为空!"));
               }
               tagNameParam.type = XNLType.String;
           }
           else
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("没有tagName属性!"));
           }
           if (tagNameParam.value.ToString().IndexOf(',') >= 0)
           {
               throw (new Exception("tag名称不允许含有\",\"字符!"));
           }
           //检查是否已存在相在名称
           int siteId = ManageUtil.getCurSiteID(XNLPage);
           object obj = DataHelper.ExecuteScalar("select Ct_id from sn_contenttag where Ct_name=@tagname and ct_id<>@tagid and Ct_siteid=" + siteId.ToString(), labelParams);
           int groupId = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
           if (groupId > 0)
           {
               Dictionary<string, string> errorList = new Dictionary<string, string>();
               throw (new Exception("此tag名称已存在!"));
           }
           DbConnection dbConn = DataHelper.CreateConnection();
           dbConn.Open();
           DbTransaction trans = dbConn.BeginTransaction();
           try
           {
               DbCommand command = DataHelper.GetSqlStringCommand("update sn_contenttag set Ct_Name=@tagName where ct_id=@tagid");
               DataHelper.SetParameterValue(command, labelParams);
               command.Connection = dbConn;
               command.Transaction = trans;
               DataHelper.ExecuteNonQuery(command);
               trans.Commit();
               trans.Dispose();
               dbConn.Close();
           }
           catch (Exception e)
           {
               trans.Rollback();
               trans.Dispose();
               dbConn.Close();
               throw (e);
           }
       }
       private void deleteTagInfo(Dictionary<string, XNLParam> labelParams, WebContext XNLPage)
       {
           string groupIdStr = labelParams["tagid"].value.ToString().Trim();
           if (groupIdStr.Equals(""))
           {
               throw (new Exception("没有指定内容tag编号"));
           }
           else if (!Regex.IsMatch(groupIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的内容组编号"));
           }
           string nodeIdStr = labelParams["nodeid"].value.ToString().Trim();
           if (nodeIdStr.Equals(""))
           {
               throw (new Exception("没有指定栏目编号"));
           }
           else if (!Regex.IsMatch(nodeIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的栏目编号"));
           }
           string infoIdStr = labelParams["infoid"].value.ToString().Trim();
           if (infoIdStr.Equals(""))
           {
               throw (new Exception("没有指定信息编号"));
           }
           else if (!Regex.IsMatch(infoIdStr.Replace(",", ""), "\\d+"))
           {
               throw (new Exception("错误的信息编号"));
           }
           string sql = "delete from SN_Contenttags where CTS_ID =" + groupIdStr + " and CTS_nodeId";
           if (nodeIdStr.IndexOf(',') >= 0)
           {
               sql += " in (" + nodeIdStr + " )";
           }
           else
           {
               sql += "=" + nodeIdStr;
           }
           if (infoIdStr.IndexOf(',') >= 0)
           {
               sql += " and CTS_ContentId in (" + infoIdStr + " )";
           }
           else
           {
               sql += " and CTS_ContentId=" + infoIdStr;
           }
          int delnum=DataHelper.ExecuteNonQuery(sql);
          DataHelper.ExecuteNonQuery("update sn_contenttag set ct_usenum=ct_usenum-" + delnum.ToString() + " where ct_id=" + groupIdStr);
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
