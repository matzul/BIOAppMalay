<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="AssetListing.aspx.cs" Inherits="AssetListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Aset Harta Modal <small>SENARAI</small></h2>
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
                    <a class="btn btn-app" onclick="openaddnewasset();">
                      <i class="fa fa-plus-square green"></i>Daftar 
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i>Carian
                    </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                        <section class="panel">

                            <div class="x_title">
                                <h2>Carian Aset Harta Modal</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <label for="assetno">Id/ No. Aset:</label>
                                    <input type="text" id="assetno" class="form-control" name="assetno" value="<%=sAssetNo %>" />
                                    <label for="assetdesc">Nama/ Keterangan Aset:</label>
                                    <input type="text" id="assetdesc" class="form-control" name="assetdesc" value="<%=sAssetDesc %>" />
                                    <br/>
                                    <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                    <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                    <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                    <div style="display: none;">
                                        <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                        <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                        <input type="hidden" name="hidNextPage" id="hidNextPage" value="<%=sCurrPage %>" />
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <table id="datatable-custom2" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>Id/ No. Aset</th>
                          <th>Nama/ Keterangan Aset</th>
                          <th>Kategori</th>
                          <th>Jenis</th>
                          <th>Tarikh Daftar</th>
                          <th>Waranti</th>
                          <th>Nilai Aset</th>
                          <th>Susut Nilai</th>
                          <th>Nilai Semasa</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsAsset.Count > 0)
                            {
                                //for (int i = 0; i < lsBP.Count; i++)
                                for (int i = 0; i < (lsAsset.Count <= 10 ? lsAsset.Count : 10); i++)
                                {
                                    MainModel modAsset = (MainModel)lsAsset[i];
                        %>       
                        <tr>
                          <td><a href="#" class="btn-link" onclick="openeditasset('<%=modAsset.GetSetcomp %>','<%=modAsset.GetSetassetno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                          <td><a href="#" class="btn-link" onclick="openeditasset('<%=modAsset.GetSetcomp %>','<%=modAsset.GetSetassetno %>');"><%=modAsset.GetSetassetno %></a></td>
                          <td><%=modAsset.GetSetassetdesc %></td>
                          <td><%=modAsset.GetSetassetcat %></td>
                          <td><%=modAsset.GetSetassettyp %></td>
                          <td><%=modAsset.GetSetdatereg %></td>
                          <td><%=modAsset.GetSetdatewarend %></td>
                          <td><%=modAsset.GetSetcostreg %></td>
                          <td><%=modAsset.GetSetdepraccum %></td>
                          <td><%=modAsset.GetSetassetnbv %></td>
                          <td><%=modAsset.GetSetstatus %></td>
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
                                <td></td>
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    <div class="toolbar"></div>
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

        function openaddnewasset() {
            var popupWindow = window.open("AssetDetails.aspx?action=ADD", "add_newasset", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openeditasset(comp, assetno) {
            var popupWindow = window.open("AssetDetails.aspx?action=OPEN&comp=" + comp + "&assetno=" + assetno, "open_asset", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            document.getElementById("hidNextPage").value = document.getElementById("selNextPage").value;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
            openaddnewasset();
        <%
        }
        %>

        $(document).ready(function () {
            $("div.toolbar").html('<select id="selNextPage" name="selNextPage" onchange="actionclick(\'NEXT_PAGE\');"><%for(int x=1;x<=Math.Ceiling((double)lsAssetCount.Count / 10);x++){%><option <%=sCurrPage.Equals(x.ToString())?"selected":""%> value="<%=x%>">Page <%=x%></option><%}%></select> / <% Response.Write(Math.Ceiling((double)lsAssetCount.Count / 10));%> Pages');
        });

    </script>
</asp:Content>

