<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="LedgerTranPage.aspx.cs" Inherits="Accounting_LedgerTranPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Transaksi Lejer / Ledger Transactions</a></td>
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
                    <td width="20%" class="tblTextCommon">Tarikh Transaksi</td>
                    <td colspan="20" class="tblText2">Dari:
                        <input class="input" name="txtFindFromDate" id="txtFindFromDate" type="text" value="<%=sCurrDateFrom %>" size="14" maxlength="16">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindFromDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindFromDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y %H:%M:%S",   	// format of the input field
                                daFormat: "%d-%m-%Y %H:%M:%S",
                                timeFormat: "24",
                                button: "imgFindFromDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                showsTime: true,
                                singleClick: true
                            });
                        </script>
                        Hingga:
                        <input class="input" name="txtFindToDate" id="txtFindToDate" type="text" value="<%=sCurrDateTo %>" size="14" maxlength="16">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindToDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindToDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y %H:%M:%S",   	// format of the input field
                                daFormat: "%d-%m-%Y %H:%M:%S",
                                timeFormat: "24",
                                button: "imgFindToDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                showsTime: true,
                                singleClick: true
                            });
                        </script>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Status:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindStatus" name="lsFindStatus">
                            <option value="" <%=sCurrStatus.Equals("") ? "selected" : "" %>>-Pilihan-</option>
                            <option value="NEW" <%=sCurrStatus.Equals("NEW") ? "selected" : "" %>>NEW</option>
                            <option value="CONFIRMED" <%=sCurrStatus.Equals("CONFIRMED") ? "selected" : "" %>>CONFIRMED</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Debit:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindDebit" name="txtFindDebit" type="text" size="6" maxlength="15" value="0" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kredit:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindCredit" name="txtFindCredit" type="text" size="6" maxlength="15" value="0" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Baki:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindBalance" name="txtFindBalance" type="text" size="6" maxlength="15" value="0" class="input disabled" readonly>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Master Carta Akaun / Master of Chart of Account (COA)</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod"></td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">No. Transaksi</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Kod Transaksi</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Lejer</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Rujukan</td>
                    </tr>
                    <%
                        if (lsFisLedgerTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisLedgerTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisLedgerTran[i];
                                dTotalDebit = dTotalDebit + modAcc.GetSetdebit;
                                dTotalCredit = dTotalCredit + modAcc.GetSetcredit;
                                if(modAcc.GetSetacctype.Equals("A") || modAcc.GetSetacctype.Equals("B"))
                                {
                                    sLeftRight = "LEFT";
                                }
                                else
                                {
                                    sLeftRight = "RIGHT";
                                }
                    %>
                    <tr class="tblText1">
                        <td valign="top" align="center"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccdesc %></td>
                        <td valign="top" align="left"><a href="#" onclick="openFisLedgerDetail('<%=modAcc.GetSetaccid %>','<%=modAcc.GetSettranno %>','<%=modAcc.GetSettrancode %>');" ><%=modAcc.GetSettranno %></a></td>
                        <td valign="top" align="left"><a href="#" onclick="openFisLedgerDetail('<%=modAcc.GetSetaccid %>','<%=modAcc.GetSettranno %>','<%=modAcc.GetSettrancode %>');" ><%=modAcc.GetSettrancode %></a></td>
                        <td valign="top" align="left"><%=modAcc.GetSetledgerdate %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdebit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcredit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetrefno %></td>
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
                                <td width="50%" height="15" align="left"><%=lsFisLedgerTran.Count %> record(s)</td>
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
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisLedgerDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">
                                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Transaksi Lejer</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                                        <td width="80%" class="tblText2"><span id="detfyr" name="detfyr"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Transaksi:</td>
                                        <td width="80%" class="tblText2"><span id="dettranno" name="dettranno">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kod Transaksi:</td>
                                        <td width="80%" class="tblText2"><span id="dettrancode" name="dettrancode">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh Lejer:</td>
                                        <td width="80%" class="tblText2"><span id="detledgerdate" name="detledgerdate">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Currency:</td>
                                        <td width="80%" class="tblText2"><span id="detcurrency" name="detcurrency">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tbFisLedgerDetail" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td height="20" width="12%" valign="middle" align="left" class="tblTitle3Mod">No. Lejer</td>
                                        <td width="20%" align="left" valign="left" class="tblTitle3Mod">Keterangan</td>
                                        <td width="10%" align="left" valign="left" class="tblTitle3Mod">Sub Koding</td>
                                        <td width="10%" valign="middle" align="right" class="tblTitle3Mod">Debit</td>
                                        <td width="10%" valign="middle" align="right" class="tblTitle3Mod">Kredit</td>
                                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Rujukan</td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button type="button" class="button1" onclick="closeFisLedgerDetail();">Tutup</button>
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
                document.getElementById("lsFindType").selectedIndex = 0;
                document.getElementById("lsFindCategory").selectedIndex = 0;
                document.getElementById("lsFindOption").selectedIndex = 0;
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            $('#txtFindDebit').val('<%=dTotalDebit %>');
            $('#txtFindCredit').val('<%=dTotalCredit %>');
            $('#txtFindBalance').val('<%=sLeftRight.Equals("LEFT")?dTotalDebit-dTotalCredit:dTotalCredit-dTotalDebit %>');

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>"];
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

            var trid = $(target).closest("[data-accid]");
            //get data-accid value for the TR
            var accid = (trid.data("accid"));

            var action = target.getAttribute('data-action')
            if (action) {
                //alert(action);
                if (action == 'select') {
                    /*
                    if ($(target).hasClass('disabled')) {
                        $(target).removeClass('disabled').addClass('enabled');
                        $(target).prop('checked', true);
                    }
                    else {
                        if ($(target).prop('checked')) {

                            var i = fiscoatranStoredArray.indexOf(accid);
                            if (i == -1) {
                                if (fiscoatranAddArray.indexOf(accid) == -1)
                                    fiscoatranAddArray.push(accid);
                            } else {

                            }
                            var j = fiscoatranRemoveArray.indexOf(accid);
                            if (j >= 0) {
                                fiscoatranRemoveArray.splice(j, 1);
                            }
                        } else {

                            var i = fiscoatranStoredArray.indexOf(accid);
                            if (i >= 0) {
                                if (fiscoatranRemoveArray.indexOf(accid) == -1)
                                    fiscoatranRemoveArray.push(accid);
                            }
                            else {

                            }
                            var j = fiscoatranAddArray.indexOf(accid);
                            if (j >= 0) {
                                fiscoatranAddArray.splice(j, 1);
                            }
                        }
                    }
                    //alert('Stored:' + fiscoatranStoredArray + '\n' + 'Add:' + fiscoatranAddArray + '\n' + 'Remove:' + fiscoatranRemoveArray);
                    //alert('Add:'+fiscoatranAddArray);
                    //alert('Remove:'+fiscoatranRemoveArray);
                    */
                }
                else if (action == 'edit') {
                    /*
                    //alert('edit:' + accid);
                    openFisCOADetail('OPEN', accid);
                    */
                }
            }
        });

        function openFisLedgerDetail(accid, tranno, trancode) {
            $('#FisLedgerDetail').modal({ backdrop: "static" });

            var getFisLedgerDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr %>", "accid", accid, "tranno", tranno, "trancode", trancode];
            PageMethod("getFisLedgerDetail", getFisLedgerDetail_parameters, getFisLedgerDetail_succeedAjaxFn, getFisLedgerDetail_failedAjaxFn, false);
        }

        var getFisLedgerDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisLedgerDetail_succeedAjaxFn: " + textStatus);
            var getFisLedgerDetail_result = JSON.parse(data.d);
            if (getFisLedgerDetail_result.result == "Y") {
                $.each(getFisLedgerDetail_result.fisledger, function (i, result) {
                    var strdesc = '<tr class="tblText1"> \
                                      <td class="tblText2">'+ result.GetSetaccid +'</td> \
                                      <td class="tblText2">'+ result.GetSetaccdesc +'</td> \
                                      <td class="tblText2">'+ result.GetSetacccode +'</td> \
                                      <td class="tblText2">'+ result.GetSetdebit + '</td> \
                                      <td class="tblText2">'+ result.GetSetcredit + '</td> \
                                      <td class="tblText2">'+ result.GetSetstatus + '</td> \
                                      <td class="tblText2">'+ result.GetSetrefno + '</td> \
                                   </tr >';
                    $('#tbFisLedgerDetail').append(strdesc);

                    $('#detfyr').text(result.GetSetfyr);
                    $('#dettranno').text(result.GetSettranno);
                    $('#dettrancode').text(result.GetSettrancode);
                    $('#detledgerdate').text(result.GetSetledgerdate);
                    $('#detcurrency').text(result.GetSetcurrency);
                });
            }
            else {
                alert(getFisLedgerDetail_result.message);
            }
        }

        var getFisLedgerDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisLedgerDetail_failedAjaxFn: " + textStatus);
        }

        function closeFisLedgerDetail() {
            resetFisLedgerDetail();
            $('#FisLedgerDetail').modal('hide');
        }

        function resetFisLedgerDetail() {
            var el = $('#tbFisLedgerDetail .tblText1');
            $(el).closest("tr").remove();
            $('#detfyr').text('');
            $('#dettranno').text('');
            $('#dettrancode').text('');
            $('#detledgerdate').text('');
            $('#detcurrency').text('');

        }

    </script>
</asp:Content>

