<xnl:if>
<!--#添加模板#-->
<ifItem value="{&action}" test="add">
<if>
<CMS.Manage:UserTag action="add"  tagName="{%tagName}" tagDesc="{%tagDesc}">
		<UserTag.success>
		添加标签成功
		</UserTag.success>
		<UserTag.Error>
		有错误:
		<UserTag.Error.Item>
		<li>{@UserTag.Error.Msg}</li>
		</UserTag.Error.Item>
		</UserTag.Error>
</CMS.Manage:UserTag>
</if>
</ifItem>

<ifItem value="{&action}" test="delete">
<if>
<CMS.Manage:UserTag action="delete" tagId="{&tagId}">
		<UserTag.success>
		删除标签成功
		</UserTag.success>
		<UserTag.Error>
		有错误:
		<UserTag.Error.Item>
		<li>{@UserTag.Error.Msg}</li>
		</UserTag.Error.Item>
		</UserTag.Error>
</CMS.Manage:UserTag>
</if>
</ifItem>

<ifItem value="{&action}" test="copy">
<if>
<CMS.Manage:UserTag action="copy" tagId="{&tagId}" tagName="{%tagName}">
		<UserTag.success>
		复制标签成功
		</UserTag.success>
		<UserTag.Error>
		有错误:
		<UserTag.Error.Item>
		<li>{@UserTag.Error.Msg}</li>
		</UserTag.Error.Item>
		</UserTag.Error>
</CMS.Manage:UserTag>
</if>
</ifItem>

<ifitem value="{&action}" test="modify">
<if>
<CMS.Manage:UserTag action="modify" tagname="{%tagName}"   tagid="{%tagid}" tagDesc="{%tagDesc}">
		<UserTag.success>
		成功
		</UserTag.success>
		<UserTag.Error>
		有错误:
		<UserTag.Error.Item>
		<li>{@UserTag.Error.Msg}</li>
		</UserTag.Error.Item>
		</UserTag.Error>
</CMS.Manage:UserTag>
</if>
</ifitem>
</xnl:if>