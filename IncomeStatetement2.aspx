﻿<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="IncomeStatetement2.aspx.cs" Inherits="IncomeStatetement2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <style type="text/css">
            /*                        
            .page-header, .page-header-space {
              height: 100px;
            }

            .page-footer, .page-footer-space {
              height: 50px;

            }

            .page-footer {
              position: fixed;
              bottom: 0;
              width: 100%;
              border-top: 1px solid black;
              background: yellow; 
            }

            .page-header {
              position: fixed;
              top: 0mm;
              width: 100%;
              border-bottom: 1px solid black; 
              background: yellow; 
            }

            .page {
              page-break-after: always;
            }

            @page {
              margin: 20mm
            }

            @media print {
               thead {display: table-header-group;} 
               tfoot {display: table-footer-group;}
   
               button {display: none;}
   
               body {margin: 0;}
            }
            */

            /*
            @media print {
            thead { display: table-header-group; }
            tfoot { display: table-footer-group; }
            }
            @media screen {
            thead { display: block; }
            tfoot { display: block; }
            }
            */
        </style>

            <div id="printableArea" class="col-md-12 col-sm-12 col-xs-12">
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
                          <td></td>
                          <td></td>
                          <td>Bulan Transaksi</td>
                          <td>Jenis Transaksi</td>
                          <td>Kategori</td>
                          <td style="text-align:right;">Jumlah Invois/ Bil</td>
                          <td style="text-align:right;">Jumlah TAX</td>
                          <td style="text-align:right;">Jumlah Harga</td>
                          <td style="text-align:right;">Jumlah Total</td>
                        </tr>
                      </thead>
                      <tbody>
                            <tr>
                                <td colspan="7"><h4 class="dark">PEROLEHAN ASET & BAHAN MODAL</h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalassetprocurement" class="dark">0.00</h4></td>
                            </tr>
                            <%
                                totalassetcapitalamount = 0;
                                if (lsAssetCapitalHeader.Count > 0)
                                {
                                    for (int i = 0; i < lsAssetCapitalHeader.Count; i++) 
                                    {
                                        MainModel oAssetCapital = (MainModel)lsAssetCapitalHeader[i];
                                        totalassetcapitalamount = totalassetcapitalamount + oAssetCapital.GetSettotalamount;
                            %>
                                    <tr>
                                        <td></td>
                                        <td><i class="glyphicon glyphicon-play"></i></td>
                                        <td><%=oAssetCapital.GetSetconfirmeddate%></td>
                                        <td><%=oAssetCapital.GetSetexpensescat%></td>
                                        <td><%=oAssetCapital.GetSetexpensestype%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oAssetCapital.GetSetexpensesamount)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oAssetCapital.GetSettaxamount)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oAssetCapital.GetSettotalamount)%></td>
                                        <td></td>
                                    </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="5">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="7"><h4 class="green">PENDAPATAN / HASIL</h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="green" id="totalrevenueamount">0.00</h4></td>
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
                                            <td><%=oRevenue.GetSetinvoicetype%></td>
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
                                <td colspan="5">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="7"><h4 class="orange">KOS JUALAN (STOK / INVENTORI) <i class="fa fa-minus-square red"></i></h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalinventoryamount" class="orange">0.00</h4></td>
                            </tr>
                            <%
                                totalinventoryamount = totalopeningstockamount + totaladditionstockamount - totalclosingstockamount;
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=sDateFrom %></td>
                                <td>JUMLAH STOK / INVENTORI</td>
                                <td>[A] STOK AWALAN</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalopeningstockamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>JUMLAH STOK / INVENTORI</td>
                                <td>[B] STOK TAMBAHAN</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totaladditionstockamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=sDateTo %></td>
                                <td>JUMLAH STOK / INVENTORI</td>
                                <td>[C] STOK AKHIRAN</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalclosingstockamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>KOS STOK JUALAN</td>
                                <td>[A + B - C] COGS</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",totalinventoryamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="7"><h4 class="red">PERBELANJAAN <i class="fa fa-minus-square red"></i></h4></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalexpensesamount" class="red">0.00</h4></td>
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
                                        <td><%=oExpenses.GetSetexpensestype%></td>
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
                                <td colspan="5">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <%
                                //calculate total & statement amount
                                totalstatementamount = totalrevenueamount - totalinventoryamount - totalexpensesamount;                               
                            %>
                            <tr>
                                <td colspan="7"><h4 class="blue">KEUNTUNGAN / KERUGIAN</h4></td>
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
                                <td colspan="6">UNTUNG</td>
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
                                <td colspan="5">RUGI</td>
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
                                <td colspan="5">PULANG MODAL</td>
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
            $('#totalassetprocurement').text('<%=String.Format("{0:#,##0.00}",totalassetcapitalamount)%>');

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

        function openprintpage() {
            //$(".tableToPrint td, .tableToPrint th").each(function () { $(this).css("width", $(this).width() + "px") });
            //$(".tableToPrint tr").wrap("<div class='avoidBreak'></div>");
            //window.print();
            printPartOfPage();
        }

        function printPartOfPage() {
            //Works with Chome, Firefox, IE, Safari
            //Get the HTML of div
            var title = document.title;
            var divElements = document.getElementById('printableArea').innerHTML;
            var printWindow = window.open("", "_blank", "");
            //open the window
            printWindow.document.open();
            //write the html to the new window, link to css file
            printWindow.document.write('<html><head><title>' + title + '</title><link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css"><style> .topright { position: absolute; top: 5px; right: 5px; text-align: right; } </style></head><body>');
            printWindow.document.write('<center><div style="width:750px !important;height:100px; align:center;"><span style="text-align:center"><img src="../image/logoblack.jpg" /></span></div>');
            printWindow.document.write(divElements);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();

            setTimeout(function () {
                printWindow.print();
                printWindow.close();
            }, 100);
        }
    </script>
</asp:Content>

