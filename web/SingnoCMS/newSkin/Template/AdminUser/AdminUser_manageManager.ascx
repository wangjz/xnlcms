<CMS.Manage:AdminLoginCheck><AdminLoginCheck.noItem><script>alert("没登陆");top.location.href="../default.html";</script></AdminLoginCheck.noItem></CMS.Manage:AdminLoginCheck>
<xnl:set><attrs><attr type="string" name="adminName"><CMS.Manage:Common action="getcuradmin"></CMS.Manage:Common></attr></attrs></xnl:set>
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
	<div class="Ico_lock" title="锁定"></div>
	<div class="Ico_unlock" title="解锁"></div><div class="line"></div>
	<div class="Ico_del" title="删除"></div><div class="line"></div>
	<div id="searchBar" class="_hover">
		<div class="input"><input name="keyWord" type="text" id="keywords" value="{&keyWord}"/></div>
		<div class="cmd"><input type="button" value="搜索" onClick="javascript:toSearch()" /></div>
		<div class="more" id="moreSearch"></div>
	</div>
	<div id="searchMenu">
		<ul class="blue u">高级搜索选项</ul>
		<ul>
			<li>
				<div class="fl">角色：</div>
				<div class="fr">
					<select name="" id="">
						<option value="">超级管理员</option>
						<option value="">普通管理员</option>
					</select>
				</div>
			</li>
			<li>
				<div class="fl">关键字：</div>
				<div class="fr">
					<select name="" id="">
						<option value="">登录名</option>
						<option value="">显示名</option>
					</select>
				</div>
			</li>
			<li>创建时间至最后登录时间：</li>
			<li>
				<div class="fl">时间起：</div>
				<div class="fr" id="startData"></div>
			</li>
			<li>
				<div class="fl">时间至：</div>
				<div class="fr" id="endData"></div>
			</li>
		</ul>
	</div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
		<thead>
			<tr>
				<td width="2%"></td>
				<td class="left" width="17%">管理员登录名</td>
				<td width="17%">管理员显示名</td>
                <td width="10%">角色</td>
				<td width="16%">创建日期</td>
				<td width="16%">最后登陆日期</td>
				<td width="9%">登陆次数</td>
				<td width="13%" align="center">操作</td>
			</tr>
		</thead>
		<tbody id="ctrl_cmd">
        <xnl:repeater sqlcommand="select a.LoginName,a.DisplayName,a.CreateDate,a.LastLoginDate,a.LoginCount,a.IsLocked,a.RoleId,b.RoleName from sn_admin as a LEFT JOIN  SN_AdminRole as b on a.roleid=b.id"><itemTemplate>
			<tr>
				<td><input type="checkbox" name="classListItem" id="" /></td>
				<td class="left"><a href="#">{$loginName}</a></td>
				<td>{$displayName}</td>
                 <td><xnl:if><ifItem test="{$roleid}" value="0"><if>超级管理员</if><else>{$rolename}</else></ifItem></xnl:if></td>
				<td>{$CreateDate}</td>
				<td>{$LastLoginDate} </td>
				<td>{$LoginCount}</td>
				<td class="icons">
					<!--<div class="Ico_lock" title="锁定"></div>
					<div class="Ico_unlock" title="解锁"></div>
					<div id="editManager" class="Ico_edit" title="编辑"></div>-->
					
                    <a href="javascript:alertModifyPassPage('{$loginName}')">重设密码</a>
                     <xnl:if><ifItem test="{!adminName}" value="{$loginName}"><else><a href="AdminUser_action.aspx?action=deleteAdmin&loginName={$loginName}"><div class="Ico_del" title="删除"></div></a><a href="javascript:alertModifyRolePage('{$loginName}')"><div class="Ico_edit" title="权限设置"></div></a><xnl:if><ifItem test="{$IsLocked}" value="1"><if><a href="AdminUser_action.aspx?action=unlockAdmin&loginName={$loginName}"><div class="Ico_unlock" title="解锁"></div></a></if><else><a href="AdminUser_action.aspx?action=lockAdmin&loginName={$loginName}"><div class="Ico_lock" title="锁定"></div></a></else></ifItem></xnl:if></else></ifItem></xnl:if> 
				</td>
			</tr> </itemTemplate>
             </xnl:repeater>
	</tbody>
	<tfoot><tr><td colspan="7"></td></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Text('searchMenu');

new Sio.Calendar({id:'DateFrom',renderTo:'startData',width:163,align:'right'});

new Sio.Calendar({id:'DateTo',renderTo:'endData',width:163,align:'right'});

//获取值
function getVal(id){
	return $i(id).value;
};
//开始搜索
function toSearch(){
	
};

/*//编辑管理员
$i('editManager').onclick = function(){
	top.Sio.Alert({width:400,height:300,src:'',title:'编辑管理员',button:false});
}*/

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');

function alertModifyPassPage(loginName)
{
	top.Sio.Alert({width:500,height:400,src:"AdminUser/AdminUser_modifyAdminPass.aspx?loginName="+loginName,title:"重设密码",okFn:function(){
		//location.reload();
},closeFn:function(){
	//location.reload();
}});
}

function alertModifyRolePage(loginName)
{
	top.Sio.Alert({width:500,height:400,src:"AdminUser/AdminUser_modifyAdminRole.aspx?loginName="+loginName,title:"设置角色",okFn:function(){
		//location.reload();
},closeFn:function(){
	//location.reload();
}});
}
</script>
</body>
</html> 
