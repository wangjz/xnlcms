

//打开高级搜索菜单
$i('keywords').onfocus = function(){
	this.select();
}
$i('moreSearch').onclick = function(){
	if(this.className == 'more'){
		this.className = 'more_curr';
		$i('searchMenu').style.display = 'block';
	}else{
		this.className = 'more';
		$i('searchMenu').style.display = 'none';
	}
}
