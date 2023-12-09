<%@ Page Title="" Language="C#" MasterPageFile="~/HumanResource/MasterPageAttendanceChild.master" AutoEventWireup="true" CodeFile="AttendanceWorkingDay.aspx.cs" Inherits="HumanResource_AttendanceWorkingDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contentfolder">
        <form id="form1" runat="server">

            <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
                <tr>
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Penyediaan Masa Kerja</a></td>
                </tr>
            </table>
            <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
                <tr class="tblTitle3Mod">
                    <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
                </tr>                
                <tr>
                    <td width="20%" class="tblTextCommon">Tarikh:</td>
                    <td width="80%" class="tblText2">
                        <input class="input" name="txtFindDayDate" id="txtFindDayDate" type="text" value="<%=sDayDate %>" size="20" maxlength="20">
                        <img src="images/icon_cal.gif" alt="Show Calendar" width="16" height="16" border="0" align="absmiddle" id="imgFindDayDate">
                        <script type="text/javascript">
                            Calendar.setup({
                                inputField: "txtFindDayDate",     	// id of the input field
                                ifFormat: "%d-%m-%Y ",   	// format of the input field
                                button: "imgFindDayDate",  		// trigger for the calendar (image ID)
                                align: "B1",
                                singleClick: true
                            });
                        </script>
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
                    <td align="center" valign="top"><a href="#" class="activeTab tab">Senarai Masa Kerja</a></td>
                </tr>
            </table>
            <table id="mytable" width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="center">
                <tbody>
                    <tr class="tblTitle3Mod">
                        <td height="25px" width="20%" valign="middle" align="left" class="tblTitle3Mod">Tahun/ Bulan</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Kod</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Tarikh</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Hari</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Masa Mula</td>
                        <td width="10%" valign="middle" align="left" class="tblTitle3Mod">Masa Akhir</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Salin Hari Sebelum</td>
                        <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Catatan</td>
                    </tr>
                    <%
                        if (lsWorkingDayAll.Count > 0)
                        {
                            for (int i = 0; i < lsWorkingDayAll.Count; i++)
                            {
                                HRModel modWorkDay = (HRModel)lsWorkingDayAll[i];
                                ArrayList restdayarray = oHRCon.tokenString(modWorkingGroup.GetSetrest_day,",");
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
                                <td width="50%" height="15" align="left"><%=lsWorkingDayAll.Count %> record(s)</td>
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

        function actionclick(action) {
            var proceed = true;
            if (action == "RESET") {
                document.getElementById("txtFindDayDate").value = "";
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
        //lsWorkingDay.Sort();
        String prevStrToExpand = "";
        for (int i = 0; i < lsWorkingDaySearch.Count; i++)
        {
            HRModel modWorDay = (HRModel)lsWorkingDaySearch[i];
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

