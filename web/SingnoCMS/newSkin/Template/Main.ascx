<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>SCMS -- </title>
<link href="css/base.css" rel="stylesheet" type="text/css" />
<link href="css/main.css" rel="stylesheet" type="text/css" />
<link href="css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body scroll="no">
<div id="header">
	<div class="welcome">您好 <CMS.Manage:Common action="getcuradmin"></CMS.Manage:Common>！ [ 超级管理员 |  <a href="#">退出</a> ] <a href="#">官方支持</a> | <a href="#">授权</a> | <a href="#">网站地图</a> | <a href="#">返回面板</a> | <a href="#">帮助</a></div>
	<div class="tab">
		<div class="left"></div>
		<div id="tab" class="center">
			<ul>
				<li>内容</li>
				<li>功能</li>
				<li>用户</li>
				<li id="liw">模版标签</li>
				<li>配置</li>
				<li>其他</li>
			</ul>
		</div>
		<div class="right"></div>
	</div>
</div>
<div id="line"></div>
<div class="body">
	<div id="list">
		<div class="name">
			<div class="left"></div>
			<div class="center">站点名称</div>
			<div class="right"></div>
		</div>
		<div id="listCon" class="content">
			<ul>
				<li class="show">
					<ul class="adminItem">
						<li><a href="Model/Model_addModel.aspx" target="mainFrame">添加模型</a></li>
						<li><a href="Model/Model_manageModel.aspx" target="mainFrame">模型管理</a></li>
					</ul>
					<ul class="adminItem">
						<li><a href="Channel/Channel_addChannel.aspx" target="mainFrame">添加栏目</a></li>
						<li><a href="Channel/Channel_manageChannel.aspx" target="mainFrame">栏目管理</a></li>
						<li><a href="Channel/Channel_ChannelRecycle.aspx" target="mainFrame">栏目回收站</a></li>
					</ul>
					<ul>
						<li><a href="Channel/Channel_addChannelGroup.aspx" target="mainFrame">添加栏目组</a></li>
						<li><a href="Channel/Channel_manageChannelGroup.aspx" target="mainFrame">栏目组管理</a></li>
					</ul>
					<ul>
						<li><a href="Content/Content_AddContent.aspx?action=add" target="mainFrame">添加信息</a></li>
						<li><a href="Content/Content_manageinfo.aspx" target="mainFrame">信息管理</a></li>
						<li><a href="Content/Content_Recycle.aspx" target="mainFrame">信息回收站</a></li>
					</ul>
                    <ul>
						<li><a href="Content/Content_addContentGroup.aspx" target="mainFrame">添加内容组</a></li>
						<li><a href="Content/Content_manageContentGroup.aspx" target="mainFrame">内容组管理</a></li>
						<li><a href="Content/Content_manageTag.aspx" target="mainFrame">内容tag管理</a></li>
					</ul>
				</li>
				<li class="hide">
					<ul>
						<li><a href="Function/Function_createIndex.aspx" target="mainFrame">生成首页</a></li>
						<li><a href="Function/Function_createColumns.aspx" target="mainFrame">生成栏目页</a></li>
						<li><a href="Function/Function_createContent.aspx" target="mainFrame">生成内容页</a></li>
						<li><a href="Function/Function_createFiles.aspx" target="mainFrame">生成文件页</a></li>
					</ul>
				</li>
				<li class="hide">
					<ul>
						<li><a href="AdminUser/AdminUser_createRole.aspx" target="mainFrame">创建角色</a></li>
						<li><a href="AdminUser/AdminUser_manageRole.aspx" target="mainFrame">管理角色</a></li>
					</ul>
					<ul>
						<li><a href="AdminUser/AdminUser_addManager.aspx" target="mainFrame">添加管理员</a></li>
						<li><a href="AdminUser/AdminUser_manageManager.aspx" target="mainFrame">管理员管理</a></li>
					</ul>
					<ul>
						<li><a href="User/User_addUser.aspx" target="mainFrame">添加用户</a></li>
						<li><a href="User/User_manageUser.aspx" target="mainFrame">用户管理</a></li>
						<li><a href="User/User_manageUserGroups.aspx" target="mainFrame">用户组管理</a></li>
					</ul>
				</li>
				<li class="hide">
					<ul>
						<li><a href="TemplateLabel/Template_addTemplate.aspx" target="mainFrame">添加模版</a></li>
						<li><a href="TemplateLabel/Template_manageTemplate.aspx" target="mainFrame">模版管理</a></li>
					</ul>
					<ul>
						<li><a href="TemplateLabel/Template_addLabel.aspx" target="mainFrame">新建标签</a></li>
						<li><a href="TemplateLabel/Template_manageLabel.aspx" target="mainFrame">标签管理</a></li>
					</ul>
					<ul>
						<!--<li><a href="TemplateLabel/Template_addProject.aspx" target="mainFrame">新建模板方案</a></li>-->
						<li><a href="TemplateLabel/Template_matchTemplates.aspx" target="mainFrame">模板匹配</a></li>
						<li><a href="TemplateLabel/Template_manageRuleSet.aspx" target="mainFrame">生成地址管理</a></li>
					</ul>
				</li>
                <li class="hide">
					<ul>
						<li><a href="Config/Config_System.aspx" target="mainFrame">系统配置</a></li>
						<li><a href="Config/Config_Email.aspx" target="mainFrame">邮箱配置</a></li>
						<li><a href="Config/Config_License.aspx" target="mainFrame">系统许可配置</a></li>
						<li><a href="Config/Config_User.aspx" target="mainFrame">会员中心设置</a></li>
					</ul>
					<ul>
						<li><a href="Config/Config_Site.aspx" target="mainFrame">站点基本配置</a></li>
						<li><a href="Config/Config_Upload.aspx" target="mainFrame">站点上传配置</a></li>
						<li><a href="Config/Config_GB.aspx" target="mainFrame">站点留言配置</a></li>
						<li><a href="Config/Config_PicWater.aspx" target="mainFrame">图片水印配置</a></li>
						<li><a href="Config/Config_Create.aspx" target="mainFrame">站点生成配置</a></li>
					</ul>
					<ul>
						<li><a href="Config/Config_Content.aspx" target="mainFrame">内容信息配置</a></li>
						<li><a href="Config/Config_Comments.aspx" target="mainFrame">评论配置</a></li>
						<li><a href="Config/Config_CreateChannel.aspx" target="mainFrame">栏目生成配置</a></li>
						<li><a href="Config/Config_Received.aspx" target="mainFrame">投稿配置</a></li>
						<li><a href="Config/Config_cright.aspx" target="mainFrame">栏目权限配置</a></li>
					</ul>
				</li>
				<li class="hide">
					<ul>
						<li><a href="Other/Other_manageComments.aspx" target="mainFrame">评 论</a></li>
						<li><a href="Other/Other_manageGB.aspx" target="mainFrame">留言管理</a></li>
						<li><a href="Other/Other_manageLinks.aspx" target="mainFrame">连接管理</a></li>
						<li><a href="Other/Other_manageFiles.aspx" target="mainFrame">站点文件管理</a></li>
					</ul>
				</li>
			</ul>
		</div>
	</div>
	<div id="main">
		<div id="nav">当前位置：<span>系统</span> 〉<span>工作台</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><a href="javascript:history.go(-1);" title="返回">返回</a></span>&nbsp;&nbsp;&nbsp;&nbsp;<span><a href="javascript:history.go(1);" title="前进">前进</a></span></div>
		<iframe id="mainFrame" name="mainFrame" src="WorkIndex.html" frameborder="0" width="100%"></iframe>
	</div>
</div>
<script type="text/javascript" src="JS/Sio.min.js"></script>
<script type="text/javascript" src="JS/main.js"></script>
</body>
</html>
