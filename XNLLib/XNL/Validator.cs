using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
using COM.SingNo.XNLEngine;
namespace COM.SingNo.XNLLib.XNL
{
 public  class Validator:IXNLTag<WebContext>
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
               MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelContentStr,"validatorItem");
               int curItem = 0;
               foreach (Match item in itemMatchs)
               {
                   curItem += 1;
                   string attriablesStr = "@frontscript@$.formValidator.initConfig({@attriables@});\n@validatorList@"; //@scriptend@
                   int tmpParamId = 0;
                   string speatorStr = "";
                   string tmpattriablesStr = "";
                   Dictionary<string, XNLParam> itemParams = RegxpEngineCommon.getXNLParams(item.Groups[2].Value, XNLPage);
                   foreach (KeyValuePair<string, XNLParam> tmpParam in itemParams)
                   {
                       if (tmpParamId > 0) speatorStr = ",";
                       switch (tmpParam.Key)
                       {
                           case "alertmessage":
                               tmpattriablesStr += speatorStr + "alertmessage:" +Convert.ToString(tmpParam.Value.value);
                               break;
                           case "autotip":
                               tmpattriablesStr += speatorStr + "autotip:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "errorfocus":
                               tmpattriablesStr += speatorStr + "errorfocus:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "forcevalid":
                               tmpattriablesStr += speatorStr + "forcevalid:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "wideword":
                               tmpattriablesStr += speatorStr + "wideword:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "onsuccess":
                               tmpattriablesStr += speatorStr + "onsuccess:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "submitonce":
                               tmpattriablesStr += speatorStr + "submitonce:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "onerror":
                               tmpattriablesStr += speatorStr + "onerror:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           case "debug":
                               tmpattriablesStr += speatorStr + "debug:" + Convert.ToString(tmpParam.Value.value);
                               break;
                           default:
                               tmpattriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                               break;
                       }

                       tmpParamId += 1;
                   }
                   if (curItem > 1) attriablesStr = attriablesStr.Replace("@frontscript@", "");
                   if (curItem < itemMatchs.Count)
                   {
                       attriablesStr = attriablesStr.Replace("@validatorList@", "");
                      // attriablesStr = attriablesStr.Replace("@scriptend@", "");
                   }
                   attriablesStr = attriablesStr.Replace("@attriables@", tmpattriablesStr);
                   labelContentStr = labelContentStr.Replace(item.Value, attriablesStr);
                
               }
              // labelContentStr = labelContentStr + "@validatorList@";

               string itemStr = "";
               for (int i = 1; i <= Convert.ToInt32(labelParams["validatoritemcount"].value); i++)
               {
                   itemStr +=Convert.ToString(labelParams["validatoritem"+i].value);
               }

               labelContentStr = labelContentStr.Replace("@validatorList@", itemStr);
               string startScriptStr = "<script type=\"text/javascript\">\n$(document).ready(function(){\n ";
               labelContentStr = labelContentStr.Replace("@frontscript@", startScriptStr);
               labelContentStr = labelContentStr+ "\n})\n</script>";
               if (labelParams.ContainsKey("autoloadjs") && Convert.ToString(labelParams["autoloadjs"].value).Trim().ToLower().Equals("true"))
               {
                   var sitePath = XNLPage.curChannel.siteNode.siteWebPath;
                   StringBuilder jsSb = new StringBuilder();
                   if (!(labelParams.ContainsKey("loadjquery") && Convert.ToString(labelParams["loadjquery"].value).Trim().ToLower().Equals("false")))
                   {
                       if (XNLPage.hasJquery == false)
                       {
                           XNLPage.hasJquery = true;
                           jsSb.Append("\n<script src=\"" + sitePath + "/Common/jquery/jquery.js\" type=\"text/javascript\"></script>\n");
                       }
                   }
                   jsSb.Append( "<link type=\"text/css\" rel=\"stylesheet\" href=\"" + sitePath + "/Common/Validator/css/validator.css\"></link>\n");
                   jsSb.Append("<script src=\"" + sitePath + "/Common/Validator/js/formValidator.js\" type=\"text/javascript\"></script>\n");
                   jsSb.Append( "<script src=\"" + sitePath + "/Common/Validator/js/formValidatorRegex.js\" type=\"text/javascript\"></script>\n");
                   jsSb.Append( "<script src=\"" + sitePath + "/Common/Validator/js/formValidatorRegex.js\" type=\"text/javascript\"></script>\n");
                   jsSb.Append("<script src=\"" + sitePath + "/Common/datepicker/WdatePicker.js\" type=\"text/javascript\"></script>\n");
                   labelContentStr = jsSb.Append( labelContentStr).ToString();
               }
             return labelContentStr;
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
