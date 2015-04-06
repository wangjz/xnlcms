using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using COM.SingNo.XNLCore;
namespace COM.SingNo.DAL
{
    /// <summary>
    /// 数据库抽象工厂接口
    /// </summary>
    public interface IDataBase
    {
        /// <summary>
        /// 建立默认连接
        /// </summary>
        /// <returns>数据库连接</returns>
        DbConnection CreateConnection();
        /// <summary>
        /// 根据连接字符串建立Connection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns>Connection对象</returns>
        DbConnection CreateConnection(string strConn);
        /// <summary>
        /// 建立Command对象
        /// </summary>
        /// <returns>Command对象</returns>
        DbCommand CreateCommand();
        /// <summary>
        /// 建立DataAdapter对象
        /// </summary>
        /// <returns>DataAdapter对象</returns>
        DbDataAdapter CreateDataAdapter();
        /// <summary>
        /// 根据Connection建立Transaction
        /// </summary>
        /// <param name="myDbConnection">Connection对象</param>
        /// <returns>Transaction对象</returns>
        DbTransaction CreateTransaction(DbConnection myDbConnection);
        /// <summary>
        /// 根据Command建立DataReader
        /// </summary>
        /// <param name="myDbCommand">Command对象</param>
        /// <returns>DataReader对象</returns>
        DbDataReader CreateDataReader(DbCommand myDbCommand);
        /// <summary>
        ///  根据Command建立DataReader
        /// </summary>
        /// <param name="myDbCommand">Command对象</param>
        /// <param name="cmdBehavior">CommandBehavior类型</param>
        /// <returns>DataReader对象</returns>
        DbDataReader CreateDataReader(DbCommand myDbCommand, CommandBehavior cmdBehavior);
        /// <summary>
        /// 获得连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        string GetConnectionString();
        void AddInParameter(DbCommand command, string name, DbType dbType, object value);
        DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues);
        DbCommand GetStoredProcCommand(string storedProcedureName);
        object GetParameterValue(DbCommand command, string name);
        void AddInParameter(DbCommand command, string name, DbType dbType);
        void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion);
        void AddOutParameter(DbCommand command, string name, DbType dbType, int size);
        void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value);
        void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);
        DataTable GetSchemaTable(DbCommand command, CommandBehavior cmdBehavior);
        string GetKeyInfo(DbCommand command);
        DataTable GetDataTableBySqlWithPage(DbCommand command, Dictionary<string, XNLParam> pageInfoparams);
        string ModifySqlToCountSql(string strSql, string strPrimaryKey);
        /// <summary>
        /// 根据xml描述得到建表的sql
        /// </summary>
        /// <param name="tableXMLStr"></param>
        List<string> GetTableCreateSql(string tableXMLStr);
        /// <summary>
        /// 根据xml描述得到建字段的sql
        /// </summary>
        /// <param name="fieldXMLStr"></param>
        List<string> GetFieldCreateSql(string fieldXMLStr);

        string GetFieldDelSql(string tableName, string fieldName);
    }
}

