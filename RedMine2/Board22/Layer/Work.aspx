<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Work.aspx.cs" Inherits="Board22.Layer.Work" %>
<%@ Register src="~/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="~/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .issue {
    /*background-color: #ffffdd;*/
      background-color: lightgoldenrodyellow;
    padding: 6px;
    margin-bottom: 6px;
    border: 1px solid #d7d7d7;
}
        .auto-style9 {
            width: 18px;
            height: 20px;
        }
        .closed {
    background: #BAE0BA none repeat scroll 0%;
}
        .contextual {
    float: right;
    white-space: nowrap;
    line-height: 1.4em;
    margin-top: 5px;
    padding-left: 10px;
    font-size: 0.9em;
}
        .icon-edit{
            background-image: url(/images/77.png)
        }
         .icon-edit1{
            background-image: url(/images/88.png)
        }
          .icon-edit2{
            background-image: url(/images/99.png)
        }
             .icon-edit22{
            background-image: url(/images/98.png)
        }
           .icon-edit3{
            background-image: url(/images/100.png)
        }
        .icon {
    background-position: 0% 50%;
    background-repeat: no-repeat;
    padding-left: 20px;
    padding-top: 2px;
    padding-bottom: 3px;
}
           .iconattachment{
            background-image: url(/images/66.png)
        }
    </style>
   
    <script>
        function issuewatcher() {
            var data = document.getElementById("issuewatcher");

            if (data.getAttribute("class") == "icon-edit2 icon") {
                data.setAttribute("class", "icon-edit22 icon");
                data.innerText = "관심끄기";
            }
            else {
                data.setAttribute("class", "icon-edit2 icon");
                 data.innerText = "지켜보기";
            }

    }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post">
          <uc1:Top ID="Top" runat="server" />
     <h1> <%=WC_State %> #<%=Request.QueryString["sn"]%> </h1>  <%--<img src="/images/77.png"  align="right"  /> <a href="#" style="text-align:right">편집</a>--%>
        <div class="contextual">
           <a href="EditWork2.aspx?sn=<%=WC_id%>" class="icon-edit icon">편집</a>
              <a href="#" class="icon-edit1 icon">작업시간 기록</a>
              <a href="#" class="icon-edit2 icon" id="issuewatcher" onclick="issuewatcher();">지켜보기</a>
              <a href="#" class="icon-edit3 icon">복사</a>
        </div>
      
        <div class="issue">
            <img src="/images/redminimg.jpg" alt="Alternate Text" /> <span style="font-size:50px; font-weight: bold; text-align:center"> <%=WC_Name %></span>
                <hr />
            <table>
                <tr>
                    <td style="width:350px">상태:</td>
                    <td style="width:400px"> <%=WC_State %></td>
                    <td style="width:350px"> 시작시간:</td>
                    <td style="width:400px"><%=WC_date %> </td>
                </tr>
                  <tr>
                    <td > 우선순위:</td>
                    <td > <%=WC_Priority %></td>
                    <td > 완료시간:</td>
                    <td ><%=WC_Completedate %> </td>
                </tr>
                  <tr>
                    <td > 담당자:</td>
                    <td > <img src="/images/redminimg.jpg" class="auto-style9" /> <a href="#"> <%=WC_Manager %></a> </td>
                    <td > 진척도:</td>
                      <td > <%=GetProcessGraph(WC_Progress.ToString())%>   <%=WC_Progress %>% </td>
                    
                </tr>
                  <tr>
                    <td > 범주:</td>
                    <td > <%=WC_Typestring %></td>
                    <td > 소요 시간:</td>
                    <td ><%=WC_NeedTime %> </td>
                </tr>
                  <tr>
                    <td > 목표버전:</td>
                    <td > <%=WC_Version %></td>
                    <td > </td>
                    <td ></td>
                </tr>
            </table>
            <hr />
            설명 <br />
            <%=WC_Contents %> <br />
         

              <%if(WC_Files!=""){%>
      <a href="#" class="iconattachment icon"> </a>
            <asp:Literal ID="ltrAttachFile" runat="server" Text=""></asp:Literal>
      <%}else if(WC_Files==""){%>
    
      <%}%>

       
            <br />
            일감 관람자: <%for (int i = 0; i < WorkManagerlist.Count(); ++i){%>
             <%= WorkManagerlist[i].ToString() %>
              <%}%>
       
        </div>


    </form>
    <uc1:Bottom ID="Bottom" runat="server" />
</body>
</html>
