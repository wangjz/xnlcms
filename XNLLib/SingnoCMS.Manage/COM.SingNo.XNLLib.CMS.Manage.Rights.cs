using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using System.Xml;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class Rights:IXNLTag<WebContext>
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
           string typeStr = labelParams["type"].value.ToString();
           MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Rights.Item");
           Match itemMatch = itemMatchs[0];
           Match yesItem = RegxpEngineCommon.matchsItemTagByName(itemMatch.Groups[3].Value, "Right.Yes")[0];
           Match noItem = RegxpEngineCommon.matchsItemTagByName(itemMatch.Groups[3].Value, "Right.No")[0];
           XNLParam roleIdParam;
           int roleId = -99;
           if (labelParams.TryGetValue("roleid", out roleIdParam))
           {
               roleId = Convert.ToInt32(roleIdParam.value);
           }
           string returnStr = "";
           switch (typeStr)
           {
               case "system":
                   returnStr = setSystemRights(labelContentStr, XNLPage, itemMatch, yesItem, noItem, roleId);
                   break;
               case "site":
                   int siteId = -1;
                   XNLParam siteIdParam;
                   if (labelParams.TryGetValue("siteid", out siteIdParam)) siteId =Convert.ToInt32(siteIdParam.value);
                   returnStr = setSiteRights(labelContentStr, XNLPage, itemMatch, yesItem, noItem, roleId, siteId);
                   break;
               case "channel":
                   int nodeId = -1;
                   XNLParam nodeIdParam;
                   if (labelParams.TryGetValue("nodeid", out nodeIdParam)) nodeId = Convert.ToInt32(nodeIdParam.value);
                   returnStr = setChannelRights(labelContentStr, XNLPage, itemMatch, yesItem, noItem, roleId,nodeId);
                   break;
               case "others":
                   break;
               case "pulgins":
                   break;
           }
           return returnStr;
            */
           return "";
       }
       #endregion
       /*
       private string setSystemRights(string labelContentStr, WebContext XNLPage, Match itemMatch, Match yesItem, Match noItem, int roleId)
       {
           //判断当前页面是否有权限xml对象 
           XmlNodeList rightNodes = SystemConfig.systemXMLNodes;
           if (rightNodes==null||rightNodes.Count.Equals(0)) return "";
           labelContentStr = RegxpEngineCommon.replaceAttribleVariableByName(labelContentStr, "rights.count", rightNodes.Count.ToString());
           string itemStr=itemMatch.Groups[3].Value;
           if(roleId.Equals(-99))  //设置不用设置角色列表
           {
               StringBuilder listSb = new StringBuilder();
               string noItemStr = noItem.Groups[3].Value;
               int i = 1;
               foreach (XmlNode node in rightNodes)
               {
                   Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                   tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                   tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                   tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                   listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, noItemStr));
                   i=i+1;
               }
               itemStr=itemStr.Replace(yesItem.Value,"");
               itemStr=itemStr.Replace(noItem.Value,listSb.ToString());
               labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
               return labelContentStr;
           }
           else
           {
               AdminRole roleObj = getAdminRole(XNLPage, roleId);
               StringBuilder listSb = new StringBuilder();
               string noItemStr = noItem.Groups[3].Value;
               string yesItemStr = yesItem.Groups[3].Value;
               int i = 1;
               foreach (XmlNode node in rightNodes)
               {
                   Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                   tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                   tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                   tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                   listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, setItemStr(roleObj, roleObj.sysRightsList, Convert.ToInt32(node.Attributes["code"].Value), yesItemStr, noItemStr)));
                   i = i + 1;
               }
               itemStr = itemStr.Replace(yesItem.Value, "");
               itemStr = itemStr.Replace(noItem.Value, listSb.ToString());
               labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
               return labelContentStr;
           }
       }
        private string setSiteRights(string labelContentStr, WebContext XNLPage, Match itemMatch, Match yesItem, Match noItem, int roleId, int siteId)
        {
            //判断当前页面是否有权限xml对象 
            XmlNodeList rightNodes = SystemConfig.siteXMLNodes;
            if (rightNodes==null||rightNodes.Count.Equals(0)) return "";
            labelContentStr = RegxpEngineCommon.replaceAttribleVariableByName(labelContentStr, "rights.count", rightNodes.Count.ToString());
            string itemStr = itemMatch.Groups[3].Value;
            if (roleId.Equals(-99))  //设置不用设置角色列表
            {
                StringBuilder listSb = new StringBuilder();
                string noItemStr = noItem.Groups[3].Value;
                int i = 1;
                foreach (XmlNode node in rightNodes)
                {
                    Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                    tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                    tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                    tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                    listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, noItemStr));
                }
                itemStr = itemStr.Replace(yesItem.Value, "");
                itemStr = itemStr.Replace(noItem.Value, listSb.ToString());
                labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
                return labelContentStr;
            }
            else
            {
                AdminRole roleObj = getAdminRole(XNLPage, roleId);
                StringBuilder listSb = new StringBuilder();
                string noItemStr = noItem.Groups[3].Value;
                string yesItemStr = yesItem.Groups[3].Value;
                int i = 1;
                foreach (XmlNode node in rightNodes)
                {
                    Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                    tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                    tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                    tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                    List<int> rightList = null;
                    if (roleObj.siteNodeRightsList != null && roleObj.siteNodeRightsList.ContainsKey(siteId))
                    {
                        rightList=roleObj.siteNodeRightsList[siteId].siteRightList;
                    }
                    listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, setItemStr(roleObj, rightList, Convert.ToInt32(node.Attributes["code"].Value), yesItemStr, noItemStr)));
                    i = i + 1;
                }
                itemStr = itemStr.Replace(yesItem.Value, "");
                itemStr = itemStr.Replace(noItem.Value, listSb.ToString());
                labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
                return labelContentStr;
            }
        }
        private string setChannelRights(string labelContentStr, WebContext XNLPage, Match itemMatch, Match yesItem, Match noItem, int roleId, int nodeId)
        {
            //判断当前页面是否有权限xml对象 
            XmlNodeList rightNodes = SystemConfig.channelXMLNodes;
            if (rightNodes==null||rightNodes.Count.Equals(0)) return "";
            labelContentStr = RegxpEngineCommon.replaceAttribleVariableByName(labelContentStr, "rights.count", rightNodes.Count.ToString());
            string itemStr = itemMatch.Groups[3].Value;
            string itemAttrStr = itemMatch.Groups[2].Value;
            if (roleId.Equals(-99))  //设置不用设置角色列表
            {
                StringBuilder listSb = new StringBuilder();
                string noItemStr = noItem.Groups[3].Value;
                int i = 1;
                foreach (XmlNode node in rightNodes)
                {
                    Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                    tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                    tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                    tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                    listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, noItemStr));
                }
                itemStr = itemStr.Replace(yesItem.Value, "");
                itemStr = itemStr.Replace(noItem.Value, listSb.ToString());
                labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
                return labelContentStr;
            }
            else
            {
                AdminRole roleObj = getAdminRole(XNLPage, roleId);
                StringBuilder listSb = new StringBuilder();
                string noItemStr = noItem.Groups[3].Value;
                string yesItemStr = yesItem.Groups[3].Value;
                int i = 1;
                foreach (XmlNode node in rightNodes)
                {
                    Dictionary<string, XNLParam> tmpParams = new Dictionary<string, XNLParam>(3);
                    tmpParams.Add("right.itemid", new XNLParam( XNLType.Int32, i));
                    tmpParams.Add("right.code", new XNLParam( XNLType.String, node.Attributes["code"].Value));
                    tmpParams.Add("right.name", new XNLParam( XNLType.String, node.Attributes["name"].Value));
                    List<int> rightList = null;
                    int siteId = -1;
                    if (ChannelConfigManager.createInstance().channelDataColls.ContainsKey(nodeId))
                    {
                        siteId = ChannelConfigManager.createInstance().channelDataColls[nodeId].siteNode.nodeID;
                    }
                    if (roleObj.siteNodeRightsList != null &&siteId>0)
                    {
                        if (roleObj.siteNodeRightsList.ContainsKey(siteId))
                        {
                            if (roleObj.siteNodeRightsList[siteId].nodeRightsList!=null&&roleObj.siteNodeRightsList[siteId].nodeRightsList.Count > 0)
                            {
                                foreach (KeyValuePair<int,List<int>> r in roleObj.siteNodeRightsList[siteId].nodeRightsList)
                                {
                                    rightList = r.Value;
                                    break;
                                }
                               // rightList = roleObj.siteNodeRightsList[siteId].nodeRightsList.Keys.
                            }
                            //if (roleObj.siteNodeRightsList[siteId].nodeRightsList.ContainsKey(nodeId)) rightList = roleObj.siteNodeRightsList[siteId].nodeRightsList[nodeId];
                        } 
                    }
                    listSb.Append(RegxpEngineCommon.replaceAttribleVariable(tmpParams, setItemStr(roleObj, rightList, Convert.ToInt32(node.Attributes["code"].Value), yesItemStr, noItemStr)));
                    i = i + 1;
                }
                itemStr = itemStr.Replace(yesItem.Value, "");
                itemStr = itemStr.Replace(noItem.Value, listSb.ToString());
                labelContentStr = labelContentStr.Replace(itemMatch.Value, itemStr);
                return labelContentStr;
            }
        }
        private string setItemStr(AdminRole roleObj,List<int> rightsList,int rightId,string yesItemStr,string noItemStr)
        {
            if (roleObj.isSuperRole ||(rightsList!=null&&rightsList.Contains(rightId)))
            {
                //返回yesItemStr
                return yesItemStr;
            }
            else
            {
                //返回noItemStr
                return noItemStr;
            }
        }
       private AdminRole getAdminRole(WebContext XNLPage,int roleId)
       {
           AdminRole roleObj;
           if (XNLPage.Context.Items.Contains("role" + roleId))
           {
               roleObj = XNLPage.Context.Items["role" + roleId] as AdminRole;
           }
           else
           {
               roleObj = new AdminRole();
               roleObj.fileInfoById(roleId);
               XNLPage.Context.Items.Add("role" + roleId, roleObj);
           }
           return roleObj;
       }
       */

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
