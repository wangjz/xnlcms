using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.DAL;
using System.Data.Common;
using COM.SingNo.XNLCore;
using System.Data;
using System.Xml;
namespace COM.SingNo.CMS.Core
{
    /// <summary>
    /// 管理员角色
    /// </summary>
    public class AdminRole
    {
      /// <summary>
      /// 角色名称
      /// </summary>
        public string roleName;
        /// <summary>
        /// 角色编号
        /// </summary>
        public int roleId;
      /// <summary>
        /// 系统权限值列表
      /// </summary>
        public List<int> sysRightsList;
        /// <summary>
        /// 站点权限值列表
        /// </summary>
        public Dictionary<int,CSiteRight> siteNodeRightsList;
        /// <summary>
        /// 是否管理所有站点
        /// </summary>
        public bool isAllSiteRights;
        /// <summary>
        /// 插件权限
        /// </summary>
        public List<int> pluginsRightsList;
        /// <summary>
        /// 其它权限
        /// </summary>
        public List<int> othersRightsList;
        /// <summary>
        /// 是否超管角色
        /// </summary>
        public bool isSuperRole;
        public bool fileInfoById(int roleId)
        {
            if(roleId==0)
            {
                this.isSuperRole = true;
                this.isAllSiteRights = true;
                this.roleName = "超级管理员";
                this.roleId = 0;
                return true;
            }
            DbDataReader reader = DataHelper.ExecuteReader("select RoleName,SysRights,SiteNodeRights,PluginsRights,OthersRights from SN_AdminRole where ID="+roleId.ToString());
            if(reader.HasRows)
            {
                reader.Read();
                this.roleId = roleId;
                this.roleName = Convert.ToString(reader["RoleName"]);
                string sysRightStr = "";
                sysRightStr = Convert.IsDBNull(reader["SysRights"]) ? "" : Convert.ToString(reader["SysRights"]);
               //得到系统权限
                char[] char1=new char[] { ',' };
                char[] char2 = new char[] { '|' };
                char[] char3 = new char[] { ':' };
                int _len = 0;
                if (!sysRightStr.Equals(""))
                {
                    string[] right_arr = sysRightStr.Split(char1, StringSplitOptions.RemoveEmptyEntries);
                    _len=right_arr.Length;
                    if (_len > 0)
                    {
                        sysRightsList = new List<int>(right_arr.Length);
                        for (int i = 0; i < _len; i++)
                        {
                            sysRightsList.Add(Convert.ToInt32(right_arr[i]));
                        }
                    }
                }
                string siteNodeRightStr = "";
                siteNodeRightStr = Convert.IsDBNull(reader["SiteNodeRights"]) ? "" : Convert.ToString(reader["SiteNodeRights"]);
                if (!siteNodeRightStr.Equals(""))
                {
                    try { getSiteRight(siteNodeRightStr); }
                    catch { }
                }
                string pluginRightStr = "";
                pluginRightStr = Convert.IsDBNull(reader["PluginsRights"]) ? "" : Convert.ToString(reader["PluginsRights"]);
                //得到插件权限
                if (!pluginRightStr.Equals(""))
                {
                    string[] right_arr = pluginRightStr.Split(char1, StringSplitOptions.RemoveEmptyEntries);
                    _len = right_arr.Length;
                    if (_len > 0)
                    {
                        pluginsRightsList= new List<int>(right_arr.Length);
                        for (int i = 0; i < _len; i++)
                        {
                            pluginsRightsList.Add(Convert.ToInt32(right_arr[i]));
                        }
                    }
                }
                string otherRightStr = "";
                otherRightStr = Convert.IsDBNull(reader["OthersRights"]) ? "" : Convert.ToString(reader["OthersRights"]);
                //得到其它权限
                if (!otherRightStr.Equals(""))
                {
                    string[] right_arr = otherRightStr.Split(char1, StringSplitOptions.RemoveEmptyEntries);
                    _len = right_arr.Length;
                    if (_len > 0)
                    {
                        othersRightsList = new List<int>(right_arr.Length);
                        for (int i = 0; i < _len; i++)
                        {
                            othersRightsList.Add(Convert.ToInt32(right_arr[i]));
                        }
                    }
                }
                reader.Close();
                return true;
            }
            reader.Close();
            return false;
        }
        public AdminRole()
        {
            isSuperRole=false;
            isAllSiteRights=false;
        }
        private void getSiteRight(string siteRightXmlStr)
        {
            char[] char1 = new char[] { ',' };
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(siteRightXmlStr);
            XmlNodeList siteList = xmlDoc.SelectNodes("/root/site");
            if(siteList.Count>0)
            {
                this.siteNodeRightsList = new Dictionary<int, CSiteRight>(siteList.Count);
                bool isAllSite = false;
                foreach (XmlNode siteNode in siteList)
                {
                    CSiteRight siteNodeRight = new CSiteRight();
                    int siteId = Convert.ToInt32(siteNode.Attributes["id"].Value); //得到站点栏目id
                    if (siteId.Equals(0)) isAllSite = true;
                    string siteRightStr = siteNode.SelectSingleNode("siteRights").InnerText;
                    //设置站点权限
                    if(!siteRightStr.ToLower().Equals(""))
                    {
                        string[] rights_arr = siteRightStr.Split(char1, StringSplitOptions.RemoveEmptyEntries);
                        if (rights_arr.Length > 0)
                        {
                            siteNodeRight.siteRightList = new List<int>(rights_arr.Length);
                            for (int i = 0; i < rights_arr.Length; i++) siteNodeRight.siteRightList.Add(Convert.ToInt32(rights_arr[i]));
                        }
                    }
                    XmlNodeList nodeList = siteNode.SelectNodes("node");
                    if(nodeList.Count>0) //如果定义了栏目的权限
                    {
                        siteNodeRight.nodeRightsList = new Dictionary<int, List<int>>(nodeList.Count);
                        bool isAllNode = false;
                        foreach (XmlNode _node in nodeList)
                        {
                            int nodeId =Convert.ToInt32(_node.Attributes["id"].Value);
                            if (nodeId.Equals(0)) isAllNode = true;
                            string nodeRightStr = _node.SelectSingleNode("nodeRights").InnerText;
                            //设置站点权限
                            if (!nodeRightStr.ToLower().Equals(""))
                            {
                                string[] rights_arr = nodeRightStr.Split(char1, StringSplitOptions.RemoveEmptyEntries);
                                if (rights_arr.Length > 0)
                                {
                                    List<int> r_list = new List<int>(rights_arr.Length);
                                    for (int i = 0; i < rights_arr.Length; i++) r_list.Add(Convert.ToInt32(rights_arr[i]));
                                    siteNodeRight.nodeRightsList.Add(nodeId, r_list);
                                }
                            }
                        }
                        if (isAllNode && nodeList.Count.Equals(1)) siteNodeRight.isAllNodeRights = true;
                    }
                    this.siteNodeRightsList.Add(siteId, siteNodeRight);
                }
                if (isAllSite && siteList.Count.Equals(1)) this.isAllSiteRights = true;
            }
        }
        public bool ContainsSite(int siteId)
        {
            if (siteNodeRightsList!=null&&siteNodeRightsList.ContainsKey(siteId)) return true;
            return false;
        }
    }
    /// <summary>
    /// 站点权限类
    /// </summary>
    public class CSiteRight
    {
        /// <summary>
        /// 是否可管理所有栏目
        /// </summary>
        public bool isAllNodeRights;
        public List<int> siteRightList;
        /// <summary>
        /// 栏目权限值列表
        /// </summary>
        public Dictionary<int, List<int>> nodeRightsList;
        public CSiteRight()
        {
            isAllNodeRights=false;
        }
    }
}