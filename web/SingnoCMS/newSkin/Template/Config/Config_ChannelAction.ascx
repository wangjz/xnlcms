<xnl:if>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='base','True','False')]}" value="True">
<if>
<CMS.Manage:ChannelConfig action="modify" type="base" channelid="{&channelid}" autoSaveImage="{%saveOutPic}" partpagetype="{%pageType}" wordnum="{%wordNums}" prepageitem="{%itemNums}"  contentgroup="{%isContentGroup}" contenttag="{%contenttag}" countcontentclick="{%isCountClick}" countdownload="{%isCountDown}" hitsofhot="{%hitsOfHot}" opentype="{%opentype}" itemopentype="{%itemOpenType}" canaddchannel="{%isAddChannel}" canaddcontent="{%isAddContent}"  info="{%info}" metakeyword="{%keyword}" metadesc="{%desc}">
{"state":<ChannelConfig.Success>"Y","msg":"修改完成"</ChannelConfig.Success><ChannelConfig.Error>"N","msg":<ChannelConfig.Error.Item>"{@ChannelConfig.Error.Msg}"</ChannelConfig.Error.Item></ChannelConfig.Error>}
</CMS.Manage:ChannelConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='comments','True','False')]}" value="True">
<if>
<CMS.Manage:ChannelConfig action="modify" type="comments" channelid="{&channelid}" enabled="{%contentCommented}" needlogin="{%needLogin}" needaudit="{%isAudit}" successmsg="{%successmsg}" failmsg="{%failmsg}">{"state":<ChannelConfig.Success>"Y","msg":"修改完成"</ChannelConfig.Success><ChannelConfig.Error>"N","msg":<ChannelConfig.Error.Item>"{@ChannelConfig.Error.Msg}"</ChannelConfig.Error.Item></ChannelConfig.Error>}</CMS.Manage:ChannelConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='create','True','False')]}" value="True">
<if>
<CMS.Manage:ChannelConfig action="modify" type="create" channelid="{&channelid}" contentchange="{%contentChange}" channelchange="{%channelChange}">{"state":<ChannelConfig.Success>"Y","msg":"修改完成"</ChannelConfig.Success><ChannelConfig.Error>"N","msg":<ChannelConfig.Error.Item>"{@ChannelConfig.Error.Msg}"</ChannelConfig.Error.Item></ChannelConfig.Error>}</CMS.Manage:ChannelConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='contribute','True','False')]}" value="True">
<if>
<CMS.Manage:ChannelConfig action="modify" type="contribute" channelid="{&channelid}" enabled="{%isReceived}" needaudit="{%isAudit}">{"state":<ChannelConfig.Success>"Y","msg":"修改完成"</ChannelConfig.Success><ChannelConfig.Error>"N","msg":<ChannelConfig.Error.Item>"{@ChannelConfig.Error.Msg}"</ChannelConfig.Error.Item></ChannelConfig.Error>}</CMS.Manage:ChannelConfig>
</if>
</ifItem>
</xnl:if>