<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>{[site.name]}</title>
<meta name="keywords" content="{[site.matekeywords]}" />
<meta name="description" content="{[site.matedesc]} " />
<link href="/css/base.css" rel="stylesheet" type="text/css" />
<link href="/css/list.css" rel="stylesheet" type="text/css" />
</head>

<body>
<xnl.mytag:通用头部></xnl.mytag:通用头部>
<div id="classMenu">
	<a href="{[site.path]}case/case_web.html" class="curr">网站建设</a>|<a href="{[site.path]}case/case_ui.html">界面设计</a>|<a href="{[site.path]}case/case_flash.html">flash设计</a>|<a href="{[site.path]}case/case_game.html">flash游戏</a>|<a href="{[site.path]}case/case_ria.html">RIA/AIR开发</a>
</div>
<div id="case">
	<ul><xnl:repeater sqlcommand="select title,id,Summary,ImageUrl,adddate from sn_u_article where nodeid={[channel.id]}" >
		<itemtemplate><li>
			<a href="{[site.path]}case/{[channel.indexname]}/{$id}.html"><img src="{$ImageUrl}" alt="" /><span>{$title}</span></a><br />
			{$summary}			<div>{[toshortdate('{$adddate}')]}<a href="{[site.path]}case/{[channel.indexname]}/{$id}.html">查看</a></div>
		</li></itemtemplate></xnl:repeater>
			</ul>
</div>


<xnl.mytag:版权></xnl.mytag:版权>
<script type="text/javascript" src="/js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="/js/nav.js"></script>
</body>
</html>
