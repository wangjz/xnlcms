<XNL:if>
<ifItem value="getpinyin" test="{&action}">
<if>{[CHS2PinYin("{&sitename}",'',false)]}</if>
</ifItem>
<ifitem value="getcontenttag" test="{&action}">
<if>[{}<xnl:repeater siteid="{&siteid}" sqlcommand="select ct_name,ct_level from sn_contenttag where ct_siteid=@siteid"><itemtemplate>,{"n":"{$ct_name}","l":{$ct_level}}</itemtemplate></xnl:repeater>]</if>
</ifitem>
</XNL:if>