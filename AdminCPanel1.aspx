<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AdminCPanel1.aspx.cs" Inherits="AdminCPanel1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-offset-2 col-md-offset-2">
        <div class="col-lg-10 col-md-10">
            <div>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Sistem BIOApp: Dashboard Pengguna</h2>
                        <ul class="nav navbar-right panel_toolbox">
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <form id="form1" runat="server">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form1">
                                    <label for="username">Nama:</label>
                                    <input type="text" id="username" class="form-control" name="username" value="<%=oUserProfile.GetSetusername %>" />
                                    <label for="userid">Id Pengguna/ Emel Id:</label>
                                    <input type="text" id="userid" class="form-control" name="userid" readonly="readonly" value="<%=oUserProfile.GetSetuserid %>" />
                                    <label for="userpassword">Katalaluan/ Password:</label>
                                    <input type="password" id="userpassword" class="form-control" name="userpassword" value="<%=oUserProfile.GetSetuserpwd %>" />
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="useradd">Alamat:</label>
                                    <textarea id="useradd" class="form-control" rows="4" style="resize: none" name="useradd"><%=oUserProfile.GetSetuseradd%></textarea>
                                    <label for="usertelno">No. Telefon:</label>
                                    <input type="text" id="usertelno" class="form-control" required="required" name="usertelno" value="<%=oUserProfile.GetSetusertelno%>" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <section class="panel">
                                <div class="panel-body">
                                    <div id="action-form">
                                        <%
                                            if (sAction.Equals("OPEN"))
                                            {
                                        %>
                                        <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');">Kemaskini</button>
                                        <%
                                            }
                                            else if (sAction.Equals("EDIT"))
                                            {
                                        %>
                                        <button id="btnSave" name="btnSave" type="button" class="btn btn-primary" onclick="actionclick('SAVE');">Simpan</button>
                                        <button id="btnBack" name="btnBack" type="button" class="btn btn-warning" onclick="actionclick('OPEN');">Kembali</button>
                                        <%
                                            }
                                        %>
                                        <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="actionclick('LOGOUT');">Log Keluar</button>
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
                                            <input type="hidden" name="hidUserId" id="hidUserId" value="<%=sUserId %>" />
                                            <input type="hidden" name="hidUserComp" id="hidUserComp" value="<%=sUserComp %>" />
                                            <input type="hidden" name="hidLineNo1" id="hidLineNo1" value="" />
                                            <input type="hidden" name="hidLineNo2" id="hidLineNo2" value="" />
                                        </div>
                                    </div>
                                </div>
                            </section>

                            <!--BEGIN dialog box for add new company-->
                            <div class="modal fade modal-add-new-company" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Tambah Syarikat</h4>
                                        </div>
                                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <label class="control-label">Kod Syarikat <span class="required">*</span></label>
                                                <input id="compcode" name="compcode" type="text" class="form-control" value="" />
                                                <label class="control-label">Nama Syarikat <span class="required">*</span></label>
                                                <input id="compname" name="compname" type="text" class="form-control" value="" />
                                                <label class="control-label">Id Syarikat  <span class="required">*</span></label>
                                                <input id="compid" name="compid" type="text" class="form-control" value="" />
                                                <label class="control-label">Alamat Syarikat <span class="required">*</span></label>
                                                <textarea id="compaddress" class="form-control" rows="2" name="compaddress"></textarea>
                                                <label class="control-label">Nama Pegawai <span class="required">*</span></label>
                                                <input id="compcontact" name="compcontact" type="text" class="form-control" value="" />
                                                <label class="control-label">Telefon No <span class="required">*</span></label>
                                                <input id="compcontactno" name="compcontactno" type="text" class="form-control" value="" />
                                                <label class="control-label">Bank Didaftarkan </label>
                                                <input id="compaccountbank" name="compaccountbank" type="text" class="form-control" value="" />
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <label class="control-label">No. Akaun Bank </label>
                                                <input id="compaccountno" name="compaccountno" type="text" class="form-control" value="" />
                                                <label class="control-label">Web Site </label>
                                                <input id="compwebsite" name="compwebsite" type="text" class="form-control" value="" />
                                                <label class="control-label">Emel </label>
                                                <input id="compemail" name="compemail" type="text" class="form-control" value="" />
                                                <label class="control-label">Icon Syarikat </label>
                                                <input id="compicon" name="compicon" type="text" class="form-control" value="" />
                                                <label class="control-label">Logo(1) Syarikat </label>
                                                <input id="complogo1" name="complogo1" type="text" class="form-control" value="" />
                                                <label class="control-label">Logo(2) Syarikat </label>
                                                <input id="complogo2" name="complogo2" type="text" class="form-control" value="" />
                                                <label class="control-label">Status <span class="required">*</span></label>
                                                <select id="compstatus" name="compstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                                    <option value="ACTIVE">AKTIF</option>
                                                    <option value="INACTIVE">TIDAK AKTIF</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="modal-footer" style="text-align:left;">
                                            <button type="button" class="btn btn-primary" id="btnEditComp" onclick="actionclick('UPDATE');">Kemaskini</button>
                                            <button type="button" class="btn btn-primary" id="btnSaveComp" onclick="actionclick('STORE');">Simpan</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="actionclick('CLOSE')";>Tutup</button>
                                            
                                            <div id="paneladduser" class="x_panel">
                                                <a id="adduser" name="adduser" class="btn btn-app" data-toggle="modal" data-target=".modal-add-new-user" onclick="openadduser();">
                                                    <i class="fa fa-plus-square green"></i>Tambah Pengguna
                                                </a>
                                                <table id="datatable" class="table">
                                                    <thead>
                                                        <tr>
                                                            <th></th>
                                                            <th>Id Pengguna</th>
                                                            <th>Nama Pengguna</th>
                                                            <th>Jenis</th>
                                                            <th>Role Id</th>
                                                            <th>Status</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                                           
                                </div>
                            </div>
                            <!--END dialog box for add new company-->

                            <!--BEGIN dialog box for add new user-->
                            <div class="modal fade modal-add-new-user" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <label class="control-label">Pengguna <span class="required">*</span></label>
                                            <select id="adduserid" name="adduserid" class="form-control" tabindex="-1" style="width: 100%;">
                                                <option value="">-select-</option>
                                            </select>
                                            <label class="control-label">Role Id <span class="required">*</span></label>
                                            <select id="adduserroleid" name="adduserroleid" class="form-control" tabindex="-1" style="width: 100%;">
                                                <option value="02" selected="selected">02</option>
                                                <option value="05">05</option>
                                            </select>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-primary" id="btnAddUser" onclick="actionclick('ADD_USER');">Tambah Pengguna</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for add new user-->

                            <!--BEGIN dialog box for confirm delete-->
                            <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Anda pasti untuk Keluarkan Pengguna ini?</h4>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-warning" id="btnDeleteLineItem" data-dismiss="modal" onclick="actionclick('DELETE');">Ya</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for confirm delete-->

                        </div>
                    </form>
                    <div class="" role="tabpanel" data-example-id="togglable-tabs">
                        <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                            <li role="presentation" class="active">
                                <a href="#" id="comp-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('COMP_LISTING');">Senarai Syarikat</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="user-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('USER_LISTING');">Senarai Pengguna</a>
                            </li>
                        </ul>
                        <div id="myTabContent" class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="home-tab">                    
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <a id="addcomp" name="addcomp" class="btn btn-app" data-toggle="modal" data-target=".modal-add-new-company" onclick="openaddcomp();">
                                            <i class="fa fa-plus-square green"></i>Tambah Syarikat
                                        </a>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">

                                        <table id="datatable2" class="table">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Kod Syarikat</th>
                                                <th>Nama Syarikat</th>
                                                <th>Id Syarikat</th>
                                                <th>Status</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        </table>

                                    </div>

                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        //New method to connect to Application Server
        function PageMethod(fn, paramArray, successFn, errorFn, asyncFn) {
            var pagePath = window.location.pathname;
            var paramList = '';

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }
            paramList = '{' + paramList + '}';
            //Call the page method
            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: successFn,
                error: errorFn,
                timeout: 600000,
                async: asyncFn
            });
        }


    </script>

    <script type="text/javascript">

    var selectedComp = "";
    var storeAction = "";

    $(document).ready(function () {
        getCompList();

    });

    function getCompList() {
        //$('#datatable-buttons').dataTable().clear();
        $('#datatable2').dataTable().fnClearTable();

        var parameters = ["userid", "<%=sUserId%>"];
            PageMethod("getCompListObject", parameters, succeededCompListObject, failedCompListObject, false);
        }

        succeededCompListObject = function (data, textStatus, jqXHR) {
            console.log("succeededCompListObject: " + textStatus);
            resultJSON = JSON.parse(data.d);


            if (resultJSON.result == "Y") {
                var t = $('#datatable2').DataTable();
                $.each(resultJSON.complist, function (i, result) {
                    //$('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>').appendTo('#datatable');
                    //$('#datatable > tbody').append('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>');
                    t.row.add([
                            '<a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a>',
                            result.comp,
                            result.comp_name,
                            result.comp_id,
                            (result.status == 'ACTIVE'?'AKTIF':'TIDAK AKTIF'),
                            (result.comp == 'T01' ? '' : '<a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a>')
                    ]).draw(false);
                });
            }
            else {
                alert("Tiada Rekod!");
            }
        };

        failedCompListObject = function (jqXHR, textStatus, errorThrown) {
            console.log("failedCompListObject: " + textStatus);
            alert(textStatus);

        }

        function actionclick(action) {
            var proceed = true;

            if (action == 'EDIT') {
                enabledisableinputform(false);
            }
            if (action == 'SAVE') {
                enabledisableinputform(false);
            }
            if (action == 'LOGOUT') {
                window.location.href = "HiddenEvent.aspx?event=logout";
                proceed = false;
            }
            if (action == 'COMP_LISTING') {
                window.location.href = "AdminCPanel1.aspx?action=OPEN";
                proceed = false;
            }
            if (action == 'USER_LISTING') {
                window.location.href = "AdminCPanel2.aspx?action=OPEN";
                proceed = false;
            }
            if (action == 'UPDATE') {
                proceed = false;
                enabledisableinforform(false);
                selectedComp = $('#compcode').val();

                $('#compcode').prop('disabled', true);
                $('#btnEditComp').prop('disabled', true);
                $('#btnSaveComp').prop('disabled', false);

            }
            if (action == 'STORE') {
                proceed = false;
                if ($('#compcode').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Kod Syarikat!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compname').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Nama Syarikat!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compid').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Id Syarikat (No pendaftaran Syarikat)"!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compaddress').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Alamat Syarikat!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compcontact').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Nama Pegawai!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compcontactno').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi No. Telefon!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#compstatus').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila pilih Status!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else {
                    if ($('#compcode').val() == selectedComp) {
                        storeAction = "U";
                        var parameters = ["flag", storeAction,
                                          "comp_code", $('#compcode').val(),
                                          "comp_name", $('#compname').val(),
                                          "comp_id", $('#compid').val(),
                                          "comp_accountbank", $('#compaccountbank').val(),
                                          "comp_accountno", $('#compaccountno').val(),
                                          "comp_address", $('#compaddress').val(),
                                          "comp_contact", $('#compcontact').val(),
                                          "comp_contactno", $('#compcontactno').val(),
                                          "comp_website", $('#compwebsite').val(),
                                          "comp_email", $('#compemail').val(),
                                          "comp_icon", $('#compicon').val(),
                                          "comp_logo1", $('#complogo1').val(),
                                          "comp_logo2", $('#complogo2').val(),
                                          "status", $('#compstatus').val(),
                                          "createdby", "<%=sUserId%>",
                                          "confirmedby", "<%=sUserId%>"];
                        PageMethod("putCompObject", parameters, succeededPutCompObject, failedPutCompObject, false);

                    }
                    else {
                        storeAction = "I";
                        var parameters = ["flag", storeAction,
                                          "comp_code", $('#compcode').val(),
                                          "comp_name", $('#compname').val(),
                                          "comp_id", $('#compid').val(),
                                          "comp_accountbank", $('#compaccountbank').val(),
                                          "comp_accountno", $('#compaccountno').val(),
                                          "comp_address", $('#compaddress').val(),
                                          "comp_contact", $('#compcontact').val(),
                                          "comp_contactno", $('#compcontactno').val(),
                                          "comp_website", $('#compwebsite').val(),
                                          "comp_email", $('#compemail').val(),
                                          "comp_icon", $('#compicon').val(),
                                          "comp_logo1", $('#complogo1').val(),
                                          "comp_logo2", $('#complogo2').val(),
                                          "status", $('#compstatus').val(),
                                          "createdby", "<%=sUserId%>",
                                          "confirmedby", "<%=sUserId%>"];
                        PageMethod("putCompObject", parameters, succeededPutCompObject, failedPutCompObject, false);
                    }
                }
        }
        if (action == 'CLOSE') {
            proceed = false;
        }

        if (action == 'ADD_USER') {
            proceed = false;

            var parameters = ["comp", $('#compcode').val(), "userid", $('#adduserid').val(), "roleid", $('#adduserroleid').val()];
            PageMethod("insertUserRoleObject", parameters, succeededInsertUserRoleObject, failedInsertUserRoleObject, false);
        }

        if (action == 'DELETE') {
            proceed = false;

            var parameters = ["comp", $('#hidLineNo1').val(), "userid", $('#hidLineNo2').val()];
            PageMethod("delUserObject", parameters, succeededDelUserObject, failedDelUserObject, false);
        }

        if (proceed) {
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }
    }

    succeededPutCompObject = function (data, textStatus, jqXHR) {
    console.log("succeededPutCompObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        selectedComp = $('#compcode').val();
        enabledisableinforform(true);
        $('#btnEditComp').prop('disabled', false);
        $('#btnSaveComp').prop('disabled', true);

        getCompList();

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", $('#compcode').val(), "userid", ""];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);

        if (storeAction == "I") {
            alert("Penambahan Syarikat Berjaya!");
        }
        else if (storeAction == "U") {
            alert("Kemaskini Syarikat Berjaya!");
        }
        showhidepaneladduser("block");
    }
    else {

        enabledisableinforform(false);
        $('#btnEditComp').prop('disabled', true);
        $('#btnSaveComp').prop('disabled', false);

        if (storeAction == "I") {
            alert("Penambahan Syarikat Tidak Berjaya!");
        }
        else if (storeAction == "U") {
            $('#compcode').prop('disabled', true);
            alert("Kemaskini Syarikat Tidak Berjaya!");
        }
    }
};

failedPutCompObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedPutCompObject: " + textStatus);

    enabledisableinforform(false);
    $('#btnEditComp').prop('disabled', true);
    $('#btnSaveComp').prop('disabled', false);

    if (storeAction == "I") {
        alert("Penambahan Syarikat Tidak Berjaya!");
    }
    else if (storeAction == "U") {
        $('#compcode').prop('disabled', true);
        alert("Kemaskini Syarikat Tidak Berjaya!");
    }
}

succeededInsertUserRoleObject = function (data, textStatus, jqXHR) {
    console.log("succeededInsertUserRoleObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        alert("Pengguna Berjaya Dimasukkan!");

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", $('#compcode').val(), "userid", ""];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else if (resultJSON.result == "N") {
        alert("Pengguna Tidak Berjaya Dimasukkan! Sila berhubung dengan System Administrator");
    }
};

failedInsertUserRoleObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedInsertUserRoleObject: " + textStatus);
    alert(textStatus);
}

succeededDelUserObject = function (data, textStatus, jqXHR) {
    console.log("succeededDelUserObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        alert("Pengguna Berjaya Dikeluarkan!");

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", $('#compcode').val(), "userid", ""];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else if (resultJSON.result == "N") {
        alert("Pengguna Tidak Berjaya Dikeluarkan! Sila berhubung dengan System Administrator");
    }
};

failedDelUserObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedDelUserObject: " + textStatus);
    alert(textStatus);
}

function openaddcomp() {
    enabledisableinforform(false);
    selectedComp = "";
    $('#compcode').prop('disabled', false);
    $('#compcode').val("");
    $('#compname').val("");
    $('#compid').val("");
    $('#compaddress').val("");
    $('#compcontact').val("");
    $('#compcontactno').val("");
    $('#compaccountbank').val("");
    $('#compaccountno').val("");
    $('#compwebsite').val("");
    $('#compemail').val("");
    $('#compicon').val("");
    $('#complogo1').val("");
    $('#complogo2').val("");
    $('#compstatus').val("ACTIVE");
    $('#btnEditComp').prop('disabled', true);
    $('#btnSaveComp').prop('disabled', false);
    showhidepaneladduser("none");
}

function showhidepaneladduser(flag) {
    var x = document.getElementById("paneladduser");
    x.style.display = flag;
}
function enabledisableinforform(flag) {
    $('#compcode').prop('disabled', flag);
    $('#compname').prop('disabled', flag);
    $('#compid').prop('disabled', flag);
    $('#compaddress').prop('disabled', flag);
    $('#compcontact').prop('disabled', flag);
    $('#compcontactno').prop('disabled', flag);
    $('#compaccountbank').prop('disabled', flag);
    $('#compaccountno').prop('disabled', flag);
    $('#compwebsite').prop('disabled', flag);
    $('#compemail').prop('disabled', flag);
    $('#compicon').prop('disabled', flag);
    $('#complogo1').prop('disabled', flag);
    $('#complogo2').prop('disabled', flag);
    $('#compstatus').prop('disabled', flag);
}

function openeditcomp(compcode) {
    enabledisableinforform(true);
    
    var parameters = ["compcode", compcode];
    PageMethod("getCompObject", parameters, succeededCompObject, failedCompObject, false);
}

succeededCompObject = function (data, textStatus, jqXHR) {
    console.log("succeededCompObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        $('#compcode').val(resultJSON.compinfo.comp);
        $('#compname').val(resultJSON.compinfo.comp_name);
        $('#compid').val(resultJSON.compinfo.comp_id);
        $('#compaddress').val(resultJSON.compinfo.comp_address);
        $('#compcontact').val(resultJSON.compinfo.comp_contact);
        $('#compcontactno').val(resultJSON.compinfo.comp_contactno);
        $('#compaccountbank').val(resultJSON.compinfo.comp_accountbank);
        $('#compaccountno').val(resultJSON.compinfo.comp_accountno);
        $('#compwebsite').val(resultJSON.compinfo.comp_website);
        $('#compemail').val(resultJSON.compinfo.comp_email);
        $('#compicon').val(resultJSON.compinfo.comp_icon);
        $('#complogo1').val(resultJSON.compinfo.comp_logo1);
        $('#complogo2').val(resultJSON.compinfo.comp_logo2);
        $('#compstatus').val(resultJSON.compinfo.status);
        $('#btnEditComp').prop('disabled', false);
        $('#btnSaveComp').prop('disabled', true);
        selectedComp = $('#compcode').val();
        showhidepaneladduser("block");

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", $('#compcode').val(), "userid", ""];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else {
        alert("Tiada Rekod!");
        $('#btnEditComp').prop('disabled', true);
        $('#btnSaveComp').prop('disabled', true);
    }
};

failedCompObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedCompObject: " + textStatus);
    alert(textStatus);
    $('#btnEditComp').prop('disabled', true);
    $('#btnSaveComp').prop('disabled', true);
}

succeededCompUserListObject = function (data, textStatus, jqXHR) {
    console.log("succeededCompUserListObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        var t = $('#datatable').DataTable();
        $.each(resultJSON.compuserlist, function (i, result) {
            //$('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>').appendTo('#datatable');
            //$('#datatable > tbody').append('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>');
            if (result.userid != "sysadmin" || "<%=sUserId%>" == "sysadmin") {
                t.row.add([
                        '<a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a>',
                        result.userid,
                        result.username,
                        (result.usertype == '01' ? 'System Admin' : result.usertype == '02' ? 'Operasi' : 'Lain-lain'),
                        result.roleid,
                        (result.status == 'A' ? 'AKTIF' : 'TIDAK AKTIF'),
                        '<a href="#" class="btn btn-warning btn-xs" onclick="confirmdeletelineitem(' + '\'' + result.comp + '\'' + ',' + '\'' + result.userid + '\'' + ');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-pencil"></i> Hapus </a>'
                ]).draw(false);
            }
        });
    }
};

failedCompUserListObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedCompUserListObject: " + textStatus);
    alert(textStatus);
}

function enabledisableinputform(flag) {
    $('#username').prop('disabled', flag);
    $('#userpassword').prop('disabled', flag);
    $('#useradd').prop('disabled', flag);
    $('#usertelno').prop('disabled', flag);
}

        <%
        if (sAction.Equals("EDIT"))
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

        function confirmdeletelineitem(comp,userid) {
            $('#hidLineNo1').val(comp);
            $('#hidLineNo2').val(userid);
        }

        function openadduser() {
            var parameters = ["userid", "<%=sUserId%>"];
            PageMethod("getUserListObject", parameters, succeededUserListObject, failedUserListObject, false);
        }

        succeededUserListObject = function (data, textStatus, jqXHR) {
            console.log("succeededUserListObject: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                //var t = $('#datatable').DataTable();
                $.each(resultJSON.userlist, function (i, result) {
                    $('#adduserid').append('<option value=' + result.userid + '>' + result.userid + ' - ' + result.username + '</option>');
                });
            }
        };

        failedUserListObject = function (jqXHR, textStatus, errorThrown) {
            console.log("failedUserListObject: " + textStatus);
            alert(textStatus);
        }

    </script>

</asp:Content>

