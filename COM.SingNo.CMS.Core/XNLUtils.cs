using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using System.Text.RegularExpressions;
using System.Data;
using COM.SingNo.DAL;
namespace COM.SingNo.CMS.Core
{
   public class XNLUtils
    {
       public const RegexOptions MatchOptions = (((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
       public static DataTable setContentVariableData(WebContext XNLPage)
       {
           DataTable tb = null;
           //设置模板内容中的内容表达式变量
           if (XNLPage.items != null && XNLPage.items.ContainsKey("@_c"))
           {
               tb = (DataTable)XNLPage.items["@_c"];
           }
           else
           {
               //if (XNLPage.items == null) XNLPage.items = new Dictionary<string, object>(1);
               string dbFields;
               if (!(XNLPage.globalAttriableColls != null && XNLPage.globalAttriableColls.TryGetValue("$_cf_$", out dbFields)))
               {
                   dbFields = "*";
               }
               tb = DataHelper.ExecuteDataTable("select "+dbFields+" from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
               XNLContext.setItem(XNLPage, "@_c", tb);
               //XNLPage.items.Add("@_c", tb);
           }
           return tb;
       }
       public static DataTable setContentVariableData(string contentField,WebContext XNLPage)
       {
           DataTable tb = null;
           if (XNLPage.items != null && XNLPage.items.ContainsKey("@_c"))
           {
                 tb = (DataTable)XNLPage.items["@_c"];
                 if (contentField.Equals("styletitle"))contentField = "title";
                 if (contentField.Equals("pagecontent")) contentField = "content";
                 if (!tb.Columns.Contains(contentField))
                 {
                     DataTable t_dt = DataHelper.ExecuteDataTable("select " + contentField + " from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
                     tb.Columns.Add(t_dt.Columns[0].ColumnName, t_dt.Columns[0].DataType);
                     tb.Rows[0][contentField] = t_dt.Rows[0][0];
                 }
                 if (XNLPage.accessType == AccessType.Static) //静态模式
                 {
                     List<string> cfl = (List<string>)(XNLPage.items["@_c_f"]);
                     if (!cfl.Contains(contentField)) cfl.Add(contentField);
                 }
                 return tb;
           }
           if (XNLPage.accessType == AccessType.Static) //静态模式
           {
                   if (XNLPage.items == null) XNLPage.items = new Dictionary<string, object>(2);
                   object cfl_obj = null;
                   string fields;
                   if (XNLPage.items.TryGetValue("@_c_f", out cfl_obj))
                   {
                       List<string> _cfl = (List<string>)cfl_obj;
                       if (!_cfl.Contains(contentField))
                       {
                           _cfl.Add(contentField);
                       }
                       if (!_cfl.Contains("titlecolor"))
                       {
                           _cfl.Add("titlecolor");
                       }
                       if (!_cfl.Contains("bold"))
                       {
                           _cfl.Add("bold");
                       }
                       if (!_cfl.Contains("italic"))
                       {
                           _cfl.Add("italic");
                       }
                       if (!_cfl.Contains("underline"))
                       {
                           _cfl.Add("underline");
                       }
                       if (!_cfl.Contains("pagetype"))
                       {
                           _cfl.Add("pagetype");
                       }
                       if (!_cfl.Contains("pagewords"))
                       {
                           _cfl.Add("pagewords");
                       }
                       //if (!_cfl.Contains("tag"))
                       //{
                       //    _cfl.Add("tag");
                       //}
                       int i = 0;
                       fields = "";
                       foreach (string str in _cfl)
                       {
                           fields += (i == 0 ? str : ("," + str));
                           i++;
                       }
                   }
                   else
                   {
                       fields = "*";
                       List<string> cf_l = new List<string>();
                       if (XNLPage.curChannel.model.getFieldByName("content") != null)
                       {
                           cf_l.Add("content");
                       }
                       cf_l.Add(contentField);
                       cf_l.Add("titlecolor");
                       cf_l.Add("bold");
                       cf_l.Add("italic");
                       cf_l.Add("underline");
                       cf_l.Add("pagetype");
                       cf_l.Add("pagewords");
                       //cf_l.Add("tag");
                       XNLPage.items.Add("@_c_f", cf_l);
                   }
                   tb = DataHelper.ExecuteDataTable("select "+fields+" from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
                   XNLPage.items.Add("@_c", tb);
           }
           else //动态模式
           {
               if (XNLPage.items == null) XNLPage.items = new Dictionary<string, object>(1);
               string fields;
               if(!(XNLPage.globalAttriableColls!=null&&XNLPage.globalAttriableColls.TryGetValue("$_cf_$",out fields))) //$_cf_$内容表字段
               {
                   fields = "*";
               }
               tb = DataHelper.ExecuteDataTable("select "+fields+" from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
               XNLPage.items.Add("@_c", tb);
           }           
           return tb;
       }
       public static DataTable updateContentPageData(DataTable tb,string contentField, WebContext XNLPage)
       {
           if (contentField.Equals("styletitle")) contentField = "title";
           if (!tb.Columns.Contains(contentField))
           {
               DataTable t_dt = DataHelper.ExecuteDataTable("select " + contentField + " from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
               tb.Columns.Add(t_dt.Columns[0].ColumnName, t_dt.Columns[0].DataType);
               tb.Rows[0][contentField] = t_dt.Rows[0][0];
               if (XNLPage.accessType == AccessType.Static) //静态模式
               {
                   List<string> cfl = (List<string>)(XNLPage.items["@_c_f"]);
                   if(!cfl.Contains(contentField))cfl.Add(contentField);
               }
           }
           return tb;
       }
       public static DataTable setContentVariableDataRow(string contentField, WebContext XNLPage)
       {
           //根据内容表字段得到数据
           DataTable tb = null;
           //设置模板内容中的内容表达式变量
           if (XNLPage.items != null && XNLPage.items.ContainsKey("@_c"))
           {
               tb = (DataTable)XNLPage.items["@_c"];
               if (contentField.Equals("styletitle")) contentField = "title";
               if (contentField.Equals("pagecontent")) contentField = "content";
               if(!tb.Columns.Contains(contentField))
               {
                  DataTable t_dt=DataHelper.ExecuteDataTable("select " + contentField + " from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
                  tb.Columns.Add(t_dt.Columns[0].ColumnName, t_dt.Columns[0].DataType);
                  tb.Rows[0][contentField] = t_dt.Rows[0][0];
                  List<string> cfl = (List<string>)(XNLPage.items["@_c_f"]);
                  cfl.Add(contentField);
               }
           }
           else
           {
               if (XNLPage.items == null) XNLPage.items = new Dictionary<string, object>();
               string t_fiels = contentField;
               object cfl_obj = null;
               if (XNLPage.items.TryGetValue("@_c_f", out cfl_obj))
               {
                   List<string> _cfl = (List<string>)cfl_obj;
                   if (!_cfl.Contains(contentField))
                   {
                       _cfl.Add(contentField);
                   }
                   int i=0;
                   t_fiels="";
                   foreach (string str in _cfl)
                   {
                       t_fiels += (i == 0 ? str : ("," + str));
                       i++;
                   }
               }
               else
               {
                   List<string> cf_l = new List<string>();
                   if (XNLPage.curChannel.model.getFieldByName("content") != null)
                   {
                       cf_l.Add("content");
                   }
                   cf_l.Add(contentField);
                   XNLPage.items.Add("@_c_f", cf_l);
               }
               tb = DataHelper.ExecuteDataTable("select " + t_fiels + " from " + XNLPage.curChannel.model.TableName + " where id=" + XNLPage.contentId.ToString());
               XNLPage.items.Add("@_c", tb);
           }
           return tb;
       }
       /// <summary>
       /// 由站点名称得到站点对象
       /// </summary>
       /// <param name="siteName"></param>
       /// <returns></returns>
       public static Cchannel getSiteByName(string siteName)
       {
           Cchannel channelObj = null;
           try
           {
               foreach (KeyValuePair<int, Cchannel> site in SiteConfigManager.createInstance().siteDataColls)
               {
                   if (site.Value.nodeName.ToLower().Equals(siteName.ToLower()))
                   {
                       channelObj = site.Value;
                       break;
                   }
               }
           }
           catch
           {
           }
           if (channelObj == null) { throw (new Exception("查找站点出错!")); }
           return channelObj;
       }
       /// <summary>
       /// 在指定节点下的全部节点里查找指定名称的节点
       /// </summary>
       /// <param name="channelName"></param>
       /// <param name="curChannel"></param>
       /// <returns></returns>
       public static Cchannel getChannelByNameFromChannel(string channelName, Cchannel curChannel)
       {
           /*
           XNLParam _param = new XNLParam("string", channelName, DbType.String);
           Dictionary<string, XNLParam> _params = new Dictionary<string, XNLParam>(1);
           _params.Add("nodename", _param);
           try
           {
               object nodeObj = DataHelper.ExecuteScalar("select nodeid from sn_nodes where nodename=@nodename and rootid=" + curChannel.nodeID, _params);
               return ChannelConfigManager.createInstance().channelDataColls[Convert.ToInt32(nodeObj)];
           }
           catch
           {
               return null;
           }
            */
           Cchannel outChannel = null;
           try
           {
               _getChannelByNameFromChannel(channelName, curChannel,ref outChannel);
               return outChannel;
           }
           catch
           {
               return null;
           }
       }
       private static void _getChannelByNameFromChannel(string channelName, Cchannel curChannel,ref Cchannel outChannel)
       {
           if (outChannel != null) return;
           if (string.Compare(curChannel.nodeName, channelName, true) == 0)
           {
               outChannel=curChannel;
               return;
           }
           else if (curChannel.subNodeColls != null)
           {
               foreach (KeyValuePair<int, Cchannel> _channel in curChannel.subNodeColls)
               {
                   if (string.Compare(_channel.Value.nodeName, channelName, true) == 0)
                   {
                       outChannel=_channel.Value;
                       return;
                   }
                   else
                   {
                       _getChannelByNameFromChannel(channelName, _channel.Value,ref outChannel);
                   }
               }
           }
       }
       /// <summary>
       /// 在指定节点下的全部节点里查找指定索引名称的节点
       /// </summary>
       /// <param name="IndexName"></param>
       /// <param name="curChannel"></param>
       /// <returns></returns>
       public static Cchannel getChannelByIndexFromChannel(string IndexName, Cchannel curChannel)
       {
           /*
           XNLParam _param = new XNLParam("string", IndexName, DbType.String);
           Dictionary<string, XNLParam> _params = new Dictionary<string, XNLParam>(1);
           _params.Add("indexname", _param);
           try
           {
               object nodeObj = DataHelper.ExecuteScalar("select nodeid from sn_nodes where indexname=@indexname and rootid=" + curChannel.nodeID, _params);
               return ChannelConfigManager.createInstance().channelDataColls[Convert.ToInt32(nodeObj)];
           }
           catch
           {
               return null;
           }
            */
           Cchannel outChannel = null;
           try
           {
               _getChannelByIndexFromChannel(IndexName, curChannel,ref outChannel);
               return outChannel;
           }
           catch
           {
               return null;
           }
       }
       private static void _getChannelByIndexFromChannel(string IndexName,Cchannel curChannel,ref Cchannel outChannel)
       {
           if (outChannel != null) return;
           if (string.Compare(curChannel.nodeIndexName, IndexName, true) == 0)
           {
               outChannel= curChannel;
               return;
           }
           else if (curChannel.subNodeColls != null)
           {
               foreach (KeyValuePair<int, Cchannel> _channel in curChannel.subNodeColls)
               {
                   if (string.Compare(_channel.Value.nodeIndexName, IndexName, true) == 0)
                   {
                       outChannel= _channel.Value;
                       return;
                   }
                   else
                   {
                       _getChannelByIndexFromChannel(IndexName, _channel.Value,ref outChannel);
                   }
               }
           }
       }
       /// <summary>
       /// 在指定节点的子节点里查找指定名称的节点
       /// </summary>
       /// <param name="channelName"></param>
       /// <param name="channel"></param>
       /// <returns></returns>
       public static Cchannel getChannelByNameFromSub(string channelName, Cchannel channel)
       {
           Cchannel channelObj = null;
           try
           {
               foreach (KeyValuePair<int, Cchannel> _channel in channel.subNodeColls)
               {
                   if (string.Compare(_channel.Value.nodeName, channelName, true) == 0)
                   {
                       channelObj = _channel.Value;
                       break;
                   }
               }
           }
           catch
           {

           }
           return channelObj;
       }
       /// <summary>
       /// 根据提供的正则表达式得到标签集合
       /// </summary>
       /// <param name="labelStr"></param>
       /// <param name="regexStr"></param>
       /// <returns>xnl标签的所有集合</returns>
       public static MatchCollection getMatchCollsByRegex(string labelStr, string regexStr)
       {
           System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regexStr, (((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) | RegexOptions.Multiline) | RegexOptions.IgnoreCase));
           MatchCollection matchColls = reg.Matches(labelStr);
           return matchColls;
       }
       public static string getChannelImageUrl(Cchannel channel)
       {
           string url = channel.imageUrl;
           if(url.IndexOf("://")!=-1)
           {
               url = SystemConfig.systemUrl + ((url.StartsWith("@/") ? url : "@/" + url).Replace("@", channel.siteWebPath).Replace("//", "/"));
           }
           return url;
       }
       public static string getChannelImagePath(Cchannel channel)
       {
           string url = channel.imageUrl;
           if (url.IndexOf("://") != -1)
           {
               url = ((url.StartsWith("@/") ? url : "@/" + url).Replace("@", channel.siteWebPath).Replace("//", "/"));
           }
           return url;
       }
       public static Dictionary<string,List<string>> getContentVarList(Cchannel channel, string contentStr)
       {
           MatchCollection matchs = Regex.Matches(contentStr, "{@content\\.(.+?)}", MatchOptions);
           if (matchs.Count == 0)
           {
               return null;
           }
           else
           {
               Dictionary<string, List<string>> allList = new Dictionary<string, List<string>>(2);
               List<string> fieldList = new List<string>();
               List<string> eList = new List<string>();
               fieldList.Add("id");
               fieldList.Add("nodeid");
               fieldList.Add("pagetype");
               fieldList.Add("pagewords");
               allList.Add("f", fieldList);//数据库字段
               allList.Add("e", eList);//ext(扩展)字段
               foreach (Match match in matchs)
               {
                   string lowStr = match.Groups[1].Value.ToLower();
                   switch (lowStr)
                   {
                       case "inputuser":
                       case "lastedituser":
                       case "lasteditdate":
                       case "dayhits":
                       case "weekhits":
                       case "monthhits":
                       case "title":
                       case "indexid":
                       case "createtime":
                       case "passedtime":
                       //case "tag":
                       case "pinyintitle":
                       case "pytitle":
                       case "iscomment":
                       case "isrecycle":
                       case "state":
                       case "lasthittime":
                       case "hits":
                       case "readpoint":
                       case "titlecolor":
                       case "underline":
                       case "italic":
                       case "bold":
                       case "stars":
                       case "diggs":
                       case "comments":
                           if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           break;
                       case "pagecontent":
                           bool Have = false;
                           foreach (ModelField Field in channel.model.fieldList)
                           {
                               if (string.Compare(Field.FieldName,"content",true)==0)
                               {
                                   Have = true;
                                   break;
                               }
                           }
                           if (Have)
                           {
                               if (!fieldList.Contains("content")) fieldList.Add("content");
                           }
                           if (!eList.Contains("pagecontent")) eList.Add("pagecontent");
                           break;
                       case "path":
                           if (!fieldList.Contains("linkurl")) fieldList.Add("linkurl");
                           if (!eList.Contains("path")) eList.Add("path");
                           break;
                       case "url":
                           if (!fieldList.Contains("linkurl")) fieldList.Add("linkurl");
                           if (!eList.Contains("url")) eList.Add("url");
                           break;
                       case "styletitle":
                           if (!fieldList.Contains("titlecolor")) fieldList.Add("titlecolor");
                           if (!fieldList.Contains("underline")) fieldList.Add("underline");
                           if (!fieldList.Contains("italic")) fieldList.Add("italic");
                           if (!fieldList.Contains("bold")) fieldList.Add("bold");
                           if (!fieldList.Contains("title")) fieldList.Add("title");
                           if (!eList.Contains("styletitle")) eList.Add("styletitle");
                           break;
                       case "dir":
                       case "dirpath":
                       case "dirurl":
                       case "pagefilename":
                       case "pagefileext":
                       case "pagefile":
                           if (!eList.Contains(lowStr)) eList.Add(lowStr);
                           break;
                       default:
                           bool isHave = false;
                           foreach (ModelField Field in channel.model.fieldList)
                           {
                               if (string.Compare(Field.FieldName,lowStr,true)==0)
                               {
                                   isHave = true;
                                   break;
                               }
                           }
                           if (isHave)
                           {
                               if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           }
                           break;
                   }
               }
               return allList;
           }
       }
       public static Dictionary<string, List<string>> getContentsVarList(string contentStr)
       {
           MatchCollection matchs = Regex.Matches(contentStr, "{@contents\\.(.+?)}", MatchOptions);
           if (matchs.Count == 0)
           {
               return null;
           }
           else
           {
               Dictionary<string, List<string>> allList = new Dictionary<string, List<string>>(2);
               List<string> fieldList = new List<string>();
               List<string> eList = new List<string>();
               fieldList.Add("id");
               fieldList.Add("nodeid");
               fieldList.Add("pagetype");
               fieldList.Add("pagewords");
               allList.Add("f", fieldList);//数据库字段
               allList.Add("e", eList);//ext(扩展)字段
               foreach (Match match in matchs)
               {
                   string lowStr = match.Groups[1].Value.ToLower();
                   switch (lowStr)
                   {
                       case "inputuser":
                       case "lastedituser":
                       case "lasteditdate":
                       case "dayhits":
                       case "weekhits":
                       case "monthhits":
                       case "title":
                       case "indexid":
                       case "createtime":
                       case "passedtime":
                       //case "tag":
                       case "pinyintitle":
                       case "pytitle":
                       case "iscomment":
                       case "isrecycle":
                       case "state":
                       case "lasthittime":
                       case "hits":
                       case "readpoint":
                       case "titlecolor":
                       case "underline":
                       case "italic":
                       case "bold":
                       case "stars":
                       case "diggs":
                       case "comments":
                           if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           break;
                       case "pagecontent":
                           if (!eList.Contains("content")) eList.Add("content");
                           break;
                       case "path":
                       case "url":
                       case "autourl":
                       case "autopath":
                           if (!eList.Contains("linkurl")) eList.Add("linkurl");
                           break;
                       case "styletitle":
                           if (!fieldList.Contains("titlecolor")) fieldList.Add("titlecolor");
                           if (!fieldList.Contains("underline")) fieldList.Add("underline");
                           if (!fieldList.Contains("italic")) fieldList.Add("italic");
                           if (!fieldList.Contains("bold")) fieldList.Add("bold");
                           if (!eList.Contains("title")) eList.Add("title");
                           break;
                       case "dir":
                       case "dirpath":
                       case "dirurl":
                       case "filename":
                       case "fileext":
                       case "file":
                       case "channelid":
                       case "channelname":
                       case "channelindex":
                           break;
                       default:
                           if (!eList.Contains(lowStr)) eList.Add(lowStr);
                           break;
                   }
               }
               return allList;
           }
       }

       public static Dictionary<string, List<string>> getContentNavVarList(string contentStr)
       {
           MatchCollection matchs = Regex.Matches(contentStr, "{@contentnav\\.(.+?)}", MatchOptions);
           if (matchs.Count == 0)
           {
               return null;
           }
           else
           {
               Dictionary<string, List<string>> allList = new Dictionary<string, List<string>>(2);
               List<string> fieldList = new List<string>();
               List<string> eList = new List<string>();
               fieldList.Add("id");
               fieldList.Add("nodeid");
               fieldList.Add("pagetype");
               fieldList.Add("pagewords");
               allList.Add("f", fieldList);//数据库字段
               allList.Add("e", eList);//ext(扩展)字段
               foreach (Match match in matchs)
               {
                   string lowStr = match.Groups[1].Value.ToLower();
                   switch (lowStr)
                   {
                       case "inputuser":
                       case "lastedituser":
                       case "lasteditdate":
                       case "dayhits":
                       case "weekhits":
                       case "monthhits":
                       case "title":
                       case "indexid":
                       case "createtime":
                       case "passedtime":
                       //case "tag":
                       case "pinyintitle":
                       case "pytitle":
                       case "iscomment":
                       case "isrecycle":
                       case "state":
                       case "lasthittime":
                       case "hits":
                       case "readpoint":
                       case "titlecolor":
                       case "underline":
                       case "italic":
                       case "bold":
                       case "stars":
                       case "diggs":
                       case "comments":
                           if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           break;
                       case "pagecontent":
                           if (!eList.Contains("content")) eList.Add("content");
                           break;
                       case "path":
                       case "url":
                       case "autourl":
                       case "autopath":
                           if (!eList.Contains("linkurl")) eList.Add("linkurl");
                           break;
                       case "styletitle":
                           if (!fieldList.Contains("titlecolor")) fieldList.Add("titlecolor");
                           if (!fieldList.Contains("underline")) fieldList.Add("underline");
                           if (!fieldList.Contains("italic")) fieldList.Add("italic");
                           if (!fieldList.Contains("bold")) fieldList.Add("bold");
                           if (!eList.Contains("title")) eList.Add("title");
                           break;
                       case "dir":
                       case "dirpath":
                       case "dirurl":
                       case "filename":
                       case "fileext":
                       case "file":
                       case "channelid":
                       case "channelname":
                       case "channelindex":
                           break;
                       default:
                           if (!eList.Contains(lowStr)) eList.Add(lowStr);
                           break;
                   }
               }
               return allList;
           }
       }

       public static Dictionary<string, List<string>> getPageContentsVarList(string contentStr)
       {
           MatchCollection matchs = Regex.Matches(contentStr, "{@pagecontents\\.(.+?)}", MatchOptions);
           if (matchs.Count == 0)
           {
               return null;
           }
           else
           {
               Dictionary<string, List<string>> allList = new Dictionary<string, List<string>>(2);
               List<string> fieldList = new List<string>();
               List<string> eList = new List<string>();
               fieldList.Add("id");
               fieldList.Add("nodeid");
               fieldList.Add("pagetype");
               fieldList.Add("pagewords");
               allList.Add("f", fieldList);//数据库字段
               allList.Add("e", eList);//ext(扩展)字段
               foreach (Match match in matchs)
               {
                   string lowStr = match.Groups[1].Value.ToLower();
                   switch (lowStr)
                   {
                       case "inputuser":
                       case "lastedituser":
                       case "lasteditdate":
                       case "dayhits":
                       case "weekhits":
                       case "monthhits":
                       case "title":
                       case "indexid":
                       case "createtime":
                       case "passedtime":
                       //case "tag":
                       case "pinyintitle":
                       case "pytitle":
                       case "iscomment":
                       case "isrecycle":
                       case "state":
                       case "lasthittime":
                       case "hits":
                       case "readpoint":
                       case "titlecolor":
                       case "underline":
                       case "italic":
                       case "bold":
                       case "stars":
                       case "diggs":
                       case "comments":
                           if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           break;
                       case "pagecontent":
                           if (!eList.Contains("content")) eList.Add("content");
                           break;
                       case "path":
                       case "url":
                       case "autourl":
                       case "autopath":
                           if (!eList.Contains("linkurl")) eList.Add("linkurl");
                           break;
                       case "styletitle":
                           if (!fieldList.Contains("titlecolor")) fieldList.Add("titlecolor");
                           if (!fieldList.Contains("underline")) fieldList.Add("underline");
                           if (!fieldList.Contains("italic")) fieldList.Add("italic");
                           if (!fieldList.Contains("bold")) fieldList.Add("bold");
                           if (!eList.Contains("title")) eList.Add("title");
                           break;
                       case "dir":
                       case "dirpath":
                       case "dirurl":
                       case "filename":
                       case "fileext":
                       case "file":
                       case "channelid":
                       case "channelname":
                       case "channelindex":
                           break;
                       default:
                           if (!eList.Contains(lowStr)) eList.Add(lowStr);
                           break;
                   }
               }
               return allList;
           }
       }
    }
}
