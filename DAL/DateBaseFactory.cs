using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Xml;
namespace COM.SingNo.DAL
{
    /// <summary>
    /// Factory类
    /// </summary>
    public sealed class DataBaseFactory
    {
        private static volatile DataBaseFactory singleFactory = null;
        private static DataBaseType DatabaseType;
        public static Dictionary<string,string> connectionColls=new Dictionary<string,string>();
        private static object syncObj = new object();
        /// <summary>
        /// Factory类构造函数
        /// </summary>
        private DataBaseFactory()
        {
            string databaseType = ConfigurationManager.AppSettings["DatabaseType"].ToLower();
            switch (databaseType.ToLower())
            {
                case "sqlserver":
                    {
                        DatabaseType = DataBaseType.sqlServer;
                        break;
                    }
                case "access":
                    {
                        DatabaseType = DataBaseType.Access;
                        break;
                    }
                case "oracle":
                    {
                        DatabaseType = DataBaseType.Oracle;
                        break;
                    }
                case "firebird":
                    {
                        DatabaseType = DataBaseType.FireBird;
                        break;
                    }
                case "mysql":
                    {
                        DatabaseType = DataBaseType.MySql;
                        break;
                    }
            }
            /*
			XmlDocument xmlDoc=new XmlDocument();
			xmlDoc.LoadXml(HttpContext.Current.Server.MapPath("/GlobalFiles/Config/Data.config"));
            int _s = 1;
            _s = 1;
            _s = 1;
			XmlNodeList nodeList=xmlDoc.SelectNodes("/connectionStrings//add");
			foreach(XmlNode _node in nodeList)
			{
				string _name=_node.Attributes["name"].Value.ToLower();
                string connStr=_node.Attributes["connectionString"].Value;
				if(_name.Equals("access")||_name.Equals("firdbird"))
                {
                    connStr = connStr.Replace("~\\", HttpContext.Current.Server.MapPath("~"));
                }
                connectionColls.Add(_name, connStr);
			}  */
            ConnectionStringSettingsCollection conns =ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < conns.Count; i++)
            {
                ConnectionStringSettings cs = conns[i];
                string _name=cs.Name.ToLower();
                string connStr=cs.ConnectionString;
                if(_name.Equals("access")||_name.Equals("firdbird"))
                {
                    connStr = connStr.Replace("~\\", HttpContext.Current.Server.MapPath("~"));
                }
                connectionColls.Add(_name, connStr);
            }
          
        }
        /// <summary>
        /// 获得Factory类的实例
        /// </summary>
        /// <returns>Factory类实例</returns>
        public static DataBaseFactory GetInstance()
        {
            if (singleFactory == null)
            {
                lock (syncObj)
                {
                    if (singleFactory == null)
                    {
                        singleFactory = new DataBaseFactory();
                    }
                }
            }
            return singleFactory;
        }
        /// <summary>
        /// 建立Factory类实例
        /// </summary>
        /// <returns>Factory类实例</returns>
        public IDataBase CreateInstance()
        {
            IDataBase abstractDbFactory = null;
            switch (DatabaseType)
            {
                case DataBaseType.sqlServer:
                {
                     abstractDbFactory = new SqlFactory();
                     break;
                 }
                case DataBaseType.Access:
                {
                     abstractDbFactory = new AccessFactory();
                     break;
                }
                case DataBaseType.Oracle:
               {
                    abstractDbFactory = new OracleFactory();
                    break;
                }
                case DataBaseType.FireBird:
                {
                     abstractDbFactory = new FirebirdFactory();
                     break;
                 }
                case DataBaseType.MySql:
                {
                     break;
                }
            }
            return abstractDbFactory;
        }
        /// <summary>
        /// 建立Factory类实例
        /// </summary>
        /// <param name="dbTypeName">数据库类型名称</param>
        /// <returns>actory类实例</returns>
        public IDataBase CreateInstance(DataBaseType dataBaseType)
        {
            IDataBase abstractDbFactory = null;
            switch (dataBaseType)
            {
                case DataBaseType.sqlServer:
               {
                     abstractDbFactory = new SqlFactory();
                     break;
               }
                case DataBaseType.Access:
                {
                     abstractDbFactory = new AccessFactory();
                     break;
                }
                case DataBaseType.Oracle:
                {
                     abstractDbFactory = new OracleFactory();
                     break;
                }
                case DataBaseType.FireBird:
                {
                    abstractDbFactory = new FirebirdFactory();
                    break;
                }
                case DataBaseType.MySql:
                {
                    break;
                }
            }
            return abstractDbFactory;
        }
        /// <summary>
        /// 得到当前使用的数据库类型
        /// </summary>
        /// <returns></returns>
        public string getDataBaseType()
        {
            return DatabaseType.ToString();
        }
    }
}

