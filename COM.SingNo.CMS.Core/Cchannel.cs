using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using COM.SingNo.Common;
using COM.SingNo.XNLCore;
namespace COM.SingNo.CMS.Core
{
    public class Cchannel  //栏目类
    {
        private object subChannelSyncRoot = new Object();
        private object filedsListSyncRoot = new Object();
        private object templateProjectSyncRoot = new Object();
        private object templateSyncRoot = new Object();
        private int _nodeID;//节点编号
        private int _siteID;//站点编号
        private string _nodeName;//节点名称
        private string _nodeIndexName;//节点索引名称,相当于文件夹
        private string _addDate;//添加日期
        private bool _isSite;//是否站点
        private string _siteWebPath;//站点路径 ,虚拟路径
        private Cchannel _parentNode;//父节点
        private SafeDictionary<int, Cchannel> _subNodeColls;//子节点集合
        private int _state;//状态 ，0为正常，1为放入回收站，2为删除
        private Cchannel _siteNode;//所属站点节点
        private siteConfig _theSiteConfig;  //站点配置
        public int depth;
        public string imageUrl { get; set;}
       /// <summary>
       /// 栏目添加日期
       /// </summary>
        public string addDate
        {
            get
            {
                return _addDate;
            }
            set
            {
                _addDate = value;
            }
        }
        /// <summary>
        /// 节点模型
        /// </summary>
        public DataModel model { get; set; }
        /// <summary>
        /// 栏目配置
        /// </summary>
        public ChannelConfig theChannelConfig;//栏目配置       
        /// <summary>
        /// 节点编号
        /// </summary>
        public int nodeID
        {
            get
            {
                return _nodeID;
            }
            set
            {
                _nodeID = value;
            }
        }
        /// <summary>
        /// 站点编号
        /// </summary>
        public int siteID
        {
            get
            {
                if (!_isSite)
                {
                    return siteNode.siteID;
                }
                return _siteID;
            }
            set
            {
                _siteID = value;
            }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string nodeName
        {
            get
            {
                if (_nodeName == null) return "";
                return _nodeName;
            }
            set
            {
                _nodeName = value;
            }
        }
        /// <summary>
        /// 节点索引名称,相当于文件夹
        /// </summary>
        public string nodeIndexName
        {
            get
            {
                if (_nodeIndexName == null) return "";
                return _nodeIndexName;
            }
            set
            {
                _nodeIndexName = value;
            }
        }
        /// <summary>
        /// 是否站点
        /// </summary>
        public bool isSite
        {
            get
            {
                return _isSite;
            }
            set
            {
                _isSite = value;
            }
        }
        /// <summary>
        /// 节点虚拟路径，如是站点表示站点路径
        /// </summary>
        public string siteWebPath
        {
            get
            {
                if (_isSite)
                {
                    if (_siteWebPath == null) return "";
                    return _siteWebPath;
                }
                else
                {
                    return siteNode.siteWebPath;
                }

            }
            set
            {
                if (_isSite)
                {
                    _siteWebPath = value;
                }
                else
                {
                    siteNode.siteWebPath = value;
                }
            }
        }
        /// <summary>
        /// 父节点
        /// </summary>
        public Cchannel parentNode
        {
            get
            {
                if (_parentNode == null)
                {
                    return this;
                }
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }
        /// <summary>
        /// 子节点集合
        /// </summary>
        public SafeDictionary<int, Cchannel> subNodeColls
        {
            get
            {
                return _subNodeColls;
            }
        }
        /// <summary>
        /// 状态 ，0为正常，1为放入回收站，2为删除
        /// </summary>
        public int state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        /// <summary>
        /// 所属站点节点
        /// </summary>
        public Cchannel siteNode
        {
            get
            {
                if (_siteNode == null) return this;
                if (_isSite) return this;
                return _siteNode;
            }
            set
            {
                _siteNode = value;
            }
        }
        /// <summary>
        /// 站点配置
        /// </summary>
        public siteConfig theSiteConfig
        {
            get
            {
                if (!_isSite) return siteNode.theSiteConfig;
                return _theSiteConfig;
            }
            set
            {
                if (!_isSite)
                {
                    siteNode.theSiteConfig = value;
                }
                else
                {
                    _theSiteConfig = value;
                }
            }
        }
        private int _defaultProjectId;
        public int defaultProjectId
        {
            get
            {
                if (!_isSite) return siteNode.defaultProjectId;
                return _defaultProjectId;
            }
            set
            {
                _defaultProjectId = value;
            }
        }
        private SinglePageCreateThreadInfo _indexPageCreateThreadInfo;
        /// <summary>
        /// 首页生成线程的信息
        /// </summary>
        public SinglePageCreateThreadInfo indexPageCreateThreadInfo
        {
            get
            {
                if (!_isSite) return this.siteNode.indexPageCreateThreadInfo;
                return _indexPageCreateThreadInfo;
            }
            set
            {
                if (!_isSite) this.siteNode.indexPageCreateThreadInfo = value;
                _indexPageCreateThreadInfo = value;
            }
        }

        private SinglePageCreateThreadInfo _channelPageCreateThreadInfo;
        /// <summary>
        /// 栏目页生成线程的信息
        /// </summary>
        public SinglePageCreateThreadInfo channelPageCreateThreadInfo
        {
            get
            {
                if (!_isSite) return this.siteNode._channelPageCreateThreadInfo;
                return _channelPageCreateThreadInfo;
            }
            set
            {
                if (!_isSite) this.siteNode.channelPageCreateThreadInfo = value;
                _channelPageCreateThreadInfo = value;
            }
        }
        /// <summary>
        /// 内容页生成线程的信息
        /// </summary>
        public ContentPageCreateThreadInfo contentPageCreateThreadInfo
        {
            get
            {
                if (!_isSite) return this.siteNode.contentPageCreateThreadInfo;
                return _contentPageCreateThreadInfo;
            }
            set
            {
                if (!_isSite) this.siteNode.contentPageCreateThreadInfo = value;
                _contentPageCreateThreadInfo = value;
            }
        }
        private ContentPageCreateThreadInfo _contentPageCreateThreadInfo;
        private SinglePageCreateThreadInfo _singlePageCreateThreadInfo;
        /// <summary>
        /// 单页生成线程的信息
        /// </summary>
        public SinglePageCreateThreadInfo singlePageCreateThreadInfo
        {
            get
            {
                if (!_isSite) return this.siteNode.singlePageCreateThreadInfo;
                return _singlePageCreateThreadInfo;
            }
            set
            {
                if (!_isSite) this.siteNode.singlePageCreateThreadInfo = value;
                _singlePageCreateThreadInfo = value;
            }
        }     
        private SafeDictionary<int, TemplateProject> _templateProjectColls;
        /// <summary>
        /// 站点模板方案集合
        /// </summary>
        public SafeDictionary<int, TemplateProject> templateProjectColls
        {
            get
            {
                if (!_isSite) return siteNode.templateProjectColls;
                return _templateProjectColls;
            }
        }
        /// <summary>
        /// 添加模板方案
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="projectName"></param>
        public void addTemplateProject(int projectID, TemplateProject templateProject)
        {
            if (!_isSite)
            {
                siteNode.addTemplateProject(projectID, templateProject);
            }
            else
            {
                lock (templateProjectSyncRoot)
                {
                    if (_templateProjectColls == null) _templateProjectColls = new SafeDictionary<int, TemplateProject>();
                    if (_templateProjectColls.ContainsKey(projectID))
                    {
                        _templateProjectColls[projectID] = templateProject;
                    }
                    else
                    {
                        _templateProjectColls.Add(projectID, templateProject);
                    }
                }
            }
        }
        public Cchannel()
        {
            _isSite = false;
            _state = 0;
            imageUrl =string.Empty;
            theChannelConfig = new ChannelConfig();
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="nodeID">节点编号</param>
        /// <param name="subNode">节点对象</param>
        public void addSubNode(int nodeID, Cchannel subNode)
        {
            lock (subChannelSyncRoot)
            {
                if (_subNodeColls == null)
                {
                    _subNodeColls = _subNodeColls = new SafeDictionary<int, Cchannel>();
                }
                if (_subNodeColls.ContainsKey(nodeID))
                {
                    _subNodeColls[nodeID] = subNode;
                }
                else
                {
                    _subNodeColls.Add(nodeID, subNode);
                }
            }
        }
    }
        /// <summary>
        /// 栏目配置类
        /// </summary>
        public class ChannelConfig  //栏目配置类
        {
            private ChannelBaseConfig _baseConfig;
            private ChannelCommentsConfig _commentsConfig;
            private ContributeConfig _contributeConfig;
            private ChannelCreateConfig _createConfig;
            /// <summary>
            /// 栏目基本配置
            /// </summary>
            public ChannelBaseConfig baseConfig
            {
                get
                {
                    return _baseConfig;
                }
            }
            /// <summary>
            /// 栏目信息评论配置
            /// </summary>
            public ChannelCommentsConfig commentsConfig
            {
                get
                {
                    return _commentsConfig;
                }
            }
            /// <summary>
            /// 投稿配置
            /// </summary>
            public ContributeConfig contributeConfig
            {
                get
                {
                    return _contributeConfig;
                }
            }
            public ChannelCreateConfig createConfig
            {
                get
                {
                    return _createConfig;
                }
            }
            public ChannelConfig()
            {
                _baseConfig = new ChannelBaseConfig();
                _commentsConfig = new ChannelCommentsConfig();
                _contributeConfig = new ContributeConfig();
                _createConfig = new ChannelCreateConfig();
            }
        }
        public class ChannelBaseConfig
        {
            /// <summary>
            /// 默认每页显示信息数，分页用
            /// </summary>
            public int prePageInfoNum { get; set; }
            /// <summary>
            /// 是否统计内容点击数
            /// </summary>
            public bool countContentClick { get; set; }
            /// <summary>
            /// 是否统计文件下载
            /// </summary>
            public bool countFileDownload { get; set; }
            /// <summary>
            /// 针对搜索引擎的关键字
            /// </summary>
            public string metaKeyword { get;set;} 
            /// <summary>
            /// 针对搜索引擎的说明
            /// </summary>
            public string metaDesc { get; set; }
            /// <summary>
            /// 是否位置导航处显示
            /// </summary>
            public bool showOnPath { get; set; }
            /// <summary>
            /// 是否在网站地图（栏目导航）处显示
            /// </summary>
            public bool showOnMap { get; set; }
            /// <summary>
            /// 本栏目热点的点击数最小值
            /// </summary>
            public int hitsOfHot { get; set; }
            /// <summary>
            /// 链接打开方式  此节点的打开方式( _blank,_parent,_top ,_self)
            /// </summary>
            public string openType { get; set; }
            /// <summary>
            /// 此节点下项目的打开方式( _blank,_parent,_top ,_self)
            /// </summary>
            public string itemOpenType { get; set; }
            /// <summary>
            /// 此节点下项目列表的排序方式
            /// </summary>
            public int itemOrderType { get; set; }
            /// <summary>
            /// 内容是否自动分页
            /// </summary>
            public  PartPage partPage { get; set; }
            /// <summary>
            /// 内容自动分页每页字数
            /// </summary>
            public int autoWordNums { get; set; }
            public enum PartPage{No,Auto,Manual};
            /// <summary>
            /// 使用的编辑器
            /// </summary>
            public string editor{get;set;}
            /// <summary>
            /// 是否能添加栏目
            /// </summary>
            public bool canAddChannel{get;set;}
            /// <summary>
            /// 是否能添加内容
            /// </summary>
            public bool canAddContent{get;set;}
            /// <summary>
            /// 启用内容组
            /// </summary>
            public bool useContentGroup{get;set;}
            /// <summary>
            /// 保存外部图片
            /// </summary>
            public bool SaveExtImage{get;set;}
            ///// <summary>
            ///// 栏目简介
            ///// </summary>
            public string info{get;set;}
            public void setPartPage(string typeStr)
            {
                switch(typeStr)
                {
                    case "No":
                        partPage = PartPage.No;
                    break;
                    case "Auto":
                        partPage = PartPage.Auto;
                    break;
                    case "Manual":
                        partPage = PartPage.Manual;
                    break;
                }
            }
            public bool useContentTag { get; set; }
            public ChannelBaseConfig()
            {
                prePageInfoNum = 50;
                countContentClick = false;
                countFileDownload = false;
                metaKeyword = "";
                metaDesc = "";
                showOnPath = true;
                showOnMap = true;
                hitsOfHot = 200;
                openType = "_self";
                itemOpenType = "_blank";
                itemOrderType = 0;
                partPage = PartPage.Auto;
                autoWordNums = 1500;
                editor = "xHtmlEditor";
                canAddChannel = true;
                canAddContent = true;
                useContentGroup = true;
                SaveExtImage = false;
                info = "";
                useContentTag = true;
            }
        }
        public class ChannelCreateConfig
        {
            public bool createContentChange { get; set; }
            public bool createChannelChange { get; set; }
            public ChannelCreateConfig()
            {
                createContentChange = false;
                createChannelChange = false;
            }
        }
        public class ContributeConfig
        {
            /// <summary>
            /// 是否允许投稿
            /// </summary>
            public bool enabled{get;set;}
            /// <summary>
            /// 是否需要审核
            /// </summary>
            public bool needAudit{get;set;}
            public ContributeConfig()
            {
                enabled=false;
                needAudit=true;
            }
        }
        public class ChannelCommentsConfig //栏目信息评论配置
        {
            /// <summary>
            /// 内容可以评论
            /// </summary>
            public bool isContentComments{ get; set; }
            /// <summary>
            /// 未登录用户是否可以评论
            /// </summary>
            public bool isNeedLogin { get; set; }
            /// <summary>
            /// 评论是否需要审核
            /// </summary>
            public bool isNeedAudit{ get; set; }
            /// <summary>
            /// 评论提交成功提示信息
            /// </summary>
            public string successTipMsg { get; set; }//评论提交成功提示信息
            /// <summary>
            /// 评论提交失败提示信息
            /// </summary>
            public string FailTipMsg { get; set; }//评论提交失败提示信息
            public ChannelCommentsConfig()
            {
                isContentComments = false;
                isNeedLogin = true;
                isNeedAudit = true;
                successTipMsg = "";
                FailTipMsg = "";
            }
        }
        public class siteConfig //站点配置类
        {
            private siteBaseConfig _baseConfig;
            private MailConfig _mailConfig;
            private UploadConfig _uploadConfig;
            private GuestBookConfig _guestBookConfig;
            private WaterMarkConfig _waterMarkConfig;
            private siteCreateConfig _createConfig;
            /// <summary>
            /// 站点基本配置
            /// </summary>
            public siteBaseConfig baseConfig
            {
                get
                {
                    return _baseConfig;
                }
            }
            /// <summary>
            /// 站点邮件服务器配置
            /// </summary>
            public MailConfig mailConfig
            {
                get
                {
                    return _mailConfig;
                }
            }
            public UploadConfig uploadConfig
            {
                get
                {
                    return _uploadConfig;
                }
            }
            /// <summary>
            /// 留言本配置
            /// </summary>
            public GuestBookConfig guestBookConfig
            {
                get
                {
                    return _guestBookConfig;
                }
            }
            /// <summary>
            /// 水印配置
            /// </summary>
            public WaterMarkConfig waterMarkConfig
            {
                get
                {
                    return _waterMarkConfig;
                }
            }
            public siteCreateConfig createConfig
            {
                get { return _createConfig; }
            }
            public siteConfig()
            {
                _baseConfig = new siteBaseConfig();
                _mailConfig = new MailConfig();
                _uploadConfig = new UploadConfig();
                _guestBookConfig = new GuestBookConfig();
                _waterMarkConfig = new WaterMarkConfig();
                _createConfig = new siteCreateConfig();
            }
        }
        public class siteBaseConfig //站点基础配置
        {
            /// <summary>
            /// 访问方式,二种，0:静态，1:伪静态，2动态
            /// </summary>
            public int AccessType { get; set; }
            /// <summary>
            /// 页面扩展名,伪静态时使用
            /// </summary>
            //public string htmlPageExtName { get; set; }
            /// <summary>
            /// 网页默认编码
            /// </summary>
            public Encoding encoder { get; set; }
            /// <summary>
            /// 模板存放类型  0 硬盘1 内存
            /// </summary>
            public int TemplateSaveType { get; set; }
            /// <summary>
            /// 站点标题
            /// </summary>
            public string title{get;set;} 
            /// <summary>
            /// 针对搜索引擎的关键字
            /// </summary>
            public string mateKeyWord { get; set; }
            /// <summary>
            /// 针对搜索引擎的说明 
            /// </summary>
            public string mateDesc { get; set; } 
            /// <summary>
            /// 审核机制
            /// </summary>
            public int auditLevel{get;set;} 
            /// <summary>
            /// ico
            /// </summary>
            public string ico{get;set;}
            /// <summary>
            /// 站点介绍
            /// </summary>
            public string info { get; set; }
            public siteBaseConfig()
            {
                AccessType = 0;
                encoder = Encoding.UTF8;
                TemplateSaveType = 0;
                auditLevel = 0;
                title = string.Empty;
                mateKeyWord = string.Empty;
                mateDesc = string.Empty;
                info = string.Empty;
            }
        }
        public class MailConfig //站点邮件发送配置
        {
            /// <summary>
            /// SMTP服务器
            /// </summary>
            public string SMTPServer { get; set; } //SMTP服务器
            /// <summary>
            /// SMTP端口
            /// </summary>
            public string SMTPPort { get; set; } //SMTP端口
            /// <summary>
            /// 系统邮箱
            /// </summary>
            public string SystemMail { get; set; } //系统邮箱
            /// <summary>
            /// 系统邮箱密码,要加密
            /// </summary>
            public string SystemMailPass { get; set; } //系统邮箱密码,要加密
            public MailConfig()
            {
                SMTPServer = "mail.singno.com";
                SMTPPort = "25";
                SystemMail = "test@singno.com";
                SystemMailPass = "";
            }
        }
    /// <summary>
    /// 站点上传配置
    /// </summary>
        public class UploadConfig 
        {
            /// <summary>
            /// 站点上传目录
            /// </summary>
            public string UploadFolder{get;set;} 
            /// <summary>
            /// Y:年,Y/M:年/月,Y/M/D:年/月/日
            /// </summary>
            public string FileSaveStyle{get;set;}
            /// <summary>
            /// 是否以上传时间修改文件名
            /// </summary>
            public bool IsRenameByTime{get;set;}
            /// <summary>
            /// 图片上传设置信息
            /// </summary>
            public UploadFileInfo ImageUploadInfo{get;set;}
            /// <summary>
            /// 媒体文件上传设置信息
            /// </summary>
            public UploadFileInfo MediaUploadInfo{get;set;}
            /// <summary>
            /// 文档类型文件上传设置信息
            /// </summary>
            public UploadFileInfo DocUploadInfo { get; set; }
            public UploadConfig()
            {
                UploadFolder = "upload";
                FileSaveStyle = "Y/M";
                IsRenameByTime = true;
                ImageUploadInfo = new UploadFileInfo();
                MediaUploadInfo = new UploadFileInfo();
                DocUploadInfo = new UploadFileInfo();
                ImageUploadInfo.MaxSize = 5;
                MediaUploadInfo.MaxSize = 20;
                DocUploadInfo.MaxSize = 5;
            }
        }
        public class UploadFileInfo
        {
            /// <summary>
            /// 可以上传的文件类型
            /// </summary>
            public string FileTypes { get; set; }
            /// <summary>
            /// 上传文件的最大字节
            /// </summary>
            public int MaxSize { get; set; }
            /// <summary>
            /// 上传大小计量单位,K or M
            /// </summary>
            public string SizeType{get;set;}
            public UploadFileInfo()
            {
                FileTypes = string.Empty;
                SizeType = "M";
            }
            public bool checkFileType(string typeStr)
            {
                if (FileTypes.Equals("")) return false;
                string[] type_arr = FileTypes.Split(',');
                bool isEnabled = false;
                foreach (string str  in type_arr)
                {
                    if(string.Compare(str,typeStr,true)==0)
                    {
                        isEnabled = true;
                        break;
                    }
                }
                return isEnabled;
            }
        }
    /// <summary>
    /// 留言本配置
    /// </summary>
        public class GuestBookConfig
        {
            /// <summary>
            /// 是否启用
            /// </summary>
            public bool IsEnabled{get;set;}
            /// <summary>
            /// 是否需要审核
            /// </summary>
            public bool IsNeedAudit{get;set;}
            /// <summary>
            /// 是否需要登陆
            /// </summary>
            public bool IsNeedLogin{get;set;}
            /// <summary>
            /// 是否使用验证码
            /// </summary>
            public bool IsVerifyCode{get;set;}
            public GuestBookConfig()
            {
                IsEnabled = false;
                IsNeedLogin = true;
                IsNeedAudit = true;
                IsVerifyCode = true;
            }
        }
        public class WaterMarkConfig
        {
            public bool enabled{get;set;}
            public int position{get;set;}
            public int alpha{get;set;}
            public int imageMinWidth{get;set;}
            public int imageMinHeight{get;set;}
            public string  markType{get;set;}
            public string imagePath { get; set; }
            public string content{get;set;}
            public string font{get;set;}
            public int fontSize{get;set;}
            public WaterMarkConfig()
            {
                enabled = false;
                position = 9;
                alpha = 50;
                imageMinWidth = 200;
                imageMinHeight = 200;
                markType = "text";
                imagePath = "";
                content = "";
                font = "arial";
                fontSize = 12;
            }
        }
        /// <summary>
        /// 站点出错处理索引
        /// </summary>
        public enum SiteError { toIndexPage, toErrorPage, writeMsgNoSql, writeMsgWithSql }
       
        /// <summary>
        /// 站点生成配置
        /// </summary>
        public class siteCreateConfig
        {
            public SiteError siteErrorHandle{get;set;}
            public XNLOnErrorAction labelErrorHandle{get;set;}
            public bool PreCompiled{get;set;}
            public siteCreateConfig()
            {
                siteErrorHandle = SiteError.writeMsgNoSql;
                labelErrorHandle = XNLOnErrorAction.OutMsg;
                PreCompiled = false;
            }
           // public string pageExtName { get; set; }//页面生成扩展名
            public void setSiteErrorHandle(string typeStr)
            {
                switch(typeStr)
                {
                    case "toIndexPage":
                        siteErrorHandle = SiteError.toIndexPage;
                        break;
                    case "toErrorPage":
                        siteErrorHandle = SiteError.toErrorPage;
                        break;
                    case "writeMsgNoSql":
                        siteErrorHandle = SiteError.writeMsgNoSql;
                        break;
                    case "writeMsgWithSql":
                        siteErrorHandle = SiteError.writeMsgWithSql;
                        break;
                }
            }
            public void setLabelErrorHandle(string typeStr)
            {
                switch (typeStr)
                {
                    case "writeMsg":
                        labelErrorHandle = XNLOnErrorAction.OutMsg;
                        break;
                    case "throwError":
                        labelErrorHandle = XNLOnErrorAction.ThrowError;
                        break;
                    case "writeEmpty":
                        labelErrorHandle = XNLOnErrorAction.OutEmpty;
                        break;
                }
            }
        }
        /// <summary>
        /// 模板
        /// </summary>
        public class Template
        {
            /// <summary>
            /// 单页类型   0:首页模板 1：栏目模板 2：内容模板 3：单页模板
            /// </summary>
            public int templateStyle { get; set; }
            /// <summary>
            /// 模板方案id
            /// </summary>
            public int templateProjectId{get;set;}
            /// <summary>
            /// 模板路径
            /// </summary>
            public string TemplatePath { get; set; }
            private string _TemplateContent;
            /// <summary>
            /// 所属的站点类
            /// </summary>
            public Cchannel theSite { get; set; }
            /// <summary>
            /// 模板页内容
            /// </summary>
            public string TemplateContent
            {
                get
                {
                    if (theSite.theSiteConfig.baseConfig.TemplateSaveType == 0)
                    {
                        _TemplateContent = null;
                        return CMSUtils.loadTempleteByPath(TemplatePath, encoder);
                    }
                    if (_TemplateContent == null) return "";
                    return _TemplateContent;
                }
                set
                {
                    _TemplateContent = value;
                }
            }
            /// <summary>
            /// 编码
            /// </summary>
            public Encoding encoder { get; set; }
        }
        //单页集合
        public class singlePage //单页类
        {
            /// <summary>
            /// 单页类所属的节点类
            /// </summary>
            public Cchannel theChannel { get; set; }//单页类所属的节点类
            /// <summary>
            /// 模板
            /// </summary>
            public Template template { get; set; }
        }
        /// <summary>
        /// 模板方案类
        /// </summary>
        public class TemplateProject{
            public string templateProjectName{get;set;}
            /// <summary>
            /// 首页地址
            /// </summary>
            public string indexUrl{get;set;}
            private SafeDictionary<int, string> _channelUrlColls;
            private SafeDictionary<int, string> _contentUrlRuleColls;
            public string getContentUrl(int channelId, string contentId)
            {
                string pathRule = _contentUrlRuleColls[channelId];
                string extStr = CMSUtils.getExtName(pathRule);
                if (extStr.ToLower().Equals(".aspx"))
                {
                    return pathRule.Replace("[#ContentID]", "Content").Replace(extStr, "["+"id=" + contentId+"]"+extStr);
                }
                else
                {
                    return pathRule.Replace("[#ContentID]", contentId);
                }
            }
            public TemplateProject()
            {
                _channelUrlColls = new SafeDictionary<int, string>();
                _contentUrlRuleColls = new SafeDictionary<int, string>();
            }
            public string getChannelUrl(int channelId){
                return _channelUrlColls[channelId];
            }
            /// <summary>
            /// 添加或修改栏目页地址
            /// </summary>
            /// <param name="channelId"></param>
            /// <param name="channelUrl"></param>
            public void setChannelUrl(int channelId, string channelUrl)
            {
                if (_channelUrlColls.ContainsKey(channelId))
                {
                    _channelUrlColls[channelId] = channelUrl;
                }
                else
                {
                    _channelUrlColls.Add(channelId, channelUrl);
                }
            }
            /// <summary>
            /// 移除栏目页地址
            /// </summary>
            /// <param name="channelId"></param>
            public void removeChannelUrl(int channelId)
            {
                if (_channelUrlColls.ContainsKey(channelId))
                {
                    _channelUrlColls.Remove(channelId);
                }
            }
            /// <summary>
            /// 添加或修改内容页地址
            /// </summary>
            /// <param name="channelId"></param>
            /// <param name="contentUrlRule"></param>
            public void setContentUrlRule(int channelId, string contentUrlRule)
            {
                if (_contentUrlRuleColls.ContainsKey(channelId))
                {
                    _contentUrlRuleColls[channelId] = contentUrlRule; 
                }
                else
                {
                    _contentUrlRuleColls.Add(channelId, contentUrlRule);
                }
            }
            /// <summary>
            /// 移除内容页地址
            /// </summary>
            /// <param name="channelId"></param>
            public void removeContentUrlRule(int channelId)
            {
                if (_contentUrlRuleColls.ContainsKey(channelId))
                {
                    _contentUrlRuleColls.Remove(channelId);
                }
            }
            public void disponse(){
                _channelUrlColls = null;
                _contentUrlRuleColls = null;
            }
        }
        public enum CreateThreadState
        {
            NO,
            YES
        }
        public class PageCreateThreadInfo
        {
            private int _pageCount;
            /// <summary>
            /// 生成页面总数
            /// </summary>
            public int pageCount
            {
                get
                {
                    return _pageCount;
                }
                set
                {
                     _pageCount = value;
                }
            }
            private int _createdPageCount;
            /// <summary>
            /// 已生成页面计数
            /// </summary>
            public int createdPageCount
            {
                get
                {
                    return _createdPageCount;
                }
            }
            private CreateThreadState _state;
            /// <summary>
            /// 当前生成状态，NO：当前没有生成线程执行 YES：当前有生成线程执行
            /// </summary>
            public CreateThreadState state
            {
                get
                {
                    return _state;
                }
            }
            /// <summary>
            /// 当前所有生成线程列表
            /// </summary>
            //private List<WorkItem> _threadList;
            //public List<WorkItem> threadList
            //{
            //    get
            //    {
            //        return _threadList;
            //    }
            //    set
            //    {
            //        _threadList = value;
            //    }
            //}
            public PageCreateThreadInfo()
            {
                _pageCount = 0;
                _createdPageCount = 0;
                _state = CreateThreadState.NO;
                //_threadList = new List<WorkItem>();
            }
            public void addCreatePage()
            {
                Interlocked.Add(ref _createdPageCount, 1);

            }
            /// <summary>
            /// 设置状态
            /// </summary>
            /// <param name="state"></param>
            public void setState(CreateThreadState state)
            {
                //lock (stateSyncRoot)
                //{
                    _state = state;
                //}
            }
            /// <summary>
            /// 取消
            /// </summary>
            public void cancle()
            {
                //lock (syncRoot)
                //{
                    //try
                    //{
                    //    int count = _threadList.Count;
                    //    for (int i = 0; i < count; i++)
                    //    {
                    //        AbortableThreadPool.Cancel(_threadList[i], false);
                    //    }
                    //}
                    //catch { }
                    _pageCount = 0;
                    _createdPageCount = 0;
                    _state = CreateThreadState.NO;
                    //_threadList.Clear();
                //}
            }
            /// <summary>
            /// 重置
            /// </summary>
            public void reSet()
            {
                //lock (syncRoot)
                //{
                    _pageCount = 0;
                    _createdPageCount = 0;
                    _state = CreateThreadState.NO;
                    //_threadList.Clear();
                //}
            }
        }
        public class NodeContentCreateSet
        {
            ///<summary>
            /// 访问方式,二种，0:静态，1:伪静态，2动态
            /// </summary>
            public string fileExtName;
            public int templateId;
            public string contentPathRule;
            public string contentDirPathRule;
            public int nodeId;
            public string charset;
            public int curPage=0;
            public int allPage=0;
            public int allContentPage=0;//生成总数
            public int curCreatedPage=0;//当前生成的页面
            public int curPageCreatedPage = 0; //当前分页生成的页面数
            public Dictionary<string, string> myTagColls;
            public List<string> contentFieldList; //内容页字段列表
            public void addCurPage()
            {
                Interlocked.Add(ref curPage, 1);
                curPageCreatedPage = 0;
            }
            public void addCreatedPage()
            {
                Interlocked.Add(ref curCreatedPage, 1);
                Interlocked.Add(ref curPageCreatedPage, 1);
            }
        }
    /// <summary>
    /// 单个页面的生成线程信息,适用于首页，栏目页，单页
    /// </summary>
        public class SinglePageCreateThreadInfo:PageCreateThreadInfo
        {
            public WebContext XNLPage;
            //private object obj = new object();
            private Queue<ParseInfo> ParseInfoQueue = new Queue<ParseInfo>();
            public void addParseInfo(ParseInfo parseInfo)
            {
                ParseInfoQueue.Enqueue(parseInfo);
            }
            public ParseInfo getWhileCreateParseInfo()
            {
                //lock (obj)
                //{
                    //threadList.Clear();
                    if(ParseInfoQueue.Count>0)
                    {
                        return ParseInfoQueue.Dequeue();
                    }
                    else
                    {
                        return null;
                    }
                //}
            }
            /// <summary>
            /// 取消
            /// </summary>
            public new void cancle()
            {
                lock (this)
                {
                    ParseInfoQueue.Clear();
                    base.cancle();
                    XNLPage = null;
                }

            }
            /// <summary>
            /// 重置
            /// </summary>
            public new void reSet()
            {
                lock (this)
                {
                    ParseInfoQueue.Clear();
                    base.reSet();
                    XNLPage = null;
                }
            }
        }
        public class ContentPageCreateThreadInfo:PageCreateThreadInfo
        {
            public WebContext XNLPage;
            //每次生成1个信息页
            private int _createdPageCount;
            /// <summary>
            /// 已生成页面计数
            /// </summary>
            public new int createdPageCount
            {
                get
                {
                    return _createdPageCount;
                }
            }
            public new void addCreatePage()
            {
                Interlocked.Add(ref _createdPageCount, 1);
                _nodeCreateAttrs[_nodeQueue.Peek()].addCreatedPage();
            }
            private Dictionary<int, NodeContentCreateSet> _nodeCreateAttrs = new Dictionary<int, NodeContentCreateSet>(); //Dictionary<栏目id, allpage> 
            public Dictionary<int, NodeContentCreateSet> nodeCreateAttrs
            {
                get
                {
                    return _nodeCreateAttrs;
                }
            }
            public Dictionary<int, string> templateColls;
            public void addNodeCreateAttrs(int nodeId, NodeContentCreateSet createAttr)
            {
                _nodeCreateAttrs.Add(nodeId, createAttr);
            }
            public int getCurCreateNode()
            {
                return _nodeQueue.Peek();
            }
            public NodeContentCreateSet getNodeSetting(int nodeId)
            {
                return _nodeCreateAttrs[nodeId];
            }
            private Queue<int> _nodeQueue=new Queue<int>(); //生成栏目队列
            public Queue<int> nodeQueue
            {
                get{
                    return _nodeQueue;
                }
            }
            public string whereStr;
            public void addNodeToQueue(int nodeId)
            {
                _nodeQueue.Enqueue(nodeId);
            }
            //public void clearCurThread()
            //{
            //    //threadList.Clear();
            //}
            private Queue<ParseInfo> ParseInfoQueue = new Queue<ParseInfo>();
            public void addParseInfo(ParseInfo parseInfo)
            {
                ParseInfoQueue.Enqueue(parseInfo);
            }
            public ParseInfo getWhileCreateParseInfo()
            {
                if (ParseInfoQueue.Count > 0)
                {
                    return ParseInfoQueue.Dequeue();
                }
                else
                {
                     return null;
                }
            }
            /// <summary>
            /// 取消
            /// </summary>
            public new void cancle()
            {
                lock (this)
                {
                    _nodeQueue.Clear();
                    _nodeCreateAttrs.Clear();
                    _createdPageCount = 0;
                    ParseInfoQueue.Clear();
                    templateColls = null;
                    base.cancle();
                    XNLPage = null;
                }
               
            }
            /// <summary>
            /// 重置
            /// </summary>
            public new void reSet()
            {
                lock(this)
                {
                    _createdPageCount = 0;
                    _nodeQueue.Clear();
                    _nodeCreateAttrs.Clear();
                    ParseInfoQueue.Clear();
                    templateColls = null;
                    base.reSet();
                    XNLPage = null;
                }
                
            }
        }
}
