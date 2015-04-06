<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body onload="onLoad()">
<form action="Config_SystemAction.aspx?action=modify&type=base" method="post" name="form1" id="form1" onsubmit="submitTest();return false;">
<table class="forms">
	<tbody>
    <CMS.Manage:MSystemConfig action="viewbase">
		<tr>
			<td valign="top">后台模板方案：</td>
			<td>
				<select name="projectName" id="projectName">
                	<xnl:foreach foreachvar="{@AllProject}" split="|" stringsplitoptions="RemoveEmpty">
                    	<foreachItem>
                        <xnl:if><ifItem value="{@foreachValue}" test="{@curProject}"><if> <option value="{@foreachValue}" selected="selected" >{@foreachValue}</option></if>
                        <else> <option value="{@foreachValue}" >{@foreachValue}</option></else></ifItem></xnl:if>
						</foreachItem>
                    </xnl:foreach>
				</select>
			</td>
		</tr>
		<tr>
			<td>后台访问限制：</td>
			<td>
				<select name="AccessType" id="AccessType">
                 <xnl:if>
                 <ifItem value="{@accesstype}" test="None"><if>
                 	<option value="None">无访问限制</option>
					<option value="Black">启用黑名单，禁止黑名单中的IP进行访问，其余允许访问</option>
                    <option value="White">启用白名单，允许白名单中的IP进行访问，其余禁止访问</option></if></ifItem>
               <ifItem value="{@accesstype}" test="Black"><if>
               		<option value="None">无访问限制</option>
					<option value="Black" selected="selected">启用黑名单，禁止黑名单中的IP进行访问，其余允许访问</option>
                    <option value="White">启用白名单，允许白名单中的IP进行访问，其余禁止访问</option></if></ifItem>
                    <ifItem value="{@accesstype}" test="White">
                    <if><option value="None">无访问限制</option>
                    <option value="Black">启用黑名单，禁止黑名单中的IP进行访问，其余允许访问</option>
                    <option value="White" selected="selected">启用白名单，允许白名单中的IP进行访问，其余禁止访问</option></if></ifItem>
                    </xnl:if>
				</select>
			</td>
		</tr>
		<tr id="blackList" class="hide">
			<td></td>
			<td><input id="addBlackList" type="button" value="添加黑名单" /> <input id="manageBlackList" type="button" value="黑名单管理" />
		    <input type="hidden" name="BlackIp" id="BlackIp" /></td>
		</tr>
		<tr id="whiteList" class="hide">
			<td></td>
			<td><input id="addWhiteList" type="button" value="添加白名单" /> <input id="manageWhiteList" type="button" value="白名单管理" />
		    <input type="hidden" name="WhiteIp" id="WhiteIp" /></td>
		</tr>
	</CMS.Manage:MSystemConfig>
    </tbody>
    
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
function onLoad()
{
	switch($i('AccessType').value){
		case 'None':
			$i('blackList').className = 'hide';
			$i('whiteList').className = 'hide';
			break;
		case 'Black':
			$i('blackList').className = '';
			$i('whiteList').className = 'hide';
			break;
		case 'White':
			$i('blackList').className = 'hide';
			$i('whiteList').className = '';
			break;
	}
}
Sio.Text();
Sio.Button();

//
$i('AccessType').onchange = function(){
	switch(this.value){
		case 'None':
			$i('blackList').className = 'hide';
			$i('whiteList').className = 'hide';
			break;
		case 'Black':
			$i('blackList').className = '';
			$i('whiteList').className = 'hide';
			break;
		case 'White':
			$i('blackList').className = 'hide';
			$i('whiteList').className = '';
			break;
	}
}
//添加黑名单
$i('addBlackList').onclick = function(){
	top.Sio.Alert({width:450,height:210,title:'添加黑名单',src:'Config/Config_addBlackList.html',button:false,okFn:function(args){
		
		alert("ok")
	}});
}
//黑名单管理
$i('manageBlackList').onclick = function(){
	top.Sio.Alert({width:500,height:300,title:'黑名单管理',src:'Config/Config_manageBlackList.aspx',button:false,okFn:function(){
		
	}});
}
//添加白名单
$i('addWhiteList').onclick = function(){
	top.Sio.Alert({width:450,height:210,title:'添加白名单',src:'Config/Config_addWhiteList.html',button:false,okFn:function(){
		
	}});
}
//黑名单管理
$i('manageWhiteList').onclick = function(){
	top.Sio.Alert({width:500,height:300,title:'白名单管理',src:'Config/Config_manageWhiteList.aspx',button:false,okFn:function(){
		
	}});
}
/*function colseAlert()
{
	//alert("close");
}*/
function submitTest()
{
	var projectName=$i("projectName").value;
	var AccessType=$i("AccessType").value;
	Sio.ajax({url:'Config_SystemAction.aspx?action=modifySystemBase',method:'post',complete:complete,data:{projectName:projectName,AccessType:AccessType}});
	//new Sio.Ajax("Config_SystemAction.aspx?action=modifySystemBase",{parameters:{projectName:projectName,AccessType:AccessType},onComplete:complete})
}

function complete(request)
{
	var obj=eval("("+request+")");
	if(obj.state=="Y")
	{
		document.location.reload();
	}
	else
	{
		alert(obj.msg);
	}
}
</script>
</body>
</html>