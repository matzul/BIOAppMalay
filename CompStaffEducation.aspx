<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompStaffEducation.aspx.cs" Inherits="CompStaffEducation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        ul.nav > li.active > a {
            background-color: #d1e6eb;
            color:blue;
            font-weight:bold;
        }
        
        ul.nav > li.active > a:hover {
            background-color: #d1e6eb;
            color:blue;
            font-weight:bold;
        }

        ul.nav > li > a:hover {
	        background-color: #000000;
	        color: #FFFFFF;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-offset-2 col-md-offset-2">
        <div class="col-lg-10 col-md-10">
            <div>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Maklumat Kakitangan</h2>
                        <ul class="nav navbar-right panel_toolbox">
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <form id="form1" runat="server">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form1">
                                    <label class="control-label">No. Pekerja <span class="required">*</span></label>
                                    <input id="staffno" name="staffno" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetstaffno %>" />
                                    <label class="control-label">Nama Pekerja <span class="required">*</span></label>
                                    <input id="staffname" name="staffname" type="text" class="form-control" value="<%=oModStaff.GetSetname %>" />
                                    <label class="control-label">No. K/P <span class="required">*</span></label>
                                    <input id="staffnicno" name="staffnicno" type="text" maxlength="12" class="form-control" value="<%=oModStaff.GetSetnicno %>" />
                                    <label class="control-label">No. Passport</label>
                                    <input id="staffpassport" name="staffpassport" type="text" class="form-control" value="<%=oModStaff.GetSetpassport %>" />
                                    <label class="control-label">Status <span class="required">*</span></label>
                                    <select id="staffstatus" name="staffstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                        <option value="ACTIVE" <%=(oModStaff.GetSetstatus.Equals("ACTIVE")?"selected":"") %>>AKTIF</option>
                                        <option value="IN-ACTIVE" <%=(oModStaff.GetSetstatus.Equals("IN-ACTIVE")?"selected":"") %>>TIDAK AKTIF</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label class="control-label">Jabatan/ Bahagian <span class="required">*</span></label>
                                    <input id="staffdeptname" name="staffdeptname" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetdept_name %>" />
                                    <label class="control-label">Gred Kedudukan <span class="required">*</span></label>
                                    <input id="staffgredname" name="staffgredname" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetgred_name %>" />
                                    <label class="control-label">Jawatan <span class="required">*</span></label>
                                    <input id="staffposname" name="staffposname" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetpos_name %>" />
                                    <label class="control-label">Handphone<span class="required">*</span></label>
                                    <input id="staffmobile1" name="staffmobile1" type="text" class="form-control" value="<%=oModStaff.GetSetmobile1 %>" />
                                    <label class="control-label">Email</label>
                                    <input id="staffemail1" name="staffemail1" type="text" class="form-control" value="<%=oModStaff.GetSetemail1 %>" />
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
                                        <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();" >Tutup</button>
                                        <%
                                        HRModel oAlerMssg = oHRCon.getAlertMessage(sAlertMessage);
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
                                            <input type="hidden" name="hidStaffNo" id="hidStaffNo" value="<%=sStaffNo %>" />
                                        </div>
                                    </div>
                                </div>
                            </section>

                        </div>
                    </form>
                    <div class="" role="tabpanel" data-example-id="togglable-tabs">
                        <ul id="myTab" class="nav nav-tabs" role="tablist">
                            <li role="presentation">
                                <a href="#" id="details-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('STAFF_DETAILS');">Maklumat Peribadi</a>
                            </li>
                            <li role="presentation" class="active">
                                <a href="#" id="family-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_FAMILY');">Maklumat Keluarga</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="employment-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_EMPLOYMENT');">Maklumat Perjawatan</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="attendance-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_ATTENDANCE');">Maklumat Kehadiran</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="leave-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_LEAVE');">Maklumat Cuti</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="salary-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_SALARY');">Maklumat Gaji</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="education-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_EDUCATION');">Maklumat Pendidikan</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="experience-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_EXPERIENCE');">Pengalaman Kerja</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="expertise-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('STAFF_EXPERTISE');">Kepakaran & Kemahiran</a>
                            </li>
                            <li role="presentation">
                                <a href="#" id="emergency-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('STAFF_EMERGENCY');">Maklumat Kecemasan</a>
                            </li>
                        </ul>
                        <div id="myTabContent" class="tab-content">
                                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="details-tab">                    
                                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                    
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-top:20px;padding-bottom:20px;">
                                        <a id="addStaffFamily" name="addStaffFamily" class="btn btn-app" data-toggle="modal" data-target=".modal-add-edit-staff-family" onclick="openstaffFamily('NEW','0');">
                                            <i class="fa fa-plus-square green"></i>Tambah
                                        </a>
                                        <table id="datatable-buttons" class="table table-striped jambo_table">
                                            <thead>
                                            <tr>
                                                <th></th>
                                                <th>Nama Tanggungan</th>
                                                <th>No. K/P</th>
                                                <th>Hubungan</th>
                                                <th>Pekerjaan</th>
                                                <th>Pendapatan</th>
                                                <th>Status</th>
                                                <th></th>
                                            </tr>
                                            </thead>

                                            <tbody>
                                            <%
                                                if (lsStaffEducation.Count > 0)
                                                {
                                                    for (int i = 0; i < lsStaffEducation.Count; i++)
                                                    {
                                                        HRModel modStaff = (HRModel)lsStaffEducation[i];
                                            %>       
                                            <tr>
                                                <td><a href="#" class="btn-link" onclick="openstaffEmployment('VIEW','<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-add-edit-staff-employment"><i class="glyphicon glyphicon-play"></i></a></td>
                                                <td><a href="#" class="btn-link" onclick="openstaffEmployment('VIEW','<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-add-edit-staff-employment"><%=modStaff.GetSetdept_name %></a></td>
                                                <td><%=modStaff.GetSetgred_name %></td>
                                                <td><%=modStaff.GetSetpos_name %></td>
                                                <td><%=modStaff.GetSettype %><br /><%=modStaff.GetSetcat %></td>
                                                <td><%=modStaff.GetSetstr_fromdate %></td>
                                                <td><%=modStaff.GetSetstatus %></td>
                                                <td>
                                                    <a href="#" class="btn btn-info btn-xs" onclick="openstaffEmployment('VIEW','<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-add-edit-staff-employment"><i class="fa fa-pencil"></i> Kemaskini </a>
                                                    <a href="#" class="btn btn-warning btn-xs" onclick="confirmdeletestaffEmployment('<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-confirm-delete-staff-employment"><i class="fa fa-pencil"></i> Hapus </a>
                                                </td>
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
        $(document).ready(function () {

            $('#staffdob').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            var getSaluteList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getSaluteList", getSaluteList_parameters, getSaluteList_succeedAjaxFn, getSaluteList_failedAjaxFn, false);

            var getRaceList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getRaceList", getRaceList_parameters, getRaceList_succeedAjaxFn, getRaceList_failedAjaxFn, false);

            var getReligionList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getReligionList", getReligionList_parameters, getReligionList_succeedAjaxFn, getReligionList_failedAjaxFn, false);

            var getCountryList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCountryList", getCountryList_parameters, getCountryList_succeedAjaxFn, getCountryList_failedAjaxFn, false);

            var getStateList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getStateList", getStateList_parameters, getStateList_succeedAjaxFn, getStateList_failedAjaxFn, false);

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

        });

        //BEGIN Response for getSaluteList
        getSaluteList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSaluteList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getSaluteList = JSON.parse(data.d);
            if (result_getSaluteList.result == "Y") {
                $.each(result_getSaluteList.salutelist, function (i, result) {
                    if (result.GetSetid == "<%=oModStaff.GetSetsalute%>") {
                        output += "<option value='" + result.GetSetid + "' selected>" + result.GetSetdesc + "</option>";
                    } else {
                        output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                    }
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#staffsalute').html("").append(output);
        };

        getSaluteList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSaluteList_failedAjaxFn: " + textStatus);
        }
        //END Response for getSaluteList

        //BEGIN Response for getRaceList
        getRaceList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getRaceList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getRaceList = JSON.parse(data.d);
            if (result_getRaceList.result == "Y") {
                $.each(result_getRaceList.racelist, function (i, result) {
                    if (result.GetSetid == "<%=oModStaff.GetSetrace%>") {
                        output += "<option value='" + result.GetSetid + "' selected>" + result.GetSetdesc + "</option>";
                    } else {
                        output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                    }
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#staffrace').html("").append(output);
        };

        getRaceList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getRaceList_failedAjaxFn: " + textStatus);
        }
        //END Response for getRaceList

        //BEGIN Response for getReligionList
        getReligionList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getReligionList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getReligionList = JSON.parse(data.d);
            if (result_getReligionList.result == "Y") {
                $.each(result_getReligionList.religionlist, function (i, result) {
                    if (result.GetSetid == "<%=oModStaff.GetSetreligion%>") {
                        output += "<option value='" + result.GetSetid + "' selected>" + result.GetSetdesc + "</option>";
                    } else {
                        output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                    }
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#staffreligion').html("").append(output);
        };

        getReligionList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getReligionList_failedAjaxFn: " + textStatus);
        }
        //END Response for getReligionList

        //BEGIN Response for getCountryList
        getCountryList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCountryList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCountryList = JSON.parse(data.d);
            if (result_getCountryList.result == "Y") {
                $.each(result_getCountryList.countrylist, function (i, result) {
                    if (result.GetSetid == "<%=oModStaff.GetSetpcountry%>") {
                        output += "<option value='" + result.GetSetid + "' selected>" + result.GetSetdesc + "</option>";
                    } else {
                        output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                    }
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#staffpcountry').html("").append(output);
        };

        getCountryList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCountryList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCountryList

        //BEGIN Response for getStateList
        getStateList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStateList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStateList = JSON.parse(data.d);
            if (result_getStateList.result == "Y") {
                $.each(result_getStateList.statelist, function (i, result) {
                    if (result.GetSetid == "<%=oModStaff.GetSetpstate%>") {
                        output += "<option value='" + result.GetSetid + "' selected>" + result.GetSetdesc + "</option>";
                    } else {
                        output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                    }
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#staffpstate').html("").append(output);
        };

        getStateList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStateList_failedAjaxFn: " + textStatus);
        }
        //END Response for getStateList

        function enabledisableinputform(flag) {
            $('#staffname').prop('disabled', flag);
            $('#staffnicno').prop('disabled', flag);
            $('#staffpassport').prop('disabled', flag);
            $('#staffstatus').prop('disabled', flag);
            $('#staffdeptid').prop('disabled', flag);
            $('#staffgredid').prop('disabled', flag);
            $('#staffposid').prop('disabled', flag);
            $('#staffmobile1').prop('disabled', flag);
        }

        function actionclick(action) {
            var proceed = true;

            if (action == 'OPEN') {
            }
            else if (action == 'EDIT') {
            }
            else if (action == 'SAVE') {
                if ($('#staffname').val() == "") {
                    proceed = false;
                    alert("Sila isi Nama Pekerja!");
                } else if ($('#staffnicno').val() == "") {
                    proceed = false;
                    alert("Sila isi No. K/P!");
                } else if ($('#staffmobile1').val() == "") {
                    proceed = false;
                    alert("Sila isi Nombor Handphone!");
                }
            } else if (action == 'STAFF_DETAILS') {
                window.location.href = "CompStaffDetails.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
                return false; //to check IsPostBack
            } else if (action == 'STAFF_FAMILY') {
                window.location.href = "CompStaffFamily.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
                return false; //to check IsPostBack
            } else if (action == 'STAFF_EMPLOYMENT') {
                window.location.href = "CompStaffEmployment.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
                return false; //to check IsPostBack
            }

            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        function updateStaffDetails() {

        }
    </script>

</asp:Content>

