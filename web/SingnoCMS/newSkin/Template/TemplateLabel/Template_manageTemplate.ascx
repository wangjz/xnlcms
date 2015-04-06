<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.tables td{text-align:center;}
.tables td.left{ text-align:left}
</style>
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
				<div class="fl">模版类型：</div>
				<div class="fr">
					<select id="templateType" name="templateType">
						<option value="0">首页模板</option>
						<option value="1">栏目模板</option>
						<option value="2">内容页模板</option>
						<option value="3">单页模板</option>
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
			<td width="25%" class="left">
				<div class="selectCmd"><div title="全选"></div></div>
				<div class="infoTitle">模版名称</div>
			</td>
			<td width="28%" class="left">模版文件</td>
			<td width="15%">模版类型</td>
			<td width="15%">使用次数</td>
			<td width="15%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<xnl:repeater  sqlCommand="select templateID,TemplateName,TemplateStyle,TemplateFileName,UseNumber,IsDefault from SN_Template where siteid=@siteid order by TemplateStyle,UseNumber desc">
			<attrs>
				<attr name="siteid" type="int">
					<CMS.manage:common action="getsiteidbysn"></CMS.manage:common>
				</attr>
			</attrs>
			<itemTemplate>
			<tr>
				<td><!--<input type="checkbox" name="classListItem" id="" />--></td>
				<td class="left">
					<a href="Template_ModifyTemplate.aspx?templateID={$templateID}">{$TemplateName}</a> {[iif({$IsDefault}=1,'[默认]','')]}
				</td>
				<td class="left">{$TemplateFileName}</td>
				<td>
					<xnl:if><ifItem value="0" test="{$TemplateStyle}"><if>首页模板</if></ifItem>
						<ifItem value="1" test="{$TemplateStyle}"><if>栏目模版</if></ifItem>
						<ifItem value="2" test="{$TemplateStyle}"><if>内容模板</if></ifItem>
						<ifItem value="3" test="{$TemplateStyle}"><if>单页模板</if></ifItem></xnl:if>
				</td>
				<td>{[iif({$TemplateStyle}>2,{$UseNumber}+1,{$UseNumber})]}</td>
				<td>
					<div class="Ico_copy" title="复制" onclick="copyTemplate({$templateid},'{$TemplateName}')"></div>
					<div class="Ico_del" title="删除" onclick="if(confirm('确定要删除吗？删除后不能恢复!'))location.href='template_action.aspx?action=delete&templateId={$templateId}'"></div>
                         <xnl:if><ifItem value="1"><attrs><attr name="test">{[iif({$TemplateStyle}<3 and {$IsDefault}=0,1,0)]}</attr></attrs><if><a href="Template_action.aspx?action=setdefault&templateId={$templateid}"><div class="Ico_default" title="设为默认"></div></a></if></ifItem></xnl:if>
				</td>
			</tr>
			</itemTemplate>
		</xnl:repeater>
	</tbody>
	<tfoot><tr><td colspan="6"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<!--<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>-->
<script type="text/javascript">

//获取值
function getVal(id){
	return $i(id).value;
};
//开始搜索
function toSearch(){
	
};

function copyTemplate(id,name)
{
	top.Sio.Alert({width:300,height:170,title:'复制模板',src:'TemplateLabel/Template_setCopy.aspx?action=copy&templateId='+id+"&templateName="+name,button:false,okFn:function(){
		
	}});
}
//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
