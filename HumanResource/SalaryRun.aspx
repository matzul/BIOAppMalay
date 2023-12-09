<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageSalary.master" AutoEventWireup="true" CodeFile="SalaryRun.aspx.cs" Inherits="HumanResource_SalaryRun" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script type="text/javascript">
            function populateButtonAction(status, idno) {
                if (status == "APPROVED" || status == "REJECTED" || status == "CANCELLED") {
                    $('#btnRun_' + idno).hide();
                    $('#btnApprove_' + idno).hide();
                    $('#btnDelete_' + idno).hide();
                } else if (status == "CONFIRMED") {
                    $('#btnRun_' + idno).hide();
                    $('#btnApprove_' + idno).show();
                    $('#btnDelete_' + idno).hide();
                } else if (status == "VERIFIED") {
                    $('#btnRun_' + idno).show();
                    $('#btnApprove_' + idno).hide();
                    $('#btnDelete_' + idno).show();
                } else {
                    $('#btnRun_' + idno).hide();
                    $('#btnApprove_' + idno).hide();
                    $('#btnDelete_' + idno).show();
                }

            }
        </script>
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Pemprosesan Gaji</a></td>
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
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemCat" name="txtFindItemCat" type="text" size="10" maxlength="10" value="<%=sCat%>" class="input">
                        <div id="txtFindItemCat-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Jenis:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindItemType" name="txtFindItemType" type="text" size="50" maxlength="50" value="<%=sType%>" class="input">
                        <div id="txtFindItemType-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Pemprosesan Gaji</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Pemprosesan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bilangan Kakitangan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsRunSalary.Count > 0)
                        {
                            for (int i = 0; i < lsRunSalary.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsRunSalary[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left">Pemperosesan <%=modItem.GetSettype %> bagi Bulan <%=modItem.GetSetmonth %> Tahun <%=modItem.GetSetyear %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetmonth %>-<%=modItem.GetSetyear %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcount %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstatus %></td>
                        <td valign="top" align="center">
                            <button type="button" id="btnRun_<%=modItem.GetSetid %>" class="button_primary enabled" data-action="run">PROSES</button>
                            <button type="button" id="btnApprove_<%=modItem.GetSetid %>" class="button_primary enabled" data-action="approve">LULUS</button>
                            <button type="button" id="btnEdit_<%=modItem.GetSetid %>" class="button_warning enabled" data-action="edit">Edit</button>
                            <button type="button" id="btnDelete_<%=modItem.GetSetid %>" class="button_warning enabled" data-action="delete">Hapus</button>
                            <button type="button" class="button_warning enabled" data-action="itemgaji">Senarai Kakitangan</button>
                        </td>
                    </tr>
                    <script type="text/javascript">
                        populateButtonAction("<%=modItem.GetSetstatus %>","<%=modItem.GetSetid %>");
                    </script>
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
                                <td width="50%" height="15" align="left"><%=lsRunSalary.Count %> record(s)</td>
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
                <input type="hidden" name="hidStatus" id="hidStatus" value="" />
                <input type="hidden" name="hidFyr" id="hidFyr" value="" />
            </div>

            <div class="modal fade" id="SGItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Pemprosesan Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsSalaryYear" name="lsSalaryYear" disabled style="background-color:gray; color:floralwhite;">
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
                                        <td width="20%" class="tblTextCommon">Bulan</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsSalaryMonth" name="lsSalaryMonth">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                                <option value="11">11</option>
                                                <option value="12">12</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemDesc" name="txtItemDesc" size="70" maxlength="70" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsSalaryCat" name="lsSalaryCat">
                                                <option value="">-Select-</option>
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
                                                <option value="CREATED">CREATED</option>
                                                <option value="MODIFIED">MODIFIED</option>
                                                <option value="VERIFIED">VERIFIED</option>
                                                <option value="CONFIRMED">CONFIRMED</option>
                                                <option value="APPROVED">APPROVED</option>
                                                <option value="REJECTED">REJECTED</option>
                                                <option value="CANCELLED">CANCELLED</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblStaffGajiAdd" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
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

            <div class="modal fade" id="SGItemProcess" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGItemProcess" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Penyediaan Pembayaran Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsProcessYear" name="lsProcessYear" disabled style="background-color:gray; color:floralwhite;">
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
                                        <td width="20%" class="tblTextCommon">Bulan</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsProcessMonth" name="lsProcessMonth">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                                <option value="9">9</option>
                                                <option value="10">10</option>
                                                <option value="11">11</option>
                                                <option value="12">12</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtProcessDesc" name="txtProcessDesc" size="70" maxlength="70" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori</td>
                                        <td width="80%" class="tblText2">
                                            <select class="select" id="lsProcessCat" name="lsProcessCat">
                                                <option value="">-Select-</option>
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
                                            <select class="select" id="lsProcessType" name="lsProcessType">
                                                <option value="GAJI">GAJI</option>
                                                <option value="BONUS">BONUS</option>
                                                <option value="ELAUN">ELAUN</option>
                                                <option value="EXGRATIA">EXGRATIA</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblProcessExpensesTo" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblProcessButton" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnSubmitProcess" name="btnSubmitProcess" type="button" class="button1 btn-primary" onclick="submitProcessExpenses();">Teruskan</button>
                                            <button type="button" class="button1" onclick="closeSGItemProcess();">Tutup</button>
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

        var ItemCatArray = [];
        var maxlengthdataautocomplete = 20;
        var ItemTypeArray = [];
        var maxlengthdataautocomplete2 = 20;
        var StaffGajiArray = [];
        var ItemGajiArray = [];
        var paymentList = [];

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindItemCat").value = "";
                document.getElementById("txtFindItemType").value = "";
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
                var itemcat = '';
                var itemtype = '';
                $.each(getSGItemList_result.itemlist, function (i, result) {
                    if (itemcat != result.GetSetcat) {
                        var objData = {};
                        objData.value = result.GetSetcat;
                        objData.data = result.GetSetcat;
                        ItemCatArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemcat = result.GetSetcat;
                    }

                    if (itemtype != result.GetSettype) {
                        var objData = {};
                        objData.value = result.GetSettype;
                        objData.data = result.GetSettype;
                        ItemTypeArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemtype = result.GetSettype;
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

        $('#txtFindItemCat').autocomplete({
            lookup: ItemCatArray,
            appendTo: '#txtFindItemCat-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemCat').val(suggestion.data);
            }
        });

        $('#txtFindItemType').autocomplete({
            lookup: ItemTypeArray,
            appendTo: '#txtFindItemType-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindItemType').val(suggestion.data);
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
                else if (action == 'run') {
                    //alert('run:' + id);
                    //pemprosesan gaji
                    processSGItemDetail(id);
                }
                else if (action == 'approve') {
                    //alert('run:' + id);
                    //kelulusan gaji
                    approveSGItemDetail(id);
                }
            }
        });

        function deleteSGItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $("#lsFindFyr").val(), "id", id];
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

        //------------------------------------------------------------
        function processSGItemDetail(id) {
            if (confirm("Teruskan Pemprosesan?") == true) {
                var processSGStaffGaji_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $("#lsFindFyr").val(), "ss_id", id];
                PageMethod("processSGStaffGaji", processSGStaffGaji_parameters, processSGStaffGaji_succeedAjaxFn, processSGStaffGaji_failedAjaxFn, false);
            }
        }

        var processSGStaffGaji_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("processSGStaffGaji_succeedAjaxFn: " + textStatus);
            var processSGStaffGaji_result = JSON.parse(data.d);
            if (processSGStaffGaji_result.result == "Y") {
                //alert(deleteSGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(processSGStaffGaji_result.message);
            }
        }

        var processSGStaffGaji_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("processSGStaffGaji_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to process Salary Staff...");
        }

        //--------------------------------------------------------------
        function approveSGItemDetail(id) {
            //if (confirm("Teruskan Kelulusan?") == true) {
                var approveSGStaffGaji_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $("#lsFindFyr").val(), "ss_id", id];
                PageMethod("approveSGStaffGaji", approveSGStaffGaji_parameters, approveSGStaffGaji_succeedAjaxFn, approveSGStaffGaji_failedAjaxFn, false);
            //}
        }

        var approveSGStaffGaji_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("approveSGStaffGaji_succeedAjaxFn: " + textStatus);
            var approveSGStaffGaji_result = JSON.parse(data.d);
            if (approveSGStaffGaji_result.result == "Y") {

                openSGItemProcess('OPEN', approveSGStaffGaji_result.id, approveSGStaffGaji_result.paymentlist);
            }
            else {
                alert(approveSGStaffGaji_result.message);
            }
        }

        var approveSGStaffGaji_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("approveSGStaffGaji_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to approve Salary Staff...");
        }

        //------------------------------------------------------------------------------------
        function openSGItemProcess(typ, id, paylist) {

            $('#SGItemProcess').modal({ backdrop: "static" });
            $('#hidId').val(id);
            $('#hidFyr').val("<%=sCurrFyr%>");
            paymentList = paylist;

            $('#tblProcessExpensesTo').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="30%" valign="middle" align="left" class="tblTitle3Mod">Bayaran Kepada</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan/ Jenis</td>' +
                '<td width="30%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                '<td width="15%" valign="middle" align="left" class="tblTitle3Mod">Jumlah Bayaran</td>' +
                '</tr>';

            $('#tblProcessExpensesTo').append(trHTML);

            if (typ == 'OPEN') {
                $('#lsProcessYear').prop('disabled', true);
                $('#lsProcessMonth').prop('disabled', true);
                $('#txtProcessDesc').prop('disabled', true);
                $('#lsProcessCat').prop('disabled', true);
                $('#lsProcessType').prop('disabled', true);

                var getSGItemProcess_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getSGItemDetail", getSGItemProcess_parameters, getSGItemProcess_succeedAjaxFn, getSGItemProcess_failedAjaxFn, false);

            }
        }

        var getSGItemProcess_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemProcess_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#lsProcessYear').val(getSGItemDetail_result.itemdetail.GetSetyear).change();
                $('#lsProcessMonth').val(getSGItemDetail_result.itemdetail.GetSetmonth).change();
                $('#lsProcessCat').val(getSGItemDetail_result.itemdetail.GetSetcat).change();
                $('#lsProcessType').val(getSGItemDetail_result.itemdetail.GetSettype).change();

                $('#txtProcessDesc').val("Pembayaran " + $('#lsProcessType').val() + " bagi Bulan " + $('#lsProcessMonth').val() + " Tahun " + $('#lsProcessYear').val());

                //get list pembayaran to
                if (paymentList.length > 0) {
                    var trHTML = "";

                    $.each(paymentList, function (i, result) {

                        trHTML += '<tr data-id="' + result.id + '"><td valign="top"><font>' + (i+1) + '</font></td>' +
                            '<td valign="top"><font>' + result.bpid + '<br />' + result.bpdesc + '<br />' + result.bpaddress + '</font></td>' +
                            '<td align="left" valign="top"><font>' + result.exp_paytype + '</font></td>' +
                            '<td align="left" valign="top"><font>' + result.exp_type + '<br />' + result.exp_itemdesc + '</font></td>' +
                            '<td align="left" valign="top"><font>' + result.exp_remarks + '</font></td>' +
                            '<td align="left" valign="top"><font>' + result.exp_amount + '</font></td>' +
                            '</tr>';
                    });
                    $('#tblProcessExpensesTo').append(trHTML);
                }

            }
            else {
                alert(getSGItemDetail_result.message);
            }
        }

        var getSGItemProcess_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemProcess_failedAjaxFn: " + textStatus);
        }

        function closeSGItemProcess() {
            resetSGItemProcess();
            $('#SGItemProcess').modal('hide');
            actionclick('SEARCH');
        }

        function resetSGItemProcess() {
            $('#hidId').val("");
            $('#txtProcessDesc').val("");
            $('#lsProcessCat').val("").change();
            $('#lsProcessType').val("GAJI").change();

        }

        //------------------------------------------------------------------------------------

        function submitProcessExpenses() {
            if (confirm("Teruskan Penyediaan Pembayaran?") == true) {
                //var paymentSGStaffGaji_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $("#lsFindFyr").val(), "ss_id", $('#hidId').val(), "paymentlist", paymentList];
                var paramlist = {};
                paramlist.currcomp = "<%=sCurrComp%>";
                paramlist.currfyr = $("#lsFindFyr").val();
                paramlist.ss_id = $('#hidId').val();
                paramlist.paymentlist = paymentList;
                var json_string = JSON.stringify(paramlist);
                PageMethod2("paymentSGStaffGaji", json_string, paymentSGStaffGaji_succeedAjaxFn, paymentSGStaffGaji_failedAjaxFn, false);
            }
        }

        var paymentSGStaffGaji_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("paymentSGStaffGaji_succeedAjaxFn: " + textStatus);
            var paymentSGStaffGaji_result = JSON.parse(data.d);
            if (paymentSGStaffGaji_result.result == "Y") {
                alert(paymentSGStaffGaji_result.message);
                closeSGItemProcess();
            }
            else {
                alert(paymentSGStaffGaji_result.message);
            }
        }

        var paymentSGStaffGaji_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("paymentSGStaffGaji_failedAjaxFn: " + textStatus);
        }

        //------------------------------------------------------------------------------------
        function openSGItemDetail(typ, id) {
            $('#SGItemDetail').modal({ backdrop: "static" });
            $('#hidId').val(id);
            $('#hidFyr').val("<%=sCurrFyr%>");

            $('#tblStaffGajiAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="50%" valign="middle" align="left" class="tblTitle3Mod">Kakitangan</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="22%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '</tr>';

            $('#tblStaffGajiAdd').append(trHTML);

            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();
                $('#lsSalaryCat').prop('disabled', true);
                $('#lsSalaryType').prop('disabled', true);

                var getSGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getSGItemDetail", getSGItemDetail_parameters, getSGItemDetail_succeedAjaxFn, getSGItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
                populateDesc();
            }
        }

        var getSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemDetail_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#lsSalaryYear').val(getSGItemDetail_result.itemdetail.GetSetyear).change();
                $('#lsSalaryMonth').val(getSGItemDetail_result.itemdetail.GetSetmonth).change();
                $('#lsSalaryCat').val(getSGItemDetail_result.itemdetail.GetSetcat).change();
                $('#lsSalaryType').val(getSGItemDetail_result.itemdetail.GetSettype).change();
                $('#txtSalaryCount').val(getSGItemDetail_result.itemdetail.GetSetcount);
                $('#lsStatus').val(getSGItemDetail_result.itemdetail.GetSetstatus).change();

                populateDesc();
                var getSGStaffGajiList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsSalaryYear').val(), "sg_id", 0, "salary_cat", $("#lsSalaryCat").val(), "salary_type", $("#lsSalaryType").val()];
                PageMethod("getSGStaffGajiList", getSGStaffGajiList_parameters, getSGStaffGajiList_succeedAjaxFn, getSGStaffGajiList_failedAjaxFn, false);
            }
            else {
                alert(getSGItemDetail_result.message);
            }
        }

        var getSGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemDetail_failedAjaxFn: " + textStatus);
        }

        $("#lsSalaryYear").change(function () {
            populateDesc();
        });

        $("#lsSalaryMonth").change(function () {
            populateDesc();
        });

        $("#lsSalaryType").change(function () {
            populateDesc();
            var getSGStaffGajiList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsSalaryYear').val(), "sg_id", 0, "salary_cat", $("#lsSalaryCat").val(), "salary_type", $("#lsSalaryType").val()];
            PageMethod("getSGStaffGajiList", getSGStaffGajiList_parameters, getSGStaffGajiList_succeedAjaxFn, getSGStaffGajiList_failedAjaxFn, false);
        });

        $("#lsSalaryCat").change(function () {
            populateDesc();
            var getSGStaffGajiList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsSalaryYear').val(), "sg_id", 0, "salary_cat", $("#lsSalaryCat").val(), "salary_type", $("#lsSalaryType").val()];
            PageMethod("getSGStaffGajiList", getSGStaffGajiList_parameters, getSGStaffGajiList_succeedAjaxFn, getSGStaffGajiList_failedAjaxFn, false);
        });

        var getSGStaffGajiList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGStaffGajiList_succeedAjaxFn: " + textStatus);
            $('#tblStaffGajiAdd').empty();
            var getSGStaffGajiList_result = JSON.parse(data.d);
            if (getSGStaffGajiList_result.result == "Y") {
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="50%" valign="middle" align="left" class="tblTitle3Mod">Kakitangan</td>' +
                    '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                    '<td width="22%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                    '</tr>';
                $.each(getSGStaffGajiList_result.itemlist, function (i, result) {

                    trHTML += '<tr data-id="' + result.GetSetstaffno + '-' + result.GetSetid + '"><td valign="top"><input type="checkbox" name="chk_id" class="salarystaff" value="' + result.GetSetstaffno + '-' + result.GetSetid +'"></td>' +
                        '<td align="left" valign="top"><font>' + result.GetSetname + '</font><input type="hidden" name="input_staffno" value="' + result.GetSetstaffno + '" /><br/>' +
                        '<font>' + result.GetSetpos_name + ' [' + result.GetSetgred_name + ']</font><input type="hidden" name="input_sgid" value="' + result.GetSetsg_id + '" /><br/>' +
                        '<font>' + result.GetSetdept_name + '</font><input type="hidden" name="input_ssgid" value="' + result.GetSetid + '" /></td>' +
                        '<td align="left" valign="top"><font>' + result.GetSetcat + '</font><input type="hidden" name="input_cat" value="' + result.GetSetcat + '" class="input"/></td>' +
                        '<td align="left" valign="top"><font>' + result.GetSettype + '</font><input type="hidden" name="input_type" value="' + result.GetSettype + '" class="input"/></td>' +
                        '</tr>';
                });

                $('#tblStaffGajiAdd').append(trHTML);

                if ($('#hidId').val() != "0" && $('#hidId').val() != "") {
                    var getSGStaffGajiChecked_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#hidFyr').val(), "ss_id", $('#hidId').val()];
                    PageMethod("getSGStaffGajiChecked", getSGStaffGajiChecked_parameters, getSGStaffGajiChecked_succeedAjaxFn, getSGStaffGajiChecked_failedAjaxFn, false);
                }
            }
            else {
                console.log("getSGStaffGajiList_result.result: " + getSGItemList_result.result);
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="50%" valign="middle" align="left" class="tblTitle3Mod">Kakitangan</td>' +
                    '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                    '<td width="22%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                    '</tr>';

                $('#tblStaffGajiAdd').append(trHTML);

            }
        }

        var getSGStaffGajiList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGStaffGajiList_failedAjaxFn: " + textStatus);

            $('#tblStaffGajiAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="3%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="50%" valign="middle" align="left" class="tblTitle3Mod">Kakitangan</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="22%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '</tr>';

            $('#tblStaffGajiAdd').append(trHTML);
        }

        var getSGStaffGajiChecked_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGStaffGajiChecked_succeedAjaxFn: " + textStatus);
            var getSGStaffGajiChecked_result = JSON.parse(data.d);
            if (getSGStaffGajiChecked_result.result == "Y") {
                StaffGajiArray = [];
                $.each(getSGStaffGajiChecked_result.itemlist, function (i, result) {
                    StaffGajiArray.push(result.GetSetstaffno.toString() + '-' + result.GetSetssg_id.toString());
                });

                var inputs = $('.salarystaff');
                for (var i = 0; i < inputs.length; i++) {
                    var pl1 = inputs[i].value;
                    if (StaffGajiArray.includes(pl1)) {
                        inputs[i].checked = true;
                    }
                    else {
                        inputs[i].checked = false;
                    }
                }
            }
            else {
                console.log("getSGStaffGajiChecked_result.result: " + getSGStaffGajiChecked_result.result);
            }
        }

        var getSGStaffGajiChecked_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGStaffGajiChecked_failedAjaxFn: " + textStatus);
        }

        function populateDesc() {
            $('#txtItemDesc').val("Pemperosesan " + $('#lsSalaryType').val() + " bagi Bulan " + $('#lsSalaryMonth').val() + " Tahun " + $('#lsSalaryYear').val());
        }

        function insertSGItemDetail() {

            var paramArray = {};
            var ItemInputArray = [];

            var input_staffno = document.getElementsByName('input_staffno');
            var input_sgid = document.getElementsByName('input_sgid');
            var input_ssgid = document.getElementsByName('input_ssgid');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = $('#hidFyr').val();
            paramArray.run_cat = $('#lsSalaryCat').val();
            paramArray.run_type = $('#lsSalaryType').val();
            paramArray.run_count = $('#txtSalaryCount').val();
            paramArray.run_month = $('#lsSalaryMonth').val();
            paramArray.run_year = $('#lsSalaryYear').val();

            var inputs = $('.salarystaff');
            for (var i = 0; i < inputs.length; i++) {
                var pl1 = inputs[i].value;
                if (inputs[i].checked == true) {
                    //prepare array input for updating database table
                    var input_salary_item = {};
                    input_salary_item.GetSetstaffno = input_staffno[i].value;
                    input_salary_item.GetSetsg_id = input_sgid[i].value;
                    input_salary_item.GetSetssg_id = input_ssgid[i].value;
                    input_salary_item.GetSetcat = input_cat[i].value;
                    input_salary_item.GetSettype = input_type[i].value;
                    ItemInputArray.push(input_salary_item);

                }
            }

            //call ajax to update database table
            paramArray.inputarray = ItemInputArray;
            var json_string = JSON.stringify(paramArray);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/insertSGItemDetail",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: insertSGItemDetail_succeedAjaxFn,
                error: insertSGItemDetail_failedAjaxFn,
                async: false
            });

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

            var paramArray = {};
            var ItemInputArray = [];

            var input_staffno = document.getElementsByName('input_staffno');
            var input_sgid = document.getElementsByName('input_sgid');
            var input_ssgid = document.getElementsByName('input_ssgid');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = $('#hidFyr').val();
            paramArray.ss_id = $('#hidId').val();
            paramArray.run_month = $('#lsSalaryMonth').val();
            paramArray.status = $('#lsStatus').val();

            var inputs = $('.salarystaff');
            for (var i = 0; i < inputs.length; i++) {
                var pl1 = inputs[i].value;
                if (inputs[i].checked == true) {
                    //prepare array input for updating database table
                    var input_salary_item = {};
                    input_salary_item.GetSetstaffno = input_staffno[i].value;
                    input_salary_item.GetSetsg_id = input_sgid[i].value;
                    input_salary_item.GetSetssg_id = input_ssgid[i].value;
                    input_salary_item.GetSetcat = input_cat[i].value;
                    input_salary_item.GetSettype = input_type[i].value;
                    ItemInputArray.push(input_salary_item);

                }
            }

            //call ajax to update database table
            paramArray.inputarray = ItemInputArray;
            var json_string = JSON.stringify(paramArray);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/updateSGItemDetail",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: updateSGItemDetail_succeedAjaxFn,
                error: updateSGItemDetail_failedAjaxFn,
                async: false
            });
        }

        var updateSGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateSGItemDetail_succeedAjaxFn: " + textStatus);
            var updateSGItemDetail_result = JSON.parse(data.d);
            if (updateSGItemDetail_result.result == "Y") {
                alert(updateSGItemDetail_result.message);
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
            $('#txtItemDesc').val("");
            $('#lsSalaryCat').val("").change();
            $('#lsSalaryType').val("GAJI").change();
            $('#txtSalaryCount').val("0");
            $('#lsStatus').val("CREATED").change();
            $('#lsSalaryCat').prop('disabled', false);
            $('#lsSalaryType').prop('disabled', false);

        }

        function openSGItemSalaryDetail(typ, id) {
            if (typ == 'OPEN') {                
                fOpenWindow('SalaryStaffRun.aspx?fyr=' + $('#lsFindFyr').val() + '&id=' + id);
            }
        }

        function getCurrentDate() {
            var todaydate = "";

            var today = new Date();
            var selyear = today.getFullYear();
            var selmonth = today.getMonth() + 1;
            if (selmonth < 10) {
                selmonth = '0' + selmonth;
            }
            var selday = today.getDate();
            if (selday < 10) {
                selday = '0' + selday;
            }
            var seltime = (today.getHours() < 10 ? '0' + today.getHours() : today.getHours()) + ':' + (today.getMinutes() < 10 ? '0' + today.getMinutes() : today.getMinutes()) + ':' + (today.getSeconds() < 10 ? '0' + today.getSeconds() : today.getSeconds());

            todaydate = selday + '-' + selmonth + '-' + selyear + " " + seltime;
            //todaydate = selday + '-' + selmonth + '-' + selyear;

            return todaydate;
        }
    </script>
</asp:Content>

