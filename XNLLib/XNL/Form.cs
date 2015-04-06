using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using LitJson;
using System.Text.RegularExpressions;
using COM.SingNo.Common;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
    public class Form : IXNLTag<WebContext>
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
      //private Dictionary<string, XNLParam> Params;
        #region XNLBase 成员
        public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            //<form[\s]*[^>]*name=([^\s>]*)([\s]*|[^>]*)>
            //<form[\\s]*[^>]*name=([^\\s>]*)([\\s]*|[^>]*)>
            return "";
          /*
            string formName = "";
            if (labelParams.ContainsKey("name"))
            {
                formName =Convert.ToString( labelParams["name"].value);
            }
            else
            {
                Match nameMatch = Regex.Match(labelContentStr, "<form[\\s]*[^>]*name=([^\\s>]*)([\\s]*|[^>]*)>", XNLBaseCommon.XNL_RegexOptions);
                if (nameMatch.Success)
                {
                    formName = nameMatch.Groups[1].Value;
                    string nfirst=formName.Substring(0,1);
                    if(nfirst=="\""||nfirst=="'")
                    {
                       formName=formName.Substring(1, formName.Length - 2);
                    }
                    labelParams.Add("name",new XNLParam(formName));
                }
                else
                {
                    throw (new Exception("XNL:form标签出错</br>出错原因:表单没有定义name属性</br>出错表单内容:</br>" + labelContentStr));
                    //XNLDebug.show("XNL:form标签出错</br>出错原因:表单没有定义name属性</br>出错表单内容:</br>" + labelContentStr);
                    //XNLDebug.end();
                }
            }
            
            if (!labelParams.ContainsKey("split"))
            {
                labelParams.Add("split", new XNLParam("|"));
            }
            bool writeFormState = false;
            XNLParam StateParam;
            if (labelParams.TryGetValue("enablestate", out StateParam))
            {
                if (Convert.ToString( StateParam.value).Trim().ToLower().Equals("true")) writeFormState = true;
            }
            int validatorItemCount = 0;           
            //this.Params = labelParams;
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();
            writer.WritePropertyName("sp"); //splitstr
            writer.Write(Convert.ToString(labelParams["split"].value));
            if (labelParams.ContainsKey("sqlcommand"))
          {
            writer.WritePropertyName("sql");
            writer.Write(Convert.ToString(labelParams["sqlcommand"].value));
          }
            writer.WritePropertyName("fs"); //fields
            
            writer.WriteArrayStart();

            MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "formItem");

            int i = 0;
            foreach (Match item in itemMatchs)
            {
                Dictionary<string, XNLParam> itemParams = RegxpEngineCommon.getXNLParams(item.Groups[2].Value, XNLPage);
                string IsEncodeXnl = "true";
                string IsEncodeHtml = "true";
                if (itemParams.ContainsKey("encodexnl"))
                {
                    if (Convert.ToString(itemParams["encodexnl"].value).ToLower().Trim() == "false") IsEncodeXnl = "false";
                }

                if (itemParams.ContainsKey("encodehtml"))
                {
                    if (Convert.ToString( itemParams["encodehtml"].value).ToLower().Trim() == "false") IsEncodeHtml = "false";
                }
                string itemContentStr = item.Groups[3].Value;
                writer.WriteObjectStart();
                if (itemParams.ContainsKey("dbdefault"))
                {
                    writer.WritePropertyName("db"); //field
                    writer.Write(Convert.ToString(itemParams["dbdefault"].value));
                }
                if (itemParams.ContainsKey("name"))
                {
                    writer.WritePropertyName("n");  //name
                    writer.Write(Convert.ToString( itemParams["name"].value));
                }
                else
                {
                    string formItemName = "";
                    //  ^name=([\"\'])(.*)?\\1$|^name=([^\'\"\\s>]*)?\\b
                    Match nameMatch = Regex.Match(itemContentStr, "name=([\"\'])([.\\s\\S]*?)\\1|name=([^\'\"\\s>]*)?\\b", XNLBaseCommon.XNL_RegexOptions);
                    if (nameMatch.Success)
                    {
                        if (nameMatch.Groups[1].Value == "\"" || nameMatch.Groups[1].Value == "'")
                        {
                            formItemName = nameMatch.Groups[2].Value;
                        }
                        else
                        {
                            formItemName = nameMatch.Groups[3].Value;
                        }
                        writer.WritePropertyName("n");  //name
                        writer.Write(formItemName);
                        itemParams.Add("name", new XNLParam(formItemName));
                    }
                    else
                    {
                        throw(new Exception("XNL:form标签出错</br>出错原因:表单项没有定义name属性</br>出错表单项:</br>" + itemContentStr));
                        //XNLDebug.show("XNL:form标签出错</br>出错原因:表单项没有定义name属性</br>出错表单项:</br>" + itemContentStr);
                        //XNLDebug.end();
                    }
                }

                writer.WritePropertyName("ex"); //encodexnl
                writer.Write(IsEncodeXnl);
                writer.WritePropertyName("eh"); //encodehtml
                writer.Write(IsEncodeHtml);
                #region 加入验证信息
                MatchCollection validatorMatch = RegxpEngineCommon.matchsItemTagByName(itemContentStr, "validatorItem");

                if (validatorMatch.Count > 0)  //有validatorItem
                {
                    
                     writer.WritePropertyName("v");  //写入validator对象
                    writer.WriteObjectStart();
                    //$("#@name@")@formValidator@@inputValidator@@compareValidator@@regexValidator@@functionValidator@@ajaxValidator@@defaultPassed@;
                    string validatorItemString="@controlName@@formValidator@@inputValidator@@compareValidator@@regexValidator@@functionValidator@@ajaxValidator@@defaultPassed@;";
                    itemContentStr = itemContentStr.Replace(validatorMatch[0].Value,"");
                    Dictionary<string, XNLParam> validatorParams = RegxpEngineCommon.getXNLParams(validatorMatch[0].Groups[2].Value, XNLPage);
                    string validatorContentStr = validatorMatch[0].Groups[3].Value;
                    
                    MatchCollection formValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "formValidator"); //formValidator
                    MatchCollection inputValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "inputValidator"); //inputValidator
                    MatchCollection compareValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "compareValidator");//compareValidator
                    MatchCollection regexValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "regexValidator"); //regexValidator
                    MatchCollection ajaxValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "ajaxValidator"); //ajaxValidator
                    MatchCollection functionValidatorMatchs = RegxpEngineCommon.matchsItemTagByName(validatorContentStr, "functionValidator"); //functionValidator

                    string validatorGetParamsStr = validatorContentStr;
                    if (formValidatorMatchs.Count > 0)
                    {
                        validatorGetParamsStr = validatorGetParamsStr.Replace(formValidatorMatchs[0].Value, "");
                    }
                    if (inputValidatorMatchs.Count > 0) validatorGetParamsStr = validatorGetParamsStr.Replace(inputValidatorMatchs[0].Value, "");
                    if (compareValidatorMatchs.Count > 0) validatorGetParamsStr = validatorGetParamsStr.Replace(compareValidatorMatchs[0].Value, "");
                    if (regexValidatorMatchs.Count > 0) validatorGetParamsStr = validatorGetParamsStr.Replace(regexValidatorMatchs[0].Value, "");
                    if (ajaxValidatorMatchs.Count > 0) validatorGetParamsStr = validatorGetParamsStr.Replace(ajaxValidatorMatchs[0].Value, "");
                    if (functionValidatorMatchs.Count > 0) validatorGetParamsStr = validatorGetParamsStr.Replace(functionValidatorMatchs[0].Value, "");
                    Dictionary<string, XNLParam> validatorParams2 = RegxpEngineCommon.getXNLParams(validatorGetParamsStr, XNLPage);
                    foreach (KeyValuePair<string, XNLParam> tmpParam in validatorParams2)
                    {
                        if (validatorParams.ContainsKey(tmpParam.Key))
                        {
                            validatorParams[tmpParam.Key].value = tmpParam.Value.value;
                        }
                        else
                        {
                            validatorParams.Add(tmpParam.Key, new XNLParam(tmpParam.Value.value));
                        }
                    }

                    string formValidatorStr = "";
                    string inputValidatorStr = "";
                    string compareValidatorStr = "";
                    string regexValidatorStr = "";
                    string ajaxValidatorStr = "";
                    string functionValidatorStr = "";
                   
                    bool hasValidatorItem = false;
                    bool inputDate = false;
                    bool inputDateTime = false;
                    XNLParam defaultParam;
                    string defaultPassedStr = "";
                    if (validatorParams!=null&&validatorParams.TryGetValue("defaultpassed", out defaultParam))
                    {
                        if (string.Compare(Convert.ToString(defaultParam.value),"true",true)==0)
                        {
                            defaultPassedStr = ".defaultPassed()";
                        }
                    }
                    #region formvalidator
                    if (formValidatorMatchs.Count > 0)  //有formValidator项
                    {
                       // validatorgroup=”1”  empty=”false” automodify=”false” onempty=”为空时的提示信息” onfocus=”获得焦点时的提示信息”  oncorrect=”验证通过时的信息” tipid=”” tipcss=” ” forcevalid=”true”  defaultvalue=” ” triggerevent=”blur”
                        hasValidatorItem = true;
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(formValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(formValidatorMatchs[0].Groups[3].Value, XNLPage);
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            if (formValiParams1.ContainsKey(tmpParam.Key))
                            {
                                formValiParams1[tmpParam.Key].value = tmpParam.Value.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, new XNLParam(tmpParam.Value.value));
                            }
                        }
                        int tmpParamId = 0;
                        formValidatorStr = ".formValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            switch (tmpParam.Key)
                            {
                                case "empty":
                                    attriablesStr += speatorStr +  "empty:" +Convert.ToString( tmpParam.Value.value);
                                    break;
                                case "automodify":
                                    attriablesStr += speatorStr + "automodify:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "forcevalid":
                                    attriablesStr += speatorStr + "forcevalid:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                default :
                                    attriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                                    break;
                            }
                          
                            tmpParamId += 1;
                        }
                        formValidatorStr = formValidatorStr.Replace("@attriables", attriablesStr);

                    }
                    #endregion 
                    #region inputValidator
                    bool dateReadOnly = false;
                    if (inputValidatorMatchs.Count > 0) //有inputValidator项
                    {
                        writer.WritePropertyName("i");
                        writer.WriteObjectStart();
                        //type="size " min="1"  max="2"  onerror="" onerrormin="" onerrormax="" empty=” {leftempty:true,rightempty:true,emptyerror:null}” 
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(inputValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(inputValidatorMatchs[0].Groups[3].Value, XNLPage);

                        hasValidatorItem = true;

                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            XNLParam tmpXP;
                            if (formValiParams1.TryGetValue(tmpParam.Key, out tmpXP))
                            {
                                formValiParams1[tmpParam.Key].value = tmpXP.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, tmpXP);
                            }
                        }
                        XNLParam typeParam;
                        if (formValiParams1.TryGetValue("type", out typeParam))
                        {
                            if (Convert.ToString(typeParam.value).Equals("date"))
                            {
                                inputDate = true;
                            }
                            else if (Convert.ToString(typeParam.value).Equals("datetime"))
                            {
                                inputDateTime=true;
                            }
                        }
                        XNLParam readOnlyParam;
                        if (formValiParams1.TryGetValue("readonly", out readOnlyParam))
                        {
                            dateReadOnly = Convert.ToString(readOnlyParam.value).ToLower().Equals("true") ? true : false;
                        }
                        int tmpParamId = 0;
                        inputValidatorStr = ".inputValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            switch (tmpParam.Key)
                            {
                                case "min":
                                    attriablesStr += speatorStr + "min:" + Convert.ToString( tmpParam.Value.value);
                                    writer.WritePropertyName("min");
                                    writer.Write(Convert.ToInt32(tmpParam.Value.value));
                                    break;
                                case "max":
                                    attriablesStr += speatorStr + "max:" + Convert.ToString(tmpParam.Value.value);
                                    writer.WritePropertyName("max");
                                    writer.Write(Convert.ToInt32(tmpParam.Value.value));
                                    break;
                                case "empty":
                                    attriablesStr += speatorStr + "empty:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                default:
                                    attriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                                    writer.WritePropertyName(tmpParam.Key);
                                    writer.Write(Convert.ToString(tmpParam.Value.value));
                                    break;
                            }

                            tmpParamId += 1;
                        }
                        inputValidatorStr = inputValidatorStr.Replace("@attriables", attriablesStr);
                        writer.WriteObjectEnd();
                    }
                    #endregion 

                    #region compareValidator
                    if (compareValidatorMatchs.Count > 0) //有compareValidator项
                    {
                        //desid=”” operateor =”=” datatype =”” onerror=””
                        writer.WritePropertyName("c");
                        writer.WriteObjectStart();
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(compareValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(compareValidatorMatchs[0].Groups[3].Value, XNLPage);
                        hasValidatorItem = true;
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            XNLParam tmpXP;
                            if (formValiParams1.TryGetValue(tmpParam.Key, out tmpXP))
                            {
                                formValiParams1[tmpParam.Key].value = tmpXP.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, tmpXP);
                            }
                        }
                        int tmpParamId = 0;
                        compareValidatorStr = ".compareValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            attriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                            writer.WritePropertyName(tmpParam.Key);
                            writer.Write(Convert.ToString(tmpParam.Value.value));
                            tmpParamId += 1;
                        }
                        compareValidatorStr = compareValidatorStr.Replace("@attriables", attriablesStr);
                        writer.WriteObjectEnd();
                    }
                    #endregion

                    #region regexValidator
                    if (regexValidatorMatchs.Count > 0) //有regexValidator项
                    {
                        //regexp =”” param =”i”  datatype=””  onerror=””
                        writer.WritePropertyName("r");
                        writer.WriteObjectStart();
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(regexValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(regexValidatorMatchs[0].Groups[3].Value, XNLPage);
                        hasValidatorItem = true;
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            XNLParam tmpXP;
                            if (formValiParams1.TryGetValue(tmpParam.Key, out tmpXP))
                            {
                                formValiParams1[tmpParam.Key].value = tmpXP.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, tmpXP);
                            }
                        }
                        int tmpParamId = 0;
                        regexValidatorStr = ".regexValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            attriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                             writer.WritePropertyName(tmpParam.Key);
                             writer.Write(Convert.ToString(tmpParam.Value.value));
                            tmpParamId += 1;
                        }
                        regexValidatorStr = regexValidatorStr.Replace("@attriables", attriablesStr);
                        writer.WriteObjectEnd();
                    }
                    #endregion

                    #region ajaxValidator
                    if (ajaxValidatorMatchs.Count > 0) //有ajaxValidator项
                    {
                        //type=”Get” url =””  datatype=”” data=””  async=”true” processdata=”” success =”function(){}” complete =”function(){}” beforesend =”function (){}” buttons=”” error=”function(){}”
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(ajaxValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(ajaxValidatorMatchs[0].Groups[3].Value, XNLPage);
                        hasValidatorItem = true;
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            XNLParam tmpXP;
                            if (formValiParams1.TryGetValue(tmpParam.Key, out tmpXP))
                            {
                                formValiParams1[tmpParam.Key].value = tmpXP.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, tmpXP);
                            }
                        }
                        int tmpParamId = 0;
                        ajaxValidatorStr = ".ajaxValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            switch (tmpParam.Key)
                            {
                                case "async":
                                    attriablesStr += speatorStr + "async:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "data":
                                    string valueStr = Convert.ToString(tmpParam.Value.value);
                                    if (Regex.IsMatch(valueStr, "^{.*?}$", XNLBaseCommon.XNL_RegexOptions))
                                    {
                                        attriablesStr += speatorStr + "data:" + Convert.ToString(tmpParam.Value.value);
                                    }
                                    else
                                    {
                                        attriablesStr += speatorStr + "\"data:\"" + Convert.ToString(tmpParam.Value.value);
                                    }
                                    
                                    break;
                                case "success":
                                    attriablesStr += speatorStr + "success:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "processdata":
                                    attriablesStr += speatorStr + "processdata:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "complete":
                                    attriablesStr += speatorStr + "complete:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "error":
                                    attriablesStr += speatorStr + "error:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "beforesend":
                                    attriablesStr += speatorStr + "beforesend:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                case "buttons":
                                    attriablesStr += speatorStr + "buttons:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                default:
                                    attriablesStr += speatorStr + tmpParam.Key + ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                                    break;
                            }

                            tmpParamId += 1;
                        }
                        ajaxValidatorStr = ajaxValidatorStr.Replace("@attriables", attriablesStr);
                    }
                    #endregion
                    #region functionValidator
                    if (functionValidatorMatchs.Count > 0) //有functionValidator项
                    {
                        //fun=”外部函数名”　 onerror＝’”
                        Dictionary<string, XNLParam> formValiParams1 = RegxpEngineCommon.getXNLParams(functionValidatorMatchs[0].Groups[2].Value, XNLPage);
                        Dictionary<string, XNLParam> formValiParams2 = RegxpEngineCommon.getXNLParams(functionValidatorMatchs[0].Groups[3].Value, XNLPage);
                        hasValidatorItem = true;
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams2)
                        {
                            XNLParam tmpXP;
                            if (formValiParams1.TryGetValue(tmpParam.Key, out tmpXP))
                            {
                                formValiParams1[tmpParam.Key].value = tmpXP.value;
                            }
                            else
                            {
                                formValiParams1.Add(tmpParam.Key, tmpXP);
                            }
                        }
                        int tmpParamId = 0;
                        functionValidatorStr = ".functionValidator({@attriables})";
                        string speatorStr = "";
                        string attriablesStr = "";
                        foreach (KeyValuePair<string, XNLParam> tmpParam in formValiParams1)
                        {
                            if (tmpParamId > 0) speatorStr = ",";
                            switch (tmpParam.Key)
                            {
                                case "fun":
                                    attriablesStr += speatorStr + "fun:" + Convert.ToString(tmpParam.Value.value);
                                    break;
                                default:
                                    attriablesStr += speatorStr + tmpParam.Key+ ":\"" + Convert.ToString(tmpParam.Value.value) + "\"";
                                    break;
                            }

                            tmpParamId += 1;
                        }
                        functionValidatorStr = functionValidatorStr.Replace("@attriables", attriablesStr);
                    }
                    #endregion

                    //加入item参数 
                    if (hasValidatorItem)
                    {

                        validatorItemString = validatorItemString.Replace("@formValidator@", formValidatorStr).Replace("@inputValidator@", inputValidatorStr).Replace("@compareValidator@", compareValidatorStr).Replace("@regexValidator@", regexValidatorStr).Replace("@ajaxValidator@", ajaxValidatorStr).Replace("@functionValidator@", functionValidatorStr).Replace("@defaultPassed@", defaultPassedStr);
                        Match typeMatch = Regex.Match(itemContentStr, "input[\\s]*type=.*?(radio|checkbox)", XNLBaseCommon.XNL_RegexOptions);
                        if (typeMatch.Success)
                        {

                            validatorItemString = validatorItemString.Replace("@controlName@", "\n$(\":" + typeMatch.Groups[1].Value + "[name='" +Convert.ToString( itemParams["name"].value)+ "']\")");
                        }
                        else
                        {
                            if (inputDate||inputDateTime) //输入的是时间类型
                            {
                                string dateFmtStr = "yyyy-MM-dd";
                                StringBuilder vdateSb = new StringBuilder("\n$(\"#" + Convert.ToString(itemParams["name"].value) + "\").focus(function(){WdatePicker({dateFmt:\"|=dateFmt=|\",|=readOnly=|,oncleared:function(){$(this).blur();},onpicked:function(){$(this).blur();}})})");
                                if (inputDateTime)
                                {
                                    dateFmtStr = "yyyy-MM-dd HH:mm:ss";
                                }
                                vdateSb.Replace("|=dateFmt=|", dateFmtStr);

                                if (dateReadOnly)
                                {
                                    vdateSb.Replace("|=readOnly=|", "readOnly:true,isShowClear:false,autoPickDate:true");
                                }
                                else {
                                    vdateSb.Replace("|=readOnly=|,", "autoPickDate:true,");
                                }
                                validatorItemString = validatorItemString.Replace("@controlName@", vdateSb.ToString());
                            }
                            else
                            {
                                validatorItemString = validatorItemString.Replace("@controlName@", "\n$(\"#" +Convert.ToString( itemParams["name"].value) + "\")");
                            }
                        }
                        
                        validatorItemCount += 1;
                        string itemKeyStr="validatoritem"+validatorItemCount;
                        labelParams.Add(itemKeyStr, new XNLParam( validatorItemString));

                    }
                    writer.WriteObjectEnd(); //写validator对像结束
                }
                #endregion //加入验证信息end
               
                writer.WriteObjectEnd();  //写field对像结束
                string xnlFormHide_XNL_FORM_DEPICT="";
                if (i == 0)
                {
                    if(writeFormState)xnlFormHide_XNL_FORM_DEPICT = "<input name=\"XNL__VIEWSTATE\" type=\"hidden\" id=\"" + formName + "_XNL__VIEWSTATE\" value=\"(xnlFormHide_XNL_FORM_DEPICT)\" />";
                }
                else
                {
                    xnlFormHide_XNL_FORM_DEPICT = "";
                }

                labelContentStr = labelContentStr.Replace(item.Groups[0].Value, xnlFormHide_XNL_FORM_DEPICT + itemContentStr);
                i += 1;
            }

            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
            labelParams.Add("validatoritemcount", new XNLParam(validatorItemCount));
            return labelContentStr.Replace("(xnlFormHide_XNL_FORM_DEPICT)", EncrypString.Encrypto(EncrypString.MD5("singnocms"), sb.ToString()));
          */
        }

        #endregion


      #region IXNLTagObj<XNLContext> 成员


      public string subTagNames
      {
          get { throw new NotImplementedException(); }
      }

      #endregion

      #region IXNLTagObj<XNLContext> 成员


      public string getSubTagNames(string parentTagName)
      {
          throw new NotImplementedException();
      }

      #endregion
    }

    
}
