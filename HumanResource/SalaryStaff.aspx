<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageSalary.master" AutoEventWireup="true" CodeFile="SalaryStaff.aspx.cs" Inherits="HumanResource_SalaryStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Kakitangan Kepada Kumpulan Gaji</a></td>
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
                    <td width="20%" class="tblTextCommon">No. Pekerja:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindStaffNo" name="txtFindStaffNo" type="text" size="10" maxlength="10" value="<%=sStaffNo%>" class="input">
                        <div id="txtFindStaffNo-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Nama Pekerja:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindStaffName" name="txtFindStaffName" type="text" size="50" maxlength="50" value="<%=sStaffName%>" class="input">
                        <div id="txtFindStaffName-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="tblTextCommon">Kategori:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindSalaryCat" name="lsFindSalaryCat">
                            <option value="">-Select-</option>
                            <%
                                for (int i = 0; i < lsGredComp.Count; i++)
                                {
                                    HRModel modItem = (HRModel)lsGredComp[i];
                            %>
                            <option value="<%=modItem.GetSetname %>" <%=sSalaryCat.Equals(modItem.GetSetname)?"selected":"" %>><%=modItem.GetSetname %></option>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Kakitangan & Kategori Cuti</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="3%" valign="middle" align="left" class="tblTitle3Mod">#</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="19%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Tahun</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="20%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Bermula</td>
                        <td width="7%" valign="middle" align="left" class="tblTitle3Mod">Sehingga</td>
                        <td width="6%" valign="middle" align="left" class="tblTitle3Mod">Status</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsStaffSalaryGroup.Count > 0)
                        {
                            for (int i = 0; i < lsStaffSalaryGroup.Count; i++)
                            {
                                HRModel modItem = (HRModel)lsStaffSalaryGroup[i];
                    %>
                    <tr class="tblText1" data-id="<%=modItem.GetSetid %>">
                        <td valign="top" align="left"><%=i+1 %></td>
                        <td valign="top" align="left"><%=modItem.GetSetstaffno %></td>
                        <td valign="top" align="left"><%=modItem.GetSetname %><br /><%=modItem.GetSetpos_name %> [<%=modItem.GetSetgred_name %>]<br /><%=modItem.GetSetdept_name %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modItem.GetSetcat %></td>
                        <td valign="top" align="left"><%=modItem.GetSettype %></td>
                        <td valign="top" align="left"><%=modItem.GetSetdesc %></td>
                        <td valign="top" align="left"><%=modItem.GetSetfromdate %></td>
                        <td valign="top" align="left"><%=modItem.GetSettodate %></td>
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
                        <td colspan="13" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsStaffSalaryGroup.Count %> record(s)</td>
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
                        <input class="button1a" name="btnCreate" type="button" value="Tambah" onclick="openSGStaffUpdate('ADD', 0);">
                        <input class="button1" id="btnClose" type="button" value="Tutup" onclick="window.close();">
                    </td>
                </tr>
            </table>

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="" />
                <input type="hidden" name="hidStaffNo" id="hidStaffNo" value="" />
                <input type="hidden" name="hidId" id="hidId" value="" />
                <input type="hidden" name="hidSgId" id="hidSgId" value="" />
                <input type="hidden" name="hidFyr" id="hidFyr" value="" />
            </div>

            <div class="modal fade" id="SGStaffAdd" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGStaffAdd" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Kumpulan Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtStaffNoAdd" name="txtStaffNoAdd" type="text" size="10" maxlength="10" value="" class="input">
                                            <div id="txtStaffNoAdd-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffNameAdd" name="txtStaffNameAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jawatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtGredPositionAdd" name="txtGredPositionAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jabatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffDeptAdd" name="txtStaffDeptAdd" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtFyrAdd" name="txtFyrAdd" size="4" maxlength="4" value="<%=sCurrFyr %>" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDateAdd" id="txtFromDateAdd" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                             - 
                                            <input class="input" name="txtToDateAdd" id="txtToDateAdd" type="text" value="" size="15" maxlength="20" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Status</td>
                                        <td width="80%" class="tblText2">
                                            <select id="lsStatusAdd" name="lsStatusAdd" class="select">
                                                <option value="ACTIVE">ACTIVE</option>
                                                <option value="IN-ACTIVE">IN-ACTIVE</option>
                                            </select>
                                            <button type="button" class="button1 btn-warning" onclick="resetSGStaffAdd();">Reset</button>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblParentTableAdd" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center" style="border-spacing: 1px; border-collapse: inherit;">
                                </table>
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonAdd" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnAddItem" name="btnAddItem" type="button" class="button1 btn-primary" onclick="insertSGStaffUpdate();">Tambah</button>
                                            <button type="button" class="button1" onclick="closeSGStaffAdd();">Tutup</button>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="SGStaffUpdate" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="contentfolder">

                                <table id="tbSGStaffUpdate" width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr class="tblTitle3Mod">
                                        <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Maklumat Kakitangan & Kumpulan Gaji</td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">No. Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtStaffNo" name="txtStaffNo" type="text" size="10" maxlength="10" value="" class="input" readonly style="background-color:gray; color:floralwhite;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Nama Pekerja</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffName" name="txtStaffName" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jawatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtGredPosition" name="txtGredPosition" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jabatan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtStaffDept" name="txtStaffDept" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtFyr" name="txtFyr" size="4" maxlength="4" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Kategori:</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtSalaryCat" name="txtSalaryCat" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Jenis:</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtSalaryType" name="txtSalaryType" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Keterangan:</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtSalaryDesc" name="txtSalaryDesc" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input class="input" name="txtFromDate" id="txtFromDate" type="text" value="" size="10" maxlength="10">
                                             - 
                                            <input class="input" name="txtToDate" id="txtToDate" type="text" value="" size="10" maxlength="10">
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
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonUpdate" style="border-spacing: 1px; border-collapse: inherit;">
                                    <tr>
                                        <td align="center">
                                            <button id="btnModifyItem" name="btnModifyItem" type="button" class="button1 btn-primary" onclick="updateSGStaffUpdate();">Simpan</button>
                                            <button type="button" class="button1" onclick="closeSGStaffUpdate();">Tutup</button>
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
                                        <td width="20%" class="tblTextCommon">Keterangan</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiDesc" name="txtItemGajiDesc" size="50" maxlength="50" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tahun</td>
                                        <td width="80%" class="tblText2">
                                            <input type="text" id="txtItemGajiFyr" name="txtItemGajiFyr" size="4" maxlength="4" value="" class="input" readonly style="background-color:gray; color:floralwhite;"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" class="tblTextCommon">Tarikh</td>
                                        <td width="80%" class="tblText2">
                                            <input id="txtItemGajiFromDate" name="txtItemGajiFromDate" type="text" size="10" maxlength="10" value="0" class="input" readonly style="background-color:gray; color:floralwhite;"> - <input id="txtItemGajiToDate" name="txtItemGajiToDate" type="text" size="10" maxlength="10" value="0" class="input" readonly style="background-color:gray; color:floralwhite;">
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
                                <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButtonGaji" style="border-spacing: 1px; border-collapse: inherit;">
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

        var StaffNoArray = [];
        var maxlengthdataautocomplete = 20;
        var StaffNameArray = [];
        var maxlengthdataautocomplete2 = 20;
        var ItemGajiArray = [];
        var ValueGajiArray = [];
        var AmountGajiArray = [];

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindStaffNo").value = "";
                document.getElementById("txtFindStaffName").value = "";
                document.getElementById("lsFindSalaryCat").selectedIndex = 0;
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var getStaffEmployList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#lsFindFyr').val()];
            PageMethod("getStaffEmployList", getStaffEmployList_parameters, getStaffEmployList_succeedAjaxFn, getStaffEmployList_failedAjaxFn, false);

        });

        var getStaffEmployList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffEmployList_succeedAjaxFn: " + textStatus);
            var getStaffEmployList_result = JSON.parse(data.d);
            if (getStaffEmployList_result.result == "Y") {
                var itemno = '';
                var itemdesc = '';
                $.each(getStaffEmployList_result.itemlist, function (i, result) {
                    if (itemno != result.GetSetstaffno + '-' + result.GetSetname) {
                        var objData = {};
                        objData.value = result.GetSetstaffno + '-' + result.GetSetname;
                        objData.data = result.GetSetstaffno;
                        StaffNoArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete) {
                            maxlengthdataautocomplete = objData.value.length;
                        }
                        itemno = result.GetSetstaffno + '-' + result.GetSetname;
                    }

                    if (itemdesc != result.GetSetname) {
                        var objData = {};
                        objData.value = result.GetSetname;
                        objData.data = result.GetSetname;
                        StaffNameArray.push(objData);
                        if (objData.value.length > maxlengthdataautocomplete2) {
                            maxlengthdataautocomplete2 = objData.value.length;
                        }
                        itemdesc = result.GetSetname;
                    }

                });
            }
            else {
                console.log("getStaffEmployList_result.result: " + getStaffEmployList_result.result);
            }
        }

        var getStaffEmployList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployList_failedAjaxFn: " + textStatus);
        }

        $('#txtFindStaffNo').autocomplete({
            lookup: StaffNoArray,
            appendTo: '#txtFindStaffNo-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindStaffNo').val(suggestion.data);
            }
        });

        $('#txtFindStaffName').autocomplete({
            lookup: StaffNameArray,
            appendTo: '#txtFindStaffName-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete2 * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtFindStaffName').val(suggestion.data);
            }
        });

        $('#txtStaffNoAdd').autocomplete({
            lookup: StaffNoArray,
            appendTo: '#txtStaffNoAdd-container',
            minLength: 0,
            minChars: 0,
            width: maxlengthdataautocomplete * 12,
            onSelect: function (suggestion) {
                //console.log("suggestion: " + JSON.stringify(suggestion));
                $('#txtStaffNo').val(suggestion.data);
                var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val()];
                PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);
            }
        });

        $('#txtStaffNoAdd').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            var getStaffEmployDetails_parameters = ["currcomp", "<%=sCurrComp%>", "staffno", $('#txtStaffNo').val()];
            PageMethod("getStaffEmployDetails", getStaffEmployDetails_parameters, getStaffEmployDetails_succeedAjaxFn, getStaffEmployDetails_failedAjaxFn, false);

        });

        //BEGIN Response for getStaffEmployDetails
        getStaffEmployDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStaffEmployDetails_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStaffEmployDetails = JSON.parse(data.d);
            if (result_getStaffEmployDetails.result == "Y") {

                $('#txtStaffNameAdd').val(result_getStaffEmployDetails.staffemploy.GetSetname);
                $('#txtGredPositionAdd').val(result_getStaffEmployDetails.staffemploy.GetSetpos_name + " [" + result_getStaffEmployDetails.staffemploy.GetSetgred_name + "]");
                $('#txtStaffDeptAdd').val(result_getStaffEmployDetails.staffemploy.GetSetdept_name);
                $('#txtFyrAdd').val("<%=sCurrFyr%>");
                $('#txtFromDateAdd').val("01/01/<%=sCurrFyr%>");
                $('#txtToDateAdd').val("31/12/<%=sCurrFyr%>");

                var getSGItemList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>", "salary_cat", result_getStaffEmployDetails.staffemploy.GetSetgred_name, "staffno", ""];
                PageMethod("getSGItemList", getSGItemList_parameters, getSGItemList_succeedAjaxFn, getSGItemList_failedAjaxFn, false);

            } else {
                //alert("System Error!\nRecord not found...")
            }
        };

        getStaffEmployDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStaffEmployDetails_failedAjaxFn: " + textStatus);
        }
        //END Response for getStaffEmployDetails

        var getSGItemList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGItemList_succeedAjaxFn: " + textStatus);
            $('#tblParentTableAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '</tr>';
            var idx = 0;
            var getSGItemList_result = JSON.parse(data.d);
            if (getSGItemList_result.result == "Y") {
                $.each(getSGItemList_result.itemlist, function (i, item) {
                    idx += 1;
                    trHTML += '<tr><td valign="top"><input type="checkbox" name="chk_id" class="groupsalary" value="' + item.GetSetid + '"></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSetcode + '</font><input type="hidden" name="input_code" value="' + item.GetSetcode + '" /></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSetdesc + '</font><input type="hidden" name="input_desc" value="' + item.GetSetdesc + '" /></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSetcat + '</font><input type="hidden" name="input_cat" value="' + item.GetSetcat + '" /></td>' +
                        '<td align="left" valign="top"><font>' + item.GetSettype + '</font><input type="hidden" name="input_type" value="' + item.GetSettype + '" /></td>' +
                        '</tr>';
                });

                if (idx > 0) {
                    //$('#tblParentTableAdd').append(trHTML);
                }
                else {
                    trHTML += '<tr><td valign="top" colspan=5 ><font>No record found!</font></td>' +
                        '</tr>';
                }
            }
            else {
                alert(getSGItemList_result.message);
            }
            $('#tblParentTableAdd').append(trHTML);
        }

        var getSGItemList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGItemList_failedAjaxFn: " + textStatus);
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
                    deleteSGStaffUpdate(id);
                }
                else if (action == 'edit') {
                    //alert('edit:' + id);
                    openSGStaffUpdate('OPEN', id);
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

                var getSGItemGajiDetail_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getStaffItemDetail", getSGItemGajiDetail_parameters, getSGItemGajiDetail_succeedAjaxFn, getSGItemGajiDetail_failedAjaxFn, false);

            }
        }

        var getSGItemGajiDetail_succeedAjaxFn = function (data, textStatus, jqXHR) {
            //emptyItemGaji();
            $('#tblItemGajiAdd').empty();
            console.log("getSGItemGajiDetail_succeedAjaxFn: " + textStatus);
            var getSGItemDetail_result = JSON.parse(data.d);
            if (getSGItemDetail_result.result == "Y") {
                $('#hidId').val(getSGItemDetail_result.itemdetail.GetSetid);
                $('#hidSgId').val(getSGItemDetail_result.itemdetail.GetSetsg_id);
                $('#hidStaffNo').val(getSGItemDetail_result.itemdetail.GetSetstaffno);
                $('#hidFyr').val(getSGItemDetail_result.itemdetail.GetSetfyr);
                $('#txtItemGajiStaffNo').val(getSGItemDetail_result.itemdetail.GetSetstaffno);
                $('#txtItemGajiStaffName').val(getSGItemDetail_result.itemdetail.GetSetname);
                $('#txtItemGajiPosition').val(getSGItemDetail_result.itemdetail.GetSetpos_name + " [" + getSGItemDetail_result.itemdetail.GetSetgred_name + "]");
                $('#txtItemGajiDept').val(getSGItemDetail_result.itemdetail.GetSetdept_name);
                $('#txtItemGajiCat').val(getSGItemDetail_result.itemdetail.GetSetcat);
                $('#txtItemGajiType').val(getSGItemDetail_result.itemdetail.GetSettype);
                $('#txtItemGajiDesc').val(getSGItemDetail_result.itemdetail.GetSetdesc);
                $('#txtItemGajiFyr').val(getSGItemDetail_result.itemdetail.GetSetfyr);
                $('#txtItemGajiFromDate').val(getSGItemDetail_result.itemdetail.GetSetfromdate);
                $('#txtItemGajiToDate').val(getSGItemDetail_result.itemdetail.GetSettodate);
                $('#txtItemGajiStatus').val(getSGItemDetail_result.itemdetail.GetSetstatus);

                var getSGItemGajiList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", getSGItemDetail_result.itemdetail.GetSetfyr, "sg_id", getSGItemDetail_result.itemdetail.GetSetsg_id];
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

                var getSGItemGajiChecked_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#hidFyr').val(), "staffno", $('#hidStaffNo').val(), "sg_id", $('#hidSgId').val(), "ssg_id", $('#hidId').val()];
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
            paramArray.currfyr = $('#hidFyr').val();
            paramArray.staffno = $('#hidStaffNo').val();
            paramArray.sg_id = $('#hidSgId').val();
            paramArray.ssg_id = $('#hidId').val();

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
            $('#txtItemGajiCat').val("");
            $('#txtItemGajiType').val("");
            $('#txtItemGajiDesc').val("");
            $('#txtItemGajiFromDate').val("");
            $('#txtItemGajiToDate').val("");
            $('#txtItemGajiStatus').val("");
            emptyItemGaji();
        }

        function deleteSGStaffUpdate(id) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteSGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("deleteSGStaffUpdate", deleteSGStaffUpdate_parameters, deleteSGStaffUpdate_succeedAjaxFn, deleteSGStaffUpdate_failedAjaxFn, false);
            }
        }

        var deleteSGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteSGStaffUpdate_succeedAjaxFn: " + textStatus);
            var deleteSGStaffUpdate_result = JSON.parse(data.d);
            if (deleteSGStaffUpdate_result.result == "Y") {
                //alert(deleteSGStaffUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(deleteSGStaffUpdate_result.message);
            }
        }

        var deleteSGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteSGStaffUpdate_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete Staff Salary...");
        }

        function openSGStaffUpdate(typ, id) {
            if (typ == 'OPEN') {
                $('#SGStaffUpdate').modal({ backdrop: "static" });

                var getSGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "id", id];
                PageMethod("getStaffItemDetail", getSGStaffUpdate_parameters, getSGStaffUpdate_succeedAjaxFn, getSGStaffUpdate_failedAjaxFn, false);

            } else if (typ == 'ADD') {
                $('#SGStaffAdd').modal({ backdrop: "static" });
                $('#tblParentTableAdd').empty();
                var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                    '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>' +
                    '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                    '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                    '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                    '</tr>';
                $('#tblParentTableAdd').append(trHTML);

            }
        }

        var getSGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSGStaffUpdate_succeedAjaxFn: " + textStatus);
            var getSGStaffUpdate_result = JSON.parse(data.d);
            if (getSGStaffUpdate_result.result == "Y") {
                $('#hidId').val(getSGStaffUpdate_result.itemdetail.GetSetid);
                $('#hidSgId').val(getSGStaffUpdate_result.itemdetail.GetSetsg_id);
                $('#txtStaffNo').val(getSGStaffUpdate_result.itemdetail.GetSetstaffno);
                $('#txtStaffName').val(getSGStaffUpdate_result.itemdetail.GetSetname);
                $('#txtGredPosition').val(getSGStaffUpdate_result.itemdetail.GetSetpos_name + " [" + getSGStaffUpdate_result.itemdetail.GetSetgred_name + "]");
                $('#txtStaffDept').val(getSGStaffUpdate_result.itemdetail.GetSetdept_name);
                $('#txtFyr').val(getSGStaffUpdate_result.itemdetail.GetSetfyr);
                $('#txtSalaryCat').val(getSGStaffUpdate_result.itemdetail.GetSetcat);
                $('#txtSalaryType').val(getSGStaffUpdate_result.itemdetail.GetSettype);
                $('#txtSalaryDesc').val(getSGStaffUpdate_result.itemdetail.GetSetdesc);
                $('#txtFromDate').val(getSGStaffUpdate_result.itemdetail.GetSetfromdate);
                $('#txtToDate').val(getSGStaffUpdate_result.itemdetail.GetSettodate);
                $('#lsStatus').val(getSGStaffUpdate_result.itemdetail.GetSetstatus);
            }
            else {
                alert(getSGStaffUpdate_result.message);
            }
        }

        var getSGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function insertSGStaffUpdate() {
            var paramArray = {};
            var ItemInputArray = [];

            //construct input array
            var input_code = document.getElementsByName('input_code');
            var input_desc = document.getElementsByName('input_desc');
            var input_cat = document.getElementsByName('input_cat');
            var input_type = document.getElementsByName('input_type');

            paramArray.currcomp = "<%=sCurrComp%>";
            paramArray.currfyr = $('#txtFyrAdd').val();
            paramArray.staffno = $('#txtStaffNo').val();
            paramArray.fromdate = $('#txtFromDateAdd').val();
            paramArray.todate = $('#txtToDateAdd').val();
            paramArray.status = $('#lsStatus').val();

            var inputs = $('.groupsalary');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked == true) {
                    var input_group_salary = {};
                    input_group_salary.GetSetsg_id = inputs[i].value;
                    input_group_salary.GetSetcode = input_code[i].value;
                    input_group_salary.GetSetdesc = input_desc[i].value;
                    input_group_salary.GetSetcat = input_cat[i].value;
                    input_group_salary.GetSettype = input_type[i].value;
                    ItemInputArray.push(input_group_salary);
                }
            }

            paramArray.inputarray = ItemInputArray;
            var json_string = JSON.stringify(paramArray);

            //Call the page method
            $.ajax({
                type: "POST",
                url: window.location.pathname + "/insertSGStaffUpdate",
                contentType: "application/json; charset=utf-8",
                data: json_string,
                dataType: "json",
                success: insertSGStaffUpdate_succeedAjaxFn,
                error: insertSGStaffUpdate_failedAjaxFn,
                async: false
            });

        }

        var insertSGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertSGStaffUpdate_succeedAjaxFn: " + textStatus);
            var insertSGStaffUpdate_result = JSON.parse(data.d);
            if (insertSGStaffUpdate_result.result == "Y") {
                //alert(insertSGStaffUpdate_result.message);
                actionclick('SEARCH');
            }
            else {
                alert(insertSGStaffUpdate_result.message);
            }
        }

        var insertSGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertSGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function closeSGStaffAdd() {
            resetSGStaffAdd();
            $('#SGStaffAdd').modal('hide');
        }

        function resetSGStaffAdd() {
            $('#hidId').val("");
            $('#hidSgId').val("");
            $('#txtStaffNoAdd').val("");
            $('#txtStaffNameAdd').val("");
            $('#txtStaffDeptAdd').val("");
            $('#txtGredPositionAdd').val("");
            $('#txtFyrAdd').val("");
            $('#txtFromDateAdd').val("");
            $('#txtToDateAdd').val("");
            document.getElementById("lsStatusAdd").selectedIndex = 0;

            $('#tblParentTableAdd').empty();
            var trHTML = '<tr class="tblTitle3Mod"><td height="25px" width="5%" valign="middle" align="center" class="tblTitle3Mod">#</td>' +
                '<td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>' +
                '<td width="25%" valign="middle" align="left" class="tblTitle3Mod">Keterangan</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>' +
                '<td width="20%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>' +
                '</tr>';
            $('#tblParentTableAdd').append(trHTML);
        }

        function updateSGStaffUpdate() {
            var updateSGStaffUpdate_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", $('#txtFyr').val(), "staffno", $('#txtStaffNo').val(), "fromdate", $('#txtFromDate').val(), "todate", $('#txtToDate').val(), "brought", $('#txtLeaveBrought').val(), "count", $('#txtLeaveCount').val(), "taken", $('#txtLeaveTaken').val(), "status", $('#lsStatus').val(), "id", $('#hidId').val()];
            PageMethod("updateSGStaffUpdate", updateSGStaffUpdate_parameters, updateSGStaffUpdate_succeedAjaxFn, updateSGStaffUpdate_failedAjaxFn, false);
        }

        var updateSGStaffUpdate_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateSGStaffUpdate_succeedAjaxFn: " + textStatus);
            var updateSGStaffUpdate_result = JSON.parse(data.d);
            if (updateSGStaffUpdate_result.result == "Y") {
                actionclick('SEARCH');
            }
            else {
                alert(updateSGStaffUpdate_result.message);
            }
        }

        var updateSGStaffUpdate_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateSGStaffUpdate_failedAjaxFn: " + textStatus);
        }

        function closeSGStaffUpdate() {
            resetSGStaffUpdate();
            $('#SGStaffUpdate').modal('hide');
        }

        function resetSGStaffUpdate() {
            $('#hidId').val("");
            $('#hidSgId').val("");
            $('#txtStaffNo').val("");
            $('#txtStaffName').val("");
            $('#txtStaffDept').val("");
            $('#txtGredPosition').val("");
            $('#txtFyr').val("");
            $('#txtSalaryCat').val("");
            $('#txtSalaryType').val("");
            $('#txtSalaryDesc').val("");
            $('#txtFromDate').val("");
            $('#txtToDate').val("");
            document.getElementById("lsStatus").selectedIndex = 0;
        }

        /*
        $('#txtLeaveBrought').on('focus', function () {
            calculateTotalLeave();

        }).on('blur', function () {
            calculateTotalLeave()

        });

        $('#txtLeaveCount').on('focus', function () {
            calculateTotalLeave();

        }).on('blur', function () {
            calculateTotalLeave()

        });
        
        function calculateTotalLeave() {
            var totalleave = Number($('#txtLeaveBrought').val()) + Number($('#txtLeaveCount').val());
            $('#txtLeaveTotal').val(totalleave);
        }
        */

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

