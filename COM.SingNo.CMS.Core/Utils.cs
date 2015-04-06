using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using COM.SingNo.XNLCore;
namespace COM.SingNo.CMS.Core
{
   public class CMSUtils
   {
       internal const RegexOptions XNL_RegexOptions = (((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
       /// <summary>
       /// 以路径加载模板内容
       /// </summary>
       /// <param name="pathStr"></param>
       /// <returns></returns>
       public static string loadTempleteByPath(string pathStr,Encoding encoder)
       {
           StreamReader TxtReader = new StreamReader(pathStr, encoder);
           string FileContent;
           FileContent = TxtReader.ReadToEnd();
           TxtReader.Close();
           return FileContent;
       }

       public static string getExtName(string pathStr)
       {
           int index = pathStr.IndexOf('.');
           if (index == -1) return "";
           string extName = pathStr.Substring(index);
           return extName;
       }
       
       public static string getAbsolutePath(string filePath)
       {
           filePath=filePath.Replace("\\","/");
           int rootid = filePath.IndexOf('@');
           if (rootid >= 0)
           {
               filePath = filePath.Substring(rootid + 1);
               if (!filePath.StartsWith("/"))
               {
                   filePath = "/" + filePath;
               }
               return "@" + filePath;
           }
           else
           {
               string pathStr = "";
               char[] split_arr = { '/' };
               string[] path_arr = filePath.Split(split_arr, StringSplitOptions.RemoveEmptyEntries);
               for (int i = 0; i < path_arr.Length; i++)
               {
                   pathStr += "/"+path_arr[i];
               }
               return "@" + pathStr;
           }
       }
       /// <summary>
       /// 根据生成文件路径得目录路径
       /// </summary>
       /// <param name="contentPageNameRule">内容页命名规则</param>
       /// <returns></returns>
       public static string getDirectoryPath(string filePath)
       {
           string pathStr = "";
           filePath = filePath.Replace("\\", "/");
           int rootid = filePath.IndexOf('@');
           if (rootid >= 0)
           {
               filePath = filePath.Substring(rootid + 1, filePath.Length);
           }
           char[] split_arr = { '/' };
           string[] path_arr = filePath.Split(split_arr, StringSplitOptions.RemoveEmptyEntries);
           for (int i = 0; i < path_arr.Length; i++)
           {
               if (path_arr[i].IndexOf('.') < 0)
               {
                   pathStr += path_arr[i] + "/";
               }
           }
           return "@/" + pathStr;
       }

       public static void CreateDirectory(string dirPath)
       {
           if (!Directory.Exists(dirPath))
           {
               Directory.CreateDirectory(dirPath);
           }
       }
       /// <summary>
       /// 由路径规则得到真实路径
       /// </summary>
       /// <param name="pathRule"></param>
       /// <param name="channel"></param>
       /// <param name="pageType"></param>
       /// <param name="contentId"></param>
       /// <returns></returns>
       public static string getPathByPathRule(string pathRule, int channelId, string channelIndexName,string channelName, int pageType, string contentId)
       {
           
           string fullPath = CMSUtils.getAbsolutePath(pathRule);
           fullPath = fullPath.Replace("[#ChannelID]", channelId.ToString());
           fullPath = fullPath.Replace("[#ChannelIndexName]", channelIndexName);
           fullPath = fullPath.Replace("[#ChannelName]", channelName);
           if (pageType == 2)
           {
               string extStr = CMSUtils.getExtName(pathRule);
               if (extStr.ToLower().Equals(".aspx"))
               {
                   fullPath = fullPath.Replace("[#ContentID]", "Content");
               }
               else
               {
                   fullPath = fullPath.Replace("[#ContentID]", contentId);
               }
           }
           return fullPath;
       }

       public static List<string> getContentFieldList(Cchannel channel,string contentStr)
       {
           MatchCollection matchs = Regex.Matches(contentStr, "{@content\\.(.+?)}", XNL_RegexOptions);
           if(matchs.Count==0)
           {
               return null;
           }
           else
           {
               List<string> fieldList=new List<string>();
               fieldList.Add("id");
               fieldList.Add("nodeid");
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
                       case "tag":
                       case "pinyintitle":
                       case "pytitle":
                       case "pagetype":
                       case "pagewords":
                           if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           break;
                       case "content":
                       case "pagecontent":
                           bool Have = false;
                           foreach (ModelField Field in channel.model.fieldList)
                           {
                               if (Field.FieldName.ToLower().Equals("content"))
                               {
                                   Have = true;
                                   break;
                               }
                           }
                           if (Have)
                           {
                               if (!fieldList.Contains("content")) fieldList.Add("content");
                           }
                           break;
                       case "path":
                       case "url":
                           if (!fieldList.Contains("linkurl")) fieldList.Add("linkurl");
                           break;
                       case "styletitle":
                           if (!fieldList.Contains("titlecolor")) fieldList.Add("titlecolor");
                           if (!fieldList.Contains("underline")) fieldList.Add("underline");
                           if (!fieldList.Contains("italic")) fieldList.Add("italic");
                           if (!fieldList.Contains("bold")) fieldList.Add("bold");
                           if (!fieldList.Contains("title")) fieldList.Add("title");
                           break;
                       default:
                           bool isHave = false;
                           foreach (ModelField Field in channel.model.fieldList)
                           {
                               if (Field.FieldName.ToLower().Equals(lowStr))
                               {
                                   isHave = true;
                                   break;
                               }
                           }
                           if(isHave)
                           {
                               if (!fieldList.Contains(lowStr)) fieldList.Add(lowStr);
                           }
                           break;
                   }
               }
               return fieldList;
           }
       }

       public static int getTemplateProjectIdByName(Cchannel site,string projectName)
       {
           try
           {
               foreach (KeyValuePair<int, TemplateProject> project in site.templateProjectColls)
               {
                   if (project.Value.templateProjectName.ToLower().Equals(projectName.ToLower()))
                   {
                       return project.Key;
                   }
               }
           }
           catch
           {
               return 0;
           }
           return 0;
       }

       public static void writeFile(string filePath, string charset, string responseStr)
       {
           Encoding encoder = Encoding.GetEncoding(charset);
           using (FileStream fs = File.Create(filePath))
           {
               byte[] b_info = encoder.GetBytes(responseStr);
               fs.Write(b_info, 0, b_info.Length);
           }
       }
    }
}
