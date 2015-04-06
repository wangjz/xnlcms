<xnl:if>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='base','True','False')]}" value="True">
<if>
<CMS.Manage:SiteConfig action="modify" type="base" siteName="{%siteName}" path="{%path}" title="{%title}" ico="{%ico}" info="{%info}" keyword="{%keyword}" desc="{%desc}" Charset="{%Charset}" audit="{%audit}" checkLevel="{%checkLevel}">{"state":<SiteConfig.Success>"Y","msg":"修改完成"</SiteConfig.Success><SiteConfig.Error>"N","msg":<SiteConfig.Error.Item>"{@SiteConfig.Error.Msg}"</SiteConfig.Error.Item></SiteConfig.Error>}</CMS.Manage:SiteConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='upload','True','False')]}" value="True">
<if>
<CMS.Manage:SiteConfig action="modify" type="upload" folder="{%folder}" SaveType="{%SaveType}" ReNameByTime="{%ReNameByTime}" imageType="{%imageType}" imageMaxSize="{%imageMaxSize}" mediaType="{%mediaType}" mediaMaxSize="{%mediaMaxSize}" docsType="{%docsType}" docMaxSize="{%docMaxSize}">{"state":<SiteConfig.Success>"Y","msg":"修改完成"</SiteConfig.Success><SiteConfig.Error>"N","msg":<SiteConfig.Error.Item>"{@SiteConfig.Error.Msg}"</SiteConfig.Error.Item></SiteConfig.Error>}
</CMS.Manage:SiteConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='guestbook','True','False')]}" value="True">
<if>
<CMS.Manage:SiteConfig action="modify" type="guestbook" isEnabled="{%isEnabled}" isAudit="{%isAudit}" isLogin="{%isLogin}" isCode="{%isCode}">{"state":<SiteConfig.Success>"Y","msg":"修改完成"</SiteConfig.Success><SiteConfig.Error>"N","msg":<SiteConfig.Error.Item>"{@SiteConfig.Error.Msg}"</SiteConfig.Error.Item></SiteConfig.Error>}</CMS.Manage:SiteConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='watermark','True','False')]}" value="True">
<if>
<CMS.Manage:SiteConfig action="modify" type="watermark" enabled="{%isWatermark}" position="{%pos}" alpha="{%alpha}" imageminwidth="{%imgW}" imageminheight="{%imgH}" watermarktype="{%textType}" imagepath="{%imgPath}" content="{%content}"
font="{%font}" fontsize="{%fontsize}">{"state":<SiteConfig.Success>"Y","msg":"修改完成"</SiteConfig.Success><SiteConfig.Error>"N","msg":<SiteConfig.Error.Item>"{@SiteConfig.Error.Msg}"</SiteConfig.Error.Item></SiteConfig.Error>}</CMS.Manage:SiteConfig>
</if>
</ifItem>
<ifItem test="{[iif('modify'='{&action}' and '{&type}'='create','True','False')]}" value="True">
<if>
<CMS.Manage:SiteConfig action="modify" type="create" siteError="{%siteError}" writeSQL="{%writeSQL}" LabelError="{%LabelError}" PreCompiled="{%PreCompiled}">{"state":<SiteConfig.Success>"Y","msg":"修改完成"</SiteConfig.Success><SiteConfig.Error>"N","msg":<SiteConfig.Error.Item>"{@SiteConfig.Error.Msg}"</SiteConfig.Error.Item></SiteConfig.Error>}</CMS.Manage:SiteConfig>
</if>
</ifItem>
</xnl:if>