<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="InvoiceDetails.aspx.cs" Inherits="InvoiceDetails" %>

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
                                    <label for="invoiceno">No. Invois:</label>
                                    <input type="text" id="invoiceno" class="form-control" readonly="readonly" name="invoiceno" value="<%=oModInvoice.GetSetinvoiceno %>" />
                                    <label for="invoicedate">Tarikh Invois:</label>
                                    <input type="text" id="invoicedate" class="form-control" readonly="readonly" required="required" name="invoicedate" value="<%=oModInvoice.GetSetinvoicedate %>" />
                                    <label for="invoicecat">Kategori:</label>
                                      <select class="form-control" id="invoicecat" name="invoicecat" required="required">
                                        <option value="">-select-</option>
                                        <option value="SALES_INVOICE" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE")?"selected":"" %>>INVOIS JUALAN / AGIHAN</option>
                                        <option value="TRANSFER_INVOICE" <%=oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"selected":"" %>>INVOIS PINDAHAN</option>
                                        <option value="RECEIPT_VOUCHER" <%=oModInvoice.GetSetinvoicecat.Equals("RECEIPT_VOUCHER")?"selected":"" %>>VOUCHER TERIMAAN</option>
                                        <%
                                            if (oModInvoice.GetSetstatus.Equals("CONFIRMED") || oModInvoice.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                        %>
                                        <option value="JOURNAL_VOUCHER" <%=oModInvoice.GetSetinvoicecat.Equals("JOURNAL_VOUCHER")?"selected":"" %>>VOUCHER PERLARASAN</option>
                                        <%
                                            }
                                        %>
                                      </select>
                                    <label for="invoicetype">Jenis Invois:</label>
                                      <select class="form-control" id="invoicetype" name="invoicetype" required="required">
                                        <option value="">-select-</option>
                                        <option value="NOT_APPLICABLE" <%=oModInvoice.GetSetinvoicetype.Equals("NOT_APPLICABLE")?"selected":"" %>>TIDAK BERKENAAN</option>
                                        <%
                                            for(int x=0; x < lsInvoiceType.Count; x++)
                                            {
                                                MainModel modParam = (MainModel)lsInvoiceType[x];
                                        %>
                                                <option value="<%=modParam.GetSetparamttype %>" <%=oModInvoice.GetSetinvoicetype.Equals(modParam.GetSetparamttype)?"selected":"" %>><%=modParam.GetSetparamtdesc %></option>
                                        <%
                                            }
                                        %>                                                                                          
                                        <%
                                            if (oModInvoice.GetSetstatus.Equals("CONFIRMED") || oModInvoice.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                        %>
                                        <option value="BANK_DEPOSIT" <%=oModInvoice.GetSetinvoicetype.Equals("BANK_DEPOSIT")?"selected":"" %>>DEPOSIT BANK</option>
                                        <option value="CASH_WITHDRAWAL" <%=oModInvoice.GetSetinvoicetype.Equals("CASH_WITHDRAWAL")?"selected":"" %>>PENGELUARAN TUNAI</option>
                                        <%
                                            }
                                        %>

                                     </select>
                                    <label for="invoiceterm">Terma Invois:</label>
                                      <select class="form-control" id="invoiceterm" name="invoiceterm" required="required">
                                        <option value="">-select-</option>
                                        <option value="N/A" <%=oModInvoice.GetSetinvoiceterm.Equals("N/A")?"selected":"" %>>TIDAK BERKENAAN</option>
                                        <option value="COD" <%=oModInvoice.GetSetinvoiceterm.Equals("COD")?"selected":"" %>>TUNAI PENGHANTARAN</option>
                                        <option value="7DAYS" <%=oModInvoice.GetSetinvoiceterm.Equals("7DAYS")?"selected":"" %>>7 HARI</option>
                                        <option value="15DAYS" <%=oModInvoice.GetSetinvoiceterm.Equals("15DAYS")?"selected":"" %>>15 HARI</option>
                                        <option value="30DAYS" <%=oModInvoice.GetSetinvoiceterm.Equals("30DAYS")?"selected":"" %>>30 HARI</option>
                                      </select>
                                    <label for="remarks">Rujukan/ Catatan:</label>
                                    <input type="text" id="remarks" class="form-control" name="remarks" value="<%=oModInvoice.GetSetremarks%>" />
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Invois Kepada:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option></option>
                                        <%
                                            if (lsBP.Count > 0)
                                            {
                                                for (int i = 0; i < lsBP.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modBP = (MainModel)lsBP[i];
                                                    if (oModInvoice.GetSetbpid.Equals(modBP.GetSetbpid))
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
                                    <input type="<%=selected_bp.Equals("OTHER")?"text":"hidden"%>" name="bpdesc" id="bpdesc" required="required" class="form-control" value="<%=oModInvoice.GetSetbpdesc %>" />
                                    <div id="bpdesc-container" style="position: relative; float: left; width: 100%; margin: 10px;"></div>
                                    <label for="bpaddress">Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="4" required="required" name="bpaddress"><%=oModInvoice.GetSetbpaddress%></textarea>
                                    <label for="bpcontact">Pegawai Dihubungi/ No. Tel.:</label>
                                    <input type="text" id="bpcontact" class="form-control" required="required" name="bpcontact" value="<%=oModInvoice.GetSetbpcontact%>" />
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" name="status" readonly="readonly" value="<%=oModInvoice.GetSetstatus%>"/>
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
                                            if (!oModInvoice.GetSetstatus.Equals("CONFIRMED") && !oModInvoice.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsInvoiceLineItem.Count > 0) { 
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
                                        <input type="hidden" name="hidInvoiceNo" id="hidInvoiceNo" value="<%=sInvoiceNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Invois</h4>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Invois <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addkeyno" name="addkeyno" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option></option>
                                      <%
                                          for (int i = 0; i < lsPendInvMod.Count; i++)
                                          {
                                              MainModel oPendInvLine = (MainModel)lsPendInvMod[i];
                                              if (oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE"))
                                              {
                                      %>                         
                                              <option value="<%=oPendInvLine.GetSetshipmentno %>-<%=oPendInvLine.GetSetlineno %>"><%=oPendInvLine.GetSetshipmentno %>/<%=oPendInvLine.GetSetlineno %>/<%=oPendInvLine.GetSetorderno %>/<%=oPendInvLine.GetSetitemno %></option>
                                      <%
                                              }
                                              else
                                              { 
                                      %>          
                                              <option value="<%=oPendInvLine.GetSetparamid %>"><%=oPendInvLine.GetSetparamtype %>/<%=oPendInvLine.GetSetparamdesc %></option>               
                                      <%
                                              }
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <input type="hidden" name="hidItemNo" id="hidItemNo" value="" />
                                      <input type="hidden" name="hidShipmentNo" id="hidShipmentNo" value="" />
                                      <input type="hidden" name="hidShipmentLineNo" id="hidShipmentLineNo" value="" />
                                      <input type="hidden" name="hidOrderNo" id="hidOrderNo" value="" />
                                      <input type="hidden" name="hidOrderLineNo" id="hidOrderLineNo" value="" />
                                      <input type="hidden" name="hidUnitCost" id="hidUnitCost" value="" />
                                      <input type="hidden" name="hidCostPrice" id="hidCostPrice" value="" />
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan Item </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <textarea id="additemdesc" name="additemdesc" class="form-control" rows="3" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"readonly":""%>></textarea>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Unit </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addunitprice" name="addunitprice" type="text" class="form-control" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"readonly":""%> value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Diskaun </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="adddiscamount" name="adddiscamount" type="text" class="form-control" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"readonly":""%> value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addquantity" name="addquantity" type="text" class="form-control" value="0" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"readonly":""%>/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Invois
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addinvoiceprice" name="addinvoiceprice" type="text" class="form-control" readonly value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod TAX <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addtaxcode" name="addtaxcode" class="form-control" <%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?"readonly":""%>>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Invois </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtotalprice" name="addtotalprice" type="text" class="form-control" readonly="readonly" value="0"/>
                                        </div>
                                      </div>

                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" id="btnAddLineItem" onclick="actionclick('INSERT');">Tambah</button>
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
                    <a id="printinvoice" name="printinvoice" class="btn btn-app" onclick="openprintpage();">
                      <i class="fa fa-print dark"></i>Cetak Invois
                    </a>
                    <a id="invoicedetails" name="invoicedetails" class="btn btn-app" data-toggle="modal" data-target=".modal-invoice-details">
                      <i class="fa fa-edit dark"></i>Maklumat Tambahan
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th></th>
                              <th>No</th>
                              <th>No. Penghantaran</th>
                              <th>No</th>
                              <th>No. Pesanan</th>
                              <th>Kod Item</th>
                              <th>Keterangan Item</th>
                              <th>Harga Unit</th>
                              <th>Jumlah Diskaun</th>
                              <th>Kuantiti</th>
                              <th>Harga Invois</th>
                              <th>Kod TAX</th>
                              <th>Jumlah TAX</th>
                              <th>Jumlah Invois</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsInvoiceLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsInvoiceLineItem.Count; i++)
                                {
                                    MainModel modInvDet = (MainModel)lsInvoiceLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modInvDet.GetSetlineno%></td>
                              <td><%=modInvDet.GetSetshipmentno%></td>
                              <td><%=modInvDet.GetSetshipment_lineno%></td>
                              <td><%=modInvDet.GetSetorderno%></td>
                              <td><%=modInvDet.GetSetitemno%></td>
                              <td><%=modInvDet.GetSetitemdesc%></td>
                              <td><%=modInvDet.GetSetunitprice%></td>
                              <td><%=modInvDet.GetSetdiscamount%></td>
                              <td><%=modInvDet.GetSetquantity%></td>
                              <td><%=modInvDet.GetSetinvoiceprice%></td>
                              <td><%=modInvDet.GetSettaxcode%></td>
                              <td><%=modInvDet.GetSettaxamount%></td>
                              <td><%=modInvDet.GetSettotalinvoice%></td>
                              <td>
                                  <%
                                    if (!oModInvoice.GetSetstatus.Equals("CONFIRMED") && !oModInvoice.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modInvDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
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
                                <td colspan="15">Rekod tiada...</td>
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
            <!--dialog box for order details-->
            <div class="modal fade modal-invoice-details" tabindex="-1" role="dialog" aria-hidden="true">
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
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Invois</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalsalesamount" name="totalsalesamount" type="text" class="form-control" readonly="readonly" value="<%=oModInvoice.GetSetsalesamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Diskaun</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaldiscountamount" name="totaldiscountamount" type="text" class="form-control" readonly="readonly" value="<%=oModInvoice.GetSetdiscamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah TAX</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaltaxamount" name="totaltaxamount" type="text" class="form-control" readonly="readonly" value="<%=oModInvoice.GetSettaxamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Keseluruhan Inovis</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalinvoiceamount" name="totalinvoiceamount" type="text" class="form-control" readonly="readonly" value="<%=oModInvoice.GetSettotalamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Bayaran Telah Terima</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalpaymentreceipt" name="totalpaymentreceipt" type="text" class="form-control" readonly="readonly" value="<%=oModInvoice.GetSetpayrcptamount %>"/>
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
                                  <td><%=oModInvoice.GetSetcreatedby %></td>
                                  <td><%=oModInvoice.GetSetcreateddate %></td>
                                </tr>
                                <tr>
                                  <td>Confirmed</td>
                                  <td><%=oModInvoice.GetSetconfirmedby %></td>
                                  <td><%=oModInvoice.GetSetconfirmeddate %></td>
                                </tr>
                                <tr>
                                  <td>Cancelled</td>
                                  <td><%=oModInvoice.GetSetcancelledby %></td>
                                  <td><%=oModInvoice.GetSetcancelleddate %></td>
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
                            //$('#bpaddress').text('');
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
                                    //$('#bpaddress').text("");
                                    document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modBP.GetSetbpaddress, ", ")%>";
                                    $('#bpcontact').val("<%=modBP.GetSetbpcontact%>");
                                    $('#bpdesc').val("<%=modBP.GetSetbpdesc%>");
                                }
                    <% 
                                      }
                                  }
                    %>
                        if ($('#bpdesc').val() == 'OTHER') {
                            //$('textarea#bpaddress').text('').val();
                            document.getElementById("bpaddress").value = "";
                            $('#bpaddress').prop('disabled', false);
                            $('#bpcontact').val('');
                            $('#bpcontact').prop('disabled', false);
                            $('#bpdesc').attr('type', 'text');
                            $('#bpdesc').val('');
                        } else {
                            $('#bpdesc').attr('type', 'hidden');
                            $('#bpaddress').prop('disabled', true);
                            $('#bpcontact').prop('disabled', true);
                        }

                    });

                    $('#addkeyno').change(function () {
                        if ($(this).val() == '') {
                            $('#hidShipmentNo').val('');
                            $('#hidShipmentLineNo').val('0');
                            $('#hidOrderNo').val('');
                            $('#hidOrderLineNo').val('0');
                            $('#hidUnitCost').val('0');
                            $('#hidCostPrice').val('0');
                            $('#additemdesc').text('');
                            $('#addunitprice').val('0');
                            $('#adddiscamount').val('0');
                            $('#addquantity').val('0');
                            $('#addinvoiceprice').val('0');
                            $('#addtaxcode').val('');
                            $('#addtaxrate').val('0');
                            $('#addtaxamount').val('0');
                            $('#addtotalprice').val('0');
                        }
                        <%
                            if (lsPendInvMod.Count > 0)
                            {
                                for (int i = 0; i < lsPendInvMod.Count; i++)
                                {
                                    MainModel oPendInvLine = (MainModel)lsPendInvMod[i];
                        %>
                                    if ($(this).val() == '<%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?oPendInvLine.GetSetshipmentno+"-"+oPendInvLine.GetSetlineno:oPendInvLine.GetSetparamid%>') {
                                        $('#hidItemNo').val('<%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?oPendInvLine.GetSetitemno:oPendInvLine.GetSetparamid%>');
                                        $('#hidShipmentNo').val('<%=oPendInvLine.GetSetshipmentno%>');
                                        $('#hidShipmentLineNo').val('<%=oPendInvLine.GetSetlineno%>');
                                        $('#hidOrderNo').val('<%=oPendInvLine.GetSetorderno%>');
                                        $('#hidOrderLineNo').val('<%=oPendInvLine.GetSetorder_lineno%>');
                                        $('#hidUnitCost').val('<%=oPendInvLine.GetSetunitcost%>');
                                        $('#hidCostPrice').val('<%=oPendInvLine.GetSetcostprice%>');
                                        $('#additemdesc').text("<%=oModInvoice.GetSetinvoicecat.Equals("SALES_INVOICE") || oModInvoice.GetSetinvoicecat.Equals("TRANSFER_INVOICE")?oPendInvLine.GetSetitemdesc:""%>");
                                        $('#addunitprice').val('<%=oPendInvLine.GetSetunitprice%>');
                                        $('#adddiscamount').val('<%=oPendInvLine.GetSetdiscamount%>');
                                        $('#addquantity').val('<%=oPendInvLine.GetSetquantity%>');
                                        $('#addinvoiceprice').val('<%=oPendInvLine.GetSetinvoiceprice%>');
                                        $('#addtaxcode').val('<%=oPendInvLine.GetSettaxcode%>');
                                        $('#addtaxrate').val('<%=oPendInvLine.GetSettaxrate%>');
                                        $('#addtaxamount').val('<%=oPendInvLine.GetSettaxamount%>');
                                        $('#addtotalprice').val('<%=oPendInvLine.GetSettotalinvoice%>');
                                    }
                        <% 
                                }
                            }
                        %>
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

                    /*
                    $('#plandate').daterangepicker({
                        singleDatePicker: true,
                        format: 'DD-MM-YYYY',
                        calender_style: "picker_1"
                    }, function (start, end, label) {
                        console.log(start.toISOString(), end.toISOString(), label);
                    });
                    */

                });

                function calculateTotalAmount(screen) {
                    if (screen == 'ADD_LINE_ITEM') {
                        var invoiceprice = ($('#addunitprice').val() - $('#adddiscamount').val()) * $('#addquantity').val();
                        $('#addinvoiceprice').val(invoiceprice.toFixed(2));
                        var taxamnt = $('#addinvoiceprice').val() * $('#addtaxrate').val() / 100;
                        $('#addtaxamount').val(taxamnt.toFixed(2));
                        var totamnt = parseFloat($('#addinvoiceprice').val()) + parseFloat($('#addtaxamount').val());
                        $('#addtotalprice').val(totamnt.toFixed(2));
                    }
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#invoicedate').removeAttr('required');
                        $('#invoicecat').removeAttr('required');
                        $('#invoicetype').removeAttr('required');
                        $('#invoiceterm').removeAttr('required');
                        $('#bpid').removeAttr('required');
                        $('#bpdesc').removeAttr('required');
                        $('#bpaddress').removeAttr('required');
                        $('#bpcontact').removeAttr('required');
                        $('#status').removeAttr('required');
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update invoice header for case already have Line Item which logic, disable user from updating new bpid
                        $('#bpid').prop('disabled', false);
                        $('#bpdesc').prop('disabled', false);
                        $('#bpaddress').prop('disabled', false);
                        $('#bpcontact').prop('disabled', false);
                        $('#invoicecat').prop('disabled', false);
                        $('#invoicetype').prop('disabled', false);
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
                        else if ($('#addkeyno').val().length == 0)
                        {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Item Invois"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#hidItemNo').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'System Error, please contact system admin!',
                                type: 'error',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#additemdesc').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Keterangan Item"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#adddiscamount').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Jumlah Diskaun"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addunitprice').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Harga Unit"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addunitprice').val().length > 0 && parseFloat($('#addunitprice').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Harga Unit"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addquantity').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addquantity').val().length > 0 && parseInt($('#addquantity').val()) == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila isi "Kuantiti"!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        else if ($('#addtaxcode').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Sila pilih "Kod TAX"!',
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
                        if (lsInvoiceLineItem.Count > 0) { 
                    %>
                            $('#bpid').prop('disabled', true);
                            $('#bpdesc').prop('disabled', true);
                            $('#bpaddress').prop('disabled', true);
                            $('#bpcontact').prop('disabled', true);
                            $('#invoicecat').prop('disabled', true);
                            $('#invoicetype').prop('disabled', true);
                    <%
                        }else{ 
                    %>
                            $('#bpid').prop('disabled', flag);
                            $('#bpdesc').prop('disabled', flag);
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
                            %>
                            $('#invoicecat').prop('disabled', flag);
                            $('#invoicetype').prop('disabled', flag);
                            <%
                        }
                    %>
                    $('#invoiceterm').prop('disabled', flag);
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
                    $('#addlineno').val('<%=lsInvoiceLineItem.Count + 1%>');
                }

                function confirmdeletelineitem(lineno) {
                    $('#hidLineNo').val(lineno);
                }

                //enable & disable button
                $(document).ready(function () {

                    $('#addlineitem').prop('disabled', true);
                    $('#printinvoice').prop('disabled', true);
                    $('#invoicedetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printinvoice').attr('disabled', 'disabled');
                    $('#invoicedetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModInvoice.GetSetstatus.Equals("CONFIRMED") && !oModInvoice.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsInvoiceLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printinvoice').prop('disabled', false);
                    $('#invoicedetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printinvoice').removeAttr('disabled');
                    $('#invoicedetails').removeAttr('disabled');

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
                            if (lsInvoiceLineItem.Count > 0)
                            {
                %>
                    $('#printinvoice').prop('disabled', false);
                    $('#printinvoice').removeAttr('disabled');
                    $('#invoicedetails').prop('disabled', false);
                    $('#invoicedetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

                function openprintpage() {
                    var popupWindow = window.open("InvoicePage.aspx?invoiceno=<%=sInvoiceNo%>", "open_printinvoice", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
                                                document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modOthBP.GetSetobpaddress, ", ")%>";
                                                $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                            }
                                    <%
                                        }
                                        else if (i < lsOtherBP.Count)
                                        { 
                                    %>
                                            else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                            {
                                                document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modOthBP.GetSetobpaddress, ", ")%>";
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
                                    document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modOthBP.GetSetobpaddress, ", ")%>";
                                    $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                }
                        <%
                            }
                            else if (i < lsOtherBP.Count)
                            { 
                        %>
                                else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                {
                                    document.getElementById("bpaddress").value = "<%=oMainCon.RegExReplace(modOthBP.GetSetobpaddress, ", ")%>";
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

                    });

                <%
                }
                %>
                });
            </script>
            <!-- /jQuery autocomplete -->

</asp:Content>

