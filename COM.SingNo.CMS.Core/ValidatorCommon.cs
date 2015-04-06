using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.Text.RegularExpressions;
using System.Web;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
namespace COM.SingNo.CMS.Core
{
    
    public class ValidatorInfo
    {
        public bool isValidatorPass = true;
        public string errorMsg = "";
        //public List<object> resultList;//执行数据库操作的结果ID
        //public void addResult(object resultObj)
        //{
        //    if (resultList == null) resultList = new List<object>();
        //    resultList.Add(resultObj);
        //}
    }
 public class ValidatorCommon
 {
     /// <summary>
     /// 得到验证信息
     /// </summary>
     /// <param name="XNl_viewStateStr"></param>
     /// <returns></returns>
     public static ValidatorInfos getValidatorInfos(string XNl_viewStateStr)
     {
         ///////////////////////////////////////////////////
         JsonData xnlFromData = JsonMapper.ToObject(XNl_viewStateStr);
         int errorsCount = 0;
         Dictionary<string, string> errorMsgList = new Dictionary<string, string>();
         for (var i = 0; i < xnlFromData["fs"].Count; i++)
         {
             string name = xnlFromData["fs"][i]["n"].ToString();
             string reqString = "";
             if (HttpContext.Current.Request[name] != null) reqString = HttpContext.Current.Request[name].ToString();
             string fiedlJsonStr = xnlFromData["fs"][i].ToJson();
             // object validatorObj;
             if (fiedlJsonStr.IndexOf("\"v\":") >= 0)//有验证对象
             {
                 ValidatorInfo validatorInfoObj = testValidator(xnlFromData["fs"][i]["v"], reqString);
                 if (!validatorInfoObj.isValidatorPass)  //没有通过验证
                 {
                     errorsCount += 1;
                     errorMsgList.Add(Convert.ToString(errorsCount), validatorInfoObj.errorMsg);
                 }
             }

         }
         ValidatorInfos vInfoS = new ValidatorInfos(errorMsgList);
         /////////////////////////////////////////////////
         return vInfoS;
     }

     public static ValidatorInfos getValidatorInfos(string XNl_viewStateStr, XNLParams _Params)
     {
         ///////////////////////////////////////////////////
         JsonData xnlFromData = JsonMapper.ToObject(XNl_viewStateStr);
         //Debug.show(xnlFromData.ToJson());
         int errorsCount = 0;
         Dictionary<string, string> errorMsgList = new Dictionary<string, string>();
         if (_Params == null) _Params =new XNLParams();
         for (var i = 0; i < xnlFromData["fs"].Count; i++)
         {
             string name = xnlFromData["fs"][i]["n"].ToString().Trim().ToLower();
             string reqString = "";
             if (HttpContext.Current.Request[name] != null)
             {
                 reqString = HttpContext.Current.Request[name].ToString();
             }

             string fieldJsonStr = xnlFromData["fs"][i].ToJson();

             if (reqString.Trim().Equals(string.Empty))
             {
                 if (UtilsCode.jsonHasAttriable(fieldJsonStr, "db"))
                 {
                     reqString = xnlFromData["fs"][i]["db"].ToString();
                 }
             }
                 string encodex = xnlFromData["fs"][i]["ex"].ToString();
                 string encodeh = xnlFromData["fs"][i]["eh"].ToString();
                 string encodeReqStr = reqString;
                 if (encodex == "true") encodeReqStr = UtilsCode.encodeXNL(encodeReqStr);
                 if (encodeh == "true") encodeReqStr = HttpContext.Current.Server.HtmlEncode(encodeReqStr);
                 if (_Params.ContainsKey(name))
                 {
                     _Params[name].value = encodeReqStr;
                 }
                 else
                 {
                     _Params.Add(name, new XNLParam(encodeReqStr));
                 }
             //}
             // object validatorObj;
             if (fieldJsonStr.IndexOf("\"v\":") >= 0)//有验证对象
             {
                 ValidatorInfo validatorInfoObj = testValidator(xnlFromData["fs"][i]["v"], reqString);
                 if (!validatorInfoObj.isValidatorPass)  //没有通过验证
                 {
                     errorsCount += 1;
                     errorMsgList.Add(Convert.ToString(errorsCount), validatorInfoObj.errorMsg);
                 }
             }

         }
         ValidatorInfos vInfoS = new ValidatorInfos(_Params,errorMsgList);
         /////////////////////////////////////////////////
         return vInfoS;
     }

    public static ValidatorInfo testValidator(JsonData jData, string reqString)
        {
            ValidatorInfo vInfo = new ValidatorInfo();
            //验证代码
            string tmpValObjStr = jData.ToJson();
            if (tmpValObjStr.IndexOf("\"r\":") >= 0) //regex
            {
                string regObjStr = jData["r"].ToJson();
                if (UtilsCode.jsonHasAttriable(regObjStr, "regexp"))
                {
                    RegexOptions regOption = RegexOptions.IgnoreCase;
                    if (UtilsCode.jsonHasAttriable(regObjStr, "param"))
                    {
                        if (jData["r"]["param"].ToString().IndexOf("m") >= 0) regOption = regOption | RegexOptions.Multiline;
                    }
                    Regex reg = new Regex(jData["r"]["regexp"].ToString(), regOption);
                    if (!reg.IsMatch(reqString))
                    {
                        vInfo.isValidatorPass = false;
                        if (UtilsCode.jsonHasAttriable(regObjStr, "onerror")) //有onerror属性
                        {
                            vInfo.errorMsg = jData["r"]["onerror"].ToString();
                        }
                        else
                        {
                            vInfo.errorMsg = "输入格式不正确!";
                        }
                        return vInfo;
                    }

                }

            }

            if (tmpValObjStr.IndexOf("\"i\":") >= 0)  //input
            {
                string inputObjStr = jData["i"].ToJson();
                string type = "size";
                int min = 0;
                long max = 99999999999;
                string onerror = "输入错误!";
                string onerrormin = "";
                string onerrormax = "";

                if (UtilsCode.jsonHasAttriable(inputObjStr, "type")) type = jData["i"]["type"].ToString();
                if (UtilsCode.jsonHasAttriable(inputObjStr, "onerror")) onerror = jData["i"]["onerror"].ToString();
                if (UtilsCode.jsonHasAttriable(inputObjStr, "onerrormin")) onerror = jData["i"]["onerrormin"].ToString();
                if (UtilsCode.jsonHasAttriable(inputObjStr, "onerrormax")) onerror = jData["i"]["onerrormax"].ToString();
                if (onerrormin == "") onerrormin = onerror;
                if (onerrormax == "") onerrormax = onerror;
                switch (type)
                {
                    case "size":
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "min")) min = Convert.ToInt32(jData["i"]["min"].ToString());
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "max")) max = Convert.ToInt64(jData["i"]["max"].ToString());
                        if (reqString.Length < min || reqString.Length > max)
                        {
                            vInfo.isValidatorPass = false;
                            if (reqString.Length < min) vInfo.errorMsg = onerrormin;
                            if (reqString.Length > max) vInfo.errorMsg = onerrormax;
                            return vInfo;
                        }
                        break;
                    case "number":
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "min")) min = Convert.ToInt32(jData["i"]["min"].ToString());
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "max")) max = Convert.ToInt64(jData["i"]["max"].ToString());
                        try
                        {
                            if (Convert.ToInt32(reqString) < min || Convert.ToInt64(reqString) > max)
                            {
                                vInfo.isValidatorPass = false;
                                if (reqString.Length < min) vInfo.errorMsg = onerrormin;
                                if (reqString.Length > max) vInfo.errorMsg = onerrormax;
                                return vInfo;
                            }
                        }
                        catch
                        {
                            vInfo.isValidatorPass = false;
                            vInfo.errorMsg = onerror;
                            return vInfo;
                        }
                        break;
                    case "string":
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "min")) min = Convert.ToInt32(jData["i"]["min"].ToString());
                        if (UtilsCode.jsonHasAttriable(inputObjStr, "max")) max = Convert.ToInt64(jData["i"]["max"].ToString());
                        if (reqString.Length < min || reqString.Length > max)
                        {
                            vInfo.isValidatorPass = false;
                            if (reqString.Length < min) vInfo.errorMsg = onerrormin;
                            if (reqString.Length > max) vInfo.errorMsg = onerrormax;
                            return vInfo;
                        }
                        break;
                    case "date":
                    case "datetime":
                        break;
                    #region  需要重写
                    //try
                        //{
                        //    if (Convert.ToDateTime(reqString) < Convert.ToDateTime(min) || Convert.ToDateTime(reqString) > Convert.ToDateTime(max))
                        //    {
                        //        vInfo.isValidatorPass = false;
                        //        if (reqString.Length < min) vInfo.errorMsg = onerrormin;
                        //        if (reqString.Length > max) vInfo.errorMsg = onerrormax;
                        //        return vInfo;
                        //    }
                        //}
                        //catch
                        //{
                        //    vInfo.isValidatorPass = false;
                        //    vInfo.errorMsg = onerror;
                        //    return vInfo;
                        //}

                        //try
                        //{
                        //    if (Convert.ToDateTime(reqString) < Convert.ToDateTime(min) || Convert.ToDateTime(reqString) > Convert.ToDateTime(max))
                        //    {
                        //        vInfo.isValidatorPass = false;
                        //        if (reqString.Length < min) vInfo.errorMsg = onerrormin;
                        //        if (reqString.Length > max) vInfo.errorMsg = onerrormax;
                        //        return vInfo;
                        //    }
                        //}
                        //catch
                        //{
                        //    vInfo.isValidatorPass = false;
                        //    vInfo.errorMsg = onerror;
                        //    return vInfo;
                        //}
                    //break;
                    #endregion
                }
            }

            if (tmpValObjStr.IndexOf("\"c\":") >= 0) //compare
            {
                string compareObjStr = jData["c"].ToJson();
                string desid = "";
                string operateor = "=";
                string datatype = "string";
                string onerror = "输入错误!";
                if (UtilsCode.jsonHasAttriable(compareObjStr, "desid")) desid = jData["c"]["desid"].ToString();
                if (UtilsCode.jsonHasAttriable(compareObjStr, "operateor")) operateor = jData["c"]["operateor"].ToString();
                if (UtilsCode.jsonHasAttriable(compareObjStr, "datatype")) datatype = jData["c"]["datatype"].ToString();
                if (UtilsCode.jsonHasAttriable(compareObjStr, "onerror")) onerror = jData["c"]["onerror"].ToString();
                if (desid != "" && HttpContext.Current.Request[desid] != null)
                {
                    string desidStr = HttpContext.Current.Request[desid].ToString();
                    switch (operateor)
                    {
                        case "=":
                            if (!(desidStr.Trim().ToLower().Equals(reqString.Trim().ToLower())))
                            {
                                vInfo.isValidatorPass = false;
                                vInfo.errorMsg = onerror;
                                return vInfo;
                            }
                            break;
                        case "!=":
                            if (desidStr.Trim().ToLower().Equals(reqString.Trim().ToLower()))
                            {
                                vInfo.isValidatorPass = false;
                                vInfo.errorMsg = onerror;
                                return vInfo;
                            }
                            break;
                        case ">":
                            switch (datatype)
                            {
                                case "number":
                                    try
                                    {
                                        if (Convert.ToDouble(reqString) <= Convert.ToDouble(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "datetime":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) <= Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "data":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) <= Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                            }
                            break;
                        case ">=":
                            //
                            switch (datatype)
                            {
                                case "number":
                                    try
                                    {
                                        if (Convert.ToDouble(reqString) < Convert.ToDouble(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "datetime":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) < Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "data":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) < Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                            }
                            //
                            break;
                        case "<":
                            //
                            switch (datatype)
                            {
                                case "number":
                                    try
                                    {
                                        if (Convert.ToDouble(reqString) >= Convert.ToDouble(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "datetime":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) >= Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "data":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) >= Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                            }
                            //
                            break;
                        case "<=":
                            //
                            switch (datatype)
                            {
                                case "number":
                                    try
                                    {
                                        if (Convert.ToDouble(reqString) > Convert.ToDouble(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "datetime":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) > Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                                case "data":
                                    try
                                    {
                                        if (Convert.ToDateTime(reqString) > Convert.ToDateTime(desidStr))
                                        {
                                            vInfo.isValidatorPass = false;
                                            vInfo.errorMsg = onerror;
                                            return vInfo;
                                        }
                                    }
                                    catch
                                    {
                                        vInfo.isValidatorPass = false;
                                        vInfo.errorMsg = onerror;
                                        return vInfo;
                                    }
                                    break;
                            }
                            //
                            break;
                    }
                }
            }
            return vInfo;
        }
 }

}
