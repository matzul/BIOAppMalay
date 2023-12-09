<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/MasterPageAccounting.master" AutoEventWireup="true" CodeFile="CustomerPage.aspx.cs" Inherits="Accounting_CustomerPage" %>
<%@ Register Src="TopMenu.ascx" TagName="TopMenu" TagPrefix="tm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- TOPMENU:START -->
    <tm:topmenu id="TopMenu1" runat="server" />
    <!-- TOPMENU:END -->
    <div class="contentfolder">

        <table width="98%" border="0" cellspacing="1" cellpadding="5" align="center">
            <tr>
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Carta Akaun / Chart of Account (COA)</a></td>
            </tr>
        </table>
		<table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="tblBg">
            <tr class="tblTitle3Mod">
			  <td colspan="2" align="left" valign="middle" class="tblTitle3ModBold">&nbsp;Carian</td>
		    </tr>
		    <tr> 
			  <td width="20%" class="tblTextCommon">No. Koding:</td>
			  <td width="80%" class="tblText2"><input id="txtFindLedgerNo" name="txtFindLedgerNo" type="text" size="20" maxlength="20" value="" class="input"></td>
			</tr>
		    <tr> 
			  <td width="20%" class="tblTextCommon">Nama Koding:</td>
			  <td width="80%" class="tblText2"><input id="txtFindLedgerDesc" name="txtFindLedgerDesc" type="text" size="80" maxlength="80" value="" class="input"></td>
			</tr>
			<tr> 
			  <td width="20%" class="tblTextCommon">Kategori:</td>
			  <td width="80%" class="tblText2"><select class="select" id="lsFindCategory" name="lsFindCategory">
			                                       <option value="" selected>-Semua-</option>
	                                               <option value="ASSET">Aset</option>              
	                                               <option value="LIABILITY">Liabiliti</option>              
	                                               <option value="EQUITY">Equiti/Modal</option>              
	                                               <option value="REVENUE">Hasil</option>              
	                                               <option value="EXPENSES">Belanja</option>              
                                               </select>
			   </td>
			</tr>
			<tr> 
			  <td width="20%" class="tblTextCommon">Sub-Koding:</td>
			  <td width="80%" class="tblText2"><select class="select" id="lsFindSubCoding" name="lsFindSubCoding">
			                                       <option value="" selected>-Semua-</option>
	                                               <option value="BANK">Bank</option>              
	                                               <option value="CUSTOMER">Pelanggan</option>              
	                                               <option value="SUPPLIER">Pembekal</option>              
	                                               <option value="INVEST">Pelaburan</option>              
	                                               <option value="ASSET">Hartanah/Loji/Aset</option>              
                                               </select>
			   </td>
			</tr>
		    <tr> 
			  <td width="20%" class="tblTextCommon">No. Sub-Koding:</td>
			  <td width="80%" class="tblText2"><input id="txtFindSubLedgerNo" name="txtFindSubLedgerNo" type="text" size="20" maxlength="20" value="" class="input"></td>
			</tr>
		    <tr> 
			  <td width="20%" class="tblTextCommon">Nama Sub-Koding:</td>
			  <td width="80%" class="tblText2"><input id="txtFindSubLedgerDesc" name="txtFindSubLedgerDesc" type="text" size="80" maxlength="80" value="" class="input"></td>
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
                <td align="center" valign="top" ><a href="#" class="activeTab tab">Senarai Carta Akaun / List of Chart of Account (COA)</a></td>
            </tr>
        </table>
        <table width="98%" border="0" cellspacing="1" cellpadding="2" class="tblBg" align="center">
		  <tr class="tblTitle3Mod">
			<td height="20" width="3%" valign="middle" align="left" class="tblTitle3Mod" >&nbsp;#</td>
			<td width="10%" align="left" valign="middle" class="tblTitle3Mod" >Item No.</td>
			<td width="15%" align="left" valign="middle" class="tblTitle3Mod" >Requisition</td>
			<td width="18%" valign="middle" align="left" class="tblTitle3Mod" >Business Reason/ Benefit/ Justification</td>
			<td width="8%" valign="middle" align="left" class="tblTitle3Mod" >Request Type</td>
			<td width="12%" valign="middle" align="left" class="tblTitle3Mod" >Requester</td>
			<td width="7%" valign="middle" align="left" class="tblTitle3Mod" >Department</td>
			<td width="7%" valign="middle" align="left" class="tblTitle3Mod" >Date Required</td>
			<td width="19%" valign="middle" align="left" class="tblTitle3Mod" >Status</td>
		  </tr>
          <tr class="tblText1">
			<td colspan="9" class="tblText2">&nbsp;No Record Found</td>
		  </tr>		
		  
      	</table>

		<form id="form1" runat="server">

			<table width="98%" border="0" cellspacing="1" cellpadding="0" align="center" class="tblBg">
			  <tr>
				<td>
					<table width="100%" cellpadding="2" cellspacing="1" class="tblTextMod">
						<tr>
							<td width="50%" height="15" align="left">0 record(s)</td>
							<td width="50%" align="right">
								page <asp:DropDownList CssClass="select" ID="lsPageList" runat="server"  OnSelectedIndexChanged="lsPageList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> of <%=sTotalPage %></td>
						</tr>
					</table>
				</td>
			  </tr>
			</table>   

			<div style="display: none;">
				<asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
				<input type="hidden" name="hidAction" id="hidAction" value="" />
			</div>
		</form>

    </div>
	<script type="text/javascript">
        function openaccountingpage(url) {
            var popupWindow = window.open("Accounting/" + url, "accounting_page", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1000) / 2;
                wtop = (screen.height - 800) / 2;
                if (wleft < 0) {
                    wleft = 0;
                }
                if (wtop < 0) {
                    wtop = 0;
                }
                popupWindow.moveTo(wleft, wtop);
            }
        }

        function actionclick(action) {
            var proceed = true;

            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }
    </script>
</asp:Content>

