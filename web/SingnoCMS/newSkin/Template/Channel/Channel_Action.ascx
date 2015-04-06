<XNL:if>
<!--# 添加栏目#-->
<ifItem value="{&action}" test="add">
<if>
<CMS.Manage:Channel action="checkchannel" parentnodeid="{%classOf}" nodeName="{%channelName}">
<Channel.Success>
<!--# 添加栏目#-->
	<CMS.Manage:Channel action="add"  parentnodeid="{%classOf}" nodeName="{%channelName}" indexname="{%IndexName}" modelid="{%modelid}" nodeGroup="{%nodeGroup}" imageUrl="{%channelPic}" LinkUrl="{%LinkUrl}" info="{%info}" matekeyword="{%matekeyword}" metadesc="{%metadesc}"> 
	<Channel.Success>
	添加栏目成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</Channel.Success>
<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>

<ifItem value="{&action}" test="addgroup">
<if>
	<CMS.Manage:Channel action="addgroup" groupName="{%groupName}" Description="{%Description}">
    	<Channel.Success>
			添加栏目组成功
		</Channel.Success>
        <Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
    </CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="remove">
<if>
	<CMS.Manage:Channel action="remove" nodeid="{&nodeid}">
    	<Channel.Success>
			成功
		</Channel.Success>
        <Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
    </CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="delete">
<if>
	<CMS.Manage:Channel action="delete" nodeid="{&nodeid}">
    	<Channel.Success>
			删除成功
		</Channel.Success>
        <Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
    </CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="restore">
<if>
	<CMS.Manage:Channel action="restore" nodeid="{&nodeid}">
    	<Channel.Success>
			还原成功
		</Channel.Success>
        <Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
    </CMS.Manage:Channel>
</if>
</ifItem>

<ifItem value="{&action}" test="modify">
<if>
	<CMS.Manage:Channel action="modify" parentnodeid="{%parentnodeid}" nodeid="{%nodeid}" nodeName="{%channelName}" indexname="{%IndexName}" modelid="{%modelid}" nodeGroup="{%nodeGroup}" imageurl="{%channelPic}" LinkUrl="{%LinkUrl}" info="{%info}" matekeyword="{%matekeyword}" metadesc="{%metadesc}">
	<Channel.Success>
	栏目修改成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="up">
<if>
	<CMS.Manage:Channel action="up"  nodeid="{&nodeid}" sortnum="{%sortnum}">
	<Channel.Success>
	上移成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>

<ifItem value="{&action}" test="down">
<if>
	<CMS.Manage:Channel action="down"  nodeid="{&nodeid}" sortnum="{%sortnum}">
	<Channel.Success>
	下移成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="deletegroup">
<if>
	<CMS.Manage:Channel action="deletegroup"  groupid="{&groupid}">
	<Channel.Success>
	删除成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>
<ifItem value="{&action}" test="deletegroupnode">
<if>
	<CMS.Manage:Channel action="deletegroupnode"  groupid="{&groupid}" nodeid="{&nodeid}">
	<Channel.Success>
	删除成功
	</Channel.Success>
	<Channel.Error>
	有错误:
        <Channel.Error.Item>
        <li>{@Channel.Error.Msg}</li>
        </Channel.Error.Item>
	</Channel.Error>
</CMS.Manage:Channel>
</if>
</ifItem>
</XNL:if>
