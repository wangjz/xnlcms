<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/User.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JS/calendar.min.js"></script>
<style type="text/css">
.tables td{text-align:center;}
.tables td.left{ text-align:left}
</style>
</head>
<body>
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_lock" title="锁定"></div>
	<div class="Ico_unlock" title="解锁"></div>
	<div class="Ico_userGroup" title="设置用户组"></div>
	<div class="Ico_del" title="删除"></div><div class="line"></div>
	<div class="Ico_addUser" id="addUser" title="添加用户"></div><div class="line"></div>
	<div id="searchBar" class="_hover">
		<div class="input"><input name="keyWord" type="text" id="keywords" value="{&keyWord}"/></div>
		<div class="cmd"><input type="button" value="搜索" onclick="javascript:toSearch()" /></div>
		<div class="more" id="moreSearch"></div>
	</div>
	<div id="searchMenu">
		<ul class="blue u">高级搜索选项</ul>
		<ul>
			<li>
				<div class="fl">所属用户组：</div>
				<div class="fr">
					<select id="userGroup" name="userGroup">
						<option>新手上路</option>
						<option>注册会员</option>
						<option>中级会员</option>
						<option>高级会员</option>
						<option>金牌会员</option>
						<option>元老会员</option>
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
				<div class="fr" id="startDate"><!--<input name="DateFrom" type="text" id="DateFrom" value="" onClick="showcalendar(event, this)"/>--></div>
			</li>
			<li>
				<div class="fl">时间至：</div>
				<div class="fr" id="endDate"><!--<input name="DateTo" type="text" id="DateTo" value="" onClick="showcalendar(event, this)"/>--></div>
			</li>
		</ul>
	</div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
		<thead>
			<tr>
				<td width="2%"></td>
				<td class="left" width="14%">登录名</td>
				<td width="14%">显示名</td>
				<td width="14%">用户组</td>
				<td width="14%">创建日期</td>
				<td width="14%">最后登陆日期</td>
				<td width="12%">用户积分</td>
				<td width="16%" align="center">操作</td>
			</tr>
		</thead>
		<tbody id="ctrl_cmd">
			<tr>
				<td><input type="checkbox" name="classListItem" id="" /></td>
				<td class="left"><a href="#">admin</a></td>
				<td>admin</td>
				<td>新手上路</td>
				<td>2010-01-01</td>
				<td>2010-01-01</td>
				<td>12</td>
				<td class="icons">
					<div class="Ico_lock" title="锁定"></div>
					<div class="Ico_unlock" title="解锁"></div>
					<div class="Ico_userGroup" title="设置用户组"></div>
					<div id="editUser" class="Ico_edit" title="编辑"></div>
					<div class="Ico_del" title="删除"></div>
				</td>
			</tr>
	</tbody>
	<tfoot><tr><td colspan="8"></td></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Text('searchMenu');

new Sio.Calendar({id:'DateFrom',renderTo:'startDate',align:'right',width:160,addTime:true});

new Sio.Calendar({id:'DateTo',renderTo:'endDate',align:'right',width:160});

//获取值
function getVal(id){
	return $i(id).value;
};
//开始搜索
function toSearch(){
	
};
//添加用户
$i('addUser').onclick = function(){
	top.Sio.Alert({width:400,height:400,src:'User/M_User_addUser.html',title:'添加用户',button:false});
}

//编辑用户
$i('editUser').onclick = function(){
	top.Sio.Alert({width:400,height:300,src:'',title:'编辑用户',button:false});
}

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
