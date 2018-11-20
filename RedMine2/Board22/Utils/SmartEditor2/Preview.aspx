<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="GW.Approval.Web.Utils.SmartEditor2.Preview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>미리보기</title>
    <link href="/community/css/boards.css" rel="stylesheet" type="text/css"/>    
<style>
    div.contentsdiv{
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
    }
    div.contentsdiv table{
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
        width:630px;
    }
    div.contentsdiv table tr td{
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
    }    
    div.contentsdiv table tbody tr td{
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
    }    
    div.contentsdiv table tbody tr td a{
        white-space: pre-wrap; /* css-3 */
        white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
        white-space: -pre-wrap; /* Opera 4-6 */
        white-space: -o-pre-wrap; /* Opera 7 */
        word-wrap: break-word; /* Internet Explorer 5.5+ */
    }          
</style>
</head>
<body>
<div class="boardcode approval_list">
    <h1>작성 중인 본문 미리보기</h1>
</div>    
<div class="boardform" style="text-align:right;">
    <a href="javascript:window.close();" class="btn_big"><span>닫기</span></a>
</div>
<div class="boardform" style="border:1px solid #ccc;padding:5px;">
    <div style="width:654px;overflow:hidden;z-index:0;" class="contentsdiv"><asp:Literal ID="lblContents" runat="server" EnableViewState="false" /></div>
</div>
</body>
</html>
