<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPaymentReceipt2.aspx.cs" Inherits="PrintPaymentReceipt2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="ActiveXReport" method="post" runat="server">
	</form>
		<div id="pagebody">
			<object id="arv" codebase="arview2.cab#version=-1,-1,-1,-1" height="100%" width="100%" classid="clsid:8569D715-FF88-44BA-8D1D-AD3E59543DDE" viewastext>
				<param name="_ExtentX" value="26141">
				<param name="_ExtentY" value="11959">
			</object>
            <script type="text/javascript" for="arv" event="ControlLoaded">
                return arv_ControlLoaded();
            </script>
            <script type="text/javascript">
                function arv_ControlLoaded() {
                    document.getElementById("arv").DataPath = "PrintPaymentReceipt2.aspx?ReturnReport=1";
                }

                setTimeout(function() {
                      //window.close();
                }, 5000);
            </script>
		</div>
</body>
</html>
