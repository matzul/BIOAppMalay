<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="PostingPage2.aspx.cs" Inherits="Accounting_PostingPage2" %>

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
                    %>
                    <tr data-id="<%=modAcc.GetSetrefno %>" class="tblText1">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettranno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrancode %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetrefno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrandate %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettrandesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettranamount %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetbpid %><br /><%=modAcc.GetSetbpdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <input id="addcheck_<%=i %>" name="addcheck_<%=i %>" data-action="select" type="checkbox" <%=modAcc.GetSethaschecked.Equals("1")?"checked class='disabled'":"enabled"%> />
                        </td>
                    </tr>
                    <tr data-id="<%=modAcc.GetSetrefno %>" class="tblText1">
                        <td data-id="" colspan="10" class="tblText2">
                            <button type="button" class="button_warning enabled" data-action="add">+</button>
                            <div class="content"></div>
                        </td>
                    </tr>
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
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Simpan" onclick="updatePostingData();">
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
                document.getElementById("txtFindFromDate").value = "01/01/<%=sCurrFyr%>";
                document.getElementById("txtFindToDate").value = "31/12/<%=sCurrFyr%>";
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

            //var trid = $(target).closest("[data-id]");
            var trid = $(target).closest("tr.tblText1");
            //get data-accid value for the TR
            var id = (trid.data("id"));

            var action = target.getAttribute('data-action')
            if (action) {
                alert(id);
                if (action == 'select') {
                    //alert('delete:' + id);
                }
                else if (action == 'add') {
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

        $('button.btndel').on('click', function (e) {
            var target = e && e.target || event.srcElement;
            var trid = $(target).closest("tr");
            var id = (trid.data("id"));
            alert(id);
            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'del') {
                    $(target).remove();
                }
            }
        });

    </script>
</asp:Content>

