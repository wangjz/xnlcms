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
<form action="AdminUser_action.aspx?action=createRole" method="post" id="form1" >
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">角色名称：</td>
			<td><input type="text" name="roleName" id="roleName" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">备注：</td>
			<td><textarea name="desc" id="desc" cols="60" rows="5"></textarea></td>
		</tr>
		<tr>
			<td>站点权限设置：</td>
			<td id="sites">
            <xnl:repeater sqlcommand="select a.SiteId,b.nodeid,b.nodename from sn_sites as a,sn_nodes as b where a.nodeid=b.nodeid">
            <itemTemplate><input type="checkbox" id="site{$nodeid}" name="site{$siteid}" value="1" title="{$nodename}" onclick="setSiteRight(this)" />{$nodename}</itemTemplate>
            </xnl:repeater>
			</td>
            <input id="siteRights" name="siteRights" type="hidden" value="" />
		</tr>
		<tr>
			<td valign="top">系统权限设置：</td>
			<td id="permisList">
				<div class="permisHead"><input type="checkbox" name="permisItem" id="permisItem0"/><label for="permisItem0">全选</label></div>
                <CMS.Manage:Rights type="system">
                <Rights.Item><Right.Yes></Right.Yes><Right.No><div class="permisList"><input type="checkbox" name="systemRights" id="permisItem{@right.code}"/><label for="permisItem{@right.code}">{@right.name}</label></div></Right.No>
                </Rights.Item>
                </CMS.Manage:Rights>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="创建角色" /></div></td>
		</tr>
	</tfoot>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
//设置权限
Sio.addEvent(Sio.get('#sites>a'),'click',function(e){
	top.Sio.Alert({width:630,height:200,title:'设置权限',src:'User/M_User_permis.html',okFn:function(){
		
	},closeFn:function(){
		
	}});
});

var _permisList = Sio.get('#permisList>div.permisList>input[type=checkbox]');
//批量设置
$i('permisItem0').onclick = function(){
	if($i('permisItem0').checked){
		Sio.each(_permisList,function(i,e){
			e.checked = 'checked';
		});
	}else{
		Sio.each(_permisList,function(i,e){
			e.checked = '';
		});
	}
}

</script>
<script type="text/javascript">
var siteChannelRights_arr=new Array();//存放所有站点权限的数组
var siteRight_arr=new Array();  //站点权限数组
var channelRight_arr=new Array();
var channel_arr=new Array();
function setSiteRight(obj)
{
	var siteNodeId=obj.id.substr(4);
	if(obj.checked)
	{
		top.Sio.Alert({width:800,height:500,src:"AdminUser/AdminUser_siteNodeRights.aspx?rootid="+siteNodeId,title:"设置站点和栏目权限",okFn:function(v){
		//处理得到的权限
		siteChannelRights_arr.push({id:siteNodeId,siteRightObj:v});
		var siteAllRightStr="<root>";
		setSiteAllRight();
},closeFn:function(){
	obj.checked=false;
}});
	}
	else
	{
		Sio.each(siteChannelRights_arr,function(i,e){
			if(e.id==siteNodeId) e.splice(i,1)
		});
		setSiteAllRight();
	}
}

function setSiteAllRight()
{
	var siteAllRightStr="<root>";
	Sio.each(siteChannelRights_arr,function(i,e){
		siteAllRightStr += setSiteRightXML(e);
	});
	siteAllRightStr+="</root>";	
	$i("siteRights").value=siteAllRightStr;
	if(siteAllRightStr=="<root></root>")document.getElementById("siteRights").value="";
}
function setSiteRightXML(v)
{
		var obj=v.siteRightObj;
		siteRight_arr=obj.siteRights;
		channelRight_arr=obj.channelRights;
		channel_arr=obj.channels;
		var siteRightStr=siteRight_arr.toString();
		var siteChannelRightStr="<site id=\""+v.id+"\">"+"<siteRights>"+siteRightStr+"</siteRights>";
		Sio.each(channel_arr,function(i,e){
			var _str="<node id=\""+ e +"\"><nodeRights>"+channelRight_arr.toString()+"</nodeRights></node>";
			siteChannelRightStr += _str;
		});
		siteChannelRightStr+="</site>";
		return siteChannelRightStr;
}
</script>
</body>
</html>