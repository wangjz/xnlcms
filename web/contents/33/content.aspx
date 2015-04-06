<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="gb2312" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 33;
    pageStyle=2;
    projectId = 1;
    document=new StringBuilder(17);documentAppendLine("{[content.title]}");
setContentField("content,title,titlecolor,bold,italic,underline");

}
</script>