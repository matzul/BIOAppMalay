<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="LedgerQueryPage2.aspx.cs" Inherits="Accounting_LedgerQueryPage2" %>

<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/style_table_tree.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:TopMenu ID="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">

        <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
            <tr>
                <td align="center" valign="top"><a href="#" class="activeTab tab">Carian Lejer / Ledger Query</a></td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
            <tr class="tblTitle3Mod">
                <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
            </tr>
            <tr>
                <td width="20%" class="tblTextCommon">No. Koding:</td>
                <td width="80%" class="tblText2">
                    <input id="txtFindLedgerNo" name="txtFindLedgerNo" type="text" size="20" maxlength="20" value="" class="input"></td>
            </tr>
            <tr>
                <td width="20%" class="tblTextCommon">No. Sub-Koding:</td>
                <td width="80%" class="tblText2">
                    <input id="txtFindSubLedgerNo" name="txtFindSubLedgerNo" type="text" size="20" maxlength="20" value="" class="input"></td>
            </tr>
            <tr>
                <td width="20%" class="tblTextCommon">Tarikh Transaksi</td>
                <td colspan="20" class="tblText2">Dari:
                    <input class="input" name="txtFindFromDate" id="txtFindFromDate" type="text" value="" size="10" maxlength="12">
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
                    <input class="input" name="txtFindToDate" id="txtFindToDate" type="text" value="" size="10" maxlength="12">
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
            <thead>
                <th align="center" valign="top"><a href="#" class="activeTab tab">Transaksi Lejer / Ledger Transaction</a></th>
            </thead>
        </table>
        <table width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg" align="center">
            <thead>
                <tr class="tblTitle3Mod">
                    <td height="20" width="12%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                    <td width="20%" align="left" valign="left" class="tblTitle3Mod">Nama Koding</td>
                    <td width="10%" align="left" valign="left" class="tblTitle3Mod">Sub Koding</td>
                    <td width="15%" align="left" valign="left" class="tblTitle3Mod">No. Transaksi</td>
                    <td width="5%" valign="middle" align="middle" class="tblTitle3Mod">FYR</td>
                    <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                    <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No. Rujukan</td>
                    <td width="8%" valign="middle" align="left" class="tblTitle3Mod">T.Operasi</td>
                    <td width="8%" valign="middle" align="right" class="tblTitle3Mod">Debit</td>
                    <td width="8%" valign="middle" align="right" class="tblTitle3Mod">Kredit</td>
                </tr>
            </thead>
            <tbody>
                <tr class="tblText1">
                    <td class="tblText2 start">Item 1</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 cont">Item 1a</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 cont">Item 1b</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 end">Item 1c</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 start">Item 2</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 cont">Item 2a</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 cont">Item 2b</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td class="tblText2 end">Item 2c</td>
                    <td class="tblText2" colspan="9">123</td>
                </tr>
                <tr class="tblText1">
                    <td colspan="10" class="tblText2">&nbsp;No Record Found</td>
                </tr>
            </tbody>
        </table>

        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left">0 record(s)</td>
                                <td width="50%" align="right">page
                                    <asp:DropDownList CssClass="select" ID="lsPageList" runat="server" OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    of <%=sTotalPage %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
            </div>
        </form>

    </div>
    <script type="text/javascript">
        function openaccountingpage(url) {
            var popupWindow = window.open("Accounting/" + url, "accounting_page", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function actionclick(action) {
            var proceed = true;

            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }
    </script>
</asp:Content>

