<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="ExpensesDetails.aspx.cs" Inherits="ExpensesDetails" %>

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
                                    <label for="expensesno">No. Bil & Belanja:</label>
                                    <input type="text" id="expensesno" class="form-control" readonly="readonly" name="expensesno" value="<%=oModExpenses.GetSetexpensesno %>" />
                                    <label for="expensesdate">Tarikh Bil & Belanja:</label>
                                    <input type="text" id="expensesdate" class="form-control" readonly="readonly" required="required" name="expensesdate" value="<%=oModExpenses.GetSetexpensesdate %>" />
                                    <label for="expensescat">Kategori:</label>
                                      <select class="form-control" id="expensescat" name="expensescat" required="required">
                                        <option value="">-select-</option>
                                        <option value="PURCHASE_INVOICE" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE")?"selected":"" %>>INVOIS BELIAN</option>
                                        <option value="TRANSFER_INVOICE" <%=oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"selected":"" %>>INVOIS PINDAHAN</option>
                                        <option value="PAYMENT_VOUCHER" <%=oModExpenses.GetSetexpensescat.Equals("PAYMENT_VOUCHER")?"selected":"" %>>VOUCHER BAYARAN</option>
                                        <option value="JOURNAL_VOUCHER" <%=oModExpenses.GetSetexpensescat.Equals("JOURNAL_VOUCHER")?"selected":"" %>>VOUCHER PERLARASAN</option>
                                        <!--
                                        <option value="FINANCIAL_RELINQUISHMENT" <%=oModExpenses.GetSetexpensescat.Equals("FINANCIAL_RELINQUISHMENT")?"selected":"" %>>PERBELANJAAN KEWANGAN</option>
                                        <option value="BANK_DEPOSIT" <%=oModExpenses.GetSetexpensescat.Equals("BANK_DEPOSIT")?"selected":"" %>>DEPOSIT BANK</option>
                                        <option value="CASH_WITHDRAWAL" <%=oModExpenses.GetSetexpensescat.Equals("CASH_WITHDRAWAL")?"selected":"" %>>PENGELUARAN TUNAI</option>
                                        <option value="OTHER_PAYMENT" <%=oModExpenses.GetSetexpensescat.Equals("OTHER_PAYMENT")?"selected":"" %>>LAIN-LAIN BAYARAN</option>
                                        -->
                                      </select>
                                    <label for="expensestype">Jenis Bil & Belanja:</label>
                                      <select class="form-control" id="expensestype" name="expensestype" required="required">
                                        <option value="">-select-</option>
                                        <option value="NOT_APPLICABLE" <%=oModExpenses.GetSetexpensestype.Equals("NOT_APPLICABLE")?"selected":"" %>>TIDAK BERKENAAN</option>
                                        <option value="FINANCIAL_RELINQUISHMENTS" <%=oModExpenses.GetSetexpensestype.Equals("FINANCIAL_RELINQUISHMENTS")?"selected":"" %>>PENGEMBALIAN KEWANGAN</option>
                                        <option value="BANK_DEPOSIT" <%=oModExpenses.GetSetexpensestype.Equals("BANK_DEPOSIT")?"selected":"" %>>BANK DEPOSIT</option>
                                        <option value="CASH_WITHDRAWAL" <%=oModExpenses.GetSetexpensestype.Equals("CASH_WITHDRAWAL")?"selected":"" %>>PENGELUARAN TUNAI</option>
                                        <option value="SUPPLY_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("SUPPLY_EXPENSES")?"selected":"" %>>BEKALAN & BAHAN MENTAH</option>
                                        <option value="SALARIES_WAGES" <%=oModExpenses.GetSetexpensestype.Equals("SALARIES_WAGES")?"selected":"" %>>GAJI & UPAH</option>
                                        <option value="TRAVEL_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("TRAVEL_EXPENSES")?"selected":"" %>>PERBELANJAAN PERJALANAN</option>
                                        <option value="ENTERTAINMENT_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("ENTERTAINMENT_EXPENSES")?"selected":"" %>>HIBURAN & KERAIAN</option>
                                        <option value="EMPLOYEE_BENEFIT" <%=oModExpenses.GetSetexpensestype.Equals("EMPLOYEE_BENEFIT")?"selected":"" %>>MANFAAT & KEMUDAHAN PEKERJA</option>
                                        <option value="MARKETING_ADVERTISING" <%=oModExpenses.GetSetexpensestype.Equals("MARKETING_ADVERTISING")?"selected":"" %>>PEMASARAN & PROMOSI</option>
                                        <option value="RENTAL_LEASING" <%=oModExpenses.GetSetexpensestype.Equals("RENTAL_LEASING")?"selected":"" %>>SEWAAN & PAJAKAN</option>
                                        <option value="REPAIR_MAINTENANCE" <%=oModExpenses.GetSetexpensestype.Equals("REPAIR_MAINTENANCE")?"selected":"" %>>PEMBAIKAN & PENYELENGGARAAN</option>
                                        <option value="DEPRECIATION_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("DEPRECIATION_EXPENSES")?"selected":"" %>>SUSUT NILAI</option>
                                        <option value="BAD_DEBT_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("BAD_DEBT_EXPENSES")?"selected":"" %>>HUTANG LAPOK</option>
                                        <option value="SUBSCRIPTION_REGISTRATION" <%=oModExpenses.GetSetexpensestype.Equals("SUBSCRIPTION_REGISTRATION")?"selected":"" %>>PENDAFTARAN & MELANGGAN</option>
                                        <option value="INSURANCE_SECURITY" <%=oModExpenses.GetSetexpensestype.Equals("INSURANCE_SECURITY")?"selected":"" %>>INSURANS & KESELAMATAN</option>
                                        <option value="PROFESSIONAL_STATUTORY" <%=oModExpenses.GetSetexpensestype.Equals("PROFESSIONAL_STATUTORY")?"selected":"" %>>PROFESIONAL & STATUTORI</option>
                                        <option value="BILL_UTILITIES" <%=oModExpenses.GetSetexpensestype.Equals("BILL_UTILITIES")?"selected":"" %>>BIL UTILITI</option>
                                        <option value="TAXATION" <%=oModExpenses.GetSetexpensestype.Equals("TAXATION")?"selected":"" %>>PERCUKAIAN (TAX)</option>
                                        <option value="SELLING_SERVICES" <%=oModExpenses.GetSetexpensestype.Equals("SELLING_SERVICES")?"selected":"" %>>KOS PENJUALAN & PERKHIDMATAN</option>
                                        <option value="OTHER_EXPENSES" <%=oModExpenses.GetSetexpensestype.Equals("OTHER_EXPENSES")?"selected":"" %>>PERBELANJAAN LAIN-LAIN</option>
                                      </select>
                                    <label for="remarks">Rujukan/ Catatan:</label>
                                    <input type="text" id="remarks" class="form-control" name="remarks" value="<%=oModExpenses.GetSetremarks%>" />
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpid">Bayar Kepada:</label>
                                    <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" required="required" style="width:100%;">
                                        <option></option>
                                        <%
                                            if (lsPayTo.Count > 0)
                                            {
                                                for (int i = 0; i < lsPayTo.Count; i++)
                                                {
                                                    String selected = "";
                                                    MainModel modBP = (MainModel)lsPayTo[i];
                                                    if (oModExpenses.GetSetbpid.Equals(modBP.GetSetbpid))
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
                                    <input type="<%=selected_bp.Equals("OTHER")?"text":"hidden"%>" name="bpdesc" id="bpdesc" required="required" class="form-control" value="<%=oModExpenses.GetSetbpdesc %>" />
                                    <div id="bpdesc-container" style="position: relative; float: left; width: 100%; margin: 10px;"></div>
                                    <label for="bpaddress"> Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="4" required="required" name="bpaddress"><%=oModExpenses.GetSetbpaddress%></textarea>
                                    <label for="bpcontact">Pegawai Dihubungi/ No. Tel.:</label>
                                    <input type="text" id="bpcontact" class="form-control" required="required" name="bpcontact" value="<%=oModExpenses.GetSetbpcontact%>" />
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModExpenses.GetSetstatus%>"/>
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
                                            if (!oModExpenses.GetSetstatus.Equals("CONFIRMED") && !oModExpenses.GetSetstatus.Equals("CANCELLED"))
                                            { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                    <%
                                                if (lsExpensesLineItem.Count > 0) { 
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
                                        <input type="hidden" name="hidExpensesNo" id="hidExpensesNo" value="<%=sExpensesNo %>" />
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
                                        <h4 class="modal-title">Tambah Item Bil & Belanja</h4>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Item Bil & Belanja <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addkeyno" name="addkeyno" class="select2_single form-control" tabindex="-1" style="width:100%;">
                                            <option></option>
                                      <%
                                          for (int i = 0; i < lsPendExpMod.Count; i++)
                                          {
                                              MainModel oPendExpLine = (MainModel)lsPendExpMod[i];
                                              if (oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE"))
                                              { 
                                      %>                         
                                            <option value="<%=oPendExpLine.GetSetreceiptno %>-<%=oPendExpLine.GetSetlineno %>"><%=oPendExpLine.GetSetreceiptno %>/<%=oPendExpLine.GetSetlineno %>/<%=oPendExpLine.GetSetorderno %>/<%=oPendExpLine.GetSetitemno %></option>
                                      <%
                                              }
                                              else
                                              {
                                      %>                         
                                            <option value="<%=oPendExpLine.GetSetparamid %>"><%=oPendExpLine.GetSetparamtype %>/<%=oPendExpLine.GetSetparamdesc %></option>
                                      <%
                                                  
                                              }
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <input type="hidden" name="hidItemNo" id="hidItemNo" value="" />
                                      <input type="hidden" name="hidReceiptNo" id="hidReceiptNo" value="" />
                                      <input type="hidden" name="hidReceiptLineNo" id="hidReceiptLineNo" value="" />
                                      <input type="hidden" name="hidOrderNo" id="hidOrderNo" value="" />
                                      <input type="hidden" name="hidOrderLineNo" id="hidOrderLineNo" value="" />
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan Item </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <textarea id="additemdesc" name="additemdesc" class="form-control" rows="3" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"readonly":""%>></textarea>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Unit </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addunitprice" name="addunitprice" type="text" class="form-control" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"readonly":""%> value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Diskaun
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="adddiscamount" name="adddiscamount" type="text" class="form-control" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"readonly":""%> value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addquantity" name="addquantity" type="text" class="form-control" value="0" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"readonly":""%>/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Bil & Belanja
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addexpensesprice" name="addexpensesprice" type="text" class="form-control" readonly value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod TAX <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addtaxcode" name="addtaxcode" class="form-control" <%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE") || oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?"readonly":""%>>
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
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Bil & Amount </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtotalexpenses" name="addtotalexpenses" type="text" class="form-control" readonly="readonly" value="0"/>
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
                    <a id="printexpenses" name="printexpenses" class="btn btn-app" onclick="openprintpage();">
                      <i class="fa fa-print dark"></i>Cetak Bil
                    </a>
                    <a id="expensesdetails" name="expensesdetails" class="btn btn-app" data-toggle="modal" data-target=".modal-expenses-details">
                      <i class="fa fa-edit dark"></i>Maklumat Tambahan
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th></th>
                              <th>No</th>
                              <th>No. Penerimaan</th>
                              <th>No. Pesanan</th>
                              <th>Kod Item</th>
                              <th>Keterangan Item</th>
                              <th>Harga Unit</th>
                              <th>Diskaun</th>
                              <th>Kuantiti</th>
                              <th>Harga Bil & Belanja</th>
                              <th>Kod TAX</th>
                              <th>Jumlah TAX</th>
                              <th>Harga Bil & Belanja</th>
                              <th></th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsExpensesLineItem.Count > 0)
                            {
                                for (int i = 0; i < lsExpensesLineItem.Count; i++)
                                {
                                    MainModel modExpDet = (MainModel)lsExpensesLineItem[i];
                        %>       
                            <tr>
                              <td><i class="glyphicon glyphicon-play"></i></td>
                              <td><%=modExpDet.GetSetlineno%></td>
                              <td><%=modExpDet.GetSetreceiptno%></td>
                              <td><%=modExpDet.GetSetorderno%></td>
                              <td><%=modExpDet.GetSetitemno%></td>
                              <td><%=modExpDet.GetSetitemdesc%></td>
                              <td><%=modExpDet.GetSetunitprice%></td>
                              <td><%=modExpDet.GetSetdiscamount%></td>
                              <td><%=modExpDet.GetSetquantity%></td>
                              <td><%=modExpDet.GetSetexpensesprice%></td>
                              <td><%=modExpDet.GetSettaxcode%></td>
                              <td><%=modExpDet.GetSettaxamount%></td>
                              <td><%=modExpDet.GetSettotalexpenses%></td>
                              <td>
                                  <%
                                    if (!oModExpenses.GetSetstatus.Equals("CONFIRMED") && !oModExpenses.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modExpDet.GetSetlineno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
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
                                <td colspan="14">Rekod tiada...</td>
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
            <div class="modal fade modal-expenses-details" tabindex="-1" role="dialog" aria-hidden="true">
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
                                <input id="totalpurchaseamount" name="totalpurchaseamount" type="text" class="form-control" readonly="readonly" value="<%=oModExpenses.GetSetpurchaseamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Diskaun</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaldiscountamount" name="totaldiscountamount" type="text" class="form-control" readonly="readonly" value="<%=oModExpenses.GetSetdiscamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah TAX</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totaltaxamount" name="totaltaxamount" type="text" class="form-control" readonly="readonly" value="<%=oModExpenses.GetSettaxamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Keseluruhan Bil & Belanja</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalexpensesamount" name="totalexpensesamount" type="text" class="form-control" readonly="readonly" value="<%=oModExpenses.GetSettotalamount %>"/>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-5 col-sm-5 col-xs-12">Jumlah Bayaran Telah Dibuat</label>
                            <div class="col-md-7 col-sm-7 col-xs-12">
                                <input id="totalpaymentpaid" name="totalpaymentpaid" type="text" class="form-control" readonly="readonly" value="<%=oModExpenses.GetSetpaypaidamount %>"/>
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
                                  <td><%=oModExpenses.GetSetcreatedby %></td>
                                  <td><%=oModExpenses.GetSetcreateddate %></td>
                                </tr>
                                <tr>
                                  <td>Confirmed</td>
                                  <td><%=oModExpenses.GetSetconfirmedby %></td>
                                  <td><%=oModExpenses.GetSetconfirmeddate %></td>
                                </tr>
                                <tr>
                                  <td>Cancelled</td>
                                  <td><%=oModExpenses.GetSetcancelledby %></td>
                                  <td><%=oModExpenses.GetSetcancelleddate %></td>
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
                            //$('textarea#bpaddress').text('').val();
                            document.getElementById("bpaddress").value = "";
                            $('#bpcontact').val('');
                            $('#bpdesc').val('');
                        }
                    <%
                                  if (lsPayTo.Count > 0)
                                  {
                                      for (int i = 0; i < lsPayTo.Count; i++)
                                      {
                                          MainModel modBP = (MainModel)lsPayTo[i];
                    %>      
                                if ($(this).val() == "<%=modBP.GetSetbpid%>") {
                                    //$('textarea#bpaddress').text("<%=modBP.GetSetbpaddress%>").val();
                                    document.getElementById("bpaddress").value = "<%=modBP.GetSetbpaddress%>";
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
                            $('#hidItemNo').val('');
                            $('#hidReceiptNo').val('');
                            $('#hidReceiptLineNo').val('0');
                            $('#hidOrderNo').val('');
                            $('#hidOrderLineNo').val('0');
                            $('#additemdesc').text('');
                            $('#addunitprice').val('0');
                            $('#adddiscamount').val('0');
                            $('#addquantity').val('0');
                            $('#addexpensesprice').val('0');
                            $('#addtaxcode').val('');
                            $('#addtaxrate').val('0');
                            $('#addtaxamount').val('0');
                            $('#addtotalexpenses').val('0');
                        }
                        <%
                            if (lsPendExpMod.Count > 0)
                            {
                                for (int i = 0; i < lsPendExpMod.Count; i++)
                                {
                                    MainModel oPendExpLine = (MainModel)lsPendExpMod[i];
                        %>
                                    if ($(this).val() == '<%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE")||oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?oPendExpLine.GetSetreceiptno+"-"+oPendExpLine.GetSetlineno:oPendExpLine.GetSetparamid%>')
                                    {
                                        $('#hidItemNo').val('<%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE")||oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?oPendExpLine.GetSetitemno:oPendExpLine.GetSetparamid%>');
                                        $('#hidReceiptNo').val('<%=oPendExpLine.GetSetreceiptno%>');
                                        $('#hidReceiptLineNo').val('<%=oPendExpLine.GetSetlineno%>');
                                        $('#hidOrderNo').val('<%=oPendExpLine.GetSetorderno%>');
                                        $('#hidOrderLineNo').val('<%=oPendExpLine.GetSetorder_lineno%>');
                                        $('#additemdesc').text("<%=oModExpenses.GetSetexpensescat.Equals("PURCHASE_INVOICE")||oModExpenses.GetSetexpensescat.Equals("TRANSFER_INVOICE")?oPendExpLine.GetSetitemdesc:""%>");
                                        $('#addunitprice').val('<%=oPendExpLine.GetSetunitprice%>');
                                        $('#adddiscamount').val('<%=oPendExpLine.GetSetdiscamount%>');
                                        $('#addquantity').val('<%=oPendExpLine.GetSetquantity%>');
                                        $('#addexpensesprice').val('<%=oPendExpLine.GetSetexpensesprice%>');
                                        $('#addtaxcode').val('<%=oPendExpLine.GetSettaxcode%>');
                                        $('#addtaxrate').val('<%=oPendExpLine.GetSettaxrate%>');
                                        $('#addtaxamount').val('<%=oPendExpLine.GetSettaxamount%>');
                                        $('#addtotalexpenses').val('<%=oPendExpLine.GetSettotalexpenses%>');
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
                        var expensesprice = ($('#addunitprice').val() - $('#adddiscamount').val()) * $('#addquantity').val();
                        $('#addexpensesprice').val(expensesprice.toFixed(2));
                        var taxamnt = $('#addexpensesprice').val() * $('#addtaxrate').val() / 100;
                        $('#addtaxamount').val(taxamnt.toFixed(2));
                        var totamnt = parseFloat($('#addexpensesprice').val()) + parseFloat($('#addtaxamount').val());
                        $('#addtotalexpenses').val(totamnt.toFixed(2));
                    }
                }

                function actionclick(action) {
                    var proceed = true;
                    if (action == 'ADD') {
                        $('#expensesdate').removeAttr('required');
                        $('#expensestype').removeAttr('required');
                        $('#expensescat').removeAttr('required');
                        $('#bpid').removeAttr('required');
                        $('#bpdesc').removeAttr('required');
                        $('#bpaddress').removeAttr('required');
                        $('#bpcontact').removeAttr('required');
                        $('#status').removeAttr('required');
                    }
                    if (action == 'CREATE' || action == 'SAVE') {
                        //to enable bpid field before submitting update expenses header for case already have Line Item which logic, disable user from updating new bpid
                        $('#bpid').prop('disabled', false);
                        $('#bpdesc').prop('disabled', false);
                        $('#bpaddress').prop('disabled', false);
                        $('#bpcontact').prop('disabled', false);
                        $('#expensestype').prop('disabled', false);
                        $('#expensescat').prop('disabled', false);
                        /*
                        if ($('#bpdesc').val().length == 0) {
                            proceed = false;
                            new PNotify({
                                title: 'Alert',
                                text: 'Please fill out "BP Description" field!',
                                type: 'warning',
                                styling: 'bootstrap3'
                            });
                        }
                        */
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
                                text: 'Sila pilih "Item Bil & Belanja"!',
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
                        if (lsExpensesLineItem.Count > 0) { 
                    %>
                            $('#bpid').prop('disabled', true);
                            $('#bpdesc').prop('disabled', true);
                            $('#bpaddress').prop('disabled', true);
                            $('#bpcontact').prop('disabled', true);
                            $('#expensestype').prop('disabled', true);
                            $('#expensescat').prop('disabled', true);
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
                            $('#expensestype').prop('disabled', flag);
                            $('#expensescat').prop('disabled', flag);
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
                    $('#addlineno').val('<%=lsExpensesLineItem.Count + 1%>');
                }

                function confirmdeletelineitem(lineno) {
                    $('#hidLineNo').val(lineno);
                }

                //enable & disable button
                $(document).ready(function () {

                    $('#addlineitem').prop('disabled', true);
                    $('#printexpenses').prop('disabled', true);
                    $('#expensesdetails').prop('disabled', true);

                    $('#addlineitem').attr('disabled', 'disabled');
                    $('#printexpenses').attr('disabled', 'disabled');
                    $('#expensesdetails').attr('disabled', 'disabled');

                <%
                    if (sAction.Equals("OPEN"))
                    {
                        if (!oModExpenses.GetSetstatus.Equals("CONFIRMED") && !oModExpenses.GetSetstatus.Equals("CANCELLED"))
                        {
                            if (lsExpensesLineItem.Count > 0)
                            { 
                %>
                    $('#addlineitem').prop('disabled', false);
                    $('#printexpenses').prop('disabled', false);
                    $('#expensesdetails').prop('disabled', false);

                    $('#addlineitem').removeAttr('disabled');
                    $('#printexpenses').removeAttr('disabled');
                    $('#expensesdetails').removeAttr('disabled');

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
                            if (lsExpensesLineItem.Count > 0)
                            {
                %>
                    $('#printexpenses').prop('disabled', false);
                    $('#printexpenses').removeAttr('disabled');
                    $('#expensesdetails').prop('disabled', false);
                    $('#expensesdetails').removeAttr('disabled');
                <%
                            }
                        }
                    }
                %>

                });

                function openprintpage() {
                    var popupWindow = window.open("ExpensesPage.aspx?expensesno=<%=sExpensesNo%>", "open_printexpenses", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
                if (lsOtherPayTo.Count > 0)
                {
                %>
                    var bpother = {
                        <%
                        for (int i = 0; i < lsOtherPayTo.Count; i++)
                        {
                            MainModel modOthBP = (MainModel)lsOtherPayTo[i];
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
                                    for (int i = 0; i < lsOtherPayTo.Count; i++)
                                    {
                                        MainModel modOthBP = (MainModel)lsOtherPayTo[i];
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
                                        else if (i < lsOtherPayTo.Count)
                                        { 
                                    %>
                                        else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                        {
                                            document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                            $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                        }
                                    <%
                                        }

                                        if (i == lsOtherPayTo.Count-1) 
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
                        for (int i = 0; i < lsOtherPayTo.Count; i++)
                        {
                            MainModel modOthBP = (MainModel)lsOtherPayTo[i];
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
                            else if (i < lsOtherPayTo.Count)
                            { 
                        %>
                                else if($('#bpdesc').val() == '<%=modOthBP.GetSetobpdesc%>')
                                {
                                    document.getElementById("bpaddress").value = "<%=modOthBP.GetSetobpaddress%>";
                                    $('#bpcontact').val('<%=modOthBP.GetSetobpcontact%>');
                                }
                        <%
                            }

                            if (i == lsOtherPayTo.Count-1) 
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

