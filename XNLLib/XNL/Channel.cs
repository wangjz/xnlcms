using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLCore;
using COM.SingNo.Common;
using COM.SingNo.XNLEngine;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.XNL
{
    public class Channel:IXNLTag<WebContext>
    {
        /*
        不指定sitename默认为当前站点
         * ImageTag="true" LinkTag="true"
        <XNL:Channel  id="" Indexname="" Name="" siteName="" channelid="" parent="true">
         {@channel.path}{@channel.url}{@channel.summary}{@channel.matekeywords} {@channel.matedesc} {@channel.logo} {@channel.modelname}{@channel.modelid}{@channel.modelicon}{@channel.itemname}{@channel.itemunit}
            {@channel.Name} {@channelId} {@ChannelIndex} {@summary}  {@AddDate} {@ImageUrl}  {@channelIndexNum}  {@ChannelCount} {@allchannelcount}  {@ContentCount} {@allcontentcount} {@ImageContentCount} {@allimagecontentcount}
          {@matedesc}{@matekeywords}
        </XNL.Channel>
       */
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
        /// channel标签
        /// </summary>
        /// <param name="labelParams"></param>
        /// <param name="labelContentStr"></param>
        /// <param name="XNLPage"></param>
        /// <returns></returns>
        public string main(XNLTagStruct tagStruct, WebContext XNLPage)
        {
            /*
            XNLParam channelIndexParam=null;
            XNLParam channelNameParam=null;
            XNLParam channelIdParam = null;
            Cchannel curChannel=XNLPage.curChannel;
            //int TemplateProjectId = XNLPage.templateProjectId;
            if (labelParams.TryGetValue("id", out channelIdParam) || labelParams.TryGetValue("indexname", out channelIndexParam) || labelParams.TryGetValue("name", out channelNameParam))
            {
                XNLParam siteNameParam;
                Cchannel curSite;
                if (labelParams.TryGetValue("sitename", out siteNameParam))
                {
                    string siteName = siteNameParam.value.ToString();
                    curSite = XNLUtils.getSiteByName(siteName);
                }
                else
                {
                    curSite = XNLPage.curChannel.siteNode;
                }
                //XNLParam templateProjectParam=null;
                //if (labelParams.TryGetValue("projectname", out templateProjectParam))
                //{
                //    string projectName=templateProjectParam.theValue.ToString();
                //    TemplateProjectId = Utils.getTemplateProjectIdByName(curSite, projectName);
                //    throw (new Exception(projectName + ":模板方案未找到"));
                //}
                //else
                //{
                //    TemplateProjectId = curSite.defaultProjectId;
                //}
                if (channelIdParam!=null)
                {
                    try
                    {
                        curChannel = ChannelConfigManager.createInstance().channelDataColls[Convert.ToInt32(channelIdParam.value)];
                    }
                    catch
                    {
                        throw (new Exception("栏目已不存在"));
                    }
                }
                else if(channelIndexParam!=null)
                {
                    curChannel = XNLUtils.getChannelByIndexFromChannel(channelIndexParam.value.ToString(), curSite);
                }
                else if (channelNameParam!=null)
                {
                    curChannel = XNLUtils.getChannelByNameFromChannel(channelNameParam.value.ToString(), curSite);
                }
            }

            if(curChannel==null)
            {
                throw (new Exception("栏目已不存在"));
            }
            bool isParent = false;
            XNLParam parentParam;
            if (labelParams.TryGetValue("parent", out parentParam))
            {
                isParent=(string.Compare("true",parentParam.value.ToString(),true)==0?true:false); 
            }
            if (isParent) curChannel = curChannel.parentNode;
            if (channelIdParam == null)
            {
                labelParams.Add("channel.id", new XNLParam(curChannel.nodeID));
            }
            if (channelIndexParam == null)
            {
                labelParams.Add("channel.indexname", new XNLParam(curChannel.nodeIndexName));
            }
            if (channelNameParam == null)
            {
                labelParams.Add("channel.name", new XNLParam(curChannel.nodeName));
            }
            labelParams.Add("channel.adddate", new XNLParam(curChannel.addDate));
            labelParams.Add("channel.depth", new XNLParam(curChannel.depth));
            labelParams.Add("channel.summary", new XNLParam(curChannel.theChannelConfig.baseConfig.info));
            labelParams.Add("channel.matedesc", new XNLParam(curChannel.theChannelConfig.baseConfig.metaDesc));
            labelParams.Add("channel.matekeywords", new XNLParam(curChannel.theChannelConfig.baseConfig.metaKeyword));
            labelParams.Add("channel.modelid", new XNLParam(curChannel.model.ModelId));
            labelParams.Add("channel.modelname", new XNLParam(curChannel.model.ModelName));
            labelParams.Add("channel.modelicon", new XNLParam(curChannel.model.ItemIcon));
            labelParams.Add("channel.itemname", new XNLParam(curChannel.model.ItemName));
            labelParams.Add("channel.itemunit", new XNLParam(curChannel.model.ItemUnit));
            labelParams.Add("channel.path", new XNLParam(XNLWebCommon.getChannelVariable(curChannel,XNLPage,"path")));
            labelParams.Add("channel.url", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "url")));
            labelParams.Add("channel.dir", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "dir")));
            labelParams.Add("channel.dirpath", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "dirpath")));
            labelParams.Add("channel.dirurl", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "dirurl")));
            labelParams.Add("channel.imgurl", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "imgurl")));
            labelParams.Add("channel.imgpath", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "imgpath")));
            labelParams.Add("channel.indexnum", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "indexnum")));
            labelParams.Add("channel.childcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "channelcount")));
            labelParams.Add("channel.allchildcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "allchannelcount")));
            labelParams.Add("channel.contentcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "contentcount")));
            labelParams.Add("channel.allcontentcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "allcontentcount")));
            labelParams.Add("channel.imgcontentcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "imagecontentcount")));
            labelParams.Add("channel.allimgcontentcount", new XNLParam(XNLWebCommon.getChannelVariable(curChannel, XNLPage, "allimagecontentcount")));
            labelContentStr = RegxpEngineCommon.replaceAttribleVariable(labelParams, labelContentStr);
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
