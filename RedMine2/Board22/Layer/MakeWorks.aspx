<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeWorks.aspx.cs" Inherits="Board22.Layer.MakeWorks" ValidateRequest="false"  %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc1" %>
<!DOCTYPE html>

<style>
    .box {
    padding: 6px;
    margin-bottom: 10px;
    background-color: #f6f6f6;
    color: #505050;
    line-height: 1.5em;
    border: 1px solid #e4e4e4;
}
    .auto-style9 {
        width: 950px;
    }
    .btn_big{

    padding: 0 8px 1px 12px;
    background: url(/images/bg_btn_default.gif) no-repeat;
    }
    a.btn_big {
    color: #333;
    line-height: 26px;
    word-spacing: -1px;
    text-decoration: none !important;
    white-space: nowrap;
    align-items: flex-start;
    text-align: center;
    cursor: default;
    color: buttontext;
    background-color: buttonface;
    box-sizing: border-box;
    padding: 2px 6px 3px;
    border-width: 2px;
    border-style: outset;
    border-color: buttonface;
    border-image: initial;
    font-weight:100;
}

    .tabular label {
    font-weight: bold;
    float: left;
    text-align: right;
    margin-left: -180px;
    width: 175px;
}
    body {
    font-size: 13px;
    color: #484848;
    margin: 0;
    padding: 0;
    min-width: 900px;
    font-weight: bold;
}
    .auto-style13 {
        width: 20px;
        height: 26px;
    }
    .auto-style19 {
        height: 50px;
    }
    .auto-style21 {
        height: 25px;
    }
    .text{
        text-align:right;
    }
    .auto-style23 {
        width: 1000px;
        height: 293px;
    }
    .auto-style24 {
        width: 350px;
    }
    .auto-style25 {
        height: 25px;
        width: 350px;
    }
    .auto-style26 {
        height: 26px;
    }
    .auto-style27 {
        width: 350px;
        height: 26px;
    }

    .autocomplete {
    background: #fff url(../images/112.png) no-repeat 2px 50%;
    padding-left: 20px !important;
    border: 1px solid #9EB1C2;
    border-radius: 2px;
    height: 1.5em;
}

    img.ui-datepicker-trigger {
    cursor: pointer;
    vertical-align: middle;
    margin-left: 4px;
}

    
element.style {
    outline: 0px;
    z-index: 1002;
    height: auto;
    width: 600px;
    top: 324px;
    left: 588.5px;
    display: block;
}
div.modal {
    border-radius: 5px;
    background: #fff;
    z-index: 50;
    padding: 4px;
}
.ui-widget-header1 {
    border: 1px solid #628db6;
    color: #fff;
    font-weight: bold;
    background-color:#759fcf;
   padding: .4em 1em;
}
.search_for_watchers, span.add_attachment {
    font-size: 80%;
    line-height: 2.5em;
}
.autocomplete {
    background: #fff url(../images/112.png) no-repeat 2px 50%;
    padding-left: 20px !important;
    border: 1px solid #9EB1C2;
    border-radius: 2px;
    height: 1.5em;
}
#users_for_watcher {
    height: 200px;
    overflow: auto;
}

.w3-modal1{z-index:3;display:none;padding-top:100px;position:fixed;left:0;top:0;width:100%;height:100%;
           overflow:auto;background-color:rgb(0,0,0);background-color:rgba(0,0,0,0.4);}

       .btn_sml1{display:inline-block;padding-right:4px;background:url(../images/bg_btn_default.gif) no-repeat 100% -27px;font-family:'맑은 고딕',nanumgothic,'돋움',dotum;font-size:11px;color:#333;line-height:21px;letter-spacing:-1px;word-spacing:-1px;text-decoration:none !important;white-space:nowrap}
    </style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>
<uc1:Top ID="Top" runat="server" />
   <%--제이커리--%>
    <script src="/Scripts/jquery-1.5.1-vsdoc.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <link href="/Utils/jquery-ui-1.8.21.smoothness/css/smoothness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Utils/jquery-ui-1.8.21.smoothness/js/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>

    <%--데이터피커--%>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<%--    <script class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.0/jquery.min.js"></script>--%>
    <%--SmartEditor 항상 jQuery뒤에순서중요!--%>
    <script src="/Utils/smarteditor2.9/js/service/HuskyEZCreator.js"></script>
   <script type="text/javascript" src="MakeWorks.js"></script>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
    <script type="text/javascript">
        $(function () {
      	    $("#biz_start_dt").val("<%=DateTime.Now.ToShortDateString()%>");
			$("#biz_start_dt").attr("readonly", "readonly");
			$("#biz_start_dt").datepicker();
			$("#biz_end_dt").val("<%=DateTime.Now.ToShortDateString()%>");
			$("#biz_end_dt").attr("readonly", "readonly");
            $("#biz_end_dt").datepicker();
            $("#modaldatepicker").val("<%=DateTime.Now.ToShortDateString()%>");
			$("#modaldatepicker").attr("readonly", "readonly");
            $("#modaldatepicker").datepicker();
            var appendTo = $(".selector").autocomplete("option", "appendTo");
            var autoFocus = $( ".selector" ).autocomplete( "option", { disabled: true });
            var autocomplete_text = ["자동완성기능", "Autocomplete", "개발로짜", "국이", "개자식"];
             var selectedItems= [];
	
        //var availableTags = [
        //               "ActionScript",
        //               "AppleScript",
        //    ];

         <%-- //  var availableTags =  <%= WorkManagerarray%>;--%>

     
             var availableTags =<%= sJSON%>;

<%--            var propertyArray = new Array();
            propertyArray =<%=WorkManagerarray%>;--%>

           // var availableTags = JSON.parse('@Html.Raw(Json.Encode(<%=WorkManagerarray%>))');

              $('#autocomplete').autocomplete({
            source: availableTags,
            minLength: 0,
            select:function(event, ui) {
                                
                selectedItems.push(ui.item.label);
              }
        }).focus(function () {
            try {
                $(this).autocomplete("search");
            }
            catch (e) {

            }
        })
        //.data( "autocomplete" )._renderItem = function( ul, item ) {
        //       var checked = ($.inArray(item.label, selectedItems) >= 0 ? 'checked' : '');
        //return $( "<li></li>" )
        //    .data( "item.autocomplete", item )
        //    .append( '<a><input type="checkbox"' + checked + '/>' + item.label + '</a>' )
        //    .appendTo( ul );
        //        }

        });

        function myFunction() {
            document.getElementById('id01').style.display = 'block';
        }
         function AddManager() {
            document.getElementById('id02').style.display = 'block';
        }

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
</head>
<body>
    <form id="form1" runat="server">
      <%--    <uc1:Top ID="Top" runat="server" />--%>
        <h2>&nbsp;새 일감만들기</h2>
        <%--  modal2--%>
   <div id="id02" class="w3-modal1">
    <div  id="id03" class="w3-modal-content w3-card-4 w3-animate-zoom" style="max-width:600px">
      <div class="w3-center"><br>
     <div class="ui-dialog-titlebar ui-widget-header1 ui-corner-all ui-helper-clearfix" style="text-align:left; height:60px">일감관람자 추가
             <span onclick="document.getElementById('id02').style.display='none'" class="w3-button w3-xlarge w3-hover-red" style="height:50px; width:100px; margin-left: 300px;" title="Close Modal">&times;</span></div>
      </div>
        <div class="w3-section" id="id04">
          
                사용자 찾기::<br />  <%-- jquery 자동완성 참조--%>
                <input type="text" class="autocomplete" id="autocomplete" onfocus="true"  runat="server" /><br />
        </div>
      <div class="w3-container w3-border-top w3-padding-16 w3-light-grey">
       <%-- <button onclick="document.getElementById('id02').style.display='none'" type="button"  style="margin-left:300px;">추가</button>--%>
       <%-- <asp:Button ID="button1" runat="server" Width="0" Height="0" OnClick="addworkmanager" type="button" Text="추가" ></asp:Button>--%>
          <asp:Button ID="button1" runat="server" OnClick="addworkmanager" type="button" Text="추가" />
          <asp:Button ID="Button2" runat="server" Text="추가하고 계속하기" />
          <asp:Button ID="Button3" runat="server" Text="취소" />
<%--          <asp:Button ID="button1" runat="server" Width="0" Height="0" OnClick="addworkmanager" type="button" Text="추가"/ >--%>
<%--        <asp:Button ID="button2" runat="server" Width="0" Height="0"  type="button" >추가하고 계속하기</asp:Button>--%>
<%--        <button onclick="document.getElementById('id02').style.display='none'" type="button" >추가하고 계속하기</button>--%>
     <%--   <button onclick="document.getElementById('id02').style.display='none'" type="button" >취소</button>--%>
      </div>
    </div>
  </div>

     
        <div class="box tabular" style="margin-left:130px">
                <table>
                    <tr>
                        <td class="auto-style21 text">유형</td>
                        <td class="auto-style21"> <asp:Label ID="Label1" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label></td>
                        <td >  <asp:DropDownList ID="WC_State" runat="server">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="버그" Selected="True">버그</asp:ListItem>							
						<asp:ListItem Value="업무">업무</asp:ListItem>
						<asp:ListItem Value="건의">건의</asp:ListItem>
                      	<asp:ListItem Value="기능">기능</asp:ListItem>
                      	<asp:ListItem Value="지원">지원</asp:ListItem>
                      	<asp:ListItem Value="잡일">잡일</asp:ListItem>
                      	<asp:ListItem Value="운영관리">운영관리</asp:ListItem>
                      	<asp:ListItem Value="변경점">변경점</asp:ListItem>
                      	<asp:ListItem Value="자산">자산</asp:ListItem>
                      	<asp:ListItem Value="크래시">크래시</asp:ListItem>
                  </asp:DropDownList> </td>
                    </tr>
                      <tr>
                        <td class="auto-style21 text" > 제목</td>
                    <td class="auto-style21">  <asp:Label ID="WC_Name" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> </td>
                        <td > <input id="txtTitle" runat="server" type="text" class="auto-style9" /></td>
                    </tr>
                     </table>
           
                          <span class="text">설명</span>
                
                              <asp:HiddenField ID="biz_MakeWork" runat="server" />
					<textarea name="ir_MakeWork"  id="ir_MakeWork" class="auto-style23"></textarea>
               
            <table>
                    
                <tr>
                    <td class="auto-style26" style="text-align:right">상태</td> 
                    <td class="auto-style13"><asp:Label ID="Label2" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label></td>
                     <td class="auto-style26"> <asp:DropDownList ID="DropDownList2" runat="server"  Width="350px" Height="25px">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="새 일감" Selected="True">새 일감</asp:ListItem>							
						<asp:ListItem Value="진행중">진행중</asp:ListItem>
						<asp:ListItem Value="확인 요망">확인 요망</asp:ListItem>
                      	<asp:ListItem Value="QA중">QA중</asp:ListItem>
                      	<asp:ListItem Value="완료">완료</asp:ListItem>
                      	<asp:ListItem Value="재발생">재발생</asp:ListItem>
                      	<asp:ListItem Value="지속">지속</asp:ListItem>
                      	<asp:ListItem Value="폐기">폐기</asp:ListItem>
                      	<asp:ListItem Value="보류">보류</asp:ListItem>
                  </asp:DropDownList>
                <td class="auto-style27"></td>
                    <td style="width:80px">상위일감 </td>
                         <td><asp:TextBox ID="issue_parent_issue_id" class="autocomplete" Width="120px" runat="server"></asp:TextBox> </td>
                </tr>
                <tr>
                    <td class="auto-style21 text">우선순위</td>
                    <td class="auto-style21"><asp:Label ID="Label5" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label></td>
                    <td class="auto-style21"> <asp:DropDownList ID="WC_Priority" runat="server" Width="350px" Height="25px">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
                      	<asp:ListItem Value="낮음">낮음</asp:ListItem>
						<asp:ListItem Value="보통" Selected="True">보통</asp:ListItem>							
						<asp:ListItem Value="중요">중요</asp:ListItem>
						<asp:ListItem Value="높음">높음</asp:ListItem>
                      	<asp:ListItem Value="긴급">긴급</asp:ListItem>
                  </asp:DropDownList></td>
                       <td class="auto-style24"></td>
                    <td style="width:20px">시작시간 </td>
                         <td>
                   <asp:TextBox ID="biz_start_dt" Width="100px" runat="server"></asp:TextBox>
                             <img class="ui-datepicker-trigger" src="../images/113.png" />
                         </td>
                </tr>
                <tr>
                      <td class="auto-style21 text">담당자</td>
                <td class="auto-style21"> <asp:Label ID="Label7" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label></td>
                <td class="auto-style21">    <asp:TextBox ID="DL_Manager" Width="350px" runat="server" Height="25px" ></asp:TextBox></td>
                     <td class="auto-style24"></td>
                    <td style="width:20px">완료기한 </td>
                         <td>
                        <asp:TextBox ID="biz_end_dt" Width="100px" runat="server"></asp:TextBox>
                         <img class="ui-datepicker-trigger" src="../images/113.png" /></td>
                </tr>
                <tr>
                 <td class="auto-style21 text">목표버전</td>
                    <td class="auto-style21"><asp:Label ID="Label8" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label></td>
                    <td class="auto-style21">  <asp:DropDownList ID="DL_Version" runat="server" Width="350px" Height="25px">
                	<asp:ListItem Value="">--선택--</asp:ListItem>			
                  </asp:DropDownList>  <a href="javascript:myFunction()">  <img style="vertical-align: top;" src="../images/115.png"  /></a>  </td>
                        <td class="auto-style24"></td>
                    <td style="width:20px">추정시간 </td>
                         <td><asp:TextBox ID="NeedTime" Width="75px" runat="server"></asp:TextBox>  시간</td>
                </tr>
                 <tr>
                    <td class="auto-style21"></td>
                    <td class="auto-style21"></td>
                    <td class="auto-style21"></td>
                   <td class="auto-style24"></td>
                    <td style="width:20px">진척도 </td>
                         <td><asp:DropDownList ID="WC_Progress" runat="server" width="90">
                	<asp:ListItem Value="0" Selected="True">0%</asp:ListItem>
                      	<asp:ListItem Value="10">10%</asp:ListItem>
						<asp:ListItem Value="20">20%</asp:ListItem>							
						<asp:ListItem Value="30">30%</asp:ListItem>
						<asp:ListItem Value="40">40%</asp:ListItem>
                      	<asp:ListItem Value="50">50%</asp:ListItem>
                        <asp:ListItem Value="60">60%</asp:ListItem>
                   	    <asp:ListItem Value="70">70%</asp:ListItem>
                   	    <asp:ListItem Value="80">80%</asp:ListItem>
                   	    <asp:ListItem Value="90">90%</asp:ListItem>
                   	    <asp:ListItem Value="100">100%</asp:ListItem>
                  </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style21 text">파일</td>
                    <td class="auto-style21"></td>
                    <td class="auto-style21"> 
                    <%--    <asp:FileUpload ID="FileUpload1" runat="server" Height="19px" AllowMultiple="true" />--%>
                        <!--첨부파일-->
             


	<%--			<iframe id="ifmUpload"
					src="FileUpload.aspx"
					width="100%" height="100px"
					frameborder=0></iframe>--%>

                            <div>
   <asp:HiddenField ID="hdfAttachFile" runat="server" />
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


                    <span style="font-size:10px">(최대 크기: 100 MB)</span></td>
                    <td class="auto-style25"> </td>
                </tr>
                <tr>
                    <td class="auto-style19 text">일감관람자</td><td class="auto-style19"></td>
                    <td class="auto-style19 search_for_watchers"><a  href="javascript:AddManager()">    <img  src="../images/114.png" />추가할만한 일감 관람자 선택</a></td>
                </tr>
            </table>
            </div>


      <%--  modal--%>
        <div id="id01" class="w3-modal">
    <div class="w3-modal-content w3-card-4 w3-animate-zoom" style="max-width:600px">
      <div class="w3-center"><br>
     <div class="ui-dialog-titlebar ui-widget-header1 ui-corner-all ui-helper-clearfix" style="text-align:left; height:60px">새 버전&nbsp;
             <span onclick="document.getElementById('id01').style.display='none'" class="w3-button w3-xlarge w3-hover-red" style="height:50px; width:100px; margin-left: 400px;" title="Close Modal">&times;</span></div>
      </div>
        <div class="w3-section">
          <table>
                <tr>
                    <td style="text-align:right;" ><label><b>이름</b><label style="color:red;">＊</label></label></td>
                    <td ><input type="text" id="versionname" runat="server" ></td>
                </tr>
                 <tr>  
                    <td style="text-align:right;"><label><b>설명</b></label></td>
                  <td style="text-align:right">  <input type="text" id="modalexplnation" runat="server"  ></td>
                </tr>
                  <tr>
                   <td style="text-align:right;"><label><b>상태</b></label></td>
                   <td style="text-align:left">  
                       <asp:DropDownList ID="DropDownList1" runat="server" Height="26px">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="진행" Selected="True">진행</asp:ListItem>							
						<asp:ListItem Value="잠김">잠김</asp:ListItem>
                        <asp:ListItem Value="닫힘">닫힘</asp:ListItem>
                  </asp:DropDownList> 
                   </td>
                </tr>
                 <tr>
                    <td style="text-align:right;"><label><b>위키 페이지</b></label></td>
                      <td style="text-align:right">  <input type="text" id="modalwikipage"  runat="server" ></td>
                </tr>
                 <tr>
                   <td style="text-align:right;"><label><b>날짜</b></label></td>
                        <td>   <asp:TextBox ID="modaldatepicker" style="text-align:right" Width="100px" runat="server"></asp:TextBox>
                               <img  src="../images/113.png" />
                      </td>
                </tr>
                 <tr>
                    <td style="text-align:right;"><label><b>공유</b></label></td>
                      <td style="text-align:left"> 
                           <asp:DropDownList ID="DropDownList3" runat="server" Height="26px">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="공유없음" Selected="True">공유없음</asp:ListItem>							
						<asp:ListItem Value="하위 프로젝트">하위 프로젝트</asp:ListItem>
                        <asp:ListItem Value="상위 및 하위 프로젝트">상위 및 하위 프로젝트</asp:ListItem>
                        <asp:ListItem Value="최상위 및 하위 프로젝트">최상위 및 하위 프로젝트</asp:ListItem>
                  </asp:DropDownList> 
                      </td>
                </tr>
            </table>
       <%--   <button class="w3-button w3-block w3-green w3-section w3-padding" type="submit">Login</button>--%>
<%--          <input class="w3-check w3-margin-top" type="checkbox" checked="checked"> Remember me--%>
        </div>
      <div class="w3-container w3-border-top w3-padding-16 w3-light-grey">
<%--        <button onclick="document.getElementById('id01').style.display='none'" type="button" style="margin-left:400px;">만들기</button>
        <button onclick="document.getElementById('id01').style.display='none'" type="button">취소</button>--%>

        <asp:Button ID="button4" runat="server" OnClick="MakeVersion" type="button" Text="만들기" />
        <asp:Button ID="Button5" runat="server" Text="취소" />
      </div>
    </div>
  </div>
        <%--   <input type="text" class="autocomplete" id="autocomplete" onfocus="true" /><br />--%>


       
<%--        <h2>자동완성기능</h2>
<input id="autocomplete" type="text" />--%>
<%--<script>
    $(function(){
    var autocomplete_text = ["자동완성기능","Autocomplete","개발로짜","국이"];
         $("#autocomplete").autocomplete({
            source: autocomplete_text
         });
})


</script>--%>



        <div class="btn">
            <a class="btn_big" href="javascript:MakeWorks();"><strong>만들기</strong></a>
            <a class="btn_big" href="javascript:MakeGoWorks();"><strong>만들고 계속하기</strong></a>
              <a href="#">미리보기</a>
        </div>
        <asp:LinkButton ID="lnbSave" runat="server" Width=0 Height=0 OnClick="lnbSave_Click"></asp:LinkButton>
    </form>
    <script type="text/javascript">
             var oEditors_price = [];
		nhn.husky.EZCreator.createInIFrame({
			oAppRef: oEditors_price,
			elPlaceHolder: "ir_MakeWork",
			sSkinURI: "/Utils/SmartEditor2.9/SmartEditor2Skin.html",
			fCreator: "createSEditor2"
        });
    </script>
         <uc1:Bottom ID="Bottom" runat="server" />
</body>
</html>