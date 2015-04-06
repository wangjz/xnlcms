<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body{ margin:0 10px; cursor:default}
</style>
</head>
<body>
<XNL:repeater DescriptId="{&id}" sqlCommand="select * from SN_ModelDescript where DescriptId=@DescriptId">
<itemTemplate>
<table class="tables" cellpadding="0" cellspacing="0">
	<tbody id="ctrl_cmd">
		<tr>
			<td width="90px">字段名：</td><td>{$FieldName}</td>
		</tr>
		<tr>
			<td>模型名：</td><td>{$ModelName}</td>
		</tr>
		<tr>
			<td>数据类型：</td><td>{$DataType}</td>
		</tr>
		<tr>
			<td>数据长度：</td><td>{$DataLength}</td>
		</tr>
		<tr>
			<td>数据库默认值：</td><td>{[iif('{$DataType}'='Boolean','{[iif('{$DefaultValue}'='0','否','是')]}','{$DefaultValue}')]}</td>
		</tr>
		<tr>
			<td>显示名称：</td><td>{$DisplayName}</td>
		</tr>
		<tr>
			<td>显示帮助提示：</td><td>{$HelpText}</td>
		</tr>
		<tr>
			<td>需要验证：</td><td>{[iif('{$IsValidator}'='1','是','否')]}</td>
		</tr>
	</tbody>
</table>
</itemTemplate>
</XNL:repeater>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
