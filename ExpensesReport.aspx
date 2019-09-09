<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ExpensesReport.aspx.cs" Inherits="ExpensesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Bil & Belanja <small>SENARAI</small></h2>
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
                    <form id="Form1" runat="server">
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="expensesno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Bil & Belanja:</label>
                                <input type="text" id="expensesno" class="form-control" name="expensesno" value="<%=sExpensesNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Bayar Kepada:</label>
                                <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" style="width: 100%;">
                                    <option></option>
                                    <%
                                        if (lsPayTo.Count > 0)
                                        {
                                            for (int i = 0; i < lsPayTo.Count; i++)
                                            {
                                                MainModel modBP = (MainModel)lsPayTo[i];
                                    %>
                                    <option value="<%=modBP.GetSetbpid %>" <%=sPayToId.Equals(modBP.GetSetbpid)?"selected":"" %>><%=modBP.GetSetbpdesc %></option>
                                    <% 
                                            }
                                        }
                                    %>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="receiptno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penerimaan:</label>
                                <input type="text" id="receiptno" class="form-control" name="receiptno" value="<%=sReceiptNo %>" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="datefrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Bil & Belanja [Dari]:</label>
                                <input type="text" id="datefrom" class="date-picker form-control" name="datefrom" value="<%=sStartDate %>" />
                            </div>
                            <div class="form-group">
                                <label for="itemno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Kod Item:</label>
                                <select id="itemno" name="itemno" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                    <option></option>
                                    <%
                                        for (int i = 0; i < lsItem.Count; i++)
                                        {
                                            MainModel oItem = (MainModel)lsItem[i];
                                    %>
                                    <option value="<%=oItem.GetSetitemno %>" <%=sItemNo.Equals(oItem.GetSetitemno)?"selected":"" %>><%=oItem.GetSetitemno %></option>
                                    <%
                                        }
                                    %>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="expensesstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Bil & Belanja:</label>
                                <select id="expensesstatus" class="select2_single form-control" tabindex="-1" name="expensesstatus" style="width: 100%;">
                                    <option value="">-Select-</option>
                                    <option value="NEW" <%=sStatus.Equals("NEW")?"selected":"" %>>NEW</option>
                                    <option value="CONFIRMED" <%=sStatus.Equals("CONFIRMED")?"selected":"" %>>CONFIRMED</option>
                                    <option value="CANCELLED" <%=sStatus.Equals("CANCELLED")?"selected":"" %>>CANCELLED</option>
                                    <option value="IN-PROGRESS" <%=sStatus.Equals("IN-PROGRESS")?"selected":"" %>>IN-PROGRESS</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="dateto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Bil & Belanja [Hingga]:</label>
                                <input type="text" id="dateto" class="date-picker form-control" name="dateto" value="<%=sEndDate %>" />
                            </div>
                            <div class="form-group">
                                <label for="orderno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Pesanan:</label>
                                <input type="text" id="orderno" class="form-control" name="orderno" value="<%=sOrderNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="paymentstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Bayaran Belanja:</label>
                                <select id="paymentstatus" name="paymentstatus" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                    <option value="">-Select-</option>
                                    <option value="Y" <%=paymentStatus.Equals("Y")?"selected":"" %>>SELESAI</option>
                                    <option value="N" <%=paymentStatus.Equals("N")?"selected":"" %>>DALAM PROSES</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                            <div class="form-group">
                                <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                <div style="display: none;">
                                    <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                    <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                </div>
                            </div>
                        </div>
                    </form>

                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>No. Bil & Belanja</th>
                          <th>Jenis Bil & Belanja</th>
                          <th>Bayar Kepada</th>
                          <th>Tarikh Bil & Belanja</th>
                          <th>Pesanan & Penerimaan</th>
                          <th>Jumlah Bil & Belanja</th>
                          <th>No. Bayaran Belanja</th>
                          <th>Jumlah Bayaran Belanja</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsExpensesHeaderDetails.Count > 0)
                            {
                                for (int i = 0; i < lsExpensesHeaderDetails.Count; i++)
                                {
                                    MainModel modExpHdr = (MainModel)lsExpensesHeaderDetails[i];
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditexpenses('<%=modExpHdr.GetSetcomp %>','<%=modExpHdr.GetSetexpensesno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditexpenses('<%=modExpHdr.GetSetcomp %>','<%=modExpHdr.GetSetexpensesno %>');"><%=modExpHdr.GetSetexpensesno %></a></td>
                              <td><%=modExpHdr.GetSetexpensestype %></td>
                              <td><%=modExpHdr.GetSetbpdesc %></td>
                              <td><%=modExpHdr.GetSetexpensesdate %></td>
                              <td>
                                    <%
                                        if(modExpHdr.GetSetExpensesDetails.Count > 0) { 
                                    %>
                                            <table border="1" class="table">                                            
                                                <tr>
                                                    <th>Kod Item/ Keterangan</th>
                                                    <th>No. Pesanan</th>
                                                    <th>No. Penerimaan</th>
                                                    <th>Qty</th>
                                                    <th>Harga Bil</th>
                                                    <th>Jumlah TAX</th>
                                                    <th>Jumlah Bil</th>
                                                </tr>
                                    <%                                    
                                            for(int x = 0; x < modExpHdr.GetSetExpensesDetails.Count; x++)
                                            {
                                                MainModel modExpDet = (MainModel)modExpHdr.GetSetExpensesDetails[x];
                                    %>
                                                <tr>
                                                    <td><%=modExpDet.GetSetitemno %>/ <%=modExpDet.GetSetitemdesc %></td>
                                                    <td><%=modExpDet.GetSetorderno %></td>
                                                    <td><%=modExpDet.GetSetreceiptno %></td>
                                                    <td><%=modExpDet.GetSetquantity %></td>
                                                    <td><%=modExpDet.GetSetexpensesprice %></td>
                                                    <td><%=modExpDet.GetSettaxamount %></td>
                                                    <td><%=modExpDet.GetSettotalexpenses %></td>
                                                </tr>
                                    <%
                                            }
                                    %>
                                            </table>
                                    <%
                                        }
                                    %>
                              </td>
                              <td><%=modExpHdr.GetSettotalamount %></td>
                              <td>
                                    <%         
                                        String tempStr = "";
                                        for (int y = 0; y < modExpHdr.GetSetPaymentDetails.Count; y++)
                                        {
                                            MainModel modPayDet = (MainModel)modExpHdr.GetSetPaymentDetails[y];
                                            if (y == 0)
                                            {
                                                tempStr = modPayDet.GetSetpaypaidno;
                                            }
                                            else
                                            {
                                                tempStr = tempStr + ", " + modPayDet.GetSetpaypaidno;
                                            }
                                        }
                                    %>
                                    <%= tempStr%>
                              </td>
                              <td><%=modExpHdr.GetSetpaypaidamount %></td>
                              <td><%=modExpHdr.GetSetstatus %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td colspan="10">Rekod tiada...</td>
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
        function openeditexpenses(comp,expensesno) {
            var popupWindow = window.open("ExpensesDetails.aspx?action=OPEN&comp=" + comp + "&expensesno=" + expensesno, "open_expenses", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        $(document).ready(function () {
            $('#datefrom').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });
            $('#dateto').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });
        });

    </script>
</asp:Content>

