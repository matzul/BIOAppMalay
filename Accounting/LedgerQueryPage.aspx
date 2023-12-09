<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="LedgerQueryPage.aspx.cs" Inherits="Accounting_LedgerQueryPage" %>

<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:TopMenu ID="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Transaksi Carta Akaun / Trasaction of Chart of Account (COA)</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
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
                    <td width="20%" class="tblTextCommon">No. Sub-Koding:</td>
                    <td width="80%" class="tblText2">
                        <input id="txtFindAccNumber" name="txtFindAccNumber" type="text" size="20" maxlength="20" value="<%=sCurrAccNumber %>" class="input">
                    </td>
                </tr>

            </table>
            <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center">
                <tr>
                    <td height="30" width="20%"></td>
                    <td width="80%" align="left">
                        <input class="button1" name="btnSearch" type="button" value="Carian" onclick="actionclick('SEARCH');">
                        <input class="button1" name="btnReset" type="button" value="Reset" onclick="actionclick('RESET');">
                    </td>
                </tr>
            </table>

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Transaksi Carta Akaun / List of Chart of Account (COA) Transaction</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="20%" valign="middle" align="left" class="tblTitle3Mod">No. Koding</td>
                        <td width="25%" valign="middle" align="left" class="tblTitle3Mod">Nama Koding</td>
                        <td width="13%" valign="middle" align="left" class="tblTitle3Mod">Sub-Koding</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">FYR</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan</td>
                        <td width="5%" valign="middle" align="left" class="tblTitle3Mod">Jenis</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Koding Rujukan</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Debit</td>
                        <td width="8%" valign="middle" align="left" class="tblTitle3Mod">Kredit</td>
                    </tr>
                    <%
                        if (lsFisCOATran.Count > 0)
                        {
                            for (int i = 0; i < lsFisCOATran.Count; i++)
                            {
                                AccountingModel modAcc = (AccountingModel)lsFisCOATran[i];
                    %>
                    <tr data-depth="<%=modAcc.GetSetacclevel %>" class="<%=modAcc.GetSetendlevel.Equals("Y") ? "" :"collapse"%> level<%=modAcc.GetSetacclevel %> tblText1">
                        <td valign="top" align="left"><%=modAcc.GetSetendlevel.Equals("Y") ? "" : "<span id='"+modAcc.GetSetaccid+"' class='toggle collapse'></span>" %><%=modAcc.GetSetaccid %></td>
                        <td valign="top" align="left"><a href="#" onclick="fOpenWindow('LedgerTranPage.aspx?fyr=<%=sCurrFyr %>&accid=<%=modAcc.GetSetaccid %>');"><%=modAcc.GetSetaccdesc %></a></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccnumber %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetfyr %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetaccgroup %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetacctype %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetparentid %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetdebit %></td>
                        <td valign="top" align="left"><%=modAcc.GetSetcredit %></td>
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

            <table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
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

	        <table width="98%" border="0" cellpadding="2" cellspacing="1" align="center" id="tblParentButton">
			    <tr>
				    <td align="center">
                        <input class="button1" name="btnClose" type="button" value="Tutup" onclick="window.close();">					    
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

        var fiscoaArray = [];
        var maxlengthdataautocomplete = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindLedgerNo").value = "";
                //document.getElementById("lsFindFyr").value = "<%=sCurrFyr%>";
                document.getElementById("lsFindType").selectedIndex = 0;
                document.getElementById("lsFindCategory").selectedIndex = 0;
                document.getElementById("txtFindAccNumber").value = "";
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        $(document).ready(function () {

            var el = $('#mytable .toggle');
            var tr = el.closest('tr'); //Get <tr> parent of toggle button
            var children = findChildren(tr);

            //Remove already collapsed nodes from children so that we don't
            //make them visible. 
            //(Confused? Remove this code and close Item 2, close Item 1 
            //then open Item 1 again, then you will understand)

            var subnodes = children.filter('.expand');
            subnodes.each(function () {
                //var subnode = $(this);
                var subnode = $('#mytable .toggle');
                var subnodeChildren = findChildren(subnode);
                children = children.not(subnodeChildren);
            });

            //Change icon and hide/show children
            if (tr.hasClass('collapse')) {
                tr.removeClass('collapse').addClass('expand');
                children.hide();
            } else {
                tr.removeClass('expand').addClass('collapse');
                children.show();
            }

            <%
            lsFisAccId.Sort();
            for (int i = 0; i < lsFisAccId.Count; i++)
            {
                String strToExpand = (String)lsFisAccId[i];
            %>
                var el<%=i%> = $('#<%=strToExpand%>');
                var tr<%=i%> = el<%=i%>.closest('tr'); //Get <tr> parent of toggle button
                var children<%=i%> = findChildren(tr<%=i%>);

                //Remove already collapsed nodes from children so that we don't
                //make them visible. 
                //(Confused? Remove this code and close Item 2, close Item 1 
                //then open Item 1 again, then you will understand)
                var subnodes<%=i%> = children<%=i%>.filter('.expand');
                subnodes<%=i%>.each(function () {
                    //var subnode<%=i%> = $('#<%=strToExpand%> .toggle');
                    var subnode<%=i%> = $(this);
                    var subnodeChildren<%=i%> = findChildren(subnode<%=i%>);
                    children<%=i%> = children<%=i%>.not(subnodeChildren<%=i%>);
                });

                //Change icon and hide/show children
                if (tr<%=i%>.hasClass('collapse')) {
                    tr<%=i%>.removeClass('collapse').addClass('expand');
                    children<%=i%>.hide();
                } else {
                    tr<%=i%>.removeClass('expand').addClass('collapse');
                    children<%=i%>.show();
                }
            <%
            }
            %>

            var getFisCOAList_parameters = ["currcomp", "<%=sCurrComp%>", "currfyr", "<%=sCurrFyr%>"];
            PageMethod("getFisCOAList", getFisCOAList_parameters, getFisCOAList_succeedAjaxFn, getFisCOAList_failedAjaxFn, false);

            //return children;

        });

        var findChildren = function (tr) {
            var depth = tr.data('depth');
            return tr.nextUntil($('tr').filter(function () {
                return $(this).data('depth') <= depth;
            }));
        };

        $('#mytable').on('click', '.toggle', function () {
            //Gets all <tr>'s  of greater depth
            //below element in the table

            var el = $(this);
            var tr = el.closest('tr'); //Get <tr> parent of toggle button
            var children = findChildren(tr);

            //Remove already collapsed nodes from children so that we don't
            //make them visible. 
            //(Confused? Remove this code and close Item 2, close Item 1 
            //then open Item 1 again, then you will understand)
            var subnodes = children.filter('.expand');
            subnodes.each(function () {
                var subnode = $(this);
                var subnodeChildren = findChildren(subnode);
                children = children.not(subnodeChildren);
            });

            //Change icon and hide/show children
            if (tr.hasClass('collapse')) {
                tr.removeClass('collapse').addClass('expand');
                children.hide();
            } else {
                tr.removeClass('expand').addClass('collapse');
                children.show();
            }
            return children;
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

        // initialize autocomplete with custom appendTo
        /*
        var objData = {A101:'TEST1', A201:'TEST2'};
        console.log("objData2: " + objData);       
        
        //fiscoaArray must be in format [{'value':'DESC', 'data':'ID'},{'value':'DESC', 'data':'ID'}]
        var fiscoaArray = $.map(objData, function (value, key) {
            return {
                value: value,
                data: key
            };
        });           
        */

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
        //}).click(function () { $(this).autocomplete("search", " "); });

    </script>
</asp:Content>

