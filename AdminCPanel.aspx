<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AdminCPanel.aspx.cs" Inherits="AdminCPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-offset-2 col-md-offset-2">
        <div class="col-lg-10 col-md-10">
            <div>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>BIOApp System: Dashboard Pengguna</h2>
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
                                        <div class="modal-body">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
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
                                            <div class="col-md-6 col-sm-6 col-xs-12">
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
                                            <!--
                                            <div id="form2" class="form-horizontal form-label-left">

                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod Syarikat <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compcode" name="compcode" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Nama Syarikat <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compname" name="compname" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Id Syarikat  <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compid" name="compid" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Alamat Syarikat <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <textarea id="compaddress" class="form-control" rows="2" name="compaddress"></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Nama Pegawai <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compcontact" name="compcontact" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Telefon No <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compcontactno" name="compcontactno" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Bank Didaftarkan </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compaccountbank" name="compaccountbank" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Akaun Bank </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compaccountno" name="compaccountno" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Web Site </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compwebsite" name="compwebsite" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Emel </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compemail" name="compemail" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Icon Syarikat </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="compicon" name="compicon" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Logo(1) Syarikat </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="complogo1" name="complogo1" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Logo(2) Syarikat </label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <input id="complogo2" name="complogo2" type="text" class="form-control" value="" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Status <span class="required">*</span></label>
                                                    <div class="col-md-9 col-sm-9 col-xs-12">
                                                        <select id="compstatus" name="compstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                                            <option value="ACTIVE">AKTIF</option>
                                                            <option value="INACTIVE">TIDAK AKTIF</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            -->
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-primary" id="btnEditComp" onclick="actionclick('UPDATE');">Kemaskini</button>
                                            <button type="button" class="btn btn-primary" id="btnSaveComp" onclick="actionclick('STORE');">Simpan</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="actionclick('CLOSE')";>Tutup</button>

                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for add line item-->
                            <!--BEGIN dialog box for confirm delete-->
                            <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Anda pasti untuk keluarkan Item ini?</h4>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-warning" id="btnDeleteLineItem" onclick="actionclick('DELETE');">Ya</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for confirm delete-->
                            <!--BEGIN dialog box for confirm cancel payment paid-->
                            <div class="modal fade modal-confirm-cancel-paypaid" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Anda pasti untuk batalkan proses ini?</h4>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-warning" id="btnCancelOrder" onclick="actionclick('CANCEL');">Ya</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for confirm delete-->

                        </div>
                    </form>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <a id="addcomp" name="addcomp" class="btn btn-app" data-toggle="modal" data-target=".modal-add-new-company" onclick="openaddcomp();">
                            <i class="fa fa-plus-square green"></i>Tambah Syarikat
                        </a>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">

                        <table id="datatable-buttons" class="table">
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

        var storedComp = false;
        var selectedComp = "";

        $(document).ready(function () {
            getCompList();

        });

        function getCompList() {
            var parameters = ["userid", "<%=sUserId%>"];
            PageMethod("getCompListObject", parameters, succeededCompListObject, failedCompListObject, false);
        }

        succeededCompListObject = function (data, textStatus, jqXHR) {
            console.log("succeededCompListObject: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {

                $.each(resultJSON.complist, function (i, result) {
                    $('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.comp + '</td><td>' + result.comp_name + '</td><td>' + result.comp_id + '</td><td>' + result.status + '</td><td><a href="#" class="btn btn-info btn-xs" onclick="openeditcomp(' + '\'' + result.comp + '\'' + ');" data-toggle="modal" data-target=".modal-add-new-company"><i class="fa fa-pencil"></i> Kemaskini </a></td></tr>').appendTo('#datatable-buttons');
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
            if (action == 'UPDATE') {
                proceed = false;
                enabledisableinforform(false);
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

                    }
                    else {
                        var parameters = ["flag", "I",
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
        if (storedComp) {
            storedComp = false;
            getCompList();
        }
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
        alert("Penambahan Syarikat Berjaya!");

        enabledisableinforform(true);
        $('#btnEditComp').prop('disabled', false);
        $('#btnSaveComp').prop('disabled', true);
        storedComp = true;
    }
    else {
        alert("Penambahan Syarikat Tidak Berjaya!");

        enabledisableinforform(false);
        $('#btnEditComp').prop('disabled', true);
        $('#btnSaveComp').prop('disabled', false);
        storedComp = false;
    }
};

failedPutCompObject = function (jqXHR, textStatus, errorThrown) {
    console.log("failedPutCompObject: " + textStatus);
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
    $('#compcode').prop('disabled', false);
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

    </script>

</asp:Content>

