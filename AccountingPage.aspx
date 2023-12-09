<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="AccountingPage.aspx.cs" Inherits="AccountingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* link */
        a {
            text-decoration: none;
            color: #003399;
        }

            a:hover {
                color: #0033cc;
                text-decoration: underline;
            }

            a:active {
                color: #0066FF;
            }

        .homeContent {
            position: relative;
            background: #fff;
            padding: 5px 0 30px;
            left: 0px;
            width: 100%;
            overflow: auto;
        }

            .homeContent .homeMainContent {
                position: relative;
                top: 10px;
                left: 50px;
                width: 540px;
                min-height: 450px;
                height: auto !important;
                height: 450px; /* min-height for IE */
            }

            .homeContent .homeMainContent {
                top: 15px !important;
            }

        div.homeMenu {
            line-height: inherit;
            margin: 0 5px 10px 5px;
            border-top: 1px dotted #666;
        }

            div.homeMenu ul.homeMenuList {
                width: 100%;
                list-style: none;
                border-bottom: 1px dotted #666;
                margin: 0;
            }

                div.homeMenu ul.homeMenuList li {
                    float: left;
                    /*width: 33%; 3 column*/
                    /*width: 33%; 3 column*/
                    /*width: 50%; 2 column*/
                    width: 100%; /* 1 column*/
                    position: relative;
                }

                div.homeMenu ul.homeMenuList li {
                    float: left;
                    /*width: 33%; 3 column*/
                    /*width: 50%; 2 column*/
                    width: 100%; /* 1 column*/
                    position: relative;
                }

                div.homeMenu ul.homeMenuList li {
                    float: left;
                    /*width: 33%; 3 column*/
                    /*width: 50%; 2 column*/
                    width: 100%; /* 1 column*/
                    position: relative;
                }

                    div.homeMenu ul.homeMenuList li a.wrap {
                        display: block;
                    }

                        div.homeMenu ul.homeMenuList li a.wrap:hover {
                            background: #ffc;
                        }

                    div.homeMenu ul.homeMenuList li dl {
                        padding: 5px;
                    }

                    div.homeMenu ul.homeMenuList li dt {
                        font-size: 14px;
                        font-weight: bold;
                        margin-bottom: 10px;
                    }

                    div.homeMenu ul.homeMenuList li dd {
                        font-size: 11px;
                        margin-bottom: 2px;
                    }

                    div.homeMenu ul.homeMenuList li a.wrap strong {
                        font-size: 14px;
                        padding: 5px 10px 0;
                        display: block;
                    }

                    div.homeMenu ul.homeMenuList li a.wrap em {
                        font-size: 11px;
                        font-style: normal;
                        color: #333;
                        padding: 0 10px 10px;
                        display: block;
                    }

        #wrap {
            position: relative;
            background-image: url(images/wrap.gif);
            background-color: #fff;
        }

        /* clearer */

        /* clearfix */
        .clearfix:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        .clearfix {
            display: inline-table;
            min-height: 1%;
        }
        /* Hides from IE-mac \*/
        * html .clearfix {
            height: 1%;
        }

        .clearfix {
            display: block;
        }
        /* End hide from IE-mac */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <a class="btn btn-dark" data-toggle="modal" data-target=".modal-update-dashboard">KEMASKINI PERAKAUNAN</a>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <br />
    </div>
    <!-- top tiles -->
    <div class="row tile_count">
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-import green"></i>ASET</span>
            <div class="count green"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(jumaset)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export blue"></i>EKUITI</span>
            <div class="count blue"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(jumekuiti)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export orange"></i>LIABILITI</span>
            <div class="count orange"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(jumliabiliti)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export dark"></i>UNTUNG/RUGI</span>
            <div class="count dark"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(jumhasil-jumbelanja)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
    </div>
    <!-- /top tiles -->
    <!-- Accounting Modules -->
    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Konfigurasi / Configuration</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('COAPage.aspx?sTabMenu=1.1');">
                                    <strong>Carta Akaun / Chart of Account (COA)</strong>
                                    <em>Struktur carta akaun dan senarai kod akaun yang diperlukan bagi tujuan pelaporan urusniaga kewangan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('BankPage.aspx?sTabMenu=1.2');">
                                    <strong>Senarai Bank / Bank Details </strong>
                                    <em>Senarai bank yang digunakan bagi pengrekodan hasil dan belanja melalui tunai di bank</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('BPIDPage.aspx?sTabMenu=1.3');">
                                    <strong>Senarai Rakan Niaga (Pelanggan & Pembekal) / Customer & Supplier Details </strong>
                                    <em>Senarai maklumat perseorangan atau agensi sebagai pelanggan atau yang berhutang dengan syarikat / sebagai pembekal atau yang memberi hutang kepada syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('InventoryPage.aspx?sTabMenu=1.4');">
                                    <strong>Senarai Inventori / Inventory Details </strong>
                                    <em>Senarai stok dan inventori yang menjadi modal milikan bagi urusniaga transaksi kepada syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Senarai Pelaburan / Investment Details </strong>
                                    <em>Senarai pelaburan yang dilabur oleh syarikat sebagai meraih keuntungan atas dividen, hibah dan sebagainya</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('AssetPage.aspx?sTabMenu=1.6');">
                                    <strong>Senarai Hartanah, Loji & Peralatan / Land, Plant & Equipment Details </strong>
                                    <em>Senarai aset alih dan aset tak alih yang menjadi milikan kepada syarikat</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Pentadbiran / Administration</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('OBCBPage.aspx?sTabMenu=2.1');">
                                    <strong>Pembukaan & Penutupan Baki Akaun / Opening & Closing Balance</strong>
                                    <em>Baki jumlah dana yang dimiliki syarikat pada bermula dan berakhir tempoh perakaunan untuk sesuatu tahun kewangan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('PostingPage.aspx?sTabMenu=2.2');">
                                    <strong>Maklumat Posting Data / Posting Details</strong>
                                    <em>Proses permindahan data/ maklumat transaksi dari operasi syarikat ke lejer akaun bagi tujuan pengrekodan penyata kewangan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Penyesuaian Penyata Bank / Bank Statement Reconciliation</strong>
                                    <em>Proses pemadanan transaksi dalam rekod perakaunan kewangan syarikat dengan maklumat yang terdapat pada penyata kewangan bank</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Lejar AM / General Ledger (GL)</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('JournalVoucherPage.aspx?sTabMenu=3.1');">
                                    <strong>Baucar Jurnal / Journal Voucher</strong>
                                    <em>Baucar jurnal disediakan bagi tujuan memproses catatan, peruntukan dan pembetulan transaksi perakaunan semasa yang mana kaedah kemasukan tidak melalui transaksi operasi syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('PaymentVoucherPage.aspx?sTabMenu=3.2');">
                                    <strong>Baucar Bayaran / Payment Voucher</strong>
                                    <em>Baucar pembayaran adalah permintaan yang dikeluarkan untuk pembayaran dan dikeluarkan sebelum pembayaran</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('ReceiptVoucherPage.aspx?sTabMenu=3.3');">
                                    <strong>Baucar Terimaan / Receipt Voucher</strong>
                                    <em>Baucar penerimaan adalah pengesahan pembayaran dan dikeluarkan selepas pembayaran</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('CashVoucherPage.aspx?sTabMenu=3.4');">
                                    <strong>Baucar Tunai / Cash Voucher</strong>
                                    <em>Baucar tunai disediakan untuk penerimaan atau pembayaran secara tunai samada secara debit atau kredit</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('BankVoucherPage.aspx?sTabMenu=3.5');">
                                    <strong>Baucar Bank / Bank Voucher</strong>
                                    <em>Baucar bank disediakan untuk penerimaan atau pembayaran secara perbankan (EFT/CEK/INTERNET BANKING) samada secara debit atau kredit</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Nota Debit / Debit Note</strong>
                                    <em>Nota debit (juga dikenal sebagai memo debit) dikeluarkan dari syarikat kepada pembekal untuk menunjukkan atau meminta pengembalian dana kerana barang yang salah atau rosak yang diterima, pembatalan pembelian, atau keadaan lain yang ditentukan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Nota Kredit / Credit Note</strong>
                                    <em>Nota kredit (juga dikenali sebagai memo kredit) dikeluarkan oleh syarikat untuk menunjukkan pengembalian dana sekiranya berlaku kesilapan invois, produk yang salah atau rosak, pembatalan pembelian, atau keadaan lain yang dinyatakan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('LedgerQueryPage.aspx?sTabMenu=3.8');">
                                    <strong>Carian Lejer / Ledger Query</strong>
                                    <em>Carian ke atas lejer bagi tujuan untuk menyenaraikan semua data/ maklumat transaksi ke atas lejar untuk sesuatu tahun kewangan</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Akaun Terimaan / Accounts Receivable (AR)</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('InvoiceIssuedPage.aspx?sTabMenu=4.1');"">
                                    <strong>Invois Dikeluarkan / Invoice Issued</strong>
                                    <em>Inbois (Sales Invoice) adalah dokumen yang dikeluarkan kepada pelanggan yang menentukan jumlah dan kos produk atau perkhidmatan yang telah disediakan oleh syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('PaymentReceiptPage.aspx?sTabMenu=4.2');">
                                    <strong>Bayaran Terima / Payment Receipt</strong>
                                    <em>Proses pengrekodan bagi membuktikan pelanggan telah membuat pembayaran pada penerimaan produk atau perkhidmatan tertentu yang telah disediakan oleh syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('AdvancedPaymentReceivedPage.aspx?sTabMenu=4.3');">
                                    <strong>Bayaran Pendahuluan / Advance Payment Receipt</strong>
                                    <em>Bayaran pendahuluan atau wang pendahuluan adalah sejumlah wang yang dibayar oleh pelanggan kepada syarikat sebelum penghantaran produk atau perkhidmatan yang telah dibeli. Amaun selebihnya akan dibayar setelah penghantaran dibuat atau di kemudian hari yang dinyatakan dalam kontrak undang-undang</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('AccountReceivedBadDebtPage.aspx?sTabMenu=4.4');">
                                    <strong>Hutang Lapuk Akaun Terimaan / AR Bad Debt</strong>
                                    <em>Hutang lapuk akaun terimaan dirujuk sebagai perbelanjaan ke atas akaun pelanggan yang tidak dapat ditagih atau perbelanjaan ke atas akaun ragu pelanggan</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Akaun Bayaran / Accounts Payable (AP)</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('BillReceivedPage.aspx?sTabMenu=5.1');">
                                    <strong>Bil Diterima/ Bill Received</strong>
                                    <em>Bil (Purchase Invoice) adalah dokumen yang diterima daripada pembekal yang menentukan jumlah dan kos produk atau perkhidmatan yang telah disediakan kepada syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('PaymentPaidPage.aspx?sTabMenu=5.2');">
                                    <strong>Bayaran Belanja / Payment Paid</strong>
                                    <em>Proses pengrekodan bagi membuktikan syarikat telah membuat pembayaran pada penerimaan produk atau perkhidmatan tertentu yang telah disediakan oleh pembekal</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('AdvancedPaymentPaidPage.aspx?sTabMenu=5.3');">
                                    <strong>Belanja Pendahuluan / Advance Payment Paid</strong>
                                    <em>Bayaran pendahuluan atau wang pendahuluan adalah sejumlah wang yang dibayar oleh syarikat kepada pembekal sebelum penghantaran produk atau perkhidmatan diterima oleh syarikat. Amaun selebihnya akan dibayar setelah penghantaran diterima atau di kemudian hari yang dinyatakan dalam kontrak undang-undang</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('AccountPaymentBadDebtPage.aspx?sTabMenu=5.4');">
                                    <strong>Hutang Lapuk Akaun Bayaran / AP Bad Debt</strong>
                                    <em>Hutang lapuk akaun bayaran dirujuk sebagai perbelanjaan ke atas akaun pembekal yang tidak dapat dilangsaikan atau perbelanjaan ke atas akaun ragu pembekal</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Pelaporan / Reporting</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                </ul>
            </div>
            <div class="x_content">
                <div class="homeContent">
                    <div class="homeMenu">
                        <ul class="homeMenuList clearfix" style="padding-left: initial;">
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('TrialBalancePage.aspx?sTabMenu=6.1');">
                                    <strong>Penyata Lembaran Imbangan / Trial Balance</strong>
                                    <em>Penyata lembaran imbangan adalah percubaan ke atas baki debit dan kredit akaun-akaun lejer syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('ProfitLossPage.aspx?sTabMenu=6.2');">
                                    <strong>Penyata Pendapatan / Profit & Loss (P&L)</strong>
                                    <em>Penyata pendapatan adalah penyata untung & rugi yang merangkumi pendapatan, kos dan perbelanjaan kepada syarikat</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openaccountingpage('BalanceSheetPage.aspx?sTabMenu=6.2');">
                                    <strong>Penyata Kedudukan Kewangan / Balance Sheet</strong>
                                    <em>Penyata kedudukan kewangan adalah penyata kewangan yang melaporkan aset, liabiliti, dan ekuiti pemegang saham atau modal kepada syarikat serta menunjukan kedudukan dan prestasi perniagaan syarikat untuk sesuatu tahun kewangan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Penyata Akaun Bank / Bank Account Statement</strong>
                                    <em>View and manage your accounts, make payments, and transfer funds</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Penyata Akaun Pelanggan / Customer Account Statement</strong>
                                    <em>Penyata akaun adalah dokumen yang menyenaraikan urusniaga antara pelanggan dan boleh digunakan secara berkala untuk memantau akaun pelanggan</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="overlay();">
                                    <strong>Penyata Akaun Pembekal / Supplier Account Statement</strong>
                                    <em>Penyata akaun adalah dokumen yang menyenaraikan urusniaga antara pembekal dan boleh digunakan secara berkala untuk memantau akaun pembekal</em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Update Dashboard -->
    <!--BEGIN dialog box for update dashboard report data-->
    <div class="modal fade modal-update-dashboard" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">Kemaskini Data Perakaunan?</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnUpdateDashboard" onclick="actionclick('UPDATE_DASHBOARD');">Submit</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                </div>

            </div>
        </div>
    </div>
    <!--END dialog box for update dashboard report data-->

    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
            <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
        </div>
    </form>


    <script>
        $(document).ready(function () {
            <%
        if (lsFisCOATran.Count == 0)
        {
            %>
            alert('<%=sCurrComp%> : Tahun Kewangan Semasa Belum didaftarkan!');
            <%
        }
            %>

        });
    </script>

    <script type="text/javascript">
        function openaccountingpage(url) {
            var popupWindow = window.open("Accounting/"+url, "accounting_page", "toolbar=0,location=0,status=0,menubar=0,resizable=1,scrollbars=1,width=1200,height=900");
            if (popupWindow == null) {
                alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
            } else {
                wleft = (screen.width - 1200) / 2;
                wtop = (screen.height - 900) / 2;
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

