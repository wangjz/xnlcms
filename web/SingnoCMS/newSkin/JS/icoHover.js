
if(Sio.BS.IE6){
	//图标效果
	var _ico = Sio.get('#navigator>div,#ctrl_cmd>tr>td.icons>div');
	Sio.addEvent(_ico,'mouseover',function(e){
		if(e.className != 'line') e.className = e.className + '_hover';
	});
	Sio.addEvent(_ico,'mouseout',function(e){
		e.className = e.className.replace('_hover','');
	});
}




