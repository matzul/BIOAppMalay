<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendance.master" AutoEventWireup="true" CodeFile="AttendanceWorkingDayTable.aspx.cs" Inherits="HumanResource_AttendanceWorkingDayTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Jadual Kehadiran Kakitangan</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
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
                    <td width="20%" class="tblTextCommon">Kumpulan Kehadiran:</td>
                    <td width="80%" class="tblText2">
                        <select class="select" id="lsFindGrpId" name="lsFindGrpId">
                            <option value="">-Select-</option>
                            <%
                                for(int i=0; i<lsWorkingGroup.Count; i++) 
                                { 
                                    HRModel modWorkGroup = (HRModel)lsWorkingGroup[i]; 
                            %>
                            <option value="<%=modWorkGroup.GetSetid %>" <%=sGrpId.Equals(modWorkGroup.GetSetid) ? "selected" : "" %>><%=modWorkGroup.GetSetdesc %> [<%=modWorkGroup.GetSetcode %>]</option>
                            <%
                                }
                            %>
                        </select>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Jadual Kehadiran</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="20%" valign="middle" align="left" class="tblTitle3Mod">Kumpulan Kehadiran</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">No. Pekerja</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Nama Pekerja</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Jawatan</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Mula</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Tarikh Akhir</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Hari Rehat</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod"></td>
                    </tr>
                    <%
                        if (lsWorkingGroupTableAll.Count > 0)
                        {
                            for (int i = 0; i < lsWorkingGroupTableAll.Count; i++)
                            {
                                HRModel modWorkDay = (HRModel)lsWorkingGroupTableAll[i];
                                ArrayList restdayarray = oHRCon.tokenString(modWorkDay.GetSetrest_day,",");
                                var boolrestday = oHRCon.isBoolContains(restdayarray, modWorkDay.GetSetday_name);
                                var boolphday = oHRCon.isBoolContains(lsPublicHolidayString, modWorkDay.GetSetday_date);
                                ArrayList datearray = oHRCon.tokenString(modWorkDay.GetSetday_date,"-");
                                String dd = datearray[0].ToString();
                                String mm = datearray[1].ToString();
                                String mon = "";
                                if (mm.Equals("01"))
                                {
                                    mon = "Januari";
                                }
                                else if (mm.Equals("02"))
                                {
                                    mon = "Februari";
                                }
                                else if (mm.Equals("03"))
                                {
                                    mon = "Mac";
                                }
                                else if (mm.Equals("04"))
                                {
                                    mon = "April";
                                }
                                else if (mm.Equals("05"))
                                {
                                    mon = "Mei";
                                }
                                else if (mm.Equals("06"))
                                {
                                    mon = "Jun";
                                }
                                else if (mm.Equals("07"))
                                {
                                    mon = "Julai";
                                }
                                else if (mm.Equals("08"))
                                {
                                    mon = "Ogos";
                                }
                                else if (mm.Equals("09"))
                                {
                                    mon = "September";
                                }
                                else if (mm.Equals("10"))
                                {
                                    mon = "Oktober";
                                }
                                else if (mm.Equals("11"))
                                {
                                    mon = "November";
                                }
                                else if (mm.Equals("12"))
                                {
                                    mon = "Disember";
                                }
                                if (dd.Equals("01"))
                                {
                    %>
                    <tr data-depth="0" class="collapse level0 tblText1" data-id="">
                        <td valign="top" align="left" class="tblTitle2Mod" colspan="8"><span id='<%=modWorkDay.GetSetcode%>-<%=mm %>' class='toggle collapse'></span><%=mon %> - <%=modWorkDay.GetSetfyr %></td>
                    </tr>
                    <tr data-depth="1" class="level1 <%=boolrestday.Equals(true)?"tblTitle2":boolphday.Equals(true)?"hilite1":"tblText1" %>" data-id="<%=modWorkDay.GetSetid %>">
                        <td valign="top" align="left"></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetcode %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetday_date %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetday_name %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetfromtime %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSettotime %><br /><%=modWorkDay.GetSetnext_day.Equals("1")?"(Hari Berikut)":"" %></td>
                        <td valign="top" align="left"><input type="checkbox" id="chkFollowPrevious" class="chkFollowPrevious" value="1" <%=modWorkDay.GetSetfollow_previous.Equals("1")?"checked":"" %> disabled></td>
                        <td valign="top" align="left"><%=boolrestday.Equals(true)?"REST-DAY":boolphday.Equals(true)?"OFF-DAY":"" %></td>
                    </tr>
                    <%
                                }
                                else
                                {
                    %>
                    <tr data-depth="1" class="level1 <%=boolrestday.Equals(true)?"tblTitle2":boolphday.Equals(true)?"hilite1":"tblText1" %>" data-id="<%=modWorkDay.GetSetid %>">
                        <td valign="top" align="left"></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetcode %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetday_date %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetday_name %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSetfromtime %></td>
                        <td valign="top" align="left"><%=modWorkDay.GetSettotime %><br /><%=modWorkDay.GetSetnext_day.Equals("1")?"(Hari Berikut)":"" %></td>
                        <td valign="top" align="left"><input type="checkbox" id="chkFollowPrevious" class="chkFollowPrevious" value="1" <%=modWorkDay.GetSetfollow_previous.Equals("1")?"checked":"" %> disabled></td>
                        <td valign="top" align="left"><%=boolrestday.Equals(true)?"REST-DAY":boolphday.Equals(true)?"OFF-DAY":"" %></td>
                    </tr>
                    <%                            
                                }                                               
                    %>
                    <%
                            }
                        }
                        else
                        {
                    %>
                    <tr class="tblText1">
                        <td colspan="8" class="tblText2">&nbsp;No Record Found</td>
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
                                <td width="50%" height="15" align="left"><%=lsWorkingGroupTableAll.Count %> record(s)</td>
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
        var StaffNoArray = [];
        var maxlengthdataautocomplete = 20;
        var StaffNameArray = [];
        var maxlengthdataautocomplete2 = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindStaffNo").value = "";
                document.getElementById("txtFindStaffName").value = "";
                document.getElementById("lsFindGrpId").selectedIndex = 0;
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
        //lsWorkingDay.Sort();
        String prevStrToExpand = "";
        for (int i = 0; i < lsWorkingGroupTableSearch.Count; i++)
        {
            HRModel modWorDay = (HRModel)lsWorkingGroupTableSearch[i];
            ArrayList datearray = oHRCon.tokenString(modWorDay.GetSetday_date,"-");
            String dd = datearray[0].ToString();
            String mm = datearray[1].ToString();
            String strToExpand = modWorDay.GetSetcode+"-"+mm;
            if (!prevStrToExpand.Equals(strToExpand))
            {
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
            prevStrToExpand = strToExpand;
        }
            %>

            //return children;

        });

        var findChildren = function (tr) {
            var depth = tr.data('depth');
            return tr.nextUntil($('tr').filter(function () {
                return $(this).data('depth') <= depth;
            }));
        };

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

    </script>
</asp:Content>

