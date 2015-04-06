<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<XNL:validator name="v1" autoloadjs="true"><validatorItem validatorgroup="1" formid="form1"  onsuccess="function(){}" onerror ="function(msg){alert(msg);}" wideword="false"></validatorItem></XNL:validator>
</head>
<body>
<form  method="post" action="model_action.aspx" name="form1" id="form1">
<XNL:form enableState="true" validatorName="v1" >
<xnl:repeater id="{&id}" sqlcommand="select ModelName,TableName,ItemName,ItemUnit,ItemIcon,Description from sn_model where modelid=@id"><itemtemplate>
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody>
		<tr>
			<td width="100px" align="center">模型名称：</td>
			<td colspan="2"><formItem name="modelName"><validatorItem defaultpassed="true">
<formValidator onfocus="请输入模型名称" oncorrect="输入正确" tipid="modelNameTipDiv"><attrs><attr name="onshow">输入模型名称,不能为空</attr></attrs></formValidator><inputValidator min="2" max="8" onerror="模型名称:输入错误，2-8个字符"></inputValidator><ajaxValidator type="get" url="model_action.aspx?action=checkModel&type=modify&modelId={&id}" data="{action:'checkModel',modelName:$('#modelName').val(),type:'modify',modelId:{&id}}" success="function(data){return($.trim(data)=='False')}" buttons="$('#Submit')" onerror="此模型名称已经存在" error="function(){alert('服务器没有返回数据，可能服务器忙，请重试');}",
></ajaxValidator></validatorItem>
<input name="modelName" type="text" id="modelName" value="{$ModelName}" /><span class="required" id="modelNameTipDiv">*<span class="grayBg">
</span></span></formItem></td>
		</tr>
		<tr>
			<td align="center">模型表名：</td>
			<td colspan="2">SN_U_ <input name="modelTableName" type="text" disabled="disabled" id="modelTableName" style="width:91px" value="{[replace('{$TableName}','SN_U_','')]}" readonly="readonly" /></td>
		</tr>
		<tr>
			<td align="center">项目名称：</td>
			<td colspan="2"><formItem name="itemName"><validatorItem defaultpassed="true">
<formValidator onshow="输入项目名称,不能为空,如文章、新闻、日志、信息" onfocus="请输入项目名称" oncorrect="输入正确" tipid="itemNameTipDiv"></formValidator>
<inputValidator min="1" max="8" onerror="输入错误，1-8个字符"></inputValidator></validatorItem><input name="itemName" type="text" id="itemName" value="{$ItemName}"/><span class="required" id="itemNameTipDiv">*</span></formItem></td>
		</tr>
		<tr>
			<td align="center">项目单位：</td>
			<td colspan="2"><formItem name="itemUnit"><validatorItem defaultpassed="true">
<formValidator onshow="输入项目单位,不能为空" onfocus="请输入项目单位,如“篇”" oncorrect="输入正确" tipid="itemUnitTipDiv"></formValidator>
<inputValidator min="1" max="8" onerror="输入错误，1-8个字符"></inputValidator>
</validatorItem><input name="itemUnit" type="text"  id="itemUnit" value="{$ItemUnit}"/><span class="required" id="itemUnitTipDiv">默认为“篇”</span></formItem></td>
		</tr>
		<tr>
			<td align="center">模型图标：</td>
			<td>
				<span class="fl"><a href="#"><img id="modelIco" src="../Images/icons/ico_01.gif" alt=""/></a></span>
				<div id="icoSelect" class="hide">
					<ul>
						<li><a href="#"><img src="../Images/icons/ico_01.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_02.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_03.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_04.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_05.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_06.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_07.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_08.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_09.gif" alt=""/></a></li>
						<li><a href="#"><img src="../Images/icons/ico_10.gif" alt=""/></a></li>
					</ul>
				</div>
				<span class="exg" style="line-height:38px">点击图片更换图标</span>
				<input name="modelIcon" type="hidden" id="modelIcoName" value="{$ItemIcon}" />
			</td>
			<td></td>
		</tr>
		<tr>
			<td align="center" valign="top">模型描述：</td>
			<td colspan="2"><textarea cols="70" name="modelNote" rows="8">{$Description}</textarea></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="hidden" name="action" value="modify" /><input name="Submit" type="submit" id="Submit" value="添加模型" />
			  <input name="modelid" type="hidden" id="modelid" value="{&id}" /><input name="srcModelName" type="hidden" id="srcModelName" value="{$ModelName}" />
			</div></td>
		</tr>
	</tfoot>
</table>
</itemtemplate></xnl:repeater>
</XNL:form>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/selectIcos.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
</script>
</body>
</html> 
