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
<form action="Config_ChannelAction.aspx?action=modify&type=create&channelid={&nodeid}" id="form1" name="form1" method="post">
<CMS.Manage:ChannelConfig action="view" type="create" channelid="{&nodeid}">
<table class="forms">
	<tbody>
		<tr>
			<td width="120">内容变动时生成：</td>
			<td>
				<input name="contentChange" type="radio" id="contentChange1" value="True" {[iif('{@contentchange}'='True','checked="checked"','')]} /><label for="contentChange1">是</label> 
				<input name="contentChange" type="radio" id="contentChange2" value="False" {[iif('{@contentchange}'='False','checked="checked"','')]}/><label for="contentChange2">否</label>
			</td>
		</tr>
		<tr>
			<td width="120">栏目变动时生成：</td>
			<td>
				<input name="channelChange" type="radio" id="channelChange1" value="True" {[iif('{@channelchange}'='True','checked="checked"','')]} /><label for="channelChange1">是</label> 
				<input name="channelChange" type="radio" id="channelChange2" value="False" {[iif('{@channelchange}'='False','checked="checked"','')]}/><label for="channelChange2">否</label>
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
Sio.Text();
Sio.Button();

</script>
</body>
</html>