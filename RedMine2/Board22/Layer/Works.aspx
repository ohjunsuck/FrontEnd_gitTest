<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Works.aspx.cs" Inherits="Board22.Layer.Works" %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
       .auto-style1 {
    background-color: #EEEEEE;
    border-color: #EEEEEE;
    padding: 4px;
    white-space: nowrap;
}
       .linkText{
             text-decoration:none;
             color: #169;
       }
       .Board{
           text-align:center;
           color:black;
       }
       #sidebar {
    float: right;
    width: 23%;
    position: relative;
    z-index: 9;
    padding: 0;
    margin: 0;
    background-color: lightgray;
}
    
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
          <uc1:Top ID="Top" runat="server" />
<%--      "<%=Request.Path %>";--%>
       <img src="/images/33.png" alt="Alternate Text" /> <a href="#">적용</a> <img src="/images/11.png" alt="Alternate Text" />  <a href="#">지우기</a><img src="/images/22.png" alt="Alternate Text" />  <a href="#">저장</a>

        <div id="sidebar"> 
           <h3> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 일감</h3>
            <ul>
                <li><a href="#">모든 일감 보기</a></li>
                <li><a href="#">요약</a></li>
                <li><a href="#">달력</a></li>
                <li><a href="#">Gantt 차트</a></li>
            </ul>
        </div>
        <table border="1"  style="color:#808080; border-collapse:collapse; border-color:white;" >
				<tbody>
					<tr  style="color:black; background-color:white; font-weight:bold;">
                        <th class="auto-style1" style="width:20px"> <img src="/images/44.png" alt="Alternate Text" /></th>
						<th class="auto-style1" style="width:100px"><a class="linkText"href="#">#</a></th>
						<th class="auto-style1" style="width:100px"><a class="linkText"href="#">상태</a></th>
						<th class="auto-style1" style="width:100px"><a class="linkText"href="#">우선순위</a></th>
						<th class="auto-style1" style="width:600px"><a class="linkText"href="#">제목</a></th>
                        <th class="auto-style1" style="width:100px"><a class="linkText"href="#">저자</a></th>
                        <th class="auto-style1" style="width:100px"><a class="linkText"href="#">담당자</a></th>
                        <th class="auto-style1" style="width:200px"><a class="linkText"href="#">변경</a></th>
                        <th class="auto-style1" style="width:150px"><a class="linkText"href="#">목표버전</a></th>
                        <th class="auto-style1" style="width:150px"><a class="linkText"href="#">진척도</a></th>
					</tr>
				<asp:Repeater ID="rptList" runat="server">
					<ItemTemplate>
					<tr class="select" style="cursor:pointer">
                           <td class="Board"> <input type="checkbox" name="name" value="" /></td>
                      <td  class="Board"><%# ShowTitle(Eval("WC_id").ToString(),Eval("WC_id").ToString()) %></td>
                         <td  class="Board"><%# ShowWorkState(Eval("WC_State").ToString()) %></td>
                      <%--  	<td  class="Board">  <%# ShowTitle(
                                Eval("WC_id").ToString(), Eval("WC_Name").ToString(),
                                Eval("WC_Reply").ToString()) %></td>--%>
                          <td  class="Board"> <%#ShowWriter(Eval("WC_Priority").ToString()) %></td>
				      <td  class="Board"><%# ShowTitle(Eval("WC_Name").ToString(),Eval("WC_id").ToString()) %></td>
                    <td  class="Board"> <%#ShowWriter(Eval("WC_Author").ToString()) %></td>
                         <td  class="Board"><%# ShowReadCount(Eval("WC_Manager").ToString()) %></td>
                      	<td   class="Board">   <%# ShowDate((DateTime)Eval("WC_date")) %></td>  
                         <td  class="Board"><%# ShowReadCount(Eval("WC_Version").ToString()) %></td>
                         <td  class="Board"><%# ShowReadCount(Eval("WC_Progress").ToString()) %></td>     
					</tr>
					</ItemTemplate>
				</asp:Repeater>
				</tbody>
			</table>
             <asp:Literal ID="ltrNone" runat="server"></asp:Literal>
      <asp:Literal ID="ltrPaging" runat="server"></asp:Literal>
    </form>
             <uc1:Bottom ID="Bottom" runat="server" />
</body>

   
</html>