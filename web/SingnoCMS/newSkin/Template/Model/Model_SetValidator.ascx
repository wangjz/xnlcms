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
<form  action="model_action.aspx?action=setValidator&id={&id}" method="post" id="form1" name="form1">
<xnl:repeater modelid="{&id}" sqlcommand="select IsValidator,ValidatorSet from SN_ModelDescript where DescriptId=@modelid"><itemtemplate>
<div class="hide"><textarea name="validatorSet" id="validatorSet" cols="1" rows="1">{$ValidatorSet}</textarea></div> 
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody id="ctrl_cmd">
    <tr>
			<td width="125">启用验证：</td>
	  <td>
      <label for="IsValidator1">是</label>
		    <input type="radio" name="IsValidator" id="IsValidator1" value="1" onclick="setShow(true);" {[iif('{$IsValidator}'='1','checked="checked"','')]}/>
		    <label for="IsValidator2">否</label>
	      <input name="IsValidator" type="radio" id="IsValidator2" value="0"  onclick="setShow(false);" {[iif('{$IsValidator}'='0','checked="checked"','')]}/></td>
	  </tr>
		<tr id="requiredRow" class="{[iif('{$IsValidator}'='1','','hide')]}">
			<td width="125">是否必填：</td>
			<td><label for="enabled2">是</label>
		    <input type="radio" name="required" id="enabled2" value="true" />
		    <label for="enabled3">否</label>
		    <input name="required" type="radio" id="enabled3" value="false" checked="checked" /></td>
		</tr>
		<tr id="regexpRow" class="{[iif('{$IsValidator}'='1','','hide')]}">
			<td>正则验证：</td>
			<td><label>
		        <input name="regexpType" type="radio" id="RadioGroup1_0" value="system"  onclick="$i('sysregDiv').className='';$i('userregDiv').className='hide';" checked="checked" />
			      在列表中选择</label>
		      <label>
			      <input type="radio" name="regexpType" value="user" id="RadioGroup1_1" onclick="$i('sysregDiv').className='hide';$i('userregDiv').className='';" />
			      自定义</label><span id="sysregDiv">
              <select name="regexp" id="regexp" style="width:300px">
	              <option value="">不验证</option>
	              <option value="^\d+$">包含数字字符(0-9)的字符串</option>
	              <option value="^[+-]?\d*[.]?\d*$">数字</option>
	              <option value="^([1-9]|d*">自然数</option>
	              <option value="[\u4e00-\u9fa5]">汉字</option>
	              <option value="\.swf$">swf格式文件</option>
	              <option value="\.gif$|\.jpg$|\.bmp$|\.ico$|\.png$">图片格式文件</option>
	              <option value="^\d+(\.{0,1}\d{0,2})?$">金额(两位小数)</option>
	              <option value="^[^0]\d*$">除0以外的所有数字</option>
	              <option value="\d{3}-\d{8}|\d{4}-\d{7}">国内电话号码</option>
	              <option value="[1-9][0-9]{4,9}">腾讯QQ号</option>
	              <option value="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email地址</option>
	              <option value="[a-zA-z]+://[^\s]*">网址URL</option>
	              <option value="^\d{6}$">中国邮政编码</option>
	              <option value="\d{15}|\d{18}">身份证</option>
	              <option value="[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}">ip地址</option>
	              <option value="^\w+$">由数字、26个英文字母或者下划线组成的字符串</option>
	              <option value="^[A-Za-z0-9]+$">数字和26个英文字母组成的字符串</option>
	              <option value="^[a-z]+$">26个英文字母的小写组成的字符串</option>
	              <option value="^[A-Z]+$">26个英文字母的大写组成的字符串</option>
	              <option value="^[A-Za-z]+$">26个英文字母组成的字符串</option>
	              <option value="^-[1-9]\d*|0$">非正整数（负整数 + 0）</option>
	              <option value="^[1-9]\d*|0$">非负整数（正整数 + 0）</option>
	              <option value="^-?[1-9]\d*$">整数</option>
	              <option value="^-[1-9]\d*$">负整数 </option>
	              <option value="^[1-9]\d*$">正整数</option>
            </select></span>
            <span id="userregDiv" class="hide"><input name="userRegexp" type="text" id="userRegexp" size="60" maxlength="255" /></span></td>
		</tr>
        <tr id="errorRow" class="{[iif('{$IsValidator}'='1','','hide')]}">
			<td height="47">验证失败提示：</td>
			<td><input name="error" type="text" id="error" size="60" maxlength="80" /></td>
	  </tr>
	</tbody>
</table>
</itemtemplate></xnl:repeater>
	<input name=""  type="submit" value="设置样式"/> 
</div>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
var option_array=[];
var validatorSetStr=$i('validatorSet').value;
setStyleInput(validatorSetStr);

function encodeHtml(str)
{
	str=str.replace(/\&/g,"&amp;");
	str=str.replace(/\</g,"&lt;");
	str=str.replace(/\>/g,"&gt;");
	return str;
}
function setShow(isShow)
{
	if(isShow)
	{
		$i('requiredRow').className='';
		$i('regexpRow').className='';
		$i('errorRow').className='';
	}
	else
	{
		$i('requiredRow').className='hide';
		$i('regexpRow').className='hide';
		$i('errorRow').className='hide';
	}
}
//根据样式设置显示界面
function setStyleInput(styleStr)
{
	//得到是否必填
	var required=styleStr.match(/<ValidatorSet required="(true|false)">/)[1];
	required=="true"?$i('enabled2').checked=true:$i('enabled3').checked=true;
	//得到正则
	var regexp=styleStr.match(/<regexp>(.*?)<\/regexp>/)[1];
	if(regexp.indexOf("<![CDATA[")==0)
	{
		regexp=regexp.match(/<!\[CDATA\[(.*?)\]\]>/)[1];
	}
	var regexpSel=$i('regexp');
	$i('RadioGroup1_0').checked=true;
	if(Sio.trim(regexp)!="")
	{
		//循环匹配
		var sysRegType=false;
		for(var i=0,l=regexpSel.options.length;i<l;i++)
		{
			var option=regexpSel.options[i];
			if(option.value==regexp)
			{
				sysRegType=true;
				regexpSel.selectedIndex=i;break;
			}
		}
		if(!sysRegType)
		{
			$i('RadioGroup1_1').checked=true;
			$i('sysregDiv').className='hide';
			$i('userregDiv').className='';
			$i('userRegexp').value=regexp;
		}
	}
	var msgTip=styleStr.match(/<error>(.*?)<\/error>/)[1];
	if(msgTip.indexOf("<![CDATA[")==0)
	{
		msgTip=msgTip.match(/<!\[CDATA\[(.*)\]\]>/)[1];
	}
	$i('error').value=msgTip;
}
</script>
</body>
</html> 
