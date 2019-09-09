<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="GiveReport.aspx.cs" Inherits="GiveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Pesanan Agihan <small>LAPORAN</small></h2>
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
                    <form id="search" runat="server">
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="orderno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Pesanan:</label>
                                <input type="text" id="orderno" class="form-control" name="orderno" value="<%=sOrderNo %>" />
                            </div>
                            <div class="form-group">
                                <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Pelanggan:</label>
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
                                    <option value="DONE" <%=shipmentStatus.Equals("DONE")?"selected":"" %>>SELESAI</option>
                                    <option value="IN-PROGRESS" <%=shipmentStatus.Equals("IN-PROGRESS")?"selected":"" %>>DALAM PROSES</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="orderdatefrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Pesanan [Dari]:</label>
                                <input type="text" id="orderdatefrom" class="date-picker form-control" name="orderdatefrom" value="<%=sStartDate %>" />
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
                                    <option value="DONE" <%=invoiceStatus.Equals("DONE")?"selected":"" %>>SELESAI</option>
                                    <option value="IN-PROGRESS" <%=invoiceStatus.Equals("IN-PROGRESS")?"selected":"" %>>DALAM PROSES</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="form-group">
                                <label for="orderdateto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Tarikh Pesanan [Hingga]:</label>
                                <input type="text" id="orderdateto" class="date-picker form-control" name="orderdateto" value="<%=sEndDate %>" />
                            </div>
                            <div class="form-group">
                                <label for="orderstatus" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Status Pesanan:</label>
                                <select id="orderstatus" class="select2_single form-control" tabindex="-1" name="orderstatus" style="width: 100%;">
                                    <option value="">-Select-</option>
                                    <option value="NEW" <%=sStatus.Equals("NEW")?"selected":"" %>>NEW</option>
                                    <option value="CONFIRMED" <%=sStatus.Equals("CONFIRMED")?"selected":"" %>>CONFIRMED</option>
                                    <option value="CANCELLED" <%=sStatus.Equals("CANCELLED")?"selected":"" %>>CANCELLED</option>
                                    <option value="IN-PROGRESS" <%=sStatus.Equals("IN-PROGRESS")?"selected":"" %>>IN-PROGRESS</option>
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
                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                        <table id="datatable-buttons" class="table table-striped jambo_table">
                            <thead>
                                <tr class="headings">
                                    <th></th>
                                    <th>No. Pesanan</th>
                                    <th>Kategori</th>
                                    <th>Jenis Pesanan</th>
                                    <th>Pelanggan</th>
                                    <th>Tarikh Pesanan</th>
                                    <th>Kod Item</th>
                                    <th>Qty Pesanan</th>
                                    <th>No. Penghantaran</th>
                                    <th>Qty Penghantaran</th>
                                    <th>Jumlah Pesanan</th>
                                    <th>No. Invois</th>
                                    <th>Jumlah Invois</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <script type="text/javascript">
                                //New method to get Shipment Information
                                function PageMethod(fn, paramArray, asyncFn) {
                                    var pagePath = window.location.pathname;
                                    var retValue = '';
                                    var paramList = '';

                                    if (paramArray.length > 0) {
                                        for (var i = 0; i < paramArray.length; i += 2) {
                                            if (paramList.length > 0) paramList += ',';
                                            paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                                        }
                                    }
                                    paramList = '{' + paramList + '}';
                                    //Call the page method
                                    $.ajax({
                                        type: "POST",
                                        url: pagePath + "/" + fn,
                                        contentType: "application/json; charset=utf-8",
                                        data: paramList,
                                        dataType: "json",
                                        success: function (data, textStatus, jqXHR) {
                                            console.log("succeededGetAccessabilityStatus: " + textStatus);
                                            resultJSON = JSON.parse(data.d);
                                            retValue = resultJSON.result;
                                        },
                                        timeout: 600000,
                                        error: function (jqXHR, textStatus, errorThrown) {
                                            console.log("failedGetAccessabilityStatus: " + textStatus);
                                            retValue = "";
                                        },
                                        async: asyncFn
                                    });

                                    return retValue;
                                }
                            </script>
                            <tbody>
                                <%
                                    if (lsOrderHeaderDetails.Count > 0)
                                    {
                                        for (int i = 0; i < lsOrderHeaderDetails.Count; i++)
                                        {
                                            MainModel modOrdHdrDet = (MainModel)lsOrderHeaderDetails[i];
                                %>
                                <tr class="even pointer">
                                    <td><a href="#" class="btn-link" onclick="openeditorder('<%=modOrdHdrDet.GetSetcomp %>','<%=modOrdHdrDet.GetSetorderno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                    <td><a href="#" class="btn-link" onclick="openeditorder('<%=modOrdHdrDet.GetSetcomp %>','<%=modOrdHdrDet.GetSetorderno %>');"><%=modOrdHdrDet.GetSetorderno %></a></td>
                                    <td><%=modOrdHdrDet.GetSetordercat %></td>
                                    <td><%=modOrdHdrDet.GetSetordertype %></td>
                                    <td><%=modOrdHdrDet.GetSetbpdesc %></td>
                                    <td><%=modOrdHdrDet.GetSetorderdate %></td>
                                    <td><%=modOrdHdrDet.GetSetitemno %></td>
                                    <td><%=modOrdHdrDet.GetSetquantity %></td>
                                    <td>
                                        <script type="text/javascript">
                                            var parameters = ["comp", "<%=modOrdHdrDet.GetSetcomp %>", "orderno", "<%=modOrdHdrDet.GetSetorderno %>", "itemno", "<%=modOrdHdrDet.GetSetitemno %>"];
                                            document.write(PageMethod("getShipmentNo", parameters, false));
                                        </script>
                                    </td>
                                    <td><%=modOrdHdrDet.GetSetdeliverqty %></td>
                                    <td><%=modOrdHdrDet.GetSettotalprice %></td>
                                    <td>
                                        <script type="text/javascript">
                                            var parameters = ["comp", "<%=modOrdHdrDet.GetSetcomp %>", "orderno", "<%=modOrdHdrDet.GetSetorderno %>", "itemno", "<%=modOrdHdrDet.GetSetitemno %>"];
                                            document.write(PageMethod("getInvoiceNo", parameters, false));
                                        </script>
                                    </td>
                                    <td><%=modOrdHdrDet.GetSetinvoiceamount %></td>
                                    <td><%=modOrdHdrDet.GetSetorderstatus %></td>
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

            function openeditorder(comp, orderno) {
                var popupWindow = window.open("OrderDetails.aspx?action=OPEN&comp=" + comp + "&orderno=" + orderno, "open_order", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
                $('#orderdatefrom').daterangepicker({
                    singleDatePicker: true,
                    format: 'DD-MM-YYYY',
                    calender_style: "picker_1"
                }, function (start, end, label) {
                    console.log(start.toISOString(), end.toISOString(), label);
                });
                $('#orderdateto').daterangepicker({
                    singleDatePicker: true,
                    format: 'DD-MM-YYYY',
                    calender_style: "picker_1"
                }, function (start, end, label) {
                    console.log(start.toISOString(), end.toISOString(), label);
                });
                /*
                $('#confirmdatefrom').daterangepicker({
                    singleDatePicker: true,
                    format: 'DD-MM-YYYY',
                    calender_style: "picker_1"
                }, function (start, end, label) {
                    console.log(start.toISOString(), end.toISOString(), label);
                });
                $('#confirmdateto').daterangepicker({
                    singleDatePicker: true,
                    format: 'DD-MM-YYYY',
                    calender_style: "picker_1"
                }, function (start, end, label) {
                    console.log(start.toISOString(), end.toISOString(), label);
                });
                */
            });


        </script>
</asp:Content>

