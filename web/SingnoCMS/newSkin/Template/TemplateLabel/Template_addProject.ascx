<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="Template_action.aspx?action=addProject" id="form1" method="post">
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">模板方案名称：</td>
			<td>
				<input name="projectName" type="text" size="25" maxlength="50" />
			<!--栏目页默认生成路径规则:--><input name="ChannelFilePathRule" type="hidden" value="channels/[#ChannelID].html" size="50" maxlength="200" />
			<!--内容页默认生成路径规则:--><input name="ContentFilePathRule" type="hidden" value="contents/[#ChannelID]/[#ContentID].html" size="50" maxlength="200" />
			<span class="required">*</span>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" name="button" id="button" value="新建方案" /></div></td>
		</tr>
	</tfoot>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html>