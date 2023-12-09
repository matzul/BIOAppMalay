<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="AssetPage.aspx.cs" Inherits="Accounting_AssetPage" %>

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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Aset Harta Modal / Asset Capital</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">No. Aset:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAssetNo" name="txtFindAssetNo" type="text" size="10" maxlength="10" value="<%=sCurrAssetNo %>" class="input">
                        <div id="txtFindAssetNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Nama/ Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAssetDesc" name="txtFindAssetDesc" type="text" size="50" maxlength="50" value="<%=sCurrAssetDesc %>" class="input">
                        <div id="txtFindAssetDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Aset / List of Asset</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="10%" valign="middle" align="left" class="tblTitle3Mod">No. Aset</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisAsset.Count > 0)
                        {
                            for (int i = 0; i < lsFisAsset.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisAsset[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=modAcc.GetSetassetno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassetdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassetcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassettyp %></td>
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
                                <td width="50%" height="15" align="left"><%=lsFisAsset.Count %> record(s)</td>
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
                        <input class="button1a" name="btnModify" type="button" value="Kemaskini" onclick="fOpenWindow('AssetMasterPage.aspx');">					    
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisAssetDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisAssetDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Aset</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Aset</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtAssetNo" name="txtAssetNo" type="text" size="15" maxlength="20" value="" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAssetDesc" name="txtAssetDesc" size="30" maxlength="30" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAssetCat" name="txtAssetCat" size="15" maxlength="15" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAssetTyp" name="txtAssetTyp" class="input" />
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
                                            <button id="btnAddAsset" name="btnAddAsset" type="button" class="button1 btn-primary" onclick="insertFisAssetDetail();">Tambah</button>
                                            <button id="btnModifyAsset" name="btnModifyAsset" type="button" class="button1 btn-primary" onclick="updateFisAssetDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisAssetDetail();">Tutup</button>
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

        var fisAssetNoArray = [];
        var maxlengthdataautocomplete = 20;
        var fisAssetDescArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindAssetNo").value = "";
                document.getElementById("txtFindAssetDesc").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisAssetList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisAssetList", getFisAssetList_parameters, getFisAssetList_succeedAjaxFn, getFisAssetList_failedAjaxFn, false);

        });

        var getFisAssetList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisAssetList_succeedAjaxFn: " + textStatus);
            var getFisAssetList_result = JSON.parse(data.d);
            if (getFisAssetList_result.result == "Y") {
                var assetno = '';
                var assetdesc = '';
                $.each(getFisAssetList_result.fisassetlist, function (i, result) {
                    if (assetno != result.GetSetassetno + ':' + result.GetSetassetdesc) {
                        var objData = {};
                        objData.value = result.GetSetassetno + ':' + result.GetSetassetdesc;
                        objData.data = result.GetSetassetno;
                        fisAssetNoArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        assetno = result.GetSetassetno + ':' + result.GetSetassetdesc;
                    }

                    if (assetdesc != result.GetSetassetdesc) {
                        var objData = {};
                        objData.value = result.GetSetassetdesc;
                        objData.data = result.GetSetassetdesc;
                        fisAssetDescArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        assetdesc = result.GetSetassetdesc;
                    }

                });
            }
            else {
                console.log("getFisAssetList_result.result: " + getFisAssetList_result.result);
            }
        }

        var getFisAssetList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisAssetList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindAssetNo').autocomplete({
            lookup: fisAssetNoArray,
            appendTo: '#txtFindAssetNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindAssetNo').val(suggestion.data);
            }
        });

        $('#txtFindAssetDesc').autocomplete({
            lookup: fisAssetDescArray,
            appendTo: '#txtFindAssetDesc-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindAssetDesc').val(suggestion.data);
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
                    openFisAssetDetail('OPEN', id);
                }
            }
        });

        function openFisAssetDetail(typ, id) {
            $('#FisAssetDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddAsset').hide();
                $('#btnModifyAsset').show();

                var getFisAssetDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getFisAssetDetail", getFisAssetDetail_parameters, getFisAssetDetail_succeedAjaxFn, getFisAssetDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddAsset').show();
                $('#btnModifyAsset').hide();
            }
        }

        var getFisAssetDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisAssetDetail_succeedAjaxFn: " + textStatus);
            var getFisAssetDetail_result = JSON.parse(data.d);
            if (getFisAssetDetail_result.result == "Y") {
                $('#hidId').val(getFisAssetDetail_result.fisassetdetail.GetSetid);
                $('#txtAssetNo').val(getFisAssetDetail_result.fisassetdetail.GetSetassetno);
                $('#txtAssetDesc').val(getFisAssetDetail_result.fisassetdetail.GetSetassetdesc);
                $('#txtAssetCat').val(getFisAssetDetail_result.fisassetdetail.GetSetassetcat);
                $('#txtAssetTyp').val(getFisAssetDetail_result.fisassetdetail.GetSetassettyp);
                $('#lsStatus').val(getFisAssetDetail_result.fisassetdetail.GetSetstatus);
            }
            else {
                alert(getFisAssetDetail_result.message);
            }
        }

        var getFisAssetDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisAssetDetail_failedAjaxFn: " + textStatus);
        }

        function insertFisAssetDetail() {
            var insertFisAssetDetail_parameters = ["currcomp", "<%=sCurrComp%>", "assetno", $('#txtAssetNo').val(), "assetdesc", $('#txtAssetDesc').val(), "assetcat", $('#txtAssetCat').val(), "assettyp", $('#txtAssetTyp').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisAssetDetail", insertFisAssetDetail_parameters, insertFisAssetDetail_succeedAjaxFn, insertFisAssetDetail_failedAjaxFn, false);
        }

        var insertFisAssetDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisAssetDetail_succeedAjaxFn: " + textStatus);
            var insertFisAssetDetail_result = JSON.parse(data.d);
            if (insertFisAssetDetail_result.result == "Y") {
                alert(insertFisAssetDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisAssetDetail_result.message);
            }
        }

        var insertFisAssetDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisAssetDetail_failedAjaxFn: " + textStatus);
        }

        function updateFisAssetDetail() {
            var updateFisAssetDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "assetno", $('#txtAssetNo').val(), "assetdesc", $('#txtAssetDesc').val(), "assetcat", $('#txtAssetCat').val(), "assettyp", $('#txtAssetTyp').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisAssetDetail", updateFisAssetDetail_parameters, updateFisAssetDetail_succeedAjaxFn, updateFisAssetDetail_failedAjaxFn, false);
        }

        var updateFisAssetDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisAssetDetail_succeedAjaxFn: " + textStatus);
            var updateFisAssetDetail_result = JSON.parse(data.d);
            if (updateFisAssetDetail_result.result == "Y") {
                alert(updateFisAssetDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisAssetDetail_result.message);
            }
        }

        var updateFisAssetDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisAssetDetail_failedAjaxFn: " + textStatus);
        }

        function closeFisAssetDetail() {
            resetFisAssetDetail();
            $('#FisAssetDetail').modal('hide');
        }

        function resetFisAssetDetail() {
            $('#hidId').val("");
            $('#txtAssetNo').val("");
            $('#txtAssetDesc').val("");
            $('#txtAssetCat').val("");
            $('#txtAssetTyp').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

