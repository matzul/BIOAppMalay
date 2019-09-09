<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockStateClosing.aspx.cs" Inherits="StockStateClosing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock State Closing Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        PENUTUPAN STOK / INVENTORI
        <br />
        <%
        MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
        %>
        <strong><%=oAlerMssg.GetSetalertstatus%><br /><%=oAlerMssg.GetSetalertmessage %></strong> 
    </div>
    </form>
</body>
</html>
