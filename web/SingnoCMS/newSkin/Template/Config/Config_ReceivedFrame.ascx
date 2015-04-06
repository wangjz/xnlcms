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
<form action="Config_ChannelAction.aspx?action=modify&type=contribute&channelid={&nodeid}" id="form1" name="form1" method="post">
<CMS.Manage:ChannelConfig action="view" type="contribute" channelid="{&nodeid}">
<table class="forms">
	<tbody>
		<tr>
			<td width="120">允许投稿：</td>
			<td>
			<input type="radio" name="isReceived" id="isReceived1"  value="True" {[iif('{@enabled}'='True','checked="checked"','')]} />
			允许
			<input type="radio" name="isReceived" id="isReceived2" value="False" {[iif('{@enabled}'='False','checked="checked"','')]} />
			不允许</td>
		</tr>
		<tr>
			<td valign="top">审核控制：</td>
			<td>
				<input type="radio" name="isAudit" id="isAudit1" value="True" {[iif('{@needaudit}'='True','checked="checked"','')]}/><label for="isAudit1">开启</label>
				<input type="radio" name="isAudit" id="isAudit2" value="False" {[iif('{@needaudit}'='False','checked="checked"','')]}/><label for="isAudit2">关闭</label>
			</td>
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
Sio.Button();

</script>
</body>
</html>