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
<form method="post" action="model_action.aspx?action=setStyle&id={&id}" onsubmit="return submitHandle()">
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
					<ifItem value="{$isvisible}" test="1">
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
			<td>表单显示类型：</td>
			<td><xnl:set><attrs><attr name="InputType">{$InputType}</attr></attrs></xnl:set>
				<select name="formTemp" id="formTemp" style="width:120px">
					<option value="Text" {[iif('{$InputType}'='Text','selected="selected"','')]}>文本框（单行）</option>
					<option value="TextArea" {[iif('{$InputType}'='TextArea','selected="selected"','')]}>文本框（多行）</option>
					<option value="TextEditor" {[iif('{$InputType}'='TextEditor','selected="selected"','')]}>本文编辑器</option>
					<option value="CheckBox" {[iif('{$InputType}'='CheckBox','selected="selected"','')]}>复选列表</option>
					<option value="Radio" {[iif('{$InputType}'='Radio','selected="selected"','')]}>单选列表</option>
					<option value="SelectOne" {[iif('{$InputType}'='SelectOne','selected="selected"','')]}>下拉列表（单选）</option>
					<option value="SelectMultiple" {[iif('{$InputType}'='SelectMultiple','selected="selected"','')]}>下拉列表（多选）</option>
					<option value="Date" {[iif('{$InputType}'='Date','selected="selected"','')]}>日期选择</option>
                    <option value="DateTime" {[iif('{$InputType}'='DateTime','selected="selected"','')]}>日期/时间选择</option>
					<option value="Image" {[iif('{$InputType}'='Image','selected="selected"','')]}>图片</option>
					<option value="File" {[iif('{$InputType}'='File','selected="selected"','')]}>附件</option>
				</select>
			</td>
		</tr>
	</tbody>
</table>
<div id="formType">
	<ul>
		<li id="m_Text" class="hide">
		  <table class="forms" cellpadding="0" cellspacing="0">
		  <tbody>
		    <tr>
		      <td><table class="forms" cellpadding="0" cellspacing="0">
		        <tbody>
		          <tr>
		            <td width="125px">文本框大小：</td>
		            <td> 宽：
		              <input name="Text_width" type="text" class="unit" id="Text_width" value="0" />
		              px, 
		              高：
		              <input name="Text_height" type="text" class="unit" id="Text_height" value="0" />
		              px <span class="exg">(数值类型，0代表默认)</span></td>
		            </tr>
		          <tr>
		            <td>显示默认值：</td>
		            <td><input type="text" name="Text_value" id="Text_value" />
		             </td>
		            </tr>
		          </tbody>
		        </table></td>
		      </tr>
		  </tbody>
		  </table>
		</li>
		<li id="m_TextArea" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">文本域大小：</td>
						<td>
							宽：<input name="TextArea_width" type="text" class="unit" id="TextArea_width" value="0" />px, 
							高：<input name="TextArea_height" type="text" class="unit" id="TextArea_height" value="0" />px
							<span class="exg">(数值类型，0代表默认)</span>
						</td>
					</tr>
					<tr>
						<td valign="top">显示默认值：</td>
						<td><textarea name="TextArea_value" id="TextArea_value" cols="70" rows="3"></textarea></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_TextEditor" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">编辑器大小：</td>
						<td>
							宽：<input name="TextEditor_width" type="text" class="unit" id="TextEditor_width" value="0" />px, 
							高：<input name="TextEditor_height" type="text" class="unit" id="TextEditor_height" value="0" />px
							<span class="exg">(数值类型，0代表默认)</span>
						</td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_CheckBox" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">排列方向：</td>
						<td>
							<select name="CheckBox_direction" id="CheckBox_direction">
								<option value="Horizontal">垂直</option>
								<option value="Vertical">水平</option>
							</select>
						</td>
					</tr>
					<tr>
						<td valign="top">列数：</td>
						<td><input name="CheckBox_columns" type="text" class="unit" id="CheckBox_columns" value="1" /><span class="exg">(数值类型，0代表未设置此属性)</span></td>
					</tr>
					<tr>
						<td>设置选项数目：</td>
						<td><input type="text" name="CheckBox_options" id="CheckBox_options" class="unit" /> <input type="button" name="setOptions" value="设置" /></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_Radio" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">排列方向：</td>
						<td>
							<select name="Radio_direction" id="Radio_direction">
								<option value="Horizontal">垂直</option>
								<option value="Vertical">水平</option>
							</select>
						</td>
					</tr>
					<tr>
						<td valign="top">列数：</td>
						<td><input name="Radio_columns" type="text" class="unit" id="Radio_columns" value="1" /><span class="exg">(数值类型，0代表未设置此属性)</span></td>
					</tr>
					<tr>
						<td>设置选项数目：</td>
						<td><input type="text" name="Radio_options" id="Radio_options" class="unit"/> <input type="button" name="setOptions" value="设置" /></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_SelectOne" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">设置选项数目：</td>
						<td><input type="text" name="SelectOne_options" id="SelectOne_options" class="unit" /> <input type="button" name="setOptions" value="设置" /></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_SelectMultiple" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="125px">设置选项数目：</td>
						<td><input type="text" name="SelectMultiple_options" id="SelectMultiple_options" class="unit" /> <input type="button" name="setOptions" value="设置" /></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li id="m_Date" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
                 <tr>
		            <td width="125px">文本框大小：</td>
		            <td> 宽：
		              <input name="Date_width" type="text" class="unit" id="Date_width" value="0" />
		              px, 
		              高：
		              <input name="Date_height" type="text" class="unit" id="Date_height" value="0" />
		              px <span class="exg">(数值类型，0代表默认)</span></td>
	              </tr>
		          <tr>
			  <tr>
						<td width="125px">设置默认值：</td>
						<td><input type="text" name="Date_value" id="Date_value" /></td>
					</tr>
				</tbody>
			</table>
		</li>
        <li id="m_DateTime" class="hide">
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
                <tr>
		            <td width="125px">文本框大小：</td>
		            <td> 宽：
		              <input name="DateTime_width" type="text" class="unit" id="DateTime_width" value="0" />
		              px, 
		              高：
		              <input name="DateTime_height" type="text" class="unit" id="DateTime_height" value="0" />
		              px <span class="exg">(数值类型，0代表默认)</span></td>
	              </tr>
					<tr>
						<td width="125px">设置默认值：</td>
						<td><input type="text" name="DateTime_value" id="DateTime_value" /></td>
					</tr>
				</tbody>
			</table>
		</li>
	</ul>
</div>
<!--用来显示需要设置子项的容器-->
<div id="showItems"></div>
<div class="grayBg">
	<input name=""  type="submit" value="设置样式"/> 
	<!--隐藏字段，存储生成代码-->
	<input name="reset" type="button"  value="重置" onclick="location.reload();" id="reset" />
	 <input type="hidden" name="InputTypeSet" id="InputTypeSet" value='{$InputTypeSet}'/><xnl:set><attrs><attr name="inputtypeset">{$InputTypeSet}</attr></attrs></xnl:set>
</div>
</itemTemplate>
</XNL:repeater>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
var srcStyleStr='{!inputtypeset}';
var srcType='{!InputType}';
var option_array=[];
setStyleInput(srcStyleStr,srcType)
//选择表单显示样式
var _li = $i('formType').children[0].children;
$i('formTemp').onchange = function(){
	Sio.each(_li,function(i,e){
		e.className = 'hide';
	});
	if(this.value != 'Image' && this.value != 'File') $i('m_'+this.value).className = '';
	$i('showItems').innerHTML = '';
	if(this.value==srcType&&(this.value== 'CheckBox'||this.value == 'SelectMultiple'||this.value=="Radio"||this.value=="SelectOne"))
	{
		$i('showItems').innerHTML = createItem(this.value,option_array.length,option_array);
		Sio.Text('showItems');
	}
}

//设置子项序列
Sio.addEvent(Sio.get('#formType input[name=setOptions]'),'click',function(e){
	var num = Math.round(e.parentNode.parentNode.parentNode.children[0].value) || '0';
	$i('showItems').innerHTML = createItem($i('formTemp').value,num,$i('formTemp').value==srcType?option_array:null);
	Sio.Text('showItems');
});

function submitHandle()
{
	var _code = createInput();
	//赋值给隐藏字段
	$i('InputTypeSet').value = _code;
	return true;
}
//生成子项
function createItem(type,num,option_array){
		var check = (type == 'CheckBox'||type == 'SelectMultiple') ? '<input type="checkbox" name="setItems" id="" @checked/>' : '<input type="radio" name="setItems" id="" @checked/>';
	var code="";
	//构建表单序列
	if(typeof(num)=='number' && num != 0){
		for(var i=0;i<num;i++){
code +='<div class="contains"><div class="num">'+ (i+1) +'</div><div class="itemForm"><table cellpadding="0" cellspacing="5px"><tbody><tr><td width="100px">选项标题：</td><td><input type="text" name="" id="" value="'+(option_array==null?'':option_array[i].label)+'"/></td></tr><tr><td>选项值：</td><td><input type="text" name="" id="" value="'+(option_array==null?'':option_array[i].value)+'"/></td></tr><tr><td>选定状态：</td><td>'+ check.replace("@checked",(option_array==null?"":option_array[i].optionCheckd)) +' 选定</td></tr></tbody></table></div></div>';
		}
	}
	return code;
}

//生成代码
function createInput(){
	var type = $i('formTemp').value,
		width = '0',
		height = '0',
		dir = 'Horizontal',
		col = '1',
		val = '',
		label = '',
		opt = '',
		optLen = '',
		opts = '',
		temp = '';
	
	//分类别列出
	switch(type){
		case 'Text':
		case 'TextArea':
			width = $i(type +'_width').value;
			height = $i(type +'_height').value;
			opts = '<value>'+encodeHtml($i(type +'_value').value) +'</value>';
			break;
		case 'TextEditor':
			width = $i(type +'_width').value;
			height = $i(type +'_height').value;
			opts = '<value>'+ encodeHtml($i(type +'_value').value) +'</value>';
			break;
		case 'CheckBox':
		case 'Radio':
			dir = $i(type +'_direction').value;
			col = $i(type +'_columns').value;
			optLen = $i(type +'_options').value;
			if(optLen > 0){
				var tag = $i('showItems');
				for(var i=0;i<optLen;i++){
					var _table = tag.children[i].children[1].children[0].children[0],
						_tr = _table.children;
					//获取标题
					label = _tr[0].children[1].children[0].value;
					//获取值
					val = _tr[1].children[1].children[0].value;
					//获取选中状态
					checked = _tr[2].children[1].children[0].checked;
					//输出的
					opt += '<option label="'+ label +'" checked="'+ checked +'"><value>'+ encodeHtml(val) +'</value></option>'
				}
			}
			opts = '<options>'+ opt +'</options>';
			break;
		case 'SelectOne':
		case 'SelectMultiple':
			optLen = $i(type +'_options').value;
			if(optLen > 0){
				var tag = $i('showItems');
				for(var i=0;i<optLen;i++){
					var _table = tag.children[i].children[1].children[0].children[0],
						_tr = _table.children;
					//获取标题
					label = _tr[0].children[1].children[0].value;
					//获取值
					val = _tr[1].children[1].children[0].value;
					//获取选中状态
					checked = _tr[2].children[1].children[0].checked;
					//输出的
					opt += '<option label="'+ label +'" checked="'+ checked +'"><value>'+ encodeHtml(val) +'</value></option>'
				}
			}
			opts = '<options>'+ opt +'</options>';
			break;
		case 'Date':
		case 'DateTime':
			opts = '<value>'+ encodeHtml($i(type +'_value').value) +'</value>';
			break;
		case 'Image':
		case 'File':
			opts = '<value></value>';
			break;
	}
	//输出字符串
	var dirAndCol="";
	var readonly=" readonly=\"false\"";
	if(type=="CheckBox"||type=="Radio")
	{
		dirAndCol=" direction=\""+dir+"\" columns=\""+col+"\"";
		readonly="";
	}
	if(type=="TextEditor"||type=="SelectOne"||type=="SelectMultiple")readonly="";
	temp = "<InputTypeSet style=\"\" width=\""+width+"\" height=\""+ height +"\""+dirAndCol+readonly+">"+opts+"<\/InputTypeSet>";
	return temp;
}
function encodeHtml(str)
{
	str=str.replace(/\&/g,"&amp;");
	str=str.replace(/\</g,"&lt;");
	str=str.replace(/\>/g,"&gt;");
	return str;
}
//根据样式设置显示界面
function setStyleInput(styleStr,type)
{
	if(type != 'Image' && type != 'File') $i('m_'+type).className = '';
	if(type=="CheckBox"||type=="Radio"||type=="SelectOne"||type=="SelectMultiple")
	{
		//得到每一项内属性
		var regexp=new RegExp("<option[ ]+label=\"(.*?)\"[ ]+(checked=\"(false|true)\"|selected=\"(false|true)\")[ ]*>[ ]*<value>(.*?)<\/value>[ ]*<\/option>","gm");
		var matchObj=regexp.exec(styleStr);
		option_array=[];
		if(matchObj!=null)
		{
			var optionObj={label:matchObj[1],optionCheckd:matchObj[3]!=""?matchObj[3]:matchObj[4],value:matchObj[5]};
			var optionType=matchObj[2].split('=')[0];
			optionObj.optionCheckd=(optionObj.optionCheckd=="true"?optionType+"=\""+optionType+"\"":"");
			option_array.push(optionObj);
		}
		do
		{
   			matchObj=regexp.exec(styleStr);
			if(matchObj!=null)
			{
				var optionObj={label:matchObj[1],optionType:matchObj[2],optionCheckd:matchObj[3]!=""?matchObj[3]:matchObj[4],value:matchObj[5]};
				var optionType=matchObj[2].split('=')[0];
				optionObj.optionCheckd=(optionObj.optionCheckd=="true"?optionType+"=\""+optionType+"\"":"");
				option_array.push(optionObj);
			}
		}
		while (matchObj!=null)
		if(type=="CheckBox"||type=="Radio"){
		//得到方向，列数
			var direction=styleStr.match(/direction=\"(\w+)\"/)[1];
			var columns=styleStr.match(/columns=\"(\d+)\"/)[1];
			$i(type+'_direction').selectedIndex=(direction=="Horizontal"?0:1);
			$i(type+'_columns').value=columns;
		}
		$i(type+"_options").value=option_array.length;
		$i('showItems').innerHTML = createItem($i('formTemp').value,option_array.length,option_array);
		Sio.Text('showItems');
	}
	else
	{
		//得到宽高，默认值
		var width=styleStr.match(/width=\"(\d+)\"/)[1];
		var height=styleStr.match(/height=\"(\d+)\"/)[1];
		var v=styleStr.match(/<value>(.*)<\/value>/m)[1];
		if(type!="Image"&&type!="File")
		{
			//设置宽高默认值
			$i(type+"_width").value=width;
			$i(type+"_height").value=height;
			if(type!="TextEditor")$i(type+"_value").value=v;
		}
	}
}
</script>
</body>
</html> 
