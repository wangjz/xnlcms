using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Collections.Generic;
using COM.SingNo.XNLCore;
using System.Text.RegularExpressions;
using System.Xml;
using System.Text;
namespace COM.SingNo.DAL
{
    /// <summary>
    /// 针对OleDb连接的工厂
    /// </summary>
    public class AccessFactory : IDataBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AccessFactory()
        {
        }
        /// <summary>
        /// 建立默认Connection对象
        /// </summary>
        /// <returns>Connection对象</returns>
        public DbConnection CreateConnection()
        {
            return new OleDbConnection(GetConnectionString());
        }
        /// <summary>
        /// 根据连接字符串建立Connection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns>Connection对象</returns>
        public DbConnection CreateConnection(string strConn)
        {
            return new OleDbConnection(strConn);
        }
        /// <summary>
        /// 建立Command对象
        /// </summary>
        /// <returns>Command对象</returns>
        public DbCommand CreateCommand()
        {
            return new OleDbCommand();
        }
        /// <summary>
        /// 建立DataAdapter对象
        /// </summary>
        /// <returns>DataAdapter对象</returns>
        public DbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }
        /// <summary>
        /// 根据Connection建立Transaction
        /// </summary>
        /// <param name="myDbConnection">Connection对象</param>
        /// <returns>Transaction对象</returns>
        public DbTransaction CreateTransaction(DbConnection myDbConnection)
        {
            return myDbConnection.BeginTransaction();
        }
        /// <summary>
        /// 根据Command建立DataReader
        /// </summary>
        /// <param name="myDbCommand">Command对象</param>
        /// <returns>DataReader对象</returns>
        public DbDataReader CreateDataReader(DbCommand myDbCommand)
        {
            return myDbCommand.ExecuteReader();
        }
      /// <summary>
        ///  根据Command建立DataReader
      /// </summary>
        /// <param name="myDbCommand">Command对象</param>
        /// <param name="cmdBehavior">CommandBehavior类型</param>
        /// <returns>DataReader对象</returns>
        public DbDataReader CreateDataReader(DbCommand myDbCommand,CommandBehavior cmdBehavior)
        {
            return myDbCommand.ExecuteReader(cmdBehavior);
        }
        /// <summary>
        /// 获得连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public string GetConnectionString()
        {
            return DataBaseFactory.connectionColls["access"];
        }
        #region IDataBase 成员
        public DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            throw new Exception("Access数据库不支持存储过程。");
        }

        public DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            throw new Exception("Access数据库不支持存储过程。");
        }

        public object GetParameterValue(DbCommand command, string name)
        {
            return command.Parameters[name];
        }
        public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
                DbParameter param = command.CreateParameter();
                param.ParameterName = name;
                param.DbType = dbType;
                param.Value = value;
                command.Parameters.Add(param);
        }
        public void AddInParameter(DbCommand command, string name, DbType dbType)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            command.Parameters.Add(param);
        }
        public void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
            command.Parameters.Add(param);
        }
        public void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Size = size;
            param.Direction = ParameterDirection.Output;
            command.Parameters.Add(param);
        }
        public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Direction = direction;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
            param.Value = value;
            command.Parameters.Add(param);
        }
        public void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OleDbParameter param = (OleDbParameter)command.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Size = size;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.Scale = scale;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
            param.Value = value;
            command.Parameters.Add(param);
        }
        #endregion
        #region IDataBase 成员
        public DataTable GetSchemaTable(DbCommand command, CommandBehavior cmdBehavior)
        {
            DbDataReader dr= CreateDataReader(command, cmdBehavior);
            DataTable dt= dr.GetSchemaTable();
            dr.Close();
            return dt;
        }
        public DataTable GetDataTableBySqlWithPage(DbCommand command, Dictionary<string, XNLParam> pageInfoparams)
        {
            string sqlStr = command.CommandText;
            string firstIndexSql = sqlStr+" ";  //前面的sql 
            string indexSql = firstIndexSql;  //后面的sql
            int scrSqlTopNum = 0;
            string tableName = "";
            string fieldsStr = "";
            bool isHasTop = false;
            Match sqlMatch = Regex.Match(sqlStr, "^[\\s]*select[\\s]+(top)[\\s]+([\\d]+)[\\s]+(.+?)[\\s]+from[\\s]+(.+?)$|^[\\s]*select[\\s]+(.+?)[\\s]+from[\\s]+(.+?)$", DALUtils.MatchOptions);
            if (sqlMatch.Success)
            {
                if (sqlMatch.Groups[1].Value.Trim().ToLower().Equals("top"))
                {
                    isHasTop = true;
                    scrSqlTopNum = Convert.ToInt32(sqlMatch.Groups[2].Value);
                    firstIndexSql = DALUtils.onceReplace(firstIndexSql, sqlMatch.Groups[2].Value, " <sqlTopNum> ");
                    indexSql = DALUtils.onceReplace(indexSql, sqlMatch.Groups[2].Value, " <sqlTopNum> ");
                    indexSql = DALUtils.onceReplace(indexSql, sqlMatch.Groups[3].Value, " * ");
                    fieldsStr = sqlMatch.Groups[3].Value;
                    tableName=sqlMatch.Groups[4].Value.Split(new char[1]{' '},StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else
                {
                    firstIndexSql = DALUtils.onceReplace(firstIndexSql, sqlMatch.Groups[5].Value, " top <sqlTopNum> " + sqlMatch.Groups[5].Value + " ");
                    indexSql = DALUtils.onceReplace(indexSql, sqlMatch.Groups[5].Value, " top <sqlTopNum> " + " * ");
                    fieldsStr = sqlMatch.Groups[5].Value;
                    tableName = sqlMatch.Groups[6].Value.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                }
            }
            else
            {
                throw new Exception("sql 语句格式不正确。");
            }
            int totalNums = Convert.ToInt32(pageInfoparams["totalrecordsnum"].value);
            int perPageRecordsNum = Convert.ToInt32(pageInfoparams["perpagerecordsnum"].value);
            DataTable dt;
            if (totalNums <= perPageRecordsNum)
            {
                DataHelper.SetParameterValue(command, pageInfoparams);
                dt=DataHelper.ExecuteDataTable(command);
                return dt;
            }
            int tmpCurPageNum = Convert.ToInt32(pageInfoparams["curpagenum"].value);
            if (tmpCurPageNum<=0)
            {
                tmpCurPageNum = 1;
                pageInfoparams["curpagenum"].value = 1;
            }
            if (tmpCurPageNum == 1)
            {
                command.CommandText = firstIndexSql.Replace("<sqlTopNum>", perPageRecordsNum.ToString());
                DataHelper.SetParameterValue(command, pageInfoparams);
                dt = DataHelper.ExecuteDataTable(command);
                return dt;
            }
            string sqlMatchReg = "\\(.+?\\)";
            MatchCollection sqlReplaceMatchs = Regex.Matches(sqlStr, sqlMatchReg);
            int tmpNum = 0;
            foreach (Match i in sqlReplaceMatchs)
            {
                firstIndexSql = firstIndexSql.Replace(i.Groups[0].Value, "<sqlmatchreplace>" + tmpNum + "</sqlmatchreplace>");
                indexSql = indexSql.Replace(i.Groups[0].Value, "<sqlmatchreplace>" + tmpNum + "</sqlmatchreplace>");
                tmpNum += 1;
            }
            int indexNum = tmpCurPageNum* perPageRecordsNum;
            int firstIndexNum = perPageRecordsNum;
            string primaryKeyStr = pageInfoparams["primarykey"].value.ToString();
            #region  设置反序语句

            Match sortTypeMatch = Regex.Match(firstIndexSql, "order[\\s]+by[\\s]+(.+)", DALUtils.MatchOptions);
            string reverseSortSql = indexSql;
            if (sortTypeMatch.Success) //有排序语句
            {
                //设置反序
                string ordersStr = sortTypeMatch.Groups[1].Value;
                string[] orderField_arr = ordersStr.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] reverseField_arr = new string[orderField_arr.Length];
                for (int i = 0; i < orderField_arr.Length; i++)
                {
                    string[] order_arr = orderField_arr[i].Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (i < orderField_arr.Length)
                    {
                        if (order_arr.Length == 1)
                        {
                            reverseField_arr[i] = order_arr[0] + " desc ";
                        }
                        else if (order_arr.Length == 2)
                        {
                            if (string.Compare(order_arr[1], "desc", true) == 0) //desc
                            {
                                reverseField_arr[i] = order_arr[0] + " asc ";
                            }
                            else  //asc
                            {
                                reverseField_arr[i] = order_arr[0] + " desc ";
                            }
                        }
                    }
                    else
                    {
                        if (order_arr.Length == 1)
                        {
                            reverseField_arr[i] = order_arr[0] + " desc ";
                        }
                        else
                        {

                            if (string.Compare(order_arr[1], "desc", true) == 0) //desc
                            {
                                reverseField_arr[i] = order_arr[0] + " asc ";
                            }
                            else  //asc
                            {
                                reverseField_arr[i] = order_arr[0] + " desc ";
                            }
                            if (order_arr.Length > 2)
                            {
                                for (int t = 2; t < order_arr.Length; t++)
                                {
                                    reverseField_arr[i] += (" " + order_arr[t] + " ");
                                }
                            }
                        }
                    }
                }
                string _reverseSortStr = "";
                for (int _t = 0; _t < reverseField_arr.Length; _t++)
                {
                    _reverseSortStr += (_t == 0 ? "" : ",") + reverseField_arr[_t];
                }
                reverseSortSql = DALUtils.onceReplace(reverseSortSql, ordersStr, _reverseSortStr);
            }
            else
            {
                reverseSortSql += " order by [" + primaryKeyStr + "] desc";
                firstIndexSql += " order by [" + primaryKeyStr + "] ";
            }
            tableName = " " + tableName + " ";
            string finishSql = "";
            if(isHasTop)
            {
                string allSql = indexSql;
                allSql = allSql.Replace("<sqlTopNum>", indexNum.ToString());
                string reverseAllSql = reverseSortSql.Replace("<sqlTopNum>", firstIndexNum.ToString()); //最里层的反序sql
                reverseAllSql = DALUtils.onceReplace(reverseAllSql, tableName, " (" + allSql + ") ");
                finishSql = firstIndexSql.Replace("<sqlTopNum>", firstIndexNum.ToString());
                finishSql = DALUtils.onceReplace(finishSql, tableName, " (" + reverseAllSql + ") ");
            }
            else
            {
                int totalPagesNum = (totalNums / perPageRecordsNum) + ((totalNums % perPageRecordsNum) > 0 ? 1 : 0);
                int centerPageNum = (int)(totalPagesNum / 2);
                if (tmpCurPageNum >= centerPageNum)
                {
                    indexNum = (totalPagesNum - tmpCurPageNum + 1) * perPageRecordsNum;
                    string reverseAllSql = reverseSortSql.Replace("<sqlTopNum>", indexNum.ToString()); //最里层的反序sql
                    finishSql = firstIndexSql.Replace("<sqlTopNum>", firstIndexNum.ToString());
                    finishSql = DALUtils.onceReplace(finishSql, tableName, " (" + reverseAllSql + ") ");
                }
                else
                {
                    string allSql = indexSql;
                    allSql = allSql.Replace("<sqlTopNum>", indexNum.ToString());
                    string reverseAllSql = reverseSortSql.Replace("<sqlTopNum>", firstIndexNum.ToString()); //最里层的反序sql
                    reverseAllSql = DALUtils.onceReplace(reverseAllSql, tableName, " (" + allSql + ") ");
                    finishSql = firstIndexSql.Replace("<sqlTopNum>", firstIndexNum.ToString());
                    finishSql = DALUtils.onceReplace(finishSql, tableName, " (" + reverseAllSql + ") ");
                }
            }
            #endregion
            for (int i = 0; i < sqlReplaceMatchs.Count; i++)
            {
                finishSql = finishSql.Replace("<sqlmatchreplace>" + i + "</sqlmatchreplace>", sqlReplaceMatchs[i].Groups[0].Value);
            }
            command.CommandText = finishSql;
            DataHelper.SetParameterValue(command, pageInfoparams);
            dt = DataHelper.ExecuteDataTable(command);
            return dt;
        }
        public string ModifySqlToCountSql(string strSql, string strPrimaryKey)
        {
            string strCountSql = string.Empty;
            strCountSql = "select count(1) from (" + strSql + ")";
            return strCountSql;
            /*
            Match sqlMatch = Regex.Match(strSql, "^[\\s]*select[\\s]+(top)[\\s]+[\\d]+[\\s]+.+?[\\s]+from[\\s]+.+?$|^[\\s]*select[\\s]+(.+?)[\\s]+from[\\s]+.+?$", DALUtils.MatchOptions);
            if (sqlMatch.Success)
            {
                string strCountSql = string.Empty;
                if (string.Compare(sqlMatch.Groups[1].Value,"top",true)==0)
                {
                    strCountSql = "select count(1) from (" + strSql + ")";
                }
                else
                {
                    strCountSql = DALUtils.onceReplace(strSql, sqlMatch.Groups[2].Value, " count(1) ");
                }
                return strCountSql;
            }
            else
            {
                throw new Exception("sql 语句格式不正确。");
            }
             */ 
        }
        public string GetKeyInfo(DbCommand command)
        {
            Match sqlMatch = Regex.Match(command.CommandText, "^[\\s]*select[\\s]+(.+?)[\\s]+from[\\s]+.+?$", DALUtils.MatchOptions);
            if (sqlMatch.Success)
            {
                command.CommandText = DALUtils.onceReplace(command.CommandText, sqlMatch.Groups[1].Value, " * ");
                DataTable dt = GetSchemaTable(command, CommandBehavior.KeyInfo);
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToBoolean(row["iskey"]) && Convert.ToBoolean(row["isautoincrement"]))
                    {
                        dt.Dispose();
                        return row["columnname"].ToString();
                    }
                }
                dt.Dispose();
            }
            else
            {
                throw new Exception("sql 语句格式不正确。");
            }

            return "";
        }
        #endregion

        #region IDataBase 成员

        public List<string> GetTableCreateSql(string tableXMLStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(tableXMLStr);
            string tableName = xmlDoc.SelectSingleNode("/Root/Table/@Name").InnerText;
            StringBuilder sb = new StringBuilder("CREATE TABLE [@TableName]([ID] counter NOT NULL ,[IndexID] integer NOT NULL DEFAULT 0 ,[NodeID] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[InputUser] text (255) NOT NULL DEFAULT \"\" ,");
            sb.Append("[LastEditUser] text (255) NOT NULL DEFAULT \"\" ,");
            sb.Append("[LastEditDate] datetime NOT NULL DEFAULT now() ,");
            sb.Append("[CreateTime] datetime NOT NULL DEFAULT now() ,");
            sb.Append("[PassedTime] datetime NOT NULL DEFAULT now() ,");
            sb.Append("[State] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[LastHitTime] datetime NOT NULL DEFAULT now() ,");
            sb.Append("[Hits] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[Dayhits] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[WeekHits] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[MonthHits] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[ReadPoint] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[PinYinTitle] text (255) NOT NULL DEFAULT \"\" ,");
            sb.Append("[PYTitle] text (255) NOT NULL DEFAULT \"\" ,");
            //sb.Append("[Settings] memo NOT NULL DEFAULT \"\",");
            sb.Append("[Tag] text(255) NOT NULL DEFAULT \"\",");
            sb.Append("[IsComment] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[IsRecycle] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[Bold] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[Italic] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[UnderLine] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[TitleColor] text (7) NOT NULL DEFAULT \"#000000\" ,");
            sb.Append("[PageType] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[PageWords] integer NOT NULL DEFAULT 0 ,");
            sb.Append("[IsRecommend] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[IsHot] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[IsColor] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[IsTop] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[stars] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[diggs] TINYINT  NOT NULL DEFAULT 0 ,");
            sb.Append("[comments] TINYINT  NOT NULL DEFAULT 0 ,");
            XmlNodeList list = xmlDoc.SelectNodes("/Root/Table/Field");
            foreach( XmlNode node in list)
            {
                string str;
                str = "[@name] @type @length @isnull @default ,";
                string name = node.Attributes["Name"].Value;
                string length = node.Attributes["DataLength"].Value;
                string isnull = node.Attributes["IsNull"].Value=="N"?"NOT NULL":"";
                string defa = node.Attributes["DefaultValue"].Value;
                string type = "";
                str = str.Replace("@name", name);
                str = str.Replace("@isnull", isnull);
                string defaultStr = "";
                switch (node.Attributes["DataType"].Value)
                {
                    case "NVarChar":
                        type = "text";
                        str = str.Replace("@type", type);
                        str = str.Replace("@length", "("+length+")");
                        defaultStr = "DEFAULT \""+defa+"\"";
                        break;
                    case "NText":
                        type = "memo";
                        str = str.Replace("@type", type);
                        str = str.Replace("@length", "");
                        defaultStr = "DEFAULT \"" + defa + "\"";
                        break;
                    case "Boolean":
                        type = "TINYINT";
                        str = str.Replace("@type", type);
                        str = str.Replace("@length", "");
                        if (!string.IsNullOrEmpty(defa))
                        {
                            int t;
                            if (int.TryParse(defa, out t))
                            {
                                if (t > 1) defa = "1";
                            }
                            else
                            {
                                defa = "0";
                            }
                        }
                        else
                        {
                            defa = "0";
                        }
                        defaultStr = "DEFAULT " + defa ;
                        break;
                    case "DateTime":
                        type = "datetime";
                        str = str.Replace("@type", type);
                        str = str.Replace("@length", "");
                        if (!string.IsNullOrEmpty(defa))
                        {
                            if (defa == "[Current]")
                            {
                                defa = "Now()";
                                defaultStr = "DEFAULT "+ defa;
                            }
                            else
                            {
                                DateTime t;
                                if (!DateTime.TryParse(defa, out t))
                                {
                                    defaultStr = "DEFAULT " + defa;
                                }
                            }
                        }
                        break;
                    case "Integer":
                        type = "integer";
                        str = str.Replace("@type", type);
                        str = str.Replace("@length", "");
                        if (!string.IsNullOrEmpty(defa))
                        {
                            int t;
                            if (!int.TryParse(defa, out t))
                            {
                                defaultStr = "DEFAULT " + defa;
                            }
                        }
                        break;
                }
                str = str.Replace("@default", defaultStr);
                sb.Append(str);
            }
            sb.Append("CONSTRAINT [PK_@TableName] PRIMARY KEY ([ID])) ");
            sb.Replace("@TableName", tableName);
            string indexContentStr = "CREATE INDEX [IX_Content] ON [@TableName](ID DESC,indexID DESC)";
            string indexStr = " CREATE INDEX [IX_Index] ON [@TableName](nodeid desc, IndexID DESC)";
            indexContentStr = indexContentStr.Replace("@TableName", tableName);
            indexStr = indexStr.Replace("@TableName", tableName);
            List<string> sqlList = new List<string>(3);
            sqlList.Add(sb.ToString());
            sqlList.Add(indexContentStr);
            sqlList.Add(indexStr);
            return sqlList;
        }

        public List<string> GetFieldCreateSql(string fieldXMLStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(fieldXMLStr);
            string tableName = xmlDoc.SelectSingleNode("/Root/Table/@Name").InnerText;
            XmlNodeList list = xmlDoc.SelectNodes("/Root/Table/Field");
             List<string> sqlList = new List<string>();
             foreach (XmlNode node in list)
             {
                 StringBuilder sb = new StringBuilder("ALTER TABLE [@TableName] ADD COLUMN @FieldName ");
                 sb.Replace("@TableName", tableName);
                 string name = node.Attributes["Name"].Value;
                 string length = node.Attributes["DataLength"].Value;
                 sb.Replace("@FieldName", "["+name+"]");
                 switch (node.Attributes["DataType"].Value)
                 {
                     case "NVarChar":
                         sb.Append(" text (" + length+")");
                         break;
                     case "NText":
                         sb.Append(" memo ");
                         break;
                     case "Boolean":
                         sb.Append(" TINYINT ");
                         break;
                     case "DateTime":
                         sb.Append(" datetime ");
                         break;
                     case "Integer":
                        sb.Append(" integer ");
                         break;
                     case "Decimal":
                        int Len = Convert.ToInt32(node.Attributes["DecimalLen"].Value);
                        sb.Append(" DECIMAL(18," + Len.ToString() + ") ");
                         break;
                     case "Money":
                         int decimalLen = Convert.ToInt32(node.Attributes["DecimalLen"].Value);
                         sb.Append(" CURRENCY ");
                         break;
                 }
                 sqlList.Add(sb.ToString());
             }
             return sqlList;
        }

        public string GetFieldDelSql(string tableName, string fieldName)
        {
            return "alter table ["+tableName+"] DROP COLUMN ["+fieldName+"]";
        }
        #endregion
    }
}
