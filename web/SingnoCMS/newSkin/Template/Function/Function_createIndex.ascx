<XNL:set><attrs><attr name="nodeid" type="int"><CMS.manage:common action="getcursiteid"></CMS.manage:common></attr></attrs></XNL:set>
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
				<select name="projectID" id="projectID" onChange="if(this.value>0)location.href='?projectID='+this.value">
				<option value="0">请选择模板方案</option>
				<xnl:repeater sqlcommand="select ProjectID ,ProjectName from SN_TemplateProject where siteID=@siteID">
				<attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
				<itemTemplate> <option value="{$projectID}">{$projectName}</option></itemTemplate>
				</xnl:repeater>
				</select>
			</td>
		</tr>
		</if>
		<else>
		<tr class="hide" id="progressTrow">
			<td colspan="2"><div id="progress"></div>
          <input type="button" name="cancleBtn" id="cancleBtn" value="取消生成"  onclick="toCancle()"/>
          </td>
		</tr>
		<tr class="hide" id="createTrow">
			<td colspan="2">
				<div class="grayBg">
						<input type="button" name="startBtn" id="startBtn" value="生成首页"  onclick="toCreate()"/>
                    </div>
			</td>
		</tr>
	</tbody>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();
var inter=-1;
var allPageCount=1;
var createPageCount=-1;
var state="init";
var pb;
Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=0&action=create&nodeid={!nodeid}&debug=true&r='+Math.random(),complete:initLoadComplate});
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
			//if(createPageCount==0)createPageCount=allPageCount;
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
function setProgress(progressNum)
{
	pb.setValue(Math.floor(progressNum));
}
function progress()
{
	Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=0&action=create&debug=true&nodeid={!nodeid}&r='+Math.random(),complete:progressLoadComplate});
}
function createProgressBar()
{
	pb=new Sio.ProgressBar({id:'pb1',renderTo:'progress',value:0,complete:function(v){
			clearInterval(inter);
			inter=-1;
			alert("完成,共生成"+allPageCount+"页");
			}});
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
function toCreate()
{
	$i('createTrow').className='hide';
	$i('progressTrow').className='';
	state="progress";//正在生成状态,当前有生成任务
	createProgressBar();
	Sio.ajax({url:'Function_CreateAction.aspx?projectID={&projectID}&pagestyle=0&action=create&nodeid={!nodeid}&r='+Math.random(),complete:createLoadComplate});
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
		var progressNum=Math.floor((createPageCount/allPageCount)*100);
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
	Sio.ajax({url:'Function_CreateAction.aspx?pagestyle=0&action=cancle&r='+Math.random(),method:'post',complete:function(e){alert(e);}});
}
</script>
</else>
</ifItem>
</xnl:if>
</body>
</html> 
