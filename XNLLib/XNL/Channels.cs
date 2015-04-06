using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using System.Text.RegularExpressions;
using COM.SingNo.XNLEngine;
using System.Data;
using COM.SingNo.Common;
using COM.SingNo.DAL;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class Channels:IXNLTag<WebContext>
    {
        /*
         * siteid,sitename
         *channelid channelindex ,channelname,channelgroup,channelgroupnot, uplevel
         * upLevel     显示上几级栏目的下级栏目列表，0为显示下级栏目列表，1为显示同级栏目列表，2为显示父栏目的同级栏目列表，以此类推。 
topLevel     显示从首页向下某个级别栏目的下级栏目列表，0代表网站首页，1代表一级栏目，以此类推。 
 isself=false ;
isAllChild     是否显示所有级别的子栏目 
  true   显示所有级别的子栏目 
  false  只显示子栏目，不包括子栏目的子栏目。 
isImage     是否只显示带有图片的栏目。此属性只可属于以下可能的取值的一种。 
  true   只显示带有图片的栏目 
  false  显示所有的栏目 
groupchannel     栏目组别，如设置此属性，则只显示栏目组别等于group的栏目列表。 
groupchannelNot     非栏目组别，如设置此属性，则只显示栏目组别不等于group的栏目列表。 
order     栏目列表排序方式。此属性只可属于以下可能的取值的一种。 
  Default  默认排序，即栏目管理界面中的排序 
  Back   默认排序的相反方向 
  AddDate   按添加时间排序 
  AddDateBack   按添加时间的相反方向排序 
  Hits   按点击量排序 
  Random   随机显示栏目 
totalNum     设置列表一共显示多少条信息，0代表显示所有信息。 
startNum   1 从第几条信息开始显示，默认从第一条信息开始显示。
where
        <XNL:Channels channelid="" channelIndex="" channelName=""   groupChannel="" groupChannelNot="" order="" totalNum="" startNum="" isImage="" where="">
        <channels.header></channels.header>
         *  <channels.Footer></channels.Footer>
         *  <channels.Item></channels.Item>
         *  <channels.Alternating></channels.Alternating>
         *  <channels.Separator step="1"></channels.Separator>
         *  <channels.Selected></channels.Selected>
         *  <channels.empty></channels.empty>
        </XNL:Channels>
         * {@Channels.Name} {@Channels.Id} {@Channels.IndexName} {@Channels.Content} {@Channels.AddDate}
         * {@Channels.ImageUrl} {@Channels.IndexId} {@Channels.ItemId} {@Channels.childCount} {@Channel.ContentsCount} {@Channels.ImgContentsCount}
         */
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
       public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            return "";
           /*
            if (labelContentStr.Trim().Equals(""))
            {
                return labelContentStr;
            }
            MatchCollection channelsColls = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "xnl", "channels");
            if (channelsColls.Count>0)labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, channelsColls);
            Cchannel curSite=XNLPage.curChannel.siteNode;
            XNLParam _tmpParam;
            if (labelParams.TryGetValue("siteid", out _tmpParam))
            {
                curSite = SiteConfigManager.createInstance().siteDataColls[Convert.ToInt32(_tmpParam.value)];  
            }
            else if (labelParams.TryGetValue("sitename", out _tmpParam))
            {
                curSite = XNLUtils.getSiteByName(_tmpParam.value.ToString());  
            }
            if (curSite == null) throw (new Exception("此站点不存在!"));
            XNLParam _groupParam = null;
            XNLParam _groupnotParam = null;
            string sql = "select ";
            int itemCount = 0;
            labelParams.Add("channels.count", new XNLParam(itemCount));
            if (labelParams.TryGetValue("totalnum", out _tmpParam))
            {
                sql += "top " + _tmpParam.value.ToString();
            }
            string order = "default";
            if (labelParams.TryGetValue("order", out _tmpParam))
            {
                order = _tmpParam.value.ToString().ToLower();
            }
            Dictionary<int, Cchannel> nodeColls = new Dictionary<int, Cchannel>();
            if (labelParams.TryGetValue("groupchannel", out _groupParam))
            {
                sql += " nodeid,indexid,ParentPath,linkurl from sn_nodes where rootid=@sitenodeid ";
                string groupsName = _groupParam.value.ToString();
                if (groupsName.IndexOf(',') >= 0)
                {
                    string groups = "";
                    string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < group_arr.Length; i++)
                    {
                        groups += (i > 0 ? ",'" : "'") + group_arr[i].Replace("'", "''") + "'";
                    }
                    sql += "and nodeid in (select NGS_NodeID from SN_NodeGroup ,SN_NodeGroups  where NG_Name in(" + groups + ") and NGS_ID=NG_ID and NG_siteid=@siteid @order) ";
                }
                else
                {
                    sql += "and nodeid in (select NGS_NodeID from SN_NodeGroup ,SN_NodeGroups  where NG_Name='" + _groupParam.value.ToString().Replace("'", "''") + "' and NGS_ID=NG_ID and NG_siteid=@siteid @order) ";
                }
                //if (_groupParam != null)
                //{
                    
                //}
                //else if (_groupnotParam != null)
                //{
                //    sql += "and nodeid in (select d.NodeID from SN_NodeGroup c,SN_NodeGroups d where c.GroupName not in(" + _groupnotParam.theValue.ToString().Replace("'", "''") + ") and d.GroupID=c.GroupID and c.siteid=@siteid) ";
                //}
                if (labelParams.TryGetValue("isimage", out _tmpParam) && string.Compare("true", _tmpParam.value.ToString(), true) == 0)
                {
                    sql += "and imageurl<>\"\" ";
                }
                if (labelParams.TryGetValue("where", out _tmpParam))
                {
                    sql +="and "+ _tmpParam.value.ToString()+" ";
                }
               
                switch (order)
                {
                    case "default":
                        sql = sql.Replace("@order", "order by NGS_indexid");
                        break;
                    case "back":
                        sql = sql.Replace("@order", "order by NGS_indexid desc");
                        break;
                    case "adddate":
                        sql += " order by adddate";
                        break;
                    case "adddateback":
                        sql += " order by adddate desc";
                        break;
                    //case "hits":
                    // sql += " order by adddate";
                    // break;
                    case "random":
                        Random r = new Random();
                        int index = r.Next(0, 3);
                        string[] sql_arr = new string[] { sql.Replace("@order", "order by NGS_indexid"), sql.Replace("@order", "order by NGS_indexid desc"), sql + " order by adddate", sql + " order by adddate desc" };
                        sql= sql_arr[index];
                        break;
                }
                itemCount = 1;
            }
            else if (labelParams.TryGetValue("groupchannelnot", out _groupnotParam))
            {
                sql += " nodeid,indexid,ParentPath,linkurl from sn_nodes where rootid=@sitenodeid ";
                if (_groupnotParam.value.ToString().IndexOf(',') >= 0)
                {
                    sql += "and nodeid in (select NGS_NodeID from SN_NodeGroup,SN_NodeGroups where NG_Name not in(" + _groupnotParam.value.ToString().Replace("'", "''") + ") and NGS_ID=NG_ID and NG_siteid=@siteid) ";
                }
                else
                {
                    sql += "and nodeid in (select NGS_NodeID from SN_NodeGroup,SN_NodeGroups where NG_Name<>'" + _groupnotParam.value.ToString().Replace("'", "''") + "' and NGS_ID=NG_ID and NG_siteid=@siteid) ";
                }
                if (labelParams.TryGetValue("isimage", out _tmpParam) && string.Compare("true", _tmpParam.value.ToString(), true) == 0)
                {
                    sql += "and imageurl<>\"\" ";
                }
                if (labelParams.TryGetValue("where", out _tmpParam))
                {
                    sql +="and "+ _tmpParam.value.ToString()+" ";
                }
               
                switch (order)
                {
                    case "default":
                        sql = sql.Replace("@order", "order by NGS_indexid");
                        break;
                    case "back":
                        sql = sql.Replace("@order", "order by NGS_indexid desc");
                        break;
                    case "adddate":
                        sql += " order by adddate";
                        break;
                    case "adddateback":
                        sql += " order by adddate desc";
                        break;
                    //case "hits":
                    // sql += " order by adddate";
                    // break;
                    case "random":
                        Random r = new Random();
                        int index = r.Next(0, 3);
                        string[] sql_arr = new string[] { sql.Replace("@order", "order by NGS_indexid"), sql.Replace("@order", "order by NGS_indexid desc"), sql + " order by adddate", sql + " order by adddate desc" };
                        sql= sql_arr[index];
                        break;
                }
                itemCount = 1;
            }
            else
            {
                Cchannel curChannel = XNLPage.curChannel;
                int channelid = curChannel.nodeID;
                if (labelParams.TryGetValue("channelid", out _tmpParam))
                {
                    channelid = Convert.ToInt32(_tmpParam.value);
                    if (channelid != curChannel.nodeID)
                    {
                        curChannel = ChannelConfigManager.createInstance().channelDataColls[channelid];
                    }
                }
                else if (labelParams.TryGetValue("channelindex", out _tmpParam))
                {
                    curChannel = XNLUtils.getChannelByIndexFromChannel(_tmpParam.value.ToString(), curSite);
                }
                else if (labelParams.TryGetValue("channelname", out _tmpParam))
                {
                    curChannel = XNLUtils.getChannelByNameFromChannel(_tmpParam.value.ToString(), curSite);
                }
                if (curChannel == null) throw (new Exception("栏目不存在"));
                //检查是否有uplevel参数，检查是否有toplevel参数
                if (labelParams.TryGetValue("uplevel", out _tmpParam))
                {
                    int upLevel = Convert.ToInt32(_tmpParam.value);
                    if (upLevel == 0)
                    {
                        if (curChannel.subNodeColls != null)
                        {
                            foreach (KeyValuePair<int, Cchannel> n in curChannel.subNodeColls)
                            {
                                nodeColls.Add(n.Key, n.Value);
                            }
                        }
                    }
                    else if (upLevel > 0)
                    {
                        if (curChannel.depth - upLevel >= curChannel.siteNode.depth) //在有效范围内
                        {
                            Cchannel _n = curChannel;
                            for (int i = 1; i >= upLevel; i++)
                            {
                                _n = _n.parentNode;
                            }
                            foreach (KeyValuePair<int, Cchannel> n in _n.subNodeColls)
                            {
                                nodeColls.Add(n.Key, n.Value);
                            }
                        }
                        else
                        {
                            nodeColls.Add(curChannel.siteNode.nodeID, curChannel.siteNode);
                        }
                    }
                }
                if (labelParams.TryGetValue("toplevel", out _tmpParam))
                {
                    int topLevel = Convert.ToInt32(_tmpParam.value) + 1;
                    //得到相应层级的下级子栏目
                    if (topLevel >= 1) getNodeByDepth(curChannel.siteNode, topLevel, nodeColls);
                }
                if (labelParams.TryGetValue("isallchild", out _tmpParam) && string.Compare("true", _tmpParam.value.ToString(), true) == 0)
                {
                    Dictionary<int, Cchannel> newColls = new Dictionary<int, Cchannel>();
                    if (nodeColls.Count == 0)
                    {
                        getAllChildList(curChannel, newColls);
                    }
                    else if (nodeColls.ContainsKey(curSite.nodeID))
                    {
                        getAllChildList(curSite, newColls);
                    }
                    else
                    {
                        foreach (KeyValuePair<int, Cchannel> n in nodeColls)
                        {
                            getAllChildList(n.Value, newColls);
                        }
                    }
                    foreach (KeyValuePair<int, Cchannel> n in newColls)
                    {
                        if (!nodeColls.ContainsKey(n.Key)) nodeColls.Add(n.Key, n.Value);
                    }
                }
                else
                {
                    if (nodeColls.Count == 0)
                    {
                        if (curChannel.subNodeColls != null)
                        {
                            foreach (KeyValuePair<int, Cchannel> n in curChannel.subNodeColls)
                            {
                                if (!nodeColls.ContainsKey(n.Key)) nodeColls.Add(n.Key, n.Value);
                            }
                        }
                    }
                    else
                    {
                        Dictionary<int, Cchannel> newColls = new Dictionary<int, Cchannel>();
                        foreach (KeyValuePair<int, Cchannel> n in nodeColls)
                        {
                            if (n.Value.subNodeColls != null)
                            {
                                foreach (KeyValuePair<int, Cchannel> nn in n.Value.subNodeColls)
                                {
                                    if (!newColls.ContainsKey(nn.Key)) newColls.Add(nn.Key, nn.Value);
                                }
                            }
                        }
                        foreach (KeyValuePair<int, Cchannel> n in newColls)
                        {
                            if (!nodeColls.ContainsKey(n.Key)) nodeColls.Add(n.Key, n.Value);
                        }
                    }
                }

                if (labelParams.TryGetValue("isself", out _tmpParam) && string.Compare("true", _tmpParam.value.ToString(), true) == 0)
                {
                    if (!nodeColls.ContainsKey(curChannel.nodeID)) nodeColls.Add(curChannel.nodeID, curChannel);
                }
                itemCount = nodeColls.Count;
                string nodeLsStr = getNodeLsString(nodeColls);
                if (nodeLsStr != string.Empty)
                {
                    sql += " nodeid,indexid,parentpath,linkurl from sn_nodes where nodeid in (";
                    sql += nodeLsStr + ") and RootID=@sitenodeid";
                    if (labelParams.TryGetValue("isimage", out _tmpParam) && string.Compare("true", _tmpParam.value.ToString(), true) == 0)
                    {
                        sql += "and imageurl<>\"\" ";
                    }
                    if (labelParams.TryGetValue("where", out _tmpParam))
                    {
                        sql += "and " + _tmpParam.value.ToString() + " ";
                    }
                    switch (order)
                    {
                        case "default":
                            sql += " order by sortid";
                            break;
                        case "back":
                            sql += " order by sortid desc";
                            break;
                        case "adddate":
                            sql += " order by adddate desc";
                            break;
                        case "adddateback":
                            sql += " order by adddate";
                            break;
                        //case "hits":
                        // sql += " order by adddate";
                        // break;
                        case "random":
                            Random r = new Random();
                            int index = r.Next(0, 3);
                            string[] o_arr = new string[] { "default", "back" };
                            string[] sql_arr = new string[] { " order by sortid", " order by sortid desc", " order by adddate desc", " order by adddate" };
                            sql += sql_arr[index];
                            if (index < 2) order = o_arr[index];
                            break;
                    }
                }
            }
            Match itemMatch = null;
            Match emptyMatch = null;
            Match headMatch = null;
            Match footMatch = null;
            Match alterMatch = null;
            Match separMatch = null;
            MatchCollection itemVarColls = null;
            MatchCollection alterItemVarColls = null;
            MatchCollection spearItemVarColls = null;
           // MatchCollection headItemVarColls = null;
           // MatchCollection footItemVarColls = null;
            int SpearStepNum = 1;
            MatchCollection itemsColls = Regex.Matches(labelContentStr, @"<channels\.([a-z]+)[\s]*[^<>]*>(.*)</channels\.\1>", XNLBaseCommon.XNL_RegexOptions);
            //替换数据行
            StringBuilder contentSb = new StringBuilder(labelContentStr);
            foreach (Match match in itemsColls)
            {
                switch (match.Groups[1].Value.ToLower())
                {
                    case "item":
                        itemMatch = match;
                        contentSb.Replace(match.Value, "◇singno_channels[head]◎◇singno_channels[item]◎◇singno_channels[foot]◎");
                        itemVarColls = Regex.Matches(match.Groups[2].Value, RegxpEngineCommon.RegexStr_XNLAttriableVariable, XNLBaseCommon.XNL_RegexOptions);
                        break;
                    case "empty":
                        emptyMatch = match;
                        contentSb.Replace(match.Value,"");
                        break;
                    case "header":
                        headMatch = match;
                        contentSb.Replace(match.Value, "");
                        //headItemVarColls = Regex.Matches(match.Groups[2].Value, XNLCommon.matchXNLAttriableVariableString, XNLCommon.Matchoptions);
                        break;
                    case "footer":
                        footMatch = match;
                        contentSb.Replace(match.Value, "");
                        //footItemVarColls = Regex.Matches(match.Groups[2].Value, XNLCommon.matchXNLAttriableVariableString, XNLCommon.Matchoptions);
                        break;
                    case "alternating":
                        alterMatch = match;
                        if (itemMatch == null)
                        {
                            contentSb.Replace(match.Value, "◇singno_channels[head]◎◇singno_channels[item]◎◇singno_channels[foot]◎");
                        }
                        else
                        {
                            contentSb.Replace(match.Value, "");
                        }
                        alterItemVarColls = Regex.Matches(match.Groups[2].Value, RegxpEngineCommon.RegexStr_XNLAttriableVariable, XNLBaseCommon.XNL_RegexOptions);
                        break;
                    case "separator":
                        separMatch = match;
                        if (itemMatch == null && alterMatch == null)
                        {
                            contentSb.Replace(match.Value, "◇singno_channels[head]◎◇singno_channels[item]◎◇singno_channels[foot]◎");
                        }
                        else
                        {
                            contentSb.Replace(match.Value, "");
                        }
                        int startin=match.Groups[1].Index;
                        int len=match.Groups[2].Index-startin;
                        string paramStr = match.Value.Substring(startin,len);
                        Match tmpMatch = Regex.Match(match.Value, "step=\"(\\d+)\"", XNLBaseCommon.XNL_RegexOptions);
                        if (tmpMatch.Success)
                        {
                            SpearStepNum=Convert.ToInt32(tmpMatch.Groups[1].Value);
                        }
                        spearItemVarColls = Regex.Matches(match.Groups[2].Value, RegxpEngineCommon.RegexStr_XNLAttriableVariable, XNLBaseCommon.XNL_RegexOptions);
                        break;
                }
            }
            if (itemCount>0)
            {
                sql = sql.Replace("@siteid", curSite.siteID.ToString());
                sql = sql.Replace("@sitenodeid", curSite.nodeID.ToString());
                DbConnection dbConn = DataHelper.CreateConnection();
                dbConn.Open();
                DbCommand cmd = DataHelper.GetSqlStringCommand(sql);
                cmd.Connection = dbConn;
                DataTable dt = DataHelper.ExecuteDataTable(cmd);
                dbConn.Close();
                if (dt.Rows.Count == 0)
                {
                    string cStr = contentSb.ToString();
                    contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));
                    return setEmptyContent(contentSb, labelParams, channelsColls, itemMatch, alterMatch, headMatch, footMatch, separMatch, emptyMatch);
                }
                else
                {
                    int startNum=0;
                    if (labelParams.TryGetValue("startnum", out _tmpParam))
                    {
                        startNum = Convert.ToInt32(_tmpParam.value);
                    }
                    labelParams["channels.count"].value = dt.Rows.Count-startNum;
                    string cStr = contentSb.ToString();
                    contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));
                        #region 排序不需要了
                        //if (order.Equals("default") || order.Equals("back"))
                        //{
                        //    //整理排序
                        //    DataTable parentTable = DataHelper.ExecuteDataTable("select nodeid, indexid from sn_nodes where RootID="+curSite.nodeID+" and ChildsNum>0");
                        //    dt.Columns.Add(new DataColumn("sn_tmp_sortid", System.Type.GetType("System.String")));
                        //    foreach (DataRow row in dt.Rows)
                        //    {
                        //        string parentpath = row["parentpath"].ToString();
                        //        if (!parentpath.Equals("0")) //0,2
                        //        {
                        //            string trueParents = parentpath.Remove(0, 2);
                        //            if (trueParents.Equals(row["nodeid"].ToString()))
                        //            {
                        //                row["sn_tmp_sortid"] = getSortString(row["indexid"].ToString());
                        //            }
                        //            else
                        //            {
                        //                string[] parent_arr = trueParents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //                string sortStr = "";
                        //                for (int i = 0; i < parent_arr.Length - 1; i++)
                        //                {
                        //                   DataRow[] p_rows= parentTable.Select("nodeid=" + parent_arr[i]);
                        //                   sortStr += getSortString(p_rows[0]["indexid"].ToString());
                        //                }
                        //                row["sn_tmp_sortid"] = sortStr;
                        //            }
                        //        }
                        //        else  //0,表示站点栏目
                        //        {
                        //            row["sn_tmp_sortid"] = "0000";
                        //        }
                        //    }
                        //   dt.DefaultView.Sort = "sn_tmp_sortid " + (order.Equals("default") ? "asc" : "desc");
                        //   DataTable dt2= dt.DefaultView.ToTable();
                        //   dt = dt2;
                        //}
                        #endregion
                        SafeDictionary<int,Cchannel> _tmpNodeColls=ChannelConfigManager.createInstance().channelDataColls;
                        int j=0;

                        //Cchannel headNode = null;
                       // DataRow headRow = null;
                        //Cchannel footNode = null;
                       // DataRow footRow = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (j < startNum) { j++;continue; }
                            Cchannel tmpNode;
                            int _nodeid=Convert.ToInt32(row["nodeid"]);
                            if (!nodeColls.TryGetValue(_nodeid, out tmpNode))
                            {
                                tmpNode = _tmpNodeColls[_nodeid];
                                nodeColls.Add(_nodeid, tmpNode);
                            }
                           // if (j == startNum) { headNode = tmpNode; headRow = row; }
                           // if (j == (dt.Rows.Count - startNum - 1)) { footNode = tmpNode; footRow = row; }
                            if (j % 2 == 0)
                            {
                                if (itemMatch!=null)
                                {
                                    contentSb.Replace("◇singno_channels[item]◎", getItemContent(tmpNode, row, labelParams, (channelsColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(itemMatch.Groups[2].Value, channelsColls) : itemMatch.Groups[2].Value), itemVarColls, j + 1, XNLPage, itemCount.ToString()) + "◇singno_channels[item]◎");
                                }
                            }
                            else
                            {
                                if (alterMatch!=null)
                                {
                                    contentSb.Replace("◇singno_channels[item]◎", getItemContent(tmpNode, row, labelParams, (channelsColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(alterMatch.Groups[2].Value, channelsColls) : alterMatch.Groups[2].Value), alterItemVarColls, j + 1, XNLPage, itemCount.ToString()) + "◇singno_channels[item]◎");
                                }
                                else
                                {
                                    if (itemMatch!=null)
                                    {
                                        contentSb.Replace("◇singno_channels[item]◎", getItemContent(tmpNode, row, labelParams, (channelsColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(itemMatch.Groups[2].Value, channelsColls) : itemMatch.Groups[2].Value), itemVarColls, j + 1, XNLPage, itemCount.ToString()) + "◇singno_channels[item]◎");
                                    }
                                }
                            }

                            if (separMatch!=null)
                            {
                                if ((j + 1) % SpearStepNum == 0)
                                {
                                    contentSb.Replace("◇singno_channels[item]◎", getItemContent(tmpNode, row, labelParams, (channelsColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(separMatch.Groups[2].Value, channelsColls) : separMatch.Groups[2].Value), spearItemVarColls, j + 1, XNLPage, itemCount.ToString()) + "◇singno_channels[item]◎");
                                }
                            }
                            j ++;
                        }
                        //替换head,foot
                        if (headMatch != null)
                        {
                            contentSb.Replace("◇singno_channels[head]◎", (channelsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(headMatch.Groups[2].Value, channelsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, headMatch.Groups[2].Value)));
                        }
                        else { contentSb.Replace("◇singno_channels[head]◎", ""); }
                        if (footMatch != null)
                        {
                            contentSb.Replace("◇singno_channels[foot]◎", (channelsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(footMatch.Groups[2].Value, channelsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, footMatch.Groups[2].Value)));
                           // contentSb.Replace("◇singno_channels[item]◎", getItemContent(footNode, footRow, labelParams, (channelsColls.Count > 0 ? XNLCommon.enabledNestedXNLLabel(footMatch.Groups[2].Value, channelsColls) : footMatch.Groups[2].Value), footItemVarColls, j, XNLPage, itemCount.ToString()));
                        }
                        else { contentSb.Replace("◇singno_channels[foot]◎", ""); }
                        contentSb.Replace("◇singno_channels[item]◎", "");
                        return contentSb.ToString();
                }
            }
            else
            {
                string cStr = contentSb.ToString();
                contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));
                return setEmptyContent(contentSb, labelParams, channelsColls, itemMatch, alterMatch, headMatch, footMatch, separMatch, emptyMatch);
            }
           */
        }
        #endregion
       /*
        private void getAllChildList(Cchannel curChannel,Dictionary<int, Cchannel> nodeColls)
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
        private void getNodeByDepth(Cchannel channel, int depth, Dictionary<int, Cchannel> nodeColls)
        {
            if (channel.depth == depth)
            {
                if (!nodeColls.ContainsKey(channel.nodeID)) nodeColls.Add(channel.nodeID,channel);
            }
            else if (channel.depth<depth)
            {
                if (channel.subNodeColls != null)
                {
                    foreach (KeyValuePair<int, Cchannel> n in channel.subNodeColls)
                    {
                        getNodeByDepth(n.Value, depth, nodeColls);
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
                ns+=(i==0?n.Key.ToString():","+n.Key.ToString());
                i++;
            }
            return ns;
        }
        private string getSortString(string indexid)
       {
           string sortStr = "";
           for (int i = 0; i < 4 - indexid.Length; i++)
           {
               sortStr += "0";
           }
           sortStr += indexid;
           return sortStr;
       }
        private string setEmptyContent(StringBuilder contentSb,Dictionary<string, XNLParam> labelParams,MatchCollection channelsColls,Match itemMatch, Match alterMatch, Match headMatch,Match footMatch,Match spearMatch,Match emptyMatch)
        {
            //if (channelsColls.Count > 0)
            //{
            //    return XNLCommon.enabledNestedXNLLabel(contentSb.ToString(), channelsColls);
            //}
            if (headMatch != null)
            {
                contentSb.Replace("◇singno_channels[head]◎", (channelsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams,RegxpEngineCommon.enabledNestedXNLTag(headMatch.Groups[2].Value,channelsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, headMatch.Groups[2].Value)));
            }
            else
            {
                contentSb.Replace("◇singno_channels[head]◎", "");
            }
            if (footMatch != null)
            {
                //contentSb.Replace("◇singno_channels[foot]◎", XNLCommon.replaceAttribleVariable(labelParams, footMatch.Groups[2].Value));
                contentSb.Replace("◇singno_channels[head]◎", (channelsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(footMatch.Groups[2].Value, channelsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, footMatch.Groups[2].Value)));
            }
            else
            {
                contentSb.Replace("◇singno_channels[foot]◎", "");
            }
            if (emptyMatch != null)
            {
                contentSb.Replace("◇singno_channels[head]◎", (channelsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(emptyMatch.Groups[2].Value, channelsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, emptyMatch.Groups[2].Value)));
                //contentSb.Replace("◇singno_channels[item]◎", XNLCommon.replaceAttribleVariable(labelParams, emptyMatch.Groups[2].Value));
            }
            else
            {
                contentSb.Replace("◇singno_channels[item]◎", "");
            }
            
            return contentSb.ToString();
        }
        private string getItemContent(Cchannel channel, DataRow row, Dictionary<string, XNLParam> attParams, String contentStr, MatchCollection varColls, int itemid, WebContext XNLPage, string countStr)
       {
            if (varColls.Count > 0)
            {
                StringBuilder contentSb=new StringBuilder(contentStr);
                Dictionary<int, List<string>> match1Colls = new Dictionary<int, List<string>>();
                List<string> match2Colls = new List<string>(); //可以替换的
                foreach (Match match in varColls)
                {
                    string trueAttr = match.Groups[1].Value;
                    string attribStr = trueAttr.ToLower();
                     XNLParam param=null;
                    if(!(attribStr.TrimStart('@').StartsWith("channels.") || attParams.TryGetValue(attribStr, out param)))continue;
                    if (attribStr.Substring(0, 1) == "@")
                    {
                        string trueAttrStr = trueAttr.Replace("@", "");
                        string lowTrueAttr = trueAttrStr.ToLower();
                        int len = attribStr.Length - trueAttrStr.Length;
                        List<string> attrList;
                        string _attrStr = trueAttr.Substring(1);
                        if (match1Colls.TryGetValue(len, out attrList))
                        {
                            if ((lowTrueAttr.StartsWith("channels.")||attParams.ContainsKey(lowTrueAttr)) && !attrList.Contains(_attrStr)) attrList.Add(_attrStr);
                        }
                        else //添加
                        {
                            attrList = new List<string>();
                            attrList.Add(_attrStr);
                            match1Colls.Add(len, attrList);
                        }
                    }
                    else  //可以替换
                    {
                       
                        if (!match2Colls.Contains(trueAttr))
                        {
                            if(attribStr.StartsWith("channels."))
                            {
                                string varName=attribStr.Split('.')[1];
                                switch (varName)
                                {
                                    case "itemid":
                                        contentSb.Replace(match.Groups[0].Value,itemid.ToString());
                                        break;
                                    case "autourl":
                                        break;
                                    case "autopath":
                                        break;
                                    case "outlink": //外部链接
                                        break;
                                    case "count":
                                        contentSb.Replace(match.Groups[0].Value,countStr);
                                        break;
                                    default:
                                        contentSb.Replace(match.Groups[0].Value, XNLWebCommon.getChannelVariable(channel, XNLPage, varName));
                                        break;
                                }
                            }
                            else
                            {
                                contentSb.Replace(match.Groups[0].Value, Convert.ToString(param.value));
                            }
                            match2Colls.Add(trueAttr);
                        }
                    }
                }
                /////////////////
                //替换其它层变量
                for (int i = 1; i <= match1Colls.Count; i++)
                {
                    foreach (string str in match1Colls[i])
                    {
                        contentSb.Replace("{@" + str + "}", "{" + str + "}");
                    }
                }
                return contentSb.ToString();
            }
            return contentStr;
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
