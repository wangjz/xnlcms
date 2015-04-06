<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.tables{ margin:0 10px 0 0; border-left:10px solid #fff; border-right:10px solid #fff}
input.X_input_text{ width:300px}
</style>
</head>
<body>
<div id="navigator" class="navigator">
	<span><input id="selectAll" type="checkbox" name=""/> 全选</span><div class="line"></div>
	<div class="Ico_del" title="删除" onclick="deleteSelect()"></div> 
</div>
<table class="tables">
	<thead>
		<tr>
			<td width="2%"></td>
			<td width="83%">黑名单标题</td>
			<td width="15%" align="center">操作</td>
		</tr>
	</thead>
    <CMS.Manage:MSystemConfig  action="viewblack">
	<tbody id="ctrl_cmd">
    <xnl:foreach foreachvar="{@allip}" split="|" stringsplitoptions="RemoveEmpty">
        	<foreachItem>
       <tr>
			<td><input type="checkbox" name="classListItem" id="ip{@foreachid}" value="{@foreachValue}"/></td>
			<td><xnl:if><ifItem test="{[iif('{&action}'='edit' and {&id}={@foreachid},'1','0')]}" value="1"><if><a name="edit" id="edit"></a>
			  <input name="ip" type="text" id="ip" value="{@foreachValue}" size="25" maxlength="36"/>
		    <input name="updateBtn" type="button" value="更新" id="updateBtn" onclick="update('{@foreachValue}')" /></if><else>{@foreachValue}</else></ifItem></xnl:if></td>
			<td class="icons">
				<div class="Ico_del" title="删除" onclick="del('{@foreachValue}')"></div> 
				<div class="Ico_edit" title="编辑" onclick="edit({@foreachid})"></div>
			</td>
        </tr>
			</foreachItem>
        </xnl:foreach>
	</tbody>
    </CMS.Manage:MSystemConfig>
	<tfoot><tr><td colspan="3"></td></tr></tfoot>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/icoHover.js"></script>
<script type="text/javascript" src="../JS/selectAllItem.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');

function edit(id)
{
	location.href="?action=edit&id="+id+"#edit";
}

function del(ip)
{
	Sio.ajax({url:'Config_SystemAction.aspx?action=delblack',method:'post',complete:complete,data:{ip:ip}});
	//new Sio.Ajax("Config_SystemAction.aspx?action=delblack",{parameters:{ip:ip},onComplete:complete});
}
function deleteSelect()
{
	var obj=document.getElementsByName("classListItem");
	var ipStr="";
	for(var i=0;i<obj.length;i++)
	{
		var itemObj=obj.item(i);
		if(itemObj.checked)
		{
			ipStr+=(i>0?",":"")+itemObj.value;
		}
	}
	Sio.ajax({url:'Config_SystemAction.aspx?action=delblack',method:'post',complete:complete,data:{ip:ipStr}});
	//new Sio.Ajax("Config_SystemAction.aspx?action=delblack",{parameters:{ip:ipStr},onComplete:complete});
}
function update(ip)
{
	var newIp=Sio.trim($i('ip').value);
	if(newIp==ip)
	{
		document.location.href="config_manageBlackList.aspx";
		return;
	}
	Sio.ajax({url:'Config_SystemAction.aspx?action=updateblack',method:'post',complete:complete,data:{ip:ip,newip:newIp}});
	//new Sio.Ajax("Config_SystemAction.aspx?action=updateblack",{parameters:{ip:ip,newip:newIp},onComplete:complete});
}
function complete(request)
{
	var obj=eval("("+request+")");
	if(obj.state=="Y")
	{
		document.location.href="config_manageBlackList.aspx";
	}
	else
	{
		alert(obj.msg);
	}
}
</script>
</body>
</html>