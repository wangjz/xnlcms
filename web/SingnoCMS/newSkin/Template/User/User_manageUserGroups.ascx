<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/User.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.tables td{text-align:center;}
.tables td.left{ text-align:left}
</style>
</head>
<body>
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_edit" title="编辑"></div>
	<div class="Ico_del" title="删除"></div><div class="line"></div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
		<thead>
			<tr>
				<td width="2%"></td>
				<td class="left" width="25%">用户组名称</td>
				<td width="15%">积分范围</td>
				<td width="15%">权限级别</td>
				<td width="15%">会员数</td>
				<td width="18%">星星数</td>
				<td width="10%" align="center">操作</td>
			</tr>
		</thead>
		<tbody id="ctrl_cmd">
			<tr>
				<td><input type="checkbox" name="classListItem" id="" /></td>
				<td class="left"><a href="#">新手上路</a></td>
				<td>0-999</td>
				<td>12</td>
				<td>999</td>
				<td>
					<img src="../Images/Stars/star_level3.gif" alt="" />
					<img src="../Images/Stars/star_level2.gif" alt="" />
					<img src="../Images/Stars/star_level1.gif" alt="" />
				</td>
				<td class="icons">
					<div id="editUserGroup" class="Ico_edit" title="编辑"></div>
					<div class="Ico_del" title="删除"></div>
				</td>
			</tr>
	</tbody>
	<tfoot><tr><td colspan="7" class="left"><div class="grayBg"><input id="addUserGroup" type="button" value="添加用户组" /></div></td></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Button();
//编辑用户组
$i('editUserGroup').onclick = function(){
	top.Sio.Alert({width:400,height:300,src:'',title:'编辑用户组',button:false});
}
//添加用户组
$i('addUserGroup').onclick = function(){
	top.Sio.Alert({width:500,height:300,src:'',title:'添加用户组',button:false});
}

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
