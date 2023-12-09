<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendance.master" AutoEventWireup="true" CodeFile="AttendanceExcuseStaff.aspx.cs" Inherits="HumanResource_AttendanceExcuseStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server" enctype="multipart/form-data">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pengecualian Kehadiran Kakitangan</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindFyr" name="lsFindFyr">
                            <option value="2019" <%=sCurrFyr.Equals("2019") ? "selected" : "" %>>2019</option>
                            <option value="2020" <%=sCurrFyr.Equals("2020") ? "selected" : "" %>>2020</option>
                            <option value="2021" <%=sCurrFyr.Equals("2021") ? "selected" : "" %>>2021</option>
                            <option value="2022" <%=sCurrFyr.Equals("2022") ? "selected" : "" %>>2022</option>
                            <option value="2023" <%=sCurrFyr.Equals("2023") ? "selected" : "" %>>2023</option>
                            <option value="2024" <%=sCurrFyr.Equals("2024") ? "selected" : "" %>>2024</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">No. Pekerja:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindStaffNo" name="txtFindStaffNo" type="text" size="10" maxlength="10" value="<%=sStaffNo%>" class="input">
                        <div id="txtFindStaffNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Nama Pekerja:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindStaffName" name="txtFindStaffName" type="text" size="50" maxlength="50" value="<%=sStaffName%>" class="input">
                        <div id="txtFindStaffName-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kategori Pengecualian:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindExcCat" name="lsFindExcCat">
                            <option value="" <%=sExcCat.Equals("") ? "selected" : "" %>>-Select-</option>
                            <option value="PENGECUALIAN" <%=sExcCat.Equals("PENGECUALIAN") ? "selected" : "" %>>PENGECUALIAN</option>
                            <option value="TIDAK DAFTAR KEHADIRAN" <%=sExcCat.Equals("TIDAK DAFTAR KEHADIRAN") ? "selected" : "" %>>TIDAK DAFTAR KEHADIRAN</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Jenis Pengecualian:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindExcType" name="lsFindExcType">
                            <option value="">-Select-</option>
                        </select>
                    </td>
                </tr>
            </table>
            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td height="30" width="20%"></td>
                    <td width="80%" align="left">
                        <input class="button1" name="btnSearch" type="button" value="Carian" onclick="actionclick('SEARCH');">
                        <input class="button1" name="btnReset" type="button" value="Reset" onclick="actionclick('RESET');">
                    </td>
                </tr>
            </table>
            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Permohoanan Pengecualian Kehadiran</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Catatan</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsExcuseStaffAttendance.Count > 0)
                        {
                            for (int i = 0; i < lsExcuseStaffAttendance.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsExcuseStaffAttendance[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfromdate %> - <%=modItem.GetSettodate %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="left"><%=modItem.GetSetreason %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" class="button_warning enabled" data-action="delete">Hapus</button>
                            <button type="button" class="button_warning enabled" data-action="excuseday">Jadual Kehadiran</button>
                        </td>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="10" class="tblText2">&nbsp;No Record Found</td>
                    </tr>
                    <%
                        }
                    %>
                </tbody>
            </table>

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsExcuseStaffAttendance.Count %> record(s)</td>
                                <td width="50%" align="right">page
                                    <asp:DropDownList CssClass="select" ID="lsPageList" runat="server" OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    of <%=sTotalPage %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center">
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openExcStaff('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="ExcStaff" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbExcStaff" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Pengecualian Kehadiran</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtStaffNo" name="txtStaffNo" type="text" size="10" maxlength="10" value="" class="input">
                                            <div id="txtStaffNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffName" name="txtStaffName" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jawatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtGredPosition" name="txtGredPosition" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jabatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffDept" name="txtStaffDept" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtFyr" name="txtFyr" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori Pengecualian:</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsExcCat" name="lsExcCat">
                                                <option value="PENGECUALIAN">PENGECUALIAN</option>
                                                <option value="TIDAK DAFTAR KEHADIRAN">TIDAK DAFTAR KEHADIRAN</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis Pengecualian:</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsExcType" name="lsExcType">
                                                <option value="">-Select-</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDate" id="txtFromDate" type="text" value="" size="15" maxlength="20">
                                            <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFromTranDate">
                                            <script type="text/javascript">
                                                Calendar.setup({
                                                    inputField: "txtFromDate",     	// id of the input field
                                                    ifFormat: "%d-%m-%Y ",   	// format of the input field
                                                    button: "imgFromTranDate",  		// trigger for the calendar (image ID)
                                                    align: "B1",
                                                    singleClick: true
                                                });
                                            </script>
                                             - 
                                            <input class="input" name="txtToDate" id="txtToDate" type="text" value="" size="15" maxlength="20">
                                            <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgToTranDate">
                                            <script type="text/javascript">
                                                Calendar.setup({
                                                    inputField: "txtToDate",     	// id of the input field
                                                    ifFormat: "%d-%m-%Y ",   	// format of the input field
                                                    button: "imgToTranDate",  		// trigger for the calendar (image ID)
                                                    align: "B1",
                                                    singleClick: true
                                                });
                                            </script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Masa</td>
                                        <td width="80%" class="tblText2">
                                            <input type="time" id="timein" value="08:00:00" class="timein" step="1">
                                            -
                                            <input type="time" id="timeout" value="17:00:00" class="timeout" step="1">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Lampiran</td>
                                        <td width="80%" class="tblText2">
                                            <input id="FileUpload1" name="FileUpload1" class="input" type="file" size="45" runat="server"/>
                                            <br />
                                            <p id="FolderFile1">
                                                <a id="FileAttached1" name="FileAttached1" href="#"></a>
                                                <button id="BtnAttached1" name="BtnAttached1" type="button" class="btn-small btn-danger enabled">Hapus</button>
                                                <br />
                                            </p>
                                            <input id="FileUpload2" name="FileUpload2" class="input" type="file" size="45" runat="server"/>
                                            <br />
                                            <p id="FolderFile2">
                                                <a id="FileAttached2" name="" href="#"></a>
                                                <button id="BtnAttached2" name="BtnAttached2" type="button" class="btn-small btn-danger enabled">Hapus</button>
                                                <br />
                                            </p>
                                            <input id="FileUpload3" name="FileUpload3" class="input" type="file" size="45" runat="server"/>
                                            <br />
                                            <p id="FolderFile3">
                                                <a id="FileAttached3" name="" href="#"></a>
                                                <button id="BtnAttached3" name="BtnAttached3" type="button" class="btn-small btn-danger enabled">Hapus</button>
                                                <br />
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <option value="PERMOHONAN">PERMOHONAN</option>
                                                <option value="PENGESAHAN">PENGESAHAN</option>
                                                <option value="LULUS">LULUS</option>
                                                <option value="DITOLAK">DITOLAK</option>
                                                <option value="BATAL">BATAL</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Catatan</td>
                                        <td width="80%" class="tblText2">
                                            <textarea id="txtReason" name="txtReason" class="form-control" cols="50" rows="3"></textarea>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertExcStaff();">Tambah</button>
                                            <button id="btnResetItem" name="btnResetItem"type="button" class="button1 btn-warning" onclick="resetExcStaff();">Reset</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateExcStaff();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeExcStaff();">Tutup</button>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </form>

    </div>
    <script type="text/javascript">

        var StaffNoArray = [];
        var maxlengthdataautocomplete = 20;
        var StaffNameArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindStaffNo").value = "";
                document.getElementById("txtFindStaffName").value = "";
                document.getElementById("lsFindExcCat").selectedIndex = 0;
                document.getElementById("lsFindExcType").selectedIndex = 0;
            }
            if (action == "INSERT") {

            }
            if (action == "UPDATE") {

            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getStaffEmployList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getStaffEmployList", getStaffEmployList_parameters, getStaffEmployList_succeedAjaxFn, getStaffEmployList_failedAjaxFn, false);

        });

        var getStaffEmployList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffEmployList_succeedAjaxFn: " + textStatus);
            var getStaffEmployList_result = JSON.parse(data.d);
            if (getStaffEmployList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getStaffEmployList_result.itemlist, function (i, result) {
                    if (itemno != result.GetSetstaffno + '-' + result.GetSetname) {
                        var objData = {};
                        objData.value = result.GetSetstaffno + '-' + result.GetSetname;
                        objData.data = result.GetSetstaffno;
                        StaffNoArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemno = result.GetSetstaffno + '-' + result.GetSetname;
                    }

                    if (itemdesc != result.GetSetname) {
                        var objData = {};
                        objData.value = result.GetSetname;
                        objData.data = result.GetSetname;
                        StaffNameArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemdesc = result.GetSetname;
                    }

                });
            }
            else {
                console.log("getStaffEmployList_result.result: " + getStaffEmployList_result.result);
            }
        }

        var getStaffEmployList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindStaffNo').autocomplete({
            lookup: StaffNoArray,
            appendTo: '#txtFindStaffNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindStaffNo').val(suggestion.data);
            }
        });

        $('#txtFindStaffName').autocomplete({
            lookup: StaffNameArray,
            appendTo: '#txtFindStaffName-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindStaffName').val(suggestion.data);
            }
        });

        $('#txtStaffNo').autocomplete({
            lookup: StaffNoArray,
            appendTo: '#txtStaffNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtStaffNo').val(suggestion.data);
                var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val(), "id", 0];
                PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);
            }
        });

        $('#txtStaffNo').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val()];
            PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);

        });

        //BEGIN Response for getStaffEmployDetails
        getStaffEmployDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffEmployDetails_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStaffEmployDetails = JSON.parse(data.d);
            if (result_getStaffEmployDetails.result == "Y") {

                $('#txtStaffName').val(result_getStaffEmployDetails.staffemploy.GetSetname);
                $('#txtGredPosition').val(result_getStaffEmployDetails.staffemploy.GetSetpos_name + " [" + result_getStaffEmployDetails.staffemploy.GetSetgred_name + "]");
                $('#txtStaffDept').val(result_getStaffEmployDetails.staffemploy.GetSetdept_name);

            } else {
                //alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        $('#lsFindExcCat').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            var getExcuseTypeList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "exccategory", $('#lsFindExcCat').val(), "exctype", ""];
            PageMethod("getExcuseTypeList", getExcuseTypeList_parameters, getExcuseTypeList_succeedAjaxFn, getExcuseTypeList_failedAjaxFn, false);

        });

        var getExcuseTypeList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getExcuseTypeList_succeedAjaxFn: " + textStatus);
            var getExcuseTypeList_result = JSON.parse(data.d);
            if (getExcuseTypeList_result.result == "Y") {
                var output = "";
                $.each(getExcuseTypeList_result.itemlist, function (i, result) {
                    if (result.GetSettype == "<%=sExcType%>") {
                        output += "<option value='" + result.GetSettype + "' selected>" + result.GetSettype + "</option>";
                    } else {
                        output += "<option value='" + result.GetSettype + "'>" + result.GetSettype + "</option>";
                    }
                });

            }
            else {
                output += "<option value=''>-Select-</option>";
                console.log("getExcuseTypeList_result.result: " + getExcuseTypeList_result.result);
            }
            $('#lsFindExcType').html("").append(output);
        }

        var getExcuseTypeList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getExcuseTypeList_failedAjaxFn: " + textStatus);
        }

        $('#lsExcCat').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            var getExcuseTypeList2_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "exccategory", $('#lsExcCat').val(), "exctype", ""];
            PageMethod("getExcuseTypeList", getExcuseTypeList2_parameters, getExcuseTypeList2_succeedAjaxFn, getExcuseTypeList2_failedAjaxFn, false);

        });

        var getExcuseTypeList2_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getExcuseTypeList2_succeedAjaxFn: " + textStatus);
            var getExcuseTypeList2_result = JSON.parse(data.d);
            if (getExcuseTypeList2_result.result == "Y") {
                var output = "";
                $.each(getExcuseTypeList2_result.itemlist, function (i, result) {
                    output += "<option value='" + result.GetSettype + "'>" + result.GetSettype + "</option>";
                });

            }
            else {
                output += "<option value=''>-Select-</option>";
                console.log("getExcuseTypeList2_result.result: " + getExcuseTypeList2_result.result);
            }
            $('#lsExcType').html("").append(output);
        }

        var getExcuseTypeList2_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getExcuseTypeList2_failedAjaxFn: " + textStatus);
        }

        $('#mytable').on('click', function (e) {
            var target = e && e.target || event.srcElement

            //alert(target); //show htmldocument on javascript
            //to use jquery function must add $ into target

            //to getstatus
            //alert('checkbox:' + $(target).prop('checked'));
            //to uncheck
            //$(target).prop('checked', false);
            //to check
            //$(target).prop('checked', true);

            var trid = $(target).closest("[data-id]");
            //get data-accid value for the TR
            var id = (trid.data("id"));

            var action = target.getAttribute('data-action')
            if (action) {
                //alert(action);
                if (action == 'delete') {
                    //alert('delete:' + id);
                    deleteExcStaff(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openExcStaff('OPEN', id);
                }
                else if (action == 'excuseday') {
                    //alert('edit:' + id);
                    openWGItemList('OPEN', id);
                }
            }
        });

        function deleteExcStaff(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteStaffExcuse_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteStaffExcuse", deleteStaffExcuse_parameters, deleteStaffExcuse_succeedAjaxFn, deleteStaffExcuse_failedAjaxFn, false);
            }
        }

        var deleteStaffExcuse_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteStaffExcuse_succeedAjaxFn: " + textStatus);
            var deleteStaffExcuse_result = JSON.parse(data.d);
            if (deleteStaffExcuse_result.result == "Y") {
                //alert(deleteStaffExcuse_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteStaffExcuse_result.message);
            }
        }

        var deleteStaffExcuse_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteStaffExcuse_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Staff Excuse...");
        }

        function openExcStaff(typ, id) {
            $('#ExcStaff').modal({ backdrop: "static" });

            $('#txtFyr').val("<%=sCurrFyr%>");
            var getExcuseTypeList2_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "exccategory", $('#lsExcCat').val(), "exctype", ""];
            PageMethod("getExcuseTypeList", getExcuseTypeList2_parameters, getExcuseTypeList2_succeedAjaxFn, getExcuseTypeList2_failedAjaxFn, false);

            $('#FolderFile1').hide();
            $('#FolderFile2').hide();
            $('#FolderFile3').hide();

            if (typ == 'OPEN') {
                $('#btnAddItem').hide();
                $('#btnResetItem').hide();                
                $('#btnModifyItem').show();

                var getStaffExcuseDetails_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getStaffExcuseDetails", getStaffExcuseDetails_parameters, getStaffExcuseDetails_succeedAjaxFn, getStaffExcuseDetails_failedAjaxFn, false);
            } else {

                $('#btnAddItem').show();
                $('#btnResetItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getStaffExcuseDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffExcuseDetails_succeedAjaxFn: " + textStatus);
            var getStaffExcuseDetails_result = JSON.parse(data.d);
            if (getStaffExcuseDetails_result.result == "Y") {
                $('#hidId').val(getStaffExcuseDetails_result.itemdetail.GetSetid);
                $('#txtStaffNo').val(getStaffExcuseDetails_result.itemdetail.GetSetstaffno);
                $('#txtStaffName').val(getStaffExcuseDetails_result.itemdetail.GetSetname);
                $('#txtGredPosition').val(getStaffExcuseDetails_result.itemdetail.GetSetpos_name + " [" + getStaffExcuseDetails_result.itemdetail.GetSetgred_name + "]");
                $('#txtStaffDept').val(getStaffExcuseDetails_result.itemdetail.GetSetdept_name);
                $('#txtFyr').val(getStaffExcuseDetails_result.itemdetail.GetSetfyr);
                $('#lsExcCat').val(getStaffExcuseDetails_result.itemdetail.GetSetcat);
                $('#lsExcType').val(getStaffExcuseDetails_result.itemdetail.GetSettype);
                $('#txtFromDate').val(getStaffExcuseDetails_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getStaffExcuseDetails_result.itemdetail.GetSettodate);
                $('#timein').val(getStaffExcuseDetails_result.itemdetail.GetSetfromtime);
                $('#timeout').val(getStaffExcuseDetails_result.itemdetail.GetSettotime);
                if (getStaffExcuseDetails_result.itemdetail.GetSetfilename1.length > 0) {
                    //$("#FileAttached1").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename1 + "')");
                    $("#FileAttached1").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename1); })
                    $("#FileAttached1").text(getStaffExcuseDetails_result.itemdetail.GetSetfilename1);
                    $("#BtnAttached1").on("click", function () { removeAttachedExcStaff("1", getStaffExcuseDetails_result.itemdetail.GetSetfilename1); })
                    $('#FolderFile1').show();
                }
                if (getStaffExcuseDetails_result.itemdetail.GetSetfilename2.length > 0) {
                    //$("#FileAttached2").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename2 + "')");
                    //$("#FileAttached2").prop("href", "../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename2);
                    $("#FileAttached2").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename2); })
                    $("#FileAttached2").text(getStaffExcuseDetails_result.itemdetail.GetSetfilename2);
                    $("#BtnAttached2").on("click", function () { removeAttachedExcStaff("2", getStaffExcuseDetails_result.itemdetail.GetSetfilename2); })
                    $('#FolderFile2').show();
                }
                if (getStaffExcuseDetails_result.itemdetail.GetSetfilename3.length > 0) {
                    //$("#FileAttached3").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename3 + "')");
                    //$("#FileAttached3").prop("href", "../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename3);
                    $("#FileAttached3").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffExcuseDetails_result.itemdetail.GetSetfilename3); })
                    $("#FileAttached3").text(getStaffExcuseDetails_result.itemdetail.GetSetfilename3);
                    $("#BtnAttached3").on("click", function () { removeAttachedExcStaff("3", getStaffExcuseDetails_result.itemdetail.GetSetfilename3); })
                    $('#FolderFile3').show();
                }
                $('#lsStatus').val(getStaffExcuseDetails_result.itemdetail.GetSetstatus);
                $('#txtReason').val(getStaffExcuseDetails_result.itemdetail.GetSetreason);
            }
            else {
                alert(getStaffExcuseDetails_result.message);
            }
        }

        var getStaffExcuseDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffExcuseDetails_failedAjaxFn: " + textStatus);
        }

        function insertExcStaff() {
            var formData = new FormData();
            formData.append('inputitem[0]', $('#lsFindFyr').val());
            formData.append('inputitem[1]', $('#txtStaffNo').val());
            formData.append('inputitem[2]', $('#lsExcCat').val());
            formData.append('inputitem[3]', $('#lsExcType').val());
            formData.append('inputitem[4]', $('#txtFromDate').val());
            formData.append('inputitem[5]', $('#txtToDate').val());
            formData.append('inputitem[6]', $('#lsStatus').val());
            formData.append('inputitem[7]', $('#txtReason').val());
            formData.append("inputitem[8]", $('input[type="file"]')[0].files[0]);
            formData.append("inputitem[9]", $('input[type="file"]')[1].files[0]);
            formData.append("inputitem[10]", $('input[type="file"]')[2].files[0]);
            formData.append('inputitem[11]', $('#timein').val());
            formData.append('inputitem[12]', $('#timeout').val());

            if ($('#lsFindFyr').val().length > 0 && $('#txtStaffNo').val().length > 0 && $('#lsExcCat').val().length > 0 && $('#lsExcType').val().length > 0 && $('#txtFromDate').val().length > 0 && $('#txtToDate').val().length > 0) {
                
                $.ajax({
                    type: 'POST',
                    url: "../WebService.asmx/insertStaffExcuse",
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-insert: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            actionclick('SEARCH');
                        }
                        else {
                            alert(result.message);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function () {
                        alert("Internal Server Error!");
                    }
                });
                
            } else {
                alert("Maklumat tidak lengkap!");
            }
        }

        function updateExcStaff() {
            var formData = new FormData();
            formData.append('inputitem[0]', $('#hidId').val());
            formData.append('inputitem[1]', $('#txtStaffNo').val());
            formData.append('inputitem[2]', $('#lsExcCat').val());
            formData.append('inputitem[3]', $('#lsExcType').val());
            formData.append('inputitem[4]', $('#txtFromDate').val());
            formData.append('inputitem[5]', $('#txtToDate').val());
            formData.append('inputitem[6]', $('#lsStatus').val());
            formData.append('inputitem[7]', $('#txtReason').val());
            formData.append("inputitem[8]", $('input[type="file"]')[0].files[0]);
            formData.append("inputitem[9]", $('input[type="file"]')[1].files[0]);
            formData.append("inputitem[10]", $('input[type="file"]')[2].files[0]);
            formData.append('inputitem[11]', $('#timein').val());
            formData.append('inputitem[12]', $('#timeout').val());

            if ($('#lsFindFyr').val().length > 0 && $('#txtStaffNo').val().length > 0 && $('#lsExcCat').val().length > 0 && $('#lsExcType').val().length > 0 && $('#txtFromDate').val().length > 0 && $('#txtToDate').val().length > 0) {

                $.ajax({
                    type: 'POST',
                    url: "../WebService.asmx/updateStaffExcuse",
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-insert: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            actionclick('SEARCH');
                        }
                        else {
                            alert(result.message);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function () {
                        alert("Internal Server Error!");
                    }
                });

            } else {
                alert("Maklumat tidak lengkap!");
            }
        }

        function removeAttachedExcStaff(filenumber, filename) {
            var removeAttachedStaffExcuse_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "filenumber", filenumber, "filename", filename];
            PageMethod("removeAttachedStaffExcuse", removeAttachedStaffExcuse_parameters, removeAttachedStaffExcuse_succeedAjaxFn, removeAttachedStaffExcuse_failedAjaxFn, false);
        }

        var removeAttachedStaffExcuse_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("removeAttachedStaffExcuse_succeedAjaxFn: " + textStatus);
            var removeAttachedStaffExcuse_result = JSON.parse(data.d);
            if (removeAttachedStaffExcuse_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(removeAttachedStaffExcuse_result.message);
            }
        }

        var removeAttachedStaffExcuse_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("removeAttachedStaffExcuse_failedAjaxFn: " + textStatus);
        }

        function closeExcStaff() {
            resetExcStaff();
            $('#ExcStaff').modal('hide');
        }

        function resetExcStaff() {
            $('#hidId').val("");
            $('#txtStaffNo').val("");
            $('#txtStaffName').val("");
            $('#txtStaffDept').val("");
            $('#txtGredPosition').val("");
            $('#txtFyr').val("<%=sCurrFyr%>");
            document.getElementById("lsExcCat").selectedIndex = 0;
            document.getElementById("lsExcType").selectedIndex = 0;
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            document.getElementById("lsStatus").selectedIndex = 0;
        }

        function openWGItemList(typ, id) {
            if (typ == "OPEN") {
                fOpenWindow('AttendanceWorkingGeneralTableStaff.aspx?fyr=' + $('#lsFindFyr').val() + '&id=' + id + '&typ=FROM_EXCUSE');
            }
        }

        function openAttachement(url) {
            fOpenWindow(url);
        }
    </script>
</asp:Content>

