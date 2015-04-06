<XNL:if>
<ifItem value="{&action}" test="init">
<if><CMS.Manage:init nodeid="{&nodeid}"></CMS.Manage:init>
<script type="text/javascript">
	location.href="main.aspx"
</script></if>
<else><XNL:repeater sqlcommand="select a.nodeid,a.nodename from sn_nodes as a, sn_sites as b where a.nodeid=b.nodeid">
<XNL:if>
<ifItem value="0" test="{@repeateritemcount}">
<if>系统正在初始化……
<script type="text/javascript">
	location.href="option/M_Option_createSite2.aspx"
</script></if>
<else>
<itemTemplate>
<ul>
<li><a href="init.aspx?nodeID={$nodeid}&action=init">{$nodename}</a> </li>
</ul>
</itemTemplate>
</else>
</ifItem>
</XNL:if>
</XNL:repeater></else>
</ifItem>
</XNL:if>