/*
	数据格式说明（节点支持无限分级）：
	data    :  指定树形结构根节点数组，在实例化对象时需指定
	text    :  节点显示文本
	urlData :  节点缓存数据，用于回调函数调用
	nodes   :  节点的子节点集合
*/
function getNodeById(id,_arr)
{
	for(var i=0;i<_arr.length;i++)
    {
    	if(id==_arr[i].urlData)
        {
            return _arr[i];
        }
    }
    return null;
}
function setNodeArray(node_array,nodeObjArray)
{
	for(var i=0;i<node_array.length;i++)
    {
    	var node=node_array[i];
    	var nodeObj;
        if(node.childNum>0||node_array.length==1)
        {
       		 nodeObj={text:node.name,urlData:node.id,nodes:[]};
        }
        else
        {
        	nodeObj={text:node.name,urlData:node.id};
        }
    	nodeObjArray.push(nodeObj);
    }
}
var node_array=[];
<XNL:set><attrs><attr name="nodeid" type="int"><CMS.manage:common action="getCurSiteId"></CMS.manage:common></attr></attrs></XNL:set><CMS.Manage:channels depthTag="" nodeid="{!nodeid}" sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID,ParentID from SN_Nodes where RootID=@nodeid"><channelsItem>
node_array.push({id:{$nodeid},name:"{$nodename}",childNum:{$ChildsNum},parentid:{$ParentID}});</channelsItem></CMS.Manage:channels>
var classDate = [];
var nodeObj_array=[];
setNodeArray(node_array,nodeObj_array);
for(var i=0;i<node_array.length;i++)
{
	var node=node_array[i];
    var nodeObj;
    nodeObj=nodeObj_array[i];
    if(node.parentid==0||node_array.length==1)
    {
        classDate.push(nodeObj);
    }
    else
    {
    	//得到父级节点
        var parentObj=getNodeById(node_array[i].parentid,nodeObj_array);
        parentObj.nodes.push(nodeObj);
    }
}