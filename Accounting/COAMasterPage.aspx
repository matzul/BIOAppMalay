<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccountingChild.master" AutoEventWireup="true" CodeFile="COAMasterPage.aspx.cs" Inherits="Accounting_COAMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var fiscoatranStoredArray = [];
        var fiscoatranAddArray = [];
        var fiscoatranRemoveArray = [];
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Master Carta Akaun / Master Chart of Account (COA)</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
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
                        <input id="txtFindFyr" name="txtFindFyr" type="text" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input disabled" readonly>
                    </td>
                </tr>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Master Carta Akaun / Master of Chart of Account (COA)</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod"></td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Level</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama Koding</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Sub-Koding</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Koding Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsFisCOAMasterTran.Count > 0)
                        {
                            for (int i = 0; i < lsFisCOAMasterTran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisCOAMasterTran[i];
                    %>
                    <tr class="tblText1" data-accid="<%=modAcc.GetSetaccid %>">
                        <td valign="top" align="center">
                            <input id="addcheck_<%=modAcc.GetSetaccid %>" name="addcheck_<%=modAcc.GetSetaccid %>" data-action="select" type="checkbox" <%=modAcc.GetSethaschecked.Equals("1")?"checked class='disabled'":"enabled"%> /></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetacclevel %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccnumber %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccgroup %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetacctype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetparentid %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Kemaskini</button>
                        </td>
                        <script type="text/javascript">
                            <%=modAcc.GetSethaschecked.Equals("1")?"fiscoatranStoredArray.push('"+modAcc.GetSetaccid+"');":""%>
                        </script>
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

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td>
                        <table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
                            <tr>
                                <td width="50%" height="15" align="left"><%=lsFisCOATran.Count %> record(s)</td>
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
                        <input class="button1a" id="btnSave" type="button" value="Simpan COA" onclick="">
                        <input class="button1a" id="btnAdd" name="btnAdd" type="button" value="Daftar Lejer" onclick="openFisCOADetail('NEW', '');">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="FisCOADetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbFisCOADetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Lejer</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsAccType" name="lsAccType" class="select">
                                                <option value="A">Aset</option>
                                                <option value="L">Liabiliti</option>
                                                <option value="E">Equiti/Modal</option>
                                                <option value="H">Hasil</option>
                                                <option value="B">Belanja</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kumpulan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccGroup" name="txtAccGroup" class="input" />
                                            <div id="txtAccGroup-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Level</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccLevel" name="txtAccLevel" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">End Level</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsEndLevel" name="lsEndLevel" class="select">
                                                <option value="N">N</option>
                                                <option value="Y">Y</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsAccCat" name="lsAccCat" class="select">
                                                <option value="">-Select-</option>
                                                <option value="BANK">Bank</option>
                                                <option value="CUSTOMER">Pelanggan</option>
                                                <option value="SUPPLIER">Pembekal</option>
                                                <option value="INVENTORY">Inventori</option>
                                                <option value="INVESTMENT">Pelaburan</option>
                                                <option value="ASSET">Hartanah/Loji/Aset</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Sub-Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccCode" name="txtAccCode" class="input" />
                                            <div id="txtAccCode-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Akaun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccNumber" name="txtAccNumber" class="input" />
                                            <div id="txtAccNumber-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Koding Rujukan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtParentId" name="txtParentId" class="input" />
                                            <div id="txtParentId-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccId" name="txtAccId" class="input" /></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Koding</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtAccName" name="txtAccName" class="input" /></td>
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
                                            <button id="btnAddFisCOA" name="btnAddFisCOA" type="button" class="button1 btn-primary" onclick="insertFisCOADetail();">Tambah</button>
                                            <button id="btnModifyFisCOA" name="btnModifyFisCOA" type="button" class="button1 btn-primary" onclick="updateFisCOADetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeFisCOADetail();">Tutup</button>
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

        var fiscoaArray = [];
        var maxlengthdataautocomplete = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindLedgerNo").value = "";
                document.getElementById("lsFindType").selectedIndex = 0;
                document.getElementById("lsFindCategory").selectedIndex = 0;
                document.getElementById("lsFindOption").selectedIndex = 0;
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getFisCOAList", getFisCOAList_parameters, getFisCOAList_succeedAjaxFn, getFisCOAList_failedAjaxFn, false);

        });

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


        $('#txtAccGroup').focus(function () {
            var paramArray = ["currcomp", "<%=sCurrComp%>", "acctype", $('#lsAccType').val()];
            var fn = "getLegderAccGroup";
            var paramList = '';
            var pagePath = window.location.pathname;
            var fisAccGroup = [];
            var maxlengthdatafisAccGroup = 20;

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }

            paramList = '{' + paramList + '}';
            console.log("getLegderAccGroup_paramList: " + paramList);

            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    console.log("getLegderAccGroup_succeedAjaxFn: " + textStatus);
                    var getLegderAccGroup_result = JSON.parse(data.d);
                    if (getLegderAccGroup_result.result == "Y") {
                        //fisAccGroup = [];
                        $.each(getLegderAccGroup_result.fisaccgroup, function (i, result) {
                            var objData = {};
                            objData.value = result.GetSetaccgroup + '-' + result.GetSetaccdesc;
                            objData.data = result.GetSetaccgroup;
                            fisAccGroup.push(objData);
                            if (objData.value.length > maxlengthdatafisAccGroup) {
                                maxlengthdatafisAccGroup = objData.value.length;
                            }
                        });
                        $('#txtAccGroup').autocomplete({
                            lookup: fisAccGroup,
                            appendTo: '#txtAccGroup-container',
                            minLength: 0,
                            minChars: 0,
                            width: maxlengthdatafisAccGroup * 12,
                            onSelect: function (suggestion) {
                                //console.log("suggestion: " + JSON.stringify(suggestion));
                                $('#txtAccGroup').val(suggestion.data);
                            }
                        });
                    }
                    else {
                        $('#txtAccGroup').autocomplete("close");
                        console.log("getLegderAccGroup_result.message: " + getLegderAccGrou_result.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("getLegderAccGroup_failedAjaxFn: " + textStatus);
                },
                async: true
            });
            console.log("getLegderAccGroup_object: " + fisAccGroup);
        });

        $('#txtParentId').focus(function () {
            var paramArray = ["currcomp", "<%=sCurrComp%>", "acctype", $('#lsAccType').val(), "accgroup", $('#txtAccGroup').val(), "acclevel", $('#txtAccLevel').val()];
            var fn = "getLegderParentId";
            var paramList = '';
            var pagePath = window.location.pathname;
            var fisParentId = [];
            var maxlengthdatafisParentId = 20;

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }

            paramList = '{' + paramList + '}';
            console.log("getLegderParentId_paramList: " + paramList);

            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    console.log("getLegderParentId_succeedAjaxFn: " + textStatus);
                    var getLegderParentId_result = JSON.parse(data.d);
                    if (getLegderParentId_result.result == "Y") {
                        //fisParentId = [];
                        $.each(getLegderParentId_result.fisparentid, function (i, result) {
                            var objData = {};
                            objData.value = result.GetSetaccid + '-' + result.GetSetaccdesc;
                            objData.data = result.GetSetaccid;
                            fisParentId.push(objData);
                            if (objData.value.length > maxlengthdatafisParentId) {
                                maxlengthdatafisParentId = objData.value.length;
                            }
                        });
                        $('#txtParentId').autocomplete({
                            lookup: fisParentId,
                            appendTo: '#txtParentId-container',
                            minLength: 0,
                            minChars: 0,
                            width: maxlengthdatafisParentId * 12,
                            onSelect: function (suggestion) {
                                //console.log("suggestion: " + JSON.stringify(suggestion));
                                $('#txtParentId').val(suggestion.data);
                            }
                        });
                    }
                    else {
                        $('#txtParentId').autocomplete("close");
                        console.log("getLegderParentId_result.message: " + getLegderAccGrou_result.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("getLegderParentId_failedAjaxFn: " + textStatus);
                },
                async: true
            });
            console.log("getLegderParentId_object: " + fisParentId);
        });

        $('#txtAccCode').focus(function () {
            if ($('#lsAccCat').val() == 'BANK') {
                var paramArray = ["currcomp", "<%=sCurrComp%>", "bankid", "", "accountno", ""];
                var fn = "getFisBankList";
                var paramList = '';
                var pagePath = window.location.pathname;
                var fisBankId = [];
                var maxlengthdatafisBankId = 20;

                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }

                paramList = '{' + paramList + '}';
                //console.log("getLegderBankId_paramList: " + paramList);

                $.ajax({
                    type: "POST",
                    url: pagePath + "/" + fn,
                    contentType: "application/json; charset=utf-8",
                    data: paramList,
                    dataType: "json",
                    success: function (data, textStatus, jqXHR) {
                        console.log("getLegderBankId_succeedAjaxFn: " + textStatus);
                        var getLegderBankId_result = JSON.parse(data.d);
                        if (getLegderBankId_result.result == "Y") {
                            var bankid = '';
                            $.each(getLegderBankId_result.fisbanklist, function (i, result) {
                                if (bankid != result.GetSetbankid + ':' + result.GetSetbankname) {
                                    var objData = {};
                                    objData.value = result.GetSetbankid + ':' + result.GetSetbankname;
                                    objData.data = result.GetSetbankid;
                                    fisBankId.push(objData);
                                    if (objData.value.length > maxlengthdatafisBankId) {
                                        maxlengthdatafisBankId = objData.value.length;
                                    }
                                    bankid = result.GetSetbankid + ':' + result.GetSetbankname;
                                }
                            });
                            $('#txtAccCode').autocomplete({
                                lookup: fisBankId,
                                appendTo: '#txtAccCode-container',
                                minLength: 0,
                                minChars: 0,
                                width: maxlengthdatafisBankId * 12,
                                onSelect: function (suggestion) {
                                    //console.log("suggestion: " + JSON.stringify(suggestion));
                                    $('#txtAccCode').val(suggestion.data);
                                    $('#txtAccName').val(suggestion.value.split(':')[1]);

                                }
                            });
                        }
                        else {
                            $('#txtAccCode').autocomplete("close");
                            console.log("getLegderBankId_result.message: " + getLegderBankId_result.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("getLegderBankId_failedAjaxFn: " + textStatus);
                    },
                    async: true
                });
                //console.log("getLegderBankId_object: " + fisBankId);
            }
            if ($('#lsAccCat').val() == 'CUSTOMER' || $('#lsAccCat').val() == 'SUPPLIER') {
                var paramArray = ["currcomp", "<%=sCurrComp%>", "bpid", "", "bpdesc", ""];
                var fn = "getFisBpList";
                var paramList = '';
                var pagePath = window.location.pathname;
                var fisBpId = [];
                var maxlengthdatafisBpId = 20;

                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }

                paramList = '{' + paramList + '}';

                $.ajax({
                    type: "POST",
                    url: pagePath + "/" + fn,
                    contentType: "application/json; charset=utf-8",
                    data: paramList,
                    dataType: "json",
                    success: function (data, textStatus, jqXHR) {
                        console.log("getFisBpList_succeedAjaxFn: " + textStatus);
                        var getFisBpList_result = JSON.parse(data.d);
                        if (getFisBpList_result.result == "Y") {
                            var bpid = '';
                            $.each(getFisBpList_result.fisbplist, function (i, result) {
                                if (bpid != result.GetSetbpid + ':' + result.GetSetbpdesc) {
                                    var objData = {};
                                    objData.value = result.GetSetbpid + ':' + result.GetSetbpdesc;
                                    objData.data = result.GetSetbpid;
                                    fisBpId.push(objData);
                                    if (objData.value.length > maxlengthdatafisBpId) {
                                        maxlengthdatafisBpId = objData.value.length;
                                    }
                                    bpid = result.GetSetbpid + ':' + result.GetSetbpdesc;
                                }
                            });
                            $('#txtAccCode').autocomplete({
                                lookup: fisBpId,
                                appendTo: '#txtAccCode-container',
                                minLength: 0,
                                minChars: 0,
                                width: maxlengthdatafisBpId * 12,
                                onSelect: function (suggestion) {
                                    //console.log("suggestion: " + JSON.stringify(suggestion));
                                    $('#txtAccCode').val(suggestion.data);
                                    $('#txtAccNumber').val(suggestion.data);
                                    $('#txtAccName').val(suggestion.value.split(':')[1]);
                                }
                            });
                        }
                        else {
                            $('#txtAccCode').autocomplete("close");
                            console.log("getFisBpList_result.message: " + getFisBpList_result.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("getFisBpList_failedAjaxFn: " + textStatus);
                    },
                    async: true
                });
                console.log("getFisBpList_object: " + fisBpId);
            }

            if ($('#lsAccCat').val() == 'INVENTORY') {
                var paramArray = ["currcomp", "<%=sCurrComp%>", "itemno", "", "itemdesc", ""];
                var fn = "getFisItemList";
                var paramList = '';
                var pagePath = window.location.pathname;
                var fisItem = [];
                var maxlengthdatafisitem = 20;

                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }

                paramList = '{' + paramList + '}';

                $.ajax({
                    type: "POST",
                    url: pagePath + "/" + fn,
                    contentType: "application/json; charset=utf-8",
                    data: paramList,
                    dataType: "json",
                    success: function (data, textStatus, jqXHR) {
                        console.log("getFisItemList_succeedAjaxFn: " + textStatus);
                        var getFisItemList_result = JSON.parse(data.d);
                        if (getFisItemList_result.result == "Y") {
                            var itemno = '';
                            $.each(getFisItemList_result.fisitemlist, function (i, result) {
                                if (itemno != result.GetSetitemno + ':' + result.GetSetitemdesc) {
                                    var objData = {};
                                    objData.value = result.GetSetitemno + ':' + result.GetSetitemdesc;
                                    objData.data = result.GetSetitemno;
                                    fisItem.push(objData);
                                    if (objData.value.length > maxlengthdatafisitem) {
                                        maxlengthdatafisitem = objData.value.length;
                                    }
                                    itemno = result.GetSetitemno + ':' + result.GetSetitemdesc;
                                }
                            });
                            $('#txtAccCode').autocomplete({
                                lookup: fisItem,
                                appendTo: '#txtAccCode-container',
                                minLength: 0,
                                minChars: 0,
                                width: maxlengthdatafisitem * 12,
                                onSelect: function (suggestion) {
                                    //console.log("suggestion: " + JSON.stringify(suggestion));
                                    $('#txtAccCode').val(suggestion.data);
                                    $('#txtAccNumber').val(suggestion.data);
                                    $('#txtAccName').val(suggestion.value.split(':')[1]);
                                }
                            });
                        }
                        else {
                            $('#txtAccCode').autocomplete("close");
                            console.log("getFisItemList_result.message: " + getFisItemList_result.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("getFisItemList_failedAjaxFn: " + textStatus);
                    },
                    async: true
                });
                //console.log("getFisItemList_object: " + fisItem);
            }

            if ($('#lsAccCat').val() == 'ASSET') {
                var paramArray = ["currcomp", "<%=sCurrComp%>", "assetno", "", "assetdesc", "", "assettyp", ""];
                var fn = "getFisAssetList";
                var paramList = '';
                var pagePath = window.location.pathname;
                var fisAsset = [];
                var maxlengthdatafisasset = 20;

                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }

                paramList = '{' + paramList + '}';

                $.ajax({
                    type: "POST",
                    url: pagePath + "/" + fn,
                    contentType: "application/json; charset=utf-8",
                    data: paramList,
                    dataType: "json",
                    success: function (data, textStatus, jqXHR) {
                        console.log("getFisAssetList_succeedAjaxFn: " + textStatus);
                        var getFisAssetList_result = JSON.parse(data.d);
                        if (getFisAssetList_result.result == "Y") {
                            var assetinfo = '';
                            $.each(getFisAssetList_result.fisassetlist, function (i, result) {
                                if (assetinfo != result.GetSetassetno + ':' + result.GetSetassetdesc + ":" + result.GetSetassettyp) {
                                    var objData = {};
                                    objData.value = result.GetSetassetno + ':' + result.GetSetassetdesc + ":" + result.GetSetassettyp;
                                    objData.data = result.GetSetassetno;
                                    fisAsset.push(objData);
                                    if (objData.value.length > maxlengthdatafisasset) {
                                        maxlengthdatafisasset = objData.value.length;
                                    }
                                    assetinfo = result.GetSetassetno + ':' + result.GetSetassetdesc + ":" + result.GetSetassettyp;
                                }
                            });
                            $('#txtAccCode').autocomplete({
                                lookup: fisAsset,
                                appendTo: '#txtAccCode-container',
                                minLength: 0,
                                minChars: 0,
                                width: maxlengthdatafisasset * 12,
                                onSelect: function (suggestion) {
                                    //console.log("suggestion: " + JSON.stringify(suggestion));
                                    $('#txtAccCode').val(suggestion.value.split(':')[2]);
                                    $('#txtAccNumber').val(suggestion.data);
                                    $('#txtAccName').val(suggestion.value.split(':')[1]);
                                }
                            });
                        }
                        else {
                            $('#txtAccCode').autocomplete("close");
                            console.log("getFisAssetList_result.message: " + getFisAssetList_result.message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("getFisAssetList_failedAjaxFn: " + textStatus);
                    },
                    async: true
                });
                //console.log("getFisAssetList_object: " + fisAsset);
            }

        });

        $('#txtAccNumber').focus(function () {
            var paramArray = ["currcomp", "<%=sCurrComp%>", "bankid", $('#txtAccCode').val(), "accountno", ""];
            var fn = "getFisBankList";
            var paramList = '';
            var pagePath = window.location.pathname;
            var fisAccountNo = [];
            var maxlengthdatafisAcctNo = 20;

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }

            paramList = '{' + paramList + '}';
            //console.log("getLegderAcctNo_paramList: " + paramList);

            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: function (data, textStatus, jqXHR) {
                    console.log("getLegderAcctNo_succeedAjaxFn: " + textStatus);
                    var getLegderAcctNo_result = JSON.parse(data.d);
                    if (getLegderAcctNo_result.result == "Y") {
                        var accountno = '';
                        $.each(getLegderAcctNo_result.fisbanklist, function (i, result) {
                            if (accountno != result.GetSetaccountno + ':' + result.GetSetbankname) {
                                var objData = {};
                                objData.value = result.GetSetaccountno + ':' + result.GetSetbankname;
                                objData.data = result.GetSetaccountno;
                                fisAccountNo.push(objData);
                                if (objData.value.length > maxlengthdatafisAcctNo) {
                                    maxlengthdatafisAcctNo = objData.value.length;
                                }
                                accountno = result.GetSetaccountno + ':' + result.GetSetbankname;
                            }
                        });
                        $('#txtAccNumber').autocomplete({
                            lookup: fisAccountNo,
                            appendTo: '#txtAccNumber-container',
                            minLength: 0,
                            minChars: 0,
                            width: maxlengthdatafisAcctNo * 12,
                            onSelect: function (suggestion) {
                                //console.log("suggestion: " + JSON.stringify(suggestion));
                                $('#txtAccNumber').val(suggestion.data);
                            }
                        });
                    }
                    else {
                        $('#txtAccNumber').autocomplete("close");
                        console.log("getLegderAcctNo_result.message: " + getLegderAcctNo_result.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("getLegderAcctNo_failedAjaxFn: " + textStatus);
                },
                async: true
            });
            console.log("getLegderAcctNo_object: " + fisAccountNo);
        });

        /*
        $('#txtAccGroup').autocomplete({
            lookup: fisAccGroup,
            appendTo: '#txtAccGroup-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdatafisAccGroup * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtAccGroup').val(suggestion.data);
            }
        }).bind('focus', function () { $(this).autocomplete("search"); });
        */

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

            var trid = $(target).closest("[data-accid]");
            //get data-accid value for the TR
            var accid = (trid.data("accid"));

            var action = target.getAttribute('data-action')
            if (action) {
                //alert(action);
                if (action == 'select') {

                    if ($(target).hasClass('disabled')) {
                        $(target).removeClass('disabled').addClass('enabled');
                        $(target).prop('checked', true);
                    }
                    else {
                        if ($(target).prop('checked')) {

                            var i = fiscoatranStoredArray.indexOf(accid);
                            if (i == -1) {
                                if (fiscoatranAddArray.indexOf(accid) == -1)
                                    fiscoatranAddArray.push(accid);
                            } else {

                            }
                            var j = fiscoatranRemoveArray.indexOf(accid);
                            if (j >= 0) {
                                fiscoatranRemoveArray.splice(j, 1);
                            }
                        } else {

                            var i = fiscoatranStoredArray.indexOf(accid);
                            if (i >= 0) {
                                if (fiscoatranRemoveArray.indexOf(accid) == -1)
                                    fiscoatranRemoveArray.push(accid);
                            }
                            else {

                            }
                            var j = fiscoatranAddArray.indexOf(accid);
                            if (j >= 0) {
                                fiscoatranAddArray.splice(j, 1);
                            }
                        }
                    }
                    //alert('Stored:' + fiscoatranStoredArray + '\n' + 'Add:' + fiscoatranAddArray + '\n' + 'Remove:' + fiscoatranRemoveArray);
                    //alert('Add:'+fiscoatranAddArray);
                    //alert('Remove:'+fiscoatranRemoveArray);
                }
                else if (action == 'edit') {
                    //alert('edit:' + accid);
                    openFisCOADetail('OPEN', accid);
                }
            }
        });

        $("#btnSave").on("click", function (e) {
            var updateFisCOATranList_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr%>", "coaadd", fiscoatranAddArray, "coaremove", fiscoatranRemoveArray];
            PageMethod("updateFisCOATranList", updateFisCOATranList_parameters, updateFisCOATranList_succeedAjaxFn, updateFisCOATranList_failedAjaxFn, false);
        });

        var updateFisCOATranList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisCOATranList_succeedAjaxFn: " + textStatus);
            var updateFisCOATranList_result = JSON.parse(data.d);
            if (updateFisCOATranList_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(updateFisCOATranList_result.message);
            }
            console.log("updateFisCOATranList_result.result: " + updateFisCOATranList_result.result);
            console.log("updateFisCOATranList_result.result: " + updateFisCOATranList_result.message);
        }

        var updateFisCOATranList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisCOATranList_failedAjaxFn: " + textStatus);
        }

        function openFisCOADetail(typ, accid) {
            $('#FisCOADetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddFisCOA').hide();
                $('#btnModifyFisCOA').show();

                var getFisCOADetail_parameters = ["currcomp", "<%=sCurrComp%>", "accid", accid];
                PageMethod("getFisCOADetail", getFisCOADetail_parameters, getFisCOADetail_succeedAjaxFn, getFisCOADetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddFisCOA').show();
                $('#btnModifyFisCOA').hide();
            }
        }

        var getFisCOADetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getFisCOADetail_succeedAjaxFn: " + textStatus);
            var getFisCOADetail_result = JSON.parse(data.d);
            if (getFisCOADetail_result.result == "Y") {
                $('#hidId').val(getFisCOADetail_result.fiscoadetail.GetSetid);
                $('#lsAccType').val(getFisCOADetail_result.fiscoadetail.GetSetacctype);
                $('#txtAccGroup').val(getFisCOADetail_result.fiscoadetail.GetSetaccgroup);
                $('#txtAccLevel').val(getFisCOADetail_result.fiscoadetail.GetSetacclevel);
                $('#lsEndLevel').val(getFisCOADetail_result.fiscoadetail.GetSetendlevel);
                $('#lsAccCat').val(getFisCOADetail_result.fiscoadetail.GetSetacccategory);
                $('#txtAccId').val(getFisCOADetail_result.fiscoadetail.GetSetaccid);
                $('#txtAccName').val(getFisCOADetail_result.fiscoadetail.GetSetaccdesc);
                $('#txtAccCode').val(getFisCOADetail_result.fiscoadetail.GetSetacccode);
                $('#txtAccNumber').val(getFisCOADetail_result.fiscoadetail.GetSetaccnumber);
                $('#txtParentId').val(getFisCOADetail_result.fiscoadetail.GetSetparentid);
                $('#lsStatus').val(getFisCOADetail_result.fiscoadetail.GetSetstatus);
            }
            else {
                alert(getFisCOADetail_result.message);
            }
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.result);
            //console.log("getFisCOADetail_succeedAjaxFn.result: " + updateFisCOATranList_result.message);
        }

        var getFisCOADetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getFisCOADetail_failedAjaxFn: " + textStatus);
        }

        function insertFisCOADetail() {
            var insertFisCOADetail_parameters = ["currcomp", "<%=sCurrComp%>", "accid", $('#txtAccId').val(), "accdesc", $('#txtAccName').val(), "parentid", $('#txtParentId').val(), "accgroup", $('#txtAccGroup').val(), "acclevel", $('#txtAccLevel').val(), "endlevel", $('#lsEndLevel').val(), "acctype", $('#lsAccType').val(), "acccat", $('#lsAccCat').val(), "acccode", $('#txtAccCode').val(), "accnumber", $('#txtAccNumber').val(), "status", $('#lsStatus').val()];
            PageMethod("insertFisCOADetail", insertFisCOADetail_parameters, insertFisCOADetail_succeedAjaxFn, insertFisCOADetail_failedAjaxFn, false);
        }

        var insertFisCOADetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertFisCOADetail_succeedAjaxFn: " + textStatus);
            var insertFisCOADetail_result = JSON.parse(data.d);
            if (insertFisCOADetail_result.result == "Y") {
                alert(insertFisCOADetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertFisCOADetail_result.message);
            }
        }

        var insertFisCOADetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertFisCOADetail_failedAjaxFn: " + textStatus);
        }

        function updateFisCOADetail() {
            var updateFisCOADetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", $('#hidId').val(), "accid", $('#txtAccId').val(), "accdesc", $('#txtAccName').val(), "parentid", $('#txtParentId').val(), "accgroup", $('#txtAccGroup').val(), "acclevel", $('#txtAccLevel').val(), "endlevel", $('#lsEndLevel').val(), "acctype", $('#lsAccType').val(), "acccat", $('#lsAccCat').val(), "acccode", $('#txtAccCode').val(), "accnumber", $('#txtAccNumber').val(), "status", $('#lsStatus').val()];
            PageMethod("updateFisCOADetail", updateFisCOADetail_parameters, updateFisCOADetail_succeedAjaxFn, updateFisCOADetail_failedAjaxFn, false);
        }

        var updateFisCOADetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateFisCOADetail_succeedAjaxFn: " + textStatus);
            var updateFisCOADetail_result = JSON.parse(data.d);
            if (updateFisCOADetail_result.result == "Y") {
                alert(updateFisCOADetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateFisCOADetail_result.message);
            }
        }

        var updateFisCOADetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateFisCOADetail_failedAjaxFn: " + textStatus);
        }

        function closeFisCOADetail() {
            resetFisCOADetail();
            $('#FisCOADetail').modal('hide');
        }

        function resetFisCOADetail() {
            $('#hidId').val("");
            $('#lsAccType').val("");
            $('#txtAccGroup').val("");
            $('#txtAccLevel').val("");
            $('#lsEndLevel').val("");
            $('#lsAccCat').val("");
            $('#txtAccId').val("");
            $('#txtAccName').val("");
            $('#txtAccCode').val("");
            $('#txtAccNumber').val("");
            $('#txtParentId').val("");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

