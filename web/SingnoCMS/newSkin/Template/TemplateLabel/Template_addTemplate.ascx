<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1"  action="Template_action.aspx?action=addTemplate" method="post" name="form1">
<table class="forms">
	<tbody>
		<tr>
			<td width="120">模版名称：</td>
			<td><input name="templateName" type="text" id="templateName" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td width="120">模版类型：</td>
			<td>
				<input type="radio" name="templateStyle" id="tempType1" value="0" title="首页模版" onclick="setFileVisible(true)"/><label for="tempType1">首页模版</label> 
				<input name="templateStyle" type="radio" id="tempType2" title="栏目页模版" value="1" checked="checked" onclick="setFileVisible(false)"/><label for="tempType2">栏目页模版</label> 
				<input type="radio" name="templateStyle" id="tempType3" value="2" title="内容页模版"/><label for="tempType3" onclick="setFileVisible(false)">内容页模版</label> 
				<input type="radio" name="templateStyle" id="tempType4" value="3" title="单页模版" onclick="setFileVisible(true)"/><label for="tempType4">单页模版</label>
			</td>
		</tr>
		<tr id="fileNameRow" class="hide">
			<td width="120">生成文件名：</td>
			<td><input name="createFileName" type="text" id="createFileName" /><span class="required">*</span></td>
		</tr>
		<tr id="fileExtRow" class="hide">
			<td valign="top">生成扩展名：</td>
			<td>
				<span class="fl">
				<select name="extName" id="extName">
					<option value=".htm">.htm</option>
					<option value=".html">.html</option>
					<option value=".shtml">.shtml</option>
					<option value=".asp">.asp</option>
					<option value=".aspx">.aspx</option>
					<option value=".xml">.xml</option>
					<option value="none">自定义格式</option>
                </select></span>
				<span id="custExtName" class="hide fl"><input name="userExtName" type="text" class="name" id="userExtName" value=".txt" maxlength="8" style="width:30px"/>(例.txt)</span>
			</td>
		</tr>
		<tr>
			<td width="120">网页编码：</td>
			<td> <xnl:set><attrs><attr name="charset"><CMS.Manage:SiteConfig action="view" type="base">{@charset}</CMS.Manage:SiteConfig></attr></attrs></xnl:set>
				<select name="charset" id="charset">
					<option value="utf-8" {[iif('{!charset}'='utf-8','selected="selected"','')]}>Unicode (UTF-8)</option>
					<option {[iif('{!charset}'='gb2312','selected="selected"','')]} value="gb2312">简体中文 (GB2312)</option>
					<option {[iif('{!charset}'='big5','selected="selected"','')]} value="big5">繁体中文 (Big5)</option>
					<option {[iif('{!charset}'='iso-8859-1','selected="selected"','')]} value="iso-8859-1">西欧 (iso-8859-1)</option>
					<option {[iif('{!charset}'='euc-kr','selected="selected"','')]} value="euc-kr">韩文 (euc-kr)</option>
					<option {[iif('{!charset}'='euc-jp','selected="selected"','')]} value="euc-jp">日文 (euc-jp)</option>
					<option {[iif('{!charset}'='iso-8859-6','selected="selected"','')]} value="iso-8859-6">阿拉伯文 (iso-8859-6)</option>
					<option {[iif('{!charset}'='windows-874','selected="selected"','')]} value="windows-874">泰文 (windows-874)</option>
					<option {[iif('{!charset}'='iso-8859-9','selected="selected"','')]} value="iso-8859-9">土耳其文 (iso-8859-9)</option>
					<option {[iif('{!charset}'='iso-8859-5','selected="selected"','')]} value="iso-8859-5">西里尔文 (iso-8859-5)</option>
					<option {[iif('{!charset}'='iso-8859-8','selected="selected"','')]} value="iso-8859-8">希伯来文 (iso-8859-8)</option>
					<option {[iif('{!charset}'='iso-8859-7','selected="selected"','')]} value="iso-8859-7">希腊文 (iso-8859-7)</option>
					<option {[iif('{!charset}'='windows-1258','selected="selected"','')]} value="windows-1258">越南文 (windows-1258)</option>
					<option {[iif('{!charset}'='iso-8859-2','selected="selected"','')]} value="iso-8859-2">中欧 (iso-8859-2)</option>
				</select>
			</td>
		</tr>
		<tr>
			<td width="120">模版方案：</td>
			<td>
				<select name="projectId" id="charset">
                	<xnl:repeater sqlcommand="select projectid,projectname,isdefault from SN_TemplateProject where SiteID=@siteid">
						<attrs>
							<attr name="siteid" type="int">
								<CMS.manage:common action="getsiteidbysn"></CMS.manage:common>
							</attr>
						</attrs>
						<itemTemplate>
							<xnl:if>
								<ifitem test="1" value="{$isdefault}">
									<if>
										<option value="{$projectid}" selected="selected">{$projectname}</option></if>
									<else>
										<option value="{$projectid}">{$projectname}</option>
									</else>
								</ifitem>
							</xnl:if>
						</itemTemplate>
                    </xnl:repeater>
				</select>
			</td>
		</tr>
		<tr>
			<td valign="top">模版内容：</td>
			<td><textarea cols="160" rows="25" name="templateContent" id="templateContent"></textarea></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input name="Submit" type="submit" value="创建模版" /></div></td>
		</tr>
	</tfoot>
</table>
</form>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

$i('extName').onclick = function(){
	$i("custExtName").className = (this.value == 'none') ? 'fl' : 'hide fl';
}
function setFileVisible(isShow)
{
	$i('fileNameRow').className=$i('fileExtRow').className=(isShow?'':'hide');
}
</script>
</body>
</html>