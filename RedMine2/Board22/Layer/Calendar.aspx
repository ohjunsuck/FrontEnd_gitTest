<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="Board22.Layer.Calendar" %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
          <uc1:Top ID="Top" runat="server" />
      "<%=Request.Path %>";
    </form>
</body>
</html>