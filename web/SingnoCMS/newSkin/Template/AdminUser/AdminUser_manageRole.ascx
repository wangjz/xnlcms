<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/User.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
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
				<td width="28%">类型</td>
				<td width="62%">备注</td>
				<td width="8%" align="center">操作</td>
			</tr>
		</thead>
		<tbody id="ctrl_cmd">
        <xnl:repeater sqlcommand="select ID,RoleName,Descript,CreateDate from SN_AdminRole"><itemTemplate>
			<tr>
				<td><input type="checkbox" name="classListItem" id="" /></td>
				<td>{$roleName}</td>
				<td>{$Descript}</td>
				<td class="icons">
					<a href="AdminUser_manageRoleRight.aspx?roleName={$roleName}&roleId={$Id}"><div id="editRole" class="Ico_edit" title="编辑"></div></a>
					<a href="AdminUser_action.aspx?action=deleteRole&roleId={$Id}"><div class="Ico_del" title="删除"></div></a>
				</td>
			</tr>
            </itemTemplate></xnl:repeater>
	</tbody>
	<tfoot><tr><td colspan="4"></td></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
