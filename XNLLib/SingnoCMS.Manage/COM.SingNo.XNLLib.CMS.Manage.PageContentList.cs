using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
using COM.SingNo.Common;
using System.Text.RegularExpressions;
using System.Data;
using COM.SingNo.DAL;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class PageContentList:IXNLTag<WebContext>
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

       public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
           /*
            XNLParam P_nodeId; //nodeid参数
            int nodeId=0;
            if (!labelParams.TryGetValue("nodeid", out P_nodeId))
            {
                nodeId = ManageUtil.getCurSiteID(XNLPage);
            }
            else
            {
                if (!int.TryParse(P_nodeId.value.ToString(),out nodeId))
                {
                    nodeId = ManageUtil.getCurSiteID(XNLPage);
                }
            }
            labelParams["nodeid"].value = nodeId;
            string keyWord="";
            string dateFrom = "";
            string DateTo = "";
            string searchType = "";
            string State = "";
            int pageNum = 60;
            string pageName = "page";
            keyWord = labelParams["keyword"].value.ToString();
            dateFrom = labelParams["datefrom"].value.ToString();
            DateTo = labelParams["dateto"].value.ToString();
            searchType = labelParams["searchtype"].value.ToString();
            State = labelParams["state"].value.ToString();
            string pageNumStr = labelParams["pagenum"].value.ToString();
            pageName = labelParams["pagename"].value.ToString();
            labelParams.Add("primarykey", new XNLParam( XNLType.String, "id"));
            labelParams["datefrom"].type = XNLType.DateTime;
            labelParams["dateto"].type = XNLType.DateTime;
            labelParams["state"].type = XNLType.Int32;
            if (!int.TryParse(pageNumStr, out pageNum))
            {
                pageNum = 60;
            }
            labelParams.Add("perpagerecordsnum", new XNLParam(XNLType.Int32, pageNum));
            if (pageName.Trim().Equals(""))
            {
                pageName = "page";
            }
            if (!(XNLPage.Context.Request[pageName] == null || !UtilsCode.IsInt(XNLPage.Context.Request[pageName])))
            {
                if (!(XNLPage.Context.Request[pageName].Trim().Equals(string.Empty)) && Convert.ToInt32(XNLPage.Context.Request[pageName]) > 0)
                {
                    int tmpCurPageNum = Convert.ToInt32(XNLPage.Context.Request[pageName]);
                    labelParams.Add("curpagenum", new XNLParam(XNLType.Int32, tmpCurPageNum));//[].theValue = tmpCurPageNum;
                }
            }
            labelContentStr = RegxpEngineCommon.replaceAttribleVariableByName(labelContentStr, "item.nodeid", "{$nodeid}");
            labelContentStr = RegxpEngineCommon.replaceAttribleVariableByName(labelContentStr, "item.id", "{$id}");
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            //得到当前节点的模型
            Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
            int modelId = curChannel.model.ModelId;
            string modelName = curChannel.model.ModelName;
            labelParams.Add("modelname",new XNLParam(XNLType.String,modelName));
            //得到所有可以在列表中显示的字段
            //"select FieldName,DataType,DisplayName from SN_ModelDescript where ModelName=@modelname"
            DataTable modelDt = DataHelper.ExecuteDataTable("select FieldName,DataType,DisplayName,isShowOnList from SN_ModelDescript where ModelName=@modelname and isvisible=1 order by indexid", labelParams);
            Dictionary<string, DataRow> modelRowColls = new Dictionary<string, DataRow>(modelDt.Rows.Count);
            //select id,NodeID,State, from table where nodeid=@nodeid
            //得到sql语句字段列表
            string fieldListStr = "";
            foreach (DataRow row in modelDt.Rows)
            {
                if (Convert.ToInt32(row["isShowOnList"]) == 1)
                {
                    fieldListStr += ",[" + Convert.ToString(row["FieldName"])+"]";
                }
                modelRowColls.Add(Convert.ToString(row["FieldName"]), row);
            }
            fieldListStr = "[id],[nodeid],[state],[titleColor],[underLine],[Italic],[Bold],[IsRecommend],[IsHot],[IsColor],[IsTop],[isComment] " + fieldListStr;
            int isRecycle = 0;
            isRecycle = Convert.ToInt32(labelParams["isrecycle"].value);
            //查找数据
            Dictionary<string, string> sqls = setSearchSql(fieldListStr, curChannel.model, searchType, keyWord, dateFrom, DateTo, State, isRecycle,modelRowColls);
            labelParams.Add("countsql", new XNLParam( XNLType.String, sqls["count"]));
            labelParams["nodeid"].type = XNLType.Int32;
            DbCommand dbCommand = DataHelper.GetSqlStringCommand(sqls["select"]);
            DbConnection dbConn = DataHelper.CreateConnection();
            dbConn.Open();
            dbCommand.Connection = dbConn;
            DataTable ds = DataHelper.GetDataTableBySqlWithPage(dbCommand, labelParams);
            dbConn.Close();
            StringBuilder contentSb = new StringBuilder(labelContentStr);
            Match headItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "PageContentList.HeadTemplate")[0];
            Match dataItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "pageContentList.ItemTemplate")[0];
            Match noDateItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "pageContentList.NoRecordTemplate")[0];
            if (ds.Rows.Count == 0)
            {
                contentSb.Replace(headItemMatch.Value, "");
                contentSb.Replace(dataItemMatch.Value, "");
                contentSb.Replace(noDateItemMatch.Value, noDateItemMatch.Groups[3].Value);
            }
            else
            {
                contentSb.Replace(noDateItemMatch.Value, "");
                Match headColMatch = RegxpEngineCommon.matchsItemTagByName(headItemMatch.Value, "PageContentList.Head.Cols")[0];
                StringBuilder headColSb = new StringBuilder();
                int colNum = 1;
                foreach (DataRow row in modelDt.Rows)
                {
                    if (Convert.ToInt32(row["isShowOnList"]) == 1)
                    {
                        string headStr = headColMatch.Groups[3].Value;
                        headStr = RegxpEngineCommon.replaceAttribleVariableByName(headStr, "pageContentList.ColNum", colNum.ToString());
                        headStr = RegxpEngineCommon.replaceAttribleVariableByName(headStr, "pageContentList.ColDisplayName", Convert.ToString(row["DisplayName"]));
                        headColSb.Append(headStr);
                        colNum++;
                    }
                }
                contentSb.Replace(headColMatch.Value, headColSb.ToString());
                headItemMatch = RegxpEngineCommon.matchsItemTagByName(contentSb.ToString(), "PageContentList.HeadTemplate")[0];
                contentSb.Replace(headItemMatch.Value, headItemMatch.Groups[3].Value);
                //替换item template内容
                Match itemColMatch = RegxpEngineCommon.matchsItemTagByName(dataItemMatch.Value, "PageContentList.Item.Cols")[0];
                StringBuilder itemColSb = new StringBuilder();
                colNum = 1;
                foreach (DataRow row in modelDt.Rows)
                {
                    if (Convert.ToInt32(row["isShowOnList"]) == 1)
                    {
                        string itemStr = itemColMatch.Groups[3].Value;
                        itemStr = RegxpEngineCommon.replaceAttribleVariableByName(itemStr, "pageContentList.ColNum", colNum.ToString());
                        string fieldName = row["FieldName"].ToString();
                        string fieldStr = "{$" + fieldName + "}";
                        if (string.Compare("title", fieldName, true) == 0)
                        {
                            //fieldStr = "<font color=\"{$titlecolor}\" style=\"font-weight:{[iif('{$bold}'='1','bold','')]}; font-style:{[iif('{$italic}'='1','italic','')]};\">" + fieldStr + "</font>";
                            fieldStr = "{@titleleft}" + fieldStr + "{@titleright}";
                        }
                        itemStr = RegxpEngineCommon.replaceAttribleVariableByName(itemStr, "fieldName", fieldStr);
                        itemColSb.Append(itemStr);
                        colNum++;
                    }
                }
                contentSb.Replace(itemColMatch.Value, itemColSb.ToString());
                dataItemMatch = RegxpEngineCommon.matchsItemTagByName(contentSb.ToString(), "pageContentList.ItemTemplate")[0];
                StringBuilder dataItemSb = new StringBuilder();
                foreach (DataRow r in ds.Rows)
                {
                    string rowStr = RegxpEngineCommon.replaceDataBaseVariable(r, dataItemMatch.Groups[3].Value);
                    string leftStr = "<font color=\"" + r["titlecolor"].ToString() + "\" style=\"font-weight:" + (Convert.ToInt32(r["bold"]) == 1 ? "bold" : "") + "; font-style:" + (Convert.ToInt32(r["italic"]) == 1 ? "italic" : "") + ";\">" + (Convert.ToInt32(r["underline"]) == 1 ? "<u>" : "");
                    string rightStr = (Convert.ToInt32(r["underline"]) == 1 ? "</u>" : "") + "</font>  " + (Convert.ToInt32(r["istop"]) == 1 ? "[置顶]" : "") + (Convert.ToInt32(r["IsRecommend"]) == 1 ? "[推荐]" : "") + (Convert.ToInt32(r["ishot"]) == 1 ? "[热点]" : "") + (Convert.ToInt32(r["iscolor"]) == 1 ? "[醒目]" : "");
                    rowStr = XNLPage.xnlParser.replaceAttribleVariableByName(rowStr, "titleleft", leftStr, XNLPage);
                    rowStr = XNLPage.xnlParser.replaceAttribleVariableByName(rowStr, "titleright", rightStr, XNLPage);
                    dataItemSb.Append(rowStr);
                }
                contentSb.Replace(dataItemMatch.Value, dataItemSb.ToString());
            }
            return XNLPage.xnlParser.replaceAttribleVariable(contentSb.ToString(), labelParams, XNLPage);// RegxpEngineCommon.replaceAttribleVariable(labelParams, contentSb.ToString());
            */
            return "";
        }

        #endregion
        private Dictionary<string, string> setSearchSql(string fieldListStr, DataModel model, string searchType, string keyWord, string dateFrom, string dateTo, string state,int isRecycle,Dictionary<string, DataRow> modelRowColls)
       {
           string selectStr = "select " + fieldListStr + " from " + model.TableName + " where nodeid=@nodeid ";
           string countStr = "select count(id) from " + model.TableName + " where nodeid=@nodeid ";
           Dictionary<string, string> sqlcolls = new Dictionary<string, string>(2);
           state = state.Trim().ToLower();
           if (state.Equals("") || state.Equals("all"))
           {
               selectStr += "and isRecycle=" +isRecycle.ToString();
               countStr += "and isRecycle=" + isRecycle.ToString();
           }
           else
           {
               selectStr += "and state=@state and isRecycle=" + isRecycle.ToString();
               countStr += "and state=@state and isRecycle=" + isRecycle.ToString();
           }
           if (searchType.Trim().Equals(""))
           {
               selectStr += " order by istop desc,id desc";
               sqlcolls.Add("select", selectStr);
               sqlcolls.Add("count", countStr);
               return sqlcolls;
           }
           
            if (!dateFrom.Equals("") && !dateTo.Equals(""))
            {
                selectStr += " and (CreateTime>=@dateFrom and CreateTime<=@dateTo) ";
                countStr += " and (CreateTime>=@dateFrom and CreateTime<=@dateTo) ";
            }
            string dataType = "NVarChar";
            string l_SearchType=searchType.ToLower();
            if (l_SearchType.Equals("id") || l_SearchType.Equals("inputuser") || l_SearchType.Equals("lastedituser"))
            {
                switch (l_SearchType)
                {
                    case "id":
                        int k;
                        if (int.TryParse(keyWord, out k))
                        {
                            selectStr += " and ID=" + k.ToString();
                            countStr += " and ID=" + k.ToString();
                        }
                        break;
                    case "inputuser":
                    case "lastedituser":
                        selectStr += " and " + searchType + " like '%" + keyWord.Replace("'", "''") + "%'";
                        countStr += " and " + searchType + " like '%" + keyWord.Replace("'", "''") + "%'";
                        break;
                }
            }
            else
            {
                dataType = Convert.ToString(modelRowColls[searchType]["DataType"]);
                switch (dataType)
                {
                    case "NVarChar":
                    case "NText":
                        selectStr += " and " + searchType + " like '%" + keyWord.Replace("'", "''") + "%'";
                        countStr += " and " + searchType + " like '%" + keyWord.Replace("'", "''") + "%'";
                        break;
                    case "Boolean":
                        int i;
                        if (!int.TryParse(keyWord, out i))
                        {
                            i = 0;
                            keyWord = keyWord.Trim().ToLower();
                            if (keyWord.Equals("true") || keyWord.Equals("false"))
                            {
                                if (keyWord.Equals("true")) i = 1;
                            }
                        }
                        if (i == 0 || i == 1)
                        {
                            selectStr += " and " + searchType + "=" + i.ToString();
                            countStr += " and " + searchType + "=" + i.ToString();
                        }
                        break;
                    case "DateTime":
                        DateTime t;
                        if (DateTime.TryParse(keyWord, out t))
                        {
                            selectStr += " and " + searchType + "='" + keyWord + "'";
                            countStr += " and " + searchType + "='" + keyWord + "'";
                        }
                        break;
                    case "Integer":
                        int k;
                        if (int.TryParse(keyWord, out k))
                        {
                            selectStr += " and " + searchType + "=" + k.ToString();
                            countStr += " and " + searchType + "=" + k.ToString();
                        }
                        break;
                }
            }
            selectStr += " order by istop desc,id desc";
            sqlcolls.Add("select", selectStr);
            sqlcolls.Add("count", countStr);
            return sqlcolls;
       }

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
