<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWork.aspx.cs" Inherits="Board22.Layer.EditWork" %>
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
    .auto-style10 {
        width: 630%;
        height: 270px;
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
}
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>


    <%--데이터피커--%>
	<link href="/Utils/Datepicker/css/ui-lightness/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
	<script src="/Utils/Datepicker/js/jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
	<script src="/Utils/Datepicker/js/jquery.ui.datepicker-ko.js" type="tet/javascript"></script>
    <%--SmartEditor 항상 jQuery뒤에순서중요!--%>
    <script src="/Utils/smarteditor2.9/js/service/HuskyEZCreator.js"></script>
   <script type="text/javascript" src="EditWorks.js"></script>
    <script type="text/javascript">
        $(function () {
      	    $("#biz_start_dt").val("<%=DateTime.Now.ToShortDateString()%>");
            $("#biz_end_dt").val("<%=DateTime.Now.ToShortDateString()%>");
            $("#txtTitle").val("<%=WC_Name.ToString()%>");
        });



        $("#txtTitle").change(function () {
            alert(1);
        });
      
	</script>
</head>
<body>
    <form id="form1" runat="server">
          <uc1:Top ID="Top" runat="server" />
        <h1>편집</h1>
        <div class="box tabular">유형
            <asp:Label ID="Label1" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
                  <asp:DropDownList ID="WC_State1" runat="server">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="버그">버그</asp:ListItem>							
						<asp:ListItem Value="업무">업무</asp:ListItem>
						<asp:ListItem Value="건의">건의</asp:ListItem>
                      	<asp:ListItem Value="기능">기능</asp:ListItem>
                      	<asp:ListItem Value="지원">지원</asp:ListItem>
                      	<asp:ListItem Value="잡일">잡일</asp:ListItem>
                      	<asp:ListItem Value="운영관리">운영관리</asp:ListItem>
                      	<asp:ListItem Value="변경점">변경점</asp:ListItem>
                      	<asp:ListItem Value="자산">자산</asp:ListItem>
                      	<asp:ListItem Value="크래시">크래시</asp:ListItem>
                  </asp:DropDownList> <br />
            제목
            <asp:Label ID="WC_Name1" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
<%--            <input id="txtTitle"  runat="server" type="text" class="auto-style9" /><br />--%>
            <asp:TextBox ID="txtTitle" class="auto-style9"  runat="server" OnTextChanged="EditText" ></asp:TextBox><br />
&nbsp;설명
            	<asp:HiddenField ID="biz_MakeWork" runat="server" />
					<textarea name="ir_MakeWork"  runat="server" id="ir_MakeWork" class="auto-style10"></textarea>
            <br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 상태<asp:Label ID="Label3" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
                  <asp:DropDownList ID="DropDownList2" runat="server">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
						<asp:ListItem Value="새 일감">새 일감</asp:ListItem>							
						<asp:ListItem Value="진행중">진행중</asp:ListItem>
						<asp:ListItem Value="확인 요망">확인 요망</asp:ListItem>
                      	<asp:ListItem Value="QA중">QA중</asp:ListItem>
                      	<asp:ListItem Value="완료">완료</asp:ListItem>
                      	<asp:ListItem Value="재발생">재발생</asp:ListItem>
                      	<asp:ListItem Value="지속">지속</asp:ListItem>
                      	<asp:ListItem Value="폐기">폐기</asp:ListItem>
                      	<asp:ListItem Value="보류">보류</asp:ListItem>
                  </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
        						시작시간<asp:TextBox ID="biz_start_dt" Width="80px" runat="server"></asp:TextBox> 
            <br />
                  우선순위<asp:Label ID="Label4" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
                  <asp:DropDownList ID="WC_Priority1" runat="server">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
                      	<asp:ListItem Value="낮음">낮음</asp:ListItem>
						<asp:ListItem Value="보통">보통</asp:ListItem>							
						<asp:ListItem Value="중요">중요</asp:ListItem>
						<asp:ListItem Value="높음">높음</asp:ListItem>
                      	<asp:ListItem Value="긴급">긴급</asp:ListItem>
                  </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
        						완료기한<asp:TextBox ID="biz_end_dt" Width="80px" runat="server"></asp:TextBox> <br />
                  &nbsp;&nbsp;&nbsp; 담당자<asp:Label ID="DL_Manager" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
                  <asp:TextBox ID="WC_Manager1" Width="80px" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
        						추정시간<asp:TextBox ID="NeedTime" Width="35px" runat="server"></asp:TextBox>  시간<br />
                   목표버전<asp:Label ID="Label6" runat="server" Text="＊" ForeColor="#FF3300"></asp:Label> &nbsp;
                  <asp:DropDownList ID="DL_Version" runat="server">
                	<asp:ListItem Value="">--선택--</asp:ListItem>
                      	<asp:ListItem Value="1">GBOX</asp:ListItem>
						<asp:ListItem Value="2">IBOX</asp:ListItem>						
                  </asp:DropDownList>  &nbsp;&nbsp;&nbsp;&nbsp;
             진척도
             <asp:DropDownList ID="WC_Progress1" runat="server">
                	<asp:ListItem Value="0">0%</asp:ListItem>
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
                  </asp:DropDownList>
            <br />
<!--첨부파일-->
            <div class="container">
                <table>
                    <tr>
                        <td class="left box_top">첨부파일</td>
                        <td class="center">&nbsp; &nbsp;</td>
                        <td class="right">
                           <asp:FileUpload ID="FileUpload1" runat="server" />
       <%-- <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="파일업로드" />  --%>
                        &nbsp;(최대 크기: 100 MB)</td>
                    </tr>
                </table>
            </div>
            일감 관람자<br />

        </div>
        <div class="btn">
            <a class="btn_big" href="javascript:EditWorks();"><strong>편집하기</strong></a>
            <a class="btn_big" href="javascript:EditGoWorks();"><strong>편집하고 계속하기</strong></a>
              <a href="#">미리보기</a>
        </div>
        <asp:LinkButton ID="lnbSave" runat="server" Width=0 Height=0 OnClick="lnbEdit_Click"></asp:LinkButton>
    </form>
    <script type="text/javascript">
        var oEditors_price = [];
		nhn.husky.EZCreator.createInIFrame({
			oAppRef: oEditors_price,
			elPlaceHolder: "ir_MakeWork",
			sSkinURI: "/Utils/SmartEditor2.9/SmartEditor2Skin.html",
			fCreator: "createSEditor2"
        });

    //   var contents = {
    //    biz_content: oEditors_price.getById["ir_MakeWork"].getIR()
    //};
    //oEditors_price.getById["ir_MakeWork"].exec("UPDATE_CONTENTS_FIELD", []);
    //document.getElementById("biz_MakeWork").value = contents.biz_content;
    </script>
         <uc1:Bottom ID="Bottom" runat="server" />
</body>
</html>