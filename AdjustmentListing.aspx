<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="AdjustmentListing.aspx.cs" Inherits="AdjustmentListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Perlarasan Stok & Inventori <small>SENARAI</small></h2>
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
                        <a class="btn btn-app" onclick="openaddnewadjustment();">
                          <i class="fa fa-plus-square green"></i>Daftar
                        </a>
                        <a class="btn btn-app" onclick="enabledisablesearchbox();">
                          <i class="fa fa-search"></i> Carian
                        </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-section" style="display:none;">
                        <section class="panel">
                            <div class="x_title">
                                <h2>Carian Perlarasan Stok & Inventori</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <div class="form-group">
                                        <label for="adjustmentno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Perlarasan:</label>
                                        <input type="text" id="adjustmentno" class="form-control" name="adjustmentno" value="<%=sAdjustmentNo %>" />
                                    </div>
                                    <div class="form-group">
                                        <label for="itemno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Kod Item:</label>
                                        <select id="itemno" class="select2_single form-control" tabindex="-1" name="itemno" style="width: 100%;">
                                                <option></option>
                                                <%
                                                    if (lsItem.Count > 0)
                                                    {
                                                        for (int i = 0; i < lsItem.Count; i++)
                                                        {
                                                            MainModel modItem = (MainModel)lsItem[i];
                                                %>       
                                                            <option value="<%=modItem.GetSetitemno %>" <%=sItemNo.Equals(modItem.GetSetitemno)?"selected":"" %>><%=modItem.GetSetitemno %></option>
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
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>No. Perlarasan</th>
                          <th>Jenis Perlarasan</th>
                          <th>Tarikh Perlarasan</th>
                          <th>Tarikh Pengesahan</th>
                          <th>Status</th>
                          <th>Catatan</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsAdjustmentHeader.Count > 0)
                            {
                                for (int i = 0; i < lsAdjustmentHeader.Count; i++)
                                {
                                    MainModel modAdjHdr = (MainModel)lsAdjustmentHeader[i];
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditadjustment('<%=modAdjHdr.GetSetcomp %>','<%=modAdjHdr.GetSetadjustmentno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditadjustment('<%=modAdjHdr.GetSetcomp %>','<%=modAdjHdr.GetSetadjustmentno %>');"><%=modAdjHdr.GetSetadjustmentno %></a></td>
                              <td><%=modAdjHdr.GetSetadjustmenttype %></td>
                              <td><%=modAdjHdr.GetSetadjustmentdate %></td>
                              <td><%=modAdjHdr.GetSetconfirmeddate %></td>
                              <td><%=modAdjHdr.GetSetstatus %></td>
                              <td><%=modAdjHdr.GetSetremarks %></td>
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

        function openaddnewadjustment() {
            var popupWindow = window.open("AdjustmentDetails.aspx?action=ADD", "add_adjustment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
        function openeditadjustment(comp, adjustmentno) {
            var popupWindow = window.open("AdjustmentDetails.aspx?action=OPEN&comp=" + comp + "&adjustmentno=" + adjustmentno, "open_adjustment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            openaddnewadjustment();
        <%
        }
        %>
    </script>
</asp:Content>

