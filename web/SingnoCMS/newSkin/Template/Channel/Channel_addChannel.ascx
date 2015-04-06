<XNL:set>
 <attrs><attr name="siteid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs>
</XNL:set>
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
body{ margin:10px}
</style>
</head>
<body>
<div id="classTab">
	<ul>
		<li>基本信息</li>
		<li>栏目选项</li>
		<li>权限选项</li>
	</ul>
</div>
<form action="Channel_Action.aspx?action=add" method="post" >
<div id="classContent">
	<ul>
		<li>
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="120px">所属栏目：</td>
						<td>
							<select id="classOf" name="classOf">
							<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;" nodeid="{!siteid}" sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid"><channelsItem><xnl:if><ifitem test="{&nodeid}" value="{$nodeid}"><if><option value="{$nodeId}" selected="selected"> {@depthTag}└ {$nodename}</option></if><else><option value="{$nodeId}"> {@depthTag}└ {$nodename}</option></else></ifitem></xnl:if></channelsItem>
							</CMS.Manage:channels>
							</select>
							<span class="required">*</span>
						</td>
					</tr>
					<tr>
						<td>栏目名称：</td>
						<td><input name="channelName" type="text" id="channelName" value="" size="30"/><span class="required">*</span></td>
					</tr>
					<tr>
						<td>索引名称：</td>
						<td><input name="IndexName" type="text" id="IndexName" value="" size="30"/></td>
					</tr>
					<tr>
						<td>栏目类型：</td>
						<td>
							<XNL:repeater sqlCommand="select ModelID,ModelName from SN_Model"  >
								<select id="modelId" name="modelId">
									<itemtemplate>
										<option value="{$ModelID}">{$ModelName}</option>
									</itemtemplate>
								</select>
								<emptyDataTemplate>还没有建立模型</emptyDataTemplate>
							</XNL:repeater>
							<span class="required">*</span>
						</td>
					</tr>
					<tr>
						<td>栏目组：</td>
						<td>
							<XNL:repeater siteId="{!siteid}" sqlcommand="select ng_id,ng_name from sn_nodegroup where ng_siteid=@siteid">
								<itemTemplate>
									<input name="nodeGroup" type="checkbox" id="nodeGroup{$ng_id}" value="{$ng_id}">
									<label for="nodeGroup{$ng_id}">{$ng_name}</label>
								</itemTemplate> 
							</XNL:repeater>
						</td>
					</tr>
				</tbody>
			</table>
		</li>
		<li>
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="120px">栏目关键字：</td>
						<td><input name="matekeyword" type="text" class="input" id="matekeyword" value="" size="30"/></td>
					</tr>
					<tr>
						<td>栏目图片：</td>
						<td>
							<span class="fl"><a href="#"><img id="modelIco" src="../Images/icons/ico_01.gif" alt=""/></a></span>
							<div id="icoSelect" class="hide">
								<ul>
									<li><a href="#"><img src="../Images/icons/ico_01.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_02.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_03.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_04.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_05.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_06.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_07.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_08.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_09.gif" alt=""/></a></li>
									<li><a href="#"><img src="../Images/icons/ico_10.gif" alt=""/></a></li>
								</ul>
							</div>
							<span class="exg" style="line-height:38px">点击图片更换图标</span>
							<input name="channelPic" type="hidden" id="modelIcoName" />
						</td>
					</tr>
					<tr>
						<td>栏目介绍：</td>
						<td><textarea name="info" cols="60" rows="5" id="info"></textarea></td>
					</tr>
					<tr>
						<td>网页描述：</td>
						<td><textarea name="metadesc" cols="60" rows="5" id="metadesc"></textarea></td>
					</tr>
				</tbody>
			</table>
		</li>
		<li>
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="120px">浏览选项：</td>
						<td>
							<input type="checkbox" name="explorer" id="explorer0" value=""/><label for="explorer0">栏目管理员</label> 
							<input type="checkbox" name="explorer" id="explorer1" value=""/><label for="explorer1">高级会员</label> 
							<input type="checkbox" name="explorer" id="explorer2" value=""/><label for="explorer2">普通会员</label> 
							<input type="checkbox" name="explorer" id="explorer3" value=""/><label for="explorer3">游客</label> 
						</td>
					</tr>
					<tr>
						<td width="120px">投稿选项：</td>
						<td>
							<input type="checkbox" name="received0" id="received0" value=""/><label for="received0">栏目管理员</label> 
							<input type="checkbox" name="received0" id="received1" value=""/><label for="received1">高级会员</label> 
							<input type="checkbox" name="received0" id="received2" value=""/><label for="received2">普通会员</label> 
							<input type="checkbox" name="received0" id="received3" value=""/><label for="received3">游客</label> 
						</td>
					</tr>
					<tr>
						<td>阅读所需点数：</td>
						<td><input type="text" class="unit" size="6"/>&nbsp;点</td>
					</tr>
					<tr>
						<td>分成比率：</td>
						<td><input type="text" class="unit" value="" size="6"/>&nbsp;%</td>
					</tr>
				</tbody>
			</table>
		</li>
	</ul>
</div>

<div class="grayBg"><input name="提交" type="submit" value="添加栏目"/></div>
</form>


<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript" src="../JS/selectIcos.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();
//选项卡
Sio.Tabs({tab:'classTab',con:'classContent'});

//如果IE6，添加选项卡鼠标经过样式
if(Sio.BS().IE6) Sio.hoverClass($i('classTab').children[0].children,'hover'); 
</script>
</body>
</html> 
