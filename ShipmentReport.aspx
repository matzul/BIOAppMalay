<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ShipmentReport.aspx.cs" Inherits="ShipmentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Penghantaran Pesanan <small>LAPORAN</small></h2>
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
                                <label for="shipmentno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penghantaran:</label>
                                <input type="text" id="shipmentno" class="form-control" name="shipmentno" value="<%=sShipmentNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Hantar Kepada:</label>
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
                                <label for="shipmentstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Penghantaran:</label>
                                <select id="shipmentstatus" class="select2_single form-control" tabindex="-1" name="shipmentstatus" style="width: 100%;">
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
                                <label for="datefrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Penghantaran [Dari]:</label>
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
                                    <option value="Y" <%=invoiceStatus.Equals("Y")?"selected":"" %>>SELESAI</option>
                                    <option value="N" <%=invoiceStatus.Equals("N")?"selected":"" %>>DALAM PROSES</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="dateto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Penghantaran [Hingga]:</label>
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
                                <th>No. Penghantaran</th>
                                <th>Kategori</th>
                                <th>Hantar Kepada</th>
                                <th>Tarikh Penghantaran</th>
                                <th>Kod Item</th>
                                <th>No. Pesanan</th>
                                <th>Qty Pesanan</th>
                                <th>Qty Penghantaran</th>
                                <th>Lokasi Stok</th>
                                <th>Sudah Invois</th>
                                <th>Status</th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsShipmentHeaderDetails.Count > 0)
                                {
                                    for (int i = 0; i < lsShipmentHeaderDetails.Count; i++)
                                    {
                                        MainModel modHdrDet = (MainModel)lsShipmentHeaderDetails[i];
                            %>
                            <tr>
                                <td><a href="#" class="btn-link" onclick="openeditshipment('<%=modHdrDet.GetSetcomp %>','<%=modHdrDet.GetSetshipmentno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                <td><a href="#" class="btn-link" onclick="openeditshipment('<%=modHdrDet.GetSetcomp %>','<%=modHdrDet.GetSetshipmentno %>');"><%=modHdrDet.GetSetshipmentno %></a></td>
                                <td><%=modHdrDet.GetSetshipmentcat %></td>
                                <td><%=modHdrDet.GetSetbpdesc %></td>
                                <td><%=modHdrDet.GetSetshipmentdate %></td>
                                <td><%=modHdrDet.GetSetitemno %></td>
                                <td><%=modHdrDet.GetSetorderno %></td>
                                <td><%=modHdrDet.GetSetorder_quantity %></td>
                                <td><%=modHdrDet.GetSetshipment_quantity %></td>
                                <td><%=modHdrDet.GetSetlocation %></td>
                                <td><%=modHdrDet.GetSethasinvoice %></td>
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
                                <td>Tiada rekod...</td>
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

        function openeditshipment(comp, shipmentno) {
            var popupWindow = window.open("ShipmentDetails.aspx?action=OPEN&comp=" + comp + "&shipmentno=" + shipmentno, "open_shipment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

