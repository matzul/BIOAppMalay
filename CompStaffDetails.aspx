<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompStaffDetails.aspx.cs" Inherits="CompStaffDetails" %>

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
                            <li role="presentation" class="active">
                                <a href="#" id="details-tab" role="tab" data-toggle="tab" aria-expanded="true" onclick="actionclick('STAFF_DETAILS');">Maklumat Peribadi</a>
                            </li>
                            <li role="presentation">
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
                    
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-top:20px;padding-bottom:20px;">
                                            <label class="control-label">Salutasi <span class="required">*</span></label>
                                            <select id="staffsalute" name="staffsalute" class="form-control">
                                                <option value="">-Select-</option>
                                            </select>
                                            <label class="control-label">Nick Name</label>
                                            <input id="staffnickname" name="staffnickname" type="text" class="form-control" value="<%=oModStaff.GetSetnickname %>" />
                                            <label class="control-label">Warganegara <span class="required">*</span></label>
                                            <select id="staffnationality" name="staffnationality" class="form-control">
                                                <option value="WARGANEGARA" <%=(oModStaff.GetSetnationality.Equals("WARGANEGARA")?"selected":"") %>>WARGANEGARA</option>
                                                <option value="BUKAN WARGANEGARA" <%=(oModStaff.GetSetnationality.Equals("BUKAN WARGANEGARA")?"selected":"") %>>BUKAN WARGANEGARA</option>
                                            </select>
                                            <label class="control-label left">Tarikh Lahir <span class="required">*</span></label>/ <label id="staffage" class="control-label right">Umur: </label>
                                            <input type="text" id="staffdob" class="date-picker form-control" name="staffdob" value="<%=oModStaff.GetSetstr_dob %>" />
                                            <label class="control-label">Tempat Lahir </label>
                                            <input id="staffbirthplace" name="staffbirthplace" type="text" class="form-control" value="<%=oModStaff.GetSetbirthplace %>"/>
                                            <label class="control-label">Jantina <span class="required">*</span></label>
                                            <select id="staffgender" name="staffgender" class="form-control">
                                                <option value="LELAKI" <%=(oModStaff.GetSetgender.Equals("LELAKI")?"selected":"") %>>LELAKI</option>
                                                <option value="PEREMPUAN" <%=(oModStaff.GetSetgender.Equals("PEREMPUAN")?"selected":"") %>>PEREMPUAN</option>
                                            </select>
                                            <label class="control-label">Bangsa <span class="required">*</span></label>
                                            <select id="staffrace" name="staffrace" class="form-control">
                                                <option value="">-Select-</option>
                                            </select>
                                            <label class="control-label">Agama <span class="required">*</span></label>
                                            <select id="staffreligion" name="staffreligion" class="form-control">
                                                <option value="">-Select-</option>
                                            </select>
                                            <label class="control-label">Status Perkahwinan <span class="required">*</span></label>
                                            <select id="staffmarital" name="staffmarital" class="form-control">
                                                <option value="KAHWIN" <%=(oModStaff.GetSetmarital.Equals("KAHWIN")?"selected":"") %>>KAHWIN</option>
                                                <option value="BUJANG" <%=(oModStaff.GetSetmarital.Equals("BUJANG")?"selected":"") %>>BUJANG</option>
                                                <option value="DUDA" <%=(oModStaff.GetSetmarital.Equals("DUDA")?"selected":"") %>>DUDA</option>
                                                <option value="JANDA" <%=(oModStaff.GetSetmarital.Equals("JANDA")?"selected":"") %>>JANDA</option>
                                            </select>
                                            <label class="control-label">Tarikh Perkahwinan (Jika berkaitan) </label>
                                            <input type="text" id="staffdtmarried" class="date-picker form-control" name="staffdtmarried" value="<%=oModStaff.GetSetdtmarried %>" />
                                            <label class="control-label">BANK (Emolumen/Gaji/Upah) </label>
                                            <input id="staffbankname" name="staffbankname" type="text" class="form-control" value="<%=oModStaff.GetSetbankname %>" />
                                            <label class="control-label">No. Akaun BANK </label>
                                            <input id="staffaccountno" name="staffaccountno" type="text" class="form-control" value="<%=oModStaff.GetSetaccountno %>" />
                                            <label class="control-label">No. Akaun KWSP </label>
                                            <input id="staffepfno" name="staffepfno" type="text" class="form-control" value="<%=oModStaff.GetSetepfno %>" />
                                            <label class="control-label">No. Akaun PERKESO</label>
                                            <input id="staffsocsono" name="staffsocsono" type="text" class="form-control" value="<%=oModStaff.GetSetsocsono %>" />
                                            <label class="control-label">No. Akaun LHDN (TAX)</label>
                                            <input id="stafftaxno" name="stafftaxno" type="text" class="form-control" value="<%=oModStaff.GetSettaxno %>" />
                                            <label class="control-label left">Umur Pesaraan</label>/ <label id="staffretiredate" name="staffretiredate" class="control-label right">Tarikh Pesaraan: </label>
                                            <input id="staffretireage" name="staffretireage" type="text" class="form-control" value="<%=oModStaff.GetSetretireage %>" />
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12" style="padding-top:20px;padding-bottom:20px;">
                                            <label class="control-label">User Id <span class="required">*</span></label>
                                            <input id="staffuserid" name="staffuserid" type="text" class="form-control" value="<%=oModStaff.GetSetuserid %>" />
                                            <label class="control-label">Password <span class="required">*</span></label>
                                            <input id="staffpassword" name="staffpassword" type="password" class="form-control" value="<%=oModStaff.GetSetpassword %>" />
                                            <label class="control-label">Status Login Sistem </label>
                                            <input id="staffstatuslogon" name="staffstatuslogon" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetstatuslogon %>" />                                            
                                            <label class="control-label">Akses Terakhir </label>
                                            <input id="stafflastaccess" name="stafflastaccess" type="text" class="form-control" readonly="readonly" style="background-color:gray; color:floralwhite;" value="<%=oModStaff.GetSetlastaccess %>" />
                                            <label class="control-label">Alamat Tetap<span class="required">*</span></label>
                                            <input id="staffpaddress1" name="staffpaddress1" type="text" class="form-control" value="<%=oModStaff.GetSetpaddress1 %>" />
                                            <input id="staffpaddress2" name="staffpaddress2" type="text" class="form-control" value="<%=oModStaff.GetSetpaddress2 %>" />
                                            <input id="staffpaddress3" name="staffpaddress3" type="text" class="form-control" value="<%=oModStaff.GetSetpaddress3 %>" />
                                            <input id="staffpaddress4" name="staffpaddress4" type="text" class="form-control" value="<%=oModStaff.GetSetpaddress4 %>" />
                                            <label class="control-label">Poskod<span class="required">*</span></label>
                                            <input id="staffppostcode" name="staffppostcode" type="text" class="form-control" value="<%=oModStaff.GetSetppostcode %>" />
                                            <label class="control-label">Bandar<span class="required">*</span></label>
                                            <input id="staffpcity" name="staffpcity" type="text" class="form-control" value="<%=oModStaff.GetSetpcity %>" />
                                            <label class="control-label">Negeri <span class="required">*</span></label>
                                            <select id="staffpstate" name="staffpstate" class="form-control">
                                                <option value="">-Select-</option>
                                            </select>
                                            <label class="control-label">Negara <span class="required">*</span></label>
                                            <select id="staffpcountry" name="staffpcountry" class="form-control">
                                                <option value="">-Select-</option>
                                            </select>
                                            <label class="control-label">Telefon</label>
                                            <input id="staffptelephone" name="staffptelephone" type="text" class="form-control" value="<%=oModStaff.GetSetptelephone %>" />
                                            <label class="control-label">Facebook Page</label>
                                            <input id="stafffacebook" name="stafffacebook" type="text" class="form-control" value="<%=oModStaff.GetSetfacebook %>" />
                                            <label class="control-label">Instagram Page</label>
                                            <input id="staffinstagram" name="staffinstagram" type="text" class="form-control" value="<%=oModStaff.GetSetinstagram %>" />
                                            <label class="control-label">WhatsApp Id</label>
                                            <input id="staffwhatsapp" name="staffwhatsapp" type="text" class="form-control" value="<%=oModStaff.GetSetwhatsapp %>" />
                                            <label class="control-label">Catatan </label>
                                            <textarea id="staffremarks" class="form-control" rows="2" name="staffremarks"><%=oModStaff.GetSetremarks %></textarea>
                                        </div>


                                    </div>
                                    <button type="button" id="btnUpdateDetails" name="btnUpdateDetails" class="btn btn-primary" onclick="updateStaffDetails()">Kemaskini</button>
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
            var proceed = true;
            if ($('#staffsalute').val() == "" || $('#staffsalute').val() == null || $('#staffsalute').val() == undefined) {
                proceed = false;
                alert("Sila isi Salutasi!");
            }
            else if ($('#staffdob').val() == "") {
                proceed = false;
                alert("Sila isi Tarikh Lahir!");
            }
            else if ($('#staffgender').val() == "" || $('#staffgender').val() == null || $('#staffgender').val() == undefined) {
                proceed = false;
                alert("Sila isi Jantina!");
            }
            else if ($('#staffrace').val() == null || $('#staffrace').val() == "" || $('#staffrace').val() == undefined) {
                proceed = false;
                alert("Sila isi Bangsa!");
            }
            else if ($('#staffreligion').val() == null || $('#staffreligion').val() == "" || $('#staffreligion').val() == undefined) {
                proceed = false;
                alert("Sila isi Agama!");
            }
            if (proceed) {
                var updateStaffDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", "<%=sStaffNo%>", "salute", $('#staffsalute').val(), "nickname", $('#staffnickname').val(), "nationality", $('#staffnationality').val(), "dob", $('#staffdob').val(), "birthplace", $('#staffbirthplace').val(), "gender", $('#staffgender').val(), "race", $('#staffrace').val(), "religion", $('#staffreligion').val(),
                    "marital", $('#staffmarital').val(), "dtmarried", $('#staffdtmarried').val(), "bankname", $('#staffbankname').val(), "accountno", $('#staffaccountno').val(), "epfno", $('#staffepfno').val(), "socsono", $('#staffsocsono').val(), "taxno", $('#stafftaxno').val(), "retireage", $('#staffretireage').val(), "userid", $('#staffuserid').val(), "password", $('#staffpassword').val(),
                    "paddress1", $('#staffpaddress1').val(), "paddress2", $('#staffpaddress2').val(), "paddress3", $('#staffpaddress3').val(), "paddress4", $('#staffpaddress4').val(), "ppostcode", $('#staffppostcode').val(), "pcity", $('#staffpcity').val(), "pstate", $('#staffpstate').val(), "pcountry", $('#staffpcountry').val(), "ptelephone", $('#staffptelephone').val(), "facebook", $('#stafffacebook').val(),
                    "instagram", $('#staffinstagram').val(), "whatsapp", $('#staffwhatsapp').val(), "remarks", $('#staffremarks').val()];
                PageMethod("updateStaffDetails", updateStaffDetails_parameters, updateStaffDetails_succeedAjaxFn, updateStaffDetails_failedAjaxFn, false);
            }
        }

        var updateStaffDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateStaffDetails_succeedAjaxFn: " + textStatus);
            var updateStaffDetails_result = JSON.parse(data.d);
            if (updateStaffDetails_result.result == "Y") {
                alert(updateStaffDetails_result.message);
                window.location.href = "CompStaffDetails.aspx?action=OPEN&comp=<%=sCurrComp%>&staffno=<%=sStaffNo%>";
                return false; //to check IsPostBack
            }
            else {
                alert(updateStaffDetails_result.message);
            }
        }

        var updateStaffDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateStaffDetails_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to update staff info...");
        }
    </script>

</asp:Content>

