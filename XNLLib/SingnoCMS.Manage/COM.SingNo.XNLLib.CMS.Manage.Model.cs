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
using System.Xml;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public  class Model:IXNLTag<WebContext>
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
              labelContentStr=RegxpEngineCommon.replaceAttribleVariable(labelParams,labelContentStr);
              MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Model.Success");
              MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Model.Error");
              string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
              try
              {
                  if (actionStr.Equals("add"))
                  {
                      modelAdd(labelParams, labelContentStr);
                  }
                  else if (actionStr.Equals("modify"))
                  {
                      modelModify(labelParams, labelContentStr);
                  }
                  else if (actionStr.Equals("delete"))
                  {
                      modelDelete(labelParams, labelContentStr);
                  }
                  else if (actionStr.Equals("checkmodeltable"))
                  {
                      return checkModelTable(labelParams).ToString();
                  }
                  else if (actionStr.Equals("checkmodel"))
                  {
                      return checkModelName(labelParams).ToString();
                  }
                  else if (actionStr.Equals("addfield"))
                  {
                      modelAddField(labelParams, labelContentStr,XNLPage);
                  }
                  else if (actionStr.Equals("setstyle"))
                  {
                       modelSetStyle(labelParams, labelContentStr);
                  }
                  else if (actionStr.Equals("delfield"))
                  {
                      modelDelField(labelParams, labelContentStr);
                  }
                  else if (actionStr.Equals("setvalidator"))
                  {
                      setValidator(labelParams, labelContentStr);
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
        #endregion
        private void modelAdd(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            if (checkModelName(labelParams))
            {
                throw (new Exception("此模型名称已存在!"));
            }
                //检测是否已经有此表名
                if (checkModelTable(labelParams))
                {
                    throw (new Exception("此模型表名已存在!"));
                }
                string tabelname = labelParams["tablename"].value.ToString();
                tabelname = "SN_U_"+tabelname;
                labelParams["tablename"].value = tabelname;
                string tableXML = ManageUtil.getTableXML(tabelname).Replace("@tableName", tabelname);
                List<string> sqlList = DataHelper.GetTableCreateSql(tableXML);
                Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                foreach (string sql in sqlList)
                {
                    sqlColls.Add(sqlColls.Count.ToString(), sql);
                }
                string addModelSql = "insert into SN_Model(ModelName,TableName,ItemName,ItemUnit,ItemIcon,Description)values(@ModelName,@TableName,@ItemName,@ItemUnit,@ModelIcon,@Description)";
                sqlColls.Add(sqlColls.Count.ToString(), addModelSql);
                sqlColls.Add(sqlColls.Count.ToString(), "select ModelID from SN_Model where ModelName=@ModelName");
                string resultId = (sqlColls.Count-1).ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(tableXML);
                XmlNodeList fieldList = xmlDoc.SelectNodes("/Root/Table/Field");
                int i = 1;
                foreach (XmlNode node in fieldList)
                {
                    string FieldName = node.Attributes["Name"].Value;
                    string DataType = node.Attributes["DataType"].Value;
                    string DataLength = node.Attributes["DataLength"].Value;
                    string DefaultValue = node.Attributes["DefaultValue"].Value;
                    string DisplayName = node.Attributes["DisplayName"].Value;
                    string IndexId = i.ToString();
                    string IsSystem = node.Attributes["IsSystem"].Value;
                    string Description = node.Attributes["Description"].Value;
                    string HelpText = node.Attributes["HelpText"].Value;
                    string IsVisible = node.Attributes["IsVisible"].Value;
                    string IsValidator = node.Attributes["IsValidator"].Value;
                    string isShowOnList = node.Attributes["isShowOnList"].Value;
                    string InputType = node.Attributes["InputType"].Value;
                    string InputTypeSet = node.SelectSingleNode("InputTypeSet").OuterXml;
                    string ValidatorSet = node.SelectSingleNode("ValidatorSet").OuterXml;
                    StringBuilder sb = new StringBuilder("insert into SN_ModelDescript(ModelName,FieldName,DataType,DataLength,DefaultValue,DisplayName,IndexId,IsSystem,Description,HelpText,IsVisible,isShowOnList,IsValidator,InputType,InputTypeSet,validatorSet) values(@ModelName,'");
                    sb.Append(FieldName.Replace("'","''"));
                    sb.Append("','");
                    sb.Append(DataType.Replace("'", "''"));
                    sb.Append("',");
                    sb.Append(DataLength.Replace("'", "''"));
                    sb.Append(",'");
                    sb.Append(DefaultValue.Replace("'", "''"));
                    sb.Append("','");
                    sb.Append(DisplayName.Replace("'","''"));
                    sb.Append("',");
                    sb.Append(IndexId);
                    sb.Append(",");
                    sb.Append(IsSystem.Replace("'", "''"));
                    sb.Append(",'");
                    sb.Append(Description.Replace("'", "''"));
                    sb.Append("','");
                    sb.Append(HelpText.Replace("'", "''"));
                    sb.Append("',");
                    sb.Append(IsVisible.Replace("'", "''"));
                    sb.Append(",");
                    sb.Append(isShowOnList.Replace("'", "''"));
                    sb.Append(",");
                    sb.Append(IsValidator.Replace("'", "''"));
                    sb.Append(",'");
                    sb.Append(InputType.Replace("'", "''"));
                    sb.Append("','");
                    sb.Append(InputTypeSet.Replace("'", "''"));
                    sb.Append("','");
                    sb.Append(ValidatorSet.Replace("'", "''"));
                    sb.Append("')");
                    sqlColls.Add(sqlColls.Count.ToString(), sb.ToString());
                    i = i + 1;
                }
                DbResultInfo dbResultInfo=DataHelper.ExrcuteScalarSomeSqlWithTransaction(sqlColls, labelParams);
                if (!dbResultInfo.isSuccess)
                {
                    throw (new Exception(dbResultInfo.exception.Message));
                }
                else
                {
                    //添加model进缓存
                    DataModel model = new DataModel();
                    model.ModelName = labelParams["modelname"].value.ToString();
                    model.ModelId = Convert.ToInt32(labelParams["_dbresult" + resultId].value);
                    model.ItemIcon = labelParams["modelicon"].value.ToString();
                    model.ItemName = labelParams["itemname"].value.ToString();
                    model.ItemUnit = labelParams["itemunit"].value.ToString();
                    model.State = 1;
                    model.TableName = labelParams["tablename"].value.ToString();
                    //model.UseNumber = 0;
                    DataModelManager.createInstance().addModel(model.ModelId, model);
                }
        }
        private void modelModify(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            if (checkModelName(labelParams))
            {
                throw (new Exception("此模型名称已存在!"));
            }
            Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
            sqlColls.Add("1", "update SN_Model set ModelName=@ModelName,ItemName=@ItemName,ItemUnit=@ItemUnit,ItemIcon=@ModelIcon,Description=@Description where modelid=@modelid");
            sqlColls.Add("2", "update SN_ModelDescript set ModelName=@ModelName where ModelName=@srcModelName");
            DbResultInfo dbResultInfo = DataHelper.ExrcuteScalarSomeSqlWithTransaction(sqlColls, labelParams);
            if (!dbResultInfo.isSuccess)
            {
                throw (new Exception(dbResultInfo.exception.Message));
            }
            DataModel model = DataModelManager.createInstance().getModel(Convert.ToInt32(labelParams["modelid"].value));
            model.ModelName = labelParams["modelname"].value.ToString();
            model.ItemIcon = labelParams["modelicon"].value.ToString();
            model.ItemName = labelParams["itemname"].value.ToString();
            model.ItemUnit = labelParams["itemunit"].value.ToString();
            model.State = 1;
        }
        private void modelDelete(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            DbDataReader dr = DataHelper.ExecuteReader("select TableName,modelname,UseNumber,ModelStyle from sn_model where modelid=@modelid", labelParams);
            string modelTableName = string.Empty;
            string modelName = string.Empty;
            int UseNumber = 0;
            int ModelStyle = 0;
            int modelId=0;
            if (dr.HasRows)
            {
                dr.Read();
                modelTableName=dr["tablename"].ToString();
                modelName = dr["modelname"].ToString();
                UseNumber = Convert.ToInt32(dr["UseNumber"]);
                ModelStyle = Convert.ToInt32(dr["ModelStyle"]);
                modelId = Convert.ToInt32(labelParams["modelid"].value);
            }
            dr.Close();
            dr.Dispose();
            if (modelTableName.Equals(string.Empty))
            {
                throw (new Exception("没有找到要删除的模型！"));
            }
            if (ModelStyle == 0)
            {
                throw (new Exception("系统模型，不允许删除！")); 
            }
            if (UseNumber>0)
            {
                throw (new Exception("模型使用中，不允许删除！"));
            }
           
            labelParams.Add("modelname",new XNLParam(modelName));
            Dictionary<string, string> sqlColls = new Dictionary<string, string>();
            string delModelStr = "delete from sn_model where ModelID=@modelid";
            string delModelMateStr = "delete from SN_ModelDescript where ModelName=@modelName";
            string dropModelTableStr = "drop table "+modelTableName;
            // DROP   TABLE   table_name
            sqlColls.Add("delmodel", delModelStr);
            sqlColls.Add("delmodelmate", delModelMateStr);
            sqlColls.Add("delmodeltable", dropModelTableStr);
            DbResultInfo dbResultInfo = DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
            if (dbResultInfo.isSuccess)
            {
                //删除模型缓存
                DataModelManager.createInstance().removeModel(modelId);
                //modelId 
            }
            else
            {
                throw (new Exception(dbResultInfo.exception.Message));    
            }
        }
        private void modelAddField(Dictionary<string, XNLParam> labelParams, string labelContentStr,XNLContext XNLPage)
        {
            string tableNameSql = "select TableName,ModelID from sn_model where ModelName=@ModelName";
            string tableName = string.Empty;
            int modelId = 0;
            DataTable dt = DataHelper.ExecuteDataTable(tableNameSql, labelParams);
            if (dt.Rows.Count > 0) 
            {
                tableName = dt.Rows[0]["tableName"].ToString();
                modelId = Convert.ToInt32(dt.Rows[0]["ModelID"]);
                labelParams.Add("tablename", new XNLParam(tableName));
                DataTable indexDt = DataHelper.ExecuteDataTable("select max(IndexId) as nextindex from SN_ModelDescript where ModelName=@ModelName", labelParams);
                int nextIndex = 1;
                if (indexDt.Rows.Count > 0)
                {
                    nextIndex = Convert.ToInt32(indexDt.Rows[0]["nextindex"])+1;
                }
                string name = labelParams["fieldname"].value.ToString();
                string DataType = labelParams["datatype"].value.ToString();
                string dataLength = labelParams["datalength"].value.ToString();
                string DefaultValue = labelParams["defaultvalue"].value.ToString();
                labelParams.Add("indexid", new XNLParam( XNLType.Int32, nextIndex));
                StringBuilder fieldXML = new StringBuilder();
                fieldXML.Append("<Root><Table Name=\"@tableName\">".Replace("@tableName", tableName));
                fieldXML.Append("<Field Name=\"@Name\" DataType=\"@DataType\" DataLength=\"@DataLength\" DefaultValue=\"@DefaultValue\" IsNull=\"Y\"  DecimalLen=\"@DecimalLen\"></Field></Table></Root>");
                fieldXML.Replace("@Name", name);
                fieldXML.Replace("@DataType", DataType);          
                labelParams.Add("default", new XNLParam( XNLType.String, DefaultValue));
                string dataType=labelParams["datatype"].value.ToString();
                switch (dataType)
                {
                    case "Boolean":
                        labelParams["datalength"].value = 1;
                        dataLength = "1";
                        if (DefaultValue.Trim().Equals(string.Empty))
                        {
                            labelParams["defaultvalue"].value = 0;
                            labelParams["default"].value = 0;
                            DefaultValue = "0";
                        }
                        else 
                        {
                            int t;
                            if (!Int32.TryParse(DefaultValue, out t))
                            {
                                labelParams["defaultvalue"].value = 0;
                                labelParams["default"].value = 0;
                                DefaultValue = "0";
                            }
                        }
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                    case "Integer":
                        labelParams["datalength"].value = 18;
                        if (DefaultValue.Trim().Equals(string.Empty))
                        {
                            labelParams["defaultvalue"].value = 0;
                            labelParams["default"].value = 0;
                            DefaultValue = "0";
                        }
                        else
                        {
                            int t;
                            if (!Int32.TryParse(DefaultValue, out t))
                            {
                                labelParams["defaultvalue"].value = 0;
                                labelParams["default"].value = 0;
                                DefaultValue = "0";
                            }
                        }
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                    case "Decimal":
                        labelParams["datalength"].value = 18;
                        if (DefaultValue.Trim().Equals(string.Empty))
                        {
                            labelParams["defaultvalue"].value = 0;
                            labelParams["default"].value = 0;
                            DefaultValue = "0";
                        }
                        else
                        {
                            Decimal t;
                            if (!Decimal.TryParse(DefaultValue, out t))
                            {
                                labelParams["defaultvalue"].value = 0;
                                labelParams["default"].value = 0;
                                DefaultValue = "0";
                            }
                        }
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        fieldXML.Replace("@DecimalLen", labelParams["decimallen"].value.ToString());
                        break;
                    case "DateTime":
                        labelParams["datalength"].value = 20;
                        dataLength = "8";
                        if (DefaultValue.Trim().Equals(string.Empty))
                        {
                            labelParams["defaultvalue"].value = "[Current]";
                            DefaultValue = "[Current]";
                            labelParams["default"].value = DateTime.Now;
                        }
                        else
                        {
                            DateTime t;
                            if (!DateTime.TryParse(DefaultValue, out t))
                            {
                                labelParams["defaultvalue"].value = "[Current]";
                                DefaultValue = "[Current]";
                                labelParams["default"].value = DateTime.Now;
                            }
                        }
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                    case "Money":
                        labelParams["datalength"].value = 18;
                        if (DefaultValue.Trim().Equals(string.Empty))
                        {
                            labelParams["defaultvalue"].value = 0;
                            labelParams["default"].value = 0;
                            DefaultValue = "0";
                        }
                        else
                        {
                            double t;
                            if (!double.TryParse(DefaultValue, out t))
                            {
                                labelParams["defaultvalue"].value = 0;
                                labelParams["default"].value = 0;
                                DefaultValue = "0";
                            }
                        }
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                    case "NText":
                        labelParams["datalength"].value = 16;
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                    case "NVarChar":
                        fieldXML.Replace("@DataLength", dataLength);
                        fieldXML.Replace("@DefaultValue", DefaultValue);
                        break;
                }
                List<string> sqlList = DataHelper.GetFieldCreateSql(fieldXML.ToString());
                Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                foreach (string str in sqlList)
                {
                    sqlColls.Add(sqlColls.Count.ToString(), str);
                }
                labelParams.Add("displayname", labelParams["fieldname"]);
                labelParams.Add("description", labelParams["fieldname"]);
                labelParams.Add("helptext", labelParams["fieldname"]);
                sqlColls.Add(sqlColls.Count.ToString(), "insert into SN_ModelDescript(ModelName,FieldName,DataType,DataLength,IsValidator,DefaultValue,DisplayName,IndexId,IsSystem,Description,HelpText,IsVisible,isShowOnList,InputType,InputTypeSet,validatorSet) values(@ModelName,@fieldName,@DataType,@dataLength,0,@defaultvalue,@DisplayName,@indexid,0,@Description,@HelpText,0,0,'Text','<InputTypeSet readonly=\"false\" style=\"\" width=\"0\" height=\"0\"><value></value></InputTypeSet>','<ValidatorSet required=\"false\"><regexValidator><error></error><regexp></regexp></regexValidator></ValidatorSet>')");
               // sqlColls.Add(sqlColls.Count.ToString(), "update " + tableName + " set " + name + "=@default");
                DbResultInfo dbResultInfo = DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
                if (dbResultInfo.isSuccess)
                {
                    ModelField f = new ModelField();
                    f.FieldName = name;
                    DataModelManager.createInstance().getModel(modelId).addField(f);
                }
                else
                {
                    throw (new Exception(dbResultInfo.exception.Message));
                }
            }
            else
            {
                throw (new Exception("参数错误,没有此模型名称:"+labelParams["modelname"].value.ToString()+"!"));
            }
            dt.Dispose();
        }
        private bool checkModelTable(Dictionary<string, XNLParam> labelParams)
        {
            bool isHave = false;
            string type = "add";
            XNLParam typeParam;
            if (labelParams.TryGetValue("type", out typeParam))
            {
                type = typeParam.value.ToString();
            }
            if (type.Equals(""))type="add";
            string sql = "select modelid from SN_Model where TableName='SN_U_'+@TableName";
            if (type.Equals("modify")) sql = "select modelid from SN_Model where TableName='SN_U_'+@TableName and ModelID<>@ModelID";
            object dataObj = DataHelper.ExecuteScalar(sql, labelParams);
            int modelId = Convert.ToInt32(Convert.IsDBNull(dataObj) ? 0 : dataObj);
            if (modelId > 0) isHave = true;
            return isHave;
        }
        private bool checkModelName(Dictionary<string, XNLParam> labelParams)
        {
            bool isHave = false;
            string type = "add";
            XNLParam typeParam;
            if(labelParams.TryGetValue("type",out typeParam))
            {
                type =typeParam.value.ToString();
            }
            if (type.Equals(""))type = "add";
            string sql = "select modelid from SN_Model where ModelName=@modelName";
            if (type.Equals("modify")) sql ="select Modelid from SN_Model where ModelName=@modelName and ModelID<>@ModelID";
            object dataObj = DataHelper.ExecuteScalar(sql, labelParams);
            int modelId = Convert.ToInt32(Convert.IsDBNull(dataObj) ? 0 : dataObj);
            if (modelId > 0) isHave = true;
            return isHave;
        }
        private void modelSetStyle(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            labelParams.Add("inputtypeset", new XNLParam( XNLType.String, HttpContext.Current.Request.Form["InputTypeSet"].ToString()));
            string sql = "update SN_ModelDescript set DisplayName=@DisplayName,helpText=@helpText,isVisible=@isVisible,isshowonlist=@isshowonlist,inputType=@inputType,InputTypeSet=@InputTypeSet where DescriptId=@id";
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            DbCommand dbCommand = DataHelper.GetSqlStringCommand(sql);
            dbCommand.Connection = dbConn;
            dbCommand.Transaction = transaction;
            DataHelper.SetParameterValue(dbCommand, labelParams);
            try
            {
                DataHelper.ExecuteNonQuery(dbCommand);
                transaction.Commit();
                transaction.Dispose();
                dbConn.Close();
                //dbConn.Dispose();
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
        private void modelDelField(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            DbDataReader dr= DataHelper.ExecuteReader("select ModelName,FieldName from SN_ModelDescript where DescriptId=@DescriptId", labelParams);
            if (dr.HasRows)
            {
                dr.Read();
                string modelName = Convert.ToString(dr["ModelName"]);
                string FieldName = Convert.ToString(dr["FieldName"]);
                dr.Close();
                dr.Dispose();
                labelParams.Add("modelname", new XNLParam( XNLType.String, modelName));
                DbDataReader dr2 = DataHelper.ExecuteReader("select ModelID,TableName from SN_Model where ModelName=@ModelName", labelParams);
                string tableName = string.Empty;
                int modelId = 0;
                if (dr2.HasRows)
                {
                    dr2.Read();
                    tableName = Convert.ToString(dr2["TableName"]);
                    modelId = Convert.ToInt32(dr2["ModelID"]);
                    dr2.Close();
                    dr2.Dispose();
                }
                else
                {
                    dr2.Close();
                    dr2.Dispose();
                    throw (new Exception("此字段已不存在!"));
                }
                string delSql = DataHelper.GetFieldDelSql(tableName, FieldName);
                Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                sqlColls.Add(sqlColls.Count.ToString(), delSql);
                sqlColls.Add(sqlColls.Count.ToString(), "delete  from SN_ModelDescript where DescriptId=@DescriptId");
                DbResultInfo dbInfo = DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
                if (!dbInfo.isSuccess)
                {
                    throw (dbInfo.exception);
                }
                DataModelManager.createInstance().getModel(modelId).removeByName(FieldName);
            }
            else
            {
                dr.Close();
                dr.Dispose();
                throw (new Exception("此字段已不存在!"));
            }
        }
        private void setValidator(Dictionary<string, XNLParam> labelParams, string labelContentStr)
        {
            var regexpStr = HttpContext.Current.Request.Form["regexp"];
            string regexpType = labelParams["regexptype"].value.ToString();
            if (string.Compare(regexpType, "user", true) == 0) regexpStr = HttpContext.Current.Request.Form["userRegexp"];
            string validatorSet = "<ValidatorSet required=\"" + labelParams["required"].value.ToString() + "\"><regexValidator><error><![CDATA[" + HttpContext.Current.Request.Form["error"] + "]]></error><regexp><![CDATA[" + regexpStr + "]]></regexp></regexValidator></ValidatorSet>";
            labelParams.Add("validatorset", new XNLParam(XNLType.String, validatorSet));
            string sql = "update SN_ModelDescript set IsValidator=@IsValidator, ValidatorSet=@validatorset where DescriptId=@id";
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction transaction = dbConn.BeginTransaction();
            DbCommand dbCommand = DataHelper.GetSqlStringCommand(sql);
            dbCommand.Connection = dbConn;
            dbCommand.Transaction = transaction;
            DataHelper.SetParameterValue(dbCommand, labelParams);
            try
            {
                DataHelper.ExecuteNonQuery(dbCommand);
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
