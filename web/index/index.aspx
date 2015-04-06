<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page EnableTheming="false" enableEventValidation="false" ResponseEncoding="utf-8" EnableViewState="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never"  Inherits="COM.SingNo.Web.WebPage"%>
<script language="C#" runat="server"> 
void page_init(object sender, EventArgs e)
{
    nodeId = 1;
    pageStyle=0;
    projectId = 1;
    document=new StringBuilder(409);documentAppendLine("新华网马德里１月５日电（记者明金维 冯俊伟）国务院副总理李克强５日在中国西班牙企业家早餐会上致辞时说，中西双方将签署总额达７５亿美元的政府协议和商业合同。\r");
documentAppendLine("\r");
documentAppendLine("李克强说，我们此次来，将与西方签订一系列经贸合作协议，很快我将和贵国首相萨帕特罗一起见证１６项协议的签字仪式，涉及金额达７５亿美元。\r");
documentAppendLine("\r");
documentAppendLine("李克强说，我们将从西班牙购进几千万美元的橄榄油、葡萄酒、火腿。大家都知道，再过一个月，中国传统的新年佳节就到了，我们也希望这些产品能够尽快摆上中国人民欢庆春节的餐桌。我们将继续鼓励中国居民到西班牙旅游和度假，领略独特的西班牙风情和风光。\r");
documentAppendLine("\r");
documentAppendLine("应西班牙政府邀请，李克强当地时间４日下午抵达马德里，开始对西班牙进行为期３天的正式访问。５日在参加完中国西班牙企业家早餐会后，李克强将会见西班牙首相萨帕特罗、国王卡洛斯一世和外交大...全文>> (来源：新华网)\r");
documentAppendLine("\r");
documentAppendLine("http://localhost:18888\r");
documentAppendLine("\r");
documentAppendLine("");

}
</script>