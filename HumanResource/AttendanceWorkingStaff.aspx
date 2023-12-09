<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendance.master" AutoEventWireup="true" CodeFile="AttendanceWorkingStaff.aspx.cs" Inherits="HumanResource_AttendanceWorkingStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kakitangan Kepada Kumpulan Kehadiran</a></td>
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
                    <td width="20%" class="tblTextCommon">Kumpulan Kehadiran:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindGrpId" name="lsFindGrpId">
                            <option value="">-Select-</option>
                            <%
                                for(int i=0; i<lsWorkingGroup.Count; i++) 
                                { 
                                    HRModel modWorkGroup = (HRModel)lsWorkingGroup[i]; 
                            %>
                            <option value="<%=modWorkGroup.GetSetid %>" <%=sGrpId.Equals(modWorkGroup.GetSetid) ? "selected" : "" %>><%=modWorkGroup.GetSetdesc %> [<%=modWorkGroup.GetSetcode %>]</option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kakitangan & Kumpulan Kehadiran</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan Kehadiran</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bermula</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Sehingga</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Hari Rehat</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsWorkingGroupStaff.Count > 0)
                        {
                            for (int i = 0; i < lsWorkingGroupStaff.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsWorkingGroupStaff[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetdesc %> [<%=modItem.GetSetcode %>]</td>
                        <td valign="top" align="left"><%=modItem.GetSetfromdate %></td>
                        <td valign="top" align="left"><%=modItem.GetSettodate %></td>
                        <td valign="top" align="left"><%=modItem.GetSetrest_day %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" class="button_warning enabled" data-action="delete">Hapus</button>
                            <button type="button" class="button_warning enabled" data-action="workingday">Masa Kerja</button>
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
                                <td width="50%" height="15" align="left"><%=lsWorkingGroupStaff.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openWGStaff('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="WGStaff" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbWGStaff" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Kumpulan Kehadiran</td>
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
                                        <td width="20%" class="tblTextCommon">Kumpulan Kehadiran:</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsGrpId" name="lsGrpId">
                                                <option value="">-Select-</option>
                                                <%
                                                    for(int i=0; i<lsWorkingGroup.Count; i++) 
                                                    { 
                                                        HRModel modWorkGroup = (HRModel)lsWorkingGroup[i]; 
                                                %>
                                                <option value="<%=modWorkGroup.GetSetid %>"><%=modWorkGroup.GetSetdesc %> [<%=modWorkGroup.GetSetcode %>]</option>
                                                <%
                                                    }
                                                %>
                                            </select>
                                            <input class="input" name="txtGrpCode" id="txtGrpCode" type="hidden" value=""/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDate" id="txtFromDate" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                             - 
                                            <input class="input" name="txtToDate" id="txtToDate" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Hari Rehad</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtRestDay" name="txtRestDay" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
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
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertWGStaff();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateWGStaff();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeWGStaff();">Tutup</button>
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
                document.getElementById("lsFindGrpId").selectedIndex = 0;
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

            } else {
                //alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        $('#lsGrpId').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';
        }).on('click', function () {
            if ($('#lsGrpId').val().length > 0) {
                var getWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#lsGrpId').val()];
                PageMethod("getWGItemDetail", getWGItemDetail_parameters, getWGItemDetail_succeedAjaxFn, getWGItemDetail_failedAjaxFn, false);
            }
        }).on('blur', function () {
            var getWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#lsGrpId').val()];
            PageMethod("getWGItemDetail", getWGItemDetail_parameters, getWGItemDetail_succeedAjaxFn, getWGItemDetail_failedAjaxFn, false);

        });

        var getWGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getWGItemDetail_succeedAjaxFn: " + textStatus);
            var getWGItemDetail_result = JSON.parse(data.d);
            if (getWGItemDetail_result.result == "Y") {
                $('#txtGrpCode').val(getWGItemDetail_result.itemdetail.GetSetcode);
                $('#txtFromDate').val(getWGItemDetail_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getWGItemDetail_result.itemdetail.GetSettodate);
                $('#txtRestDay').val(getWGItemDetail_result.itemdetail.GetSetrest_day);
            }
            else {
                alert(getWGItemDetail_result.message);
            }
        }

        var getWGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getWGItemDetail_failedAjaxFn: " + textStatus);
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
                    deleteWGStaff(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openWGStaff('OPEN', id);
                }
                else if (action == 'workingday') {
                    //alert('edit:' + id);
                    openWDItemList('OPEN', id);
                }
            }
        });

        function deleteWGStaff(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteWGStaff_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteWGStaff", deleteWGStaff_parameters, deleteWGStaff_succeedAjaxFn, deleteWGStaff_failedAjaxFn, false);
            }
        }

        var deleteWGStaff_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteWGStaff_succeedAjaxFn: " + textStatus);
            var deleteWGStaff_result = JSON.parse(data.d);
            if (deleteWGStaff_result.result == "Y") {
                //alert(deleteWGStaff_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteWGStaff_result.message);
            }
        }

        var deleteWGStaff_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteWGStaff_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Public Holiday...");
        }

        function openWGStaff(typ, id) {
            $('#WGStaff').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getWGStaff_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getWGStaff", getWGStaff_parameters, getWGStaff_succeedAjaxFn, getWGStaff_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getWGStaff_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getWGStaff_succeedAjaxFn: " + textStatus);
            var getWGStaff_result = JSON.parse(data.d);
            if (getWGStaff_result.result == "Y") {
                $('#hidId').val(getWGStaff_result.itemdetail.GetSetid);
                $('#txtStaffNo').val(getWGStaff_result.itemdetail.GetSetstaffno);
                $('#txtStaffName').val(getWGStaff_result.itemdetail.GetSetname);
                $('#txtGredPosition').val(getWGStaff_result.itemdetail.GetSetpos_name + " [" + getWGStaff_result.itemdetail.GetSetgred_name + "]");
                $('#txtStaffDept').val(getWGStaff_result.itemdetail.GetSetdept_name);
                $('#txtFyr').val(getWGStaff_result.itemdetail.GetSetfyr);
                $('#lsGrpId').val(getWGStaff_result.itemdetail.GetSetwg_id);
                $('#txtFromDate').val(getWGStaff_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getWGStaff_result.itemdetail.GetSettodate);
                $('#txtRestDay').val(getWGStaff_result.itemdetail.GetSetrest_day);
                $('#lsStatus').val(getWGStaff_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getWGStaff_result.message);
            }
        }

        var getWGStaff_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getWGStaff_failedAjaxFn: " + textStatus);
        }

        function insertWGStaff() {
            var insertWGStaff_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "staffno", $('#txtStaffNo').val(), "grpid", $('#lsGrpId').val(), "grpcode", $('#txtGrpCode').val(), "status", $('#lsStatus').val()];
            PageMethod("insertWGStaff", insertWGStaff_parameters, insertWGStaff_succeedAjaxFn, insertWGStaff_failedAjaxFn, false);
        }

        var insertWGStaff_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertWGStaff_succeedAjaxFn: " + textStatus);
            var insertWGStaff_result = JSON.parse(data.d);
            if (insertWGStaff_result.result == "Y") {
                //alert(insertWGStaff_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertWGStaff_result.message);
            }
        }

        var insertWGStaff_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertWGStaff_failedAjaxFn: " + textStatus);
        }

        function updateWGStaff() {
            var updateWGStaff_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "staffno", $('#txtStaffNo').val(), "grpid", $('#lsGrpId').val(), "grpcode", $('#txtGrpCode').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateWGStaff", updateWGStaff_parameters, updateWGStaff_succeedAjaxFn, updateWGStaff_failedAjaxFn, false);
        }

        var updateWGStaff_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateWGStaff_succeedAjaxFn: " + textStatus);
            var updateWGStaff_result = JSON.parse(data.d);
            if (updateWGStaff_result.result == "Y") {
                //alert(updateWGStaff_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateWGStaff_result.message);
            }
        }

        var updateWGStaff_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateWGStaff_failedAjaxFn: " + textStatus);
        }

        function closeWGStaff() {
            resetWGStaff();
            $('#WGStaff').modal('hide');
        }

        function resetWGStaff() {
            $('#hidId').val("");
            $('#txtStaffNo').val("");
            $('#txtStaffName').val("");
            $('#txtStaffDept').val("");
            $('#txtGredPosition').val("");
            $('#txtFyr').val("");
            document.getElementById("lsGrpId").selectedIndex = 0;
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            $('#txtRestDay').val("");
            $('#lsStatus').val("");
        }

        function openWDItemList(typ, id) {
            if (typ == "OPEN") {
                fOpenWindow('AttendanceWorkingStaffDay.aspx?fyr=' + $('#lsFindFyr').val() + '&id=' + id);
            }
        }

    </script>
</asp:Content>

