<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompStaffEmployment.aspx.cs" Inherits="CompStaffEmployment" %>

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
                                            <input type="hidden" name="hidEmploymentId" id="hidEmploymentId" value="<%=sStaffNo %>" />
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
                            <li role="presentation">
                                <a href="#" id="family-tab" role="tab" data-toggle="tab" aria-expanded="false" onclick="actionclick('STAFF_FAMILY');">Maklumat Keluarga</a>
                            </li>
                            <li role="presentation" class="active">
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
                                        <a id="addStaffEmployment" name="addStaffEmployment" class="btn btn-app" data-toggle="modal" data-target=".modal-add-edit-staff-employment" onclick="openstaffEmployment('NEW','0');">
                                            <i class="fa fa-plus-square green"></i>Tambah
                                        </a>
                                        <table id="datatable-buttons" class="table table-striped jambo_table">
                                            <thead>
                                            <tr>
                                                <th></th>
                                                <th>Jabatan/ Bahagian</th>
                                                <th>Gred Kedudukan</th>
                                                <th>Nama Jawatan</th>
                                                <th>Penempatan/ Taraf</th>
                                                <th>Tarikh Lantikan</th>
                                                <th>Tarikh Tamat</th>
                                                <th>Penyelia/ Ketua</th>
                                                <th>Status</th>
                                                <th></th>
                                            </tr>
                                            </thead>

                                            <tbody>
                                            <%
                                                if (lsStaffEmployment.Count > 0)
                                                {
                                                    for (int i = 0; i < lsStaffEmployment.Count; i++)
                                                    {
                                                        HRModel modStaff = (HRModel)lsStaffEmployment[i];
                                            %>       
                                            <tr>
                                                <td><a href="#" class="btn-link" onclick="openstaffEmployment('VIEW','<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-add-edit-staff-employment"><i class="glyphicon glyphicon-play"></i></a></td>
                                                <td><a href="#" class="btn-link" onclick="openstaffEmployment('VIEW','<%=modStaff.GetSetid %>');" data-toggle="modal" data-target=".modal-add-edit-staff-employment"><%=modStaff.GetSetdept_name %></a></td>
                                                <td><%=modStaff.GetSetgred_name %></td>
                                                <td><%=modStaff.GetSetpos_name %></td>
                                                <td><%=modStaff.GetSettype %><br /><%=modStaff.GetSetcat %></td>
                                                <td><%=modStaff.GetSetstr_fromdate %></td>
                                                <td><%=modStaff.GetSetstr_todate %></td>
                                                <td><%=modStaff.GetSetreportto %></td>
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

    <!--BEGIN dialog box for staff employment details-->
    <div id="myStaffEmployment" class="modal fade modal-add-edit-staff-employment" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Maklumat Perjawatan</h4>
                </div>
                <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="control-label">No. Pekerja <span class="required">*</span></label>
                        <input id="addstaffno" name="addstaffno" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetstaffno %>"" />
                        <label class="control-label">Nama Pekerja <span class="required">*</span></label>
                        <input id="addstaffname" name="addstaffname" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetname %>" />
                        <label class="control-label">Jabatan/ Bahagian <span class="required">*</span></label>
                        <select id="addstaffdeptid" name="addstaffdeptid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Gred Kedudukan <span class="required">*</span></label>
                        <select id="addstaffgredid" name="addstaffgredid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Jawatan <span class="required">*</span></label>
                        <select id="addstaffposid" name="addstaffposid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Status <span class="required">*</span></label>
                        <select id="addstaffstatus" name="addstaffstatus" class="form-control" tabindex="-1" style="width: 100%;">
                            <option value="ACTIVE">AKTIF</option>
                            <option value="IN-ACTIVE">TIDAK AKTIF</option>
                        </select>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="control-label">Penempatan <span class="required">*</span></label>
                        <select id="addstafftype" name="addstafftype" class="form-control">
                            <option value="PERLANTIKAN">PERLANTIKAN</option>
                            <option value="PROMOSI">PROMOSI</option>
                            <option value="KENAIKAN PANGKAT">KENAIKAN PANGKAT</option>
                            <option value="PENURUNAN PANGKAT">PENURUNAN PANGKAT</option>
                            <option value="PERLETAKAN JAWATAN">PERLETAKAN JAWATAN</option>
                            <option value="PEMINDAHAN">PEMINDAHAN</option>
                            <option value="PEMINJAMAN">PEMINJAMAN</option>
                            <option value="PERSARAAN">PERSARAAN</option>
                            <option value="PENAMATAN">PENAMATAN</option>
                        </select>
                        <label class="control-label">Kategori <span class="required">*</span></label>
                        <select id="addstaffcat" name="addstaffcat" class="form-control">
                            <option value="TETAP">TETAP</option>
                            <option value="KONTRAK">KONTRAK</option>
                            <option value="SEMENTARA">SEMENTARA</option>
                            <option value="SAMBILAN">SAMBILAN</option>
                            <option value="PERCUBAAN">PERCUBAAN</option>
                        </select>
                        <label class="control-label">Tarikh Lantikan <span class="required">*</span></label>
                        <input id="addstafffromdate" name="addstafffromdate" type="text" class="date-picker form-control" value="" />                        
                        <label class="control-label">Tarikh Tamat <span class="required">*</span></label>
                        <input id="addstafftodate" name="addstafftodate" type="text" class="date-picker form-control" value="" />                        
                        <label class="control-label">Penyelia/ Ketua <span class="required">*</span></label>
                        <select id="addstaffreportto" name="addstaffreportto" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Catatan </label>
                        <textarea id="addstaffremarks" class="form-control" rows="2" name="addstaffremarks"></textarea>
                    </div>
                </div>
                <div class="modal-footer" style="text-align:left;">
                    <button type="button" class="btn btn-primary" id="btnSaveStaff" onclick="actionclick('ADD');">Tambah</button>
                    <button type="button" class="btn btn-primary" id="btnEditStaff" onclick="actionclick('UPDATE');">Kemaskini</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>                                            
                </div>
            </div>
                                                           
        </div>
    </div>
    <!--END dialog box for staff employment details-->

    <!--BEGIN dialog box for confirm delete-->
    <div class="modal fade modal-confirm-delete-staff-employment" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Anda pasti untuk Hapuskan Perjawatan ini?</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" id="btnDeleteLineItem" data-dismiss="modal" onclick="actionclick('DELETE');">Ya</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                </div>

            </div>
        </div>
    </div>
    <!--END dialog box for confirm delete-->

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

            $('#addstafffromdate').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#addstafftodate').daterangepicker({
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

            var getDeptList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompDeptList", getDeptList_parameters, getDeptList_succeedAjaxFn, getDeptList_failedAjaxFn, false);

            var getGredList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompGredList", getGredList_parameters, getGredList_succeedAjaxFn, getGredList_failedAjaxFn, false);

            var getPosList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompPosList", getPosList_parameters, getPosList_succeedAjaxFn, getPosList_failedAjaxFn, false);

            getStaffGredReportTo("<%=oModStaff.GetSetgred_id%>");

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

        //BEGIN Response for getCompDeptList
        getDeptList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getDeptList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompDeptList = JSON.parse(data.d);
            if (result_getCompDeptList.result == "Y") {
                $.each(result_getCompDeptList.deptlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                    
                    //if (result.GetSetsid == "<%=oModStaff.GetSetdept_id%>") {
                    //    output += "<option value='" + result.GetSetsid + "' selected>" + result.GetSetname + "</option>";
                    //} else {
                    //    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                    //}
                    
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffdeptid').html("").append(output);
        };

        getDeptList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getDeptList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompDeptList

        //BEGIN Response for getCompGredList
        getGredList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getGredList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompGredList = JSON.parse(data.d);
            if (result_getCompGredList.result == "Y") {
                $.each(result_getCompGredList.gredlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";

                    //if (result.GetSetsid == "<%=oModStaff.GetSetgred_id%>") {
                    //    output += "<option value='" + result.GetSetsid + "' selected>" + result.GetSetname + "</option>";
                    //} else {
                    //    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                    //}

                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffgredid').html("").append(output);
        };

        getGredList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getGredList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompGredList

        //BEGIN Response for getCompPosList
        getPosList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getPosList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompPosList = JSON.parse(data.d);
            if (result_getCompPosList.result == "Y") {
                $.each(result_getCompPosList.poslist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";

                    //if (result.GetSetsid == "<%=oModStaff.GetSetpos_id%>") {
                    //    output += "<option value='" + result.GetSetsid + "' selected>" + result.GetSetname + "</option>";
                    //} else {
                    //    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                    //}

                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffposid').html("").append(output);
        };

        getPosList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getPosList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompPosList

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
            else if (action == 'ADD') {
                if ($('#addstaffno').val() == "") {
                    proceed = false;
                    alert("System Error!");
                }
                else if ($('#addstaffname').val() == "") {
                    proceed = false;
                    alert("System Error!");
                }
                else if ($('#addstaffdeptid').val() == "" || $('#addstaffdeptid').val() == null || $('#addstaffdeptid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jabatan/Bahagian!");
                }
                else if ($('#addstaffgredid').val() == "" || $('#addstaffgredid').val() == null || $('#addstaffgredid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Gred Kedudukan!");
                }
                else if ($('#addstaffposid').val() == "" || $('#addstaffposid').val() == null || $('#addstaffposid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jawatan!");
                }
                else if ($('#addstafffromdate').val() == "") {
                    proceed = false;
                    alert("Sila isi Tarikh Lantikan!");
                }
                /*
                else if ($('#addstaffreportto').val() == "" || $('#addstaffreportto').val() == null || $('#addstaffreportto').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Penyelia/ Ketua!");
                }
                */
                //ajax to add new staff employment
                if (proceed) {
                    var insertStaffEmploy_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", "<%=sStaffNo%>", "deptid", $('#addstaffdeptid').val(), "gredid", $('#addstaffgredid').val(), "posid", $('#addstaffposid').val(), "type", $('#addstafftype').val(), "cat", $('#addstaffcat').val(), "probation", 0, "fromdate", $('#addstafffromdate').val(),
                        "todate", $('#addstafftodate').val(), "reportto", $('#addstaffreportto').val(), "status", $('#addstaffstatus').val(), "remarks", $('#addstaffremarks').val()];
                    PageMethod("insertStaffEmploy", insertStaffEmploy_parameters, insertStaffEmploy_succeedAjaxFn, insertStaffEmploy_failedAjaxFn, false);
                    return false; //to check IsPostBack
                }
            }
            else if (action == 'UPDATE') {
                if ($('#addstaffno').val() == "") {
                    proceed = false;
                    alert("System Error!");
                }
                else if ($('#addstaffname').val() == "") {
                    proceed = false;
                    alert("System Error!");
                }
                else if ($('#addstaffdeptid').val() == "" || $('#addstaffdeptid').val() == null || $('#addstaffdeptid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jabatan/Bahagian!");
                }
                else if ($('#addstaffgredid').val() == "" || $('#addstaffgredid').val() == null || $('#addstaffgredid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Gred Kedudukan!");
                }
                else if ($('#addstaffposid').val() == "" || $('#addstaffposid').val() == null || $('#addstaffposid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jawatan!");
                }
                else if ($('#addstafffromdate').val() == "") {
                    proceed = false;
                    alert("Sila isi Tarikh Lantikan!");
                }
                /*
                else if ($('#addstaffreportto').val() == "" || $('#addstaffreportto').val() == null || $('#addstaffreportto').val() == undefined) {
                    if ($('#addstaffstatus').val() == "ACTIVE") {
                        proceed = false;
                        alert("Sila isi Penyelia/ Ketua!");
                    } else {
                        if ($('#addstafftodate').val() == "") {
                            alert("Sila isi Tarikh Tamat!");
                            proceed = false;
                        } else {
                            proceed = true;
                        }
                    }
                }
                */
                //ajax to add update staff employment
                if (proceed) {
                    var updateStaffEmploy_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", "<%=sStaffNo%>", "id", $('#hidEmploymentId').val(), "deptid", $('#addstaffdeptid').val(), "gredid", $('#addstaffgredid').val(), "posid", $('#addstaffposid').val(), "type", $('#addstafftype').val(), "cat", $('#addstaffcat').val(), "probation", 0, "fromdate", $('#addstafffromdate').val(),
                        "todate", $('#addstafftodate').val(), "reportto", $('#addstaffreportto').val(), "status", $('#addstaffstatus').val(), "remarks", $('#addstaffremarks').val()];
                    PageMethod("updateStaffEmploy", updateStaffEmploy_parameters, updateStaffEmploy_succeedAjaxFn, updateStaffEmploy_failedAjaxFn, false);
                    return false; //to check IsPostBack
                }
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
            } else if (action == 'DELETE') {
                if ($('#hidEmploymentId').val() == "") {
                    proceed = false;
                    alert("System Error!");
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

        var insertStaffEmploy_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertStaffEmploy_succeedAjaxFn: " + textStatus);
            var insertStaffEmploy_result = JSON.parse(data.d);
            if (insertStaffEmploy_result.result == "Y") {
                alert(insertStaffEmploy_result.message);
                window.location.href = "CompStaffEmployment.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
            }
            else {
                alert(insertStaffEmploy_result.message);
            }
        }

        var insertStaffEmploy_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertStaffEmploy_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to insert staff employment...");
        }

        var updateStaffEmploy_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateStaffEmploy_succeedAjaxFn: " + textStatus);
            var updateStaffEmploy_result = JSON.parse(data.d);
            if (updateStaffEmploy_result.result == "Y") {
                alert(updateStaffEmploy_result.message);
                window.location.href = "CompStaffEmployment.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
            }
            else {
                alert(updateStaffEmploy_result.message);
            }
        }

        var updateStaffEmploy_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateStaffEmploy_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to update staff employment...");
        }

        function openstaffEmployment(action, id) {
            var proceed = true;
            
            if (action == 'NEW') {
                $('#addstaffdeptid').val("");
                $('#addstaffgredid').val("");
                $('#addstaffposid').val("");
                $('#addstaffstatus').val("ACTIVE");
                $('#addstafftype').val("PERLANTIKAN");
                $('#addstaffcat').val("TETAP");
                $('#addstafffromdate').val("");
                $('#addstafftodate').val("");
                $('#addstaffreportto').val("");
                $('#addstaffremarks').val("");
                $('#btnSaveStaff').show();
                $('#btnEditStaff').hide();
            } else if (action == 'VIEW') {
                //ajax to get details of staff employment
                $('#hidEmploymentId').val(id);
                var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", "<%=sStaffNo%>", "id", $('#hidEmploymentId').val()];
                PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);
                $('#btnSaveStaff').hide();
                $('#btnEditStaff').show();
            }
        }

        //BEGIN Response for getStaffEmployDetails
        getStaffEmployDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffEmployDetails_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStaffEmployDetails = JSON.parse(data.d);
            if (result_getStaffEmployDetails.result == "Y") {
                //$("#YourSelect  option[value='${YourValue}']`).prop('selected', true);

                $('#myStaffEmployment').on('show.bs.modal', function (e) {

                    $("#addstaffdeptid option[value='" + result_getStaffEmployDetails.staffemploy.GetSetdept_id + "']").prop("selected", true);
                    $("#addstaffgredid option[value='" + result_getStaffEmployDetails.staffemploy.GetSetgred_id + "']").prop("selected", true);
                    $("#addstaffposid option[value='" + result_getStaffEmployDetails.staffemploy.GetSetpos_id + "']").prop("selected", true);
                    $("#addstaffstatus option[value='" + result_getStaffEmployDetails.staffemploy.GetSetstatus + "']").prop("selected", true);
                    $("#addstafftype option[value='" + result_getStaffEmployDetails.staffemploy.GetSettype + "']").prop("selected", true);
                    $("#addstaffcat option[value='" + result_getStaffEmployDetails.staffemploy.GetSetcat + "']").prop("selected", true);
                    $("#addstafffromdate").val(result_getStaffEmployDetails.staffemploy.GetSetstr_fromdate);
                    $("#addstafftodate").val(result_getStaffEmployDetails.staffemploy.GetSetstr_todate);
                    $("#addstaffreportto option[value='" + result_getStaffEmployDetails.staffemploy.GetSetreportto + "']").prop("selected", true);
                    $("#addstaffremarks").val(result_getStaffEmployDetails.staffemploy.GetSetremarks);

                });

            } else {
                alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        function confirmdeletestaffEmployment(id) {
            $('#hidEmploymentId').val(id);
        }

        $('#addstaffreportto').on('focus', function () {
            if ($('#addstaffgredid').val()) {
                if ($("#addstaffgredid").val().length > 0) {
                    getStaffGredReportTo($("#addstaffgredid").val());
                }
            }
        });

        function getStaffGredReportTo(gredid) {
            var getStaffGredReportTo_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", "<%=sStaffNo%>", "gredid", gredid];
            PageMethod("getStaffGredReportTo", getStaffGredReportTo_parameters, getStaffGredReportTo_succeedAjaxFn, getStaffGredReportTo_failedAjaxFn, false);
        }

        //BEGIN Response for getGredReportTo
        getStaffGredReportTo_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffGredReportTo_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStaffGredReportTo = JSON.parse(data.d);
            if (result_getStaffGredReportTo.result == "Y") {
                output += "<option value=''>-Sila Pilih-</option>";
                $.each(result_getStaffGredReportTo.stafflist, function (i, result) {
                    output += "<option value='" + result.GetSetstaffno + "'>" + result.GetSetname + " [" + result.GetSetstaffno + "]</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffreportto').html("").append(output);
        };

        getStaffGredReportTo_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffGredReportTo_failedAjaxFn: " + textStatus);
        }
        //END Response for getGredReportTo

    </script>

</asp:Content>

