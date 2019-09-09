<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="StockOnHandDetails.aspx.cs" Inherits="StockOnHandDetails" %>

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
                                    <label for="itemno">No. Item:</label>
                                    <input type="text" id="itemno" class="form-control" required="required" name="itemno" value="<%=oModItem.GetSetitemno %>" />
                                    <label for="itemcat">Kategori Item:</label>
                                      <select class="form-control" id="itemcat" name="itemcat" required="required">
                                        <option value="">-select-</option>
                                        <option value="INVENTORY" <%=oModItem.GetSetitemcat.Equals("INVENTORY")?"selected":"" %>>INVENTORI</option>
                                        <option value="SERVICE" <%=oModItem.GetSetitemcat.Equals("SERVICE")?"selected":"" %>>PERKHIDMATAN</option>
                                        <option value="ASSET" <%=oModItem.GetSetitemcat.Equals("ASSET")?"selected":"" %>>ASET</option>
                                      </select>
                                    <label for="qtysoh">Qty SOH:</label>
                                    <input type="text" id="qtysoh" class="form-control" readonly="readonly" name="qtysoh" value="<%=oModItem.GetSetqtysoh %>"/>
                                    <label for="costsoh">Kos SOH:</label>
                                    <input type="text" id="costsoh" class="form-control" readonly="readonly" name="costsoh" value="<%=oModItem.GetSetcostsoh %>"/>
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="itemdesc">Keterangan Item:</label>
                                    <textarea id="itemdesc" class="form-control" rows="3" name="itemdesc" required="required"><%=oModItem.GetSetitemdesc %></textarea>
                                    <label for="qtyorder">Qty Pesanan:</label>
                                    <input type="text" id="qtyorder" class="form-control" readonly="readonly" name="qtyorder" value="<%=oModItem.GetSetqtyorder %>"/>
                                    <label for="qtydemand">Qty Permintaan:</label>
                                    <input type="text" id="qtydemand" class="form-control" readonly="readonly" name="qtydemand" value="<%=oModItem.GetSetqtydemand %>"/>
                                </div>
                    </div>
                  </div>
                  <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
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
                                        <input type="hidden" name="hidItemNo" id="hidItemNo" value="<%=sItemNo %>" />
                                    </div>
                                </div>
                            </div>
                        </section>
                  </div> 
                  </form>                   
                  <div class="col-md-12 col-sm-12 col-xs-12">
                      <div class="table-responsive">
                        <table id="datatable-buttons" class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th>#</th>
                              <th>Lokasi</th>
                              <th>Tarikh SOH</th>
                              <th>Qty SOH</th>
                              <th>Kos SOH</th>
                              <th>Qty Diperuntukkan</th>
                              <th>Qty Tersedia</th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsStockListing.Count > 0)
                            {
                                for (int i = 0; i < lsStockListing.Count; i++)
                                {
                                    MainModel modItemStock = (MainModel)lsStockListing[i];
                        %>       
                            <tr>
                              <td><%=i+1%></td>
                              <td><%=modItemStock.GetSetlocation %></td>
                              <td><%=modItemStock.GetSetdatesoh %></td>
                              <td><%=modItemStock.GetSetqtysoh %></td>
                              <td><%=modItemStock.GetSetcostsoh %></td>
                              <td><%=modItemStock.GetSetqtyallocated %></td>
                              <td><%=modItemStock.GetSetqtyavailable %></td>
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
                        $('#itemno').removeAttr('required');
                        $('#itemdesc').removeAttr('required');
                        $('#itemcat').removeAttr('required');
                    }
                    document.getElementById("hidAction").value = action;
                    var button = document.getElementById("<%=btnAction.ClientID %>");
                    button.click();
                }

                function enabledisableinputform(flag) {
                    <% if (sAction.Equals("EDIT")){%>
                    $('#itemno').prop('disabled', true);
                    <% }else{%>
                    $('#itemno').prop('disabled', flag);
                    <% }%>
                    $('#itemdesc').prop('disabled', flag);
                    $('#itemcat').prop('disabled', flag);
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
