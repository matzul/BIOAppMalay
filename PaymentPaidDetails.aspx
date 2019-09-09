<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="PaymentPaidDetails.aspx.cs" Inherits="PaymentPaidDetails" %>

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
                                    <label for="paypaidno">No. Bayaran Belanja:</label>
                                    <input type="text" id="paypaidno" class="form-control" readonly="readonly" name="paypaidno" value="<%=oModPayPaid.GetSetpaypaidno %>" />
                                    <label for="payrpaiddate">Tarikh Bayaran Belanja:</label>
                                    <input type="text" id="payrpaiddate" class="form-control" readonly="readonly" name="payrpaiddate" value="<%=oModPayPaid.GetSetpaypaiddate %>" />
                                    <label for="paypaidtype">Jenis Bayaran Belanja:</label>
                                      <select class="form-control" id="paypaidtype" name="paypaidtype" required="required">
                                        <option value="">-select-</option>
                                        <option value="VOUCHER" <%=oModPayPaid.GetSetpaypaidtype.Equals("VOUCHER")?"selected":"" %>>BIL & BELANJA</option>
                                      </select>
                                    <label for="remarks">Catatan:</label>
                                    <textarea id="remarks" class="form-control" rows="3" name="remarks"><%=oModPayPaid.GetSetremarks%></textarea>
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" name="status" readonly="readonly" value="<%=oModPayPaid.GetSetstatus %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Bayaran Belanja Kepada:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option></option>
                                        <%
                                            if (lsBP.Count > 0)
                                            {
                                                for (int i = 0; i < lsBP.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modBP = (MainModel)lsBP[i];
                                                    if (oModPayPaid.GetSetbpid.Equals(modBP.GetSetbpid))
                                                    {
                                                        selected_bp = modBP.GetSetbpdesc;
                                                        selected = "selected";
                                                    }
                                        %>       
                                                    <option value="<%=modBP.GetSetbpid %>" <%=selected %>><%=modBP.GetSetbpdesc %></option>
                                        <% 
                                                }
                                            }
                                        %>
                                    </select>
                                    <input type="<%=selected_bp.Equals("OTHER")?"text":"hidden"%>" name="bpdesc" id="bpdesc" required="required" class="form-control" value="<%=oModPayPaid.GetSetbpdesc %>" />
                                    <div id="bpdesc-container" style="position: relative; float: left; width: 100%; margin: 10px;"></div>
                                    <label for="bpaddress">Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="4" readonly="readonly" required="required" name="bpaddress"><%=oModPayPaid.GetSetbpaddress%></textarea>
                                    <label for="bpcontact">Pegawai Dihubungi/ No. Tel.:</label>
                                    <input type="text" id="bpcontact" class="form-control" readonly="readonly" required="required" name="bpcontact" value="<%=oModPayPaid.GetSetbpcontact%>" />
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
                                            if (!oModPayPaid.GetSetstatus.Equals("CONFIRMED") && !oModPayPaid.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsPayPaidLineItem.Count > 0)
                                                { 
                                    %>
                                    <button id="btnApprove" name="btnApprove" type="button" class="btn btn-success" onclick="actionclick('CONFIRM');" >Confirm</button>
                                    <%
                                                }
                                    %>
                                    <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-paypaid">Batal</button>
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
                                        <input type="hidden" name="hidPayPaidNo" id="hidPayPaidNo" value="<%=sPayPaidNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Bayaran Belanja</h4>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Bil & Belanja<span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addexpensesno" name="addexpensesno" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option></option>
                                      <%
                                          for (int i = 0; i < lsPendPayPaidMod.Count; i++)
                                          {
                                              MainModel oPendPayPaid = (MainModel)lsPendPayPaidMod[i];
                                      %>                         
                                            <option value="<%=oPendPayPaid.GetSetexpensesno %>"><%=oPendPayPaid.GetSetexpensesno %>/DATE [<%=oPendPayPaid.GetSetexpensesdate %>]/AMOUNT [<%=oPendPayPaid.GetSettotalamount-oPendPayPaid.GetSetpaypaidamount %>]</option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh Bil & Belanja</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="addexpensesdate" name="addexpensesdate" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Bil & Belanja</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addexpensesamount" name="addexpensesamount" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis Bayaran</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addpaytype" name="addpaytype" class="form-control" tabindex="-1" style="width:100%;">
                                            <option value="">-select-</option>
                                            <option value="CASH">CASH</option>
                                            <option value="CHEQUE">CEK</option>
                                            <option value="BANKING">BANK (EFT/I-Banking/Kad)</option>
                                            <option value="CONTRA_PAYMENT">BAYARAN KONTRA</option>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Rujukan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addpayrefno" name="addpayrefno" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Bayaran Belanja
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addpaypaidamount" name="addpaypaidamount" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Catatan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addpayremarks" name="addpayremarks" type="text" class="form-control" value=""/>
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
                    <!--BEGIN dialog box for confirm cancel payment paid-->
                    <div class="modal fade modal-confirm-cancel-paypaid" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <!--END dialog box for confirm delete-->

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
                    <a id="paiddetails" name="paiddetails" class="btn btn-app" data-toggle="modal" data-target=".modal-paid-details">
                      <i class="fa fa-edit dark"></i>Maklumat Tambahan
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th></th>
                              <th>No</th>
                              <th>Jenis Bayaran Belanja</th>
                              <th>No. Rujukan</th>
                              <th>No. Bil & Belanja</th>
                              <th>Tarikh Bil & Belanja</th>
                              <th>Jumlah Bil & Belanja</th>
                              <th>Jumlah Bayaran Belanja</th>
                              <th>Catatan</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsPayPaidLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsPayPaidLineItem.Count; i++)
                                {
                                    MainModel modPayPaidDet = (MainModel)lsPayPaidLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modPayPaidDet.GetSetlineno%></td>
                              <td><%=modPayPaidDet.GetSetpaytype%></td>
                              <td><%=modPayPaidDet.GetSetpayrefno%></td>
                              <td><%=modPayPaidDet.GetSetexpensesno%></td>
                              <td><%=modPayPaidDet.GetSetexpensesdate%></td>
                              <td><%=modPayPaidDet.GetSetexpensesprice%></td>
                              <td><%=modPayPaidDet.GetSetpaypaidprice%></td>
                              <td><%=modPayPaidDet.GetSetpayremarks%></td>
                              <td>
                                  <%
                                    if (!oModPayPaid.GetSetstatus.Equals("CONFIRMED") && !oModPayPaid.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-info btn-xs" onclick="openeditlineitem('<%=modPayPaidDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-add-line-item"><i class="fa fa-pencil"></i> Kemaskini </a>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modPayPaidDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
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
            <!--dialog box for payment paid details-->
            <div class="modal fade modal-paid-details" tabindex="-1" role="dialog" aria-hidden="true">
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
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Bil & Belanja</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalexpensesamount" name="totalexpensesamount" type="text" class="form-control" readonly="readonly" value="<%=oModPayPaid.GetSetexpensesamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Bayaran Belanja</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalpaypaidamount" name="totalpaypaidamount" type="text" class="form-control" readonly="readonly" value="<%=oModPayPaid.GetSetpaypaidamount %>"/>
                            </div>
                            </div>

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
                                  <td><%=oModPayPaid.GetSetcreatedby %></td>
                                  <td><%=oModPayPaid.GetSetcreateddate %></td>
                                </tr>
                                <tr>
                                  <td>Confirmed</td>
                                  <td><%=oModPayPaid.GetSetconfirmedby %></td>
                                  <td><%=oModPayPaid.GetSetconfirmeddate %></td>
                                </tr>
                                <tr>
                                  <td>Cancelled</td>
                                  <td><%=oModPayPaid.GetSetcancelledby %></td>
                                  <td><%=oModPayPaid.GetSetcancelleddate %></td>
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

                //populate bp details, item discount
                $(document).ready(function () {
                    $('#bpid').change(function () {
                        if ($(this).val() == '') {
                            document.getElementById("bpaddress").value = "";
                            $('#bpcontact').val('');
                            $('#bpdesc').val('');
                        }
                    <%
                                  if (lsBP.Count > 0)
                                  {
                                      for (int i = 0; i < lsBP.Count; i++)
                                      {
                                          MainModel modBP = (MainModel)lsBP[i];
                    %>      
                                            if ($(this).val() == "<%=modBP.GetSetbpid%>") {
                                                document.getElementById("bpaddress").value = "<%=modBP.GetSetbpaddress%>";
                                                $('#bpcontact').val("<%=modBP.GetSetbpcontact%>");
                                                $('#bpdesc').val("<%=modBP.GetSetbpdesc%>");
                                            }
                    <% 
                                      }
                                  }
                    %>
                        if ($('#bpdesc').val() == 'OTHER') {
                            document.getElementById("bpaddress").value = "";
                            $('#bpcontact').val('');
                            $('#bpdesc').attr('type', 'text');
                            $('#bpdesc').val('');
                        } else {
                            $('#bpdesc').attr('type', 'hidden');
                        }

                    });

                    $('#addexpensesno').change(function () {
                        if ($(this).val() == '') {
                            $('#addexpensesdate').val('0');
                            $('#addexpensesamount').val('0');
                        }
                        <%
                            if (lsPendPayPaidMod.Count > 0)
                            {
                                for (int i = 0; i < lsPendPayPaidMod.Count; i++)
                                {
                                    MainModel modPendPayPaid = (MainModel)lsPendPayPaidMod[i];
                        %>
                                    if ($(this).val() == '<%=modPendPayPaid.GetSetexpensesno%>') {
                                        $('#addexpensesdate').val('<%=modPendPayPaid.GetSetexpensesdate%>');
                                        $('#addexpensesamount').val('<%=modPendPayPaid.GetSettotalamount-modPendPayPaid.GetSetpaypaidamount%>');
                                    }
                        <% 
                                }
                            }
                        %>
                    });

                });

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#paypaidtype').removeAttr('required');
                        $('#bpid').removeAttr('required');
                        $('#bpaddress').removeAttr('required');
                        $('#bpcontact').removeAttr('required');
                        $('#status').removeAttr('required');
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update payment receipt header for case already have Line Item which logic, disable user from updating new bpid
                        $('#bpid').prop('disabled', false);
                        $('#bpdesc').prop('disabled', false);
                        $('#bpaddress').prop('disabled', false);
                        $('#bpcontact').prop('disabled', false);
                        $('#paypaidtype').prop('disabled', false);

                        if ($('#bpdesc').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please select a valid Other Business Partner in the list!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }

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
                                text: 'Please fill out "Line No" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addexpensesno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Item Bil & Belanja"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addpaytype').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Jenis Bayaran"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addpayrefno').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "No. Rujukan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addpaypaidamount').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Jumlah Bayaran Belanja"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addpaypaidamount').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Jumlah Bayaran Belanja"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
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
                    if (lsPayPaidLineItem.Count > 0)
                    { 
                    %>
                        $('#bpid').prop('disabled', true);
                        $('#bpid').prop('disabled', true);
                        $('#bpdesc').prop('disabled', true);
                        $('#bpaddress').prop('disabled', true);
                        $('#bpcontact').prop('disabled', true);
                        $('#paypaidtype').prop('disabled', true);
                    <%
                    }else{ 
                    %>
                        $('#bpid').prop('disabled', flag);
                        $('#bpdesc').prop('disabled', flag);
                        $('#paypaidtype').prop('disabled', flag);
                        <%
                            if (selected_bp.Equals("OTHER"))
                            {
                        %>
                                $('#bpdesc').attr('type', 'text');
                                $('#bpaddress').prop('disabled', flag);
                                $('#bpcontact').prop('disabled', flag);
                        <%
                            }else{ 
                        %>
                                $('#bpdesc').attr('type', 'hidden');
                                $('#bpaddress').prop('disabled', true);
                                $('#bpcontact').prop('disabled', true);
                        <%
                        }
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
                    $('#addlineno').val('<%=lsPayPaidLineItem.Count + 1%>');
                }

                function openeditlineitem(lineno) {
                    $('#btnAddLineItem').hide();
                    $('#btnEditLineItem').show();

                    $('#hidLineNo').val(lineno);
                    <%
                    for (int i = 0; i < lsPayPaidLineItem.Count; i++)
                    {
                        MainModel modPayPaidDet = (MainModel)lsPayPaidLineItem[i];
                    %>
                    if ($('#hidLineNo').val() == '<%=modPayPaidDet.GetSetlineno%>') {
                        $('#addlineno').val('<%=modPayPaidDet.GetSetlineno%>');
                        $('#addexpensesno').val('<%=modPayPaidDet.GetSetexpensesno%>').change();
                        $('#addexpensesdate').text('<%=modPayPaidDet.GetSetexpensesdate%>');
                        $('#addexpensesamount').val('<%=modPayPaidDet.GetSetexpensesprice%>');
                        $('#addpaytype').val('<%=modPayPaidDet.GetSetpaytype%>').change();
                        $('#addpayrefno').val('<%=modPayPaidDet.GetSetpayrefno%>');
                        $('#addpaypaidamount').val('<%=modPayPaidDet.GetSetpaypaidprice%>');
                        $('#addpayremarks').val('<%=modPayPaidDet.GetSetpayremarks%>');
                    }
                    <%
                    }
                    %>

                }

                function confirmdeletelineitem(lineno) {
                    $('#hidLineNo').val(lineno);
                }

                //enable & disable button
                $(document).ready(function () {

                    $('#addlineitem').prop('disabled', true);
                    $('#printorder').prop('disabled', true);
                    $('#paiddetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printorder').attr('disabled', 'disabled');
                    $('#paiddetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModPayPaid.GetSetstatus.Equals("CONFIRMED") && !oModPayPaid.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsPayPaidLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printorder').prop('disabled', false);
                    $('#paiddetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printorder').removeAttr('disabled');
                    $('#paiddetails').removeAttr('disabled');

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
                            if (lsPayPaidLineItem.Count > 0)
                            {
                %>
                    $('#printorder').prop('disabled', false);
                    $('#printorder').removeAttr('disabled');
                    $('#paiddetails').prop('disabled', false);
                    $('#paiddetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

            </script>            

            <!-- jQuery autocomplete -->
            <script>
                $(document).ready(function () {
                <%
                if (lsOtherBP.Count > 0)
                {
                %>
                    var bpother = {
                        <%
                        for (int i = 0; i < lsOtherBP.Count; i++)
                        {
                            MainModel modOthBP = (MainModel)lsOtherBP[i];
                            if (i == 0) 
                            { 
                        %>
                            <%=modOthBP.GetSetcomp%><%=i%>:"<%=modOthBP.GetSetobpdesc%>"
                        <%
                            }
                            else 
                            { 
                        %>
                            ,<%=modOthBP.GetSetcomp%><%=i%>:"<%=modOthBP.GetSetobpdesc%>"
                        <%
                            }
                        }
                        %>
                    };

                    var bpotherArray = $.map(bpother, function (value, key) {
                        return {
                            value: value,
                            data: key
                        };
                    });

                    // initialize autocomplete with custom appendTo
                    $('#bpdesc').autocomplete({
                        lookup: bpotherArray,
                        appendTo: '#bpdesc-container',
                        onSelect: function(suggestion){
                                    <%
                                    for (int i = 0; i < lsOtherBP.Count; i++)
                                    {
                                        MainModel modOthBP = (MainModel)lsOtherBP[i];
                                        if (i == 0) 
                                        {
                                    %>
                            if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                            {
                                document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                            $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                        }
                                    <%
                                        }
                                        else if (i < lsOtherBP.Count)
                                        { 
                                    %>
                                        else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                        {
                                            document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                            $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                        }
                                    <%
                                        }

                                        if (i == lsOtherBP.Count - 1) 
                                        {
                                    %>
                                        else
                                        {
                                            document.getElementById("bpaddress").value = "";
                                            $('#bpcontact').val('');
                                        }
                                        <%
                                        }
                                    }
                                    %>
                        }
                    });

                    // defined value of other bp
                    $('#bpdesc').blur(function () {
                        <%
                        for (int i = 0; i < lsOtherBP.Count; i++)
                        {
                            MainModel modOthBP = (MainModel)lsOtherBP[i];
                            if (i == 0) 
                            {
                        %>
                        if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                        {
                            document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                    $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                }
                        <%
                            }
                            else if (i < lsOtherBP.Count)
                            { 
                        %>
                                else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                {
                                    document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                    $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                }
                        <%
                            }

                            if (i == lsOtherBP.Count - 1) 
                            {
                        %>
                                else
                                {
                                    $('#bpdesc').val('');
                                    document.getElementById("bpaddress").value = "";
                                    $('#bpcontact').val('');
                                }
                        <%
                            }
                        }
                        %>

                    });
                    <%
                }
                %>
                });
            </script>
            <!-- /jQuery autocomplete -->

</asp:Content>

