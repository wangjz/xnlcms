﻿<XNL:set><attrs><attr name="nodeid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs></XNL:set>
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
.tables{ margin:5px 0 0 0}
</style>
</head>
<body>
<div id="classList"></div>
<div id="manageInfo">
	<div id="navigator" class="navigator">
		<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
		<div class="Ico_create" title="生成"></div>
		<div class="Ico_audit" title="审核"></div>
		<div class="Ico_shift" title="转移"></div>
		<div class="Ico_del" title="删除"></div><div class="line"></div>
		<div id="searchBar" class="_hover">
			<div class="input"><input name="keyWord" type="text" id="keywords" value="{&keyWord}"/></div>
			<div class="cmd"><input type="button" value="搜索" onClick="javascript:toSearch()" /></div>
			<div class="more" id="moreSearch"></div>
		</div>
		<div id="searchMenu">
			<ul class="blue u">高级搜索选项</ul>
			<ul>
				<li>
					<div class="fl">栏目：</div>
					<div class="fr">
						<select id="classOf" name="nodeid">
							<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;" nodeid="{!nodeid}" sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid">
								<channelsItem>
									<xnl:if>
										<ifitem test="{&nodeid}" value="{$nodeid}">
											<if>
												<option value="{$nodeId}" selected="selected"> {@depthTag}└ {$nodename}</option>
											</if>
											<else>
												<option value="{$nodeId}"> {@depthTag}└ {$nodename}</option></else>
										</ifitem>
									</xnl:if>
								</channelsItem>
							</CMS.Manage:channels>
						</select>
					</div>
				</li>
				<li>
					<div class="fl">时间起：</div>
					<div class="fr" id="startData"></div>
				</li>
				<li>
					<div class="fl">时间至：</div>
					<div class="fr" id="endData"></div>
				</li>
				<li>
					<div class="fl">目标：</div>
					<div class="fr">
						<select id="targets" name="SearchType">
						<XNL:repeater sqlcommand="select a.FieldName,a.DisplayName from SN_ModelDescript a,SN_Model b where a.ModelName=b.ModelName and b.ModelID=@modelId and a.IsVisible=1 order by a.indexid">
							<attrs>
								<attr name="modelid" type="int">
									<CMS.Manage:Common action="getmodelid" nodeid="{!nodeid}"></CMS.Manage:Common>
								</attr>
							</attrs>
							<itemTemplate>
								<xnl:if>
									<ifItem  test="{&SearchType}" value="{$fieldName}">
										<if>
											<option value="{$fieldName}" selected="selected">{$displayName}</option>
										</if>
										<else>
											<option  value="{$fieldName}">{$displayName}</option>
										</else>
									</ifItem>
								</xnl:if>
							</itemTemplate>
							<xnl:if>
								<ifItem test="{&SearchType}" value="ID">
									<if>
										<option value="ID">内容ID</option>
									</if>
									<else>
										<option value="ID">内容ID</option>
									</else>
								</ifItem>
								<ifItem test="{&SearchType}" value="InputUser">
									<if>
										<option value="InputUser">添加者</option>
									</if>
									<else>
										<option value="InputUser">添加者</option>
									</else>
								</ifItem>
								<ifItem test="{&SearchType}" value="LastEditUser">
									<if>
										<option value="LastEditUser">最后修改者</option>
									</if>
									<else>
										<option value="LastEditUser">最后修改者</option>
									</else>
								</ifItem>
							</xnl:if>
						</XNL:repeater>
						</select>
					</div>
				</li>
				<li>
					<div class="fl">状态：</div>
					<div class="fr">
						<select id="State" name="State">
							<xnl:if>
								<ifItem test="{&State}" value="ALL">
									<if>
										<option value="ALL" selected="selected">全部</option>
									</if>
									<else>
										<option value="ALL">全部</option>
									</else>
								</ifItem>
								<ifItem test="{&State}" value="99">
									<if>
										<option value="99" selected="selected">已审核</option>
									</if>
									<else>
										<option value="99">已审核</option>
									</else>
								</ifItem>
								<ifItem test="{&State}" value="0">
									<if>
										<option value="0" selected="selected">未审核</option>
									</if>
									<else>
										<option value="0">未审核</option>
									</else>
								</ifItem>
								<ifItem test="{&State}" value="1">
									<if>
										<option value="1" selected="selected">投稿</option>
									</if>
									<else>
										<option value="1">投稿</option>
									</else>
								</ifItem>
								<ifItem test="{&State}" value="-2">
									<if>
										<option value="-2" selected="selected">退稿</option>
									</if>
									<else>
										<option value="-2">退稿</option>
									</else>
								</ifItem>
								<ifItem test="{&State}" value="-1">
									<if>
										<option value="-1" selected="selected">草稿</option>
									</if>
									<else>
										<option value="-1">草稿</option>
									</else>
								</ifItem>
							</xnl:if>
						</select>
					</div>
				</li>
			</ul>
		</div>
	</div>
	<table class="tables" cellpadding="0" cellspacing="0">
		<CMS.Manage:PageContentList nodeid="{&nodeid}" keyword="{&keyword}" dateFrom="{&dateFrom}" dateTo="{&dateTo}" searchType="{&searchType}" state="{&state}" pageNum="{&pageNum}" pageName="page">
			<PageContentList.HeadTemplate>
			<thead>
				<tr>
					<td width="2%"></td><PageContentList.Head.Cols>
					<td >{@pageContentList.ColDisplayName}</td></pageContentList.Head.Cols>
					<td width="25%" align="center">操作</td>
				</tr>
			</thead>
			</PageContentList.HeadTemplate>
			<tbody id="ctrl_cmd">
				<pageContentList.ItemTemplate>
					<tr>
						<td><input type="checkbox" name="classListItem" id="" /></td>
					  <PageContentList.Item.Cols><td>{@fieldName}</td> </PageContentList.Item.Cols>
						<td align="center" class="icons">
							<div id="setInfoAttr" class="Ico_attr" title="设定属性"></div>
							<a href="M_Content_addInfo.aspx?action=modify&nodeid={@item.nodeid}&contentId={@item.id}"><div class="Ico_edit" title="编辑"></div></a>
							<div class="Ico_create" title="生成"></div>
							<div class="Ico_audit" title="审核"></div>
							<div class="Ico_shift" title="转移"></div>
							<a href="M_Content_Action.aspx?action=delete&nodeid={@item.nodeid}&contentId={@item.id}"><div class="Ico_del" title="删除"></div></a>
						</td>
					</tr>
				</pageContentList.ItemTemplate>
		</tbody>
			<pageContentList.NoRecordTemplate>
			没有找到相关数据
			</pageContentList.NoRecordTemplate>
		</CMS.Manage:PageContentList>
		<tfoot><tr><td colspan="5"></td></tr></tfoot>
	</table>
</div>


<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../inc/classDate.aspx"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Text('searchMenu');
Sio.Button('ctrl_cmd');

new Sio.Calendar({id:'DateFrom',renderTo:'startData',width:163,align:'right'});

new Sio.Calendar({id:'DateTo',renderTo:'endData',width:163,align:'right'});

//设置文章属性
$i('setInfoAttr').onclick = function(){
	top.Sio.Alert({width:400,height:300,src:'Content/M_Content_showAttr.html',title:'设置文章属性',button:false});
}

//获取值
function getVal(id){
	return $i(id).value;
};
//开始搜索
function toSearch(){
	location.href = "?nodeid=" + getVal("classOf") + "&keyWord=" + getVal("keywords") + "&DateFrom=" + getVal("DateFrom") + "&dateTo=" + getVal("DateTo") + "&SearchType=" + getVal("targets") + "&State=" + getVal("State");
};

//构建栏目列表
new Sio.Tree({id:'Class',width:150,data:classDate,renderTo:'classList',url:"../images/Plugin",fn:function(e){
	alert(e);
}});

//布局控制
var $classList = $i('classList'),
	$manageInfo = $i('manageInfo'),
	_doc = document.documentElement;
resize();
window.onresize = resize;
function resize(){
	$classList.style.height = _doc.clientHeight + 'px';
	$manageInfo.style.width = ((_doc.clientWidth - 150 >= 0) ? _doc.clientWidth - 150 : 0) + 'px';
	$manageInfo.style.height = _doc.clientHeight + 'px';
}

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
