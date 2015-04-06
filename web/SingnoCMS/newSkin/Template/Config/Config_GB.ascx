<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="config_siteaction.aspx?action=modify&type=guestbook" id="form1" name="form1" method="post" >
<CMS.Manage:SiteConfig action="view" type="guestbook">
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">留言本状态：</td>
			<td>
			<input type="radio" name="isEnabled" id="switchGB1" {[iif('{@enabled}'='True','checked="checked"','')]} value="True"/><label for="switchGB1">开启</label>
			<input type="radio" name="isEnabled" id="switchGB2" {[iif('{@enabled}'='False','checked="checked"','')]} value="False"/><label for="switchGB2">关闭</label>
			</td>
		</tr>
		<tr>
			<td valign="top">回复审核：</td>
			<td>
			  <input type="radio" name="isAudit" id="switchReplyAudit1"  {[iif('{@needaudit}'='True','checked="checked"','')]} value="True"/><label for="switchReplyAudit1">需要审核</label>
				<input type="radio" name="isAudit" id="switchReplyAudit2" {[iif('{@needaudit}'='False','checked="checked"','')]} value="False"/><label for="switchReplyAudit2">不需要审核</label>
			</td>
		</tr>
		<tr>
			<td>是否需要登陆：</td>
			<td>
				<input type="radio" name="isLogin" id="isLogin1" {[iif('{@needlogin}'='True','checked="checked"','')]} value="True"/><label for="isLogin1">需要登陆</label>
				<input type="radio" name="isLogin" id="isLogin2" {[iif('{@needlogin}'='False','checked="checked"','')]} value="False"/><label for="isLogin2">不需要登陆</label>
			</td>
		</tr>
		<tr>
			<td valign="top">验证码功能：</td>
			<td>
				<input type="radio" name="isCode" id="isCode1"   {[iif('{@needverify}'='True','checked="checked"','')]} value="True"/><label for="isCode1">开启</label>
				<input type="radio" name="isCode" id="isCode2"  {[iif('{@needverify}'='False','checked="checked"','')]} value="False"/><label for="isCode2">关闭</label>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</CMS.Manage:SiteConfig>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();

</script>
</body>
</html>