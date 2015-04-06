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
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">登陆名：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">显示名：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>密码：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>密码确认：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>所属用户组：</td>
			<td>
				<select id="userGroup" name="userGroup">
					<option>新手上路</option>
					<option>注册会员</option>
					<option>中级会员</option>
					<option>高级会员</option>
					<option>金牌会员</option>
					<option>元老会员</option>
				</select>
			</td>
		</tr>
		<tr>
			<td>积分设置：</td>
			<td><input type="text" name="" id="" /></td>
		</tr>
		<tr>
			<td>电子邮件：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>找到密码问题：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>找回密码答案：</td>
			<td><input type="text" name="" id="" /><span class="required">*</span></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="添加用户" /></div></td>
		</tr>
	</tfoot>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html>