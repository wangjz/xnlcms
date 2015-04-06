<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="utf-8" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 1;
    pageStyle=3;
    projectId = 1;
    document=new StringBuilder(421);documentAppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r");
documentAppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r");
documentAppendLine("<head>\r");
documentAppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r");
documentAppendLine("<title>无标题文档</title>\r");
documentAppendLine("</head>\r");
documentAppendLine("<body>\r");
documentAppendLine("<xnl.mytag:头部></xnl.mytag:头部>\r");
documentAppendLine("/channels/1.aspx\r");
documentAppendLine("\r");
documentAppendLine("<a href=\"http://localhost:4815/index/index.aspx?aa=规划符合规范\">jjj</a>\r");
documentAppendLine("</body>\r");
documentAppendLine("</html>");
addMyTag("头部","http://localhost:4815");

}
</script>