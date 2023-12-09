<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="BPIDPage.aspx.cs" Inherits="Accounting_BPIDPage" %>

<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:TopMenu ID="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pelanggan & Pembekal / Customer & Supplier</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Id:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBpId" name="txtFindBpId" type="text" size="10" maxlength="10" value="<%=sCurrBpId %>" class="input">
                        <div id="txtFindBpId-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Nama/ Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBpDesc" name="txtFindBpDesc" type="text" size="50" maxlength="50" value="<%=sCurrBpDesc %>" class="input">
                        <div id="txtFindBpDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Rujukan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBpRef" name="txtFindBpRef" type="text" size="20" maxlength="20" value="<%=sCurrBpRef %>" class="input">
                        <div id="txtFindBpRef-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Pelanggan & Pembekal / List of Customer & Supplier</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="10%" valign="middle" align="left" class="tblTitle3Mod">Id</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama/ Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Alamat</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisBP.Count > 0)
                        {
                            for (int i = 0; i < lsFisBP.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisBP[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=modAcc.GetSetbpid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaddress %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpreference %></td>
                        <td valign="top" align="right"><%=modAcc.GetSetdebit %></td>
                        <td valign="top" align="right"><%=modAcc.GetSetcredit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                        </td>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="8" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsFisBP.Count %> record(s)</td>
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
                        <input class="button1a" name="btnModify" type="button" value="Kemaskini" onclick="fOpenWindow('BPIDMasterPage.aspx');">					    
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisBpDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisBankDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Pelanggan & Pembekal</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Id</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtBpId" name="txtBpId" type="text" size="15" maxlength="20" value="" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama/ Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBpDesc" name="txtBpDesc" size="30" maxlength="30" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Alamat</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBpAddress" name="txtBpAddress" size="50" maxlength="50" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Hubungi/ No. Telefon</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBpContact" name="txtBpContact" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Rujukan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBpRef" name="txtBpRef" class="input" /></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <option value="NEW">NEW</option>
                                                <option value="CONFIRMED">CONFIRMED</option>
                                                <option value="CANCELLED">CANCELLED</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddBp" name="btnAddBp" type="button" class="button1 btn-primary" onclick="insertFisBpDetail();">Tambah</button>
                                            <button id="btnModifyBp" name="btnModifyBp" type="button" class="button1 btn-primary" onclick="updateFisBpDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisBpDetail();">Tutup</button>
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

        var fisBpIdArray = [];
        var maxlengthdataautocomplete = 20;
        var fisBpDescArray = [];
        var maxlengthdataautocomplete2 = 20;
        var fisBpRefArray = [];
        var maxlengthdataautocomplete3 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindBpId").value = "";
                document.getElementById("txtFindBpDesc").value = "";
                document.getElementById("txtFindBpRef").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisBpList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisBpList", getFisBpList_parameters, getFisBpList_succeedAjaxFn, getFisBpList_failedAjaxFn, false);

        });

        var getFisBpList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisBpList_succeedAjaxFn: " + textStatus);
            var getFisBpList_result = JSON.parse(data.d);
            if (getFisBpList_result.result == "Y") {
                var bpid = '';
                var bpdesc = '';
                var bpref = '';
                $.each(getFisBpList_result.fisbanklist, function (i, result) {
                    if (bpid != result.GetSetbpid + '-' + result.GetSetbpdesc) {
                        var objData = {};
                        objData.value = result.GetSetbpid + '-' + result.GetSetbpdesc;
                        objData.data = result.GetSetbpid;
                        fisBpIdArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        bankid = result.GetSetbpid + '-' + result.GetSetbpdesc;
                    }

                    if (bpdesc != result.GetSetbpdesc) {
                        var objData = {};
                        objData.value = result.GetSetbpdesc;
                        objData.data = result.GetSetbpdesc;
                        fisBpDescArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        bpdesc = result.GetSetbpdesc;
                    }

                    if (bpref != result.GetSetbpreference) {
                        var objData = {};
                        objData.value = result.GetSetbpreference;
                        objData.data = result.GetSetbpreference;
                        fisBpRefArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete3) {
                            maxlengthdataautocomplete3 = objData.value.length;
                        }
                        bpref = result.GetSetbpreference;
                    }
                });
            }
            else {
                console.log("getFisBpList_result.result: " + getFisBpList_result.result);
            }
        }

        var getFisBpList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisBpList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindBpId').autocomplete({
            lookup: fisBpIdArray,
            appendTo: '#txtFindBpId-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindBpId').val(suggestion.data);
            }
        });

        $('#txtFindBpDesc').autocomplete({
            lookup: fisBpDescArray,
            appendTo: '#txtFindBpDesc-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindBpDesc').val(suggestion.data);
            }
        });

        $('#txtFindBpRef').autocomplete({
            lookup: fisBpRefArray,
            appendTo: '#txtFindBpRef-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete3 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindBpRef').val(suggestion.data);
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
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openFisBpDetail('OPEN', id);
                }
            }
        });

        function openFisBpDetail(typ, id) {
            $('#FisBpDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddBp').hide();
                $('#btnModifyBp').show();

                var getFisBpDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getFisBpDetail", getFisBpDetail_parameters, getFisBpDetail_succeedAjaxFn, getFisBpDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddBp').show();
                $('#btnModifyBp').hide();
            }
        }

        var getFisBpDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisBpDetail_succeedAjaxFn: " + textStatus);
            var getFisBpDetail_result = JSON.parse(data.d);
            if (getFisBpDetail_result.result == "Y") {
                $('#hidId').val(getFisBpDetail_result.fisbpdetail.GetSetid);
                $('#txtBpId').val(getFisBpDetail_result.fisbpdetail.GetSetbpid);
                $('#txtBpDesc').val(getFisBpDetail_result.fisbpdetail.GetSetbpdesc);
                $('#txtBpAddress').val(getFisBpDetail_result.fisbpdetail.GetSetbpaddress);
                $('#txtBpContact').val(getFisBpDetail_result.fisbpdetail.GetSetbpcontact);
                $('#txtBpRef').val(getFisBpDetail_result.fisbpdetail.GetSetbpreference);
                $('#lsStatus').val(getFisBpDetail_result.fisbpdetail.GetSetstatus);
            }
            else {
                alert(getFisBpDetail_result.message);
            }
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.result);
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.message);
        }

        var getFisBpDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisBpDetail_failedAjaxFn: " + textStatus);
        }

        function insertFisBpDetail() {
            var insertFisBpDetail_parameters = ["currcomp", "<%=sCurrComp%>", "bpid", $('#txtBpId').val(), "bpdesc", $('#txtBpDesc').val(), "bpaddress", $('#txtBpAddress').val(), "bpcontact", $('#txtBpContact').val(), "bpreference", $('#txtBpRef').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisBpDetail", insertFisBpDetail_parameters, insertFisBpDetail_succeedAjaxFn, insertFisBpDetail_failedAjaxFn, false);
        }

        var insertFisBpDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisBpDetail_succeedAjaxFn: " + textStatus);
            var insertFisBpDetail_result = JSON.parse(data.d);
            if (insertFisBpDetail_result.result == "Y") {
                alert(insertFisBpDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisBpDetail_result.message);
            }
        }

        var insertFisBpDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisBpDetail_failedAjaxFn: " + textStatus);
        }

        function updateFisBpDetail() {
            var updateFisBpDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "bpid", $('#txtBpId').val(), "bpdesc", $('#txtBpDesc').val(), "bpaddress", $('#txtBpAddress').val(), "bpcontact", $('#txtBpContact').val(), "bpreference", $('#txtBpRef').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisBpDetail", updateFisBpDetail_parameters, updateFisBpDetail_succeedAjaxFn, updateFisBpDetail_failedAjaxFn, false);
        }

        var updateFisBpDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBpDetail_succeedAjaxFn: " + textStatus);
            var updateFisBpDetail_result = JSON.parse(data.d);
            if (updateFisBpDetail_result.result == "Y") {
                alert(updateFisBpDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisBpDetail_result.message);
            }
        }

        var updateFisBpDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBpDetail_failedAjaxFn: " + textStatus);
        }

        function closeFisBpDetail() {
            resetFisBpDetail();
            $('#FisBpDetail').modal('hide');
        }

        function resetFisBpDetail() {
            $('#hidId').val("");
            $('#txtBpId').val("");
            $('#txtBpDesc').val("");
            $('#txtBpAddress').val("");
            $('#txtBpContact').val("");
            $('#txtBpRef').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

