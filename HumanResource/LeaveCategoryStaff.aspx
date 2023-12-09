<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageLeave.master" AutoEventWireup="true" CodeFile="LeaveCategoryStaff.aspx.cs" Inherits="HumanResource_LeaveCategoryStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kakitangan Kepada Kategori Cuti</a></td>
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
                            <option value="">-Select-</option>
                            <%
                                for (int i = 0; i < lsGredComp.Count; i++)
                                {
                                    HRModel modItem = (HRModel)lsGredComp[i];
                            %>
                            <option value="<%=modItem.GetSetname %>"><%=modItem.GetSetname %></option>
                            <%
                                }
                            %>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kakitangan & Kategori Cuti</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bermula</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Sehingga</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Cuti Bawa</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Cuti Tahunan</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Jumlah Cuti</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Cuti Diambil</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsStaffLeaveGroup.Count > 0)
                        {
                            for (int i = 0; i < lsStaffLeaveGroup.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsStaffLeaveGroup[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfromdate %></td>
                        <td valign="top" align="left"><%=modItem.GetSettodate %></td>
                        <td valign="top" align="left"><%=modItem.GetSetbrought %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcount %></td>
                        <td valign="top" align="left"><%=modItem.GetSetbrought + modItem.GetSetcount %></td>
                        <td valign="top" align="left"><%=modItem.GetSettaken %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" class="button_warning enabled" data-action="delete">Hapus</button>
                        </td>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="13" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsStaffLeaveGroup.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openLGStaffUpdate('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="LGStaffAdd" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbLGStaffAdd" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Kumpulan Kehadiran</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtStaffNoAdd" name="txtStaffNoAdd" type="text" size="10" maxlength="10" value="" class="input">
                                            <div id="txtStaffNoAdd-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffNameAdd" name="txtStaffNameAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jawatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtGredPositionAdd" name="txtGredPositionAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jabatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffDeptAdd" name="txtStaffDeptAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtFyrAdd" name="txtFyrAdd" size="4" maxlength="4" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDateAdd" id="txtFromDateAdd" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                             - 
                                            <input class="input" name="txtToDateAdd" id="txtToDateAdd" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatusAdd" name="lsStatusAdd" class="select">
                                                <option value="ACTIVE">ACTIVE</option>
                                                <option value="IN-ACTIVE">IN-ACTIVE</option>
                                            </select>
                                            <button type="button" class="button1 btn-warning" onclick="resetLGStaffAdd();">Reset</button>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblParentTableAdd" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonAdd" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertLGStaffUpdate();">Tambah</button>
                                            <button type="button" class="button1" onclick="closeLGStaffAdd();">Tutup</button>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="LGStaffUpdate" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbLGStaffUpdate" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Kumpulan Kehadiran</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtStaffNo" name="txtStaffNo" type="text" size="10" maxlength="10" value="" class="input" readonly style="background-color:gray; color:floralwhite;">
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
                                        <td width="20%" class="tblTextCommon">Jenis:</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtLeaveType" name="txtLeaveType" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDate" id="txtFromDate" type="text" value="" size="15" maxlength="20">
                                             - 
                                            <input class="input" name="txtToDate" id="txtToDate" type="text" value="" size="15" maxlength="20">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Cuti Bawa</td>
                                        <td width="80%" class="tblText2">
                                            <input type="number" id="txtLeaveBrought" name="txtLeaveBrought" size="5" maxlength="5" value="0" class="input"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Cuti Tahunan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="number" id="txtLeaveCount" name="txtLeaveCount" size="5" maxlength="5" value="0" class="input"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jumlah Cuti</td>
                                        <td width="80%" class="tblText2">
                                            <input type="number" id="txtLeaveTotal" name="txtLeaveTotal" size="5" maxlength="5" value="0" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Cuti Diambil</td>
                                        <td width="80%" class="tblText2">
                                            <input type="number" id="txtLeaveTaken" name="txtLeaveTaken" size="5" maxlength="5" value="0" class="input"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <option value="ACTIVE">ACTIVE</option>
                                                <option value="IN-ACTIVE">IN-ACTIVE</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonUpdate" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateLGStaffUpdate();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeLGStaffUpdate();">Tutup</button>
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

        $('#txtStaffNoAdd').autocomplete({
            lookup: StaffNoArray,
            appendTo: '#txtStaffNoAdd-container',
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

        $('#txtStaffNoAdd').on('focus', function () {
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

                $('#txtStaffNameAdd').val(result_getStaffEmployDetails.staffemploy.GetSetname);
                $('#txtGredPositionAdd').val(result_getStaffEmployDetails.staffemploy.GetSetpos_name + " [" + result_getStaffEmployDetails.staffemploy.GetSetgred_name + "]");
                $('#txtStaffDeptAdd').val(result_getStaffEmployDetails.staffemploy.GetSetdept_name);
                $('#txtFyrAdd').val("<%=sCurrFyr%>");
                $('#txtFromDateAdd').val("01/01/<%=sCurrFyr%>");
                $('#txtToDateAdd').val("31/12/<%=sCurrFyr%>");

                var getLGItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>", "leave_cat", result_getStaffEmployDetails.staffemploy.GetSetgred_name, "staffno", result_getStaffEmployDetails.staffemploy.GetSetstaffno];
                PageMethod("getLGItemList", getLGItemList_parameters, getLGItemList_succeedAjaxFn, getLGItemList_failedAjaxFn, false);

            } else {
                //alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        var getLGItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getLGItemList_succeedAjaxFn: " + textStatus);
            $('#tblParentTableAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kod/ Keterangan</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori/ Jenis</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Bawa</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Tahunan</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Diambil</td>' +
                '</tr>';
            var idx = 0;
            var getLGItemList_result = JSON.parse(data.d);
            if (getLGItemList_result.result == "Y") {
                $.each(getLGItemList_result.itemlist, function (i, item) {
                    idx += 1;
                    trHTML += '<tr><td valign="top"><font>' + idx + '.</font></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSetcode + ' - ' + item.GetSetdesc + '</font><input type="hidden" name="input_code" value="' + item.GetSetcode + '" /></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSettype + '</font><input type="hidden" name="input_lgid" value="' + item.GetSetid + '" /></td>' +
                        '<td align="left" valign="top"><input type="input" name="input_brought" size="5" maxlength="5" value="' + item.GetSetbrought + '" class="input"/></td>' +
                        '<td align="left" valign="top"><input type="input" name="input_count" size="5" maxlength="5" value="' + item.GetSetcount + '" class="input" /></td>' +
                        '<td align="left" valign="top"><input type="input" name="input_taken" size="5" maxlength="5" value="' + item.GetSettaken + '" class="input"/></td>' +
                        '</tr>';
                });

                if (idx > 0) {
                    //$('#tblParentTableAdd').append(trHTML);
                }
                else {
                    trHTML += '<tr><td valign="top" colspan=6 ><font>No record found!</font></td>' +
                        '</tr>';
                }
            }
            else {
                alert(getLGItemList_result.message);
            }
            $('#tblParentTableAdd').append(trHTML);
        }

        var getLGItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getLGItemList_failedAjaxFn: " + textStatus);
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
                    deleteLGStaffUpdate(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openLGStaffUpdate('OPEN', id);
                }
            }
        });

        function deleteLGStaffUpdate(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteLGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteLGStaffUpdate", deleteLGStaffUpdate_parameters, deleteLGStaffUpdate_succeedAjaxFn, deleteLGStaffUpdate_failedAjaxFn, false);
            }
        }

        var deleteLGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteLGStaffUpdate_succeedAjaxFn: " + textStatus);
            var deleteLGStaffUpdate_result = JSON.parse(data.d);
            if (deleteLGStaffUpdate_result.result == "Y") {
                //alert(deleteLGStaffUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteLGStaffUpdate_result.message);
            }
        }

        var deleteLGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteLGStaffUpdate_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Staff Leave...");
        }

        function openLGStaffUpdate(typ, id) {
            if (typ == 'OPEN') {
                $('#LGStaffUpdate').modal({ backdrop: "static" });

                var getLGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getLGStaffUpdate", getLGStaffUpdate_parameters, getLGStaffUpdate_succeedAjaxFn, getLGStaffUpdate_failedAjaxFn, false);

            } else if (typ == 'ADD') {
                $('#LGStaffAdd').modal({ backdrop: "static" });
                $('#tblParentTableAdd').empty();
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kod/ Keterangan</td>' +
                    '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori/ Jenis</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Bawa</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Tahunan</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Diambil</td>' +
                    '</tr>';
                $('#tblParentTableAdd').append(trHTML);

            }
        }

        var getLGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getLGStaffUpdate_succeedAjaxFn: " + textStatus);
            var getLGStaffUpdate_result = JSON.parse(data.d);
            if (getLGStaffUpdate_result.result == "Y") {
                $('#hidId').val(getLGStaffUpdate_result.itemdetail.GetSetid);
                $('#txtStaffNo').val(getLGStaffUpdate_result.itemdetail.GetSetstaffno);
                $('#txtStaffName').val(getLGStaffUpdate_result.itemdetail.GetSetname);
                $('#txtGredPosition').val(getLGStaffUpdate_result.itemdetail.GetSetpos_name + " [" + getLGStaffUpdate_result.itemdetail.GetSetgred_name + "]");
                $('#txtStaffDept').val(getLGStaffUpdate_result.itemdetail.GetSetdept_name);
                $('#txtFyr').val(getLGStaffUpdate_result.itemdetail.GetSetfyr);
                $('#txtLeaveCat').val(getLGStaffUpdate_result.itemdetail.GetSetcat);
                $('#txtLeaveType').val(getLGStaffUpdate_result.itemdetail.GetSettype);
                $('#txtFromDate').val(getLGStaffUpdate_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getLGStaffUpdate_result.itemdetail.GetSettodate);
                $('#txtLeaveBrought').val(getLGStaffUpdate_result.itemdetail.GetSetbrought);
                $('#txtLeaveCount').val(getLGStaffUpdate_result.itemdetail.GetSetcount);
                $('#txtLeaveTotal').val(getLGStaffUpdate_result.itemdetail.GetSetbrought + getLGStaffUpdate_result.itemdetail.GetSetcount);
                $('#txtLeaveTaken').val(getLGStaffUpdate_result.itemdetail.GetSettaken);
                $('#lsStatus').val(getLGStaffUpdate_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getLGStaffUpdate_result.message);
            }
        }

        var getLGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getLGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function insertLGStaffUpdate() {
            var paramArray = {};
            var LeaveInputArray = [];

            //construct input array
            var input_code = document.getElementsByName('input_code');
            var input_lgid = document.getElementsByName('input_lgid');
            var input_brought = document.getElementsByName('input_brought');
            var input_count = document.getElementsByName('input_count');
            var input_taken = document.getElementsByName('input_taken');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = $('#txtFyrAdd').val();
            paramArray.staffno = $('#txtStaffNo').val();
            paramArray.fromdate = $('#txtFromDateAdd').val();
            paramArray.todate = $('#txtToDateAdd').val();
            paramArray.status = $('#lsStatus').val();

            for (var i = 0; i < input_code.length; i++) {
                var input_leave = {};
                input_leave.GetSetcode = input_code[i].value;
                input_leave.GetSetlg_id = input_lgid[i].value;
                input_leave.GetSetbrought = input_brought[i].value;
                input_leave.GetSetcount = input_count[i].value;
                input_leave.GetSettaken = input_taken[i].value;
                LeaveInputArray.push(input_leave);
            }

            paramArray.inputarray = LeaveInputArray;
            var json_string = JSON.stringify(paramArray);

            //var insertLGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyrAdd').val(), "staffno", $('#txtStaffNo').val(), "fromdate", $('#txtFromDateAdd').val(), "todate", $('#txtToDateAdd').val(), "status", $('#lsStatus').val(), "inputarray", LeaveInputArray ];
            //PageMethod("insertLGStaffUpdate", insertLGStaffUpdate_parameters, insertLGStaffUpdate_succeedAjaxFn, insertLGStaffUpdate_failedAjaxFn, false);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/insertLGStaffUpdate",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: insertLGStaffUpdate_succeedAjaxFn,
                error: insertLGStaffUpdate_failedAjaxFn,
                async: false
            });

        }

        var insertLGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertLGStaffUpdate_succeedAjaxFn: " + textStatus);
            var insertLGStaffUpdate_result = JSON.parse(data.d);
            if (insertLGStaffUpdate_result.result == "Y") {
                //alert(insertLGStaffUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertLGStaffUpdate_result.message);
            }
        }

        var insertLGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertLGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function closeLGStaffAdd() {
            resetLGStaffAdd();
            $('#LGStaffAdd').modal('hide');
        }

        function resetLGStaffAdd() {
            $('#hidId').val("");
            $('#txtStaffNoAdd').val("");
            $('#txtStaffNameAdd').val("");
            $('#txtStaffDeptAdd').val("");
            $('#txtGredPositionAdd').val("");
            $('#txtFyrAdd').val("");
            $('#txtLeaveCatAdd').val("");
            $('#txtLeaveTypeAdd').val("");
            //document.getElementById("txtLeaveCatAdd").selectedIndex = 0;
            $('#txtFromDateAdd').val("");
            $('#txtToDateAdd').val("");
            //$('#lsStatus').val("");
            document.getElementById("lsStatusAdd").selectedIndex = 0;

            $('#tblParentTableAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kod/ Keterangan</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori/ Jenis</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Bawa</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Tahunan</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Cuti Diambil</td>' +
                '</tr>';
            $('#tblParentTableAdd').append(trHTML);
        }

        function updateLGStaffUpdate() {
            var updateLGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "staffno", $('#txtStaffNo').val(), "fromdate", $('#txtFromDate').val(), "todate", $('#txtToDate').val(), "brought", $('#txtLeaveBrought').val(), "count", $('#txtLeaveCount').val(), "taken", $('#txtLeaveTaken').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateLGStaffUpdate", updateLGStaffUpdate_parameters, updateLGStaffUpdate_succeedAjaxFn, updateLGStaffUpdate_failedAjaxFn, false);
        }

        var updateLGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateLGStaffUpdate_succeedAjaxFn: " + textStatus);
            var updateLGStaffUpdate_result = JSON.parse(data.d);
            if (updateLGStaffUpdate_result.result == "Y") {
                //alert(updateLGStaffUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateLGStaffUpdate_result.message);
            }
        }

        var updateLGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateLGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function closeLGStaffUpdate() {
            resetLGStaffUpdate();
            $('#LGStaffUpdate').modal('hide');
        }

        function resetLGStaffUpdate() {
            $('#hidId').val("");
            $('#txtStaffNo').val("");
            $('#txtStaffName').val("");
            $('#txtStaffDept').val("");
            $('#txtGredPosition').val("");
            $('#txtFyr').val("");
            $('#txtLeaveCat').val("");
            $('#txtLeaveType').val("");
            //document.getElementById("lsGrpId").selectedIndex = 0;
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            $('#txtLeaveBrought').val("");
            $('#txtLeaveCount').val("");
            $('#txtLeaveTotal').val("");
            $('#txtLeaveTaken').val("");
            document.getElementById("lsStatus").selectedIndex = 0;
        }

        $('#txtLeaveBrought').on('focus', function () {
            calculateTotalLeave();

        }).on('blur', function () {
            calculateTotalLeave()

        });

        $('#txtLeaveCount').on('focus', function () {
            calculateTotalLeave();

        }).on('blur', function () {
            calculateTotalLeave()

        });

        function calculateTotalLeave() {
            var totalleave = Number($('#txtLeaveBrought').val()) + Number($('#txtLeaveCount').val());
            $('#txtLeaveTotal').val(totalleave);
        }

    </script>
</asp:Content>

