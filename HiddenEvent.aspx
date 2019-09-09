<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="HiddenEvent.aspx.cs" Inherits="HiddenEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--BEGIN dialog box for opening CPanel System Admin-->
    <div id="myModal" class="modal fade modal-cpanel-system-admin" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Kategori Id Pengguna anda adalah System Admin. <br />Klik pada CPanel Dashboard untuk menguruskan Syarikat & Pengguna anda. <br /> Klik pada Teruskan untuk menggunakan Sistem BIOAppSystem.</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnAddComp" onclick="actionclick('CPANEL');">CPanel Dashboard</button>
                    <button type="button" class="btn btn-default" onclick="actionclick('PROCEED');">Teruskan <span id="timer"></span></button>
                </div>

            </div>
        </div>
    </div>
    <!--END dialog box for opening CPanel System Admin-->
    <script type="text/javascript">
        var counter = 10;
        $(document).ready(function () {
            $('#myModal').modal('show');

        });

        window.onload = function () {
            var sec = 30;
            var myVar = setInterval(function () {
                var a = new Date();
                sec--;
                document.getElementById("timer").innerHTML = sec;
                if (sec == 00) {
                    //window.location.href = "HiddenEvent.aspx?event=select_comp&comp=<%=Session["comp"]%>";
                    clearInterval(myVar);
                    actionclick('PROCEED');
                }
            }, 500);
        }

        function actionclick(action) {
            var proceed = true;

            if (action == 'CPANEL') {
                window.location.href = "AdminCPanel1.aspx?action=OPEN";
            }
            if (action == 'PROCEED') {
                window.location.href = "HiddenEvent.aspx?event=select_comp&comp=<%=Session["comp"]%>";
            }
        }
    </script>
</asp:Content>

