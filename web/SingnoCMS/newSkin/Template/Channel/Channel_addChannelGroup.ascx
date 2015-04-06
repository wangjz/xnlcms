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
body{ margin:10px}
</style>
</head>
<body>
<form  method="post" class="KForm" action="Channel_Action.aspx?action=addgroup" name="form1" id="form1">
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody>
		<tr>
			<td width="120px">栏目组名称：</td>
			<td><input type="text" name="GroupName" id="GroupName" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">栏目组描述：</td>
			<td><textarea name="Description" cols="50" rows="10" id="Description"></textarea></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" name="button" id="button" value="提交" /></div></td>
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
