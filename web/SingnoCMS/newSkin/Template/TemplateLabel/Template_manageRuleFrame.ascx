<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
#ctrl_cmd input.X_input_text{ width:400px}
</style>
</head>
<body>
<xnl:if><ifItem test="" value="{&projectId}"><if>请选择要操作的栏目</if><else>
<form id="form1" action="Template_action.aspx?action=modifyRule" method="post">
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody id="ctrl_cmd">
		<xnl:repeater  sqlcommand="select ChannelFilePathRule ,ContentFilePathRule from SN_TemplateMatch where NodeID={&nodeid} and ProjectID={&projectId}">
			<itemTemplate>
			<tr>
				<td width="150px">栏目页地址生成规则：</td>
				<td width="420px"><input name="channelPathRule" type="text" id="channelPathRule" value="{$ChannelFilePathRule}" size="50" maxlength="200" /></td>
				<td><input type="button" id="madeChannelRule" value="构造规则" /></td>
			</tr>
			<tr>
				<td>内容页地址生成规则：</td>
				<td>
					<input name="contentPathRule" type="text" id="contentPathRule" value="{$contentFilePathRule}" size="50" maxlength="200" />
					<input name="projectid" type="hidden" id="projectid" value="{&projectid}" />
					<input name="nodeid" type="hidden" id="nodeid" value="{&nodeid}" />
				</td>
				<td><input type="button" id="madeConetentRule" value="构造规则" /></td>
			</tr>
			<tr>
				<td colspan="3"><div class="grayBg"><input type="submit" name="button" id="button" value="保存设置" /></div></td>
			</tr>
			</itemTemplate>
		</xnl:repeater>
	</tbody>
</table>
</form></else></ifItem>
</xnl:if>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

var $channelPathRule = $i('channelPathRule'),
	$contentPathRule = $i('contentPathRule'),
	$madeChannelRule = $i('madeChannelRule'),
	$madeConetentRule = $i('madeConetentRule');
$madeChannelRule.onclick = function(){
	top.Sio.Alert({width:650,height:210,title:'',src:'TemplateLabel/Template_configCreateRule.html',args:$channelPathRule.value,okFn:function(e){
		$channelPathRule.value = e;
	}});
};
$madeConetentRule.onclick = function(){
	top.Sio.Alert({width:650,height:210,title:'',src:'TemplateLabel/Template_configCreateRule.html',args:$contentPathRule.value,okFn:function(e){
		$contentPathRule.value = e;
	}});
};
</script>
</body>
</html>
