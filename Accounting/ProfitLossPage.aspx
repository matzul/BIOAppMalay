<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="ProfitLossPage.aspx.cs" Inherits="Accounting_ProfitLossPage" %>

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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Laporan Penyata Pendapatan / Profit & Loss Report</a></td>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Penyata Pendapatan / List of Profit & Loss Report</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="45%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Dari</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Hingga</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsProfitLoss.Count > 0)
                        {
                            for (int i = 0; i < lsProfitLoss.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsProfitLoss[i];

                    %>
                    <tr data-id="<%=modAcc.GetSetid %>" class="tblText1">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemtypedesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdatefrom %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdateto %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" data-action="open_trans" class="button_warning" onclick="openProfitLossDetails('OPEN','<%=modAcc.GetSetfyr %>','<%=modAcc.GetSetitemtype %>', '<%=modAcc.GetSetitemtypedesc %>','<%=modAcc.GetSetdatefrom %>', '<%=modAcc.GetSetdateto %>', '<%=modAcc.GetSetcurrency %>', '<%=modAcc.GetSetstatus %>');">Senarai Penyata Pendapatan</button>
                        </td>
                    </tr>
                    <%
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
                                <td width="50%" height="15" align="left"><%=lsProfitLoss.Count %> record(s)</td>
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

        function openProfitLossDetails(typ, fyr, itemtype, itemtypedesc, datefrom, dateto, currency, status) {
            if (typ == 'OPEN') {
                fOpenWindow('ProfitLossDetailsPage.aspx?fyr=' + fyr + '&itemtype=' + itemtype + '&itemtypedesc=' + itemtypedesc + '&datefrom=' + datefrom + '&dateto=' + dateto + '&currency=' + currency + '&status=' + status);
            }
        }

    </script>
</asp:Content>

