<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body{ padding:0 0 0 10px}
</style>
</head>
<body>
<form action="Config_ChannelAction.aspx?action=modify&type=base&channelid={&nodeid}" id="form1" name="form1" method="post">
<CMS.Manage:ChannelConfig action="view" type="base" channelid="{&nodeid}">
<table class="forms">
	<tbody>
		<tr>
			<td width="170">自动保存外部图片：</td>
			<td>
			<input name="saveOutPic" type="radio" id="saveOutPic1" value="True" {[iif('{@autosaveimage}'='True','checked="checked"','')]}  /><label for="saveOutPic1">保存</label>
			<input name="saveOutPic" type="radio" id="saveOutPic2" value="False" {[iif('{@autosaveimage}'='False','checked="checked"','')]}/><label for="saveOutPic2">不保存</label>
			</td>
		</tr>
		<tr>
			<td>分页设置：</td>
			<td>
				<input name="pageType" type="radio" id="pageType1" value="No" {[iif('{@partpagetype}'='No','checked="checked"','')]} /><label for="pageType1">不分</label>
				<input name="pageType" type="radio" id="pageType2" value="Auto"  {[iif('{@partpagetype}'='Auto','checked="checked"','')]}/><label for="pageType2">自动</label>
				<input name="pageType" type="radio" id="pageType3" value="Manual" {[iif('{@partpagetype}'='Manual','checked="checked"','')]}/><label for="pageType3">手动</label>
			</td>
		</tr>
		<tr>
			<td>自动分页每页字数：</td>
			<td><input type="text" name="wordNums" id="wordNums"  value="{@wordnum}" style="width:80px"/></td>
		</tr>
		<tr>
			<td>显示内容条数：</td>
			<td><input name="itemNums" type="text" id="itemNums" style="width:80px" value="{@prepageitem}"/></td>
		</tr>
		<tr>
			<td>是否启用内容组：</td>
			<td>
				<input name="isContentGroup" type="radio" id="isContentGroup1" value="True" {[iif('{@contentgroup}'='True','checked="checked"','')]}/><label for="isContentGroup1">启用</label> 
				<input name="isContentGroup" type="radio" id="isContentGroup2" value="False" {[iif('{@contentgroup}'='False','checked="checked"','')]}/><label for="isContentGroup2">不启用</label>
			</td>
		</tr>
        <tr>
			<td>是否启用内容tag：</td>
			<td>
				<input name="isContentTag" type="radio" id="isContentTag1" value="True" {[iif('{@contenttag}'='True','checked="checked"','')]}/><label for="isContentTag1">启用</label> 
				<input name="isContentTag" type="radio" id="isContentTag2" value="False" {[iif('{@contenttag}'='False','checked="checked"','')]}/><label for="isContentTag2">不启用</label>
			</td>
		</tr>
		<tr>
			<td>是否统计点击数：</td>
			<td>
			  <input name="isCountClick" type="radio" id="isCountClick1" value="True" {[iif('{@countcontentclick}'='True','checked="checked"','')]}/><label for="isCountClick1">统计</label> 
				<input name="isCountClick" type="radio" id="isCountClick2" value="False" {[iif('{@countcontentclick}'='False','checked="checked"','')]} /><label for="isCountClick2">不统计</label>
			</td>
		</tr>
		<tr>
			<td>是否统计文件下载：</td>
			<td>
			  <input name="isCountDown" type="radio" id="isCountDown1" value="True" {[iif('{@countdownload}'='True','checked="checked"','')]}/><label for="isCountDown1">统计</label> 
				<input name="isCountDown" type="radio" id="isCountDown2" value="False" {[iif('{@countdownload}'='False','checked="checked"','')]}/><label for="isCountDown2">不统计</label>
			</td>
		</tr>
		<tr>
			<td>栏目热点的点击数最小值：</td>
			<td><input name="hitsOfHot" type="text" id="hitsOfHot" style="width:80px" value="{@hitsofhot}" /></td>
		</tr>
		<tr>
			<td>链接打开方式：</td>
			<td>
				<select name="openType" id="openType">
				  <option value="_self"  {[iif('{@opentype}'='_self','selected="selected"','')]}>_self</option>
				  <option value="_blank" {[iif('{@opentype}'='_blank','selected="selected"','')]}>_blank</option>
				  <option value="_parent" {[iif('{@opentype}'='_parent','selected="selected"','')]}>_parent</option>
				  <option value="_top" {[iif('{@opentype}'='_top','selected="selected"','')]}>_top</option>
                </select></td>
		</tr>
        <tr>
			<td>内容链接打开方式：</td>
			<td><select name="itemOpenType" id="itemOpenType">
			  <option value="_self" {[iif('{@itemopentype}'='_self','selected="selected"','')]}>_self</option>
			  <option value="_blank" {[iif('{@itemopentype}'='_blank','selected="selected"','')]}>_blank</option>
			  <option value="_parent" {[iif('{@itemopentype}'='_parent','selected="selected"','')]}>_parent</option>
			  <option value="_top" {[iif('{@itemopentype}'='_top','selected="selected"','')]}>_top</option>
		    </select></td>
		</tr>
		<tr>
			<td>允许添加栏目：</td>
			<td>
		    <input name="isAddChannel" type="radio" id="isAddChannel1" value="True" {[iif('{@canaddchannel}'='True','checked="checked"','')]} /><label for="isAddChannel1">开启</label> 
				<input name="isAddChannel" type="radio" id="isAddChannel2" value="False" {[iif('{@canaddchannel}'='False','checked="checked"','')]}/><label for="isAddChannel2">关闭</label>
			</td>
		</tr>
		<tr>
			<td>允许添加内容：</td>
			<td>
		    <input name="isAddContent" type="radio" id="isAddContent1" value="True" {[iif('{@canaddcontent}'='True','checked="checked"','')]} /><label for="isAddContent1">开启</label> 
				<input name="isAddContent" type="radio" id="isAddContent2" value="False" {[iif('{@canaddcontent}'='False','checked="checked"','')]}/><label for="isAddContent2">关闭</label>
			</td>
		</tr>
         <tr>
			<td valign="top">栏目介绍：</td>
			<td><textarea name="info" id="info" cols="80" rows="4">{@info}</textarea></td>
		</tr>
		<tr>
			<td valign="top">针对搜索引擎的关键字：</td>
			<td><textarea name="keyword" id="keyword" cols="80" rows="4">{@metakeyword}</textarea></td>
		</tr>
        <tr>
			<td valign="top">针对搜索引擎的说明：</td>
			<td><textarea name="desc" id="desc" cols="80" rows="4">{@metadesc}</textarea></td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</CMS.Manage:ChannelConfig>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html>