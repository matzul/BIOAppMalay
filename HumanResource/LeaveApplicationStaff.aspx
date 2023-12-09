<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageLeave.master" AutoEventWireup="true" CodeFile="LeaveApplicationStaff.aspx.cs" Inherits="HumanResource_LeaveApplicationStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server" enctype="multipart/form-data">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pemohonan Cuti Kakitangan</a></td>
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
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindLeaveCat" name="lsFindLeaveCat">
                            <option value="" <%=sLeaveCat.Equals("") ? "selected" : "" %>>-Select-</option>
                            <%
                                for (int i = 0; i < lsGredComp.Count; i++)
                                {
                                    HRModel modItem = (HRModel)lsGredComp[i];
                            %>
                            <option value="<%=modItem.GetSetname %>" <%=sLeaveCat.Equals(modItem.GetSetname) ? "selected" : "" %>><%=modItem.GetSetname %></option>
                            <%
                                }
                            %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Jenis Cuti:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindLeaveType" name="lsFindLeaveType">
                            <option value="" <%=sLeaveType.Equals("") ? "selected" : "" %>>-Select-</option>
                            <option value="CUTI TAHUNAN" <%=sLeaveType.Equals("CUTI TAHUNAN") ? "selected" : "" %>>CUTI TAHUNAN</option>
                            <option value="CUTI KECEMASAN" <%=sLeaveType.Equals("CUTI KECEMASAN") ? "selected" : "" %>>CUTI KECEMASAN</option>
                            <option value="CUTI SAKIT (MC)" <%=sLeaveType.Equals("CUTI SAKIT (MC)") ? "selected" : "" %>>CUTI SAKIT (MC)</option>
                            <option value="CUTI GANTI" <%=sLeaveType.Equals("CUTI GANTI") ? "selected" : "" %>>CUTI GANTI</option>
                            <option value="CUTI TANPA REKOD" <%=sLeaveType.Equals("CUTI TANPA REKOD") ? "selected" : "" %>>CUTI TANPA REKOD</option>
                            <option value="CUTI KUARANTIN" <%=sLeaveType.Equals("CUTI KUARANTIN") ? "selected" : "" %>>CUTI KUARANTIN</option>
                            <option value="CUTI HAJI" <%=sLeaveType.Equals("CUTI HAJI") ? "selected" : "" %>>CUTI HAJI</option>
                            <option value="CUTI UMRAH" <%=sLeaveType.Equals("CUTI UMRAH") ? "selected" : "" %>>CUTI UMRAH</option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Permohoanan Cuti Kakitangan</a></td>
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
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jenis Cuti</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Masa</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Catatan</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsLeaveStaffApplication.Count > 0)
                        {
                            for (int i = 0; i < lsLeaveStaffApplication.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsLeaveStaffApplication[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfromdate %> - <%=modItem.GetSettodate %></td>
                        <td valign="top" align="left"><%=modItem.GetSetvariety == 1 ? "SATU(1) HARI" : modItem.GetSetvariety == 2 ? "SEPARUH(PAGI) HARI" : "SEPARUH(PETANG) HARI" %><br /><%=modItem.GetSetfromtime %> - <%=modItem.GetSettotime %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="left"><%=modItem.GetSetreason %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" class="button_warning enabled" data-action="delete">Hapus</button>
                            <button type="button" class="button_warning enabled" data-action="leaveday">Jadual Kehadiran</button>
                        </td>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="12" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsLeaveStaffApplication.Count %> record(s)</td>
                                <td width="50%" align="right">page
                                    <asp:DropDownList CssClass="select" ID="lsPageList" runat="server" OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    of <%=sTotalPage %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonFind" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center">
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openLeaveStaff('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="LeaveStaff" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbLeaveStaff" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
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
                                        <td width="20%" class="tblTextCommon">Kategori:</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtLeaveCat" name="txtLeaveCat" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis Cuti:</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsLeaveType" name="lsLeaveType">
                                                <option value="">-Select-</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Pilihan:</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsLeaveVariety" name="lsLeaveVariety">
                                                <option value="1">SATU(1) HARI</option>
                                                <option value="2">SEPARUH(PAGI) HARI</option>
                                                <option value="3">SEPARUH(PETANG) HARI</option>
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
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonAdd" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertLeaveStaff();">Tambah</button>
                                            <button id="btnResetItem" name="btnResetItem"type="button" class="button1 btn-warning" onclick="resetLeaveStaff();">Reset</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateLeaveStaff();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeLeaveStaff();">Tutup</button>
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
                document.getElementById("lsFindLeaveCat").selectedIndex = 0;
                document.getElementById("lsFindLeaveType").selectedIndex = 0;
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
                var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val()];
                PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);
            }
        });

        $('#txtStaffNo').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val(), "id", 0];
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
                $('#txtLeaveCat').val(result_getStaffEmployDetails.staffemploy.GetSetgred_name);

                var getStaffLGList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "staffno", result_getStaffEmployDetails.staffemploy.GetSetstaffno, "status", "ACTIVE"];
                PageMethod("getStaffLGList", getStaffLGList_parameters, getStaffLGList_succeedAjaxFn, getStaffLGList_failedAjaxFn, false);


            } else {
                //alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        var getStaffLGList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffLGList_succeedAjaxFn: " + textStatus);
            var getStaffLGList_result = JSON.parse(data.d);
            if (getStaffLGList_result.result == "Y") {
                var output = "";
                $.each(getStaffLGList_result.itemlist, function (i, result) {
                    output += "<option value='" + result.GetSettype + "'>" + result.GetSettype + "</option>";
                });

            }
            else {
                output += "<option value=''>-Select-</option>";
                console.log("getStaffLGList_result.result: " + getStaffLGList_result.result);
            }
            $('#lsLeaveType').html("").append(output);
        }

        var getStaffLGList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffLGList_failedAjaxFn: " + textStatus);
        }

        $('#lsLeaveVariety').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';
        }).on('blur', function () {
            setTimeInOut();
        }).on('select', function () {
            setTimeInOut();
        });

        function setTimeInOut() {
            if ($('#lsStatus').val() == "1") {
                $('#timein').val("08:00:00");
                $('#timein').val("17:00:00");
            } else if ($('#lsStatus').val() == "2") {
                $('#timein').val("08:00:00");
                $('#timein').val("13:00:00");
            } else if ($('#lsStatus').val() == "3") {
                $('#timein').val("13:00:00");
                $('#timein').val("17:00:00");
            }
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
                    deleteLeaveStaff(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openLeaveStaff('OPEN', id);
                }
                else if (action == 'leaveday') {
                    //alert('edit:' + id);
                    openWGItemList('OPEN', id);
                }
            }
        });

        function deleteLeaveStaff(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteStaffLeave_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteStaffLeave", deleteStaffLeave_parameters, deleteStaffLeave_succeedAjaxFn, deleteStaffLeave_failedAjaxFn, false);
            }
        }

        var deleteStaffLeave_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteStaffLeave_succeedAjaxFn: " + textStatus);
            var deleteStaffLeave_result = JSON.parse(data.d);
            if (deleteStaffLeave_result.result == "Y") {
                //alert(deleteStaffLeave_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteStaffLeave_result.message);
            }
        }

        var deleteStaffLeave_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteStaffLeave_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Staff Excuse...");
        }

        function openLeaveStaff(typ, id) {
            $('#LeaveStaff').modal({ backdrop: "static" });

            $('#txtFyr').val("<%=sCurrFyr%>");

            $('#FolderFile1').hide();
            $('#FolderFile2').hide();
            $('#FolderFile3').hide();

            if (typ == 'OPEN') {
                $('#btnAddItem').hide();
                $('#btnResetItem').hide();
                $('#btnModifyItem').show();

                var getStaffLeaveDetails_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getStaffLeaveDetails", getStaffLeaveDetails_parameters, getStaffLeaveDetails_succeedAjaxFn, getStaffLeaveDetails_failedAjaxFn, false);
            } else {

                $('#btnAddItem').show();
                $('#btnResetItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getStaffLeaveDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffLeaveDetails_succeedAjaxFn: " + textStatus);
            var getStaffLeaveDetails_result = JSON.parse(data.d);
            if (getStaffLeaveDetails_result.result == "Y") {

                var getStaffLGList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "staffno", getStaffLeaveDetails_result.itemdetail.GetSetstaffno, "status", "ACTIVE"];
                PageMethod("getStaffLGList", getStaffLGList_parameters, getStaffLGList_succeedAjaxFn, getStaffLGList_failedAjaxFn, false);

                $('#hidId').val(getStaffLeaveDetails_result.itemdetail.GetSetid);
                $('#txtStaffNo').val(getStaffLeaveDetails_result.itemdetail.GetSetstaffno);
                $('#txtStaffName').val(getStaffLeaveDetails_result.itemdetail.GetSetname);
                $('#txtGredPosition').val(getStaffLeaveDetails_result.itemdetail.GetSetpos_name + " [" + getStaffLeaveDetails_result.itemdetail.GetSetgred_name + "]");
                $('#txtStaffDept').val(getStaffLeaveDetails_result.itemdetail.GetSetdept_name);
                $('#txtFyr').val(getStaffLeaveDetails_result.itemdetail.GetSetfyr);
                $('#txtLeaveCat').val(getStaffLeaveDetails_result.itemdetail.GetSetcat);
                $('#lsLeaveType').val(getStaffLeaveDetails_result.itemdetail.GetSettype);
                $('#txtFromDate').val(getStaffLeaveDetails_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getStaffLeaveDetails_result.itemdetail.GetSettodate);
                $('#lsLeaveVariety').val(getStaffLeaveDetails_result.itemdetail.GetSetvariety);
                $('#timein').val(getStaffLeaveDetails_result.itemdetail.GetSetfromtime);
                $('#timeout').val(getStaffLeaveDetails_result.itemdetail.GetSettotime);
                if (getStaffLeaveDetails_result.itemdetail.GetSetfilename1.length > 0) {
                    //$("#FileAttached1").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename1 + "')");
                    $("#FileAttached1").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename1); })
                    $("#FileAttached1").text(getStaffLeaveDetails_result.itemdetail.GetSetfilename1);
                    $("#BtnAttached1").on("click", function () { removeAttachedLeaveStaff("1", getStaffLeaveDetails_result.itemdetail.GetSetfilename1); })
                    $('#FolderFile1').show();
                }
                if (getStaffLeaveDetails_result.itemdetail.GetSetfilename2.length > 0) {
                    //$("#FileAttached2").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename2 + "')");
                    //$("#FileAttached2").prop("href", "../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename2);
                    $("#FileAttached2").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename2); })
                    $("#FileAttached2").text(getStaffLeaveDetails_result.itemdetail.GetSetfilename2);
                    $("#BtnAttached2").on("click", function () { removeAttachedLeaveStaff("2", getStaffLeaveDetails_result.itemdetail.GetSetfilename2); })
                    $('#FolderFile2').show();
                }
                if (getStaffLeaveDetails_result.itemdetail.GetSetfilename3.length > 0) {
                    //$("#FileAttached3").prop("onclick", "openAttachement('../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename3 + "')");
                    //$("#FileAttached3").prop("href", "../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename3);
                    $("#FileAttached3").on("click", function () { fOpenWindow("../Attachment/HumanResource/" + getStaffLeaveDetails_result.itemdetail.GetSetfilename3); })
                    $("#FileAttached3").text(getStaffLeaveDetails_result.itemdetail.GetSetfilename3);
                    $("#BtnAttached3").on("click", function () { removeAttachedLeaveStaff("3", getStaffLeaveDetails_result.itemdetail.GetSetfilename3); })
                    $('#FolderFile3').show();
                }
                $('#lsStatus').val(getStaffLeaveDetails_result.itemdetail.GetSetstatus);
                $('#txtReason').val(getStaffLeaveDetails_result.itemdetail.GetSetreason);
            }
            else {
                alert(getStaffLeaveDetails_result.message);
            }
        }

        var getStaffLeaveDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffLeaveDetails_failedAjaxFn: " + textStatus);
        }

        function insertLeaveStaff() {
            var formData = new FormData();
            formData.append('inputitem[0]', $('#lsFindFyr').val());
            formData.append('inputitem[1]', $('#txtStaffNo').val());
            formData.append('inputitem[2]', $('#txtLeaveCat').val());
            formData.append('inputitem[3]', $('#lsLeaveType').val());
            formData.append('inputitem[4]', $('#txtFromDate').val());
            formData.append('inputitem[5]', $('#txtToDate').val());
            formData.append('inputitem[6]', $('#lsStatus').val());
            formData.append('inputitem[7]', $('#txtReason').val());
            formData.append("inputitem[8]", $('input[type="file"]')[0].files[0]);
            formData.append("inputitem[9]", $('input[type="file"]')[1].files[0]);
            formData.append("inputitem[10]", $('input[type="file"]')[2].files[0]);
            formData.append('inputitem[11]', $('#timein').val());
            formData.append('inputitem[12]', $('#timeout').val());
            formData.append('inputitem[13]', $('#lsLeaveVariety').val());

            if ($('#lsFindFyr').val().length > 0 && $('#txtStaffNo').val().length > 0 && $('#txtLeaveCat').val().length > 0 && $('#lsLeaveType').val().length > 0 && $('#txtFromDate').val().length > 0 && $('#txtToDate').val().length > 0) {
                
                $.ajax({
                    type: 'POST',
                    url: "../WebService.asmx/insertStaffLeave",
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

        function updateLeaveStaff() {
            var formData = new FormData();
            formData.append('inputitem[0]', $('#hidId').val());
            formData.append('inputitem[1]', $('#txtStaffNo').val());
            formData.append('inputitem[2]', $('#txtLeaveCat').val());
            formData.append('inputitem[3]', $('#lsLeaveType').val());
            formData.append('inputitem[4]', $('#txtFromDate').val());
            formData.append('inputitem[5]', $('#txtToDate').val());
            formData.append('inputitem[6]', $('#lsStatus').val());
            formData.append('inputitem[7]', $('#txtReason').val());
            formData.append("inputitem[8]", $('input[type="file"]')[0].files[0]);
            formData.append("inputitem[9]", $('input[type="file"]')[1].files[0]);
            formData.append("inputitem[10]", $('input[type="file"]')[2].files[0]);
            formData.append('inputitem[11]', $('#timein').val());
            formData.append('inputitem[12]', $('#timeout').val());
            formData.append('inputitem[13]', $('#lsLeaveVariety').val());

            if ($('#lsFindFyr').val().length > 0 && $('#txtStaffNo').val().length > 0 && $('#txtLeaveCat').val().length > 0 && $('#lsLeaveType').val().length > 0 && $('#txtFromDate').val().length > 0 && $('#txtToDate').val().length > 0) {

                $.ajax({
                    type: 'POST',
                    url: "../WebService.asmx/updateStaffLeave",
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

        function removeAttachedLeaveStaff(filenumber, filename) {
            var removeAttachedStaffLeave_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "filenumber", filenumber, "filename", filename];
            PageMethod("removeAttachedStaffLeave", removeAttachedStaffLeave_parameters, removeAttachedStaffLeave_succeedAjaxFn, removeAttachedStaffLeave_failedAjaxFn, false);
        }

        var removeAttachedStaffLeave_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("removeAttachedStaffLeave_succeedAjaxFn: " + textStatus);
            var removeAttachedStaffLeave_result = JSON.parse(data.d);
            if (removeAttachedStaffLeave_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(removeAttachedStaffLeave_result.message);
            }
        }

        var removeAttachedStaffLeave_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("removeAttachedStaffLeave_failedAjaxFn: " + textStatus);
        }

        function closeLeaveStaff() {
            resetLeaveStaff();
            $('#LeaveStaff').modal('hide');
        }

        function resetLeaveStaff() {
            $('#hidId').val("");
            $('#txtStaffNo').val("");
            $('#txtStaffName').val("");
            $('#txtStaffDept').val("");
            $('#txtGredPosition').val("");
            $('#txtFyr').val("<%=sCurrFyr%>");
            $('#txtLeaveCat').val("");
            document.getElementById("lsLeaveType").selectedIndex = 0;
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            document.getElementById("lsLeaveVariety").selectedIndex = 0;
            $('#timein').val("08:00:00");
            $('#timeout').val("17:00:00");
            document.getElementById("lsStatus").selectedIndex = 0;
        }

        function openWGItemList(typ, id) {
            if (typ == "OPEN") {
                fOpenWindow('AttendanceWorkingGeneralTableStaff.aspx?fyr=' + $('#lsFindFyr').val() + '&id=' + id + '&typ=FROM_LEAVE');
            }
        }

        function openAttachement(url) {
            fOpenWindow(url);
        }
    </script>
</asp:Content>

