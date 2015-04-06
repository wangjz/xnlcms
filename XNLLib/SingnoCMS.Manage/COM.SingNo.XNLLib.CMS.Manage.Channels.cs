using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.XNLEngine;
using COM.SingNo.DAL;
using System.Data;
using System.Text.RegularExpressions;
using COM.SingNo.XNLCore;
using COM.SingNo.CMS.Core;
namespace COM.SingNo.XNLLib.CMS.Manage
{
  public  class Channels:IXNLTag<WebContext>
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
            return "";
          /*
            string depthTag = labelParams["depthtag"].value.ToString();
            MatchCollection LabelitemColls = RegxpEngineCommon.matchsXNLTagByName(labelContentStr, "Manage", "channels");
            labelContentStr = RegxpEngineCommon.disableNestedXNLTag(labelContentStr, LabelitemColls);
            MatchCollection itemColl = RegxpEngineCommon.matchsItemTagByName(labelContentStr, "channelsItem");
            string itemStr = itemColl[0].Groups[3].Value;
            itemStr=RegxpEngineCommon.enabledNestedXNLTag(itemStr,LabelitemColls);
            int minDepth = 1;
            DataTable dt = DataHelper.ExecuteDataTable(labelParams["sqlcommand"].value.ToString(), labelParams);
            if (dt.Rows.Count == 0) return "";
            minDepth =(int) dt.Compute("min(depth)", "");
            Dictionary<int, Cchannels> channelsList = new Dictionary<int, Cchannels>();
            string returnStr = "";
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["depth"]) == minDepth)
                {
                    returnStr += setChannel(row,dt, minDepth, depthTag, itemStr);
                }
            }
            return returnStr;
          */
        }
      /*
        public string setChannel(DataRow row, DataTable dt, int minDepth, string depthTag, string itemStr)
        {
            string tmpStr = RegxpEngineCommon.replaceDataBaseVariable(row, itemStr);
            string curDepth = row["depth"].ToString();
            string tmpDepthTag = "";
            for (int i = 0; i < Convert.ToInt32(curDepth) - minDepth; i++)
            {
                tmpDepthTag += depthTag;
            }
            tmpStr = RegxpEngineCommon.replaceAttribleVariableByName(tmpStr, "depthtag", tmpDepthTag);
            if (Convert.ToInt32(row["ChildsNum"]) == 0)
            {
                return tmpStr;
            }
            string[] splitStr = { "," };  //分隔字符默认为","
            string[] channelId_array = row["arrChildID"].ToString().Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < channelId_array.Length; i++)
            {
                foreach (DataRow row2 in dt.Rows)
                {
                    if (row2["nodeid"].ToString().Equals(channelId_array[i]))
                    {
                        tmpStr += setChannel(row2, dt, minDepth, depthTag, itemStr);
                    }
                }
            }

            return tmpStr;
        }
       */
      /* 
      public Cchannels setChannel(DataRow row, DataTable dt)
        {
            Cchannels chanlelObj = new Cchannels();
            chanlelObj.channelRow = row;
            if (Convert.ToInt32(row["ChildsNum"]) == 0)
            {
                
                return chanlelObj;
            }
            string[] splitStr = { "," };  //分隔字符默认为","
            string[] channelId_array = row["arrChildID"].ToString().Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            foreach (DataRow row2 in dt.Rows)
            {
                for (int i = 0; i < channelId_array.Length; i++)
                {
                    if(row2["nodeid"].ToString().Equals(channelId_array[i]))
                    {
                        Cchannels subChannelObj = setChannel(row2, dt);
                        chanlelObj.addSubChannel(subChannelObj);
                        //if (subChannelObj != null)chanlelObj.addSubChannel(subChannelObj);
                    }
                }
            }
            return chanlelObj;
        }

        public string setChannelToStr( Cchannels channel,int minDepth, string depthTag, string setDepth, string itemStr)
        {
            string tmpStr = "";
            tmpStr = XNLCommon.replaceDataBaseVariable(channel.channelRow, itemStr);
            string curDepth = channel.channelRow["depth"].ToString();
            string tmpDepthTag = "";
            for (int i = 0; i < Convert.ToInt32(curDepth) - minDepth; i++)
            {
                tmpDepthTag += setDepth;
            }
            tmpDepthTag += depthTag;
            tmpStr = XNLCommon.replaceAttribleVariableByName(tmpStr, "depthtag", tmpDepthTag);
            foreach (KeyValuePair<string, Cchannels> subchannel in channel.subChannelsList)
            {
                tmpStr += setChannelToStr(subchannel.Value, minDepth, depthTag, setDepth, itemStr);
            }
            return tmpStr;
        }
       */
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

  internal class Cchannels
  {
      private DataRow _channelRow; //包含 channels数据行
      private Dictionary<string, Cchannels> _subChannelsList;
      public Cchannels()
      {
          _subChannelsList = new Dictionary<string, Cchannels>();
      }

      public DataRow channelRow
      {
          get { return _channelRow; }
          set { _channelRow = value; }
      }

      public Dictionary<string, Cchannels> subChannelsList
      {
          get { return _subChannelsList; }
      }

      public int subChannelsNum
      {
          get { return _subChannelsList.Count; }
      }

      public void addSubChannel(Cchannels subChannel)
      {
          _subChannelsList.Add(_subChannelsList.Count.ToString(), subChannel);
      }
  }
}
