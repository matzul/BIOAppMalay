<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="PaymentStatement.aspx.cs" Inherits="PaymentStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penyata Bayaran <small>MAKLUMAT</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">

                  <form id="Form1" runat="server">

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
                          <i class="fa fa-print blue"></i>Cetak Penyata Bayaran
                        </a>
                    </div>
                    <table class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th></th>
                          <th>Tarikh Sah</th>
                          <th>Keterangan</th>
                          <th>Tarikh Bayaran</th>
                          <th style="text-align:right;">Bank</th>
                          <th style="text-align:right;">Tunai Ditangan</th>
                          <th style="text-align:right;">Jumlah</th>
                        </tr>
                      </thead>

                      <tbody>
                            <tr>
                                <td colspan="5"><h4 class="blue">BAYARAN TERIMA <i class="fa fa-plus-square green"></i></h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalpaymentreceipt">0.00</h4></td>
                            </tr>
                            <%
                                if (lsPayRcptHeaderDetails.Count > 0) {
                                    for (int i = 0; i < lsPayRcptHeaderDetails.Count; i++) 
                                    {
                                        MainModel oPayRcptDet = (MainModel)lsPayRcptHeaderDetails[i];
                                        oModCashFlow.GetSetbankpaymentreceiptamount = oModCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                                        oModCashFlow.GetSetcashpaymentreceiptamount = oModCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oPayRcptDet.GetSetpaymentconfirmeddate%></td>
                                <td><%=oPayRcptDet.GetSetpaymentno %><br /><%=oPayRcptDet.GetSetpaymenttype%><br /><%=oPayRcptDet.GetSetbpdesc%><br /><%=oPayRcptDet.GetSetpaydetno%> [<%=oPayRcptDet.GetSetpayrefno%>]</td>
                                <td><%=oPayRcptDet.GetSetpaymentdate%></td>
                                <td style="text-align:right;"><%=oPayRcptDet.GetSetpaytype.Equals("CASH")?"":String.Format("{0:#,##0.00}",oPayRcptDet.GetSetpayamount)%></td>
                                <td style="text-align:right;"><%=oPayRcptDet.GetSetpaytype.Equals("CASH")?String.Format("{0:#,##0.00}",oPayRcptDet.GetSetpayamount):""%></td>
                                <td></td>
                            </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="5"><h4 class="red">BAYARAN BELANJA <i class="fa fa-minus-square red"></i></h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalpaymentpaid">0.00</h4></td>
                            </tr>
                            <%
                                if (lsPayPaidHeaderDetails.Count > 0) { 
                                    for (int i = 0; i < lsPayPaidHeaderDetails.Count; i++) 
                                    {
                                        MainModel oPayPaidDet = (MainModel)lsPayPaidHeaderDetails[i];
                                        oModCashFlow.GetSetbankpaymentpaidamount = oModCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                                        oModCashFlow.GetSetcashpaymentpaidamount = oModCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oPayPaidDet.GetSetpaymentconfirmeddate%></td>
                                <td><%=oPayPaidDet.GetSetpaymentno %><br /><%=oPayPaidDet.GetSetpaymenttype%><br /><%=oPayPaidDet.GetSetbpdesc%><br /><%=oPayPaidDet.GetSetpaydetno%> [<%=oPayPaidDet.GetSetpayrefno%>]</td>
                                <td><%=oPayPaidDet.GetSetpaymentdate%></td>
                                <td style="text-align:right;"><%=oPayPaidDet.GetSetpaytype.Equals("CASH")?"":String.Format("{0:#,##0.00}",oPayPaidDet.GetSetpayamount)%></td>
                                <td style="text-align:right;"><%=oPayPaidDet.GetSetpaytype.Equals("CASH")?String.Format("{0:#,##0.00}",oPayPaidDet.GetSetpayamount):""%></td>
                                <td></td>
                            </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td></td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <%
                                //calculate total & closing amount
                                oModCashFlow.GetSetbankclosingamount = oModCashFlow.GetSetbankpaymentreceiptamount - oModCashFlow.GetSetbankpaymentpaidamount;
                                oModCashFlow.GetSetcashclosingamount = oModCashFlow.GetSetcashpaymentreceiptamount - oModCashFlow.GetSetcashpaymentpaidamount;
                                
                            %>
                            <tr>
                                <td colspan="5"><h4 class="green">LEBIHAN BAYARAN</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="blue"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankclosingamount + oModCashFlow.GetSetcashclosingamount)%></h4></td>
                            </tr>
                      </tbody>
                    </table>
                    <div style="display: none;">
                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
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

            $('#totalpaymentreceipt').text('<%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankpaymentreceiptamount+oModCashFlow.GetSetcashpaymentreceiptamount)%>');
            $('#totalpaymentpaid').text('<%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankpaymentpaidamount+oModCashFlow.GetSetcashpaymentpaidamount)%>');

        });

    </script>
</asp:Content>

