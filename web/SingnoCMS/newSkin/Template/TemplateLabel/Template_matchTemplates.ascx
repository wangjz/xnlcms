<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JS/Sio.min.js"></script>
</head>
<body id="matchTemp">  
<xnl:if>
	<ifItem value="" test="{&projectid}">
		<if>
			<span class="fl"><xnl:repeater sqlcommand="select ProjectID ,ProjectName from SN_TemplateProject where siteID=@siteID">
					<attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
			<xnl:if><ifItem test="{@repeateritemcount}" value="1" operator="&lt;"><if><select name="projectID" id="projectID" onchange="if(this.value>0)location.href='?projectID='+this.value"><option value="0">请选择模板方案</option></if></ifItem>
                
					<itemTemplate> <ifItem test="{@repeateritemcount}" value="1"><if><script type="text/javascript">location.href="?projectID={$projectID}";</script></if></ifItem>
						<option value="{$projectID}">{$projectName}</option>
					</itemTemplate>
			<ifItem test="{@repeateritemcount}" value="1" operator="&lt;"><if></select></if></ifItem></xnl:if></xnl:repeater>
			</span>
			</if>
			<else>
			<script type="text/javascript">
			var match_arr=[];
			//matchObj={nodeID:"0",nodeName:"dfd",channelTemplateID:"1",channelTemplateName:"",contentTemplateID:"1",contentTemplateName:""}
			function addMatch(matchObj)
			{
				var testMatch=checkMatchArray(matchObj);
				if(testMatch.isMatch)
				{
					if(matchObj.templateStyle=="1")
					{
						testMatch.obj.channelTemplateID=matchObj.templateID;
						testMatch.obj.channelTemplateName=matchObj.templateName;
					}
					else if(matchObj.templateStyle=="2")
					{
						testMatch.obj.contentTemplateID=matchObj.templateID;
						testMatch.obj.contentTemplateName=matchObj.templateName;
					}
				}
				else
				{
					var obj=new Object();
					obj.nodeID=matchObj.nodeID;
					obj.nodeName=matchObj.nodeName;
					if(matchObj.templateStyle=="1")
					{
						obj.channelTemplateID=matchObj.templateID;
						obj.channelTemplateName=matchObj.templateName;
					}
					else if(matchObj.templateStyle=="2")
					{
						obj.contentTemplateID=matchObj.templateID;
						obj.contentTemplateName=matchObj.templateName;
					}
					match_arr.push(obj);
				}
			}
			
			function checkMatchArray(matchObj)
			{
				for(var i in match_arr)
				{
					var temObj=match_arr[i];
					if(temObj.nodeID==matchObj.nodeID)
					{
						return {isMatch:true,obj:temObj};
					}
				}
				return {isMatch:false};
			}
			
			function setTemplateMatch(templateStyle)
			{
				var nodeSel=$i("NodeList");
				 for(i=0;i<nodeSel.length;i++){
					if(nodeSel.options[i].selected){
						var txt=nodeSel.options[i].innerHTML.replace(/<script>.+<\/script>/gi,"");
						var id=nodeSel.options[i].value.toString();
						for(var j in match_arr)
						{
							var temObj=match_arr[j];
							if(id==temObj.nodeID)
							{
								if(templateStyle=="1")
								{
									nodeSel.options[i].innerHTML=txt.replace(txt.match(/(\(.+?\))[.\s]*(\(.+?\))/)[1],"("+temObj.channelTemplateName+")");
								}else if(templateStyle=="2"){
									nodeSel.options[i].innerHTML=txt.replace(txt.match(/(\(.+?\))[.\s]*(\(.+?\))/)[2],"("+temObj.contentTemplateName+")");
								}
								break;
							}
						}
					}		
				}
				//设置表单值
				var match_str="";
				for(var i=0;i<match_arr.length;i++)
				{
					var temp_str=match_arr[i].nodeID+","+match_arr[i].channelTemplateID+","+match_arr[i].contentTemplateID;
					match_str+=(i>0?"|":"")+temp_str;
				}
				$i("match").value=match_str;
			}
			
			function setChannelMatch()
			{
				var sel = $i("ChannelTemplateList");
				if(sel.selectedIndex==-1)
				{
					alert("请选择要匹配的栏目模板!")
					return false;
				}
				var nodeSel=$i("NodeList");
				if(nodeSel.selectedIndex==-1)
				{
					alert("请选择要参与匹配的节点!")
					return false;
				}
				var node_arr=[];
				for(i=0;i<nodeSel.length;i++){   
					if(nodeSel.options[i].selected){
						node_arr.push(nodeSel.options[i].value);
					}
				}
				var id = sel.options[sel.selectedIndex].value;
				var name = sel.options[sel.selectedIndex].text;
				for(var i=0;i<node_arr.length;i++)
				{
					var nodeId=node_arr[i].toString();
					addMatch({nodeID:nodeId,templateStyle:"1",templateID:id,templateName:name});
				}
				setTemplateMatch("1")
			}
			
			function setContentMatch()
			{
				var sel = $i("ContentTemplateList");
				if(sel.selectedIndex==-1)
				{
					alert("请选择要匹配的内容模板!")
					return false;
				}
				var nodeSel=$i("NodeList");
				if(nodeSel.selectedIndex==-1)
				{
					alert("请选择要参与匹配的节点!")
					return false;
				}
				var node_arr=[];
				for(i=0;i<nodeSel.length;i++){   
					if(nodeSel.options[i].selected){
						node_arr.push(nodeSel.options[i].value);
					}
				}
				var id = sel.options[sel.selectedIndex].value;
				var name = sel.options[sel.selectedIndex].text;
				for(var i=0;i<node_arr.length;i++)
				{
					var nodeId=node_arr[i].toString();
					addMatch({nodeID:nodeId,templateStyle:"2",templateID:id,templateName:name});
				}
				setTemplateMatch("2")
			}
			</script>
			<span class="fl">
			<select name="NodeList" size="25" multiple="multiple"  id="NodeList">
				<CMS.Manage:channels depthTag="&nbsp;&nbsp;&nbsp;&nbsp;"  sqlcommand="select Nodeid,nodename,depth,ChildsNum,arrChildID from SN_Nodes where RootID=@nodeid">
					<attrs>
						<attr name="nodeid" type="int">
							<CMS.manage:common action="getCurSiteId"></CMS.manage:common>
						</attr>
					</attrs>
					<channelsItem>
					<option value="{$nodeid}">{@depthTag}└ {$nodename}
						<xnl:repeater projectID="{&projectID}" sqlcommand="select b.templateID,b.templateStyle, b.templateName from SN_TemplateMatch as a,SN_Template as b where a.NodeID={$nodeid} and a.ProjectID=@projectid  and (ChannelTemplateID=b.templateID  or a.ContentTemplateID=b.templateID )">
							<itemTemplate>
								({$templateName})
								<script>addMatch({nodeID:"{$nodeid}",nodeName:"{$nodename}",templateStyle:"{$templateStyle}",templateID:"{$templateId}",templateName:"{$templateName}"});</script>
							</itemTemplate>
						</xnl:repeater>
					</option>
					</channelsItem>
				</CMS.Manage:channels>
			</select>
			</span>
			<span class="fl">
			<input type="button" name="matchChannelBtn" id="matchChannelBtn" value="匹配栏目模板" onclick="setChannelMatch()"/>
			</span>
			<span class="fl">
			<select name="ChannelTemplateList" size="25" id="ChannelTemplateList">
				<xnl:repeater projectID="{&projectID}" sqlcommand="select templateId,templateName from SN_Template where TemplateProjectID=@projectID and templateStyle=1">
					<itemTemplate>
						<option value="{$templateID}">{$templateName}</option>
					</itemTemplate>
				</xnl:repeater>
			</select>
			</span>
			<span class="fl">
			<input type="button" name="matchContentBtn" id="matchContentBtn" value="匹配内容模板" onclick="setContentMatch()"/>
			</span>
			<span class="fl">
			<select name="ContentTemplateList" size="25" id="ContentTemplateList">
				<xnl:repeater projectID="{&projectID}" sqlcommand="select templateId,templateName from SN_Template where TemplateProjectID=@projectID and templateStyle=2">
					<itemTemplate>
						<option value="{$templateID}">{$templateName}</option>
					</itemTemplate>
				</xnl:repeater>
			</select>
			</span>
			
			<table width="100%">
			<form action="Template_action.aspx?action=matchTemplate" id="form1" method="post">
				<tfoot>
					<tr>
						<td>
							<div class="grayBg">
								<input name="match" type="hidden" id="match"/>
								<input name="projectId" type="hidden" id="projectId" value="{&projectId}"/>
								<input type="submit" name="button" id="button" value="保存匹配" />
							</div>
						</td>
					</tr>
				</tfoot>
			</form>
			</table>
		</else>
	</ifItem>
</xnl:if>


<script type="text/javascript">
Sio.Button();

</script>
</body>
</html>
