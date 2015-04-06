<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.forms{ margin:0 10px}
</style>
</head>
<body>
<table class="forms">
	<tbody>
		<tr>
			<td width="80px">阅读权限：</td>
			<td>
				<input type="checkbox" name="userRight" id="userRight1" /><label for="userRight1">栏目管理员</label> 
				<input type="checkbox" name="userRight" id="userRight2" /><label for="userRight2">高级会员</label> 
				<input type="checkbox" name="userRight" id="userRight3" /><label for="userRight3">普通会员</label> 
				<input type="checkbox" name="userRight" id="userRight4" /><label for="userRight4">游客</label>
			</td>
		</tr>
		<tr>
			<td>所需点数：</td>
			<td><input type="text" name="" id="" /></td>
		</tr>
		<tr>
			<td>允许评论：</td>
			<td>
				<input type="radio" name="isCommented" id="isCommented1" /><label for="isCommented1">允许</label> 
				<input type="radio" name="isCommented" id="isCommented2" /><label for="isCommented2">不允许</label>
			</td>
		</tr>
		<tr>
			<td>状态：</td>
			<td>
				<input id="state1" value="-1" type="radio" name="State"   {@State.CaoGao} /><label for="state1">草稿</label>
				<input id="state2" value="0" type="radio" name="State"   {@State.ShenHe} /><label for="state2">待审核</label>
				<input id="state3" name="State" type="radio" value="99"  {@State.ShenHeOK} /><label for="state3">终审通过</label>
			</td>
		</tr>
		<tr>
			<td>设置属性：</td>
			<td>
				<input type="checkbox" name="optionAttr" id="optionAttr1"/><label for="optionAttr1">推荐</label> 
				<input type="checkbox" name="optionAttr" id="optionAttr2"/><label for="optionAttr2">热点</label> 
				<input type="checkbox" name="optionAttr" id="optionAttr3"/><label for="optionAttr3">醒目</label> 
				<input type="checkbox" name="optionAttr" id="optionAttr4"/><label for="optionAttr4">置顶</label>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center"><input type="button" onclick="javascript:top.closeAlert()" id="saveAttr" value="保存"/></td>
		</tr>
	</tbody>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
</script>
</body>
</html>
