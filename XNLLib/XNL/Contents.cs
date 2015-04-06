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
   public class Contents : IXNLTag<WebContext>
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
        /*
         * 1.先得到所有栏目列表
         * 2.得到所有栏目模型表
         * 3.统计栏目模型表字段
         * 4.建联合查询sql
        * <XNL:Contents channelIndex="" channelName="" upLevel="显示上几级栏目的内容列表，0为本级，1为父栏目，以此类推" topLevel="显示从首页向下某个级别栏目的内容列表，0代表网站首页，1代表一级栏目，以此类推" scope="Self(只显示本栏目下的所有内容)|Children(只显示下一极子栏目下的所有内容)|SelfAndChildren(显示本级栏目及下一极子栏目下的所有内容)|Descendant(显示所有级别的子栏目下的所有内容)|All(显示全部内容) " 
         siteid,sitename,channelid,channelindex,channelname
         * * isImage="是否只显示带有图片（不带图片）的内容"
         * isFile=" 是否只显示带有附件（不带附件）的内容"  isTop="是否只显示置顶（非置顶）内容" 
         * isRecommend="是否只显示推荐（非推荐）内容" isHot="是否只显示热点（非热点）内容" isColor="是否只显示醒目（非醒目）内容。" 
         * isRelated="是否显示相关内容列表" 
         * groupChannel="栏目组别，如设置此属性，则只显示指定栏目组别的内容列表"
         * groupChannelNot="非栏目组别，如设置此属性，则只显示不等于指定栏目组别的内容列表"
         * groupContent="内容组别，如设置此属性，则只显示指定内容组别的内容列表"
         * groupContentNot="非内容组别，如设置此属性，则只显示不等于内容组别的内容列表"
         * order="Default|Back|AddDate|AddDateBack|LastEdit|LastEditBack|Hits|DayHits|WeekHits|MonthHits|Stars|Digg|Comments|Random"
         * totalNum="设置列表一共显示多少条信息，0代表显示所有信息"
         * startNum="从第几条信息开始显示，默认从第一条信息开始显示"
         * titleWordNum="设置内容标题显示的字数，0代表不限制字数"
         * ellipsis="..."
         * tag=""
         * where
         * scope	  	  	 内容范围。此属性只可属于以下可能的取值的一种。
 	 Self		 只显示本栏目下的所有内容
 	 Children	  	 只显示下一极子栏目下的所有内容
 	 SelfAndChildren	  	 显示本级栏目及下一极子栏目下的所有内容
 	 Descendant	  	 显示所有级别的子栏目下的所有内容
 	 All	  	 显示全部内容
         * >
         * isRelated	  	  	 是否显示相关内容列表
 	 true	  	 显示相关内容列表
 	 false		 不显示相关内容列表
         * totalNum	  	  	 设置列表一共显示多少条信息，0代表显示所有信息。
startNum	  	 1	 从第几条信息开始显示，默认从第一条信息开始显示。
titleWordNum	  	  	设置内容标题显示的字数，0代表不限制字数。
         * tags	  	  	 仅显示指定标签的内容列表，多个标签用“,”分隔。
where	  	  	获取内容列表的条件判断
        * <contents.header >
         * <contents.footer>
         * <contents.item>
         * <channels.Alternating>
         * <contents.empty>
         * <contents.Separator>
        * </XNL:Contents>
        */
       public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
           /*
            if (string.IsNullOrEmpty(labelContentStr.Trim()))
            {
                return labelContentStr;
            }
            //得到所有内容参数列表
            Dictionary<string, List<string>> allVarList = XNLUtils.getContentsVarList(labelContentStr); //得到所有字段列表
            MatchCollection labelColls = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "xnl", "contents");
            if (labelColls.Count>0)labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, labelColls);
            XNLParam _tmpParam;
            Cchannel curSite=XNLPage.curChannel.siteNode;
            if (labelParams.TryGetValue("siteid", out _tmpParam))
            {
                curSite = SiteConfigManager.createInstance().siteDataColls[Convert.ToInt32(_tmpParam.value)];
            }
            else if (labelParams.TryGetValue("sitename", out _tmpParam))
            {
                curSite = XNLUtils.getSiteByName(_tmpParam.value.ToString());
            }
            if (curSite == null) throw (new Exception("此站点不存在!"));

            Cchannel curChannel=XNLPage.curChannel;
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
            channelid = curChannel.nodeID;
            curSite = curChannel.siteNode;
            if (labelParams.TryGetValue("isrelated", out _tmpParam)) //是否显示相关内容
            {
                if (XNLPage.contentId > 0)
                {
                    //得到相关内容
                    DataTable tag_dt = DataHelper.ExecuteDataTable("select distinct ct_name from sn_contenttags where ct_contentid=" + XNLPage.contentId.ToString() + " and ct_nodeid=" + channelid.ToString());
                    string tags = "";
                    for (int i = 0; i < tag_dt.Rows.Count; i++)
                    {
                        tags += (i > 0 ? "," : "") +Convert.ToString(tag_dt.Rows[i][0]);
                    }
                    if (labelParams.ContainsKey("tags"))
                    {
                        labelParams["tags"].value = tags;
                    }
                    else
                    {
                        labelParams.Add("tags", new XNLParam(tags));
                    }
                }
                else
                {
                    throw (new Exception("isRelated属性只能在内容页使用"));
                }
            }
            
            //得到所有模型表
            int itemCount = 0;
            labelParams.Add("contents.count", new XNLParam(itemCount));
            string nodeSql = "";
            int isGroup = 0;
           // Cchannel curChannel = XNLPage.curChannel;
            if (labelParams.TryGetValue("groupchannel", out _tmpParam))
            {
                //得到所有属于些栏目组的栏目
                isGroup = 1;
                string groupsName = _tmpParam.value.ToString();
                if (groupsName.IndexOf(',')>= 0)
                {
                    string groups = "";
                    string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < group_arr.Length; i++)
                    {
                        groups +=(i>0?",'":"'")+ group_arr[i].Replace("'","''")+"'";
                    }
                    nodeSql = "select distinct NGS_id from SN_NodeGroup,SN_NodeGroups where NG_id=NGS_id and NG_siteid="+curSite.siteID.ToString()+" and NG_name in ("+groups+")";
                }
                else
                {
                    nodeSql = "select distinct NGS_id from SN_NodeGroup,SN_NodeGroups where NG_id=NGS_id and NG_siteid=" + curSite.siteID.ToString() + " and NG_name='" + groupsName.Replace("'", "''") + "'";
                }
                itemCount = 1;
            }
            else if (labelParams.TryGetValue("groupchannelnot", out _tmpParam))
            {
                isGroup = 1;
                string groupsName = _tmpParam.value.ToString();
                if (groupsName.IndexOf(',') >= 0)
                {
                    string groups = "";
                    string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < group_arr.Length; i++)
                    {
                        groups += (i > 0 ? ",'" : "'") + group_arr[i].Replace("'", "''") + "'";
                    }
                    nodeSql = "select distinct NGS_nodeid from SN_NodeGroup,SN_NodeGroups where NG_id=NGS_id and NG_siteid=" + curSite.siteID.ToString() + " and NG_name not in (" + groups + ")";
                }
                else
                {
                    nodeSql = "select distinct NGS_nodeid from SN_NodeGroup,SN_NodeGroups where NG_id=NGS_id and NG_siteid=" + curSite.siteID.ToString() + " and NG_name<>'" + groupsName.Replace("'", "''") + "'";
                }
                itemCount = 1;
            }
            else if (labelParams.TryGetValue("groupcontent", out _tmpParam))
            {
                isGroup = 2;
                string groupsName = _tmpParam.value.ToString();
                if (groupsName.IndexOf(',') >= 0)
                {
                    string groups = "";
                    string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < group_arr.Length; i++)
                    {
                        groups += (i > 0 ? ",'" : "'") + group_arr[i].Replace("'", "''") + "'";
                    }
                    nodeSql = "select distinct CGS_NodeId,CG_ID from SN_ContentGroup,SN_ContentGroups where CG_id=CGS_id and CG_siteid=" + curSite.siteID.ToString() + " and CG_name in (" + groups + ")";
                }
                else
                {
                    nodeSql = "select distinct CGS_NodeId,CG_ID from SN_ContentGroup,SN_ContentGroups where CG_id=CGS_id and CG_siteid=" + curSite.siteID.ToString() + " and CG_name='" + groupsName.Replace("'", "''") + "'";
                }
                itemCount = 1;
            }
            else if (labelParams.TryGetValue("groupcontentnot", out _tmpParam))
            {
                isGroup = 2;
                string groupsName = _tmpParam.value.ToString();
                if (groupsName.Length > 0)
                {
                    if (groupsName.IndexOf(',') >= 0)
                    {
                        string groups = "";
                        string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < group_arr.Length; i++)
                        {
                            groups += (i > 0 ? ",'" : "'") + group_arr[i].Replace("'", "''") + "'";
                        }
                        nodeSql = "select distinct CGS_nodeid,CG_id from SN_ContentGroup,SN_ContentGroups where CG_id=CGS_id and CG_siteid=" + curSite.siteID.ToString() + " and CG_name not in (" + groups + ")";
                    }
                    else
                    {
                        nodeSql = "select distinct CGS_nodeid,CG_id from SN_ContentGroup,SN_ContentGroups where CG_id=CGS_id and CG_siteid=" + curSite.siteID.ToString() + " and CG_name<>'" + groupsName.Replace("'", "''") + "'";
                    }
                    itemCount = 1;
                }
            }
            else if (labelParams.TryGetValue("tags", out _tmpParam))
            {
                isGroup = 3; //设置tags内容组
                string groupsName = _tmpParam.value.ToString();
                if (groupsName.IndexOf(',') >= 0)
                {
                    string tags = "";
                    string[] group_arr = groupsName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < group_arr.Length; i++)
                    {
                        tags += (i > 0 ? ",'" : "'") + group_arr[i].Replace("'", "''") + "'";
                    }
                    nodeSql = "select distinct CtS_NodeId,Ct_ID from SN_ContentTag,SN_ContentTags where Ct_id=CtS_id and Ct_siteid=" + curSite.siteID.ToString() + " and Ct_name in (" + tags + ")";
                }
                else
                {
                    nodeSql = "select distinct CtS_NodeId,Ct_ID from SN_ContentTag,SN_ContentTags where Ct_id=CtS_id and Ct_siteid=" + curSite.siteID.ToString() + " and Ct_name='" + groupsName.Replace("'", "''") + "'";
                }
                itemCount = 1;
            }
            
            Dictionary<DataModel, List<Cchannel>> modelColls = new Dictionary<DataModel, List<Cchannel>>();
            SafeDictionary<int,Cchannel> globalNodeColls=ChannelConfigManager.createInstance().channelDataColls;
            Dictionary<int, Cchannel> nodeColls = new Dictionary<int, Cchannel>();
            string group_ids = "";
            if (isGroup>0) //得到设置组别属性的内容列表
            {
                if (nodeSql.Length > 0)
                {
                    DataTable dt = DataHelper.ExecuteDataTable(nodeSql);
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        int rownodeid = Convert.ToInt32(row[0]);
                        Cchannel node = globalNodeColls[rownodeid];
                        nodeColls.Add(rownodeid, node);
                        DataModel model = node.model;
                        List<Cchannel> nodeList;
                        if (modelColls.TryGetValue(model, out nodeList))
                        {
                            if (!nodeList.Contains(node)) nodeList.Add(node);
                        }
                        else
                        {
                            nodeList = new List<Cchannel>();
                            nodeList.Add(node);
                            modelColls.Add(model, nodeList);
                        }
                        if (isGroup >= 2)
                        {
                            group_ids += (i == 0 ? Convert.ToString(row[1]) : "," + Convert.ToString(row[1])); ;
                        }
                        i++;
                    }
                }
            }
            else
            {
                //设置以指定栏目为参数的内容列表 
                bool isUporTop = false;
                if (labelParams.TryGetValue("uplevel", out _tmpParam))
                {
                    isUporTop = true;
                    int upLevel = Convert.ToInt32(_tmpParam.value);
                    if (upLevel > 0)
                    {
                        if (curChannel.depth - upLevel >= curChannel.siteNode.depth) //在有效范围内
                        {
                            Cchannel _n = curChannel;
                            for (int i = 1; i >= upLevel; i++)
                            {
                                _n = _n.parentNode;
                            }
                            addChannelToList(_n, nodeColls);
                            //添加此栏目
                        }
                        else
                        {
                            addChannelToList(curSite, nodeColls);
                        }
                    }
                }
                if (labelParams.TryGetValue("toplevel", out _tmpParam))
                {
                    isUporTop = true;
                    int topLevel = Convert.ToInt32(_tmpParam.value) + 1;
                    //得到相应层级的下级子栏目
                    if (topLevel >= 1) getNodeByDepth(curChannel.siteNode, topLevel, nodeColls);
                }
                if (!isUporTop)
                {
                    string scopeStr = "self";
                    if (labelParams.TryGetValue("scope", out _tmpParam))
                    {
                        //   Self		 只显示本栏目下的所有内容
                        // Children	  	 只显示下一极子栏目下的所有内容
                        // SelfAndChildren	  	 显示本级栏目及下一极子栏目下的所有内容
                        // Descendant	  	 显示所有级别的子栏目下的所有内容
                        // All	
                        scopeStr = _tmpParam.value.ToString().ToLower();
                    }
                    switch (scopeStr)
                    {
                        case "self":
                            addChannelToList(curChannel, nodeColls);
                            break;
                        case "children":
                            if (curChannel.subNodeColls != null)
                            {
                                foreach (KeyValuePair<int, Cchannel> n in curChannel.subNodeColls)
                                {
                                    addChannelToList(n.Value, nodeColls);
                                }
                            }
                            break;
                        case "selfchildren":
                            addChannelToList(curChannel, nodeColls);
                            if (curChannel.subNodeColls != null)
                            {
                                foreach (KeyValuePair<int, Cchannel> n in curChannel.subNodeColls)
                                {
                                    addChannelToList(n.Value, nodeColls);
                                }
                            }
                            break;
                        case "descendant":
                            break;
                        case "all":
                            getAllChildList(curChannel, nodeColls);
                            addChannelToList(curChannel, nodeColls);
                            break;

                    }
                }  
                //由栏目得到模型信息
                foreach (KeyValuePair<int, Cchannel> n in nodeColls)
                {
                    DataModel model =n.Value.model;
                    List<Cchannel> nodeList;
                    if (modelColls.TryGetValue(model, out nodeList))
                    {
                        if (!nodeList.Contains(n.Value)) nodeList.Add(n.Value);
                    }
                    else
                    {
                        nodeList = new List<Cchannel>();
                        nodeList.Add(n.Value);
                        modelColls.Add(model, nodeList);
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
            int SpearStepNum = 1;
            MatchCollection itemsColls = Regex.Matches(labelContentStr, @"<contents\.([a-z]+)[\s]*[^<>]*>(.*)</contents\.\1>", XNLBaseCommon.XNL_RegexOptions);
            //替换数据行
            StringBuilder contentSb = new StringBuilder(labelContentStr);
            foreach (Match match in itemsColls)
            {
                switch (match.Groups[1].Value.ToLower())
                {
                    case "item":
                        itemMatch = match;
                        contentSb.Replace(match.Value, "◇singno_contents[head]◎◇singno_contents[item]◎◇singno_contents[foot]◎");
                        itemVarColls = Regex.Matches(match.Groups[2].Value, RegxpEngineCommon.RegexStr_XNLAttriableVariable, XNLBaseCommon.XNL_RegexOptions);
                        break;
                    case "empty":
                        emptyMatch = match;
                        contentSb.Replace(match.Value, "");
                        break;
                    case "header":
                        headMatch = match;
                        contentSb.Replace(match.Value, "");
                        break;
                    case "footer":
                        footMatch = match;
                        contentSb.Replace(match.Value, "");
                        break;
                    case "alternating":
                        alterMatch = match;
                        if (itemMatch == null)
                        {
                            contentSb.Replace(match.Value, "◇singno_contents[head]◎◇singno_contents[item]◎◇singno_contents[foot]◎");
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
                            contentSb.Replace(match.Value, "◇singno_contents[head]◎◇singno_contents[item]◎◇singno_contents[foot]◎");
                        }
                        else
                        {
                            contentSb.Replace(match.Value, "");
                        }
                        int startin = match.Groups[1].Index;
                        int len = match.Groups[2].Index - startin;
                        string paramStr = match.Value.Substring(startin, len);
                        Match tmpMatch = Regex.Match(match.Value, "step=\"(\\d+)\"", XNLBaseCommon.XNL_RegexOptions);
                        if (tmpMatch.Success)
                        {
                            SpearStepNum = Convert.ToInt32(tmpMatch.Groups[1].Value);
                        }
                        spearItemVarColls = Regex.Matches(match.Groups[2].Value, RegxpEngineCommon.RegexStr_XNLAttriableVariable, XNLBaseCommon.XNL_RegexOptions);
                        break;
                }
            }
            if (modelColls.Count > 0)
            {
                int totalnum = -1;
                if (labelParams.TryGetValue("totalnum", out _tmpParam))
                {
                    totalnum=Convert.ToInt32(_tmpParam.value);
                }
                
                string order = "default";
                if (labelParams.TryGetValue("order", out _tmpParam))
                {
                    order = _tmpParam.value.ToString();
                }
                string isImage = ""; string isFile = ""; string isTop = ""; string isRecommend = ""; string isHot = ""; string isColor = ""; string tag = ""; string where = "";
                if (labelParams.TryGetValue("isimage", out _tmpParam)) isImage = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("isfile", out _tmpParam)) isFile = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("istop", out _tmpParam)) isTop = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("isrecommend", out _tmpParam)) isRecommend = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("ishot", out _tmpParam)) isHot = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("iscolor", out _tmpParam)) isColor = _tmpParam.value.ToString();
                if (labelParams.TryGetValue("where", out _tmpParam)) where = _tmpParam.value.ToString();
                string sql = getSql(totalnum,order.ToLower(), modelColls, allVarList,isGroup,group_ids,isImage,isFile,isTop,isRecommend,isHot,isColor,tag,where);
                DataTable dt=null;
                if (sql.Length > 0)
                {
                    dt = DataHelper.ExecuteDataTable(sql);
                }
                if (dt!=null)
                {
                    int titleWord = 0;
                    if (labelParams.TryGetValue("titleword", out _tmpParam))
                    {
                        if (!int.TryParse(_tmpParam.value.ToString(), out titleWord))
                        {
                            titleWord = 0;
                        }
                    }
                    string ellipsis = "...";
                    if (labelParams.TryGetValue("ellipsis", out _tmpParam))
                    {
                        ellipsis = _tmpParam.value.ToString();
                    }
                    int startNum = 0;
                    if (labelParams.TryGetValue("startnum", out _tmpParam))
                    {
                        startNum = Convert.ToInt32(_tmpParam.value);
                    }
                    
                    string cStr = contentSb.ToString();
                    contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));

                    int j = 0;
                    int _nodeid = 0;
                    Cchannel tmpNode = null;
                    TemplateProject project=null;
                    try
                    {
                        project =curSite.templateProjectColls[XNLPage.templateProjectId];
                    }
                    catch
                    {
                        project = curSite.templateProjectColls[curSite.defaultProjectId];
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        if (j < startNum) { j++; continue; }
                        _nodeid = Convert.ToInt32(row["nodeid"]);
                        if (tmpNode == null || tmpNode.nodeID != _nodeid)
                        {
                            if (!nodeColls.TryGetValue(_nodeid, out tmpNode))
                            {
                                tmpNode = globalNodeColls[_nodeid];
                                nodeColls.Add(_nodeid, tmpNode);
                            }
                        }

                        if (j % 2 == 0)
                        {
                            if (itemMatch != null)
                            {
                                contentSb.Replace("◇singno_contents[item]◎", getItemContent(tmpNode, row, labelParams, (labelColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(itemMatch.Groups[2].Value, labelColls) : itemMatch.Groups[2].Value), itemVarColls, j + 1, XNLPage, itemCount.ToString(), project, titleWord, ellipsis) + "◇singno_contents[item]◎");
                            }
                        }
                        else
                        {
                            if (alterMatch != null)
                            {
                                contentSb.Replace("◇singno_contents[item]◎", getItemContent(tmpNode, row, labelParams, (labelColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(alterMatch.Groups[2].Value, labelColls) : alterMatch.Groups[2].Value), alterItemVarColls, j + 1, XNLPage, itemCount.ToString(), project, titleWord, ellipsis) + "◇singno_contents[item]◎");
                            }
                            else
                            {
                                if (itemMatch != null)
                                {
                                    contentSb.Replace("◇singno_contents[item]◎", getItemContent(tmpNode, row, labelParams, (labelColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(itemMatch.Groups[2].Value, labelColls) : itemMatch.Groups[2].Value), itemVarColls, j + 1, XNLPage, itemCount.ToString(), project, titleWord, ellipsis) + "◇singno_contents[item]◎");
                                }
                            }
                        }

                        if (separMatch != null)
                        {
                            if ((j + 1) % SpearStepNum == 0)
                            {
                                contentSb.Replace("◇singno_contents[item]◎", getItemContent(tmpNode, row, labelParams, (labelColls.Count > 0 ? RegxpEngineCommon.enabledNestedXNLTag(separMatch.Groups[2].Value, labelColls) : separMatch.Groups[2].Value), spearItemVarColls, j + 1, XNLPage, itemCount.ToString(), project, titleWord, ellipsis) + "◇singno_contents[item]◎");
                            }
                        }
                        j++;
                    }
                    return contentSb.ToString();
                }
                else
                {
                    string cStr = contentSb.ToString();
                    contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));
                    return setEmptyContent(contentSb, labelParams, labelColls, itemMatch, alterMatch, headMatch, footMatch, separMatch, emptyMatch);
                }
            }
            else
            {
                //设置空内容
                string cStr = contentSb.ToString();
                contentSb.Replace(cStr, RegxpEngineCommon.replaceAttribleVariable(labelParams, cStr));
                return setEmptyContent(contentSb, labelParams, labelColls, itemMatch, alterMatch, headMatch, footMatch, separMatch, emptyMatch);
            }
            */
            return "";
        }
        #endregion
       /*
        private void getNodeByDepth(Cchannel channel, int depth, Dictionary<int, Cchannel> nodeColls)
        {
            if (channel.depth == depth)
            {
                if (!nodeColls.ContainsKey(channel.nodeID)) nodeColls.Add(channel.nodeID, channel);
            }
            else if (channel.depth < depth)
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
        private void addChannelToList(Cchannel channel, Dictionary<int, Cchannel> nodeColls)
        {
            if (!nodeColls.ContainsKey(channel.nodeID)) nodeColls.Add(channel.nodeID, channel);
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
        private string getSql(int totalnum, string order, Dictionary<DataModel, List<Cchannel>> modelColls, Dictionary<string, List<string>> allVarList, int grouptype, string groups, string isImage, string isFile, string isTop, string isRecommend, string isHot, string isColor, string tag, string where)
        {
            if (grouptype > 0 && groups.Length == 0) return "";
            int i = 0;
            string tmpSql = "";
            List<string> fieldList = allVarList["f"];
            List<string> extFieldList = allVarList["e"];
           // string orderStr = "order by istop desc,isRecommend desc, indexid desc,id desc";
            string orderStr = "order by istop desc,id desc";
            switch (order)
            {
                case "random":
                    Random r = new Random();
                    int index = r.Next(0, 12);
                    string[] order_arr = new string[] { "default", "back", "adddate", "adddateback", "lastedit", "lasteditback", "hits", "dayhits", "weekhits", "monthhits", "stars", "digg", "comments" };
                    order = order_arr[index];
                    orderStr = getOrderStr(order, fieldList);
                    break;
                default:
                    orderStr = getOrderStr(order, modelColls.Count>1?fieldList:null);
                    break;

            }
            foreach (KeyValuePair<DataModel, List<Cchannel>> mn in modelColls)
            {
                if (i > 0) tmpSql += " union ";
                if (grouptype == 2) //内容组
                {
                    if (mn.Value.Count > 1)
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + ",SN_ContentGroups where CGS_id in(" + groups + ") and NodeID=CGS_NodeID and ID=CGS_ContentId @where";
                    }
                    else
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + ",SN_ContentGroups where CGS_id=" + groups + " and NodeID=CGS_NodeID and ID=CGS_ContentId @where";
                    }
                }
                else if (grouptype == 3) //tag组
                {
                    if (mn.Value.Count > 1)
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + ",SN_ContentTags where CTS_id in(" + groups + ") and NodeID=CTS_NodeID and ID=CTS_ContentId @where";
                    }
                    else
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + ",SN_ContentTags where CTS_id=" + groups + " and NodeID=CTS_NodeID and ID=CTS_ContentId @where";
                    }
                }
                else
                {
                    if (mn.Value.Count > 1)
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + " where nodeid in (" + listToString(mn.Value) + ") @where";
                    }
                    else
                    {
                        tmpSql += "select @top " + fieldListToString(fieldList) + extFieldListToString(extFieldList, mn.Key) + " from " + mn.Key.TableName + " where nodeid=" + listToString(mn.Value) + " @where";
                    }
                }
                i++;
            }
            //设置where
            string whereStr = "";
            if (!string.IsNullOrEmpty(isImage))
            {
                if (string.Compare(isImage, "true", true) == 0)
                {
                    whereStr += " and isimage=1 ";
                }
                else
                {
                    whereStr += " and isimage=0 ";
                }
            }
            if (!string.IsNullOrEmpty(isColor))
            {
                if (string.Compare(isColor, "true", true) == 0)
                {
                    whereStr += " and isColor=1 ";
                }
                else
                {
                    whereStr += " and isColor=0 ";
                }
            }
            if (!string.IsNullOrEmpty(isFile))
            {
                if (string.Compare(isFile, "true", true) == 0)
                {
                    whereStr += " and isFile=1 ";
                }
                else
                {
                    whereStr += " and isFile=0 ";
                }
            }
            if (!string.IsNullOrEmpty(isHot))
            {
                if (string.Compare(isHot, "true", true) == 0)
                {
                    whereStr += " and isHot=1 ";
                }
                else
                {
                    whereStr += " and isHot=0 ";
                }
            }
            if (!string.IsNullOrEmpty(isRecommend))
            {
                if (string.Compare(isRecommend, "true", true) == 0)
                {
                    whereStr += " and isRecommend=1 ";
                }
                else
                {
                    whereStr += " and isRecommend=0 ";
                }
            }
            if (!string.IsNullOrEmpty(isTop))
            {
                if (string.Compare(isTop, "true", true) == 0)
                {
                    whereStr += " and isTop=1 ";
                }
                else
                {
                    whereStr += " and isTop=0 ";
                }
            }
            if (!string.IsNullOrEmpty(tag))
            {

            }
            if (!string.IsNullOrEmpty(where))
            {
                whereStr += " and "+where+" ";
            }
            tmpSql = tmpSql.Replace("@where", whereStr);
            if (i > 1)
            {
                tmpSql = tmpSql.Replace("@top", "");
                if (totalnum > 0)
                {
                    tmpSql = "select top " + totalnum.ToString() + " * from (" + tmpSql + ")";
                }
                else
                {
                    tmpSql = "select * from (" + tmpSql + ")";
                }
            }
            else
            {
                if (totalnum >0)
                {
                    tmpSql = tmpSql.Replace("@top",  "top " + totalnum.ToString());
                }
                else
                {
                    tmpSql = tmpSql.Replace("@top", "");
                }
            }
            tmpSql += " "+orderStr+" ";
            return tmpSql;
        }
        private string listToString(List<Cchannel> list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += (i == 0 ? list[i].nodeID.ToString() : "," + list[i].nodeID.ToString());
            }
            return str;
        }
        private string fieldListToString(List<string> list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += (i == 0 ? list[i] : "," + list[i]);
            }
            return str;
        }
        private string extFieldListToString(List<string> list,DataModel model)
        {
            string str = "";
            List<string> extList = new List<string>();
            foreach (ModelField f in model.fieldList)
            {
                extList.Add(f.FieldName.ToLower());
            }
            for (int i = 0; i < list.Count; i++)
            {
                string _str = extList.Contains(list[i]) ? list[i] : "''";
                str += "," + _str;
            }
            return str;
        }
        private string setEmptyContent(StringBuilder contentSb, Dictionary<string, XNLParam> labelParams, MatchCollection contentsColls, Match itemMatch, Match alterMatch, Match headMatch, Match footMatch, Match spearMatch, Match emptyMatch)
        {
            if (headMatch != null)
            {
                contentSb.Replace("◇singno_contents[head]◎", (contentsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(headMatch.Groups[2].Value, contentsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, headMatch.Groups[2].Value)));
            }
            else
            {
                contentSb.Replace("◇singno_contents[head]◎", "");
            }
            if (footMatch != null)
            {
                contentSb.Replace("◇singno_contents[head]◎", (contentsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(footMatch.Groups[2].Value, contentsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, footMatch.Groups[2].Value)));
            }
            else
            {
                contentSb.Replace("◇singno_contents[foot]◎", "");
            }
            if (emptyMatch != null)
            {
                contentSb.Replace("◇singno_contents[head]◎", (contentsColls.Count > 0 ? RegxpEngineCommon.replaceAttribleVariable(labelParams, RegxpEngineCommon.enabledNestedXNLTag(emptyMatch.Groups[2].Value, contentsColls)) : RegxpEngineCommon.replaceAttribleVariable(labelParams, emptyMatch.Groups[2].Value)));
            }
            else
            {
                contentSb.Replace("◇singno_contents[item]◎", "");
            }

            return contentSb.ToString();
        }
        private string getItemContent(Cchannel channel, DataRow row, Dictionary<string, XNLParam> attParams, String contentStr, MatchCollection varColls, int itemid, WebContext XNLPage, string countStr, TemplateProject project, int titleWord, string ellipsis)
        {
            if (varColls.Count > 0)
            {
                string contentPath =contentPath=(project.getContentUrl(channel.nodeID, row["id"].ToString()).Replace("@", channel.siteWebPath)).Replace("//", "/");                
                StringBuilder contentSb = new StringBuilder(contentStr);
                Dictionary<int, List<string>> match1Colls = new Dictionary<int, List<string>>();
                List<string> match2Colls = new List<string>(); //可以替换的
                foreach (Match match in varColls)
                {
                    string trueAttr = match.Groups[1].Value;
                    string attribStr = trueAttr.ToLower();
                    XNLParam param = null;
                    if (!(attribStr.TrimStart('@').StartsWith("contents.") || attParams.TryGetValue(attribStr, out param))) continue;
                    if (attribStr.Substring(0, 1) == "@")
                    {
                        string trueAttrStr = trueAttr.Replace("@", "");
                        string lowTrueAttr = trueAttrStr.ToLower();
                        int len = attribStr.Length - trueAttrStr.Length;
                        List<string> attrList;
                        string _attrStr = trueAttr.Substring(1);
                        if (match1Colls.TryGetValue(len, out attrList))
                        {
                            if ((lowTrueAttr.StartsWith("contents.") || attParams.ContainsKey(lowTrueAttr)) && !attrList.Contains(_attrStr)) attrList.Add(_attrStr);
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
                            if (attribStr.StartsWith("contents."))
                            {
                                string varName = attribStr.Split('.')[1];
                                switch (varName)
                                {
                                    case "itemid":
                                        contentSb.Replace(match.Groups[0].Value, itemid.ToString());
                                        break;
                                    case "urllinktag":
                                        break;
                                    case "pathlinktag":
                                        break;
                                    case "outlink": //外部链接
                                        break;
                                    case "count":
                                        contentSb.Replace(match.Groups[0].Value, countStr);
                                        break;
                                    case "title":
                                        string title = XNLWebCommon.getContentVariable(XNLPage, varName, row, channel, contentPath);
                                        if (titleWord > 0 && title.Length > titleWord)
                                        {
                                            title = title.Substring(0, titleWord)+ellipsis;
                                        }
                                        contentSb.Replace(match.Groups[0].Value, title);
                                        break;
                                    case "styletitle":
                                        string titleColor = Convert.ToString(row["titlecolor"]);
                                        int Bold = Convert.ToInt32(row["bold"]);
                                        int italic = Convert.ToInt32(row["italic"]);
                                        int underline = Convert.ToInt32(row["underline"]);
                                        string styleTitle = Convert.ToString(row["title"]);
                                        if (titleWord > 0 && styleTitle.Length > titleWord)
                                        {
                                            styleTitle = styleTitle.Substring(0, titleWord) + ellipsis;
                                        }
                                        styleTitle = "<font color=\"" + titleColor + "\"" + (Bold == 1 ? " style=\"font-weight:bold;" : "") + (italic == 1 ? " font-style:italic;" : "") + ">" + (underline == 1 ? "<u>" : "") + styleTitle + (underline == 1 ? "</u>" : "") + "</font>";
                                        contentSb.Replace(match.Groups[0].Value, styleTitle);
                                        break;
                                    default:
                                        contentSb.Replace(match.Groups[0].Value, XNLWebCommon.getContentVariable(XNLPage,varName,row, channel, contentPath));
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
        private string getOrderStr(string order, List<string> fieldList)
        {
            //string orderStr = "order by istop desc,isRecommend desc, indexid desc,id desc";
            string orderStr = "order by istop desc,id desc";
            switch (order)
            {
                case "default":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("istop")) fieldList.Add("istop");
                        if (!fieldList.Contains("isrecommend")) fieldList.Add("isrecommend");
                        if (!fieldList.Contains("indexid")) fieldList.Add("indexid");
                    }
                    break;
                case "back":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("istop")) fieldList.Add("istop");
                        if (!fieldList.Contains("isrecommend")) fieldList.Add("isrecommend");
                        if (!fieldList.Contains("indexid")) fieldList.Add("indexid");
                    }
                   // orderStr = "order by istop,isRecommend, indexid,id";
                    orderStr = "order by istop,id";
                    break;
                case "adddate":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("adddate")) fieldList.Add("adddate");
                    }
                    orderStr = "order by adddate desc";
                    break;
                case "adddateback":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("adddate")) fieldList.Add("adddate");
                    }
                    orderStr = "order by adddate";
                    break;
                case "lastedit":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("laseeditdate")) fieldList.Add("laseeditdate");
                    }
                    orderStr = "order by laseeditdate desc";
                    break;
                case "lasteditback":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("laseeditdate")) fieldList.Add("laseeditdate");
                    }
                    orderStr = "order by laseeditdate";
                    break;
                case "hits":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("hits")) fieldList.Add("hits");
                    }
                    orderStr = "order by hits desc";
                    break;
                case "dayhits":
                    if (!fieldList.Contains("dayhits")) fieldList.Add("dayhits");
                    orderStr = "order by dayhits desc";
                    break;
                case "weekhits":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("weekhits")) fieldList.Add("weekhits");
                    }
                    orderStr = "order by weekhits desc";
                    break;
                case "monthhits":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("monthhits")) fieldList.Add("monthhits");
                    }
                    orderStr = "order by monthhits desc";
                    break;
                case "stars":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("stars")) fieldList.Add("stars");
                    }
                    orderStr = "order by stars desc";
                    break;
                case "digg":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("diggs")) fieldList.Add("diggs");
                    }
                    orderStr = "order by diggs desc";
                    break;
                case "comments":
                    if (fieldList != null)
                    {
                        if (!fieldList.Contains("comments")) fieldList.Add("comments");
                    }
                    orderStr = "order by comments desc";
                    break;
            }
            return orderStr;
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
