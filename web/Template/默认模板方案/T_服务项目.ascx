<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>{[site.name]}</title>
<meta name="keywords" content="{[site.matekeywords]}" />
<meta name="description" content="{[site.matedesc]} " />
<link href="css/base.css" rel="stylesheet" type="text/css" />
<link href="css/list.css" rel="stylesheet" type="text/css" />
</head>

<body>
<xnl.mytag:通用头部></xnl.mytag:通用头部>
<div id="service">
	<ul>
<xnl:repeater nodeid="{[channel.id]}" sqlcommand="select id,title,Summary,ImageUrl,adddate from sn_u_article where nodeid=@nodeid"><itemtemplate>
		<li><a href="{[channel.indexname]}/{$id}.html"><img src="{$ImageUrl}" alt="{$title}" /><span>{$title}</span></a><br />{$Summary}</li>
        </itemtemplate>
        </xnl:repeater>
	</ul>
</div>


<xnl.mytag:版权></xnl.mytag:版权><script type="text/javascript" src="js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="js/nav.js"></script>
</body>
</html>