<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>{[site.name]}</title>
<meta name="keywords" content="{[site.matekeywords]}" />
<meta name="description" content="{[site.matedesc]} " />
<link href="css/base.css" rel="stylesheet" type="text/css" />
<link href="css/index.css" rel="stylesheet" type="text/css" />
</head>

<body>
<xnl.mytag:通用头部></xnl.mytag:通用头部>
<div id="head">
	<div class="pic"><img src="images/banner.jpg" alt="" /></div>
	<ul>
		<li><span></span></li><li><span></span></li><li><span></span></li><li><span></span></li><li><span></span></li><li><span></span></li>
	</ul>
</div>
<div id="news">
	<div class="list fl">
		<div class="top"></div>
		<div class="li">
			<div class="title"><a href="{[site.path]}news.html">新闻资讯</a></div>
			<ul>
<xnl:repeater sqlcommand="select top 10  id,title from sn_u_article where nodeid=33 order by id desc">
				<itemtemplate><li><a href="{[site.path]}news/{$id}.html">{$title}</a></li></itemtemplate>
        </xnl:repeater>
			</ul>
		</div>
	</div>
	<div class="list listLeft">
		<div class="top"></div>
		<div class="li">
			<div class="title"><a href="{[site.path]}support/documents.html">技术文档</a></div>
			<ul>
<xnl:repeater sqlcommand="select top 10  id,title from sn_u_article where nodeid=52 order by id desc">
				<itemtemplate><li><a href="{[site.path]}support/documents/{$id}.html">{$title}</a></li></itemtemplate>
        </xnl:repeater>
			</ul>
		</div>
	</div>
	<div class="list fr">
		<div class="top"></div>
		<div id="case" class="li">
			<div class="title">案例展示</div><xnl:repeater sqlcommand="select ArrChildID from sn_nodes where nodeid=41"><itemtemplate>
<xnl:repeater sqlcommand="select top 5  id,title,ImageUrl from sn_u_article where nodeid in ({$ArrChildID}) order by id desc"><itemtemplate>
			<div class="case"><span>{$title}</span><img src="{$ImageUrl}" width="270" height="110" alt="" /></div>
</itemtemplate></xnl:repeater></itemtemplate></xnl:repeater>
		</div>
	</div>
</div>
<div id="service">
	<div class="top">
		<ul>
			<li>界面设计</li>
			<li>动画设计</li>
			<li>RIA/AIR开发</li>
			<li>网站建设</li>
		</ul>
	</div>
	<div class="list">
		<div class="item">
			<div class="ico"><img src="images/ico_01.png" alt="" /></div>
			<div class="txt">
				<p>人机交互图形化用户界面设计。纵观国际相关产业在图形化用户界面设计方面的发展现状，许多国际知名公司早己意识到UI在产品方面产生的强大增值功能，以及带动的巨大市场价值，随着中国IT产业的迅猛发展.....</p>
				<a href="{[site.path]}service/4.html">详细信息</a>
			</div>
		</div>
		<div class="item">
			<div class="ico"><img src="images/ico_02.png" alt="" /></div>
			<div class="txt">
				<p>Flash技术的实现对业界来讲是一次飞跃，现已涵盖至各行各业，它借鉴了大量的非线性编辑的设计手段，大胆的运用大量的电影因素，不但动感十足，音效震撼，还具备强大的交互性，在表达力上大大超越了传统的表现媒体 ...</p>
				<a href="{[site.path]}service/7.html">详细信息</a>
			</div>
		</div>
		<div class="item">
			<div class="ico"><img src="images/ico_03.png" alt="" /></div>
			<div class="txt">
				<p>RIA 具有的桌面应用程序的特点包括：在消息确认和格式编排方面提供互动用户界面；在无刷新页面之下提供快捷的界面响应时间；提供通用的用户界面特性如拖放式（drag and drop）以及在线和离线操作能力…</p>
				<a href="{[site.path]}service/6.html">详细信息</a>
			</div>
		</div>
		<div class="webs">
			<ul>
				<li>
					<div class="pic"><img src="images/icon_01.gif" alt="" /></div>
					<div class="txt"><a href="#">企业标准型</a><br /><span>Standard Site</span></div>
				</li>
				<li>
					<div class="pic"><img src="images/icon_02.gif" alt="" /></div>
					<div class="txt"><a href="#">企业增强型</a><br /><span>Enhanced Site</span></div>
				</li>
				<li>
					<div class="pic"><img src="images/icon_03.gif" alt="" /></div>
					<div class="txt"><a href="#">企业智能型</a><br /><span>Smart Site</span></div>
				</li>
				<li>
					<div class="pic"><img src="images/icon_04.gif" alt="" /></div>
					<div class="txt"><a href="#">多功能品牌塑造型</a><br /><span>Multifunction Site</span></div>
				</li>
				<li>
					<div class="pic"><img src="images/icon_05.gif" alt="" /></div>
					<div class="txt"><a href="#">门户型</a><br /><span>Portal Site</span></div>
				</li>
			</ul>
		</div>
	</div>
	<div class="bottom"></div>
</div>
<xnl.mytag:版权带统计></xnl.mytag:版权带统计>
<script type="text/javascript" src="js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="js/nav.js"></script>
<script type="text/javascript">
var caseLi = Sio.get('#case div.case');
caseLi[0].className = 'case case_curr';
Sio.addEvent(caseLi,'mouseover',function(e){
	Sio.removeClass(caseLi,'case_curr');
	e.className = 'case case_curr';
});
</script>
</body>
</html>