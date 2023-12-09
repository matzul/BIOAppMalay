<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageSalaryChild.master" AutoEventWireup="true" CodeFile="SalaryStaffRun.aspx.cs" Inherits="HumanResource_SalaryStaffRun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Maklumat Pemprosesan Gaji</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun:</td>
                    <td width="80%" class="tblText2"><%=sRunSalaryDetails.GetSetyear %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Bulan:</td>
                    <td width="80%" class="tblText2"><%=sRunSalaryDetails.GetSetmonth %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">Pemperosesan <%=sRunSalaryDetails.GetSettype %> bagi Bulan <%=sRunSalaryDetails.GetSetmonth %> Tahun <%=sRunSalaryDetails.GetSetyear %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2"><%=sRunSalaryDetails.GetSetcat %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Jenis:</td>
                    <td width="80%" class="tblText2"><%=sRunSalaryDetails.GetSettype %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Bilangan Kakitangan:</td>
                    <td width="80%" class="tblText2"><%=lsStaffSalaryList.Count %></td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Status:</td>
                    <td width="80%" class="tblText2"><%=sRunSalaryDetails.GetSetstatus %></td>
                </tr>
            </table>
            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kakitangan & Item Gaji</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="40%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="27%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsStaffSalaryList.Count > 0)
                        {
                            for (int i = 0; i < lsStaffSalaryList.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsStaffSalaryList[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="itemgaji">Item Gaji</button>
                            <br />
                            <p id="FolderFile1">
                                <a id="FileAttached1" name="FileAttached1" href="#" onclick="fOpenWindow2('../Attachment/HumanResource/<%=modItem.GetSetfilename1 %>');"><%=modItem.GetSetfilename1 %></a>
                            </p>
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
                                <td width="50%" height="15" align="left"><%=lsStaffSalaryList.Count %> record(s)</td>
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
                <input type="hidden" name="hidFyr" id="hidFyr" value="<%=sCurrFyr %>" />
                <input type="hidden" name="hidId" id="hidId" value="<%=sId %>" />
                <input type="hidden" name="hidSsdId" id="hidSsdId" value="" />
                <input type="hidden" name="hidSgId" id="hidSgId" value="" />
                <input type="hidden" name="hidSsgId" id="hidSsgId" value="" />
                <input type="hidden" name="hidStaffNo" id="hidStaffNo" value="" />
            </div>

            <div class="modal fade" id="SGItemSalaryDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGItemGajiDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Item Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemGajiStaffNo" name="txtItemGajiStaffNo" type="text" size="10" maxlength="10" value="" class="input" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiStaffName" name="txtItemGajiStaffName" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jawatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiPosition" name="txtItemGajiPosition" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jabatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiDept" name="txtItemGajiDept" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiCat" name="txtItemGajiCat" size="50" maxlength="50" value="<%=sRunSalaryDetails.GetSetcat %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiType" name="txtItemGajiType" size="20" maxlength="20" value="<%=sRunSalaryDetails.GetSettype %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiDesc" name="txtItemGajiDesc" size="50" maxlength="50" value="Pemperosesan <%=sRunSalaryDetails.GetSettype %> bagi Bulan <%=sRunSalaryDetails.GetSetmonth %> Tahun <%=sRunSalaryDetails.GetSetyear %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiFyr" name="txtItemGajiFyr" size="4" maxlength="4" value="<%=sRunSalaryDetails.GetSetyear %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Bulan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiMonth" name="txtItemGajiMonth" size="4" maxlength="4" value="<%=sRunSalaryDetails.GetSetmonth %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblItemGajiAdd" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonGaji" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnSaveItemGaji" name="btnSaveItemGaji" type="button" class="button1 btn-primary" onclick="insertSGItemGajiUpdate();">Kemaskini</button>
                                            <button type="button" class="button1" onclick="closeSGItemGajiAdd();">Tutup</button>
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

        var StaffNoArray = [];
        var maxlengthdataautocomplete = 20;
        var StaffNameArray = [];
        var maxlengthdataautocomplete2 = 20;
        var ItemGajiArray = [];
        var ValueGajiArray = [];
        var AmountGajiArray = [];

        function actionclick(action) {
            var proceed = true;
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $('#mytable').on('click', function (e) {
            var target = e && e.target || event.srcElement

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
                }
                else if (action == 'itemgaji') {
                    //alert('edit:' + id);
                    openSGItemSalaryDetail('OPEN', id);
                }
            }
        });

        function openSGItemSalaryDetail(typ, id) {
            if (typ == 'OPEN') {
                $('#SGItemSalaryDetail').modal({ backdrop: "static" });

                var getSGItemGajiDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>", "ss_id", <%=sId%>, "id", id];
                PageMethod("getStaffItemDetail", getSGItemGajiDetail_parameters, getSGItemGajiDetail_succeedAjaxFn, getSGItemGajiDetail_failedAjaxFn, false);

            }
        }

        var getSGItemGajiDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            //emptyItemGaji();
            $('#tblItemGajiAdd').empty();
            console.log("getSGItemGajiDetail_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidSsdId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#hidSgId').val(getSGItemDetail_result.itemdetail.GetSetsg_id);
                $('#hidSsgId').val(getSGItemDetail_result.itemdetail.GetSetssg_id);
                $('#hidStaffNo').val(getSGItemDetail_result.itemdetail.GetSetstaffno);
                $('#hidFyr').val(getSGItemDetail_result.itemdetail.GetSetfyr);
                $('#txtItemGajiStaffNo').val(getSGItemDetail_result.itemdetail.GetSetstaffno);
                $('#txtItemGajiStaffName').val(getSGItemDetail_result.itemdetail.GetSetname);
                $('#txtItemGajiPosition').val(getSGItemDetail_result.itemdetail.GetSetpos_name + " [" + getSGItemDetail_result.itemdetail.GetSetgred_name + "]");
                $('#txtItemGajiDept').val(getSGItemDetail_result.itemdetail.GetSetdept_name);

                var getSGItemGajiList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>", "staffno", $('#hidStaffNo').val(), "sg_id", $('#hidSgId').val(), "ssg_id", $('#hidSsgId').val()];
                PageMethod("getSGItemGajiList", getSGItemGajiList_parameters, getSGItemGajiList_succeedAjaxFn, getSGItemGajiList_failedAjaxFn, false);

            }
            else {
                alert(getSGItemDetail_result.message);
                emptyItemGaji();
            }
        }

        var getSGItemGajiDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            emptyItemGaji();
            console.log("getSGItemGajiDetail_failedAjaxFn: " + textStatus);
        }

        var getSGItemGajiList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemGajiList_succeedAjaxFn: " + textStatus);
            var getSGItemGajiList_result = JSON.parse(data.d);
            if (getSGItemGajiList_result.result == "Y") {
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="5%" valign="middle" align="left" class="tblTitle3Mod">Kod/Keterangan/Kategori</td>' +
                    '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis/Kumpulan Item</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>' +
                    '</tr>';
                $.each(getSGItemGajiList_result.itemlist, function (i, result) {

                    trHTML += '<tr data-id="' + result.GetSetcode + '"><td valign="top"><input type="checkbox" name="chk_id" class="groupsalaryitem" value="' + result.GetSetcode + '"></td>' +
                        '<td align="left" valign="top"><font>' + result.GetSetcode + '</font><input type="hidden" name="input_code" value="' + result.GetSetcode + '" /><br/>' +
                        '<font>' + result.GetSetdesc + '</font><input type="hidden" name="input_desc" value="' + result.GetSetdesc + '" /><br/>' +
                        '<font>' + result.GetSetcat + '</font><input type="hidden" name="input_cat" value="' + result.GetSetcat + '" class="input"/></td>' +
                        '<td align="left" valign="top"><font>' + result.GetSettype + '</font><input type="hidden" name="input_type" value="' + result.GetSettype + '" class="input"/><br/><font length="10">' + result.GetSetitemgroup + '</font><input type="hidden" name="input_group" value="' + result.GetSetitemgroup + '" class="input"/></td>' +
                        '<td align="left" valign="top"><input type="input" name="input_value" size="5" maxlength="5" value="' + result.GetSetitemvalue + '" class="input" /></td>' +
                        '<td align="left" valign="top"><input type="input" name="input_amount" size="5" maxlength="5" value="' + result.GetSetitemamount + '" class="input" readonly style="background-color:gray; color:floralwhite;"/></td>' +
                        '</tr>';
                });

                $('#tblItemGajiAdd').append(trHTML);

                var getSGItemGajiChecked_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>", "staffno", $('#hidStaffNo').val(), "ss_id", <%=sId%>, "ssd_id", $('#hidSsdId').val()];
                PageMethod("getStaffItemGajiChecked", getSGItemGajiChecked_parameters, getSGItemGajiChecked_succeedAjaxFn, getSGItemGajiChecked_failedAjaxFn, false);

            }
            else {
                console.log("getSGItemGajiList_result.result: " + getSGItemList_result.result);
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="5%" valign="middle" align="left" class="tblTitle3Mod">Kod/Keterangan/Kategori</td>' +
                    '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis/Kumpulan Item</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>' +
                    '</tr>';

                $('#tblItemGajiAdd').append(trHTML);

            }
        }

        var getSGItemGajiList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemGajiList_failedAjaxFn: " + textStatus);

            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="5%" valign="middle" align="left" class="tblTitle3Mod">Kod/Keterangan/Kategori</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis/Kumpulan Item</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>' +
                '</tr>';

            $('#tblItemGajiAdd').append(trHTML);
        }

        var getSGItemGajiChecked_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemGajiChecked_succeedAjaxFn: " + textStatus);
            var getSGItemGajiChecked_result = JSON.parse(data.d);
            if (getSGItemGajiChecked_result.result == "Y") {
                //$.each(getSGItemGajiChecked_result.itemlist, function (i, result) {

                    ItemGajiArray = [];
                    ValueGajiArray = [];
                    AmountGajiArray = [];
                    $.each(getSGItemGajiChecked_result.itemlist, function (i, result) {
                        ItemGajiArray.push(result.GetSetcode);
                        ValueGajiArray.push(result.GetSetitemvalue);
                        AmountGajiArray.push(result.GetSetitemamount);
                    });

                    var input_type = document.getElementsByName('input_type');
                    var input_group = document.getElementsByName('input_group');
                    var input_value = document.getElementsByName('input_value');
                    var input_amount = document.getElementsByName('input_amount');
                    var inputs = $('.groupsalaryitem');

                    for (var i = 0; i < inputs.length; i++) {
                        var pl1 = inputs[i].value;
                        var indexof = ItemGajiArray.indexOf(pl1);
                        //if (ItemGajiArray.includes(pl1)) {
                        if (indexof >= 0 ) {
                            inputs[i].checked = true;
                            input_value[i].value = ValueGajiArray[indexof];
                            input_amount[i].value = AmountGajiArray[indexof];
                        }
                        else {
                            inputs[i].checked = false;
                        }
                    }

                    for (var i = 0; i < inputs.length; i++) {
                        var pl1 = inputs[i].value;
                        if (inputs[i].checked == true) {
                            if (input_type[i].value == "AMOUNT") {
                                input_amount[i].value = input_value[i].value;
                            } else {
                                //calculate percentage
                                if (input_group[i].value.length > 0) {
                                    let arr = input_group[i].value.split(',');
                                    input_amount[i].value = formatMoney(calculateGajiAmount(arr) * parseFloat(input_value[i].value) / 100);
                                } else {
                                    input_amount[i].value = "0.00";
                                }
                            }
                        }
                        else {
                            input_amount[i].value = "0.00";
                        }
                    }

                //});
            }
            else {
                console.log("getSGItemGajiChecked_result.result: " + getSGItemGajiChecked_result.result);
            }
        }

        var getSGItemGajiChecked_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemGajiChecked_failedAjaxFn: " + textStatus);
        }

        $('#tblItemGajiAdd').on('click', function (e) {
            var target = e && e.target || event.srcElement

            var trid = $(target).closest("[data-id]");
            //get data-accid value for the TR
            var id = (trid.data("id"));

            var input_code = document.getElementsByName('input_code');
            var input_desc = document.getElementsByName('input_desc');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');
            var input_group = document.getElementsByName('input_group');
            var input_value = document.getElementsByName('input_value');
            var input_amount = document.getElementsByName('input_amount');

            var inputs = $('.groupsalaryitem');
            for (var i = 0; i < inputs.length; i++) {
                var pl1 = inputs[i].value;
                if (inputs[i].checked == true) {
                    if (input_type[i].value == "AMOUNT") {
                        input_amount[i].value = input_value[i].value;
                    } else {
                        //calculate percentage
                        if (input_group[i].value.length > 0) {
                            let arr = input_group[i].value.split(',');
                            input_amount[i].value = formatMoney(calculateGajiAmount(arr) * parseFloat(input_value[i].value) / 100);
                        } else {
                            input_amount[i].value = "0.00";
                        }
                    }
                }
                else
                {
                    input_amount[i].value = "0.00";
                }
            }
        });

        function calculateGajiAmount(arr) {
            let totalvalue = 0;
            var input_type = document.getElementsByName('input_type');
            var input_value = document.getElementsByName('input_value');
            var inputs = $('.groupsalaryitem');
            for (var i = 0; i < inputs.length; i++) {
                var pl1 = inputs[i].value;
                if (arr.includes(pl1)) {
                    if (inputs[i].checked == true) {
                        if (input_type[i].value == "AMOUNT") {
                            totalvalue = totalvalue + parseFloat(input_value[i].value);
                        }
                    }
                }
            }
            return totalvalue;
        }

        function emptyItemGaji() {
            $('#tblItemGajiAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="5%" valign="middle" align="left" class="tblTitle3Mod">Kod/Keterangan/Kategori</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis/Kumpulan Item</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jumlah</td>' +
                '</tr>';
            $('#tblItemGajiAdd').append(trHTML);
        }

        function insertSGItemGajiUpdate() {
            var paramArray = {};
            var ItemInputArray = [];

            var input_code = document.getElementsByName('input_code');
            var input_desc = document.getElementsByName('input_desc');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');
            var input_group = document.getElementsByName('input_group');
            var input_value = document.getElementsByName('input_value');
            var input_amount = document.getElementsByName('input_amount');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = "<%=sCurrFyr%>";
            paramArray.staffno = $('#hidStaffNo').val();
            paramArray.ss_id = <%=sId%>;
            paramArray.ssd_id = $('#hidSsdId').val();
            paramArray.sg_id = $('#hidSgId').val();
            paramArray.ssg_id = $('#hidSsgId').val();

            var inputs = $('.groupsalaryitem');
            for (var i = 0; i < inputs.length; i++) {
                var pl1 = inputs[i].value;
                if (inputs[i].checked == true) {
                    //prepare array input for updating database table
                    var input_salary_item = {};
                    input_salary_item.GetSetcode = input_code[i].value;
                    input_salary_item.GetSetdesc = input_desc[i].value;
                    input_salary_item.GetSetcat = input_cat[i].value;
                    input_salary_item.GetSettype = input_type[i].value;
                    input_salary_item.GetSetitemvalue = input_value[i].value;
                    input_salary_item.GetSetitemgroup = input_group[i].value;
                    input_salary_item.GetSetitemamount = input_amount[i].value;
                    ItemInputArray.push(input_salary_item);

                }
            }

            //call ajax to update database table
            paramArray.inputarray = ItemInputArray;
            var json_string = JSON.stringify(paramArray);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/insertSGItemGajiUpdate",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: insertSGItemGajiUpdate_succeedAjaxFn,
                error: insertSGItemGajiUpdate_failedAjaxFn,
                async: false
            });

        }

        var insertSGItemGajiUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertSGItemGajiUpdate_succeedAjaxFn: " + textStatus);
            var insertSGItemGajiUpdate_result = JSON.parse(data.d);
            if (insertSGItemGajiUpdate_result.result == "Y") {
                alert(insertSGItemGajiUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertSGItemGajiUpdate_result.message);
            }
        }

        var insertSGItemGajiUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertSGItemGajiUpdate_failedAjaxFn: " + textStatus);
        }

        function closeSGItemGajiAdd() {
            resetSGItemGajiDetail();
            $('#SGItemSalaryDetail').modal('hide');
        }

        function resetSGItemGajiDetail() {
            $('#hidId').val("");
            $('#hidSgId').val("");
            $('#txtItemGajiStaffNo').val("");
            $('#txtItemGajiStaffName').val("");
            $('#txtItemGajiPosition').val("");
            $('#txtItemGajiDept').val("");
            //$('#txtItemGajiCat').val("");
            //$('#txtItemGajiType').val("");
            //$('#txtItemGajiDesc').val("");
            emptyItemGaji();
        }

        function formatMoney(amount, decimalCount = 2, decimal = ".", thousands = ",") {
            try {
                decimalCount = Math.abs(decimalCount);
                decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

                const negativeSign = amount < 0 ? "-" : "";

                let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
                let j = (i.length > 3) ? i.length % 3 : 0;

                return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
            } catch (e) {
                console.log(e)
            }
        }
    </script>
</asp:Content>

