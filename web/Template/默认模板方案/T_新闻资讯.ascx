<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>{[site.name]}:新闻资讯-{[site.matekeywords]}</title>
<meta name="keywords" content="{[site.matekeywords]}" />
<meta name="description" content="{[site.matedesc]} " />
<link href="css/base.css" rel="stylesheet" type="text/css" />
<link href="css/list.css" rel="stylesheet" type="text/css" />
</head>

<body>
<xnl.mytag:通用头部></xnl.mytag:通用头部>
<div id="articleList">
	<ul><xnl:pagecontents perpagerecordsnum="14"><pagecontents.item>
		<li>
			<div class="fl"><img src="images/pic1.jpg" alt="" /></div>
			<div class="fr">
				<div class="title"><a href="news/{@pagecontents.id}.html">{@pagecontents.title}</a><span>{[toshortdate('{@pagecontents.adddate}')]}</span></div>
				<div class="txt">{@pagecontents.Summary}</div>
				<div class="enter"><a href="news/{@pagecontents.id}.html">查看</a></div>
			</div>
		</li>
        </pagecontents.item>
        </xnl:pagecontents>
	</ul>
<xnl.mytag:分页></xnl.mytag:分页>
</div>
<xnl.mytag:版权></xnl.mytag:版权>
<script type="text/javascript" src="js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="js/nav.js"></script>
</body>
</html>