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
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span>
	<div class="Ico_del" title="移到回收站" onclick="removeSelectNode()"></div>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="2%"></td>
			<td width="35%">栏目名称</td>
			<td width="16%">栏目索引</td>
			<td width="16%">所属模型</td>
			<td width="16%">栏目统计</td>
			<td width="15%" align="center">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<xnl:channels isallchild="true" isself="true"><attrs><attr name="channelid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
        <channels.Item>
				<tr>
					<td><input type="checkbox" name="nodeId" id="" value="{@channels.id}"/></td>
					<td><a href="channel_modifyChannel.aspx?nodeid={@channels.id}"><xnl:for forstart="1" forend="{@channels.depth}"><foritem>&nbsp;&nbsp;&nbsp;&nbsp;</foritem></xnl:for>└ {@channels.name}</a></td>
					<td>{@channels.indexname}</td>
					<td><CMS.Manage:Common action="getmodelname" nodeid="{@channels.id}"></CMS.Manage:Common></td>
					<td>栏目统计</td>
					<td class="icons">
						<a href="../Content/Content_AddContent.aspx?action=add&contentId=&nodeid={@channels.id}"><div class="Ico_addInfo" title="快速添加信息"></div></a>
						<a href="Channel_addChannel.aspx?nodeid={@channels.id}"><div class="Ico_addRSClass" title="添加子栏目"></div></a>
						<a href="Channel_Action.aspx?action=remove&nodeid={@channels.id}"><div class="Ico_del" title="移到回收站"></div></a>
                        <a href="javascript:setSort('up','{@channels.id}')"><div class="Ico_toUp" title="上移"></div></a> 
                        <a href="javascript:setSort('down','{@channels.id}')"><div class="Ico_toDown" title="下移"></div></a>
					</td>
				</tr></channels.Item></xnl:channels>
	</tbody>
	<tfoot><tr><td colspan="6"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Button();

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
function removeSelectNode()
{
	var nodeList=document.getElementsByName("nodeId");
	var len=nodeList.length;
	var node_arr=[];
	Sio.each(nodeList,function(i,e){
		if(e.checked) node_arr.push(e.value);
	});
	if(node_arr.length==0){alert("请选择栏目");return;}
	location.href="channel_action.aspx?action=remove&nodeId="+node_arr.toString();
}
function setSort(type,nodeid)
{
	var moveStr=(type=="up"?"上移":"下移");
	top.Sio.Alert({width:300,height:170,title:'栏目'+moveStr,src:'channel/Channel_setSort.aspx?action='+type+"&nodeid="+nodeid,button:false,okFn:function(){
		
	}});

}
</script>
</body>
</html> 
