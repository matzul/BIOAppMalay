<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPaymentReceipt3.aspx.cs" Inherits="PrintPaymentReceipt3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript"> 
        function ClientSidePrint(idDiv) {
            var w = 600;
            var h = 400;
            var l = (window.screen.availWidth - w) / 2;
            var t = (window.screen.availHeight - h) / 2;

            var sOption = "toolbar=no,location=no,directories=no,menubar=no,scrollbars=yes,width=" + w + ",height=" + h + ",left=" + l + ",top=" + t;
            // Get the HTML content of the div
            var sDivText = window.document.getElementById(idDiv).innerHTML;
            // Open a new window
            var objWindow = window.open("", "Print", sOption);
            // Write the div element to the window
            objWindow.document.write(sDivText);
            objWindow.document.close();
            // Print the window            
            objWindow.print();
            // Close the window
            objWindow.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <button onclick="ClientSidePrint('AccessCard');">Print Access Card...</button>
            <br />
            Barcode Client-side Printing Sample!
        <br />
            <br />
        </div>
        <div id="AccessCard">
            <div style="width: 300px; border: solid 2px black; text-align: center; padding: 5px">
                <strong><span style="font-size: 16pt; font-family: Arial">AdventureWorks<br />
                    Access Card</span></strong><br />
                <br />
                <span style="font-family: Arial"><strong>
                    <br />
                    Gilbert, Guy<br />
                </strong>
                    <span style="font-size: 10pt">Software Developer<br />
                    </span></span>
                <br />                
                <br />
                <br />
                <span style="font-size: 8pt"><span style="font-family: Arial"><strong>IMPORTANT<br />
                </strong>The cardholder
                        accepts responsibility for materials changed to this card and so on! </span></span>
            </div>
        </div>
    </form>
</body>
</html>
