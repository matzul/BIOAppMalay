<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendance.master" AutoEventWireup="true" CodeFile="AttendanceWorkingGroup.aspx.cs" Inherits="HumanResource_AttendanceWorkingGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kumpulan Kehadiran & Masa Kerja</a></td>
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
                    <td width="20%" class="tblTextCommon">Kod:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemCode" name="txtFindItemCode" type="text" size="10" maxlength="10" value="<%=sCode%>" class="input">
                        <div id="txtFindItemCode-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemDesc" name="txtFindItemDesc" type="text" size="50" maxlength="50" value="<%=sDesc%>" class="input">
                        <div id="txtFindItemDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kumpulan Kehadiran & Masa Kerja</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>
                        <td width="11%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bermula</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Sehingga</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Hari Rehat</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsWorkingGroup.Count > 0)
                        {
                            for (int i = 0; i < lsWorkingGroup.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsWorkingGroup[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcode %></td>
                        <td valign="top" align="left"><%=modItem.GetSetdesc %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
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
                        <td colspan="9" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsWorkingGroup.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openWGItemDetail('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
                <input type="hidden" name="hidRestDay" id="hidRestDay" value="" />
            </div>

            <div class="modal fade" id="WGItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbWGItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kumpulan Kehadiran & Masa Kerja</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kod</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemCode" name="txtItemCode" type="text" size="10" maxlength="10" value="" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemDesc" name="txtItemDesc" size="50" maxlength="50" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDate" id="txtFromDate" type="text" value="" size="15" maxlength="20">
                                            <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFromDate">
                                            <script type="text/javascript">
                                                Calendar.setup({
                                                    inputField: "txtFromDate",     	// id of the input field
                                                    ifFormat: "%d-%m-%Y ",   	// format of the input field
                                                    button: "imgFromDate",  		// trigger for the calendar (image ID)
                                                    align: "B1",
                                                    singleClick: true
                                                });
                                            </script>
                                             - 
                                            <input class="input" name="txtToDate" id="txtToDate" type="text" value="" size="15" maxlength="20">
                                            <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgToDate">
                                            <script type="text/javascript">
                                                Calendar.setup({
                                                    inputField: "txtToDate",     	// id of the input field
                                                    ifFormat: "%d-%m-%Y ",   	// format of the input field
                                                    button: "imgToDate",  		// trigger for the calendar (image ID)
                                                    align: "B1",
                                                    singleClick: true
                                                });
                                            </script>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Hari Rehat</td>
                                        <td width="80%" class="tblText2">
                                            <input type="checkbox" id="chkSunday" class="restday" value="AHAD">Ahad
                                            <input type="checkbox" id="chkMonday" class="restday" value="ISNIN">Isnin
                                            <input type="checkbox" id="chkTuesday" class="restday" value="SELASA">Selasa
                                            <input type="checkbox" id="chkWednesday" class="restday" value="RABU">Rabu
                                            <input type="checkbox" id="chkThursday" class="restday" value="KHAMIS">Khamis
                                            <input type="checkbox" id="chkFriday" class="restday" value="JUMAAT">Jumaat
                                            <input type="checkbox" id="chkSaturday" class="restday" value="SABTU">Sabtu
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
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertWGItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateWGItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeWGItemDetail();">Tutup</button>
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

        var ItemCodeArray = [];
        var maxlengthdataautocomplete = 20;
        var ItemDescArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindItemCode").value = "";
                document.getElementById("txtFindItemDesc").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getWGItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getWGItemList", getWGItemList_parameters, getWGItemList_succeedAjaxFn, getWGItemList_failedAjaxFn, false);

        });

        var getWGItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getWGItemList_succeedAjaxFn: " + textStatus);
            var getWGItemList_result = JSON.parse(data.d);
            if (getWGItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getWGItemList_result.itemlist, function (i, result) {
                    if (itemno != result.GetSetcode + '-' + result.GetSetdesc) {
                        var objData = {};
                        objData.value = result.GetSetcode + '-' + result.GetSetdesc;
                        objData.data = result.GetSetcode;
                        ItemCodeArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemno = result.GetSetcode + '-' + result.GetSetdesc;
                    }

                    if (itemdesc != result.GetSetdesc) {
                        var objData = {};
                        objData.value = result.GetSetdesc;
                        objData.data = result.GetSetdesc;
                        ItemDescArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemdesc = result.GetSetdesc;
                    }

                });
            }
            else {
                console.log("getWGItemList_result.result: " + getWGItemList_result.result);
            }
        }

        var getWGItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getWGItemList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindItemCode').autocomplete({
            lookup: ItemCodeArray,
            appendTo: '#txtFindItemCode-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemCode').val(suggestion.data);
            }
        });

        $('#txtFindItemDesc').autocomplete({
            lookup: ItemDescArray,
            appendTo: '#txtFindItemDesc-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemDesc').val(suggestion.data);
            }
        });

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
                    deleteWGItemDetail(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openWGItemDetail('OPEN', id);
                }
                else if (action == 'workingday') {
                    //alert('edit:' + id);
                    openWDItemList('OPEN', id);
                }
            }
        });

        function checkRestDay() {
            var res = "";
            //var inputs = document.querySelectorAll('.pl');
            var inputs = $('.restday');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked == true) {
                    var pl1 = inputs[i].value;
                    if (res.length > 0) {
                        res = res + "," + pl1;
                    } else {
                        res = pl1;
                    }
                }   
            }
            $('#hidRestDay').val(res);
        }

        function checkedRestDay(restday) {
            if (restday.length > 0) {
                let arr = restday.split(',');
                //var inputs = document.querySelectorAll('.pl');
                var inputs = $('.restday');
                for (var i = 0; i < inputs.length; i++) {
                    var pl1 = inputs[i].value;
                    if (arr.includes(pl1)) {
                        inputs[i].checked = true;
                    }
                    else {
                        inputs[i].checked = false;
                    }
                }
            }
        }

        function deleteWGItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteWGItemDetail", deleteWGItemDetail_parameters, deleteWGItemDetail_succeedAjaxFn, deleteWGItemDetail_failedAjaxFn, false);
            }
        }

        var deleteWGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteWGItemDetail_succeedAjaxFn: " + textStatus);
            var deleteWGItemDetail_result = JSON.parse(data.d);
            if (deleteWGItemDetail_result.result == "Y") {
                //alert(deleteWGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteWGItemDetail_result.message);
            }
        }

        var deleteWGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteWGItemDetail_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Public Holiday...");
        }

        function openWGItemDetail(typ, id) {
            $('#WGItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getWGItemDetail", getWGItemDetail_parameters, getWGItemDetail_succeedAjaxFn, getWGItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getWGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getWGItemDetail_succeedAjaxFn: " + textStatus);
            var getWGItemDetail_result = JSON.parse(data.d);
            if (getWGItemDetail_result.result == "Y") {
                $('#hidId').val(getWGItemDetail_result.itemdetail.GetSetid);
                $('#txtItemCode').val(getWGItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemDesc').val(getWGItemDetail_result.itemdetail.GetSetdesc);
                $('#txtFromDate').val(getWGItemDetail_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getWGItemDetail_result.itemdetail.GetSettodate);
                $('#lsStatus').val(getWGItemDetail_result.itemdetail.GetSetstatus);
                checkedRestDay(getWGItemDetail_result.itemdetail.GetSetrest_day);
            }
            else {
                alert(getWGItemDetail_result.message);
            }
        }

        var getWGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getWGItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertWGItemDetail() {
            checkRestDay();
            var insertWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "fromdate", $('#txtFromDate').val(), "todate", $('#txtToDate').val(), "rest_day", $('#hidRestDay').val(), "status", $('#lsStatus').val()];
            PageMethod("insertWGItemDetail", insertWGItemDetail_parameters, insertWGItemDetail_succeedAjaxFn, insertWGItemDetail_failedAjaxFn, false);
        }

        var insertWGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertWGItemDetail_succeedAjaxFn: " + textStatus);
            var insertWGItemDetail_result = JSON.parse(data.d);
            if (insertWGItemDetail_result.result == "Y") {
                //alert(insertWGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertWGItemDetail_result.message);
            }
        }

        var insertWGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertWGItemDetail_failedAjaxFn: " + textStatus);
        }

        function updateWGItemDetail() {
            checkRestDay();
            var updateWGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "fromdate", $('#txtFromDate').val(), "todate", $('#txtToDate').val(), "rest_day", $('#hidRestDay').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateWGItemDetail", updateWGItemDetail_parameters, updateWGItemDetail_succeedAjaxFn, updateWGItemDetail_failedAjaxFn, false);
        }

        var updateWGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateWGItemDetail_succeedAjaxFn: " + textStatus);
            var updateWGItemDetail_result = JSON.parse(data.d);
            if (updateWGItemDetail_result.result == "Y") {
                //alert(updateWGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateWGItemDetail_result.message);
            }
        }

        var updateWGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateWGItemDetail_failedAjaxFn: " + textStatus);
        }

        function closeWGItemDetail() {
            resetWGItemDetail();
            $('#WGItemDetail').modal('hide');
        }

        function resetWGItemDetail() {
            $('#hidId').val("");
            $('#txtItemCode').val("");
            $('#txtItemDesc').val("");
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            $('#lsStatus').val("");
        }

        function openWDItemList(typ, id) {
            if (typ == "OPEN") {
                fOpenWindow('AttendanceWorkingDay.aspx?fyr=' + $('#lsFindFyr').val() + '&id=' + id);
            }
        }

    </script>
</asp:Content>

