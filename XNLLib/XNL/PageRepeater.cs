using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using System.Text.RegularExpressions;
using COM.SingNo.DAL;
using System.Data;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using System.Data.Common;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
   public class PageRepeater:IXNLTag<WebContext>
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
            int itemCount = 0;
            int SpearStepNum = 1;
            if (!labelParams.ContainsKey("perpagerecordsnum"))
            {
                labelParams.Add("perpagerecordsnum", new XNLParam(XNLPage.curChannel.theChannelConfig.baseConfig.prePageInfoNum));
            }
           
            string pageRequestNameStr = labelParams["pagename"].value.ToString();
            if (!labelParams.ContainsKey("curpagenum"))
            {
                labelParams.Add("curpagenum", new XNLParam("1"));  //当前页
            }
            if (XNLPage.accessType == AccessType.Static)
            {
               ParseInfo parseInfo = (ParseInfo)labelParams["_parseinfo_"].value;
               if (parseInfo != null && parseInfo.pageRequestColls != null)
               {
                   if (parseInfo.pageStyle.Equals("2")) labelParams.Add("contentid", new XNLParam( XNLType.Int32, XNLPage.contentId));  //当前内容id
                   labelParams["curpagenum"].value = parseInfo.pageRequestColls[pageRequestNameStr];
               }
            }
            else
            {
                if (!(XNLPage.Context.Request[pageRequestNameStr] == null || !UtilsCode.IsInt(XNLPage.Context.Request[pageRequestNameStr])))
                {
                    if (!(XNLPage.Context.Request[pageRequestNameStr].Trim().Equals(string.Empty)) && Convert.ToInt32(XNLPage.Context.Request[pageRequestNameStr]) > 0)
                    {
                        int tmpCurPageNum = Convert.ToInt32(XNLPage.Context.Request[pageRequestNameStr]);
                        labelParams["curpagenum"].value = tmpCurPageNum;
                    }
                }
            }
            
           // Params = labelParams;  //得到所有参数
            string sqlCommandStr = XNLBaseCommon.decodeHTML(Convert.ToString(labelParams["sqlcommand"].value));
            string tmpItemStr = "";
            DbCommand dbCommand=DataHelper.GetSqlStringCommand(sqlCommandStr);
            DbConnection dbConn = DataHelper.CreateConnection();
            dbCommand.Connection = dbConn;
            dbConn.Open();
            DataTable ds = DataHelper.GetDataTableBySqlWithPage(dbCommand, labelParams);
            dbConn.Close();
           itemCount = ds.Rows.Count;
            XNLParam itemCountParam;
            if (!labelParams.TryGetValue("pagerepeateritemcount", out itemCountParam))
            {
                labelParams.Add("pagerepeateritemcount", new XNLParam(itemCount));
            }
            else
            {
                itemCountParam.value = itemCount;
            }
            string labelContent = labelContentStr;
            string labelReturnContent = labelContent;
            labelReturnContent = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelReturnContent);
            MatchCollection xnlLabelMatchsColls = RegxpEngineCommon.matchsXNLTagByName(labelReturnContent, "XNL", "pageRepeater");

            labelReturnContent = RegxpEngineCommon.disableNestedXNLTag(labelReturnContent, xnlLabelMatchsColls);

            MatchCollection alternatingItemMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterAlternatingItemTemplate");

            MatchCollection footerMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterFooterTemplate");

            MatchCollection headerMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterHeaderTemplate");

            MatchCollection itemMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterItemTemplate");

            MatchCollection separatorMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterSeparatorTemplate");

            MatchCollection emptyDataMatchs = RegxpEngineCommon.matchsItemTagByName(labelReturnContent, "pageRepeaterEmptyDataTemplate");

            if (alternatingItemMatchs.Count > 0)
            {
                AlternatingTmpStr = alternatingItemMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, AlternatingTmpStr, "");
                boolHasAlternating = true;
            }

            if (footerMatchs.Count > 0)
            {
                FooterTmpStr = footerMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, FooterTmpStr, "");
                boolHasFooter = true;
            }

            if (headerMatchs.Count > 0)
            {
                HeaderTmpStr = headerMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, HeaderTmpStr, "");
                boolHasHeader = true;
            }

            if (itemMatchs.Count > 0)
            {
                ItemTmpStr = itemMatchs[0].Groups[0].Value;
                //将匹配到的forItem替换为"<itemTemplateReplaaceString></itemTemplateReplaaceString>"
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, ItemTmpStr, "<pageRepeaterheaderTemplateReplaaceString></pageRepeaterheaderTemplateReplaaceString><pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString><pageRepeaterfooterTemplateReplaaceString></pageRepeaterfooterTemplateReplaaceString>");
                boolHasItem = true;
            }

            if (separatorMatchs.Count > 0)
            {
                SpearatorTmpStr = separatorMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, SpearatorTmpStr, "");
                boolHasSpearator = true;

                XNLParam SpearParam;
                Dictionary<string, XNLParam> SpearParams = RegxpEngineCommon.getXNLParams(separatorMatchs[0].Groups[2].Value, XNLPage);
                if (SpearParams.TryGetValue("step", out SpearParam))
                {
                    SpearStepNum = Math.Abs(Convert.ToInt32(SpearParam.value));
                }
            }

            if (emptyDataMatchs.Count > 0)
            {
                EmptyTmpStr = emptyDataMatchs[0].Groups[0].Value;
                labelReturnContent = UtilsCode.onceReplace(labelReturnContent, EmptyTmpStr, "");
                boolHasEmpty = true;
            }

            using (ds)
            {
                DataTable table = ds;
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
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "pageRepeaterItemId",Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr) + "<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>");
                            }
                        }
                        else
                        {
                            if (boolHasAlternating)
                            {
                                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(alternatingItemMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "pageRepeaterItemId", Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr) + "<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>");
                            }
                            else
                            {
                                if (boolHasItem)
                                {
                                    tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(itemMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                    tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "pageRepeaterItemId", Convert.ToString((i + 1)));
                                    labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr) + "<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>");
                                }
                            }
                        }

                        if (boolHasSpearator)
                        {
                            if ((i + 1) % SpearStepNum == 0)
                            {
                                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(separatorMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                                tmpItemStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpItemStr, "pageRepeaterItemId", Convert.ToString((i + 1)));
                                labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", RegxpEngineCommon.replaceDataBaseVariable(row, tmpItemStr) + "<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>");
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
                        labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", tmpItemStr);
                    }
                }
            };



            if (boolHasHeader)
            {
                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(headerMatchs[0].Groups[3].Value, xnlLabelMatchsColls);
                labelReturnContent = labelReturnContent.Replace("<pageRepeaterheaderTemplateReplaaceString></pageRepeaterheaderTemplateReplaaceString>", tmpItemStr);
            }


            if (boolHasFooter)
            {
                tmpItemStr = RegxpEngineCommon.enabledNestedXNLTag(footerMatchs[0].Groups[3].Value, xnlLabelMatchsColls); 
                labelReturnContent = labelReturnContent.Replace("<pageRepeaterfooterTemplateReplaaceString></pageRepeaterfooterTemplateReplaaceString>", tmpItemStr);
            }

            labelReturnContent = labelReturnContent.Replace("<pageRepeateritemTemplateReplaaceString></pageRepeateritemTemplateReplaaceString>", "");
            labelReturnContent = labelReturnContent.Replace("<pageRepeaterheaderTemplateReplaaceString></pageRepeaterheaderTemplateReplaaceString>", "");
            labelReturnContent = labelReturnContent.Replace("<pageRepeaterfooterTemplateReplaaceString></pageRepeaterfooterTemplateReplaaceString>", "");
            labelReturnContent = RegxpEngineCommon.enabledNestedXNLTag(labelReturnContent, xnlLabelMatchsColls);
            return labelReturnContent;
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
