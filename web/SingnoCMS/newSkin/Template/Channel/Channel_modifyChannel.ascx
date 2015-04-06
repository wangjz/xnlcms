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
<form action="Channel_Action.aspx?action=modify" method="post" >
<xnl:repeater nodeid="{&nodeid}" sqlcommand="select nodeid,nodename,indexname,parentid,modelid,imageurl,LinkUrl from sn_nodes where nodeid=@nodeid"><itemtemplate>
<div id="classContent">
	<ul>
		<li>
			<table class="forms" cellpadding="0" cellspacing="0">
				<tbody>
					<tr>
						<td width="120px">所属栏目：</td>
						<td>
                            <xnl:repeater  sqlcommand="select nodename  from sn_nodes  where nodeid={$parentid}"><input name="parentnodeid" type="hidden" id="parentnodeid" value="{$parentid}" /><itemtemplate>{$$nodename}</itemtemplate><emptyDataTemplate>站点根栏目<input name="parentnodeid" type="hidden" id="parentnodeid" value="0" /></emptyDataTemplate></xnl:repeater>
						</td>
					</tr>
					<tr>
						<td>栏目名称：</td>
						<td><input name="channelName" type="text" id="channelName" value="{$nodename}" size="30"/><span class="required">*</span></td>
					</tr>
					<tr>
						<td>索引名称：</td>
						<td><input name="IndexName" type="text" id="IndexName" value="{$indexname}" size="30"/></td>
					</tr>
					<tr>
						<td>栏目模型：</td>
						<td>
							<XNL:repeater sqlCommand="select ModelID,ModelName from SN_Model where modelid={$modelid}"><itemtemplate>{$ModelName}</itemtemplate></XNL:repeater>
						</td>
					</tr>
					<tr>
						<td>栏目组：</td>
						<td>
                        <xnl:repeater sqlcommand="select a.ng_id,a.ng_name from sn_nodegroup a,sn_nodegroups b where b.ngs_nodeid={$nodeid} and a.ng_id=b.ngs_id" >
                        <itemtemplate><input name="nodeGroup" type="checkbox" id="nodeGroup{$ng_id}" value="{$ng_id}" checked="checked" >
									<label for="nodeGroup{$ng_id}">{$ng_name}</label><xnl:if><ifItem value="{@@repeaterItemId}" test="{@@repeateritemcount}"><if><XNL:repeater><attrs><attr name="sqlcommand">select ng_id as groupid2,ng_name as groupname2 from sn_nodegroup where ng_id not in (select ngs_id from sn_nodegroups where ngs_nodeid={$nodeid}) and ngs_siteid={!siteid}</attr></attrs><itemTemplate><input name="nodeGroup" type="checkbox" id="nodeGroup{$groupid2}" value="{$groupid2}" ><label for="nodeGroup{$groupid2}">{$groupname2}</label></itemTemplate></XNL:repeater></if></ifItem></xnl:if></itemtemplate><emptyDataTemplate><XNL:repeater sqlcommand="select ng_id as gid3,ng_name as gname3 from sn_nodegroup  where ng_siteid={!siteid}"><itemTemplate>
				<input name="nodeGroup" type="checkbox" id="nodeGroup{$gid3}" value="{$gid3}" >
									<label for="nodeGroup{$gid3}">{$gname3}</label>
								</itemTemplate> 
							</XNL:repeater></emptyDataTemplate></xnl:repeater>
						</td>
					</tr>
				</tbody>
			</table>
		</li>
		<li>
        <CMS.Manage:ChannelConfig action="view" type="base" channelid="{&nodeid}">
          <table class="forms" cellpadding="0" cellspacing="0">
          <tbody>
            <tr></tr>
          </tbody>
          <tbody>
            <tr>
              <td width="120px">栏目关键字：</td>
              <td><input name="matekeyword" type="text" class="input" id="matekeyword" value="{@metakeyword}" size="30"/></td>
            </tr>
            <tr>
              <td>栏目图片：</td>
              <td><span class="fl"><a href="#"><img id="modelIco" src="../Images/icons/ico_01.gif" alt=""/></a></span>
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
                <input name="channelPic" type="hidden" id="modelIcoName" value="{$imageurl}"/></td>
            </tr>
            <tr>
              <td>栏目介绍：</td>
              <td><textarea name="info" cols="60" rows="5" id="info">{@info}</textarea></td>
            </tr>
            <tr>
              <td>网页描述：</td>
              <td><textarea name="metadesc" cols="60" rows="5" id="metadesc">{@metadesc}</textarea></td>
            </tr>
          </tbody>
          <tbody>
          </tbody>
          </table>
        </CMS.Manage:ChannelConfig>
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
						<td><input type="text" class="unit" value="" size="6"/>&nbsp;%
					    <input name="nodeid" type="hidden" id="nodeid" value="{&nodeid}" /></td>
					</tr>
                   
				</tbody>
			</table>
		</li>
	</ul>
</div>
</itemtemplate>
</xnl:repeater>
<div class="grayBg"><input name="提交" type="submit" value="确认修改"/></div>
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
