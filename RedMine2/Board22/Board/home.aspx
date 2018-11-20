<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Board22.Board.home" %>
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
            width: 622px;
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" action="List.aspx" method="get" >
<header class="auto-style3">
      <uc1:Top ID="Top" runat="server" />
</header>
      
<section>
  <nav>
       <uc1:Left ID="Left" runat="server" />
  </nav>
  <article>
  
  </article>
</section>

<footer>
    <uc1:Bottom ID="Bottom" runat="server" />
</footer>
    </form>
</body>
</html>
