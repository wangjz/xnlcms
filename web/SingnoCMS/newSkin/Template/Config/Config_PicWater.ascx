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
<form action="Config_SiteAction.aspx?action=modify&type=watermark" method="post" id="form1" name="form1" >
<CMS.Manage:siteConfig action="view" type="watermark">
<table class="forms">
	<tbody>
		<tr>
			<td width="120px">是否启用水印功能：</td>
			<td>
		    <input type="radio" name="isWatermark" id="isWatermark1"  value="True" {[iif('{@enabled}'='True','checked="checked"','')]}/><label for="isWatermark1">开启</label>
				<input type="radio" name="isWatermark" id="isWatermark2" value="False" {[iif('{@enabled}'='False','checked="checked"','')]}/><label for="isWatermark2">关闭</label></td>
		</tr>
	</tbody>
	<tbody id="watermarkContent" class="{[iif('{@enabled}'='True','','hide')]}">
		<tr>
			<td>添加水印位置：</td>
			<td>
				<div id="demoPic">
					<table width="100%" cellpadding="0" cellspacing="0">
						<tr>
							<td><input type="radio" name="pos" id="pos1" value="1" {[iif({@position}=1,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos2"  value="2" {[iif({@position}=2,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos3" value="3" {[iif({@position}=3,'checked="checked"','')]}/></td>
						</tr>
						<tr>
							<td><input type="radio" name="pos" id="pos4" value="4" {[iif({@position}=4,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos5" value="5" {[iif({@position}=5,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos6" value="6" {[iif({@position}=6,'checked="checked"','')]}/></td>
						</tr>
						<tr>
							<td><input type="radio" name="pos" id="pos7" value="7" {[iif({@position}=7,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos8" value="8" {[iif({@position}=8,'checked="checked"','')]}/></td>
							<td><input type="radio" name="pos" id="pos9" value="9" {[iif({@position}=9,'checked="checked"','')]} /></td>
						</tr>
					</table>
				</div>
			</td>
		</tr>
		<tr>
			<td valign="top">水印透明度：</td>
			<td><input name="alpha" type="text" id="alpha" style="width:60px" value="{@alpha}" /> % <span class="exg">(必填0-100之间的整数)</span></td>
		</tr>
		<tr>
			<td>图片最小尺寸：</td>
			<td>
				宽：<input name="imgW" type="text" id="imgW" style="width:60px" value="{@imageminwidth}"/>
		    高：<input name="imgH" type="text" id="imgH" style="width:60px" value="{@imageminheight}"/>
				<span class="exg">(单位为px，0代表不限)</span>
			</td>
		</tr>
		<tr>
			<td valign="top">水印类型：</td>
			<td>
				<input type="radio" name="textType" id="textType1" value="Image" {[iif('{@watermarktype}'='Image','checked="checked"','')]}/><label for="textType1">图片</label>
				<input type="radio" name="textType" id="textType2" value="Text" {[iif('{@watermarktype}'='Text','checked="checked"','')]}/><label for="textType2">文字</label>
			</td>
		</tr>
		<tr>
			<td colspan="2" id="isTextType">
				<table class="{[iif('{@watermarktype}'='Text','forms','hide')]}">
					<tbody>
						<tr>
							<td valign="top" width="115px">文字型水印内容：</td>
							<td><input type="text" name="content" id="content" value="{@content}"/></td>
						</tr>
						<tr>
							<td valign="top">文字水印字体：</td>
							<td>
								<select name="font" id="font">
                                	<xnl:foreach foreachvar="{@fontfamilies}" split=",">
                                    <foreachItem><option value="{@foreachvalue}"  {[iif('{@font}'='{@foreachvalue}','selected="selected"','')]}>{@foreachvalue}</option></foreachItem>
                                    </xnl:foreach>
									
								</select>
							</td>
						</tr>
						<tr>
							<td>水印字体大小：</td>
							<td><input type="text" name="fontsize" id="fontSize" style="width:60px" value="{@fontsize}"/><span class="exg">(单位为px)</span></td>
						</tr>
					</tbody>
				</table>
                <table class="{[iif('{@watermarktype}'='Image','forms','hide')]}">
					<tbody>
						<tr>
							<td valign="top" width="115px">图片路径：</td>
							<td><input name="imgPath" type="text" id="imgPath" value="{@imagepath}" /></td>
						</tr>
					</tbody>
				</table>
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
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

//开启水印模式
Sio.addEvent(Sio.get('input[name=isWatermark]'),'click',function(e){
	$i('watermarkContent').className = ($i('isWatermark1').checked) ? '' : 'hide';
});

//开启文字水印模式
Sio.addEvent(Sio.get('input[name=textType]'),'click',function(e){
	var isImageCheck=($i('textType1').checked);
	$i('isTextType').children[0].className =isImageCheck? 'hide' : 'forms';
	$i('isTextType').children[1].className = !isImageCheck ? 'hide' : 'forms';
});
</script>
</body>
</html>