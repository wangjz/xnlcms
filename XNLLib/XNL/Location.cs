using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using System.Text.RegularExpressions;
using COM.SingNo.XNLEngine;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class Location:IXNLTag<WebContext>
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
        //location.name,location.url,location.path,location.itemid,location.target split,rootname location.count<location.item></location.item>
       public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            /*
             MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "location.item");
             labelParams.Add("location.count", new XNLParam(1));
             string splitStr = " &gt; ";
             string rootname = "首页";
             XNLParam t_param;
             if (labelParams.TryGetValue("split",out t_param))
             {
                 splitStr = t_param.value.ToString();
             }
             if (labelParams.TryGetValue("rootname", out t_param))
             {
                 rootname = t_param.value.ToString();
             }
             string itemStr = labelContentStr;
             if (itemMatchs.Count > 0)
             {
                 itemStr = itemMatchs[0].Groups[3].Value;
             }
             if (itemStr.Trim().Equals(""))
             {
                 itemStr = "<a href=\"{@location.path}\" target=\"{@location.target}\">{@location.name}</a>";
             }
             labelParams.Add("location.name", new XNLParam());
             labelParams.Add("location.url", new XNLParam());
             labelParams.Add("location.path", new XNLParam());
             labelParams.Add("location.itemid", new XNLParam());
             labelParams.Add("location.target", new XNLParam());
             StringBuilder locationSb = new StringBuilder();
             if (XNLPage.curChannel.depth > XNLPage.curChannel.siteNode.depth)
             {
                 System.Collections.Generic.Stack<Cchannel> stack = new Stack<Cchannel>();
                 Cchannel pn = XNLPage.curChannel;
                 if (pn.theChannelConfig.baseConfig.showOnPath) stack.Push(pn);
                 for (int k = 1; k <= XNLPage.curChannel.depth - XNLPage.curChannel.siteNode.depth; k++)
                 {
                     pn = pn.parentNode;
                     if (pn.theChannelConfig.baseConfig.showOnPath)stack.Push(pn);
                 }
                 while (stack.Count > 0)
                 {
                     labelParams["location.itemid"].value = stack.Count;
                     pn = stack.Pop();
                     labelParams["location.target"].value = pn.theChannelConfig.baseConfig.openType;
                     if (!pn.isSite)
                     {
                         labelParams["location.name"].value = pn.nodeName;
                         labelParams["location.url"].value = SystemConfig.systemUrl + (pn.siteNode.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(pn.nodeID).Replace("@", pn.siteNode.siteWebPath).Replace("//", "/"));
                         labelParams["location.path"].value = (pn.siteNode.templateProjectColls[XNLPage.templateProjectId].getChannelUrl(pn.nodeID).Replace("@", pn.siteNode.siteWebPath)).Replace("//", "/");
                         locationSb.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, itemStr) + (stack.Count > 0 ? splitStr : ""));
                     }
                     else
                     {
                         labelParams["location.name"].value = rootname;
                         labelParams["location.url"].value = SystemConfig.systemUrl + (pn.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", pn.siteWebPath).Replace("//", "/"));
                         labelParams["location.path"].value = (pn.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", pn.siteWebPath).Replace("//", "/"));
                         labelParams["location.target"].value = pn.theChannelConfig.baseConfig.openType;
                         locationSb.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, itemStr) + (stack.Count > 0 ? splitStr : ""));
                     }
                  }
             }
             else
             {
                 if (XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.showOnPath)
                 {
                     labelParams["location.itemid"].value = 1;
                     labelParams["location.name"].value = rootname;
                     labelParams["location.url"].value = SystemConfig.systemUrl+(XNLPage.curChannel.siteNode.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", XNLPage.curChannel.siteNode.siteWebPath).Replace("//", "/"));
                     labelParams["location.path"].value = (XNLPage.curChannel.siteNode.templateProjectColls[XNLPage.templateProjectId].indexUrl.Replace("@", XNLPage.curChannel.siteNode.siteWebPath).Replace("//", "/"));
                     labelParams["location.target"].value = XNLPage.curChannel.siteNode.theChannelConfig.baseConfig.openType;
                     labelParams["location.name"].value = rootname;
                     locationSb.Append(RegxpEngineCommon.replaceAttribleVariable(labelParams, itemStr));
                 }
             }
             if (itemMatchs.Count > 0)
             {                
                 labelContentStr =(RegxpEngineCommon.replaceAttribleVariable(labelParams,labelContentStr.Replace(itemMatchs[0].Groups[0].Value, "[####==xnl:location item==####]")).Replace("[####==xnl:location item==####]", locationSb.ToString()));
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
