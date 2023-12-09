<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMobile.master" AutoEventWireup="true" CodeFile="StockStateSOHMobile.aspx.cs" Inherits="StockStateSOHMobile" %>

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
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                    <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();">Close</button>
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
                                        <input type="hidden" name="hidStockStateNo" id="hidStockStateNo" value="<%=sStockStateNo %>" />
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
                              <th>Item</th>
                              <th>Description</th>
                              <th>Location</th>
                              <th>Date SOH</th>
                              <th>Qty SOH</th>
                              <th>Unit Cost</th>
                              <th>Cost SOH</th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsStockListing.Count > 0)
                            {
                                for (int i = 0; i < lsStockListing.Count; i++)
                                {
                                    MainModel modItemStock = (MainModel)lsStockListing[i];
                                    double unitcost = 0;
                                    if (modItemStock.GetSetqtysoh > 0)
                                    {
                                        unitcost = Math.Round(modItemStock.GetSetcostsoh / modItemStock.GetSetqtysoh,2,MidpointRounding.AwayFromZero);
                                    }
                        %>       
                            <tr>
                              <td><%=i+1%></td>
                              <td><%=modItemStock.GetSetitemno %></td>
                              <td><%=modItemStock.GetSetitemdesc %></td>
                              <td><%=modItemStock.GetSetlocation %></td>
                              <td><%=modItemStock.GetSetdatesoh %></td>
                              <td><%=modItemStock.GetSetqtysoh %></td>
                              <td><%=unitcost %></td>
                              <td><%=modItemStock.GetSetcostsoh %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                              <td></td>
                              <td>No record found...</td>
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
            <script type="text/javascript">
                function actionclick(action) {
                    if (action == 'ADD') {
                    }
                    document.getElementById("hidAction").value = action;
                    var button = document.getElementById("<%=btnAction.ClientID %>");
                    button.click();
                }

            </script>            
</asp:Content>
