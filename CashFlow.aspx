<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CashFlow.aspx.cs" Inherits="CashFlow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Aliran Kewangan <small>MAKLUMAT</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">

                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form1">
                            <label for="cashflowno">No. Aliran Kewangan:</label>
                            <input type="text" id="cashflowno" class="form-control" readonly="readonly" name="cashflowno" value="<%=oModCashFlow.GetSetcashflowno %>" />
                            <label for="openingtype">Jenis Pembukaan:</label>
                            <input type="text" id="openingtype" class="form-control" readonly="readonly" name="openingtype" value="<%=oModCashFlow.GetSetopeningtype%>"/>                                    
                            <label for="openingdate">Tarikh Pembukaan:</label>
                            <input type="text" id="openingdate" class="form-control" readonly="readonly" name="openingdate" value="<%=oModCashFlow.GetSetopeningdate%>"/>                                    
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="status">Status:</label>
                            <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModCashFlow.GetSetstatus%>"/>
                            <label for="closingtype">Jenis Penutupan:</label>
                            <input type="text" id="closingtype" class="form-control" readonly="readonly" name="closingtype" value="<%=oModCashFlow.GetSetclosingtype%>"/>                                    
                            <label for="closingdate">Tarikh Penutupan:</label>
                            <input type="text" id="closingdate" class="form-control" readonly="readonly" name="closingdate" value="<%=oModCashFlow.GetSetclosingdate%>"/>                                    
                        </div>
                    </div>
                  
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                     
                                    <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();" >Close</button>
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
                        <a id="begincashflow" name="begincashflow" class="btn btn-app" data-toggle="modal" data-target=".modal-begincashflow">
                          <i class="fa fa-play <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"":oModCashFlow.GetSetstatus.Equals("CLOSED")?"":"green"%>"></i>Mula Aliran Kewangan
                        </a>
                        <a id="closecashflow" name="closecashflow" class="btn btn-app" data-toggle="modal" data-target=".modal-closecashflow">
                          <i class="fa fa-flag <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"orange":""%>"></i>Tutup Aliran Kewangan
                        </a>
                        <a id="bankdeposit" name="bankdeposit" class="btn btn-app" data-toggle="modal" data-target=".modal-bankdeposit">
                          <i class="glyphicon glyphicon-save <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"blue":""%>"></i>Deposit Bank
                        </a>
                        <a id="cashwithdrawal" name="cashwithdrawal" class="btn btn-app" data-toggle="modal" data-target=".modal-cashwithdrawal">
                          <i class="glyphicon glyphicon-open <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"blue":""%>"></i>Pengeluaran Tunai
                        </a>
                        <a id="printcashflow" name="printcashflow" class="btn btn-app" onclick="openprintpage();">
                          <i class="fa fa-print <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"blue":oModCashFlow.GetSetstatus.Equals("CLOSED")?"blue":""%>"></i>Cetak 
                        </a>
                        <a id="endcashflow" name="endcashflow" class="btn btn-app" data-toggle="modal" data-target=".modal-endcashflow">
                          <i class="fa fa-stop <%=oModCashFlow.GetSetstatus.Equals("IN-PROGRESS")?"red":""%>"></i>Akhir Aliran Kewangan
                        </a>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <form id="search" runat="server">
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
                                <td colspan="5"><h4 class="orange">PEMBUKAAN BAKI</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="orange"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankopeningamount + oModCashFlow.GetSetcashopeningamount) %></h4></td>
                            </tr>
                            <%
                                if (oModCashFlow.GetSetcashflowno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModCashFlow.GetSetopeningdate %></td>
                                <td>BAKI BUKA</td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankopeningamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetcashopeningamount)%></td>
                                <td></td>
                            </tr>
                            <%
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
                                        if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                                        {
                                            oModCashFlow.GetSetbankpaymentreceiptamount = oModCashFlow.GetSetbankpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? 0 : oPayRcptDet.GetSetpayamount);
                                            oModCashFlow.GetSetcashpaymentreceiptamount = oModCashFlow.GetSetcashpaymentreceiptamount + (oPayRcptDet.GetSetpaytype.Equals("CASH") ? oPayRcptDet.GetSetpayamount : 0);
                                        }
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
                                        if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                                        {
                                            oModCashFlow.GetSetbankpaymentpaidamount = oModCashFlow.GetSetbankpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? 0 : oPayPaidDet.GetSetpayamount);
                                            oModCashFlow.GetSetcashpaymentpaidamount = oModCashFlow.GetSetcashpaymentpaidamount + (oPayPaidDet.GetSetpaytype.Equals("CASH") ? oPayPaidDet.GetSetpayamount : 0);
                                        }
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
                                if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
                                {
                                    oModCashFlow.GetSetbankclosingamount = oModCashFlow.GetSetbankopeningamount + oModCashFlow.GetSetbankpaymentreceiptamount - oModCashFlow.GetSetbankpaymentpaidamount;
                                    oModCashFlow.GetSetcashclosingamount = oModCashFlow.GetSetcashopeningamount + oModCashFlow.GetSetcashpaymentreceiptamount - oModCashFlow.GetSetcashpaymentpaidamount;
                                }
                                
                            %>
                            <tr>
                                <td colspan="5"><h4 class="green">PENUTUPAN BAKI</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="blue"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankclosingamount + oModCashFlow.GetSetcashclosingamount)%></h4></td>
                            </tr>
                            <%
                                if (oModCashFlow.GetSetcashflowno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><input type="text" id="closedate" class="date-picker form-control" name="closedate" required="required" readonly="readonly" value="<%=oModCashFlow.GetSetclosingdate.Trim().Length > 0 ? oModCashFlow.GetSetclosingdate.Trim() :sClosingDate%>"/><!--<i class="glyphicon glyphicon-calendar fa fa-calendar"></i>--></td>
                                <td>BAKI TUTUP</td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankclosingamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetcashclosingamount)%></td>
                                <td></td>
                            </tr>
                            <%
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
                      </tbody>
                    </table>
                    <div style="display: none;">
                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                        <input type="hidden" name="hidCashFlowNo" id="hidCashFlowNo" value="<%=oModCashFlow.GetSetcashflowno %>" />
                        <input type="hidden" name="hidBankOpeningAmount" id="hidBankOpeningAmount" value="<%=oModCashFlow.GetSetbankopeningamount %>" />
                        <input type="hidden" name="hidCashOpeningAmount" id="hidCashOpeningAmount" value="<%=oModCashFlow.GetSetcashopeningamount %>" />
                        <input type="hidden" name="hidBankPaymentReceiptAmount" id="hidBankPaymentReceiptAmount" value="<%=oModCashFlow.GetSetbankpaymentreceiptamount %>" />
                        <input type="hidden" name="hidCashPaymentReceiptAmount" id="hidCashPaymentReceiptAmount" value="<%=oModCashFlow.GetSetcashpaymentreceiptamount %>" />
                        <input type="hidden" name="hidBankPaymentPaidAmount" id="hidBankPaymentPaidAmount" value="<%=oModCashFlow.GetSetbankpaymentpaidamount %>" />
                        <input type="hidden" name="hidCashPaymentPaidAmount" id="hidCashPaymentPaidAmount" value="<%=oModCashFlow.GetSetcashpaymentpaidamount %>" />
                        <input type="hidden" name="hidBankClosingAmount" id="hidBankClosingAmount" value="<%=oModCashFlow.GetSetbankclosingamount %>" />
                        <input type="hidden" name="hidCashClosingAmount" id="hidCashClosingAmount" value="<%=oModCashFlow.GetSetcashclosingamount %>" />
                    </div>
                    <!-- BEGIN FOR DIALOG MODAL --> 
                    <div class="modal fade modal-begincashflow" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title green">Pilih Tarikh PERMULAAN Aliran Kewangan</h4>
                                </div>
                                <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh<span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">        
                                            <input type="text" id="begindate" class="date-picker form-control" name="begindate" readonly="readonly" required="required" value="<%=oModCashFlow.GetSetopeningdate.Trim().Length > 0 ? oModCashFlow.GetSetopeningdate.Trim() :sOpeningDate%>"/>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-warning" id="btnBeginCashFlow" onclick="actionclick('BEGINING');">Mula</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-endcashflow" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title red">Anda pasti untuk MENGAKHIRI Aliran Kewangan ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnEndCashFlow" onclick="actionclick('ENDING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-closecashflow" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title blue">Teruskan untuk MENUTUP Aliran Kewangan ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnCloseCashFlow" onclick="actionclick('CLOSING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-bankdeposit" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title blue">Masukkan TUNAI ke BANK</h4>
                            </div>
                            <div class="modal-body">
                                    <div class="form-horizontal form-label-left">

                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Daripada <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input type="text" id="bdfrombpdesc" name="bdfrombpdesc" readonly="readonly" required="required" class="form-control" value="<%=oModCashOnHand.GetSetbpdesc %>"/>
                                          <input type="hidden" id="bdfrombpid" name="bdfrombpid" class="form-control" value="<%=oModCashOnHand.GetSetbpid %>"/>
                                          <input type="hidden" id="bdfrombpaddress" name="bdfrombpaddress" class="form-control" value="<%=oModCashOnHand.GetSetbpaddress %>"/>
                                          <input type="hidden" id="bdfrombpcontact" name="bdfrombpcontact" class="form-control" value="<%=oModCashOnHand.GetSetbpcontact %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kepada <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input type="text" id="bdtobpdesc" name="bdtobpdesc" readonly="readonly" required="required" class="form-control" value="<%=oModBankOfAccount.GetSetbpdesc %>"/>
                                          <input type="hidden" id="bdtobpid" name="bdtobpid" class="form-control" value="<%=oModBankOfAccount.GetSetbpid %>"/>
                                          <input type="hidden" id="bdtobpaddress" name="bdtobpaddress" class="form-control" value="<%=oModBankOfAccount.GetSetbpaddress %>"/>
                                          <input type="hidden" id="bdtobpcontact" name="bdtobpcontact" class="form-control" value="<%=oModBankOfAccount.GetSetbpcontact %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="bddate" name="bddate" type="text" class="form-control" required="required" readonly="readonly" value="<%=DateTime.Now.ToString("dd-MM-yyyy") %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="bdtype" name="bdtype" type="text" class="form-control" required="required" readonly="readonly" value="<%=oModBankDepositParam.GetSetparamtype %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="bditem" name="bditem" type="text" class="form-control" required="required" readonly="readonly" value="<%=oModBankDepositParam.GetSetparamdesc %>"/>
                                            <input id="bditemid" name="bditemid" type="hidden" class="form-control" value="<%=oModBankDepositParam.GetSetparamid %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="bddesc" name="bddesc" type="text" class="form-control" required="required" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="bdamount" name="bdamount" type="text" class="form-control" required="required" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis Bayaran (OUT/IN)</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="bdpaytypeout" name="bdpaytypeout" type="text" class="form-control" required="required" readonly="readonly" value="CASH"/>
                                          <input id="bdpaytypein" name="bdpaytypein" type="text" class="form-control" required="required" readonly="readonly" value="BANKING"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Rujukan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="bdpayrefno" name="bdpayrefno" type="text" class="form-control" required="required" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Catatan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="bdpayremarks" name="bdpayremarks" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                    </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnSubmitBankDeposit" onclick="actionclick('BANK_DEPOSIT');">Submit</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-cashwithdrawal" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title blue">Keluarkan TUNAI dari BANK</h4>
                            </div>
                            <div class="modal-body">
                                    <div class="form-horizontal form-label-left">

                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Daripada <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input type="text" id="cwfrombpdesc" name="cwfrombpdesc" readonly="readonly" required="required" class="form-control" value="<%=oModBankOfAccount.GetSetbpdesc %>"/>
                                          <input type="hidden" id="cwfrombpid" name="cwfrombpid" class="form-control" value="<%=oModBankOfAccount.GetSetbpid %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kepada <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input type="text" id="cwtobpdesc" name="cwtobpdesc" readonly="readonly" required="required" class="form-control" value="<%=oModCashOnHand.GetSetbpdesc %>"/>
                                          <input type="hidden" id="cwtobpid" name="cwtobpid" class="form-control" value="<%=oModCashOnHand.GetSetbpid %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="cwdate" name="cwdate" type="text" class="form-control" required="required" readonly="readonly" value="<%=DateTime.Now.ToString("dd-MM-yyyy") %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="cwtype" name="cwtype" type="text" class="form-control" required="required" readonly="readonly" value="<%=oModCashWithdrawalParam.GetSetparamtype %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="cwitem" name="cwitem" type="text" class="form-control" required="required" readonly="readonly" value="<%=oModCashWithdrawalParam.GetSetparamdesc %>"/>
                                            <input id="cwitemid" name="cwitemid" type="hidden" class="form-control" value="<%=oModCashWithdrawalParam.GetSetparamid %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="cwdesc" name="cwdesc" type="text" class="form-control" required="required" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="cwamount" name="cwamount" type="text" class="form-control" required="required" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis Bayaran (OUT/IN)</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="cwpaytypeout" name="cwpaytypeout" type="text" class="form-control" required="required" readonly="readonly" value="BANKING"/>
                                          <input id="cwpaytypein" name="cwpaytypein" type="text" class="form-control" required="required" readonly="readonly" value="CASH"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Rujukan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="cwpayrefno" name="cwpayrefno" type="text" class="form-control" required="required" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Catatan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="cwpayremarks" name="cwpayremarks" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                    </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnSubmitCashWithdrawal" onclick="actionclick('CASH_WITHDRAWAL');">Submit</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <!-- END FOR DIALOG MODAL --> 
                    </form>
                    </div>
                  </div>
                </div>
            </div>

    <script type="text/javascript">

        function actionclick(action) {
            if (action == 'OPEN') {
                $('#begindate').removeAttr('required');
                $('#closedate').removeAttr('required');

                $('#bdfrombpdesc').removeAttr('required');
                $('#bdtobpdesc').removeAttr('required');
                $('#bddate').removeAttr('required');
                $('#bdtype').removeAttr('required');
                $('#bditem').removeAttr('required');
                $('#bddesc').removeAttr('required');
                $('#bdamount').removeAttr('required');
                $('#bdpaytypeout').removeAttr('required');
                $('#bdpaytypein').removeAttr('required');
                $('#bdpayrefno').removeAttr('required');

                $('#cwfrombpdesc').removeAttr('required');
                $('#cwtobpdesc').removeAttr('required');
                $('#cwdate').removeAttr('required');
                $('#cwtype').removeAttr('required');
                $('#cwitem').removeAttr('required');
                $('#cwdesc').removeAttr('required');
                $('#cwamount').removeAttr('required');
                $('#cwpaytypeout').removeAttr('required');
                $('#cwpaytypein').removeAttr('required');
                $('#cwpayrefno').removeAttr('required');
            }
            else if (action == 'BEGINING')
            {
                $('#closedate').removeAttr('required');

                $('#bdfrombpdesc').removeAttr('required');
                $('#bdtobpdesc').removeAttr('required');
                $('#bddate').removeAttr('required');
                $('#bdtype').removeAttr('required');
                $('#bditem').removeAttr('required');
                $('#bddesc').removeAttr('required');
                $('#bdamount').removeAttr('required');
                $('#bdpaytypeout').removeAttr('required');
                $('#bdpaytypein').removeAttr('required');
                $('#bdpayrefno').removeAttr('required');

                $('#cwfrombpdesc').removeAttr('required');
                $('#cwtobpdesc').removeAttr('required');
                $('#cwdate').removeAttr('required');
                $('#cwtype').removeAttr('required');
                $('#cwitem').removeAttr('required');
                $('#cwdesc').removeAttr('required');
                $('#cwamount').removeAttr('required');
                $('#cwpaytypeout').removeAttr('required');
                $('#cwpaytypein').removeAttr('required');
                $('#cwpayrefno').removeAttr('required');
            }
            else if (action == 'CLOSING' || action == 'ENDING')
            {
                $('#begindate').removeAttr('required');

                $('#bdfrombpdesc').removeAttr('required');
                $('#bdtobpdesc').removeAttr('required');
                $('#bddate').removeAttr('required');
                $('#bdtype').removeAttr('required');
                $('#bditem').removeAttr('required');
                $('#bddesc').removeAttr('required');
                $('#bdamount').removeAttr('required');
                $('#bdpaytypeout').removeAttr('required');
                $('#bdpaytypein').removeAttr('required');
                $('#bdpayrefno').removeAttr('required');

                $('#cwfrombpdesc').removeAttr('required');
                $('#cwtobpdesc').removeAttr('required');
                $('#cwdate').removeAttr('required');
                $('#cwtype').removeAttr('required');
                $('#cwitem').removeAttr('required');
                $('#cwdesc').removeAttr('required');
                $('#cwamount').removeAttr('required');
                $('#cwpaytypeout').removeAttr('required');
                $('#cwpaytypein').removeAttr('required');
                $('#cwpayrefno').removeAttr('required');
            }
            else if (action == 'BANK_DEPOSIT')
            {
                $('#closedate').removeAttr('required');
                $('#begindate').removeAttr('required');

                $('#cwfrombpdesc').removeAttr('required');
                $('#cwtobpdesc').removeAttr('required');
                $('#cwdate').removeAttr('required');
                $('#cwtype').removeAttr('required');
                $('#cwitem').removeAttr('required');
                $('#cwdesc').removeAttr('required');
                $('#cwamount').removeAttr('required');
                $('#cwpaytypeout').removeAttr('required');
                $('#cwpaytypein').removeAttr('required');
                $('#cwpayrefno').removeAttr('required');
            }
            else if (action == 'CASH_WITHDRAWAL')
            {
                $('#closedate').removeAttr('required');
                $('#begindate').removeAttr('required');

                $('#bdfrombpdesc').removeAttr('required');
                $('#bdtobpdesc').removeAttr('required');
                $('#bddate').removeAttr('required');
                $('#bdtype').removeAttr('required');
                $('#bditem').removeAttr('required');
                $('#bddesc').removeAttr('required');
                $('#bdamount').removeAttr('required');
                $('#bdpaytypeout').removeAttr('required');
                $('#bdpaytypein').removeAttr('required');
                $('#bdpayrefno').removeAttr('required');
            }
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        //enable & disable button
        $(document).ready(function () {

            $('#begincashflow').prop('disabled', true);
            $('#endcashflow').prop('disabled', true);
            $('#closecashflow').prop('disabled', true);
            $('#bankdeposit').prop('disabled', true);
            $('#cashwithdrawal').prop('disabled', true);

            $('#begincashflow').attr('disabled', 'disabled');
            $('#endcashflow').attr('disabled', 'disabled');
            $('#closecashflow').attr('disabled', 'disabled');
            $('#bankdeposit').attr('disabled', 'disabled');
            $('#cashwithdrawal').attr('disabled', 'disabled');

            <%
            if (oModCashFlow.GetSetstatus.Equals("IN-PROGRESS"))
            {
            %>
                //$('#begincashflow').prop('disabled', false);
                $('#endcashflow').prop('disabled', false);
                $('#closecashflow').prop('disabled', false);
                $('#bankdeposit').prop('disabled', false);
                $('#cashwithdrawal').prop('disabled', false);

                //$('#begincashflow').removeAttr('disabled');
                $('#endcashflow').removeAttr('disabled');
                $('#closecashflow').removeAttr('disabled');
                $('#bankdeposit').removeAttr('disabled');
                $('#cashwithdrawal').removeAttr('disabled');
            <%
            }
            else if (oModCashFlow.GetSetstatus.Equals("CLOSED"))
            { 
            %>
                $('#begincashflow').prop('disabled', true);
                $('#endcashflow').prop('disabled', true);
                $('#closecashflow').prop('disabled', true);
                $('#bankdeposit').prop('disabled', true);
                $('#cashwithdrawal').prop('disabled', true);

                $('#begincashflow').attr('disabled', 'disabled');
                $('#endcashflow').attr('disabled', 'disabled');
                $('#closecashflow').attr('disabled', 'disabled');
                $('#bankdeposit').attr('disabled', 'disabled');
                $('#cashwithdrawal').attr('disabled', 'disabled');
            <%
            }
            else
            {
            %>
                $('#begincashflow').prop('disabled', false);
                //$('#endcashflow').prop('disabled', false);
                //$('#closecashflow').prop('disabled', false);
                //$('#bankdeposit').prop('disabled', false);
                //$('#cashwithdrawal').prop('disabled', false);

                $('#begincashflow').removeAttr('disabled');
                //$('#endcashflow').removeAttr('disabled');
                //$('#closecashflow').removeAttr('disabled');
                //$('#bankdeposit').removeAttr('disabled');
                //$('#cashwithdrawal').removeAttr('disabled');
            <%
            }
            %>

            $('#begindate').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: 'DD-MM-YYYY HH:mm:ss',
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#closedate').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY HH:mm:ss",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
                actionclick('OPEN');
            });

            $('#totalpaymentreceipt').text('<%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankpaymentreceiptamount+oModCashFlow.GetSetcashpaymentreceiptamount)%>');
            $('#totalpaymentpaid').text('<%=String.Format("{0:#,##0.00}",oModCashFlow.GetSetbankpaymentpaidamount+oModCashFlow.GetSetcashpaymentpaidamount)%>');

            $('#closedate').change(function () {
                //actionclick('OPEN');
            });

            <%
                if(oModCashFlow.GetSetstatus.Equals("CLOSED"))
                { 
            %>
                    $('#closedate').prop('disabled', true);
            <%
                }
                else 
                { 
            %>
                    $('#closedate').prop('disabled', false);
            <%
                }
            %>
        });

    </script>
</asp:Content>

