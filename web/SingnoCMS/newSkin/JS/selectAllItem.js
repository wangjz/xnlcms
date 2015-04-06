

//批量操作控制
var _checked = Sio.get('#ctrl_cmd>tr>td>input[type=checkbox]');
$i('selectAll').onclick = function(){
	if(this.checked){
		Sio.each(_checked,function(i,e){
			e.checked = true;
		});
	}else{
		Sio.each(_checked,function(i,e){
			e.checked = false;
		});
	}
};

