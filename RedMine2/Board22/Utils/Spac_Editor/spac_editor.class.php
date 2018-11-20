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
class SpacEditor {

	var $ver = '11.12.1';

	var $base_web_path;
	var $base_abs_path;
	var $upload_web_path;
	var $upload_abs_path;
	var $upload_dir_array;
	var $upload_file_array;

	var $use_auth;
	var $islogin;
	var $se_member_id;

	var $public_add_folder;
	var $public_del_folder;
	var $public_upload_file;
	var $public_delete_file;

	var $use_member_dir;

	var $thumb_max_size;
	var $allow_image_max_width;
	var $allow_image_max_volume;
	var $allow_folder_count;
	var $allow_file_count;
	var $overwrite;

	var $get_dir;
	var $action;
	var $chk_files;
	var $file_count = 0;
	var $folder_count = 0;

	var $curr_upload_web_path;
	var $curr_upload_abs_path;
	var $curr_upload_thumb_web_path;
	var $curr_upload_thumb_abs_path;

	var $root_name;
	
	function SpacEditor() {
		session_start();
		$this->initConfig();
		$this->checkAuth();
		$this->setMemberVars();
		$this->checkAction();
		$this->getTree();
		$this->getCurrFiles();
	}
	function initConfig() {

		$this->base_web_path				= '/Spac_Editor';
		$this->base_abs_path				= '.';

		//파일업로드 경로(쓰기권한필요)
		$this->upload_web_path				= '/Spac_Editor/upload';
		$this->upload_abs_path				= 'upload';
		$this->upload_dir_array				= array();
		$this->upload_file_array			= array();
		
		//auth
		$this->use_auth						= false;			//인증접속? (true면 $_SESSION['SE_LOGIN'] 값이 true 인경우만 접속가능)
		$this->islogin						= false;			//수정하지 마세요
		$this->se_member_id					= null;				//수정하지 마세요

		//방문자 권한 (미인증) : 위 use_auth 가 false 인경우만 적용됨 (관리자는 $_SESSION['SE_LOGIN'] 값이 true 여야 함)
		$this->public_add_folder			= true;			//방문자가 폴더 만들 수 있는 권한
		$this->public_del_folder			= true;			//방문자가 폴더 삭제할 수 있는 권한
		$this->public_upload_file			= true;			//방문자가 파일 업로드 할 수 있는 권한
		$this->public_delete_file			= true;			//방문자가 파일 삭제할 수 있는 권한

		//member dir
		$this->use_member_dir				= false;			//회원전용공간 사용(true면 $_SESSION['SE_MEMBER_ID']값으로 디렉토리 자동 생성후 그 안의 자료만 이용

		// thumb
		$this->thumb_max_size				= 100;				//썸네일 생성시 가로, 세로 중 최대값
		$this->allow_image_max_width		= 960;				//원본 이미지 등록시 가로 사이즈 제한 픽셀제한 (0:무제한) => 제한픽셀을 넘는 이미지를 등록하면 자동으로 제한사이즈로 조절되어 저장됨
		$this->allow_image_max_volume		= 1048576;			// 업로드제한용량(byte) (0: 무제한 => 무제한으로 세팅해도 서버의 설정에따라 제한이 걸릴 수 있습니다.)
		$this->allow_folder_count			= 10;				// 최대 생성 폴더 개수 (0: 무제한)
		$this->allow_file_count				= 100;				// 1개폴더당 최대 업로드 파일 개수 (0: 무제한)
		$this->overwrite					= false; 
		$this->root_name					= "ROOT";

		$this->setLocalSession();
		$this->setMemberDirectory();

	}
	function setLocalSession() {
		if(isset($_SESSION['SE_LOGIN']) && $_SESSION['SE_LOGIN']) {
			$this->islogin = true;
		} else {
			$this->islogin = false;
		}
		$this->se_member_id = $_SESSION['SE_MEMBER_ID'];
	}
	function setMemberDirectory() {
		if($this->use_member_dir == true) {
			if($this->islogin == false || !$this->se_member_id) {
				$this->accessDeny();
				exit;
			}
			$this->upload_web_path			.= '/__'.$this->se_member_id;
			$this->upload_abs_path			.= '/__'.$this->se_member_id;			
			if(!is_dir($this->upload_abs_path)) {
				if(!mkdir($this->upload_abs_path,0707)) {
					$this->ErrorBackMsg("유저폴더생성에 실패했습니다. 다시한번 시도해주세요");
					exit;
				}
				chmod($this->upload_abs_path,0707);
			}
			$this->root_name				= strtoupper($this->se_member_id);
		}		
	}
	function checkAuth() {
		if($this->use_auth == true) {
			if($this->islogin == false) {
				$this->accessDeny();
				exit;
			}
		}		
	}
	function setMemberVars() {
		$this->action		= $_GET['action'];
		$this->get_dir		= $_GET['get_dir'];
		if(isset($_POST['action']) && $_POST['action'])		$this->action	= $_POST['action'];
		if(isset($_POST['get_dir']) && $_POST['get_dir'])	$this->get_dir	= $_POST['get_dir'];
		$this->get_dir			= trim(addslashes($this->get_dir));
		if($this->get_dir) {
			if(!$this->checkFolderNaming($this->get_dir)) {
				$this->ErrorBackMsg("잘못된 폴더명입니다.");
				exit;
			}
		}
	}
	function checkAction() {
		if($this->action == 'file_delete')		$this->deleteFile();
		if($this->action == 'file_upload')		$this->uploadFile();
		if($this->action == 'make_folder')		$this->makeFolder();
		if($this->action == 'delete_folder')	$this->deleteFolder();
		if($this->action == 'file_delete_one')	$this->deleteFileOne();
	}
	function getTree() {
		$this->folder_count = 0;
		if ($handle = opendir($this->upload_abs_path)) {
			while (false !== ($dir_name = readdir($handle))) {
				if($dir_name != '.' && $dir_name != '..' && is_dir($this->upload_abs_path."/".$dir_name) && !preg_match('/^_/',$dir_name)) {
					$this->upload_dir_array[]	= $dir_name;
					$this->folder_count++;
				}
			}
			closedir($handle); 
			$handle = null;
			asort($this->upload_dir_array);
			$tmp = $this->upload_dir_array;
			$this->last_dir_name		= array_pop($tmp);
			unset($tmp);
		}
	}
	function getCurrFiles() {
		$this->setCurrPath();
		$this->upload_file_array		= array();
		$this->file_count = 0;
		if(is_dir(($this->curr_upload_abs_path))) {
			if ($handle = opendir($this->curr_upload_abs_path)) {
				while (false !== ($file_name = readdir($handle))) {
					if($file_name != '.' && $file_name != '..' && !is_dir($this->curr_upload_abs_path."/".$file_name)) {
						$imgObj														= getimagesize($this->curr_upload_abs_path."/".$file_name);
						$file_size													= filesize($this->curr_upload_abs_path."/".$file_name);
						$this->upload_file_array[$this->file_count]['name']			= $file_name;
						$this->upload_file_array[$this->file_count]['width']		= $imgObj[0];
						$this->upload_file_array[$this->file_count]['height']		= $imgObj[1];
						$this->upload_file_array[$this->file_count]['size']			= $this->format_size($file_size);
						$this->upload_file_array[$this->file_count]['file_url']		= $this->curr_upload_web_path.'/'.$file_name;

						$thumb_name													= 't_'.$file_name;
						$thumbObj													= getimagesize($this->curr_upload_thumb_abs_path.'/'.$thumb_name);
						$this->upload_file_array[$this->file_count]['thumb_name']	= $thumb_name;
						$this->upload_file_array[$this->file_count]['thumb_width']	= $thumbObj[0];
						$this->upload_file_array[$this->file_count]['thumb_height']	= $thumbObj[1];
						$this->upload_file_array[$this->file_count]['thumb_url']	= $this->curr_upload_thumb_web_path.'/'.$thumb_name;						

						$this->file_count++;
					}					
				}
				closedir($handle);
				//asort($this->upload_file_array);
			}
		}
	}
	function format_size($size) {
		$sizes = array(" Bytes", " KB", " MB", " GB", " TB", " PB", " EB", " ZB", " YB");
		if ($size == 0) { return('n/a'); } else {
		return (round($size/pow(1024, ($i = floor(log($size, 1024)))), 2) . $sizes[$i]); }
	}
	function setCurrPath() {
		$this->setMemberVars();
		if($this->get_dir) {
			$this->curr_upload_web_path			= $this->upload_web_path."/".$this->get_dir;
			$this->curr_upload_abs_path			= $this->upload_abs_path."/".$this->get_dir;
		} else {
			$this->curr_upload_web_path			= $this->upload_web_path;
			$this->curr_upload_abs_path			= $this->upload_abs_path;
		}		
		if(!is_dir($this->curr_upload_abs_path)) {
			$this->ErrorBackMsg("잘못된 폴더명입니다.");
			exit;
		}
		$this->curr_upload_thumb_web_path		= $this->curr_upload_web_path."/_thumb";
		$this->curr_upload_thumb_abs_path		= $this->curr_upload_abs_path."/_thumb";
	}
	function resizeString($Str, $size, $addStr="...")  { 
	    if(mb_strlen($Str, "UTF-8") > $size) return mb_substr($Str, 0, $size, "UTF-8").$addStr; 
	    else return $Str; 
	}	
	function deleteFile() {	
		$this->checkDeleteFileAuth();
		$this->chk_files	= $_POST['chk_file'];
		$this->setCurrPath();
		foreach($this->chk_files as $files) {
			@unlink($this->curr_upload_abs_path."/".$files);
			@unlink($this->curr_upload_thumb_abs_path."/t_".$files);
		}
		$this->goListPage();
	}
	function deleteFileOne() {	
		$this->checkDeleteFileAuth();
		$this->setCurrPath();
		$this->filename	= $_GET['filename'];
		@unlink($this->curr_upload_abs_path."/".$this->filename);
		@unlink($this->curr_upload_thumb_abs_path."/t_".$this->filename);
	}
	function checkUploadFileVolume() {
		if(!$this->upfile_size) return false;
		if($this->allow_image_max_volume > 0) {
			if($this->upfile_size > $this->allow_image_max_volume) {
				@unlink($this->upfile);
				$this->ErrorBackMsg("파일용량이 ".$this->format_size($this->allow_image_max_volume)." 를 초과하였습니다. ".$this->format_size($this->allow_image_max_volume)." 이하의 파일만 업로드 해주세요!");
				exit;
			}
		}
	}
	function uploadFile() {		
		$this->checkUploadFileAuth();
		$this->setCurrPath();
		$this->checkFileCount();
		$this->upfile		= $_FILES['upfile']['tmp_name'];
		$this->upfile_name	= $_FILES['upfile']['name'];
		$this->upfile_size	= $_FILES['upfile']['size'];
		$this->upfile_path	= $this->curr_upload_abs_path."/".$this->upfile_name;
		$this->checkUploadFileVolume();
		if($this->overwrite == false) {
			if(file_exists($this->upfile_path)) {
				$upfile_name_array	= explode(".",$this->upfile_name);
				$upfile_ext			= array_pop($upfile_name_array);
				$upfile_name_str	= $upfile_name_array[0];
				$loop_count = 5;
				for($i = 0; $i < $loop_count; $i++) {
					$upfile_name_str	= $upfile_name_str."_1";
					$this->upfile_path		= $this->curr_upload_abs_path."/".$upfile_name_str.".".$upfile_ext;
					if(!file_exists($this->upfile_path) || $i  == ($loop_count-1)) {
						$this->upfile_name = $upfile_name_str.".".$upfile_ext;
						break;
					}
				}
			}
		}
		if(is_uploaded_file($this->upfile)) {
			if($this->allow_image_max_width > 0) {
				$imageObj = getimagesize($this->upfile);
				if($imageObj[2] != 1 && $imageObj[2] != 2 && $imageObj[2] != 3) {
					$this->ErrorBackMsg("GIF, JPG, PNG 이미지파일만 업로드 가능합니다.");	
					exit;
				}
				if($imageObj[2] == 1) {
					$im = imagecreatefromgif($this->upfile); 
				} elseif($imageObj[2] == 2) {
					$im = imagecreatefromjpeg($this->upfile); 
				} elseif($imageObj[2] == 3) {
					$im = imagecreatefrompng($this->upfile); 
				}
				if($imageObj[0] > $this->allow_image_max_width) {
					$width = $this->allow_image_max_width; 
					$height = ($imageObj[1]*$this->allow_image_max_width) / $imageObj[0];
					$sheet = imagecreatetruecolor($width, $height); 
					imagecopyresampled($sheet, $im, 0, 0, 0, 0, $width, $height, $imageObj[0], $imageObj[1]); 
					if($imageObj[2] == 1) {
						imagegif($sheet,$this->upfile_path, 90);
					} elseif($imageObj[2] == 2)	{
						imagejpeg($sheet,$this->upfile_path, 90);
					} elseif($imageObj[2] == 3) {
						imagepng($sheet,$this->upfile_path, 9);
					}
					imagedestroy($sheet); 
				} else {
					if(!move_uploaded_file($this->upfile,$this->upfile_path)) {
						echo "업로드 실패!";
						exit;
					}
					@chmod($this->upfile_path,0707);
				}
			} else {
				if(!move_uploaded_file($this->upfile,$this->upfile_path)) {
					echo "업로드 실패!";
					exit;
				}
				@chmod($this->upfile_path,0707);
			}
			$this->checkThumbPath();
			$this->thumbnail($this->upfile_path, $this->thumb_path.'/t_'.$this->upfile_name);
			@unlink($this->upfile);
		}
		$this->goListPage();
		exit;
	}
	function checkThumbPath() {
		$this->thumb_path = $this->curr_upload_abs_path.'/_thumb';
		if(!is_dir($this->thumb_path)) {
			mkdir($this->thumb_path,0707);
			chmod($this->thumb_path,0707);
		}
	}
	function thumbnail($file_path, $save_path,$max_size = 0){ 
		if($max_size > 0) {
			$this->thumb_max_size = $max_size;
		}
		$imageObj = getimagesize($file_path);
		if($imageObj[2] != 1 && $imageObj[2] != 2 && $imageObj[2] != 3) {
			$this->ErrorBackMsg("GIF, JPG, PNG 이미지파일만 업로드 가능합니다.");	
			exit;
		}
		if($imageObj[2] == 1) {
			$im = imagecreatefromgif($file_path); 
		} elseif($imageObj[2] == 2) {
			$im = imagecreatefromjpeg($file_path); 
		} elseif($imageObj[2] == 3) {
			$im = imagecreatefrompng($file_path); 
		}
		if($imageObj[0] <= $this->thumb_max_size && $imageObj[1] <= $this->thumb_max_size){ 
			$width	= $imageObj[0]; 
			$height = $imageObj[1]; 
		}
		if($imageObj[0] > $imageObj[1]){ 
			$width	= $this->thumb_max_size; 
			$height	= ($imageObj[1]*$this->thumb_max_size) / $imageObj[0]; 
		}
		if($imageObj[0] < $imageObj[1]){ 
			$width	= ($imageObj[0]*$this->thumb_max_size) / $imageObj[1]; 
			$height = $this->thumb_max_size; 
		}
		if($imageObj[0] == $imageObj[1]){ 
			$width	= $this->thumb_max_size;
			$height = $this->thumb_max_size;
		}
		$sheet = imagecreatetruecolor($width, $height); 
		imagecopyresampled($sheet, $im, 0, 0, 0, 0, $width, $height, $imageObj[0], $imageObj[1]); 
		if($imageObj[2] == 1) {
			imagegif($sheet,$save_path, 90);
		} elseif($imageObj[2] == 2)	{
			imagejpeg($sheet,$save_path, 90);
		} elseif($imageObj[2] == 3) {
			imagepng($sheet,$save_path, 9);
		}
		imagedestroy($sheet); 
		return true;
	}
	function checkFolderCount() {
		if($this->allow_folder_count > 0) {
			$this->folder_count = 0;
			if ($handle = opendir($this->upload_abs_path)) {
				while (false !== ($dir_name = readdir($handle))) {
					if($dir_name != '.' && $dir_name != '..' && is_dir($this->upload_abs_path."/".$dir_name) && !preg_match('/^_/',$dir_name)) {
						$this->upload_dir_array[]	= $dir_name;
						$this->folder_count++;
					}
				}
				closedir($handle); 
			}
			if($this->folder_count >= $this->allow_folder_count) {
				$this->ErrorBackMsg("더이상 폴더를 생성할 수 없습니다. (가능최대폴더개수:".$this->allow_folder_count.")");
				exit;
			}
		}
	}
	function checkFileCount() {
		if($this->allow_file_count > 0) {
			$this->setCurrPath();
			$this->file_count = 0;
			if(is_dir(($this->curr_upload_abs_path))) {
				if ($handle = opendir($this->curr_upload_abs_path)) {
					while (false !== ($file_name = readdir($handle))) {
						if($file_name != '.' && $file_name != '..' && !is_dir($this->curr_upload_abs_path."/".$file_name)) {
							$this->file_count++;
						}					
					}
					closedir($handle); 
				}
			}
			if($this->file_count >= $this->allow_file_count) {
				$this->ErrorBackMsg("더이상 파일을 등록할 수 수 없습니다. (폴더당최대파일개수:".$this->allow_file_count.")");
				exit;
			}
		}
	}
	function checkMakeFolderAuth() {
		if($this->islogin == false) {
			if($this->use_auth == false) {
				if($this->public_add_folder == false) {
					$this->ErrorBackMsg("폴더생성권한이 없습니다.");
					exit;
				}
			}
		}
	}
	function checkDeleteFolderAuth() {
		if($this->islogin == false) {
			if($this->use_auth == false) {
				if($this->public_del_folder == false) {
					$this->ErrorBackMsg("폴더삭제권한이 없습니다.");
					exit;
				}
			}
		}
	}
	function checkUploadFileAuth() {
		if($this->islogin == false) {
			if($this->use_auth == false) {
				if($this->public_upload_file == false) {
					$this->ErrorBackMsg("파일 업로드권한이 없습니다.");
					exit;
				}
			}
		}
	}
	function checkDeleteFileAuth() {
		if($this->islogin == false) {
			if($this->use_auth == false) {
				if($this->public_delete_file == false) {
					$this->ErrorBackMsg("파일 삭제권한이 없습니다.");
					exit;
				}
			}
		}
	}
	function checkFolderNaming($folder_name) {
		if(!preg_match('/^[a-zA-Z][a-zA-Z0-9_]{1,9}$/', $folder_name)) {
			return false;;
		} else {
			return true;
		}
	}
	function makeFolder() {
		$this->checkMakeFolderAuth();
		$this->checkFolderCount();
		$this->new_dir		= $_POST['new_dir'];
		if(!$this->checkFolderNaming($this->new_dir)) {
			$this->ErrorBackMsg("폴더명은 알파벳으로 시작하는 알파벳,숫자,언더바(_)만으로된 2~10글자를 입력해주세요");
			exit;
		}
		$this->setCurrPath();
		$this->new_path		= $this->upload_abs_path."/".$this->new_dir;
		if(is_dir($this->new_path)) {
			$this->ErrorBackMsg("이미 등록된 폴더명입니다. 다른 이름을 지정해주세요");
			exit;
		}
		if(!mkdir($this->new_path,0707)) {
			$this->ErrorBackMsg("폴더생성에 실패했습니다. 다시한번 시도해주세요");
			exit;
		}
		@chmod($this->new_path,0707);
		$this->get_dir = $this->new_dir;
		$this->goListPage();
		exit;
	}
	function deleteFolder() {
		$this->checkDeleteFolderAuth();
		$this->setCurrPath();
		if(!is_dir($this->curr_upload_abs_path)) {
			$this->ErrorBackMsg("없는 폴더입니다. 다시한번 확인해주세요");
			exit;
		}
		if(is_dir($this->curr_upload_thumb_abs_path)) {
			if ($handle = opendir($this->curr_upload_thumb_abs_path)) {
				while (false !== ($file_name = readdir($handle))) {
					if($file_name != '.' && $file_name != '..') {
						if(is_dir($this->curr_upload_thumb_abs_path.'/'.$file_name)) {
							@rmdir($this->curr_upload_thumb_abs_path.'/'.$file_name);
						} else {
							@unlink($this->curr_upload_thumb_abs_path.'/'.$file_name);	
						}
					}					
				}
				closedir($handle); 
				@rmdir($this->curr_upload_thumb_abs_path);
			}
		}
		if ($handle = opendir($this->curr_upload_abs_path)) {
			while (false !== ($file_name = readdir($handle))) {
				if($file_name != '.' && $file_name != '..') {
					@unlink($this->curr_upload_abs_path.'/'.$file_name);
				}					
			}
			closedir($handle); 
			@rmdir($this->curr_upload_abs_path);
		}
		$this->get_dir = '';
		$this->goListPage();
		exit;		
	}
	function goListPage() {
		$this->redirect("?get_dir=".$this->get_dir."&uploaded_file=".$this->upfile_name);exit;
	}
	function redirect($url) {
		echo "<meta http-equiv=\"Refresh\" content=\"0; url=" . $url . "\">";exit;
	}
	function ErrorBackMsg($msg) {
		echo("
			<html><head><title>$msg</title><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><script>function Error() {alert('".$msg."');document.location.href='/Spac_Editor/spac_editor_file_tree.php';}</script></head><body onLoad='Error()'><!-- Error Page...--></body></html>");exit;
	}
	function accessDeny() {		
		echo("<html><head><title>$msg</title><meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head><body><div style='width:auto;text-align:center;margin-top:50px;color:red;font-size:15px'>접근권한이 없습니다.</div></body></html>");
		exit;
	}
}
$SpacEditor = new SpacEditor;
?>