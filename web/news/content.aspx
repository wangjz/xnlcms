<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="utf-8" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 33;
    pageStyle=2;
    projectId = 1;
    document=new StringBuilder(103);documentAppendLine("{[content.id]}\r");
documentAppendLine("</br>\r");
documentAppendLine("{[content.title]}\r");
documentAppendLine("{[content.content]}\r");
documentAppendLine("{[content.adddate]}\r");
documentAppendLine("{[content.summary]}");
setContentField("content,title,titlecolor,bold,italic,underline,adddate,summary");

}
</script>