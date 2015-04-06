using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using COM.SingNo.XNLEngine;
using System.Xml;
using System.Text.RegularExpressions;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
   public class MSystemConfig:IXNLTag<WebContext>
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
             string actionStr = labelParams["action"].value.ToString().ToLower();
             try
             {
                 switch (actionStr)
                 {
                     case "viewbase":
                         labelContentStr = setViewControlerByType(labelParams, labelContentStr, XNLPage,"base");
                         break;
                     case "viewblack":
                         labelContentStr = setViewControlerByType(labelParams, labelContentStr, XNLPage, "black");
                         break;
                     case "viewwhite":
                         labelContentStr = setViewControlerByType(labelParams, labelContentStr, XNLPage, "white");
                         break;
                     case "modifysystembase":
                         labelContentStr = setModifyControlerByType(labelParams, labelContentStr, XNLPage,"base");
                         break;
                     case "addblackip":
                         labelContentStr = addBlackIp(labelParams, labelContentStr, XNLPage);
                         break;
                     case "addwhiteip":
                         labelContentStr = addWhiteIp(labelParams, labelContentStr, XNLPage);
                         break;
                     case "delblack":
                         labelContentStr = DeleteBlack(labelParams, labelContentStr, XNLPage);
                         break;
                     case "updateblack":
                         labelContentStr = UpdateBlack(labelParams, labelContentStr, XNLPage);
                         break;
                     case "delwhite":
                         labelContentStr = DeleteWhite(labelParams, labelContentStr, XNLPage);
                         break;
                     case "updatewhite":
                         labelContentStr = UpdateWhite(labelParams, labelContentStr, XNLPage);
                         break;
                     default:
                         return "controler error";
                 }
             }
             catch (Exception e)
             {
                 return e.Message;
             }
             return labelContentStr;
            */
            return "";
        }
        #endregion
       /*
       private string addBlackIp(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string ip = labelParams["ip"].value.ToString().Trim();
                validatorIp(ip);
                addIp(ip, "black");
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch(Exception e)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", e.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
       private string addWhiteIp(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string ip=labelParams["ip"].value.ToString().Trim();
                validatorIp(ip);
                addIp(ip, "white");
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (Exception e)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", e.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
        private string setViewControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage,string type)
       {
           switch (type)
           {
               case "base":
                   labelContentStr = viewBaseConfig(labelParams, labelContentStr, XNLPage);
                   break;
               case "black":
               case "white":
                   labelContentStr = viewBlackOrWhite(labelParams, labelContentStr, XNLPage,type);
                   break;

           }
           return labelContentStr;
       }
       /// <summary>
       /// 系统基本配置的表单显示
       /// </summary>
       /// <param name="labelParams"></param>
       /// <param name="labelContentStr"></param>
       /// <param name="XNLPage"></param>
       /// <returns></returns>
        private string viewBaseConfig(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XNLPage.Context.Server.MapPath("/GlobalFiles/Config/backGroupSite.config"));
            string bgSitePath = xmlDoc.SelectSingleNode("/site/@rootPath").Value;
            string curProject = xmlDoc.SelectSingleNode("/site/@template").Value;
            labelParams.Add("foldername", new XNLParam( XNLType.String, bgSitePath));
            labelParams.Add("curproject", new XNLParam( XNLType.String, curProject));
            XmlNodeList nl = xmlDoc.SelectNodes("/site/templates/template/@name");
            string allProject = "";
            foreach (XmlNode n in nl)
            {
                allProject += (n.Value+"|");
            }
            labelParams.Add("allproject", new XNLParam( XNLType.String, allProject));
            xmlDoc.Load(XNLPage.Context.Server.MapPath("/GlobalFiles/Config/System.config"));
            string accessType = xmlDoc.SelectSingleNode("/configuration/AccessConfig/@type").Value;
            labelParams.Add("accesstype", new XNLParam( XNLType.String, accessType));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            return labelContentStr ;
        }
        private string setModifyControlerByType(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage, string type)
        {
            switch (type)
            {
                case "base":
                  labelContentStr=modifyBase(labelParams, labelContentStr, XNLPage);
                    break;
            }
            return labelContentStr;
        }
        private void validatorIp(string ip)
        {
            if(ip.Equals(""))
            {
                throw (new Exception("ip不能为空"));
            }
            if(ip.IndexOf('-')>=0)
            {
                string[] ip_array = ip.Split('-');
                if (ip_array.Length > 2)
                {
                    throw (new Exception("ip范围格式错误"));
                }
                bool isError = false;
                for(int i=0;i<ip_array.Length;i++)
                {
                    if (!Regex.IsMatch(ip_array[i], "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}"))
                    {
                        isError = true;
                        break;
                    }
                }
                if(isError)
                {
                    throw (new Exception("范围格式错误,不是正确的ip地址！"));
                }
                string[] ip1_array = ip_array[0].Split('.');
                string[] ip2_array = ip_array[1].Split('.');
                for (int i = 0; i < ip1_array.Length; i++)
                {
                    int ip1=Convert.ToInt32(ip1_array[i]);
                    int ip2=Convert.ToInt32(ip2_array[i]);
                    if (ip2 < ip1 || ip1>255||ip2>255)
                    {
                        isError = true;
                        break;
                    }
                }
                if (isError)
                {
                    throw (new Exception("范围数值错误,数值不能大于255且后面的ip地址应大于前面的ip地址！"));
                }
            }
            else if (ip.IndexOf('*') >= 0)
            {
                if(!Regex.IsMatch(ip,"(\\d{1,3}|\\*)\\.(\\d{1,3}|\\*)\\.(\\d{1,3}|\\*)\\.(\\d{1,3}|\\*)"))
                {
                    throw (new Exception("不是正确的ip地址"));
                }
                else
                {
                    string[] ip_array = ip.Split('.');
                    for (int i = 0; i < ip_array.Length; i++)
                    {
                        if (ip_array[i]!="*"&&Convert.ToInt32(ip_array[i]) > 255)
                        {
                            throw (new Exception("不是正确的ip地址"));
                        }
                    }
                }
            }
            else
            {
                if (!Regex.IsMatch(ip, "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}"))
                {
                    throw (new Exception("不是正确的ip地址"));
                }
                string[] ip_array = ip.Split('.');
                for(int i=0;i<ip_array.Length;i++)
                {
                    if (Convert.ToInt32(ip_array[i])>255)
                    {
                        throw (new Exception("不是正确的ip地址"));
                    }
                }
            }
        }
        private void addIp(string ip, string type)
        {
            switch (type)
            {
                case "white":
                    SystemConfig.accessConfig.addWhiteIp(ip);
                    break;
                case "black":
                    SystemConfig.accessConfig.addBlackIp(ip);
                    break;
            }
            SystemConfig.accessConfig.updateXMl();
        }
        private string modifyBase(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string xmlPath = XNLPage.Context.Request.PhysicalApplicationPath + "GlobalFiles\\config\\backGroupSite.config";
                xmlDoc.Load(xmlPath);
                string template = xmlDoc.SelectSingleNode("/site/@template").Value;
                string projectName = labelParams["projectname"].value.ToString();
                string AccessType = labelParams["accesstype"].value.ToString();
                AccessConfig.Type type = SystemConfig.accessConfig.accessType;
                AccessConfig.Type newType = AccessConfig.Type.none;
                switch (AccessType)
                {
                    case "Black":
                        newType = AccessConfig.Type.black;
                        break;
                    case "White":
                        newType = AccessConfig.Type.white;
                        break;
                }
                if (!type.Equals(newType))
                {
                    SystemConfig.accessConfig.setType(AccessType);
                    SystemConfig.accessConfig.updateXMl();
                }
                if (!projectName.Equals(template))
                {
                    xmlDoc.SelectSingleNode("/site/@template").Value = projectName;
                    xmlDoc.Save(xmlPath);
                    BackgroundSiteConfigManage bgSiteManage = BackgroundSiteConfigManage.loadConfig(xmlPath);
                    bgSiteManage.reSet();
                    BackgroundSiteConfigManage.loadConfig(xmlPath);
                    XNLPage.Context.Response.Write("<script type=\"text/javascript\">top.location.href=\"" + XNLPage.curChannel.siteNode.siteWebPath + "/\"</script>");
                    XNLPage.Context.Response.End();
                }
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
        private string viewBlackOrWhite(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage, string type)
        {
            string allIp = "";
            switch(type)
            {
                case "black":
                    allIp = SystemConfig.accessConfig.getAllBlackIp();
                    break;
                case "white":
                    allIp = SystemConfig.accessConfig.getAllWhiteIp();
                    break;
            }
            labelParams.Add("allip", new XNLParam( XNLType.String, allIp));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
            return labelContentStr;
        }
        private string DeleteBlack(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string ip = labelParams["ip"].value.ToString();
                string[] ip_arr = ip.Split(',');
                foreach (string str in ip_arr)
                {
                    SystemConfig.accessConfig.removeBlackIp(str);
                }
                SystemConfig.accessConfig.updateXMl();
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
        private string UpdateBlack(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string newip = labelParams["newip"].value.ToString();
                validatorIp(newip);
                string ip = labelParams["ip"].value.ToString();
                SystemConfig.accessConfig.removeBlackIp(ip);
                SystemConfig.accessConfig.addBlackIp(newip);
                SystemConfig.accessConfig.updateXMl();
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
        private string DeleteWhite(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string ip = labelParams["ip"].value.ToString();
                string[] ip_arr = ip.Split(',');
                foreach (string str in ip_arr)
                {
                    SystemConfig.accessConfig.removeWhiteIp(str);
                }
                SystemConfig.accessConfig.updateXMl();
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
        }
        private string UpdateWhite(Dictionary<string, XNLParam> labelParams, string labelContentStr, WebContext XNLPage)
        {
            MatchCollection matchSuccessItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Success");
            MatchCollection matchFailItem = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "MSystemConfig.Error");
            try
            {
                string newip = labelParams["newip"].value.ToString();
                validatorIp(newip);
                string ip = labelParams["ip"].value.ToString();
                SystemConfig.accessConfig.removeWhiteIp(ip);
                SystemConfig.accessConfig.addWhiteIp(newip);
                SystemConfig.accessConfig.updateXMl();
                return XNLWebCommon.setValidatorSuccessItem(labelContentStr, matchSuccessItem, matchFailItem);
            }
            catch (System.Exception ex)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>(1);
                errorList.Add("1", ex.Message);
                return XNLWebCommon.setValidatorErrorItem(labelContentStr, matchSuccessItem, matchFailItem, new ValidatorInfos(errorList));
            }
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
