<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMobile.master" AutoEventWireup="true" CodeFile="StockStateMobile.aspx.cs" Inherits="StockStateMobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penyata Stok & Inventori <small>MAKLUMAT</small></h2>
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
                            <label for="stockstateno">No. Stok Penyata:</label>
                            <input type="text" id="stockstateno" class="form-control" readonly="readonly" name="stockstateno" value="<%=oModStockState.GetSetstockstateno %>" />
                            <label for="openingtype">Jenis Pembukaan:</label>
                            <input type="text" id="openingtype" class="form-control" readonly="readonly" name="openingtype" value="<%=oModStockState.GetSetopeningtype%>"/>                                    
                            <label for="openingdate">Tarikh Pembukaan:</label>
                            <input type="text" id="openingdate" class="form-control" readonly="readonly" name="openingdate" value="<%=oModStockState.GetSetopeningdate%>"/>                                    
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-6">
                        <div id="add-form2">
                            <label for="status">Status:</label>
                            <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModStockState.GetSetstatus%>"/>
                            <label for="closingtype">Jenis Penutupan:</label>
                            <input type="text" id="closingtype" class="form-control" readonly="readonly" name="closingtype" value="<%=oModStockState.GetSetclosingtype%>"/>                                    
                            <label for="closingdate">Tarikh Penutupan:</label>
                            <input type="text" id="closingdate" class="form-control" readonly="readonly" name="closingdate" value="<%=oModStockState.GetSetclosingdate%>"/>                                    
                        </div>
                    </div>
                  
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                     
                                    <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();" >Tutup</button>
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
                        <a id="beginstockstate" name="beginstockstate" class="btn btn-app" data-toggle="modal" data-target=".modal-beginstockstatement">
                          <i class="fa fa-play <%=oModStockState.GetSetstatus.Equals("IN-PROGRESS")?"":oModStockState.GetSetstatus.Equals("CLOSED")?"":"green"%>"></i>Mula Penyata Stok
                        </a>
                        <a id="closestockstate" name="closestockstate" class="btn btn-app" data-toggle="modal" data-target=".modal-closestockstatement">
                          <i class="fa fa-flag <%=oModStockState.GetSetstatus.Equals("IN-PROGRESS")?"orange":""%>"></i>Tutup Penyata Stok
                        </a>
                        <a id="printstockstate" name="printstockstate" class="btn btn-app" onclick="openprintpage();">
                          <i class="fa fa-print <%=oModStockState.GetSetstatus.Equals("IN-PROGRESS")?"blue":oModStockState.GetSetstatus.Equals("CLOSED")?"blue":""%>"></i>Cetak Penyata Stok
                        </a>
                        <a id="endstockstate" name="endstockstate" class="btn btn-app" data-toggle="modal" data-target=".modal-endstockstatement">
                          <i class="fa fa-stop <%=oModStockState.GetSetstatus.Equals("IN-PROGRESS")?"red":""%>"></i>Akhir Penyata Stok
                        </a>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <form id="search" runat="server">
                    <table class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th></th>
                          <th>Tarikh Transaksi</th>
                          <th style="display:none;">Jenis Transaksi</th>
                          <th style="display:none;">No. Transaksi</th>
                          <th style="display:none;">No</th>
                          <th>Kod Item/ Keterangan</th>
                          <th style="display:none;">Lokasi</th>
                          <th style="display:none;">Tarikh SOH</th>
                          <th style="text-align:right;">Qty</th>
                          <th style="text-align:right;">Harga Unit</th>
                          <th style="text-align:right;">Jumlah Harga</th>
                          <th style="text-align:right;">Jumlah Total</th>
                        </tr>
                      </thead>

                      <tbody>
                            <tr>
                                <td colspan="4"><h4 class="orange">OPENING STOCK</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="orange" id="totalstockopeningamount">0.00</h4></td>
                            </tr>
                            <%
                                if (oModStockState.GetSetstockstateno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td><%=oModStockState.GetSetopeningdate %></td>
                                <td colspan="3">PEMBUKAAN STOK/ INVENTORI</td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockopeningamount) %></td>
                                <td></td>
                            </tr>

                                <%
                                    if (lsStockBeginDetails.Count > 0)
                                    {
                                        for (int i = 0; i < lsStockBeginDetails.Count; i++) 
                                        {
                                            MainModel oStockInDet = (MainModel)lsStockBeginDetails[i];
                                            if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                                            {
                                                oModStockState.GetSetstockopeningamount = oModStockState.GetSetstockopeningamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice,2,MidpointRounding.AwayFromZero);
                                            }
                                %>
                                        <tr>
                                            <td></td>
                                            <td><i class="glyphicon glyphicon-play"></i></td>
                                            <td><%=oStockInDet.GetSettransdate%></td>
                                            <td style="display:none;"><%=oStockInDet.GetSettranstype%>: STOCK_BEGIN</td>
                                            <td style="display:none;"><%=oStockInDet.GetSettransno%></td>
                                            <td style="display:none;"><%=oStockInDet.GetSettrans_lineno%></td>
                                            <td><%=oStockInDet.GetSetitemno %><br /><%=oStockInDet.GetSetitemdesc%><br /><%=oStockInDet.GetSetlocation%><br /><%=oStockInDet.GetSetdatesoh%></td>
                                            <td style="display:none;"><%=oStockInDet.GetSetlocation%></td>
                                            <td style="display:none;"><%=oStockInDet.GetSetdatesoh%></td>
                                            <td style="text-align:right;"><%=oStockInDet.GetSettransqty%></td>
                                            <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockInDet.GetSettransprice)%></td>
                                            <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockInDet.GetSettransprice * oStockInDet.GetSettransqty)%></td>
                                            <td></td>
                                        </tr>
                                <%
                                        }
                                    }
                                %>

                            <%
                                }else{

                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4>PENAMBAHAN STOK/ INVENTORI <i class="fa fa-plus-square green"></i></h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalstockinamount">0.00</h4></td>
                            </tr>
                            <%
                                if (lsStockInDetails.Count > 0)
                                {
                                    for (int i = 0; i < lsStockInDetails.Count; i++) 
                                    {
                                        MainModel oStockInDet = (MainModel)lsStockInDetails[i];
                                        if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                                        {
                                            oModStockState.GetSetstockinamount = oModStockState.GetSetstockinamount + Math.Round(oStockInDet.GetSettransqty * oStockInDet.GetSettransprice,2,MidpointRounding.AwayFromZero) ;
                                        }
                            %>
                                    <tr>
                                        <td></td>
                                        <td><i class="glyphicon glyphicon-play"></i></td>
                                        <td><%=oStockInDet.GetSettransdate%></td>
                                        <td style="display:none;"><%=oStockInDet.GetSettranstype%></td>
                                        <td style="display:none;"><%=oStockInDet.GetSettransno%></td>
                                        <td style="display:none;"><%=oStockInDet.GetSettrans_lineno%></td>
                                        <td><%=oStockInDet.GetSetitemno %><br /><%=oStockInDet.GetSetitemdesc%><br /><%=oStockInDet.GetSetlocation%><br /><%=oStockInDet.GetSetdatesoh%></td>
                                        <td style="display:none;"><%=oStockInDet.GetSetlocation%></td>
                                        <td style="display:none;"><%=oStockInDet.GetSetdatesoh%></td>
                                        <td style="text-align:right;"><%=oStockInDet.GetSettransqty%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockInDet.GetSettransprice)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockInDet.GetSettransprice * oStockInDet.GetSettransqty)%></td>
                                        <td></td>
                                    </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <tr>
                                <td colspan="4"><h4>PENGURANGAN STOK/ INVENTORI <i class="fa fa-minus-square red"></i></h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 id="totalstockoutamount">0.00</h4></td>
                            </tr>
                            <%
                                if (lsStockOutDetails.Count > 0)
                                {
                                    for (int i = 0; i < lsStockOutDetails.Count; i++) 
                                    {
                                        MainModel oStockOutDet = (MainModel)lsStockOutDetails[i];
                                        if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                                        {
                                            oModStockState.GetSetstockoutamount = oModStockState.GetSetstockoutamount + Math.Round(oStockOutDet.GetSettransqty * oStockOutDet.GetSettransprice,2,MidpointRounding.AwayFromZero);
                                        }
                            %>
                                    <tr>
                                        <td></td>
                                        <td><i class="glyphicon glyphicon-play"></i></td>
                                        <td><%=oStockOutDet.GetSettransdate%></td>
                                        <td style="display:none;"><%=oStockOutDet.GetSettranstype%></td>
                                        <td style="display:none;"><%=oStockOutDet.GetSettransno%></td>
                                        <td style="display:none;"><%=oStockOutDet.GetSettrans_lineno%></td>
                                        <td><%=oStockOutDet.GetSetitemno %><br /><%=oStockOutDet.GetSetitemdesc%><br /><%=oStockOutDet.GetSetlocation%><br /><%=oStockOutDet.GetSetdatesoh%></td>
                                        <td style="display:none;"><%=oStockOutDet.GetSetlocation%></td>
                                        <td style="display:none;"><%=oStockOutDet.GetSetdatesoh%></td>
                                        <td style="text-align:right;"><%=oStockOutDet.GetSettransqty%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockOutDet.GetSettransprice)%></td>
                                        <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oStockOutDet.GetSettransprice * oStockOutDet.GetSettransqty)%></td>
                                        <td></td>
                                    </tr>
                            <%
                                    }
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Rekod tiada...</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <%
                                }
                            %>
                            <%
                                //calculate total & closing stock amount
                                if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
                                {
                                    oModStockState.GetSetstockclosingamount = oModStockState.GetSetstockopeningamount + oModStockState.GetSetstockinamount - oModStockState.GetSetstockoutamount;
                                }
                                
                            %>
                            <tr>
                                <td colspan="4"><h4 class="blue">PENUTUPAN STOK/ INVENTORI</h4></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align:right;"><h4 class="blue"><%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockclosingamount)%></h4></td>
                            </tr>
                            <%
                                if (oModStockState.GetSetstockstateno.Length > 0)
                                {
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2"><input type="text" id="closedate" class="date-picker form-control" name="closedate" required="required" readonly="readonly" value="<%=oModStockState.GetSetclosingdate.Trim().Length > 0 ? oModStockState.GetSetclosingdate.Trim() :sClosingDate%>"/><!--<i class="glyphicon glyphicon-calendar fa fa-calendar"></i>--></td>
                                <td colspan="3"><button type="button" onclick="viewstockstateSOH('<%=oModStockState.GetSetcomp %>','<%=oModStockState.GetSetstockstateno %>');" class="btn btn-primary">SENARAI STOK</button></td>
                                <td style="text-align:right;"><%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockclosingamount)%></td>
                            </tr>
                            <%
                                }else{
                            %>
                            <tr>
                                <td></td>
                                <td><i class="glyphicon glyphicon-play"></i></td>
                                <td colspan="2">Rekod tiada...</td>
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
                    <div style="display: none;">
                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                        <input type="hidden" name="hidStockStateNo" id="hidStockStateNo" value="<%=oModStockState.GetSetstockstateno %>" />
                        <input type="hidden" name="hidStockOpeningAmount" id="hidStockOpeningAmount" value="<%=oModStockState.GetSetstockopeningamount %>" />
                        <input type="hidden" name="hidStockInAmount" id="hidStockInAmount" value="<%=oModStockState.GetSetstockinamount %>" />
                        <input type="hidden" name="hidStockOutAmount" id="hidStockOutAmount" value="<%=oModStockState.GetSetstockoutamount %>" />
                        <input type="hidden" name="hidStockClosingAmount" id="hidStockClosingAmount" value="<%=oModStockState.GetSetstockclosingamount %>" />
                    </div>
                    <!-- BEGIN FOR DIALOG MODAL --> 
                    <div class="modal fade modal-beginstockstatement" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title green">Pilih Tarikh PERMULAAN Penyata Stok</h4>
                                </div>
                                <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh<span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">        
                                            <input type="text" id="begindate" class="date-picker form-control" name="begindate" readonly="readonly" required="required" value="<%=oModStockState.GetSetopeningdate.Trim().Length > 0 ? oModStockState.GetSetopeningdate.Trim() :sOpeningDate%>"/>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-warning" id="btnBeginStockStatement" onclick="actionclick('BEGINING');">Mula</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Batal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-endstockstatement" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title red">Anda pasti untuk MENGAKHIRI Penyata Stok ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnEndStockStatement" onclick="actionclick('ENDING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal fade modal-closestockstatement" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title blue">Teruskan untuk MENUTUP Penyata Stok ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnCloseStockStatement" onclick="actionclick('CLOSING');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
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
            if (action == 'BEGINING')
            {
                $('#closedate').removeAttr('required');
            }
            else if (action == 'CLOSING' || action == 'ENDING')
            {
                $('#begindate').removeAttr('required');
            }
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        //enable & disable button
        $(document).ready(function () {

            $('#beginstockstate').prop('disabled', true);
            $('#endstockstate').prop('disabled', true);
            $('#closestockstate').prop('disabled', true);

            $('#beginstockstate').attr('disabled', 'disabled');
            $('#endstockstate').attr('disabled', 'disabled');
            $('#closestockstate').attr('disabled', 'disabled');

            <%
            if (oModStockState.GetSetstatus.Equals("IN-PROGRESS"))
            {
            %>
                //$('#beginstockstate').prop('disabled', false);
                $('#endstockstate').prop('disabled', false);
                $('#closestockstate').prop('disabled', false);

                //$('#beginstockstate').removeAttr('disabled');
                $('#endstockstate').removeAttr('disabled');
                $('#closestockstate').removeAttr('disabled');
            <%
            }
            else if (oModStockState.GetSetstatus.Equals("CLOSED"))
            { 
            %>
                $('#beginstockstate').prop('disabled', true);
                $('#endstockstate').prop('disabled', true);
                $('#closestockstate').prop('disabled', true);

                $('#beginstockstate').attr('disabled', 'disabled');
                $('#endstockstate').attr('disabled', 'disabled');
                $('#closestockstate').attr('disabled', 'disabled');
            <%
            }
            else
            {
            %>
                $('#beginstockstate').prop('disabled', false);
                //$('#endstockstate').prop('disabled', false);
                //$('#closestockstate').prop('disabled', false);

                $('#beginstockstate').removeAttr('disabled');
                //$('#endstockstate').removeAttr('disabled');
                //$('#closestockstate').removeAttr('disabled');
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
                actionclick('OPEN');
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#totalstockopeningamount').text('<%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockopeningamount)%>'); 
            $('#totalstockinamount').text('<%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockinamount)%>');
            $('#totalstockoutamount').text('<%=String.Format("{0:#,##0.00}",oModStockState.GetSetstockoutamount)%>');

            $('#closedate').change(function () {
                //actionclick('OPEN');
            });

            <%
                if(oModStockState.GetSetstatus.Equals("CLOSED"))
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

        function viewstockstateSOH(comp, stockstateno) {
            var popupWindow = window.open("StockStateSOHMobile.aspx?action=OPEN&userid=<%=sUserId%>&stockstateno=" + stockstateno, "open_stockstateSOH", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

