using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using COM.SingNo.DAL;
using System.Data;
using System.Text.RegularExpressions;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using System.Xml;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
    /// <summary>
    /// 设置内容页的表单
    /// </summary>
  public  class ContentForm:IXNLTag<WebContext>
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
          return "";
          /*
          string actionStr = labelParams["action"].value.ToString().ToLower();
          if (!(actionStr.Equals("add") || actionStr.Equals("modify"))) return "执行命令错误。";
          int nodeId = 0;
          if (Convert.ToString(labelParams["nodeid"].value).Equals(""))
          {
              nodeId = Convert.ToInt32(labelParams["sitenodeid"].value);
          }
          else
          {
              nodeId = Convert.ToInt32(labelParams["nodeid"].value);
          }
          DataModel curModel = ChannelConfigManager.createInstance().channelDataColls[nodeId].model;
          DataRow dataRow = null;
          StringBuilder labelContentSb = new StringBuilder(labelContentStr);
          MatchCollection channelListAreaMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.ChannelList");
          string contentIdStr = "";
          if (actionStr.Equals("modify"))
          {
              //得到当前内容的数据
              contentIdStr = labelParams["contentid"].value.ToString();
              int contentId;
              if (int.TryParse(contentIdStr, out contentId))
              {
                  string getDataSql = "select [-@fieldList-] from " + curModel.TableName + " where ID=@contentId";
                  string fieldListStr = "";
                  foreach (ModelField f in curModel.fieldList)
                  {
                      fieldListStr += ","+f.FieldName;
                  }
                  fieldListStr = "[State],[isComment],[Hits],[titleColor],[underLine],[Italic],[Bold],[IsRecommend],[IsHot],[IsColor],[IsTop],[pageType],[pageWords] " + fieldListStr;
                  getDataSql = getDataSql.Replace("[-@fieldList-]", fieldListStr);
                  DataTable dt = DataHelper.ExecuteDataTable(getDataSql, labelParams);
                  if (dt.Rows.Count == 0)
                  {
                      return "没有找到记录";
                  }
                  dataRow = dt.Rows[0];
                  labelContentSb.Replace(channelListAreaMatch[0].Groups[0].Value, "<input type=\"hidden\"  name=\"nodeid\" id=\"nodeid\" value=\""+nodeId+"\" />");
              }
              else
              {
                  return "没有指定内容ID";
              } 
          }else
          {
              labelContentSb.Replace(channelListAreaMatch[0].Groups[0].Value, channelListAreaMatch[0].Groups[3].Value);
          }
          int ModelId = curModel.ModelId;
          labelParams.Add("modelid", new XNLParam(ModelId));
          string tableName = curModel.TableName;
          labelParams.Add("modelname", new XNLParam(curModel.ModelName)); //添加模型名参数
          MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ContentForm.Items");
          MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "ContentForm.Error");
          MatchCollection titleItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Title");
          MatchCollection subTitleItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.SubTitle");
          MatchCollection imageUrlItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.ImageUrl");
          MatchCollection summaryItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Summary");
          MatchCollection linkUrlItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.LinkUrl");
          MatchCollection authorItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Author");
          MatchCollection sourceItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Source");
          MatchCollection fileUrlItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.FileUrl");
          //MatchCollection isRecommendItemMatch = XNLCommon.matchsItemLabelByName(labelContentStr, "Area.IsRecommend");
          //MatchCollection isHotItemMatch = XNLCommon.matchsItemLabelByName(labelContentStr, "Area.IsHot");
          //MatchCollection isColorItemMatch = XNLCommon.matchsItemLabelByName(labelContentStr, "Area.IsColor");
          //MatchCollection isTopItemMatch = XNLCommon.matchsItemLabelByName(labelContentStr, "Area.IsTop");
          MatchCollection contentItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Content");
          MatchCollection keyWordItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.KeyWord");
          MatchCollection addDateItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.AddDate");
          MatchCollection UserDimsItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.UserDims");
          MatchCollection GroupItemMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.ContentGroup");
          MatchCollection GroupListItemMatch = RegxpEngineCommon.matchsItemTagByName(GroupItemMatch[0].Groups[0].Value, "Area.ContentGroup.item");
          //MatchCollection stateItemMatch = XNLCommon.matchsItemLabelByName(labelContentStr, "Area.State");
          MatchCollection contentTagMatch = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "Area.Tag");
          StringBuilder userFormItemsListSb =new StringBuilder("");
            //得到模型描述列表
          string modelMateSql = "select [FieldName],[DataType],[DataLength],[IsValidator],[DefaultValue],[DisplayName],[IndexId],[HelpText],[IsSystem],[IsVisible],[InputType],[InputTypeSet],[ValidatorSet] from [SN_ModelDescript] where [modelName]=@modelname order by [indexid]";
            DataTable modelDt = DataHelper.ExecuteDataTable(modelMateSql, labelParams);
            int userRowId = 0;
            //int viewAttrNum = 0;
            List<string> sysFieldList = new List<string>();
            foreach (DataRow row in modelDt.Rows)
            {
                string fieldName = Convert.ToString(row["FieldName"]).Trim();
                string lowFieldName = fieldName.ToLower();
                if (Convert.ToInt32(row["IsVisible"]) == 0)
                {
                        switch (lowFieldName)
                        {
                            case "title":
                                labelContentSb.Replace(titleItemMatch[0].Groups[0].Value, "");
                                break;
                            case "subtitle":
                                labelContentSb.Replace(subTitleItemMatch[0].Groups[0].Value, "");
                                break;
                            case "imageurl":
                                labelContentSb.Replace(imageUrlItemMatch[0].Groups[0].Value, "");
                                break;
                            case "summary":
                                labelContentSb.Replace(summaryItemMatch[0].Groups[0].Value, "");
                                break;
                            case "linkurl":
                                labelContentSb.Replace(linkUrlItemMatch[0].Groups[0].Value, "");
                                break;
                            case "author":
                                labelContentSb.Replace(authorItemMatch[0].Groups[0].Value, "");
                                break;
                            case "source":
                                labelContentSb.Replace(sourceItemMatch[0].Groups[0].Value, "");
                                break;
                            case "fileurl":
                                labelContentSb.Replace(fileUrlItemMatch[0].Groups[0].Value, "");
                                break;
                            case "content":
                                labelContentSb.Replace(contentItemMatch[0].Groups[0].Value, "");
                                break;
                            case "keyword":
                                labelContentSb.Replace(keyWordItemMatch[0].Groups[0].Value, "");
                                break;
                            case "adddate":
                                labelContentSb.Replace(addDateItemMatch[0].Groups[0].Value, "");
                                break;
                            //case "isrecommend":
                            //    labelContentSb.Replace(isRecommendItemMatch[0].Groups[0].Value, "");
                            //    break;
                            //case "ishot":
                            //    labelContentSb.Replace(isHotItemMatch[0].Groups[0].Value, "");
                            //    break;
                            //case "iscolor":
                            //    labelContentSb.Replace(isColorItemMatch[0].Groups[0].Value, "");
                            //    break;
                            //case "istop":
                            //    labelContentSb.Replace(isTopItemMatch[0].Groups[0].Value, "");
                            //    break;
                        }
                }
                else
                {
                    #region
                    string inputStr = SetFields(row, actionStr, dataRow);
                    StringBuilder UserDimsItemSb = new StringBuilder(UserDimsItemMatch[0].Groups[3].Value);
                    string DisplayName=Convert.ToString(row["DisplayName"]);
                    string HelpText = Convert.ToString(row["HelpText"]);
                    if (Convert.ToInt32(row["isSystem"]) == 1)
                    {
                        StringBuilder areaSb = new StringBuilder();
                        labelParams.Add("displayname." + lowFieldName, new XNLParam( XNLType.String, DisplayName));
                        labelParams.Add("helptext." + lowFieldName, new XNLParam( XNLType.String, HelpText));
                        labelParams.Add("input." + lowFieldName, new XNLParam( XNLType.String, inputStr));
                        sysFieldList.Add(lowFieldName);
                        switch (lowFieldName)
                        {
                            case "title":
                                string titleAreaStr = titleItemMatch[0].Groups[3].Value;
                                if (actionStr.ToLower().Equals("add"))
                                {
                                    titleAreaStr=titleAreaStr.Replace("{title.isBold}","");
                                    titleAreaStr = titleAreaStr.Replace("{title.isItalic}", "");
                                    titleAreaStr = titleAreaStr.Replace("{title.isU}", "");
                                    titleAreaStr = titleAreaStr.Replace("{title.color}", "#000000");
                                }
                                else if (actionStr.ToLower().Equals("modify"))
                                {
                                    //string settingStr = Convert.ToString(dataRow["Settings"]);
                                    //得到标题属性
                                    //string[] attrs = settingStr.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    //string[] v = new string[] {"f","f","f","#ff0000" }; //初始值，0，粗体 ，1斜体,2下划线3,颜色
                                    //Dictionary<string,int> kvindex=new Dictionary<string,int>(4);
                                    //kvindex.Add("b",0);kvindex.Add("i",1);kvindex.Add("u",2);kvindex.Add("c",3);
                                    //foreach (string s in attrs)
                                    //{
                                    //    string[] kandv = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                    //    v[kvindex[kandv[0]]] = kandv[1];
                                    //}
                                    titleAreaStr = titleAreaStr.Replace("{title.isBold}", (Convert.ToInt32(dataRow["bold"])==1 ? "checked=\"checked\"" : ""));
                                    titleAreaStr = titleAreaStr.Replace("{title.isItalic}", (Convert.ToInt32(dataRow["Italic"]) == 1 ? "checked=\"checked\"" : ""));
                                    titleAreaStr = titleAreaStr.Replace("{title.isU}", (Convert.ToInt32(dataRow["underLine"]) == 1 ? "checked=\"checked\"" : ""));
                                    titleAreaStr = titleAreaStr.Replace("{title.color}", Convert.ToString(dataRow["titleColor"]));
                                }
                                areaSb.Append(titleAreaStr);
                                labelContentSb.Replace(titleItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "subtitle":
                                areaSb.Append(subTitleItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(subTitleItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "imageurl":
                                areaSb.Append(imageUrlItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(imageUrlItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "summary":
                                areaSb.Append(summaryItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(summaryItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "linkurl":
                                areaSb.Append(linkUrlItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(linkUrlItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "author":
                                areaSb.Append(authorItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(authorItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "source":
                                areaSb.Append(sourceItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(sourceItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "fileurl":
                                areaSb.Append(fileUrlItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(fileUrlItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "content":
                                areaSb.Append(contentItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(contentItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "keyword":
                                areaSb.Append(keyWordItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(keyWordItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            case "adddate":
                                areaSb.Append(addDateItemMatch[0].Groups[3].Value);
                                labelContentSb.Replace(addDateItemMatch[0].Groups[0].Value, areaSb.ToString());
                                break;
                            //case "isrecommend":
                                //viewAttrNum++;
                            //    areaSb.Append(isRecommendItemMatch[0].Groups[3].Value);
                            //    labelContentSb.Replace(isRecommendItemMatch[0].Groups[0].Value, areaSb.ToString());
                            //    break;
                            //case "ishot":
                            //    viewAttrNum++;
                            //    areaSb.Append(isHotItemMatch[0].Groups[3].Value);
                            //    labelContentSb.Replace(isHotItemMatch[0].Groups[0].Value, areaSb.ToString());
                            //    break;
                            //case "iscolor":
                            //    viewAttrNum++;
                            //    areaSb.Append(isColorItemMatch[0].Groups[3].Value);
                            //    labelContentSb.Replace(isColorItemMatch[0].Groups[0].Value, areaSb.ToString());
                            //    break;
                            //case "istop":
                            //    viewAttrNum++;
                            //    areaSb.Append(isTopItemMatch[0].Groups[3].Value);
                            //    labelContentSb.Replace(isTopItemMatch[0].Groups[0].Value, areaSb.ToString());
                            //    break;
                        }
                    }
                    else
                    {
                        userRowId += 1;
                        UserDimsItemSb.Replace("{@DisplayName.UserDims}", DisplayName);
                        UserDimsItemSb.Replace("{@HelpText.UserDims}", HelpText);
                        UserDimsItemSb.Replace("{@Input.UserDims}", inputStr);
                        userFormItemsListSb.Append(UserDimsItemSb.ToString());
                    }
                    #endregion
                }
            }
            if (!sysFieldList.Contains("title")) labelContentSb.Replace(titleItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("subtitle")) labelContentSb.Replace(subTitleItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("imageurl")) labelContentSb.Replace(imageUrlItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("summary")) labelContentSb.Replace(summaryItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("linkurl")) labelContentSb.Replace(linkUrlItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("author")) labelContentSb.Replace(authorItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("source")) labelContentSb.Replace(sourceItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("fileurl")) labelContentSb.Replace(fileUrlItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("content")) labelContentSb.Replace(contentItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("keyword")) labelContentSb.Replace(keyWordItemMatch[0].Groups[0].Value, "");
            if (!sysFieldList.Contains("adddate")) labelContentSb.Replace(addDateItemMatch[0].Groups[0].Value, "");
            //if (!sysFieldList.Contains("isrecommend")) labelContentSb.Replace(isRecommendItemMatch[0].Groups[0].Value, "");
            //if (!sysFieldList.Contains("ishot")) labelContentSb.Replace(isHotItemMatch[0].Groups[0].Value, "");
            //if (!sysFieldList.Contains("iscolor")) labelContentSb.Replace(isColorItemMatch[0].Groups[0].Value, "");
            //if (!sysFieldList.Contains("istop")) labelContentSb.Replace(isTopItemMatch[0].Groups[0].Value, "");
            if (userRowId > 0)
            {
                labelContentSb.Replace(UserDimsItemMatch[0].Groups[0].Value, userFormItemsListSb.ToString());
            }
            else
            {
                labelContentSb.Replace(UserDimsItemMatch[0].Groups[0].Value, "");
            }
            //MatchCollection viewAttributeAreaMatch = XNLCommon.matchsItemLabelByName(labelContentSb.ToString(), "Area.ViewAttribute");
            //if (viewAttrNum == 0)
            //{
            //    labelContentSb.Replace(viewAttributeAreaMatch[0].Groups[0].Value, "");
            //}
            //else
            //{
            //    labelContentSb.Replace(viewAttributeAreaMatch[0].Groups[0].Value, viewAttributeAreaMatch[0].Groups[3].Value);
            //}
            labelParams.Add("state.caogao",new XNLParam(XNLType.String));
            labelParams.Add("state.shenhe", new XNLParam( XNLType.String));
            labelParams.Add("state.shenheok", new XNLParam( XNLType.String));
            Cchannel curChannel = ChannelConfigManager.createInstance().channelDataColls[nodeId];
            COM.SingNo.CMS.Core.ChannelConfig channelConfig = curChannel.theChannelConfig;
            ChannelBaseConfig baseConfig = channelConfig.baseConfig;
            if (actionStr.Equals("add"))
            {
                if (curChannel.theChannelConfig.baseConfig.useContentGroup)
                {
                    //设置内容组列表
                    DataTable g_dt = DataHelper.ExecuteDataTable("select CG_Id,CG_Name from SN_ContentGroup where CG_SiteID="+curChannel.siteID);
                    if (g_dt.Rows.Count == 0)
                    {
                        labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, "");
                    }
                    else
                    {
                        StringBuilder g_sb = new StringBuilder();
                        foreach (DataRow _gr in g_dt.Rows)
                        {
                            g_sb.Append(GroupItemMatch[0].Groups[3].Value.Replace("{@contentgroup.id}", _gr[0].ToString()).Replace("{@contentgroup.name}", _gr[1].ToString()).Replace("{@contentgroup.checked}", ""));
                        }
                        labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, g_sb.ToString());
                    }
                }
                else
                {
                    labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, "");
                }
                if (curChannel.theChannelConfig.baseConfig.useContentTag)
                {
                    //labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, contentTagMatch[0].Groups[3].Value.Replace("{@content.tags}", "").Replace("{@content.tagsid}", ""));
                    labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, contentTagMatch[0].Groups[3].Value.Replace("{@content.tags}", ""));
                }
                else
                {
                    labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, "");
                }
                labelParams["state.shenheok"].value = "checked=\"checked\"";
                labelParams.Add("isrecommend", new XNLParam(XNLType.String, "0"));
                labelParams.Add("ishot", new XNLParam(XNLType.String, "0"));
                labelParams.Add("iscolor", new XNLParam(XNLType.String, "0"));
                labelParams.Add("istop", new XNLParam(XNLType.String, "0"));
                int pageType = 0;
                int pageWords = 1500;
                switch (baseConfig.partPage)
                {
                    case ChannelBaseConfig.PartPage.No:
                        pageType = 0;
                        break;
                    case ChannelBaseConfig.PartPage.Auto:
                        pageType = 1;
                        break;
                    case ChannelBaseConfig.PartPage.Manual:
                        pageType = 2;
                        pageWords = baseConfig.autoWordNums;
                        break;
                }
                labelParams.Add("pagetype", new XNLParam(XNLType.String, pageType.ToString()));
                labelParams.Add("pagewords", new XNLParam(XNLType.String, pageWords.ToString()));
                labelParams.Add("iscomment", new XNLParam(XNLType.String,channelConfig.commentsConfig.isContentComments?1:0));
            }
            else
            {
                if (curChannel.theChannelConfig.baseConfig.useContentGroup)
                {
                    //设置内容组列表
                    DataTable g_dt = DataHelper.ExecuteDataTable("select a.CG_Id,a.CG_Name from SN_ContentGroup a,SN_ContentGroups b where b.CGS_ContentId=" + contentIdStr + " and a.cg_id=b.cgs_id and a.cg_siteid=" + curChannel.siteID.ToString());
                    if (g_dt.Rows.Count == 0)
                    {
                        g_dt = DataHelper.ExecuteDataTable("select CG_Id,CG_Name from SN_ContentGroup where CG_SiteID=" + curChannel.siteID.ToString());
                        if (g_dt.Rows.Count == 0)
                        {
                            labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, "");
                        }
                        else
                        {
                            StringBuilder g_sb = new StringBuilder();
                            foreach (DataRow _gr in g_dt.Rows)
                            {
                                g_sb.Append(GroupListItemMatch[0].Groups[3].Value.Replace("{@contentgroup.id}", _gr[0].ToString()).Replace("{@contentgroup.name}", _gr[1].ToString()).Replace("{@contentgroup.checked}", ""));
                            }
                            labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, GroupItemMatch[0].Groups[3].Value.Replace(GroupListItemMatch[0].Groups[0].Value, g_sb.ToString()));
                        }
                    }
                    else
                    {
                        StringBuilder g_sb = new StringBuilder();
                        int i = 0;
                        string _gid = "";
                        foreach (DataRow _gr in g_dt.Rows)
                        {
                            _gid += (i > 0 ? "," : "") + _gr[0].ToString();
                            g_sb.Append(GroupListItemMatch[0].Groups[3].Value.Replace("{@contentgroup.id}", _gr[0].ToString()).Replace("{@contentgroup.name}", _gr[1].ToString()).Replace("{@contentgroup.checked}", "checked=\"checked\""));
                            i++;
                        }
                        g_dt = DataHelper.ExecuteDataTable("select CG_Id,CG_Name from SN_ContentGroup  where cg_siteid=" + curChannel.siteID+" and cg_id not in("+_gid+")" );
                        foreach (DataRow _gr in g_dt.Rows)
                        {
                            g_sb.Append(GroupListItemMatch[0].Groups[3].Value.Replace("{@contentgroup.id}", _gr[0].ToString()).Replace("{@contentgroup.name}", _gr[1].ToString()).Replace("{@contentgroup.checked}", ""));
                        }
                        labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, GroupItemMatch[0].Groups[3].Value.Replace(GroupListItemMatch[0].Groups[0].Value, g_sb.ToString()));
                    }
                    g_dt = null;
                }
                else
                {
                    labelContentSb.Replace(GroupItemMatch[0].Groups[0].Value, "");
                }
                if (curChannel.theChannelConfig.baseConfig.useContentTag)
                {
                    //查找此内容所使用的tag
                    //DataTable t_dt = DataHelper.ExecuteDataTable("select ct_id,ct_name from sn_contenttag,sn_contenttags where cts_siteid=" + curChannel.siteID.ToString() + " and cts_nodeid=" + nodeId.ToString() + " and cts_contentid="+contentIdStr+" and ct_id=cts_id");
                    DataTable t_dt = DataHelper.ExecuteDataTable("select ct_name from sn_contenttag,sn_contenttags where cts_siteid=" + curChannel.siteID.ToString() + " and cts_nodeid=" + nodeId.ToString() + " and cts_contentid=" + contentIdStr + " and ct_id=cts_id");
                    if (t_dt.Rows.Count == 0)
                    {
                        labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, contentTagMatch[0].Groups[3].Value.Replace("{@content.tags}", "").Replace("{@content.tagsid}", ""));
                    }
                    else
                    {
                        //替换为相应值
                        string tags = "";
                        //string tagids = "";
                        for (int i = 0; i < t_dt.Rows.Count; i++)
                        {
                            //tagids += (i > 0 ? "," : "") + t_dt.Rows[i][0].ToString();
                            //tags += (i > 0 ? "," : "") + t_dt.Rows[i][1].ToString();
                            tags += (i > 0 ? "," : "") + t_dt.Rows[i][0].ToString();
                        }
                        //labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, contentTagMatch[0].Groups[3].Value.Replace("{@content.tags}", tags).Replace("{@content.tagsid}", tagids));
                        labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, contentTagMatch[0].Groups[3].Value.Replace("{@content.tags}", tags));
                    } 
                }
                else
                {
                    labelContentSb.Replace(contentTagMatch[0].Groups[0].Value, "");
                }
                labelParams.Add("isrecommend", new XNLParam(XNLType.String, dataRow["isrecommend"].ToString()));
                labelParams.Add("ishot", new XNLParam(XNLType.String, dataRow["ishot"].ToString()));
                labelParams.Add("iscolor", new XNLParam(XNLType.String, dataRow["iscolor"].ToString()));
                labelParams.Add("istop", new XNLParam(XNLType.String, dataRow["istop"].ToString()));
                labelParams.Add("pagetype", new XNLParam(XNLType.String, dataRow["pagetype"].ToString()));
                labelParams.Add("pagewords", new XNLParam(XNLType.String, dataRow["pagewords"].ToString()));
                labelParams.Add("iscomment", new XNLParam(XNLType.String, dataRow["iscomment"].ToString()));
                switch(Convert.ToInt32(dataRow["State"]))
                {
                    case 0:
                        labelParams["state.shenhe"].value = "checked=\"checked\"";
                        break;
                    case 99:
                        labelParams["state.shenheok"].value = "checked=\"checked\"";
                        break;
                    case -1:
                        labelParams["state.caogao"].value = "checked=\"checked\"";
                        break;
                }
            }
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentSb.ToString());
          
            return labelContentStr;*/
        }
        #endregion
      /*
        private string SetFields(DataRow row, string actionStr, DataRow dataRow)
        {
            string action = actionStr.ToLower();
            if (Convert.ToInt32(row["IsVisible"]) == 0) //不显示
            {
                return "";
            }
            StringBuilder sb = new StringBuilder("<formItem name=\"|-{inputname}-|\">");
            sb.Append("<validatorItem>");
            sb.Append("<formValidator  validatorgroup=\"1\" defaultvalue=\"{defaultvalue}\"></formValidator>");
            sb.Append("{inputvalidatorstr}");
            sb.Append("</validatorItem>");
            sb.Append("{input.temp_formitemtext}");
            sb.Append("</formItem>");
            string fieldName=row["FieldName"].ToString();
            sb.Replace("|-{inputname}-|", fieldName);
            StringBuilder validatorSb =new StringBuilder("");
            string displayName=row["DisplayName"].ToString();
            string inputTypeStr = row["InputType"].ToString();
            if (Convert.ToInt32(row["IsValidator"]) == 1)
            {
                //判断验证对象
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Convert.ToString(row["ValidatorSet"]));
                string requireStr=xmlDoc.SelectSingleNode("/ValidatorSet/@required").InnerText;
                if (requireStr.ToLower().Equals("true"))
                {
                    validatorSb.Append("<inputValidator type=\"|---type---|\" min=\"1\" onerror=\""+displayName+"不能为空\" ></inputValidator>");
                    if (inputTypeStr.Equals("Date") || inputTypeStr.Equals("DateTime"))
                    {
                        validatorSb.Replace("|---type---|", inputTypeStr.ToLower());
                    }
                    else 
                    {
                        validatorSb.Replace("|---type---|", "size"); 
                    }
                }
                //检查正则验证
                string regexStr = xmlDoc.SelectSingleNode("/ValidatorSet/regexValidator/regexp").InnerText;
                if (!regexStr.Trim().Equals(""))
                {
                    regexStr = Regex.Escape(regexStr);
                    string errorStr = xmlDoc.SelectSingleNode("/ValidatorSet/regexValidator/error").InnerText;
                    validatorSb.Append("<regexValidator regexp=\""+regexStr+"\" onerror=\""+errorStr+"\"></regexValidator>");
                }
                sb.Replace("{inputvalidatorstr}", validatorSb.ToString());
            }
            else
            {
                sb.Replace("{inputvalidatorstr}", "");
            }
            //设置显示
            string InputTypeSet = row["InputTypeSet"].ToString();
            XmlDocument xmlInputDoc = new XmlDocument();
            xmlInputDoc.LoadXml(InputTypeSet);
            string style = "";
            if (xmlInputDoc.SelectSingleNode("/InputTypeSet/@style")!=null)style= xmlInputDoc.SelectSingleNode("/InputTypeSet/@style").InnerText;
            StringBuilder inputSb = new StringBuilder();
            switch (inputTypeStr)
            {
                case "Text":  //文本框
                case "TextArea":  //文本域
                case "Image": //图片
                case "File": //文件
                case "TextEditor": //编辑框
                case "Date":
                case "DateTime":
                    if (inputTypeStr == "Text")
                    {
                        inputSb.Append("<input type=\"text\" name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\"  |-@width-| |-@height-| value=\"|-@value-|\"  maxlength=\"|-@maxlength-|\"  |-@readonly-|/>");
                    }
                    else if (inputTypeStr == "TextArea")
                    {
                        inputSb.Append("<textarea name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\"  |-@width-| |-@height-| |-@readonly-|>|-@value-|</textarea>");
                    }
                    else if (inputTypeStr=="Image")
                    {
                        inputSb.Append("<input type=\"text\" name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\"  |-@width-| |-@height-| value=\"|-@value-|\"  maxlength=\"|-@maxlength-|\"  |-@readonly-|/> <input name=\"|-@inputName-|_selectBtn\" type=\"button\"  id=\"|-@inputName-|_selectBtn\" value=\"选择\" onclick=\"\"/><input name=\"|-@inputName-|_uploadBtn\" type=\"button\"  id=\"|-@inputName-|_uploadBtn\" value=\"上传\" onclick=\"\"/>");
                    }
                    else if (inputTypeStr == "File")
                    {
                        inputSb.Append("<input type=\"text\" name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\"  |-@width-| |-@height-| value=\"|-@value-|\"  maxlength=\"|-@maxlength-|\"  |-@readonly-|/> <input name=\"|-@inputName-|_selectBtn\" type=\"button\"  id=\"|-@inputName-|_selectBtn\" value=\"选择\" onclick=\"\"/><input name=\"|-@inputName-|_uploadBtn\" type=\"button\"  id=\"|-@inputName-|_uploadBtn\" value=\"上传\" onclick=\"\"/>");
                    }
                    else if (inputTypeStr == "TextEditor")
                    {
                        inputSb.Append("<input name=\"|-@inputName-|\" type=\"hidden\" id=\"|-@inputName-|\" value=\"|-@value-|\" /><iframe id=\"|-@inputName-|_fr\" src=\"{[site.Path]}/Common/editor/xHtmlEditor/editor.htm?id=|-@inputName-|&ReadCookie=0\" frameborder=\"0\" scrolling=\"no\" |-@width-| |-@height-| ></iframe>");
                    }
                    else if (inputTypeStr == "Date" || inputTypeStr == "DateTime")
                    {
                        sb.Replace("></inputValidator>", " readOnly=\"true\" ></inputValidator>");
                        sb.Replace("></formValidator>", " defaultpassed=\"true\" ");
                        inputSb.Append("<input type=\"text\" name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\"  |-@width-| |-@height-| value=\"|-@value-|\" />");
                    }
                    inputSb.Replace("|-@maxlength-|", row["DataLength"].ToString());
                    string valueStr = "";
                    if(xmlInputDoc.SelectSingleNode("/InputTypeSet/value")!=null)valueStr = xmlInputDoc.SelectSingleNode("/InputTypeSet/value").InnerText;
                    if (action.Equals("add"))
                    {
                        if (valueStr.Trim().Equals(""))  //设置为数据默认值
                        {
                            var _tmpType = row["DataType"].ToString();
                            if (inputTypeStr.Equals("Date")) _tmpType = inputTypeStr;
                            inputSb.Replace("|-@value-|", ManageUtil.setItemDefault(_tmpType, row["DefaultValue"].ToString()));
                            sb.Replace("{defaultvalue}", ManageUtil.setItemDefault(_tmpType, row["DefaultValue"].ToString()));
                        }
                        else
                        {
                            if (inputTypeStr == "TextEditor")
                            {
                                valueStr = UtilsCode.encodeHtmlAndXnl(valueStr);
                            }
                            else if(inputTypeStr == "Date")
                            {
                                DateTime _datetime;
                                if(!DateTime.TryParse(valueStr,out _datetime))
                                {
                                    valueStr = DateTime.Now.ToShortDateString();
                                }
                            }
                            else if(inputTypeStr == "DateTime")
                            {
                                DateTime _datetime;
                                if (!DateTime.TryParse(valueStr, out _datetime))
                                {
                                    valueStr = DateTime.Now.ToString();
                                }
                            }
                            inputSb.Replace("|-@value-|", valueStr);
                            sb.Replace("{defaultvalue}", valueStr);
                        }
                    }
                    else
                    {
                        valueStr =Convert.ToString(dataRow[fieldName]); 
                        if (inputTypeStr == "TextEditor") valueStr = UtilsCode.encodeHtmlAndXnl(valueStr);
                        // sb.Replace("{defaultvalue}", valueStr);////修改修改界面，去掉验证里的defaultvalue属性,2011,2,21
                        sb.Replace("defaultvalue=\"{defaultvalue}\"", "");
                        inputSb.Replace("|-@value-|", valueStr);
                    }
                    break;
                case "CheckBox": //多选框
                case "Radio":
                    sb.Replace("defaultvalue=\"{defaultvalue}\"","");
                    inputSb.Append(setCheckBoxType(xmlInputDoc, action, dataRow, fieldName, inputTypeStr));
                    break;
                case "SelectOne":
                case "SelectMultiple":
                    sb.Replace("defaultvalue=\"{defaultvalue}\"", "");
                    inputSb.Append(setSelectType(xmlInputDoc, action, dataRow, fieldName, inputTypeStr));
                    break;
            }
            inputSb.Replace("|-@style-|", style);
            inputSb.Replace("|-@inputName-|", fieldName);
            if (xmlInputDoc.SelectSingleNode("/InputTypeSet").Attributes["width"] != null)
            {
                string widthStr = xmlInputDoc.SelectSingleNode("/InputTypeSet/@width").InnerText;
                if (widthStr.Equals("0"))
                {
                    string tmpInputType=row["InputType"].ToString();
                    switch (tmpInputType)
                     {
                         case "Text":  //文本框
                         case "Image":  //图片框
                         case "File":  //文件框
                         case "Date":
                         case "DateTime":
                             if (tmpInputType == "DateTime" || tmpInputType=="Date")
                             {
                                 inputSb.Replace("|-@width-|", "size=\"20\"");
                             }
                             else { inputSb.Replace("|-@width-|", "size=\"50\""); }
                           break;
                         case "TextEditor":
                           inputSb.Replace("|-@width-|", "width=\"" + 700 + "\"");
                           break;
                         case "TextArea":
                           inputSb.Replace("|-@width-|", "cols=\"50\"");
                           break;
                    }
                }
                else
                {
                    switch (row["InputType"].ToString())
                    {
                        case "Text":  //文本框
                        case "Image":  //图片框
                        case "File":  //文件框
                        case "Date":
                        case "DateTime":
                            inputSb.Replace("|-@width-|", "size=\"" + widthStr + "\"");
                            break;
                        case "TextEditor":
                            inputSb.Replace("|-@width-|", "width=\"" + widthStr + "\"");
                            break;
                        case "TextArea":
                            inputSb.Replace("|-@width-|", "cols=\"" + widthStr + "\"");
                            break;
                    }
                }
            }

            if (xmlInputDoc.SelectSingleNode("/InputTypeSet").Attributes["height"] != null)
            {
                string hightStr = xmlInputDoc.SelectSingleNode("/InputTypeSet/@height").InnerText;
                if (hightStr.Equals("0"))
                {
                    switch (row["InputType"].ToString())
                    {
                        case "TextArea":
                            inputSb.Replace("|-@height-|", "rows=\"" + 10+ "\"");
                            break;
                        case "TextEditor":
                            inputSb.Replace("|-@height-|", "height=\"" + 460 + "\"");
                            break;
                        default:
                            inputSb.Replace("|-@height-|", "");
                            break;
                    }
                }
                else
                {
                    switch (row["InputType"].ToString())
                    {
                        case "TextArea":
                            inputSb.Replace("|-@height-|", "rows=\"" + hightStr + "\"");
                            break;
                        case "TextEditor":
                            inputSb.Replace("|-@height-|", "width=\"" + hightStr + "\"");
                            break;
                        default:
                            inputSb.Replace("|-@height-|", "height=\"" + hightStr + "\"");
                            break;
                    }
                }
            }
            if (xmlInputDoc.SelectSingleNode("/InputTypeSet").Attributes["readonly"] != null)
            {
                string readonlyStr = xmlInputDoc.SelectSingleNode("/InputTypeSet/@readonly").InnerText;
                if (readonlyStr.ToLower().Equals("true"))
                {
                    inputSb.Replace("|-@readonly-|", "readonly=\"readonly\"");
                }
                else
                {
                    inputSb.Replace("|-@readonly-|", "");
                }
            }
            sb.Replace("{input.temp_formitemtext}", inputSb.ToString());
            return sb.ToString();
        }
        private string setCheckBoxType(XmlDocument xmlDoc,string action,DataRow dataRow,string fieldName,string typeStr)
        {
            XmlNodeList options = xmlDoc.SelectNodes("/InputTypeSet/options/option");
            if (options.Count == 0) return "";
            string direction = xmlDoc.SelectSingleNode("/InputTypeSet/@direction").InnerText;
            string columns = xmlDoc.SelectSingleNode("/InputTypeSet/@columns").InnerText;
            int cols=Convert.ToInt32(columns);
            if (cols == 0) cols = 1;
            int rows =(int)Math.Ceiling((float)options.Count /(float)cols);
            int[,] n =new int[rows, cols];
            for (int k = 0; k < rows; k++)
            {
                for (int j = 0; j < cols; j++) n[k, j] = -1;
            }
            Dictionary<int, string> s = new Dictionary<int, string>();
            int i = 0;
            foreach (XmlNode node in options)
            {
                string label = node.Attributes["label"].Value;
                string check=node.Attributes["checked"].Value;
                string v = node.SelectSingleNode("value").InnerText;
                string control = "<input id=\"|-@inputName-|_" + i + "\" type=\""+typeStr+"\" name=\"|-@inputName-|\" $checked$ value=\"$value$\" style=\"|-@style-|\"/><label for=\"|-@inputName-|_" + i + "\">" + label + "</label>";
                control = control.Replace("$value$", v);
                if (action.Equals("add"))
                {
                    if (check.Equals("true"))
                    {
                        control = control.Replace("$checked$", "checked=\"checked\"");
                    }
                    else
                    {
                        control = control.Replace("$checked$", "");
                    }
                }
                else
                {
                    if (v.Equals(Convert.ToString(dataRow[fieldName])))
                    {
                        control = control.Replace("$checked$", "checked=\"checked\"");
                    }
                    else
                    {
                        control = control.Replace("$checked$", "");
                    }
                }
                s.Add(i, control);
                i++;
            }
            //设置排列
            for (i = 0; i < s.Count; i++)
            {
                 if (direction.Equals("Horizontal"))  //水平
                 {
                     int r = (int)(i / cols);
                    int c = i -r*cols;     //计算列号
                    n[r, c] = i;
                 }
                 else//垂直
                 {
                   int r = i % rows;
                   int c = (i-r) % cols; //列号
                   n[r, c] = i;
                 }
            }
            StringBuilder reSb = new StringBuilder("<table id=\"|-@inputName-|_tb\" border=\"0\">");
            for (i = 0; i < rows; i++)
            {
                reSb.Append("<tr>");
                for (int j = 0; j < cols; j++)
                {
                    if (n[i, j] != -1)
                    {
                        reSb.Append("<td>" + s[n[i, j]] + "</td>");
                    }
                    else
                    {
                        reSb.Append("<td></td>");
                    }
                }
                reSb.Append("</tr>");
            }
            reSb.Append("</table>");
            reSb.Replace("|-@inputName-|", fieldName);
            return reSb.ToString();
        }
        private string setSelectType(XmlDocument xmlDoc, string action, DataRow dataRow, string fieldName,string typeStr)
        {
            StringBuilder inputSb = new StringBuilder();
            if (typeStr.Equals("SelectOne"))
            {
                inputSb.Append("<select name=\"|-@inputName-|\" id=\"|-@inputName-|\" style=\"|-@style-|\">");
            }
            else
            {
                inputSb.Append("<select name=\"|-@inputName-|\" id=\"|-@inputName-|\" multiple=\"multiple\" style=\"|-@style-|\">");
            }
            //<InputTypeSet style=""><options><option label="推荐" checked="false"><value>1</value></option></options></InputTypeSet>
            StringBuilder oSb = new StringBuilder();
            XmlNodeList nodeList = xmlDoc.SelectNodes("/InputTypeSet/options/option");
            foreach (XmlNode node in nodeList)
            {
                string label = node.Attributes["label"].Value;
                string check = node.Attributes["checked"].Value;
                string v = node.SelectSingleNode("value").InnerText;
                string control = "<option value=\"$value$\" $checked$>$label$</option>";
                control = control.Replace("$value$", v);
                if (action.Equals("add"))
                {
                    if (check.Equals("true"))
                    {
                        control = control.Replace("$checked$", "selected =\"selected\"");
                    }
                    else
                    {
                        control = control.Replace("$checked$", "");
                    }
                }
                else
                {
                    if (v.Equals(Convert.ToString(dataRow[fieldName])))
                    {
                        control = control.Replace("$checked$", "selected=\"selected\"");
                    }
                    else
                    {
                        control = control.Replace("$checked$", "");
                    }
                }
                oSb.Append(control);
                oSb.Replace("$label$", label);
            }
            inputSb.Append(oSb.ToString());
            inputSb.Append("</select>");
            return inputSb.ToString();
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