<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="13%">模型表</td>
			<td width="15%">模型名称</td>
			<td width="8%">类型</td>
			<td width="10%">使用次数</td>
			<td width="13%">状态</td>
			<td width="13%">允许修改</td>
			<td width="16%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd"> <XNL:repeater pageName="page1" sqlCommand="select * from SN_model order by ModelStyle"><itemTemplate>
				<tr>
					<td>{$TableName}</td>
					<td>{$ModelName}</td>
					<td>{[iif('{$ModelStyle}'='0', '系统', '用户')]}</td>
					<td>{$useNumber}</td>
					<td>{[iif('{$State}'='0', '禁用', '开启')]} </td>
					<td>{[iif('{$ModelStyle}'='0', '否', '是')]}</td>
					<td>
						<a href="Model_Modify.aspx?id={$modelid}" class="blue">修改</a> | 
						<a href="javascript:manageTip('{$modelname}')" class="blue">管理字段</a> |<XNL:if><ifItem value="{$ModelStyle}" test="1"><if>	<XNL:if><ifItem value="{$useNumber}" test="0"><if><a href="javascript:delTip('{$modelId}')">删除</a></if></ifItem></XNL:if></if></ifItem></XNL:if>	</td>
				</tr></itemTemplate></XNL:repeater>
	</tbody>
	<tfoot><tr><td colspan="7"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
function manageTip(e){
	//location.href = 'M_Content_manageModelManage.aspx?modelName=' + escape(e);
	location.href = 'Model_ModelManage.aspx?modelName=' + escape(e);
}
function delTip(e){
	if(confirm('确实要删除吗？此模型包含的全部内容也将被全部删除！')) location.href('model_action.aspx?action=delete&modelId=' + e);
}
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
