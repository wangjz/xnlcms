<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="utf-8" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 1;
    pageStyle=3;
    projectId = 1;
    document=new StringBuilder(427);documentAppendLine("<xnl:location><location.item><a href=\"{@location.url}\">{@location.name}</a></location.item></xnl:location>\r");
documentAppendLine("<xnl:pagetext type=\"tag\" pagename=\"page\">北京时间3月15日上午消息，微软公司今天在影视音乐互动大会(SXSW)上宣布推出最新版本Windows Internet Explorer 9浏览器。该浏览器支持39个语言版本，不支持XP系统。 [#xnl_page#]　　在Beta版本测试期间，Internet Explorer 9成为微软历史上下载数量最多的测试版浏览器，下载次数超过4000万，其在Windows 7上使用率已经超过2%。目前已有250多家全球顶级网站利用IE9的功能为用户提供与众不同的体验，这些合作伙伴的用户覆盖了超过10亿的网络活跃用户。\r");
documentAppendLine("</xnl:pagetext>\r");
documentAppendLine("");

}
</script>