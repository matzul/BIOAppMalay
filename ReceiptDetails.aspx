<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="ReceiptDetails.aspx.cs" Inherits="ReceiptDetails" %>

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
                                    <label for="shipmentno">No. Penerimaan:</label>
                                    <input type="text" id="receiptno" class="form-control" readonly="readonly" name="receiptno" value="<%=oModHeader.GetSetreceiptno %>" />
                                    <label for="shipmentdate">Tarikh Penerimaan:</label>
                                    <input type="text" id="receiptdate" class="form-control" readonly="readonly" name="receiptdate" value="<%=oModHeader.GetSetreceiptdate %>" />
                                    <label for="receiptcat">Kategori:</label>
                                      <select class="form-control" id="receiptcat" name="receiptcat" required="required">
                                        <option value="">-select-</option>
                                        <option value="PURCHASE_ORDER" <%=oModHeader.GetSetreceiptcat.Equals("PURCHASE_ORDER")?"selected":"" %>>PESANAN BELIAN</option>
                                        <option value="RECEIVE_ORDER" <%=oModHeader.GetSetreceiptcat.Equals("RECEIVE_ORDER")?"selected":"" %>>PESANAN TERIMAAN</option>
                                        <option value="TRANSFER_ORDER" <%=oModHeader.GetSetreceiptcat.Equals("TRANSFER_ORDER")?"selected":"" %>>PESANAN PINDAHAN</option>
                                      </select>
                                    <label for="remarks">Catatan:</label>
                                    <textarea id="remarks" class="form-control" rows="3" name="remarks"><%=oModHeader.GetSetremarks%></textarea>
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" name="status" readonly="readonly" value="<%=oModHeader.GetSetstatus %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Terima Daripada:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option value="">-select-</option>
                                        <%
                                            if (lsBP.Count > 0 && (oModHeader.GetSetreceiptcat.Equals("PURCHASE_ORDER") || oModHeader.GetSetreceiptcat.Equals("RECEIVE_ORDER")))
                                            {
                                                for (int i = 0; i < lsBP.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modBP = (MainModel)lsBP[i];
                                                    if (oModHeader.GetSetbpid.Equals(modBP.GetSetbpid))
                                                    {
                                                        selected_bp = modBP.GetSetbpdesc;
                                                        selected = "selected";
                                                    }
                                        %>       
                                                    <option value="<%=modBP.GetSetbpid %>" <%=selected %>><%=modBP.GetSetbpdesc %></option>
                                        <% 
                                                }
                                            }
                                            else if (lsComp.Count > 0 && oModHeader.GetSetreceiptcat.Equals("TRANSFER_ORDER"))
                                            {
                                                for (int i = 0; i < lsComp.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modComp = (MainModel)lsComp[i];
                                                    if (oModHeader.GetSetbpid.Equals(modComp.GetSetcomp))
                                                    {
                                                        //selected_bp = modBP.GetSetbpdesc;
                                                        selected = "selected";
                                                    }
                                        %>       
                                                    <option value="<%=modComp.GetSetcomp %>" <%=selected %>><%=modComp.GetSetcomp_name %></option>
                                        <% 
                                                }
                                            }
                                        %>
                                    </select>
                                    <input type="hidden" id="bpdesc" name="bpdesc" value="<%=oModHeader.GetSetbpdesc%>" />
                                    <label for="bpaddress">Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="4" readonly="readonly" name="bpaddress"><%=oModHeader.GetSetbpaddress%></textarea>
                                    <label for="bpcontact">Pegawai Dihubungi/ Tel. No.:</label>
                                    <input type="text" id="bpcontact" class="form-control" readonly="readonly" name="bpcontact" value="<%=oModHeader.GetSetbpcontact%>" />
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
                                            if (!oModHeader.GetSetstatus.Equals("CONFIRMED") && !oModHeader.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsLineItem.Count > 0)
                                                { 
                                    %>
                                    <button id="btnApprove" name="btnApprove" type="button" class="btn btn-success" onclick="actionclick('CONFIRM');" >Confirm</button>
                                    <%
                                                }
                                    %>
                                    <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-receipt">Batal</button>
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
                                        <input type="hidden" name="hidReceiptNo" id="hidReceiptNo" value="<%=sReceiptNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Penerimaan</h4>
                                    </div>
                                    <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">

                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addlineno" name="addlineno" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Pesanan Belian/Terimaan <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="orderitem" name="orderitem" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option value="">-select-</option>
                                      <%
                                          for (int i = 0; i < lsPendReceipt.Count; i++)
                                          {
                                              MainModel oPendReceipt = (MainModel)lsPendReceipt[i];
                                      %>                         
                                            <option value="<%=oPendReceipt.GetSetorderno %>_<%=oPendReceipt.GetSetlineno %>"><%=oPendReceipt.GetSetorderno %>/ LINE_NO [<%=oPendReceipt.GetSetlineno %>]/ ITEM [<%=oPendReceipt.GetSetitemno %>]/ ORDER QTY [<%=oPendReceipt.GetSetorder_quantity%>]</option>
                                      <%
                                          }
                                      %>
                                          </select>
                                          <input type="hidden" id="addorderno" name="addorderno" value="" />
                                          <input type="hidden" id="addorder_lineno" name="addorder_lineno" value="0" />
                                          <input type="hidden" id="additemno" name="additemno" value="" />
                                          <input type="hidden" id="additemdesc" name="additemdesc" value="" />
                                          <input type="hidden" id="additemcat" name="additemcat" value="" />
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addorder_quantity" name="addorder_quantity" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Lokasi Inventori</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addlocation" name="addlocation" class="select2_single form-control" readonly="readonly" tabindex="-1" style="width:100%;">
                                            <option value="">-select-</option>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti Penerimaan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addreceipt_quantity" name="addreceipt_quantity" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Baki Kuantiti Pesanan</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="qtybalance" name="qtybalance" type="text" class="form-control" readonly="readonly" value="0"/>
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
                    <!--BEGIN dialog box for confirm cancel Receipt-->
                    <div class="modal fade modal-confirm-cancel-receipt" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <a id="printreceipt" name="printreceipt" class="btn btn-app" onclick="openprintreceipt();">
                      <i class="fa fa-print dark"></i>Cetak Penerimaan
                    </a>
                    <a id="receiptdetails" name="receiptdetails" class="btn btn-app" data-toggle="modal" data-target=".modal-receipt-details">
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
                              <th>No Pesanan</th>
                              <th>No</th>
                              <th>Kuantiti Pesanan</th>
                              <th>Lokasi Inventori</th>
                              <th>Kuantiti Penerimaan</th>
                              <th>Baki Kuantiti Pesanan</th>
                              <th>Catatan</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsLineItem.Count; i++)
                                {
                                    MainModel modDet = (MainModel)lsLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modDet.GetSetlineno%></td>
                              <td><%=modDet.GetSetitemno%></td>
                              <td><%=modDet.GetSetitemdesc%></td>
                              <td><%=modDet.GetSetorderno%></td>
                              <td><%=modDet.GetSetorder_lineno%></td>
                              <td><%=modDet.GetSetorder_quantity%></td>
                              <td><%=modDet.GetSetlocation%></td>
                              <td><%=modDet.GetSetreceipt_quantity%></td>
                              <td><%=modDet.GetSetorder_quantity - modDet.GetSetreceipt_quantity%></td>
                              <td><%=modDet.GetSetremarks%></td>
                              <td>
                                  <%
                                    if (!oModHeader.GetSetstatus.Equals("CONFIRMED") && !oModHeader.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
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
            <!--dialog box for receipt details-->
            <div class="modal fade modal-receipt-details" tabindex="-1" role="dialog" aria-hidden="true">
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
                                  <td><%=oModHeader.GetSetcreatedby %></td>
                                  <td><%=oModHeader.GetSetcreateddate %></td>
                                </tr>
                                <tr>
                                  <td>Confirmed</td>
                                  <td><%=oModHeader.GetSetconfirmedby %></td>
                                  <td><%=oModHeader.GetSetconfirmeddate %></td>
                                </tr>
                                <tr>
                                  <td>Cancelled</td>
                                  <td><%=oModHeader.GetSetcancelledby %></td>
                                  <td><%=oModHeader.GetSetcancelleddate %></td>
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

                    $('#receiptcat').change(function () {
                        if ($(this).val() == 'TRANSFER_ORDER' && <%=(lsComp.Count>0?"true":"false")%>==true) {
                            var select = document.getElementById("bpid");
                            for (var option in select) {
                                select.remove(option);
                            }
                            document.getElementById("bpaddress").value = "";
                            $('#bpcontact').val('');
                            $('#bpdesc').val('');
                            document.getElementById("bpid").add(new Option("-Select-",""));
                            <%
                            for (int i = 0; i < lsComp.Count; i++)
                            {
                                MainModel modComp = (MainModel)lsComp[i];
                            %>
                                document.getElementById("bpid").add(new Option("<%=modComp.GetSetcomp_name%>", "<%=modComp.GetSetcomp%>"));
                            <%
                            }
                            %>
                        }
                        else
                        {
                            var select = document.getElementById("bpid");
                            for (var option in select) {
                                select.remove(option);
                            }
                            document.getElementById("bpaddress").value = "";
                            $('#bpcontact').val('');
                            $('#bpdesc').val('');
                            document.getElementById("bpid").add(new Option("-Select-",""));
                            <%
                            for (int i = 0; i < lsBP.Count; i++)
                            {
                                MainModel modBP = (MainModel)lsBP[i];
                            %>
                                document.getElementById("bpid").add(new Option("<%=modBP.GetSetbpdesc%>", "<%=modBP.GetSetbpid%>"));
                            <%
                            }
                            %>
                        }
                    });

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
                                                document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modBP.GetSetbpaddress, ", ")%>";
                                                $('#bpcontact').val("<%=modBP.GetSetbpcontact%>");
                                                $('#bpdesc').val("<%=modBP.GetSetbpdesc%>");
                                            }
                    <% 
                                      }
                                  }
                    %>
                    <%
                                  if (lsComp.Count > 0)
                                  {
                                      for (int i = 0; i < lsComp.Count; i++)
                                      {
                                          MainModel modComp = (MainModel)lsComp[i];
                    %>      
                                            if ($(this).val() == "<%=modComp.GetSetcomp%>") {
                                                document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modComp.GetSetcomp_address, ", ")%>";
                                                $('#bpcontact').val("<%=modComp.GetSetcomp_contact%>");
                                                $('#bpdesc').val("<%=modComp.GetSetcomp_name%>");
                                            }
                    <% 
                                      }
                                  }
                    %>

                    });

                    $('#orderitem').change(function () {
                        if ($(this).val() == '') {
                            $('#addorderno').val('');
                            $('#addorder_lineno').val('');
                            $('#additemno').val('');
                            $('#additemdesc').val('');
                            $('#additemcat').val('');
                            $('#addorder_quantity').val('0');
                            $('#addreceipt_quantity').val('0');
                            $('#qtybalance').val('0');
                        }
                        <%
                            if (lsPendReceipt.Count > 0)
                            {
                                for (int i = 0; i < lsPendReceipt.Count; i++)
                                {
                                    MainModel oPendReceipt = (MainModel)lsPendReceipt[i];
                        %>
                                    if ($(this).val() == '<%=oPendReceipt.GetSetorderno %>_<%=oPendReceipt.GetSetlineno %>')
                                    {
                                        $('#addorderno').val('<%=oPendReceipt.GetSetorderno%>');
                                        $('#addorder_lineno').val('<%=oPendReceipt.GetSetlineno%>');
                                        $('#additemno').val('<%=oPendReceipt.GetSetitemno %>');
                                        $('#additemdesc').val("<%=oPendReceipt.GetSetitemdesc %>");
                                        $('#additemcat').val('<%=oPendReceipt.GetSetitemcat %>');
                                        $('#addorder_quantity').val('<%=oPendReceipt.GetSetorder_quantity %>');
                                        $('#addreceipt_quantity').val('0');
                                        $('#qtybalance').val('<%=oPendReceipt.GetSetorder_quantity %>');
                                    }
                        <% 
                                }
                            }
                        %>

                        //disable location for item is not inventory
                        if ($('#additemcat').val() == 'INVENTORY') {
                            //$('#addlocation').change();
                        } else {
                            $('#addlocation').val('').change();
                        }

                        calculateReceiptQuantity();
                    });

                    $('#addlocation').change(function () {
                        if ($('#additemcat').val().length > 0 && $('#additemcat').val() == 'INVENTORY') {
                        }
                        else if ($('#additemcat').val().length > 0 && $('#additemcat').val() == 'ASSET') {
                        }
                        else if ($('#additemcat').val().length > 0 && $('#addlocation').val().length > 0) {
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila abaikan jika Item bukan kategori STOK/ INVENTORI/ ASET!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                            $('#addlocation').val('').change();
                        }
                    });

                    $('#addreceipt_quantity').blur(function () {
                        var proceed = true;
                        if ($('#addreceipt_quantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penerimaan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addreceipt_quantity').val()) == 0) {
                            proceed = true;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penerimaan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        if (proceed) {
                            calculateReceiptQuantity();
                        }
                    });
                });

                function calculateReceiptQuantity() {
                    var qtybalance = parseInt($('#addorder_quantity').val()) - parseInt($('#addreceipt_quantity').val());
                    $('#qtybalance').val(qtybalance);
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#receiptcat').removeAttr('required');
                        $('#bpid').removeAttr('required');
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update payment receipt header for case already have Line Item which logic, disable user from updating new bpid
                        $('#bpid').prop('disabled', false);
                        $('#receiptcat').prop('disabled', false);

                        if ($('#bpdesc').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please select a valid Business Partner in the list!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }

                    }
                    if (action == 'INSERT')
                    {
                        if ($('#addlineno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please fill in "Line No" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#orderitem').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Item Pesanan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addorder_quantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error on order quantity!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addorder_quantity').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Kuantiti Pesanan adalah "0"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addlocation').val().length == 0 && $('#additemcat').val() == 'INVENTORY') {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Lokasi Inventori"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addreceipt_quantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penerimaan"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addreceipt_quantity').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penerimaan"!!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#qtybalance').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error on balance quantity!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#qtybalance').val()) < 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error on balance quantity!',
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
                    if (lsLineItem.Count > 0)
                    { 
                    %>
                        $('#bpid').prop('disabled', true);
                        $('#receiptcat').prop('disabled', true);
                    <%
                    }else{ 
                    %>
                        $('#bpid').prop('disabled', flag);
                        $('#receiptcat').prop('disabled', flag);
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
                    $('#addlineno').val('<%=lsLineItem.Count + 1%>');
                }

                function confirmdeletelineitem(lineno) {
                    $('#hidLineNo').val(lineno);
                }

                //enable & disable button
                $(document).ready(function () {

                    $('#addlineitem').prop('disabled', true);
                    $('#printreceipt').prop('disabled', true);
                    $('#receiptdetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printreceipt').attr('disabled', 'disabled');
                    $('#receiptdetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModHeader.GetSetstatus.Equals("CONFIRMED") && !oModHeader.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printreceipt').prop('disabled', false);
                    $('#receiptdetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printshipment').removeAttr('disabled');
                    $('#receiptdetails').removeAttr('disabled');

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
                            if (lsLineItem.Count > 0)
                            {
                %>
                    $('#printreceipt').prop('disabled', false);
                    $('#printreceipt').removeAttr('disabled');
                    $('#receiptdetails').prop('disabled', false);
                    $('#receiptdetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

            </script>            

</asp:Content>

