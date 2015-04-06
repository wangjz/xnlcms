<XNL:if>
<ifItem value="Y" test="{[iif('{&action}'='add' or '{&action}'='modify','Y','N')]}">
<if>
    <XNL:validatorTest>
        <validatorTest.Success>
            <XNL:if>
                <ifItem value="{&action}" test="add">
                    <if>
                        <CMS.Manage:Content action="add" nodeid="{%nodeid}" state="{%State}">
                            <Content.Success>
                            执行成功
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                
                <ifItem value="{&action}" test="modify">
                    <if>
                        <CMS.Manage:Content action="modify" contentId="{%contentId}" nodeid="{%nodeid}" state="{%State}">
                            <Content.Success>
                            修改成功
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem> 
            </XNL:if>
        </validatorTest.Success>
        <validatorTest.Error>
            <validatorTest.Error.item>
            {@validatorTest.Error.Msg}
            </validatorTest.Error.item>
        </validatorTest.Error>
    </XNL:validatorTest>
</if>
<else>
<XNL:if>
                <ifItem value="{&action}" test="recycle">
                    <if>
                        <CMS.Manage:Content action="recycle" contentId="{&contentId}" nodeId="{&nodeid}">
                            <Content.Success>
                            已删除到回收站
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                 <ifItem value="{&action}" test="restore">
                    <if>
                        <CMS.Manage:Content action="restore" contentId="{&contentId}" nodeId="{&nodeid}">
                            <Content.Success>
                            已还原
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                 <ifItem value="{&action}" test="delete">
                    <if>
                        <CMS.Manage:Content action="delete" contentId="{&contentId}" nodeId="{&nodeid}">
                            <Content.Success>
                            已删除
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                 <ifItem value="{&action}" test="audit">
                    <if>
                        <CMS.Manage:Content action="audit" contentId="{&contentId}" nodeId="{&nodeid}">
                            <Content.Success>
                            已审核
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                <ifItem value="{&action}" test="attribute">
                    <if>
                        <CMS.Manage:Content action="attribute" contentId="{%contentId}" nodeId="{%nodeid}" state="{%State}">
                            <Content.Success>
                          属性已修改
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifItem>
                <ifitem value="{&action}" test="addgroup">
                	 <if>
                        <CMS.Manage:Content action="addgroup" GroupName="{%GroupName}" Description="{%Description}">
                            <Content.Success>
                            内容组添加完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifitem>
                 <ifitem value="{&action}" test="deletegroup">
                	 <if>
                        <CMS.Manage:Content action="deletegroup" groupid="{&groupid}">
                            <Content.Success>
                            内容组删除完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifitem>
                 <ifitem value="{&action}" test="deletegroupinfo">
                	 <if>
                        <CMS.Manage:Content action="deletegroupinfo" groupid="{&groupid}" nodeid="{&nodeid}" infoid="{&infoid}">
                            <Content.Success>
                            内容组内容删除完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content>
                    </if>
                </ifitem>
                <ifitem value="{&action}" test="addtag">
                <if> <CMS.Manage:Content action="addtag" tagname="{%tagname}">
                            <Content.Success>
                            tag添加完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content></if>
                </ifitem>
                <ifitem value="{&action}" test="deletetag">
                <if> <CMS.Manage:Content action="deletetag" tagid="{&tagid}">
                            <Content.Success>
                            tag删除完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content></if>
                </ifitem>
                 <ifitem value="{&action}" test="deletetaginfo">
                <if> <CMS.Manage:Content action="deletetaginfo" tagid="{&tagid}" nodeid="{&nodeid}" infoid="{&infoid}">
                            <Content.Success>
                            删除完成
                            </Content.Success>
                            <Content.Error>
                                <Content.Error.Item>
                                	{@Content.Error.Msg}
                                </Content.Error.Item>
                            </Content.Error>
                        </CMS.manage:Content></if>
                </ifitem>
            </XNL:if>    
</else>
</ifItem>
</XNL:if>