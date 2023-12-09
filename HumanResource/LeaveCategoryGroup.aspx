<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageLeave.master" AutoEventWireup="true" CodeFile="LeaveCategoryGroup.aspx.cs" Inherits="HumanResource_LeaveCategoryGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kategori & Jenis Cuti</a></td>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kategori & Jenis Cuti</a></td>
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
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Jumlah Cuti</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsLeaveGroup.Count > 0)
                        {
                            for (int i = 0; i < lsLeaveGroup.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsLeaveGroup[i];
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
                                <td width="50%" height="15" align="left"><%=lsLeaveGroup.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openLGItemDetail('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
            </div>

            <div class="modal fade" id="LGItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbLGItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kategori & Jenis Cuti</td>
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
                                            <select class="select" id="lsLeaveCat" name="lsLeaveCat">
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
                                            <select class="select" id="lsLeaveType" name="lsLeaveType">
                                                <option value="CUTI TAHUNAN">CUTI TAHUNAN</option>
                                                <option value="CUTI KECEMASAN">CUTI KECEMASAN</option>
                                                <option value="CUTI SAKIT (MC)">CUTI SAKIT (MC)</option>
                                                <option value="CUTI GANTI">CUTI GANTI</option>
                                                <option value="CUTI TANPA REKOD">CUTI TANPA REKOD</option>
                                                <option value="CUTI KUARANTIN">CUTI KUARANTIN</option>
                                                <option value="CUTI HAJI">CUTI HAJI</option>
                                                <option value="CUTI UMRAH">CUTI UMRAH</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jumlah Cuti</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtLeaveCount" name="txtLeaveCount" type="text" size="10" maxlength="10" value="0" class="input">
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
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertLGItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateLGItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeLGItemDetail();">Tutup</button>
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

            var getLGItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getLGItemList", getLGItemList_parameters, getLGItemList_succeedAjaxFn, getLGItemList_failedAjaxFn, false);

        });

        var getLGItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getLGItemList_succeedAjaxFn: " + textStatus);
            var getLGItemList_result = JSON.parse(data.d);
            if (getLGItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getLGItemList_result.itemlist, function (i, result) {
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
                console.log("getLGItemList_result.result: " + getLGItemList_result.result);
            }
        }

        var getLGItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getLGItemList_failedAjaxFn: " + textStatus);
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
                    deleteLGItemDetail(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openLGItemDetail('OPEN', id);
                }
            }
        });

        function deleteLGItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteLGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteLGItemDetail", deleteLGItemDetail_parameters, deleteLGItemDetail_succeedAjaxFn, deleteLGItemDetail_failedAjaxFn, false);
            }
        }

        var deleteLGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteLGItemDetail_succeedAjaxFn: " + textStatus);
            var deleteLGItemDetail_result = JSON.parse(data.d);
            if (deleteLGItemDetail_result.result == "Y") {
                //alert(deleteLGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteLGItemDetail_result.message);
            }
        }

        var deleteLGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteLGItemDetail_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Leave Details...");
        }

        function openLGItemDetail(typ, id) {
            $('#LGItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getLGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getLGItemDetail", getLGItemDetail_parameters, getLGItemDetail_succeedAjaxFn, getLGItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getLGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getLGItemDetail_succeedAjaxFn: " + textStatus);
            var getLGItemDetail_result = JSON.parse(data.d);
            if (getLGItemDetail_result.result == "Y") {
                $('#hidId').val(getLGItemDetail_result.itemdetail.GetSetid);
                $('#txtItemCode').val(getLGItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemDesc').val(getLGItemDetail_result.itemdetail.GetSetdesc);
                $('#lsLeaveCat').val(getLGItemDetail_result.itemdetail.GetSetcat);
                $('#lsLeaveType').val(getLGItemDetail_result.itemdetail.GetSettype);
                $('#txtLeaveCount').val(getLGItemDetail_result.itemdetail.GetSetcount);
                $('#lsStatus').val(getLGItemDetail_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getLGItemDetail_result.message);
            }
        }

        var getLGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getLGItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertLGItemDetail() {
            var insertLGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "leave_cat", $('#lsLeaveCat').val(), "leave_type", $('#lsLeaveType').val(), "count", $('#txtLeaveCount').val(), "status", $('#lsStatus').val()];
            PageMethod("insertLGItemDetail", insertLGItemDetail_parameters, insertLGItemDetail_succeedAjaxFn, insertLGItemDetail_failedAjaxFn, false);
        }

        var insertLGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertLGItemDetail_succeedAjaxFn: " + textStatus);
            var insertLGItemDetail_result = JSON.parse(data.d);
            if (insertLGItemDetail_result.result == "Y") {
                //alert(insertLGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertLGItemDetail_result.message);
            }
        }

        var insertLGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertLGItemDetail_failedAjaxFn: " + textStatus);
        }

        function updateLGItemDetail() {
            var updateLGItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val(), "code", $('#txtItemCode').val(), "desc", $('#txtItemDesc').val(), "leave_cat", $('#lsLeaveCat').val(), "leave_type", $('#lsLeaveType').val(), "count", $('#txtLeaveCount').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateLGItemDetail", updateLGItemDetail_parameters, updateLGItemDetail_succeedAjaxFn, updateLGItemDetail_failedAjaxFn, false);
        }

        var updateLGItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateLGItemDetail_succeedAjaxFn: " + textStatus);
            var updateLGItemDetail_result = JSON.parse(data.d);
            if (updateLGItemDetail_result.result == "Y") {
                //alert(updateLGItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateLGItemDetail_result.message);
            }
        }

        var updateLGItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateLGItemDetail_failedAjaxFn: " + textStatus);
        }

        function closeLGItemDetail() {
            resetLGItemDetail();
            $('#LGItemDetail').modal('hide');
        }

        function resetLGItemDetail() {
            $('#hidId').val("");
            $('#txtItemCode').val("");
            $('#txtItemDesc').val("");
            $('#lsLeaveCat').val("");
            $('#lsLeaveType').val("");
            $('#txtLeaveCount').val("0");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

