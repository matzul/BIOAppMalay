<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="PostingPage.aspx.cs" Inherits="Accounting_PostingPage" %>

<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
    .Table
    {
        display: table;
    }
    .Title
    {
        display: table-caption;
        text-align: center;
        font-weight: bold;
        font-size: larger;
    }
    .Heading
    {
        display: table-row;
        font-weight: bold;
        text-align: center;
    }
    .Row
    {
        display: table-row;
    }
    .Cell
    {
        display: table-cell;
        border: dashed;
        border-width: thin;
        padding-left: 5px;
        padding-right: 5px;
        width: 130px;
    }
</style>
<script type="text/javascript">
    var fisFYRArray = [];
    var fiscoaArray = [];
    var maxlengthdataautocomplete = 40;
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:TopMenu ID="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Maklumat Posting Data / Posting Details</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input enabled" readonly>
                        <div id="txtFindFyr-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tarikh Transaksi</td>
                    <td colspan="20" class="tblText2">Dari:
                        <input class="input" name="txtFindFromDate" id="txtFindFromDate" type="text" value="<%=sCurrDateFrom %>" size="10" maxlength="12">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindFromDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindFromDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y",   	// format of the input field
                                button: "imgFindFromDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                singleClick: true
                            });
                        </script>
                        Hingga:
                        <input class="input" name="txtFindToDate" id="txtFindToDate" type="text" value="<%=sCurrDateTo %>" size="10" maxlength="12">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindToDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindToDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y",   	// format of the input field
                                button: "imgFindToDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                singleClick: true
                            });
                        </script>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Pilihan:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindOption" name="lsFindOption">
                            <option value="" selected>-Select-</option>
                            <option value="SALES_INVOICE" <%=sCurrType.Equals("SALES_INVOICE") ? "selected" : "" %>>INVOIS JUALAN</option>
                            <option value="RECEIPT_VOUCHER" <%=sCurrType.Equals("RECEIPT_VOUCHER") ? "selected" : "" %>>VOUCHER TERIMAAN</option>
                            <option value="INVOICE_CASH" <%=sCurrType.Equals("INVOICE_CASH") ? "selected" : "" %>>TERIMAAN TUNAI</option>
                            <option value="INVOICE_CHEQUE" <%=sCurrType.Equals("INVOICE_CHEQUE") ? "selected" : "" %>>TERIMAAN CEK</option>
                            <option value="INVOICE_BANKING" <%=sCurrType.Equals("INVOICE_BANKING") ? "selected" : "" %>>TERIMAAN BANKING</option>
                            <option value="INVOICE_CONTRA_PAYMENT" <%=sCurrType.Equals("INVOICE_CONTRA_PAYMENT") ? "selected" : "" %>>TERIMAAN KONTRA</option>
                            <option value="PURCHASE_INVOICE" <%=sCurrType.Equals("PURCHASE_INVOICE") ? "selected" : "" %>>BIL BELIAN</option>
                            <option value="PAYMENT_VOUCHER" <%=sCurrType.Equals("PAYMENT_VOUCHER") ? "selected" : "" %>>VOUCHER BAYARAN</option>
                            <option value="VOUCHER_CASH" <%=sCurrType.Equals("INVOICE_CASH") ? "selected" : "" %>>BELANJA TUNAI</option>
                            <option value="VOUCHER_CHEQUE" <%=sCurrType.Equals("INVOICE_CHEQUE") ? "selected" : "" %>>BELANJA CEK</option>
                            <option value="VOUCHER_BANKING" <%=sCurrType.Equals("INVOICE_BANKING") ? "selected" : "" %>>BELANJA BANKING</option>
                            <option value="VOUCHER_CONTRA_PAYMENT" <%=sCurrType.Equals("INVOICE_CONTRA_PAYMENT") ? "selected" : "" %>>BELANJA KONTRA</option>
                            <option value="EXPENSES_CASH" <%=sCurrType.Equals("INVOICE_CASH") ? "selected" : "" %>>BELANJA TUNAI</option>
                            <option value="EXPENSES_CHEQUE" <%=sCurrType.Equals("INVOICE_CHEQUE") ? "selected" : "" %>>BELANJA CEK</option>
                            <option value="EXPENSES_BANKING" <%=sCurrType.Equals("INVOICE_BANKING") ? "selected" : "" %>>BELANJA BANKING</option>
                            <option value="EXPENSES_CONTRA_PAYMENT" <%=sCurrType.Equals("INVOICE_CONTRA_PAYMENT") ? "selected" : "" %>>BELANJA KONTRA</option>
                            <option value="TRANSFER_INVOICE" <%=sCurrType.Equals("TRANSFER_INVOICE") ? "selected" : "" %>>BIL/INVOIS PINDAHAN</option>
                            <option value="JOURNAL_VOUCHER" <%=sCurrType.Equals("JOURNAL_VOUCHER") ? "selected" : "" %>>VOUCHER PERLARASAN</option>
                            <option value="ADJUSTMENT_ORDER" <%=sCurrType.Equals("ADJUSTMENT_ORDER") ? "selected" : "" %>>PERLARASAN INVENTORI</option>
                            <option value="RECEIPT" <%=sCurrType.Equals("RECEIPT") ? "selected" : "" %>>PENERIMAAN INVENTORI</option>
                            <option value="SHIPMENT" <%=sCurrType.Equals("SHIPMENT") ? "selected" : "" %>>PENGELUARAN INVENTORI</option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Posting Data / List of Posting Details</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="5%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No Transaksi</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod Transaksi</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">No Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Transaksi</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>
                        <td width="16%" valign="middle" align="left" class="tblTitle3Mod">Rakan Niaga</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsPostData.Count > 0)
                        {
                            for (int i = 0; i < lsPostData.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsPostData[i];
                                bool enabledcheckbox = true;
                                
                                String openlink = "";
                                if(modAcc.GetSetrefno.StartsWith("INV"))
                                {
                                    openlink = "openeditinvoice('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                                else if(modAcc.GetSetrefno.StartsWith("PRC"))
                                {
                                    openlink = "openeditpaymentreceipt('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                                else if(modAcc.GetSetrefno.StartsWith("PV"))
                                {
                                    openlink = "openeditexpenses('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                                else if(modAcc.GetSetrefno.StartsWith("PPD"))
                                {
                                    openlink = "openeditpaymentpaid('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                                else if(modAcc.GetSetrefno.StartsWith("GR"))
                                {
                                    openlink = "openeditreceipt('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                                else if(modAcc.GetSetrefno.StartsWith("DO"))
                                {
                                    openlink = "openeditshipment('"+modAcc.GetSetcomp+"', '"+modAcc.GetSetrefno+"');";
                                }
                    %>
                    <tr data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>" class="tblText1">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettranno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrancode %></td>
                        <td valign="top" align="left"><a href="#" onclick="<%=openlink %>"><%=modAcc.GetSetrefno %></a></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrandate %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrandesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettranamount %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpid %><br /><%=modAcc.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <input id="chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>" name="chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>" data-action="select" type="checkbox" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"checked class='disabled' disabled":"class='enabled'"%> />
                        </td>
                    </tr>
                    <tr data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>" class="tblText1">
                        <td data-id="<%=modAcc.GetSetrefno %>" colspan="10" class="tblText2">
                            <!--<button type="button" class="button_warning enabled" data-action="add">+</button>-->
                            <!--<button type="button" class="button_warning enabled" data-action="save">kemaskini</button>-->
                            <input type="hidden" id="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" value="0" />
                            <input type="hidden" id="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" value="0" />
                            <input type="hidden" id="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_3" name="hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_3" value="<%=modAcc.GetSettranamount %>" />
                            <div class="Table">
                                <div class="Heading">
                                    <div class="Cell">
                                        No. Lejer
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        Keterangan
                                    </div>
                                    <div class="Cell">
                                        Currency
                                    </div>
                                    <div class="Cell">
                                        Rate
                                    </div>
                                    <div class="Cell">
                                        
                                    </div>
                                    <div class="Cell">
                                        Jumlah
                                    </div>
                                </div>
                            </div>
                            <%
                                ArrayList lsFisLedgerTran = oAccCon.getFisLedgerTranList(modAcc.GetSetcomp, modAcc.GetSetfyr, 0, "", 0, "", modAcc.GetSettranno, modAcc.GetSettrancode, "");
                                if (lsFisLedgerTran.Count > 0)
                                {
                                    for (int j = 0; j < lsFisLedgerTran.Count; j++)
                                    {
                                        AccountingModel modLedgerTran = (AccountingModel)lsFisLedgerTran[j];

                                        if (enabledcheckbox)
                                        {
                                            if (modLedgerTran.GetSetledgerno.Equals(1))
                                            {
                                                if (modAcc.GetSettranamount == modLedgerTran.GetSetdebit)
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
                                                if (modAcc.GetSettranamount == modLedgerTran.GetSetcredit)
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
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="10" maxlength="10" value="<%=modLedgerTran.GetSetaccid %>" class="input" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                        <div id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="50" maxlength="100" value="<%=modLedgerTran.GetSetaccdesc %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="4" maxlength="4" value="<%=modLedgerTran.GetSetcurrency %>" class="input enabled" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="8" maxlength="8" value="<%=modLedgerTran.GetSetexrate %>" class="input enabled" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" class="select enabled" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"disabled":"" %>>
                                            <option value="1" <%=modLedgerTran.GetSetledgerno.Equals(1)?"selected":"" %>>Debit</option>
                                            <option value="2" <%=modLedgerTran.GetSetledgerno.Equals(2)?"selected":"" %>>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="15" maxlength="15" value="<%=modLedgerTran.GetSetledgerno.Equals(1)?modLedgerTran.GetSetdebit:modLedgerTran.GetSetcredit %>" class="input enabled" <%=modAcc.GetSetstatus.Equals("CONFIRMED")?"readonly":"" %> />
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                $('#hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>').val('<%=modLedgerTran.GetSetid %>');
                                $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>').autocomplete({
                                    lookup: fiscoaArray,
                                    appendTo: '#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: maxlengthdataautocomplete * 12,
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_<%=i+1 %>_<%=j+1%> = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_<%=i+1 %>_<%=j+1%>, getFisCOADetail_succeedAjaxFn_<%=i+1 %>_<%=j+1%>, getFisCOADetail_failedAjaxFn_<%=i+1 %>_<%=j+1%>, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_<%=i+1 %>_<%=j+1%> = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_<%=i+1 %>_<%=j+1%>: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_<%=i+1 %>_<%=j+1%> = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_<%=i+1 %>_<%=j+1%>: " + textStatus);
                                }

                            </script>
                            <%
                                    }
                                    if (lsFisLedgerTran.Count == 1)
                                    {
                                        enabledcheckbox = false;
                            %>
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="4" maxlength="4" value="MYR" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="8" maxlength="8" value="1.0" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="select enabled">
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="15" maxlength="15" value="0.00" class="input enabled" />
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').autocomplete({
                                    lookup: fiscoaArray,
                                    appendTo: '#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: maxlengthdataautocomplete * 12,
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_<%=i+1 %>_2 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_<%=i+1 %>_2, getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2, getFisCOADetail_failedAjaxFn_<%=i+1 %>_2, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_<%=i+1 %>_2 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_<%=i+1 %>_2: " + textStatus);
                                }

                            </script>
                            <%
                                    }
                                }
                                else
                                {
                                    enabledcheckbox = false;
                            %>
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="4" maxlength="4" value="MYR" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="8" maxlength="8" value="1.0" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" class="select enabled">
                                            <option value="1" selected>Debit</option>
                                            <option value="2">Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="15" maxlength="15" value="0.00" class="input enabled" />
                                    </div>
                                </div>
                            </div>
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="10" maxlength="10" value="" class="input">
                                        <div id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="50" maxlength="100" class="input enabled" readonly/>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="4" maxlength="4" value="MYR" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="8" maxlength="8" value="1.0" class="input enabled" />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="select enabled">
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="15" maxlength="15" value="0.00" class="input enabled" />
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').autocomplete({
                                    lookup: fiscoaArray,
                                    appendTo: '#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: maxlengthdataautocomplete * 12,
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_<%=i+1 %>_1 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_<%=i+1 %>_1, getFisCOADetail_succeedAjaxFn_<%=i+1 %>_1, getFisCOADetail_failedAjaxFn_<%=i+1 %>_1, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_<%=i+1 %>_1 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_<%=i+1 %>_1: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_<%=i+1 %>_1 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_<%=i+1 %>_1: " + textStatus);
                                }
                                $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').autocomplete({
                                    lookup: fiscoaArray,
                                    appendTo: '#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2-container',
                                    minLength: 0,
                                    minChars: 0,
                                    width: maxlengthdataautocomplete * 12,
                                    onSelect: function (suggestion) {
                                        //console.log("suggestion: " + JSON.stringify(suggestion));
                                        $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val(suggestion.data);
                                        //get details accid details
                                        var getFisCOADetail_parameters_<%=i+1 %>_2 = ["currcomp", "<%=sCurrComp%>", "accid", suggestion.data];
                                        PageMethod("getFisCOADetail", getFisCOADetail_parameters_<%=i+1 %>_2, getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2, getFisCOADetail_failedAjaxFn_<%=i+1 %>_2, false);
                                    }
                                });

                                var getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2 = function (data, textStatus, jqXHR) {
                                    console.log("getFisCOADetail_succeedAjaxFn_<%=i+1 %>_2: " + textStatus);
                                    var getFisCOADetail_result = JSON.parse(data.d);
                                    if (getFisCOADetail_result.result == "Y") {
                                        $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                                    }
                                    else {
                                        alert(getFisCOADetail_result.message);
                                    }
                                }

                                var getFisCOADetail_failedAjaxFn_<%=i+1 %>_2 = function (jqXHR, textStatus, errorThrown) {
                                    console.log("getFisCOADetail_failedAjaxFn_<%=i+1 %>_2: " + textStatus);
                                }

                            </script>
                            <%
                                }
                            %>

                        </td>
                    </tr>
                            <script type="text/javascript">
                                /*
                                if ("True"=="<%=enabledcheckbox%>") {
                                    $('#chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>').removeClass('disabled').addClass('enabled');
                                    //$('#chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>').prop('disabled', false);
                                } else {
                                    $('#chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>').removeClass('enabled').addClass('disabled');
                                    //$('#chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>').prop('disabled', true);
                                }
                                */
                            </script>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="11" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsPostData.Count %> record(s)</td>
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
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Simpan">
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

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindFyr").value = "<%=sCurrFyr%>";
                document.getElementById("txtFindFromDate").value = "<%=sCurrDateFrom %>";
                document.getElementById("txtFindToDate").value = "<%=sCurrDateTo %>";
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

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>"];
            PageMethod("getFisCOAList", getFisCOAList_parameters, getFisCOAList_succeedAjaxFn, getFisCOAList_failedAjaxFn, false);

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

        $('#txtFindFyr').autocomplete({
            lookup: fisFYRArray,
            appendTo: '#txtFindFyr-container',
            minLength: 0,
            minChars: 0,
            width: 100,
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

            //var trid = $(target).closest("[data-id]");
            var trid = $(target).closest("tr.tblText1");
            //get data-accid value for the TR
            var id = (trid.data("id"));

            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'select') {
                    //alert('delete:' + id);
                    var amount1 = $('#hid_' + id + '_3').val();
                    //alert(amount1);
                    var amount2 = $('#txtaccamount_' + id + '_1').val();
                    //alert(amount2);
                    var amount3 = $('#txtaccamount_' + id + '_2').val();
                    //alert(amount3);
                    if (amount1.length > 0 && amount2.length > 0 && amount3.length > 0) {
                        if ((parseFloat(amount1) == parseFloat(amount2)) && (parseFloat(amount1) == parseFloat(amount3))) {
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
                    } else {
                        //if ($(target).prop('checked')) {
                        $(target).prop('checked', flase);
                        alert('Jumlah Debit & Kredit tidak bertepatan dengan Jumlah Transaksi');
                    }
                }
                else if (action == 'save') {
                }
                else if (action == 'add') {
                    /*
                    var tdtran = $(target).closest("td.tblText2");
                    var trancode = (tdtran.data("id"));
                    alert(trancode);
                    if (trancode.length > 0) {
                        tdtran.append(
                            '<table width="99%" border="1" cellpadding="2" cellspacing="1" class="tblTextMod mytable2" data-id="trancode|tranno|accid">' +
                            '<tbody>' +
                            '<tr>' +
                            '<td width="33%" align="left">test 1a</td>' +
                            '<td width="33%" align="right">test 1b</td>' +
                            '<td width="33%" align="right"><button type="button" class="btndel" data-action="del">-</button></td>' +
                            '</tr>' +
                            '</tbody>' +
                            '</table>'
                        );
                    } else {
                        //-------------------------Begin--------------------------------

                        //--------------------------End--------------------------------
                        tdtran.data("id","INVOICE_RECEIPT|INV201920");
                        tdtran.append(
                            '<div>No. Transaksi: INV201920</div>'
                        );
                        tdtran.append(
                            //'<table class="mytable2" data-id="test1"><tr><td>Test 1</td><td>Test 2</td><td><button type="button" class="btndel" data-action="del">-</button></td></tr ></table >'
                            '<table width="99%" border="1" cellpadding="2" cellspacing="1" class="tblTextMod mytable2" data-id="trancode|tranno|accid">'+
                            '<tbody height="20px">' +
                            '<tr>' +
                            '<td width="33%" align="left">test 1a</td>'+
                            '<td width="33%" align="right">test 1b</td>' +
                            '<td width="33%" align="right"><button type="button" class="btndel" data-action="del">-</button></td>' +
                            '</tr>' +
                            '</tbody>' +
                            '</table>'
                        );
                    }

                    //alert('edit:' + id);
                    /*
                    $("td.tblText2").append(
                        '<div><a href="#shipmentitem" data-toggle="tab" onclick="getShipmentItem();">test</a></div>'
                    );
                    */
                }
                else if (action == 'del') {
                    var trid = $(target).closest("table.mytable2");
                    var id = (trid.data("id"));
                    alert(id);
                    $(trid).remove();
                }
            }
        });

        $('#btnAdd').on('click', function (e) {
            var postdataupdate = [];
            var tranno = "";
            var trancode = "";
            var trandate = "";
            var refno = "";
            var status = "";

            var id1 = "";
            var accid1 = "";
            var accdesc1 = "";
            var currency1 = "";
            var exrate1 = "";
            var ledgerno1 = "";
            var debit1 = "";
            var credit1 = "";

            var id2 = "";
            var accid2 = "";
            var accdesc2 = "";
            var currency2 = "";
            var exrate2 = "";
            var ledgerno2 = "";
            var debit2 = "";
            var credit2 = "";

        <%
        if (lsPostData.Count > 0)
        {
            for (int i = 0; i < lsPostData.Count; i++)
            {
                AccountingModel modAcc = (AccountingModel)lsPostData[i];
        %>
                tranno = "<%=modAcc.GetSettranno%>";
                trancode = "<%=modAcc.GetSettrancode %>"; 
                trandate = "<%=modAcc.GetSettrandate %>";
                refno = "<%=modAcc.GetSetrefno%>";
                if ($('#chk_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>').prop('checked')) {
                    status = "CONFIRMED";
                } else {
                    status = "NEW";
                }

                id1 = $('#hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                //id1 = document.getElementById("hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1").value;
                accid1 = $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                accdesc1 = $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                currency1 = $('#txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                exrate1 = $('#txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                ledgerno1 = $('#selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                if (ledgerno1 == "1") {
                    debit1 = $('#txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                    credit1 = "0.00";
                } else {
                    debit1 = "0.00";
                    credit1 = $('#txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1').val();
                }

            if (accid1.length > 0 && "<%=modAcc.GetSetstatus%>" == "NEW") {
                postdataupdate.push(tranno + '|' + trancode + '|' + trandate + '|' + refno + '|' + id1 + '|' + accid1 + '|' + accdesc1 + '|' + currency1 + '|' + exrate1 + '|' + ledgerno1 + '|' + debit1 + '|' + credit1 + '|' + status);
            }
                id2 = $('#hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                //id2 = document.getElementById("hid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2").value;
                accid2 = $('#txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                accdesc2 = $('#txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                currency2 = $('#txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                exrate2 = $('#txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                ledgerno2 = $('#selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                if (ledgerno2 == "1") {
                    debit2 = $('#txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                    credit2 = "0.00";
                } else {
                    debit2 = "0.00";
                    credit2 = $('#txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2').val();
                }

            if (accid2.length > 0) {
                postdataupdate.push(tranno + '|' + trancode + '|' + trandate + '|' + refno + '|' + id2 + '|' + accid2 + '|' + accdesc2 + '|' + currency2 + '|' + exrate2 + '|' + ledgerno2 + '|' + debit2 + '|' + credit2 + '|' + status);
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

        function openeditinvoice(comp, invoiceno) {
            var popupWindow = window.open("../InvoiceDetails.aspx?action=OPEN&comp=" + comp + "&invoiceno=" + invoiceno, "open_order", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function openeditpaymentreceipt(comp, payrcptno) {
            var popupWindow = window.open("../PaymentReceiptDetails.aspx?action=OPEN&comp=" + comp + "&payrcptno=" + payrcptno, "open_paymentreceipt", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function openeditexpenses(comp, expensesno) {
            var popupWindow = window.open("../ExpensesDetails.aspx?action=OPEN&comp=" + comp + "&expensesno=" + expensesno, "open_expenses", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function openeditpaymentpaid(comp, paypaidno) {
            var popupWindow = window.open("../PaymentPaidDetails.aspx?action=OPEN&comp=" + comp + "&paypaidno=" + paypaidno, "open_paymentpaid", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function openeditreceipt(comp, receiptno) {
            var popupWindow = window.open("../ReceiptDetails.aspx?action=OPEN&comp=" + comp + "&receiptno=" + receiptno, "open_receipt", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function openeditshipment(comp, shipmentno) {
            var popupWindow = window.open("../ShipmentDetails.aspx?action=OPEN&comp=" + comp + "&shipmentno=" + shipmentno, "open_shipment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

    </script>
</asp:Content>

