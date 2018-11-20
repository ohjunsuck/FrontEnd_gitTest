<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Board22.Board.List" %>
<%@ Register src="~/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Left.ascx" tagname="Left" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <link  href="/layout.css" rel ="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 900px;
        }
        .auto-style2 {
            width: 898px;
        }
        .auto-style3 {
            width: 27%;
        }
        .auto-style4 {
            width: 251px;
        }
        </style>
    <script type="text/javascript">

//검색
        function Search() {
            var url = "<%=Request.Path %>";
            var oKeyword = document.getElementById("txtKeyword");
            url += "?keyword=" + escape(oKeyword.value);
            location.href = url;
        }

//function chageLangSelect() {
//    var langSelect = document.getElementById("countboard");
//    var selectValue = langSelect.options[langSelect.selectedIndex].value;
//    //_TOPSIZE = selectValue;
//     document.countboard2.submit();
//}
    </script>
</head>
<body>
    <form id="form1"  action="List.aspx" method="post"   >
   <%-- <form id="form1"  action="List.aspx?loginid<%=Session["UserName"]%>&loginPassword=<%=Session["UserID"] %>" method="post"   > --%>
<header class="auto-style3">
      <uc1:Top ID="Top" runat="server" />
</header>
      
<section>
  <nav>
       <uc1:Left ID="Left" runat="server" />
  </nav>
  <article>
     <table class="tbl01" cellpadding="0" cellspacing="0">
        <tr><td width="5px" style="height: 3px"></td><td class="td01" style="height: 3px"></td></tr>
        <tr><td></td><td class="td03">
            <img src="/images/title_icon.gif" />
            &nbsp;&nbsp;&nbsp;글 목 록<br />
<%--            게시글수:
             <select id="countboard"  name="countboard"  onchange="this.form.submit()" >
               <option value="15">15</option>
               <option value="30">30</option>
               <option value="50">50</option>
             </select>--%>
 
<%--            <input type="submit" name="deleteAll" value="삭제" id="deletebtn" />--%>
            </td></tr>
        <tr><td></td><td class="td01"></td></tr>
        <tr><td></td><td height="15"></td></tr>
    </table>

      <table class="tbl01" cellpadding="0" cellspacing="0">
        <tr><td width="5px"></td><td>

           <div class="approval_list">
			<div class="count">전체 글수 : <%=TotalCount%></div>
			<table border="1" style="color:#808080; border-collapse:collapse;">
				<tbody>
					<tr style="color:black; background-color:white; font-weight:bold;">
                        <th class="auto-style1" style="width:58px">번호</th>
						<th class="auto-style1" style="width:250px">제목</th>
						<th class="auto-style1" style="width:70px">작성자</th>
						<th class="auto-style1" style="width:100px">날짜</th>
						<th class="auto-style1" style="width:40px">조회</th>
					</tr>
				<asp:Repeater ID="rptList" runat="server" OnItemCreated="rptList_ItemCreated">
					<ItemTemplate>
					<tr class="select" style="cursor:pointer" <%--onclick="ApprovalView('<%#Eval("form_type")%>','<%#Eval("process_id")%>')"--%>>
					<%--	<td > <%# ShowDepth((int)Eval("depth")) %></td>--%>
                       <%--<td>     <img src="~/images/공지1.png" /></td>--%>
                     <%--<td > <%# ShowReplyIcon((int)Eval("inner_id")) %></td>--%>
                    <%-- <td >   <%# ShowDate((DateTime)Eval("reg_date")) %></td>  --%>
                      <td><%# ShowSerailNum(Eval("serial_no").ToString()) %></td>
                        	<td >  <%# ShowTitle(
                                Eval("serial_no").ToString(), Eval("title").ToString(),
                                Eval("del_flag").ToString()) %></td>
                          <td> <%#ShowWriter(Eval("writer").ToString()) %></td>
						<td >   <%# ShowDate((DateTime)Eval("reg_date")) %></td>  
                        <td><%# ShowReadCount(Eval("read_count").ToString()) %></td>
					</tr>
					</ItemTemplate>
				</asp:Repeater>
				</tbody>
			</table>
		</div>
        <tr><td height="10px" style="width: 11px"></td><td colspan="2">

          <%--  <a href='Write.aspx?loginid=<%=Request.QueryString["loginid"]%>&amp;loginPassword=<%=Request.QueryString["loginPassword"] %>'>[글쓰기]</a><br />--%>
              <input type="text"  name="txtKeyword"  id="txtKeyword"onkeydown="if(event.keyCode==13){Search();return false;}else{return true;}" value="1111" />
		    <a class="btn_sml" href="javascript:Search()"><span>검색</span></a>&nbsp;
            <a href='Write.aspx'>[글쓰기]</a>   
            
      <asp:Literal ID="ltrNone" runat="server"></asp:Literal>
      <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
            </td></tr>
    </table>
  </article>

</section>

<footer>
  <%--  <uc1:Bottom ID="Bottom" runat="server" />--%>
</footer>
    </form>
</body>
</html>
