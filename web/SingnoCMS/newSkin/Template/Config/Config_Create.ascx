<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title></title>
<link href="../css/Base.css" rel="stylesheet" type="text/css" />
<link href="../css/Config.css" rel="stylesheet" type="text/css" />
<link href="../css/plugin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="Config_SiteAction.aspx?action=modify&type=create" id="form1" name="form1" method="post">
<CMS.Manage:siteConfig action="view" type="create">
<table class="forms">
	<tbody>
		<tr>
			<td width="14%">站点出错处理：</td>
			<td width="86%">
				<select name="siteError" id="siteError">
				  <option value="toIndexPage" {[iif('{@siteerror}'='toIndexPage','selected="selected"','')]}>转向首页</option>
				  <option value="toErrorPage" {[iif('{@siteerror}'='toErrorPage','selected="selected"','')]}>转向错误页</option>
				  <option value="writeMsg" {[iif('{@siteerror}'='writeMsgNoSql' or '{@siteerror}'='writeMsgWithSql','selected="selected"','')]}>输出错误信息</option>
                </select>
			</td>
		</tr>
		<tr id="isWriteSQL" class="{[iif('{@siteerror}'='writeMsgNoSql' or '{@siteerror}'='writeMsgWithSql','','hide')]}">
			<td>是否输出sql语句：</td>
			<td>
				<input type="radio" name="writeSQL" id="writeSQL1" value="writeMsgWithSql" {[iif('{@siteerror}'='writeMsgWithSql','checked="checked"','')]}/><label for="writeSQL1">输出</label>
				<input type="radio" name="writeSQL" id="writeSQL2"  value="writeMsgNoSql"  {[iif('{@siteerror}'='writeMsgNoSql','checked="checked"','')]} {[iif('{@siteerror}'='toIndexPage' or '{@siteerror}'='toErrorPage','checked="checked"','')]}/><label for="writeSQL2">不输出</label>
			</td>
		</tr>
		<tr>
			<td>标签出错处理：</td>
			<td>
				<select name="LabelError" id="LabelError">
					<option value="writeMsg" {[iif('{@labelerror}'='writeMsg','selected="selected"','')]}>输出错误信息</option>
					<option value="throwError" {[iif('{@labelerror}'='throwError','selected="selected"','')]}>抛出异常</option>
					<option value="writeEmpty" {[iif('{@labelerror}'='writeEmpty','selected="selected"','')]}>输出空字符串</option>
				</select>
			</td>
		</tr>
  <tr>
			<td>是否预编译生成：</td>
			<td>
			  <input name="PreCompiled" type="radio" id="PreCompiled1" value="True" {[iif('{@PreCompiled}'='True','checked="checked"','')]}/><label for="PreCompiled1">是</label>
				<input name="PreCompiled" type="radio" id="PreCompiled2" value="False" {[iif('{@PreCompiled}'='False','checked="checked"','')]} /><label for="PreCompiled12">否</label>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
</CMS.Manage:siteConfig>
</form>
<!--<tr>
			<td>是否缓存模板：</td>
			<td>
				<input type="radio" name="isCache" id="isCache1" /><label for="isCache1">缓存</label>
				<input type="radio" name="isCache" id="isCache2" checked="checked" /><label for="isCache2">不缓存</label>
			</td>
		</tr>
       
		<tr>
			<td width="120px" valign="top">内容变动时生成：</td>
			<td>
				<input type="radio" name="isContentChange" id="isContentChange1" checked="checked" /><label for="isContentChange1">开启</label>
				<input type="radio" name="isContentChange" id="isContentChange2" /><label for="isContentChange2">关闭</label>
			</td>
		</tr>
		<tr>
			<td>栏目变动时生成：</td>
			<td>
				<input type="radio" name="isClassChange" id="isClassChange1" checked="checked" /><label for="isClassChange1">开启</label>
				<input type="radio" name="isClassChange" id="isClassChange2" /><label for="isClassChange2">关闭</label>
			</td>
		</tr>
        
		<tr>
			<td valign="top">生成扩展名：</td>
			<td>
				<span class="fl">
				<select name="extName" id="extName">
					<option value=".htm">.htm</option>
					<option value=".html">.html</option>
					<option value=".shtml">.shtml</option>
					<option value=".asp">.asp</option>
					<option value=".aspx">.aspx</option>
					<option value=".xml">.xml</option>
					<option value="none">自定义格式</option>
				</select></span>
				<span id="custExtName" class="hide fl"><input name="myExtName" type="text" class="name" id="myExtName" value=""/></span>
				<span class="exg" style="line-height:26px">(如果扩展名为.aspx则生成模式为动态，如果为其他扩展名则生成模式为静态)</span>
			</td>
		</tr>
		<tr>
			<td valign="top">生成方式：</td>
			<td>
				<input type="radio" name="createType" id="createType1" checked="checked" value=""/><label for="createType1">绝对路径</label>
				<input type="radio" name="createType" id="createType2" value=""/><label for="createType2">相对路径</label>
			</td>
		</tr>-->
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

//错误处理
$i('siteError').onchange = function(){
	$i('isWriteSQL').className = (this.value == 'writeMsg') ? '' :'hide';
}

////选择扩展名
//$i('extName').onchange = function(){
//	$i("custExtName").className = (this.value == 'none') ? 'fl' : 'hide fl';
//}
</script>
</body>
</html>