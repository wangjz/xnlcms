<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
<style type="text/css">
body{ padding:5px}
</style>
</head>
<body>  
<xnl:repeater sqlcommand="select ProjectID ,ProjectName from SN_TemplateProject where siteID=@siteID"><attrs><attr name="siteid" type="int"><CMS.manage:common action="getsiteidbysn"></CMS.manage:common></attr></attrs>
<xnl:if><ifItem test="{@repeateritemcount}" value="1" operator="&lt;">
<if><select name="projectID" id="projectID" onchange="if(this.value>0)location.href='?projectID='+this.value"><option value="0">请选择模板方案</option></if></ifItem>
<itemTemplate> <ifItem test="{@repeateritemcount}" value="1"><if><script>location.href="template_ManageRule.aspx?projectid={$projectid}";</script></if></ifItem><option value="{$projectID}">{$projectName}</option></itemTemplate>
<ifItem test="{@repeateritemcount}" value="1" operator="&lt;"><if></select></if></ifItem></xnl:if></xnl:repeater>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Button();
</script>
</body>
</html>
