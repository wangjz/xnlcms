<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.tables{ margin:5px 0 0 0}
</style>
</head>
<body>
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_restore" title="还原" onclick="restoreSelectNode()"></div>
	<div class="Ico_del" title="删除" onclick="deleteSelectNode()"></div><div class="line"></div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="2%"></td>
			<td width="38%">栏目名称</td>
			<td width="25%">栏目索引</td>
			<td width="25%">所属模型</td>
			<td width="10%" style="padding:0 0 0 20px">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
			<xnl:repeater sqlcommand="SELECT nodeid,nodename,indexid,indexname, depth,arrChildID,state from sn_nodes group by depth,parentid,nodeid,indexid,nodename,state,arrChildID,RootID,indexname  having state=1 and RootID=@siteNodeid order by depth,indexid"><attrs><attr name="siteNodeid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs><itemtemplate>
                <tr>
					<td><input type="checkbox" name="NodeId" id="node_{$nodeid}" value="{$nodeid}" onclick="setChildNodeChecked(this,'{$arrChildID}')"/><input  type="hidden" value="{$arrChildID}" id="child_{$nodeid}" /></td>
					<td><a href="channel_modifyChannel.aspx?nodeid={$nodeid}"><xnl:for forstart="1" forend="{$depth}"><forItem>&nbsp;&nbsp;&nbsp;&nbsp;</forItem></xnl:for>└ {$nodename}</a><a href="#"></a></td>
					<td>{$indexname}</td>
					<td><CMS.Manage:Common action="getmodelname" nodeid="{$nodeid}"></CMS.Manage:Common></td>
					<td class="icons">
						<a href="channel_action.aspx?action=restore&nodeid={$nodeid}"><div class="Ico_restore" title="还原"></div></a>
						<div class="Ico_del" title="删除" onclick="deleteNode('{$nodeid}')"></div>
					</td>
				</tr></itemtemplate></xnl:repeater>
			<!--#</channelsItem></CMS.Manage:channels>#-->
	</tbody>
	<tfoot><tr><td colspan="6"></td></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Button();

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
function deleteNode(nodeId)
{
	if(confirm("确定删除吗？此栏目及栏目内容都将被删除，且不能恢复!"))
	{
		location.href="channel_action.aspx?action=delete&nodeid="+nodeId;
	}
}

function restoreSelectNode()
{
		var nodeList=document.getElementsByName("nodeId"),
			node_arr=[];
		Sio.each(nodeList,function(i,e){
			if(e.checked) node_arr.push(e.value);
		});
		if(node_arr.length==0){alert("请选择栏目");return;}
		location.href="channel_action.aspx?action=restore&nodeid="+node_arr.toString();
}
function setChildNodeChecked(checkObj,arrStr)
{
	if(Sio.trim(arrStr)=="")return;
	var node_arr=arrStr.split(",");
	if(node_arr.length==0)return;
	Sio.each(node_arr,function(i,e){
		var nodeObj=$i("node_"+e);
		if(!nodeObj)return;
		nodeObj.checked=checkObj.checked;
		var childStr=$i("child_"+e).value;
		if(Sio.trim(childStr)!=""){
			setChildNodeChecked(checkObj,childStr);
		}
	});
	
}
function deleteSelectNode()
{
	if(confirm("确定删除吗？此栏目及栏目内容都将被删除，且不能恢复!"))
	{
		var nodeList=document.getElementsByName("nodeId"),
			node_arr=[];
		Sio.each(nodeList,function(i,e){
			if(e.checked) node_arr.push(e.value);
		});
		if(node_arr.length==0){alert("请选择栏目");return;}
		location.href="channel_action.aspx?action=delete&nodeid="+node_arr.toString();
	}
}
</script>
</body>
</html> 
