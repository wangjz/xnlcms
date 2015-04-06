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
<form action="config_siteAction.aspx?action=modify&type=upload" method="post" id="form1" name="form1">
<CMS.Manage:SiteConfig action="view" type="upload">
<table class="forms">
	<tbody>
		<tr>
			<td width="150px">上传文件夹名称：</td>
			<td><input name="folder" type="text" id="folder" value="{@folder}" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">上传文件夹保存方式：</td>
			<td><xnl:if><ifItem value="{@saveType}" test="Y"><if><input name="SaveType" type="radio" id="uploadSaveType1" value="Y" checked="checked" /></if><else><input name="SaveType" type="radio" id="uploadSaveType1" value="Y" /></else></ifItem><label for="uploadSaveType1">按年输入</label>
            <ifItem value="{@saveType}" test="Y/M"><if><input name="SaveType" type="radio" id="uploadSaveType2" value="Y/M" checked="checked" /></if><else><input name="SaveType" type="radio" id="uploadSaveType2" value="Y/M" /></else></ifItem><label for="uploadSaveType2">按年/月输入</label>
            <ifItem value="{@saveType}" test="Y/M/D"><if> <input name="SaveType" type="radio" id="uploadSaveType3" value="Y/M/D" checked="checked" /></if><else> <input name="SaveType" type="radio" id="uploadSaveType3" value="Y/M/D" /></else></ifItem></xnl:if><label for="uploadSaveType3">按年/月/日输入</label>
			<span class="required">*</span></td>
		</tr>
		<tr>
			<td>自动按时间重命名文件：</td>
			<td><xnl:if><ifItem test="{@renameByTime}" value="True"><if><input name="ReNameByTime" type="radio" id="autoRename1" value="True"  checked="checked"/><label for="autoRename1">是</label><input name="ReNameByTime" type="radio" id="autoRename2" value="False" /><label for="autoRename2">否</label></if>
            <else><input name="ReNameByTime" type="radio" id="autoRename1" value="True"/><label for="autoRename1">是</label><input name="ReNameByTime" type="radio" id="autoRename2" value="False" checked="checked" /><label for="autoRename2">否</label></else></ifItem></xnl:if>
		    
		    
			<span class="required">*</span></td>
		</tr>
	</tbody>
</table>
<div id="filesType">
	<div class="title">允许上传文件类型：</div>
	<div id="classTab">
		<ul>
			<li>图片类</li>
			<li>媒体类</li>
			<li>文档类</li>
		</ul>
	</div>
	<div id="classContent">
		<ul>
			<li>
				<div class="uploadExtName">
					<ul>
						<li><input name="imageType" type="checkbox" id="imageType1" value="jpg" {[iif({[IndexOf('{@imagesType}','jpg',0,-1)]}=-1,'','checked="checked"')]} /> <label for="imageType1">jpg</label></li>
						<li><input name="imageType" type="checkbox" id="imageType2" value="jpeg" {[iif({[IndexOf('{@imagesType}','jpeg',0,-1)]}=-1,'','checked="checked"')]}/> <label for="imageType2">jpeg</label></li>
						<li><input name="imageType" type="checkbox" id="imageType3" value="gif" {[iif({[IndexOf('{@imagesType}','gif',0,-1)]}=-1,'','checked="checked"')]}/> <label for="imageType3">gif</label></li>
						<li><input name="imageType" type="checkbox" id="imageType4" value="png" {[iif({[IndexOf('{@imagesType}','png',0,-1)]}=-1,'','checked="checked"')]}/> <label for="imageType4">png</label></li>
						<li><input name="imageType" type="checkbox" id="imageType5" value="bmp" {[iif({[IndexOf('{@imagesType}','bmp',0,-1)]}=-1,'','checked="checked"')]}/> <label for="imageType5">bmp</label></li>
					</ul>
				</div>
				<div class="fileSize">
					<div class="fl">文件大小限制：</div>
					<div class="fr"><input name="imageMaxSize" type="text" id="imageMaxSize" style="width:50px" value="{@imageMaxSize}" /> 
					  M <span class="required">*</span></div>
				</div>
			</li>
			<li>
				<div class="uploadExtName">
					<ul>
						<li><input name="mediaType" type="checkbox" id="mediaType1" value="rmvb" {[iif({[IndexOf('{@mediasType}','rmvb',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType1">rmvb</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType2" value="mp3" {[iif({[IndexOf('{@mediasType}','mp3',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType2">mp3</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType3" value="swf" {[iif({[IndexOf('{@mediasType}','swf',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType3">swf</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType4" value="flv" {[iif({[IndexOf('{@mediasType}','flv',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType4">flv</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType5" value="wav" {[iif({[IndexOf('{@mediasType}','wav',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType5">wav</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType6" value="mid" {[iif({[IndexOf('{@mediasType}','mid',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType6">mid</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType7" value="midi" {[iif({[IndexOf('{@mediasType}','midi',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType7">midi</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType8" value="ra" {[iif({[IndexOf('{@mediasType}','ra',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType8">ra</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType9" value="avi" {[iif({[IndexOf('{@mediasType}','avi',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType9">avi</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType10" value="mpg" {[iif({[IndexOf('{@mediasType}','mpg',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType10">mpg</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType11" value="mpeg" {[iif({[IndexOf('{@mediasType}','mpeg',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType11">mpeg</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType12" value="asf" {[iif({[IndexOf('{@mediasType}','asf',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType12">asf</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType13" value="asx" {[iif({[IndexOf('{@mediasType}','asx',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType13">asx</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType14" value="wma" {[iif({[IndexOf('{@mediasType}','wma',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType14">wma</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType15" value="mov" {[iif({[IndexOf('{@mediasType}','mov',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType15">mov</label></li>
						<li><input name="mediaType" type="checkbox" id="mediaType16" value="rm" {[iif({[IndexOf('{@mediasType}','rm',0,-1)]}=-1,'','checked="checked"')]}/> <label for="mediaType16">rm</label></li>
					</ul>
				</div>
				<div class="fileSize">
					<div class="fl">文件大小限制：</div>
					<div class="fr"><input name="mediaMaxSize" type="text" id="mediaMaxSize" style="width:50px" value="{@mediaMaxSize}" /> M <span class="required">*</span></div>
				</div>
			</li>
			<li>
				<div class="uploadExtName">
					<ul>
						<li><input name="docsType" type="checkbox" id="docsType1" value="txt" {[iif({[IndexOf('{@docsType}','txt',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType1">txt</label></li>
						<li><input name="docsType" type="checkbox" id="docsType2" value="doc" {[iif({[IndexOf('{@docsType}','doc',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType2">doc</label></li>
						<li><input name="docsType" type="checkbox" id="docsType3" value="docx" {[iif({[IndexOf('{@docsType}','docx',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType3">docx</label></li>
						<li><input name="docsType" type="checkbox" id="docsType4" value="ppt" {[iif({[IndexOf('{@docsType}','ppt',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType4">ppt</label></li>
						<li><input name="docsType" type="checkbox" id="docsType5" value="pptx" {[iif({[IndexOf('{@docsType}','pptx',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType5">pptx</label></li>
						<li><input name="docsType" type="checkbox" id="docsType6" value="xls" {[iif({[IndexOf('{@docsType}','xls',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType6">xls</label></li>
						<li><input name="docsType" type="checkbox" id="docsType7" value="xlsx" {[iif({[IndexOf('{@docsType}','xlsx',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType7">xlsx</label></li>
						<li><input name="docsType" type="checkbox" id="docsType8" value="xml" {[iif({[IndexOf('{@docsType}','xml',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType8">xml</label></li>
						<li><input name="docsType" type="checkbox" id="docsType9" value="pdf" {[iif({[IndexOf('{@docsType}','pdf',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType9">pdf</label></li>
						<li><input name="docsType" type="checkbox" id="docsType10" value="rar" {[iif({[IndexOf('{@docsType}','rar',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType10">rar</label></li>
						<li><input name="docsType" type="checkbox" id="docsType11" value="zip" {[iif({[IndexOf('{@docsType}','zip',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType11">zip</label></li>
						<li><input name="docsType" type="checkbox" id="docsType12" value="7z" {[iif({[IndexOf('{@docsType}','7z',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType12">7z</label></li>
						<li><input name="docsType" type="checkbox" id="docsType13" value="gz" {[iif({[IndexOf('{@docsType}','gz',0,-1)]}=-1,'','checked="checked"')]}/> <label for="docsType13">gz</label></li>
					</ul>
				</div>
				<div class="fileSize">
					<div class="fl">文件大小限制：</div>
					<div class="fr"><input name="docMaxSize" type="text" id="docMaxSize" style="width:50px" value="{@docMaxSize}" /> M <span class="required">*</span></div>
				</div>
			</li>
		</ul>
	</div>
	<div class="grayBg"><input type="submit" value="提交配置" /></div>
</div>
</CMS.Manage:SiteConfig>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

//选项卡
Sio.Tabs({tab:'classTab',con:'classContent'});
if(Sio.BS().IE6) Sio.hoverClass($it('classTab','li'),'hover');

</script>
</body>
</html>