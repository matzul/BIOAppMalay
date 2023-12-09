<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="OBCBPage.aspx.cs" Inherits="Accounting_OBCBPage" %>

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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pembukaan & Penutupan Baki Akaun / Opening & Closing Balance</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input">
                        <div id="txtFindFyr-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Pilihan:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindOption" name="lsFindOption">
                            <option value="" selected>-Select-</option>
                            <option value="OPENING_BALANCE" <%=sCurrType.Equals("OPENING_BALANCE") ? "selected" : "" %>>Pembukaan Baki Akaun</option>
                            <option value="CLOSING_BALANCE" <%=sCurrType.Equals("CLOSING_BALANCE") ? "selected" : "" %>>Penutupan Baki Akaun</option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai BAKI BUKA/ TUTUP / List of OPENING/ CLOSING BALANCE</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="5%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Currency</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisBalance.Count > 0)
                        {
                            for (int i = 0; i < lsFisBalance.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisBalance[i];
                                String enableddisabled = "";
                                if (modAcc.GetSetstatus.Equals("CONFIRMED") || modAcc.GetSetstatus.Equals("CANCELLED"))
                                {
                                    enableddisabled = "disabled";
                                }
                                else
                                {
                                    enableddisabled = "enabled";
                                }
                                /*
                                if (modAcc.GetSettrancode.Equals("CLOSING_BALANCE"))
                                {
                                    enableddisabled = "disabled";
                                }
                                else
                                {
                                    if (modAcc.GetSetstatus.Equals("CONFIRMED"))
                                    {
                                        enableddisabled = "disabled";
                                    }
                                    else
                                    {
                                        enableddisabled = "enabled";
                                    }
                                }
                                */
                                modAcc.GetSetdatefrom = oAccCon.getLastFisBalance(modAcc.GetSetcomp, modAcc.GetSetfyr, "OPENING_BALANCE", modAcc.GetSettrandate, "").GetSettrandate;
                                modAcc.GetSetdateto = modAcc.GetSettrandate;

                                if (modAcc.GetSettrancode.Equals("CLOSING_BALANCE") && modAcc.GetSetstatus.Equals("CONFIRMED"))
                                {
                                    modAcc.GetSetitemcat = "CLOSED";

                                }
                                else if (modAcc.GetSettrancode.Equals("CLOSING_BALANCE") && modAcc.GetSetstatus.Equals("NEW"))
                                {
                                    modAcc.GetSetitemcat = "CURRENT";

                                }
                    %>
                    <tr data-id="<%=modAcc.GetSetid %>" class="tblText1">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrancode %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrandate %></td>
                        <td valign="top" align="left">
                            <%=modAcc.GetSettrandesc %><br />
                            <%
                                if (modAcc.GetSettrancode.Equals("CLOSING_BALANCE") && modAcc.GetSetitemcat.Equals("CLOSED"))
                                {
                            %>
                            <button type="button" class="button1 btn-warning" onclick="openBalanceSheetDetails('OPEN','<%=modAcc.GetSetfyr %>','CLOSED_BALANCE_SHEET','Laporan Penyata Kedudukan Kewangan Bagi Penutupan Baki Akaun <%=modAcc.GetSettrandate %>','<%=modAcc.GetSetdatefrom %>','<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Penyata Kewangan</button>
                            <button type="button" class="button1 btn-warning" onclick="openProfitLossDetails('OPEN','<%=modAcc.GetSetfyr %>','CLOSED_PROFIT_LOSS','Laporan Penyata Pendapatan Bagi Penutupan Baki Akaun <%=modAcc.GetSettrandate %>','<%=modAcc.GetSetdatefrom %>','<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Penyata Pendapatan</button>
                            <button type="button" class="button1 btn-warning" onclick="openTrialBalanceDetails('OPEN','<%=modAcc.GetSetfyr %>','CLOSED_TRIAL_BALANCE','Laporan Penyata Imbangan Bagi Penutupan Baki Akaun <%=modAcc.GetSettrandate %>','<%=modAcc.GetSetdatefrom %>','<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Penyata Imbangan</button>
                            <%
                                }
                                else if (modAcc.GetSettrancode.Equals("CLOSING_BALANCE") && modAcc.GetSetitemcat.Equals("CURRENT"))
                                {
                            %>
                            <button type="button" class="button1 btn-warning" onclick="openBalanceSheetDetails('OPEN','<%=modAcc.GetSetfyr %>','CURRENT_BALANCE_SHEET','Laporan Penyata Kedudukan Kewangan Semasa','<%=modAcc.GetSetdatefrom %>','<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Penyata Kewangan</button>
                            <button type="button" class="button1 btn-warning" onclick="openProfitLossDetails('OPEN','<%=modAcc.GetSetfyr %>','CURRENT_PROFIT_LOSS','Laporan Penyata Pendapatan Semasa', '<%=modAcc.GetSetdatefrom %>', '<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Untung & Rugi</button>
                            <button type="button" class="button1 btn-warning" onclick="openTrialBalanceDetails('OPEN','<%=modAcc.GetSetfyr %>','CURRENT_TRIAL_BALANCE','Laporan Penyata Imbangan Semasa','<%=modAcc.GetSetdatefrom %>','<%=modAcc.GetSetdateto %>','<%=modAcc.GetSetcurrency %>','<%=modAcc.GetSetstatus %>');">Lembaran Imbangan</button>
                            <%
                                }
                            %>
                        </td>
                        <td valign="top" align="left"><%=modAcc.GetSetcurrency %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdebit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcredit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" data-action="open" class="button_warning <%=enableddisabled%>" onclick="openFisBalance('OPEN','<%=modAcc.GetSetid %>');" <%=enableddisabled%>>Kemaskini</button>
                            <button type="button" data-action="open_trans" class="button_warning enabled" onclick="openFisBalanceDetails('OPEN','<%=modAcc.GetSettranno %>','<%=modAcc.GetSettrancode %>');">Transaksi Lejer</button>
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

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsFisBalance.Count %> record(s)</td>
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
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Daftar Baki Akaun" onclick="openFisBalance('NEW', '');">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisBalance" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisCOADetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Baki Akaun</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtFyr" name="txtFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input disabled" readonly>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Transaksi</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtTranNo" name="txtTranNo" size="10" class="input disabled" readonly/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsTranCode" name="lsTranCode" class="select disabled">
                                                <option value="OPENING_BALANCE">Pembukaan Baki Akaun</option>
                                                <option value="CLOSING_BALANCE">Penutupan Baki Akaun</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtTranDesc" name="txtTranDesc" size="50" class="input enabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtTranDate" name="txtTranDate" class="input enabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Currency</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtCurrency" name="txtCurrency" class="input enabled" />
                                            <div id="txtCurrency-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Debit</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtDebit" name="txtDebit" class="input enabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kredit</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtCredit" name="txtCredit" class="input enabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Baki</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtBalance" name="txtBalance" class="input enabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <!--
                                                <option value="NEW">NEW</option>
                                                <option value="CONFIRMED">CONFIRMED</option>
                                                <option value="CANCELLED">CANCELLED</option>
                                                -->
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddFisBalance" name="btnAddFisBalance" type="button" class="button1 btn-primary" onclick="addFisBalance();">Daftar</button>
                                            <button id="btnModifyFisBalance" name="btnModifyFisBalance" type="button" class="button1 btn-primary" onclick="updateFisBalance();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisBalance();">Tutup</button>
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

        var fisFYRArray = [];
        var maxlengthdataautocomplete = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindFyr").value = "<%=sCurrFyr%>";
                document.getElementById("lsFindOption").selectedIndex = 0;
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisFYRList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisFYRList", getFisFYRList_parameters, getFisFYRList_succeedAjaxFn, getFisFYRList_failedAjaxFn, false);

        });

        var getFisFYRList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisFYRList_succeedAjaxFn: " + textStatus);
            var getFisFYRList_result = JSON.parse(data.d);
            if (getFisFYRList_result.result == "Y") {
                $.each(getFisFYRList_result.fisfyrlist, function (i, result) {
                    var objData = {};
                    objData.value = result.GetSetfyrdesc;
                    objData.data = result.GetSetfyrid;
                    fisFYRArray.push(objData);
                });
            }
            else {
                console.log("getFisFYRList_result.result: " + getFisFYRList_result.result);
            }
        }

        var getFisFYRList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisFYRList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindFyr').autocomplete({
            lookup: fisFYRArray,
            appendTo: '#txtFindFyr-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindFyr').val(suggestion.data);
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
                if (action == 'open') {
                    //alert('delete:' + id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                }
            }
        });

        function openFisBalanceDetails(typ, tranno, trancode) {
            if (typ == 'OPEN') {
                fOpenWindow('OBCBDetailsPage.aspx?fyr=<%=sCurrFyr %>&tranno=' + tranno + '&trancode=' + trancode);
            }
        }

        function openFisBalance(typ, id) {
            $('#FisBalance').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                $('#lsTranCode').prop('disabled', true);
                $('#lsTranCode').removeClass('enabled').addClass('disabled')
                $('#btnAddFisBalance').hide();
                $('#btnModifyFisBalance').show();

                var getFisBalance_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", $('#txtFindFyr').val(), "id", id];
                PageMethod("getFisBalance", getFisBalance_parameters, getFisBalance_succeedAjaxFn, getFisBalance_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                var select = document.getElementById("lsStatus");
                for (var option in select) {
                    select.remove(option);
                }
                document.getElementById("lsStatus").add(new Option("NEW", "NEW"));
                $('#lsTranCode').prop('disabled', false);
                $('#lsTranCode').removeClass('disabled').addClass('enabled')
                $('#btnAddFisBalance').show();
                $('#btnModifyFisBalance').hide();
            }
        }

        var getFisBalance_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisBalance_succeedAjaxFn: " + textStatus);
            var getFisBalance_result = JSON.parse(data.d);
            if (getFisBalance_result.result == "Y") {
                $('#hidId').val(getFisBalance_result.fisbalance.GetSetid);
                $('#txtFyr').val(getFisBalance_result.fisbalance.GetSetfyr);
                $('#txtTranNo').val(getFisBalance_result.fisbalance.GetSettranno);
                $('#lsTranCode').val(getFisBalance_result.fisbalance.GetSettrancode);
                $('#txtTranDesc').val(getFisBalance_result.fisbalance.GetSettrandesc);
                $('#txtTranDate').val(getFisBalance_result.fisbalance.GetSettrandate);
                $('#txtCurrency').val(getFisBalance_result.fisbalance.GetSetcurrency);
                $('#txtDebit').val(getFisBalance_result.fisbalance.GetSetdebit);
                $('#txtCredit').val(getFisBalance_result.fisbalance.GetSetcredit);
                $('#txtBalance').val(0);
                var select = document.getElementById("lsStatus");
                for (var option in select) {
                    select.remove(option);
                }
                document.getElementById("lsStatus").add(new Option("NEW", "NEW"));
                if (getFisBalance_result.fisbalance.GetSettrancode == 'OPENING_BALANCE') {
                    //document.getElementById("lsStatus").add(new Option("CONFIRMED", "CONFIRMED"));
                    //document.getElementById("lsStatus").add(new Option("CANCELLED", "CANCELLED"));
                }
                else if (getFisBalance_result.fisbalance.GetSettrancode == 'CLOSING_BALANCE') {
                    document.getElementById("lsStatus").add(new Option("CANCELLED", "CANCELLED"));
                }
                $('#lsStatus').val(getFisBalance_result.fisbalance.GetSetstatus);
                if (getFisBalance_result.fisbalance.GetSetstatus == 'CONFIRMED') {
                    $('#btnModifyFisBalance').hide();
                }
            }
            else {
                alert(getFisBalance_result.message);
            }
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.result);
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.message);
        }

        var getFisBalance_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisBalance_failedAjaxFn: " + textStatus);
        }

        function addFisBalance() {
            var insertFisBalance_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", $('#txtFindFyr').val(), "trancode", $('#lsTranCode').val(), "trandate", $('#txtTranDate').val(), "trandesc", $('#txtTranDesc').val(), "currency", $('#txtCurrency').val(), "debit", $('#txtDebit').val(), "credit", $('#txtCredit').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisBalance", insertFisBalance_parameters, insertFisBalance_succeedAjaxFn, insertFisBalance_failedAjaxFn, false);
        }

        var insertFisBalance_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisBalance_succeedAjaxFn: " + textStatus);
            var insertFisBalance_result = JSON.parse(data.d);
            if (insertFisBalance_result.result == "Y") {
                alert(insertFisBalance_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisBalance_result.message);
            }
        }

        var insertFisBalance_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisBalance_failedAjaxFn: " + textStatus);
        }

        function updateFisBalance() {
            var updateFisBalance_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", $('#txtFindFyr').val(), "id", $('#hidId').val(), "trandate", $('#txtTranDate').val(), "trandesc", $('#txtTranDesc').val(), "currency", $('#txtCurrency').val(), "debit", $('#txtDebit').val(), "credit", $('#txtCredit').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisBalance", updateFisBalance_parameters, updateFisBalance_succeedAjaxFn, updateFisBalance_failedAjaxFn, false);
        }

        var updateFisBalance_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisBalance_succeedAjaxFn: " + textStatus);
            var updateFisBalance_result = JSON.parse(data.d);
            if (updateFisBalance_result.result == "Y") {
                alert(updateFisBalance_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisBalance_result.message);
            }
        }

        var updateFisBalance_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisBalance_failedAjaxFn: " + textStatus);
        }

        function closeFisBalance() {
            resetFisBalance();
            $('#FisBalance').modal('hide');
        }

        function resetFisBalance() {
            $('#hidId').val("");
            $('#txtFyr').val("");
            $('#txtTranNo').val("");
            $('#lsTranCode').val("");
            $('#txtCurrency').val("");
            $('#txtTranDesc').val("");
            $('#txtTranDate').val("");
            $('#txtDebit').val("");
            $('#txtCredit').val("");
            $('#txtBalance').val("");
            $('#lsStatus').val("");
        }

        function openBalanceSheetDetails(typ, fyr, itemtype, itemtypedesc, datefrom, dateto, currency, status) {
            if (typ == 'OPEN') {
                fOpenWindow('BalanceSheetDetailsPage.aspx?fyr=' + fyr + '&itemtype=' + itemtype + '&itemtypedesc=' + itemtypedesc + '&datefrom=' + datefrom + '&dateto=' + dateto + '&currency=' + currency + '&status=' + status);
            }
        }
        function openTrialBalanceDetails(typ, fyr, itemtype, itemtypedesc, datefrom, dateto, currency, status) {
            if (typ == 'OPEN') {
                fOpenWindow('TrialBalanceDetailsPage.aspx?fyr=' + fyr + '&itemtype=' + itemtype + '&itemtypedesc=' + itemtypedesc + '&datefrom=' + datefrom + '&dateto=' + dateto + '&currency=' + currency + '&status=' + status);
            }
        }
        function openProfitLossDetails(typ, fyr, itemtype, itemtypedesc, datefrom, dateto, currency, status) {
            if (typ == 'OPEN') {
                fOpenWindow('ProfitLossDetailsPage.aspx?fyr=' + fyr + '&itemtype=' + itemtype + '&itemtypedesc=' + itemtypedesc + '&datefrom=' + datefrom + '&dateto=' + dateto + '&currency=' + currency + '&status=' + status);
            }
        }

    </script>
</asp:Content>

