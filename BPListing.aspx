<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="BPListing.aspx.cs" Inherits="BPListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Pembekal & Pelanggan <small>SENARAI</small></h2>
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
                    <a class="btn btn-app" onclick="openaddnewbp();">
                      <i class="fa fa-plus-square green"></i>Daftar 
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i>Carian
                    </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                        <section class="panel">

                            <div class="x_title">
                                <h2>Carian Pembekal & Pelanggan</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <label for="bpid">Id Pembekal/ Pelanggan:</label>
                                    <input type="text" id="bpid" class="form-control" name="bpid" value="<%=sBPId %>" />
                                    <label for="bpdesc">Nama Pembekal/ Pelanggan:</label>
                                    <input type="text" id="bpdesc" class="form-control" name="bpdesc" value="<%=sBPDesc %>" />
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
                          <th>Id Pembekal/ Pelanggan</th>
                          <th>Nama Pembekal/ Pelanggan</th>
                          <th>Alamat</th>
                          <th>No. Tel.</th>
                          <th>Rujukan</th>
                          <th>Kategori</th>
                          <th>Jenis Diskaun</th>
                          <th>Had Limit</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsBP.Count > 0)
                            {
                                for (int i = 0; i < lsBP.Count; i++)
                                {
                                    MainModel modBP = (MainModel)lsBP[i];
                        %>       
                        <tr>
                          <td><a href="#" class="btn-link" onclick="openeditbp('<%=modBP.GetSetcomp %>','<%=modBP.GetSetbpid %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                          <td><a href="#" class="btn-link" onclick="openeditbp('<%=modBP.GetSetcomp %>','<%=modBP.GetSetbpid %>');"><%=modBP.GetSetbpid %></a></td>
                          <td><%=modBP.GetSetbpdesc %></td>
                          <td><%=modBP.GetSetbpaddress %></td>
                          <td><%=modBP.GetSetbpcontact %></td>
                          <td><%=modBP.GetSetbpreference %></td>
                          <td><%=modBP.GetSetbpcat %></td>
                          <td><%=modBP.GetSetdiscounttype %></td>
                          <td><%=modBP.GetSetcreditlimit %></td>
                          <td><%=modBP.GetSetbpstatus %></td>
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

        function openaddnewbp() {
            var popupWindow = window.open("BPDetails.aspx?action=ADD", "add_newbp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openeditbp(comp, bpid) {
            var popupWindow = window.open("BPDetails.aspx?action=OPEN&comp=" + comp + "&bpid=" + bpid, "open_bp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            openaddnewbp();
        <%
        }
        %>
    </script>
</asp:Content>

