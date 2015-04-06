y<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="permit">
	<div class="title">更换许可证</div>
	<div class="content">
		<table class="forms">
			<tbody>
				<tr>
					<td colspan="2">请在下面表单中上传您的 Siongno CMS 许可证文件。</td>
				</tr>
				<tr>
					<td colspan="2">您可以将您的机器标识或者网站域名提供给我们的销售渠道以便获取正式授权。</td>
				</tr>
				<tr>
					<td width="150px">您的机器标识：</td>
					<td>000C29E4C90A:0FEBFBFF000006FD:243FA783</td>
				</tr>
				<tr>
					<td>您的网站域名：</td>
					<td>localhost</td>
				</tr>
				<tr>
					<td>许可证文件：</td>
					<td>
						<span class="fl"><input type="text" name="" id="showFiles" style="width:400px" /></span> 
						<span class="fl" style="padding:2px 0 0 10px"><input type="button" value="浏览" id="explorer" /></span> 
						<input type="file"  name="" id="selectFiles" style="visibility:hidden" />
					</td>
				</tr>
			</tbody>
		</table>
		<input type="button" value="提交许可证" /> <input type="button" value="返回" id="back" />
	</div>
</div>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
//
$i('explorer').onclick = function(){
	$i('selectFiles').click();
	$i('showFiles').value = $i('selectFiles').value;
}
//
$i('back').onclick = function(){
	document.location = 'M_Config_ConfigPermit.html';
}
</script>
</body>
</html>