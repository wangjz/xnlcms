
var startCode = 0;
var endCode = 0;

//插入代码
function insertCode(obj,val){       
	var pre = obj.value.substr(0,startCode);
	var post = obj.value.substr(endCode);
	obj.value = pre + val + post;
}
function getTextPos(obj, charvalue){
	obj.focus();
	var r = document.selection.createRange(),
		ctr = obj.createTextRange(),
		i,
		s = obj.value,
		ivalue = "1"; 
	r.text = ivalue;
	i = obj.value.indexOf(ivalue);
	r.moveStart("character", -ivalue.length);
	r.text = "";
	obj.value = s.substr(0,i) + charvalue + s.substr(i,s.length);
	ctr.collapse(true);
	ctr.moveStart("character", i + charvalue.length);
	ctr.select();
}

//获取焦点位置
function getAreaPos(textBox){
	if(typeof(textBox.selectionStart) == "number"){
		startCode = textBox.selectionStart;
		endCode = textBox.selectionEnd;
	}else if(document.selection){
		var range = document.selection.createRange();
		if(range.parentElement().id == textBox.id){
			var range_all = document.body.createTextRange();
			range_all.moveToElementText(textBox);
			for (startCode=0; range_all.compareEndPoints("StartToStart", range) < 0; startCode++){
				range_all.moveStart('character', 1);
			}
			for (var i = 0; i <= startCode; i ++){
				if (textBox.value.charAt(i) == '\n') startCode++;
			}
			var range_all = document.body.createTextRange();
			range_all.moveToElementText(textBox);
			for (endCode = 0; range_all.compareEndPoints('StartToEnd', range) < 0; endCode ++){
				range_all.moveStart('character', 1);
				for (var i = 0; i <= endCode; i ++){
					if (textBox.value.charAt(i) == '\n') endCode ++;
				}
			}
		}
	}
}