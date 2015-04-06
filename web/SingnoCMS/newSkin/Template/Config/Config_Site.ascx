<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="Config_SiteAction.aspx?action=modify&type=base" method="post" id="form1" name="form1" onsubmit="submitForm();return false;">
<table class="forms">
	<CMS.Manage:SiteConfig action="view" type="base"><tbody>
		<tr>
			<td width="150">网站名称：</td>
			<td><input name="siteName" type="text" id="siteName" value="{@name}" maxlength="200" /><span class="required">*</span></td>
		</tr>
        <tr>
			<td>地址：</td>
			<td><input name="path" type="text" id="path" value="{@path}" readonly="readonly" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">title：</td>
			<td><input name="title" type="text" id="title" value="{@title}" maxlength="255" /></td>
		</tr>
		<tr>
			<td>ICO（地址栏前）地址：</td>
			<td><input name="ico" type="text" id="ico" value="{@ico}" /></td>
		</tr>
        <tr>
			<td valign="top">站点介绍：</td>
			<td><textarea name="info" id="info" cols="80" rows="4">{@info}</textarea></td>
		</tr>
		<tr>
			<td valign="top">针对搜索引擎的关键字：</td>
			<td><textarea name="keyword" id="keyword" cols="80" rows="4">{@keyword}</textarea></td>
		</tr>
        <tr>
			<td valign="top">针对搜索引擎的说明：</td>
			<td><textarea name="desc" id="desc" cols="80" rows="4">{@desc}</textarea></td>
		</tr>
		<tr>
			<td>站点默认编码：</td>
			<td>
				<select id="Charset" name="Charset">
                	<xnl:if>
                    <ifItem test="{@charset}" value="utf-8">
                    <if><option value="utf-8" selected="selected">Unicode(UTF-8)</option></if>
                    <else><option value="utf-8">Unicode(UTF-8)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="gb2312" selected="selected">简体中文(GB2312)</option></if>
                    <else><option value="gb2312">简体中文(GB2312)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="big5" selected="selected">繁体中文(Big5)</option></if>
                    <else><option value="big5">繁体中文(Big5)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-1" selected="selected">西欧(iso-8859-1)</option></if>
                    <else><option value="iso-8859-1">西欧(iso-8859-1)</option></else>
                    </ifItem>
                    <ifItem test="{@charset}" value="gb2312">
                    <if><option value="euc-kr" selected="selected">韩文(euc-kr)</option></if>
                    <else><option value="euc-kr">韩文(euc-kr)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="euc-jp" selected="selected">日文(euc-jp)</option></if>
                    <else><option value="euc-jp">日文(euc-jp)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-6" selected="selected">阿拉伯文(iso-8859-6)</option></if>
                    <else><option value="iso-8859-6">阿拉伯文(iso-8859-6)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="windows-874" selected="selected">泰文(windows-874)</option></if>
                    <else><option value="windows-874">泰文(windows-874)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-9" selected="selected">土耳其文(iso-8859-9)</option></if>
                    <else><option value="iso-8859-9">土耳其文(iso-8859-9)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-5" selected="selected">西里尔文(iso-8859-5)</option></if>
                    <else><option value="iso-8859-5">西里尔文(iso-8859-5)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-8" selected="selected">希伯来文(iso-8859-8)</option></if>
                    <else><option value="iso-8859-8">希伯来文(iso-8859-8)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-7" selected="selected">希腊文(iso-8859-7)</option></if>
                    <else><option value="iso-8859-7">希腊文(iso-8859-7)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="windows-1258" selected="selected">越南文(windows-1258)</option></if>
                    <else><option value="windows-1258">越南文(windows-1258)</option></else>
                    </ifItem>
					<ifItem test="{@charset}" value="gb2312">
                    <if><option value="iso-8859-2" selected="selected">中欧(iso-8859-2)</option></if>
                    <else><option value="iso-8859-2">中欧(iso-8859-2)</option></else>
                    </ifItem>
                    </xnl:if>
				</select>
			</td>
		</tr>
		<tr>
			<td>内容审核机制：</td>
			<td><xnl:if>
            <ifItem value="1" test="{@audit}">
            <if><span class="fl">
					<input type="radio" name="audit" id="audit1" value="0" checked="checked" /><label for="audit1">默认审核机制</label>
					<input type="radio" name="audit" id="audit2" value="1" /><label for="audit2">多级审核机制</label>
				</span>
				<span class="fl">
					<div id="checkLevelDiv"></div>
				</span></if>
                <else><span class="fl">
					<input type="radio" name="audit" id="audit1" value="0"  /><label for="audit1">默认审核机制</label>
					<input type="radio" name="audit" id="audit2" value="1" checked="checked"/><label for="audit2">多级审核机制</label>
				</span>
				<span class="fl">
					<div id="checkLevelDiv"><div class="fl"><select id="checkLevel" name="checkLevel"><xnl:for forstart="2" forend="5"><forItem><xnl:if><ifItem test="{@forvar}" value="{@audit}"><if><option value="{@forvar}" selected="selected">{@forvar}</option></if><else><option value="{@forvar}">{@forvar}</option></else></ifItem></xnl:if></forItem></xnl:for></select>级</div></div>
				</span></else>
            </ifItem>
            </xnl:if>
				
			</td>
		</tr>
       
	</tbody> </CMS.Manage:SiteConfig>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
var str=$i('checkLevelDiv').innerHTML;
str=Sio.trim(str)==""?'<div class=\"fl\"><select id=\"checkLevel\" name=\"checkLevel\"><option value=\"2\" selected=\"selected\">2<\/option><option value=\"3\">3<\/option><option value=\"4\">4<\/option><option value=\"5\">5<\/option><\/select> 级<\/div>':str;
//处理审核
Sio.addEvent(Sio.get('input[name=audit]'),'click',function(){
	$i('checkLevelDiv').innerHTML = ($i('audit1').checked) ? '' : str;
});
function submitForm()
{
	var siteName=escape($i('siteName').value);
	var path=$i('path').value;
	var title=escape($i('title').value);
	var ico=escape($i('ico').value);
	var info=escape($i('info').value);
	var keyword=escape($i('keyword').value);
	var desc=escape($i('desc').value);
	var Charset=$i('Charset').value;
	var audit=$i('audit1').checked?$i('audit1').value:$i('audit2').value;
	var checkLevel=1;
	if(Number(audit)==1)checkLevel=$('checkLevel').value;
	var param={siteName:siteName,path:path,title:title,ico:ico,info:info,keyword:keyword,desc:desc,Charset:Charset,audit:audit,checkLevel:checkLevel};
	Sio.ajax({url:'Config_SiteAction.aspx?action=modify&type=base',method:'post',data:param,onComplete:complete});
	//new Sio.Ajax("Config_SiteAction.aspx?action=modify&type=base",{parameters:param,onComplete:complete});
}

function complete(request)
{
	var obj=eval("("+request+")");
	if(obj.state=="Y")
	{
		document.location.reload();
	}
	else
	{
		alert(obj.msg);
	}
}
</script>
</body>
</html>