<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AdjustmentDetails.aspx.cs" Inherits="AdjustmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2><%=sActionString %></h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <form id="form1" runat="server">
                  <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form1">
                                    <label for="adjustmentno">No. Perlarasan:</label>
                                    <input type="text" id="adjustmentno" class="form-control" readonly="readonly" name="adjustmentno" value="<%=oModAdjustment.GetSetadjustmentno %>" />
                                    <label for="adjustmentdate">Tarikh Perlarasan:</label>
                                    <input type="text" id="adjustmentdate" class="form-control" readonly="readonly" name="adjustmentdate" value="<%=oModAdjustment.GetSetadjustmentdate %>" />
                                    <label for="adjustmenttype">Jenis Perlarasan:</label>
                                      <select class="form-control" id="adjustmenttype" name="adjustmenttype" required="required">
                                        <option value="">-select-</option>
                                        <option value="BEGINING_STOCK" <%=oModAdjustment.GetSetadjustmenttype.Equals("BEGINING_STOCK")?"selected":"" %>>PERMULAAN STOK & INVENTORI</option>
                                        <option value="STOCK_INCREASE" <%=oModAdjustment.GetSetadjustmenttype.Equals("STOCK_INCREASE")?"selected":"" %>>PERNAMBAHAN STOK & INVENTORI</option>
                                        <option value="STOCK_DECREASE" <%=oModAdjustment.GetSetadjustmenttype.Equals("STOCK_DECREASE")?"selected":"" %>>PENGURANGAN STOK & INVENTORI</option>
                                      </select>
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" name="status" readonly="readonly" value="<%=oModAdjustment.GetSetstatus %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="remarks">Catatan:</label>
                                    <textarea id="remarks" class="form-control" rows="3" name="remarks"><%=oModAdjustment.GetSetremarks%></textarea>
                                    <label for="createdby">Disediakan Oleh:</label>
                                    <input type="text" id="createdby" class="form-control" readonly="readonly" name="createdby" value="<%=oModAdjustment.GetSetcreatedby%>" />
                                </div>
                    </div>
                  </div>
                  <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                    <%
                                        if (sAction.Equals("ADD")) 
                                        { 
                                    %>
                                    <button id="btnCreate" name="btnCreate" type="button" class="btn btn-primary" onclick="actionclick('CREATE');">Daftar</button>
                                    <button id="btnReset" name="btnReset" type="button" class="btn btn-warning" onclick="actionclick('ADD');">Reset</button>
                                    <%
                                        }
                                        else if (sAction.Equals("OPEN")) 
                                        {
                                            if (!oModAdjustment.GetSetstatus.Equals("CONFIRMED") && !oModAdjustment.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsAdjustmentLineItem.Count > 0)
                                                { 
                                    %>
                                    <button id="btnApprove" name="btnApprove" type="button" class="btn btn-success" onclick="actionclick('CONFIRM');" >Confirm</button>
                                    <%
                                                }
                                    %>
                                    <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-adjustment">Batal</button>
                                    <%
                                            }
                                        }
                                        else if (sAction.Equals("EDIT")) 
                                        { 
                                    %>
                                    <button id="btnSave" name="btnSave" type="button" class="btn btn-primary" onclick="actionclick('SAVE');" >Simpan</button>
                                    <button id="btnBack" name="btnBack" type="button" class="btn btn-warning" onclick="actionclick('OPEN');" >Kembali</button>
                                    <%
                                        }
                                    %>
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
                                    <div style="display: none;">
                                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                        <input type="hidden" name="hidAdjustmentNo" id="hidAdjustmentNo" value="<%=sAdjustmentNo %>" />
                                        <input type="hidden" name="hidLineNo" id="hidLineNo" value="" />
                                    </div>
                                </div>
                            </div>
                        </section>

                        <!--BEGIN dialog box for add line item-->
                        <div class="modal fade modal-add-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">

                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                        </button>
                                        <h4 class="modal-title">Tambah Item Perlarasan</h4>
                                    </div>
                                    <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">

                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addlineno" name="addlineno" type="text" class="form-control" readonly="readonly" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod Item <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="additemno" name="additemno" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option>-select-</option>
                                      <%
                                          for (int i = 0; i < lsItem.Count; i++)
                                          {
                                              MainModel oAdjItem = (MainModel)lsItem[i];
                                      %>                         
                                            <option value="<%=oAdjItem.GetSetitemno %>"><%=oAdjItem.GetSetitemno %> | <%=oAdjItem.GetSetitemdesc %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                          <input type="hidden" name="additemdesc" id="additemdesc" value="" />
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Lokasi</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addlocation" name="addlocation" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                              <option>-select-</option>
                                              <%
                                                  for (int i = 0; i < lsStockLocation.Count; i++)
                                                  {
                                                      MainModel oStockLoc = (MainModel)lsStockLocation[i];
                                              %>                         
                                                    <option value="<%=oStockLoc.GetSetparamdesc %>"><%=oStockLoc.GetSetparamdesc %></option>
                                              <%
                                                  }
                                              %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh SOH</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <div class='input-group'>
                                                <input type="text" id="adddatesoh" name="adddatesoh" value="" class="form-control" />
                                                <span id='datetimepicker1' class="input-group-addon date-picker">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                            <div id="adddatesoh-container" style="position: relative; float: left; width: 100%; margin: 0px;"></div>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Qty SOH</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addqtysoh" name="addqtysoh" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kos SOH</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addcostsoh" name="addcostsoh" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Qty Perbezaan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addqtyvariance" name="addqtyvariance" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Unit</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addpricevariance" name="addpricevariance" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Qty Perlarasan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addqtyadjusted" name="addqtyadjusted" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kos Perlarasan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addcostadjusted" name="addcostadjusted" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Catatan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addremarks" name="addremarks" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" id="btnAddLineItem" onclick="actionclick('INSERT');">Tambah</button>
                                        <button type="button" class="btn btn-primary" id="btnEditLineItem" onclick="actionclick('UPDATE');">Simpan</button>
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                      <!--END dialog box for add line item-->
                    <!--BEGIN dialog box for confirm delete-->
                    <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Anda pasti untuk keluarkan Item ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnDeleteLineItem" onclick="actionclick('DELETE');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm delete-->
                    <!--BEGIN dialog box for confirm cancel adjustment order-->
                    <div class="modal fade modal-confirm-cancel-adjustment" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Anda pasti untuk batalkan proses ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnCancelOrder" onclick="actionclick('CANCEL');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm cancel adjustment order-->

                  </div> 
                  </form>                   
                  <div class="col-md-12 col-sm-12 col-xs-12">
                    <a id="addlineitem" name="addlineitem" class="btn btn-app" data-toggle="modal" data-target=".modal-add-line-item" onclick="openaddlineitem();">
                      <i class="fa fa-plus-square green"></i>Tambah Item
                    </a>
                    <!--
                    <a id="printreceipt" name="printreceipt" class="btn btn-app" onclick="openprintorder();">
                      <i class="fa fa-print dark"></i>Print Receipt
                    </a>
                    -->
                    <a id="adjustmentdetails" name="adjustmentdetails" class="btn btn-app" data-toggle="modal" data-target=".modal-adjustment-details">
                      <i class="fa fa-edit dark"></i>Maklumat Tambahan
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th></th>
                              <th>No</th>
                              <th>Kod Item</th>
                              <th>Keterangan Item</th>
                              <th>Lokasi</th>
                              <th>Tarikh SOH</th>
                              <th>Qty SOH</th>
                              <th>Kos SOH</th>
                              <th>Qty Perbezaan</th>
                              <th>Harga Unit</th>
                              <th>Qty Perlarasan</th>
                              <th>Kos Perlarasan</th>
                              <th>Catatan</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsAdjustmentLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsAdjustmentLineItem.Count; i++)
                                {
                                    MainModel modDet = (MainModel)lsAdjustmentLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modDet.GetSetlineno%></td>
                              <td><%=modDet.GetSetitemno%></td>
                              <td><%=modDet.GetSetitemdesc%></td>
                              <td><%=modDet.GetSetlocation%></td>
                              <td><%=modDet.GetSetdatesoh%></td>
                              <td><%=modDet.GetSetqtysoh%></td>
                              <td><%=modDet.GetSetcostsoh%></td>
                              <td><%=modDet.GetSetqtyvariance%></td>
                              <td><%=modDet.GetSetpricevariance%></td>
                              <td><%=modDet.GetSetqtyadjusted%></td>
                              <td><%=modDet.GetSetcostadjusted%></td>
                              <td><%=modDet.GetSetremarks%></td>
                              <td>
                                  <%
                                    if (!oModAdjustment.GetSetstatus.Equals("CONFIRMED") && !oModAdjustment.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-info btn-xs" onclick="openeditlineitem('<%=modDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-add-line-item"><i class="fa fa-pencil"></i> Kemaskini </a>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
                                  <%
                                      }
                                  %>
                              </td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td></td>
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
            <!--dialog box for adjustment details-->
            <div class="modal fade modal-adjustment-details" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                        </button>
                        <h4 class="modal-title">Maklumat Tambahan</h4>
                    </div>
                    <div class="modal-body">
                        <div id="formdetails" class="form-horizontal form-label-left">
                            <div class="form-group">
                            <table class="table table-striped">
                              <thead>
                                <tr>
                                  <th></th>
                                  <th>By</th>
                                  <th>Date</th>
                                </tr>
                              </thead>
                              <tbody>
                                <tr>
                                  <td>Created</td>
                                  <td><%=oModAdjustment.GetSetcreatedby %></td>
                                  <td><%=oModAdjustment.GetSetcreateddate %></td>
                                </tr>
                                <tr>
                                  <td>Confirmed</td>
                                  <td><%=oModAdjustment.GetSetconfirmedby %></td>
                                  <td><%=oModAdjustment.GetSetconfirmeddate %></td>
                                </tr>
                                <tr>
                                  <td>Cancelled</td>
                                  <td><%=oModAdjustment.GetSetcancelledby %></td>
                                  <td><%=oModAdjustment.GetSetcancelleddate %></td>
                                </tr>
                              </tbody>
                            </table>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                    </div>

                    </div>
                </div>
            </div>


            <script type="text/javascript">

                var dateSOH = {};
                var dateSOHArray;

                //populate bp details, item discount
                $(document).ready(function () {
                    $('#additemno').change(function () {
                        if ($(this).val() == '') {
                            $('#additemdesc').val('');
                        }
                    <%
                                  if (lsItem.Count > 0)
                                  {
                                      for (int i = 0; i < lsItem.Count; i++)
                                      {
                                          MainModel modItem = (MainModel)lsItem[i];
                    %>      
                                            if ($(this).val() == '<%=modItem.GetSetitemno%>') {
                                                $('#additemdesc').val('<%=modItem.GetSetitemdesc%>');
                                                $('#addpricevariance').val('<%=modItem.GetSetcostprice%>')
                                                calculateTotalAmount();
                                            }
                    <% 
                                      }
                                  }
                    %>
                        $('#adddatesoh').val('');

                        //get latest dateSOH
                        $.getJSON("GeneralHandler.ashx?action=GET_DATESOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val(), function (mps) {
                            dateSOH = mps;
                            updateDateSOH();
                        });

                        //get latest qtySOH & costSOH
                        $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                            $('#addqtysoh').val(mps.qtysoh);
                            $('#addcostsoh').val(mps.costsoh);
                            calculateTotalAmount();
                            //console.log('1');
                            //console.log(mps);
                        });
                    });

                    $('#addqtyvariance').change(function () {
                        calculateTotalAmount();
                    });

                    $('#addpricevariance').change(function () {
                        calculateTotalAmount();
                    });
                                        
                    $('#addlocation').change(function () {

                        $('#adddatesoh').val('');

                        $.getJSON("GeneralHandler.ashx?action=GET_DATESOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val(), function (mps) {
                            //var datesoh = {};
                            //$('#adddatesoh').val(datesoh.datesoh);
                            dateSOH = mps;
                            updateDateSOH();
                        });

                        //get latest qtySOH & costSOH
                        $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                            $('#addqtysoh').val(mps.qtysoh);
                            $('#addcostsoh').val(mps.costsoh);
                            calculateTotalAmount();
                            //console.log('2a');
                            //console.log(mps);
                        });
                    });

                });

                function calculateTotalAmount() {
                    var costvariance = parseInt($('#addqtyvariance').val()) * parseFloat($('#addpricevariance').val());
                    var addqtyadjusted = parseInt($('#addqtysoh').val()) + parseInt($('#addqtyvariance').val());
                    $('#addqtyadjusted').val(addqtyadjusted);
                    var addcostadjusted = parseFloat($('#addcostsoh').val()) + parseFloat(costvariance.toFixed(2));
                    $('#addcostadjusted').val(addcostadjusted.toFixed(2));
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#adjustmenttype').removeAttr('required');
                        /*
                        $('#additemno').removeAttr('required');
                        $('#addqtyvariance').removeAttr('required');
                        $('#addpricevariance').removeAttr('required');
                        */
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update adjustment header for case already have Line Item which logic, disable user from updating new bpid
                        $('#adjustmenttype').prop('disabled', false);
                        /*
                        $('#additemno').prop('disabled', false);
                        $('#addqtyvariance').prop('disabled', false);
                        $('#addpricevariance').prop('disabled', false);
                        $('#remarks').prop('disabled', false);
                        */
                    }
                    if (action == 'INSERT' || action == 'UPDATE')
                    {
                        if ($('#hidLineNo').val().length == 0 && action == 'UPDATE') {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'System Error, please contact system admin!',
                                type: 'error',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addlineno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'System Error, please contact system admin!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#additemno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilik "Kod Item"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addlocation').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Lokasi"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#adddatesoh').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Tarikh SOK"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addqtyvariance').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Qty Perbezaan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addqtyvariance').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Qty Perbezaan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addqtyadjusted').val()) < 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: '"Qty Perbezaan" yang dimasukkan Salah!',
                                type: 'error',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addpricevariance').val().length == 0) {
                                proceed = false;
                                new PNotify({
                                    title: 'Alert',
                                    text: 'Sila isi "Harga Unit"!',
                                    type: 'warning',
                                    styling: 'bootstrap3'
                                });
                        }
                        /*
                        else if (parseFloat($('#addpricevariance').val()) == 0) {
                                proceed = false;
                                new PNotify({
                                    title: 'Alert',
                                    text: 'Please  fill in "Unit Price" field!',
                                    type: 'warning',
                                    styling: 'bootstrap3'
                                });
                        }
                        */
                    }
                    if (action == 'DELETE') {
                        if ($('#hidLineNo').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'System Error, please contact system admin!',
                                type: 'error',
                                styling: 'bootstrap3'
                            });
                        }
                    }

                    if (proceed) {
                        document.getElementById("hidAction").value = action;
                        var button = document.getElementById("<%=btnAction.ClientID %>");
                        button.click();
                    }
                }
                function enabledisableinputform(flag) {
                    <%
                    if (lsAdjustmentLineItem.Count > 0)
                    { 
                    %>
                        $('#adjustmenttype').prop('disabled', true);
                    <%
                    }else{ 
                    %>
                        $('#adjustmenttype').prop('disabled', flag);
                    <%
                    }
                    %>
                    $('#remarks').prop('disabled', flag);
                }
                
                <%
                if (sAction.Equals("ADD") || sAction.Equals("EDIT")) 
                {
                %>
                enabledisableinputform(false);
                <%
                }
                else
                {
                %>
                enabledisableinputform(true);
                <%
                }
                %>
                
                function openaddlineitem() {

                    $('#btnAddLineItem').show();
                    $('#btnEditLineItem').hide();

                    $('#hidLineNo').val('');
                    $('#addlineno').val('<%=lsAdjustmentLineItem.Count + 1%>');
                    $('#additemno').val('').change();
                    $('#additemdesc').val('');
                    $('#addlocation').val('').change();
                    //$('#adddatesoh').val('<%=DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")%>');
                    $('#addqtysoh').val('0');
                    $('#addcostsoh').val('0');
                    $('#addqtyvariance').val('0');
                    $('#addpricevariance').val('0');
                    $('#addqtyadjusted').val('0');
                    $('#addcostadjusted').val('0');
                    $('#addremarks').val('');
                    
                    /*
                    $('#datetimepicker1').daterangepicker({
                        singleDatePicker: true,
                        timePicker: true,
                        timePicker12Hour: false,
                        timePickerIncrement: 1,
                        timePickerSeconds: true,
                        format: "DD-MM-YYYY HH:mm:ss",
                        drops: "up",
                        startDate: $('#adddatesoh').val(),
                        calender_style: "picker_4"
                    }, function (start, end, label) {
                        $('#adddatesoh').val(start.format('DD-MM-YYYY HH:mm:ss'));
                    });
                    */

                    updateDatePicker($('#adddatesoh').val());

                }

                function openeditlineitem(lineno) {
                    $('#btnAddLineItem').hide();
                    $('#btnEditLineItem').show();

                    $('#hidLineNo').val(lineno);
                    <%
                    for (int i = 0; i < lsAdjustmentLineItem.Count; i++)
                    {
                        MainModel modDet = (MainModel)lsAdjustmentLineItem[i];
                    %>
                    if ($('#hidLineNo').val() == '<%=modDet.GetSetlineno%>') {
                        $('#addlineno').val('<%=modDet.GetSetlineno%>');
                        $('#additemno').val('<%=modDet.GetSetitemno%>').change();
                        $('#additemdesc').val('<%=modDet.GetSetitemdesc%>');
                        $('#addlocation').val('<%=modDet.GetSetlocation%>').change();
                        $('#adddatesoh').val('<%=modDet.GetSetdatesoh%>');
                        $('#addqtysoh').val('<%=modDet.GetSetqtysoh%>');
                        $('#addcostsoh').val('<%=modDet.GetSetcostsoh%>');
                        $('#addqtyvariance').val('<%=modDet.GetSetqtyvariance%>');
                        $('#addpricevariance').val('<%=modDet.GetSetpricevariance%>');
                        $('#addqtyadjusted').val('<%=modDet.GetSetqtyadjusted%>');
                        $('#addcostadjusted').val('<%=modDet.GetSetcostadjusted%>');
                        $('#addremarks').val('<%=modDet.GetSetremarks%>');
                    }
                    <%
                    }
                    %>
                    
                    updateDatePicker($('#adddatesoh').val());

                    //get latest qtySOH & costSOH
                    $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                        $('#addqtysoh').val(mps.qtysoh);
                        $('#addcostsoh').val(mps.costsoh);
                        calculateTotalAmount();
                        //console.log('2a');
                        //console.log(mps);
                    });

                    /*
                    $('#datetimepicker1').daterangepicker({
                        singleDatePicker: true,
                        timePicker: true,
                        timePicker12Hour: false,
                        timePickerIncrement: 1,
                        timePickerSeconds: true,
                        format: "DD-MM-YYYY HH:mm:ss",
                        drops: "up",
                        startDate: $('#adddatesoh').val(),
                        calender_style: "picker_4"
                    }, function (start, end, label) {
                        $('#adddatesoh').val(start.format('DD-MM-YYYY HH:mm:ss'));
                    });
                    */
                }

                function updateDatePicker(datestart) {

                    //var vDate = moment(Date.now(), 'DD-MM-YYYY HH:mm:ss');
                    var vDate = moment(Date.now()).format("DD-MM-YYYY HH:mm:ss");

                    if (datestart.length > 0) {
                        vDate = datestart;
                    } else {

                    }

                    //alert(vDate);

                    $('#datetimepicker1').daterangepicker({
                        singleDatePicker: true,
                        timePicker: true,
                        timePicker12Hour: false,
                        timePickerIncrement: 1,
                        timePickerSeconds: true,
                        format: "DD-MM-YYYY HH:mm:ss",
                        drops: "up",
                        startDate: vDate,
                        calender_style: "picker_4"
                    }, function (start, end, label) {
                        $('#adddatesoh').val(start.format('DD-MM-YYYY HH:mm:ss'));
                        //get latest qtySOH & costSOH
                        $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                            $('#addqtysoh').val(mps.qtysoh);
                            $('#addcostsoh').val(mps.costsoh);
                            calculateTotalAmount();
                            //console.log('3');
                            //console.log(mps);

                        });
                    });

                    $('#datetimepicker1').on('apply.daterangepicker', function (ev, picker) {
                        $('#adddatesoh').val(picker.startDate.format('DD-MM-YYYY HH:mm:ss'));
                        //get latest qtySOH & costSOH
                        $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                            $('#addqtysoh').val(mps.qtysoh);
                            $('#addcostsoh').val(mps.costsoh);
                            calculateTotalAmount();
                            //console.log('4');
                            //console.log(mps);
                        });
                    });

                }
                
                function updateDateSOH() {
                    dateSOHArray = $.map(dateSOH, function (value, key) {
                        return {
                            value: value,
                            data: key
                        };
                    });
                    // initialize autocomplete with custom appendTo
                    $('#adddatesoh').autocomplete({
                        lookup: dateSOHArray,
                        appendTo: '#adddatesoh-container',
                        minLength: 0,
                        minChars: 0,
                        onSelect: function (suggestion) {
                            updateDatePicker($('#adddatesoh').val());
                            //get latest qtySOH & costSOH
                            $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                                $('#addqtysoh').val(mps.qtysoh);
                                $('#addcostsoh').val(mps.costsoh);
                                calculateTotalAmount();
                                //console.log('4a');
                                //console.log(mps);
                            });
                        }
                    }).click(function () { $(this).autocomplete("search", " "); });
                }

                function confirmdeletelineitem(lineno) {
                    $('#hidLineNo').val(lineno);
                }

                //enable & disable button
                $(document).ready(function () {

                    $('#addlineitem').prop('disabled', true);
                    //$('#printorder').prop('disabled', true);
                    $('#adjustmentdetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    //$('#printorder').attr('disabled', 'disabled');
                    $('#adjustmentdetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModAdjustment.GetSetstatus.Equals("CONFIRMED") && !oModAdjustment.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsAdjustmentLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    //$('#printorder').prop('disabled', false);
                    $('#adjustmentdetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    //$('#printorder').removeAttr('disabled');
                    $('#adjustmentdetails').removeAttr('disabled');

                <%
                            }
                            else
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#addlineitem').removeAttr('disabled');

                <%
                            }
                        }
                        else
                        {
                            if (lsAdjustmentLineItem.Count > 0)
                            {
                %>
                    //$('#printorder').prop('disabled', false);
                    //$('#printorder').removeAttr('disabled');
                    $('#adjustmentdetails').prop('disabled', false);
                    $('#adjustmentdetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                    //var start = moment($('#adddatesoh').val(), 'DD-MM-YYYY HH:mm:ss');
                    /*
                    $('#datetimepicker1').daterangepicker({
                        singleDatePicker: true,
                        timePicker: true,
                        timePicker12Hour: false,
                        timePickerIncrement: 1,
                        timePickerSeconds: true,
                        format: "DD-MM-YYYY HH:mm:ss",
                        drops: "up",
                        /*
                        startDate: function () { return document.getElementById("adddatesoh").value },
                        startDate: function () { return $('#adddatesoh').val() },
                        startDate: moment('12-8-2016 09:26:21','DD-MM-YYYY HH:mm:ss'),
                        */
                        /* 
                        startDate: "",
                        calender_style: "picker_4"
                    }, function (start, end, label) {
                        $('#adddatesoh').val(start.format('DD-MM-YYYY HH:mm:ss'));
                    });                    
                    */
                    //$('#datetimepicker1').datetimepicker();

                });

            </script>            
            <!-- jQuery autocomplete -->
            <script>
                $(document).ready(function () {

                    //var dateSOH = { 'datesoh': '12-08-2016 12:34:22', 'DATE2': '21-09-2016 12:34:22', 'DATE3': '16-11-2016 12:34:22' };                    
                    /*
                    dateSOHArray = $.map(dateSOH, function (value, key) {
                        return {
                            value: value,
                            data: key
                        };
                    });

                    // initialize autocomplete with custom appendTo
                    $('#adddatesoh').autocomplete({
                        lookup: dateSOHArray,
                        appendTo: '#adddatesoh-container',
                        minLength: 0,
                        minChars: 0,
                        onSelect: function (suggestion) {
                            updateDatePicker($('#adddatesoh').val());
                        }
                    }).click(function() { $(this).autocomplete("search", " "); });
                    */

                    updateDateSOH();
                    // defined value of other bp
                    $('#adddatesoh').blur(function () {
                        //to allow user select suggested value only
                        updateDatePicker($('#adddatesoh').val());

                        //get latest qtySOH & costSOH
                        //alert("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val());
                        $.getJSON("GeneralHandler.ashx?action=GET_QTYSOH&comp=<%=oModAdjustment.GetSetcomp%>&itemno=" + $('#additemno').val() + "&location=" + $('#addlocation').val() + "&datesoh=" + $('#adddatesoh').val(), function (mps) {
                            //var dateSOH = mps;
                            //updateDateSOH();
                            //console.log(mps);
                            $('#addqtysoh').val(mps.qtysoh);
                            $('#addcostsoh').val(mps.costsoh);
                            calculateTotalAmount();
                            //console.log('5');
                            //console.log(mps);
                        });
                    });

                });
            </script>
            <!-- /jQuery autocomplete -->

</asp:Content>

