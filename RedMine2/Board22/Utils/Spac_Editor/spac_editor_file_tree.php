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
?>
<?php require_once("spac_editor.class.php"); ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Spac Editor v.<?=$SpacEditor->ver?> / Image Folder & File Manager</title>
<link rel="stylesheet" type="text/css" href="spac_editor_common.css">
<link rel="stylesheet" type="text/css" href="spac_editor_file_tree.css">
<script language="Javascript" src="spac_editor_common.js"></script>
<script language="Javascript" src="spac_editor_file_tree.js"></script>
</head>
<body>
<table align='center' width='100%' height="100%" border='0' cellspacing='0' cellpadding='0'>
	<tr>
		<td width="200" valign="top">			
			<div class="tree">
				<div>
					<img src="images/ftv2folderopen.gif" align="absmiddle">
					<a href="?get_dir="><b><?=$SpacEditor->root_name?></b></a>
					<?php if(!$_GET['get_dir']) : ?>☜<?php endif; ?>
				</div>			
				<?php foreach($SpacEditor->upload_dir_array as $dir) :  ?>
				<div>					
					<?php if($dir == $SpacEditor->last_dir_name) : ?>
						<img src="images/ftv2lastnode.gif" align="absmiddle">
					<?php else : ?>	
						<img src="images/ftv2node.gif" align="absmiddle">
					<?php endif; ?>	
					<img src="images/ftv2folderclosed.gif" align="absmiddle" onmouseup="viewDirContextMenu('<?=$dir?>',event)"><a href="?get_dir=<?=$dir?>" onmouseup="viewDirContextMenu('<?=$dir?>',event)"><?=$dir?></a>
					<?php if($_GET['get_dir'] == $dir) : ?>
						☜
					<?php endif; ?>
				</div>
				<?php endforeach;  ?>
			</div>
			<?php if(!$SpacEditor->allow_folder_count || $SpacEditor->allow_folder_count > $SpacEditor->folder_count) : ?>
			<div style="margin-top:3px;">
				<form name='make_folder_form' method="POST" action="spac_editor_file_tree.php" onSubmit="return checkMakeFolderForm(this)">
					<input type="hidden" name="get_dir" value="<?=$_GET['get_dir']?>">
					<input type="hidden" name="action" value="make_folder">
					<input type="text" name="new_dir" value="" size="12" class="solid_form" title="새폴더이름을 입력해주세요"><input type="submit" value="폴더생성" class="solid_form">
				</form>
			</div>	
			<?php endif; ?>
		</td>
		<td width="5"></td>
		<td valign="top" class="contents"> 
				<table align='center' width='100%' height="450" border='0' cellspacing='0' cellpadding='0'>
					<tr>
						<td height="35" bgcolor="#d9d9d9">
							<div style="float:left;margin-left:10px;padding:5px;">
								<img src="images/ftv2folderclosed.gif" align="absmiddle"> <?=$SpacEditor->get_dir?> 디렉토리 (총 <?=$SpacEditor->file_count?> 개의 파일이 있습니다.)
							</div>
							<div style="float:right;margin-right:0px;padding:2px;"><a href="javascript:viewUploadForm()" title="파일업로드폼 보이기/감추기"><img src="images/btn_upload_down.png" id="btn_upload" alt="upload button"></div>
						</td>
					</tr>
					<tr>
						<td valign="top">
							<div class="contents_top" id="upload_form_area">
								<div>
								<form name='file_upload_form' method="POST" enctype="multipart/form-data" action="spac_editor_file_tree.php" onSubmit="return checkUploadForm(this)">
									<input type="hidden" name="allow_image_max_volume" value="<?=$SpacEditor->allow_image_max_volume?>">
									<input type="hidden" name="file_count" value="<?=$SpacEditor->file_count?>">
									<input type="hidden" name="allow_file_count" value="<?=$SpacEditor->allow_file_count?>">
									<input type="hidden" name="get_dir" value="<?=$_GET['get_dir']?>">
									<input type="hidden" name="action" value="file_upload">
									<input type="file" name="upfile" value="" title="찾아보기 버튼을 클릭하시어 파일을 선택해주세요"> <input type="submit" value="올리기" class="solid_form">
								</form>
								</div>
								<div style="margin-top:5px">
									※ 업로드 제한 : GIF,JPG,PNG포멧, 가로 <?=$SpacEditor->allow_image_max_width?>px(초과시조절됨), 용량 <?=$SpacEditor->format_size($SpacEditor->allow_image_max_volume)?>, 폴더당 파일수 <?=$SpacEditor->allow_file_count?>개
								</div>
							</div>
							<div class="contents_body">
							<form name='file_list_form' method="POST" action="spac_editor_file_tree.php" onSubmit="return deleteFile(this)">
							<input type="hidden" name="get_dir" value="<?=$_GET['get_dir']?>">
							<input type="hidden" name="upload_web_path" value="<?=$SpacEditor->upload_web_path?>">
							<input type="hidden" name="action" value="file_delete">							
							<?php $loop_count = 0; ?>
							<?php foreach($SpacEditor->upload_file_array as $file) :								 
								$box_class	= 'file_unit';
								$checked	= '';
								if($_GET['uploaded_file'] == $file['name']) {
									$box_class	= 'file_unit_up'; 
									$checked	= 'checked';
								}
							?>
								<div id="file_unit_box_<?=$loop_count?>" class="<?=$box_class?>" onMouseOver="fileboxOver(this)"  onMouseOut="fileboxOut(this,'<?=$loop_count?>')">
									<div>
										<img src="<?=$file['thumb_url']?>"  alt="<?=$file['thumb_name']?>" width="<?=$file['thumb_width']?>" height="<?=$file['thumb_height']?>" onClick="checkFile('<?=$loop_count?>')" ondblclick="insertImageOpener('<?=$file['name']?>','<?=$file['file_url']?>','<?=$file['width']?>','<?=$file['height']?>')" title="한번클릭시 선택, 더블클릭시 에디터에 입력됩니다."  onmouseup="viewFileContextMenu('<?=$_GET['get_dir']?>','<?=$file['name']?>','<?=$file['width']?>','<?=$file['height']?>','<?=$file['thumb_url']?>','<?=$file['file_url']?>',event)">
									</div>
									<div class="image_info">
										<input type="checkbox" name="chk_file[]" id="chk_<?=$loop_count?>" value="<?=$file['name']?>" title="<?=$file['name']?>" <?=$checked?> onClick="checkFile2(this,'<?=$loop_count?>')"><label for="chk_<?=$loop_count?>"  title="<?=$file['name']?>"><?=$SpacEditor->resizeString($file['name'],8)?></label>
									</div>
									<div class="image_info">
										<?=$file['width']?> × <?=$file['height']?>
									</div>
									<div class="image_info">
										<?=$file['size']?>
									</div>
								</div>
							<?php $loop_count++; ?>
							<?php endforeach;  ?>
							</div>
						</td>
					</tr>
					<tr>
						<td height="35" bgcolor="#d9d9d9">
							<div style="float:right;margin-right:0px;padding:2px;">							
								<img src="images/btn_insertimages.png" title="선택된 이미지를 모두 삽입" alt="Insert Selected Images" onClick="insertMultiImageOpener()" style="cursor:pointer" width="93" height="28">
								<input type="image" src="images/btn_delete.png" width="93" height="28" title="선택된 이미지를 모두 삭제합니다.">
							</div>	
							</form>
						</td>
					</tr>
				</table>
		</td>
	</tr>
	<tr>
		<td height="5" colspan="3"></td>
	</tr>
	<tr>
		<td height="20" colspan="3" class="bottom" align="right">Spac Editor v.<?=$SpacEditor->ver?> / Image Folder & File Manager</td>
	</tr>
</table>
</body>
</html>