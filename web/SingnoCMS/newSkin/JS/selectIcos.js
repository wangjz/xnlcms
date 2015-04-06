

//选择图标
var icoCmd = $i('modelIco'),
	icoSel = $i('icoSelect'),
	icoSeLi = $it('icoSelect','li'),
	modelIcoName = $i('modelIcoName');
icoCmd.onclick = function(){
	if(icoSel.className == 'hide'){
		icoSel.className = 'show';
		icoSel.parentNode.children[2].className = 'hide';
	}else{
		icoSel.className = 'hide';
		icoSel.parentNode.children[2].className = 'exg';
	}
	return false;
};
icoSel.onclick = function(evt){
	var e = Sio.getTarget(evt);
	if(e.nodeName.toLowerCase() == 'img'){
		var _a = e.src.split('.'),
			_b = _a[0].split('/');
		modelIcoName.value = _b[_b.length-1] +'.'+ _a[1];
		icoSel.className = 'hide';
		icoCmd.src = e.src;
		icoSel.parentNode.children[2].className = 'exg';
	}
	return false;
};





