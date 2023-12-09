﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="InventoryPage.aspx.cs" Inherits="Accounting_InventoryPage" %>

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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Stok & Inventori / Inventory & Stock</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Item No:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemNo" name="txtFindItemNo" type="text" size="10" maxlength="10" value="<%=sCurrItemNo %>" class="input">
                        <div id="txtFindItemNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Nama/ Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemDesc" name="txtFindItemDesc" type="text" size="50" maxlength="50" value="<%=sCurrItemDesc %>" class="input">
                        <div id="txtFindItemDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Stok & Inventori / List of Inventory & Stock</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="10%" valign="middle" align="left" class="tblTitle3Mod">No. Item</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisItem.Count > 0)
                        {
                            for (int i = 0; i < lsFisItem.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisItem[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=modAcc.GetSetitemno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemtype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                        </td>
                    </tr>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="6" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsFisItem.Count %> record(s)</td>
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
                        <input class="button1a" name="btnModify" type="button" value="Kemaskini" onclick="fOpenWindow('InventoryMasterPage.aspx');">					    
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Inventori & Stok</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Item</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemNo" name="txtItemNo" type="text" size="15" maxlength="20" value="" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemDesc" name="txtItemDesc" size="30" maxlength="30" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemCat" name="txtItemCat" size="15" maxlength="15" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemType" name="txtItemType" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <option value="NEW">NEW</option>
                                                <option value="CONFIRMED">CONFIRMED</option>
                                                <option value="CANCELLED">CANCELLED</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertFisItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateFisItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisItemDetail();">Tutup</button>
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

        var fisItemNoArray = [];
        var maxlengthdataautocomplete = 20;
        var fisItemDescArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindItemNo").value = "";
                document.getElementById("txtFindItemDesc").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisItemList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisItemList", getFisItemList_parameters, getFisItemList_succeedAjaxFn, getFisItemList_failedAjaxFn, false);

        });

        var getFisItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisItemList_succeedAjaxFn: " + textStatus);
            var getFisItemList_result = JSON.parse(data.d);
            if (getFisItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getFisItemList_result.fisitemlist, function (i, result) {
                    if (itemno != result.GetSetitemno + '-' + result.GetSetitemdesc) {
                        var objData = {};
                        objData.value = result.GetSetitemno + '-' + result.GetSetitemdesc;
                        objData.data = result.GetSetitemno;
                        fisItemNoArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemno = result.GetSetitemno + '-' + result.GetSetitemdesc;
                    }

                    if (itemdesc != result.GetSetitemdesc) {
                        var objData = {};
                        objData.value = result.GetSetitemdesc;
                        objData.data = result.GetSetitemdesc;
                        fisItemDescArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemdesc = result.GetSetitemdesc;
                    }

                });
            }
            else {
                console.log("getFisItemList_result.result: " + getFisItemList_result.result);
            }
        }

        var getFisItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisItemList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindItemNo').autocomplete({
            lookup: fisItemNoArray,
            appendTo: '#txtFindItemNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemNo').val(suggestion.data);
            }
        });

        $('#txtFindItemDesc').autocomplete({
            lookup: fisItemDescArray,
            appendTo: '#txtFindItemDesc-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemDesc').val(suggestion.data);
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
                if (action == 'delete') {
                    //alert('delete:' + id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openFisItemDetail('OPEN', id);
                }
            }
        });

        function openFisItemDetail(typ, id) {
            $('#FisItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getFisItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getFisItemDetail", getFisItemDetail_parameters, getFisItemDetail_succeedAjaxFn, getFisItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getFisItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisItemDetail_succeedAjaxFn: " + textStatus);
            var getFisItemDetail_result = JSON.parse(data.d);
            if (getFisItemDetail_result.result == "Y") {
                $('#hidId').val(getFisItemDetail_result.fisitemdetail.GetSetid);
                $('#txtItemNo').val(getFisItemDetail_result.fisitemdetail.GetSetitemno);
                $('#txtItemDesc').val(getFisItemDetail_result.fisitemdetail.GetSetitemdesc);
                $('#txtItemCat').val(getFisItemDetail_result.fisitemdetail.GetSetitemcat);
                $('#txtItemType').val(getFisItemDetail_result.fisitemdetail.GetSetitemtype);
                $('#lsStatus').val(getFisItemDetail_result.fisitemdetail.GetSetstatus);
            }
            else {
                alert(getFisItemDetail_result.message);
            }
        }

        var getFisItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertFisItemDetail() {
            var insertFisItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "itemno", $('#txtItemNo').val(), "itemdesc", $('#txtItemDesc').val(), "itemcat", $('#txtItemCat').val(), "itemtype", $('#txtItemType').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisItemDetail", insertFisItemDetail_parameters, insertFisItemDetail_succeedAjaxFn, insertFisItemDetail_failedAjaxFn, false);
        }

        var insertFisItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisItemDetail_succeedAjaxFn: " + textStatus);
            var insertFisItemDetail_result = JSON.parse(data.d);
            if (insertFisItemDetail_result.result == "Y") {
                alert(insertFisItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisItemDetail_result.message);
            }
        }

        var insertFisItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisItemDetail_failedAjaxFn: " + textStatus);
        }

        function updateFisItemDetail() {
            var updateFisItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "itemno", $('#txtItemNo').val(), "itemdesc", $('#txtItemDesc').val(), "itemcat", $('#txtItemCat').val(), "itemtype", $('#txtItemType').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisItemDetail", updateFisItemDetail_parameters, updateFisItemDetail_succeedAjaxFn, updateFisItemDetail_failedAjaxFn, false);
        }

        var updateFisItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisItemDetail_succeedAjaxFn: " + textStatus);
            var updateFisItemDetail_result = JSON.parse(data.d);
            if (updateFisItemDetail_result.result == "Y") {
                alert(updateFisItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisItemDetail_result.message);
            }
        }

        var updateFisItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisItemDetail_failedAjaxFn: " + textStatus);
        }

        function closeFisItemDetail() {
            resetFisItemDetail();
            $('#FisItemDetail').modal('hide');
        }

        function resetFisItemDetail() {
            $('#hidId').val("");
            $('#txtItemNo').val("");
            $('#txtItemDesc').val("");
            $('#txtItemCat').val("");
            $('#txtItemType').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>
