<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Read.aspx.cs" Inherits="Board22.Board.Read" %>
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
        .auto-style5 {
            width: 334px;
        }
    </style>
</head>
<body>
    <form id="form1" action="Read.aspx" method="get" >
<header class="auto-style3">
      <uc1:Top ID="Top" runat="server" />
</header>
      
<section>
  <nav>
       <uc1:Left ID="Left" runat="server" />
  </nav>
  <article>
       <table class="tbl01" cellpadding="0" cellspacing="0">
        <tr><td width="5px"></td><td class="td01"></td></tr>
        <tr><td></td><td class="td03">
            <img src="/images/title_icon.gif" />
            &nbsp;&nbsp;&nbsp;글 읽 기</td></tr>
        <tr><td></td><td class="td01"></td></tr>
        <tr><td></td><td height="15"></td></tr>
    </table>

    <table class="tbl01" cellpadding="0" cellspacing="0">
            <tr><td width="5px"></td><td>
            <table class="tbl01" cellpadding="0" cellspacing="0">
                <tr><td colspan="2" class="td02"></td></tr>
                <tr><td class="td05">· 작성자: </td>
                    <td style="height: 30px">
                        <%--<asp:Label ID="lblWriter" runat="server" Text="Label"></asp:Label>--%>
                        <%=writer %>
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr><td class="td04">· 날짜: </td>
                    <td>
                        <%=time %>
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr><td class="td04">· 제목: </td>
                    <td>
                         <%=title %>
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr>
                    <td style="padding-left:20px; padding-top:7px" valign="top">· 내용</td>
                    <td class="p02">
                        <textarea id="txtMessage" rows="2" cols="20" readonly="readonly" style="border-color:#B0ADF5; border-width:1px;
                        border-style:solid; height:200px; width:400px; margin-bottom: 0px;" ><%=contents %>
                        </textarea>
                    </td>
                </tr>
                <tr><td colspan="2" class="td02"></td></tr>
            </table>
    </td></tr></table>

      <table>
        <tr><td></td><td class="td06">&nbsp;</td>
            <td align="left" width="60px">
               <%-- <asp:ImageButton ID="btnEdit" runat="server"  
                    ImageUrl="~/images/btn_edit.gif"/>--%>

<%--sn=" + Eval("serial_no").ToString() +"&--%>
               <%-- <a href="Edit.aspx?loginid=<%=Request.QueryString["loginid"]%>&loginPassword=<%=Request.QueryString["loginPassword"] %>"   >[수정]</a>--%>
                 <%--<a href="Edit.aspx?sn=<%=Request.QueryString["sn"]%>&loginid=<%=Request.QueryString["loginid"]%>&loginPassword=<%=Request.QueryString["loginPassword"] %>"   >[수정]</a>--%>
                <a href="Edit.aspx?sn=<%=Request.QueryString["sn"]%>"   >[수정]</a>
            </td><td align="left" width="60px">
               <%-- <asp:ImageButton ID="btnReply" runat="server"  
                    ImageUrl="~/images/btn_reply.gif" />--%>
                <a href="List.aspx?page=<%=Session["NowPageIndex"]%>"   >[답글]</a>
            </td><td align="left" class="auto-style5">
              <%--  <asp:ImageButton ID="btnDelete" runat="server"  
                    ImageUrl="~/images/btn_delete.gif" />--%>
                <a href="Read.aspx?deletetable=true"   >[삭제]</a>
               <%--  <input type="submit" name="Submit11" id="Submit11" value="삭제"  /> --%>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
<%--                <asp:ImageButton ID="btnList" runat="server"
                    ImageUrl="~/images/btn_list.gif"
                    PostBackUrl="~/Board/List.aspx" CausesValidation="False" />--%>

               <%--   <a href="List.aspx?page=<%=Session["NowPageIndex"]%>"   >[리스트]</a>--%>

                   <%if(Session["keyword"]==""){%>
                  <a href="List.aspx?page=<%=Session["NowPageIndex"]%>"   >[리스트]</a>
      <%}else if(Session["keyword"]!=""&& Session["NowPageIndex"]==null){%>
                  <a href="List.aspx?keyword=<%=Session["keyword"]%>"   >[리스트]</a>
      <%}else if(Session["keyword"]!="" && Session["NowPageIndex"]!=null){%>
                  <a href="List.aspx?keyword=<%=Session["keyword"]%>&page=<%=Session["NowPageIndex"]%>"   >[리스트]</a>
      <%}%>

                </td>
            <td align="right">
                &nbsp;</td></tr>
        <tr><td height="10px"></td><td colspan="5"></td></tr>
    </table>


  </article>
</section>

<footer>
 <%--   <uc1:Bottom ID="Bottom" runat="server" />--%>
</footer>
    </form>
</body>
</html>
