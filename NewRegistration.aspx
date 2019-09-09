<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="NewRegistration.aspx.cs" Inherits="NewRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <form id="form1" runat="server" onsubmit="actionclick('ADD');">
            <%
                if (sAction.Equals("OPEN") || sAction.Equals("ADD"))
                {
            %>
            <div class="x_panel">
                <div class="x_title">
                    <h2>Pendaftaran Baru</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div id="formdetails" class="form-horizontal form-label-left">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label for="name" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Nama *</label>
                                <input id="name" name="name" type="text" required="required" class="form-control" value="<%=sName %>" />
                            </div>
                            <div class="form-group">
                                <label for="address" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Alamat</label>
                                <textarea id="address" class="form-control" rows="2" name="address"><%=sAddress %></textarea>
                            </div>
                            <div class="form-group">
                                <label for="telno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Telefon *</label>
                                <input id="telno" name="telno" type="text" required="required" class="form-control" value="<%=sTelNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="email" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Email (User Id) *</label>
                                <input id="email" name="email" type="text" required="required" class="form-control" value="<%=sEmail %>" />
                            </div>
                            <div class="form-group">
                                <label for="password" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Password *</label>
                                <input id="password" name="password" type="password" required="required" class="form-control" value="<%=sPassword %>" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="text-center">
                <button id="btnRegister" name="btnRegister" type="button" class="btn btn-success" onclick="actionclick('ADD');">Daftar</button>
                <button id="btnReset" name="btnDelete" type="button" class="btn btn-danger" onclick="actionclick('RESET');">Reset</button>
                <button type="button" class="btn btn-default" onclick="window.close();">Tutup</button>
            </div>
            <p>
                <span id="errMsg" style="color: Red; font-family: Verdana; font-size: 10pt;"><%=sMessage %></span>
            </p>
            <%
                }
                else if (sAction.Equals("SUCCESS"))
                {
            %>
            <div class="x_panel">
                <div class="x_title">
                    <h2>Pendaftaran Telah Berjaya</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h4>Sila log masuk ke BIOApp System menggunakan Emel & Password yang didaftarkan sebagai Id Pengguna dan Katalaluan.</h4>
                </div>
                <div class="text-center">
                    <button type="button" class="btn btn-default" onclick="window.close();">Tutup</button>
                </div>
            </div>
            <%
                }
            %>
            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
            </div>
        </form>
    </div>

    <script type="text/javascript">
        function actionclick(action) {
            var proceed = true;
            if (action == 'ADD') {

            }
            if (action == 'RESET') {
                $("#name").val("");
                $("#address").text("");
                $("#telno").val("");
                $("#email").val("");
                $("#password").val("");
                proceed = false;
            }

            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }
    </script>
</asp:Content>

