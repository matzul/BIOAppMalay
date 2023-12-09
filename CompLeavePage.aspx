<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="CompLeavePage.aspx.cs" Inherits="CompLeavePage" %>

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
        <a class="btn btn-dark" href="#">SISTEM PENGURUSAN CUTI</a>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <br />
    </div>
    <!-- top tiles -->
    <div class="row tile_count">
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-import gray"></i>HARI TAHUN</span>
            <div class="count gray"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumharitahun)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-import green"></i>HARI KERJA</span>
            <div class="count green"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumharikerja)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export blue"></i>HARI PERLEPASAN</span>
            <div class="count blue"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumhariperlepasan)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export orange"></i>CUTI SAKIT (MC)</span>
            <div class="count orange"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumcutimc)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export orange"></i>CUTI KECEMASAN</span>
            <div class="count orange"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumcutiemer)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 tile_stats_count">
            <span class="count_top dark"><i class="glyphicon glyphicon-export red"></i>CUTI TANPA REKOD</span>
            <div class="count red"><%=String.Format("{0:#,##0}",Convert.ToInt16(jumcutiunrecord)) %></div>
            <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
        </div>
    </div>
    <!-- /top tiles -->
    <!-- Attendance Modules -->
    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Konfigurasi & Pentadbiran</h2>
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
                                <a href="#" class="wrap" onclick="openleavepage('LeavePublicHoliday.aspx?sTabMenu=1');">
                                    <strong>Penyediaan Hari Perlepasan (Public Holiday)</strong>
                                    <em>Senarai hari perlepasan bagi tempoh satu tahun akan ditentukan dengan status OFFDAY. Namum begitu, sekiranya terdapat kakitangan yang mempunyai rekod kehadiran pada hari tersebut, status kekal kepada OFFDAY.</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openleavepage('LeaveCategoryGroup.aspx?sTabMenu=2');">
                                    <strong>Penyediaan Kategori & Jenis Cuti </strong>
                                    <em>Struktur kategori cuti serta jenis cuti yang dibenarkan. Bilangan hari cuti yang dibenarkan adalah mengikut kepada kategori dan jenis cuti tersebut.</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openleavepage('LeaveCategoryStaff.aspx?sTabMenu=3');">
                                    <strong>Penyediaan Kakitangan Kepada Kategori Cuti </strong>
                                    <em>Senarai kakitangan yang akan didaftarkan bilangan cuti meraka berdasarkan kepada kategori dan jenis cuti yang dibenarkan dalam tempoh satu tahun.</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" class="wrap" onclick="openleavepage('LeaveApplicationStaff.aspx?sTabMenu=4');">
                                    <strong>Permohonan Cuti Kakitangan Tahun <%=sCurrFyr %></strong>
                                    <em>Proses permohonan cuti yang dibenarkan sama ada cuti yang telah dirancang atau cuti kecemasan atas sebab-sebab tertentu yang tidak dapat dielakkan.</em>
                                </a>
                            </li>
                            <!--
                            <li>
                                <a href="#" class="wrap" onclick="openleavepage('LeaveApprovalStaff.aspx?sTabMenu=5');">
                                    <strong>Kelulusan Cuti Kakitangan Tahun 2022 </strong>
                                    <em>Proses kelulusan permohonan cuti yang dibenarkan bagi kakitangan yang layak.</em>
                                </a>
                            </li>                        
                            -->
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="x_panel">
            <div class="x_title" style="border: 0px;">
                <h2>Laporan & Statistik Maklumat Cuti</h2>
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
                                <a href="#" class="wrap" onclick="openleavepage('LeaveReportPage.aspx?sTabMenu=5');"">
                                    <strong>Laporan Cuti Kakitangan Tahun <%=sCurrFyr %></strong>
                                    <em>Terdapat beberapa jenis laporan yang anda boleh lihat. Untuk menyediakan, anda perlu membuat pilihan JENIS LAPORAN, dan seterusnya membuat pilihan SEMUA KAKITANGAN atau pilihan kakitangan tertentu.</em>
                                </a>
                            </li>
                            <li>
                                <a href="#" style="display:block; overflow: auto;" class="wrap" onclick="#">
                                    <strong>Statistik Maklumat Cuti Kakitangan Tahun <%=sCurrFyr %></strong>
                                    <em>Jadual data berkaitan bilangan hari cuti yang telah diluluskan. Senarai kakitangan yang mengambil cuti tersebut, dapat dilihat dengan menekan pada bilangan cuti tersebut.</em>
                                    <em>
                                        <table id="mytable" width="90%" border="1" cellspacing="1" cellpadding="2" class="tblBg fixed_header" align="left">
                                            <tbody>
                                                <tr class="tblTitle3Mod">
                                                    <td height="25px" width="40%" valign="middle" align="left" class="tblTitle3Mod">Kategori</td>
                                                    <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Tahun <%=sCurrFyr %></td>
                                                    <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Bulan Semasa</td>
                                                    <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Minggu Semasa</td>
                                                    <td width="15%" valign="middle" align="left" class="tblTitle3Mod">Hari Ini</td>
                                                </tr>
                                                <tr class="tblText1">
                                                    <td valign="top" align="left">Bilangan Hari Cuti Tahunan Diambil</td>
                                                    <td valign="top" align="right">20</td>
                                                    <td valign="top" align="right">1</td>
                                                    <td valign="top" align="right">2</td>
                                                    <td valign="top" align="right">0</td>
                                                </tr>
                                                <tr class="tblText1">
                                                    <td valign="top" align="left">Bilangan Hari Cuti Sakit (MC) Diambil</td>
                                                    <td valign="top" align="right">20</td>
                                                    <td valign="top" align="right">1</td>
                                                    <td valign="top" align="right">2</td>
                                                    <td valign="top" align="right">0</td>
                                                </tr>
                                                <tr class="tblText1">
                                                    <td valign="top" align="left">Bilangan Hari Cuti Kecemasan Diambil</td>
                                                    <td valign="top" align="right">20</td>
                                                    <td valign="top" align="right">1</td>
                                                    <td valign="top" align="right">2</td>
                                                    <td valign="top" align="right">0</td>
                                                </tr>
                                                <tr class="tblText1">
                                                    <td valign="top" align="left">Bilangan Hari Cuti Peristiwa Diambil</td>
                                                    <td valign="top" align="right">20</td>
                                                    <td valign="top" align="right">1</td>
                                                    <td valign="top" align="right">2</td>
                                                    <td valign="top" align="right">0</td>
                                                </tr>
                                                <tr class="tblText1">
                                                    <td valign="top" align="left">Bilangan Hari Cuti Tanpa Rekod Diambil</td>
                                                    <td valign="top" align="right">20</td>
                                                    <td valign="top" align="right">1</td>
                                                    <td valign="top" align="right">2</td>
                                                    <td valign="top" align="right">0</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </em>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
            <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
        </div>
    </form>


    <script>
        $(document).ready(function () {

        });
    </script>

    <script type="text/javascript">
        function openleavepage(url) {
            var popupWindow = window.open("HumanResource/"+url, "leave_page", "toolbar=0,location=0,status=0,menubar=0,resizable=1,scrollbars=1,width=1200,height=900");
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

