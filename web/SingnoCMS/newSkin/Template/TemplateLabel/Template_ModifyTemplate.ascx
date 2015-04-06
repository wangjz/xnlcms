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
<form action="Template_action.aspx?action=modifyTemplate" id="form1" method="post">
<xnl:repeater TemplateID="{&TemplateID}" sqlcommand="select TemplateName,TemplateStyle,CreatedFileFullName,CreatedFileExtName,Charset,IsDefault,TemplateProjectID,siteid,templatefilepath from SN_Template where TemplateID=@TemplateID">
<itemTemplate>
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">模版名称：</td>
			<td><input name="templateName" type="text" id="templateName" value="{$TemplateName}" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td width="120px">模版类型：</td>
			<td>
				<xnl:if>
					<ifItem value="0" test="{$TemplateStyle}">
						<if>首页模板</if>
					</ifItem>
					<ifItem value="1" test="{$TemplateStyle}">
						<if>栏目模板</if>
					</ifItem>
					<ifItem value="2" test="{$TemplateStyle}">
						<if>内容模板</if>
					</ifItem>
					<ifItem value="3" test="{$TemplateStyle}" >
						<if>单页模板</if>
					</ifItem>
                    <ifItem value="4" test="{$TemplateStyle}" >
						<if>单页模板(错误页)</if>
					</ifItem>
                </xnl:if>
			</td>
		</tr>
        <xnl:if><ifItem value="1"><attrs><attr name="test">{[iif({$TemplateStyle}=0 or {$TemplateStyle}>=3,1,0)]}</attr></attrs><if>
					<tr>
						<td width="120px">生成文件名：</td>
						<td><input name="createFileName" type="text" id="createFileName" value="{$CreatedFileFullName}" /><span class="required">*</span></td>
					</tr>
					<tr>
						<td valign="top">生成扩展名：</td>
						<td>
							<xnl:set> 
            <attrs><attr name="extname">{[iif('{$CreatedFileExtName}'='.html' or '{$CreatedFileExtName}'='.htm' or '{$CreatedFileExtName}'='.shtml' or '{$CreatedFileExtName}'='.asp' or '{$CreatedFileExtName}'='.aspx' or '{$CreatedFileExtName}'='.xml',1,0)]}</attr></attrs>
            <xnl:if>
            <ifItem test="{@extname}" value="1">
            <if><span class="fl"><select name="extName" id="extName">
					<option value=".htm" {[iif('{$CreatedFileExtName}'='.htm','selected="selected"','')]}>.htm</option>
					<option value=".html" {[iif('{$CreatedFileExtName}'='.html','selected="selected"','')]}>.html</option>
					<option value=".shtml" {[iif('{$CreatedFileExtName}'='.shtml','selected="selected"','')]}>.shtml</option>
					<option value=".asp" {[iif('{$CreatedFileExtName}'='.asp','selected="selected"','')]}>.asp</option>
					<option value=".aspx" {[iif('{$CreatedFileExtName}'='.aspx','selected="selected"','')]}>.aspx</option>
					<option value=".xml" {[iif('{$CreatedFileExtName}'='.xml','selected="selected"','')]}>.xml</option>
					<option value="none">自定义格式</option>
				</select></span>
				<span id="custExtName" class="hide fl"><input name="userExtName" type="text" value=".txt" id="userExtName" class="name" value=".txt" maxlength="8" style="width:30px"/>(例.txt)</span>
                </if>
            <else><span class="fl">
            <select name="extName" id="extName">
					<option value=".htm">.htm</option>
					<option value=".html">.html</option>
					<option value=".shtml">.shtml</option>
					<option value=".asp">.asp</option>
					<option value=".aspx">.aspx</option>
					<option value=".xml">.xml</option>
					<option value="none" selected="selected">自定义格式</option>
				</select></span>
				<span id="custExtName" class="fl"><input name="userExtName" type="text" class="name" id="userExtName" value="{$CreatedFileExtName}" maxlength="8" style="width:30px"/>(例.txt)</span>
            </else>
            </ifItem>
            </xnl:if>
            </xnl:set>	
						</td>
					</tr>
		</if></ifItem></xnl:if>
		<tr>
			<td width="120px">网页编码：</td>
			<td>
				<select name="charset" id="charset">
					<option value="utf-8" {[iif('{$Charset}'='utf-8','selected="selected"','')]}>Unicode (UTF-8)</option>
					<option  value="gb2312" {[iif('{$Charset}'='gb2312','selected="selected"','')]}>简体中文 (GB2312)</option>
					<option value="big5" {[iif('{$Charset}'='big5','selected="selected"','')]}>繁体中文 (Big5)</option>
					<option value="iso-8859-1" {[iif('{$Charset}'='iso-8859-1','selected="selected"','')]}>西欧 (iso-8859-1)</option>
					<option value="euc-kr" {[iif('{$Charset}'='euc-kr','selected="selected"','')]}>韩文 (euc-kr)</option>
					<option value="euc-jp" {[iif('{$Charset}'='euc-jp','selected="selected"','')]}>日文 (euc-jp)</option>
					<option value="iso-8859-6" {[iif('{$Charset}'='iso-8859-6','selected="selected"','')]}>阿拉伯文 (iso-8859-6)</option>
					<option value="windows-874" {[iif('{$Charset}'='windows-874','selected="selected"','')]}>泰文 (windows-874)</option>
					<option value="iso-8859-9" {[iif('{$Charset}'='iso-8859-9','selected="selected"','')]}>土耳其文 (iso-8859-9)</option>
					<option value="iso-8859-5" {[iif('{$Charset}'='iso-8859-5','selected="selected"','')]}>西里尔文 (iso-8859-5)</option>
					<option value="iso-8859-8" {[iif('{$Charset}'='iso-8859-8','selected="selected"','')]}>希伯来文 (iso-8859-8)</option>
					<option value="iso-8859-7" {[iif('{$Charset}'='iso-8859-7','selected="selected"','')]}>希腊文 (iso-8859-7)</option>
					<option value="windows-1258" {[iif('{$Charset}'='windows-1258','selected="selected"','')]}>越南文 (windows-1258)</option>
					<option value="iso-8859-2" {[iif('{$Charset}'='iso-8859-2','selected="selected"','')]}>中欧 (iso-8859-2)</option>
				</select>
			</td>
		</tr>
		<tr>
			<td valign="top">模版内容：</td>
			<td>
				<textarea name="templateContent" id="templateContent" cols="160" rows="25"><CMS.Manage:TemplateAction action="loadcontent" charset="{$Charset}" siteid="{$SiteID}" templatefilepath="{$templatefilepath}"></CMS.Manage:TemplateAction></textarea>
				<input name="templateId" type="hidden" id="templateId" value="{&templateid}" />
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input name="Submit" type="submit" value="保存修改" /></div></td>
		</tr>
	</tfoot>
</table>
</itemTemplate>
</xnl:repeater>
</form>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
if($i('extName')){
	$i('extName').onclick = function(){
		$i("custExtName").className = (this.value == 'none') ? 'fl' : 'hide fl';
	}
}
</script>
</body>
</html>