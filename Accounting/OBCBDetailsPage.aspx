<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="OBCBDetailsPage.aspx.cs" Inherits="Accounting_OBCBDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pembukaan & Penutupan Baki Akaun / Opening & Closing Balance</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">No. Koding:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindLedgerNo" name="txtFindLedgerNo" type="text" size="20" maxlength="20" value="<%=sCurrAccId %>" class="input">
                        <div id="txtFindLedgerNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindType" name="lsFindType">
                            <option value="" selected>-Semua-</option>
                            <option value="A" <%=sCurrType.Equals("A") ? "selected" : "" %>>Aset</option>
                            <option value="L" <%=sCurrType.Equals("L") ? "selected" : "" %>>Liabiliti</option>
                            <option value="E" <%=sCurrType.Equals("E") ? "selected" : "" %>>Equiti/Modal</option>
                            <option value="H" <%=sCurrType.Equals("H") ? "selected" : "" %>>Hasil</option>
                            <option value="B" <%=sCurrType.Equals("B") ? "selected" : "" %>>Belanja</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">No. Transaksi:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindTranNo" name="txtFindTranNo" type="text" size="10" maxlength="10" value="<%=iCurrTranNo %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kod Transaksi:</td>
                    <td width="80%" class="tblText2">
                        <select class="select disabled" id="lsFindTranCode" name="lsFindTranCode" disabled>
                            <option value="OPENING_BALANCE" <%=sCurrTranCode.Equals("OPENING_BALANCE") ? "selected" : "" %>>Pembukaan Baki Akaun</option>
                            <option value="CLOSING_BALANCE" <%=sCurrTranCode.Equals("CLOSING_BALANCE") ? "selected" : "" %>>Penutupan Baki Akaun</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Debit:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindDebit" name="txtFindDebit" type="text" size="6" maxlength="15" value="<%=modFisBalance.GetSetdebit %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kredit:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindCredit" name="txtFindCredit" type="text" size="6" maxlength="15" value="<%=modFisBalance.GetSetcredit %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Baki:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBalance" name="txtFindBalance" type="text" size="6" maxlength="15" value="<%=modFisBalance.GetSetdebit-modFisBalance.GetSetcredit%>" class="input disabled" readonly>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Carta Akaun (BUKA/TUTUP) / List of Chart of Account (OB/CB)</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="20%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama Koding</td>
                        <!--<td width="13%" valign="middle" align="left" class="tblTitle3Mod">Sub-Koding</td>-->
                        <!--<td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>-->
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Koding Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Remarks</td>
                        <td></td>
                    </tr>
                    <%
                        if (lsFisBalanceDetails.Count > 0)
                        {
                            modFisBalance.GetSetdebit = 0;
                            modFisBalance.GetSetcredit = 0;
                            for (int i = 0; i < lsFisBalanceDetails.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisBalanceDetails[i];
                                //iCurrTranNo = modAcc.GetSettranno;
                    %>
                    <tr data-id="<%=modAcc.GetSetaccid %>" class="tblText1">
                        <td valign="top" align="left"><%=modAcc.GetSetaccid %></td>
                        <td valign="top" align="left"><a href="#" onclick="fOpenWindow2('LedgerTranPage.aspx?fyr=<%=sCurrFyr %>&accid=<%=modAcc.GetSetaccid %>&datefrom=<%=modAcc.GetSetdatefrom %>&dateto=<%=modAcc.GetSetdateto %>');"><%=modAcc.GetSetaccdesc %></a><br /><%=modAcc.GetSetaccnumber %></td>
                        <!--<td valign="top" align="left"><%=modAcc.GetSetaccnumber %></td>-->
                        <!--<td valign="top" align="left"><%=modAcc.GetSetfyr %></td>-->
                        <td valign="top" align="left"><%=modAcc.GetSetaccgroup %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetacctype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetparentid %><br/><%=modAcc.GetSetparentdesc %></td>
                        <td valign="top" align="left"><input id="txtDebit_<%=modAcc.GetSetid %>" name="txtDebit_<%=modAcc.GetSetid %>" type="text" size="6" maxlength="12" value="<%=modAcc.GetSetdebit %>" class="input debit"/></td>
                        <td valign="top" align="left"><input id="txtCrebit_<%=modAcc.GetSetid %>" name="txtCrebit_<%=modAcc.GetSetid %>" type="text" size="6" maxlength="12" value="<%=modAcc.GetSetcredit %>" class="input credit"/></td>
                        <td valign="top" align="center">
                            <textarea id="txtRemarks_<%=modAcc.GetSetid %>" name="id="txtRemarks_<%=modAcc.GetSetid %>"" rows="2" class="remarks"></textarea>
                        </td>
                        <td>
                    <%
                        if (modFisBalance.GetSetstatus == "NEW" && modFisBalance.GetSettrancode.Equals("OPENING_BALANCE"))
                        {
                    %>
                            <button type="button" data-action="open" class="button_warning enabled" onclick="deleteFisBalanceDetails('<%=modAcc.GetSetid %>');">Hapus</button>
                    <%
                        }
                        
                        //if (modFisBalance.GetSetstatus == "NEW" && modFisBalance.GetSettrancode.Equals("CLOSING_BALANCE"))
                        if (modFisBalance.GetSetstatus == "NEW")
                        {
                            modFisBalance.GetSetdebit = Math.Round(modFisBalance.GetSetdebit + Math.Round(modAcc.GetSetdebit,2),2);
                            modFisBalance.GetSetcredit = Math.Round(modFisBalance.GetSetcredit + Math.Round(modAcc.GetSetcredit,2),2);
                        }

                        if (modFisBalance.GetSettrancode.Equals("CLOSING_BALANCE"))
                        {
                            if (sStatusCases.Equals("CONFIRMED"))
                            {
                                if (!modAcc.GetSetstatus.Equals(sStatusCases))
                                {
                                    sStatusCases = modAcc.GetSetstatus;
                                }
                            }
                        }
                    %>
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

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsFisBalanceDetails.Count %> record(s)</td>
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
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Tambah" onclick="addFisBalanceDetails();">
                        <input class="button1a" id="btnSave" name="btnSave" type="button" value="Simpan" onclick="updateFisBalanceDetails();">
                        <input class="button1a" id="btnConfirm" name="btnConfirm" type="button" value="Confirm" onclick="confirmFisBalanceDetails();">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisBalanceDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisCOADetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Lejer</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsAccType" name="lsAccType" class="select">
                                                <option value="A">Aset</option>
                                                <option value="L">Liabiliti</option>
                                                <option value="E">Equiti/Modal</option>
                                                <option value="H">Hasil</option>
                                                <option value="B">Belanja</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kumpulan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccGroup" name="txtAccGroup" class="input" />
                                            <div id="txtAccGroup-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccId" name="txtAccId" class="input" />
                                            <div id="txtAccId-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccName" name="txtAccName" class="input disabled" readonly/></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Debit</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtDebit" name="txtDebit" class="input"/></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kredit</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtCredit" name="txtCredit" class="input"/></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Koding Rujukan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtParentId" name="txtParentId" class="input disabled" readonly/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccCat" name="txtAccCat" class="input disabled" readonly/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Sub-Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccCode" name="txtAccCode" class="input disabled" readonly />                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Akaun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccNumber" name="txtAccNumber" class="input disabled" readonly/>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddFisBalanceDetails" name="btnAddFisBalanceDetails" type="button" class="button1 btn-primary" onclick="insertFisBalanceDetails();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisBalanceDetails();">Tutup</button>
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

        var fiscoaArray = [];
        var maxlengthdataautocomplete = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindLedgerNo").value = "";
                document.getElementById("txtFindFyr").value = "<%=sCurrFyr %>";
                document.getElementById("lsFindType").selectedIndex = 0;

            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>"];
            PageMethod("getFisCOAList", getFisCOAList_parameters, getFisCOAList_succeedAjaxFn, getFisCOAList_failedAjaxFn, false);

        });

        var getFisCOAList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisCOAList_succeedAjaxFn: " + textStatus);
            var getFisCOAList_result = JSON.parse(data.d);
            if (getFisCOAList_result.result == "Y") {
                $.each(getFisCOAList_result.fiscoalist, function (i, result) {
                    var objData = {};
                    objData.value = result.GetSetaccid + '-' + result.GetSetaccdesc;
                    objData.data = result.GetSetaccid;
                    fiscoaArray.push(objData);
                    if (objData.value.length > maxlengthdataautocomplete) {
                        maxlengthdataautocomplete = objData.value.length;
                    }
                });
            }
            else {
                console.log("getFisCOAList_result.result: " + getFisCOAList_result.result);
            }
            //console.log("fiscoaArray: " + JSON.stringify(fiscoaArray));
        }

        var getFisCOAList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisCOAList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindLedgerNo').autocomplete({
            lookup: fiscoaArray,
            appendTo: '#txtFindLedgerNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindLedgerNo').val(suggestion.data);
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
                    //openFisBankDetail('OPEN', id);
                }
            }
        });

        var resultLooping = true;
        function updateFisBalanceDetails() {

            var fisbalanceupdate = [];

            $("tr.tblText1").each(function (i, tr) {
                var debit = $("input.debit", tr).val();
                var credit = $("input.credit", tr).val();
                var remarks = $("textarea.remarks", tr).val();
                var id = $(tr).data("id");

                fisbalanceupdate.push(id + '|' + debit + '|' + credit + '|' + remarks);

            });

            
            if (resultLooping) {

                var updateFisBalanceDetails2_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode%>", "fisbalanceupdate", fisbalanceupdate];
                PageMethod("updateFisBalanceDetails2", updateFisBalanceDetails2_parameters, updateFisBalanceDetails_succeedAjaxFn, updateFisBalanceDetails_failedAjaxFn, true);

            } else {
                alert('Error...Kemaskini Tidak Berjaya!');
            }
            
        }

        var updateFisBalanceDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBalanceDetails_succeedAjaxFn: " + textStatus);
            var updateFisBalanceDetails_result = JSON.parse(data.d);
            if (updateFisBalanceDetails_result.result == "Y") {
                alert(updateFisBalanceDetails_result.message);
                //actionclick('SEARCH');

                var updateFisBalanceInfo_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode %>"];
                PageMethod("updateFisBalanceInfo", updateFisBalanceInfo_parameters, updateFisBalanceInfo_succeedAjaxFn, updateFisBalanceInfo_failedAjaxFn, true);

            }
            else {
                resultLooping = false;
                //alert(updateFisBalanceDetails_result.message);
            }
        }

        var updateFisBalanceDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBalanceDetails_failedAjaxFn: " + textStatus);
        }

        var updateFisBalanceInfo_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBalanceInfo_succeedAjaxFn: " + textStatus);
            var updateFisBalanceInfo_result = JSON.parse(data.d);
            if (updateFisBalanceInfo_result.result == "Y") {
                //alert(updateFisBalanceInfo_result.message);
                actionclick('SEARCH');

            }
            else {
                alert(updateFisBalanceInfo_result.message);
            }
        }

        var updateFisBalanceInfo_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBalanceInfo_failedAjaxFn: " + textStatus);
        }

        function deleteFisBalanceDetails(id) {
            var deleteFisBalanceDetails_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "id", id];
            PageMethod("deleteFisBalanceDetails", deleteFisBalanceDetails_parameters, deleteFisBalanceDetails_succeedAjaxFn, deleteFisBalanceDetails_failedAjaxFn, true);
        }

        var deleteFisBalanceDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteFisBalanceDetails_succeedAjaxFn: " + textStatus);
            var deleteFisBalanceDetails_result = JSON.parse(data.d);
            if (deleteFisBalanceDetails_result.result == "Y") {
                alert(deleteFisBalanceDetails_result.message);

                var updateFisBalanceInfo_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode %>"];
                PageMethod("updateFisBalanceInfo", updateFisBalanceInfo_parameters, updateFisBalanceInfo_succeedAjaxFn, updateFisBalanceInfo_failedAjaxFn, true);

            }
            else {
                alert(deleteFisBalanceDetails_result.message);
            }
        }

        var deleteFisBalanceDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteFisBalanceDetails_failedAjaxFn: " + textStatus);
        }

        function addFisBalanceDetails() {
            $('#FisBalanceDetail').modal({ backdrop: "static" });
        }

        $('#txtAccGroup').focus(function () {
            var paramArray = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "acctype", $('#lsAccType').val()];
            var fn = "getLegderAccGroup";
            var paramList = '';
            var pagePath = window.location.pathname;
            var fisAccGroup = [];
            var maxlengthdatafisAccGroup = 20;

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }

            paramList = '{' + paramList + '}';
            console.log("getLegderAccGroup_paramList: " + paramList);

            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    console.log("getLegderAccGroup_succeedAjaxFn: " + textStatus);
                    var getLegderAccGroup_result = JSON.parse(data.d);
                    if (getLegderAccGroup_result.result == "Y") {
                        //fisAccGroup = [];
                        $.each(getLegderAccGroup_result.fisaccgroup, function (i, result) {
                            var objData = {};
                            objData.value = result.GetSetaccgroup + '-' + result.GetSetaccdesc;
                            objData.data = result.GetSetaccgroup;
                            fisAccGroup.push(objData);
                            if (objData.value.length > maxlengthdatafisAccGroup) {
                                maxlengthdatafisAccGroup = objData.value.length;
                            }
                        });
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
                        });
                    }
                    else {
                        $('#txtAccGroup').autocomplete("close");
                        console.log("getLegderAccGroup_result.message: " + getLegderAccGrou_result.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("getLegderAccGroup_failedAjaxFn: " + textStatus);
                },
                async: true
            });
            console.log("getLegderAccGroup_object: " + fisAccGroup);
        });

        $('#txtAccId').focus(function () {
            var paramArray = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "acctype", $('#lsAccType').val(), "accgrp", $('#txtAccGroup').val(), "endlevel", "Y"];
            var fn = "getLegderAccId";
            var paramList = '';
            var pagePath = window.location.pathname;
            var fisAccId = [];
            var maxlengthdatafisAccId = 20;

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }

            paramList = '{' + paramList + '}';
            console.log("getLegderAccId_paramList: " + paramList);

            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    console.log("getLegderAccId_succeedAjaxFn: " + textStatus);
                    var getLegderAccId_result = JSON.parse(data.d);
                    if (getLegderAccId_result.result == "Y") {
                        //fisAccId = [];
                        $.each(getLegderAccId_result.fisaccid, function (i, result) {
                            var objData = {};
                            objData.value = result.GetSetaccid + '-' + result.GetSetaccdesc;
                            objData.data = result.GetSetaccid;
                            fisAccId.push(objData);
                            if (objData.value.length > maxlengthdatafisAccId) {
                                maxlengthdatafisAccId = objData.value.length;
                            }
                        });
                        $('#txtAccId').autocomplete({
                            lookup: fisAccId,
                            appendTo: '#txtAccId-container',
                            minLength: 0,
                            minChars: 0,
                            width: maxlengthdatafisAccId * 12,
                            onSelect: function (suggestion) {
                                //console.log("suggestion: " + JSON.stringify(suggestion));
                                $('#txtAccId').val(suggestion.data);
                                //get details accid details
                                var getFisCOADetail_parameters = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                PageMethod("getFisCOADetail", getFisCOADetail_parameters, getFisCOADetail_succeedAjaxFn, getFisCOADetail_failedAjaxFn, false);
                            }
                        });
                    }
                    else {
                        $('#txtAccId').autocomplete("close");
                        console.log("getLegderAccId_result.message: " + getLegderAccId_result.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("getLegderAccId_failedAjaxFn: " + textStatus);
                },
                async: true
            });
            console.log("getLegderAccId_object: " + fisAccId);
        });

        var getFisCOADetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisCOADetail_succeedAjaxFn: " + textStatus);
            var getFisCOADetail_result = JSON.parse(data.d);
            if (getFisCOADetail_result.result == "Y") {
                $('#txtAccName').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                $('#txtAccCat').val(getFisCOADetail_result.fiscoadetail.GetSetacccategory);
                $('#txtAccCode').val(getFisCOADetail_result.fiscoadetail.GetSetacccode);
                $('#txtAccNumber').val(getFisCOADetail_result.fiscoadetail.GetSetaccnumber);
                $('#txtParentId').val(getFisCOADetail_result.fiscoadetail.GetSetparentid);
            }
            else {
                alert(getFisCOADetail_result.message);
            }
        }

        var getFisCOADetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisCOADetail_failedAjaxFn: " + textStatus);
        }


        function insertFisBalanceDetails() {
            if ($('#txtAccId').val().length > 0) {
                var insertFisBalanceDetails_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode%>", "accid", $('#txtAccId').val(), "exrate", <%=dCurrExRate%>, "debit", $('#txtDebit').val(), "credit", $('#txtCredit').val(), "refno", "", "remarks", ""];
                PageMethod("insertFisBalanceDetails", insertFisBalanceDetails_parameters, insertFisBalanceDetails_succeedAjaxFn, insertFisBalanceDetails_failedAjaxFn, true);
                
            } else {
                alert('Sila masukkan Akaun Id/ Ledger No!');
            }
        }

        var insertFisBalanceDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisBalanceDetails_succeedAjaxFn: " + textStatus);
            var insertFisBalanceDetails_result = JSON.parse(data.d);
            if (insertFisBalanceDetails_result.result == "Y") {
                alert(insertFisBalanceDetails_result.message);

                var updateFisBalanceInfo_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode %>"];
                PageMethod("updateFisBalanceInfo", updateFisBalanceInfo_parameters, updateFisBalanceInfo_succeedAjaxFn, updateFisBalanceInfo_failedAjaxFn, true);

            }
            else {
                alert(insertFisBalanceDetails_result.message);
            }
        }

        var insertFisBalanceDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisBalanceDetails_failedAjaxFn: " + textStatus);
        }


        function closeFisBalanceDetails() {
            resetFisBalanceDetails();
            $('#FisBalanceDetail').modal('hide');
        }

        function resetFisBalanceDetails() {
            $('#lsAccType').val("");
            $('#txtAccGroup').val("");
            $('#txtAccId').val("");
            $('#txtAccName').val("");
            $('#txtDebit').val("");
            $('#txtCredit').val("");
            $('#txtAccCat').val("");
            $('#txtAccCode').val("");
            $('#txtAccNumber').val("");
            $('#txtParentId').val("");
        }

        <%
        //if (modFisBalance.GetSetstatus == "NEW" && modFisBalance.GetSettrancode.Equals("OPENING_BALANCE"))
        if (modFisBalance.GetSetstatus == "NEW")
        {
        %>
        $('#txtFindDebit').val('<%=modFisBalance.GetSetdebit %>');
        $('#txtFindCredit').val('<%=modFisBalance.GetSetcredit %>');
        $('#txtFindBalance').val('<%=modFisBalance.GetSetdebit - modFisBalance.GetSetcredit %>');
        <%
        }
        %>

        <%
        if (modFisBalance.GetSetstatus == "CONFIRMED" || modFisBalance.GetSetstatus == "CANCELLED")
        {
        %>
        $('#btnAdd').hide();
        $('#btnSave').hide();
        $('#btnConfirm').hide();
        <%
        }
        else if (modFisBalance.GetSettrancode == "CLOSING_BALANCE")
        {
        %>
        $('#btnAdd').hide();
        $('#btnSave').hide();
        <%
        }
        else
        {
        %>
        //Comment out for a while to standard confirm the opening and closing @ OBCB Page Details (Transactions)
        //$('#btnConfirm').hide();
        <%
        }
        %>

        function confirmFisBalanceDetails()
        {
            <%
            if (sStatusCases.Equals("CONFIRMED"))
            {
                if (modFisBalance.GetSettrancode.Equals("CLOSING_BALANCE"))
                {
                    %>
                    var confirmFisBalanceDetails_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode%>"];
                    PageMethod("confirmFisBalanceDetails", confirmFisBalanceDetails_parameters, confirmFisBalanceDetails_succeedAjaxFn, confirmFisBalanceDetails_failedAjaxFn, true);
                    <%
                }
                else if (modFisBalance.GetSettrancode.Equals("OPENING_BALANCE"))
                {
                    //if (modFisBalance.GetSetdebit==modFisBalance.GetSetcredit)
                    if((modFisBalance.GetSetdebit-modFisBalance.GetSetcredit)==0)
                    {
                        %>
                        var confirmFisBalanceDetails_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", <%=iCurrTranNo %>, "trancode", "<%=sCurrTranCode%>"];
                        PageMethod("confirmFisBalanceDetails", confirmFisBalanceDetails_parameters, confirmFisBalanceDetails_succeedAjaxFn, confirmFisBalanceDetails_failedAjaxFn, true);
                        <%
                    }
                    else
                    {
                        %>
                        alert("Jumlah Debit & Kredit tidak bertepatan!\nDebit:<%=modFisBalance.GetSetdebit%>\nKredit:<%=modFisBalance.GetSetcredit%>");
                        <%
                    }
                }
            }
            else
            {
                %>
                alert("Sila CONFIRM semua transaksi posting terlebih dahulu!");
                <%
            }
            %>
        }

        var confirmFisBalanceDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("confirmFisBalanceDetails_succeedAjaxFn: " + textStatus);
            var confirmFisBalanceDetails_result = JSON.parse(data.d);
            if (confirmFisBalanceDetails_result.result == "Y") {
                alert(confirmFisBalanceDetails_result.message);
                actionclick('SEARCH');

            }
            else {
                alert(confirmFisBalanceDetails_result.message);
            }
        }

        var confirmFisBalanceDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("confirmFisBalanceDetails_failedAjaxFn: " + textStatus);
        }

    </script>
</asp:Content>

