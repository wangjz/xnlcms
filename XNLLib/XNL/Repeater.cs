using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.DAL;
using System.Data;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
  public  class Repeater:IXNLTag<WebContext>
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
        /// <summary>
        ///
        /// <XNL:repeater selectCommand="">
       /// <alternatingItemTemplate></alternatingItemTemplate>
       /// <footerTemplate></footerTemplate>
      ///  <headerTemplate></headerTemplate>
      ///  <itemTemplate></itemTemplate>
      ///  <separatorTemplate></separatorTemplate>
   /// </XNL:repeater>

        /// </summary>
        /// <param name="LabelStr"></param>
        /// <returns></returns>
      public string main(XNLTagStruct tagStruct,WebContext XNLPage)
        {
            return "";
          /*
            bool boolHasItem = false;
            bool boolHasHeader = false;
            bool boolHasFooter = false;
            bool boolHasSpearator = false;
            bool boolHasAlternating = false;
            bool boolHasEmpty = false;
            string ItemTmpStr = "";
            string HeaderTmpStr = "";
            string FooterTmpStr = "";
            string SpearatorTmpStr = "";
            string AlternatingTmpStr = "";
            string EmptyTmpStr = "";
            int SpearStepNum = 1;
            string sqlCommandStr = XNLBaseCommon.decodeHTML(Convert.ToString(labelParams["sqlcommand"].value));
            if(XNLPage.contentId!=0) labelParams.Add("contentid", new XNLParam( XNLType.Int32, XNLPage.contentId));  //当前内容id
            int itemCount = 0;
            DataTable table = DataHelper.ExecuteDataTable(sqlCommandStr, labelParams);
            itemCount = table.Rows.Count;
            XNLParam itemCountParam;
            if (!labelParams.TryGetValue("repeateritemcount", out itemCountParam))
            {
                labelParams.Add("repeateritemcount", new XNLParam(itemCount));
            }
            else
            {
                itemCountParam.value = itemCount;
            }

            string labelContent = labelContentStr;

            string labelReturnContent = labelContent;

            labelReturnContent = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelReturnContent);

            MatchCollection xnlLabelMatchsColls = RegxpEngineCommon.matchsXNLTagByName(labelReturnContent, "XNL", "repeater");

            labelReturnContent = RegxpEngineCommon.disableNestedXNLTag(labelReturnContent, xnlLabelMatchsColls);

            MatchCollection alternatingItemMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "alternatingItemTemplate");

            MatchCollection footerMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "footerTemplate");

            MatchCollection headerMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "headerTemplate");

            MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "itemTemplate");

            MatchCollection separatorMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "separatorTemplate");

            MatchCollection emptyDataMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "emptyDataTemplate");

            if (alternatingItemMatchs.Count > 0)
            {

                AlternatingTmpStr = alternatingItemMatchs[0].Groups[0].Value;

                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, AlternatingTmpStr, "");
                boolHasAlternating = true;
            }

            if (footerMatchs.Count > 0)
            {

                FooterTmpStr=footerMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, FooterTmpStr, "");
                boolHasFooter = true;
            }

            if (headerMatchs.Count > 0)
            {
                HeaderTmpStr= headerMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, HeaderTmpStr, "");
                boolHasHeader = true;
            }

            if (itemMatchs.Count > 0)
            {
                ItemTmpStr=itemMatchs[0].Groups[0].Value;
               //将匹配到的forItem替换为"<itemTemplateReplaaceString></itemTemplateReplaaceString>"
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, ItemTmpStr, "<headerTemplateReplaaceString></headerTemplateReplaaceString><itemTemplateReplaaceString></itemTemplateReplaaceString><footerTemplateReplaaceString></footerTemplateReplaaceString>");
                boolHasItem = true;
            }

            if (separatorMatchs.Count > 0)
            {

                SpearatorTmpStr=separatorMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, SpearatorTmpStr, "");
                boolHasSpearator = true;

                XNLParam SpearParam;
                Dictionary<string, XNLParam> SpearParams = RegxpEngineCommon.getXNLParams(separatorMatchs[0].Groups[2].Value, XNLPage);
                if (SpearParams.TryGetValue("step", out SpearParam))
                {
                    SpearStepNum =Math.Abs(Convert.ToInt32(SpearParam.value));
                }
            }

            if (emptyDataMatchs.Count > 0)
            {
                EmptyTmpStr=emptyDataMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, EmptyTmpStr, "");
                boolHasEmpty = true;
            }
            string tmpItemStr = "";
            using (table)
            {
                if (table.Rows.Count > 0)
                {
                    
                    int i = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        if (i % 2 == 0)
                        {
                            if (boolHasItem)
                            {
                                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(itemMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "repeaterItemId", Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr )+ "<itemTemplateReplaaceString></itemTemplateReplaaceString>");
                            }
                        }
                        else
                        {
                            if (boolHasAlternating)
                            {
                                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(alternatingItemMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "repeaterItemId", Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row,  tmpItemStr) + "<itemTemplateReplaaceString></itemTemplateReplaaceString>");
                            }
                            else
                            {
                                if (boolHasItem)
                                {
                                    tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(itemMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                    tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "repeaterItemId", Convert.ToString((i + 1)));
                                    labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row,  tmpItemStr) + "<itemTemplateReplaaceString></itemTemplateReplaaceString>");
                                }
                            }
                        }

                        if (boolHasSpearator)
                        {
                            if ((i + 1) % SpearStepNum == 0)
                            {
                                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(separatorMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "repeaterItemId", Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr) + "<itemTemplateReplaaceString></itemTemplateReplaaceString>");
                            }
                        }
                        i += 1;
                    }
                }
                else
                {
                    if (boolHasEmpty)
                    {
                        tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(emptyDataMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                        labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>",  tmpItemStr);
                    }
                }
            };



            if (boolHasHeader)
            {
                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(headerMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                labelReturnContent = labelReturnContent.Replace("<headerTemplateReplaaceString></headerTemplateReplaaceString>",  tmpItemStr);
            }


            if (boolHasFooter)
            {
                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(footerMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                labelReturnContent = labelReturnContent.Replace("<footerTemplateReplaaceString></footerTemplateReplaaceString>",  tmpItemStr);
            }
            
            labelReturnContent = labelReturnContent.Replace("<itemTemplateReplaaceString></itemTemplateReplaaceString>", "");
            labelReturnContent = labelReturnContent.Replace("<headerTemplateReplaaceString></headerTemplateReplaaceString>", "");
            labelReturnContent = labelReturnContent.Replace("<footerTemplateReplaaceString></footerTemplateReplaaceString>", "");

            labelReturnContent = RegxpEngineCommon.enabledNestedXNLTag(labelReturnContent, xnlLabelMatchsColls);   
           
            return labelReturnContent;*/ 
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
