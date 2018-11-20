<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="Board22.Layer.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" profile="http://www.w3.org/2005/10/profile" link rel="icon" type="image/png" href="http://example.com/myicon.png">
    <title></title>
    <link href="/css/common.css" rel="stylesheet" type="text/css"/>
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript">
         $(function () {
  	        //alert(parent);
  	        if (parent) {
  	            //parent.document.getElementById("<%=ReturnId %>").value = document.getElementById("hdfAttachFile").value;
  	            //parent.document.getElementById("divTest").innerHTML = document.getElementById("hdfAttachFile").value;
  	        }
  	    });
  	    function AddAttachFile() {
  	        var oAttach = document.getElementById("fupAttachFile");

  	        if (oAttach.value == "") {
  	            alert("첨부할 파일을 선택하세요!");
  	        }
  	        else {
  	            __doPostBack('lnbAddAttachFile', '');
  	        }  	        
  	    }
  	    function DelSelectedFile() {
  	        var oList = document.getElementById("lstAttachFile");

  	        if (oList.options.length == 0) {
  	            alert("삭제할 파일이 없습니다.");
  	        }
  	        else {
  	            var idx = oList.options.selectedIndex;

  	            if (idx == -1)
  	                alert("삭제할 파일을 선택해 주세요.");
  	            else
  	                __doPostBack('lnbDelAttachFile', '');
  	        }
  	        
         }
    </script>
    <style>
        .btn_sml1{display:inline-block;padding-right:4px;background:url(../images/bg_btn_default.gif) no-repeat 100% -27px;font-family:'맑은 고딕',nanumgothic,'돋움',dotum;font-size:11px;color:#333;line-height:21px;letter-spacing:-1px;word-spacing:-1px;text-decoration:none !important;white-space:nowrap}
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <asp:HiddenField ID="hdfAttachFile" runat="server"/>
        <asp:FileUpload ID="fupAttachFile" runat="server"  Width="70%" style="font-size:12px;"   />
         <a class="btn_sml1" href="javascript:AddAttachFile()"><span>추가</span></a>						
	     <a class="btn_sml1" href="javascript:DelSelectedFile()"><span>삭제</span></a>
    </div>

     <div>
	    <span class="box"><asp:ListBox id="lstAttachFile" runat="server" Width="70%" /></span>
		<span class="detail" style="display:inline-block; vertical-align:bottom;">
			개수 : <span id="spanFileCount" runat="server">0</span>
			<br />
			크기 : <span id="spanFileSize" runat="server">0</span>
		</span>
		<asp:LinkButton ID="lnbAddAttachFile" runat="server" Width="0" Height="0" OnClick="lnbAddAttachFile_Click"></asp:LinkButton>
		<asp:LinkButton ID="lnbDelAttachFile" runat="server" Width="0" Height="0" OnClick="lnbDelAttachFile_Click"></asp:LinkButton>
	</div>
    
    </form>
</body>
</html>
