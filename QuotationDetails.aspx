<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="QuotationDetails.aspx.cs" Inherits="QuotationDetails" %>

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
                                    <label for="orderno">No. Sebut Harga:</label>
                                    <input type="text" id="orderno" class="form-control" readonly="readonly" name="orderno" value="<%=oModOrder.GetSetorderno %>" />
                                    <label for="orderdate">Tarikh Sebut Harga:</label>
                                    <input type="text" id="orderdate" class="form-control" readonly="readonly" required="required" name="orderdate" value="<%=oModOrder.GetSetorderdate %>" />
                                    <label for="ordercat">Kategori:</label>
                                      <select class="form-control" id="ordercat" name="ordercat" required="required">
                                        <option value="">-select-</option>
                                        <option value="SALES_ORDER" <%=oModOrder.GetSetordercat.Equals("SALES_ORDER")?"selected":"" %>>SEBUT HARGA</option>
                                      </select>
                                    <label for="orderactivity">Aktiviti Sebut Harga:</label>
                                      <select class="form-control" id="orderactivity" name="orderactivity" required="required">
                                        <option value="">-select-</option>
                                        <option value="NA" <%=oModOrder.GetSetorderactivity.Equals("NA")?"selected":"" %>>TIDAK BERKAITAN</option>
                                      </select>
                                    <label for="ordertype">Jenis Sebut Harga:</label>
                                      <select class="form-control" id="ordertype" name="ordertype" required="required">
                                        <option value="">-select-</option>
                                        <option value="NORMAL" <%=oModOrder.GetSetordertype.Equals("NORMAL")?"selected":"" %>>NORMAL</option>
                                        <option value="PROMOTION" <%=oModOrder.GetSetordertype.Equals("PROMOTION")?"selected":"" %>>PROMOSI</option>
                                        <option value="AGENT" <%=oModOrder.GetSetordertype.Equals("AGENT")?"selected":"" %>>AGENT</option>
                                        <option value="STOCKIST" <%=oModOrder.GetSetordertype.Equals("STOCKIST")?"selected":"" %>>STOCKIST</option>
                                        <!--
                                        <option value="NEWYEAR" <%=oModOrder.GetSetordertype.Equals("NEWYEAR")?"selected":"" %>>NEW YEAR</option>
                                        <option value="YEAREND" <%=oModOrder.GetSetordertype.Equals("YEAREND")?"selected":"" %>>YEAR END</option>
                                        <option value="SUBSIDIARY" <%=oModOrder.GetSetordertype.Equals("SUBSIDIARY")?"selected":"" %>>SUBSIDIARY</option>
                                        <option value="WAIVED" <%=oModOrder.GetSetordertype.Equals("WAIVED")?"selected":"" %>>WAIVED</option>
                                        -->
                                      </select>
                                    <label for="paytype">Jenis Bayaran:</label>
                                      <select class="form-control" id="paytype" name="paytype" required="required">
                                        <option value="">-select-</option>
                                        <option value="CASH" <%=oModOrder.GetSetpaytype.Equals("CASH")?"selected":"" %>>TUNAI</option>
                                        <option value="CREDIT" <%=oModOrder.GetSetpaytype.Equals("CREDIT")?"selected":"" %>>KREDIT</option>
                                      </select>
                                    <label for="expirydate">Sah Sehingga:</label>
                                    <input type="text" id="expirydate" class="date-picker form-control" name="expirydate" required="required" value="<%=oModOrder.GetSetexpirydate %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Pelanggan:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option></option>
                                        <%
                                            if (lsBP.Count > 0)
                                            {
                                                for (int i = 0; i < lsBP.Count; i++)
                                                {
                                                    MainModel modBP = (MainModel)lsBP[i];
                                        %>       
                                                    <option value="<%=modBP.GetSetbpid %>" <%=oModOrder.GetSetbpid.Equals(modBP.GetSetbpid)?"selected":"" %>><%=modBP.GetSetbpdesc %></option>
                                        <% 
                                                }
                                            }
                                        %>
                                    </select>
                                    <input type="hidden" name="bpdesc" id="bpdesc" value="<%=oModOrder.GetSetbpdesc %>" />
                                    <label for="bpaddress">Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="4" readonly="readonly" required="required" name="bpaddress"><%=oModOrder.GetSetbpaddress%></textarea>
                                    <label for="bpcontact">Pegawai Dihubungi/ No. Tel.:</label>
                                    <input type="text" id="bpcontact" class="form-control" readonly="readonly" required="required" name="bpcontact" value="<%=oModOrder.GetSetbpcontact%>" />
                                    <label for="orderremarks">Perkara/ Catatan:</label>
                                    <input type="text" id="orderremarks" class="form-control" name="orderremarks" value="<%=oModOrder.GetSetorderremarks%>" />
                                    <label for="orderstatus">Status Sebut Harga:</label>
                                    <input type="text" id="orderstatus" class="form-control" readonly="readonly" name="orderstatus" value="<%=oModOrder.GetSetorderstatus%>"/>
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
                                            if (!oModOrder.GetSetorderstatus.Equals("CONFIRMED") && !oModOrder.GetSetorderstatus.Equals("CANCELLED")) { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsOrderLineItem.Count > 0) { 
                                    %>
                                    <button id="btnApprove" name="btnApprove" type="button" class="btn btn-success" onclick="actionclick('CONFIRM');" >Confirm</button>
                                    <%
                                                }
                                    %>
                                    <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-order">Batal</button>
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
                                        <input type="hidden" name="hidOrderNo" id="hidOrderNo" value="<%=sOrderNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Sebut Harga</h4>
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
                                            <option></option>
                                      <%
                                          for (int i = 0; i < lsItemDiscount.Count; i++)
                                          {
                                              MainModel oItemDisc = (MainModel)lsItemDiscount[i];
                                      %>                         
                                            <option value="<%=oItemDisc.GetSetitemno %>"><%=oItemDisc.GetSetitemno %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan Item </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <textarea id="additemdesc" name="additemdesc" class="form-control" rows="3" readonly="readonly"></textarea>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Unit </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addunitprice" name="addunitprice" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Diskaun </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="adddiscamount" name="adddiscamount" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addquantity" name="addquantity" type="text" class="form-control" value="1"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Sebut Harga 
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addorderprice" name="addorderprice" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod TAX <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addtaxcode" name="addtaxcode" class="form-control">
                                            <option value="">-select-</option>
                                      <%
                                          for (int i = 0; i < lsTax.Count; i++)
                                          {
                                              MainModel oTax = (MainModel)lsTax[i];
                                      %>                         
                                            <option value="<%=oTax.GetSettaxcode %>"><%=oTax.GetSettaxcode %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kadar TAX (%)</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtaxrate" name="addtaxrate" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah TAX </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtaxamount" name="addtaxamount" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Sebut Harga </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtotalprice" name="addtotalprice" type="text" class="form-control" readonly="readonly" value="0"/>
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
                    <!--BEGIN dialog box for confirm cancel order-->
                    <div class="modal fade modal-confirm-cancel-order" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <a id="printorder" name="printorder" class="btn btn-app" onclick="openprintpage();">
                      <i class="fa fa-print dark"></i>Cetak Sebut Harga
                    </a>
                    <a id="convertorder" name="convertorder" class="btn btn-app" data-toggle="modal" data-target=".modal-convert-order">
                      <i class="fa fa-edit dark"></i>Tukar Kepada Pesanan
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th></th>
                              <th>No</th>
                              <th>Kod Item</th>
                              <th>Keterangan Item</th>
                              <th>Harga Unit</th>
                              <th>Diskaun</th>
                              <th>Kuantiti</th>
                              <th>Harga Sebut Harga</th>
                              <th>Kod TAX</th>
                              <th>Jumlah TAX</th>
                              <th>Jumlah Sebut Harga</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsOrderLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsOrderLineItem.Count; i++) {
                                    MainModel modOrdDet = (MainModel)lsOrderLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modOrdDet.GetSetlineno%></td>
                              <td><%=modOrdDet.GetSetitemno%></td>
                              <td><%=modOrdDet.GetSetitemdesc%></td>
                              <td><%=modOrdDet.GetSetunitprice%></td>
                              <td><%=modOrdDet.GetSetdiscamount%></td>
                              <td><%=modOrdDet.GetSetquantity%></td>
                              <td><%=modOrdDet.GetSetorderprice%></td>
                              <td><%=modOrdDet.GetSettaxcode%></td>
                              <td><%=modOrdDet.GetSettaxamount%></td>
                              <td><%=modOrdDet.GetSettotalprice%></td>
                              <td>
                                  <%
                                    if (!oModOrder.GetSetorderstatus.Equals("CONFIRMED") && !oModOrder.GetSetorderstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-info btn-xs" onclick="openeditlineitem('<%=modOrdDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-add-line-item"><i class="fa fa-pencil"></i> Kemaskini </a>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modOrdDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
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
                                <td colspan="12">Tiada rekod...</td>
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
            <!--dialog box for convert order-->
            <div class="modal fade modal-convert-order" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                        </button>
                        <h4 class="modal-title">Tukar Kepada Pesanan Jualan</h4>
                    </div>
                    <div class="modal-body">
                        <div id="formdetails" class="form-horizontal form-label-left">
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Pelanggan</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="convertbpname" name="convertbpname" type="text" class="form-control" readonly="readonly" value="<%=oModOrder.GetSetbpdesc %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Alamat</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <textarea id="convertbpaddress" name="convertbpaddress" class="form-control" rows="3" readonly="readonly"><%=oModOrder.GetSetbpaddress %></textarea>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Sebut Harga</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalsalesamount" name="totalsalesamount" type="text" class="form-control" readonly="readonly" value="<%=oModOrder.GetSetsalesamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Diskaun</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaldiscountamount" name="totaldiscountamount" type="text" class="form-control" readonly="readonly" value="<%=oModOrder.GetSetdiscamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah TAX</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaltaxamount" name="totaltaxamount" type="text" class="form-control" readonly="readonly" value="<%=oModOrder.GetSettaxamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Keseluruhan Sebut Harga</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalorderamount" name="totalorderamount" type="text" class="form-control" readonly="readonly" value="<%=oModOrder.GetSettotalamount %>"/>
                            </div>
                            </div>

                            <div class="form-group">
                            <table class="table table-striped">
                              <thead>
                                <tr>
                                  <th>#</th>
                                  <th>Keterangan Item</th>
                                  <th>Kuantiti</th>
                                  <th>Jumlah Sebut Harga</th>
                                </tr>
                              </thead>
                              <tbody>
                        <%
                            if (lsOrderLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsOrderLineItem.Count; i++) {
                                    MainModel modOrdDet = (MainModel)lsOrderLineItem[i];
                        %>       
                            <tr>
                              <td><%=modOrdDet.GetSetlineno%></td>
                              <td><%=modOrdDet.GetSetitemno%><br /><%=modOrdDet.GetSetitemdesc%></td>
                              <td><%=modOrdDet.GetSetquantity%></td>
                              <td><%=modOrdDet.GetSettotalprice%></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td colspan="4">Tiada rekod...</td>
                            </tr>
                        <% 
                            }
                        %>
                              </tbody>
                            </table>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="actionclick('CONVERT');">Tukar</button>
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
                            $('#bpaddress').text('');
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
                                    $('#bpaddress').text("<%=oMainCon.RegExReplace(modBP.GetSetbpaddress,", ")%>");
                                    $('#bpcontact').val("<%=modBP.GetSetbpcontact%>");
                                    $('#bpdesc').val("<%=modBP.GetSetbpdesc%>");
                                }
                    <% 
                                      }
                                  }
                    %>
                    });

                    $('#additemno').change(function () {
                        if ($(this).val() == '') {
                            $('#additemdesc').text('');
                            $('#addunitprice').val('0');
                            $('#adddiscamount').val('0');
                            $('#addorderprice').val('0');
                            $('#addquantity').val('1');
                        }
                        <%
                            if (lsItemDiscount.Count > 0)
                            {
                                for (int i = 0; i < lsItemDiscount.Count; i++)
                                {
                                    MainModel modItemDisc = (MainModel)lsItemDiscount[i];
                        %>
                                    if ($(this).val() == "<%=modItemDisc.GetSetitemno%>") {
                                        $('#additemdesc').text("<%=modItemDisc.GetSetitemdesc%>");
                                        $('#addunitprice').val("<%=modItemDisc.GetSetsalesprice%>");
                                        $('#adddiscamount').val("<%=modItemDisc.GetSetdiscamount%>");
                                    }
                        <% 
                                }
                            }
                        %>
                        calculateTotalAmount('ADD_LINE_ITEM');
                    });

                    $('#addtaxcode').change(function () {
                        if ($(this).val() == '') {
                            $('#addtaxrate').val('0');
                            $('#addtaxcamount').val('0');
                        }
                        <%
                            if (lsTax.Count > 0)
                            {
                                for (int i = 0; i < lsTax.Count; i++)
                                {
                                    MainModel modTax = (MainModel)lsTax[i];
                        %>
                        if ($(this).val() == '<%=modTax.GetSettaxcode%>') {
                                    $('#addtaxrate').val('<%=modTax.GetSettaxrate%>');
                        }
                        <% 
                                }
                            }
                        %>
                        calculateTotalAmount('ADD_LINE_ITEM');
                    });

                    $('#addunitprice').change(function () {
                        calculateTotalAmount('ADD_LINE_ITEM');
                    });
                    $('#adddiscamount').change(function () {
                        calculateTotalAmount('ADD_LINE_ITEM');
                    });
                    $('#addquantity').change(function () {
                        calculateTotalAmount('ADD_LINE_ITEM');
                    });

                    $('#expirydate').daterangepicker({
                        singleDatePicker: true,
                        format: 'DD-MM-YYYY',
                        calender_style: "picker_1"
                    }, function (start, end, label) {
                        console.log(start.toISOString(), end.toISOString(), label);
                    });

                });

                function calculateTotalAmount(screen) {
                    if (screen == 'ADD_LINE_ITEM') {
                        var orderprice = ($('#addunitprice').val() - $('#adddiscamount').val()) * $('#addquantity').val();
                        $('#addorderprice').val(orderprice.toFixed(2));
                        var taxamnt = $('#addorderprice').val() * $('#addtaxrate').val() / 100;
                        $('#addtaxamount').val(taxamnt.toFixed(2));
                        var totamnt = parseFloat($('#addorderprice').val()) + parseFloat($('#addtaxamount').val());
                        $('#addtotalprice').val(totamnt.toFixed(2));
                    }
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#orderdate').removeAttr('required');
                        $('#ordercat').removeAttr('required');
                        $('#orderactivity').removeAttr('required');
                        $('#ordertype').removeAttr('required');
                        $('#paytype').removeAttr('required');
                        $('#expirydate').removeAttr('required');
                        $('#bpid').removeAttr('required');
                        $('#bpaddress').removeAttr('required');
                        $('#bpcontact').removeAttr('required');
                        $('#orderstatus').removeAttr('required');
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
                        else if ($('#additemno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please select an item in "Item No" list!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addunitprice').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please  fill out "Unit Price" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#adddiscamount').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please  fill out "Discount Amount" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addquantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please  fill out "Order Qty" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addtaxcode').val().length == 0) {
                                proceed = false;
                                new PNotify({
                                    title: 'Alert',
                                    text: 'Please select an item in "Tax Code" list!',
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
                    $('#ordercat').prop('disabled', flag);
                    $('#orderactivity').prop('disabled', flag);
                    $('#ordertype').prop('disabled', flag);
                    $('#paytype').prop('disabled', flag);
                    $('#expirydate').prop('disabled', flag);
                    $('#bpid').prop('disabled', flag);
                    $('#orderremarks').prop('disabled', flag);
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
                    $('#addlineno').val('<%=lsOrderLineItem.Count + 1%>');
                }

                function openeditlineitem(lineno) {
                    $('#btnAddLineItem').hide();
                    $('#btnEditLineItem').show();

                    $('#hidLineNo').val(lineno);
                    <%
                    for (int i = 0; i < lsOrderLineItem.Count; i++)
                    {
                        MainModel modOrdDet = (MainModel)lsOrderLineItem[i];
                    %>
                    if ($('#hidLineNo').val() == '<%=modOrdDet.GetSetlineno%>') {
                        $('#addlineno').val('<%=modOrdDet.GetSetlineno%>');
                        $('#additemno').val('<%=modOrdDet.GetSetitemno%>').change();
                        $('#additemdesc').text('<%=modOrdDet.GetSetitemdesc%>');
                        $('#addunitprice').val('<%=modOrdDet.GetSetunitprice%>');
                        $('#adddiscamount').val('<%=modOrdDet.GetSetdiscamount%>');
                        $('#addquantity').val('<%=modOrdDet.GetSetquantity%>');
                        $('#addorderprice').val('<%=modOrdDet.GetSetorderprice%>');
                        $('#addtaxcode').val('<%=modOrdDet.GetSettaxcode%>').change();
                        $('#addtaxrate').val('<%=modOrdDet.GetSettaxrate%>');
                        $('#addtaxamount').val('<%=modOrdDet.GetSettaxamount%>');
                        $('#addtotalprice').val('<%=modOrdDet.GetSettotalprice%>');
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
                    $('#convertorder').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printorder').attr('disabled', 'disabled');
                    $('#convertorder').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModOrder.GetSetorderstatus.Equals("CONFIRMED") && !oModOrder.GetSetorderstatus.Equals("CANCELLED"))
                        {
                            if (lsOrderLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printorder').prop('disabled', false);
                    //$('#convertorder').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printorder').removeAttr('disabled');
                    //$('#convertorder').removeAttr('disabled');

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
                            if (lsOrderLineItem.Count > 0)
                            {
                %>
                    $('#printorder').prop('disabled', false);
                    $('#printorder').removeAttr('disabled');
                    $('#convertorder').prop('disabled', false);
                    $('#convertorder').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

                function openprintpage() {
                    var popupWindow = window.open("QuotationPage.aspx?orderno=<%=sOrderNo%>", "open_printquotation", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

