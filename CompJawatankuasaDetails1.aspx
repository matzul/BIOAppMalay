<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompJawatankuasaDetails1.aspx.cs" Inherits="CompJawatankuasaDetails1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.19/css/dataTables.bootstrap.min.css"/>
    <script src="https://adminlte.io/themes/AdminLTE/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="js/WebMethodService.Call.Helper.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <label for="compId">Comp:</label>
                            <input type="text" id="compCode" class="form-control" name="compCode" value="<%=oModComp.GetSetcomp %>" required="required" readonly="readonly" />
                            <label for="compName">Nama/ Keterangan:</label>
                            <input type="text" id="compName" class="form-control" name="compName" value="<%=oModComp.GetSetcomp_name %>" required="required"  readonly="readonly"/>
                            <label for="committeeid">No. ID:</label>
                            <input type="text" id="committeeid" class="form-control" name="committeeid" value="<%=oModCommittee.GetSetcommittee_id %>" required="required" />
                            <label for="committeename">Nama:</label>
                            <input type="text" id="committeename" class="form-control" name="committeename" value="<%=oModCommittee.GetSetcommittee_name %>" required="required" />
                            <label for="committeecontact">Telefon No:</label>
                            <input type="text" id="committeecontact" class="form-control" name="committeecontact" required="required" value="<%=oModCommittee.GetSetcommittee_contact %>" />
                            <label for="committeedob">Tarikh Lahir:</label>
                            <input type="date" id="committeedob" class="form-control" name="committeedob" required="required" value="<%=oModCommittee.GetSetcommittee_dob %>" />
                            <label for="committeeage">Umur:</label>
                            <input type="text" id="committeeage" class="form-control" name="committeeage" required="required" value="<%=oModCommittee.GetSetcommittee_age %>" />
                            <label for="committeejob">Pekerjaan:</label>
                            <input type="text" id="committeejob" class="form-control" name="committeejob" required="required" value="<%=oModCommittee.GetSetcommittee_job %>" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="committeeaddress">Alamat:</label>
                            <textarea id="committeeaddress" class="form-control" rows="3" name="committeeaddress" required="required"><%=oModCommittee.GetSetcommittee_address %></textarea>
                            <label for="committeerole">Jawatan:</label>
                            <select class="form-control" id="committeerole" name="committeerole" required="required">
                                <option value="">-select-</option>
                                <option value="Pengerusi" <%=oModCommittee.GetSetcommittee_role.Equals("Pengerusi")?"selected":"" %>>Pengerusi</option>
                                <option value="Timbalan Pengerusi" <%=oModCommittee.GetSetcommittee_role.Equals("Timbalan Pengerusi")?"selected":"" %>>Timbalan Pengerusi</option>
                                <option value="Setiausaha" <%=oModCommittee.GetSetcommittee_role.Equals("Setiausaha")?"selected":"" %>>Setiausaha</option>
                                <option value="Bendahari" <%=oModCommittee.GetSetcommittee_role.Equals("Bendahari")?"selected":"" %>>Bendahari</option>
                                <option value="Ahli" <%=oModCommittee.GetSetcommittee_role.Equals("Ahli")?"selected":"" %>>Ahli</option>
                            </select>
                            <!-- DOA = date of appointment -->
                            <label for="committeedoa">Tarikh Lantikan:</label>
                            <input type="date" id="committeedoa" class="form-control" name="committeedoa" required="required" value="<%=oModCommittee.GetSetcommittee_doa %>" />
                            
                            <label for="committeeappointmentby">Lantikan Oleh:</label>
                            <select class="form-control" id="committeeappointmentby" name="committeeappointmentby" required="required">
                                <option value="Pengerusi" <%=oModCommittee.GetSetcommittee_appointmentby.Equals("Pengerusi")?"selected":"" %>>Pengerusi</option>
                                <option value="PBT" <%=oModCommittee.GetSetcommittee_appointmentby.Equals("PBT")?"selected":"" %>>PBT</option>
                                <option value="Lain-Lain" <%=oModCommittee.GetSetcommittee_appointmentby.Equals("Lain-Lain")?"selected":"" %>>Lain-Lain</option>
                            </select>
                            <label for="committeestatus">Status:</label>
                            <select class="form-control" id="committeestatus" name="committeestatus" required="required">
                                <option value="">-select-</option>
                                <option value="ACTIVE" <%=oModCommittee.GetSetcommittee_status.Equals("ACTIVE")?"selected":"" %>>AKTIF</option>
                                <option value="IN-ACTIVE" <%=oModCommittee.GetSetcommittee_status.Equals("IN-ACTIVE")?"selected":"" %>>TIDAK AKTIF</option>
                            </select>
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
                                <button id="btnCreate" name="btnCreate" type="button" class="btn btn-info" onclick="actionclick('EXCHANGE')">Daftar</button>
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
                                <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="actionclick('CLOSE');">Tutup</button>
                                <%
                                    MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
                                    if (oAlerMssg.GetSetalertstatus.Equals("SUCCESS"))
                                    {
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
                                    <input type="hidden" name="hidCompId" id="hidCompId" value="<%=sCompId %>" />
                                    <input type="hidden" name="hidUserAction" id="hidUserAction" value="<%=sUserAction %>" />
                                    <input type="hidden" name="hidLineNo" id="hidLineNo" value="" />
                                    <input type="hidden" name="hidLineNo1" id="hidLineNo1" value="" />
                                    <input type="hidden" name="exchangeid" id="exchangeid" />
                                </div>
                            </div>
                        </div>
                    </section>
                    
                    <!--BEGIN dialog box for confirm delete-->
                    <div class="modal fade modal-delete-current-user" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title">Anda pasti untuk keluar pengguna ini?</h4>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-warning" id="btnDeleteLineItem" data-dismiss="modal" onclick="actionclick('DELETE');">Ya</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm delete-->
                     <!--BEGIN dialog box for confirm position-->
                    <div class="modal fade modal-position-user" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title">Pengesahan Pendaftaran Jawatan!</h4>
                                </div>
                                <div class="modal-body ">
                                    <table id="tbPositionList" class="table" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Butiran Pegawai</th>
                                                <th style="display: none;">desc</th>
                                                <th style="display: none;">address</th>
                                                <th style="display: none;">telno</th>
                                                <th class="text-center"><i class="fa fa-cog" aria-hidden="true"></i></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" id="addposition" name="addposition">Tambah Baru</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm position-->
                </div>
            </form>
             <%
                if (sAction.Equals("OPEN") || sAction.Equals("EDIT"))
                {
            %>
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="home-tab">                    
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <a id="addcurrentuser" name="addcurrentuser" class="btn btn-app" data-toggle="modal" data-target=".modal-delete-current-user" 
                            onclick="confirmdeleteuser('<%=oModCommittee.GetSetcommittee_id %>','<%=oModComp.GetSetcomp %>');">
                            <i class="fa fa-minus-square red"></i>Hapus Pengguna
                        </a>
                    </div>
                </div>
            </div>
            <%
                }
            %>
        </div>
    </div>

    <div class="modal fade" id="myCommList" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            <table id="tbCommList" class="table" style="width:100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>No ID</th>
                                        <th style="display:none;">desc</th>
                                        <th style="display:none;">address</th>
                                        <th style="display:none;">telno</th>
                                        <th>Senarai Jawatankuasa</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <br/>
                        <br/>
                    </div>
                </div>
            </div>

    <script type="text/javascript">        
        var check = 0;

        $(document).ready(function () {
            $("#committeeid").blur(function () {
               // alert($('#committeeid').val());
                var parameters_getCommitteeList = ["commid", $('#committeeid').val()];
                PageMethod("getCommitteeList", parameters_getCommitteeList, succeededAjaxFn_getCommitteeList, failedAjaxFn_getCommitteeList, false);
            });
        });

        succeededAjaxFn_getCommitteeList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getCommitteeList: " + textStatus);

            result_getCommitteeList = JSON.parse(data.d);
            if (result_getCommitteeList.commlist.length > 0) {
                if (result_getCommitteeList.result == "Y") {
                    $('#myCommList').modal('show');

                    bpTable.clear().draw();
                    $.each(result_getCommitteeList.commlist, function (i, result) {
                        bpTable.row.add([
                            i + 1,
                            result.GetSetcommittee_id,
                            result.GetSetcommittee_name,
                            result.GetSetcommittee_address,
                            result.GetSetcommittee_contact,
                            result.GetSetcommittee_name + '<br/>' + result.GetSetcommittee_address
                        ]).draw(false);
                    });
                }
            } else {
                //alert("Tiada data ditemui");
            }

        }

        failedAjaxFn_getCommitteeList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getCommitteeList: " + textStatus);
        }

        bpTable = $('#tbCommList').DataTable({
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

        $('#tbCommList tbody').on('click', 'tr', function () {
            var data = bpTable.row(this).data();
            selectedbpid = data[1];
            //alert(selectedbpid);
            $('#myCommList').modal('hide');
            //get info from business partner
            var parameters_getCommitteeDetails = ["commid", selectedbpid];
            PageMethod("getCommitteeDetails", parameters_getCommitteeDetails, succeededAjaxFn_getCommitteeDetails, failedAjaxFn_getCommitteeDetails, false);
        
        });

        succeededAjaxFn_getCommitteeDetails = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getCommitteeDetails: " + textStatus);
            result_getCommitteeDetails = JSON.parse(data.d);
            if (result_getCommitteeDetails.result == "Y") {
                if (result_getCommitteeDetails.commdetails.GetSetcommittee_id != "") {
                    populatecommdetails(result_getCommitteeDetails.commdetails.GetSetcommittee_id, result_getCommitteeDetails.commdetails.GetSetcommittee_name, result_getCommitteeDetails.commdetails.GetSetcommittee_address,
                        result_getCommitteeDetails.commdetails.GetSetcommittee_contact, result_getCommitteeDetails.commdetails.GetSetcommittee_dob, result_getCommitteeDetails.commdetails.GetSetcommittee_age,
                        result_getCommitteeDetails.commdetails.GetSetcommittee_job, result_getCommitteeDetails.commdetails.GetSetcommittee_role, result_getCommitteeDetails.commdetails.GetSetcommittee_doa);
                } else {
                    alert("Internal Server Error!")
                }
            }
        }

        failedAjaxFn_getCommitteeDetails = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getCommitteeDetails: " + textStatus);
        }

        function populatecommdetails(id, desc, address, telno, dob, age, job, role, doa) {
            $("#committeeid").val(id);
            $("#committeename").val(desc);
            $("#committeeaddress").val(address);
            $("#committeecontact").val(telno);
            $("#committeedob").val(dob);
            $("#committeeage").val(age);
            $("#committeejob").val(job);
            $("#committeerole").val(role);
            $("#committeedoa").val(doa);
        }

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

        function confirmdeleteuser(committeeid, comp) {
            $('#hidLineNo').val(committeeid);
            $('#hidLineNo1').val(comp);
        }

        function actionclick(action) {

            if (action == 'ADD') {
                check = 1;
                $('#compCode').removeAttr('required');
                $('#compName').removeAttr('required');
                $('#committeename').removeAttr('required');
                $('#committeeid').removeAttr('required');
                $('#committeecontact').removeAttr('required');
                $('#committeeaddress').removeAttr('required');
                $('#committeerole').removeAttr('required');
                $('#committeestatus').removeAttr('required');
                $('#committeedob').removeAttr('required');
                $('#committeeage').removeAttr('required');
                $('#prevcommitteerole').removeAttr('required');
                $('#committeedoa').removeAttr('required');
                $('#committeeappointmentby').removeAttr('required');
                $('#committeecertno').removeAttr('required');
                $('#committeejob').removeAttr('required');

            } else if (action == 'CLOSE') {
                check = 1;
                window.close();
                window.opener.location.reload();
            } else if (action == 'EXCHANGE') {
                //check = 1;

                var compCode = document.getElementById("compCode");
                var compName = document.getElementById("compName");
                var committeename = document.getElementById("committeename");
                var committeeid = document.getElementById("committeeid");
                var committeecontact = document.getElementById("committeecontact");
                var committeeaddress = document.getElementById("committeeaddress");
                var committeerole = document.getElementById("committeerole");
                var committeestatus = document.getElementById("committeestatus");
                var committeedob = document.getElementById("committeedob");
                var committeejob = document.getElementById("committeejob");
                var committeeage = document.getElementById("committeeage");
                var prevcommitteerole = document.getElementById("prevcommitteerole");
                var committeedoa = document.getElementById("committeedoa");
                var committeeappointmentby = document.getElementById("committeeappointmentby");

                if (committeeid.checkValidity() && committeename.checkValidity() && committeecontact.checkValidity() && committeecontact.checkValidity()
                    && committeeaddress.checkValidity() && committeerole.checkValidity() && committeestatus.checkValidity() && committeedob.checkValidity()
                    && committeeage.checkValidity() && committeedoa.checkValidity() && committeeappointmentby.checkValidity()
                    && committeejob.checkValidity()) {

                    positionchecking();
                }

            } else if (action == 'DELETE' || action == 'EDIT' || action == 'SAVE' || action == 'CREATE') {
                check = 1;
            }

            if (check != 0) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }


            
        }
        function enabledisableinputform(flag) {
            $('#committeename').prop('disabled', flag);
            $('#committeeid').prop('disabled', flag);
            $('#compContact').prop('disabled', flag);
            $('#committeecontact').prop('disabled', flag);
            $('#committeeaddress').prop('disabled', flag);
            $('#committeerole').prop('disabled', flag);
            $('#committeestatus').prop('disabled', flag);
            $('#committeedob').prop('disabled', flag);
            $('#committeeage').prop('disabled', flag);
            $('#prevcommitteerole').prop('disabled', flag);
            $('#committeedoa').prop('disabled', flag);
            $('#committeeappointmentby').prop('disabled', flag);
            $('#committeecertno').prop('disabled', flag);
            $('#committeejob').prop('disabled', flag);
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

        $('#tbPositionList tbody').on('click', 'tr', function () {
            var data = pTable.row(this).data();

            selectedbpid = data[2];
            selectedname = data[3];

            $("#exchangeid").val(selectedbpid);
            //updatecommittee(selectedbpid, selectedname);
        });

        function positionchecking() {
            //alert($('#committeerole').val());
            var parameters_getPositionList = ["compId", "<%=sCompId%>", "committeeid", "", "committeerole", $('#committeerole').val(), "committeetype", "JK_COMP"];
            PageMethod("getPositionList", parameters_getPositionList, succeededAjaxFn_getPositionList, failedAjaxFn_getPositionList, false);
        }

        succeededAjaxFn_getPositionList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getPositionList: " + textStatus);
            
            result_getPositionList = JSON.parse(data.d);
            if (result_getPositionList.positionlist.length > 0) {
                if (result_getPositionList.result == "Y") {
                    $(".modal-position-user").modal("show");

                    pTable.clear().draw();
                    $.each(result_getPositionList.positionlist, function (i, result) {
                        pTable.row.add([
                            i + 1,
                            result.GetSetcommittee_id + "<br/>" + result.GetSetcommittee_name,
                            result.GetSetcommittee_id,
                            result.GetSetcommittee_name,
                            result.GetSetcommittee_contact,
                            result.GetSetcommittee_role
                        ]).draw(false);
                        idx = idx + 1;
                    });
                } else {
                    alert("System Error! Please contact System Administrator");
                }
            } else {
                actionclick("CREATE");
            }
        }

        failedAjaxFn_getPositionList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getPositionList: " + textStatus);
        }

        function updatecommittee(bpid, name) {
            if (confirm("Adakah anda pasti menggantikan " + name + " ?")) {
                $(".modal-position-user").modal("hide");
            }
        }

        $("#addposition").click(function () {
            actionclick("CREATE");
        });
    </script>
</asp:Content>
