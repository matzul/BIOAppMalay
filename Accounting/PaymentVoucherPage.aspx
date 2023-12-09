<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="PaymentVoucherPage.aspx.cs" Inherits="Accounting_PaymentVoucherPage" %>
<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

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
        var fisFYRArray = [];
        var fiscoaArray = [];
        var maxlengthdataautocomplete = 20;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:topmenu id="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">
        <form id="form1" runat="server">

        <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
            <tr>
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Baucar Bayaran / Payment Voucher (PV)</a></td>
            </tr>
        </table>
		<table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
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
            <!--
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
                <td width="20%" class="tblTextCommon">Sub-Koding:</td>
                <td width="80%" class="tblText2">
                    <select class="select" id="lsFindCategory" name="lsFindCategory">
                        <option value="" selected>-Semua-</option>
                        <option value="BANK" <%=sCurrCategory.Equals("BANK") ? "selected" : "" %>>Bank</option>
                        <option value="CUSTOMER" <%=sCurrCategory.Equals("CUSTOMER") ? "selected" : "" %>>Pelanggan</option>
                        <option value="SUPPLIER" <%=sCurrCategory.Equals("SUPPLIER") ? "selected" : "" %>>Pembekal</option>
                        <option value="INVENTORY" <%=sCurrCategory.Equals("INVENTORY") ? "selected" : "" %>>Inventori</option>
                        <option value="INVESTMENT" <%=sCurrCategory.Equals("INVESTMENT") ? "selected" : "" %>>Pelaburan</option>
                        <option value="ASSET" <%=sCurrCategory.Equals("ASSET") ? "selected" : "" %>>Hartanah/Loji/Aset</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td width="20%" class="tblTextCommon">No. Sub-Koding:</td>
                <td width="80%" class="tblText2">
                    <input id="txtFindAccNumber" name="txtFindAccNumber" type="text" size="20" maxlength="20" value="<%=sCurrAccNumber %>" class="input">
                </td>
            </tr>
            -->
        </table>
	    <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center">
			<tr>
				<td height="30" width="20%"></td>
				<td width="80%" align="left">
					<input class="button1" name="btnSearch" type="button" value="Carian" onclick="actionclick('SEARCH');">
					<input class="button1" name="btnReset" type="button" value="Reset" onclick="actionclick('RESET');">
                </td>
			</tr>
	    </table>
        <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
            <tr>
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Senarai Transaksi Baucar Bayaran / List of Payment Voucher Transaction</a></td>
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
                    </tr>
                    <%
                        if (lsFisJournalTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisJournalTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisJournalTran[i];
                                bool enabledcheckbox = true;

                                String openlink = "";
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
                    </tr>
                    <tr data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>" class="tblText1">
                        <td data-id="" colspan="10" class="tblText2">
                            <button type="button" class="button_warning enabled" onclick="fOpenWindow('JournalEntryPage.aspx?fyr=<%=sCurrFyr %>&action=OPEN&tranno=<%=modAcc.GetSettranno %>&trancode=<%=modAcc.GetSettrancode %>');"><%=modAcc.GetSetstatus.Equals("CONFIRMED")?"Lihat":"Kemaskini" %></button>
                            <div class="content"></div>
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
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="10" maxlength="10" value="<%=modLedgerTran.GetSetaccid %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="50" maxlength="100" value="<%=modLedgerTran.GetSetaccdesc %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="4" maxlength="4" value="<%=modLedgerTran.GetSetcurrency %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="8" maxlength="8" value="<%=modLedgerTran.GetSetexrate %>" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" class="select enabled" disabled>
                                            <option value="1" <%=modLedgerTran.GetSetledgerno.Equals(1)?"selected":"" %>>Debit</option>
                                            <option value="2" <%=modLedgerTran.GetSetledgerno.Equals(2)?"selected":"" %>>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_<%=j+1%>" type="text" size="15" maxlength="15" value="<%=modLedgerTran.GetSetledgerno.Equals(1)?modLedgerTran.GetSetdebit:modLedgerTran.GetSetcredit %>" class="input enabled" readonly />
                                    </div>
                                </div>
                            </div>

                            <%
                                    }
                                    if (lsFisLedgerTran.Count == 1)
                                    {
                                        enabledcheckbox = false;
                            %>
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="10" maxlength="10" value="" class="input enabled" readonly >
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="4" maxlength="4" value="MYR" class="input enabled" readonly  />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="8" maxlength="8" value="1.0" class="input enabled" readonly  />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="select enabled" disabled>
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="15" maxlength="15" value="0.00" class="input enabled" readonly  />
                                    </div>
                                </div>
                            </div>

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
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="10" maxlength="10" value="" class="input enabled" readonly >
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="50" maxlength="100" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="4" maxlength="4" value="MYR" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="8" maxlength="8" value="1.0" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" class="select enabled" disabled>
                                            <option value="1" selected>Debit</option>
                                            <option value="2">Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_1" type="text" size="15" maxlength="15" value="0.00" class="input enabled" readonly />
                                    </div>
                                </div>
                            </div>
                            <div data-id="<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="Table">
                                <div class="Row">
                                    <div class="Cell">
                                        <input id="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccid_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="10" maxlength="10" value="" class="input enabled" readonly>
                                    </div>
                                    <div class="Cell" style="width:350px;">
                                        <input id="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccdesc_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="50" maxlength="100" class="input enabled" readonly/>
                                    </div>
                                    <div class="Cell">
                                        <input id="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtcurrency_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="4" maxlength="4" value="MYR" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <input id="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtexrate_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="8" maxlength="8" value="1.0" class="input enabled" readonly />
                                    </div>
                                    <div class="Cell">
                                        <select id="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="selaccledger_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" class="select enabled" disabled>
                                            <option value="1">Debit</option>
                                            <option value="2" selected>Kredit</option>
                                        </select>                                    
                                    </div>
                                    <div class="Cell">
                                        <input id="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" name="txtaccamount_<%=modAcc.GetSettrancode %>_<%=modAcc.GetSettranno %>_<%=i+1 %>_2" type="text" size="15" maxlength="15" value="0.00" class="input enabled" readonly />
                                    </div>
                                </div>
                            </div>

                            <%
                                }
                            %>

                        </td>
                    </tr>
                            <script type="text/javascript">
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
                                <td width="50%" height="15" align="left"><%=lsFisJournalTran.Count %> record(s)</td>
                                <td width="50%" align="right">page
                                    <asp:DropDownList CssClass="select" ID="lsPageList" runat="server" OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    of <%=sTotalPage %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

	        <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton">
			    <tr>
				    <td align="center">
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Daftar Baucar" onclick="fOpenWindow('JournalEntryPage.aspx?fyr=<%=sCurrFyr %>&action=ADD&tranno=&trancode=<%=sTranCode%>');">
                        <input class="button1" name="btnClose" type="button" value="Tutup" onclick="window.close();">					    
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
                document.getElementById("txtFindLedgerNo").value = "";
                //document.getElementById("lsFindFyr").value = "<%=sCurrFyr%>";
                //document.getElementById("txtFindFromDate").value = "<%=sCurrDateFrom %>";
                //document.getElementById("txtFindToDate").value = "<%=sCurrDateTo %>";
                //document.getElementById("lsFindType").selectedIndex = 0;
                //document.getElementById("lsFindCategory").selectedIndex = 0;
                //document.getElementById("txtFindAccNumber").value = "";
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

    </script>
</asp:Content>

