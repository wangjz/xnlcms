<div id="top">
	<div class="left"></div>
	<div class="center">
		<div id="moveCon">
			<span class="fl"><a href="{[site.path]}index.html"><img src="{[site.path]}images/logo.gif" alt="仙诺科技" border="0"/></a></span>
			<div id="search" class="fr">
				<input id="searchInput" type="text" class="fl" />
				<div class="fr"><img id="searchCmd" src="{[site.path]}images/close.gif" alt="" /></div>
			</div>
			<div id="nav" class="fr">
				<ul><xnl:repeater groupname="顶部导航" sqlcommand="select a.NodeName,a.IndexName from sn_nodes a,sn_nodegroup b,SN_NodeGroups c where b.ng_Name=@groupname and b.ng_ID=c.ngs_ID and a.NodeID=c.ngs_nodeId"><itemTemplate>
					<li><a href="{[site.path]}{$IndexName}.html">{$NodeName}</a></li></itemTemplate>
                    </xnl:repeater>
				</ul>
			</div>
		</div>
	</div>
	<div class="right"></div>
</div>