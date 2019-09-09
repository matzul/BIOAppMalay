<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="IncomeStatetement.aspx.cs" Inherits="IncomeStatetement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penyata Pendapatan <small>MAKLUMAT</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">

                  <form id="search" runat="server">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form1">
                            <label for="datefrom">Tarikh Dari: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="datefrom" class="date-picker form-control" name="datefrom" required="required" value="<%=sDateFrom %>"/>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="dateto">Tarikh Hingga: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="dateto" class="date-picker form-control" name="dateto" required="required" value="<%=sDateTo %>"/>
                        </div>
                    </div>
                  
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                    <button type="button" id="btnSearch" onclick="actionclick('OPEN');" class="btn btn-primary">Submit</button>
                                    <%
                                        MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
                                        if (oAlerMssg.GetSetalertstatus.Equals("SUCCESS")) { 
                                    %>
                                            <div class="alert alert-success alert-dismissible fade in" role="alert">
                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                                <strong>Success!</strong> <%=oAlerMssg.GetSetalertmessage %>
                                            </div>
                                    <%
                                        }
                                        else if (oAlerMssg.GetSetalertstatus.Equals("ERROR")) 
                                        { 
                                    %>
                                            <div class="alert alert-danger alert-dismissible fade in" role="alert">
                                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                                <strong>Error!</strong> <%=oAlerMssg.GetSetalertmessage %>
                                            </div>
                                    <%
                                        }
                                        //to reset alertmessage
                                        sAlertMessage = "";
                                    %>
                                </div>
                            </div>
                        </section>
                    </div>
                  
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <a id="printincomestatement" name="printincomestatement" class="btn btn-app" onclick="openprintpage();">
                          <i class="fa fa-print blue"></i>Cetak Penyata Pendapatan
                        </a>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <table class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th></th>
                          <th>Tarikh Transaksi</th>
                          <th>Jenis Transaksi</th>
                          <th>No. Daftar</th>
                          <th>Tarikh Daftar</th>
                          <th>Keterangan/ Catatan</th>
                          <th style="text-align:right;">Jumlah Invois/ Bil</th>
                          <th style="text-align:right;">Jumlah TAX</th>
                          <th style="text-align:right;">Jumlah Harga</th>
                          <th style="text-align:right;">Jumlah Total</th>
                        </tr>
                      </thead>

                      <tbody>
                            <tr>
                                <td colspan="9"><h4 class="orange">PENDAPATAN / HASIL</h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="orange" id="totalrevenueamount">0.00</h4></td>
                            </tr>
                            <%
                                totalrevenueamount = 0;
                                if (lsRevenueHeader.Count > 0)
                                {
                            %>
                            <%
                                        for (int i = 0; i < lsRevenueHeader.Count; i++) 
                                        {
                                            MainModel oRevenue = (MainModel)lsRevenueHeader[i];
                                            totalrevenueamount = totalrevenueamount + oRevenue.GetSettotalamount;
                                %>
                                        <tr>
                                            <td></td>
                                            <td><i class="glyphicon glyphicon-play"></i></td>
                                            <td><%=oRevenue.GetSetconfirmeddate%></td>
                                            <td><%=oRevenue.GetSetinvoicecat%></td>
                                            <td><%=oRevenue.GetSetinvoiceno%></td>
                                            <td><%=oRevenue.GetSetinvoicedate%></td>
                                            <td>INV [<%=oRevenue.GetSetinvoicetype%>]: <%=oRevenue.GetSetbpdesc %><br />HUBUNGI: <%=oRevenue.GetSetbpcontact%></td>
                                            <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oRevenue.GetSetinvoiceamount)%></td>
                                            <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oRevenue.GetSettaxamount)%></td>
                                            <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oRevenue.GetSettotalamount)%></td>
                                            <td></td>
                                        </tr>
                                <%
                                        }
                                %>

                            <%
                                }else{

                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="9"><h4 class="blue">KOS JUALAN (STOK / INVENTORI) <i class="fa fa-plus-square green"></i></h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalinventoryamount">0.00</h4></td>
                            </tr>
                            <%
                                totalinventoryamount = 0;
                                if (lsStockTransHeader.Count > 0)
                                {
                                    for (int i = 0; i < lsStockTransHeader.Count; i++) 
                                    {
                                        var oStockTrans = (MainModel)lsStockTransHeader[i];
                                        totalinventoryamount = totalinventoryamount + oStockTrans.GetSettotalamount;
                            %>
                                    <tr>
                                        <td></td>
                                        <td><i class="glyphicon glyphicon-play"></i></td>
                                        <td><%=oStockTrans.GetSettransdate%></td>
                                        <td><%=oStockTrans.GetSettranstype%></td>
                                        <td><%=oStockTrans.GetSettransno%></td>
                                        <td></td>
                                        <td><%=oStockTrans.GetSetorderno%><%=oStockTrans.GetSetadjustmenttype%></td>
                                        <td style="text-align:right;"></td>
                                        <td style="text-align:right;"></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockTrans.GetSettotalamount)%></td>
                                        <td></td>
                                    </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="9"><h4 class="red">PERBELANJAAN <i class="fa fa-minus-square red"></i></h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalexpensesamount">0.00</h4></td>
                            </tr>
                            <%
                                totalexpensesamount = 0;
                                if (lsExpensesHeader.Count > 0)
                                {
                                    for (int i = 0; i < lsExpensesHeader.Count; i++) 
                                    {
                                        MainModel oExpenses = (MainModel)lsExpensesHeader[i];
                                        totalexpensesamount = totalexpensesamount + oExpenses.GetSettotalamount;
                            %>
                                    <tr>
                                        <td></td>
                                        <td><i class="glyphicon glyphicon-play"></i></td>
                                        <td><%=oExpenses.GetSetconfirmeddate%></td>
                                        <td><%=oExpenses.GetSetexpensescat%></td>
                                        <td><%=oExpenses.GetSetexpensesno%></td>
                                        <td><%=oExpenses.GetSetexpensesdate%></td>
                                        <td>PV [<%=oExpenses.GetSetexpensestype%>]: <%=oExpenses.GetSetbpdesc %><br />HUBUNGI: <%=oExpenses.GetSetbpcontact%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oExpenses.GetSetexpensesamount)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oExpenses.GetSettaxamount)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oExpenses.GetSettotalamount)%></td>
                                        <td></td>
                                    </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <%
                                //calculate total & statement amount
                                totalstatementamount = totalrevenueamount + totalinventoryamount - totalexpensesamount;                               
                            %>
                            <tr>
                                <td colspan="9"><h4 class="green">KEUNTUNGAN / KERUGIAN</h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="blue"><%=String.Format("{0:#,##0.00}",totalstatementamount)%></h4></td>
                            </tr>
                            <%
                                if (totalstatementamount > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">UNTUNG</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalstatementamount)%></td>
                                <td></td>
                            </tr>
                            <%
                                }
                                else if (totalstatementamount < 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">RUGI</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalstatementamount)%></td>
                                <td></td>
                            </tr>
                            <%
                                }
                                else
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="7">PULANG MODAL</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalstatementamount)%></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                      </tbody>
                    </table>
                    <div style="display: none;">
                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                    </div>
                    </div>
                  </form>
                  
                  </div>
                </div>
            </div>

    <script type="text/javascript">

        function actionclick(action) {
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        //enable & disable button
        $(document).ready(function () {

            $('#datefrom').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY HH:mm:ss",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                //actionclick('OPEN');
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#datefrom').change(function () {
                //actionclick('OPEN');
            });

            $('#dateto').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY HH:mm:ss",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                //actionclick('OPEN');
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#dateto').change(function () {
                //actionclick('OPEN');
            });

            $('#totalrevenueamount').text('<%=String.Format("{0:#,##0.00}",totalrevenueamount)%>'); 
            $('#totalinventoryamount').text('<%=String.Format("{0:#,##0.00}",totalinventoryamount)%>');
            $('#totalexpensesamount').text('<%=String.Format("{0:#,##0.00}",totalexpensesamount)%>');

        });

        function viewstockstateSOH(comp, stockstateno) {
            var popupWindow = window.open("StockStateSOH.aspx?action=OPEN&comp=" + comp + "&stockstateno=" + stockstateno, "open_stockstateSOH", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

    </script>
</asp:Content>

