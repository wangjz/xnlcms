using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using COM.SingNo.XNLCore;
namespace COM.SingNo.DAL
{
    /// <summary>
    /// 针对SqlServer专用连接的工厂
    /// </summary>
    public class SqlFactory : IDataBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlFactory()
        {
        }
        #region IDataBase 成员
        /// <summary>
        /// 建立默认Connection对象
        /// </summary>
        /// <returns>Connection对象</returns>
        public DbConnection CreateConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        /// <summary>
        /// 根据连接字符串建立Connection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns>Connection对象</returns>
        public DbConnection CreateConnection(string strConn)
        {
            return new SqlConnection(strConn);
        }

        /// <summary>
        /// 建立Command对象
        /// </summary>
        /// <returns>Command对象</returns>
        public DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// 建立DataAdapter对象
        /// </summary>
        /// <returns>DataAdapter对象</returns>
        public DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
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
        public DbDataReader CreateDataReader(DbCommand myDbCommand, CommandBehavior cmdBehavior)
        {
            return myDbCommand.ExecuteReader(cmdBehavior);
        }
        /// <summary>
        /// 获得连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public string GetConnectionString()
        {
            return DataBaseFactory.connectionColls["sqlserver"];
        }

        public DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            DbCommand dbCmd= CreateCommand();
            dbCmd.CommandText = storedProcedureName;
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddRange(parameterValues);
            return dbCmd;
        }

        public DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            DbCommand dbCmd = CreateCommand();
            dbCmd.CommandText = storedProcedureName;
            dbCmd.CommandType = CommandType.StoredProcedure;
            return dbCmd;
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
            SqlParameter param = (SqlParameter)command.CreateParameter();
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
        public DataTable GetSchemaTable(DbCommand command, CommandBehavior cmdBehavior)
        {
            throw new NotImplementedException();
        }
        public DataTable GetDataTableBySqlWithPage(DbCommand command, Dictionary<string, XNLParam> pageInfoparams)
        {
            throw new NotImplementedException();
        }
        public string ModifySqlToCountSql(string strSql, string strPrimaryKey)
        {
            throw new NotImplementedException();
        }

        public string GetKeyInfo(DbCommand command)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IDataBase 成员


        public List<string> GetTableCreateSql(string tableXMLStr)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFieldCreateSql(string fieldXMLStr)
        {
            throw new NotImplementedException();
        }
        public string GetFieldDelSql(string tableName, string fieldName)
        {

            return "";
        }
        #endregion
    }
}