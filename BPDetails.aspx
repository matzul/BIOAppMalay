<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="BPDetails.aspx.cs" Inherits="BPDetails" %>

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
                                    <label for="bpid">Id Pembekal/ Pelanggan:</label>
                                    <input type="text" id="bpid" class="form-control"  name="bpid" value="<%=oModBP.GetSetbpid %>" readonly="readonly" />
                                    <label for="bpcat">Kategori:</label>
                                      <select class="form-control" id="bpcat" name="bpcat" required="required">
                                        <option value="">-select-</option>
                                        <option value="FINANCIAL" <%=oModBP.GetSetbpcat.Equals("FINANCIAL")?"selected":"" %>>INSTITUSI KEWANGAN</option>
                                        <option value="SUBSIDIARY" <%=oModBP.GetSetbpcat.Equals("SUBSIDIARY")?"selected":"" %>>ANAK SYARIKAT</option>
                                        <option value="GOVERMENT" <%=oModBP.GetSetbpcat.Equals("GOVERMENT")?"selected":"" %>>KERAJAAN/ BADAN BERKANUN</option>
                                        <option value="CORPORATE" <%=oModBP.GetSetbpcat.Equals("CORPORATE")?"selected":"" %>>SYARIKAT SWASTA</option>
                                        <option value="PARTNERS" <%=oModBP.GetSetbpcat.Equals("PARTNERS")?"selected":"" %>>RAKAN STRATEGIK</option>
                                        <option value="STAKEHOLDER" <%=oModBP.GetSetbpcat.Equals("STAKEHOLDER")?"selected":"" %>>PEMEGANG TARUH</option>
                                        <option value="SHOP" <%=oModBP.GetSetbpcat.Equals("SHOP")?"selected":"" %>>KEDAI RUNCIT/ PEMBORONG</option>
                                        <option value="SERVICE" <%=oModBP.GetSetbpcat.Equals("SERVICE")?"selected":"" %>>AGENSI PERKHIDMATAN</option>
                                        <option value="CLUB" <%=oModBP.GetSetbpcat.Equals("CLUB")?"selected":"" %>>KELAB/ PERSATUAN</option>
                                        <option value="ORGANIZATION" <%=oModBP.GetSetbpcat.Equals("ORGANIZATION")?"selected":"" %>>ORGANISASI/ PERTUBUHAN</option>
                                        <option value="FACTORY" <%=oModBP.GetSetbpcat.Equals("FACTORY")?"selected":"" %>>PERKILANGAN</option>
                                        <option value="RESTAURANT" <%=oModBP.GetSetbpcat.Equals("RESTAURANT")?"selected":"" %>>RESTORAN/ CAFETERIA</option>
                                        <option value="RECREATION" <%=oModBP.GetSetbpcat.Equals("RECREATION")?"selected":"" %>>REKREASI/ PERLANCONGAN</option>
                                        <option value="INDIVIDUAL" <%=oModBP.GetSetbpcat.Equals("INDIVIDUAL")?"selected":"" %>>INDIVIDU/ ORANG AWAM</option>
                                        <option value="STOCKIST" <%=oModBP.GetSetbpcat.Equals("STOCKIST")?"selected":"" %>>STOCKIST</option>
                                        <option value="AGENT" <%=oModBP.GetSetbpcat.Equals("AGENT")?"selected":"" %>>AGENT</option>
                                        <option value="INTERNAL" <%=oModBP.GetSetbpcat.Equals("INTERNAL")?"selected":"" %>>INTERNAL</option>
                                        <option value="SYSTEM" <%=oModBP.GetSetbpcat.Equals("SYSTEM")?"selected":"" %>>SISTEM</option>
                                        <option value="OTHER" <%=oModBP.GetSetbpcat.Equals("OTHER")?"selected":"" %>>LAIN-LAIN</option>
                                      </select>
                                    <label for="discounttype">Jenis Diskaun:</label>
                                      <select class="form-control" id="discounttype" name="discounttype" required="required">
                                        <option value="">-select-</option>
                                        <option value="NORMAL" <%=oModBP.GetSetdiscounttype.Equals("NORMAL")?"selected":"" %>>NORMAL</option>
                                        <option value="PROMOTION" <%=oModBP.GetSetdiscounttype.Equals("PROMOTION")?"selected":"" %>>PROMOSI</option>
                                        <option value="AGENT" <%=oModBP.GetSetdiscounttype.Equals("AGENT")?"selected":"" %>>AGENT</option>
                                        <option value="STOCKIST" <%=oModBP.GetSetdiscounttype.Equals("STOCKIST")?"selected":"" %>>STOCKIST</option>
                                      </select>
                                    <label for="cashguarantee">Jaminan Wang Tunai:</label>
                                    <input type="text" id="cashguarantee" class="form-control" name="cashguarantee" required="required" value="<%=oModBP.GetSetcashguarantee %>"/>
                                    <label for="bankguarantee">Jaminan Di Bank:</label>
                                    <input type="text" id="bankguarantee" class="form-control" name="bankguarantee" required="required" value="<%=oModBP.GetSetbankguarantee %>"/>
                                    <label for="creditlimit">Had Kredit:</label>
                                    <input type="text" id="creditlimit" class="form-control" name="creditlimit" readonly="readonly" value="<%=oModBP.GetSetcreditlimit %>"/>
                                    <label for="bpstatus">Status:</label>
                                      <select class="form-control" id="bpstatus" name="bpstatus" required="required">
                                        <option value="">-select-</option>
                                        <option value="ACTIVE" <%=oModBP.GetSetbpstatus.Equals("ACTIVE")?"selected":"" %>>AKTIF</option>
                                        <option value="IN-ACTIVE" <%=oModBP.GetSetbpstatus.Equals("IN-ACTIVE")?"selected":"" %>>TIDAK AKTIF</option>
                                      </select>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="bpdesc">Nama Pembekal/ Pelanggan:</label>
                                    <textarea id="bpdesc" class="form-control" rows="3" name="bpdesc" required="required"><%=oModBP.GetSetbpdesc %></textarea>
                                    <label for="bpaddress">Alamat:</label>
                                    <textarea id="bpaddress" class="form-control" rows="3" name="bpaddress" required="required"><%=oModBP.GetSetbpaddress %></textarea>
                                    <label for="bpcontact">No. Tel.:</label>
                                    <input type="text" id="bpcontact" class="form-control" name="bpcontact" required="required" value="<%=oModBP.GetSetbpcontact %>"/>
                                    <label for="bpreference">Rujukan:</label>
                                    <input type="text" id="bpreference" class="form-control" name="bpreference" value="<%=oModBP.GetSetbpreference %>"/>
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
                                    <button id="btnCreate" name="btnCreate" type="button" class="btn btn-info" onclick="actionclick('CREATE');">Daftar</button>
                                    <button id="btnReset" name="btnReset" type="button" class="btn btn-warning" onclick="actionclick('ADD');">Reset</button>
                                    <%
                                        }
                                        else if (sAction.Equals("OPEN")) 
                                        { 
                                    %>
                                    <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');">Kemaskini</button>
                                    <%
                                        }
                                        else if (sAction.Equals("EDIT")) 
                                        { 
                                    %>
                                    <button id="btnSave" name="btnSave" type="button" class="btn btn-success" onclick="actionclick('SAVE');">Simpan</button>
                                    <%
                                        }
                                    %>
                                    <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();">Tutup</button>
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
                                        <input type="hidden" name="hidBPId" id="hidBPId" value="<%=sBPId %>" />
                                    </div>
                                </div>
                            </div>
                        </section>
                  </div> 
                  </form>                   
                  <div class="col-md-12 col-sm-12 col-xs-12">
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th>No. Invois</th>
                              <th>Tarikh Invois</th>
                              <th>No. Penghantaran</th>
                              <th>No. Pesanan</th>
                              <th>Jumlah Invois</th>
                              <th>Bayaran Jelas</th>
                              <th>Belum Jelas</th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsInvoiceCollection.Count > 0)
                            {
                                for (int i = 0; i < lsInvoiceCollection.Count; i++)
                                {
                                    MainModel modInvoice = (MainModel)lsInvoiceCollection[i];
                        %>       
                            <tr>
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
                            } else {
                        %>
                            <tr>
                                <td colspan="7">Tiada rekod...</td>
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
            <script type="text/javascript">
                function actionclick(action) {
                    if (action == 'ADD') {
                        $('#bpcat').removeAttr('required');
                        $('#discounttype').removeAttr('required');
                        $('#cashguarantee').removeAttr('required');
                        $('#bankguarantee').removeAttr('required');
                        $('#creditlimit').removeAttr('required');
                        $('#bpstatus').removeAttr('required');
                        $('#bpdesc').removeAttr('required');
                        $('#bpaddress').removeAttr('required');
                        $('#bpcontact').removeAttr('required');
                        $('#bpreference').removeAttr('required');
                    }
                    document.getElementById("hidAction").value = action;
                    var button = document.getElementById("<%=btnAction.ClientID %>");
                    button.click();
                }
                function enabledisableinputform(flag) {
                    $('#bpcat').prop('disabled', flag);
                    $('#discounttype').prop('disabled', flag);
                    $('#cashguarantee').prop('disabled', flag);
                    $('#bankguarantee').prop('disabled', flag);
                    $('#bpstatus').prop('disabled', flag);
                    $('#bpdesc').prop('disabled', flag);
                    $('#bpaddress').prop('disabled', flag);
                    $('#bpcontact').prop('disabled', flag);
                    $('#bpreference').prop('disabled', flag);
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
            </script>            
</asp:Content>
