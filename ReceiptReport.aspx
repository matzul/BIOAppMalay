<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ReceiptReport.aspx.cs" Inherits="ReceiptReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Penerimaan Pesanan <small>SENARAI</small></h2>
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
                                <label for="receiptno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penerimaan:</label>
                                <input type="text" id="receiptno" class="form-control" name="receiptno" value="<%=sReceiptNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Terima Daripada:</label>
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
                                <label for="receiptstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Penerimaan:</label>
                                <select id="receiptstatus" class="select2_single form-control" tabindex="-1" name="receiptstatus" style="width: 100%;">
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
                                <label for="datefrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Penerimaan [Dari]:</label>
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
                                <label for="expensesstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Bil:</label>
                                <select id="expensesstatus" class="select2_single form-control" tabindex="-1" name="expensesstatus" style="width: 100%;">
                                    <option value="">-Select-</option>
                                    <option value="Y" <%=expensesStatus.Equals("Y")?"selected":"" %>>SELESAI</option>
                                    <option value="N" <%=expensesStatus.Equals("N")?"selected":"" %>>DALAM PROSES</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="dateto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Penerimaan [Hingga]:</label>
                                <input type="text" id="dateto" class="date-picker form-control" name="dateto" value="<%=sEndDate %>" />
                            </div>
                            <div class="form-group">
                                <label for="orderno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Pesanan:</label>
                                <input type="text" id="orderno" class="form-control" name="orderno" value="<%=sOrderNo %>" />
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
                                <th>No. Penerimaan</th>
                                <th>Kategori</th>
                                <th>Terima Daripada</th>
                                <th>Tarikh Penerimaan</th>
                                <th>Kod Item</th>
                                <th>No. Pesanan</th>
                                <th>Qty Pesanan</th>
                                <th>Qty Penerimaan</th>
                                <th>Lokasi Stok</th>
                                <th>Terima Bil</th>
                                <th>Status</th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsReceiptDetailsHeader.Count > 0)
                                {
                                    for (int i = 0; i < lsReceiptDetailsHeader.Count; i++)
                                    {
                                        MainModel modHdrDet = (MainModel)lsReceiptDetailsHeader[i];
                            %>
                            <tr>
                                <td><a href="#" class="btn-link" onclick="openeditreceipt('<%=modHdrDet.GetSetcomp %>','<%=modHdrDet.GetSetreceiptno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                <td><a href="#" class="btn-link" onclick="openeditreceipt('<%=modHdrDet.GetSetcomp %>','<%=modHdrDet.GetSetreceiptno %>');"><%=modHdrDet.GetSetreceiptno %></a></td>
                                <td><%=modHdrDet.GetSetreceiptcat %></td>
                                <td><%=modHdrDet.GetSetbpdesc %></td>
                                <td><%=modHdrDet.GetSetreceiptdate %></td>
                                <td><%=modHdrDet.GetSetitemno %></td>
                                <td><%=modHdrDet.GetSetorderno %></td>
                                <td><%=modHdrDet.GetSetorder_quantity %></td>
                                <td><%=modHdrDet.GetSetreceipt_quantity %></td>
                                <td><%=modHdrDet.GetSetlocation %></td>
                                <td><%=modHdrDet.GetSethasbilling %></td>
                                <td><%=modHdrDet.GetSetstatus %></td>
                            </tr>
                            <% 
                                    }
                                }
                                else
                                {
                            %>
                            <tr>
                                <td></td>
                                <td>Rekod tiada...</td>
                                <td></td>
                                <td></td>
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
        function openeditreceipt(comp, receiptno) {
            var popupWindow = window.open("ReceiptDetails.aspx?action=OPEN&comp=" + comp + "&receiptno=" + receiptno, "open_receipt", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

