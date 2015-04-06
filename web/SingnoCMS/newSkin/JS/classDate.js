
/*
	数据格式说明（节点支持无限分级）：
	data    :  指定树形结构根节点数组，在实例化对象时需指定
	text    :  节点显示文本
	urlData :  节点缓存数据，用于回调函数调用
	nodes   :  节点的子节点集合
*/


var classDate = [
	{text:'站点名称',urlData:'$test',nodes:[
		{text:'栏目1',urlData:'$t01',nodes:[
			{text:'子栏目1',urlData:'$test01'},
			{text:'子栏目2',urlData:'$test02'},
			{text:'子栏目3',urlData:'$test03'},
			{text:'子栏目4',urlData:'$test04'}
		]},
		{text:'栏目2',urlData:'$t02',nodes:[
			{text:'子栏目1',urlData:'$test201'},
			{text:'子栏目2',urlData:'$test202'}
		]},
		{text:'栏目3',urlData:'$t03'},
		{text:'栏目4',urlData:'$t04'},
		{text:'栏目5',urlData:'$t05'}
	]}
];
















