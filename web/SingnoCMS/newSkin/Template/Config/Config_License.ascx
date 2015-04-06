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
<div id="permit">
	<div class="title">Singno CMS许可证</div>
	<div class="content">
		<ul>
			<li>此产品为官方正式授权产品，以下为授权信息：</li>
			<li>产品版本：Singno CMS beta版 1.0</li>
			<li>产品授权域名：localhost</li>
			<li>可建网站数量：1</li>
		</ul>
		<input type="button" id="changePermit" value="更改许可证" />
	</div>
</div>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();
//
$i('changePermit').onclick = function(){
	document.location = 'M_Config_changePermit.html';
}
</script>
</body>
</html>