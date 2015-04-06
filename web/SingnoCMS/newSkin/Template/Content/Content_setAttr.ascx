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
<form action="content_action.aspx?action=attribute" method="post" id="form1" name="form1">
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
			  <input name="isComment" type="radio" id="isCommented1" value="1" {[iif('{&comment}'='1','checked="checked"','')]} /><label for="isCommented1">允许</label> 
				<input name="isComment" type="radio" id="isCommented2" value="0" {[iif('{&comment}'='0','checked="checked"','')]}/><label for="isCommented2">不允许</label>
			</td>
		</tr>
		<tr>
			<td>状态：</td>
			<td>
				<input id="state1" value="-1" type="radio" name="State"   {[iif('{&State}'='-1','checked="checked"','')]} /><label for="state1">草稿</label>
				<input id="state2" value="0" type="radio" name="State"   {[iif('{&State}'='0','checked="checked"','')]} /><label for="state2">待审核</label>
				<input id="state3" name="State" type="radio" value="99"  {[iif('{&State}'='99','checked="checked"','')]} /><label for="state3">终审通过</label>
			</td>
		</tr>
		<tr>
			<td>设置属性：</td>
			<td>
			  <input name="isRecommend" type="checkbox" id="optionAttr1" value="1" {[iif('{&Recommend}'='1','checked="checked"','')]}/><label for="optionAttr1">推荐</label> 
			  <input name="isHot" type="checkbox" id="optionAttr2" value="1" {[iif('{&hot}'='1','checked="checked"','')]}/><label for="optionAttr2">热点</label> 
			  <input name="isColor" type="checkbox" id="optionAttr3" value="1" {[iif('{&color}'='1','checked="checked"','')]}/><label for="optionAttr3">醒目</label> 
				<input name="isTop" type="checkbox" id="optionAttr4" value="1" {[iif('{&top}'='1','checked="checked"','')]}/><label for="optionAttr4">置顶</label>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center"><input name="nodeid" type="hidden" id="nodeid" value="{&nodeid}" />
		  <input name="contentid" type="hidden" id="contentid" value="{&contentid}" /><input name="提交" type="submit" id="saveAttr" value="保存"/></td>
		</tr>
	</tbody>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
</script>
</body>
</html>
