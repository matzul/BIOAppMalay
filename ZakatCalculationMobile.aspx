<%@ Page Title="" Language="C#" MasterPageFile="./MasterPageMobile.master" AutoEventWireup="true" CodeFile="ZakatCalculationMobile.aspx.cs" Inherits="ZakatCalculationMobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penyata Taksiran Zakat <small>MAKLUMAT</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">

                    <div class="col-md-6 col-sm-6 col-xs-6">
                        <div id="add-form1">
                            <label for="zakatcalculationno">No. Penyata Zakat:</label>
                            <input type="text" id="zakatcalculationno" class="form-control" readonly="readonly" name="zakatcalculationno" value="<%=oModZakatCalculation.GetSetzakatcalculationno %>" />
                            <label for="openingtype">Jenis Pembukaan:</label>
                            <input type="text" id="openingtype" class="form-control" readonly="readonly" name="openingtype" value="<%=oModZakatCalculation.GetSetopeningtype%>"/>                                    
                            <label for="openingdate">Tarikh Pembukaan:</label>
                            <input type="text" id="openingdate" class="form-control" readonly="readonly" name="openingdate" value="<%=oModZakatCalculation.GetSetopeningdate%>"/>                                    
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-6">
                        <div id="add-form2">
                            <label for="status">Status:</label>
                            <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModZakatCalculation.GetSetstatus%>"/>
                            <label for="closingtype">Jenis Penutupan:</label>
                            <input type="text" id="closingtype" class="form-control" readonly="readonly" name="closingtype" value="<%=oModZakatCalculation.GetSetclosingtype%>"/>                                    
                            <label for="closingdate">Tarikh Penutupan:</label>
                            <input type="text" id="closingdate" class="form-control" readonly="readonly" name="closingdate" value="<%=oModZakatCalculation.GetSetclosingdate%>"/>                                    
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
                        <a id="beginzakatcalculation" name="beginzakatcalculation" class="btn btn-app" data-toggle="modal" data-target=".modal-beginzakatcalculation">
                          <i class="fa fa-play <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"":oModZakatCalculation.GetSetstatus.Equals("CLOSED")?"":"green"%>"></i>Mula Penyata Taksiran Zakat
                        </a>
                        <a id="closezakatcalculation" name="closezakatcalculation" class="btn btn-app" data-toggle="modal" data-target=".modal-closezakatcalculation">
                          <i class="fa fa-flag <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"orange":""%>"></i>Tutup Penyata Taksiran Zakat
                        </a>
                        <a id="printzakatcalculation" name="printzakatcalculation" class="btn btn-app" onclick="openprintpage();">
                          <i class="fa fa-print <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"blue":oModZakatCalculation.GetSetstatus.Equals("CLOSED")?"blue":""%>"></i>Cetak 
                        </a>
                        <a id="endzakatcalculation" name="endzakatcalculation" class="btn btn-app" data-toggle="modal" data-target=".modal-endzakatcalculation">
                          <i class="fa fa-stop <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"red":""%>"></i>Akhir Penyata Taksiran Zakat
                        </a>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <form id="search" runat="server">
                    <table class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th></th>
                          <th>Tarikh</th>
                          <th>Keterangan</th>
                          <th style="text-align:right;">Bank</th>
                          <th style="text-align:right;">Tunai</th>
                          <th style="text-align:right;">Sub-Jumlah</th>
                          <th style="text-align:right;">Jumlah</th>
                        </tr>
                      </thead>

                      <tbody>
                            <tr>
                                <td colspan="4"><h4 class="green">ASET KEWANGAN</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetopeningdate %></td>
                                <td>Aset Kewangan Buka</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankopeningamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetcashopeningamount)%></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Bayaran Terima Terkumpul</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankpaymentreceiptamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetcashpaymentreceiptamount)%></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Bayaran Belanja Terkumpul</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankpaymentpaidamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetcashpaymentpaidamount)%></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                            </tr>
                            <%
                                //calculate total & closing amount
                                //To handle in cs file
                                /*
                                if (oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS"))
                                {
                                    oModZakatCalculation.GetSetbankclosingamount = oModZakatCalculation.GetSetbankopeningamount + oModZakatCalculation.GetSetbankpaymentreceiptamount - oModZakatCalculation.GetSetbankpaymentpaidamount;
                                    oModZakatCalculation.GetSetcashclosingamount = oModZakatCalculation.GetSetcashopeningamount + oModZakatCalculation.GetSetcashpaymentreceiptamount - oModZakatCalculation.GetSetcashpaymentpaidamount;
                                }
                                */
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetclosingdate.Trim().Length > 0 ? oModZakatCalculation.GetSetclosingdate.Trim() :sClosingDate%></td>
                                <td>Aset Kewangan Tutup</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankclosingamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetcashclosingamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankclosingamount + oModZakatCalculation.GetSetcashclosingamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Bayaran Belum Terima Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetpendingreceiptamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Bayaran Belum Belanja Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetpendingpaidamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4"><h4 class="green">JUMLAH ASET KEWANGAN</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankclosingamount + oModZakatCalculation.GetSetcashclosingamount + oModZakatCalculation.GetSetpendingreceiptamount - oModZakatCalculation.GetSetpendingpaidamount)%></h4></td>
                                <td style="text-align:right;"><h4 class="green"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankclosingamount + oModZakatCalculation.GetSetcashclosingamount + oModZakatCalculation.GetSetpendingreceiptamount - oModZakatCalculation.GetSetpendingpaidamount)%></h4></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4 class="blue">ASET STOK/ INVENTORI</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetopeningdate %></td>
                                <td>Aset Stok/ Inventori Buka</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockopeningamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Penambahan Stok/ Inventori Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockinamount)%></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Pengurangan Stok/ Inventori Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockoutamount)%></td>
                                <td></td>
                            </tr>
                            <%
                                //calculate total & closing amount
                                //To handle in cs file
                                /*
                                if (oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS"))
                                {
                                    oModZakatCalculation.GetSetstockclosingamount = oModZakatCalculation.GetSetstockopeningamount + oModZakatCalculation.GetSetstockinamount - oModZakatCalculation.GetSetstockoutamount;
                                }
                                */
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetclosingdate.Trim().Length > 0 ? oModZakatCalculation.GetSetclosingdate.Trim() :sClosingDate%></td>
                                <td>Aset Stok/ Inventori Tutup</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockclosingamount)%></td>
                                <td></td>
                            </tr>                            
                            <tr>
                                <td colspan="4"><h4 class="blue">TAMBAHAN: JUMLAH ASET STOK/ INVENTORI</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockclosingamount)%></h4></td>
                                <td style="text-align:right;"><h4 class="blue"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetstockclosingamount)%></h4></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4 class="red">PERLARASAN ASET SEMASA</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Penghutang Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetpendingreceiptamount)%></td>
                                <td></td>
                            </tr>
                              <%
                                  oModZakatCalculation.GetSetadjustmentsubtractionamount = oModZakatCalculation.GetSetpendingreceiptamount;
                                  for(int i=0; i<lsItemZakatSubstraction.Count; i++)
                                  {
                                      MainModel modItemZakat = (MainModel)lsItemZakatSubstraction[i];
                                      oModZakatCalculation.GetSetadjustmentsubtractionamount = oModZakatCalculation.GetSetadjustmentsubtractionamount + modItemZakat.GetSettotalamount;
                              %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td colspan="3"><%=modItemZakat.GetSetparamdesc %></td>
                                <td><input type="text" id="txt_<%=modItemZakat.GetSetparamid %>" name="txt_<%=modItemZakat.GetSetparamid %>" value="<%=String.Format("{0:#,##0.00}",modItemZakat.GetSettotalamount)%>" <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"onclick=\"editadjustment('"+modItemZakat.GetSetadjustmentno+"', "+modItemZakat.GetSetlineno+", '"+modItemZakat.GetSetadjustmenttype+"', '"+modItemZakat.GetSetparamdesc+"', "+modItemZakat.GetSettotalamount+");\"":""%> readonly="readonly" style="text-align:right;"/></td>
                                <td></td>
                            </tr>
                             <%
                              }
                             %>                            
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetclosingdate.Trim().Length > 0 ? oModZakatCalculation.GetSetclosingdate.Trim() :sClosingDate%></td>
                                <td>Pernolakan Perlarasan Aset Semasa</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentsubtractionamount)%></td>
                                <td></td>
                            </tr>                            
                            <tr>
                                <td colspan="4"><h4 class="red">TOLAKAN: JUMLAH PERLARASAN ASET SEMASA</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentsubtractionamount)%></h4></td>
                                <td style="text-align:right;"><h4 class="red"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentsubtractionamount)%></h4></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4 class="orange">PERLARASAN LIABILITI SEMASA</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td>Pemiutang Terkumpul</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetpendingpaidamount)%></td>
                                <td></td>
                            </tr>
                              <%
                                  oModZakatCalculation.GetSetadjustmentadditionamount = oModZakatCalculation.GetSetpendingpaidamount;
                                  for(int i=0; i<lsItemZakatAddition.Count; i++)
                                  {
                                      MainModel modItemZakat = (MainModel)lsItemZakatAddition[i];
                                      oModZakatCalculation.GetSetadjustmentadditionamount = oModZakatCalculation.GetSetadjustmentadditionamount + modItemZakat.GetSettotalamount;
                              %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td colspan="3"><%=modItemZakat.GetSetparamdesc %></td>
                                <td><input type="text" id="txt_<%=modItemZakat.GetSetparamid %>" name="txt_<%=modItemZakat.GetSetparamid %>" value="<%=String.Format("{0:#,##0.00}", modItemZakat.GetSettotalamount)%>" <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"onclick=\"editadjustment('"+modItemZakat.GetSetadjustmentno+"', "+modItemZakat.GetSetlineno+", '"+modItemZakat.GetSetadjustmenttype+"', '"+modItemZakat.GetSetparamdesc+"', "+modItemZakat.GetSettotalamount+");\"":""%> readonly="readonly" style="text-align:right;"/></td>
                                <td></td>
                            </tr>
                             <%
                              }
                             %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModZakatCalculation.GetSetclosingdate.Trim().Length > 0 ? oModZakatCalculation.GetSetclosingdate.Trim() :sClosingDate%></td>
                                <td>Penambahan Perlarasan Liabiliti Semasa</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentadditionamount)%></td>
                                <td></td>
                            </tr>                            
                            <tr>
                                <td colspan="4"><h4 class="orange">TAMBAHAN: JUMLAH PERLARASAN LIABILITI SEMASA</h4></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentadditionamount)%></h4></td>
                                <td style="text-align:right;"><h4 class="orange"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetadjustmentadditionamount)%></h4></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4 class="dark">TAKSIRAN ZAKAT</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                if (oModZakatCalculation.GetSetzakatcalculationno.Length > 0)
                                {
                                    oModZakatCalculation.GetSettotalamountforzakat1 = oModZakatCalculation.GetSetbankclosingamount + oModZakatCalculation.GetSetcashclosingamount + oModZakatCalculation.GetSetpendingreceiptamount - oModZakatCalculation.GetSetpendingpaidamount;
                                    oModZakatCalculation.GetSettotalamountforzakat1 = oModZakatCalculation.GetSettotalamountforzakat1 + oModZakatCalculation.GetSetstockclosingamount;
                                    oModZakatCalculation.GetSettotalamountforzakat1 = oModZakatCalculation.GetSettotalamountforzakat1 - oModZakatCalculation.GetSetadjustmentsubtractionamount;
                                    oModZakatCalculation.GetSettotalamountforzakat1 = oModZakatCalculation.GetSettotalamountforzakat1 + oModZakatCalculation.GetSetadjustmentadditionamount;
                                    if(oModZakatCalculation.GetSettotalamountforzakat1 >= oModZakatCalculation.GetSetzakatnisabamount)
                                    {
                                        oModZakatCalculation.GetSettotalamountforzakat2 = oModZakatCalculation.GetSettotalamountforzakat1;
                                    }
                                    else
                                    {
                                        oModZakatCalculation.GetSettotalamountforzakat2 = 0;
                                    }
                                    oModZakatCalculation.GetSettotalamountforzakat3 = oModZakatCalculation.GetSettotalamountforzakat2 * oModZakatCalculation.GetSetsharepercentage / 100;
                                    oModZakatCalculation.GetSettotalamountpayzakat = oModZakatCalculation.GetSettotalamountforzakat3 * oModZakatCalculation.GetSetzakatrate / 100;
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td colspan="3">Jumlah Lebihan Aset/ Liabiliti</td>
                                <td></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSettotalamountforzakat1)%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td colspan="3">Jumlah Yang Dikenakan Zakat (Melebihi harga 85 gram emas)</td>
                                <td  style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSetzakatnisabamount)%></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSettotalamountforzakat2)%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td></td>
                                <td colspan="3">Peratus Pemilikan Saham Muslim (%)</td>
                                <td><input type="text" id="txtMuslimShares" name="txtMuslimShares" value="<%=oModZakatCalculation.GetSetsharepercentage %>" <%=oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS")?"onclick=\"editsharepercentage("+oModZakatCalculation.GetSetsharepercentage+");\"":""%> readonly="readonly" style="text-align:right;"/></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSettotalamountforzakat3)%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><input type="text" id="closedate" class="form-control" name="closedate" required="required" readonly="readonly" value="<%=oModZakatCalculation.GetSetclosingdate.Trim().Length > 0 ? oModZakatCalculation.GetSetclosingdate.Trim() :sClosingDate%>"/></td>
                                <td>Kadar Taksiran Zakat (%)</td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><%=oModZakatCalculation.GetSetzakatrate %></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSettotalamountpayzakat)%></td>
                            </tr>
                            <tr>
                                <td colspan="4"><h4 class="dark">JUMLAH TAKSIRAN ZAKAT</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="dark"><%=String.Format("{0:#,##0.00}", oModZakatCalculation.GetSettotalamountpayzakat)%></h4></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Tiada rekod...</td>
                                <td style="text-align:right;"></td>
                                <td style="text-align:right;"></td>
                                <td></td>
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
                        <input type="hidden" name="hidZakatCalculationNo" id="hidZakatCalculationNo" value="<%=oModZakatCalculation.GetSetzakatcalculationno %>" />
                        <input type="hidden" name="hidBankOpeningAmount" id="hidBankOpeningAmount" value="<%=oModZakatCalculation.GetSetbankopeningamount %>" />
                        <input type="hidden" name="hidCashOpeningAmount" id="hidCashOpeningAmount" value="<%=oModZakatCalculation.GetSetcashopeningamount %>" />
                        <input type="hidden" name="hidBankPaymentReceiptAmount" id="hidBankPaymentReceiptAmount" value="<%=oModZakatCalculation.GetSetbankpaymentreceiptamount %>" />
                        <input type="hidden" name="hidCashPaymentReceiptAmount" id="hidCashPaymentReceiptAmount" value="<%=oModZakatCalculation.GetSetcashpaymentreceiptamount %>" />
                        <input type="hidden" name="hidBankPaymentPaidAmount" id="hidBankPaymentPaidAmount" value="<%=oModZakatCalculation.GetSetbankpaymentpaidamount %>" />
                        <input type="hidden" name="hidCashPaymentPaidAmount" id="hidCashPaymentPaidAmount" value="<%=oModZakatCalculation.GetSetcashpaymentpaidamount %>" />
                        <input type="hidden" name="hidBankClosingAmount" id="hidBankClosingAmount" value="<%=oModZakatCalculation.GetSetbankclosingamount %>" />
                        <input type="hidden" name="hidCashClosingAmount" id="hidCashClosingAmount" value="<%=oModZakatCalculation.GetSetcashclosingamount %>" />
                        <input type="hidden" name="hidStockOpeningAmount" id="hidStockOpeningAmount" value="<%=oModZakatCalculation.GetSetstockopeningamount %>" />
                        <input type="hidden" name="hidStockInAmount" id="hidStockInAmount" value="<%=oModZakatCalculation.GetSetstockinamount %>" />
                        <input type="hidden" name="hidStockOutAmount" id="hidStockOutAmount" value="<%=oModZakatCalculation.GetSetstockoutamount %>" />
                        <input type="hidden" name="hidStockClosingAmount" id="hidStockClosingAmount" value="<%=oModZakatCalculation.GetSetstockclosingamount %>" />
                        <input type="hidden" name="hidPendingReceiptAmount" id="hidPendingReceiptAmount" value="<%=oModZakatCalculation.GetSetpendingreceiptamount %>" />
                        <input type="hidden" name="hidPendingPaidAmount" id="hidPendingPaidAmount" value="<%=oModZakatCalculation.GetSetpendingpaidamount %>" />
                        <input type="hidden" name="hidZakatNisabAmount" id="hidZakatNisabAmount" value="<%=oModZakatCalculation.GetSetzakatnisabamount %>" />
                        <input type="hidden" name="hidZakatRate" id="hidZakatRate" value="<%=oModZakatCalculation.GetSetzakatrate %>" />
                        <input type="hidden" name="hidSharePercentage" id="hidSharePercentage" value="<%=oModZakatCalculation.GetSetsharepercentage %>" />
                    </div>
                    <!-- BEGIN FOR DIALOG MODAL --> 
                    <div class="modal fade modal-beginzakatcalculation" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title green">Pilih Tarikh PERMULAAN Penyata Taksiran Zakat</h4>
                                </div>
                                <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh<span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">        
                                            <input type="text" id="begindate" class="date-picker form-control" name="begindate" readonly="readonly" required="required" value="<%=oModZakatCalculation.GetSetopeningdate.Trim().Length > 0 ? oModZakatCalculation.GetSetopeningdate.Trim() :sOpeningDate%>"/>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-warning" id="btnBeginZakatCalculation" onclick="actionclick('BEGINING');">Mula</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-endzakatcalculation" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title red">Anda pasti untuk MENGAKHIRI Penyata Taksiran Zakat ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnEndZakatCalculation" onclick="actionclick('ENDING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-closezakatcalculation" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title blue">Teruskan untuk MENUTUP Penyata Taksiran Zakat ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnCloseZakatCalculation" onclick="actionclick('CLOSING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>

                    <div class="modal fade" id="myAdjustmentItem" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <table id="tbLineItem" class="table">
                                        <thead>
                                            <tr>
                                                <th colspan="2"><span id="lblAdjustmentType" ></span></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><span id="lblAdjustmentDesc"></span></td>
                                                <td>
                                                    <input id="txtAdjustmentType" name="txtAdjustmentType" type="hidden" value="" />
                                                    <input id="txtLineNo" name="txtLineNo" type="hidden" value="1" />
                                                    <input id="lblAdjustmentNo" name="lblAdjustmentNo" type="hidden" value="" />
                                                    <input id="txtTotalAmount" name="txtTotalAmount" type="text" value="0" style="width: 40px" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Tutup</button>
                                    <button type="button" class="btn btn-sm btn-primary" onclick="actionclick('ADJUSTMENT');">Kemaskini</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="mySharePercentage" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <table id="tbSharePercentage" class="table">
                                        <thead>
                                            <tr>
                                                <th colspan="2">Pemilikan Saham Muslim</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Peratus Pemilikan (%)</td>
                                                <td>
                                                    <input id="txtSharePercentage" name="txtSharePercentage" type="text" value="100" style="width: 40px" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Tutup</button>
                                    <button type="button" class="btn btn-sm btn-primary" onclick="actionclick('SHARE_PERCENTAGE');">Kemaskini</button>
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

            }
            else if (action == 'BEGINING')
            {
                $('#closedate').removeAttr('required');

            }
            else if (action == 'CLOSING' || action == 'ENDING')
            {
                $('#begindate').removeAttr('required');

            }
            else if (action == 'ADJUSTMENT')
            {
                $('#begindate').removeAttr('required');
                $('#closedate').removeAttr('required');
            }
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        //enable & disable button
        $(document).ready(function () {

            $('#beginzakatcalculation').prop('disabled', true);
            $('#endzakatcalculation').prop('disabled', true);
            $('#closezakatcalculation').prop('disabled', true);

            $('#beginzakatcalculation').attr('disabled', 'disabled');
            $('#endzakatcalculation').attr('disabled', 'disabled');
            $('#closezakatcalculation').attr('disabled', 'disabled');

            <%
            if (oModZakatCalculation.GetSetstatus.Equals("IN-PROGRESS"))
            {
            %>
                //$('#beginzakatcalculation').prop('disabled', false);
                $('#endzakatcalculation').prop('disabled', false);
                $('#closezakatcalculation').prop('disabled', false);

                //$('#beginzakatcalculation').removeAttr('disabled');
                $('#endzakatcalculation').removeAttr('disabled');
                $('#closezakatcalculation').removeAttr('disabled');
            <%
            }
            else if (oModZakatCalculation.GetSetstatus.Equals("CLOSED"))
            { 
            %>
                $('#beginzakatcalculation').prop('disabled', true);
                $('#endzakatcalculation').prop('disabled', true);
                $('#closezakatcalculation').prop('disabled', true);

                $('#beginzakatcalculation').attr('disabled', 'disabled');
                $('#endzakatcalculation').attr('disabled', 'disabled');
                $('#closezakatcalculation').attr('disabled', 'disabled');
            <%
            }
            else
            {
            %>
                $('#beginzakatcalculation').prop('disabled', false);
                $('#beginzakatcalculation').removeAttr('disabled');
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

            $('#totalpaymentreceipt').text('<%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankpaymentreceiptamount+oModZakatCalculation.GetSetcashpaymentreceiptamount)%>');
            $('#totalpaymentpaid').text('<%=String.Format("{0:#,##0.00}",oModZakatCalculation.GetSetbankpaymentpaidamount+oModZakatCalculation.GetSetcashpaymentpaidamount)%>');

            $('#closedate').change(function () {
                //actionclick('OPEN');
            });

            <%
                if(oModZakatCalculation.GetSetstatus.Equals("CLOSED"))
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

        function editadjustment(adjustmentno, lineno, adjustmenttype, adjustmentdesc, totalamount)
        {
            $('#lblAdjustmentNo').val(adjustmentno);
            $('#txtLineNo').val(lineno);
            $('#txtAdjustmentType').val(adjustmenttype);
            $('#lblAdjustmentType').text(adjustmenttype=='ZAKAT_ADDITION'?'TAMBAHAN: PERLARASAN LIABILITI SEMASA':adjustmenttype=='ZAKAT_SUBTRACTION'?'TOLAKAN: PERLARASAN ASET SEMASA':'');
            $('#lblAdjustmentDesc').text(adjustmentdesc);
            $('#txtTotalAmount').val(totalamount);
            $('#myAdjustmentItem').modal({ backdrop: "static" });
        }

        function editsharepercentage(sharepercentage)
        {
            $('#txtSharePercentage').val(sharepercentage);
            $('#mySharePercentage').modal({ backdrop: "static" });
        }
        
        function formatMoney(amount, decimalCount = 2, decimal = ".", thousands = ",") {
            try {
                decimalCount = Math.abs(decimalCount);
                decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

                const negativeSign = amount < 0 ? "-" : "";

                let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
                let j = (i.length > 3) ? i.length % 3 : 0;

                return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
            } catch (e) {
                console.log(e)
            }
        }

    </script>
</asp:Content>

