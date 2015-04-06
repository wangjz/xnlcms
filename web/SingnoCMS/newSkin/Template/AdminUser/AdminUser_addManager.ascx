<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/User.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="AdminUser_action.aspx?action=addAdmin" id="form1" method="post" >
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">管理员登陆名：</td>
			<td><input type="text" name="LoginName" id="LoginName" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">管理员显示名：</td>
			<td><input type="text" name="DisplayName" id="DisplayName" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>管理员密码：</td>
			<td><input type="text" name="Password" id="Password" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>密码确认：</td>
			<td><input type="text" name="ConfirmPass" id="ConfirmPass" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>电子邮件：</td>
			<td><input type="text" name="Email" id="Email" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>找到密码问题：</td>
			<td><input type="text" name="Question" id="Question" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>找回密码答案：</td>
			<td><input type="text" name="Answer" id="Answer" /><span class="required">*</span></td>
		</tr>
        <tr>
			<td>管理员角色：</td>
			<td><select name="RoleId" id="RoleId">
			  <option value="0">超级管理员</option>
              <xnl:repeater sqlcommand="select ID,roleName from  SN_AdminRole">
              <itemTemplate><option value="{$id}">{$roleName}</option></itemTemplate>
              </xnl:repeater></select></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="添加管理员" /></div></td>
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