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
body{ margin:0 10px}
</style>
</head>
<body>
<form action="Model_action.aspx?action=addfield" method="post" id="form1" onsubmit="return submitTest()">
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody>
		<tr>
			<td>字段名：</td>
			<td><input type="text" value="" name="fieldName" id="fieldName"/><span class="required">*</span></td>
		</tr>
		<tr>
			<td>数据类型：</td>
			<td>
				<select id="DataStyle" name="DataStyle" onchange="setStyleData(this)">
				  <option value="NVarChar">文本</option>
					<option value="NText">备注</option>
					<option value="Integer">整数</option>
                    <option value="Decimal">小数</option>
					<option value="Boolean">是/否</option>
					<option value="DateTime">日期/时间</option>
					<option value="Money">货币</option>
				</select>
		    </td>
		</tr>
<tr class="hide" id="DecimalRow">
			<td>小数位数：</td>
			<td><input name="DecimalLen" type="text" id="DecimalLen" value="2" size="2" maxlength="2" /></td>
		</tr>
		<tr class="" id="dataLenRow">
			<td>数据长度：</td>
			<td><input name="dataLength" type="text" value="255" size="4" maxlength="5" id="dataLength"/><span class="required">*</span></td>
		</tr>
        
		<tr>
			<td>数据库默认值：</td>
			<td>
				<input type="text" value="" name="DefaultValue" id="DefaultValue"/>
	    		<input name="modelName" type="hidden" id="modelName" value="{&modelname}" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center"><input type="submit"  value="添加" id="submitBtn"/></td>
		</tr>
	</tbody>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
function submitTest()
{
	var fieldName=Sio.trim($i('fieldName').value);
	if(fieldName=="")
	{
		alert("字段名称不能为空!");
		$i('fieldName').focus();
		return false;
	}
	var dataStyleObj=$i('DataStyle');
	var datalenObj=$i('dataLength');
	if(dataStyleObj.value=="NVarChar"&&Sio.trim(datalenObj.value)=="")
	{
		alert("文本类型字段长度没有设置!");
		datalenObj.focus();
		return false;
	}
	if(dataStyleObj.value=="Decimal"&&Sio.trim($i('DecimalLen').value)=="")
	{
		alert("没有设置小数位数!");
		$i('DecimalLen').focus();
		return false;
	}
}
function hideRow()
{
	$i('DecimalRow').className='hide';
}
function showRow()
{
	$i('DecimalRow').className='';
}
function setStyleData(selectObj)
{
	switch(selectObj.value)
	{
		case "NVarChar":
			hideRow();
			$i('dataLenRow').className='';
			$i('dataLength').value=255;
			$i('DefaultValue').value='';
			break;
		case "NText":
			hideRow();
			$i('dataLength').value=16;
			$i('dataLenRow').className='hide';
			break;
		case "Integer":
		case "Boolean":
			hideRow()
			$i('dataLength').value=1;
			$i('dataLenRow').className='hide';
			$i('DefaultValue').value='';
			break;
		case "Decimal":
			showRow();
			$i('dataLength').value=1;
			$i('dataLenRow').className='hide';
			$i('DefaultValue').value='';
			break;
		case "DateTime":
			hideRow();
			$i('dataLength').value=8;
			$i('dataLenRow').className='hide';
			$i('DefaultValue').value='[Current]';
			break;
		case "Money":
			$i('DecimalRow').className='hide';
			$i('dataLength').value=8;
			$i('dataLenRow').className='hide';
			$i('DefaultValue').value='';
			break;
		default:
				$i('numberRow').className='hide';
				$i('DecimalRow').className='hide';
				$i('dataLenRow').className='hide';
				$i('DefaultValue').value='hide';
			break;
			
	}
}
</script>
</body>
</html> 
