using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public  class Common:IXNLTag<WebContext>
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
            string actionStr =labelParams["action"].value.ToString().ToLower();
            switch (actionStr)
            {
                    //得到后台当前管理的站点编号
                case "getcursiteid":
                    return ManageUtil.getCurSiteID(XNLPage).ToString();
                    //得到相应站点的模型编号
                case "getmodelid":
                    string modelId = "0";
                    if (!labelParams.ContainsKey("nodeid"))
                    {
                        return modelId;
                    }
                    return ChannelConfigManager.createInstance().channelDataColls[Convert.ToInt32(labelParams["nodeid"].value)].model.ModelId.ToString();
                case "getmodelname":
                    int nodeid=Convert.ToInt32(labelParams["nodeid"].value);
                    return ManageUtil.getModelName(nodeid);
                case "getsiteidbysn":  //由当前站点的nodeid得到站点的siteid
                    return ChannelConfigManager.createInstance().channelDataColls[ManageUtil.getCurSiteID(XNLPage)].siteNode.siteID.ToString();
                case "getcuradmin":
                    return ManageUtil.getCurAdminName();
                case "checknodegroup":
                    int _nodeid = Convert.ToInt32(labelParams["nodeid"].value);
                    return ManageUtil.checkNodeGroup(_nodeid);
                case "issiteright": //是否有管理站点权限
                    int roleId=Convert.ToInt32(labelParams["roleid"].value);
                    int siteNodeId = Convert.ToInt32(labelParams["sitenodeid"].value);
                    AdminRole roleObj;
                    if(XNLPage.Context.Items.Contains("role"+roleId))
                    {
                        roleObj = XNLPage.Context.Items["role" + roleId] as AdminRole;
                    }
                    else
                    {
                        roleObj = new AdminRole();
                        roleObj.fileInfoById(roleId);
                        XNLPage.Context.Items.Add("role" + roleId,roleObj);
                    }
                    return (roleObj.isAllSiteRights||roleObj.ContainsSite(siteNodeId)).ToString();
                case "ischannelright":
                    int roleId2 = Convert.ToInt32(labelParams["roleid"].value);
                    int nodeId = Convert.ToInt32(labelParams["nodeid"].value);
                    AdminRole roleObj2;
                    if (XNLPage.Context.Items.Contains("role" + roleId2))
                    {
                        roleObj2 = XNLPage.Context.Items["role" + roleId2] as AdminRole;
                    }
                    else
                    {
                        roleObj2 = new AdminRole();
                        roleObj2.fileInfoById(roleId2);
                        XNLPage.Context.Items.Add("role" + roleId2, roleObj2);
                    }
                    int siteId = -1;
                    if (ChannelConfigManager.createInstance().channelDataColls.ContainsKey(nodeId))
                    {
                        siteId = ChannelConfigManager.createInstance().channelDataColls[nodeId].siteNode.nodeID;
                        if(roleObj2.siteNodeRightsList!=null&&roleObj2.siteNodeRightsList.ContainsKey(siteId))
                        {
                            CSiteRight siteRight=roleObj2.siteNodeRightsList[siteId];
                            if(siteRight.isAllNodeRights||(siteRight.nodeRightsList!=null&&siteRight.nodeRightsList.ContainsKey(nodeId)))
                            {
                                return "True";
                            }
                            else{return "False";}
                        }
                        else
                        {
                            return "False";
                        }
                    }
                    else
                    {
                        return "False";
                    }
            }
           */ 
            return "";
        }

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
