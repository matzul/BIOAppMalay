<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="BankPage.aspx.cs" Inherits="Accounting_BankPage" %>

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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Akaun Bank / Bank Account</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Id Bank:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBankId" name="txtFindBankId" type="text" size="10" maxlength="10" value="<%=sCurrBankId %>" class="input">
                        <div id="txtFindBankId-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Akaun No:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAccountNo" name="txtFindAccountNo" type="text" size="20" maxlength="20" value="<%=sCurrBankAcctNo %>" class="input">
                        <div id="txtFindAccountNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Akaun Bank / List of Bank Account</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="10%" valign="middle" align="left" class="tblTitle3Mod">Id Bank</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama Bank</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">No. Akaun</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Matawang</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisBank.Count > 0)
                        {
                            for (int i = 0; i < lsFisBank.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisBank[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=modAcc.GetSetbankid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbankname %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccountno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcurrency %></td>
                        <td valign="top" align="right"><%=modAcc.GetSetdebit %></td>
                        <td valign="top" align="right"><%=modAcc.GetSetcredit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Kemaskini</button>
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
                                <td width="50%" height="15" align="left"><%=lsFisBank.Count %> record(s)</td>
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
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Daftar Akaun Bank" onclick="openFisBankDetail('NEW', '');">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisBankDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisBankDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Akaun Bank</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Id Bank</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtBankId" name="txtBankId" type="text" size="20" maxlength="20" value="" class="input">
                                            <div id="txtBankId-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Bank</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBankName" name="txtBankName" size="50" maxlength="50" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Alamat</td>
                                        <td width="80%" class="tblText2">
                                            <!--<input type="text" id="txtBankAddress" name="txtBankAddress" size="100" maxlength="100" value="" class="input" />-->
                                            <textarea id="txtBankAddress" class="input" rows="3" cols="50" name="txtBankAddress"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Akaun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccNo" name="txtAccNo" size="20" maxlength="20" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Matawang</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtCurrency" name="txtCurrency" class="input" />
                                            <div id="txtCurrency-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kadar Tukaran</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtExcgRate" name="txtExcgRate" class="input" /></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Pegawai Bank</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtContactPIC" name="txtContactPIC" size="50" maxlength="50" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Telefon</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtContactNo" name="txtContactNo" size="20" maxlength="20" value="" class="input" />
                                        </td>
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
                                            <button id="btnAddBank" name="btnAddBank" type="button" class="button1 btn-primary" onclick="insertFisBankDetail();">Tambah</button>
                                            <button id="btnModifyBank" name="btnModifyBank" type="button" class="button1 btn-primary" onclick="updateFisBankDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisBankDetail();">Tutup</button>
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

        var fisBankIdArray = [];
        var maxlengthdataautocomplete = 20;
        var fisBankAcctNoArray = [];
        var maxlengthdataautocomplete2 = 20;
        var fisBankCurrencyArray = [];
        var maxlengthdataautocomplete3 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindBankId").value = "";
                document.getElementById("txtFindAccountNo").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisBankList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisBankList", getFisBankList_parameters, getFisBankList_succeedAjaxFn, getFisBankList_failedAjaxFn, false);

        });

        var getFisBankList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisBankList_succeedAjaxFn: " + textStatus);
            var getFisBankList_result = JSON.parse(data.d);
            if (getFisBankList_result.result == "Y") {
                var bankid = '';
                var bankacctno = '';
                var bankcurrency = '';
                $.each(getFisBankList_result.fisbanklist, function (i, result) {
                    if (bankid != result.GetSetbankid + '-' + result.GetSetbankname) {
                        var objData = {};
                        objData.value = result.GetSetbankid + '-' + result.GetSetbankname;
                        objData.data = result.GetSetbankid;
                        fisBankIdArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        bankid = result.GetSetbankid + '-' + result.GetSetbankname;
                    }

                    if (bankacctno != result.GetSetaccountno + ': ' + result.GetSetbankid + '-' + result.GetSetbankname) {
                        var objData = {};
                        objData.value = result.GetSetaccountno + ': ' + result.GetSetbankid + '-' + result.GetSetbankname;
                        objData.data = result.GetSetaccountno;
                        fisBankAcctNoArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        bankacctno = result.GetSetaccountno + ': ' + result.GetSetbankid + '-' + result.GetSetbankname;
                    }

                    if (bankcurrency != result.GetSetcurrency) {
                        var objData = {};
                        objData.value = result.GetSetcurrency;
                        objData.data = result.GetSetcurrency;
                        fisBankCurrencyArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete3) {
                            maxlengthdataautocomplete3 = objData.value.length;
                        }
                        bankcurrency = result.GetSetcurrency;
                    }
                });
            }
            else {
                console.log("getFisBankList_result.result: " + getFisCOAList_result.result);
            }
            //console.log("fiscoaArray: " + JSON.stringify(fiscoaArray));
        }

        var getFisBankList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisBankList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindBankId').autocomplete({
            lookup: fisBankIdArray,
            appendTo: '#txtFindBankId-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindBankId').val(suggestion.data);
            }
        });

        $('#txtFindAccountNo').autocomplete({
            lookup: fisBankAcctNoArray,
            appendTo: '#txtFindAccountNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindAccountNo').val(suggestion.data);
            }
        });

        $('#txtBankId').autocomplete({
            lookup: fisBankIdArray,
            appendTo: '#txtBankId-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtBankId').val(suggestion.data);
            }
        });

        $('#txtCurrency').autocomplete({
            lookup: fisBankCurrencyArray,
            appendTo: '#txtCurrency-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtCurrency').val(suggestion.data);
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
                    openFisBankDetail('OPEN', id);
                }
            }
        });

        function openFisBankDetail(typ, id) {
            $('#FisBankDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddBank').hide();
                $('#btnModifyBank').show();

                var getFisBankDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getFisBankDetail", getFisBankDetail_parameters, getFisBankDetail_succeedAjaxFn, getFisBankDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddBank').show();
                $('#btnModifyBank').hide();
            }
        }

        var getFisBankDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisBankDetail_succeedAjaxFn: " + textStatus);
            var getFisBankDetail_result = JSON.parse(data.d);
            if (getFisBankDetail_result.result == "Y") {
                $('#hidId').val(getFisBankDetail_result.fisbankdetail.GetSetid);
                $('#txtBankId').val(getFisBankDetail_result.fisbankdetail.GetSetbankid);
                $('#txtBankName').val(getFisBankDetail_result.fisbankdetail.GetSetbankname);
                $('#txtBankAddress').val(getFisBankDetail_result.fisbankdetail.GetSetaddress);
                $('#txtAccNo').val(getFisBankDetail_result.fisbankdetail.GetSetaccountno);
                $('#txtCurrency').val(getFisBankDetail_result.fisbankdetail.GetSetcurrency);
                $('#txtExcgRate').val(getFisBankDetail_result.fisbankdetail.GetSetexrate);
                $('#txtContactPIC').val(getFisBankDetail_result.fisbankdetail.GetSetcontact);
                $('#txtContactNo').val(getFisBankDetail_result.fisbankdetail.GetSetcontactno);
                $('#lsStatus').val(getFisBankDetail_result.fisbankdetail.GetSetstatus);
            }
            else {
                alert(getFisBankDetail_result.message);
            }
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.result);
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.message);
        }

        var getFisBankDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisBankDetail_failedAjaxFn: " + textStatus);
        }

        function insertFisBankDetail() {
            var insertFisBankDetail_parameters = ["currcomp", "<%=sCurrComp%>", "bankid", $('#txtBankId').val(), "bankname", $('#txtBankName').val(), "address", $('#txtBankAddress').val(), "accnumber", $('#txtAccNo').val(), "currency", $('#txtCurrency').val(), "exrate", $('#txtExcgRate').val(), "contact", $('#txtContactPIC').val(), "contactno", $('#txtContactNo').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisBankDetail", insertFisBankDetail_parameters, insertFisBankDetail_succeedAjaxFn, insertFisBankDetail_failedAjaxFn, false);
        }

        var insertFisBankDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisBankDetail_succeedAjaxFn: " + textStatus);
            var insertFisBankDetail_result = JSON.parse(data.d);
            if (insertFisBankDetail_result.result == "Y") {
                alert(insertFisBankDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisBankDetail_result.message);
            }
        }

        var insertFisBankDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisBankDetail_failedAjaxFn: " + textStatus);
        }

        function updateFisBankDetail() {
            var updateFisBankDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "bankid", $('#txtBankId').val(), "bankname", $('#txtBankName').val(), "address", $('#txtBankAddress').val(), "accnumber", $('#txtAccNo').val(), "currency", $('#txtCurrency').val(), "exrate", $('#txtExcgRate').val(), "contact", $('#txtContactPIC').val(), "contactno", $('#txtContactNo').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisBankDetail", updateFisBankDetail_parameters, updateFisBankDetail_succeedAjaxFn, updateFisBankDetail_failedAjaxFn, false);
        }

        var updateFisBankDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBankDetail_succeedAjaxFn: " + textStatus);
            var updateFisBankDetail_result = JSON.parse(data.d);
            if (updateFisBankDetail_result.result == "Y") {
                alert(updateFisBankDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisBankDetail_result.message);
            }
        }

        var updateFisBankDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBankDetail_failedAjaxFn: " + textStatus);
        }

        function closeFisBankDetail() {
            resetFisBankDetail();
            $('#FisBankDetail').modal('hide');
        }

        function resetFisBankDetail() {
            $('#hidId').val("");
            $('#txtBankId').val("");
            $('#txtBankName').val("");
            $('#txtBankAddress').val("");
            $('#txtAccNo').val("");
            $('#txtCurrency').val("");
            $('#txtExcgRate').val("");
            $('#txtContactPIC').val("");
            $('#txtContactNo').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

