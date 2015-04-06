<xnl:set><attrs><attr name="groupname"><xnl:repeater sqlCommand="select cg_name from sn_contentGroup where cg_id={&groupid}"><itemtemplate>{$cg_name}</itemtemplate></xnl:repeater></attr></attrs></xnl:set><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	<div class="Ico_del" title="删除所选" onClick="deleteSelect()"></div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="2%"></td>
			<td width="18%">内容组名称</td>
			<td width="35%">内容标题</td>
			<td width="35%">栏目名称</td>
			<td width="10%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<xnl:contents groupcontent="{!groupname}"><attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
			<contents.item>
				<tr>
					<td><input type="checkbox" name="gn"  value="{&groupid}"/></td>
					<td>{!groupname}</td>
					<td><a href="../Content/Content_AddContent.aspx?action=modify&nodeid={@contents.channelid}&contentId={@contents.id}">{@contents.title}</a></td>
					<td><a href="../Channel/channel_modifyChannel.aspx?nodeid={@contents.channelid}">{@contents.channelname}</a></td>
					<td class="icons">
						<a href="content_action.aspx?action=deletegroupinfo&groupid={&groupid}&nodeid={@contents.channelid}&infoid={@contents.id}"><div class="Ico_del" title="删除栏目组栏目"></div></a>
					</td>
				</tr>
			</contents.item>
		</xnl:contents>
	</tbody>
	<tfoot><tr><td colspan="5"></td></tr></tfoot>
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
	document.location.href="content_action.aspx?action=deletegroupinfo&groupid={&groupid}&nodeid="+nodeId_arr.join(",");
}
</script>
</body>
</html> 
