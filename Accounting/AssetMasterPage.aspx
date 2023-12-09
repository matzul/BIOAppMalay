<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="AssetMasterPage.aspx.cs" Inherits="Accounting_AssetMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var fisassettranStoredArray = [];
        var fisassettranAddArray = [];
        var fisassettranRemoveArray = [];
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <td width="20%" class="tblTextCommon">No Aset:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAssetNo" name="txtFindAssetNo" type="text" size="10" maxlength="10" value="<%=sCurrAssetNo %>" class="input">
                        <div id="txtFindAssetNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAssetDesc" name="txtFindAssetDesc" type="text" size="50" maxlength="50" value="<%=sCurrAssetDesc %>" class="input">
                        <div id="txtFindAssetDesc-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Aset / List of Asset</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod"></td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">No. Aset</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                    </tr>
                    <%
                        if (lsFisAssetMasterTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisAssetMasterTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisAssetMasterTran[i];
                    %>
                    <tr class="tblText1" data-assetno="<%=modAcc.GetSetassetno %>|<%=modAcc.GetSetassettyp %>">
                        <td valign="top" align="center">
                            <input data-action="select" type="checkbox" <%=modAcc.GetSethaschecked.Equals("1")?"checked class='disabled'":"enabled"%> /></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassetno %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassetdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassetcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetassettyp %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
                        <script type="text/javascript">
                            <%=modAcc.GetSethaschecked.Equals("1")?"fisassettranStoredArray.push('"+modAcc.GetSetassetno+"|"+modAcc.GetSetassettyp+"');":""%>
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
                                <td width="50%" height="15" align="left"><%=lsFisAssetMasterTran.Count %> record(s)</td>
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

        var fisAssetNoArray = [];
        var maxlengthdataautocomplete = 20;
        var fisAssetDescArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindAssetNo").value = "";
                document.getElementById("txtFindAssetDesc").value = "";
                document.getElementById("lsFindOption").selectedIndex = 0;
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

            var trassetno = $(target).closest("[data-assetno]");
            //get data-accid value for the TR
            var assetinfo = (trassetno.data("assetno"));
            var assetarray = assetinfo.split("|");
            var assetno = "";
            var assettyp = "";
            if (assetarray.length > 0) {
                assetno = assetarray[0];
                assettyp = assetarray[1];
            }
            var action = target.getAttribute('data-action')
            if (action) {
                if (action == 'select') {

                    if ($(target).hasClass('disabled')) {
                        $(target).removeClass('disabled').addClass('enabled');
                        $(target).prop('checked', true);
                    }
                    else {
                        if ($(target).prop('checked')) {

                            var i = fisassettranStoredArray.indexOf(assetinfo);
                            if (i == -1) {
                                if (fisassettranAddArray.indexOf(assetinfo) == -1)
                                    fisassettranAddArray.push(assetinfo);
                            } else {

                            }
                            var j = fisassettranRemoveArray.indexOf(assetinfo);
                            if (j >= 0) {
                                fisassettranRemoveArray.splice(j, 1);
                            }
                        } else {

                            var i = fisassettranStoredArray.indexOf(assetinfo);
                            if (i >= 0) {
                                if (fisassettranRemoveArray.indexOf(assetinfo) == -1)
                                    fisassettranRemoveArray.push(assetinfo);
                            }
                            else {

                            }
                            var j = fisassettranAddArray.indexOf(assetinfo);
                            if (j >= 0) {
                                fisassettranAddArray.splice(j, 1);
                            }
                        }
                    }
                }
                else if (action == 'edit') {
                    //alert('edit:' + bpid);
                    openFisAssetDetail('OPEN', assetno, assettyp);
                }
            }
        });

        $("#btnSave").on("click", function (e) {
            var updateFisAssetMasterTranList_parameters = ["currcomp", "<%=sCurrComp%>", "assetadd", fisassettranAddArray, "assetremove", fisassettranRemoveArray];
            PageMethod("updateFisAssetMasterTranList", updateFisAssetMasterTranList_parameters, updateFisAssetMasterTranList_succeedAjaxFn, updateFisAssetMasterTranList_failedAjaxFn, false);
        });

        var updateFisAssetMasterTranList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisAssetMasterTranList_succeedAjaxFn: " + textStatus);
            var updateFisAssetMasterTranList_result = JSON.parse(data.d);
            if (updateFisAssetMasterTranList_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(updateFisAssetMasterTranList_result.message);
            }
            console.log("updateFisAssetMasterTranList_result.result: " + updateFisAssetMasterTranList_result.result);
            console.log("updateFisAssetMasterTranList_result.result: " + updateFisAssetMasterTranList_result.message);
        }

        var updateFisAssetMasterTranList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisAssetMasterTranList_failedAjaxFn: " + textStatus);
        }

        function openFisAssetDetail(typ, assetno, assettyp) {
            $('#FisAssetDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getFisAssetDetail_parameters = ["currcomp", "<%=sCurrComp%>", "assetno", assetno, "assettyp", assettyp];
                PageMethod("getFisAssetDetail", getFisAssetDetail_parameters, getFisAssetDetail_succeedAjaxFn, getFisAssetDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
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

