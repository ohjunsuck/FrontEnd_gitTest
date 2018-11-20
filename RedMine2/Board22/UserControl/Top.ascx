<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Top.ascx.cs" Inherits="Board22.UserControl.Top" %>
<header class="auto-style3" style="   background-color: #628DB6;">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link  href="/layout.css" rel ="stylesheet" type="text/css" />
       <style type="text/css">
        .auto-style2 {
            width: 936px;
                color: #f8f8f8;
                background-color: #628DB6;
        }
        .auto-style3 {
            width: 100%;
                color: #f8f8f8;
                background-color: #628DB6;
        }
        .RedMineTop{
                color: #f8f8f8;
                background-color: #628DB6;
                text-align:center;
        }
        .RedMineLetter{
                color: white;
              text-decoration:none;
        }
        .ClassoverViewSelected{
            color: black;
            background-color:white;
            text-decoration:none;
        }
         .login {
            width: 84%;
                color: white;
                background-color:#3E5B76;
             
        }
        .auto-style4 {
            width: 771px;
        }
        .auto-style6 {
            width: 1010px;
        }
           .auto-style8 {
               width: 100%;
               color: white;
               background-color: #3E5B76;
           }
        </style>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<%--    <script src="../config.js" type="text/javascript"></script>--%>
      <script type="text/javascript">
          $(document).ready(function () {
    $(".RedMineTop").hover(function(){
        $(this).css("background-color", "lightblue");
        }, function(){
            $(this).css("background-color", "#628DB6");
                });
         
            $(".RedMineLetter").click(function () {
              //현재의 attr가 RedMineLetter가 아닌애들을 RedMineLetter로바꾸기
                //$("input:not(:checked) + span").css("background-color", "yellow");
                //$( "input").attr( "disabled", "disabled" );
  
               // $(this).attr('class', 'ClassoverViewSelected');     
        });
              var url = "<%=Request.Path %>";
               var jbSplit = url.split('/');
              $("#"+jbSplit[2].split('.')).attr('class', 'ClassoverViewSelected');  
        });
  

    </script>

         <%if(m_bisLogin==true){%>
      <table id="beforelogin" class="auto-style8">
          <tr><td class="auto-style4"></td>
           <td>아이디 :<input type="text" id="loginid" name="loginid" />비밀번호 :<input type="password" id="txtPassword" name="loginPassword" /><a href="/index.aspx">[회원가입]</a>  <a href="/index.aspx">[암호찾기]</a> <input type="submit"  id="btnLogin" value="로그인"   /></td></tr>
        </table>
      <%}else if(m_bisLogin==false){%>
      <table id="afterlogin" class="auto-style8" >
            <tr><td class="auto-style6"></td>
         
                <td> <%=m_sLoginID %> <a href="/index.aspx">[암호변경]</a><input type="submit"  name="Submit1" value="로그아웃" /> </td></tr> 
      </table>
      <%}%>
    	<h1 style="text-align:left">플랫폼 개발팀</h1>
	<table class="auto-style2" style="   background-color: #628DB6;">
		<tr>
		<td class="RedMineTop" ><a href="/Layer/Summary.aspx" class="RedMineLetter" id="Summary"  >개요</a></td>
	<td class="RedMineTop" ><a href="/Layer/WorkContents.aspx" class="RedMineLetter" id="WorkContents"  >작업내역</a></td>
	<td class="RedMineTop" ><a href="/Layer/RoadMap.aspx" class="RedMineLetter" id="RoadMap" >로드맵</a></td>
	<td class="RedMineTop" ><a href="/Layer/Works.aspx" class="RedMineLetter" id="Works" >일감</a></td>
	<td class="RedMineTop" ><a href="/Layer/MakeWorks.aspx" class="RedMineLetter" id="MakeWorks"  >새 일감만들기</a></td>
	<td class="RedMineTop" ><a href="/Layer/GanttChart.aspx" class="RedMineLetter"id="GanttChart"  >Gantt 차트</a></td>
    <td class="RedMineTop" ><a href="/Layer/Calendar.aspx" class="RedMineLetter"id="Calendar"   >달력</a></td>
    <td class="RedMineTop" ><a href="/Layer/News.aspx" class="RedMineLetter" id="News" >뉴스</a></td>
    <td class="RedMineTop" ><a href="/Layer/documents.aspx" class="RedMineLetter"id="documents"  >문서</a></td>
    <td class="RedMineTop" ><a href="/Layer/Wikis.aspx" class="RedMineLetter" id="Wikis" >위키</a></td>
    <td class="RedMineTop" ><a href="/Layer/Board.aspx" class="RedMineLetter"id="Board"   >게시판</a></td>
    <td class="RedMineTop" ><a href="/Layer/Files.aspx" class="RedMineLetter" id="Files" >파일</a></td>
    <td class="RedMineTop" ><a href="/Layer/Storage.aspx" class="RedMineLetter"id="Storage" >저장소</a></td>
    <td class="RedMineTop" ><a href="/Layer/Setting.aspx" class="RedMineLetter"id="Setting"  >설정</a></td>
		</tr>
	</table>
</header>
 