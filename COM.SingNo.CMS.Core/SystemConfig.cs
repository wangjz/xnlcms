using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Drawing;
using COM.SingNo.Common;
using COM.SingNo.XNLCore;
using COM.SingNo.XNLEngine;
namespace COM.SingNo.CMS.Core
{
  public static  class SystemConfig
    {
      private static bool _Installer=true;
      public static string systemDir;
      public static string systemUrl;
      public static string systemHost;
      public static bool Installer
      {
          set
          {
              if (value == true && _Installer == false)
              {
                  string xmlPath = HttpContext.Current.Server.MapPath("~/GlobalFiles/Config/System.config");
                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.Load(xmlPath);
                  xmlDoc.SelectSingleNode("/configuration/appSettings/add[@key='Installer']").Attributes["value"].Value = "True";
                  xmlDoc.Save(xmlPath);
              }
          }
          get
          {
              return _Installer;
          }
      }
      private static XmlNodeList _systemXMLNodes;
      private static XmlNodeList _siteXMLNodes;
      private static XmlNodeList _channelXMLNodes;
      private static VerifyCode.StringMode _verifyCode_StringMode = VerifyCode.StringMode.Mix;
      private static int _verifyCode_Length=4;
      private static Color _verifyCode_BgColor=Color.White;
      private static Color _verifyCode_FontColor=Color.Red;
      private static int _verifyCode_FontSize=12;
      private static int _singleCacheCount = 50; //针对站点,栏目缓存
      private static AccessConfig _accessConfig;
      public static AccessConfig accessConfig
      {
          get
          {
              return _accessConfig;
          }
      }
      /// <summary>
      /// 系统权限的XmlNodeList对象
      /// </summary>
      public static XmlNodeList systemXMLNodes 
      {
          get
          {
              return _systemXMLNodes;
          }
      }
      /// <summary>
      /// 站点权限的XmlNodeList对象
      /// </summary>
      public static XmlNodeList siteXMLNodes
      {
          get
          {
              return _siteXMLNodes;
          }
      }
      /// <summary>
      /// 栏目权限的XmlNodeList对象
      /// </summary>
      public static XmlNodeList channelXMLNodes
      {
          get
          {
              return _channelXMLNodes;
          }
      }
      public static XmlNodeList otherXMLNodes;
      public static XmlNodeList pluginsXMLNodes;
      
      public static VerifyCode.StringMode verifyCode_StringMode
      {
          get
          {
              return _verifyCode_StringMode;
          }
      }

      public static int verifyCode_Length
      {
          get
          {
              return _verifyCode_Length;
          }
      }

      public static Color verifyCode_BgColor
      {
          get
          {
              return _verifyCode_BgColor;
          }
      }

      public static Color verifyCode_FontColor
      {
          get
          {
              return _verifyCode_FontColor;
          }
      }

      public static int verifyCode_FontSize
      {
          get
          {
              return _verifyCode_FontSize;
          }
      }

      public static int singleCacheCount
      {
          get { return _singleCacheCount; }
      }
      //初始化
      public static void initialize()
      {
          ParserEngine<WebContext>.xnlParser = RegexParser<WebContext>.getInstance();
          XNLLib<WebContext>.initialize(new List<XNLLib<WebContext>> { new XNLLib<WebContext>("xnl", true, new string[] { "page" }, new Dictionary<string, string> { { "channel|content|site|system|page", "xnl:extexp" } }) });
          RegxpEngineCommon <WebContext>.updateXNLConfig();
          try
          {
              Uri uri = HttpContext.Current.Request.Url;
              systemUrl = uri.AbsoluteUri.Replace(uri.PathAndQuery, "");
              systemDir = HttpContext.Current.Server.MapPath("~/");
              systemHost = uri.Host;
              XmlDocument sysXmlDoc = new XmlDocument();
              sysXmlDoc.Load(HttpContext.Current.Server.MapPath("~/GlobalFiles/Config/System.config"));
              string stringMode = sysXmlDoc.SelectSingleNode("/configuration/CAPTCHAConfig/StringMode").InnerText;
              switch (stringMode)
              {
                  case "LowerLetter":
                      _verifyCode_StringMode = VerifyCode.StringMode.LowerLetter;
                      break;
                  case "UpperLetter":
                      _verifyCode_StringMode = VerifyCode.StringMode.UpperLetter;
                      break;
                  case "Letter":
                      _verifyCode_StringMode = VerifyCode.StringMode.Letter;
                      break;
                  case "Digital":
                      _verifyCode_StringMode = VerifyCode.StringMode.Digital;
                      break;
                  case "All":
                      _verifyCode_StringMode = VerifyCode.StringMode.Mix;
                      break;

              }
              _verifyCode_Length = Convert.ToInt32(sysXmlDoc.SelectSingleNode("/configuration/CAPTCHAConfig/Length").InnerText);
              _verifyCode_FontSize = Convert.ToInt32(sysXmlDoc.SelectSingleNode("/configuration/CAPTCHAConfig/FontSize").InnerText);
              _verifyCode_BgColor = getColorByStr(sysXmlDoc.SelectSingleNode("/configuration/CAPTCHAConfig/BackgroundColor").InnerText, _verifyCode_BgColor);
              _verifyCode_FontColor = getColorByStr(sysXmlDoc.SelectSingleNode("/configuration/CAPTCHAConfig/FontColor").InnerText, _verifyCode_FontColor);
              _singleCacheCount = Convert.ToInt32(sysXmlDoc.SelectSingleNode("/configuration/appSettings/add[@key='SingleCacheCount']/@value").Value);
              _Installer = sysXmlDoc.SelectSingleNode("/configuration/appSettings/add[@key='Installer']/@value").Value.ToLower().Equals("true");
              Installer = _Installer;
              _accessConfig = new AccessConfig();
              _accessConfig.init(sysXmlDoc.SelectSingleNode("/configuration/AccessConfig"));
          }
          catch(Exception e)
          {
              //HttpContext.Current.Response.Write(e.Message);
              throw (new Exception("初始化失败，请检查System.config文件。</br>错误信息:"+e.Message));

          }
          try
          {
              XmlDocument xmlDoc = new XmlDocument();
              xmlDoc.Load(HttpContext.Current.Server.MapPath("~/GlobalFiles/Config/Rights.config"));
              _systemXMLNodes = xmlDoc.SelectNodes("/configuration/system//action");
              _siteXMLNodes = xmlDoc.SelectNodes("/configuration/site//action");
              _channelXMLNodes = xmlDoc.SelectNodes("/configuration/channel//action");
          }
          catch (Exception e)
          {
              throw (new Exception("初始化失败，请检查Rights.config文件。</br>错误信息:"+e.Message));
          }
         
          System.Text.RegularExpressions.Regex.CacheSize = 100;
          try
          {
              ChannelConfigManager.createInstance();
          }
          catch (Exception e)
          {
              throw (new Exception("站点栏目信息初始化失败。</br>错误信息:" + e.Message));
          }
      }

      private static Color getColorByStr(string colorStr,Color defaultColor)
      {
          switch (colorStr)
          {
              case "White":
                  return Color.White;
              case "Blue":
                  return Color.Blue;
              case "Red":
                  return Color.Red;
              case "Black":
                  return Color.Black;
              case "Brown":
                  return Color.Brown;
              case "Cyan":
                  return Color.Cyan;
              case "Gold":
                  return Color.Gold;
              case "Gray":
                  return Color.Gray;
              case "Green":
                  return Color.Green;
              case "Yellow":
                  return Color.Yellow;
          }
          return defaultColor;
      }
    }
    /// <summary>
    /// 后台访问配置
    /// </summary>
    public class AccessConfig
    {
        public enum Type{none,black,white}
        public Type accessType { get; set; }
        public List<string> blackList{get;set;}
        public List<string> whiteList { get; set; }
        public AccessConfig()
        {
            accessType = Type.none;
        }
        public void addBlackIp(string ip)
        {
            if (ip.Trim().Equals("")) return;
            if (blackList == null) blackList = new List<string>();
            if (blackList.Contains(ip)) return;
            blackList.Add(ip);
        }
        public void addWhiteIp(string ip)
        {
            if (ip.Trim().Equals("")) return;
            if (whiteList == null) whiteList = new List<string>();
            if (whiteList.Contains(ip)) return;
            whiteList.Add(ip);
        }
        public void clearBlackIp()
        {
            if (blackList == null) return;
            blackList.Clear();
        }
        public void clearWhiteIp()
        {
            if (whiteList == null) return;
            whiteList.Clear();
        }
        public void setType(string type)
        {
            switch (type)
            {
                case "None":
                    accessType = Type.none;
                    break;
                case "Black":
                    accessType = Type.black;
                    break;
                case "White":
                    accessType = Type.white;
                    break;
            }
        }
        public void removeBlackIp(string ip)
       {
           if (ip.Trim().Equals("")) return;
           if (blackList == null) return;
           if(blackList.Contains(ip)) blackList.Remove(ip);
       }
        public void removeWhiteIp(string ip)
        {
            if (ip.Trim().Equals("")) return;
            if (whiteList == null) return;
            if (whiteList.Contains(ip)) whiteList.Remove(ip);
        }
        /// <summary>
        /// 更新xml
        /// </summary>
        public void updateXMl()
        {
            string xmlPath = HttpContext.Current.Server.MapPath("/GlobalFiles/Config/System.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode typeNode=xmlDoc.SelectSingleNode("/configuration/AccessConfig/@type");
            switch(accessType)
            {
                case Type.none:
                    typeNode.Value = "None";
                    break;
                case Type.white:
                    typeNode.Value = "White";
                    break;
                case Type.black:
                    typeNode.Value = "Black";
                    break;
            } 
            //更新黑名单
            if(blackList!=null)
            {
                xmlDoc.SelectSingleNode("/configuration/AccessConfig/Black").InnerText = getAllBlackIp();
            }
            //更新白名单
            if (whiteList != null)
            {
                xmlDoc.SelectSingleNode("/configuration/AccessConfig/White").InnerText = getAllWhiteIp();
            }
            xmlDoc.Save(xmlPath);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void init(XmlNode node)
        {
            setType(node.Attributes["type"].Value);
            string[] _arr = node.SelectSingleNode("Black").InnerText.Split(new char[]{'|'},StringSplitOptions.RemoveEmptyEntries);
            for(int i=0;i<_arr.Length;i++)
            {
                addBlackIp(_arr[i]);
            }
            _arr = node.SelectSingleNode("White").InnerText.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < _arr.Length; i++)
            {
                addWhiteIp(_arr[i]);
            }
        }
        public string getAllBlackIp()
        {
            if (blackList == null) return "";
            StringBuilder sb = new StringBuilder();
            foreach (string str in blackList)
            {
                sb.Append(str + "|");
            }
            if (sb.Length == 0) return "";
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public string getAllWhiteIp()
        {
            if (whiteList == null) return "";
            StringBuilder sb = new StringBuilder();
            foreach (string str in whiteList)
            {
                sb.Append(str + "|");
            }
            if (sb.Length == 0) return "";
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
