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
	<div class="Ico_del" title="删除栏目组栏目" onclick="deleteSelect()"></div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="2%"></td>
			<td width="18%">栏目组名称</td>
			<td width="70%">栏目名称</td>
			<td width="10%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<XNL:repeater groupid="{&groupid}" sqlCommand="select a.ngs_id,a.ngs_nodeid,c.ng_name,b.NodeName,a.ngs_indexId from sn_nodeGroups a,sn_nodes b,sn_nodeGroup c where a.ngs_id=@groupid and a.ngs_nodeid=b.nodeid and a.ngs_id=c.ng_id"><attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
			<itemTemplate>
				<tr>
					<td><input type="checkbox" name="gn"  value="{$ngs_nodeid}"/></td>
					<td>{$ng_name}</td>
					<td>{$nodename}</td>
					<td class="icons">
						<a href="channel_action.aspx?action=deletegroupnode&groupid={$ngs_id}&nodeid={$ngs_nodeid}"><div class="Ico_del" title="删除栏目组栏目"></div></a>
					</td>
				</tr>
			</itemTemplate>
		</XNL:repeater>
	</tbody>
	<tfoot><tr><td colspan="4"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Button();

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
function deleteSelect()
{
	var listObj=document.getElementsByName("gn"),
		select_arr=[],
		nodeId_arr=[];
	Sio.each(listObj,function(i,e){
		if(e.checked){
			nodeId_arr.push(e.value);
			select_arr.push(e);
		}
	});
	if(select_arr.length==0)
	{
		alert("没有选择任何项");return;
	}
	document.location.href="channel_action.aspx?action=deletegroupnode&groupid={&groupid}&nodeid="+nodeId_arr.join(",");
}
</script>
</body>
</html> 
