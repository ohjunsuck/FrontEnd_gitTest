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
function insertImageOpener(name,src,width,height) {
	var d = new Date();
	var d_image_id = "I_"+d.getFullYear() +"_"+ (d.getMonth() + 1)+"_"+ d.getDate()+"_"+d.getHours()+"_"+d.getMinutes()+"_"+d.getSeconds();
	var html = "<img src='"+src+"' width='"+width+"' height='"+height+"' alt='"+name+"' title='"+name+"' id='"+d_image_id+"' onmouseup=\"viewImageContextMenu(this,event)\">";
	if (isIE()) {
		opener.document.getElementById('id_iframe').contentWindow.focus();
		var obj = opener.document.frames["id_iframe"].document.selection.createRange();
		obj.pasteHTML(html);
	} else {
		openerObj = window.opener.editor;
		openerObj.focus();
		openerObj.document.execCommand('inserthtml', null, html);
	}
	window.close();
}
function viewUploadForm() {
	var form = document.file_upload_form;
	if(eval(form.file_count.value) >= eval(form.allow_file_count.value) && eval(form.allow_file_count.value) > 0) {
		alert("더이상 파일을 등록할 수 없습니다. (폴더당최대파일개수:"+form.allow_file_count.value+")");
		return;
	}	
	var obj = document.getElementById('upload_form_area');
	var btn_obj = document.getElementById('btn_upload');
	if(obj.style.display == 'block') {
		obj.style.display = 'none';
		btn_obj.src = btn_obj.src.replace(/btn_upload_up/,'btn_upload_down');
	} else {
		obj.style.display = 'block';
		btn_obj.src = btn_obj.src.replace(/btn_upload_down/,'btn_upload_up');
	}
}
function checkFile(count) {
	var obj = document.getElementById('chk_'+count);
	var box_obj = document.getElementById('file_unit_box_'+count);
	if(obj.checked == true) {
		obj.checked = false;
		box_obj.className = 'file_unit';
	} else {
		obj.checked = true;
		box_obj.className = 'file_unit_up';
	}
}
function checkFile2(obj,count) {
	var box_obj = document.getElementById('file_unit_box_'+count);
	if(obj.checked == true) {
		box_obj.className = 'file_unit_up';
	} else {
		box_obj.className = 'file_unit';
	}
}
function fileboxOver(obj) {
	obj.className = 'file_unit_up';
}
function fileboxOut(obj,count) {
	var chk_obj = document.getElementById('chk_'+count);
	if(chk_obj.checked == false) {
		obj.className = 'file_unit';
	}
}
function deleteFile(form) {
	var chk_obj = document.getElementsByName('chk_file[]');
	var chk_count = 0;
	for(var i = 0; i < chk_obj.length; i++) {
		if(chk_obj[i].checked == true) chk_count++;
	}
	if(chk_count == 0) {
		alert("삭제할 파일을 선택해 주세요");
		return false;
	}
	if(confirm("선택하신 "+chk_count+"개 파일을 삭제하시겠습니까?")) {
		return true;
	} else {
		return false;
	}
}
function insertMultiImageOpener() {
	var form = document.file_list_form;
	var chk_obj = document.getElementsByName('chk_file[]');
	var upload_web_path = form.upload_web_path.value;
	var get_dir = form.get_dir.value;
	var chk_count = 0;
	var html = "";
	var d = new Date();
	var d_image_id = "I_"+d.getFullYear() +"_"+ (d.getMonth() + 1)+"_"+ d.getDate()+"_"+d.getHours()+"_"+d.getMinutes()+"_"+d.getSeconds();
	if(get_dir != "") upload_web_path += "/" + get_dir;
	for(var i = 0; i < chk_obj.length; i++) {
		if(chk_obj[i].checked == true) {
			d_image_id += i;
			html += "<img src='"+upload_web_path+"/"+chk_obj[i].value+"' alt='"+chk_obj[i].value+"' title='"+chk_obj[i].value+"' id='"+d_image_id+"' onmouseup=\"viewImageContextMenu(this,event)\">";
			chk_count++;
		}
	}
	if(chk_count == 0) {
		alert("에디터에 삽입할 이미지를 선택해 주세요");
		return;
	}
	if (isIE()) {
		opener.document.getElementById('id_iframe').contentWindow.focus();
		var obj = opener.document.frames["id_iframe"].document.selection.createRange();
		obj.pasteHTML(html);
	} else {
		openerObj = window.opener.editor;
		openerObj.focus();
		openerObj.document.execCommand('inserthtml', null, html);
	}
	window.close();
}
function checkUploadForm(form) {	
	if(form.upfile.value == '') {
		alert("업로드할 파일을 선택해 주세요");
		form.upfile.focus();
		return false;
	}	
	var upfiles = form.upfile.value.split(".");
	var ext		= upfiles[upfiles.length-1];
	var fileExp	= /(gif|jpg|jpeg|png)/gi;
	if ( !fileExp.test( form.upfile.value ) ) {
		alert("GIF,JPG,PNG 파일만 업로드 가능합니다.");
		form.upfile.focus();
		form.upfile.select();
		return false;
	}
	return true;
}
function checkMakeFolderForm(form) {
	if(form.new_dir.value == '') {
		alert("폴더명을 입력해주세요");
		form.new_dir.focus();
		return false;
	}
	var regExp	= /^[a-zA-Z0-9_]{2,15}$/;
	if ( !regExp.test( form.new_dir.value ) ) {
		alert("폴더명은 알파벳,숫자,언더바(_)만으로된 2~15글자를 입력해주세요");
		form.new_dir.focus();
		form.new_dir.select();
		return false;
	}
	return true;
}
function viewDirContextMenu(dirname,event) {	
	var menu_array = new Array(
		"<span style='color:red'>×</span>|deleteDir('"+dirname+"')|이폴더삭제하기"
	);
	viewContextMenu(menu_array,event);
}
function deleteDir(dirname) {
	if(confirm("폴더를 삭제하면 폴더내 파일도 모두 삭제됩니다. "+dirname+" 폴더를 삭제하시겠습니까?")) {
		document.location.href = "spac_editor_file_tree.php?get_dir="+dirname+"&action=delete_folder";
	} else {
		hideBlindScreen();
		return;
	}
}
function viewFileContextMenu(dirname,filename,width,height,thumb_url,file_url,event) {
	var menu_array = new Array(
		"<span style='color:red'>×</span>|deleteOneFile('"+dirname+"','"+filename+"')|이파일 삭제하기",
		"<span style='color:red'>▨</span>|viewImagePropery('"+dirname+"','"+filename+"','"+width+"','"+height+"','"+thumb_url+"','"+file_url+"',event)|이미지 상세정보"
	);
	viewContextMenu(menu_array,event);
}
function deleteOneFile(dirname,filename) {
	if(confirm(dirname+"/"+filename+"\"파일을 삭제하시겠습니까?")) {
		document.location.href = "spac_editor_file_tree.php?get_dir="+dirname+"&filename="+filename+"&action=file_delete_one";
	}
	hideBlindScreen();
}
var img_ratio			= 1;
function viewImagePropery(dirname,filename,width,height,thumb_url,file_url,event) {
	contextDiv				= document.getElementById('theContentsLayer');
	contextDiv.innerHTML	= '';
	contextDiv.className	= 'property';
	contextDiv.style.left	= eval(contextDiv.style.left.replace("px","")) - 230+"px";
	img_ratio			= eval(height)/eval(width);
	var html	 = '';
	html		+= "<div style='float:left;width:100px;padding:5px;font-weight:bold;height:auto;cursor:default'>Image Property</div><div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div><div style='cursor:default;clear:both'><table align='center' width='500' border='0' cellspacing='0' cellpadding='5' bgcolor=\"#d9d9d9\"><tr bgcolor=\"#FFFFFF\"><td width='135' align='center' valign=\"middle\" ><img src='"+thumb_url+"' width=120 height=120><br/><div style='margin:3px;word-break:break-all;'>"+filename+"</div></td><td valign=\"top\"><table align='center' width='100%' border='0' cellspacing='1' cellpadding='2' bgcolor=\"#d9d9d9\"><tr bgcolor=\"#FFFFFF\"><td width=90 align=right>가로길이 : </td><td width=250><input type='hidden' name='src' id='img_src' value='"+file_url+"'><input type=\"text\" name=\"width\" id='img_width' value=\""+width+"\" class=\"\" size='5' onKeyUp=\"calcImageHeight()\"> px</td></tr><tr bgcolor=\"#FFFFFF\"><td align=right>세로길이 : </td><td width><input type=\"text\" name=\"height\"  id='img_height' value=\""+height+"\" class=\"\" size='5'> px <input type='checkbox' name='auto_height' id='img_auto_height' value='1' onClick=\"calcImageHeight()\" checked='checked'><label for='img_auto_height'>가로길이에비례</label></td></tr><tr bgcolor=\"#FFFFFF\"><td align=right>위치배열 : </td><td width><select name=\"align\" id='img_align'><option value=\"top\">top</option><option value=\"middle\">middle</option><option value=\"bottom\" selected='selected'>bottom</option><option value=\"left\">left</option><option value=\"right\">right</option></select></td></tr><tr bgcolor=\"#FFFFFF\"><td align=right>이미지안내 : </td><td width><input type=\"text\" name=\"alt\"  id='img_alt' size='30' value=\""+filename+"\" class=\"\"></td></tr><tr bgcolor=\"#FFFFFF\"><td align=right>하이퍼링크 : </td><td><select name=\"target\" id='img_target'><option value=\"_blank\">_blank</option><option value=\"_self\">_self</option></select><br/><input type=\"text\" name=\"link\" id='img_link' value=\"\" class=\"\" size='34'></td></tr></table></td></tr></table><div style='padding:5px;background-color:white;text-align:center;width:auto'><img src='/Web/Approval/Utils/Spac_Editor/images/insert_html.png' onClick=\"insertImageOpenerProperty()\" style='cursor:pointer' title='이미지를 에디터에 삽입합니다.' alt='insert html button'></div></div>";
	contextDiv.innerHTML = html;
}
function calcImageHeight() {
	if(document.getElementById('img_auto_height').checked == true) {
		var new_width	= eval(document.getElementById('img_width').value);
		var new_height	= Math.floor(new_width*eval(img_ratio));
		document.getElementById('img_height').value = new_height;
	}
	return;
}
function insertImageOpenerProperty() {
	var src		= document.getElementById('img_src').value;
	var width	= document.getElementById('img_width').value;
	var height	= document.getElementById('img_height').value;
	var align	= document.getElementById('img_align').value;
	var alt		= document.getElementById('img_alt').value;
	var link	= document.getElementById('img_link').value;
	var target	= document.getElementById('img_target').value;
	var html = "";
	var d = new Date();
	var d_image_id = "I_"+d.getFullYear() +"_"+ (d.getMonth() + 1)+"_"+ d.getDate()+"_"+d.getHours()+"_"+d.getMinutes()+"_"+d.getSeconds();
	if(link == '') {
		html	= "<img src='"+src+"' width='"+width+"' height='"+height+"' align='"+align+"' alt='"+alt+"' title='"+alt+"' id='"+d_image_id+"' onmouseup=\"viewImageContextMenu(this,event)\">";
	} else {
		html	= "<a href='"+link+"' target='"+target+"' title='"+alt+"'><img src='"+src+"' width='"+width+"' height='"+height+"' align='"+align+"' alt='"+alt+"' id='"+d_image_id+"' onmouseup=\"viewImageContextMenu(this,event)\"></a>";
	}
	if (isIE()) {
		opener.document.getElementById('id_iframe').contentWindow.focus();
		var obj = opener.document.frames["id_iframe"].document.selection.createRange();
		obj.pasteHTML(html);
	} else {
		openerObj = window.opener.editor;
		openerObj.focus();
		openerObj.document.execCommand('inserthtml', null, html);
	}
	window.close();
}