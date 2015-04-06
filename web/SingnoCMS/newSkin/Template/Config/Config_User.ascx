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
<table class="forms">
	<tbody>
		<tr>
			<td width="160px">会员登陆奖励积分设置：</td>
			<td>
			<input type="radio" name="switchReg" id="switchReg1" checked="checked" /><label for="switchReg1">开启</label>
			<input type="radio" name="switchReg" id="switchReg2" /><label for="switchReg2">关闭</label>
			<span class="required">*</span>
			</td>
		</tr>
		<tr>
			<td valign="top">注册时赠送资金：</td>
			<td>
				<input type="radio" name="isSameReg" id="isSameReg1" /><label for="isSameReg1">允许</label>
				<input type="radio" name="isSameReg" id="isSameReg2" checked="checked" /><label for="isSameReg2">不允许</label>
			</td>
		</tr>
		<tr>
			<td valign="top">注册时赠送积分：</td>
			<td><textarea name="" id="" cols="80" rows="5"></textarea></td>
		</tr>
		<tr>
			<td valign="top">注册时赠送点券：</td>
			<td><textarea name="" id="" cols="80" rows="5"></textarea></td>
		</tr>
		<tr>
			<td valign="top">积分和点券兑换比率：</td>
			<td><textarea name="" id="" cols="80" rows="5"></textarea></td>
		</tr>
		<tr>
			<td valign="top">会员积分与有效期兑换比率：</td>
			<td><textarea name="" id="" cols="80" rows="5"></textarea></td>
		</tr>
		<tr>
			<td>资金与点券兑换比率：</td>
			<td><input type="text" name="" id="" style="width:50px" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td>会员资金与有效期兑换比率：</td>
			<td><input type="text" name="" id="" style="width:50px" /><span class="required">*</span></td>
		</tr>
		<tr>
			<td valign="top">点券名称：</td>
			<td><textarea name="" id="" cols="80" rows="3"></textarea></td>
		</tr>
		<tr>
			<td>点券单位：</td>
			<td>
				<input type="radio" name="isSameLogin" id="isSameLogin1" checked="checked"/><label for="isSameLogin1">允许</label>
				<input type="radio" name="isSameLogin" id="isSameLogin2"/><label for="isSameLogin2">不允许</label>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<fieldset> 
				<legend class="u">推广计划设置：</legend> 
				<table class="forms">
					<tbody>
						<tr>
							<td width="150px">推广计划开关：</td>
							<td>
								<input type="radio" name="promotionSwitch" id="promotionSwitch1"/><label for="promotionSwitch1">开启</label>
								<input type="radio" name="promotionSwitch" id="promotionSwitch2" checked="checked"/><label for="promotionSwitch2">关闭</label>
							</td>
						</tr>
					</tbody>
					<tbody id="promotionContent" class="hide">
						<tr>
							<td>赠送积分：</td>
							<td><input type="text" name="" id="" /></td>
						</tr>
						<tr>
							<td valign="top">推广文字与链接：</td>
							<td><textarea name="" id="" cols="60" rows="5"></textarea></td>
						</tr>
					</tbody>
				</table>
			</fieldset>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<td colspan="2"><div class="grayBg"><input type="submit" value="提交配置" /></div></td>
		</tr>
	</tfoot>
</table>
<script type="text/javascript" src="../JS/Sio.min.js"></script>
<script type="text/javascript">
Sio.Text();
Sio.Button();

Sio.addEvent(Sio.get('input[name=promotionSwitch]'),'click',function(e){
	$i('promotionContent').className = ($i('promotionSwitch1').checked) ? '' : 'hide'; 
});

</script>
</body>
</html>