<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>{[site.name]}</title>
<meta name="keywords" content="{[site.matekeywords]}" />
<meta name="description" content="{[site.matedesc]} " />
<link href="{[site.path]}css/base.css" rel="stylesheet" type="text/css" />
<link href="{[site.path]}css/list.css" rel="stylesheet" type="text/css" />
</head>

<body>
<xnl.mytag:通用头部></xnl.mytag:通用头部>
<div id="articleList">
	<ul><xnl:repeater nodeid="{[channel.id]}" sqlcommand="select  id,title,Summary,adddate from sn_u_article where nodeid=53 order by id desc"><itemtemplate>
		<li>
			<div class="fl"><img src="{[site.path]}images/pic1.jpg" alt="" /></div>
			<div class="fr">
				<div class="title"><a href="{[site.path]}{[channel.indexname]}/help/{$id}.html">{$title}</a><span>{[toshortdate('{$adddate}')]}</span></div>
				<div class="txt">{$Summary}</div>
				<div class="enter"><a href="{[site.path]}{[channel.indexname]}/help/{$id}.html">查看</a></div>
			</div>
		</li>
        </itemtemplate>
        </xnl:repeater>
	</ul>
</div>
<xnl.mytag:版权></xnl.mytag:版权>
<script type="text/javascript" src="{[site.path]}js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="{[site.path]}js/nav.js"></script>
</body>
</html>