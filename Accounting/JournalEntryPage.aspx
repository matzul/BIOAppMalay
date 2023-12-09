<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="JournalEntryPage.aspx.cs" Inherits="Accounting_JournalEntryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Table {
            display: table;
        }

        .Title {
            display: table-caption;
            text-align: center;
            font-weight: bold;
            font-size: larger;
        }

        .Heading {
            display: table-row;
            font-weight: bold;
            text-align: center;
        }

        .Row {
            display: table-row;
        }

        .Cell {
            display: table-cell;
            border: dashed;
            border-width: thin;
            padding-left: 5px;
            padding-right: 5px;
            width: 150px;
        }
    </style>
    <script type="text/javascript">
        var fiscoaArray = [];
        var maxlengthdataautocomplete = 20;
        var fiscoatranArray = [];
        var maxlengthdatatranautocomplete = 20;
        var acctype = "";
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Maklumat Transaksi Baucer / Voucher Transaction Details</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Baucar Jurnal</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=modJournalTran.GetSetfyr %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">No. Transaksi:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindTranNo" name="txtFindTranNo" type="text" size="20" maxlength="20" value="<%=modJournalTran.GetSettranno %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kod Transaksi:</td>
                    <td width="80%" class="tblText2">
                        <select class="select disabled" id="lsFindTranCode" name="lsFindTranCode" disabled>
                            <option value="" selected>-Semua-</option>
                            <option value="JOURNAL_VOUCHER" <%=modJournalTran.GetSettrancode.Equals("JOURNAL_VOUCHER") ? "selected" : "" %>>BAUCAR JURNAL</option>
                            <option value="PAYMENT_VOUCHER" <%=modJournalTran.GetSettrancode.Equals("PAYMENT_VOUCHER") ? "selected" : "" %>>BAUCAR BAYARAN</option>
                            <option value="RECEIPT_VOUCHER" <%=modJournalTran.GetSettrancode.Equals("RECEIPT_VOUCHER") ? "selected" : "" %>>BAUCAR TERIMAAN</option>
                            <option value="CASH_VOUCHER" <%=modJournalTran.GetSettrancode.Equals("CASH_VOUCHER") ? "selected" : "" %>>BAUCAR TUNAI</option>
                            <option value="DEBIT_NOTE" <%=modJournalTran.GetSettrancode.Equals("DEBIT_NOTE") ? "selected" : "" %>>NOTA DEBIT</option>
                            <option value="CREDIT_NOTE" <%=modJournalTran.GetSettrancode.Equals("CREDIT_NOTE") ? "selected" : "" %>>NOTA KREDIT</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <textarea id="txtFindTranDesc" class="textarea" name="txtFindTranDesc" cols="35" rows="2"><%=modJournalTran.GetSettrandesc %></textarea>
                    </td>
                </tr>

                <tr>
                    <td width="20%" class="tblTextCommon">Tarikh Transaksi</td>
                    <td colspan="20" class="tblText2">
                        <input class="input" name="txtFindTranDate" id="txtFindTranDate" type="text" value="<%=modJournalTran.GetSettrandate %>" size="15" maxlength="20">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindTranDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindTranDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y ",   	// format of the input field
                                button: "imgFindTranDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                singleClick: true
                            });
                        </script>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Currency:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindCurrency" name="txtFindCurrency" type="text" size="6" maxlength="15" value="<%=modJournalTran.GetSetcurrency %>" class="input" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Jumlah:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAmount" name="txtFindAmount" type="text" size="12" maxlength="12" value="<%=modJournalTran.GetSettranamount %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td width="20%" class="tblTextCommon">Status:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindStatus" name="lsFindStatus">
                            <option value="" <%=modJournalTran.GetSetstatus.Equals("") ? "selected" : "" %>>-Pilihan-</option>
                            <option value="NEW" <%=modJournalTran.GetSetstatus.Equals("NEW") ? "selected" : "" %>>NEW</option>
                            <option value="CONFIRMED" <%=modJournalTran.GetSetstatus.Equals("CONFIRMED") ? "selected" : "" %>>CONFIRMED</option>
                            <option value="CANCELLED" <%=modJournalTran.GetSetstatus.Equals("CANCELLED") ? "selected" : "" %>>CANCELLED</option>
                        </select>
                    </td>
                </tr>
            </table>
            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td height="30" width="20%"></td>
                    <td width="80%" align="left">
                        <%
                        if (modJournalTran.GetSetstatus.Equals("NEW")) { 
                            if (sAction.Equals("ADD"))
                            {
                        %>
                        <input class="button1" name="btnSave" type="button" value="Daftar" onclick="actionclick('SAVE');">
                        <%
                            }
                            else
                            {
                        %>
                        <input class="button1" name="btnUpdate" type="button" value="Kemaskini" onclick="actionclick('UPDATE');">
                        <input class="button1" name="btnCancel" type="button" value="Batal" onclick="actionclick('CANCEL');">
                        <%
                            }
                        }
                        %>
                    </td>
                </tr>
            </table>
            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Transaksi Baucer / List of Voucher Transactions</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="2%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">No Transaksi</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod Transaksi</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">No Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Transaksi</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Rakan Niaga</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="5%" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (modJournalTran.GetSetid > 0)
                        {
                    %>
                    <tr data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" class="tblText1">
                        <td valign="top" align="left">1</td>
                        <td valign="top" align="left"><%=modJournalTran.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSettranno %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSettrancode %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSetrefno %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSettrandate %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSettrandesc %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSettranamount %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSetbpid %><br />
                            <%=modJournalTran.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modJournalTran.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <input id="chk_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" name="chk_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" data-action="select" type="checkbox" <%=modJournalTran.GetSetstatus.Equals("CONFIRMED") ? "checked class='disabled' disabled" : "class='enabled'"%> />
                        </td>
                    </tr>
                    <%
                            if (modJournalTran.GetSetstatus.Equals("NEW") && lsFisJournalTran.Count > 1)
                            {

                    %>
                    <tr data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" class="tblText1">
                        <td data-id="" colspan="11" class="tblText2">
                            <button type="button" class="button_warning enabled" onclick="actionclick('NEWROW');">+</button>
                            <div class="content"></div>
                        </td>
                    </tr>
                    <%
                            }
                        }
                    %>
                    <tr data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" class="tblText1">
                        <td data-id="<%=modJournalTran.GetSetrefno %>" colspan="11" class="tblText2">
                            <div class="Table">
                                <div class="Heading">
                                    <div class="Cell">
                                        No. Lejer
                                    </div>
                                    <div class="Cell" style="width: 600px;">
                                        Keterangan
                                    </div>
                                    <div class="Cell">
                                    </div>
                                    <div class="Cell">
                                        Jumlah
                                    </div>
                                    <div class="Cell">
                                        
                                    </div>
                                </div>
                            </div>
                            <%
                                bool enabledcheckbox = true;

                                //ArrayList lsFisLedgerTran = oAccCon.getFisLedgerTranList(modJournalTran.GetSetcomp, modJournalTran.GetSetfyr, 0, "", 0, "", modJournalTran.GetSettranno, modJournalTran.GetSettrancode, "");
                                if (lsFisJournalTran.Count > 0)
                                {
                                    for (int j = 0; j < lsFisJournalTran.Count; j++)
                                    {
                                        AccountingModel modJournalDet = (AccountingModel)lsFisJournalTran[j];

                                        dTotalDebit = dTotalDebit + modJournalDet.GetSetdebit;
                                        dTotalCredit = dTotalCredit + modJournalDet.GetSetcredit;

                                        if (enabledcheckbox)
                                        {
                                            if (modJournalDet.GetSetledgerno.Equals(1))
                                            {
                                                if (modJournalDet.GetSettranamount == modJournalDet.GetSetdebit)
                                                {
                                                    enabledcheckbox = true;
                                                }
                                                else
                                                {
                                                    enabledcheckbox = false;
                                                }
                                            }
                                            else
                                            {
                                                if (modJournalDet.GetSettranamount == modJournalDet.GetSetcredit)
                                                {
                                                    enabledcheckbox = true;
                                                }
                                                else
                                                {
                                                    enabledcheckbox = false;
                                                }
                                            }
                                        }
                            %>
                            <div data-id="<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input type="hidden" id="hid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" name="hid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" value="<%=modJournalDet.GetSetid %>" />
                                        <input id="txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" name="txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" type="text" size="10" maxlength="10" value="<%=modJournalDet.GetSetaccid %>" class="input" <%=modJournalDet.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                        <div id="txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width: 350px;">
                                        <input id="txtaccdesc_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" name="txtaccdesc_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" type="text" size="50" maxlength="100" value="<%=modJournalDet.GetSetaccdesc %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" name="selaccledger_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" class="select enabled" <%=modJournalDet.GetSetstatus.Equals("CONFIRMED")?"disabled":"" %>>
                                            <option value="1" <%=modJournalDet.GetSetledgerno.Equals(1)?"selected":"" %>>Debit</option>
                                            <option value="2" <%=modJournalDet.GetSetledgerno.Equals(2)?"selected":"" %>>Kredit</option>
                                        </select>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" name="txtaccamount_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>" type="text" size="12" maxlength="12" value="<%=modJournalDet.GetSetledgerno.Equals(1)?modJournalDet.GetSetdebit:modJournalDet.GetSetcredit %>" class="input enabled" <%=modJournalDet.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                    </div>
                                    <div class="Cell" data-id="<%=modJournalDet.GetSetid %>">
                                        <button type="button" class="button_warning enabled" data-action="delete">-</button>
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                var selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> = "";
                                if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%>').val() == "1") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> = "TRAN";
                                } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%>').val() == "2") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> = "TRAN";
                                } else {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> = "";
                                }

                                $('#txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>').autocomplete({
                                    lookup: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> == "TRAN" ? fiscoatranArray : fiscoaArray),
                                    appendTo: '#txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=j+1%> == "TRAN" ? maxlengthdatatranautocomplete * 20 : maxlengthdataautocomplete * 20),
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_1_<%=j+1%> = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_1_<%=j+1%>, getFisCOADetail_succeedAjaxFn_1_<%=j+1%>, getFisCOADetail_failedAjaxFn_1_<%=j+1%>, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_1_<%=j+1%> = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_1_<%=j+1%>: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modJournalDet.GetSettrancode %>_<%=modJournalDet.GetSettranno %>_1_<%=j+1%>').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_1_<%=j+1%> = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_1_<%=j+1%>: " + textStatus);
                                }

                            </script>
                            <%
                                    }//end of FOR LOOP
                                                                       
                                    //set total line -1
                                    iTotalLine = lsFisJournalTran.Count;

                                    if (lsFisJournalTran.Count > 1 && sAction.Equals("NEWROW"))
                                    {
                                        enabledcheckbox = false;
                                        iTotalLine = lsFisJournalTran.Count + 1;

                            %>
                            <div data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input type="hidden" id="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" name="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" value="0" />
                                        <input id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" name="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width: 350px;">
                                        <input id="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" name="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" name="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" class="select enabled">
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" name="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>" type="text" size="12" maxlength="12" value="0.00" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <!--<button type="button" class="button_warning enabled">-</button>-->
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                var selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> = "";
                                if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>').val() == "1") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> = "TRAN";
                                } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>').val() == "2") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> = "TRAN";
                                } else {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> = "";
                                }

                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>').autocomplete({
                                    lookup: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> == "TRAN" ? fiscoatranArray : fiscoaArray),
                                    appendTo: '#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %> == "TRAN" ? maxlengthdatatranautocomplete * 20 : maxlengthdataautocomplete * 20),
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_1_<%=iTotalLine %> = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_1_<%=iTotalLine %>, getFisCOADetail_succeedAjaxFn_1_<%=iTotalLine %>, getFisCOADetail_failedAjaxFn_1_<%=iTotalLine %>, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_1_<%=iTotalLine %> = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_1_<%=iTotalLine %>: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=iTotalLine %>').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_1_<%=iTotalLine %> = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_1_<%=iTotalLine %>: " + textStatus);
                                }

                            </script>
                            <%

                                    }                                    
                                    else if (lsFisJournalTran.Count == 1)
                                    {
                                        enabledcheckbox = false;

                            %>
                            <div data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input type="hidden" id="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" value="0" />
                                        <input id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width: 350px;">
                                        <input id="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" class="select enabled">
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="12" maxlength="12" value="0.00" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <!--<button type="button" class="button_warning enabled">-</button>-->
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                var selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "";
                                if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val() == "1") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "TRAN";
                                } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val() == "2") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "TRAN";
                                } else {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "";
                                }

                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').autocomplete({
                                    lookup: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 == "TRAN" ? fiscoatranArray : fiscoaArray),
                                    appendTo: '#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 == "TRAN" ? maxlengthdatatranautocomplete * 20 : maxlengthdataautocomplete * 20),
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_1_2 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_1_2, getFisCOADetail_succeedAjaxFn_1_2, getFisCOADetail_failedAjaxFn_1_2, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_1_2 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_1_2: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_1_2 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_1_2: " + textStatus);
                                }

                            </script>
                            <%
                                        //set total line -2
                                        iTotalLine = 2;
                                    }
                                }
                                else
                                {
                                    enabledcheckbox = false;
                            %>
                            <div data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input type="hidden" id="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" name="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" value="0" />
                                        <input id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" name="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width: 350px;">
                                        <input id="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" name="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" name="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" class="select enabled">
                                            <option value="1" selected>Debit</option>
                                            <option value="2">Kredit</option>
                                        </select>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" name="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1" type="text" size="12" maxlength="12" value="0.00" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <!--<button type="button" class="button_warning enabled">-</button>-->
                                    </div>
                                </div>
                            </div>
                            <div data-id="<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input type="hidden" id="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" value="0" />
                                        <input id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width: 350px;">
                                        <input id="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" class="select enabled">
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" name="txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2" type="text" size="12" maxlength="12" value="0.00" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <!--<button type="button" class="button_warning enabled">-</button>-->
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                var selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 = "";
                                if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').val() == "1") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 = "TRAN";
                                } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').val() == "2") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 = "TRAN";
                                } else {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 = "";
                                }

                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').autocomplete({
                                    lookup: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 == "TRAN" ? fiscoatranArray : fiscoaArray),
                                    appendTo: '#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1 == "TRAN" ? maxlengthdatatranautocomplete * 20 : maxlengthdataautocomplete * 20),
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_1_1 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_1_1, getFisCOADetail_succeedAjaxFn_1_1, getFisCOADetail_failedAjaxFn_1_1, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_1_1 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_1_1: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_1_1 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_1_1: " + textStatus);
                                }

                                var selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "";
                                if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val() == "1") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "TRAN";
                                } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER" && $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val() == "2") {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "TRAN";
                                } else {
                                    selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 = "";
                                }

                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').autocomplete({
                                    lookup: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 == "TRAN" ? fiscoatranArray : fiscoaArray),
                                    appendTo: '#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: (selectcoa_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2 == "TRAN" ? maxlengthdatatranautocomplete * 20 : maxlengthdataautocomplete * 20),
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_1_2 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_1_2, getFisCOADetail_succeedAjaxFn_1_2, getFisCOADetail_failedAjaxFn_1_2, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_1_2 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_1_2: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_1_2 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_1_2: " + textStatus);
                                }

                                <%
                                if (modJournalTran.GetSetstatus.Equals("NEW") && sAction.Equals("ADD"))
                                {
                                %>
                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').prop('disabled', true);
                                $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').prop('disabled', true);
                                $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').prop('disabled', true);
                                $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_1').prop('disabled', true);
                                $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').prop('disabled', true);
                                $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').prop('disabled', true);
                                $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').prop('disabled', true);
                                $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_2').prop('disabled', true);
                                <%
                                }
                                %>
                            </script>
                            <%
                                    //set total line -3
                                    iTotalLine = 2;

                                }
                            %>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" id="hidTranAmount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" name="hidTranAmount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" value="" />
                            <input type="hidden" id="hidTotalDebit_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" name="hidTotalDebit_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" value="<%=dTotalDebit %>" />
                            <input type="hidden" id="hidTotalCredit_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" name="hidTotalCredit_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1" value="<%=dTotalCredit %>" />
                        </td>
                    </tr>
                    <script type="text/javascript">
                    </script>
                </tbody>
            </table>

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsFisJournalTran.Count %> record(s)</td>
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
                    <%
                        if (modJournalTran.GetSetstatus.Equals("NEW") && modJournalTran.GetSetid > 0)
                        {

                    %>

                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Simpan">
                    <%  
                        } 
                    %>
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
                                        <td width="80%" class="tblText2"><span id="dettranno" name="dettranno"></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kod Transaksi:</td>
                                        <td width="80%" class="tblText2"><span id="dettrancode" name="dettrancode"></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh Lejer:</td>
                                        <td width="80%" class="tblText2"><span id="detledgerdate" name="detledgerdate"></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Currency:</td>
                                        <td width="80%" class="tblText2"><span id="detcurrency" name="detcurrency"></td>
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

        function actionclick(action) {
            var proceed = true;

            $('#lsFindTranCode').prop('disabled', false);

            /*
            if (action == "SAVE") {
                document.getElementById("txtFindLedgerNo").value = "";
                document.getElementById("lsFindType").selectedIndex = 0;
                document.getElementById("lsFindCategory").selectedIndex = 0;
                document.getElementById("lsFindOption").selectedIndex = 0;
            }
            */
            if (action == "UPDATE") {
                document.getElementById("lsFindStatus").selectedIndex = "1";
            }
            if (action == "CANCEL") {
                document.getElementById("lsFindStatus").selectedIndex = "3";
            }

            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            if ("<%=modJournalTran.GetSettrancode%>" == "PAYMENT_VOUCHER") {
                acctype = "B";
            } else if ("<%=modJournalTran.GetSettrancode%>" == "RECEIPT_VOUCHER") {
                acctype = "H";
            } else {
                acctype = "";
            }

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr%>", "acctype", ""];
            PageMethod("getFisCOATranList", getFisCOAList_parameters, getFisCOAList_succeedAjaxFn, getFisCOAList_failedAjaxFn, false);

            var getFisCOATranList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr%>", "acctype", acctype];
            PageMethod("getFisCOATranList", getFisCOATranList_parameters, getFisCOATranList_succeedAjaxFn, getFisCOATranList_failedAjaxFn, false);

            <%
                AccountingModel oAlerMssg = oAccCon.getAlertMessage(sAlertMessage);
                if (oAlerMssg.GetSetalertstatus.Equals("SUCCESS"))
                {
            %>
                    alert("<%=oAlerMssg.GetSetalertmessage %>");
            <%
                }
                else if (oAlerMssg.GetSetalertstatus.Equals("ERROR"))
                {
            %>
                    alert("<%=oAlerMssg.GetSetalertmessage %>");
            <%
                }
                //to reset alertmessage
                sAlertMessage = "";
            %>

        });

        var getFisCOAList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisCOAList_succeedAjaxFn: " + textStatus);
            var getFisCOAList_result = JSON.parse(data.d);
            if (getFisCOAList_result.result == "Y") {
                var coatranid = '';
                $.each(getFisCOAList_result.fiscoalist, function (i, result) {
                    if (coatranid != result.GetSetaccid + '-' + result.GetSetaccdesc) {
                        var objData = {};
                        objData.value = result.GetSetaccid + '-' + result.GetSetaccdesc;
                        objData.data = result.GetSetaccid;
                        fiscoaArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        coatranid = result.GetSetaccid + '-' + result.GetSetaccdesc;
                    }
                });
            }
            else {
                console.log("getFisCOAList_result.result: " + getFisCOAList_result.result);
            }
        }

        var getFisCOAList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisCOAList_failedAjaxFn: " + textStatus);
        }

        var getFisCOATranList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisCOATranList_succeedAjaxFn: " + textStatus);
            var getFisCOATranList_result = JSON.parse(data.d);
            if (getFisCOATranList_result.result == "Y") {
                var coatranid = '';
                $.each(getFisCOATranList_result.fiscoalist, function (i, result) {
                    if (coatranid != result.GetSetaccid + '-' + result.GetSetaccdesc) {
                        var objData = {};
                        objData.value = result.GetSetaccid + '-' + result.GetSetaccdesc;
                        objData.data = result.GetSetaccid;
                        fiscoatranArray.push(objData);
                        if (objData.value.length > maxlengthdatatranautocomplete) {
                            maxlengthdatatranautocomplete = objData.value.length;
                        }
                        coatranid = result.GetSetaccid + '-' + result.GetSetaccdesc;
                    }
                });
            }
            else {
                console.log("getFisCOATranList_result.result: " + getFisCOAList_result.result);
            }
        }

        var getFisCOATranList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisCOATranList_failedAjaxFn: " + textStatus);
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

            //var trid = $(target).closest("[data-id]");
            var trid = $(target).closest("tr.tblText1");
            //get data-accid value for the TR
            var id = (trid.data("id"));

            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'select') {
                    //alert('delete:' + id);
                    var amount1 = <%=modJournalTran.GetSettranamount %>;
                    //alert(amount1);
                    var amount2 = 0;
                    //alert(amount2);
                    var amount3 = 0;
                    //alert(amount3);

                    <%
                    if (iTotalLine > 0)
                    {
                        for (int i = 0; i < iTotalLine; i++)
                        {
                    %>
                        var ledgerno_<%=i+1 %> = $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                        if (ledgerno_<%=i+1 %> == "1") {
                            amount2 = amount2 + parseFloat($('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val().length > 0 ? $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val() : "0");
                        } else {
                            amount3 = amount3 + parseFloat($('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val().length > 0 ? $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val() : "0");
                        }                            
                    <%
                        }
                    }
                    %>

                    if ((amount1 == amount2) && (amount1 == amount3)) {
                        /*
                        if ($(target).prop('checked')) {
                            $(target).prop('checked', false);
                        } else {
                            $(target).prop('checked', true);
                        }
                        */
                    } else {
                        $(target).prop('checked', false);
                        alert('Jumlah Debit & Kredit tidak bertepatan dengan Jumlah Transaksi');
                    }

                }
                else if (action == 'save') {
                }
                else if (action == 'add') {
                }
                else if (action == 'delete') {
                    var postdatadelete = [];
                    var trid = $(target).closest("div.Cell");
                    var id = (trid.data("id"));
                    if (id > 0) {
                        postdatadelete.push('<%=modJournalTran.GetSettranno %>|<%=modJournalTran.GetSettrancode %>|' + id);

                        var deletePostingData_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "postdatadelete", postdatadelete];
                        PageMethod("deletePostingData", deletePostingData_parameters, deletePostingData_succeedAjaxFn, deletePostingData_failedAjaxFn, true);
                    }
                }
            }
        });

        var deletePostingData_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deletePostingData_succeedAjaxFn: " + textStatus);
            var deletePostingData_result = JSON.parse(data.d);
            if (deletePostingData_result.result == "Y") {
                alert(deletePostingData_result.message);
                actionclick('SEARCH');

            }
            else {
                alert(deletePostingData_result.message);
            }
        }

        var deletePostingData_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deletePostingData_failedAjaxFn: " + textStatus);
        }

        $('#btnAdd').on('click', function (e) {
            var postdataupdate = [];
            var tranno = "";
            var trancode = "";
            var trandate = "";
            var refno = "";
            var status = "";

        <%
        if (iTotalLine > 0)
        {
            for (int i = 0; i < iTotalLine; i++)
            {
        %>
                tranno = "<%=modJournalTran.GetSettranno%>";
                trancode = "<%=modJournalTran.GetSettrancode %>";
                trandate = "<%=modJournalTran.GetSettrandate %>";
                refno = "<%=modJournalTran.GetSetrefno%>";
                if ($('#chk_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1').prop('checked')) {
                    status = "CONFIRMED";
                } else {
                    status = "NEW";
                }

                var id_<%=i+1 %> = "";
                var accid_<%=i+1 %> = "";
                var accdesc_<%=i+1 %> = "";
                var currency_<%=i+1 %> = "";
                var exrate_<%=i+1 %> = "";
                var ledgerno_<%=i+1 %> = "";
                var debit_<%=i+1 %> = "";
                var credit_<%=i+1 %> = "";

                id_<%=i+1 %> = $('#hid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                accid_<%=i+1 %> = $('#txtaccid_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                accdesc_<%=i+1 %> = $('#txtaccdesc_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                currency_<%=i+1 %> = "<%=modJournalTran.GetSetcurrency%>";
                exrate_<%=i+1 %> = "<%=modJournalTran.GetSetexrate%>";
                ledgerno_<%=i+1 %> = $('#selaccledger_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                if (ledgerno_<%=i+1 %> == "1") {
                    debit_<%=i+1 %> = $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                    credit_<%=i+1 %> = "0.00";
                } else {
                    debit_<%=i+1 %> = "0.00";
                    credit_<%=i+1 %> = $('#txtaccamount_<%=modJournalTran.GetSettrancode %>_<%=modJournalTran.GetSettranno %>_1_<%=i+1 %>').val();
                }

                if (accid_<%=i+1 %>.length > 0 && "<%=modJournalTran.GetSetstatus%>" == "NEW") {
                    postdataupdate.push(tranno + '|' + trancode + '|' + trandate + '|' + refno + '|' + id_<%=i+1 %> + '|' + accid_<%=i+1 %> + '|' + accdesc_<%=i+1 %> + '|' + currency_<%=i+1 %> + '|' + exrate_<%=i+1 %> + '|' + ledgerno_<%=i+1 %> + '|' + debit_<%=i+1 %> + '|' + credit_<%=i+1 %> + '|' + status);
                }
                                
        <%
            }
        }
        %>
            if (postdataupdate.length > 0) {
                var updatePostingData_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "postdataupdate", postdataupdate];
                PageMethod("updatePostingData", updatePostingData_parameters, updatePostingData_succeedAjaxFn, updatePostingData_failedAjaxFn, true);
            }
        });

        var updatePostingData_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updatePostingData_succeedAjaxFn: " + textStatus);
            var updatePostingData_result = JSON.parse(data.d);
            if (updatePostingData_result.result == "Y") {
                alert(updatePostingData_result.message);
                actionclick('SEARCH');

            }
            else {
                alert(updatePostingData_result.message);
            }
        }

        var updatePostingData_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updatePostingData_failedAjaxFn: " + textStatus);
        }

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
                                      <td class="tblText2">'+ result.GetSetaccid + '</td> \
                                      <td class="tblText2">'+ result.GetSetaccdesc + '</td> \
                                      <td class="tblText2">'+ result.GetSetacccode + '</td> \
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

