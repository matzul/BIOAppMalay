<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageSalary.master" AutoEventWireup="true" CodeFile="SalaryGroup.aspx.cs" Inherits="HumanResource_SalaryGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kumpulan Gaji</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Tahun:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindFyr" name="lsFindFyr">
                            <option value="2019" <%=sCurrFyr.Equals("2019") ? "selected" : "" %>>2019</option>
                            <option value="2020" <%=sCurrFyr.Equals("2020") ? "selected" : "" %>>2020</option>
                            <option value="2021" <%=sCurrFyr.Equals("2021") ? "selected" : "" %>>2021</option>
                            <option value="2022" <%=sCurrFyr.Equals("2022") ? "selected" : "" %>>2022</option>
                            <option value="2023" <%=sCurrFyr.Equals("2023") ? "selected" : "" %>>2023</option>
                            <option value="2024" <%=sCurrFyr.Equals("2024") ? "selected" : "" %>>2024</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kod:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemCode" name="txtFindItemCode" type="text" size="10" maxlength="10" value="<%=sCode%>" class="input">
                        <div id="txtFindItemCode-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Keterangan:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemDesc" name="txtFindItemDesc" type="text" size="50" maxlength="50" value="<%=sDesc%>" class="input">
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kumpulan Gaji</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>
                        <td width="11%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bilangan Kakitangan</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsSalaryGroup.Count > 0)
                        {
                            for (int i = 0; i < lsSalaryGroup.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsSalaryGroup[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcode %></td>
                        <td valign="top" align="left"><%=modItem.GetSetdesc %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcount %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" class="button_warning enabled" data-action="delete">Hapus</button>
                            <button type="button" class="button_warning enabled" data-action="itemgaji">Item Gaji</button>
                        </td>
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
                                <td width="50%" height="15" align="left"><%=lsSalaryGroup.Count %> record(s)</td>
                                <td width="50%" align="right">page
                                    <asp:DropDownList CssClass="select" ID="lsPageList" runat="server" OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    of <%=sTotalPage %></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton1" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center">
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openSGItemDetail('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="SGItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kumpulan Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kod</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemCode" name="txtItemCode" type="text" size="10" maxlength="10" value="" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemDesc" name="txtItemDesc" size="50" maxlength="50" value="" class="input" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsSalaryCat" name="lsSalaryCat">
                                                <%
                                                    for (int i = 0; i < lsGredComp.Count; i++)
                                                    {
                                                        HRModel modItem = (HRModel)lsGredComp[i];
                                                %>
                                                <option value="<%=modItem.GetSetname %>"><%=modItem.GetSetname %></option>
                                                <%
                                                    }
                                                %>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsSalaryType" name="lsSalaryType">
                                                <option value="GAJI">GAJI</option>
                                                <option value="BONUS">BONUS</option>
                                                <option value="ELAUN">ELAUN</option>
                                                <option value="EXGRATIA">EXGRATIA</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Bilangan Kakitangan</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtSalaryCount" name="txtSalaryCount" type="text" size="4" maxlength="6" value="0" class="input" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatus" name="lsStatus" class="select">
                                                <option value="ACTIVE">ACTIVE</option>
                                                <option value="IN-ACTIVE">IN-ACTIVE</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertSGItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateSGItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeSGItemDetail();">Tutup</button>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="SGItemSalaryDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGItemGajiDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kumpulan & Item Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kod</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemGajiCode" name="txtItemGajiCode" type="text" size="10" maxlength="10" value="" class="input" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiDesc" name="txtItemGajiDesc" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiCat" name="txtItemGajiCat" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiType" name="txtItemGajiType" size="20" maxlength="20" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Bilangan Kakitangan</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemGajiCount" name="txtItemGajiCount" type="text" size="4" maxlength="6" value="0" class="input" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiStatus" name="txtItemGajiStatus" size="20" maxlength="20" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblItemGajiAdd" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonAdd" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnSaveItemGaji" name="btnSaveItemGaji" type="button" class="button1 btn-primary" onclick="insertSGItemGajiUpdate();">Simpan</button>
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

        var ItemCodeArray = [];
        var maxlengthdataautocomplete = 20;
        var ItemDescArray = [];
        var maxlengthdataautocomplete2 = 20;
        var ItemCatArray = [];
        var maxlengthdataautocomplete3 = 20;
        var ItemGajiArray = [];

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindItemCode").value = "";
                document.getElementById("txtFindItemDesc").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getSGItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getSGItemList", getSGItemList_parameters, getSGItemList_succeedAjaxFn, getSGItemList_failedAjaxFn, false);

        });

        var getSGItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemList_succeedAjaxFn: " + textStatus);
            var getSGItemList_result = JSON.parse(data.d);
            if (getSGItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getSGItemList_result.itemlist, function (i, result) {
                    if (itemno != result.GetSetcode + '-' + result.GetSetdesc) {
                        var objData = {};
                        objData.value = result.GetSetcode + '-' + result.GetSetdesc;
                        objData.data = result.GetSetcode;
                        ItemCodeArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemno = result.GetSetcode + '-' + result.GetSetdesc;
                    }

                    if (itemdesc != result.GetSetdesc) {
                        var objData = {};
                        objData.value = result.GetSetdesc;
                        objData.data = result.GetSetdesc;
                        ItemDescArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemdesc = result.GetSetdesc;
                    }

                });
            }
            else {
                console.log("getSGItemList_result.result: " + getSGItemList_result.result);
            }
        }

        var getSGItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindItemCode').autocomplete({
            lookup: ItemCodeArray,
            appendTo: '#txtFindItemCode-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemCode').val(suggestion.data);
            }
        });

        $('#txtFindItemDesc').autocomplete({
            lookup: ItemDescArray,
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
                    deleteSGItemDetail(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openSGItemDetail('OPEN', id);
                }
                else if (action == 'itemgaji') {
                    //alert('edit:' + id);
                    openSGItemSalaryDetail('OPEN', id);
                }
            }
        });

        function deleteSGItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteSGItemDetail", deleteSGItemDetail_parameters, deleteSGItemDetail_succeedAjaxFn, deleteSGItemDetail_failedAjaxFn, false);
            }
        }

        var deleteSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteSGItemDetail_succeedAjaxFn: " + textStatus);
            var deleteSGItemDetail_result = JSON.parse(data.d);
            if (deleteSGItemDetail_result.result == "Y") {
                //alert(deleteSGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteSGItemDetail_result.message);
            }
        }

        var deleteSGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteSGItemDetail_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Salary Details...");
        }

        function openSGItemDetail(typ, id) {
            $('#SGItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getSGItemDetail", getSGItemDetail_parameters, getSGItemDetail_succeedAjaxFn, getSGItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemDetail_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#txtItemCode').val(getSGItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemDesc').val(getSGItemDetail_result.itemdetail.GetSetdesc);
                $('#lsSalaryCat').val(getSGItemDetail_result.itemdetail.GetSetcat);
                $('#lsSalaryType').val(getSGItemDetail_result.itemdetail.GetSettype);
                $('#txtSalaryCount').val(getSGItemDetail_result.itemdetail.GetSetcount);
                $('#lsStatus').val(getSGItemDetail_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getSGItemDetail_result.message);
            }
        }

        var getSGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertSGItemDetail() {
            var insertSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "salary_cat", $('#lsSalaryCat').val(), "salary_type", $('#lsSalaryType').val(), "count", $('#txtSalaryCount').val(), "status", $('#lsStatus').val()];
            PageMethod("insertSGItemDetail", insertSGItemDetail_parameters, insertSGItemDetail_succeedAjaxFn, insertSGItemDetail_failedAjaxFn, false);
        }

        var insertSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertSGItemDetail_succeedAjaxFn: " + textStatus);
            var insertSGItemDetail_result = JSON.parse(data.d);
            if (insertSGItemDetail_result.result == "Y") {
                //alert(insertSGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertSGItemDetail_result.message);
            }
        }

        var insertSGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertSGItemDetail_failedAjaxFn: " + textStatus);
        }

        function updateSGItemDetail() {
            var updateSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "salary_cat", $('#lsSalaryCat').val(), "salary_type", $('#lsSalaryType').val(), "count", $('#txtSalaryCount').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateSGItemDetail", updateSGItemDetail_parameters, updateSGItemDetail_succeedAjaxFn, updateSGItemDetail_failedAjaxFn, false);
        }

        var updateSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateSGItemDetail_succeedAjaxFn: " + textStatus);
            var updateSGItemDetail_result = JSON.parse(data.d);
            if (updateSGItemDetail_result.result == "Y") {
                //alert(updateSGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateSGItemDetail_result.message);
            }
        }

        var updateSGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateSGItemDetail_failedAjaxFn: " + textStatus);
        }

        function closeSGItemDetail() {
            resetSGItemDetail();
            $('#SGItemDetail').modal('hide');
        }

        function resetSGItemDetail() {
            $('#hidId').val("");
            $('#txtItemCode').val("");
            $('#txtItemDesc').val("");
            $('#lsSalaryCat').val("");
            $('#lsSalaryType').val("");
            $('#txtSalaryCount').val("0");
            $('#lsStatus').val("");
        }

        function openSGItemSalaryDetail(typ, id) {
            if (typ == 'OPEN') {
                $('#SGItemSalaryDetail').modal({ backdrop: "static" });

                var getSGItemGajiDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getSGItemDetail", getSGItemGajiDetail_parameters, getSGItemGajiDetail_succeedAjaxFn, getSGItemGajiDetail_failedAjaxFn, false);

            }
        }

        var getSGItemGajiDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            $('#tblItemGajiAdd').empty();
            console.log("getSGItemGajiDetail_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#txtItemGajiCode').val(getSGItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemGajiDesc').val(getSGItemDetail_result.itemdetail.GetSetdesc);
                $('#txtItemGajiCat').val(getSGItemDetail_result.itemdetail.GetSetcat);
                $('#txtItemGajiType').val(getSGItemDetail_result.itemdetail.GetSettype);
                $('#txtItemGajiCount').val(getSGItemDetail_result.itemdetail.GetSetcount);
                $('#txtItemGajiStatus').val(getSGItemDetail_result.itemdetail.GetSetstatus);

                //populate item gaji
                populateItemGaji();

                var getSGItemGajiChecked_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", getSGItemDetail_result.itemdetail.GetSetfyr, "sg_id", getSGItemDetail_result.itemdetail.GetSetid];
                PageMethod("getSGItemGajiChecked", getSGItemGajiChecked_parameters, getSGItemGajiChecked_succeedAjaxFn, getSGItemGajiChecked_failedAjaxFn, false);

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

        var getSGItemGajiChecked_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemGajiChecked_succeedAjaxFn: " + textStatus);
            var getSGItemGajiChecked_result = JSON.parse(data.d);
            if (getSGItemGajiChecked_result.result == "Y") {
                ItemGajiArray = [];
                $.each(getSGItemGajiChecked_result.itemlist, function (i, result) {
                    ItemGajiArray.push(result.GetSetsi_id.toString());
                });

                var inputs = $('.itemsalary');
                for (var i = 0; i < inputs.length; i++) {
                    var pl1 = inputs[i].value;
                    if (ItemGajiArray.includes(pl1)) {
                        inputs[i].checked = true;
                    }
                    else {
                        inputs[i].checked = false;
                    }
                }
            }
            else {
                console.log("getSGItemList_result.result: " + getSGItemList_result.result);
            }
        }

        var getSGItemGajiChecked_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemGajiChecked_failedAjaxFn: " + textStatus);
        }

        function closeSGItemGajiAdd() {
            resetSGItemGajiDetail();
            $('#SGItemSalaryDetail').modal('hide');
        }

        function resetSGItemGajiDetail() {
            $('#hidId').val("");
            $('#txtItemGajiCode').val("");
            $('#txtItemGajiDesc').val("");
            $('#txtItemGajiCat').val("");
            $('#txtItemGajiType').val("");
            $('#txtItemGajiCount').val("");
            $('#txtItemGajiStatus').val("");
            emptyItemGaji();
        }

        function populateItemGaji() {
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>' +
                '<td width="30%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan Item</td>' +
                '</tr>';
            <%
                if (lsSalaryItem.Count > 0)
                {
                    for (int i = 0; i < lsSalaryItem.Count; i++)
                    {
                        HRModel modAcc = (HRModel)lsSalaryItem[i];

            %>
            trHTML += '<tr><td valign="top"><input type="checkbox" name="chk_id" class="itemsalary" value="<%=modAcc.GetSetid %>"></td>' +
                '<td align="left" valign="top"><font><%=modAcc.GetSetcode%></font><input type="hidden" name="input_code" value="<%=modAcc.GetSetcode%>" /></td>' +
                '<td align="left" valign="top"><font><%=modAcc.GetSetdesc%></font><input type="hidden" name="input_desc" value="<%=modAcc.GetSetdesc%>" /></td>' +
                '<td align="left" valign="top"><font><%=modAcc.GetSetcat%></font><input type="hidden" name="input_cat" value="<%=modAcc.GetSetcat%>" class="input"/></td>' +
                '<td align="left" valign="top"><font><%=modAcc.GetSettype%></font><input type="hidden" name="input_type" value="<%=modAcc.GetSettype%>" class="input"/></td>' +
                '<td align="left" valign="top"><input type="input" name="input_count" size="5" maxlength="5" value="<%=modAcc.GetSetitemvalue%>" class="input" /></td>' +
                '<td align="left" valign="top"><font><%=modAcc.GetSetitemgroup%></font><input type="hidden" name="input_group" value="<%=modAcc.GetSetitemgroup%>" class="input"/></td>' +
                '</tr>';
            <%
                    }
                }
            %>
            $('#tblItemGajiAdd').append(trHTML);

        }

        function emptyItemGaji() {
            $('#tblItemGajiAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>' +
                '<td width="30%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nilai/Jumlah</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan Item</td>' +
                '</tr>';
            $('#tblItemGajiAdd').append(trHTML);
        }

        function insertSGItemGajiUpdate() {
            var paramArray = {};
            var ItemInputArray = [];

            //construct input array
            var input_code = document.getElementsByName('input_code');
            var input_desc = document.getElementsByName('input_desc');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');
            var input_count = document.getElementsByName('input_count');
            var input_group = document.getElementsByName('input_group');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = $('#lsFindFyr').val();
            paramArray.currsgid = $('#hidId').val();

            var inputs = $('.itemsalary');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked == true) {
                    var input_salary_item = {};
                    input_salary_item.GetSetid = inputs[i].value;
                    input_salary_item.GetSetcode = input_code[i].value;
                    input_salary_item.GetSetdesc = input_desc[i].value;
                    input_salary_item.GetSetcat = input_cat[i].value;
                    input_salary_item.GetSettype = input_type[i].value;
                    input_salary_item.GetSetitemvalue = input_count[i].value;
                    input_salary_item.GetSetitemgroup = input_group[i].value;
                    ItemInputArray.push(input_salary_item);
                }
            }

            paramArray.inputarray = ItemInputArray;
            var json_string = JSON.stringify(paramArray);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/insertSGItemUpdate",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: insertSGItemUpdate_succeedAjaxFn,
                error: insertSGItemUpdate_failedAjaxFn,
                async: false
            });

        }

        var insertSGItemUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertSGItemUpdate_succeedAjaxFn: " + textStatus);
            var insertSGItemUpdate_result = JSON.parse(data.d);
            if (insertSGItemUpdate_result.result == "Y") {
                alert(insertSGItemUpdate_result.message);
            }
            else {
                alert(insertSGItemUpdate_result.message);
            }
        }

        var insertSGItemUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertSGItemUpdate_failedAjaxFn: " + textStatus);
        }

    </script>
</asp:Content>

