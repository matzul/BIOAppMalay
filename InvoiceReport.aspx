<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="InvoiceReport.aspx.cs" Inherits="InvoiceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">

            <div class="x_content">
                <div class="">
                    <form id="Form1" runat="server">
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="invoiceno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Invois:</label>
                                <input type="text" id="invoiceno" class="form-control" name="invoiceno" value="<%=sInvoiceNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Invois Kepada:</label>
                                <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" style="width: 100%;">
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
                                <label for="shipmentno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penghantaran:</label>
                                <input type="text" id="shipmentno" class="form-control" name="shipmentno" value="<%=sShipmentNo %>" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="datefrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Invois [Dari]:</label>
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
                                <label for="invoicestatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Invois:</label>
                                <select id="invoicestatus" class="select2_single form-control" tabindex="-1" name="invoicestatus" style="width: 100%;">
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
                                <label for="dateto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Invois [Hingga]:</label>
                                <input type="text" id="dateto" class="date-picker form-control" name="dateto" value="<%=sEndDate %>" />
                            </div>
                            <div class="form-group">
                                <label for="orderno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Pesanan:</label>
                                <input type="text" id="orderno" class="form-control" name="orderno" value="<%=sOrderNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="paymentstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Bayaran Terimaan:</label>
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
                                <th>No. Invois</th>
                                <th>Jenis Invois</th>
                                <th>Invois Kepada</th>
                                <th>Tarikh Invois</th>
                                <th>Pesanan & Penghantaran</th>
                                <th>Jumlah Invois</th>
                                <th>No. Bayaran Terima</th>
                                <th>Jumlah Bayaran</th>
                                <th>Status</th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsInvoiceHeaderDetails.Count > 0)
                                {
                                    for (int i = 0; i < lsInvoiceHeaderDetails.Count; i++)
                                    {
                                        MainModel modInvHdr = (MainModel)lsInvoiceHeaderDetails[i];
                            %>
                            <tr>
                                <td><a href="#" class="btn-link" onclick="openeditinvoice('<%=modInvHdr.GetSetcomp %>','<%=modInvHdr.GetSetinvoiceno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                <td><a href="#" class="btn-link" onclick="openeditinvoice('<%=modInvHdr.GetSetcomp %>','<%=modInvHdr.GetSetinvoiceno %>');"><%=modInvHdr.GetSetinvoiceno %></a></td>
                                <td><%=modInvHdr.GetSetinvoicetype %></td>
                                <td><%=modInvHdr.GetSetbpdesc %></td>
                                <td><%=modInvHdr.GetSetinvoicedate %></td>
                                <td>
                                    <%
                                        if(modInvHdr.GetSetInvoiceDetails.Count > 0) { 
                                    %>
                                            <table border="1" class="table">                                            
                                                <tr>
                                                    <th>Kod Item/ Keterangan</th>
                                                    <th>No. Pesanan</th>
                                                    <th>No. Penghantaran</th>
                                                    <th>Qty</th>
                                                    <th>Harga Invois</th>
                                                    <th>Jumlah TAX</th>
                                                    <th>Jumlah Invois</th>
                                                </tr>
                                    <%                                    
                                            for(int x = 0; x < modInvHdr.GetSetInvoiceDetails.Count; x++)
                                            {
                                                MainModel modInvDet = (MainModel)modInvHdr.GetSetInvoiceDetails[x];
                                    %>
                                                <tr>
                                                    <td><%=modInvDet.GetSetitemno %>/ <%=modInvDet.GetSetitemdesc %></td>
                                                    <td><%=modInvDet.GetSetorderno %></td>
                                                    <td><%=modInvDet.GetSetshipmentno %></td>
                                                    <td><%=modInvDet.GetSetquantity %></td>
                                                    <td><%=modInvDet.GetSetinvoiceprice %></td>
                                                    <td><%=modInvDet.GetSettaxamount %></td>
                                                    <td><%=modInvDet.GetSettotalinvoice %></td>
                                                </tr>
                                    <%
                                            }
                                    %>
                                            </table>
                                    <%
                                        }
                                    %>
                                </td>
                                <td><%=modInvHdr.GetSettotalamount %></td>
                                <td>
                                    <%         
                                        String tempStr = "";
                                        for (int y = 0; y < modInvHdr.GetSetPaymentDetails.Count; y++)
                                        {
                                            MainModel modPayDet = (MainModel)modInvHdr.GetSetPaymentDetails[y];
                                            if (y == 0)
                                            {
                                                tempStr = modPayDet.GetSetpayrcptno;
                                            }
                                            else
                                            {
                                                tempStr = tempStr + ", " + modPayDet.GetSetpayrcptno;
                                            }
                                        }
                                    %>
                                    <%= tempStr%>
                                </td>
                                <td><%=modInvHdr.GetSetpayrcptamount %></td>
                                <td><%=modInvHdr.GetSetstatus %></td>
                            </tr>
                            <% 
                                    }
                                }
                                else
                                {
                            %>
                            <tr>
                                <td></td>
                                <td>Tiada rekod...</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
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
        function openeditinvoice(comp, invoiceno) {
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

