using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
namespace COM.SingNo.DAL
{
   public class DALUtils
   {
       public const RegexOptions MatchOptions = (((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
       /// <summary>
       /// 根据提供的正则表达式得到标签集合
       /// </summary>
       /// <param name="labelStr"></param>
       /// <param name="regexStr"></param>
       /// <returns>xnl标签的所有集合</returns>
       public static MatchCollection getMatchCollsByRegex(string labelStr, string regexStr)
       {
           System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regexStr, MatchOptions);
           MatchCollection matchColls = reg.Matches(labelStr);
           return matchColls;
       }
       /// <summary>
       /// 只替换第一个匹配
       /// </summary>
       /// <param name="srcStr"></param>
       /// <param name="oldStr"></param>
       /// <param name="newStr"></param>
       /// <returns></returns>
       public static string onceReplace(string srcStr, string oldStr, string newStr)
       {
           int startPos = srcStr.IndexOf(oldStr);
           if (startPos == -1 || oldStr.Trim().Equals(string.Empty)) return srcStr;
           int endPos = startPos + oldStr.Length + "<xnlReplace>".Length;
           srcStr = srcStr.Insert(startPos, "<xnlReplace>");
           srcStr = srcStr.Insert(endPos, "</xnlReplace>");
           return srcStr.Replace("<xnlReplace>" + oldStr + "</xnlReplace>", newStr);
       }
    }
}
