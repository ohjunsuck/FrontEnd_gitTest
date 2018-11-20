<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Board22.index" %>
<%@ Register src="~/UserControl/home.ascx" tagname="home" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
   
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link  href="/layout.css" rel ="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style2 {
            width: 936px;
                color: #f8f8f8;
                background-color: #628DB6;
        }
        .auto-style3 {
            width: 84%;
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
            width: 650px;
        }
        .auto-style5 {
            width: 100%;
            color: white;
            background-color: #3E5B76;
        }
        .auto-style6 {
            width: 1010px;
        }
        </style>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
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
    
        });
  
    </script>
</head>
<body>
    <form id="form1" action="index.aspx" method="post" >
<header class="auto-style3" style="   background-color: #628DB6;">
         <%if(m_bisLogin==true){%>
      <table id="beforelogin" class="auto-style5">
          <tr><td class="auto-style4"></td>
           <td>아이디 :<input type="text" id="loginid" name="loginid" />비밀번호 :<input type="password" id="txtPassword" name="loginPassword" /><a href="/index.aspx">[회원가입]</a>  <a href="/index.aspx">[암호찾기]</a> <input type="submit"  id="btnLogin" value="로그인"   /></td></tr>
        </table>
      <%}else if(m_bisLogin==false){%>
      <table id="afterlogin" class="auto-style5" >
            <tr><td class="auto-style6"></td>
         
                <td> <%=m_sLoginID %> <a href="/index.aspx">[암호변경]</a><input type="submit"  name="Submit1" value="로그아웃" /> </td></tr> 
      </table>
      <%}%>
    	<h1 style="text-align:left">플랫폼 개발팀</h1>
	<table class="auto-style2" style="   background-color: #628DB6;">
		<tr>
	<td class="RedMineTop" ><a href="/Layer/Summary.aspx" class="RedMineLetter"   >개요</a></td>
	<td class="RedMineTop" ><a href="/Layer/WorkContents.aspx" class="RedMineLetter"  >작업내역</a></td>
	<td class="RedMineTop" ><a href="/Layer/RoadMap.aspx" class="RedMineLetter"  >로드맵</a></td>
	<td class="RedMineTop" ><a href="/Layer/Works.aspx" class="RedMineLetter" >일감</a></td>
	<td class="RedMineTop" ><a href="/Layer/MakeWorks.aspx" class="RedMineLetter"   >새 일감만들기</a></td>
	<td class="RedMineTop" ><a href="/Layer/GanttChart.aspx" class="RedMineLetter"  >Gantt 차트</a></td>
    <td class="RedMineTop" ><a href="/Layer/Calendar.aspx" class="RedMineLetter"   >달력</a></td>
    <td class="RedMineTop" ><a href="/Layer/News.aspx" class="RedMineLetter"  >뉴스</a></td>
    <td class="RedMineTop" ><a href="/Layer/documents.aspx" class="RedMineLetter"  >문서</a></td>
    <td class="RedMineTop" ><a href="/Layer/Wikis.aspx" class="RedMineLetter"  >위키</a></td>
    <td class="RedMineTop" ><a href="/Layer/Board.aspx" class="RedMineLetter"   >게시판</a></td>
    <td class="RedMineTop" ><a href="/Layer/Files.aspx" class="RedMineLetter"  >파일</a></td>
    <td class="RedMineTop" ><a href="/Layer/Storage.aspx" class="RedMineLetter" >저장소</a></td>
    <td class="RedMineTop" ><a href="/Layer/Setting.aspx" class="RedMineLetter"  >설정</a></td>
		</tr>
	</table>
</header>
      
<section>

  <article>
       <uc1:home ID="home" runat="server" />
  </article>
</section>

<footer>
     Copyleft JunShock5 <img src="/images/copyleft.png" style="height: 12px; width: 12px" /> Since 2018&nbsp; 
</footer>
    </form>
</body>
</html>
