<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body{ padding:0 0 0 10px}
</style>
</head>
<body>
<form action="Config_ChannelAction.aspx?action=modify&type=comments&channelid={&nodeid}" id="form1" name="form1" method="post">
<CMS.Manage:ChannelConfig action="view" type="comments" channelid="{&nodeid}">
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">内容评论：</td>
			<td>
			<input name="contentCommented" type="radio" id="contentCommented1" value="True" {[iif('{@enabled}'='True','checked="checked"','')]} /><label for="contentCommented1">允许</label>
			<input name="contentCommented" type="radio" id="contentCommented2" value="False" {[iif('{@enabled}'='False','checked="checked"','')]}/><label for="contentCommented2">不允许</label>
			</td>
		</tr>
		<tr>
			<td valign="top">未登陆用户评论：</td>
			<td>
			  <input name="needLogin" type="radio" id="unloginCommented1" value="False" {[iif('{@needlogin}'='False','checked="checked"','')]}/><label for="unloginCommented1">允许</label>
				<input name="needLogin" type="radio" id="unloginCommented2" value="True" {[iif('{@needlogin}'='True','checked="checked"','')]}/><label for="unloginCommented2">不允许</label></td>
		</tr>
		<tr>
			<td>评论审核：</td>
			<td>
			  <input name="isAudit" type="radio" id="isAudit1" value="True" {[iif('{@needaudit}'='True','checked="checked"','')]}/><label for="isAudit1">需要审核</label>
				<input name="isAudit" type="radio" id="isAudit2" value="False" {[iif('{@needaudit}'='False','checked="checked"','')]}/><label for="isAudit2">不需要审核</label>
			</td>
		</tr>
		<tr>
			<td valign="top">评论成功提示信息：</td>
			<td><textarea name="successmsg" cols="80" rows="5" id="successmsg">{@successmsg}</textarea></td>
		</tr>
		<tr>
			<td valign="top">评论失败提示信息：</td>
			<td><textarea name="failmsg" id="failmsg" cols="80" rows="5">{@failmsg}</textarea></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</CMS.Manage:ChannelConfig>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html>