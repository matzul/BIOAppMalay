﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="BalanceSheetDetailsPage.aspx.cs" Inherits="Accounting_BalanceSheetDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyata Kedudukan Kewangan / Balance Sheet Ledger</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun Kewangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemTypeDesc" name="txtFindItemTypeDesc" type="text" size="60" maxlength="60" value="<%=sItemTypeDesc %>" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tarikh Transaksi</td>
                    <td colspan="20" class="tblText2">Dari:
                        <input class="input" name="txtFindFromDate" id="txtFindFromDate" type="text" value="<%=sStartTranDate %>" size="14" maxlength="16">
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
                        <input class="input" name="txtFindToDate" id="txtFindToDate" type="text" value="<%=sEndTranDate %>" size="14" maxlength="16">
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
                    <td width="20%" class="tblTextCommon">Aset:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAsset" name="txtFindAsset" type="text" size="10" maxlength="20" value="" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon"></td>
                    <td width="80%" class="tblText2">
                        =
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Liabiliti:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindLiability" name="txtFindLiability" type="text" size="10" maxlength="20" value="" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Ekuiti:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindEquity" name="txtFindEquity" type="text" size="10" maxlength="20" value="" class="input disabled" readonly>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Hasil - Belanja (Untung/Rugi):</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindPofitLoss" name="txtFindPofitLoss" type="text" size="10" maxlength="25" value="<%=sStatus%>" class="input disabled" readonly>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Lejer Kedudukan Kewangan / List of Balance Sheet Ledger </a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="20%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama Koding</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Koding Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jumlah + Baki Bawa</td>
                    </tr>
                    <%
                        if (lsBalanceSheetDetails.Count > 0)
                        {
                            for (int i = 0; i < lsBalanceSheetDetails.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsBalanceSheetDetails[i];

                                if (modAcc.GetSetacctype.Equals("H") || modAcc.GetSetacctype.Equals("B"))
                                {
                                    if (modAcc.GetSetaccid == oAccCon.replaceNull(ConfigurationSettings.AppSettings["COASales"]) || modAcc.GetSetaccid == oAccCon.replaceNull(ConfigurationSettings.AppSettings["COADividend"]))
                                    {
                                        //do nothing
                                    }
                                    else
                                    {
                                        if (modAcc.GetSetacctype.Equals("H"))
                                        {
                                            modAcc.GetSetcredit = modAcc.GetSetcredit - modAcc.GetSetdebit;
                                            modAcc.GetSetdebit = 0;
                                            dTotalRevenue = dTotalRevenue + modAcc.GetSetcredit;
                                        }
                                        else if (modAcc.GetSetacctype.Equals("B"))
                                        {
                                            modAcc.GetSetdebit = modAcc.GetSetdebit - modAcc.GetSetcredit;
                                            modAcc.GetSetcredit = 0;
                                            dTotalExpenses = dTotalExpenses + modAcc.GetSetdebit;
                                        }
                                        dTotalProfitLoss = dTotalProfitLoss + modAcc.GetSetcredit - modAcc.GetSetdebit;
                                    }
                                }
                                else
                                {

                                    if (modAcc.GetSetacctype.Equals("A"))
                                    {
                                        modAcc.GetSetdebit = modAcc.GetSetdebit - modAcc.GetSetcredit;
                                        modAcc.GetSetcredit = 0;
                                        modAcc.GetSetcurrdebit = modAcc.GetSetcurrdebit - modAcc.GetSetcurrcredit;
                                        modAcc.GetSetcurrcredit = 0;
                                        dTotalAsset = dTotalAsset + modAcc.GetSetdebit;
                                    }
                                    else if (modAcc.GetSetacctype.Equals("L"))
                                    {
                                        modAcc.GetSetcredit = modAcc.GetSetcredit - modAcc.GetSetdebit;
                                        modAcc.GetSetdebit = 0;
                                        modAcc.GetSetcurrcredit = modAcc.GetSetcurrcredit - modAcc.GetSetcurrdebit;
                                        modAcc.GetSetcurrdebit = 0;
                                        dTotalLiability = dTotalLiability + modAcc.GetSetcredit;
                                    }
                                    else if (modAcc.GetSetacctype.Equals("E"))
                                    {
                                        modAcc.GetSetcredit = modAcc.GetSetcredit - modAcc.GetSetdebit;
                                        modAcc.GetSetdebit = 0;
                                        modAcc.GetSetcurrcredit = modAcc.GetSetcurrcredit - modAcc.GetSetcurrdebit;
                                        modAcc.GetSetcurrdebit = 0;
                                        dTotalEquity = dTotalEquity + modAcc.GetSetcredit;
                                    }
                                    /*
                                    else if (modAcc.GetSetacctype.Equals("H"))
                                    {
                                        modAcc.GetSetcredit = modAcc.GetSetcredit - modAcc.GetSetdebit;
                                        modAcc.GetSetdebit = 0;
                                        dTotalProfitLoss = dTotalProfitLoss + modAcc.GetSetcredit;
                                    }
                                    else if (modAcc.GetSetacctype.Equals("B"))
                                    {
                                        modAcc.GetSetdebit = modAcc.GetSetdebit - modAcc.GetSetcredit;
                                        modAcc.GetSetcredit = 0;
                                        dTotalProfitLoss = dTotalProfitLoss - modAcc.GetSetdebit;
                                    }
                                    */
                    %>
                    <tr data-id="<%=modAcc.GetSetaccid %>" class="tblText1">
                        <td valign="top" align="left"><%=modAcc.GetSetaccid %></td>
                        <td valign="top" align="left"><a href="#" onclick="fOpenWindow2('LedgerTranPage.aspx?fyr=<%=sCurrFyr %>&accid=<%=modAcc.GetSetaccid %>&datefrom=<%=sStartTranDate %>&dateto=<%=sEndTranDate %>&excludeclosing=Y');"><%=modAcc.GetSetaccdesc %></a><br /><%=modAcc.GetSetaccnumber %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccgroup %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetacctype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetparentid %><br/><%=modAcc.GetSetparentdesc %></td>
                        <td valign="top" align="left"><input id="txtCurrJumlah_<%=modAcc.GetSetaccid %>" name="txtCurrJumlah_<%=modAcc.GetSetaccid %>" type="text" size="6" maxlength="12" value="<%=modAcc.GetSetacctype.Equals("A")?modAcc.GetSetcurrdebit:modAcc.GetSetacctype.Equals("B")?modAcc.GetSetcurrdebit:modAcc.GetSetcurrcredit %>" class="input"/></td>
                        <td valign="top" align="left"><input id="txtJumlah_<%=modAcc.GetSetaccid %>" name="txtJumlah_<%=modAcc.GetSetaccid %>" type="text" size="6" maxlength="12" value="<%=modAcc.GetSetacctype.Equals("A")?modAcc.GetSetdebit:modAcc.GetSetacctype.Equals("B")?modAcc.GetSetdebit:modAcc.GetSetcredit %>" class="input"/></td>
                    </tr>
                    <%          
                                }
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

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsBalanceSheetDetails.Count %> record(s)</td>
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
            <% if (sStatus.Equals("NEW")) { %>
            document.getElementById("txtCurrJumlah_<%=oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAPnL"])%>").value = parseFloat(<%=dTotalProfitLoss%>);
            document.getElementById("txtJumlah_<%=oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAPnL"])%>").value = parseFloat(parseFloat(document.getElementById("txtJumlah_<%=oAccCon.replaceNull(ConfigurationSettings.AppSettings["COAPnL"])%>").value) + <%=dTotalProfitLoss%>);
            <% 
               } else if (sStatus.Equals("CONFIRMED")) {
                    dTotalEquity = dTotalEquity - dTotalProfitLoss;
               } 
            %>
            document.getElementById("txtFindAsset").value = "<%=dTotalAsset%>";
            document.getElementById("txtFindLiability").value = "<%=dTotalLiability%>";
            document.getElementById("txtFindEquity").value = "<%=dTotalEquity%>";
            document.getElementById("txtFindPofitLoss").value = "<%=Math.Round(dTotalProfitLoss,2)%>";
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

    </script>
</asp:Content>
