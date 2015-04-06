<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="utf-8" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 1;
    pageStyle=3;
    projectId = 1;
    document=new StringBuilder(1402);documentAppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r");
documentAppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r");
documentAppendLine("<head>\r");
documentAppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r");
documentAppendLine("<title>仙诺科技</title>\r");
documentAppendLine("<meta name=\"keywords\" content=\"郑州网站建设,郑州界面设计,郑州RIA/AIR开发,郑州flash设计,郑州flash游戏开发，郑州软件开发，郑州软件设计，郑州flash动画\" />\r");
documentAppendLine("<meta name=\"description\" content=\"仙诺（郑州）信息科技有限公司（Singno Information Technology Co., Ltd.）是以技术研发为主导，专业为企业提供网络服务的科技企业，致力于UI设计、RIA/AIR开发、网站开发及管理系统开发等技术领域的研究与探索。 \" />\r");
documentAppendLine("<link href=\"css/base.css\" rel=\"stylesheet\" type=\"text/css\" />\r");
documentAppendLine("<link href=\"css/list.css\" rel=\"stylesheet\" type=\"text/css\" />\r");
documentAppendLine("</head>\r");
documentAppendLine("<body>\r");
documentAppendLine("<xnl.mytag:通用头部></xnl.mytag:通用头部>\r");
documentAppendLine("<div id=\"articleList\">\r");
documentAppendLine("\t<ul><xnl:contents scope=\"all\"><contents.item>\r");
documentAppendLine("\t\t<li>\r");
documentAppendLine("\t\t\t<div class=\"fl\"><img src=\"images/pic1.jpg\" alt=\"\" /></div>\r");
documentAppendLine("\t\t\t<div class=\"fr\">\r");
documentAppendLine("\t\t\t\t<div class=\"title\"><a href=\"news/{@contents.id}.html\">{$title}</a><span>{[toshortdate('{@contents.adddate}')]}</span></div>\r");
documentAppendLine("\t\t\t\t<div class=\"txt\">{@contents,Summary}</div>\r");
documentAppendLine("\t\t\t\t<div class=\"enter\"><a href=\"news/{@contents.id}.html\">查看</a></div>\r");
documentAppendLine("\t\t\t</div>\r");
documentAppendLine("\t\t</li></contents.item></xnl:contents>\r");
documentAppendLine("\t</ul>\r");
documentAppendLine("</div>\r");
documentAppendLine("<xnl.mytag:版权></xnl.mytag:版权>\r");
documentAppendLine("<script type=\"text/javascript\" src=\"js/Sio.base_11.02.min.js\"><|||||||script|||||||>\r");
documentAppendLine("<script type=\"text/javascript\" src=\"js/nav.js\"><|||||||script|||||||>\r");
documentAppendLine("</body>\r");
documentAppendLine("</html>");
addMyTag("通用头部","<div id=\"top\">\r\n\t<div class=\"left\"></div>\r\n\t<div class=\"center\">\r\n\t\t<div id=\"moveCon\">\r\n\t\t\t<span class=\"fl\"><a href=\"/index.html\"><img src=\"/images/logo.gif\" alt=\"仙诺科技\" border=\"0\"/></a></span>\r\n\t\t\t<div id=\"search\" class=\"fr\">\r\n\t\t\t\t<input id=\"searchInput\" type=\"text\" class=\"fl\" />\r\n\t\t\t\t<div class=\"fr\"><img id=\"searchCmd\" src=\"/images/close.gif\" alt=\"\" /></div>\r\n\t\t\t</div>\r\n\t\t\t<div id=\"nav\" class=\"fr\">\r\n\t\t\t\t<ul><xnl:repeater groupname=\"顶部导航\" sqlcommand=\"select a.NodeName,a.IndexName from sn_nodes a,sn_nodegroup b,SN_NodeGroups c where b.ng_Name=@groupname and b.ng_ID=c.ngs_ID and a.NodeID=c.ngs_nodeId\"><itemTemplate>\r\n\t\t\t\t\t<li><a href=\"/{$IndexName}.html\">{$NodeName}</a></li></itemTemplate>\r\n                    </xnl:repeater>\r\n\t\t\t\t</ul>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n\t<div class=\"right\"></div>\r\n</div>");
addMyTag("版权","<div id=\"copyright\">\r\n\t<a href=\"#\">关于我们</a> | <a href=\"#\">网站建设套餐</a> | <a href=\"#\">优化推广</a> | <a href=\"#\">主机域名</a> | <a href=\"#\">改版维护</a> | <a href=\"#\">定制服务</a> | <a href=\"#\">联系我们</a><br />\r\n\tcopyright 2009-2010 Singno.com All Rights Reserved <a href=\"mailto:webmaster@Singno.com\">webmaster@Singno.com</a> 豫ICP备09021950号\r\n</div>");

}
</script>