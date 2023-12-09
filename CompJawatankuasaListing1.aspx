<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="CompJawatankuasaListing1.aspx.cs" Inherits="CompJawatankuasaListing1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.19/css/dataTables.bootstrap.min.css" />
    <script src="https://adminlte.io/themes/AdminLTE/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="js/WebMethodService.Call.Helper.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <style>
        .google-visualization-orgchart-node {
            border: 0px !important;
        }

        .google-visualization-orgchart-nodesel {
            border: 0px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Pegawai masjid-->
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Ahli Jawatankuasa <small>Carta Organisasi & Sejarah Jawatankuasa</small></h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>

            <div class="x_content table-responsive text-center">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div id="chart_div"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2>Jawatankuasa <small>SENARAI</small></h2>
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
                    <a class="btn btn-app" onclick="openaddnewjawatankuasa();">
                        <i class="fa fa-plus-square green"></i>Daftar 
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                        <i class="fa fa-search"></i>Carian
                    </a>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display: none;">
                    <section class="panel">

                        <div class="x_title">
                            <h2>Carian Jawatankuasa</h2>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body">
                            <form id="search" runat="server">
                                <label for="committeeid">Kad Pengenalan:</label>
                                <input type="text" id="committeeid" class="form-control" name="committeeid" value="<%=sComtID %>" />
                                <label for="commiteename">Nama:</label>
                                <input type="text" id="commiteename" class="form-control" name="commiteename" value="<%=sComtName %>" />
                                <br />
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
                                <th>No. ID</th>
                                <th>Nama</th>
                                <th>No. Tel</th>
                                <th>Jawatan</th>
                                <th>Tarikh Lantikan</th>
                                <th>Status</th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsCommittee.Count > 0)
                                {
                                    for (int i = 0; i < lsCommittee.Count; i++)
                                    {
                                        MainModel modJawatankuasa = (MainModel)lsCommittee[i];
                            %>
                            <tr>
                                <td><a href="#" class="btn-link" onclick="openeditjawatankuasa('<%=modJawatankuasa.GetSetcomp %>','<%=modJawatankuasa.GetSetcommittee_id %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                <td><a href="#" class="btn-link" onclick="openeditjawatankuasa('<%=modJawatankuasa.GetSetcomp %>','<%=modJawatankuasa.GetSetcommittee_id %>');"><%=modJawatankuasa.GetSetcommittee_id %></a></td>
                                <td><%=modJawatankuasa.GetSetcommittee_name %></td>
                                <td><%=modJawatankuasa.GetSetcommittee_contact %></td>
                                <td><%=modJawatankuasa.GetSetcommittee_role %></td>
                                <td><%=modJawatankuasa.GetSetcommittee_doa%></td>
                                <td><%=modJawatankuasa.GetSetcommittee_status %></td>
                            </tr>
                            <% 
                                    }
                                }
                                else
                                {
                            %>
                            <tr>
                                <td></td>
                                <td>Tiada rekod...</td>
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

    <!--BEGIN dialog box for confirm position-->
    <div class="modal fade modal-position-user" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Maklumat Jawatan</h4>
                </div>
                <div class="modal-body ">
                    <table id="tbPositionList" class="table" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Butiran Pegawai</th>
                                <th style="display: none;">bpdesc</th>
                                <th style="display: none;">bpaddress</th>
                                <th style="display: none;">bptelno</th>
                                <th>Jawatan/ Tarikh Lantikan</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer ">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!--END dialog box for confirm position-->
    <script type="text/javascript">
        var currflag = false;

        pTable = $('#tbPositionList').DataTable({
            'paging': true,
            'pageLength': 3,
            'lengthChange': false,
            'searching': true,
            'ordering': true,
            'info': false,
            'autoWidth': true,
            "columnDefs": [
                {
                    "targets": [2],
                    "visible": false
                },
                {
                    "targets": [3],
                    "visible": false
                },
                {
                    "targets": [4],
                    "visible": false
                }
            ]
        });

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

        function openaddnewjawatankuasa() {
            var popupWindow = window.open("CompJawatankuasaDetails1.aspx?action=ADD&compid=<%=sCurrComp %>", "add_newcomp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openeditjawatankuasa(comp, userid) {
            var popupWindow = window.open("CompJawatankuasaDetails1.aspx?action=OPEN&compid=" + comp + "&user=" + userid, "open_comp", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function openmodalrole(comp, commid) {
            $(".modal-position-user").modal("show");

            //var parameters_getPositionListHistory = ["comp", comp,"committeerole", role, "committeetype", "JK_COMP", "committee_status", "IN-ACTIVE"];
            //PageMethod("getPositionListHistory", parameters_getPositionListHistory, succeededAjaxFn_getPositionListHistory, failedAjaxFn_getPositionListHistory, false);

            var parameters_getCommitteeList = ["commid", commid];
            PageMethod("getCommitteeList", parameters_getCommitteeList, succeededAjaxFn_getCommitteeList, failedAjaxFn_getCommitteeList, false);
        }

        succeededAjaxFn_getPositionListHistory = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getPositionListHistory: " + textStatus);

            result_getPositionList = JSON.parse(data.d);
            if (result_getPositionList.result == "Y") {

                pTable.clear().draw();
                $.each(result_getPositionList.positionlist, function (i, result) {
                    pTable.row.add([
                        i + 1,
                        result.GetSetcommittee_id + "<br/>" + result.GetSetcommittee_name + "<br/>" + result.GetSetcommittee_role,
                        result.GetSetcommittee_id,
                        result.GetSetcommittee_name,
                        result.GetSetcommittee_contact,
                        result.GetSetcreateddate,
                    ]).draw(false);

                });

            }
        }

        failedAjaxFn_getPositionListHistory = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getPositionListHistory: " + textStatus);
        }

        succeededAjaxFn_getCommitteeList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getCommitteeList: " + textStatus);

            result_getCommitteeList = JSON.parse(data.d);
            if (result_getCommitteeList.result == "Y") {

                pTable.clear().draw();
                $.each(result_getCommitteeList.commlist, function (i, result) {
                    pTable.row.add([
                        i + 1,
                        result.GetSetcommittee_id + "<br/>" + result.GetSetcommittee_name,
                        result.GetSetcommittee_id,
                        result.GetSetcommittee_name,
                        result.GetSetcommittee_contact,
                        result.GetSetcommittee_role + "<br/>" + result.GetSetcommittee_doa,
                    ]).draw(false);

                });

            }
        }

        failedAjaxFn_getCommitteeList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getCommitteeList: " + textStatus);
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
        openaddnewjawatankuasa();
        <%
        }
        %>

        $(document).ready(function () {
            $("div.toolbar").html('<select id="selNextPage" name="selNextPage" onchange="actionclick(\'NEXT_PAGE\');"><%for (int x = 1; x <= Math.Ceiling((double)lsCommitteeCount.Count / 10); x++)
        {%><option <%=sCurrPage.Equals(x.ToString())?"selected":""%> value="<%=x%>">Page <%=x%></option><%}%></select> / <% Response.Write(Math.Ceiling((double)lsCommitteeCount.Count / 10));%> Pages');
        });

        //Google chart in action 
        google.charts.load('current', { packages: ["orgchart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var pengerusi = [];
            var idpengerusi = [];
            var timbpengerusi = [];
            var idtimbpengerusi = [];
            var setiausaha = [];
            var idsetiausaha = [];
            var bendahari = [];
            var idbendahari = [];
            var ajk = [];
            var idajk = [];
        <%
        if (lsCommittee.Count > 0)
        {

            for (int i = 0; i < lsCommittee.Count; i++)
            {
                MainModel modJawatankuasa = (MainModel)lsCommittee[i];

                if (modJawatankuasa.GetSetcommittee_role.Equals("Pengerusi") && modJawatankuasa.GetSetcommittee_status.Equals("ACTIVE"))
                {%>
            pengerusi.push('<%=modJawatankuasa.GetSetcommittee_name%>');
            idpengerusi.push('<%=modJawatankuasa.GetSetcommittee_id%>');
                <%}
        else if (modJawatankuasa.GetSetcommittee_role.Equals("Timbalan Pengerusi") && modJawatankuasa.GetSetcommittee_status.Equals("ACTIVE"))
        {%>
            timbpengerusi.push('<%=modJawatankuasa.GetSetcommittee_name%>');
            idtimbpengerusi.push('<%=modJawatankuasa.GetSetcommittee_id%>');
                <%}
        else if (modJawatankuasa.GetSetcommittee_role.Equals("Setiausaha") && modJawatankuasa.GetSetcommittee_status.Equals("ACTIVE"))
        {%>
            setiausaha.push('<%=modJawatankuasa.GetSetcommittee_name%>');
            idsetiausaha.push('<%=modJawatankuasa.GetSetcommittee_id%>');
        <%} else if (modJawatankuasa.GetSetcommittee_role.Equals("Bendahari") && modJawatankuasa.GetSetcommittee_status.Equals("ACTIVE")) {%>
            bendahari.push('<%=modJawatankuasa.GetSetcommittee_name%>');
            idbendahari.push('<%=modJawatankuasa.GetSetcommittee_id%>');
        <%}else if (modJawatankuasa.GetSetcommittee_role.Equals("Ahli") && modJawatankuasa.GetSetcommittee_status.Equals("ACTIVE"))
                {%>
            ajk.push('<%=modJawatankuasa.GetSetcommittee_name%>');
            idajk.push('<%=modJawatankuasa.GetSetcommittee_id%>');
               <% }
            }
        }
             %>
            var hash = {};
            var temp = [];
            var populate = [];
            var tempname = '';

            for (i = 0; i < pengerusi.length; i++) {
                hash = {};
                temp = [];
                hash['f'] = pengerusi[i] + '<div onclick="openmodalrole(\'<%=sCurrComp %>\',\''+idpengerusi[i]+'\');"><a style="color:red; font-style:italic" href="#">Pengerusi</a></div>';
                hash['v'] = pengerusi[i];
                temp.push(hash);
                temp.push('');
                populate.push(temp);
                tempname = pengerusi[i];
            }

            for (i = 0; i < timbpengerusi.length; i++) {
                hash = {};
                temp = [];
                hash['f'] = timbpengerusi[i] + '<div onclick="openmodalrole(\'<%=sCurrComp %>\',\'' + idtimbpengerusi[i] +'\');"><a style="color:red; font-style:italic" href="#">Timbalan Pengerusi</a></div>';
                hash['v'] = timbpengerusi[i];
                temp.push(hash);
                temp.push(tempname);
                populate.push(temp);
                tempname = timbpengerusi[i];
            }

            for (i = 0; i < setiausaha.length; i++) {
                hash = {};
                temp = [];
                hash['f'] = setiausaha[i] + '<div onclick="openmodalrole(\'<%=sCurrComp %>\',\'' + idsetiausaha[i] +'\');"><a style="color:red; font-style:italic" href="#">Setiausaha</a></div>';
                hash['v'] = setiausaha[i];
                temp.push(hash);
                temp.push(tempname);
                populate.push(temp);
            }

            for (i = 0; i < bendahari.length; i++) {
                hash = {};
                temp = [];
                hash['f'] = bendahari[i] + '<div onclick="openmodalrole(\'<%=sCurrComp %>\',\'' + idbendahari[i] +'\');"><a style="color:red; font-style:italic" href="#">Bendahari</a></div>';
                hash['v'] = bendahari[i];
                temp.push(hash);
                temp.push(tempname);
                populate.push(temp);
            }

            for (i = 0; i < ajk.length; i++) {
                hash = {};
                temp = [];
                hash['f'] = ajk[i] + '<div onclick="openmodalrole(\'<%=sCurrComp %>\',\'' + idajk[i] +'\');"><a style="color:red; font-style:italic" href="#">Ahli</a></div>';
                hash['v'] = ajk[i];
                temp.push(hash);
                temp.push(tempname);
                populate.push(temp);
            }


            console.log(populate);
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');

            // For each orgchart box, provide the name, manager, and tooltip to show.
            data.addRows(populate);


            // Create the chart.
            var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));
            // Draw the chart, setting the allowHtml option to true for the tooltips.
            chart.draw(data, { 'allowHtml': true });
        }
    </script>
</asp:Content>

