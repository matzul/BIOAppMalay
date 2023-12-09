<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="InventoryMasterPage.aspx.cs" Inherits="Accounting_InventoryMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var fisitemtranStoredArray = [];
        var fisitemtranAddArray = [];
        var fisitemtranRemoveArray = [];
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <td width="20%" class="tblTextCommon">No Item:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemNo" name="txtFindItemNo" type="text" size="10" maxlength="10" value="<%=sCurrItemNo %>" class="input">
                        <div id="txtFindItemNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemDesc" name="txtFindItemDesc" type="text" size="50" maxlength="50" value="<%=sCurrItemDesc %>" class="input">
                        <div id="txtFindItemDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Pilihan:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindOption" name="lsFindOption">
                            <option value="LEFT" <%=sCurrOption.Equals("LEFT") ? "selected" : "" %>>Semua</option>
                            <option value="INNER" <%=sCurrOption.Equals("INNER") ? "selected" : "" %>>Telah Tambah</option>
                            <option value="ONLY" <%=sCurrOption.Equals("ONLY") ? "selected" : "" %>>Belum Tambah</option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Item Inventori / List of Invetory Items</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod"></td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">No. Item</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                    </tr>
                    <%
                        if (lsFisItemMasterTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisItemMasterTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisItemMasterTran[i];
                    %>
                    <tr class="tblText1" data-itemno="<%=modAcc.GetSetitemno %>">
                        <td valign="top" align="center">
                            <input data-action="select" type="checkbox" <%=modAcc.GetSethaschecked.Equals("1")?"checked class='disabled'":"enabled"%> /></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetitemtype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <script type="text/javascript">
                            <%=modAcc.GetSethaschecked.Equals("1")?"fisitemtranStoredArray.push('"+modAcc.GetSetitemno+"');":""%>
                        </script>
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
                                <td width="50%" height="15" align="left"><%=lsFisItemMasterTran.Count %> record(s)</td>
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
                        <input class="button1a" id="btnSave" type="button" value="Simpan" onclick="">
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

        var fisItemNoArray = [];
        var maxlengthdataautocomplete = 20;
        var fisItemDescArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindItemNo").value = "";
                document.getElementById("txtFindItemDesc").value = "";
                document.getElementById("lsFindOption").selectedIndex = 0;
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

            var tritemno = $(target).closest("[data-itemno]");
            //get data-accid value for the TR
            var itemno = (tritemno.data("itemno"));

            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'select') {

                    if ($(target).hasClass('disabled')) {
                        $(target).removeClass('disabled').addClass('enabled');
                        $(target).prop('checked', true);
                    }
                    else {
                        if ($(target).prop('checked')) {

                            var i = fisitemtranStoredArray.indexOf(itemno);
                            if (i == -1) {
                                if (fisitemtranAddArray.indexOf(itemno) == -1)
                                    fisitemtranAddArray.push(itemno);
                            } else {

                            }
                            var j = fisitemtranRemoveArray.indexOf(itemno);
                            if (j >= 0) {
                                fisitemtranRemoveArray.splice(j, 1);
                            }
                        } else {

                            var i = fisitemtranStoredArray.indexOf(itemno);
                            if (i >= 0) {
                                if (fisitemtranRemoveArray.indexOf(itemno) == -1)
                                    fisitemtranRemoveArray.push(itemno);
                            }
                            else {

                            }
                            var j = fisitemtranAddArray.indexOf(itemno);
                            if (j >= 0) {
                                fisitemtranAddArray.splice(j, 1);
                            }
                        }
                    }
                }
                else if (action == 'edit') {
                    //alert('edit:' + bpid);
                    openFisItemDetail('OPEN', itemno);
                }
            }
        });

        $("#btnSave").on("click", function (e) {
            var updateFisItemMasterTranList_parameters = ["currcomp", "<%=sCurrComp%>", "itemadd", fisitemtranAddArray, "itemremove", fisitemtranRemoveArray];
            PageMethod("updateFisItemMasterTranList", updateFisItemMasterTranList_parameters, updateFisItemMasterTranList_succeedAjaxFn, updateFisItemMasterTranList_failedAjaxFn, false);
        });

        var updateFisItemMasterTranList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisItemMasterTranList_succeedAjaxFn: " + textStatus);
            var updateFisItemMasterTranList_result = JSON.parse(data.d);
            if (updateFisItemMasterTranList_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(updateFisItemMasterTranList_result.message);
            }
            console.log("updateFisItemMasterTranList_result.result: " + updateFisItemMasterTranList_result.result);
            console.log("updateFisItemMasterTranList_result.result: " + updateFisItemMasterTranList_result.message);
        }

        var updateFisItemMasterTranList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisItemMasterTranList_failedAjaxFn: " + textStatus);
        }

        function openFisItemDetail(typ, itemno) {
            $('#FisItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getFisItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "itemno", itemno];
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

