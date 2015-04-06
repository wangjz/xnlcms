
//布局控制
var $classList = $i('classList'),
	$channelList = $i('channelList'),
	$manageInfo = $i('manageInfo'),
	$manageMain = $i('manageMain'),
	_doc = document.documentElement;
resize();
window.onresize = resize;
function resize(){
	$classList.style.height = _doc.clientHeight + 'px';
	$channelList.style.height = _doc.clientHeight + 'px';
	$manageInfo.style.width = ((_doc.clientWidth - 150 >= 0) ? _doc.clientWidth - 150 : 0) + 'px';
	$manageInfo.style.height = _doc.clientHeight + 'px';
	$manageMain.style.height = _doc.clientHeight + 'px';
}
