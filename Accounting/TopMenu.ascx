<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopMenu.ascx.cs" Inherits="Accounting_TopMenu" %>
<table border="0" cellpadding="0" cellspacing="0" height="20" width="100%">
    <tr>
        <td valign="middle">
            <%
            if(sTabMenuParent.Equals("1")){
            %>
            <!-- My Request -->
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="COAPage.aspx?sTabMenu=1.1">COA</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="BankPage.aspx?sTabMenu=1.2">Bank</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="BPIDPage.aspx?sTabMenu=1.3">Rakan Niaga</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("4")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("4")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="InventoryPage.aspx?sTabMenu=1.4">Inventori</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("5")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("5")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=1.5">Perlaburan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("6")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("6")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="AssetPage.aspx?sTabMenu=1.6">Aset</a>
                </div>
            </div>
            <%
            }else if(sTabMenuParent.Equals("2")){
            %>
            <!-- Approval -->
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="OBCBPage.aspx?sTabMenu=2.1">Baki Akaun</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="PostingPage.aspx?sTabMenu=2.2">Posting Data</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=2.3">Penyata Bank</a>
                </div>
            </div>
            <%
            }else if(sTabMenuParent.Equals("3")){
            %>
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="JournalVoucherPage.aspx?sTabMenu=3.1">Baucar Jurnal</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="PaymentVoucherPage.aspx?sTabMenu=3.2">Baucar Bayaran</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="ReceiptVoucherPage.aspx?sTabMenu=3.3">Baucar Terimaan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("4")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("4")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="CashVoucherPage.aspx?sTabMenu=3.4">Baucar Tunai</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("5")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("5")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="BankVoucherPage.aspx?sTabMenu=3.5">Baucar Bank</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("6")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("6")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=3.6">Nota Debit (DN)</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("7")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("7")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=3.7">Nota Kredit (CN)</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("8")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("8")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="LedgerQueryPage.aspx?sTabMenu=3.8">Carian Lejer</a>
                </div>
            </div>
            <%
            }else if(sTabMenuParent.Equals("4")){   
            %>
            <!-- View -->
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="InvoiceIssuedPage.aspx?sTabMenu=4.1">Invois Keluar</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="PaymentReceiptPage.aspx?sTabMenu=4.2">Bayaran Terima</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="AdvancedPaymentReceiptPage.aspx?sTabMenu=4.3">Bayaran Pendahuluan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("4")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("4")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="AccountReceivedBadDebtPage.aspx?sTabMenu=4.4">Hutang Lapuk AR</a>
                </div>
            </div>            <%
            }else if(sTabMenuParent.Equals("5")){   
            %>
            <!-- Admin -->
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="BillReceivedPage.aspx?sTabMenu=5.1">Bil Diterima</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="PaymentPaidPage.aspx?sTabMenu=5.2">Bayaran Belanja</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="AdvancedPaymentPaidPage.aspx?sTabMenu=5.3">Belanja Pendahuluan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("4")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("4")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="AccountPaymentBadDebtPage.aspx?sTabMenu=5.4">Hutang Lapuk AP</a>
                </div>
            </div>
            <%
            }else if(sTabMenuParent.Equals("6")){   
            %>
            <!-- View -->
            <div class="<%=(sTabMenuChild.Equals("1")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("1")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="TrialBalancePage.aspx?sTabMenu=6.1">Penyata Imbangan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("2")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("2")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="ProfitLossPage.aspx?sTabMenu=6.2">Untung & Rugi</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("3")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("3")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="BalanceSheetPage.aspx?sTabMenu=6.3">Penyata Kewangan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("4")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("4")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=6.4">Penyata Bank</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("5")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("5")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=6.5">Penyata Pelanggan</a>
                </div>
            </div>
            <div class="<%=(sTabMenuChild.Equals("6")?"allsubmenutabon":"allsubmenutab")%>">
                <div id="<%=(sTabMenuChild.Equals("6")?"innerallsubmenuon":"innerallsubmenu")%>">
                    <a href="RequestList.aspx?sTabMenu=6.6">Penyata Pembekal</a>
                </div>
            </div>
            <%
            }
            %>
        </td>
    </tr>
</table>