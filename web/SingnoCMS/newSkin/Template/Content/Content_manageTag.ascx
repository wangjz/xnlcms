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
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 
  全选</span>
<div class="line"></div>
	<div class="Ico_del" title="删除tag"></div><span><a href="javascript:toAddTag();">添加tag</a></span>
</div>
<table class="tables" cellpadding="0" cellspacing="0">
	<thead>
		<tr class="head">
			<td width="2%"></td>
			<td width="18%">tag名称</td>
			<td width="35%">使用次数</td>
			<td width="35%">使用级别</td>
			<td width="10%">操作</td>
		</tr>
	</thead>
	<tbody id="ctrl_cmd">
		<XNL:repeater sqlCommand="select * from sn_contenttag where ct_siteid=@SiteId"><attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
			<itemTemplate>
				<tr>
					<td><input type="checkbox" name="" id="" /></td>
					<td><a href="Content_manageTagInfo.aspx?tagid={$ct_id}">{$ct_name}</a></td>
					<td>{$ct_usenum}</td>
					<td>{$ct_level}</td>
					<td class="icons">
                       <a href="#"><div class="Ico_edit" title="修改tag"></div></a>
						<a href="content_action.aspx?action=deletetag&tagid={$ct_id}"><div class="Ico_del" title="删除tag"></div></a>
					</td>
				</tr>
			</itemTemplate>
		</XNL:repeater>
	</tbody>
	<tfoot><tr><td colspan="5"></td></tr></tfoot>
</table>

<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Button();

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
function toAddTag()
{
	top.Sio.Alert({width:300,height:150,title:'添加内容tag',src:'Content/Content_addtag.html',button:false,okFn:function(args){
		alert("ok")
	}});
}
</script>
</body>
</html> 
