<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="ShipmentDetails.aspx.cs" Inherits="ShipmentDetails" %>

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
                                    <label for="shipmentno">No. Penghantaran:</label>
                                    <input type="text" id="shipmentno" class="form-control" readonly="readonly" name="shipmentno" value="<%=oModHeader.GetSetshipmentno %>" />
                                    <label for="shipmentdate">Tarikh Penghantaran:</label>
                                    <input type="text" id="shipmentdate" class="form-control" readonly="readonly" name="shipmentdate" value="<%=oModHeader.GetSetshipmentdate %>" />
                                    <label for="shipmentcat">Kategori:</label>
                                      <select class="form-control" id="shipmentcat" name="shipmentcat" required="required">
                                        <option value="">-Select-</option>
                                        <option value="SALES_ORDER" <%=oModHeader.GetSetshipmentcat.Equals("SALES_ORDER")?"selected":"" %>>PESANAN JUALAN</option>
                                        <!--<option value="GIVE_ORDER" <%=oModHeader.GetSetshipmentcat.Equals("GIVE_ORDER")?"selected":"" %>>PESANAN AGIHAN</option>-->
                                        <option value="TRANSFER_ORDER" <%=oModHeader.GetSetshipmentcat.Equals("TRANSFER_ORDER")?"selected":"" %>>PESANAN PINDAHAN</option>
                                      </select>
                                    <label for="remarks">Catatan:</label>
                                    <textarea id="remarks" class="form-control" rows="3" name="remarks"><%=oModHeader.GetSetremarks%></textarea>
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" name="status" readonly="readonly" value="<%=oModHeader.GetSetstatus %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Hantar Kepada:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option value="">-select-</option>
                                        <%
                                            if (lsBP.Count > 0 && (oModHeader.GetSetshipmentcat.Equals("SALES_ORDER") || oModHeader.GetSetshipmentcat.Equals("GIVE_ORDER")))
                                            {
                                                for (int i = 0; i < lsBP.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modBP = (MainModel)lsBP[i];
                                                    if (oModHeader.GetSetbpid.Equals(modBP.GetSetbpid))
                                                    {
                                                        //selected_bp = modBP.GetSetbpdesc;
                                                        selected = "selected";
                                                    }
                                        %>       
                                                    <option value="<%=modBP.GetSetbpid %>" <%=selected %>><%=modBP.GetSetbpdesc %></option>
                                        <% 
                                                }
                                            }
                                            else if (lsComp.Count > 0 && oModHeader.GetSetshipmentcat.Equals("TRANSFER_ORDER"))
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
                                    <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-shipment">Batal</button>
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
                                        <input type="hidden" name="hidShipmentNo" id="hidShipmentNo" value="<%=sShipmentNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Penghantaran</h4>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Pesanan Jualan/ Agihan <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="orderitem" name="orderitem" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option value="">-select-</option>
                                      <%
                                          for (int i = 0; i < lsPendShipment.Count; i++)
                                          {
                                              MainModel oPendShipment = (MainModel)lsPendShipment[i];
                                      %>                         
                                            <option value="<%=oPendShipment.GetSetorderno %>_<%=oPendShipment.GetSetlineno %>"><%=oPendShipment.GetSetorderno %>/ LINE_NO [<%=oPendShipment.GetSetlineno %>]/ ITEM [<%=oPendShipment.GetSetitemno %>]/ ORDER QTY [<%=oPendShipment.GetSetorder_quantity%>]</option>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Inventori</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="stockavailable" name="stockavailable" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option value="">-select-</option>
                                          </select>
                                          <input type="hidden" id="addlocation" name="addlocation" value="" />
                                          <input type="hidden" id="adddatesoh" name="adddatesoh" value="" />
                                          <input type="hidden" id="addqtysoh" name="addqtysoh" value="0" />
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Qty Tersedia</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addqtyavailable" name="addqtyavailable" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Qty Penghantaran</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addshipment_quantity" name="addshipment_quantity" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Baki Qty Pesanan</label>
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
                    <!--BEGIN dialog box for confirm cancel Shipment-->
                    <div class="modal fade modal-confirm-cancel-shipment" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <a id="printshipment" name="printshipment" class="btn btn-app" onclick="openprintshipment();">
                      <i class="fa fa-print dark"></i>Cetak Penghantaran
                    </a>
                    <a id="shipmentdetails" name="shipmentdetails" class="btn btn-app" data-toggle="modal" data-target=".modal-shipment-details">
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
                              <th>Qty Pesanan</th>
                              <th>Qty Penghantaran</th>
                              <th>Baki Qty Pesanan</th>
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
                              <td><%=modDet.GetSetshipment_quantity%></td>
                              <td><%=modDet.GetSetorder_quantity - modDet.GetSetshipment_quantity%></td>
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
                                <td>Tiada rekod...</td>
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
            <!--dialog box for shipment details-->
            <div class="modal fade modal-shipment-details" tabindex="-1" role="dialog" aria-hidden="true">
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

                    $('#shipmentcat').change(function () {
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
                                                document.getElementById("bpaddress").value = "<%=modBP.GetSetbpaddress%>";
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
                                                document.getElementById("bpaddress").value = "<%=modComp.GetSetcomp_address%>";
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
                            $('#addshipment_quantity').val('0');
                            $('#qtybalance').val('0');
                        }
                        <%
                            if (lsPendShipment.Count > 0)
                            {
                                for (int i = 0; i < lsPendShipment.Count; i++)
                                {
                                    MainModel oPendShipment = (MainModel)lsPendShipment[i];
                        %>
                                    if ($(this).val() == '<%=oPendShipment.GetSetorderno %>_<%=oPendShipment.GetSetlineno %>')
                                    {
                                        $('#addorderno').val('<%=oPendShipment.GetSetorderno%>');
                                        $('#addorder_lineno').val('<%=oPendShipment.GetSetlineno%>');
                                        $('#additemno').val('<%=oPendShipment.GetSetitemno %>');
                                        $('#additemdesc').val("<%=oPendShipment.GetSetitemdesc %>");
                                        $('#additemcat').val('<%=oPendShipment.GetSetitemcat %>');
                                        $('#addorder_quantity').val('<%=oPendShipment.GetSetorder_quantity %>');
                                        $('#addshipment_quantity').val('0');
                                        $('#qtybalance').val('<%=oPendShipment.GetSetorder_quantity %>');
                                    }
                        <% 
                                }
                            }
                        %>

                        if ($('#additemcat').val() == "INVENTORY") {

                            $('#stockavailable').prop('disabled', false);
                            //get latest qtySOH & costSOH
                            $.getJSON("GeneralHandler.ashx?action=GET_STOCKAVAILABLE&comp=<%=oModHeader.GetSetcomp%>&itemno=" + $('#additemno').val(), function (mps) {
                                //reset stockavailable, hidden field of stock information & balance order qty
                                var select = document.getElementById("stockavailable");
                                for (var option in select) {
                                    select.remove(option);
                                }
                                document.getElementById("stockavailable").add(new Option("-select-", ""));
                                $('#addlocation').val('');
                                $('#adddatesoh').val('');
                                $('#addqtysoh').val('0');
                                $('#addqtyavailable').val('0');

                                for (var i in mps) {
                                    //pure javascript
                                    document.getElementById("stockavailable").add(new Option(mps[i].item + "/ LOCATION [" + mps[i].location + "] / DATE SOH [ " + mps[i].datesoh + "] / QTY AVAILABLE [ " + mps[i].qtyavailable + "]", mps[i].item + "|" + mps[i].location + "|" + mps[i].datesoh + "|" + mps[i].qtysoh + "|" + mps[i].qtyavailable));
                                    //using jquery
                                }

                            });
                        } else {

                            $('#stockavailable').prop('disabled', true);
                        }
                        calculateShipmentQuantity();
                    });

                    $('#stockavailable').change(function () {
                        if ($(this).val() == '') {
                            $('#addlocation').val('');
                            $('#adddatesoh').val('');
                            $('#addqtysoh').val('0');
                            $('#addqtyavailable').val('0');
                        } else {
                            //get latest information about stockavailability
                            var stockitem = $(this).val().split('|');
                            $('#addlocation').val(stockitem[1]);
                            $('#adddatesoh').val(stockitem[2]);
                            $('#addqtysoh').val(stockitem[3]);
                            $('#addqtyavailable').val(stockitem[4]);
                        }
                        calculateShipmentQuantity();
                    });

                    $('#addshipment_quantity').blur(function () {
                        var proceed = true;
                        if ($('#addshipment_quantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penghantaran"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addshipment_quantity').val()) == 0) {
                            proceed = true;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penghantaran"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        if (proceed) {
                            calculateShipmentQuantity();
                        }
                    });
                });

                function calculateShipmentQuantity() {
                    var qtybalance = parseInt($('#addorder_quantity').val()) - parseInt($('#addshipment_quantity').val());
                    $('#qtybalance').val(qtybalance);
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#shipmentcat').removeAttr('required');
                        $('#bpid').removeAttr('required');
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update payment receipt header for case already have Line Item which logic, disable user from updating new bpid
                        $('#bpid').prop('disabled', false);
                        $('#shipmentcat').prop('disabled', false);

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
                                text: 'Please fill out "Line No" field!',
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
                        else if ($('#addqtyavailable').val().length == 0 && $('#additemcat').val() == "INVENTORY") {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error on stock availability!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addqtyavailable').val()) == 0 && $('#additemcat').val() == "INVENTORY") {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error on stock availability!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addshipment_quantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penghantaran"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addshipment_quantity').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti Penghantaran"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if (parseInt($('#addqtyavailable').val()) < parseInt($('#addshipment_quantity').val()) && $('#additemcat').val() == "INVENTORY") {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Error: Shipment quantity can not greate than stock availability!',
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
                        $('#shipmentcat').prop('disabled', true);
                    <%
                    }else{ 
                    %>
                        $('#bpid').prop('disabled', flag);
                        $('#shipmentcat').prop('disabled', flag);
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
                    $('#printshipment').prop('disabled', true);
                    $('#shipmentdetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printshipment').attr('disabled', 'disabled');
                    $('#shipmentdetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModHeader.GetSetstatus.Equals("CONFIRMED") && !oModHeader.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printshipment').prop('disabled', false);
                    $('#shipmentdetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printshipment').removeAttr('disabled');
                    $('#shipmentdetails').removeAttr('disabled');

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
                    $('#printshipment').prop('disabled', false);
                    $('#printshipment').removeAttr('disabled');
                    $('#shipmentdetails').prop('disabled', false);
                    $('#shipmentdetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

            </script>            

</asp:Content>

