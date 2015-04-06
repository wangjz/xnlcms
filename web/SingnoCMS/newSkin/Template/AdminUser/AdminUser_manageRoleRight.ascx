<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/User.css" rel="stylesheet" type="text/css" />
<link href="../JS/css/sio.css" rel="stylesheet" type="text/css" />
<SCRIPT type="text/javascript" src="../JS/Sio.min.js" ></script>

</head>
<body>
<div class="stage">
<form action="AdminUser_action.aspx?action=modifyRole" method="post" id="form1" >
<div id="createRole">
	<ul>
		<li>
			<div class="title fl">角色名称：</div>
			<div class="info fl">{&roleName}</span>
			  <input name="roleId" type="hidden" id="roleId" value="{&roleId}" />
			</div>
		</li>
		<li class="content">
			<div class="title fl">备注：</div>
			<div class="info fl">
				<div class="tooltip"><textarea name="desc" id="desc"></textarea></div>
			</div>
		</li>
		<div class="title fl">系统权限设置：</div>
			<div>
            <CMS.Manage:Rights type="system" roleId="{&roleId}">
                <Rights.Item><Right.Yes><input name="systemRights" type="checkbox" title="{@right.name}" value="{@right.code}" checked="checked"/>{@right.name}</Right.Yes><Right.No>
                  <input type="checkbox" name="systemRights2" value="{@right.code}" title="{@right.name}"/>
                  {@right.name}</Right.No>
                </Rights.Item>
                </CMS.Manage:Rights>
			</div>
	</ul>
	<fieldset> 
		<legend class="u">站点权限设置：</legend> 
		<div id="sitePermis">
			<ul>
            <xnl:repeater sqlcommand="select a.SiteId,b.nodeid,b.nodename from sn_sites as a,sn_nodes as b where a.nodeid=b.nodeid">
            <itemTemplate><xnl:if><ifItem value="True"><attrs><attr type="String" name="test"><CMS.Manage:Common action="isSiteRight" roleId="{&roleId}" siteNodeId="{$nodeid}"></CMS.Manage:Common></attr></attrs><if><input name="site{$siteid}" type="checkbox" id="site{$nodeid}" title="{$nodename}" onclick="setSiteRight(this)" value="1" checked="checked" /></if><else><input type="checkbox" id="site{$nodeid}" name="site{$siteid}" value="1" title="{$nodename}" onclick="setSiteRight(this)" /></else></ifItem></xnl:if>{$nodename}</itemTemplate>
            </xnl:repeater>
			</ul>
		</div>
        <input id="siteRights" name="siteRights" type="hidden" value="" />
	</fieldset>
	<ul>
		<li><div class="center">
		  <input name="提交" type="submit" value="提交"/>
	  </div></li>
	</ul>
</div>
</form>
</div>
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
		top.Sio.Alert({width:800,height:600,src:"AdminUser/AdminUser_ManagesiteNodeRights.aspx?rootid="+siteNodeId+"&roleId={&roleId}&roleName={&rolename}",title:"设置站点和栏目权限",okFn:function(v){
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
		for(var i=0;i<siteChannelRights_arr.length;i++)
		{
			if(siteChannelRights_arr[i].id==siteNodeId)siteChannelRights_arr.splice(i,1);
		}
		setSiteAllRight();
	}
}

function setSiteAllRight()
{
	var siteAllRightStr="<root>";
	for(var i=0;i<siteChannelRights_arr.length;i++)
	{
		siteAllRightStr+=setSiteRightXML(siteChannelRights_arr[i]);
	}
	siteAllRightStr+="</root>";	
	document.getElementById("siteRights").value=siteAllRightStr;
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
		for(var i=0;i<channel_arr.length;i++)
		{
			var _str="<node id=\""+channel_arr[i]+"\"><nodeRights>"+channelRight_arr.toString()+"</nodeRights></node>";
			siteChannelRightStr+=_str;
		}
		siteChannelRightStr+="</site>";
		return siteChannelRightStr;
}
</script>
</body>
</html>