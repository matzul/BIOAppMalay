<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageSalary.master" AutoEventWireup="true" CodeFile="SalaryItem.aspx.cs" Inherits="HumanResource_SalaryItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Item Gaji, Manfaat, Tolakan & Caruman</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
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
                <tr>
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindCat" name="lsFindCat">
                            <option value="">-Select-</option>
                            <%
                                for(int i=0; i<lsSalaryItemCat.Count; i++)
                                {
                                    Object objData = (Object)lsSalaryItemCat[i];
                                    System.Reflection.PropertyInfo pi = objData.GetType().GetProperty("GetSettype");
                                    String sItem = (String)(pi.GetValue(objData, null));
                            %>
                                    <option value="<%=sItem%>" <%=sCat.Equals(sItem) ? "selected" : "" %>><%=sItem%></option>
                            <%
                                }
                            %>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Item Gaji, Manfaat, Tolakan & Caruman</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>
                        <td width="30%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jumlah/Nilai</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsSalaryItem.Count > 0)
                        {
                            for (int i = 0; i < lsSalaryItem.Count; i++)
                            {
                                HRModel modAcc = (HRModel)lsSalaryItem[i];
                    %>
                    <tr class="tblText1" data-id="<%=modAcc.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcode %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdesc %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcat %></td>
                        <td valign="top" align="left"><%=modAcc.GetSettype %></td>
                        <td valign="top" align="right"><%=modAcc.GetSetitemvalue %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetstatus %></td>
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
                        <td colspan="7" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsSalaryItem.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openSalaryItemDetail('ADD',0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
                <input type="hidden" name="hidItemGroup" id="hidItemGroup" value="" />
            </div>

            <div class="modal fade" id="SalaryItemDetail" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSalaryItemDetail" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Item Gaji, Manfaat, Tolakan & Caruman</td>
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
                                            <select id="lsItemCat" name="lsItemCat" class="select">
                                                <option value="">-Select-</option>
                                                <%
                                                    for(int i=0; i<lsSalaryItemCat.Count; i++)
                                                    {
                                                        Object objData = (Object)lsSalaryItemCat[i];
                                                        System.Reflection.PropertyInfo pi = objData.GetType().GetProperty("GetSettype");
                                                        String sItem = (String)(pi.GetValue(objData, null));
                                                %>
                                                        <option value="<%=sItem%>" <%=sCat.Equals(sItem) ? "selected" : "" %>><%=sItem%></option>
                                                <%
                                                    }
                                                %>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsItemType" name="lsItemType" class="select">
                                                <option value="PERCENTAGE">PERCENTAGE</option>
                                                <option value="AMOUNT">AMOUNT</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nilai</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemValue" name="txtItemValue" type="text" size="10" maxlength="10" value="0" class="input">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kumpulan Peratusan</td>
                                        <td width="80%" class="tblText2">
                                            <%
                                                for (int i = 0; i < lsItemGroup.Count; i++)
                                                {
                                                    HRModel modAcc = (HRModel)lsItemGroup[i];
                                            %>
                                                    <input type="checkbox" id="chk<%=modAcc.GetSetcode %>" class="itemgroup" value="<%=modAcc.GetSetcode %>"><%=modAcc.GetSetcode %> - <%=modAcc.GetSetdesc %><br/>
                                            <%
                                                }
                                            %>
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
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonOpen" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertSalaryItemDetail();">Tambah</button>
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateSalaryItemDetail();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeSalaryItemDetail();">Tutup</button>
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

            var getSalaryItemList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getSalaryItemList", getSalaryItemList_parameters, getSalaryItemList_succeedAjaxFn, getSalaryItemList_failedAjaxFn, false);

        });

        var getSalaryItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSalaryItemList_succeedAjaxFn: " + textStatus);
            var getSalaryItemList_result = JSON.parse(data.d);
            if (getSalaryItemList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getSalaryItemList_result.itemlist, function (i, result) {
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
                console.log("getSalaryItemList_result.result: " + getSalaryItemList_result.result);
            }
        }

        var getSalaryItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSalaryItemList_failedAjaxFn: " + textStatus);
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
                    deleteSalaryItemDetail(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openSalaryItemDetail('OPEN', id);
                }
            }
        });

        function checkItemGroup() {
            var res = "";
            //var inputs = document.querySelectorAll('.pl');
            var inputs = $('.itemgroup');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked == true) {
                    var pl1 = inputs[i].value;
                    if (res.length > 0) {
                        res = res + "," + pl1;
                    } else {
                        res = pl1;
                    }
                }
            }
            if ($('#lsItemType').val() == "PERCENTAGE") {
                $('#hidItemGroup').val(res);
            } else {
                $('#hidItemGroup').val("");
            }
        }

        function checkedItemGroup(itemgroup) {
            if (itemgroup.length > 0) {
                let arr = itemgroup.split(',');
                //var inputs = document.querySelectorAll('.pl');
                var inputs = $('.itemgroup');
                for (var i = 0; i < inputs.length; i++) {
                    var pl1 = inputs[i].value;
                    if (arr.includes(pl1)) {
                        inputs[i].checked = true;
                    }
                    else {
                        inputs[i].checked = false;
                    }

                    if ($('#lsItemType').val() == "PERCENTAGE") {
                        inputs[i].disabled = false;
                    } else {
                        inputs[i].disabled = true;
                    }
                }
            } else {
                var inputs = $('.itemgroup');
                for (var i = 0; i < inputs.length; i++) {
                    inputs[i].checked = false;

                    if ($('#lsItemType').val() == "PERCENTAGE") {
                        inputs[i].disabled = false;
                    } else {
                        inputs[i].disabled = true;
                    }
                }
            }
        }

        $("#lsItemType").change(function () {
            var inputs = $('.itemgroup');
            for (var i = 0; i < inputs.length; i++) {
                //inputs[i].checked = false;
                if ($('#lsItemType').val() == "PERCENTAGE") {
                    inputs[i].disabled = false;
                } else {
                    inputs[i].disabled = true;
                }
            }
        });


        function deleteSalaryItemDetail(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var paramlist = {};
                paramlist.currcomp = "<%=sCurrComp%>";
                paramlist.id = id;
                var json_string = JSON.stringify(paramlist);
                
                PageMethod2("deleteSalaryItemDetail", json_string, deleteSalaryItemDetail_succeedAjaxFn, deleteSalaryItemDetail_failedAjaxFn, false);
            }
        }

        var deleteSalaryItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteSalaryItemDetail_succeedAjaxFn: " + textStatus);
            var deleteSalaryItemDetail_result = JSON.parse(data.d);
            if (deleteSalaryItemDetail_result.result == "Y") {
                //alert(deleteSalaryItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteSalaryItemDetail_result.message);
            }
        }

        var deleteSalaryItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteSalaryItemDetail_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Public Holiday...");
        }

        function openSalaryItemDetail(typ, id) {
            $('#SalaryItemDetail').modal({ backdrop: "static" });
            if (typ == 'OPEN') {
                //$('#btnAddFisCOA').prop('disabled', true);
                //$('#btnModifyFisCOA').prop('disabled', false);
                $('#btnAddItem').hide();
                $('#btnModifyItem').show();

                var getSalaryItemDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getSalaryItemDetail", getSalaryItemDetail_parameters, getSalaryItemDetail_succeedAjaxFn, getSalaryItemDetail_failedAjaxFn, false);

            } else {
                //$('#btnAddFisCOA').prop('disabled', false);
                //$('#btnModifyFisCOA').prop('disabled', true);
                $('#btnAddItem').show();
                $('#btnModifyItem').hide();
            }
        }

        var getSalaryItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSalaryItemDetail_succeedAjaxFn: " + textStatus);
            var getSalaryItemDetail_result = JSON.parse(data.d);
            if (getSalaryItemDetail_result.result == "Y") {
                $('#hidId').val(getSalaryItemDetail_result.itemdetail.GetSetid);
                $('#txtItemCode').val(getSalaryItemDetail_result.itemdetail.GetSetcode);
                $('#txtItemDesc').val(getSalaryItemDetail_result.itemdetail.GetSetdesc);
                $('#lsItemCat').val(getSalaryItemDetail_result.itemdetail.GetSetcat);
                $('#lsItemType').val(getSalaryItemDetail_result.itemdetail.GetSettype);
                $('#txtItemValue').val(getSalaryItemDetail_result.itemdetail.GetSetitemvalue);
                $('#hidItemGroup').val(getSalaryItemDetail_result.itemdetail.GetSetitemgroup);
                $('#lsStatus').val(getSalaryItemDetail_result.itemdetail.GetSetstatus);
                checkedItemGroup(getSalaryItemDetail_result.itemdetail.GetSetitemgroup);
            }
            else {
                alert(getSalaryItemDetail_result.message);
            }
        }

        var getSalaryItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSalaryItemDetail_failedAjaxFn: " + textStatus);
        }

        function insertSalaryItemDetail() {
            checkItemGroup();
            var paramlist = {};
            paramlist.GetSetcomp = "<%=sCurrComp%>";
            paramlist.GetSetcode = $('#txtItemCode').val();
            paramlist.GetSetdesc = $('#txtItemDesc').val();
            paramlist.GetSetcat = $('#lsItemCat').val();
            paramlist.GetSettype = $('#lsItemType').val();
            paramlist.GetSetitemvalue = $('#txtItemValue').val();
            paramlist.GetSetstatus = $('#lsStatus').val();
            paramlist.GetSetitemgroup = $('#hidItemGroup').val();
            var parameters = { 'itemsalary': paramlist };
            var json_string = JSON.stringify(parameters);

            PageMethod2("insertSalaryItemDetail", json_string, insertSalaryItemDetail_succeedAjaxFn, insertSalaryItemDetail_failedAjaxFn, false);
        }

        var insertSalaryItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertSalaryItemDetail_succeedAjaxFn: " + textStatus);
            var insertSalaryItemDetail_result = JSON.parse(data.d);
            if (insertSalaryItemDetail_result.result == "Y") {
                //alert(insertSalaryItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertSalaryItemDetail_result.message);
            }
        }

        var insertSalaryItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertSalaryItemDetail_failedAjaxFn: " + textStatus);
        }

        function updateSalaryItemDetail() {
            checkItemGroup();
            var paramlist = {};
            paramlist.GetSetcomp = "<%=sCurrComp%>";
            paramlist.GetSetcode = $('#txtItemCode').val();
            paramlist.GetSetdesc = $('#txtItemDesc').val();
            paramlist.GetSetcat = $('#lsItemCat').val();
            paramlist.GetSettype = $('#lsItemType').val();
            paramlist.GetSetitemvalue = $('#txtItemValue').val();
            paramlist.GetSetitemgroup = $('#hidItemGroup').val();
            paramlist.GetSetstatus = $('#lsStatus').val();
            paramlist.GetSetid = $('#hidId').val();
            var parameters = { 'itemsalary': paramlist };
            var json_string = JSON.stringify(parameters);

            PageMethod2("updateSalaryItemDetail", json_string, updateSalaryItemDetail_succeedAjaxFn, updateSalaryItemDetail_failedAjaxFn, false);
        }

        var updateSalaryItemDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateSalaryItemDetail_succeedAjaxFn: " + textStatus);
            var updateSalaryItemDetail_result = JSON.parse(data.d);
            if (updateSalaryItemDetail_result.result == "Y") {
                //alert(updateSalaryItemDetail_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(updateSalaryItemDetail_result.message);
            }
        }

        var updateSalaryItemDetail_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateSalaryItemDetail_failedAjaxFn: " + textStatus);
        }

        function closeSalaryItemDetail() {
            resetSalaryItemDetail();
            $('#SalaryItemDetail').modal('hide');
        }

        function resetSalaryItemDetail() {
            $('#hidId').val("");
            $('#txtItemDate').val("");
            $('#txtItemCode').val("");
            $('#txtItemDesc').val("");
            $('#txtItemCat').val("");
            $('#txtItemType').val("");
            $('#txtItemValue').val("0");
            $('#lsStatus').val("");
        }

    </script>
</asp:Content>

