<XNL:if>
<ifItem value="create" test="{&action}">
<if>
    <CMS.Manage:CreatePage action="create" pagestyle="{&pageStyle}" nodeid="{&nodeid}" debug="{&debug}" projectid="{&projectid}" nodeList="{%nodelist}" pageList="{%pageList}" type="{%type}" startDate="{%startdate}" endDate="{%endDate}" startId="{%startId}" endId="{%endId}" contendId="{%contentId}">
    	<CreatePage.Success>
        {@CreatePage.Progress}
        </CreatePage.Success>
        <CreatePage.Error><CreatePage.Error.Item>{@CreatePage.Error.Msg}</CreatePage.Error.Item></CreatePage.Error>
    </CMS.Manage:CreatePage>
</if>
</ifItem>
<ifItem value="cancle" test="{&action}">
<if><CMS.Manage:CreatePage action="cancle" pagestyle="{&pageStyle}">
    	<CreatePage.Success>
        {@CreatePage.Progress}
        </CreatePage.Success>
        <CreatePage.Error><CreatePage.Error.Item>{@CreatePage.Error.Msg}</CreatePage.Error.Item></CreatePage.Error>
    </CMS.Manage:CreatePage>
</if>
</ifItem>
</XNL:if>

