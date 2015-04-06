
//缓存Dom
var $tabLi = $i('tab').children[0].children,
	$list = $i('list'),
	$main = $i('main'),
	$mainFrame = $i('mainFrame'),
	$conList = Sio.get('#list>div.content>ul>li'),
	$conLi = Sio.get('#list>div.content>ul>li>ul>li'),
	$navP = $i('nav').children[0],
	$navS = $i('nav').children[1];

//如果为IE6则添加鼠标经过效果
if(Sio.BS().IE6){
	Sio.hoverClass($tabLi,'hover');
}

//初始化布局
window.onload = resize;
//页面变化时即时调整布局
window.onresize = resize;
function resize(){
	var _winW = document.documentElement.clientWidth,
		_winH = document.documentElement.clientHeight;
	$list.style.height = (_winH - 71) + 'px';
	$main.style.width = (_winW - 142) + 'px';
	$main.style.height = (_winH - 72) + 'px';
	$mainFrame.style.width = (_winW - 143) + 'px';
	$mainFrame.style.height = (_winH - 102) + 'px';
	var mask = $i('alertMask');
	if(mask){
		mask.style.width = _winW + 'px';
		mask.style.height = _winH + 'px';
	}
}

//操作选项卡
Sio.Tabs({tab:'tab',con:'listCon'});

//操作栏目列表项
Sio.each($conList,function(i,e){
	e.setAttribute('_index',i);
});
Sio.addEvent($conLi,'click',function(e){
	Sio.removeClass($conLi,'curr');
	e.className = 'curr';
});

//关闭对话框
function closeAlert(){
	Sio.remove('#alertMask,#XAlert');
}




