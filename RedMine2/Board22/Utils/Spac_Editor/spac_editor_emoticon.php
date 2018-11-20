<?php
/*
* Spac Editor v11.12.1
* 공식배포처 : http://www.webeditor.kr
* 제자자 이메일 : spacer.ha@gmail.com
* 이프로그램은 개인/기업/영리/비영리 에 관계없이 웹개발에 무료로 자유롭게 사용할 수 있습니다.
* 제작자가 배포한상태 그대로인 경우에 한해 재배포를 허용하며 수정된 소스나, 수정된 소스가 포함된 프로그램, 소스중의 일부분을 타인에게 배포하거나 판매할 수 없습니다.
* 제작자와 별도의 상의없이 본프로그램의 버전및 공식사이트 정보를 보여주는 "Spac Editor는?"(제일 오른쪽에 "?" 형태의 아이콘으로 출력됨) 버튼을 임의로 안보이도록 할 없습니다.(모든 툴바를 감추는 경우는 자동으로 감추어지므로 관계없습니다.)
* 이프로그램의 사용으로 인해 발생한 어떠한 문제도 제작자는 책임지지 않습니다.
* 사용상의 문제나 건의사항은 공식 배포사이트의 게시판을 이용해주시거나 메일로 보내 주시기 바랍니다.
*/


$emoticon_path	= "emoticon";
$emoticons		= array();
if ($handle = opendir($emoticon_path)) {
	while (false !== ($file_name = readdir($handle))) {
		if($file_name != '.' && $file_name != '..' && !is_dir($emoticon_path."/".$file_name)) {
			$emoticons[]	= $file_name;
		}
	}
	closedir($handle); 
	$handle = null;
	asort($emoticons);
}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Spac Editor v.<?=$SpacEditor->ver?> / Image Folder & File Manager</title>
<link rel="stylesheet" type="text/css" href="spac_editor.css">
<link rel="stylesheet" type="text/css" href="spac_editor_common.css">
<script language="Javascript" src="spac_editor_common.js"></script>
<script language="Javascript" src="spac_editor.js"></script>
<style type="text/css" title="">
body {
	margin:5px;
	font-size:12px;
	background-color:#f9f9f9;
}
</style>
</head>
<body>
<div style="clear:both;text-align:center;font-weight:bold;margin:5px">이모티콘 넣기</div>
<?php 
$loop = 0;
foreach($emoticons as $emoticon) {
	$emoticon_info	= preg_split("/\./",$emoticon);
	$emoticon_ext	= array_pop($emoticon_info);
	$emoticon_body	= implode(".",$emoticon_info);
	$loop_emoticon_path = "/Spac_Editor/".$emoticon_path."/".$emoticon;
	echo "<img  src='".$loop_emoticon_path."' alt='".$emoticon_body."' title='".$emoticon_body."' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\"inputEmoticonOpener('".$loop."')\" id='spac_emo_".$loop."'>";
	$loop++;
}
?>
</body>
</html>