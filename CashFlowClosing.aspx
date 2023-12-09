<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashFlowClosing.aspx.cs" Inherits="CashFlowClosing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cash Flow Closing Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        PENUTUPAN ALIRAN KEWANGAN
        <br />
        <%
        MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
        %>
        <strong><%=oAlerMssg.GetSetalertstatus%><br /><%=oAlerMssg.GetSetalertmessage %></strong> 
    </div>
    </form>
</body>
</html>
