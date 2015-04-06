using System;
using System.Data;
using System.Data.Common;
using COM.SingNo.XNLCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace COM.SingNo.DAL
{
    /// <summary>
    /// DataHelper类,即进行数据库访问时需要调用的类
    /// </summary>
    public sealed class DataHelper
    {
      public static DataBaseFactory factory = DataBaseFactory.GetInstance();
      public static IDataBase abstractDbFactory = factory.CreateInstance();
      public static string connectionString = abstractDbFactory.GetConnectionString();
      private DataHelper()
      {

      }
      //---------------------------------------------------------------------------------------------//////
        /// <summary>
        /// 连立数据库连接
        /// </summary>
      /// <returns>DbConnection</returns>
      public static DbConnection CreateConnection()
      {
          return abstractDbFactory.CreateConnection();
      }
        /// <summary>
        /// 得到当前数据库类型
        /// </summary>
        /// <returns></returns>
      public static string getDataBaseType()
      {
          return factory.getDataBaseType();
      }
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
      /// <returns>DbConnection</returns>
      public static DbConnection CreateConnection(string connectionString)
      {
          return abstractDbFactory.CreateConnection(connectionString);
      }
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
      /// <param name="connectionString">数据库连接字符串</param>
      /// <returns>DbConnection</returns>
      public static DbConnection CreateConnection(DataBaseType databaseType, string connectionString)
      {
          IDataBase DataBase = GetDataBase(databaseType);
          DbConnection concreteDbConn;
          if (string.IsNullOrEmpty(connectionString))
          {
              concreteDbConn = DataBase.CreateConnection();
          }
          else
          {
              concreteDbConn = DataBase.CreateConnection(connectionString);
          }
          return concreteDbConn;
      }
       /// <summary>
       /// 建立事务
       /// </summary>
       /// <param name="conn">数据库连接对象</param>
      /// <returns>DbTransaction</returns>
      public static DbTransaction CreateTransaction(DbConnection conn)
      {
          return abstractDbFactory.CreateTransaction(conn);
      }
        /// <summary>
        /// 建立事务
        /// </summary>
      /// <param name="conn">数据库连接对象</param>
        /// <param name="databaseType">数据库类型</param>
      /// <returns>DbTransaction</returns>
      public static DbTransaction CreateTransaction(DbConnection conn,DataBaseType databaseType)
      {
          return  GetDataBase(databaseType).CreateTransaction(conn);
      }
//---------------------------------------------------------------------------------------------------------///
        /// <summary>
        /// 根据sql构建DbCommand对象
        /// </summary>
        /// <param name="query">sql语句</param>
      /// <returns>DbCommand对象</returns>
      public static DbCommand GetSqlStringCommand(string query)
      {
          DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
          concreteDbCommand.CommandText = query;
          concreteDbCommand.CommandType = CommandType.Text;
          return concreteDbCommand;
      }
        /// <summary>
      /// 根据sql构建DbCommand对象
        /// </summary>
      /// <param name="query">sql语句</param>
        /// <param name="databaseType">数据库类型</param>
      /// <returns>DbCommand对象</returns>
      public static DbCommand GetSqlStringCommand(string query, DataBaseType databaseType)
      {
          DbCommand concreteDbCommand = CreateCommand(databaseType);
          concreteDbCommand.CommandText = query;
          concreteDbCommand.CommandType = CommandType.Text;
          return concreteDbCommand;
      }
        /// <summary>
      /// 根据存储过程名称构建DbCommand对象
        /// </summary>
      /// <param name="storedProcedureName">存储过程名称</param>
      /// <returns>DbCommand对象</returns>
      public static DbCommand GetStoredProcCommand(string storedProcedureName)
      {
          return abstractDbFactory.GetStoredProcCommand(storedProcedureName);
      }
       /// <summary>
      /// 根据存储过程名称构建DbCommand对象
       /// </summary>
      /// <param name="storedProcedureName">存储过程名称</param>
      /// <param name="parameterValues">存储过程用到的参数数组</param>
      /// <returns>DbCommand</returns>
      public static DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
      {
          return abstractDbFactory.GetStoredProcCommand(storedProcedureName, parameterValues);
      }
     /// <summary>
      /// 构建指定数据库的DbCommand对象
     /// </summary>
     /// <param name="databaseType"></param>
      /// <returns>DbCommand</returns>
      public static DbCommand CreateCommand(DataBaseType databaseType)
      {
          IDataBase DataBase = GetDataBase(databaseType);
          return DataBase.CreateCommand();
      }
        /// <summary>
        /// 由指定的数据库类型得到数据库对象
        /// </summary>
        /// <param name="databaseType"></param>
      /// <returns>IDataBase</returns>
      public static IDataBase GetDataBase(DataBaseType databaseType)
      {
          IDataBase DataBase;
          if (string.IsNullOrEmpty(databaseType.ToString()))
          {
              DataBase = abstractDbFactory;
          }
          else
          {
              DataBase = factory.CreateInstance(databaseType);
          }
          return DataBase;
      }
      public static void SetParameterValue(DbCommand command, string parameterName, DbType dbType, object value)
      {
          abstractDbFactory.AddInParameter(command, parameterName, dbType, value);
      }
      public static void SetParameterValue(DbCommand command, string parameterName, DbType dbType, object value, DataBaseType databaseType)
      {
          GetDataBase(databaseType).AddInParameter(command, parameterName, dbType, value);
      }
      public static void SetParameterValue(DbCommand command, Dictionary<string, XNLParam> xnlParams)
      {
          MatchCollection sqlParamsMatch = DALUtils.getMatchCollsByRegex(command.CommandText, "@([^@\\s,;\"()[\\]]+)?");
          try
          {
              if (xnlParams != null)
              {
                  foreach (Match pMatch in sqlParamsMatch)
                  {
                      string sqlParamName = pMatch.Groups[1].Value.Trim().ToLower();
                      object sqlValue = xnlParams[sqlParamName].value;
                      SetParameterValue(command, sqlParamName, xnlParams[sqlParamName].dbType, sqlValue);
                  }
              }
          }
          catch
          {
              throw (new Exception("sql语句参数错误，请检查XNL标签参数。"+command.CommandText));
          }
      }
      public static void SetParameterValue(DbCommand command, Dictionary<string, XNLParam> xnlParams,DataBaseType databaseType)
      {
          MatchCollection sqlParamsMatch = DALUtils.getMatchCollsByRegex(command.CommandText, "@([^@\\s,;\"()[\\]]+)?");
          try
          {
              if (xnlParams != null)
              {
                  foreach (Match pMatch in sqlParamsMatch)
                  {
                      string sqlParamName = pMatch.Groups[1].Value.Trim().ToLower();
                      object sqlValue = xnlParams[sqlParamName].value;
                      SetParameterValue(command, sqlParamName, xnlParams[sqlParamName].dbType, sqlValue,databaseType);
                  }
              }
          }
          catch
          {
              throw (new Exception("sql语句参数错误，请检查XNL标签参数。"));
          }
      }
      ///------------------------------------------------------------------------///
      public static int ExecuteNonQuery(string strSql)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              int id = concreteDbCommand.ExecuteNonQuery();
              return id;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DbDataReader ExecuteReader(string strSql)
      {
          DbConnection concreteDbConn = CreateConnection();
          concreteDbConn.ConnectionString = connectionString;
          concreteDbConn.Open();
          DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
          concreteDbCommand.Connection = concreteDbConn;
          concreteDbCommand.CommandText = strSql;
          DbDataReader dr = abstractDbFactory.CreateDataReader(concreteDbCommand,CommandBehavior.CloseConnection);
          return dr;
      }
      public static object ExecuteScalar(string strSql)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              object obj = concreteDbCommand.ExecuteScalar();
              return obj;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DataSet ExecuteDataset(string strSql)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataSet ds = new DataSet();
              dtAdapter.Fill(ds);
              return ds;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DataTable ExecuteDataTable(string strSql)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataTable dt = new DataTable();
              dtAdapter.Fill(dt);
              return dt;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      #region 需要设置XNLParam 的方法
      public static int ExecuteNonQuery(string strSql, Dictionary<string, XNLParam> xnlParams)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams);
              int id = concreteDbCommand.ExecuteNonQuery();
              return id;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DbDataReader ExecuteReader(string strSql, Dictionary<string, XNLParam> xnlParams)
      {
          DbConnection concreteDbConn = CreateConnection();
          concreteDbConn.ConnectionString = connectionString;
          concreteDbConn.Open();
          DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
          concreteDbCommand.Connection = concreteDbConn;
          concreteDbCommand.CommandText = strSql;
          SetParameterValue(concreteDbCommand, xnlParams);
          DbDataReader dr = abstractDbFactory.CreateDataReader(concreteDbCommand, CommandBehavior.CloseConnection);
          return dr;
      }
      public static object ExecuteScalar(string strSql, Dictionary<string, XNLParam> xnlParams)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams);
              object obj = concreteDbCommand.ExecuteScalar();
              return obj;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
          
      }
      public static DataSet ExecuteDataset(string strSql, Dictionary<string, XNLParam> xnlParams)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams);
              DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataSet ds = new DataSet();
              dtAdapter.Fill(ds);
              return ds;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DataTable ExecuteDataTable(string strSql, Dictionary<string, XNLParam> xnlParams)
      {
          DbConnection concreteDbConn = CreateConnection();
          try
          {
              concreteDbConn.ConnectionString = connectionString;
              concreteDbConn.Open();
              DbCommand concreteDbCommand = abstractDbFactory.CreateCommand();
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams);
              DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataTable dt = new DataTable();
              dtAdapter.Fill(dt);
              return dt;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      #endregion
        /// <summary>
      /// 执行DbCommand,更新数据库,返回影响的行数
        /// </summary>
      /// <param name="command">DbCommand</param>
        /// <returns>int</returns>
      public static int ExecuteNonQuery(DbCommand command)
      {
          return command.ExecuteNonQuery(); ;
      }
        /// <summary>
      /// 执行DbCommand,得到数据库内容
        /// </summary>
      /// <param name="command">DbCommand</param>
      /// <returns>DbDataReader</returns>
      public static DbDataReader ExecuteReader(DbCommand command)
      {
          if (command.Transaction != null)
          {
              return command.ExecuteReader(CommandBehavior.CloseConnection);
          }
          return command.ExecuteReader();
      }
        /// <summary>
      /// 执行DbCommand,更新数据库,得到影响的第一行第一列的字段值
        /// </summary>
      /// <param name="command">DbCommand</param>
        /// <returns>object</returns>
      public static object ExecuteScalar(DbCommand command)
      {
          return command.ExecuteScalar();;
      }
        /// <summary>
      /// 执行DbCommand，返回DataSet
        /// </summary>
      /// <param name="command">DbCommand</param>
      /// <returns>DataSet</returns>
      public static DataSet ExecuteDataSet(DbCommand command)
      {
          DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
          dtAdapter.SelectCommand = command;
          DataSet ds = new DataSet();
          dtAdapter.Fill(ds);
          return ds;
      }
      public static DataTable ExecuteDataTable(DbCommand command)
      {
          DbDataAdapter dtAdapter = abstractDbFactory.CreateDataAdapter();
          dtAdapter.SelectCommand = command;
          DataTable dt = new DataTable();
          dtAdapter.Fill(dt);
          return dt;
      }

      public static int ExecuteNonQuerySetDBType(string strSql, Dictionary<string, XNLParam> xnlParams, string connectionString, DataBaseType databaseType)
      {
          DbConnection concreteDbConn = CreateConnection(databaseType, connectionString);
          try
          {
              concreteDbConn.Open();
              DbCommand concreteDbCommand = CreateCommand(databaseType);
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams, databaseType);
              int id = concreteDbCommand.ExecuteNonQuery();
              return id;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DbDataReader ExecuteReaderSetDBType(string strSql, Dictionary<string, XNLParam> xnlParams, string connectionString, DataBaseType databaseType)
      {
          DbConnection concreteDbConn = CreateConnection(databaseType, connectionString);
          concreteDbConn.Open();
          DbCommand concreteDbCommand = CreateCommand(databaseType);
          concreteDbCommand.Connection = concreteDbConn;
          concreteDbCommand.CommandText = strSql;
          SetParameterValue(concreteDbCommand, xnlParams, databaseType);
          DbDataReader dr = GetDataBase(databaseType).CreateDataReader(concreteDbCommand, CommandBehavior.CloseConnection);
          return dr;
      }
      public static object ExecuteScalarSetDBType(string strSql, Dictionary<string, XNLParam> xnlParams, string connectionString, DataBaseType databaseType)
      {
          DbConnection concreteDbConn = CreateConnection(databaseType, connectionString);
          try
          {
              concreteDbConn.Open();
              DbCommand concreteDbCommand = CreateCommand(databaseType);
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams, databaseType);
              object obj = concreteDbCommand.ExecuteScalar();
              return obj;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DataSet ExecuteDatasetSetDBType(string strSql, Dictionary<string, XNLParam> xnlParams, string connectionString, DataBaseType databaseType)
      {
          DbConnection concreteDbConn = CreateConnection(databaseType, connectionString);
          try
          {
              concreteDbConn.Open();
              DbCommand concreteDbCommand = CreateCommand(databaseType);
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams, databaseType);
              DbDataAdapter dtAdapter = GetDataBase(databaseType).CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataSet ds = new DataSet();
              dtAdapter.Fill(ds);
              return ds;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      public static DataTable ExecuteDataTableSetDBType(string strSql, Dictionary<string, XNLParam> xnlParams, string connectionString, DataBaseType databaseType)
      {
          DbConnection concreteDbConn = CreateConnection(databaseType, connectionString);
          try
          {
              concreteDbConn.Open();
              DbCommand concreteDbCommand = CreateCommand(databaseType);
              concreteDbCommand.Connection = concreteDbConn;
              concreteDbCommand.CommandText = strSql;
              SetParameterValue(concreteDbCommand, xnlParams, databaseType);
              DbDataAdapter dtAdapter = GetDataBase(databaseType).CreateDataAdapter();
              dtAdapter.SelectCommand = concreteDbCommand;
              DataTable dt = new DataTable();
              dtAdapter.Fill(dt);
              return dt;
          }
          catch (System.Exception ex)
          {
              throw (ex);
          }
          finally
          {
              concreteDbConn.Close();
          }
      }
      ////---------------------------------------------------------------------------------------------------------------------------///////////
      #region 原来的实现
      public static DbResultInfo ExrcuteScalarSomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection conn = CreateConnection();
          conn.Open();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Connection = conn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams);
                  object obj = ExecuteScalar(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
          }
          catch (Exception e)
          {
              conn.Close();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
              return dbResultInfo;
          }
          conn.Close();
          dbResultInfo.isSuccess = true;
          dbResultInfo.value = objArr;
          return dbResultInfo;
      }
      public static void ExrcuteScalarSomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams,DbTransaction transaction)
      {
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = transaction.Connection;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          int i = 0;
          foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
          {
               SetParameterValue(tmpParam.Value, xnlParams);
               object obj = ExecuteScalar(tmpParam.Value);
               string keyStr = "_dbresult" + i.ToString();
               if (xnlParams.ContainsKey(keyStr))
               {
                  xnlParams[keyStr].value = obj;
               }
               else
               {
                 xnlParams.Add(keyStr, new XNLParam(obj));
               }
               i = i + 1;
          }
      }
      public static DbResultInfo ExrcuteScalarSomeSqlWithTransaction(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          dbResultInfo.isSuccess =true;
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection dbConn = CreateConnection();
          dbConn.Open();
          DbTransaction transaction = dbConn.BeginTransaction();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = dbConn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams);
                  object obj = ExecuteScalar(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }

                  i = i + 1;
              }
              transaction.Commit();
          }
          catch (Exception e)
          {
              transaction.Rollback();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
          }
          finally
          {
              dbConn.Close();
              transaction.Dispose();
          }
          if (dbResultInfo.isSuccess)
          {
              dbResultInfo.value = objArr;
          }
          return dbResultInfo;
      }
      public static DbResultInfo ExrcuteNonQuerySomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection conn = CreateConnection();
          conn.Open();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Connection = conn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams);
                  int obj = ExecuteNonQuery(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
          }
          catch (Exception e)
          {
              conn.Close();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
              return dbResultInfo;
          }
          conn.Close();
          dbResultInfo.isSuccess = true;
          dbResultInfo.value = objArr;
          return dbResultInfo;
      }
      public static void ExrcuteNonQuerySomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DbTransaction transaction)
      {
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = transaction.Connection;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          int i = 0;
          foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
          {
              SetParameterValue(tmpParam.Value, xnlParams);
              int obj = ExecuteNonQuery(tmpParam.Value);
              string keyStr = "_dbresult" + i.ToString();
              if (xnlParams.ContainsKey(keyStr))
              {
                  xnlParams[keyStr].value = obj;
              }
              else
              {
                 xnlParams.Add(keyStr, new XNLParam(obj));
              }
              i = i + 1;
          }
          
      }
      public static DbResultInfo ExrcuteNonQuerySomeSqlWithTransaction(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          dbResultInfo.isSuccess = true;
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection dbConn = CreateConnection();
          dbConn.Open();
          DbTransaction transaction = dbConn.BeginTransaction();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = dbConn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams);
                  int obj =ExecuteNonQuery(tmpParam.Value);
                  objArr.Add(obj);
                 string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
              transaction.Commit();
          }
          catch (Exception e)
          {
              transaction.Rollback();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
          }
          finally
          {
              dbConn.Close();
              transaction.Dispose();
          }
          if (dbResultInfo.isSuccess)
          {
              dbResultInfo.value = objArr;
          }
          return dbResultInfo;
      }
//------------------设置DataBaseType的方法
      public static DbResultInfo ExrcuteScalarSomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType,string connectionString)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection conn = CreateConnection(databaseType,connectionString);
          
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value,databaseType);
              dbCommand.Connection = conn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          conn.Open();
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams, databaseType);
                  object obj = ExecuteScalar(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
          }
          catch (Exception e)
          {
              conn.Close();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
              return dbResultInfo;
          }
          conn.Close();
          dbResultInfo.isSuccess = true;
          dbResultInfo.value = objArr;
          return dbResultInfo;
      }
      public static void ExrcuteScalarSomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DbTransaction transaction, DataBaseType databaseType)
      {
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value,databaseType);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = transaction.Connection;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          int i = 0;
          foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
          {
              SetParameterValue(tmpParam.Value, xnlParams,databaseType);
              object obj = ExecuteScalar(tmpParam.Value);
              string keyStr = "_dbresult" + i.ToString();
              if (xnlParams.ContainsKey(keyStr))
              {
                  xnlParams[keyStr].value = obj;
              }
              else
              {
                  xnlParams.Add(keyStr, new XNLParam(obj));
              }
              i = i + 1;
          }
      }
      public static DbResultInfo ExrcuteScalarSomeSqlWithTransaction(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType, string connectionString)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          dbResultInfo.isSuccess = true;
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection dbConn = CreateConnection(databaseType, connectionString);
          DbTransaction transaction = dbConn.BeginTransaction();
          dbConn.Open();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value,databaseType);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = dbConn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
         
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams, databaseType);
                  object obj = ExecuteScalar(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
              transaction.Commit();
          }
          catch (Exception e)
          {
              transaction.Rollback();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
          }
          finally
          {
              dbConn.Close();
              transaction.Dispose();
          }
          if (dbResultInfo.isSuccess)
          {
              dbResultInfo.value = objArr;
          }
          return dbResultInfo;
      }
      public static DbResultInfo ExrcuteNonQuerySomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType, string connectionString)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection conn = CreateConnection(databaseType, connectionString);
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value, databaseType);
              dbCommand.Connection = conn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          conn.Open();
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams, databaseType);
                  int obj = ExecuteNonQuery(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
          }
          catch (Exception e)
          {
              conn.Close();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
              return dbResultInfo;
          }
          conn.Close();
          dbResultInfo.isSuccess = true;
          dbResultInfo.value = objArr;
          return dbResultInfo;
      }
      public static void ExrcuteNonQuerySomeSql(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DbTransaction transaction, DataBaseType databaseType)
      {
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value,databaseType);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = transaction.Connection;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          int i = 0;
          foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
          {
              SetParameterValue(tmpParam.Value, xnlParams,databaseType);
              int obj = ExecuteNonQuery(tmpParam.Value);
             string keyStr = "_dbresult" + i.ToString();
              if (xnlParams.ContainsKey(keyStr))
              {
                  xnlParams[keyStr].value = obj;
              }
              else
              {
                  xnlParams.Add(keyStr, new XNLParam( obj));
              }
              i = i + 1;
          }

      }
      public static DbResultInfo ExrcuteNonQuerySomeSqlWithTransaction(Dictionary<string, string> sqlColls, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType, string connectionString)
      {
          DbResultInfo dbResultInfo = new DbResultInfo();
          dbResultInfo.isSuccess = true;
          Dictionary<string, DbCommand> dbCommands = new Dictionary<string, DbCommand>();
          DbConnection dbConn = CreateConnection(databaseType, connectionString);
          dbConn.Open();
          DbTransaction transaction = dbConn.BeginTransaction();
          foreach (KeyValuePair<string, string> tmpParam in sqlColls)
          {
              DbCommand dbCommand = GetSqlStringCommand(tmpParam.Value,databaseType);
              dbCommand.Transaction = transaction;
              dbCommand.Connection = dbConn;
              dbCommands.Add(tmpParam.Key, dbCommand);
          }
          List<object> objArr = new List<object>(sqlColls.Count);
          try
          {
              int i = 0;
              foreach (KeyValuePair<string, DbCommand> tmpParam in dbCommands)
              {
                  SetParameterValue(tmpParam.Value, xnlParams, databaseType);                 
                  int obj = ExecuteNonQuery(tmpParam.Value);
                  objArr.Add(obj);
                  string keyStr = "_dbresult" + i.ToString();
                  if (xnlParams.ContainsKey(keyStr))
                  {
                      xnlParams[keyStr].value = obj;
                  }
                  else
                  {
                      xnlParams.Add(keyStr, new XNLParam(obj));
                  }
                  i = i + 1;
              }
              transaction.Commit();
          }
          catch (Exception e)
          {
              transaction.Rollback();
              dbResultInfo.isSuccess = false;
              dbResultInfo.exception = e;
          }
          finally
          {
              dbConn.Close();
              transaction.Dispose();
          }
          if (dbResultInfo.isSuccess)
          {
              dbResultInfo.value = objArr;
          }
          return dbResultInfo;
      }
//-----------------
      public static int GetSqlDataCount(DbCommand command, Dictionary<string, XNLParam> xnlParams)
      {
          XNLParam param;
          if (xnlParams.TryGetValue("countsql", out param))
          {
              command.CommandText = param.value.ToString();
              SetParameterValue(command, xnlParams);
          }
          else
          {
              XNLParam primaryKeyParam;
              string strPrimaryKey = null;
              if (xnlParams.TryGetValue("primarykey", out primaryKeyParam))
              {
                  strPrimaryKey = primaryKeyParam.value.ToString();
              }
              else
              {
                  strPrimaryKey = GetKeyInfo(command,xnlParams);
              }
              command.CommandText =abstractDbFactory.ModifySqlToCountSql(command.CommandText, strPrimaryKey);
              SetParameterValue(command, xnlParams);
          }
          object obj = DataHelper.ExecuteScalar(command);
          return Convert.ToInt32(obj);
      }
      public static DataTable GetSchemaTable(DbCommand command, CommandBehavior cmdBehavior, Dictionary<string, XNLParam> xnlParams)
      {
          SetParameterValue(command, xnlParams);
          return abstractDbFactory.GetSchemaTable(command, cmdBehavior);
      }
      public static string GetKeyInfo(DbCommand command, Dictionary<string, XNLParam> xnlParams)
      {
          SetParameterValue(command, xnlParams);
          return abstractDbFactory.GetKeyInfo(command);
      }
      public static DataTable GetDataTableBySqlWithPage(DbCommand command, Dictionary<string, XNLParam> xnlParams)
      {
          string scrSqlStr = command.CommandText;
          if (xnlParams == null) xnlParams = new Dictionary<string, XNLParam>();
          XNLParam primaryKeyParam;
          string primaryKeyStr = "";
          if (xnlParams.TryGetValue("primarykey", out primaryKeyParam))
          {
              primaryKeyStr = primaryKeyParam.value.ToString();
          }
          else //得到主键
          {
              primaryKeyStr = GetKeyInfo(command, xnlParams);
              xnlParams.Add("primarykey", new XNLParam(primaryKeyStr));
          }
          //计算count
          DbCommand countCmd = GetSqlStringCommand(scrSqlStr);
          countCmd.Connection = command.Connection;
          if (command.Transaction != null) countCmd.Transaction = command.Transaction;
          int totalNums = GetSqlDataCount(countCmd, xnlParams);
          if (xnlParams.ContainsKey("totalrecordsnum"))
          {
              xnlParams["totalrecordsnum"].value = totalNums;
          }
          else
          {
                xnlParams.Add("totalrecordsnum", new XNLParam(totalNums));  //总记录数
          }
          
          XNLParam perPageRecordsParam;

          if (!xnlParams.TryGetValue("perpagerecordsnum", out perPageRecordsParam)) //每页多少记录
          {
              xnlParams.Add("perpagerecordsnum", new XNLParam( XNLType.Int32, 20));
          }

          if (xnlParams.ContainsKey("totalpagesnum"))
          {
              xnlParams["totalpagesnum"].value = 1;
          }
          else
          {
              xnlParams.Add("totalpagesnum", new XNLParam(XNLType.Int32, 1));  //总页数
          }
          int totalPagesNum = 0;
          int perPageRecordsNum = Convert.ToInt32(perPageRecordsParam.value); //一页多少记录
          if (totalNums > 0 && totalNums > perPageRecordsNum) totalPagesNum = (totalNums / perPageRecordsNum) + ((totalNums % perPageRecordsNum) > 0 ? 1 : 0);
          xnlParams["totalpagesnum"].value = totalPagesNum; //算出一共几页
          XNLParam pageNameParam;
          if (!xnlParams.TryGetValue("pagename", out pageNameParam))  //传递curpage的request.querystring的queryname
          {
              xnlParams.Add("pagename", new XNLParam( XNLType.String, "page"));
          }
          XNLParam firstPageParam;
          XNLParam lastPageParam;
          XNLParam prevPageParam;
          XNLParam nextPageParam;
          //设置第一页参数
          int firstPageNum = 1;
          if (!xnlParams.TryGetValue("firstpagenum", out firstPageParam))
          {
              xnlParams.Add("firstpagenum", new XNLParam( XNLType.Int32, firstPageNum));  //第一页
          }
          else
          {
              firstPageNum = Convert.ToInt32(firstPageParam.value);
              if (firstPageNum <= 0) firstPageNum = 1;
              firstPageParam.value = firstPageNum;
          }
          //设置最后一页
          int lastPageNum = totalPagesNum;
          if (!xnlParams.TryGetValue("lastpagenum", out lastPageParam))
          {
              xnlParams.Add("lastpagenum", new XNLParam( XNLType.Int32, lastPageNum));  //最后一页
          }
          else
          {
              lastPageNum = Convert.ToInt32(lastPageParam.value);
              if (lastPageNum > totalPagesNum) lastPageNum = totalPagesNum;
              lastPageParam.value = lastPageNum;
          }
          //设置当前页
          int tmpCurPageNum = 1;
          XNLParam curPageParam;
          if (xnlParams.TryGetValue("curpagenum", out curPageParam))
          {
              tmpCurPageNum = Convert.ToInt32(curPageParam.value);
          }
          else
          {
              xnlParams.Add("curpagenum", new XNLParam( XNLType.Int32, 1));
          }
          if (tmpCurPageNum <= 0)
          {
              tmpCurPageNum = 1;
              xnlParams["curpagenum"].value = tmpCurPageNum;
          }

          if (tmpCurPageNum > totalPagesNum)
          {
              tmpCurPageNum = totalPagesNum;
              xnlParams["curpagenum"].value = tmpCurPageNum;
          }
          //设置上一页参数
          int prevPageNum = 1;
          if (tmpCurPageNum > 1) prevPageNum = tmpCurPageNum - 1;
          if (!xnlParams.TryGetValue("prevpagenum", out prevPageParam))
          {
              xnlParams.Add("prevpagenum", new XNLParam( XNLType.Int32, prevPageNum));  //上一页
          }
          else
          {
              prevPageParam.value = prevPageNum;
          }
          //设置下一页参数
          int nextPageNum = totalPagesNum;
          if (tmpCurPageNum < totalPagesNum) nextPageNum = tmpCurPageNum + 1;
          if (!xnlParams.TryGetValue("nextpagenum", out nextPageParam))
          {
              xnlParams.Add("nextpagenum", new XNLParam( XNLType.Int32, nextPageNum));  //下一页

          }
          else
          {
              nextPageParam.value = nextPageNum;
          }
          //得到分页数据
          DbCommand cmd = GetSqlStringCommand(scrSqlStr);
          cmd.Connection = command.Connection;
          if (command.Transaction != null) cmd.Transaction = command.Transaction;
          return abstractDbFactory.GetDataTableBySqlWithPage(cmd,xnlParams);
      }
      public static int GetSqlDataCount(DbCommand command, Dictionary<string, XNLParam> xnlParams,DataBaseType databaseType)
      {
          XNLParam param;
          if (xnlParams.TryGetValue("countsql", out param))
          {
              command.CommandText = param.value.ToString();
              SetParameterValue(command, xnlParams, databaseType);
          }
          else
          {
              XNLParam primaryKeyParam;
              string strPrimaryKey=null;
              if (xnlParams.TryGetValue("primarykey", out primaryKeyParam))
              {
                  strPrimaryKey = primaryKeyParam.value.ToString();
              }
              else
              {
                  strPrimaryKey = GetKeyInfo(command, xnlParams,databaseType);
              }
              command.CommandText=GetDataBase(databaseType).ModifySqlToCountSql(command.CommandText, strPrimaryKey);
          }

          object obj = DataHelper.ExecuteScalar(command);
          return Convert.ToInt32(obj);
      }
      public static DataTable GetSchemaTable(DbCommand command, CommandBehavior cmdBehavior, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType)
      {
          SetParameterValue(command, xnlParams, databaseType);
          return GetDataBase(databaseType).GetSchemaTable(command, cmdBehavior);
      }
      public static string GetKeyInfo(DbCommand command, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType)
      {
          SetParameterValue(command, xnlParams,databaseType);
          return GetDataBase(databaseType).GetKeyInfo(command);
      }
      public static DataTable GetDataTableBySqlWithPage(DbCommand command, Dictionary<string, XNLParam> xnlParams, DataBaseType databaseType)
      {
          string scrSqlStr = command.CommandText;
          //查找主键
          if (xnlParams == null) xnlParams = new Dictionary<string, XNLParam>();
          XNLParam primaryKeyParam;
          string primaryKeyStr="";
          if (xnlParams.TryGetValue("primarykey", out primaryKeyParam))
          {
              primaryKeyStr = primaryKeyParam.value.ToString();
          }
          else //得到主键
          {
              primaryKeyStr = GetKeyInfo(command, xnlParams, databaseType);
              xnlParams.Add("primarykey", new XNLParam( XNLType.String, primaryKeyStr));
          }
          //计算count
          DbCommand countCmd = CreateCommand(databaseType);
          countCmd.CommandText = scrSqlStr;
          countCmd.Connection = command.Connection;
          if (command.Transaction != null) countCmd.Transaction = command.Transaction;
          int totalNums = GetSqlDataCount(countCmd, xnlParams, databaseType);
          xnlParams.Add("totalrecordsnum", new XNLParam( totalNums));  //总记录数
          XNLParam perPageRecordsParam;
          if (!xnlParams.TryGetValue("perpagerecordsnum", out perPageRecordsParam)) //每页多少记录
          {
              xnlParams.Add("perpagerecordsnum", new XNLParam( 20));
          }

          xnlParams.Add("totalpagesnum", new XNLParam(1));  //总页数
         
          int totalPagesNum = 0;
          int perPageRecordsNum = Convert.ToInt32(xnlParams["perpagerecordsnum"].value); //一页多少记录
          if (totalNums > 0 && totalNums > perPageRecordsNum) totalPagesNum = (totalNums / perPageRecordsNum) + ((totalNums % perPageRecordsNum) > 0 ? 1 : 0);
          xnlParams["totalpagesnum"].value = totalPagesNum; //算出一共几页
          XNLParam pageNameParam;
          if (!xnlParams.TryGetValue("pagename", out pageNameParam))  //传递curpage的request.querystring的queryname
          {
              xnlParams.Add("pagename", new XNLParam("page"));
          }
          XNLParam firstPageParam;
          XNLParam lastPageParam;
          XNLParam prevPageParam;
          XNLParam nextPageParam;
          //设置第一页参数
          int firstPageNum=1;
          if (!xnlParams.TryGetValue("firstpagenum", out firstPageParam))
          {
              xnlParams.Add("firstpagenum", new XNLParam(firstPageNum));  //第一页
          }
          else
          {
              firstPageNum=Convert.ToInt32(firstPageParam.value);
              if (firstPageNum <= 0) firstPageNum = 1;
              firstPageParam.value = firstPageNum;
          }
          //设置最后一页
          int lastPageNum = totalPagesNum;
          if (!xnlParams.TryGetValue("lastpagenum", out lastPageParam))
          {
              xnlParams.Add("lastpagenum", new XNLParam(lastPageNum));  //最后一页
          }
          else
          {
              lastPageNum = Convert.ToInt32(lastPageParam.value);
              if (lastPageNum > totalPagesNum) lastPageNum = totalPagesNum;
              lastPageParam.value = lastPageNum;
          }
          //设置当前页
          int tmpCurPageNum = 1;
          XNLParam curPageParam;
          if (xnlParams.TryGetValue("curpagenum", out curPageParam))
          {
              tmpCurPageNum = Convert.ToInt32(curPageParam.value);
          }
          else
          {
              xnlParams.Add("curpagenum", new XNLParam( XNLType.Int32, 1));
          }
          if (tmpCurPageNum < 0)
          {
              tmpCurPageNum = 1;
              xnlParams["curpagenum"].value = tmpCurPageNum;
          }

          if (tmpCurPageNum > totalPagesNum)
          {
              tmpCurPageNum = totalPagesNum;
              xnlParams["curpagenum"].value = tmpCurPageNum;
          }
          //设置上一页参数
          int prevPageNum = 1;
          if (tmpCurPageNum > 1) prevPageNum = tmpCurPageNum - 1;
          if (!xnlParams.TryGetValue("prevpagenum", out prevPageParam))
          {
              xnlParams.Add("prevpagenum", new XNLParam( prevPageNum));  //上一页
          }
          else
          {
              prevPageParam.value = prevPageNum;
          }
          //设置下一页参数
          int nextPageNum = totalPagesNum;
          if (tmpCurPageNum < totalPagesNum) nextPageNum = tmpCurPageNum + 1;
          if (!xnlParams.TryGetValue("nextpagenum", out nextPageParam))
          {
              xnlParams.Add("nextpagenum", new XNLParam(nextPageNum));  //下一页
          }
          else
          {
              nextPageParam.value = nextPageNum;
          }
          //得到分页数据
          DbCommand cmd = CreateCommand(databaseType);
          cmd.CommandText = scrSqlStr;
          cmd.Connection = command.Connection;
          if(command.Transaction!=null)cmd.Transaction = command.Transaction;
          return GetDataBase(databaseType).GetDataTableBySqlWithPage(cmd, xnlParams);
      }
      public static List<string> GetTableCreateSql(string tableXMLStr)
      {
          return abstractDbFactory.GetTableCreateSql(tableXMLStr);
      }
      public static List<string> GetFieldCreateSql(string fieldXMLStr)
      {
          return abstractDbFactory.GetFieldCreateSql(fieldXMLStr);
      }
      public static string GetFieldDelSql(string tableName, string fieldName)
      {
          return abstractDbFactory.GetFieldDelSql(tableName, fieldName);
      }
     #endregion
    }
}
