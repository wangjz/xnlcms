<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!--<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_copy" title="复制"></div>
	<div class="Ico_del" title="删除"></div><div class="line"></div>
	<div class="Ico_empty" title="清空"></div><div class="line"></div>
	<div id="searchBar" class="_hover">
		<div class="input"><input name="keyWord" type="text" id="keywords" value="{&keyWord}"/></div>
		<div class="cmd"><input type="button" value="搜索" onclick="javascript:toSearch()" /></div>
		<div class="more" id="moreSearch"></div>
	</div>
	<div id="searchMenu">
		<ul class="blue u">高级搜索选项</ul>
		<ul>
			<li>
				<div class="fl">关键字：</div>
				<div class="fr">
					<select id="" name="">
						<option value="0">标签名称</option>
						<option value="1">标签简介</option>
					</select>
				</div>
			</li>
		</ul>
	</div>
</div>-->
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr>
			<td width="2%"></td>
			<td width="20%">标签名称</td>
			<td width="70%">标签简介</td>
			<td width="8%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
    <xnl:repeater sqlCommand="select tagId,tagName,tagDesc from sn_userTag"><itemtemplate>
			<tr>
				<td><!--<input type="checkbox" name="classListItem" id="" />--></td>
				<td>{$tagName}</td>
				<td>{$tagDesc}</td>
				<td><div class="Ico_copy" title="复制" onclick="copy({$tagid},'{$tagName}')"></div>
					<div class="Ico_edit" title="编辑" onclick="edit({$tagid})"></div>
					<div class="Ico_del" title="删除" onclick="deleteTag({$tagid})"></div>
				</td>
			</tr></itemtemplate>
            </xnl:repeater>
	</tbody>
	<tfoot><tr><td colspan="6"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<!--<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>-->
<script type="text/javascript">
//复制标签
function copy(tagId,tagName){
	top.Sio.Alert({width:300,height:170,title:'复制标签',src:'TemplateLabel/UserTag_setCopy.aspx?tagId='+tagId+"&tagName="+tagName,button:false});
}
//删除标签
function deleteTag(tagId){
	if(confirm('确定要删除吗？删除后不能恢复!'))location.href='userTag_action.aspx?action=delete&tagId='+tagId
}
//编辑标签
function edit(tagId){
	top.Sio.Alert({width:680,height:560,title:'编辑标签',src:'TemplateLabel/userTag_Modify.aspx?tagId='+tagId,button:false});
}

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
