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
html {overflow-x:hidden;_overflow-y:auto; }
body{ margin:0 10px;}
</style>
</head>
<body>
<form method="post" action="M_model_action.aspx?action=setStyle&id={&id}">
<XNL:repeater DescriptId="{&id}" sqlcommand="select * from SN_ModelDescript where DescriptId=@DescriptId">
<itemTemplate>
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody id="ctrl_cmd">
		<tr>
			<td width="125px">字段名称：</td>
			<td><input name="FieldName" type="text" id="FieldName" value="{$FieldName}" readonly="readonly"/><span class="required">*</span></td>
		</tr>
		<tr>
			<td>显示名称：</td>
			<td><input name="DisplayName" type="text" id="DisplayName" value="{$DisplayName}"/><span class="required">*</span></td>
		</tr>
		<tr>
			<td>显示帮助提示：</td>
			<td><input name="helpText" type="text" id="helpText" value="{$helpText}"/></td>
		</tr>
		<tr>
			<td>在提交表单中显示：</td>
			<td>
				<XNL:if>
					<ifItem value="{$isvisible}" test="True">
						<if>
							<input name="isVisible" id="isVisible0" type="radio" value="1" checked="checked" /><label for="isVisible0">显示</label> 
							<input name="isVisible" id="isVisible1" type="radio" value="0" /><label for="isVisible1">不显示</label> 
						</if>
						<else>
							<input name="isVisible" id="isVisible3" type="radio" value="1" /><label for="isVisible3">显示</label> 
							<input name="isVisible" id="isVisible4" type="radio" value="0" checked="checked" /><label for="isVisible4">不显示</label> 
						</else>
					</ifItem>
				</XNL:if>
			</td>
		</tr>
		<tr>
			<td>在列表显示：</td>
			<td>
				<XNL:if>
					<ifItem value="{$IsShowOnList}" test="1">
						<if>
							<input name="IsShowOnList" id="IsShowOnList0" type="radio" value="1" checked="checked" /><label for="IsShowOnList0">显示</label> 
							<input name="IsShowOnList" id="IsShowOnList1" type="radio" value="0" /><label for="IsShowOnList1">不显示</label> 
						</if>
						<else>
							<input name="IsShowOnList" id="IsShowOnList3" type="radio" value="1" /><label for="IsShowOnList3">显示</label>
							<input name="IsShowOnList" id="IsShowOnList4" type="radio" value="0" checked="checked" /><label for="IsShowOnList4">不显示</label>
						</else>
					</ifItem>
				</XNL:if>
			</td>
		</tr>
		<tr>
			<td valign="top">显示设置：</td>
			<td><textarea name="InputTypeSet" id="InputTypeSet" cols="75" rows="10">{[encodehtml('{$InputTypeSet}')]}</textarea></td>
		</tr>
		<tfoot>
			<tr>
				<td colspan="2" align="center"><div class="grayBg"><input name="提交" onclick="javascript:top.closeAlert()" type="submit" value="提交"/></div></td>
			</tr>
		</tfoot>
	</tbody>
</table>
</itemTemplate>
</XNL:repeater>
</form>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html> 
