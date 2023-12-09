<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="InvoiceIssuedPage.aspx.cs" Inherits="Accounting_InvoiceIssuedPage" %>
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
        var invoicenoArray = [];
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
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Invois Dikeluarkan / Invoice Issued</a></td>
            </tr>
        </table>
		<table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
            <tr class="tblTitle3Mod">
			  <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
		    </tr>
		    <tr> 
			  <td width="20%" class="tblTextCommon">No. Invois:</td>
			  <td width="80%" class="tblText2">
                  <input id="txtFindInvoiceNo" name="txtFindInvoiceNo" type="text" size="20" maxlength="20" value="<%=sCurrInvoiceNo %>" class="input">
                  <div id="txtFindInvoiceNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Senarai Transaksi Invois Dikeluarkan / List of Invoice Issued Transaction</a></td>
            </tr>
        </table>

        <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">No. Invois</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Keterangan Item</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Pelanggan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jumlah (RM)</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Sah</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">No. Transaksi</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod Transaksi</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status Posting</td>
                    </tr>
                    <%
                        if (lsInvoiceIssued.Count > 0)
                        {
                            for (int i = 0; i < lsInvoiceIssued.Count; i++)
                            {
                                MainModel modMain = (MainModel)lsInvoiceIssued[i];
                                bool enabledcheckbox = true;

                                String openlink = "";
                    %>
                    <tr data-id="<%=modMain.GetSetinvoiceno %>_<%=modMain.GetSetlineno %>" class="tblText1">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modMain.GetSetfyr %></td>
                        <td valign="top" align="left"><a href="#" onclick="openeditinvoice('<%=modMain.GetSetcomp%>', '<%=modMain.GetSetinvoiceno%>');"><%=modMain.GetSetinvoiceno %></a></td>
                        <td valign="top" align="left"><%=modMain.GetSetinvoicecat %></td>
                        <td valign="top" align="left"><%=modMain.GetSetitemno %><br/><%=modMain.GetSetitemdesc %></td>
                        <td valign="top" align="left"><%=modMain.GetSetbpid %><br/><%=modMain.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modMain.GetSettotalinvoice %></td>
                        <td valign="top" align="left"><%=modMain.GetSetconfirmeddate %></td>
                        <td valign="top" align="left"><a href="#" onclick="fOpenWindow('JournalPostingPage.aspx?fyr=<%=sCurrFyr %>&action=OPEN&tranno=<%=modMain.GetSettranno %>&trancode=<%=modMain.GetSettrancode %>');"><%=modMain.GetSettranno %></a></td>
                        <td valign="top" align="left"><%=modMain.GetSettrancode %></td>
                        <td valign="top" align="left"><%=modMain.GetSetstatus %></td>
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
                                <td width="50%" height="15" align="left"><%=lsInvoiceIssued.Count %> record(s)</td>
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
                document.getElementById("txtFindInvoiceNo").value = "";
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

            var getInvoiceList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>"];
            PageMethod("getInvoiceList", getInvoiceList_parameters, getInvoiceList_succeedAjaxFn, getInvoiceList_failedAjaxFn, false);

        });

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

        var getInvoiceList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getInvoiceList_succeedAjaxFn: " + textStatus);
            var getInvoiceList_result = JSON.parse(data.d);
            if (getInvoiceList_result.result == "Y") {
                $.each(getInvoiceList_result.invoicelist, function (i, result) {
                    var objData = {};
                    objData.value = result.GetSetinvoiceno + '-' + result.GetSetbpdesc;
                    objData.data = result.GetSetinvoiceno;
                    invoicenoArray.push(objData);
                    if (objData.value.length > maxlengthdataautocomplete) {
                        maxlengthdataautocomplete = objData.value.length;
                    }
                });
            }
            else {
                console.log("getInvoiceList_succeedAjaxFn.result: " + getInvoiceList_result.result);
            }
            //console.log("invoicenoArray: " + JSON.stringify(invoicenoArray));
        }

        var getInvoiceList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getInvoiceList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindInvoiceNo').autocomplete({
            lookup: invoicenoArray,
            appendTo: '#txtFindInvoiceNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                $('#txtFindInvoiceNo').val(suggestion.data);
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

