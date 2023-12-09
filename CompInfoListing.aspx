<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="CompInfoListing.aspx.cs" Inherits="CompInfoListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>INFO <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content table-responsive">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                    <a class="btn btn-app" onclick="openaddnewinfocomp();">
                      <i class="fa fa-plus-square green"></i>Daftar 
                    </a>
                    <a class="btn btn-app" onclick="openinfocomp();">
                      <i class="fa fa-building-o blue"></i>Info Organisasi 
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i>Carian
                    </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                        <section class="panel">

                            <div class="x_title">
                                <h2>Carian Aduan</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <label for="complaintStatus">Jenis:</label>
                                    <select class="form-control" id="infoType" name="infoType">
                                        <option value="">-select-</option>
                                        <option value="PERUTUSAN" <%=sinfoType.Equals("PERUTUSAN")?"selected":"" %>>PERUTUSAN</option>
                                        <option value="SEJARAH" <%=sinfoType.Equals("SEJARAH")?"selected":"" %>>SEJARAH</option>
                                        <option value="VISI" <%=sinfoType.Equals("VISI")?"selected":"" %>>VISI</option>
                                        <option value="MISI" <%=sinfoType.Equals("MISI")?"selected":"" %>>MISI</option>
                                    </select>
                                    <label for="complaintStatus">Status:</label>
                                    <select class="form-control" id="infoStatus" name="infoStatus">
                                        <option value="">-select-</option>
                                        <option value="ACTIVE" <%=sinfoStatus.Equals("ACTIVE")?"selected":"" %>>ACTIVE</option>
                                        <option value="IN-ACTIVE" <%=sinfoStatus.Equals("IN-ACTIVE")?"selected":"" %>>IN-ACTIVE</option>
                                    </select>
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
                    <table id="datatable-customs" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>No. Info</th>
                          <th>Jenis Info</th>
                          <%--<th>Huraian</th>--%>
                          <th>Status</th>
                          <%--<th>Tarikh</th>--%>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsInfo.Count > 0)
                            {
                                for (int i = 0; i < lsInfo.Count; i++)
                                {
                                    MainModel modInfo = (MainModel)lsInfo[i];
                        %>       
                        <tr>
                          <td><a href="#" class="btn-link" onclick="openeditcomp('<%=modInfo.GetSetcomp %>','<%=modInfo.GetSetinfo_no %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                          <td><a href="#" class="btn-link" onclick="openeditcomp('<%=modInfo.GetSetcomp %>','<%=modInfo.GetSetinfo_no %>');"><%=modInfo.GetSetinfo_no %></a></td>
                          <td><%=modInfo.GetSetinfo_type %></td>
                          <%--<td><% Response.Write(modInfo.GetSetinfo_desc.Substring(8) + "..."); %></td>--%>
                          <td><%=modInfo.GetSetinfo_status %></td>
                          <%--<td><%
                                DateTime dt = Convert.ToDateTime(modInfo.GetSetcreateddate);
								Response.Write(dt.ToString("dd MMMM yyyy"));
								%>
						  </td>--%>
                        </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td></td>
                                <td>Tiada rekod...</td>
                                <td></td>
                                <%--<td></td>--%>
                                <td></td>
                                <%--<td></td>--%>
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

        function openaddnewinfocomp() {
            var popupWindow = window.open("CompInfoDetails.aspx?action=ADD&compid=<%=sCurrComp %>", "add_newcomp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openinfocomp() {
            var popupWindow = window.open("CompInfoDetails2.aspx?action=OPEN&compno=<%=sCurrComp %>", "add_newcomp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openeditcomp(comp, compno) {
            var popupWindow = window.open("CompInfoDetails.aspx?action=OPEN&compid=" + comp + "&compno=" + compno, "open_comp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            openaddnewbp();
        <%
        }
        %>

        $(document).ready(function () {
            $("div.toolbar").html('<select id="selNextPage" name="selNextPage" onchange="actionclick(\'NEXT_PAGE\');"><%for(int x=1;x<=Math.Ceiling((double)lsInfoCount.Count / 10);x++){%><option <%=sCurrPage.Equals(x.ToString())?"selected":""%> value="<%=x%>">Page <%=x%></option><%}%></select> / <% Response.Write(Math.Ceiling((double)lsInfoCount.Count / 10));%> Pages');
         });
    </script>
</asp:Content>

