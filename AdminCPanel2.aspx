<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AdminCPanel2.aspx.cs" Inherits="AdminCPanel2" %>

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
                                    <label for="usertype">Jenis Pengguna:</label>
                                    <select id="usertype" name="usertype" class="form-control">
                                        <option value=''>-Select-</option>
                                        <option value='01' <%=oUserProfile.GetSetusertype.Equals("01")?"selected":""%>>01 - Super User</option>
                                        <option value='02' <%=oUserProfile.GetSetusertype.Equals("02")?"selected":""%>>02 - Normal User</option>
                                    </select>
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

                            <!--BEGIN dialog box for add new user-->
                            <div class="modal fade modal-add-new-user" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Tambah Pengguna</h4>
                                        </div>
                                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <label class="control-label">Nama Pengguna <span class="required">*</span></label>
                                                <input id="addusername" name="addusername" type="text" class="form-control" value="" />
                                                <label class="control-label">Alamat Syarikat </label>
                                                <textarea id="adduseraddress" class="form-control" rows="2" name="adduseraddress"></textarea>
                                                <label class="control-label">No. Telefon <span class="required">*</span></label>
                                                <input id="addusercontactno" name="addusercontactno" type="text" class="form-control" value="" />
                                                <label class="control-label">Jenis Pengguna <span class="required">*</span></label>
                                                <select id="addusertype" name="addusertype" class="form-control">
                                                    <option value=''>-Select-</option>
                                                    <option value='01'>01 - Super User</option>
                                                    <option value='02'>02 - Normal User</option>
                                                </select>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                                <label class="control-label">Email (Id Pengguna) <span class="required">*</span></label>
                                                <input id="adduserid" name="adduserid" type="text" class="form-control" value="" />
                                                <label class="control-label">Password <span class="required">*</span></label>
                                                <input id="adduserpassword" name="adduserpassword" type="password" class="form-control" value="" />
                                                <label class="control-label">Status <span class="required">*</span></label>
                                                <select id="adduserstatus" name="adduserstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                                    <option value="A">AKTIF</option>
                                                    <option value="I">TIDAK AKTIF</option>
                                                </select>
                                                <label class="control-label">Default Comp <span class="required">*</span></label>
                                                <select id="addusercomp" name="addusercomp" class="form-control" tabindex="-1" style="width: 100%;">
                                                    <option value="">-select-</option>
                                                    <%
                                                        for (int i = 0; i < lsComp.Count; i++)
                                                        {
                                                            MainModel modComp = (MainModel)lsComp[i];
                                                    %>
                                                    <option value="<%=modComp.GetSetcomp %>"><%=modComp.GetSetcomp %> - <%=modComp.GetSetcomp_name %></option>
                                                    <%
                                                        }
                                                    %>
                                                </select>
                                                <label class="control-label">Default Role Id <span class="required">*</span></label>
                                                <select id="adduserroleid" name="adduserroleid" class="form-control" tabindex="-1" style="width: 100%;">
                                                    <option value='01'>SYSADMIN</option>
                                                    <option value='02'>COMPADMIN</option>
                                                    <option value='03'>SALES</option>
                                                    <option value='04'>ACCOUNT</option>
                                                    <option value='05'>WAREHOUSE</option>
                                                </select>
                                                <label class="control-label">Default Dashboard Screen <span class="required">*</span></label>
                                                <select id="addscreenid" name="addscreenid" class="form-control" tabindex="-1" style="width: 100%;">
                                                    <option value="">-select-</option>                                                    
                                                    <option value="010010">010010 - Dashboard 1</option>
                                                    <option value="010020">010020 - Dashboard 2</option>
                                                    <option value="010030">010030 - Dashboard 3</option>
                                                    <option value="010040">010040 - Dashboard 3</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="modal-footer" style="text-align:left;">
                                            <button type="button" class="btn btn-primary" id="btnEditUser" onclick="actionclick('UPDATE');">Kemaskini</button>
                                            <button type="button" class="btn btn-primary" id="btnSaveUser" onclick="actionclick('STORE');">Simpan</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="actionclick('CLOSE')";>Tutup</button>
                                            
                                            <div id="paneladdcomp" class="x_panel">
                                                <a id="addcomp" name="addcomp" class="btn btn-app" data-toggle="modal" data-target=".modal-add-new-comp" onclick="openaddcomp();">
                                                    <i class="fa fa-plus-square green"></i>Tambah Syarikat
                                                </a>
                                                <table id="datatable" class="table">
                                                    <thead>
                                                        <tr>
                                                            <th></th>
                                                            <th>Kod Syarikat</th>
                                                            <th>Nama Syarikat</th>
                                                            <th>Alamat</th>
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
                            <!--END dialog box for add new user-->

                            <!--BEGIN dialog box for add new comp-->
                            <div class="modal fade modal-add-new-comp" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <label class="control-label">Syarikat <span class="required">*</span></label>
                                            <select id="addcompid" name="addcompid" class="form-control" tabindex="-1" style="width: 100%;">
                                                <option value="">-select-</option>
                                            </select>
                                            <label class="control-label">Role Id <span class="required">*</span></label>
                                            <select id="addroleid" name="addroleid" class="form-control" tabindex="-1" style="width: 100%;">
                                                <option value='01'>SYSADMIN</option>
                                                <option value='02'>COMPADMIN</option>
                                                <option value='03'>SALES</option>
                                                <option value='04'>ACCOUNT</option>
                                                <option value='05'>WAREHOUSE</option>
                                            </select>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-primary" id="btnAddComp" onclick="actionclick('ADD_COMP');">Tambah Syarikat</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for add new comp-->

                            <!--BEGIN dialog box for confirm delete-->
                            <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Anda pasti untuk Keluarkan Syarikat ini?</h4>
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
                            <li role="presentation">
                                <a href="#" id="comp-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('COMP_LISTING');">Senarai Syarikat</a>
                            </li>
                            <li role="presentation" class="active">
                                <a href="#" id="user-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('USER_LISTING');">Senarai Pengguna</a>
                            </li>
                        </ul>
                        <div id="myTabContent" class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="home-tab">                    
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <a id="adduser" name="adduser" class="btn btn-app" data-toggle="modal" data-target=".modal-add-new-user" onclick="openadduser();">
                                            <i class="fa fa-plus-square green"></i>Tambah Pengguna
                                        </a>
                                        <%
                                            if (sUserId.Equals("sysadmin"))
                                            {
                                        %>
                                        <a id="manageaccessibility" name="manageaccessibility" class="btn btn-app" onclick="manageaccessibility();">
                                            <i class="fa fa-cog green"></i>Kemaskini Capaian Pengguna
                                        </a>
                                        <%
                                            }
                                        %>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">

                                        <table id="datatable2" class="table">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Id Pengguna</th>
                                                <th>Nama Pengguna</th>
                                                <th>Jenis Pengguna</th>
                                                <th>Default Comp</th>
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

        var selectedUserId = "";
        var storeAction = "";

        $(document).ready(function () {
            getUserList();

        });

        function getUserList() {
            //$('#datatable-buttons').dataTable().clear();
            $('#datatable2').dataTable().fnClearTable();

            var parameters = ["userid", "<%=sUserId%>"];
            PageMethod("getUserListObject", parameters, succeededUserListObject, failedUserListObject, false);
        }

        succeededUserListObject = function (data, textStatus, jqXHR) {
            console.log("succeededUserListObject: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                var t = $('#datatable2').DataTable();
                $.each(resultJSON.userlist, function (i, result) {
                    //$('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>').appendTo('#datatable');
                    //$('#datatable > tbody').append('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>');
                    t.row.add([
                            '<a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a>',
                            result.userid,
                            result.username,
                            result.usertype,
                            result.comp,
                            (result.userstatus == 'A' ? 'AKTIF' : 'TIDAK AKTIF'),
                            '<a href="#" class="btn btn-info btn-xs" onclick="openedituser(' + '\'' + result.userid + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-user"><i class="fa fa-pencil"></i> Kemaskini </a>'
                    ]).draw(false);
                });
            }
            else {
                alert("Tiada Rekod!");
            }
        };

        failedUserListObject = function (jqXHR, textStatus, errorThrown) {
            console.log("failedUserListObject: " + textStatus);
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
                selectedUserId = $('#adduserid').val();

                $('#adduserid').prop('disabled', true);
                $('#btnEditUser').prop('disabled', true);
                $('#btnSaveUser').prop('disabled', false);

            }
            if (action == 'STORE') {
                proceed = false;
                if ($('#addusername').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Nama Pengguna!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#adduserid').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Email (Id Pengguna)!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#adduserpassword').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila isi Password!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#adduserstatus').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila pilih Status!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else if ($('#addusercomp').val().length == 0) {
                    new PNotify({
                        title: 'Alert',
                        text: 'Sila pilih Default Comp!',
                        type: 'warning',
                        styling: 'bootstrap3'
                    });
                }
                else {
                    if ($('#adduserid').val() == selectedUserId) {
                        storeAction = "U";
                        var parameters = ["flag", storeAction,
                                          "userid", $('#adduserid').val(),
                                          "userpwd", $('#adduserpassword').val(),
                                          "usercomp", $('#addusercomp').val(),
                                          "username", $('#addusername').val(),
                                          "useradd", $('#adduseraddress').val(),
                                          "usertelno", $('#addusercontactno').val(),
                                          "usertype", $('#addusertype').val(),
                                          "screenid", $('#addscreenid').val(),
                                          "userroleid", $('#adduserroleid').val(),
                                          "userstatus", $('#adduserstatus').val(),
                                          "createdby", "<%=sUserId%>",
                                          "confirmedby", "<%=sUserId%>"];
                        PageMethod("putUserObject", parameters, succeededPutUserObject, failedPutUserObject, false);

                    }
                    else {
                        storeAction = "I";
                        var parameters = ["flag", storeAction,
                                          "userid", $('#adduserid').val(),
                                          "userpwd", $('#adduserpassword').val(),
                                          "usercomp", $('#addusercomp').val(),
                                          "username", $('#addusername').val(),
                                          "useradd", $('#adduseraddress').val(),
                                          "usertelno", $('#addusercontactno').val(),
                                          "usertype", $('#addusertype').val(),
                                          "screenid", $('#addscreenid').val(),
                                          "userroleid", $('#adduserroleid').val(),
                                          "userstatus", $('#adduserstatus').val(),
                                          "createdby", "<%=sUserId%>",
                                          "confirmedby", "<%=sUserId%>"];
                        PageMethod("putUserObject", parameters, succeededPutUserObject, failedPutUserObject, false);
                    }
                }
}
    if (action == 'CLOSE') {
        proceed = false;
    }

    if (action == 'ADD_COMP') {
        proceed = false;

        var parameters = ["comp", $('#addcompid').val(), "userid", $('#adduserid').val(), "roleid", $('#addroleid').val()];
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

succeededPutUserObject = function (data, textStatus, jqXHR) {
    console.log("succeededPutUserObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        selectedUserId = $('#adduserid').val();
        enabledisableinforform(true);
        $('#btnEditUser').prop('disabled', false);
        $('#btnSaveUser').prop('disabled', true);

        if (storeAction == "I") {
            alert("Penambahan Pengguna Berjaya!");
        }
        else if (storeAction == "U") {
            alert("Kemaskini Pengguna Berjaya!");
        }
        showhidepaneladdcomp("block");

        getUserList();

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", "", "userid", $('#adduserid').val()];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else {

        enabledisableinforform(false);
        $('#btnEditUser').prop('disabled', true);
        $('#btnSaveUser').prop('disabled', false);

        if (storeAction == "I") {
            alert("Penambahan Pengguna Tidak Berjaya!");
        }
        else if (storeAction == "U") {
            $('#adduserid').prop('disabled', true);
            alert("Kemaskini Pengguna Tidak Berjaya!");
        }
    }
};

failedPutUserObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedPutUserObject: " + textStatus);

    enabledisableinforform(false);
    $('#btnEditUser').prop('disabled', true);
    $('#btnSaveUser').prop('disabled', false);

    if (storeAction == "I") {
        alert("Penambahan Pengguna Tidak Berjaya!");
    }
    else if (storeAction == "U") {
        $('#adduserid').prop('disabled', true);
        alert("Kemaskini Pengguna Tidak Berjaya!");
    }
}

succeededInsertUserRoleObject = function (data, textStatus, jqXHR) {
    console.log("succeededInsertUserRoleObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        alert("Syarikat Berjaya Dimasukkan!");

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", "", "userid", $('#adduserid').val()];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else if (resultJSON.result == "N") {
        alert("Syarikat Tidak Berjaya Dimasukkan! Sila berhubung dengan System Administrator");
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
        alert("Syarikat Berjaya Dikeluarkan!");

        getUserList();

        var parameters = ["userid", $('#adduserid').val()];
        PageMethod("getUserObject", parameters, succeededUserObject, failedUserObject, false);

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", "", "userid", $('#adduserid').val()];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
    }
    else if (resultJSON.result == "N") {
        alert("Syarikat Tidak Berjaya Dikeluarkan! Sila berhubung dengan System Administrator");
    }
};

failedDelUserObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedDelUserObject: " + textStatus);
    alert(textStatus);
}

function openadduser() {
    enabledisableinforform(false);
    selectedUserId = "";
    $('#adduserid').prop('disabled', false);
    $('#addusercomp').val("");
    $('#addusername').val("");
    $('#adduserid').val("");
    $('#adduserpassword').val("");
    $('#adduseraddress').text("");
    $('#addusercontactno').val("");
    $('#adduserstatus').val('A');
    $('#addscreenid').val("");
    $('#adduserroleid').val("");
    $('#addusertype').val("");
    $('#btnEditUser').prop('disabled', true);
    $('#btnSaveUser').prop('disabled', false);
    showhidepaneladdcomp("none");
}

function showhidepaneladdcomp(flag) {
    var x = document.getElementById("paneladdcomp");
    x.style.display = flag;
}
function enabledisableinforform(flag) {
    $('#addusername').prop('disabled', flag);
    $('#adduseraddress').prop('disabled', flag);
    $('#addusercontactno').prop('disabled', flag);
    $('#adduserid').prop('disabled', flag);
    $('#adduserpassword').prop('disabled', flag);    
    $('#adduserroleid').prop('disabled', flag);
    $('#addusertype').prop('disabled', flag);
    $('#adduserstatus').prop('disabled', flag);
    $('#addusercomp').prop('disabled', flag);
    $('#addscreenid').prop('disabled', flag);
}

function openedituser(userid) {
    enabledisableinforform(true);

    var parameters = ["userid", userid];
    PageMethod("getUserObject", parameters, succeededUserObject, failedUserObject, false);
}

succeededUserObject = function (data, textStatus, jqXHR) {
    console.log("succeededUserObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        $('#addusercomp').val(resultJSON.userinfo.comp);
        $('#addusername').val(resultJSON.userinfo.username);
        $('#adduserid').val(resultJSON.userinfo.userid);
        $('#adduserpassword').val(resultJSON.userinfo.userpwd);
        $('#adduseraddress').text(resultJSON.userinfo.useradd);
        $('#addusercontactno').val(resultJSON.userinfo.usertelno);
        $('#adduserroleid').val(resultJSON.userinfo.userroleid);
        $('#addusertype').val(resultJSON.userinfo.usertype);
        $('#adduserstatus').val(resultJSON.userinfo.userstatus);
        $('#addscreenid').val(resultJSON.userinfo.screenid);
        selectedUserId = $('#adduserid').val();
        showhidepaneladdcomp("block");

        $('#datatable').dataTable().fnClearTable();
        var parameters = ["comp", "", "userid", $('#adduserid').val()];
        PageMethod("getUserCompListObject", parameters, succeededCompUserListObject, failedCompUserListObject, false);
        $('#btnEditUser').prop('disabled', false);
        $('#btnSaveUser').prop('disabled', true);
    }
    else {
        alert("Tiada Rekod!");
        $('#btnEditUser').prop('disabled', true);
        $('#btnSaveUser').prop('disabled', true);
    }
};

failedUserObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedUserObject: " + textStatus);
    alert(textStatus);
    $('#btnEditUser').prop('disabled', true);
    $('#btnSaveUser').prop('disabled', true);
}

succeededCompUserListObject = function (data, textStatus, jqXHR) {
    console.log("succeededCompUserListObject: " + textStatus);
    resultJSON = JSON.parse(data.d);

    if (resultJSON.result == "Y") {
        var t = $('#datatable').DataTable();
        $.each(resultJSON.compuserlist, function (i, result) {
            //$('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>').appendTo('#datatable');
            //$('#datatable > tbody').append('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>');
            t.row.add([
                    '<a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a>',
                    result.comp,
                    result.comp_name,
                    result.comp_address,
                    result.roleid,
                    (result.comp_status == 'ACTIVE' ? 'AKTIF' : 'TIDAK AKTIF'),
                    '<a href="#" class="btn btn-warning btn-xs" onclick="confirmdeletelineitem(' + '\'' + result.comp + '\'' + ',' + '\'' + result.userid + '\'' + ');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-pencil"></i> Hapus </a>'
            ]).draw(false);
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
    $('#usertype').prop('disabled', flag);
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

        function confirmdeletelineitem(comp, userid) {
            $('#hidLineNo1').val(comp);
            $('#hidLineNo2').val(userid);
        }

        function openaddcomp() {
            var parameters = ["userid", "<%=sUserId%>"];
            PageMethod("getCompListObject", parameters, succeededCompListObject, failedCompListObject, false);
        }

        succeededCompListObject = function (data, textStatus, jqXHR) {
            console.log("succeededCompListObject: " + textStatus);
            resultJSON = JSON.parse(data.d);

            var select = document.getElementById("addcompid");
            for (var option in select) {
                select.remove(option);
            }
            document.getElementById("addcompid").add(new Option("-select-", ""));

            if (resultJSON.result == "Y") {
                var t = $('#datatable').DataTable();
                $.each(resultJSON.complist, function (i, result) {
                    $('#addcompid').append('<option value=' + result.comp + '>' + result.comp + ' - ' + result.comp_name + '</option>');
                });
            }
        };

        failedCompListObject = function (jqXHR, textStatus, errorThrown) {
            console.log("failedCompListObject: " + textStatus);
            alert(textStatus);
        }

        function manageaccessibility() {

            var popupWindow = window.open("UserAccessibilityMgt.aspx?action=OPEN", "manage_accessibility", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function manageaccessibility() {

            var popupWindow = window.open("UserAccessibilityMgt.aspx?action=OPEN", "manage_accessibility", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

    </script>

</asp:Content>

