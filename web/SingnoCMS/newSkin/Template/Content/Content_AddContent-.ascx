<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JS/calendar.min.js"></script>
<xnl:jquery></xnl:jquery>
<XNL:validator autoLoadJs="true" name="v1">
<validatorItem validatorgroup ="1" formid="form1" alertmessage="true" autotip="false" errorfocus="true" forcevalid="false" wideword="true" onsuccess="function(){}" submitonce ="true" onerror="function (msg){alert(msg)}" debug ="false">
</validatorItem>
</XNL:validator>
<title></title>
<style type="text/css">
.forms{ margin:5px 0}
</style>
</head>
<body>
<CMS.Manage:ContentForm action="{&action}" contentId="{&contentId}" nodeId="{&nodeid}">
<attrs>
		<attr name="siteNodeid" type="int">
			<CMS.manage:common action="getCurSiteId"></CMS.manage:common>
		</attr>
	</attrs>
	<XNL:form name="form1" enablestate="true" validatorName="v1" sqlCommand="">
		<form action="content_action.aspx?action={&action}" id="form1" name="form1" method="post" >
			<div id="infoMain">
				<table class="forms" cellpadding="0" cellspacing="0">
					<tbody id="ctrl_cmd">
						<Area.ChannelList>
						<tr>
							<td width="100px">所属栏目：</td>
							<td>
								<select id="nodeid" name="nodeid" onchange="location.href='?action={&action}&contentId={&contentId}&nodeid='+this.value">
									<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;" nodeid="{@siteNodeid}" sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid">
										<channelsItem>
											<xnl:if>
													<ifitem test="{&nodeid}" value="{$nodeid}">
													<if>
														<option value="{$nodeId}" selected="selected"> {@depthTag}└ {$nodename}</option>
													</if>
													<else>
														<option value="{$nodeId}"> {@depthTag}└ {$nodename}</option>
													</else>
												</ifitem>
											</xnl:if>
										</channelsItem>
									</CMS.Manage:channels>
								</select>
							</td>
						</tr>
						<tr>
							<td>标题：</td>
							<td><input type="text" name="" id="titleInput" /></td>
						</tr>
						<tr>
							<td>标题样式：</td>
							<td>
								<input id="titleStyle1" name="titleStyle_bold" type="checkbox" value="1" {title.isBold}/><label for="titleStyle1">粗体</label>
								<input id="titleStyle2" name="titleStyle_italic" type="checkbox" value="1" {title.isItalic}/><label for="titleStyle2">斜体</label>
								<input id="titleStyle3" name="titleStyle_u" type="checkbox" value="1" {title.isU}/><label for="titleStyle3">下划线</label>
							</td>
						</tr>
						<tr>
							<td>副标题：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td>关键字：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td valign="top">导读：</td>
							<td><textarea name="" id="" cols="30" rows="4"></textarea></td>
						</tr>
						<tr>
							<td>附件</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td valign="top">内容：</td>
							<td><textarea name="" id="" cols="30" rows="10"></textarea></td>
						</tr>
						<Area.UserDims>
						<tr>
							<td><!--{@DisplayName.UserDims}-->用户自定义：</td>
							<td><!--{@Input.UserDims}--></td>
						</tr>
						</Area.UserDims>
					</tbody>
					<tfoot>
						<tr>
							<td colspan="2" align="center">
								<div class="grayBg">
									<input name="contentId" type="hidden" id="contentId" value="{&contentId}" />
									<input name="" type="button" value="预览" />
									<input name="" type="submit" value="发布" />
								</div>
							</td>
						</tr>
					</tfoot>
				</table>
			</div>
			<div id="infoRight">
				<table class="forms" cellpadding="0" cellspacing="0">
					<tbody>
						<tr>
							<td colspan="2">
							标题图片：
							<div class="addPic"><a href="#"><img src="../Images/addPic.png" alt="" /></a></div>
							</td>
						</tr>
						<tr>
							<td width="70px">作者：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td>来源：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td>转向连接：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td>日期：</td>
							<td><input type="text" name="" id="" onClick="showcalendar(event, this)"/></td>
						</tr>
						<tr>
							<td>允许评论：</td>
							<td>
								<input type="radio" name="isCommented" id="isCommented1" /><label for="isCommented1">允许</label> 
								<input type="radio" name="isCommented" id="isCommented2" /><label for="isCommented2">不允许</label>
							</td>
						</tr>
						<tr>
							<td valign="top">状态：</td>
							<td>
								<input id="state1" value="-1" type="radio" name="State"   {@State.CaoGao} /><label for="state1">草稿</label><br />
								<input id="state2" value="0" type="radio" name="State"   {@State.ShenHe} /><label for="state2">待审核</label><br />
								<input id="state3" name="State" type="radio" value="99"  {@State.ShenHeOK} /><label for="state3">终审通过</label>
							</td>
						</tr>
						<tr>
							<td valign="top">设置属性：</td>
							<td>
								<input type="checkbox" name="optionAttr" id="optionAttr1"/><label for="optionAttr1">推荐</label> 
								<input type="checkbox" name="optionAttr" id="optionAttr2"/><label for="optionAttr2">热点</label><br /> 
								<input type="checkbox" name="optionAttr" id="optionAttr3"/><label for="optionAttr3">醒目</label> 
								<input type="checkbox" name="optionAttr" id="optionAttr4"/><label for="optionAttr4">置顶</label>
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
Sio.Text();
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

</script>
</body>
</html>
