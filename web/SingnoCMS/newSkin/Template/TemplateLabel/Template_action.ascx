<xnl:if>
<!--#添加模板#-->
<ifItem value="{&action}" test="addTemplate">
<if>
<CMS.Manage:TemplateAction action="addTemplate"   extName="{%extName}" userExtName="{%userExtName}"  contentRequest="templateContent">
<attrs>
<attr name="templatename" type="string">{%templateName}</attr>
<attr name="templateStyle" type="int">{%templateStyle}</attr>
<attr name="createFileName" type="string">{%createFileName}</attr>
<attr name="charset" type="string">{%charset}</attr>
<attr name="projectId" type="int">{%projectid}</attr>
</attrs>
		<TemplateAction.success>
		添加模板成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction>
</if>
</ifItem>

<ifItem value="{&action}" test="delete">
<if>
<CMS.Manage:TemplateAction action="delete" templateId="{&templateId}">
		<TemplateAction.success>
		删除模板成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction>
</if>
</ifItem>

<ifItem value="{&action}" test="copy">
<if>
<CMS.Manage:TemplateAction action="copy" templateId="{&templateId}" templateName="{%templateName}">
		<TemplateAction.success>
		复制模板成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction>
</if>
</ifItem>

<ifItem value="{&action}" test="setdefault">
<if>
<CMS.Manage:TemplateAction action="setdefault" templateId="{&templateId}">
		<TemplateAction.success>
		已设为默认模板
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction>
</if>
</ifItem>


<ifitem value="{&action}" test="modifyTemplate">
<if>
<CMS.Manage:TemplateAction action="modifyTemplate" templatename="{%templateName}"   extName="{%extName}" userExtName="{%userExtName}" createFileName="{%createFileName}" charset="{%charset}" contentRequest="templateContent"  templateID="{%templateID}">
		<TemplateAction.success>
		成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction>
</if>
</ifitem>


<ifItem value="{&action}" test="addProject">
<if>
<CMS.Manage:TemplateAction action="addproject" projectName="{%projectName}" ChannelFilePathRule="{%ChannelFilePathRule}" ContentFilePathRule="{%ContentFilePathRule}">
		<TemplateAction.success>
		成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction></if>
</ifItem>

<ifItem value="{&action}" test="matchTemplate">
<if><CMS.Manage:TemplateAction action="matchTemplate" match="{%match}" projectId="{%projectId}">
		<TemplateAction.success>
		成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction></if>
</ifItem>

<ifItem value="{&action}" test="modifyRule">
<if><CMS.Manage:TemplateAction action="modifyRule" nodeid="{%nodeid}" projectId="{%projectId}" channelPathRule="{%channelPathRule}" contentPathRule="{%contentPathRule}">
		<TemplateAction.success>
		成功
		</TemplateAction.success>
		<TemplateAction.Error>
		有错误:
		<TemplateAction.Error.Item>
		<li>{@TemplateAction.Error.Msg}</li>
		</TemplateAction.Error.Item>
		</TemplateAction.Error>
</CMS.Manage:TemplateAction></if>
</ifItem>
</xnl:if>