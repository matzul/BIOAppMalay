<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="BPIDMasterPage.aspx.cs" Inherits="Accounting_BPIDMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var fisbptranStoredArray = [];
        var fisbptranAddArray = [];
        var fisbptranRemoveArray = [];
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pelanggan & Pembekal / Customer & Supplier (Business Partner)</a></td>
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
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod"></td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Id</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama/ Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Alamat</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                    </tr>
                    <%
                        if (lsFisBPMasterTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisBPMasterTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisBPMasterTran[i];
                    %>
                    <tr class="tblText1" data-bpid="<%=modAcc.GetSetbpid %>">
                        <td valign="top" align="center">
                            <input data-action="select" type="checkbox" <%=modAcc.GetSethaschecked.Equals("1")?"checked class='disabled'":"enabled"%> /></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaddress %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpreference %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <script type="text/javascript">
                            <%=modAcc.GetSethaschecked.Equals("1")?"fisbptranStoredArray.push('"+modAcc.GetSetbpid+"');":""%>
                        </script>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="6" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsFisBPMasterTran.Count %> record(s)</td>
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
                        <input class="button1a" id="btnSave" type="button" value="Simpan" onclick="">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
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

        /*
        $('#txtAccGroup').autocomplete({
            lookup: fisAccGroup,
            appendTo: '#txtAccGroup-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdatafisAccGroup * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtAccGroup').val(suggestion.data);
            }
        }).bind('focus', function () { $(this).autocomplete("search"); });
        */

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

            var trbpid = $(target).closest("[data-bpid]");
            //get data-accid value for the TR
            var bpid = (trbpid.data("bpid"));

            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'select') {

                    if ($(target).hasClass('disabled')) {
                        $(target).removeClass('disabled').addClass('enabled');
                        $(target).prop('checked', true);
                    }
                    else {
                        if ($(target).prop('checked')) {

                            var i = fisbptranStoredArray.indexOf(bpid);
                            if (i == -1) {
                                if (fisbptranAddArray.indexOf(bpid) == -1)
                                    fisbptranAddArray.push(bpid);
                            } else {

                            }
                            var j = fisbptranRemoveArray.indexOf(bpid);
                            if (j >= 0) {
                                fisbptranRemoveArray.splice(j, 1);
                            }
                        } else {

                            var i = fisbptranStoredArray.indexOf(bpid);
                            if (i >= 0) {
                                if (fisbptranRemoveArray.indexOf(bpid) == -1)
                                    fisbptranRemoveArray.push(bpid);
                            }
                            else {

                            }
                            var j = fisbptranAddArray.indexOf(bpid);
                            if (j >= 0) {
                                fisbptranAddArray.splice(j, 1);
                            }
                        }
                    }
                }
                else if (action == 'edit') {
                    //alert('edit:' + bpid);
                    openFisBpDetail('OPEN', bpid);
                }
            }
        });

        $("#btnSave").on("click", function (e) {
            var updateFisBpMasterTranList_parameters = ["currcomp", "<%=sCurrComp%>", "bpadd", fisbptranAddArray, "bpremove", fisbptranRemoveArray];
            PageMethod("updateFisBpMasterTranList", updateFisBpMasterTranList_parameters, updateFisBpMasterTranList_succeedAjaxFn, updateFisBpMasterTranList_failedAjaxFn, false);
        });

        var updateFisBpMasterTranList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBpMasterTranList_succeedAjaxFn: " + textStatus);
            var updateFisBpMasterTranList_result = JSON.parse(data.d);
            if (updateFisBpMasterTranList_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(updateFisBpMasterTranList_result.message);
            }
            console.log("updateFisBpMasterTranList_result.result: " + updateFisBpMasterTranList_result.result);
            console.log("updateFisBpMasterTranList_result.result: " + updateFisBpMasterTranList_result.message);
        }

        var updateFisBpMasterTranList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBpMasterTranList_failedAjaxFn: " + textStatus);
        }

        function openFisBpDetail(typ, bpid) {
            $('#FisBpDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddBp').hide();
                $('#btnModifyBp').show();

                var getFisBpDetail_parameters = ["currcomp", "<%=sCurrComp%>", "bpid", bpid];
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

