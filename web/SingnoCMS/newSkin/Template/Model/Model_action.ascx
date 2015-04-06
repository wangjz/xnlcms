<XNL:if>
<!--#
添加模型管理标签
#-->
<ifItem value="{%action}" test="add">
<if><XNL:validatorTest responseend="True"><validatorTest.Success></validatorTest.Success><validatorTest.Error><script type="text/javascript">alert("执行出错!<validatorTest.Error.Item>\n{@validatorTest.Error.Msg}</validatorTest.Error.Item>");history.back();</script></validatorTest.Error></XNL:validatorTest>
<CMS.Manage:Model action="add" modelName="{%modelName}" tableName="{%modelTableName}" itemName="{%itemName}" itemUnit="{%itemUnit}" modelIcon="{%modelIcon}" description="{%modelNote}"><script type="text/javascript">
<Model.Success>if(confirm("添加成功!\n是否继续添加?")){location.href("Model_addModel.aspx");}else{location.href("Model_manageModel.aspx");}</Model.Success><Model.Error>alert("执行出错!<Model.Error.Item>\n{@Model.Error.Msg}</Model.Error.Item>");history.back();</Model.Error></script></CMS.Manage:Model>
<!--#
添加模型管理标签结束
#-->
</if>
</ifItem>

<ifItem value="{%action}" test="modify"><if><CMS.Manage:Model  action="modify" modelid="{%modelid}" type="modify" srcModelName="{%srcModelName}" modelName="{%modelName}"  itemName="{%itemName}" itemUnit="{%itemUnit}" modelIcon="{%modelIcon}" description="{%modelNote}">
<Model.Success>成功</Model.Success><Model.Error>有错误:<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>
<!--#
模型删除管理标签
#-->
<ifItem value="{&action}" test="delete"><if><CMS.Manage:Model action="delete" modelId="{&modelId}"><Model.Success><script>alert("模型已成功删除");location.href("Model_manageModel.aspx");</script></Model.Success><Model.Error>
有错误:<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>

<!--#
模型添加字段管理标签
#-->
<ifItem  value="{&action}" test="addfield"><if><CMS.Manage:Model action="addField" modelName="{%modelName}" fieldName="{%fieldName}" DataType="{%DataStyle}" dataLength="{%dataLength}"  DefaultValue="{%DefaultValue}"  DecimalLen="{%DecimalLen}"><Model.Success>字段添加成功</Model.Success><Model.Error>有错误:<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>

<!--#
模型删除字段管理标签
#-->
<ifItem  value="{&action}" test="delfield"><if><CMS.Manage:Model action="delfield"  DescriptId="{&DescriptId}"><Model.Success>字段删除成功</Model.Success><Model.Error>字段删除失败<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>

<!--#
模型字段设置样式标签
#-->
<ifItem value="{&action}" test="setStyle"><if><CMS.Manage:Model action="setStyle" id="{&id}" DisplayName="{%DisplayName}"  HelpText="{%helpText}" isVisible="{%isvisible}"  IsShowOnList="{%IsShowOnList}" inputType="{%formTemp}"><Model.Success>修改完成</Model.Success><Model.Error>有错误:<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>

<!--#
模型字段设置验证标签
#-->
<ifItem value="{&action}" test="setValidator"><if><CMS.Manage:Model action="setValidator" id="{&id}" IsValidator="{%IsValidator}" required="{%required}"  regexp="{%regexp}" error="{%error}" regexpType="{%regexpType}" userRegexp="{%userRegexp}"><Model.Success>修改完成</Model.Success><Model.Error>有错误:<Model.Error.Item><li>{@Model.Error.Msg}</li></Model.Error.Item></Model.Error></CMS.Manage:Model></if></ifItem>

<ifItem value="{&action}" test="checkModelTable"><if><CMS.Manage:Model action="checkModelTable" tableName="{&modelTableName}" type="{&type}" modelId="{&modelid}"></CMS.Manage:Model></if></ifItem>

<ifItem value="{&action}" test="checkModel"><if><CMS.Manage:Model action="checkModel" modelName="{&modelName}" type="{&type}" modelId="{&modelid}"></CMS.Manage:Model></if></ifItem>
</XNL:if>