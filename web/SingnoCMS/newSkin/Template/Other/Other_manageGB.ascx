<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/Other.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JS/calendar.min.js"></script>
<style type="text/css">
.tables{ margin:5px 0 0 0}
</style>
</head>
<body>
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_audit" title="审核"></div>
	<div class="Ico_del" title="删除"></div><div class="line"></div>
	<div id="searchBar" class="_hover">
		<div class="input"><input name="keyWord" type="text" id="keywords" value="{&keyWord}"/></div>
		<div class="cmd"><input type="button" value="搜索" onclick="javascript:toSearch()" /></div>
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
	<thead>
		<tr>
			<td width="2%"></td>
			<td width="38%">留言标题</td>
			<td width="20%">留言人</td>
			<td width="20%">留言日期</td>
			<td width="10%">状态</td>
			<td width="10%" align="center">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<tr>
			<td><input type="checkbox" name="classListItem" id="" /></td>
			<td>留言标题</td>
			<td>留言人</td>
			<td>留言日期</td>
			<td>未审核</td>
			<td class="icons">
				<div class="Ico_audit" title="审核"></div>
				<div class="Ico_del" title="删除"></div>
			</td>
		</tr>
	</tbody>
	<tfoot><tr><td colspan="6"></td></tr></tfoot>
</table>


<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/classDate.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/advSearch.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Text('searchMenu');
Sio.Button('ctrl_cmd');

new Sio.Calendar({id:'DateFrom',renderTo:'startData',width:163,align:'right'});

new Sio.Calendar({id:'DateTo',renderTo:'endData',width:163,align:'right'});

//获取值
function getVal(id){
	return $i(id).value;
};
//开始搜索
function toSearch(){
	location.href = "?nodeid=" + getVal("classOf") + "&keyWord=" + getVal("keywords") + "&DateFrom=" + getVal("DateFrom") + "&dateTo=" + getVal("DateTo") + "&SearchType=" + getVal("targets") + "&State=" + getVal("State");
};

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
