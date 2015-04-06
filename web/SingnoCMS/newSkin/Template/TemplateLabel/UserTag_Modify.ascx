<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/TemplateLabel.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="userTag_action.aspx?action=modify" method="post" id="form1" name="form1">
<table class="forms">
	<tbody>
    <xnl:repeater tagId="{&tagid}" sqlCommand="select tagName,tagDesc,TagFilePath,SiteId from sn_userTag where tagId=@tagId"><itemtemplate>
		<tr>
			<td width="120px">标签名称：</td>
			<td><input name="tagName" type="text" id="tagName" value="{$tagName}" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">标签描述：</td>
			<td><textarea name="tagDesc" id="tagDesc" cols="80" rows="5">{$tagDesc}</textarea></td>
		</tr>
		<tr>
			<td valign="top">标签编辑：</td>
			<td><textarea name="tagContent" id="tagContent" cols="80" rows="15"><CMS.Manage:userTag action="loadcontent"  siteid="{$SiteID}" filepath="{$TagFilePath}"></CMS.Manage:userTag></textarea>
		    <input name="tagId" type="hidden" id="tagId" value="{&tagId}" /></td>
		</tr></itemtemplate>
        </xnl:repeater>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="创建标签" /></div></td>
		</tr>
	</tfoot>
</table>
</form>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

</script>
</body>
</html>