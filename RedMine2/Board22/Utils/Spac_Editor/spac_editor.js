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
var ver = "11.12.1"
var toolbar_on_set = '';
var toolbar_off_set = '';
var org_contents_value = '';
function createEditor(content_name,height,content_value,except) {
	if(content_value == undefined || content_value == '') {
		org_contents_value = '<p>&nbsp;</p>';
	} else {
		org_contents_value = content_value;
	}
	if(except == undefined) {
		except = '';
	}
	createEditorbox(content_name);
	createToolbarbox(except,content_name,height);
	createEditorCanvas(content_name,height);
}
function createEditorbox(content_name) {
	var box_id = 'spaceditor_'+content_name;
	document.write("<div id="+box_id+"></div>");
	var parentObj = document.getElementById(box_id);
	var oDiv = document.createElement("div");
	oDiv.id='editbox_area';
	oDiv.className	= "editor_box";
	parentObj.appendChild(oDiv);
}
function createToolbarbox(except,content_name,height) {
	var editbox_area_Obj = document.getElementById('editbox_area');
	var oDiv = document.createElement("div");
	oDiv.id='toolbar_area';
	oDiv.className	= "menu_bg";
	editbox_area_Obj.appendChild(oDiv);
	setToolbars(except,content_name,height);
	oDiv.innerHTML = toolbar_on_set;
}
function setToolbars(except,content_name,height) {
	var except_array = except.split(",");
	var toolbar_array	= new Array(
			"font|글자모양|viewFontBoxes(event)|0",
			"fontsize|글자크기|viewFontSizeBoxes(event)|0",
			"bold|굵게|htmledit('Bold')|0",
			"italic|눕혀서|htmledit('italic')|0",
			"underline|밑줄|htmledit('underline')|0",
			"strikethrough|취소선|htmledit('strikethrough')|0",
			"superscript|윗첨자|htmledit('superscript')|0",
			"subscript|아래첨자|htmledit('subscript')|0",
			"fontcolor|글자색상바꾸기|viewFontColorTable(event,'ForeColor')|1",
			"backcolor|글자배경색상바꾸기|viewFontColorTable(event,'BackColor')|1",
			"justifyleft|왼쪽정렬|htmledit('justifyleft')|2",
			"justifycenter|가운데정렬|htmledit('justifycenter')|2",
			"justifyright|오른쪽정렬|htmledit('justifyright')|2",
			"outdent|내어쓰기|htmledit('outdent')|2",
			"indent|들여쓰기|htmledit('indent')|2",
			"insertunorderedlist|점리스트|htmledit('insertunorderedlist')|2",
			"insertorderedlist|숫자리스트|htmledit('insertorderedlist')|2",
			"createlink|하이퍼링크|addLink(event)|3",
			"unlink|하이퍼링크제거|htmledit('Unlink')|3",
			"image_link|웹이미지 삽입하기|viewImageLinkForm(event)|4",
			"image|이미지 업로드/서버저장소 이미지 사용|viewFileTree()|4",
			"emoticon|이모티콘 삽입|viewEmoticon(event)|4",
//			"emoticon2|움직이는 이모티콘 삽입|viewEmoticonWin()|4",
			"special_chars|특수문자 삽입|viewSpecialChars(event)|4",
			"textbox|글상자|viewTextBoxes(event)|5",
			"table|표넣기|viewTableBoxes(event)|5",
			"sourcebox|소스박스넣기|insertSourceBox()|5",
			"increase|에디터높이늘리기(+100px)|increase()|5",
			"decrease|에디터높이줄이기(-100px)|decrease()|5",
			"fullscreen|전체화면으로 확대하기|fullScreen('"+content_name+"','"+height+"')|5"
	);
	var toolbar_str_array = new Array();
	var toolbar_str_array2= new Array();
	for(var i = 0; i < 6; i++) {
		toolbar_str_array[i] = "";
		toolbar_str_array2[i] = "";
	}
	var except_ok		= 0;
	var values	= new Array();
	var image	= "";
	var name	= "";
	var func	= "";
	var group	= "";
	for(i = 0; i < toolbar_array.length; i++) {
		values	= toolbar_array[i].split("|");
		image	= values[0];
		name	= values[1];
		func	= values[2];
		group	= values[3];
		except_ok = 0;
		for( j = 0; j < except_array.length; j++) {
			if(image == except_array[j] || except_array[j] == 'all') {
				except_ok++;
			}
		}
		if(except_ok == 0) {
			toolbar_str_array[group] += "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/"+image+".png\" id='btn_editor_"+image+"' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\""+func+"\" style=\"cursor:pointer\" title=\""+name+"\" align=\"absmiddle\" width='16' height='16'></div>";
			toolbar_str_array2[group] += "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/"+image+".png\" id='btn2_editor_"+image+"' class=\"trans_icon\" title=\"HTML 소스모드에서는 툴을 사용할수없습니다.\" align=\"absmiddle\" width='16' height='16'></div>";
		}
	}
	var seperator_str			= "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/seperator.png\"  class=\"menu_btn\" align=\"absmiddle\" width='10' height='16'></div>";
	var toolbar_buttons = "";
	var toolbar_buttons2 = "";
	for(var k = 0; k < 6; k++) {
		if(toolbar_str_array[k] != "") {
			toolbar_buttons		+= toolbar_str_array[k];
			toolbar_buttons2	+= toolbar_str_array2[k];
			if(toolbar_str_array[k+1]) {
				toolbar_buttons		+= seperator_str;
				toolbar_buttons2	+= seperator_str;
			}
		}
	}
	var source_button			= "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/sourceoff.png\" id='btn_editor_source' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\"toggleViewMode()\" style=\"cursor:pointer\" title=\"HTML소스보기로 전환\" align=\"absmiddle\" width='16' height='16'></div>";
	if(toolbar_buttons != "") {
		source_button		+= seperator_str;
	}
	var source_button2			= "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/sourceon.png\" id='btn_editor_source' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\"toggleViewMode()\" style=\"cursor:pointer\" title=\"디자인보기로 전환\" align=\"absmiddle\" width='16' height='16'></div>";
	if(toolbar_buttons != "") {
		source_button2		+= seperator_str;
	}
	except_ok = 0;
	for( j = 0; j < except_array.length; j++) {
		if('source' == except_array[j]) {
			except_ok++;
		}
	}
	if(except_ok > 0) {
		source_button = "";
		source_button2 = "";
	}
	if(toolbar_buttons != '' || source_button != '') image = "whats", func = "viewInfo(event)"; toolbar_buttons += seperator_str + "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/"+image+".png\" id='btn_editor_"+image+"' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\""+func+"\" style=\"cursor:pointer\" title=\"Spac Editor는?\" align=\"absmiddle\" width='16' height='16'></div>"; toolbar_buttons2 += seperator_str + "<div class='toolbar_unit'><img src=\"/Web/Approval/Utils/Spac_Editor/images/"+image+".png\" id='btn_editor_"+image+"' class=\"trans_icon\" title=\"Spac Editor는?\" align=\"absmiddle\" width='16' height='16'></div>";		
	toolbar_on_set	 = "";
	toolbar_on_set	+= source_button;
	toolbar_on_set	+= toolbar_buttons;
	toolbar_off_set	 = "";
	toolbar_off_set	+= source_button2;
	toolbar_off_set	+= toolbar_buttons2;
}
function createEditorCanvas(content_name,height) {
	var editbox_area_Obj = document.getElementById('editbox_area');
	var oDiv = document.createElement("div");
	oDiv.id='editor_canvas_area';
	oDiv.className	= "editor_bg";
	editbox_area_Obj.appendChild(oDiv);
	oDiv.innerHTML = "<div id='blank_div' style='display:none'></div><iframe name=\"editor\" src=\"\" id=\"id_iframe\" class=\"iframe\" style=\"width:99.5%;height:"+height+"px\" scrolling=\"auto\" frameborder=\"0\"></iframe><textarea name=\""+content_name+"\" id=\"id_textarea\" rows=\"\" cols=\"\" class=\"textarea\" style=\"width:99.5%;height:"+height+"px\"></textarea><input type='hidden' name='view_html_source' id='view_html_sources' value='0'><input type='hidden' name='screen_mode' id='se_screen_mode' value='0'>";
}
function isIE() {
	if (document.body && document.body.contentEditable != undefined && window.ActiveXObject) {
		return true;
	} else {
		return false;
	}
}
function initializeEditor() {
	var initStyle = "<!DOCTYPE html><html><head><link rel=\"stylesheet\" type=\"text/css\" href=\"spac_editor_note.css\"><link rel=\"stylesheet\" type=\"text/css\" href=\"spac_editor_common.css\"><script src='spac_editor_common.js'></script></head><body style='font-size:12px;'>" + org_contents_value + "</body></html>";
	editor.document.open();
	editor.document.write(initStyle);
	editor.document.close();
	if (isIE()) {
		editor.document.body.contentEditable = "true";
	} else {
		editor.document.designMode = "on";
	}
	addSubmitEvent();
	addMyKeyEvent(editor.document,"onkeydown",function (event) { 
		var e = event; 
		var keycode = e.keyCode; 
		if(keycode == 9) {	
			insertTabSpace();
			if(isFF()) {
				e.preventDefault();
			} else {
				e.returnValue = false;
			}
		}
		if(keycode == 27) {
			hideBlindScreen();
		}
	}); 
	addMyKeyEvent(editor.document,"onclick",function (event) {
		editor.focus();
	}); 
	addMyKeyEvent(document,"onkeydown",function (event) { 
		var e = event; 
		var keycode = e.keyCode; 
		if(keycode == 27) {	
			hideBlindScreen();
		}
	});
	editor.focus();
}
if(window.attachEvent) { 
	window.attachEvent('onload',initializeEditor); 
} else if(window.addEventListener) { 
	window.addEventListener('load',initializeEditor,false); 
}
function syncEditor() {
	if(document.getElementById('view_html_sources').value == '0') {
		document.getElementById('id_textarea').value = document.getElementById("id_iframe").contentWindow.document.body.innerHTML;
	} else {
		document.getElementById("id_iframe").contentWindow.document.body.innerHTML = document.getElementById('id_textarea').value;
		document.getElementById('id_textarea').value = document.getElementById("id_iframe").contentWindow.document.body.innerHTML;
	}
}
function addSubmitEvent(){
	var form = document.forms[0];
	for(var i = 0; i < document.forms.length; i++) {
		if(	document.forms[i].view_html_source ) {
			form = document.forms[i];
		}
	}
	if(form.addEventListener) {
		form.addEventListener('submit',function OnSumbmit() { syncEditor(); }, true);
	} else {
		form.attachEvent('onsubmit', function() { syncEditor(); });
	}
}
function checkSpacContents() {
	syncEditor();
	var con_obj = document.getElementById('id_textarea');
	if(con_obj.value == '' || con_obj.value == '<p>&nbsp;</p>' || con_obj.value == '<p><br></p>' || con_obj.value == '<P>&nbsp;</P>' || con_obj.value == '<br>') {
		return false;
	} else {
		return true;
	}
}
function insertTabSpace() {
	var tabhtml = "<span class=\"Apple-tab-span\" style=\"white-space:pre\">	</span>";
	if(isIE()) {
		var tabhtml = "<span style=\"margin-left:30px\">&nbsp;</span>";
	}
	makeHTML(tabhtml);
	focusEditor();
}
function addMyKeyEvent(Element,Handle,Action) { 
	if (window.attachEvent) { 
		return Element.attachEvent(Handle,Action); 
	} else if (window.addEventListener) { 
		return Element.addEventListener(Handle.replace(/^on/i,""),Action,false); 
	} 
}
function checkEditable() {
	if(document.getElementById('id_view_source') && document.getElementById('id_view_source').checked == true) {
		alert("HTML 소스보기에서는 수행할 수 없습니다. 디자인보기로 변환후 사용해주세요");
		return;
	}
}
function focusEditor() {
	if (isIE()) {
		document.getElementById('id_iframe').contentWindow.focus();
	} else {
		editor.focus();
	}
}
var SelectRange;
var SelectString;
function getSelectedEditorText() {
	editor.focus();
	if (isIE()) {
		SelectRange		= editor.document.selection.createRange();
		SelectString	= SelectRange.text;
	} else {
		SelectRange		= editor.getSelection();
		SelectString	= SelectRange.toString();
	}
	return SelectString;
}
function getSelectedEditorHtmlText() {
	if (isIE()) {
		editor.focus();
		SelectRange			= editor.document.selection.createRange();
		return SelectRange.htmlText;
	} else {
		editor.focus();
		SelectRange = editor.getSelection().getRangeAt(0);
		var dummy = editor.document.createElement('p');
		dummy.appendChild(SelectRange.cloneContents());
		var html = dummy.innerHTML;
		return html;
	}
}
function addLink(event) {
	SelectString = getSelectedEditorHtmlText();
	if(SelectString == "") {
		alert("링크걸 대상을 먼저 선택해주세요");
		return;		
	}
	SelectString.match(/target[\s]*=[\s]*[\"]*(\_blank|\_self)/gi);
	var org_target = RegExp.$1;
	if(org_target == "") {
		org_target = "_self";
	}
	SelectString.match(/href[\s]*=[\s]*[\"]*([:a-zA-Z0-9\/\.\_]+)/gi);
	var org_link = RegExp.$1; 
	if(org_link == "") {
		org_link = "http://";
	}
	var box_width	= 300;
	var table		= "";
		table		+= "<div class='property' style='width:310px'>";
		table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/createlink.png' align='absmiddle' alt='create link window'> 하이퍼링크 만들기</div>";
		table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		table		+= "<div class='textboxes_contents'>";
		table		+= "<form name='inputHyperLinkForm' onSubmit=\"return inputHyperlink(this)\" style='display:inline'>";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=3 cellspacing=1 bgcolor='#d9d9d9'>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td>";
		table		+= "			링크대상 : ";
		table		+= "			<select name='hyperlink_target' id='hyperlink_targets' title='링크가 열릴 창 대상을 선택합니다.'>";
		table		+= "				<option value='_self'>_self</option>";
		if(org_target == "_blank") {		
			table		+= "				<option value='_blank' selected='selected'>_blank</option>";
		} else {
			table		+= "				<option value='_blank'>_blank</option>";
		}
		table		+= "			</select>";	
		table		+= "		</td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td>";
		table		+= "			링크주소 : <input type='text' name='hyperlink_value' id='hyperlink_values' size='30' value='"+org_link+"' title='링크 주소를 입력합니다.'>";
		table		+= "		</td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=0>";
		table		+= "	<tr>";
		table		+= "		<td align='center'>";
		table		+= "			<input type='image' src='/Web/Approval/Utils/Spac_Editor/images/btn_createlink.png' title='하이퍼링크를 입력합니다.'>";
		table		+= "		</td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "</form>";
		table		+= "</div>";
		var left	 = (getScrollSize('width'))/2-(box_width/2);
		viewBlindScreen(table,event,left);
		document.getElementById('hyperlink_values').focus();
		document.getElementById('hyperlink_values').select();
}
function inputHyperlink(form) {
	var hyperlink_target	= document.getElementById('hyperlink_targets').value;
	var hyperlink_value		= document.getElementById('hyperlink_values').value;
	if(hyperlink_value == "" || hyperlink_value == "http://") {
		alert("링크를 입력해 주세요");
		document.getElementById('hyperlink_values').select();
		return false;
	}
	if(isIE()) {
		editor.focus();
		SelectRange.execCommand('UnLink');
		var html				= "<a href=\""+hyperlink_value+"\" target=\""+hyperlink_target+"\">"+SelectString+"</a>";
		SelectRange.select();
		SelectRange.pasteHTML(html);
	} else {
		editor.focus();
		editor.document.execCommand('UnLink', false, null);
		var html				= "<a href=\""+hyperlink_value+"\" target=\""+hyperlink_target+"\">"+SelectString+"</a>";
		makeHTML(html);
	}
	hideBlindScreen();
	return false;
}
function toggleViewMode() {
	var toolbar_area_Obj = document.getElementById('toolbar_area');
	var obj = document.getElementById('view_html_sources');
	if(obj.value == '1') {
		document.getElementById('id_iframe').style.display = "block";
		document.getElementById('id_textarea').style.display = "none";
		editor.document.body.innerHTML = document.getElementById('id_textarea').value;
		toolbar_area_Obj.innerHTML = toolbar_on_set;
		obj.value = '0';
	} else {
		document.getElementById('id_iframe').style.display = "none";
		document.getElementById('id_textarea').style.display = "block";
		document.getElementById('id_textarea').value = editor.document.body.innerHTML;
		toolbar_area_Obj.innerHTML = toolbar_off_set;
		obj.value = '1';
	}
}
function viewBlindToolbars() {
	var obj			= document.getElementById('toggle_toolbar_box');
	obj.style.display = "none";
}
function hideBlindToolbars() {
	var obj			= document.getElementById('toggle_toolbar_box');
	obj.style.display = "block";
}
function htmledit(excute){
	checkEditable();
	focusEditor();
	editor.document.execCommand(excute, false, null);
}
function htmledit2(excute,value) {
	checkEditable();
	editor.document.execCommand(excute,false,value);
}
function makeHTML(html) {
	editor.focus();
	if (isIE()) {
		SelectRange			= editor.document.selection.createRange();
		SelectRange.pasteHTML(html);
	} else {
		editor.document.execCommand('inserthtml', null, html);
	}
}
function insertSourceBox() {
	var boxString = "<div class=\"source\"><ol><li>&nbsp;</li></ol></div>&nbsp;";
	makeHTML(boxString);
}
function viewFontColorTable(event,action) {
	SelectRange = null;
	editor.focus();
	SelectString = getSelectedEditorHtmlText();
	if(action == 'ForeColor') {
		head_icon = 'fontcolor';
	} else {
		head_icon = 'backcolor';
	}
	var clr = new Array('00','AA','FF');
	var code = '';
	code_table		 = '';
	code_table		+= "<div class='property' style='width:220px'>";
	code_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/"+head_icon+".png' align='absmiddle' alt='create table window'> Font "+action+"</div>";
	code_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
	code_table		+= "<div class='textboxes_contents'>";
	code_table		+= "<table width='210' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
	code_table		+= "	<tr bgcolor='#f2f2f2'>";
	code_table		+= "		<td>";
	for (var i=0;i<clr.length;i++) { 
		for (var j=0;j<clr.length;j++) {
			for (var k=0;k<clr.length;k++) {
				code = "#"+clr[i]+clr[j]+clr[k];
				code_table += "<div style='float:left;width:13px;height:13px;margin-right:1px;background-color:"+code+"'><a href=\"javascript:affectFontColor('"+action+"')\"><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='13' height='13' onMouseOver=\"PreviewColor('"+code+"')\" title='["+code+"]클릭하세요' alt='select code'></a></div>";
			}
		}
	}
	code_table		+= "		</td>";
	code_table		+= "	</tr>";
	code_table		+= "</table>";
	code_table		+= "<div style='padding:3px;margin-top:5px;width:auto;clear:both'><input type='text' name='spac_fontcolor_value' id='spac_fontcolor_value' value='' size='8'> <input type='button' value='적용하기' onClick=\"affectFontColor('"+action+"')\"></div>";
	code_table		+= "</div>";
	var box_width = 350;
	viewBlindScreen(code_table,event);
}
function PreviewColor(color) {
	var obj = document.getElementById('spac_fontcolor_value');
	obj.style.backgroundColor = color;
	obj.value = color;
}
function affectFontColor(action) {
	var obj = document.getElementById('spac_fontcolor_value');
	var color_value = obj.value;
	var style_string = '';
	if(action == 'ForeColor') {
		style_string = 'color:'+color_value;
		if(SelectString == "") {
			if(isIE()) {
				var html = "<span style='"+style_string+"''><span id='spacST'></span>&nbsp;<span id='spacED'></span></span>";
				SelectRange.pasteHTML(html);
				var stObj = editor.document.getElementById('spacST');
				var edObj = editor.document.getElementById('spacED');
				SelectRange.moveToElementText(stObj);
				var SelRangeTemp = editor.document.body.createTextRange() ;
				SelRangeTemp.moveToElementText( edObj ) ;
				SelectRange.setEndPoint( 'EndToEnd', SelRangeTemp ) ;
				SelectRange.select();
				stObj.parentNode.removeChild( stObj );
				edObj.parentNode.removeChild( edObj );
			} else {
				makeHTML("<span style='"+style_string+"'>&nbsp;</span>");
			}
		} else {
			htmledit2(action,color_value);
			changeFontColor(color_value);
		}
	} else {
		htmledit2(action,color_value);
	}
	hideBlindScreen();
}
function changeFontColor(color) {
	var fontElements = editor.document.getElementsByTagName("font");
	for (var i = 0, len = fontElements.length; i < len; ++i) {
		if(fontElements[i].getAttribute("color")) {		
			fontElements[i].removeAttribute("color");
			fontElements[i].style.color = color;
		}
	}
}
function fullScreen(content_name,height) {
	var screen_mode = document.getElementById('se_screen_mode').value;
	if(screen_mode == '0') {
		viewFullScreen(content_name);
		document.getElementById('se_screen_mode').value = '1';
		document.getElementById('btn_editor_fullscreen').src = "/Web/Approval/Utils/Spac_Editor/images/nomalscreen.png";
		document.getElementById('btn_editor_fullscreen').title = "기본화면으로 축소하기";
	} else {
		viewNomalScreen(content_name,height);
		document.getElementById('se_screen_mode').value = '0';
		document.getElementById('btn_editor_fullscreen').src = "/Web/Approval/Utils/Spac_Editor/images/fullscreen.png";
		document.getElementById('btn_editor_fullscreen').title = "전체화면으로 확대하기";
	}
}
function viewFullScreen(content_name) {
	var box_id = 'spaceditor_'+content_name;
	var box_obj = document.getElementById(box_id);
	box_obj.style.zindex = '105';
	box_obj.style.position = 'absolute';
	box_obj.style.left = 0;
	box_obj.style.top = 0;
	box_obj.style.width = '100%';
	box_obj.style.height = "100%";	
	var full_height = (box_obj.clientHeight - 50);
	var ed_obj = document.getElementById('id_iframe');
	ed_obj.style.height = full_height + 'px';
	var tx_obj = document.getElementById('id_textarea');
	tx_obj.style.height = full_height + 'px';
}
function viewNomalScreen(content_name,height) {
	var ed_obj = document.getElementById('id_iframe');
	ed_obj.style.height = height + 'px';
	var tx_obj = document.getElementById('id_textarea');
	tx_obj.style.height = height + 'px';
	var box_id = 'spaceditor_'+content_name;
	var box_obj = document.getElementById(box_id);
	box_obj.style.zindex = '';
	box_obj.style.position = '';
}
function increase() {
	var ed_obj = document.getElementById('id_iframe');
	var tx_obj = document.getElementById('id_textarea');
	var height = ed_obj.style.height;
	height		= eval(height.replace(/px/,''));
	height		+= 100;
	ed_obj.style.height = height + 'px';
	tx_obj.style.height = height + 'px';
}
function decrease() {
	var ed_obj = document.getElementById('id_iframe');
	var tx_obj = document.getElementById('id_textarea');
	var height = ed_obj.style.height;
	height		= eval(height.replace(/px/,''));
	if(height <= 100) {
		alert('더이상 줄일 수 없습니다.');
		return;
	}	
	height		-= 100;
	ed_obj.style.height = height + 'px';
	tx_obj.style.height = height + 'px';
}
function viewFileTree() {
	window.open("/Spac_Editor/spac_editor_file_tree.php","spac_file_tree","top=10,left=10,width=800,height=800,status=yes,scrollbars=yes,resizable=yes");
}
function viewFontBoxes(event) {
	var fontbox_table	= "";
	var font_array		=  new Array("나눔고딕","맑은 고딕","굴림","굴림체","돋움","돋움체","바탕","바탕체","궁서","Arial","Tahoma","Verdana");
	fontbox_table		+= "<div class='property' style='width:138px'>";
	fontbox_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/font.png' align='absmiddle' alt='font window'> 글자체</div>";
	fontbox_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
	fontbox_table		+= "<div class='textboxes_contents'>";
	fontbox_table		+= "<table width='' border=0 cellpadding=0 cellspacing=1 bgcolor='#d9d9d9'>";
	fontbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	fontbox_table		+= "		<td>";
	for(var i = 0; i < font_array.length; i++) {
		fontbox_table	+= "			<a href=\"javascript:setFontFamily('"+font_array[i]+"')\"><div style='clear:both;width:120px;font-size:12px;padding:3px;font-family:"+font_array[i]+";background-color:#f2f2f2' onMouseOver=\"this.style.backgroundColor='#d9d9d9'\" onMouseOut=\"this.style.backgroundColor='#f2f2f2'\">"+font_array[i]+"</div></a>";
	}
	fontbox_table		+= "		</td>";
	fontbox_table		+= "	</tr>";
	fontbox_table		+= "</table>";
	fontbox_table		+= "</div>";
	viewBlindScreen(fontbox_table,event);
}
function setFontFamily(font) {
	editor.focus();
	SelectString		= getSelectedEditorHtmlText();
	if(SelectString == "") {
		if(isIE()) {
			var SelRange = editor.document.selection.createRange();
			var html = "<span style='font-family:"+font+"'><span id='spacST'></span>&nbsp;<span id='spacED'></span></span>";
			SelRange.pasteHTML(html);
			var stObj = editor.document.getElementById('spacST');
			var edObj = editor.document.getElementById('spacED');
			SelRange.moveToElementText(stObj);
			var SelRangeTemp = editor.document.body.createTextRange() ;
			SelRangeTemp.moveToElementText( edObj ) ;
			SelRange.setEndPoint( 'EndToEnd', SelRangeTemp ) ;
			SelRange.select();
			stObj.parentNode.removeChild( stObj );
			edObj.parentNode.removeChild( edObj );
		} else {
			makeHTML("<span style='font-family:"+font+"'>&nbsp;</span>");
		}
	} else {
		htmledit2("fontName",font);
		changeFontFamily(font);
	}
	hideBlindScreen();
}
function changeFontFamily(font) {
	var fontElements = editor.document.getElementsByTagName("font");
	for (var i = 0, len = fontElements.length; i < len; ++i) {
		if(fontElements[i].getAttribute("face")) {		
			fontElements[i].removeAttribute("face");
			fontElements[i].style.fontFamily = font;
		}
	}
}
var fontsize_match = new Array();
	fontsize_match["7"] = "36pt";
	fontsize_match["6"] = "24pt";
	fontsize_match["5"] = "18pt";
	fontsize_match["4"] = "14pt";
	fontsize_match["3"] = "12pt";
	fontsize_match["2"] = "10pt";
	fontsize_match["1"] = "8pt";
function viewFontSizeBoxes(event) {
	var fontbox_table	= "";
	var fontsize_array		=  new Array("1","2","3","4","5","6","7");
	fontbox_table		+= "<div class='property' style='width:280px'>";
	fontbox_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/fontsize.png' align='absmiddle' alt='font size window'> 글자크기</div>";
	fontbox_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
	fontbox_table		+= "<div class='textboxes_contents'>";
	fontbox_table		+= "<table width='100%' border=0 cellpadding=0 cellspacing=1 bgcolor='#d9d9d9'>";
	fontbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	fontbox_table		+= "		<td>";
	for(var i = 0; i < fontsize_array.length; i++) {
		fontbox_table	+= "			<a href=\"javascript:setFontSize('"+fontsize_array[i]+"')\"><div style='clear:both;width:auto;padding:3px;background-color:#f2f2f2;font-size:"+fontsize_match[fontsize_array[i]]+"' onMouseOver=\"this.style.backgroundColor='#d9d9d9'\" onMouseOut=\"this.style.backgroundColor='#f2f2f2'\">안녕!("+fontsize_match[fontsize_array[i]]+")</div></a>";
	}
	fontbox_table		+= "		</td>";
	fontbox_table		+= "	</tr>";
	fontbox_table		+= "</table>";
	fontbox_table		+= "</div>";
	viewBlindScreen(fontbox_table,event);	
}
function setFontSize(size) {
	editor.focus();
	SelectString		= getSelectedEditorHtmlText();
	if(SelectString == "") {
		if(isIE()) {
			var SelRange = editor.document.selection.createRange();
			var html = "<span style='font-size:"+fontsize_match[size]+"'><span id='spacST'></span>&nbsp;<span id='spacED'></span></span>";
			SelRange.pasteHTML(html);
			var stObj = editor.document.getElementById('spacST');
			var edObj = editor.document.getElementById('spacED');
			SelRange.moveToElementText(stObj);
			var SelRangeTemp = editor.document.body.createTextRange() ;
			SelRangeTemp.moveToElementText( edObj ) ;
			SelRange.setEndPoint( 'EndToEnd', SelRangeTemp ) ;
			SelRange.select();
			stObj.parentNode.removeChild( stObj );
			edObj.parentNode.removeChild( edObj );
		} else {
			makeHTML("<span style='font-size:"+fontsize_match[size]+"'>&nbsp;</span>");
		}
	} else {
		htmledit2("fontSize",size);
		changeFontSize();
	}
	hideBlindScreen();
}
function changeFontSize() {
	var fontElements = editor.document.getElementsByTagName("font");
	for (var i = 0, len = fontElements.length; i < len; ++i) {
		if(fontElements[i].getAttribute("size")) {	
			size = fontElements[i].size;
			fontElements[i].removeAttribute("size");
			fontElements[i].style.fontSize = fontsize_match[size];
		}
	}
}
function viewTextBoxes(event) {
	editor.focus();
	var box_width = 365;
	var textbox_table	 = "";
	var color_array		 = new Array("white","black","gray","silver","brown","red","orange","yellow","green","limegreen","skyblue","blue","purple","violet");
	textbox_table		+= "<div class='property' style='width:378px'>";
	textbox_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/textbox.png' align='absmiddle' alt='textbox window'>  글상자</div>";
	textbox_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
	textbox_table		+= "<div class='textboxes_contents'>";
	textbox_table		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td width='50' align='center'>";
	textbox_table		+= "			외곽선";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td>";
	textbox_table		+= "			<select name='spac_box_style' id='spac_box_style' onChange='affrectTextBoxString()'>";
	textbox_table		+= "			<option value='solid'>실선</option>";
	textbox_table		+= "			<option value='dotted'>가는점선</option>";
	textbox_table		+= "			<option value='dashed'>굵은점선</option>";
	textbox_table		+= "			<option value=''>선없음</option>";
	textbox_table		+= "			</select>";
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			선색상";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td><div style='float:left'><input type='text' name='spactxtbox_border_color' id='spactxtbox_border_color' value='gray'  size='3' class='colorpicker'  onKeyUp='affrectTextBoxString()'></div>";
	for(var i = 0; i < color_array.length; i++) {
		textbox_table	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-right:1px;margin-bottom:1px'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTextboxBorderColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:pointer'></div>";
	}
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			배경색";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td><div style='float:left'><input type='text' name='spactxtbox_bg_color' id='spactxtbox_bg_color' value='white' size='3' class='colorpicker' onKeyUp='affrectTextBoxString()'></div>";
	for(var i = 0; i < color_array.length; i++) {
		textbox_table	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-right:1px;margin-bottom:1px'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTextboxBackgroundColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:pointer'></div>";
	}
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			글자색";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td><div style='float:left'><input type='text' name='spactxtbox_font_color' id='spactxtbox_font_color' value='black' size='3' class='colorpicker' onKeyUp='affrectTextBoxString()'></div>";
	for(var i = 0; i < color_array.length; i++) {
		textbox_table	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-right:1px;margin-bottom:1px'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTextboxFontColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:pointer'></div>";
	}
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			가로길이";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td>";
	textbox_table		+= "			<input type='text' name='spac_text_box_width' id='spac_text_box_width' disabled value='' size='3' style='text-align:right' onKeyUp='affrectTextBoxString()'>px";
	textbox_table		+= "			<input type='checkbox' name='spac_auto_width' id='spac_auto_width' value='1' checked='checked' onClick='resizableWidth(this)'><label for='spac_auto_width'>꽉채우기</label>";
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			안쪽여백";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td>";
	textbox_table		+= "			<input type='text' name='spac_text_box_padding' id='spac_text_box_padding' value='5' size='3' style='text-align:right' onKeyUp=\"affrectTextBoxString()\" >PX";
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "	<tr bgcolor='#f2f2f2'>";
	textbox_table		+= "		<td align='center'>";
	textbox_table		+= "			바깥여백";
	textbox_table		+= "		</td>";
	textbox_table		+= "		<td>";
	textbox_table		+= "			왼쪽:<input type='text' name='spac_text_box_margin_left' id='spac_text_box_margin_left' value='5' size='2' style='text-align:right' onKeyUp='affrectTextBoxString()'>PX, ";
	textbox_table		+= "			오른쪽:<input type='text' name='spac_text_box_margin_right' id='spac_text_box_margin_right' value='5' size='2' style='text-align:right' onKeyUp='affrectTextBoxString()'>PX";
	textbox_table		+= "		</td>";
	textbox_table		+= "	</tr>";
	textbox_table		+= "</table>";
	textbox_table		+= "<div class='spac_preview_box' id='spac_text_box'></div>";
	textbox_table		+= "<div style='padding:3px;margin-top:5px;width:auto;text-align:center'><img src='/Web/Approval/Utils/Spac_Editor/images/insert_html.png' onClick=\"inputTextBox()\" style='cursor:pointer' title='글상자를 에디터에 삽입합니다.' alt='insert html button'></div>";
	textbox_table		+= "</div>";
	var left			= (getScrollSize('width'))/2-(box_width/2);
	viewBlindScreen(textbox_table,event,left);
	affrectTextBoxString();
}
function affrectTextBoxString() {	
	var border_style		= document.getElementById('spac_box_style').value;
	var border_color		= document.getElementById('spactxtbox_border_color').value;
	var bg_color			= document.getElementById('spactxtbox_bg_color').value;
	var font_color			= document.getElementById('spactxtbox_font_color').value;
	var box_width			= document.getElementById('spac_text_box_width').value;
	var box_padding			= document.getElementById('spac_text_box_padding').value;
	var box_margin_left		= document.getElementById('spac_text_box_margin_left').value;
	var box_margin_right	= document.getElementById('spac_text_box_margin_right').value;
	var spac_auto_width		= document.getElementById('spac_auto_width');
	if(spac_auto_width.checked == true) {
		box_width = 'auto';
	} else {
		box_width += 'px';
	}
	var box_string			= "<div style='border-width:1px;border-style:"+border_style+";border-color:"+border_color+";padding:"+box_padding+"px;margin-left:"+box_margin_left+"px;margin-right:"+box_margin_right+"px;width:"+box_width+";color:"+font_color+";background-color:"+bg_color+"'><p>글상자 미리보기</p></div>";
	var obj = document.getElementById('spac_text_box');
	obj.innerHTML = box_string; 
}
function inputTextBox() {
	var obj = document.getElementById('spac_text_box');
	var html_string = obj.innerHTML + "<p>&nbsp;</p>";
	editor.focus();
	makeHTML(html_string);
	hideBlindScreen();
}
function previewTextboxBorderColor(color) {
	document.getElementById('spactxtbox_border_color').value = color;
	affrectTextBoxString();
}
function previewTextboxBackgroundColor(color) {
	document.getElementById('spactxtbox_bg_color').value = color;
	affrectTextBoxString();
}
function previewTextboxFontColor(color) {
	document.getElementById('spactxtbox_font_color').value = color;
	affrectTextBoxString();
}
function resizableWidth(obj) {
	var width_obj	= document.getElementById('spac_text_box_width');
	if(obj.checked == true) {
		width_obj.disabled = true;
	} else {
		width_obj.disabled = false;
		width_obj.focus();
		width_obj.select();
	}
	affrectTextBoxString();
}
function viewImageLinkForm(event) {
	editor.focus();
	var width = 0;
	var height = 0;
	var box_width = 410;
	var table = "";
		table		+= "<div class='property' style='width:420px'>";
		table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/image_link.png' align='absmiddle' alt='image link window'> 웹이미지 삽입하기</div>";
		table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		table		+= "<div class='textboxes_contents'>";
		table		+= "<form name='inputImageForm' onSubmit=\"return inputImagelink(this)\">";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=3 cellspacing=1 bgcolor='#d9d9d9'>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td width='120' align='center'>이미지주소</td>";
		table		+= "		<td bgcolor='#ffffff'><input type='text' name='image_hyperlink_value' id='image_hyperlink_values' size='30' value='http://' title='이미지 주소를 입력합니다.' onFocus='this.select()'><input type='button' value='확인' onClick='previewWebImage()'></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>가로길이</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"width\" id='img_width' value=\""+width+"\" class=\"\" size='5' onKeyUp=\"calcImageAutoHeight()\"> px</td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>세로길이</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"height\"  id='img_height' value=\""+height+"\" class=\"\" size='5' onKeyUp=\"syncPreviewImageSize()\" disabled> px <input type='checkbox' name='auto_height' id='img_auto_height' value='1' onClick=\"calcImageAutoHeight()\" checked='checked'><label for='img_auto_height'>가로길이에 비례</label></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>이미지정렬</td>";
		table		+= "		<td bgcolor='#ffffff'><select name=\"align\" id='img_align'><option value=\"top\">top</option><option value=\"middle\">middle</option><option value=\"bottom\" selected='selected'>bottom</option><option value=\"left\">left</option><option value=\"right\">right</option></select></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>이미지안내</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"alt\"  id='img_alt' size='30' value=\"\" class=\"\"></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>하이퍼링크</td>";
		table		+= "		<td bgcolor='#ffffff'><select name=\"target\" id='img_target'><option value=\"_blank\">_blank</option><option value=\"_self\">_self</option></select><br/><input type=\"text\" name=\"link\" id='img_link' value=\"\" class=\"\" size='34'></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td bgcolor='#ffffff' colspan='2'><div id='image_previews' style='margin:auto;width:395px;height:100px;overflow:auto;background-color:#FFFFFF'><p style='margin-top:40px;text-align:center;'>[이미지 미리보기]</p></div></td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=0>";
		table		+= "	<tr>";
		table		+= "		<td align='center'>";
		table		+= "			<input type='image' src='/Web/Approval/Utils/Spac_Editor/images/insert_html.png' title='하이퍼링크를 입력합니다.'>";
		table		+= "		</td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "</form>";
		table		+= "</div>";
		var left	 = (getScrollSize('width'))/2-(box_width/2);
		viewBlindScreen(table,event,left);
}
var web_image_ratio = 1;
function previewWebImage() {
	var url				= document.getElementById('image_hyperlink_values').value;
	var obj				= document.getElementById('image_previews');
	obj.innerHTML		= "<img src='"+url+"' id='preview_images'>";
	var img_obj			= document.getElementById('preview_images');
	var image_names		= url.split("/");
	image_name			= image_names[(image_names.length-1)];
	web_image_ratio	= eval(img_obj.height)/eval(img_obj.width);
	document.getElementById('img_width').value	= img_obj.width;
	document.getElementById('img_height').value = img_obj.height;
	document.getElementById('img_alt').value	= image_name;
}
function calcImageAutoHeight() {
	if(document.getElementById('img_auto_height').checked == true) {
		var new_width	= eval(document.getElementById('img_width').value);
		var new_height	= Math.floor(new_width*eval(web_image_ratio));
		document.getElementById('img_height').value = new_height;
		document.getElementById('img_height').disabled = true;
	} else {
		document.getElementById('img_height').disabled = false;
	}
	syncPreviewImageSize();
	return;
}
function syncPreviewImageSize() {
	var img_obj		= document.getElementById('preview_images');
	img_obj.width	= document.getElementById('img_width').value;
	img_obj.height	= document.getElementById('img_height').value;
}
function inputImagelink(form) {
	var d = new Date();
	var d_image_id = "I_"+d.getFullYear() +"_"+ (d.getMonth() + 1)+"_"+ d.getDate()+"_"+d.getHours()+"_"+d.getMinutes()+"_"+d.getSeconds();
	if(form.image_hyperlink_values.value == '' || form.image_hyperlink_values.value == 'http://') {
		alert("이미지 주소를 입력해 주세요");
		form.image_hyperlink_values.select();
		return false;
	}
	var img_obj			= document.getElementById('preview_images');
	var html			= "";
	html							 = "<img src='"+form.image_hyperlink_value.value+"' align='"+form.align.value+"'";
	if(form.width.value != '' && form.width.value != '0') {
		html		+= " width='"+form.width.value+"' ";
	}
	if(form.height.value != '' && form.height.value != '0') {
		html		+= " height='"+form.height.value+"' ";
	}
	if(form.alt.value != '') {
		html			+= "  alt='"+form.alt.value+"' title='"+form.alt.value+"' ";
	}
	html							+= " id='"+d_image_id+"' onmouseup=\"viewImageContextMenu(this,event)\">";
	if(form.link.value != '') {
		html			= "<a href='"+form.link.value+"' target='"+form.target.value+"'>" + html + "</a>";
	}
	editor.focus();
	makeHTML(html);
	hideBlindScreen();
	return false;
}
function viewInfo(event) {
	editor.focus();
	var box_width = 250;
	var box_heigt = '';
	var fontbox_table	= "";
		fontbox_table		+= "<div class='property' style='width:260px'>";
		fontbox_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/whats.png' align='absmiddle' alt='what`s spac window'>  Spac Editor Info</div>";
		fontbox_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		fontbox_table		+= "<div class='textboxes_contents'>";
		fontbox_table		+= "<table width='"+box_width+"' height='"+box_heigt+"' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
		fontbox_table		+= "	<tr bgcolor='#f2f2f2'>";
		fontbox_table		+= "		<td>";
		fontbox_table		+= "			<div style='font-size:14px;color:black;font-style:italic'>Spac Editor v."+ver+"</div>";
		fontbox_table		+= "			<div style='color:gray;margin-top:5px;font-style:italic'>SIMPLE WYSIWYG HTML WEB EDITOR</div>";
		fontbox_table		+= "			<div style='color:gray;margin-top:5px;font-style:italic;'><a href='http://www.webeditor.kr' target='_blank' title='go website!'>http://www.webeditor.kr</a></div>";
		fontbox_table		+= "		</td>";
		fontbox_table		+= "	</tr>";
		fontbox_table		+= "</table>";
		fontbox_table		+= "</div>";
		var left			= (getScrollSize('width'))/2-(box_width/2);
		viewBlindScreen(fontbox_table,event,left);
}
//var emoticons = new Array("10ton.gif","aniolek.gif","balwan.gif","beczy.gif","beksa.gif","ble.gif","boks.gif","budzik.gif","caluje.gif","chrapie.gif","cmok.gif","cool.gif","czas.gif","czaszka.gif","diabel.gif","dom.gif","gadula.gif","gryz.gif","gwizd.gif","hihi.gif","hura.gif","jem.gif","jezyk.gif","komorka.gif","kreci.gif","kwiatek.gif","kwitnie.gif","lapka.gif","lezaca.gif","luzak.gif","mail.gif","mniam.gif","niedobrze.gif","nie.gif","nienie.gif","nie_wiem.gif","oczko.gif","ok.gif","papa.gif","pechowiec.gif","piorun.gif","piwo.gif","prezent.gif","pytanie.gif","pytanie2.gif","serduszko.gif","smiech.gif","smutas.gif","spioch.gif","stop.gif","strach.gif","stuk.gif","taktak.gif","tanczaca.gif","telefon.gif","tuli.gif","usmiech.gif","usta.gif","wsciekly.gif","wykrzyknik.gif","zdziwona.gif","ziewanie.gif","zly.gif");
var emoticons = new Array("Angel","Ambivalent","Grin","Crazy","Nerd","Naughty","Yum","ThumbsUp","ThumbsDown","Blush","Confused","Gasp","Heart","Hot","Smile","Laugh","Tongue","Innoncent","Angry","Crying","Wink","Kiss","LargeGasp","Money-mouth","ohnoes","Pirate","Sarcastic","Sealed","Sick","Frown","Sweat","Undecided","VeryAngry");
var emoticon_basedir = "/Web/Approval/Utils/Spac_Editor/emoticon/";
function viewEmoticonWin() {
	editor.focus();
	var box_width = 280;
	var left			= (getScrollSize('width'))/2-(box_width/2);
	window.open("/Spac_Editor/spac_editor_emoticon.php","spac_emoticon","top=100,left="+left+",width="+box_width+",height=127,status=yes,scrollbars=yes,resizable=yes");
}
function inputEmoticonOpener(emo_no) {
	html	= getEmoticonHtml(emo_no);
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
function viewEmoticon(event) {
	var box_width = 300;
	var box_heigt = '';
	var emoticon_filedir = "";
	var emoticonbox_table	= "";
		emoticonbox_table		+= "<div class='property' style='width:310px'>";
		emoticonbox_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/emoticon.png' align='absmiddle' alt='emoticon window'> 이모티콘 넣기</div>";
		emoticonbox_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		emoticonbox_table		+= "<div class='textboxes_contents'>";
		emoticonbox_table		+= "<table width='"+box_width+"' height='"+box_heigt+"' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
		emoticonbox_table		+= "	<tr bgcolor='#f9f9f9'>";
		emoticonbox_table		+= "		<td>";
		for(var i = 0; i < emoticons.length; i++) {
			emoticon_filedir	 = emoticon_basedir + emoticons[i];
			emoticonbox_table	+= "			<img src='"+emoticon_filedir+".png' alt='"+emoticons[i]+"' title='"+emoticons[i]+"' class=\"menu_btn\" onMouseOver=\"this.className='menu_btn_up'\" onMouseOut=\"this.className='menu_btn'\" onClick=\"inputEmoticon('"+i+"')\" id='spac_emo_"+i+"'>";
		}
		emoticonbox_table		+= "<span style='float:right;margin-top:5px'><a href='http://dlanham.com' target='_blank' title='이모티콘제공'>http://dlanham.com</a></span></td>";
		emoticonbox_table		+= "	</tr>";
		emoticonbox_table		+= "</table>";
		emoticonbox_table		+= "</div>";
		var left			= (getScrollSize('width'))/2-(box_width/2);
		viewBlindScreen(emoticonbox_table,event,left);
}
function inputEmoticon(emo_no) {
	html	= getEmoticonHtml(emo_no);
	editor.focus();
	makeHTML(html);
	hideBlindScreen();
}
function getEmoticonHtml(emo_no) {
	var emoticon_obj = document.getElementById('spac_emo_'+emo_no);
	html	= "<img src='"+emoticon_obj.src+"' width='"+emoticon_obj.width+"' height='"+emoticon_obj.height+"' alt='"+emoticon_obj.alt+"' title='"+emoticon_obj.title+"'>";
	return html;
}
function viewSpecialChars(event) {
	var chars_array = new Array("§","※","☆","★","○","●","◎","◇","◆","□","■","△","▲","▽","▼","→","←","↑","↓","↔","◁","◀","▷","▶","♤","♠","♡","♥","♧","♣","⊙","◈","▣","◐","◑","▒","▤","▥","▨","▧","▦","▩","♨","☏","☎","☜","☞","¶","†","‡","↕","↗","↙","↖","↘","♩","♪","♬","㉿","㈜","№","㏇","™","㏂","㏘","℡","®");
	var box_width = 250;
	var specialchars_table	= "";
		specialchars_table		+= "<div class='property' style='width:260px'>";
		specialchars_table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/special_chars.png' align='absmiddle' alt='special chars window'>  특수문자 넣기</div>";
		specialchars_table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		specialchars_table		+= "<div class='textboxes_contents'>";
		specialchars_table		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
		specialchars_table		+= "	<tr bgcolor='#f9f9f9'>";
		specialchars_table		+= "		<td>";
		for(var i = 0; i < chars_array.length; i++) {
			specialchars_table	+= "			<div class='chars_btn'><a href=\"javascript:inputChars('"+chars_array[i]+"')\" title='클릭하면 입력됩니다.'>"+chars_array[i]+"</a></div>";
		}
		specialchars_table		+= "		</td>";
		specialchars_table		+= "	</tr>";
		specialchars_table		+= "</table>";
		specialchars_table		+= "</div>";
		var left			= (getScrollSize('width'))/2-(box_width/2);
		viewBlindScreen(specialchars_table,event,left);
}
function inputChars(chars) {
	makeHTML(chars);
	hideBlindScreen();
}
var d_table_id = '';
function makeTableTag(row_num,col_num,border_color,bg_color,font_color,table_width,table_align,table_style) {
	var d = new Date();
	d_table_id = "T_"+d.getFullYear() +"_"+ (d.getMonth() + 1)+"_"+ d.getDate()+"_"+d.getHours()+"_"+d.getMinutes()+"_"+d.getSeconds();
	var table_style = getTableStyleCode(table_style);
	var table_tags  = "";
		table_tags += "<table align='"+table_align+"' width='"+table_width+"' border='0' cellspacing='1' cellpadding='2' bgcolor='"+border_color+"' id='"+d_table_id+"'  onmouseup=\"viewTableContextMenu(this,event)\">";
		for(var j = 0; j < row_num; j++) {
			table_tags += "<tr>";
			for(var i = 0; i < col_num; i ++) {
				if(j == 0) {
					table_tags += "<td style='"+table_style+"'>제목"+(i+1)+"</td>";
				} else {
					var cell_string = '&nbsp;';
					if(i == 0 && j == 1) cell_string = '내용';
					table_tags += "<td style='color:"+font_color+";background-color:"+bg_color+"'>"+cell_string+"</td>";
				}
			}
			table_tags += "</tr>";
		}
		table_tags += "</table>";
		return table_tags;
}
function viewTableBoxes(event) {
	var init_table		= makeTableTag(3,4,"gray","white","black","100%",'center','gray');
	editor.focus();
	var box_width = 365;
	var tables	 = "";
	var color_array		 = new Array("white","black","gray","silver","brown","red","orange","yellow","green","limegreen","skyblue","blue","purple","violet");
	tables		+= "<div class='property' style='width:375px'>";
	tables		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/table.png' align='absmiddle' alt='create table window'>  표 삽입하기</div>";
	tables		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
	tables		+= "<div class='textboxes_contents'>";
	tables		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=1 bgcolor='#d9d9d9'>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td width='50' align='center'>";
	tables		+= "			스타일";
	tables		+= "		</td>";
	tables		+= "		<td>";
	tables		+= "			<select name='spactbl_table_class' id='spactbl_table_class' onChange='affectTableStylePreview(this)'><option value='white'>white</option><option value='black'>black</option><option value='gray' selected='selected'>gray</option><option value='blue'>blue</option><option value='green'>green</option><option value='orange'>orange</option>";
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td width='50' align='center'>";
	tables		+= "			행열수";
	tables		+= "		</td>";
	tables		+= "		<td>";
	tables		+= "			<table align='left' border='0' cellpadding='1' cellspacing='0'><tr><td rowspan='2'>행수 : </td><td rowspan='2'><input type='text' name='spactble_row_num' id='spactbl_row_num' value='3' size='2' onKeyUp='affectTablePreview()'></td><td valign='bottom' height='11px'><a href=\"javascript:modCount('spactbl_row_num','up')\"><img src='/Web/Approval/Utils/Spac_Editor/images/btn_up.gif'></a></td></tr><tr><td valign='top' height='11px'><a href=\"javascript:modCount('spactbl_row_num','down')\"><img src='/Web/Approval/Utils/Spac_Editor/images/btn_down.gif'></a></td></tr></table>";
	tables		+= "			 <table align='left' border='0' cellpadding='1' cellspacing='0'><tr><td rowspan='2'>&nbsp; 열수 : </td><td rowspan='2'><input type='text' name='spactble_col_num' id='spactbl_col_num' value='4' size='2' onKeyUp='affectTablePreview()'></td><td valign='bottom' height='11px'><a href=\"javascript:modCount('spactbl_col_num','up')\"><img src='/Web/Approval/Utils/Spac_Editor/images/btn_up.gif'></a></td></tr><tr><td valign='top' height='11px'><a href=\"javascript:modCount('spactbl_col_num','down')\"><img src='/Web/Approval/Utils/Spac_Editor/images/btn_down.gif'></a></td></tr></table>";
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td width='50' align='center'>";
	tables		+= "			정렬";
	tables		+= "		</td>";
	tables		+= "		<td>";
	tables		+= "			<select name='table_align' id='table_align' onChange='affectTablePreview()'><option value='left'>왼쪽정렬</option><option value='center' selected>가운데정렬</option><option value='right'>오른쪽정렬</option></select>";
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td align='center'>";
	tables		+= "			선색상";
	tables		+= "		</td>";
	tables		+= "		<td><div style='float:left'><input type='text' name='spactbl_border_color' id='spactbl_border_color' value='gray' size='3' class='colorpicker' onKeyUp='affectTablePreview()'></div>";
	for(var i = 0; i < color_array.length; i++) {
		tables	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-left:1px;'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTblBorderColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:default'></div>";
	}
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td align='center'>";
	tables		+= "			배경색";
	tables		+= "		</td>";
	tables		+= "		<td><div style='float:left'><input type='text' name='spactbl_bg_color' id='spactbl_bg_color' value='white' size='3' onKeyUp='affectTablePreview()' class='colorpicker'></div>";
	for(var i = 0; i < color_array.length; i++) {
		tables	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-left:1px'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTblBackgroundColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:default'></div>";
	}
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td align='center'>";
	tables		+= "			글자색";
	tables		+= "		</td>";
	tables		+= "		<td><div style='float:left'><input type='text' name='spactbl_font_color' id='spactbl_font_color' value='black' size='3' class='colorpicker' onKeyUp='affectTablePreview()'></div>";
	for(var i = 0; i < color_array.length; i++) {
		tables	+= "			<div style='width:auto;float:left;background-color:"+color_array[i]+";margin-left:1px'><img src='/Web/Approval/Utils/Spac_Editor/images/color_selector.gif' width='16' height='16' onClick=\"previewTblFontColor('"+color_array[i]+"')\" alt='"+color_array[i]+"' title='"+color_array[i]+"' style='cursor:default'></div>";
	}
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "	<tr bgcolor='#f2f2f2'>";
	tables		+= "		<td align='center'>";
	tables		+= "			가로길이";
	tables		+= "		</td>";
	tables		+= "		<td>";
	tables		+= "			<input type='text' name='spac_text_box_width' id='spac_text_box_width' disabled value='100' size='3' style='text-align:right' onKeyUp='affectTablePreview()'><span id='table_width_unit'>%</span>";
	tables		+= "			<input type='checkbox' name='spac_auto_width' id='spac_text_box_auto_width' value='1' checked='checked' onClick='autoWidthTable(this)'><label for='spac_text_box_auto_width'>꽉채우기</label>";
	tables		+= "		</td>";
	tables		+= "	</tr>";
	tables		+= "</table>";
	tables		+= "<div class='spac_preview_box' id='spac_table_box'>"+init_table+"</div>";
	tables		+= "<div style='padding:3px;margin-top:5px;width:auto;text-align:center'><img src='/Web/Approval/Utils/Spac_Editor/images/insert_html.png' onClick=\"inputTableBox()\" style='cursor:pointer' title='표를 에디터에 삽입합니다.' alt='insert html button'></div>";
	tables		+= "</div>";
	var left			= (getScrollSize('width'))/2-(box_width/2);
	viewBlindScreen(tables,event,left);
}
function autoWidthTable(obj) {
	var table_width			= document.getElementById('spac_text_box_width');
	var table_width_unit	= document.getElementById('table_width_unit');
	if(obj.checked == true) {
		table_width.value	= "100";
		table_width_unit.innerHTML = "%";
		table_width.disabled = true;
	} else {
		table_width.value	= "100";
		table_width_unit.innerHTML = "px";
		table_width.disabled = false;
		table_width.focus();
		table_width.select();
	}
	affectTablePreview();
}
function previewTblBorderColor(color) {
	document.getElementById('spactbl_border_color').value = color;
	affectTablePreview();
}
function previewTblBackgroundColor(color) {
	document.getElementById('spactbl_bg_color').value = color;
	affectTablePreview();
}
function previewTblFontColor(color) {
	document.getElementById('spactbl_font_color').value = color;
	affectTablePreview();
}
function affectTablePreview() {
	var table_class		= document.getElementById('spactbl_table_class').value;
	var row_num			= document.getElementById('spactbl_row_num').value;
	var col_num			= document.getElementById('spactbl_col_num').value;
	var table_align		= document.getElementById('table_align').value;
	var border_color	= document.getElementById('spactbl_border_color').value;
	var bg_color		= document.getElementById('spactbl_bg_color').value;
	var font_color		= document.getElementById('spactbl_font_color').value;
	var table_width		= document.getElementById('spac_text_box_width').value;
	var table_auto_width= document.getElementById('spac_text_box_auto_width').value;
	var table_width_unit= document.getElementById('table_width_unit').innerHTML;
	table_width_value = table_width+table_width_unit;
	var table_string = makeTableTag(row_num,col_num,border_color,bg_color,font_color,table_width_value,table_align,table_class);
	var obj = document.getElementById('spac_table_box');
	obj.innerHTML = table_string;
}
function affectTableStylePreview(obj) {
	var table_style = obj.value;
	document.getElementById('spactbl_border_color').value	= table_style;
	document.getElementById('spactbl_bg_color').value		= '#FFFFFF';
	document.getElementById('spactbl_font_color').value		= table_style;
	if(table_style == 'white') document.getElementById('spactbl_border_color').value	= '#000000';
	if(table_style == 'white') document.getElementById('spactbl_font_color').value		= '#000000';
	affectTablePreview();
}
function getTableStyleCode(table_style) {
	var fontcolor = '#FFFFFF'
	if(table_style == 'white') fontcolor = '#000000';
	return "background-color:"+table_style+";color:"+fontcolor+";text-align:center;";
}
function inputTableBox() {
	var obj = document.getElementById('spac_table_box');
	var html_string = obj.innerHTML + "<p>&nbsp;</p>";
	editor.focus();
	makeHTML(html_string);
	setCurrCellIdx(d_table_id);
	hideBlindScreen();
}
function modCount(obj_id,action) {
	var obj = document.getElementById(obj_id);
	if(action == 'up') {
		obj.value		= eval(obj.value)+1;
	} else {
		obj.value		= eval(obj.value)-1;
	}
	affectTablePreview();
}
var obj = {};
var table_obj;
var curr_tr_idx	= 0;
var curr_td_idx = 0;
function setCurrCellIdx(tblTD) {
	table_obj = editor.document.getElementById(tblTD);
	var oRows = table_obj.getElementsByTagName("TR");
	var oCols;
	var oCol;
	for (var i=0; i<oRows.length; i++){
		var oRow = oRows[i];
		oRow.onmouseover=function(){
		  obj.clickedRowIndex=this.rowIndex;
		};		
		oCols = oRow.getElementsByTagName("TD");
		oCol;
		for(var j = 0; j < oCols.length; j++) {
			oCol = oCols[j];
			oCol.onmouseover=function(){
			  obj.clickedCellIndex=this.cellIndex;
			};
		}
	}
	curr_tr_idx = obj.clickedRowIndex;
	curr_td_idx = obj.clickedCellIndex;
}
function addRow(tblTD,no) {
	setCurrCellIdx(tblTD);
	if(curr_tr_idx != undefined) {
		var rowNb = table_obj.rows.length;
		var colNb = table_obj.rows[curr_tr_idx].cells.length;
		var new_tr_idx = curr_tr_idx;
		var sample_tr_idx = curr_tr_idx+1;
		if(no == '2') {
			new_tr_idx		= curr_tr_idx+1;
			sample_tr_idx	= curr_tr_idx;
		}
		var objRow	= table_obj.insertRow(new_tr_idx);	
		var objCell;
		for(var i = 0; i < colNb; i++) {
			objCell = objRow.insertCell();
			objCell.innerHTML = "&nbsp;";
			objCell.style['backgroundColor'] = table_obj.rows[sample_tr_idx].cells[i].style['backgroundColor'];
			objCell.style['color'] = table_obj.rows[sample_tr_idx].cells[i].style['color'];
			objCell.colSpan = table_obj.rows[sample_tr_idx].cells[i].colSpan;
		}
	}
	hideContextMenu();
}
function addCell(tblTD,no) {
	setCurrCellIdx(tblTD);
	if(curr_tr_idx != undefined && curr_td_idx != undefined) {
		var rowNb = table_obj.rows.length;
		var colNb = table_obj.rows[0].cells.length;
		var objCell;		
		var new_td_idx = curr_td_idx;
		var sample_td_idx = curr_td_idx+1;
		if(no == '2') {
			new_td_idx = curr_td_idx+1;
			sample_td_idx = curr_td_idx;
		}
		for(var i = 0; i < rowNb; i++) {
			objCell = table_obj.rows[i].insertCell(new_td_idx);
			objCell.innerHTML = "&nbsp;";
			objCell.style['backgroundColor']	= table_obj.rows[i].cells[sample_td_idx].style['backgroundColor'];
			objCell.style['color']				= table_obj.rows[i].cells[sample_td_idx].style['color'];
			objCell.style['textAlign']			= table_obj.rows[i].cells[sample_td_idx].style['textAlign'];
			if(i == 0) {
				objCell.innerHTML = "제목n";
			}
		}
	}
	hideContextMenu();
}
function deleteRow(tblTD) {
	setCurrCellIdx(tblTD);
	if(curr_tr_idx != undefined) {
		var rowNb = table_obj.rows.length;
		if(rowNb < 2) {
			alert("더이상 삭제할 수 없습니다.");
		} else {
			var objRow	= table_obj.deleteRow(curr_tr_idx);	
		}
	}
	hideContextMenu();
}
function deleteCol(tblTD) {
	setCurrCellIdx(tblTD);
	if(curr_td_idx != undefined) {
		var rowNb = table_obj.rows.length;
		var colNb = table_obj.rows[0].cells.length;
		var objCell;
		if(colNb < 2) {
			alert("더이상 삭제할 수 없습니다. ");
		} else {
			for(var i = 0; i < rowNb; i++) {
				objCell = table_obj.rows[i].deleteCell(curr_td_idx);
			}
		}
	}
	hideContextMenu();
}
function mergeCell(tblTD,where) {
	setCurrCellIdx(tblTD);
	if(curr_tr_idx != undefined && curr_td_idx != undefined) {
		var rowNb = table_obj.rows.length;
		var colNb = table_obj.rows[curr_tr_idx].cells.length;
		if(curr_td_idx >= (colNb-1) && where == '1') {
			alert("오른쪽에 병합할 셀이 없습니다.");
		} else if (curr_tr_idx >= (rowNb-1) && where == '2') {
			alert("아래쪽에 병합할 셀이 없습니다.");
		} else {
			var colspan_you = 1;
			var rowspan_you = 1;
			var rowspan_me = 1;
			var data_you = '';
			if(where == '1') {
				data_you = table_obj.rows[curr_tr_idx].cells[(curr_td_idx+1)].innerHTML;
				colspan_you = table_obj.rows[curr_tr_idx].cells[(curr_td_idx+1)].colSpan;
				objCell = table_obj.rows[curr_tr_idx].deleteCell(curr_td_idx+1);
				objCell2 = table_obj.rows[curr_tr_idx].cells[curr_td_idx].colSpan += colspan_you;
				objCell2 = table_obj.rows[curr_tr_idx].cells[curr_td_idx].innerHTML += " "+data_you;
			}
			if(where == '2') {
				data_you = table_obj.rows[curr_tr_idx+1].cells[(curr_td_idx)].innerHTML;
				rowspan_me = table_obj.rows[curr_tr_idx].cells[curr_td_idx].rowSpan;
				rowspan_you = table_obj.rows[curr_tr_idx+rowspan_me].cells[(curr_td_idx)].rowSpan;
				objCell = table_obj.rows[curr_tr_idx+rowspan_me].deleteCell(curr_td_idx);
				objCell2 = table_obj.rows[curr_tr_idx].cells[curr_td_idx].rowSpan += rowspan_you;
				objCell2 = table_obj.rows[curr_tr_idx].cells[curr_td_idx].innerHTML += " "+data_you;
			}
		}
	}
	hideContextMenu();
}
function devideCell(tblTD) {
	setCurrCellIdx(tblTD);
	var colspan_me = table_obj.rows[curr_tr_idx].cells[curr_td_idx].colSpan;
	var rowspan_me = table_obj.rows[curr_tr_idx].cells[curr_td_idx].rowSpan;
	if(colspan_me > 1 || rowspan_me > 1) {
		var orgHTML			= table_obj.rows[curr_tr_idx].cells[curr_td_idx].innerHTML;
		var orgBgcolor		= table_obj.rows[curr_tr_idx].cells[curr_td_idx].style['backgroundColor'];
		var orgColor		= table_obj.rows[curr_tr_idx].cells[curr_td_idx].style['color'];
		var orgAlign		= table_obj.rows[curr_tr_idx].cells[curr_td_idx].style['textAlign'];
		objCell = table_obj.rows[curr_tr_idx].deleteCell(curr_td_idx);
		for(var i = 0; i < rowspan_me; i++) {
			for(var j = 0; j < colspan_me; j++) {
				objCell = table_obj.rows[curr_tr_idx+i].insertCell(curr_td_idx+j);
				if(i ==0 && j == 0) {
					objCell.innerHTML					= orgHTML;
				} else {
					objCell.innerHTML					= "&nbsp;";
				}			
				objCell.style['backgroundColor']	= orgBgcolor;
				objCell.style['color']				= orgColor;
				objCell.style['textAlign']			= orgAlign;
			}
		}
	} else {
		alert("병합된셀이 아닙니다.");
	}
	hideContextMenu();
}
var image_obj;
function viewImageProperty(img_id) {
	image_obj = editor.document.getElementById(img_id);	
	var img_src		= image_obj.src;
	var img_align	= image_obj.align;
	if(img_align == '') img_align = 'bottom';
	var img_width	= image_obj.width;
	var img_height	= image_obj.height;
	var img_title	= image_obj.title;
	var img_margin	= image_obj.style.margin.replace(/px/,"");
	if(img_margin == '') img_margin = 0;
	web_image_ratio	= eval(img_height)/eval(img_width);
	var box_width = 400;
	var table	= "";
		table		+= "<div class='property' style='width:413px'>";
		table		+= "<div class='textboxes_title'><img src='/Web/Approval/Utils/Spac_Editor/images/image_property.png' align='absmiddle' alt='special chars window'>  이미지 속성</div>";
		table		+= "<div style='float:right;width:auto;padding:3px'><img src='/Web/Approval/Utils/Spac_Editor/images/btn_close2.png' style='cursor:pointer' onClick='hideBlindScreen()' alt='close property window' title='닫기'></div>";
		table		+= "<div class='textboxes_contents'>";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=3 cellspacing=1 bgcolor='#d9d9d9'>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td colspan='2'><div id='image_previews' style='margin:auto;width:395px;height:120px;overflow:auto;background-color:#FFFFFF;color:gray'><img id='preview_images' src='"+img_src+"' align='"+img_align+"' width='"+img_width+"' height='"+img_height+"' title='"+img_title+"' alt='"+img_title+"' style='margin:"+img_margin+"px'>1. 동해물과 백두산이 마르고 닳도록 하느님이 보우하사 우리나라 만세. 무궁화 삼천리 화려강산 대한 사람, 대한으로 길이 보전하세<br/>2. 남산 위에 저 소나무, 철갑을 두른 듯 바람서리 불변함은 우리 기상일세. 무궁화 삼천리 화려강산 대한 사람, 대한으로 길이 보전하세<br/>3. 가을 하늘 공활한데 높고 구름 없이 밝은 달은 우리 가슴 일편단심일세. 무궁화 삼천리 화려강산 대한 사람, 대한으로 길이 보전하세<br/>4. 이 기상과 이 맘으로 충성을 다하여 괴로우나 즐거우나 나라 사랑하세. 무궁화 삼천리 화려강산 대한 사람, 대한으로 길이 보전하세</div></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td width='100' align='center'>가로길이</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"width\" id='img_width' value=\""+img_width+"\" class=\"\" size='5' onKeyUp=\"calcImageAutoHeight()\"> px</td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>세로길이</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"height\"  id='img_height' value=\""+img_height+"\" class=\"\" size='5' onKeyUp=\"syncPreviewImageSize()\" disabled> px <input type='checkbox' name='auto_height' id='img_auto_height' value='1' onClick=\"calcImageAutoHeight()\" checked='checked'><label for='img_auto_height'>가로길이에 비례</label></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>이미지정렬</td>";
		table		+= "		<td bgcolor='#ffffff'><select name=\"align\" id='img_align' onChange='syncPreviewImageAlign(this)'><option value=\"top\">top</option><option value=\"middle\">middle</option><option value=\"bottom\">bottom</option><option value=\"left\">left</option><option value=\"right\">right</option></select></td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>외부여백</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"alt\"  id='img_margin' size='3' value='"+img_margin+"' onKeyUp=\"syncPreviewImageMargin(this)\">px</td>";
		table		+= "	</tr>";
		table		+= "	<tr bgcolor='#f2f2f2'>";
		table		+= "		<td align='center'>이미지안내</td>";
		table		+= "		<td bgcolor='#ffffff'><input type=\"text\" name=\"alt\"  id='img_alt' size='30' value='"+img_title+"'></td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "<table width='"+box_width+"' border=0 cellpadding=5 cellspacing=0>";
		table		+= "	<tr>";
		table		+= "		<td align='center'>";
		table		+= "			<img src='/Web/Approval/Utils/Spac_Editor/images/btn_apply_img_property.png' title='지정한 이미지속성을 적용합니다.' onClick='applyImageProperty()'>";
		table		+= "		</td>";
		table		+= "	</tr>";
		table		+= "</table>";
		table		+= "</div>";
		viewBlindScreen(table,event);
		hideContextMenu();
		document.getElementById('preview_images').align = img_align;
		document.getElementById('img_align').value = img_align;
}
function syncPreviewImageAlign(obj) {
	var i_obj = document.getElementById('preview_images');
	i_obj.align = obj.value;
}
function syncPreviewImageMargin(obj) {
	var i_obj = document.getElementById('preview_images');
	if(obj.value != '') {
		i_obj.style.margin = obj.value + "px";
	}
}
function applyImageProperty() {
	image_obj.align			= document.getElementById('img_align').value;
	image_obj.width			= document.getElementById('img_width').value;
	image_obj.height		= document.getElementById('img_height').value;
	image_obj.title			= document.getElementById('img_alt').value;
	image_obj.alt			= document.getElementById('img_alt').value;
	image_obj.style.margin	= document.getElementById('img_margin').value + "px";
	hideBlindScreen();
}
function viewData() {
	alert(document.getElementById("id_iframe").contentWindow.document.body.innerHTML);	
}