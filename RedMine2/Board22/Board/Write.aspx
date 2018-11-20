<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Write.aspx.cs" Inherits="Board22.Board.Write" %>
<%@ Register src="~/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Left.ascx" tagname="Left" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <link  href="/layout.css" rel ="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" action="Write.aspx" method="post" >
<header class="auto-style3">
      <uc1:Top ID="Top" runat="server" />    <style type="text/css">
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
            &nbsp;&nbsp;&nbsp;글 쓰 기</td></tr>
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
                        <input type="text" name="editwriter" readonly="readonly"  value= <%=writer %> />
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr><td class="td04">· 비번: </td>
                    <td>
                        <input type="text" name="writepassword"  />
                    <%--    <%=time %>--%>
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr><td class="td04">· 제목: </td>
                    <td>
                        <input type="text" name="edittitle" />
                    </td></tr>
                <tr><td colspan="2" class="td08"></td></tr>
                <tr>
                    <td style="padding-left:20px; padding-top:7px" valign="top">· 내용</td>
                    <td class="p02">
                        <textarea name="txtMessage" rows="2" cols="20"  style="border-color:#B0ADF5; border-width:1px;
                        border-style:solid; height:200px; width:400px; margin-bottom: 0px;" ><%--<%=contents %>--%>
                        </textarea>
                    </td>
                </tr>
                <tr><td colspan="2" class="td02"></td></tr>
            </table>
    </td></tr></table>

      <table>
        <tr><td></td><td class="td06">&nbsp;</td>
            <td align="left" width="60px">
<%--                <a href="Edit.aspx?loginid=<%=Request.QueryString["loginid"]%>&loginPassword=<%=Request.QueryString["loginPassword"] %>"   >[수정]</a>--%>
                  <input type="submit"  id="Submit1" value="쓰기" name="Submit1" /> 
            </td><td align="left" width="60px">
            </td><td align="left" class="auto-style5">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <%-- <a href="List.aspx?loginid=<%=Request.QueryString["loginid"]%>&loginPassword=<%=Request.QueryString["loginPassword"] %>"   >[리스트]</a>--%>
                 <a href="List.aspx"   >[리스트]</a>
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
