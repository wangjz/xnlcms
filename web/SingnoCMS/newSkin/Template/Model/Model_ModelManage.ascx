<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="x-ua-compatible" content="ie=7" />
<title></title>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<table class="tables" cellpadding="0" cellspacing="0">
  <thead>
    <tr class="head"></tr>
  </thead>
  <thead>
    <tr class="head">
      <td width="30%">字段名</td>
      <td width="30%">显示名称</td>
      <td width="15%">数据类型</td>
      <td width="10%">显示样式</td>
      <td width="10%">验证设置</td>
      <td width="5%">操作</td>
    </tr>
  </thead>
  <tbody id="ctrl_cmd">
    <XNL:repeater modelName="{&modelname}" sqlCommand="select a.DescriptId,a.FieldName,a.DataType,a.DisplayName,a.IsSystem,a.modelName,b.ModelStyle from SN_ModelDescript as a,sn_model as b  where a.modelname=b.modelname and  a.modelname=@modelname order by a.isSystem desc, a.indexid">
      <itemtemplate>
        <xnl:if>
          <ifItem test="1" value="{@repeaterItemId}">
            <if>
              <xnl:set ModelStyle="{$ModelStyle}"></xnl:set>
            </if>
          </ifItem>
        </xnl:if>
        <tr>
          <td><a href="javascript:showField('{$DescriptId}')" class="blue">{$FieldName}</a></td>
          <td>{$DisplayName}</td>
          <td>{$DataType}</td>
          <td><a href="javascript:addStyle('{$DescriptId}','{&modelname}')">设置样式</a></td>
          <td><a href="javascript:setValidator('{$DescriptId}','{&modelname}')">设置验证</a></td>
          <td><XNL:if>
            <attrs>
            <ifItem value="{[iif('{$ModelStyle}'='1','1','0')]}" test="1">
              <if>
                <xnl:if>
                  <ifItem value="0" test="{[iif('{$FieldName}'='Title' or '{$FieldName}'='AddDate','1','0')]}">
                    <if><a href="javascript:delTip('{$DescriptId}')">删除</a></if>
                  </ifItem>
                </xnl:if>
              </if>
            </ifItem>
          </XNL:if></td>
        </tr>
      </itemtemplate>
    </XNL:repeater>
  </tbody>
  <tfoot>
    <tr>
      <td colspan="6"><div class="grayBg">
        <xnl:if>
          <ifItem value="1" test="{!ModelStyle}">
            <if>
              <input id="addField" type="button" onclick="addField('{&modelname}')"  value="添加字段" />
            </if>
          </ifItem>
        </xnl:if>
      </div></td>
    </tr>
  </tfoot>
  <thead>
  </thead>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();
//添加字段
function addField(modelName)
{
	top.Sio.Alert({width:400,height:350,src:'Model/Model_AddField.aspx?modelname='+escape(modelName),title:'添加字段',button:false});
}
//查看字段
function showField(id){
	top.Sio.Alert({width:300,height:310,src:'Model/Model_ShowField.aspx?id='+id,title:'查看字段',button:false});
}
//添加字段样式
function addStyle(id,name){
	top.Sio.Alert({width:650,height:560,src:'Model/Model_AddStyle.aspx?id='+id+"&modelname="+escape(name),title:'设置字段样式',button:false});
}
function setValidator(id,name)
{
	top.Sio.Alert({width:500,height:350,src:'Model/Model_setValidator.aspx?id='+id+"&modelname="+escape(name),title:'设置字段验证',button:false});
}
//删除处理
function delTip(id){
	if(confirm('确实要删除吗?表中此字内容也会被删除且不成恢复！')) location.href('model_action.aspx?action=delfield&DescriptId=' + id);
}
//为ie6下的表格行添加鼠标经过效果
if(Sio.BS().IE6) Sio.hoverClass($i('ctrl_cmd').children,'hover');
</script>
</body>
</html> 
