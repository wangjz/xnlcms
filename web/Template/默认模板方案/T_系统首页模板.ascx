<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>仙诺科技</title>
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
			<div class="title">新闻资讯</div>
			<ul>
				<li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li>
			</ul>
		</div>
	</div>
	<div class="list listLeft">
		<div class="top"></div>
		<div class="li">
			<div class="title">技术文档</div>
			<ul>
				<li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li><li><a href="#">测试</a></li>
			</ul>
		</div>
	</div>
	<div class="list fr">
		<div class="top"></div>
		<div id="case" class="li">
			<div class="title">案例展示</div>
			<div class="case case_curr"><span>自由闲</span><img src="images/pic.jpg" alt="" /></div>
			<div class="case"><span>中部来华英语</span><img src="images/pic.jpg" alt="" /></div>
			<div class="case"><span>毅方伟杰商贸有限公司</span><img src="images/pic.jpg" alt="" /></div>
			<div class="case"><span>基层一线观察网</span><img src="images/pic.jpg" alt="" /></div>
			<div class="case"><span>中国乐途培训</span><img src="images/pic.jpg" alt="" /></div>
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
				<p>人机交互图形化用户界面设计。纵观国际相关产业在图形化用户界面设计方面的发展现状，许多国际知名公司早己意识到UI在产品方面产生的强大增值功能，以及带动的巨大市场价值，随着中国IT产业的迅猛发展...</p>
				<a href="#">详细信息</a>
			</div>
		</div>
		<div class="item">
			<div class="ico"><img src="images/ico_02.png" alt="" /></div>
			<div class="txt">
				<p>人机交互图形化用户界面设计。纵观国际相关产业在图形化用户界面设计方面的发展现状，许多国际知名公司早己意识到UI在产品方面产生的强大增值功能，以及带动的巨大市场价值，随着中国IT产业的迅猛发展...</p>
				<a href="#">详细信息</a>
			</div>
		</div>
		<div class="item">
			<div class="ico"><img src="images/ico_03.png" alt="" /></div>
			<div class="txt">
				<p>人机交互图形化用户界面设计。纵观国际相关产业在图形化用户界面设计方面的发展现状，许多国际知名公司早己意识到UI在产品方面产生的强大增值功能，以及带动的巨大市场价值，随着中国IT产业的迅猛发展...</p>
				<a href="#">详细信息</a>
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
<xnl.mytag:版权></xnl.mytag:版权>
<script type="text/javascript" src="js/Sio.base_11.02.min.js"></script>
<script type="text/javascript" src="js/nav.js"></script>
<script type="text/javascript">
var caseLi = Sio.get('#case div.case');
Sio.addEvent(caseLi,'mouseover',function(e){
	Sio.removeClass(caseLi,'case_curr');
	e.className = 'case case_curr';
});
</script>
</body>
</html>