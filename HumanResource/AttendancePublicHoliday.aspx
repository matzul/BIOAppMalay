<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendance.master" AutoEventWireup="true" CodeFile="AttendancePublicHoliday.aspx.cs" Inherits="HumanResource_AttendancePublicHoliday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Hari Perlepasan (Public Holiday)</a></td>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Hari Perlepasan (Public Holiday)</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="10%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>
                        <td width="30%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsPublicHoliday.Count > 0)
                        {
                            for (int i = 0; i < lsPublicHoliday.Count; i++)
                            {
                                HRModel modAcc = (HRModel)lsPublicHoliday[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=modAcc.GetSetph_date %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcode %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
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
                        <td colspan="7" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsPublicHoliday.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openPHItemDetail('ADD',0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="PHItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbPHItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Hari Perlepasan (Public Holiday)</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtItemDate" id="txtItemDate" type="text" value="" size="15" maxlength="20">
                                            <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindTranDate">
                                            <script type="text/javascript">
                                                Calendar.setup({
                                                    inputField: "txtItemDate",     	// id of the input field
                                                    ifFormat: "%d-%m-%Y ",   	// format of the input field
                                                    button: "imgFindTranDate",  		// trigger for the calendar (image ID)
                                                    align: "B1",
                                                    singleClick: true
                                                });
                                            </script>
                                        </td>
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
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemCat" name="txtItemCat" size="20" maxlength="20" value="" class="input" />
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
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertPHItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updatePHItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closePHItemDetail();">Tutup</button>
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

            var getPHItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getPHItemList", getPHItemList_parameters, getPHItemList_succeedAjaxFn, getPHItemList_failedAjaxFn, false);

        });

        var getPHItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getPHItemList_succeedAjaxFn: " + textStatus);
            var getPHItemList_result = JSON.parse(data.d);
            if (getPHItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getPHItemList_result.itemlist, function (i, result) {
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
                console.log("getPHItemList_result.result: " + getPHItemList_result.result);
            }
        }

        var getPHItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getPHItemList_failedAjaxFn: " + textStatus);
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
                    deletePHItemDetail(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openPHItemDetail('OPEN', id);
                }
            }
        });

        function deletePHItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deletePHItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deletePHItemDetail", deletePHItemDetail_parameters, deletePHItemDetail_succeedAjaxFn, deletePHItemDetail_failedAjaxFn, false);
            }
        }

        var deletePHItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deletePHItemDetail_succeedAjaxFn: " + textStatus);
            var deletePHItemDetail_result = JSON.parse(data.d);
            if (deletePHItemDetail_result.result == "Y") {
                //alert(deletePHItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deletePHItemDetail_result.message);
            }
        }

        var deletePHItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deletePHItemDetail_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Public Holiday...");
        }

        function openPHItemDetail(typ, id) {
            $('#PHItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getPHItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getPHItemDetail", getPHItemDetail_parameters, getPHItemDetail_succeedAjaxFn, getPHItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getPHItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getPHItemDetail_succeedAjaxFn: " + textStatus);
            var getPHItemDetail_result = JSON.parse(data.d);
            if (getPHItemDetail_result.result == "Y") {
                $('#hidId').val(getPHItemDetail_result.itemdetail.GetSetid);
                $('#txtItemDate').val(getPHItemDetail_result.itemdetail.GetSetph_date);
                $('#txtItemCode').val(getPHItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemDesc').val(getPHItemDetail_result.itemdetail.GetSetdesc);
                $('#txtItemCat').val(getPHItemDetail_result.itemdetail.GetSetcat);
                $('#lsStatus').val(getPHItemDetail_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getPHItemDetail_result.message);
            }
        }

        var getPHItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getPHItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertPHItemDetail() {
            var insertPHItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "phdate", $('#txtItemDate').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "cat", $('#txtItemCat').val(), "status", $('#lsStatus').val()];
            PageMethod("insertPHItemDetail", insertPHItemDetail_parameters, insertPHItemDetail_succeedAjaxFn, insertPHItemDetail_failedAjaxFn, false);
        }

        var insertPHItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertPHItemDetail_succeedAjaxFn: " + textStatus);
            var insertPHItemDetail_result = JSON.parse(data.d);
            if (insertPHItemDetail_result.result == "Y") {
                //alert(insertPHItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertPHItemDetail_result.message);
            }
        }

        var insertPHItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertPHItemDetail_failedAjaxFn: " + textStatus);
        }

        function updatePHItemDetail() {
            var updatePHItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "phdate", $('#txtItemDate').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "cat", $('#txtItemCat').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updatePHItemDetail", updatePHItemDetail_parameters, updatePHItemDetail_succeedAjaxFn, updatePHItemDetail_failedAjaxFn, false);
        }

        var updatePHItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updatePHItemDetail_succeedAjaxFn: " + textStatus);
            var updatePHItemDetail_result = JSON.parse(data.d);
            if (updatePHItemDetail_result.result == "Y") {
                //alert(updatePHItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updatePHItemDetail_result.message);
            }
        }

        var updatePHItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updatePHItemDetail_failedAjaxFn: " + textStatus);
        }

        function closePHItemDetail() {
            resetPHItemDetail();
            $('#PHItemDetail').modal('hide');
        }

        function resetPHItemDetail() {
            $('#hidId').val("");
            $('#txtItemDate').val("");
            $('#txtItemCode').val("");
            $('#txtItemDesc').val("");
            $('#txtItemCat').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

