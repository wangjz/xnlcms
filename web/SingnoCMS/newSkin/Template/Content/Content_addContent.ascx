<xnl:set><attrs><attr name="siteid"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs></xnl:set>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<title></title>
<xnl:jquery></xnl:jquery>
<XNL:validator autoLoadJs="true" name="v1">
<validatorItem validatorgroup ="1" formid="form1" alertmessage="true" autotip="false" errorfocus="true" forcevalid="false" wideword="true" onsuccess="function(){}" submitonce ="true" onerror="function (msg){alert(msg)}" debug ="false">
</validatorItem>
</XNL:validator>
<style type="text/css">
.forms{ margin:5px 0}
#selectLabel{ width:150px; border:1px solid #ccc; height:14px; line-height:14px; padding:3px; margin:0;}
#showLabel{ position:absolute; margin:-30px 0 0 160px;*margin:-10px 0 0 0px; font-family:Verdana, Geneva, sans-serif; display:none}
#showLabel .arrow{ width:10px; height:18px; background:url(images/tags_bg_ico.gif); margin:10px 0 0 0; float:left; position:relative}
#showLabel .txt{ width:200px; height:280px; border:1px solid #ccc; margin:0 0 0 -1px; float:left; padding:5px;font-size:12px; font-family:Verdana, Geneva, sans-serif}
#showLabel .txt .title { height:30px; line-height:22px; background:url(images/insertExpress.gif) no-repeat; padding:0 0 0 20px;}
#showLabel .txt .title span{ padding:0 110px 0 0;}
#showLabel .txt .title a{ padding:5px; line-height:10px;}
#showLabel .txt .title a:hover{ background:#eee;}
#showLabel .txt .list{ width:200px; height:250px; line-height:180%; word-wrap: break-word; overflow:auto}
#showLabel .txt .list a{ margin:0 3px}
#showLabel .txt a{ color:#06C; text-decoration:none}
#showLabel .txt a:hover{ color:#f00; text-decoration:none}
</style>
</head>
<body>
<CMS.Manage:ContentForm action="{&action}" contentId="{&contentId}" nodeId="{&nodeid}"><attrs><attr name="sitenodeid"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs>
	<XNL:form name="form1" enablestate="true" validatorName="v1" sqlCommand="">
		<form action="content_action.aspx?action={&action}" id="form1" name="form1" method="post" >
			<div id="infoMain">
				<table class="forms" cellpadding="0" cellspacing="0">
					<tbody>
						<Area.ChannelList>
						<tr>
							<td width="80px">所属栏目：</td>
							<td>
								<select id="nodeid" name="nodeid" onChange="location.href='?action={&action}&contentId={&contentId}&nodeid='+this.value">
									<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;"  sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID={@siteNodeid}"><channelsItem><xnl:if><ifitem test="{&nodeid}" value="{$nodeid}">
													<if><option value="{$nodeId}" selected="selected"> {@depthTag}└ {$nodename}</option></if>
													<else><option value="{$nodeId}"> {@depthTag}└ {$nodename}</option></else></ifitem>
											</xnl:if></channelsItem>
									</CMS.Manage:channels>
								</select>
							</td>
						</tr>
						</Area.ChannelList>
						<Area.Title>
						<tr>
							<td>{@DisplayName.title}：</td>
							<td>{@Input.title}
							粗体:<input name="titleStyle_bold" type="checkbox" title="粗体" value="1" {title.isBold}/>
							斜体:<input name="titleStyle_italic" type="checkbox" title="斜体" value="1" {title.isItalic}/>
							下划线:<input name="titleStyle_u" type="checkbox" title="下划线" value="1" {title.isU}/>
							标题颜色： <input name="titleStyle_color" type="text" id="titleStyle_color" value="{title.color}"/>
							</td>
						</tr>
						</Area.Title>
						<Area.SubTitle>
						<tr>
							<td>{@DisplayName.SubTitle}：</td>
							<td>{@Input.SubTitle}</td>
						</tr>
						</Area.SubTitle>
						<Area.KeyWord>
						<tr>
							<td>{@DisplayName.KeyWord}：</td>
							<td>{@Input.KeyWord}</td>
						</tr>
						</Area.KeyWord>
						<Area.Summary>
						<tr>
							<td>{@DisplayName.Summary}：</td>
							<td>{@Input.Summary}</td>
						</tr>
						</Area.Summary>
						<Area.FileUrl>
						<tr>
							<td>{@DisplayName.FileUrl}</td>
							<td>{@Input.FileUrl}</td>
						</tr>
						</Area.FileUrl>
                        	<Area.UserDims>
						<tr>
							<td>{@DisplayName.UserDims}：</td>
							<td>{@Input.UserDims}</td>
						</tr>
						</Area.UserDims>
						<Area.Content>
						<tr>
							<td>{@DisplayName.Content}：</td>
							<td>{@Input.Content}</td>
						</tr>
						</Area.Content>
                        <Area.AddDate>
                        <tr>
							<td>{@DisplayName.AddDate}：</td>
							<td>{@Input.AddDate}</td>
						</tr>
    </Area.AddDate>
						<tr>
							<td colspan="2" align="center">
								<input name="contentId" type="hidden" id="contentId" value="{&contentId}" />
								<input name="" type="button" value="预览" />
								<input name="" type="submit" value="发布" />
							</td>
						</tr>
					</tbody>
				</table>
			</div>
			<div id="infoMain">
				<table width="422" cellpadding="0" cellspacing="0" class="forms">
					<tbody>
						<Area.ImageUrl>
						<tr>
							<td width="217">{@DisplayName.ImageUrl}：</td>
							<td width="195">{@Input.ImageUrl}</td>
						</tr>
						</Area.ImageUrl>
						<Area.Author>
						<tr>
							<td>{@DisplayName.Author}：</td>
							<td>{@Input.Author}</td>
						</tr>
						</Area.Author>
						<Area.Source>
						<tr>
							<td>{@DisplayName.Source}：</td>
							<td>{@Input.Source}</td>
						</tr>
						</Area.Source>
						<Area.linkUrl>
						<tr>
							<td>{@DisplayName.LinkUrl}：</td>
							<td>{@Input.LinkUrl}</td>
						</tr>
						</Area.linkUrl>
                        <Area.Tag>
                        	<tr>
							<td valign="top">内容tag：</td>
							<td><input id="xnl__tag"  size="50" name="xnl__tag" value="{@content.tags}" onClick="showTags(this)"/><div id="showLabel">
		<div class="arrow"></div>
		<div class="txt">
			<div class="title"><span>选择标签</span><a href="#" id="closeLabel"><strong>X</strong></a></div>
			<div class="list" id="labelList"></div>
		</div>
	</div></td>
					  </tr>
                      </Area.Tag>
                       <Area.contentGroup>
				<tr>
							<td>内容组：</td> 
							<td><Area.contentGroup.item><input name="groupid" type="checkbox" {@contentgroup.checked} id="_groupid{@contentgroup.id}" value="{@contentgroup.id}" /><label for="_groupid{@contentgroup.id}">{@contentgroup.name}</label>  </Area.contentgroup.item></td></tr></Area.contentgroup>
                      
						<tr>
							<td>分页方式：</td>
							<td>
								<select name="pageType" id="pageType" onChange="this.value==1?$i('pageWords_Div').className='':$i('pageWords_Div').className='hide'">
								  <option value="0" {[iif('{@pageType}'='0','selected="selected"','')]} >不分页</option>
								  <option value="1" {[iif('{@pageType}'='1','selected="selected"','')]}>自动分页</option>
								  <option value="2" {[iif('{@pageType}'='2','selected="selected"','')]}>手动分页</option>
								</select>
							<span id="pageWords_Div" class="{[iif('{@pageType}'='1','','hide')]}">每页字数<input name="pageWords" type="text" id="pageWords" size="8" value="{@pagewords}" /></span></td>
						</tr>
					<tr>
							<td valign="top">设置属性：</td>
							<td>
								<input name="IsRecommend" type="checkbox" id="IsRecommend" value="1" {[iif('{@IsRecommend}'='1','checked="checked"','')]}/><label for="IsRecommend">推荐</label> 
							  <input name="isHot" type="checkbox" id="isHot" value="1" {[iif('{@ishot}'='1','checked="checked"','')]}/><label for="isHot">热点</label><br /> 
						    <input name="isColor" type="checkbox" id="isColor" value="1" {[iif('{@isColor}'='1','checked="checked"','')]}/><label for="isColor">醒目</label> 
								<input name="isTop" type="checkbox" id="isTop" value="1" {[iif('{@isTop}'='1','checked="checked"','')]}/><label for="isTop">置顶</label>
					  </td>
					  </tr>
					<!--	<tr>
							<td>阅读权限：</td>
							<td>
								<input type="checkbox" name="userRight" id="userRight1" /><label for="userRight1">栏目管理员</label> 
								<input type="checkbox" name="userRight" id="userRight2" /><label for="userRight2">高级会员</label> 
								<input type="checkbox" name="userRight" id="userRight3" /><label for="userRight3">普通会员</label> 
								<input type="checkbox" name="userRight" id="userRight4" /><label for="userRight4">游客</label>
							</td>
						</tr>-->
                        
						<tr>
							<td>允许评论：</td>
							<td>
				        <input name="isComment" type="radio" id="isCommented1" value="1" {[iif('{@isComment}'='1','checked="checked"','')]}/><label for="isCommented1">允许</label> 
								<input name="isComment" type="radio" id="isCommented2" value="0" {[iif('{@isComment}'='0','checked="checked"','')]}/><label for="isCommented2">不允许</label></td>
						</tr>
	
						<tr>
							<td height="65">状态：</td>
							<td>
							  <input id="state1" value="-1" type="radio" name="State"   {@State.CaoGao} /><label for="state1">草稿</label>
								<input id="state2" value="0" type="radio" name="State"   {@State.ShenHe} /><label for="state2">待审核</label>
								<input id="state3" name="State" type="radio" value="99"  {@State.ShenHeOK} /><label for="state3">终审通过</label>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</form>
	</XNL:form>
</CMS.Manage:ContentForm>


<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/classDate.js"></script>
<script type="text/javascript">
//Sio.Text();
Sio.Button();

//布局控制
var $infoMain = $i('infoMain'),
	$infoRight = $i('infoRight'),
	_doc = document.documentElement;
resize();
window.onresize = resize;
function resize(){
	$infoMain.style.width = ((_doc.clientWidth - 271 >= 0) ? _doc.clientWidth - 271 : 0) + 'px';
}
var tags_arr=[];
var isTag=false;
var $label;
function showTags(obj)
{
if(!isTag){
	isTag=true;
	$label = obj;
	$show = $i('showLabel');
	//焦点移入时显示标签列表
	$show.style.display = 'block';	
	//ajax获得数据
	$.get('../inc/inc_common.aspx?action=getcontenttag&siteid={!siteid}&_r='+Math.random(),null,function (data, textStatus){
		var tagsObj=(eval(data));
		if(tagsObj.length>1)
		{
			for(var i=1;i<tagsObj.length;i++)$i('labelList').innerHTML+=((i>1?',':'')+'<a href="#">'+tagsObj[i].n+'</a>');
		}
	});

	$label.onfocus = function(){
		$show.style.display = 'block';
	};
	//关闭标签列表
	$i('closeLabel').onclick = function(e){
		stopDefault(e);
		$show.style.display = 'none';
	}
	//用委托事件处理点击插入标签
	$i('labelList').onclick = function(e){
		//获取事件指对象
		e = e ? e.target : event.srcElement;
		if(e.nodeName.toLowerCase() == 'a'){
			var val = $label.value;
			if(val == ''){
				$label.value = e.innerHTML;
				//不能重复添加标签
			}else{// if(val.indexOf(e.innerHTML) < 0){
				var _val=','+val+',';
				if(_val.indexOf(','+e.innerHTML+',') < 0)$label.value = val + (val[val.length-1] != ',' ? ',' : '') + e.innerHTML;
			}
		}
		stopDefault(e);
	}
	/*
	$label.onkeypress = function(e){
		stopDefault(e);
	}
	*/
	//取消链接默认行为
	function stopDefault(e){
		(e && e.preventDefault) ? e.preventDefault() : event.returnValue = false;
	}
}
}



</script>
</body>
</html>
