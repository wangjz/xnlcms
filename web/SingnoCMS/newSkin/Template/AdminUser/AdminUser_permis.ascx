<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/User.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body{ padding:10px}
</style>
</head>
<body>
<div id="permisList">
<div class="permisHead"><input type="checkbox" name="permisItem" id="permisItem0"/><label for="permisItem0">全选</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem1"/><label for="permisItem1">组别管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem2"/><label for="permisItem2">数据表单定义</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem3"/><label for="permisItem3">内容回收站</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem4"/><label for="permisItem4">站点数据统计</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem5"/><label for="permisItem5">提交表单查看</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem6"/><label for="permisItem6">投票管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem7"/><label for="permisItem7">信息采集管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem8"/><label for="permisItem8">搜索引擎优化</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem9"/><label for="permisItem9">广告管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem10"/><label for="permisItem10">站点连接管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem11"/><label for="permisItem11">数据备份恢复</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem12"/><label for="permisItem12">站点文件管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem13"/><label for="permisItem13">第三方系统集成</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem14"/><label for="permisItem14">显示管理</label></div>
<div class="permisList"><input type="checkbox" name="permisItem" id="permisItem15"/><label for="permisItem15">配置管理</label></div>
</div>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">

var _permisList = Sio.Dom.get('#permisList>div.permisList>input[type=checkbox]');
//批量设置
$i('permisItem0').onclick = function(){
	if($i('permisItem0').checked){
		Sio.each(_permisList,function(i,e){
			e.checked = 'checked';
		});
	}else{
		Sio.each(_permisList,function(i,e){
			e.checked = '';
		});
	}
}
</script>
</body>
</html>
