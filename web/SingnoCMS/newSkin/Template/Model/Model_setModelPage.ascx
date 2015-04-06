<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<xnl:jquery></xnl:jquery>
<script type="text/javascript" src="../JS/Plugin/XUI.min.js"></script>
<link href="../Css/Base.css" rel="stylesheet" type="text/css" />
<link href="../Css/content.css" rel="stylesheet" type="text/css" />
<link href="../Css/Plugin/XUI.css" rel="stylesheet" type="text/css" />
<title></title>
<XNL:validator name="v1">
<link type="text/css" rel="stylesheet" href="../JS/Validator/css/validator.css"></link>
<script src="../JS/Validator/js/formValidator.js" type="text/javascript"></script>
<script src="../JS/Validator/js/formValidatorRegex.js" type="text/javascript"></script>
<script src="../JS/datepicker/WdatePicker.js" type="text/javascript"></script>
<validatorItem validatorgroup="1" formid="form1"  onsuccess="function(){alert('ok')}" onerror ="function(msg){alert(msg);}" wideword="false">
</validatorItem>
</XNL:validator>
</head>
<body>
<XNL:form enableState="true" validatorName="v1" 
 sqlcommand="insert into SN_U_article(title,subtitle,imageurl,Summary,linkurl,author,source,fileurl,IsRecommend,IsHot,IsColor,IsTop,Content,addData)values(@title,@subtitle,@imageurl,@Summary,@linkurl,@author,@source,@fileurl,@IsRecommend,@IsHot,@IsColor,@IsTop,@Content,@addData)">
<form method="post" class="KForm" action="M_model_actionTest.aspx" name="form1" id="form1">
<table id="amTable">
<formItem field="title">
<validatorItem>
<formValidator  tipid="titleTipDiv" onshow="请输入标题" onfocus="请输入标题">
<inputValidator min="1" onerror="标题不能为空"></inputValidator>    
</formValidator>
</validatorItem>
	<tr>
		<td class="left">标题：</td>
		<td class="input"><input type="text" name="title" id="title"></td>
		<td class="txt"><div id="titleTipDiv" class="txt"></div></td>
	</tr>
</formItem>
<formItem field="subtitle">
<validatorItem>
<formValidator onshow="请输入副标题"  tipid="subtitleTipDiv" onfocus="请输入副标题"></formValidator>
</validatorItem>
	<tr>
		<td class="left">副标题：</td>
		<td class="input"><input name="subtitle" type="text" id="subtitle"/></td>
		<td class="txt"><div id="subtitleTipDiv" class="txt"></div></td>
	</tr>
</formItem>

<formItem field="ImageUrl">
<validatorItem>
<formValidator onshow="标题图片" ></formValidator>
</validatorItem>
	<tr>
		<td>标题图片：</td>
		<td><input name="ImageUrl" type="text" id="ImageUrl"/></td>
		<td><!--<div id="ImageUrlTipDiv"></div>--></td>
	</tr>
	</formItem>
<formItem field="Summary">
	<tr>
		<td>信息简介：</td>
		<td><textarea name="Summary" class="Summary"></textarea></td>
		<td><div id="SummaryTipDiv" class="txt"></div></td>
	</tr>
</formItem>
<formItem field="linkurl">	<tr>
		<td valign="top">外部链接：</td>
		<td>
        
		  <div class="icoSelect">
				<ul>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/01.gif" alt=""/></li>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/02.gif" alt=""/></li>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/03.gif" alt=""/></li>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/04.gif" alt=""/></li>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/05.gif" alt=""/></li>
					<li><img src="/singnocms/manage/SYS_img/ModelIcon/06.gif" alt=""/></li>
				</ul>
			</div>
            
		    <input name="linkurl" type="text" id="linkurl"/></td>
		<td>&nbsp;</td>
	</tr></formItem>
    <formItem field="author">
	<tr>
		<td valign="top">作者：</td>
		<td colspan="2">
			<input name="author" type="text" id="author"/></td>
	</tr>
    </formItem>
    <formItem field="source">
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			&nbsp; &nbsp;&nbsp; 来源<input name="source" type="text" id="source"/></td>
	</tr>
    </formItem>
     <formItem field="fileurl">
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			附件<input name="fileurl" type="text" id="fileurl"/></td>
	</tr>
    </formItem>
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			 <formItem field="IsRecommend" dbdefault="0">推荐  
            <input type="checkbox" name="IsRecommend" id="IsRecommend"  value="1"/>
          </formItem>
            
            <formItem field="IsHot" dbdefault="0">热点<input type="checkbox" name="IsHot" id="IsHot" value="1" /> </formItem><formItem field="IsColor" dbdefault="0">醒目<input type="checkbox" name="IsColor" id="IsColor" value="1" /></formItem>置顶<formItem field="IsTop" dbdefault="0"><input type="checkbox" name="IsTop" id="IsTop" value="1" /> </formItem>&nbsp;&nbsp;
		</td>
	</tr>
    <formItem field="Content">
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			内容
		<textarea name="Content" cols="50" rows="10" id="Content"></textarea></td>
	</tr>
     </formItem>
     <formItem field="addData">
     <validatorItem>
     <inputValidator type="data"></inputValidator> 
     </validatorItem>  
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			添加日期<input type="text" name="addData" id="addData"></td>
	
	</tr>
    </formItem>
	<tr>
		<td colspan="3" style="padding:30px 0 0 80px">
			<input name="Submit" type="submit" value="提交" id="Submit"/><input name="back" type="button" value="返回" /></td>
	</tr>
</table>
</form>
</XNL:form>
<!-- sqlcommand="insert into SN_U_article(title,subtitle,imageurl,Summary,linkurl,author,source,fileurl,IsRecommend,IsHot,IsColor,IsTop,Content,addData)values(@title,@subtitle,@imageurl,@Summary,@linkurl,@author,@source,@fileurl,@IsRecommend,@IsHot,@IsColor,@IsTop,@Content,@addData)"-->
</body>
</html>