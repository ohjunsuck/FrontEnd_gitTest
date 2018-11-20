<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Left.ascx.cs" Inherits="Board22.UserControl.Left" %>


 <%--  <%if(m_bisLogin==true){%>
      <table id="beforelogin">
          <tr><td>아이디 :<input type="text" id="txtId" name="loginid" /></td></tr>
          <tr><td>비밀번호 :<input type="password" id="txtPassword" name="loginPassword" /> </td></tr>
              <tr> <td><a href="/index.aspx">[회원가입]</a>  <a href="/index.aspx">[암호찾기]</a></td></tr>
             <tr><td><input type="submit"  id="btnLogin" value="로그인"   /></td></tr>
        </table>
      <%}else if(m_bisLogin==false){%>
      <table id="afterlogin" >
            <tr><th><%=m_sLoginID %></th></tr> 
             <tr><td><a href="/index.aspx?loginid=<%=Request.QueryString["loginid"]%>&loginPassword=<%=Request.QueryString["loginPassword"] %>"   >[암호변경]</a>
                 <input type="submit"  name="Submit1" value="로그아웃" /> </td></tr> 
      </table>
      <%}%>--%>
