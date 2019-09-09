﻿<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="TransferListing2.aspx.cs" Inherits="TransferListing2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Pesanan Pindahan <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content">
                    <div class="">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <a class="btn btn-app" onclick="openaddneworder();">
                          <i class="fa fa-plus-square green"></i>Daftar 
                        </a>
                        <a class="btn btn-app" onclick="enabledisablesearchbox();">
                          <i class="fa fa-search"></i>Carian
                        </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-section" style="display:none;">
                        <section class="panel">
                            <div class="x_title">
                                <h2>Carian Pesanan Pindahan</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <div class="form-group">
                                        <label for="orderno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Pesanan:</label>
                                        <input type="text" id="orderno" class="form-control" name="orderno" value="<%=sOrderNo %>" />
                                    </div>
                                    <div class="form-group">
                                        <label for="compfrom" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Pindah Daripada:</label>
                                        <select id="compfrom" class="select2_single form-control" tabindex="-1" name="compfrom" style="width: 100%;">
                                                <option></option>
                                                <%
                                                    if (lsComp.Count > 0)
                                                    {
                                                        for (int i = 0; i < lsComp.Count; i++)
                                                        {
                                                            MainModel modComp = (MainModel)lsComp[i];
                                                %>       
                                                            <option value="<%=modComp.GetSetcomp_id %>" <%=sCompFrom.Equals(modComp.GetSetcomp_id)?"selected":"" %>><%=modComp.GetSetcomp_name %></option>
                                                <% 
                                                        }
                                                    }
                                                %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label for="compto" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Pindah Kepada:</label>
                                        <select id="compto" class="select2_single form-control" tabindex="-1" name="compto" style="width: 100%;">
                                                <option></option>
                                                <%
                                                    if (lsComp.Count > 0)
                                                    {
                                                        for (int i = 0; i < lsComp.Count; i++)
                                                        {
                                                            MainModel modComp = (MainModel)lsComp[i];
                                                %>       
                                                            <option value="<%=modComp.GetSetcomp_id %>" <%=sCompTo.Equals(modComp.GetSetcomp_id)?"selected":"" %>><%=modComp.GetSetcomp_name %></option>
                                                <% 
                                                        }
                                                    }
                                                %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                        <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                        <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                        <div style="display: none;">
                                            <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                            <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>
                    </div>

                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr class="headings">
                          <th></th>
                          <th>No. Pesanan</th>
                          <th>Kategori</th>
                          <th>Tarikh Pesanan</th>
                          <th>Pesanan Daripada</th>
                          <th>Tarikh Hantar</th>
                          <th>Pesanan Kepada</th>
                          <th>Tarikh Terima</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsOrderHeader.Count > 0)
                            {
                                for (int i = 0; i < lsOrderHeader.Count; i++) {
                                    MainModel modOrdHdr = (MainModel)lsOrderHeader[i];
                        %>       
                            <tr class="even pointer">
                              <td><a href="#" class="btn-link" onclick="openeditorder('<%=modOrdHdr.GetSetcomp %>','<%=modOrdHdr.GetSetorderno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditorder('<%=modOrdHdr.GetSetcomp %>','<%=modOrdHdr.GetSetorderno %>');"><%=modOrdHdr.GetSetorderno %></a></td>
                              <td><%=modOrdHdr.GetSetordercat %></td>
                              <td><%=modOrdHdr.GetSetorderdate %></td>
                              <td><%=modOrdHdr.GetSetCompFromDetails.GetSetcomp %> - <%=modOrdHdr.GetSetCompFromDetails.GetSetcomp_name %></td>
                              <td><%=modOrdHdr.GetSetshipmentdate %></td>
                              <td><%=modOrdHdr.GetSetCompToDetails.GetSetcomp %> - <%=modOrdHdr.GetSetCompToDetails.GetSetcomp_name %></td>
                              <td><%=modOrdHdr.GetSetreceiptdate %></td>
                              <td><%=modOrdHdr.GetSetorderstatus %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td colspan="9">Rekod tiada...</td>
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
        var currflag = "0";
        function enabledisablesearchbox() {
            var sb = document.getElementById("search-section");
            var cf = currflag;
            if (cf == "0") {
                sb.style.display = "none";
                currflag = "1";
            } else {
                sb.style.display = "";
                currflag = "0";
            }
        }
        enabledisablesearchbox();

        function openaddneworder() {
            var popupWindow = window.open("TransferDetails.aspx?action=ADD", "add_transfer", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
        function openeditorder(comp,orderno) {
            var popupWindow = window.open("TransferDetails.aspx?action=OPEN&comp="+comp+"&orderno="+orderno, "open_transfer", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function actionclick(action) {
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
        openaddneworder();
        <%
        }
        %>
    </script>
</asp:Content>

