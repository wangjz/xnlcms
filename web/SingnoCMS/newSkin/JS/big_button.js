
//获取所有按钮
var _cmd = Sio.get('input[type=button]');

//添加按钮事件
Sio.addEvent(_cmd,'mouseover',function(e){
	e.className = 'Button_hover';
});
Sio.addEvent(_cmd,'mouseout',function(e){
	e.className = 'Button';
});
Sio.addEvent(_cmd,'mousedown',function(e){
	e.className = 'Button_down';
});
Sio.addEvent(_cmd,'mouseup',function(e){
	e.className = 'Button';
});


