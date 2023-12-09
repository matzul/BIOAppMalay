<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard1.aspx.cs" Inherits="Dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <%if(sUserType.Equals("01")){ %>
          <div class="col-md-12 col-sm-12 col-xs-12">
              <a class="btn btn-dark" data-toggle="modal" data-target=".modal-update-dashboard">Kemaskini Paparan Muka</a>
          </div>
        <%} %>
          <!-- top tiles -->
          <div class="row tile_count">
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import blue"></i> CADANGAN PENDAPATAN (RM)</span>
              <div class="count blue"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModRevenuePlan.GetSetTODATE)) %></div>
              <span class="count_bottom">Tahun [<%=sCurrFyr %>]</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import green"></i> PENDAPATAN SEBENAR (RM)</span>
              <div class="count green"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModRevenueActual.GetSetTODATE)) %></div>
              <span class="count_bottom">Tahun [<%=sCurrFyr %>]</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-export orange"></i> CADANGAN PERBELANJAAN (RM)</span>
              <div class="count orange"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModExpensesPlan.GetSetTODATE)) %></div>
              <span class="count_bottom">Tahun [<%=sCurrFyr %>]</span>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-export red"></i> PERBELANJAAN SEBENAR (RM)</span>
              <div class="count red"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModExpensesActual.GetSetTODATE)) %></div>
              <span class="count_bottom">Tahun [<%=sCurrFyr %>]</span>
            </div>
          </div>
          <!-- /top tiles -->
          <!-- revenue -->
          <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel fixed_height_390">
                  <div class="x_title">
                    <h2>Pendapatan <small>BULANAN [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <canvas id="revenueChart"></canvas>
                  </div>
                </div>
              </div>
          <!-- /revenue -->  
          <!-- Expenses -->
              <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel fixed_height_390">
                  <div class="x_title">
                    <h2>Perbelanjaan <small>BULANAN [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <canvas id="expensesChart"></canvas>
                  </div>
                </div>
              </div>
          <!-- /Expenses -->  
          <!-- Sales Orders -->
              <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel fixed_height_390">
                  <div class="x_title">
                    <h2>Jualan <small>KATEGORI [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                  <table class="table-responsive" style="width:100%">
                    <tr>
                      <td style="width: 37%;">
                        <canvas id="salesCanvas" height="100" width="100" style="margin: 15px 10px 10px 0;"></canvas>
                      </td>
                      <td>
                         <table class="table">
                          <thead>
                            <tr>
                              <th></th>
                              <th style="text-align:left">Kategori</th>
                              <th style="text-align:right"><%=getMonthText(int.Parse((sCurrAMon.Trim().Length>0?sCurrAMon:"0"))) %> (RM)</th>
                              <th style="text-align:right">Terkini (RM)</th>
                            </tr>
                          </thead>
                          <tbody>
                            <tr>
                              <td><i class="fa fa-square salesorder"></i></td>
                              <td style="text-align:left">Pesanan Jualan</td>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesOrderThisMonth.GetSettotalprice)) %></td>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesOrderToDate.GetSettotalprice)) %></td>
                            </tr>
                            <tr>
                              <td><i class="fa fa-square others"></i></td>
                              <td style="text-align:left">Lain-lain</td>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesAllThisMonth.GetSettotalprice-oModSalesOrderThisMonth.GetSettotalprice)) %></td>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesAllToDate.GetSettotalprice-oModSalesOrderToDate.GetSettotalprice)) %></td>
                            </tr>
                            <tr>
                              <th scope="row"></th>
                              <th style="text-align:left">TOTAL</th>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesAllThisMonth.GetSettotalprice)) %></td>
                              <td style="text-align:right"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModSalesAllToDate.GetSettotalprice)) %></td>
                            </tr>
                          </tbody>
                        </table>
                      </td>
                    </tr>
                  </table>
                  </div>
                </div>
              </div>
          <!-- /Sales Orders -->
          <!-- line graph -->
              <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="x_panel fixed_height_390">
                  <div class="x_title">
                    <h2>Jualan <small>BULANAN [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content2">
                    <!--IF USE MORRIS GRAPH, MUST DISABLE ECART GRAPH-->
                    <!--<div id="graph_line" style="width:100%; height:300px;"></div>-->
                    <div id="mainb" style="height:350px;"></div>
                  </div>
                </div>
              </div>
          <!-- /line graph -->
          <!-- Invoice & Collection -->
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Invois Jualan <small>Tahun [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content">
                    <div class="row top_tiles">
                      <div class="animated flipInY col-lg-3 col-md-3 col-sm-4 col-xs-12">
                          <div class="tile-stats">
                              <div class="icon"><i class="fa fa-calculator blue"></i></div>
                              <div class="count blue"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModRevenueActualSummary.GetSetTODATE)) %></div>
                              <p>TOTAL (RM)</p>
                              <h3>Jumlah Invois</h3>
                              <p>Tahun [<%=sCurrFyr %>]</p>
                          </div>
                      </div>
                      <div class="animated flipInY col-lg-3 col-md-3 col-sm-4 col-xs-12">
                          <div class="tile-stats">
                              <div class="icon"><i class="fa fa-thumbs-up green"></i></div>
                              <div class="count green"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModCollectionActualSummary.GetSetTODATE)) %></div>
                              <p>TOTAL (RM)</p>
                              <h3>Bayaran Terima</h3>
                              <p>Tahun [<%=sCurrFyr %>]</p>
                          </div>
                      </div>
                      <div class="animated flipInY col-lg-3 col-md-3 col-sm-4 col-xs-12">
                          <div class="tile-stats">
                              <div class="icon"><i class="fa fa-thumbs-down red"></i></div>
                              <div class="count red"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModRevenueActualSummary.GetSetTODATE-oModCollectionActualSummary.GetSetTODATE)) %></div>
                              <p>TOTAL (RM)</p>
                              <h3>Belum Terima</h3>
                              <p>Tahun [<%=sCurrFyr %>]</p>
                          </div>
                      </div>
                  </div>
                </div>
                </div>
              </div>
            <!--- -->
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_content">
                        <div class="table-responsive">
                          <table id="datatable" class="table table-striped jambo_table">
                            <thead>
                              <tr class="headings">
                                <th class="column-title" style="width:20%;">Pelanggan </th>
                                <th class="column-title" style="width:30%;">Alamat </th>
                                <th class="column-title" style="width:11%;">Tel. No. </th>
                                <th class="column-title" style="width:13%; text-align:right;">Jumlah Invois (RM) </th>
                                <th class="column-title" style="width:13%; text-align:right;">Bayaran Terima (RM)</th>
                                <th class="column-title" style="width:13%; text-align:right;">Belum Terima (RM)</th>
                              </tr>
                            </thead>

                            <tbody>
                        <%
                            if (lsPayRcptHeaderByBP.Count > 0)
                            {
                                for (int i = 0; i < lsPayRcptHeaderByBP.Count; i++)
                                {
                                    MainModel modPayRcptHdr = (MainModel)lsPayRcptHeaderByBP[i];
                        %>       
                              <tr class="even pointer">
                                <td class=" "><%=modPayRcptHdr.GetSetbpdesc %></td>
                                <td class=" "><%=modPayRcptHdr.GetSetbpaddress %></td>
                                <td class=" "><%=modPayRcptHdr.GetSetbpcontact %></td>
                                <td class=" " style="text-align:right;"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(modPayRcptHdr.GetSetinvoiceamount)) %></td>
                                <td class=" " style="text-align:right;"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(modPayRcptHdr.GetSetpayrcptamount)) %></td>
                                <td class=" " style="text-align:right;"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(modPayRcptHdr.GetSetinvoiceamount - modPayRcptHdr.GetSetpayrcptamount)) %></td>
                              </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td>Record not found...</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        <% 
                            }
                        %>
                            </tbody>
                          </table>
                        </div>

                  </div>
                </div>
              </div>
            <!-- -->
          <!-- Closing Cash Flow -->
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel fixed_height_390">
                  <div class="x_content">
                    <!--IF USE MORRIS GRAPH, MUST DISABLE ECART GRAPH-->
                    <!--<div id="graph_area" style="width:100%; height:300px;"></div>-->
                    <div id="echart_line" style="height:380px;"></div>
                  </div>
                </div>
              </div>


          <!-- Update Dashboard -->    
      
          <form id="form1" runat="server">
            <!--BEGIN dialog box for update dashboard report data-->
            <div class="modal fade modal-update-dashboard" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                            </button>
                            <h4 class="modal-title">Pilih Tahun & Bulan Untuk Dikemaskini</h4>
                        </div>
                        <div class="modal-body">
                        <div id="form2" class="form-horizontal form-label-left">

                            <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12">Tahun<span class="required">*</span></label>
                            <div class="col-md-9 col-sm-9 col-xs-12">
                                <select id="actualyear" name="actualyear" class="select2_single form-control" tabindex="-1" style="width:100%;" required="required">
                                <option>-select-</option>
                                <option value="2016">2016</option>
                                <option value="2017">2017</option>
                                <option value="2018">2018</option>
                                <option value="2019">2019</option>
                                <option value="2020">2020</option>
                                <option value="2021">2021</option>
                                <option value="2022">2022</option>
                                <option value="2023">2023</option>
                                </select>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="control-label col-md-3 col-sm-3 col-xs-12">Bulan<span class="required">*</span></label>
                            <div class="col-md-9 col-sm-9 col-xs-12">
                                <select id="actualmonth" name="actualmonth" class="select2_single form-control" tabindex="-1" style="width:100%;" required="required">
                                <option></option>
                                <option value="01">January</option>
                                <option value="02">February</option>
                                <option value="03">March</option>
                                <option value="04">April</option>
                                <option value="05">May</option>
                                <option value="06">June</option>
                                <option value="07">July</option>
                                <option value="08">August</option>
                                <option value="09">September</option>
                                <option value="10">October</option>
                                <option value="11">November</option>
                                <option value="12">December</option>
                                </select>
                            </div>
                            </div>
 
                        </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" id="btnUpdateDashboard" onclick="actionclick('UPDATE_DASHBOARD');">Submit</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                        </div>

                    </div>
                </div>
            </div>
            <!--END dialog box for update dashboard report data-->

            <div style="display: none;">
                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
            </div>
          </form>

    <!-- morris.js -->
    <script src="vendors/raphael/raphael.min.js"></script>
    <script src="vendors/morris.js/morris.min.js"></script>

    <!-- Chart.js -->
    <script src="vendors/Chart.js/dist/Chart.min.js"></script>

    <!-- ECharts -->
    <script src="vendors/echarts/dist/echarts.min.js"></script>
    <script src="vendors/echarts/map/js/world.js"></script>

    <!-- morris.js -->
    <script>
        $(document).ready(function () {
            <%
        if (oModFYR.GetSetfinanceyear.Trim().Length == 0)
        {
            %>
            alert('<%=sCurrComp%> : Tahun Kewangan Semasa Belum didaftarkan!');
            <%
        }
            %>
            /*
            Morris.Bar({
                element: 'graph_bar',
                data: [
                  { device: 'iPhone 4', geekbench: 380 },
                  { device: 'iPhone 4S', geekbench: 655 },
                  { device: 'iPhone 3GS', geekbench: 275 },
                  { device: 'iPhone 5', geekbench: 1571 },
                  { device: 'iPhone 5S', geekbench: 655 },
                  { device: 'iPhone 6', geekbench: 2154 },
                  { device: 'iPhone 6 Plus', geekbench: 1144 },
                  { device: 'iPhone 6S', geekbench: 2371 },
                  { device: 'iPhone 6S Plus', geekbench: 1471 },
                  { device: 'Other', geekbench: 1371 }
                ],
                xkey: 'device',
                ykeys: ['geekbench'],
                labels: ['Geekbench'],
                barRatio: 0.4,
                barColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                xLabelAngle: 35,
                hideHover: 'auto',
                resize: true
            });
            */
            /*
            Morris.Bar({
                element: 'graph_bar_group',
                data: [
                  { "period": "2016-10-01", "licensed": 807, "sorned": 660 },
                  { "period": "2016-09-30", "licensed": 1251, "sorned": 729 },
                  { "period": "2016-09-29", "licensed": 1769, "sorned": 1018 },
                  { "period": "2016-09-20", "licensed": 2246, "sorned": 1461 },
                  { "period": "2016-09-19", "licensed": 2657, "sorned": 1967 },
                  { "period": "2016-09-18", "licensed": 3148, "sorned": 2627 },
                  { "period": "2016-09-17", "licensed": 3471, "sorned": 3740 },
                  { "period": "2016-09-16", "licensed": 2871, "sorned": 2216 },
                  { "period": "2016-09-15", "licensed": 2401, "sorned": 1656 },
                  { "period": "2016-09-10", "licensed": 2115, "sorned": 1022 }
                ],
                xkey: 'period',
                barColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                ykeys: ['licensed', 'sorned'],
                labels: ['Licensed', 'SORN'],
                hideHover: 'auto',
                xLabelAngle: 60,
                resize: true
            });
            */
            /*
            Morris.Bar({
                element: 'graphx',
                data: [
                  { x: '2015 Q1', y: 2, z: 3, a: 4 },
                  { x: '2015 Q2', y: 3, z: 5, a: 6 },
                  { x: '2015 Q3', y: 4, z: 3, a: 2 },
                  { x: '2015 Q4', y: 2, z: 4, a: 5 }
                ],
                xkey: 'x',
                ykeys: ['y', 'z', 'a'],
                barColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                hideHover: 'auto',
                labels: ['Y', 'Z', 'A'],
                resize: true
            }).on('click', function (i, row) {
                console.log(i, row);
            });
            */
            /*
            Morris.Area({
                element: 'graph_area',
                data: [
                  { period: '2014 Q1', iphone: 2666, ipad: null, itouch: 2647 },
                  { period: '2014 Q2', iphone: 2778, ipad: 2294, itouch: 2441 },
                  { period: '2014 Q3', iphone: 4912, ipad: 1969, itouch: 2501 },
                  { period: '2014 Q4', iphone: 3767, ipad: 3597, itouch: 5689 },
                  { period: '2015 Q1', iphone: 6810, ipad: 1914, itouch: 2293 },
                  { period: '2015 Q2', iphone: 5670, ipad: 4293, itouch: 1881 },
                  { period: '2015 Q3', iphone: 4820, ipad: 3795, itouch: 1588 },
                  { period: '2015 Q4', iphone: 15073, ipad: 5967, itouch: 5175 },
                  { period: '2016 Q1', iphone: 10687, ipad: 4460, itouch: 2028 },
                  { period: '2016 Q2', iphone: 8432, ipad: 5713, itouch: 1791 }
                ],
                xkey: 'period',
                ykeys: ['iphone', 'ipad', 'itouch'],
                lineColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                labels: ['iPhone', 'iPad', 'iPod Touch'],
                pointSize: 2,
                hideHover: 'auto',
                resize: true
            });
            */
            /*
            Morris.Donut({
                element: 'graph_donut',
                data: [
                  { label: 'Jam', value: 25 },
                  { label: 'Frosted', value: 40 },
                  { label: 'Custard', value: 25 },
                  { label: 'Sugar', value: 10 }
                ],
                colors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                formatter: function (y) {
                    return y + "%";
                },
                resize: true
            });
            */
            /*
            Morris.Line({
                element: 'graph_line',
                xkey: 'year',
                ykeys: ['value'],
                labels: ['Value'],
                hideHover: 'auto',
                lineColors: ['#26B99A', '#34495E', '#ACADAC', '#3498DB'],
                data: [
                  { year: '2012', value: 20 },
                  { year: '2013', value: 10 },
                  { year: '2014', value: 5 },
                  { year: '2015', value: 5 },
                  { year: '2016', value: 20 }
                ],
                resize: true
            });
            */
        });
    </script>
    <!-- /morris.js -->

    <!-- Chart.js -->
    <script>
      Chart.defaults.global.legend = {
        enabled: false
      };

      // Bar chart
      var ctxRev = document.getElementById("revenueChart");
      var mybarChart = new Chart(ctxRev, {
        type: 'bar',
        data: {
            labels: ["<%=oModRevenuePlan.GetSetMON01desc%>", "<%=oModRevenuePlan.GetSetMON02desc%>", "<%=oModRevenuePlan.GetSetMON03desc%>", "<%=oModRevenuePlan.GetSetMON04desc%>", "<%=oModRevenuePlan.GetSetMON05desc%>", "<%=oModRevenuePlan.GetSetMON06desc%>", "<%=oModRevenuePlan.GetSetMON07desc%>", "<%=oModRevenuePlan.GetSetMON08desc%>", "<%=oModRevenuePlan.GetSetMON09desc%>", "<%=oModRevenuePlan.GetSetMON10desc%>", "<%=oModRevenuePlan.GetSetMON11desc%>", "<%=oModRevenuePlan.GetSetMON12desc%>"],
          datasets: [{
            label: 'Plan (RM)',
            backgroundColor: "#3498DB",
            data: [<%=oModRevenuePlan.GetSetMON01%>, <%=oModRevenuePlan.GetSetMON02%>, <%=oModRevenuePlan.GetSetMON03%>, <%=oModRevenuePlan.GetSetMON04%>, <%=oModRevenuePlan.GetSetMON05%>, <%=oModRevenuePlan.GetSetMON06%>, <%=oModRevenuePlan.GetSetMON07%>, <%=oModRevenuePlan.GetSetMON08%>, <%=oModRevenuePlan.GetSetMON09%>, <%=oModRevenuePlan.GetSetMON10%>, <%=oModRevenuePlan.GetSetMON11%>, <%=oModRevenuePlan.GetSetMON12%>]
          }, {
            label: 'Actual (RM)',
            backgroundColor: "#1ABB9C",
            data: [<%=oModRevenueActual.GetSetMON01%>, <%=oModRevenueActual.GetSetMON02%>, <%=oModRevenueActual.GetSetMON03%>, <%=oModRevenueActual.GetSetMON04%>, <%=oModRevenueActual.GetSetMON05%>, <%=oModRevenueActual.GetSetMON06%>, <%=oModRevenueActual.GetSetMON07%>, <%=oModRevenueActual.GetSetMON08%>, <%=oModRevenueActual.GetSetMON09%>, <%=oModRevenueActual.GetSetMON10%>, <%=oModRevenueActual.GetSetMON11%>, <%=oModRevenueActual.GetSetMON12%>]
          }]
        },

        options: {
          scales: {
            yAxes: [{
              ticks: {
                beginAtZero: true
              }
            }]
          }
        }
      });

      // Bar chart
      var ctxExp = document.getElementById("expensesChart");
      var mybarChart = new Chart(ctxExp, {
          type: 'bar',
          data: {
              labels: ["<%=oModExpensesPlan.GetSetMON01desc%>", "<%=oModExpensesPlan.GetSetMON02desc%>", "<%=oModExpensesPlan.GetSetMON03desc%>", "<%=oModExpensesPlan.GetSetMON04desc%>", "<%=oModExpensesPlan.GetSetMON05desc%>", "<%=oModExpensesPlan.GetSetMON06desc%>", "<%=oModExpensesPlan.GetSetMON07desc%>", "<%=oModExpensesPlan.GetSetMON08desc%>", "<%=oModExpensesPlan.GetSetMON09desc%>", "<%=oModExpensesPlan.GetSetMON10desc%>", "<%=oModExpensesPlan.GetSetMON11desc%>", "<%=oModExpensesPlan.GetSetMON12desc%>"],
              datasets: [{
                  label: 'Plan (RM)',
                  backgroundColor: "#F39C12",
                  data: [<%=oModExpensesPlan.GetSetMON01%>, <%=oModExpensesPlan.GetSetMON02%>, <%=oModExpensesPlan.GetSetMON03%>, <%=oModExpensesPlan.GetSetMON04%>, <%=oModExpensesPlan.GetSetMON05%>, <%=oModExpensesPlan.GetSetMON06%>, <%=oModExpensesPlan.GetSetMON07%>, <%=oModExpensesPlan.GetSetMON08%>, <%=oModExpensesPlan.GetSetMON09%>, <%=oModExpensesPlan.GetSetMON10%>, <%=oModExpensesPlan.GetSetMON11%>, <%=oModExpensesPlan.GetSetMON12%>]
              }, {
                  label: 'Actual (RM)',
                  backgroundColor: "#E74C3C",
                  data: [<%=oModExpensesActual.GetSetMON01%>, <%=oModExpensesActual.GetSetMON02%>, <%=oModExpensesActual.GetSetMON03%>, <%=oModExpensesActual.GetSetMON04%>, <%=oModExpensesActual.GetSetMON05%>, <%=oModExpensesActual.GetSetMON06%>, <%=oModExpensesActual.GetSetMON07%>, <%=oModExpensesActual.GetSetMON08%>, <%=oModExpensesActual.GetSetMON09%>, <%=oModExpensesActual.GetSetMON10%>, <%=oModExpensesActual.GetSetMON11%>, <%=oModExpensesActual.GetSetMON12%>]
              }]
          },

          options: {
              scales: {
                  yAxes: [{
                      ticks: {
                          beginAtZero: true
                      }
                  }]
              }
          }
      });


    </script>
    <!-- /Chart.js -->

    <!-- Doughnut Chart -->
    <script>

        // Doughnut chart
        var ctx = document.getElementById("salesCanvas");
        var data = {
            labels: [
              "Sales Order",
              "Others"
            ],
            datasets: [{
                data: [<%=oModSalesOrderToDate.GetSettotalprice %>, <%=oModSalesAllToDate.GetSettotalprice-oModSalesOrderToDate.GetSettotalprice %>],
                backgroundColor: [
                  "#5c5c5c",
                  "#BDC3C7"
                ],
                hoverBackgroundColor: [
                  "#888888",
                  "#d0d8dd"
                ]

            }]
        };

        var canvasDoughnut = new Chart(ctx, {
            type: 'doughnut',
            tooltipFillColor: "rgba(51, 51, 51, 0.55)",
            data: data
        });
       
    </script>
    <!-- /Doughnut Chart -->

    <!-- eChartLine -->
    <script>

        var theme = {
            color: [
                '#26B99A', '#34495E', '#BDC3C7', '#3498DB',
                '#9B59B6', '#8abb6f', '#759c6a', '#bfd3b7'
            ],

            title: {
                itemGap: 8,
                textStyle: {
                    fontWeight: 'normal',
                    color: '#408829'
                }
            },

            dataRange: {
                color: ['#1f610a', '#97b58d']
            },

            toolbox: {
                color: ['#408829', '#408829', '#408829', '#408829']
            },

            tooltip: {
                backgroundColor: 'rgba(0,0,0,0.5)',
                axisPointer: {
                    type: 'line',
                    lineStyle: {
                        color: '#408829',
                        type: 'dashed'
                    },
                    crossStyle: {
                        color: '#408829'
                    },
                    shadowStyle: {
                        color: 'rgba(200,200,200,0.3)'
                    }
                }
            },

            dataZoom: {
                dataBackgroundColor: '#eee',
                fillerColor: 'rgba(64,136,41,0.2)',
                handleColor: '#408829'
            },
            grid: {
                borderWidth: 0
            },

            categoryAxis: {
                axisLine: {
                    lineStyle: {
                        color: '#408829'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: ['#eee']
                    }
                }
            },

            valueAxis: {
                axisLine: {
                    lineStyle: {
                        color: '#408829'
                    }
                },
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: ['#eee']
                    }
                }
            },
            timeline: {
                lineStyle: {
                    color: '#408829'
                },
                controlStyle: {
                    normal: { color: '#408829' },
                    emphasis: { color: '#408829' }
                }
            },

            k: {
                itemStyle: {
                    normal: {
                        color: '#68a54a',
                        color0: '#a9cba2',
                        lineStyle: {
                            width: 1,
                            color: '#408829',
                            color0: '#86b379'
                        }
                    }
                }
            },
            map: {
                itemStyle: {
                    normal: {
                        areaStyle: {
                            color: '#ddd'
                        },
                        label: {
                            textStyle: {
                                color: '#c12e34'
                            }
                        }
                    },
                    emphasis: {
                        areaStyle: {
                            color: '#99d2dd'
                        },
                        label: {
                            textStyle: {
                                color: '#c12e34'
                            }
                        }
                    }
                }
            },
            force: {
                itemStyle: {
                    normal: {
                        linkStyle: {
                            strokeColor: '#408829'
                        }
                    }
                }
            },
            chord: {
                padding: 4,
                itemStyle: {
                    normal: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        },
                        chordStyle: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            }
                        }
                    },
                    emphasis: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        },
                        chordStyle: {
                            lineStyle: {
                                width: 1,
                                color: 'rgba(128, 128, 128, 0.5)'
                            }
                        }
                    }
                }
            },
            gauge: {
                startAngle: 225,
                endAngle: -45,
                axisLine: {
                    show: true,
                    lineStyle: {
                        color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                        width: 8
                    }
                },
                axisTick: {
                    splitNumber: 10,
                    length: 12,
                    lineStyle: {
                        color: 'auto'
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: 'auto'
                    }
                },
                splitLine: {
                    length: 18,
                    lineStyle: {
                        color: 'auto'
                    }
                },
                pointer: {
                    length: '90%',
                    color: 'auto'
                },
                title: {
                    textStyle: {
                        color: '#333'
                    }
                },
                detail: {
                    textStyle: {
                        color: 'auto'
                    }
                }
            },
            textStyle: {
                fontFamily: 'Arial, Verdana, sans-serif'
            }
        };


        var echartBar = echarts.init(document.getElementById('mainb'), theme);

        echartBar.setOption({
            title: {
                text: 'Jumlah Jualan',
                subtext: ''
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['Sales']
            },
            toolbox: {
                show: false
            },
            calculable: false,
            xAxis: [{
                type: 'category',
                data: ['<%=oModSalesActual.GetSetMON01desc%>', '<%=oModSalesActual.GetSetMON02desc%>', '<%=oModSalesActual.GetSetMON03desc%>', '<%=oModSalesActual.GetSetMON04desc%>', '<%=oModSalesActual.GetSetMON05desc%>', '<%=oModSalesActual.GetSetMON06desc%>', '<%=oModSalesActual.GetSetMON07desc%>', '<%=oModSalesActual.GetSetMON08desc%>', '<%=oModSalesActual.GetSetMON09desc%>', '<%=oModSalesActual.GetSetMON10desc%>', '<%=oModSalesActual.GetSetMON11desc%>', '<%=oModSalesActual.GetSetMON12desc%>']
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [{
                name: 'Jualan (RM)',
                type: 'bar',
                data: [<%=oModSalesActual.GetSetMON01%>, <%=oModSalesActual.GetSetMON02%>, <%=oModSalesActual.GetSetMON03%>, <%=oModSalesActual.GetSetMON04%>, <%=oModSalesActual.GetSetMON05%>, <%=oModSalesActual.GetSetMON06%>, <%=oModSalesActual.GetSetMON07%>, <%=oModSalesActual.GetSetMON08%>, <%=oModSalesActual.GetSetMON09%>, <%=oModSalesActual.GetSetMON10%>, <%=oModSalesActual.GetSetMON11%>, <%=oModSalesActual.GetSetMON12%>],
                markPoint: {
                    data: [{
                        <%if(sCurrFMon.Equals("01")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON01%>,
                        xAxis: 0,
                        yAxis: <%=oModSalesActual.GetSetMON01%>,
                        <%}else if(sCurrFMon.Equals("02")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON02%>,
                        xAxis: 1,
                        yAxis: <%=oModSalesActual.GetSetMON02%>,
                        <%}else if(sCurrFMon.Equals("03")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON03%>,
                        xAxis: 2,
                        yAxis: <%=oModSalesActual.GetSetMON03%>,
                        <%}else if(sCurrFMon.Equals("04")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON04%>,
                        xAxis: 3,
                        yAxis: <%=oModSalesActual.GetSetMON04%>,
                        <%}else if(sCurrFMon.Equals("05")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON05%>,
                        xAxis: 4,
                        yAxis: <%=oModSalesActual.GetSetMON05%>,
                        <%}else if(sCurrFMon.Equals("06")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON06%>,
                        xAxis: 5,
                        yAxis: <%=oModSalesActual.GetSetMON06%>,
                        <%}else if(sCurrFMon.Equals("07")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON07%>,
                        xAxis: 6,
                        yAxis: <%=oModSalesActual.GetSetMON07%>,
                        <%}else if(sCurrFMon.Equals("08")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON08%>,
                        xAxis: 7,
                        yAxis: <%=oModSalesActual.GetSetMON08%>,
                        <%}else if(sCurrFMon.Equals("09")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON09%>,
                        xAxis: 8,
                        yAxis: <%=oModSalesActual.GetSetMON09%>,
                        <%}else if(sCurrFMon.Equals("10")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON10%>,
                        xAxis: 9,
                        yAxis: <%=oModSalesActual.GetSetMON10%>,
                        <%}else if(sCurrFMon.Equals("11")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON11%>,
                        xAxis: 10,
                        yAxis: <%=oModSalesActual.GetSetMON11%>,
                        <%}else if(sCurrFMon.Equals("12")){%>
                        name: 'Amount',
                        value: <%=oModSalesActual.GetSetMON12%>,
                        xAxis: 11,
                        yAxis: <%=oModSalesActual.GetSetMON12%>,
                        <%}%>
                    }]
                },
                markLine: {
                    
                    data:[
                            [{name: 'Break Even', value: 5000, xAxis:0, yAxis:5000}, 
                            {name: 'BE', xAxis: 11, yAxis:5000}]
                        ]
                    
                    /*
                    data: [{
                        type: 'average',
                        name: 'average'
                    }]
                    */
                }
            }]
        });

        var echartLine = echarts.init(document.getElementById('echart_line'), theme);

        echartLine.setOption({
            title: {
                text: 'Aliran Kewangan',
                subtext: 'BY CLOSING DATE'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                x: 220,
                y: 40,
                data: ['Opening Amount', 'Closing Amount', 'Cash Closing']
            },
            toolbox: {
                show: true,
                feature: {
                    magicType: {
                        show: true,
                        title: {
                            line: 'Line',
                            bar: 'Bar'
                        },
                        type: ['line', 'bar']
                    },
                    restore: {
                        show: true,
                        title: "Restore"
                    },
                    saveAsImage: {
                        show: true,
                        title: "Save Image"
                    }
                }
            },
            calculable: true,
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                //data: ['<%=oModRevenueActual.GetSetMON01desc%>', '<%=oModRevenueActual.GetSetMON02desc%>', '<%=oModRevenueActual.GetSetMON03desc%>', '<%=oModRevenueActual.GetSetMON04desc%>', '<%=oModRevenueActual.GetSetMON05desc%>', '<%=oModRevenueActual.GetSetMON06desc%>', '<%=oModRevenueActual.GetSetMON07desc%>', '<%=oModRevenueActual.GetSetMON08desc%>', '<%=oModRevenueActual.GetSetMON09desc%>', '<%=oModRevenueActual.GetSetMON10desc%>', '<%=oModRevenueActual.GetSetMON11desc%>', '<%=oModRevenueActual.GetSetMON12desc%>']
                data: [
                      <% 
                        for(int i=0; i < lsClosingCashFlow.Count; i++){
                            MainModel oModCashFow = (MainModel)lsClosingCashFlow[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModCashFow.GetSetclosingdate %>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModCashFow.GetSetclosingdate %>'                              
                      <% 
                                
                            }
                        } 
                      %>
                      ]
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [{
                name: 'Opening Amount',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                //data: [<%=oModRevenueActual.GetSetMON01%>, <%=oModRevenueActual.GetSetMON02%>, <%=oModRevenueActual.GetSetMON03%>, <%=oModRevenueActual.GetSetMON04%>, <%=oModRevenueActual.GetSetMON05%>, <%=oModRevenueActual.GetSetMON06%>, <%=oModRevenueActual.GetSetMON07%>, <%=oModRevenueActual.GetSetMON08%>, <%=oModRevenueActual.GetSetMON09%>, <%=oModRevenueActual.GetSetMON10%>, <%=oModRevenueActual.GetSetMON11%>, <%=oModRevenueActual.GetSetMON12%>]
                data: [
                      <% 
                        for(int i=0; i < lsClosingCashFlow.Count; i++){
                            MainModel oModCashFow = (MainModel)lsClosingCashFlow[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModCashFow.GetSetbankopeningamount+oModCashFow.GetSetcashopeningamount%>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModCashFow.GetSetbankopeningamount+oModCashFow.GetSetcashopeningamount%>'                              
                      <% 
                                
                            }
                        } 
                      %>
                ]
            }, {
                name: 'Closing Amount',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                //data: [<%=oModCollectionActual.GetSetMON01%>, <%=oModCollectionActual.GetSetMON02%>, <%=oModCollectionActual.GetSetMON03%>, <%=oModCollectionActual.GetSetMON04%>, <%=oModCollectionActual.GetSetMON05%>, <%=oModCollectionActual.GetSetMON06%>, <%=oModCollectionActual.GetSetMON07%>, <%=oModCollectionActual.GetSetMON08%>, <%=oModCollectionActual.GetSetMON09%>, <%=oModCollectionActual.GetSetMON10%>, <%=oModCollectionActual.GetSetMON11%>, <%=oModCollectionActual.GetSetMON12%>]
                data: [
                      <% 
                        for(int i=0; i < lsClosingCashFlow.Count; i++){
                            MainModel oModCashFow = (MainModel)lsClosingCashFlow[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModCashFow.GetSetbankclosingamount+oModCashFow.GetSetcashclosingamount%>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModCashFow.GetSetbankclosingamount+oModCashFow.GetSetcashclosingamount%>'                              
                      <% 
                                
                            }
                        } 
                      %>
                ]
            }, {
                name: 'Cash Closing',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                //data: [<%=oModCashCollectionActual.GetSetMON01%>, <%=oModCashCollectionActual.GetSetMON02%>, <%=oModCashCollectionActual.GetSetMON03%>, <%=oModCashCollectionActual.GetSetMON04%>, <%=oModCashCollectionActual.GetSetMON05%>, <%=oModCashCollectionActual.GetSetMON06%>, <%=oModCashCollectionActual.GetSetMON07%>, <%=oModCashCollectionActual.GetSetMON08%>, <%=oModCashCollectionActual.GetSetMON09%>, <%=oModCashCollectionActual.GetSetMON10%>, <%=oModCashCollectionActual.GetSetMON11%>, <%=oModCashCollectionActual.GetSetMON12%>]
                data: [
                      <% 
                        for(int i=0; i < lsClosingCashFlow.Count; i++){
                            MainModel oModCashFow = (MainModel)lsClosingCashFlow[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModCashFow.GetSetcashclosingamount%>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModCashFow.GetSetcashclosingamount%>'                              
                      <% 
                                
                            }
                        } 
                      %>
                ]
            }]
        });

    </script>
    <!-- /eChartLine -->

    <script type="text/javascript">
                function actionclick(action) {
                    var proceed = true;

                    if ($('#actualyear').val().length == 0)
                    {
                        proceed = false;
                        new PNotify({
                            title: 'Alert',
                            text: 'Sila pilih [Tahun]',
                            type: 'warning',
                            styling: 'bootstrap3'
                        });
                    }
                    else if ($('#actualmonth').val().length == 0)
                    {
                        proceed = false;
                        new PNotify({
                            title: 'Alert',
                            text: 'Sila pilih [Bulan]',
                            type: 'warning',
                            styling: 'bootstrap3'
                        });
                    }

                    if (proceed) {
                        document.getElementById("hidAction").value = action;
                        var button = document.getElementById("<%=btnAction.ClientID %>");
                        button.click();
                    }
                }
    </script>         
</asp:Content>

