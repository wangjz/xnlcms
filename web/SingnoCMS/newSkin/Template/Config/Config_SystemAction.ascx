<xnl:if>
<ifItem test="addWhiteIp" value="{&action}"><if><CMS.Manage:MSystemConfig action="addWhiteIp" ip="{%ip}">{"state":<MSystemConfig.Success>"Y","msg":"添加完成"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
<ifItem test="addBlackIp" value="{&action}"><if><CMS.Manage:MSystemConfig action="addBlackIp" ip="{%ip}">{"state":<MSystemConfig.Success>"Y","msg":"添加完成"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
<ifItem test="modifySystemBase" value="{&action}"><if><CMS.Manage:MSystemConfig action="modifySystemBase"  projectName="{%projectName}" AccessType="{%AccessType}">{"state":<MSystemConfig.Success>"Y","msg":"修改完成"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if></ifItem>
<ifItem test="delblack" value="{&action}"><if><CMS.Manage:MSystemConfig action="delblack" ip="{%ip}">{"state":<MSystemConfig.Success>"Y","msg":"删除成功"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
<ifItem test="updateblack" value="{&action}"><if><CMS.Manage:MSystemConfig action="updateblack" ip="{%ip}" newip="{%newip}">{"state":<MSystemConfig.Success>"Y","msg":"更新成功"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
<ifItem test="delwhite" value="{&action}"><if><CMS.Manage:MSystemConfig action="delwhite" ip="{%ip}">{"state":<MSystemConfig.Success>"Y","msg":"删除成功"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
<ifItem test="updatewhite" value="{&action}"><if><CMS.Manage:MSystemConfig action="updatewhite" ip="{%ip}" newip="{%newip}">{"state":<MSystemConfig.Success>"Y","msg":"更新成功"</MSystemConfig.Success><MSystemConfig.Error>"N","msg":<MSystemConfig.Error.Item>"{@MSystemConfig.Error.Msg}"</MSystemConfig.Error.Item></MSystemConfig.Error>}</CMS.Manage:MSystemConfig></if>
</ifItem>
</xnl:if>