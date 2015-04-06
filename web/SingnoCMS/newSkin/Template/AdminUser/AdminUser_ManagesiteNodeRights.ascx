<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>站点权限</title>
<script type="text/javascript">
var siteRight_arr=new Array();  //站点权限数组
var channelRight_arr=new Array();
var channel_arr=new Array();
//站点权限对象:站点权限数组，栏目权限数组 ，栏目数组
//设置站点权限
var SioReturnValue={siteRights:siteRight_arr,channelRights:channelRight_arr,channels:channel_arr};
function setSiteRight(obj)
{
	var valueObj={isAdd:false,index:-1};
	var v=obj.value;
	for(var i=0;i<siteRight_arr.length;i++)
	{
		if(siteRight_arr[i]==v)
		{
			valueObj.isAdd=true;
			valueObj.index=i;
			break;
		}
	}
	if(obj.checked) //添加进数组
	{
		if(!valueObj.isAdd)siteRight_arr.push(v);
	}
	else //从数组移除
	{
		if(valueObj.isAdd)siteRight_arr.splice(valueObj.index,1);
	}
}

//添加栏目权限
function setNodeRight(obj)
{
	var valueObj={isAdd:false,index:-1};
	var v=obj.value;
	for(var i=0;i<channelRight_arr.length;i++)
	{
		if(channelRight_arr[i]==v)
		{
			valueObj.isAdd=true;
			valueObj.index=i;
			break;
		}
	}
	if(obj.checked) //添加进数组
	{
		if(!valueObj.isAdd)channelRight_arr.push(v);
	}
	else //从数组移除
	{
		if(valueObj.isAdd)channelRight_arr.splice(valueObj.index,1);
	}
}

function setNodeList(obj)
{
	var valueObj={isAdd:false,index:-1};
	var v=obj.value;
	for(var i=0;i<channel_arr.length;i++)
	{
		if(channel_arr[i]==v)
		{
			valueObj.isAdd=true;
			valueObj.index=i;
			break;
		}
	}
	if(obj.checked) //添加进数组
	{
		if(!valueObj.isAdd)channel_arr.push(v);
	}
	else //从数组移除
	{
		if(valueObj.isAdd)channel_arr.splice(valueObj.index,1);
	}
}
</script>
</head>
<body>
<CMS.Manage:Rights type="site" roleId="{&roleId}" siteId="{&rootid}">站点权限:</br><Rights.Item><Right.Yes><input name="siteRights" type="checkbox" title="{@right.name}" onclick="setSiteRight(this)" value="{@right.code}" checked="checked"/>{@right.name}</Right.Yes><Right.No>
<input type="checkbox" name="siteRights" value="{@right.code}" title="{@right.name}" onclick="setSiteRight(this)"/>{@right.name}</Right.No></Rights.Item>
</CMS.Manage:Rights>
</br>
栏目权限:
</br>
<CMS.Manage:Rights type="channel" roleId="{&roleId}" nodeId="{&rootid}">
<Rights.Item><Right.Yes><input name="channelRights" type="checkbox" title="{@right.name}" onclick="setNodeRight(this)" value="{@right.code}" checked="checked"/>{@right.name}</Right.Yes><Right.No><input type="checkbox" name="channelRights" value="{@right.code}" title="{@right.name}" onclick="setNodeRight(this)"/>{@right.name}</Right.No></Rights.Item>
</CMS.Manage:Rights>
</br>
选择要设置权限的栏目:</br>
<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;" nodeid="{&rootid}" sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid">
<channelsItem><li>{@depthTag}┣<xnl:if><ifItem value="True"><attrs><attr type="String" name="test"><CMS.Manage:Common action="isChannelRight" roleId="{&roleId}" NodeId="{$nodeid}"></CMS.Manage:Common></attr></attrs><if><input name="channel" type="checkbox"  onclick="setNodeList(this)" value="{$nodeid}" checked="checked"/></if><else><input name="channel" type="checkbox" value="{$nodeid}"  onclick="setNodeList(this)"/></else></ifItem></xnl:if>{$nodename}</li></channelsItem></CMS.Manage:channels>
</body>
</html>