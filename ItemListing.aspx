<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ItemListing.aspx.cs" Inherits="ItemListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Item & Produk <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content table-responsive">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                    <a class="btn btn-app" onclick="openaddnewitem();">
                      <i class="fa fa-plus-square green"></i>Daftar 
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i> Carian
                    </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                        <section class="panel">

                            <div class="x_title">
                                <h2>Carian Item & Produk</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <label for="itemno">Kod Item:</label>
                                    <input type="text" id="itemno" class="form-control" name="itemno" value="<%=sItemNo %>" />
                                    <label for="itemdesc">Keterangan Item:</label>
                                    <input type="text" id="itemdesc" class="form-control" name="itemdesc" value="<%=sItemDesc %>" />
                                    <br/>
                                    <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                    <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                    <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                    <div style="display: none;">
                                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>Kod Item</th>
                          <th>Keterangan Item</th>
                          <th>Kategori</th>
                          <th>Ukuran Item</th>
                          <th>Harga Belian</th>
                          <th>Harga Kos</th>
                          <th>Harga Jualan</th>
                          <th>Qty Pesanan</th>
                          <th>Qty Permintaan</th>
                          <th>Qty SOH</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsItem.Count > 0)
                            {
                                for (int i = 0; i < lsItem.Count; i++) {
                                    MainModel modItem = (MainModel)lsItem[i];
                        %>       
                        <tr>
                          <td><a href="#" class="btn-link" onclick="openedititem('<%=modItem.GetSetcomp %>','<%=modItem.GetSetitemno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                          <td><a href="#" class="btn-link" onclick="openedititem('<%=modItem.GetSetcomp %>','<%=modItem.GetSetitemno %>');"><%=modItem.GetSetitemno %></a></td>
                          <td><%=modItem.GetSetitemdesc %></td>
                          <td><%=modItem.GetSetitemcat %></td>
                          <td><%=modItem.GetSetitemtype %></td>
                          <td><%=modItem.GetSetpurchaseprice %></td>
                          <td><%=modItem.GetSetcostprice %></td>
                          <td><%=modItem.GetSetsalesprice %></td>
                          <td><%=modItem.GetSetqtyorder %></td>
                          <td><%=modItem.GetSetqtydemand %></td>
                          <td><%=modItem.GetSetqtysoh %></td>
                          <td><%=modItem.GetSetitemstatus %></td>
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

    <script type="text/javascript">
        var currflag = false;
        function enabledisablesearchbox() {
            var sb = document.getElementById("search-box");
            cf = currflag;
            if (cf == false) {
                sb.style.display = "none";
                currflag = true;
            } else {
                sb.style.display = "";
                currflag = false;
            }
        }
        enabledisablesearchbox(currflag);

        function openaddnewitem() {
            var popupWindow = window.open("ItemDetails.aspx?action=ADD", "add_newitem", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openedititem(comp, itemno) {
            var popupWindow = window.open("ItemDetails.aspx?action=OPEN&comp=" + comp + "&itemno=" + itemno, "open_item", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            openaddnewitem();
        <%
        }
        %>
    </script>
</asp:Content>

