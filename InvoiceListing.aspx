<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="InvoiceListing.aspx.cs" Inherits="InvoiceListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Invois & Bayaran <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content">
                    <div class="">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <a class="btn btn-app" onclick="openaddnewinvoice();">
                          <i class="fa fa-plus-square green"></i>Daftar 
                        </a>
                        <a class="btn btn-app" onclick="enabledisablesearchbox();">
                          <i class="fa fa-search"></i> Carian
                        </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-section" style="display:none;">
                        <section class="panel">
                            <div class="x_title">
                                <h2>Carian Invois & Bayaran</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <div class="form-group">
                                        <label for="invoiceno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Invois:</label>
                                        <input type="text" id="invoiceno" class="form-control" name="invoiceno" value="<%=sInvoiceNo %>" />
                                    </div>
                                    <div class="form-group">
                                        <label for="bpdid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Invois Kepada:</label>
                                        <select id="bpdid" class="select2_single form-control" tabindex="-1" name="bpdid" style="width: 100%;">
                                                <option></option>
                                                <%
                                                    if (lsBP.Count > 0)
                                                    {
                                                        for (int i = 0; i < lsBP.Count; i++)
                                                        {
                                                            MainModel modBP = (MainModel)lsBP[i];
                                                %>       
                                                            <option value="<%=modBP.GetSetbpid %>" <%=sBPID.Equals(modBP.GetSetbpid)?"selected":"" %>><%=modBP.GetSetbpdesc %></option>
                                                <% 
                                                        }
                                                    }
                                                %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                        <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                        <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                        <div style="display: none;">
                                            <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                            <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>No. Invois</th>
                          <th>Invois Kepada</th>
                          <th>No. Penghantaran</th>
                          <th>Tarikh Invois</th>
                          <th>Kategori</th>
                          <th>Jenis Invois</th>
                          <th>Harga Invois </th>
                          <th>Jumlah TAX</th>
                          <th>Jumlah Invois</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsInvoiceHeader.Count > 0)
                            {
                                for (int i = 0; i < lsInvoiceHeader.Count; i++)
                                {
                                    MainModel modInvHdr = (MainModel)lsInvoiceHeader[i];
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditinvoice('<%=modInvHdr.GetSetcomp %>','<%=modInvHdr.GetSetinvoiceno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditinvoice('<%=modInvHdr.GetSetcomp %>','<%=modInvHdr.GetSetinvoiceno %>');"><%=modInvHdr.GetSetinvoiceno %></a></td>
                              <td><%=modInvHdr.GetSetbpdesc %></td>
                              <td><%=modInvHdr.GetSetshipmentno %></td>
                              <td><%=modInvHdr.GetSetinvoicedate %></td>
                              <td><%=modInvHdr.GetSetinvoicecat %></td>
                              <td><%=modInvHdr.GetSetinvoicetype %></td>
                              <td><%=modInvHdr.GetSetinvoiceamount %></td>
                              <td><%=modInvHdr.GetSettaxamount %></td>
                              <td><%=modInvHdr.GetSettotalamount %></td>
                              <td><%=modInvHdr.GetSetstatus %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td colspan="10">Tiada rekod...</td>
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    </div>
                  </div>
                </div>
            </div>

    <script type="text/javascript">
        var currflag = "0";
        function enabledisablesearchbox() {
            var sb = document.getElementById("search-section");
            var cf = currflag;
            if (cf == "0") {
                sb.style.display = "none";
                currflag = "1";
            } else {
                sb.style.display = "";
                currflag = "0";
            }
        }
        enabledisablesearchbox();

        function openaddnewinvoice() {
            var popupWindow = window.open("InvoiceDetails.aspx?action=ADD", "add_order", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }
        function openeditinvoice(comp,invoiceno) {
            var popupWindow = window.open("InvoiceDetails.aspx?action=OPEN&comp=" + comp + "&invoiceno=" + invoiceno, "open_order", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function actionclick(action) {
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
        openaddnewinvoice();
        <%
        }
        %>
    </script>
</asp:Content>

