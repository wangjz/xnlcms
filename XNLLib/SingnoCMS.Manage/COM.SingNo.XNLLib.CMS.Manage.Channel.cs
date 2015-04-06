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
using System.Xml;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public  class Channel:IXNLTag<WebContext>
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
              MatchCollection labelColl = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "CMS.Manage", "Channel");
              labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, labelColl);
              MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Channel.Success");
              MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Channel.Error");
              string actionStr = labelParams["action"].value.ToString().Trim().ToLower();
              if (actionStr.Equals("add"))
              {
                  labelContentStr = addChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem,XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if (actionStr.Equals("modify"))
              {
                  labelContentStr = modifyChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem, XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if(actionStr.Equals("remove"))
              {
                  //移入回收站
                  labelContentStr = removeChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem, XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if (actionStr.Equals("restore"))
              {
                  labelContentStr = restoreChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem, XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if (actionStr.Equals("delete"))
              {
                  //删除
                  labelContentStr = deleteChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem, XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if (actionStr.Equals("checkchannel"))
              {
                  labelContentStr = checkChannel(labelParams, labelContentStr, matchSuccessItem, matchFailItem);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if (actionStr.Equals("addgroup"))
              {
                  labelContentStr = addNodeGroup(labelParams, labelContentStr, matchSuccessItem, matchFailItem,XNLPage);
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
              }
              else if(actionStr.Equals("up")||actionStr.Equals("down"))
              {
                  try
                  {
                      if(actionStr.Equals("up"))
                      {
                          upIndex(labelParams,labelContentStr,XNLPage);
                      }else
                      {
                          downIndex(labelParams,labelContentStr,XNLPage);
                      }
                      return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                  }
                  catch (System.Exception ex)
                  {
                       Dictionary<string, string> errorList = new Dictionary<string, string>();
                       errorList.Add("1", ex.Message);
                       labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                      return labelContentStr;
                  }
              }
              else if (actionStr.Equals("deletegroup") || actionStr.Equals("deletegroupnode"))
              {
                  try
                  {
                      if (actionStr.Equals("deletegroup"))
                      {
                         deleteGroup(labelParams, XNLPage);
                      }
                      else
                      {
                          deleteGroupNode(labelParams, XNLPage);
                      }
                      return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                  }
                  catch (System.Exception ex)
                  {
                      Dictionary<string, string> errorList = new Dictionary<string, string>();
                      errorList.Add("1", ex.Message);
                      labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                      return labelContentStr;
                  }
              }
              else
              {
                  Dictionary<string, string> errorList = new Dictionary<string, string>();
                  errorList.Add("1", "参数错误!");
                  labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                  labelContentStr = RegxpEngineCommon.enabledNestedXNLTag(labelContentStr, labelColl);
                  return labelContentStr;
              }
             return labelContentStr;
             */
            return "";
        }
      /*
        private string addChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem,WebContext XNLPage)
        {
            if (!labelParams.ContainsKey("parentnodeid") || string.IsNullOrEmpty(labelParams["parentnodeid"].value.ToString()))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "父栏目ID不能为空");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            int parentId = Convert.ToInt32(labelParams["parentnodeid"].value);
            Cchannel pChannel = ChannelConfigManager.createInstance().channelDataColls[parentId];
            if(!pChannel.theChannelConfig.baseConfig.canAddChannel)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "父栏目不允许添加子栏目");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            if (!labelParams.ContainsKey("nodename") || string.IsNullOrEmpty(labelParams["nodename"].value.ToString()))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目名称不能为空");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }

            if (labelParams.ContainsKey("indexname") && !string.IsNullOrEmpty(labelParams["indexname"].value.ToString()))
            {
                if (checkIndexName(labelParams,XNLPage))
                {
                    Dictionary<string, string> errorList = new Dictionary<string, string>();
                    errorList.Add("1", "索引名称已存在");
                    labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                    return labelContentStr;
                }
            }
            //首先得到上级站点的数据
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTransaction = dbConn.BeginTransaction();
            DbCommand dbParentNodeCmd = DataHelper.GetSqlStringCommand("select nodeid,ChildsNum,RootID,Depth,ParentPath,sortid from sn_nodes where nodeid=" + parentId.ToString());
            dbParentNodeCmd.Connection = dbConn;
            dbParentNodeCmd.Transaction = dbTransaction;
            DataHelper.SetParameterValue(dbParentNodeCmd, labelParams);
            DataTable dt1 = DataHelper.ExecuteDataTable(dbParentNodeCmd); //得到上级栏目数据
            int rootId = Convert.ToInt32(dt1.Rows[0]["rootid"]);
            int depth = Convert.ToInt32(dt1.Rows[0]["depth"]);
            string parentNodePath = Convert.ToString(dt1.Rows[0]["parentpath"]);
            int ChildsNum = Convert.ToInt32(dt1.Rows[0]["ChildsNum"]);
            string sortStr = Convert.ToString(dt1.Rows[0]["sortid"]);
            //添加新节点
            labelParams.Add("parentpath", new XNLParam(parentNodePath));
            labelParams.Add("depth", new XNLParam(depth + 1));
            labelParams.Add("rootid", new XNLParam(rootId));
            string channelName = Convert.ToString(labelParams["nodename"].value);
            int indexId=getNextIndexID(labelParams,dbTransaction);
            labelParams.Add("indexid", new XNLParam(indexId));
            string _sortStr = getSortIdByIndex(indexId);
            if (rootId == parentId)
            {
                labelParams.Add("sortid", new XNLParam(XNLType.String, _sortStr));
            }
            else
            {
                labelParams.Add("sortid", new XNLParam(XNLType.String, sortStr + _sortStr));
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ManageUtil.getChannelConfig());
            XmlNode xmlNode = xmlDoc.SelectSingleNode("ChannelConfig/BaseConfig");
            xmlNode.SelectSingleNode("Info").InnerText = labelParams["info"].value.ToString();
            xmlNode.SelectSingleNode("MetaKeyword").InnerText = labelParams["matekeyword"].value.ToString(); ;
            xmlNode.SelectSingleNode("MetaDesc").InnerText = labelParams["metadesc"].value.ToString();
            labelParams.Add("settingsxml", new XNLParam(xmlDoc.OuterXml ));
            labelParams.Add("creater", new XNLParam(XNLType.String, ManageUtil.getCurAdminName()));
            string addNodeSql = "insert into sn_nodes(NodeName,IndexName,modelid,ParentID,ParentPath,Depth,RootID,ChildsNum,indexID,SettingsXML,[Creater],imageUrl,LinkUrl,sortid)values(@nodeName,@indexname,@modelid,@parentNodeId,@parentpath,@depth,@rootid,0,@indexid,@settingsxml,@creater,@imageurl,@LinkUrl,@sortid)";
            string selectNodeSql = "select max(nodeid) from sn_nodes where nodeid>0 and ParentID=" + parentId.ToString();
            string updateModelSql = "update sn_model set useNumber=useNumber+1 where ModelID=@ModelID";
            string updateSiteSql = "update sn_sites set NodeCount=NodeCount+1 where siteid=" + pChannel.siteID.ToString();
            Dictionary<string, string> sqlColl1 = new Dictionary<string, string>();
            sqlColl1.Add("1",addNodeSql);
            sqlColl1.Add("2", selectNodeSql);
            sqlColl1.Add("3", updateModelSql);
            sqlColl1.Add("4", updateSiteSql);
            try
            {
                DataHelper.ExrcuteScalarSomeSql(sqlColl1,labelParams,dbTransaction);
                int newNodeId = Convert.ToInt32(labelParams["_dbresult1"].value);
                labelParams.Add("nodeid", new XNLParam(newNodeId));
                labelParams["parentpath"].value = parentNodePath + "," + Convert.ToString(newNodeId);
                string updateNodeSql = "update sn_nodes set ParentPath=@ParentPath where nodeid=@nodeid";
                string updateParentSql = "";
                if (ChildsNum == 0)
                {
                    updateParentSql = "update sn_nodes set ChildsNum=ChildsNum+1,arrChildID='" + Convert.ToString(newNodeId) + "' where nodeid=" + parentId.ToString();
                }
                else
                {
                    updateParentSql = "update sn_nodes set ChildsNum=ChildsNum+1,arrChildID=arrChildID+'," + Convert.ToString(newNodeId) + "' where nodeid=" + parentId.ToString();
                }
                //再重新设置此栏目栏目组
                Dictionary<string, string> sqlColl = new Dictionary<string, string>();
                sqlColl.Add("updateNode", updateNodeSql);
                sqlColl.Add("updateParent", updateParentSql);
                XNLParam groupsParam=null;
                if(labelParams.TryGetValue("nodegroup",out groupsParam))
                {   
                    string groups=groupsParam.value.ToString();
                    if(!groups.Trim().Equals(""))
                    {
                        string[] groups_arr = groups.Split(new char[] { ',' });
                        foreach (string str in groups_arr)
                        {
                            sqlColl.Add("addgroup" + sqlColl.Count, "insert into SN_NodeGroups (NGS_ID,NGS_NodeID) values("+str+",@nodeid)");
                        }
                    }
                }
                DataHelper.ExrcuteNonQuerySomeSql(sqlColl, labelParams,dbTransaction);
                //在内存中设置此节点
                setGlobalChannel(labelParams, dbTransaction);
                dbTransaction.Commit();
                dbTransaction.Dispose();
                dbConn.Close();
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                return labelContentStr;
            }
            catch (Exception e)
            {
                dbTransaction.Rollback();
                dbTransaction.Dispose();
                dbConn.Close();
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }            
        }
        private string checkChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem)
        {
            if (!checkChannel(labelParams))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目已存在");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
            else
            {
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            return labelContentStr;
        }
        private bool checkChannel(Dictionary<string, XNLParam> labelParams)
        {
            string checkSql = "select nodeid from sn_nodes where ParentID=@parentnodeid and nodeName=@nodeName";
            if (labelParams["action"].value.Equals("modify")) checkSql = "select nodeid from sn_nodes where ParentID=@parentnodeid and nodeid<>@nodeid and nodeName=@nodeName";
            object nodeIdObj = DataHelper.ExecuteScalar(checkSql, labelParams);
            int nodeId = Convert.ToInt32(Convert.IsDBNull(nodeIdObj) ? 0 : nodeIdObj);
            if (nodeId > 0)
            {
                return false;
            }
            return true;
        }
      /// <summary>
      /// 检查索引名称，true :已存在 false  不存在
      /// </summary>
      /// <param name="labelParams"></param>
      /// <returns></returns>
        private bool checkIndexName(Dictionary<string, XNLParam> labelParams,WebContext XNLPage)
        {
            int siteId = ManageUtil.getCurSiteID(XNLPage);
            string checkSql = "select nodeid from sn_nodes where  indexName=@indexName and RootID="+siteId;
            if(labelParams["action"].value.ToString().Equals("modify"))
            {
                checkSql = "select nodeid from sn_nodes where indexName=@indexName and NodeId<>@nodeid and RootID=" + siteId;
            }
            object nodeIdObj = DataHelper.ExecuteScalar(checkSql, labelParams);
            int nodeId = Convert.ToInt32(Convert.IsDBNull(nodeIdObj) ? 0 : nodeIdObj);
            if (nodeId > 0) return true;
            return false;
        }
        private int getNextIndexID(Dictionary<string, XNLParam> labelParams,DbTransaction transaction)
        {
            DbCommand dbCmd = DataHelper.GetSqlStringCommand("select max(indexid)  from sn_nodes where ParentID=@parentNodeId");
            dbCmd.Connection = transaction.Connection;
            dbCmd.Transaction = transaction;
            DataHelper.SetParameterValue(dbCmd, labelParams);
            object indexObj = DataHelper.ExecuteScalar(dbCmd);
            int indexID = Convert.ToInt32(Convert.IsDBNull(indexObj) ? 0 : indexObj) + 1;
            return indexID;
        }
        private void setGlobalChannel(Dictionary<string, XNLParam> labelParams,DbTransaction dbTransaction)
        {
            int nodeID = Convert.ToInt32(labelParams["nodeid"].value);
            ChannelConfigManager channelConfig = ChannelConfigManager.createInstance();
            if (channelConfig.channelDataColls.ContainsKey(nodeID)) //已存在此节点
            {
                throw new Exception("节点已存在");
            }
            else  //新建节点
            {
                Cchannel newChannel = new Cchannel();
                newChannel.nodeID = nodeID;
                newChannel.nodeIndexName = labelParams["indexname"].value.ToString();
                newChannel.nodeName = labelParams["nodename"].value.ToString();
                newChannel.parentNode = channelConfig.channelDataColls[Convert.ToInt32(labelParams["parentnodeid"].value)];
                newChannel.parentNode.addSubNode(nodeID, newChannel);
                newChannel.siteNode = newChannel.parentNode.siteNode;
                newChannel.siteID = newChannel.siteNode.siteID;
                newChannel.addDate = DateTime.Now.ToLongDateString();
                newChannel.depth = Convert.ToInt32(labelParams["depth"].value);
                newChannel.model = DataModelManager.createInstance().getModel(Convert.ToInt32(labelParams["modelid"].value));
                newChannel.imageUrl = labelParams["imageurl"].value.ToString();
                newChannel.theChannelConfig.baseConfig.info = labelParams["info"].value.ToString();
                newChannel.theChannelConfig.baseConfig.metaKeyword = labelParams["matekeyword"].value.ToString();
                newChannel.theChannelConfig.baseConfig.metaDesc = labelParams["metadesc"].value.ToString();
                //ManageUtil.setNodeModel(newChannel, labelParams, dbTransaction);
                channelConfig.channelDataColls.Add(nodeID, newChannel);
                //设置模板匹配
                Dictionary<string, string> sqlColls = new Dictionary<string, string>();
                Dictionary<string, XNLParam> tmpParam = new Dictionary<string, XNLParam>();
                foreach (KeyValuePair<int, TemplateProject> pr in newChannel.siteNode.templateProjectColls)
                {
                    DbCommand tmpDbCmd = DataHelper.GetSqlStringCommand("select ChannelTemplateID,ContentTemplateID,ChannelFilePathRule,ContentFilePathRule from SN_TemplateProject where ProjectID=" + pr.Key);
                    tmpDbCmd.Connection = dbTransaction.Connection;
                    tmpDbCmd.Transaction = dbTransaction;
                    DataTable projectDt = DataHelper.ExecuteDataTable(tmpDbCmd);
                    DataRow projectRow = projectDt.Rows[0];
                    string ChannelFilePathRule = projectRow["ChannelFilePathRule"].ToString();
                    string ContentFilePathRule = projectRow["ContentFilePathRule"].ToString();
                    tmpParam.Add("channelfilepathrule" + pr.Key, new XNLParam( XNLType.String, ChannelFilePathRule));
                    tmpParam.Add("contentfilepathrule" + pr.Key, new XNLParam( XNLType.String, ContentFilePathRule));
                    sqlColls.Add(pr.Key.ToString(), "insert into SN_TemplateMatch(nodeID,ProjectID,SiteID,ChannelTemplateID,ContentTemplateID,ChannelFilePathRule,ContentFilePathRule)values("+newChannel.nodeID+"," + pr.Key + "," + newChannel.siteNode.siteID + "," + projectRow["ChannelTemplateID"].ToString() + "," + projectRow["ContentTemplateID"].ToString() + ",@ChannelFilePathRule"+pr.Key+",@ContentFilePathRule"+pr.Key+")");
                    sqlColls.Add("chup" + pr.Key.ToString(), "update SN_Template set UseNumber=UseNumber+1 where TemplateID=" + projectRow["ChannelTemplateID"].ToString());
                    sqlColls.Add("coup" + pr.Key.ToString(), "update SN_Template set UseNumber=UseNumber+1 where TemplateID=" + projectRow["ContentTemplateID"].ToString());
                    //设置模板方案栏目地址
                    string channelUrl = (CMSUtils.getPathByPathRule(ChannelFilePathRule, newChannel.nodeID, newChannel.nodeIndexName, newChannel.nodeName, 1, null).Replace("//", "/"));
                    pr.Value.setChannelUrl(newChannel.nodeID, channelUrl);
                    //设置模板方案内容页地址规则
                    ContentFilePathRule = (CMSUtils.getPathByPathRule(ContentFilePathRule, newChannel.nodeID, newChannel.nodeIndexName, newChannel.nodeName, 1, null).Replace("//", "/"));
                    pr.Value.setContentUrlRule(newChannel.nodeID, ContentFilePathRule);
                }
                   DataHelper.ExrcuteNonQuerySomeSql(sqlColls, tmpParam,dbTransaction);
            }
        }
        private string addNodeGroup(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem,WebContext XNLPage)
        {
            //检查名称是否为空
            XNLParam groupNameParam;
            if(labelParams.TryGetValue("groupname",out groupNameParam))
            {
                if(groupNameParam.value.ToString().Trim().Equals(""))
                {
                    Dictionary<string, string> errorList = new Dictionary<string, string>();
                    errorList.Add("1", "栏目组名称不能为空!");
                    labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                    return labelContentStr;
                }
                groupNameParam.type = XNLType.String;
            }
            else
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "没有栏目组名称属性!");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            //检查是否已存在相在名称
            int siteId = ManageUtil.getCurSiteID(XNLPage);
            object obj=DataHelper.ExecuteScalar("select NG_id from sn_nodegroup where NG_name=@groupname and NG_siteid="+siteId.ToString(),labelParams);
            int groupId = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
            if(groupId>0)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目组名称已存在!");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction trans = dbConn.BeginTransaction();
            try
            {
                DbCommand command = DataHelper.GetSqlStringCommand("insert into sn_nodegroup(NG_Name,NG_SiteId,NG_Desc)values(@GroupName,"+siteId+",@Description)");
                DataHelper.SetParameterValue(command,labelParams);
                command.Connection = dbConn;
                command.Transaction = trans;
                DataHelper.ExecuteNonQuery(command);
                trans.Commit();
                trans.Dispose();
                dbConn.Close();
                //dbConn.Dispose();
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch(Exception e)
            {
                trans.Rollback();
                trans.Dispose();
                dbConn.Close();
                //dbConn.Dispose();
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            return labelContentStr;
        }
        private string removeChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem, XNLContext XNLPage)
        {
            //先分隔nodeid
            try
            {
                string nodeId = labelParams["nodeid"].value.ToString();
                string[] node_arr = nodeId.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if(node_arr.Length.Equals(0))
                {
                    throw (new Exception("没有指定栏目编号"));
                }
                for(int i=0;i<node_arr.Length;i++)
                {
                    int _nodeId;
                    if(!int.TryParse(node_arr[i],out _nodeId))
                    {
                        throw (new Exception("栏目编号类型错误"));
                    }
                }
                string sql;
                string removeSql;
                //查找是否有子节点
                if (node_arr.Length.Equals(1))
                {
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid=@nodeid and b.parentid=a.nodeid and b.state=0";
                    removeSql="update sn_nodes set state=1 where nodeid=@nodeid";
                }
                else
                {
                    string nodesStr = string.Join(",", node_arr);
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid in(" + nodesStr + ") and b.parentid=a.nodeid and b.state=0 and b.nodeid not in (" + nodesStr + ")";
                    removeSql = "update sn_nodes set state=1 where nodeid in(" + nodesStr + ")";
                }
                DbCommand dbCmd = DataHelper.GetSqlStringCommand(sql);
                DbConnection dbConn = DataHelper.CreateConnection();
                dbConn.Open();
                DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                //具体操作
                try
                {
                    DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                    if (dt.Rows.Count > 0) throw (new Exception("所选栏目有子栏目!"));
                    DbCommand removeCmd = DataHelper.GetSqlStringCommand(removeSql);
                    removeCmd.Connection = dbConn;
                    removeCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(removeCmd, labelParams);
                    removeCmd.ExecuteNonQuery();
                    dbTran.Commit();
                }
                catch (System.Exception ex)
                {
                    try { 
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
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", ex.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
        }
        private string deleteChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem, XNLContext XNLPage)
        {
            try
            {
               //检查是否指定节点
                string nodeidStr = labelParams["nodeid"].value.ToString();
                string[] node_arr = nodeidStr.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (node_arr.Length.Equals(0))
                {
                    throw (new Exception("没有指定栏目编号"));
                }
                int minNodeId = Convert.ToInt32(node_arr[0]);
                for(int i=0;i<node_arr.Length;i++)
                {
                    int _nodeId;
                    if(!int.TryParse(node_arr[i],out _nodeId))
                    {
                        throw (new Exception("栏目编号类型错误"));
                    }
                    else
                    {
                        if (_nodeId < minNodeId) minNodeId = _nodeId;
                    }
                }
                labelParams.Add("minnode", new XNLParam(XNLType.Int32, minNodeId));
                string topSql = "select Depth from sn_nodes where nodeid=@minnode";
                //检查是否是根节点
                object rootObj = DataHelper.ExecuteScalar(topSql, labelParams);
                int rootId = Convert.ToInt32(rootObj);
                if(rootId==0)
                {
                    throw (new Exception("根栏目节点不允许删除!"));
                }
                string sql;
                string removeSql;
                string deleteGroupSql;
                string deleteTemSql;
                //查找是否有子节点
                string modelSql = "";
                string nodesStr = string.Join(",", node_arr);
                if (node_arr.Length.Equals(1))
                {
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid=@nodeid and b.parentid=a.nodeid and b.state=1";
                    removeSql="delete from sn_nodes  where nodeid=@nodeid";
                    modelSql = "select a.ModelId,a.nodeid,b.TableName from sn_nodes as a,sn_model as b where a.nodeid=@nodeid and a.modelid=b.modelid";
                    deleteGroupSql = "delete from SN_NodeGroups where NG_nodeid=@nodeid";
                    deleteTemSql = "delete from SN_TemplateMatch where NodeID=@nodeid";
                    
                }
                else
                {
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid in(" + nodesStr + ") and b.parentid=a.nodeid and b.state=1 and b.nodeid not in (" + nodesStr + ")";
                    removeSql = "delete from sn_nodes where nodeid in(" + nodesStr + ")";
                    modelSql = "select a.nodeid, a.modelid,b.TableName from sn_nodes as a,sn_model as b where a.modelid=b.ModelID and a.nodeid in(" + nodesStr + ")";
                    deleteGroupSql = "delete from SN_NodeGroups where NGS_nodeid in(" + nodesStr + ")";
                    deleteTemSql = "delete from SN_TemplateMatch where nodeid in(" + nodesStr + ")";
                }
                DbCommand dbCmd = DataHelper.GetSqlStringCommand(sql);
                DbConnection dbConn = DataHelper.CreateConnection();
                dbConn.Open();
                DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                //具体操作
                try
                {
                    DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                    if (dt.Rows.Count > 0) throw (new Exception("所选栏目有子栏目!"));
                    //得到模型数组
                    Dictionary<string, List<string>> modelIdList = new Dictionary<string, List<string>>();
                    DbCommand modelCmd=DataHelper.GetSqlStringCommand(modelSql);
                    modelCmd.Connection=dbConn;
                    modelCmd.Transaction=dbTran;
                    DataHelper.SetParameterValue(modelCmd,labelParams);
                    DataTable modelDt = DataHelper.ExecuteDataTable(modelCmd);
                    Dictionary<string, string> modelTableColls = new Dictionary<string, string>();
                    Dictionary<string, int> modelCountColls = new Dictionary<string, int>();
                    foreach (DataRow row in modelDt.Rows)
                    {
                        string modelid =row["modelid"].ToString();
                        if(!modelIdList.ContainsKey(modelid))
                        {
                            List<string> tmpList = new List<string>();
                            tmpList.Add(row["nodeid"].ToString());
                            modelIdList.Add(modelid, tmpList);
                            modelTableColls.Add(modelid, row["TableName"].ToString());
                            modelCountColls.Add(modelid, 1);
                        }
                        else 
                        {
                            modelIdList[modelid].Add(row["nodeid"].ToString());
                            modelCountColls[modelid] = modelCountColls[modelid] + 1;
                        }
                    }
                    //更新模板使用数 select count(ChannelTemplateID) as count1,count(ContentTemplateID) as count2, ChannelTemplateID,ContentTemplateID from sn_templateMatch  group by ChannelTemplateID,ContentTemplateID
                    DbCommand _templateCmd = DataHelper.GetSqlStringCommand("select count(ChannelTemplateID) as count1,count(ContentTemplateID) as count2, ChannelTemplateID,ContentTemplateID from sn_templateMatch where nodeid in ("+nodeidStr+") group by ChannelTemplateID,ContentTemplateID");
                    _templateCmd.Connection = dbConn;
                    _templateCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(_templateCmd, labelParams);
                    DataTable _templateDt = DataHelper.ExecuteDataTable(_templateCmd);
                    foreach (DataRow row in _templateDt.Rows)
                    {
                        DbCommand _uptemplaCmd = DataHelper.GetSqlStringCommand("update SN_Template set UseNumber=UseNumber-" + row["count1"].ToString() + " where TemplateID=" + row["ChannelTemplateID"].ToString());
                        _uptemplaCmd.Connection = dbConn;
                        _uptemplaCmd.Transaction = dbTran;
                        _uptemplaCmd.ExecuteNonQuery();
                        //_uptemplaCmd = DataHelper.GetSqlStringCommand("update SN_Template set UseNumber=UseNumber-" + row["count2"].ToString() + " where TemplateID=" + row["ContentTemplateID"].ToString());
                        _uptemplaCmd.CommandText = "update SN_Template set UseNumber=UseNumber-" + row["count2"].ToString() + " where TemplateID=" + row["ContentTemplateID"].ToString();
                        _uptemplaCmd.ExecuteNonQuery();
                    }
                    Cchannel topChannel = ChannelConfigManager.createInstance().channelDataColls[minNodeId];
                    //更新父节点
                    if (node_arr.Length.Equals(1))
                    {
                        //得到上级节点数据
                        DbCommand _cmd = DataHelper.GetSqlStringCommand("select NodeId,ArrChildID from sn_nodes where nodeid=" + topChannel.parentNode.nodeID);
                        _cmd.Connection = dbConn;
                        _cmd.Transaction = dbTran;
                        DataTable _dt = DataHelper.ExecuteDataTable(_cmd);
                        string srcArrStr = _dt.Rows[0]["ArrChildID"].ToString();
                        string newArrStr = setNodeChildList(srcArrStr, nodeidStr);
                        string upSql = "update sn_nodes set ChildsNum=ChildsNum-1,ArrChildID='" + newArrStr + "' where nodeid=" + _dt.Rows[0]["nodeID"].ToString();
                        _cmd.CommandText = upSql;
                        _cmd.ExecuteNonQuery();
                        
                    }
                    else
                    {
                        // SELECT count(parentid) as delCount,parentid from sn_nodes where nodeid in(2,3,4,8) group by parentid;
                        DbCommand _cmd = DataHelper.GetSqlStringCommand("select count(parentid) as delCount,parentid from sn_nodes where nodeid in(" + nodeidStr + ") group by parentid");
                        _cmd.Connection = dbConn;
                        _cmd.Transaction = dbTran;
                        DataTable _dt = DataHelper.ExecuteDataTable(_cmd);
                        foreach (DataRow row in _dt.Rows)
                        {
                            DbCommand tmp_cmd = DataHelper.GetSqlStringCommand("select NodeId,ArrChildID from sn_nodes where nodeid=" + row["parentid"].ToString());
                            tmp_cmd.Connection = dbConn;
                            tmp_cmd.Transaction = dbTran;
                            DataTable p_dt = DataHelper.ExecuteDataTable(tmp_cmd);
                            string srcArrStr = p_dt.Rows[0]["ArrChildID"].ToString();
                            string newArrStr = setNodeChildList(srcArrStr, nodeidStr);
                            string tmpUpsql = "update sn_nodes set ChildsNum=ChildsNum-" + row["delCount"].ToString() + ",ArrChildID='" + newArrStr + "' where nodeid=" + row["parentid"].ToString();
                            _cmd.CommandText = tmpUpsql;
                            _cmd.ExecuteNonQuery();
                        }
                    }
                    DbCommand removeCmd = DataHelper.GetSqlStringCommand(removeSql);
                    removeCmd.Connection = dbConn;
                    removeCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(removeCmd, labelParams);
                    removeCmd.ExecuteNonQuery();
                    DbCommand groupCmd = DataHelper.GetSqlStringCommand(deleteGroupSql);
                    groupCmd.Connection = dbConn;
                    groupCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(groupCmd, labelParams);
                    groupCmd.ExecuteNonQuery();
                    DbCommand templateCmd = DataHelper.GetSqlStringCommand(deleteTemSql);
                    templateCmd.Connection = dbConn;
                    templateCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(templateCmd, labelParams);
                    templateCmd.ExecuteNonQuery();
                    //删除内容
                    foreach (KeyValuePair<string,List<string>> _item in modelIdList)
                    {
                        //修改模型使用数
                        DbCommand _modelCmd = DataHelper.GetSqlStringCommand("update sn_model set UseNumber=UseNumber-" + modelCountColls[_item.Key] + " where ModelID="+_item.Key);
                        _modelCmd.Connection = dbConn;
                        _modelCmd.Transaction = dbTran;
                        _modelCmd.ExecuteNonQuery();
                        DbCommand _tmpCmd = DataHelper.GetSqlStringCommand("delete from " + modelTableColls[_item.Key] + " where nodeid in (" + string.Join(",", _item.Value.ToArray()) + ")");
                        _tmpCmd.Connection = dbConn;
                        _tmpCmd.Transaction = dbTran;
                        _tmpCmd.ExecuteNonQuery();
                    }
                    DbCommand updatesiteCmd = DataHelper.GetSqlStringCommand("update sn_sites set nodecount=nodecount-" + node_arr.Length.ToString() + " where siteid=" + topChannel.siteID.ToString());
                    dbTran.Commit();
                    topChannel.parentNode.subNodeColls.Remove(minNodeId);
                    //移去内存中数据,移去节点
                    foreach (string nodeStr in node_arr)
                    {
                        int _id = Convert.ToInt32(nodeStr);
                        ChannelConfigManager.createInstance().channelDataColls[_id] = null;
                        ChannelConfigManager.createInstance().channelDataColls.Remove(_id);
                    }
                }
                catch (System.Exception ex)
                {
                    try { 
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
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", ex.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
        }
        private string restoreChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem, XNLContext XNLPage)
        {
            //先分隔nodeid
            try
            {
                string nodeId = labelParams["nodeid"].value.ToString();
                string[] node_arr = nodeId.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (node_arr.Length.Equals(0))
                {
                    throw (new Exception("没有指定栏目编号"));
                }
                for (int i = 0; i < node_arr.Length; i++)
                {
                    int _nodeId;
                    if (!int.TryParse(node_arr[i], out _nodeId))
                    {
                        throw (new Exception("栏目编号类型错误"));
                    }
                }
                string sql;
                string removeSql;
                //查找是否有子节点
                if (node_arr.Length.Equals(1))
                {
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid=@nodeid and b.parentid=a.nodeid and b.state=1";
                    removeSql = "update sn_nodes set state=0 where nodeid=@nodeid";
                }
                else
                {
                    string nodesStr=string.Join(",", node_arr);
                    sql = "select a.Nodeid from sn_nodes as a,sn_nodes as b where a.ChildsNum>0 and a.nodeid in(" + nodesStr + ") and b.parentid=a.nodeid and b.state=1 and b.nodeid not in (" + nodesStr + ")";
                    removeSql = "update sn_nodes set state=0 where nodeid in(" + nodesStr + ")";
                }
                DbCommand dbCmd = DataHelper.GetSqlStringCommand(sql);
                DbConnection dbConn = DataHelper.CreateConnection();
                dbConn.Open();
                DbTransaction dbTran = DataHelper.CreateTransaction(dbConn);
                dbCmd.Connection = dbConn;
                dbCmd.Transaction = dbTran;
                DataHelper.SetParameterValue(dbCmd, labelParams);
                //具体操作
                try
                {
                    DataTable dt = DataHelper.ExecuteDataTable(dbCmd);
                    if (dt.Rows.Count > 0) throw (new Exception("所选栏目有子栏目!"));
                    DbCommand removeCmd = DataHelper.GetSqlStringCommand(removeSql);
                    removeCmd.Connection = dbConn;
                    removeCmd.Transaction = dbTran;
                    DataHelper.SetParameterValue(removeCmd, labelParams);
                    removeCmd.ExecuteNonQuery();
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
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", ex.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
        }
        private string modifyChannel(Dictionary<string, XNLParam> labelParams, string labelContentStr, MatchCollection matchSuccessItem, MatchCollection matchFailItem, WebContext XNLPage)
        {
            string nodeIdStr =labelParams["nodeid"].value.ToString();
            int nodeId;
            if(!int.TryParse(nodeIdStr,out nodeId))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目编号类型不正确");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            string nodeName = "";
            if (!labelParams.ContainsKey("nodename") || string.IsNullOrEmpty(labelParams["nodename"].value.ToString()))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目名称不能为空");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            nodeName = labelParams["nodename"].value.ToString();
            //检查栏目名称
            if (!checkChannel(labelParams))
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", "栏目名称已存在");
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }
            string indexName = "";
            if (labelParams.ContainsKey("indexname") && !string.IsNullOrEmpty(labelParams["indexname"].value.ToString()))
            {
                indexName = labelParams["indexname"].value.ToString();
                if (checkIndexName(labelParams, XNLPage))
                {
                    Dictionary<string, string> errorList = new Dictionary<string, string>();
                    errorList.Add("1", "索引名称已存在");
                    labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                    return labelContentStr;
                }
            }
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTransaction = dbConn.BeginTransaction();
            try
            {
                string infoSql = "select SettingsXML from sn_nodes where nodeid="+nodeId;
                DbCommand infoCmd = DataHelper.GetSqlStringCommand(infoSql);
                infoCmd.Connection = dbConn;
                infoCmd.Transaction = dbTransaction;
                string xmlStr = Convert.ToString(infoCmd.ExecuteScalar());
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);
                string info = labelParams["info"].value.ToString();
                string matekeyword = labelParams["matekeyword"].value.ToString();
                string metadesc = labelParams["metadesc"].value.ToString();
                XmlNode xmlNode = xmlDoc.SelectSingleNode("ChannelConfig/BaseConfig");
                xmlNode.SelectSingleNode("Info").InnerText = info;
                xmlNode.SelectSingleNode("MetaKeyword").InnerText =matekeyword;
                xmlNode.SelectSingleNode("MetaDesc").InnerText = metadesc;
                
                labelParams.Add("setxml", new XNLParam(xmlDoc.OuterXml));
                string updateSql = "update SN_Nodes set NodeName=@nodeName,IndexName=@indexname,imageurl=@imageurl,LinkUrl=@LinkUrl,SettingsXML=@setxml  where nodeid=@nodeid";
                DbCommand updateCmd = DataHelper.GetSqlStringCommand(updateSql);
                updateCmd.Connection = dbConn;
                updateCmd.Transaction = dbTransaction;
                DataHelper.SetParameterValue(updateCmd, labelParams);
                updateCmd.ExecuteNonQuery();
                XNLParam groupsParam = null;
                Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
                if (labelParams.TryGetValue("nodegroup", out groupsParam))
                {
                    //再重新插入栏目组
                    string groups = groupsParam.value.ToString();
                    if (!groups.Trim().Equals(""))
                    {
                        DbCommand groupCmd = DataHelper.GetSqlStringCommand("delete from SN_NodeGroups where NGS_ID not in (" + groups + ") and ngs_nodeid="+nodeId) ;
                        groupCmd.Connection = dbConn;
                        groupCmd.Transaction = dbTransaction;
                        groupCmd.ExecuteNonQuery();
                        groupCmd.CommandText = "select NGS_ID from SN_NodeGroups  where ngs_nodeid=" + nodeId;
                        DataTable groupDt = DataHelper.ExecuteDataTable(groupCmd);
                        string[] groups_arr = groups.Split(new char[] { ',' });
                        if (groupDt.Rows.Count==0)
                        {
                            foreach (string str in groups_arr)
                            {
                                DbCommand insertCmd = DataHelper.GetSqlStringCommand("insert into SN_NodeGroups (NGS_ID,NGS_NodeID) values(" + str + "," + nodeId + ")");
                                insertCmd.Connection = dbConn;
                                insertCmd.Transaction = dbTransaction;
                                insertCmd.ExecuteNonQuery();
                            }  
                        }
                        else
                        {
                            List<string> groupList = new List<string>();
                            foreach (DataRow row in groupDt.Rows)
                            {
                                groupList.Add(row["groupid"].ToString());
                            }
                            foreach (string str in groups_arr)
                            {
                                if (!groupList.Contains(str))
                                {
                                    DbCommand insertCmd = DataHelper.GetSqlStringCommand("insert into SN_NodeGroups (NGS_ID,NGS_NodeID) values(" + str + "," + nodeId + ")");
                                    insertCmd.Connection = dbConn;
                                    insertCmd.Transaction = dbTransaction;
                                    insertCmd.ExecuteNonQuery();
                                }
                            }   
                        }
                    }
                    else
                    {
                        DbCommand groupCmd = DataHelper.GetSqlStringCommand("delete from SN_NodeGroups where  NGS_nodeid=" + nodeId);
                        groupCmd.Connection = dbConn;
                        groupCmd.Transaction = dbTransaction;
                        groupCmd.ExecuteNonQuery();
                    }
                }
                dbTransaction.Commit();
                curChannel.nodeName = nodeName;
                curChannel.nodeIndexName = indexName;
                curChannel.imageUrl = labelParams["imageurl"].value.ToString();
                curChannel.theChannelConfig.baseConfig.info = info;
                curChannel.theChannelConfig.baseConfig.metaKeyword = matekeyword;
                curChannel.theChannelConfig.baseConfig.metaDesc = metadesc;
                //在内存中设置此节点
                dbTransaction.Dispose();
                dbConn.Close();
                labelContentStr = XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
                return labelContentStr;
            }
            catch (Exception e)
            {
                dbTransaction.Rollback();
                dbTransaction.Dispose();
                dbConn.Close();
                Dictionary<string, string> errorList = new Dictionary<string, string>();
                errorList.Add("1", e.Message);
                labelContentStr = XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
                return labelContentStr;
            }      
        }
        private string setNodeChildList(string arrStr,string removeId)
        {
            string []_arr=arrStr.Split(new char[1]{','},StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>(_arr);
            string[] _removeArr = removeId.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in _removeArr)
            {
                list.Remove(i);
            }
            return string.Join(",", list.ToArray());
        }
        private void upIndex(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
        {
            int sortNum = Convert.ToInt32(labelParams["sortnum"].value);
            if(sortNum<=0)return;
            //得到当前的索引
            DbConnection dbConn=DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran=DataHelper.CreateTransaction(dbConn);
            try
            {
                DbCommand indexCmd = DataHelper.GetSqlStringCommand("select min(b.[indexid]) as minIndex,a.[ParentID], a.[indexid],a.[sortid]  from sn_nodes a,sn_nodes b  where a.nodeid=@nodeid and b.ParentID=a.ParentID group by a.indexid,a.parentid,a.sortid");
                indexCmd.Connection=dbConn;
                indexCmd.Transaction=dbTran;
                DataHelper.SetParameterValue(indexCmd,labelParams);
                DataTable indexDt = DataHelper.ExecuteDataTable(indexCmd);
                DataRow indexRow;
                if(indexDt.Rows.Count==0)throw(new Exception("栏目已不存在"));
                indexRow=indexDt.Rows[0];
                int curIndex = Convert.ToInt32(indexRow["IndexID"]);
                int parentId = Convert.ToInt32(indexRow["ParentID"]);
                int minIndex = Convert.ToInt32(indexRow["minIndex"]);
                string curSortId = Convert.ToString(indexRow["sortid"]);
                if (parentId>0&&curIndex > minIndex) //如果当前索引大于最小的
                {
                    ////得到要移动到的位置的索引
                    indexCmd.CommandText = "select [indexid] from sn_nodes where nodeid<>@nodeid and indexid<=" + curIndex + " and ParentID=" + parentId + " order by indexid desc";
                    indexDt = DataHelper.ExecuteDataTable(indexCmd);
                    int toNum;
                    DataRowCollection indexRows=indexDt.Rows;
                    if (indexRows.Count > 0)
                    {
                        if (indexRows.Count < sortNum)
                        {
                            DataRow _row=indexRows[indexRows.Count - 1];
                            toNum = Convert.ToInt32(_row[0]);
                        }
                        else
                        {
                            DataRow _row = indexRows[sortNum - 1];
                            toNum = Convert.ToInt32(_row[0]);
                        }
                        //得到新排序编号
                        string parentSort = curSortId.Remove(curSortId.Length - 4);
                        string newSortStr = parentSort + getSortIdByIndex(toNum);
                        //更新索引
                        indexCmd.CommandText = "update sn_nodes set indexid=" + toNum + ",sortid='"+newSortStr+"' where nodeid=@nodeid";
                        indexCmd.ExecuteNonQuery();
                        //更新其它栏目索引
                        indexCmd.CommandText = "select nodeid,indexid from sn_nodes where nodeid<>@nodeid and ParentID=" + parentId + " and IndexID<" + curIndex + " and indexid>=" + toNum;
                        DataTable dt = DataHelper.ExecuteDataTable(indexCmd);
                        Dictionary<int, Cchannel> newColls = new Dictionary<int, Cchannel>();
                        SafeDictionary<int, Cchannel> _tmpNodeColls = ChannelConfigManager.createInstance().channelDataColls;
                        Cchannel curNode=_tmpNodeColls[Convert.ToInt32(labelParams["nodeid"].value)];
                        getAllChildList(curNode, newColls);
                        foreach (DataRow row in dt.Rows)
                        {
                            int nodeid = Convert.ToInt32(row[0]);
                            int _indexid = Convert.ToInt32(row[1]);
                            indexCmd.CommandText = "update sn_nodes set indexid=indexid+1,sortid='" + parentSort + getSortIdByIndex(_indexid+1) + "' where nodeid="+nodeid.ToString();
                            indexCmd.ExecuteNonQuery();
                            getAllChildList(_tmpNodeColls[nodeid], newColls);
                        }
                        string subStr = getNodeLsString(newColls);
                        if (subStr != "")
                        {
                            dt = DataHelper.ExecuteDataTable("select nodeid,indexid,parentid from sn_nodes where RootID=" +curNode.siteNode.nodeID.ToString()+ " and nodeid in(" + subStr + ")");
                            //更新所有子栏目索引
                            foreach (DataRow row in dt.Rows)
                            {
                                //curSortId = Convert.ToString(row[2]);
                                int nodeid = Convert.ToInt32(row[0]);
                                indexCmd.CommandText="select SortId from sn_nodes where nodeid="+Convert.ToString(row[2]);
                                parentSort = DataHelper.ExecuteScalar(indexCmd).ToString(); //curSortId.Remove(curSortId.Length - 4);
                                int _indexid = Convert.ToInt32(row[1]);
                                indexCmd.CommandText = "update sn_nodes set sortid='" + parentSort + getSortIdByIndex(_indexid) + "' where nodeid=" + nodeid.ToString();
                                indexCmd.ExecuteNonQuery();
                            }
                        }
                        //indexCmd.CommandText = "update sn_nodes set indexid=indexid+1 where nodeid<>@nodeid and ParentID=" + parentId + " and IndexID<" + curIndex + " and indexid>=" + toNum;
                    }
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
            }
        }
        private void downIndex(Dictionary<string, XNLParam> labelParams, string labelContentStr, XNLContext XNLPage)
        {
            int sortNum = Convert.ToInt32(labelParams["sortnum"].value);
            if(sortNum<=0)return;
            //得到当前的索引
            DbConnection dbConn=DataHelper.CreateConnection();
            dbConn.Open();
            DbTransaction dbTran=DataHelper.CreateTransaction(dbConn);
            try
            {
                DbCommand indexCmd = DataHelper.GetSqlStringCommand("select max(b.[indexid]) as maxIndex,a.[ParentID], a.[indexid],a.[sortid] from sn_nodes a,sn_nodes b  where a.nodeid=@nodeid and b.ParentID=a.ParentID group by a.indexid,a.parentid,a.sortid");
                indexCmd.Connection=dbConn;
                indexCmd.Transaction=dbTran;
                DataHelper.SetParameterValue(indexCmd,labelParams);
                DataTable indexDt = DataHelper.ExecuteDataTable(indexCmd);
                DataRow indexRow;
                if(indexDt.Rows.Count==0)throw(new Exception("栏目已不存在"));
                indexRow=indexDt.Rows[0];
                int curIndex = Convert.ToInt32(indexRow["IndexID"]);
                int parentId = Convert.ToInt32(indexRow["ParentID"]);
                int maxIndex=Convert.ToInt32(indexRow["maxIndex"]);
                string curSortId = Convert.ToString(indexRow["sortid"]);
                if (parentId > 0 &&curIndex < maxIndex)
                {
                    //得到要移动到的位置的索引
                    indexCmd.CommandText = "select [indexid] from sn_nodes where nodeid<>@nodeid and indexid>=" + curIndex + " and ParentID=" + parentId + " order by indexid";
                    indexDt = DataHelper.ExecuteDataTable(indexCmd);
                    int toNum;
                    DataRowCollection indexRows = indexDt.Rows;
                    if (indexRows.Count > 0)
                    {
                        if (indexRows.Count < sortNum)
                        {
                            toNum = Convert.ToInt32(indexRows[indexRows.Count - 1][0]);
                        }
                        else
                        {
                            toNum = Convert.ToInt32(indexRows[sortNum - 1][0]);
                        }
                        //得到新排序编号
                        string parentSort = curSortId.Remove(curSortId.Length - 4);
                        string newSortStr = parentSort + getSortIdByIndex(toNum);
                        //更新索引
                        indexCmd.CommandText = "update sn_nodes set indexid=" + toNum + ",sortid='"+newSortStr+"' where nodeid=@nodeid";
                        indexCmd.ExecuteNonQuery();
                        //更新其它栏目索引
                        indexCmd.CommandText = "select nodeid,indexid from sn_nodes where nodeid<>@nodeid and ParentID=" + parentId + " and IndexID>" + curIndex + " and indexid<=" + toNum;
                        DataTable dt = DataHelper.ExecuteDataTable(indexCmd);
                        Dictionary<int, Cchannel> newColls = new Dictionary<int, Cchannel>();
                        SafeDictionary<int, Cchannel> _tmpNodeColls = ChannelConfigManager.createInstance().channelDataColls;
                        Cchannel curNode = _tmpNodeColls[Convert.ToInt32(labelParams["nodeid"].value)];
                        getAllChildList(curNode, newColls);
                        foreach (DataRow row in dt.Rows)
                        {
                            int nodeid = Convert.ToInt32(row[0]);
                            int _indexid = Convert.ToInt32(row[1]);
                            indexCmd.CommandText = "update sn_nodes set indexid=indexid-1,sortid='" + parentSort + getSortIdByIndex(_indexid - 1) + "' where nodeid=" + nodeid.ToString();
                            indexCmd.ExecuteNonQuery();
                            getAllChildList(_tmpNodeColls[nodeid], newColls);
                        }
                        string subStr = getNodeLsString(newColls);
                        if (subStr != "")
                        {
                            dt = DataHelper.ExecuteDataTable("select nodeid,indexid,parentid from sn_nodes where RootID=" + curNode.siteNode.nodeID.ToString() + " and nodeid in(" + subStr + ")");
                            //更新所有子栏目索引
                            foreach (DataRow row in dt.Rows)
                            {
                                int nodeid = Convert.ToInt32(row[0]);
                                indexCmd.CommandText = "select SortId from sn_nodes where nodeid=" + Convert.ToString(row[2]);
                                parentSort = DataHelper.ExecuteScalar(indexCmd).ToString(); //curSortId.Remove(curSortId.Length - 4);
                                int _indexid = Convert.ToInt32(row[1]);
                                indexCmd.CommandText = "update sn_nodes set sortid='" + parentSort + getSortIdByIndex(_indexid) + "' where nodeid=" + nodeid.ToString();
                                indexCmd.ExecuteNonQuery();
                            }
                        }
                        //indexCmd.CommandText = "update sn_nodes set indexid=indexid-1 where nodeid<>@nodeid and ParentID=" + parentId + " and IndexID>" + curIndex + " and indexid<=" + toNum;
                        //indexCmd.ExecuteNonQuery();
                    }
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
            }
        }
      /// <summary>
      /// 删除栏目组
      /// </summary>
      /// <param name="labelParams"></param>
      /// <param name="XNLPage"></param>
        private void deleteGroup(Dictionary<string, XNLParam> labelParams, XNLContext XNLPage)
        {
            string groupIdStr = labelParams["groupid"].value.ToString().Trim();
            if (groupIdStr.Equals(""))
            {
                throw (new Exception("没有指定栏目组编号"));
            }
            if (!Regex.IsMatch(groupIdStr.Replace(",", ""), "\\d+"))
            {
                throw (new Exception("错误的栏目组编号"));
            }
            string sql1 = "delete from SN_NodeGroup where NG_ID =" + groupIdStr;
            string sql2 = "delete from SN_NodeGroups where NGS_ID=" + groupIdStr;
            if(groupIdStr.IndexOf(',')>=0)
            {
                sql1 = "delete from SN_NodeGroup where NG_ID in(" + groupIdStr + ")";
                sql2 = "delete from SN_NodeGroups where NGS_ID in(" + groupIdStr + ")";
            }
            Dictionary<string, string> sqlColls = new Dictionary<string, string>(2);
            sqlColls.Add("1", sql1);
            sqlColls.Add("2", sql2);
            DbResultInfo dbInfo=DataHelper.ExrcuteNonQuerySomeSqlWithTransaction(sqlColls, labelParams);
            if(!dbInfo.isSuccess)
            {
                throw (dbInfo.exception);
            }
        }
        private void deleteGroupNode(Dictionary<string, XNLParam> labelParams, XNLContext XNLPage)
        {
            string groupIdStr = labelParams["groupid"].value.ToString().Trim();
            if (groupIdStr.Equals(""))
            {
                throw (new Exception("没有指定栏目组编号"));
            }
            else if (!Regex.IsMatch(groupIdStr.Replace(",", ""), "\\d+"))
            {
                throw (new Exception("错误的栏目组编号"));
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
            string sql = "delete from SN_NodeGroups where NGS_ID =" + groupIdStr + " and NGS_nodeId=" + nodeIdStr;
            if (nodeIdStr.IndexOf(',') >= 0)
            {
                sql = "delete from SN_NodeGroups where NGS_ID=" + groupIdStr + " and NGS_nodeId in(" + nodeIdStr + ")";
            }
            DataHelper.ExecuteNonQuery(sql);
        }
        private string getSortIdByIndex(int indexId)
        {
            string _sortStr = "";
            for (int i = 0; i < 4 - indexId.ToString().Length; i++)
            {
                _sortStr += "0";
            }
            _sortStr += indexId.ToString();
            return _sortStr;
        }
        private void getAllChildList(Cchannel curChannel, Dictionary<int, Cchannel> nodeColls)
        {
            if (curChannel.subNodeColls != null && !nodeColls.ContainsKey(curChannel.nodeID))
            {
                foreach (KeyValuePair<int, Cchannel> nn in curChannel.subNodeColls)
                {
                    if (nn.Value.siteID == curChannel.siteID)
                    {
                        getAllChildList(nn.Value, nodeColls);
                        if (!nodeColls.ContainsKey(nn.Key)) nodeColls.Add(nn.Key, nn.Value);
                    }
                }
            }
        }
        private string getNodeLsString(Dictionary<int, Cchannel> nodeColls)
        {
            int i = 0;
            string ns = "";
            foreach (KeyValuePair<int, Cchannel> n in nodeColls)
            {
                ns += (i == 0 ? n.Key.ToString() : "," + n.Key.ToString());
                i++;
            }
            return ns;
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
