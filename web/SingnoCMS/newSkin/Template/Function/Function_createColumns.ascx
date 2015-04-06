<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/Function.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<table class="forms" cellpadding="0" cellspacing="0">
	<tbody>
		<xnl:if>
		<ifItem test="" value="{&projectId}">
		<if>
		<tr>
			<td width="120px">请选择模板方案：</td>
			<td>
				<select name="projectID" id="projectID" onchange="if(this.value>0)location.href='?projectID='+this.value">
					<option value="0">请选择模板方案</option>
					<xnl:repeater sqlcommand="select ProjectID ,ProjectName from SN_TemplateProject where siteID=@siteID">
						<attrs>
							<attr name="siteid" type="int">
								<CMS.manage:common action="getSiteIDBySN"></CMS.manage:common>
							</attr>
						</attrs>
						<itemTemplate>
							<option value="{$projectID}">{$projectName}</option>
						</itemTemplate>
					</xnl:repeater>
				</select>
			</td>
		</tr>
		</if>
		<else>
		<tr id="createTrow" class="hide">
			<td valign="top">生成栏目页：</td>
			<td>
				<select name="NodeList" size="20"  multiple="multiple"  id="NodeList">
				<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;"  sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid">
<attrs><attr name="nodeid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs>
					<channelsItem>
						<option value="{$nodeid}" selected="selected">{@depthTag}└ {$nodename}</option>
					</channelsItem>
				</CMS.Manage:channels>
				</select>
				<input type="checkbox" name="selectAll" id="selectAll" value="" />全选<div class="grayBg"><input type="button" name="startBtn" id="startBtn" value="开始生成" onclick="toCreate()"/></div>
			</td>
		</tr>
		<tr id="progressTrow" class="hide"><td colspan="2"><div id="progress"></div>
          <input type="button" name="cancleBtn" id="cancleBtn" value="取消生成"  onclick="toCancle()"/>
          </td>
		</tr>
	</tbody>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();
//全选栏目
var $pl = $i('NodeList').options;
$i('selectAll').onclick = function(){
	if(this.checked){
		Sio.each($pl,function(i,e){
			e.selected = 'selected';
		});
	}else{
		Sio.each($pl,function(i,e){
			e.selected = '';
		});
	}
}
function getSelectNodes()
{
	node_arr=[];
	Sio.each($pl,function(i,e){
		if(e.selected){
			node_arr.push(e.value);
		}
	});
}
var node_arr=[];
var pb;
function toCreate()
{
	getSelectNodes();
	if(node_arr.length==0)
	{
		alert("请选择要生成的栏目!");return;
	}
	$i('createTrow').className='hide';
	$i('progressTrow').className='';
	state="progress";//正在生成状态,当前有生成任务
	var nodesStr=node_arr.join(",");
	createProgressBar();
	Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=1&action=create&r='+Math.random(),method:'post',complete:createLoadComplate,data:{nodeList:nodesStr}});
}
///////////////////////////////////////////////////////////////////
var inter=-1;
var allPageCount=1;
var createPageCount=-1;
var state="init";
Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=1&action=create&debug=true&r='+Math.random(),method:'post',complete:initLoadComplate});
function initLoadComplate(request)
{
	var obj=null;
	try
	{
		obj=eval("("+request+")");
		if(obj.state=="NO")
		{
			//当前没有生成任务，可以生成
			$i('createTrow').className='';
		}
		else
		{
			state="progress";//正在生成状态,当前有生成任务
			createProgressBar();
			allPageCount=obj.all;
			createPageCount=obj.cur;
			$i('progressTrow').className='';
			var progressNum=(createPageCount/obj.all)*100;
			if(progressNum==100)
			{
				setProgress(progressNum);
			}
			else
			{
				if(inter==-1)inter=setInterval("progress()",1000);
			}
		}
	}
	catch(e)
	{
		clearInterval(inter);
		inter=-1;
		if(request.indexOf("location.href")>=0)
		{
			alert("未登陆或登陆超时");
			top.location.href="../default.html";
		}
		else
		{
			alert("模板解析错误:"+request);
		}
	}
}
function createProgressBar()
{
	pb=new Sio.ProgressBar({id:'pb1',renderTo:'progress',value:0,complete:function(v){
			clearInterval(inter);
			inter=-1;
			alert("完成,共生成"+allPageCount+"页");
			}});
}

function setProgress(progressNum)
{
	pb.setValue(Math.floor(progressNum));
}
function progress()
{
	Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=1&action=create&debug=true&r='+Math.random(),method:'post',complete:progressLoadComplate});
}
function progressLoadComplate(request)
{
	clearInterval(inter);
	inter=-1;
	var obj=null;
	try
	{
		obj=eval("("+request+")");
		createPageCount=obj.cur;
		if(obj.state=="NO"&&createPageCount==0)createPageCount=allPageCount;
		var progressNum=(createPageCount/allPageCount)*100;
		setProgress(progressNum);
		if(progressNum<100)inter=setInterval("progress()",1000);
	}
	catch(e)
	{
		if(request.indexOf("location.href")>=0)
		{
			alert("未登陆或登陆超时");
			top.location.href="../default.html";
		}
		else
		{
			alert("模板解析错误:"+request);
		}
	}
}


function createLoadComplate(request)
{
	var obj=null;
	try
	{
		obj=eval("("+request+")");
		allPageCount=obj.all;
		createPageCount=obj.cur;
		if(obj.state=="NO"&&createPageCount==0)createPageCount=allPageCount;
		var progressNum=(createPageCount/allPageCount)*100;
		setProgress(progressNum);
		if(obj.state=="YES")
		{	
			if(progressNum<100){if(inter==-1)inter=setInterval("progress()",1000);}
		}
	}
	catch(e)
	{
		clearInterval(inter);
		inter=-1;
		if(request.indexOf("location.href")>=0)
		{
			alert("未登陆或登陆超时");
			top.location.href="../default.html";
		}
		else
		{
			alert("模板解析错误:"+request);
		}
		
	}
}
function toCancle()
{
	clearInterval(inter);
	inter=-1;
	Sio.ajax({url:'Function_CreateAction.aspx?pagestyle=1&action=cancle&r='+Math.random(),complete:function(e){ alert(e);}});
}
</script>
</else></ifItem></xnl:if>
</body>
</html> 
