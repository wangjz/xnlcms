<CMS.Manage:AdminLoginCheck><AdminLoginCheck.noItem><script>alert("没登陆");top.location.href="../default.html";</script></AdminLoginCheck.noItem></CMS.Manage:AdminLoginCheck>
<xnl:if>
<ifItem value="addAdmin" test="{&action}">
<if>
<CMS.Manage:AdminUser action="addAdmin" LoginName="{%LoginName}" DisplayName="{%DisplayName}" Password="{%Password}" ConfirmPass="{%ConfirmPass}" Email="{%Email}" Question="{%Question}" Answer="{%Answer}" RoleId="{%RoleId}">
	<AdminUser.Success>
    管理员添加成功
    </AdminUser.Success>
    <AdminUser.Error>
    	<AdminUser.Error.Item>
         {@AdminUser.Error.Msg}
        </AdminUser.Error.Item>
    </AdminUser.Error>
</CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="createRole" test="{&action}">
<if>
<CMS.Manage:AdminUser action="createRole" roleName="{%roleName}" desc="{%desc}" systemRights="{%systemRights}" siteRights="{%siteRights}" otherRights="{%otherRights}" pulginsRight="{%pulginsRights}">
	<AdminUser.Success>
    角色创建成功
    </AdminUser.Success>
    <AdminUser.Error>
    	<AdminUser.Error.Item>
         {@AdminUser.Error.Msg}
        </AdminUser.Error.Item>
    </AdminUser.Error>
</CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="modifyAdmin" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="modifyAdmin" LoginName="{%LoginName}" DisplayName="{%DisplayName}" Password="{%Password}" ConfirmPass="{%ConfirmPass}" Email="{%Email}" Question="{%Question}" Answer="{%Answer}" RoleId="{%RoleId}">
            <AdminUser.Success>
           管理员信息修改成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="deleteAdmin" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="deleteAdmin" LoginName="{&LoginName}">
            <AdminUser.Success>
           管理员删除成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="lockAdmin" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="lockAdmin" LoginName="{&LoginName}">
            <AdminUser.Success>
           管理员锁定成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="unlockAdmin" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="unlockAdmin" LoginName="{&LoginName}">
            <AdminUser.Success>
           管理员解除锁定成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="modifyAdminPass" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="modifyAdminPass" LoginName="{%LoginName}" Password="{%Password}" ConfirmPass="{%ConfirmPass}">
            <AdminUser.Success>
           重设密码成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>
<ifItem value="modifyAdminRole" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="modifyAdminRole" LoginName="{%LoginName}" roleId="{%roleId}">
            <AdminUser.Success>
          修改权限成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="deleteRole" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="deleteRole" roleId="{&roleid}">
            <AdminUser.Success>
           角色删除成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

<ifItem value="modifyRole" test="{&action}">
	<if>
        <CMS.Manage:AdminUser action="modifyRole" roleId="{%roleid}" desc="{%desc}" systemRights="{%systemRights}" siteRights="{%siteRights}" otherRights="{%otherRights}" pulginsRight="{%pulginsRights}">
            <AdminUser.Success>
           角色修改成功
            </AdminUser.Success>
            <AdminUser.Error>
                <AdminUser.Error.Item>
                 {@AdminUser.Error.Msg}
                </AdminUser.Error.Item>
            </AdminUser.Error>
        </CMS.Manage:AdminUser>
</if>
</ifItem>

</xnl:if>