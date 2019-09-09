<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard3.aspx.cs" Inherits="Dashboard3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <%if(sUserType.Equals("01")||sUserType.Equals("02")){ %>
          <div class="col-md-12 col-sm-12 col-xs-12">
              <a class="btn btn-dark" data-toggle="modal" data-target=".modal-update-dashboard">Kemaskini Paparan Muka</a>
          </div>
        <%} %>
          <!-- top tiles -->
          <div class="row tile_count">
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import green"></i> TERIMAAN STOK/INVENTORI</span>
              <div class="count green"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModStockTransIN.GetSetTODATE)) %></div>
              <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-export red"></i> KELUARAN STOK/INVENTORI</span>
              <div class="count red"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModStockTransOUT.GetSetTODATE * -1)) %></div>
              <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-export blue"></i> KEDUDUKAN STOK/INVENTORI</span>
              <div class="count red"><%=String.Format("{0:#,##0.00}",Convert.ToDecimal(oModStockPosition.GetSetcostsoh)) %></div>
              <span class="count_bottom">Terkini [<%=sCurrFyr %>]</span>
            </div>
          </div>
          <!-- /top tiles -->
          <!-- stock value -->
          <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Kedudukan <small>STOK/INVENTORI [<%=sCurrFyr %>]</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                    <canvas id="stockChart"></canvas>
                  </div>
                </div>
              </div>
          <!-- /stock value -->  
          <!-- Closing Stock Value -->
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_content">
                    <!--IF USE MORRIS GRAPH, MUST DISABLE ECART GRAPH-->
                    <!--<div id="graph_area" style="width:100%; height:300px;"></div>-->
                    <div id="echart_line" style="height:580px;"></div>
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
      var ctxRev = document.getElementById("stockChart");
      var mybarChart = new Chart(ctxRev, {
        type: 'bar',
        data: {
            labels: ["<%=oModStockTransIN.GetSetMON01desc%>", "<%=oModStockTransIN.GetSetMON02desc%>", "<%=oModStockTransIN.GetSetMON03desc%>", "<%=oModStockTransIN.GetSetMON04desc%>", "<%=oModStockTransIN.GetSetMON05desc%>", "<%=oModStockTransIN.GetSetMON06desc%>", "<%=oModStockTransIN.GetSetMON07desc%>", "<%=oModStockTransIN.GetSetMON08desc%>", "<%=oModStockTransIN.GetSetMON09desc%>", "<%=oModStockTransIN.GetSetMON10desc%>", "<%=oModStockTransIN.GetSetMON11desc%>", "<%=oModStockTransIN.GetSetMON12desc%>"],
          datasets: [{
            label: 'Terimaan (RM)',
            backgroundColor: "#3498DB",
            data: [<%=oModStockTransIN.GetSetMON01%>, <%=oModStockTransIN.GetSetMON02%>, <%=oModStockTransIN.GetSetMON03%>, <%=oModStockTransIN.GetSetMON04%>, <%=oModStockTransIN.GetSetMON05%>, <%=oModStockTransIN.GetSetMON06%>, <%=oModStockTransIN.GetSetMON07%>, <%=oModStockTransIN.GetSetMON08%>, <%=oModStockTransIN.GetSetMON09%>, <%=oModStockTransIN.GetSetMON10%>, <%=oModStockTransIN.GetSetMON11%>, <%=oModStockTransIN.GetSetMON12%>]
          }, {
            label: 'Keluaran (RM)',
            backgroundColor: "#1ABB9C",
            data: [<%=oModStockTransOUT.GetSetMON01 * -1%>, <%=oModStockTransOUT.GetSetMON02 * -1%>, <%=oModStockTransOUT.GetSetMON03 * -1%>, <%=oModStockTransOUT.GetSetMON04 * -1%>, <%=oModStockTransOUT.GetSetMON05 * -1%>, <%=oModStockTransOUT.GetSetMON06 * -1%>, <%=oModStockTransOUT.GetSetMON07 * -1%>, <%=oModStockTransOUT.GetSetMON08 * -1%>, <%=oModStockTransOUT.GetSetMON09 * -1%>, <%=oModStockTransOUT.GetSetMON10 * -1%>, <%=oModStockTransOUT.GetSetMON11 * -1%>, <%=oModStockTransOUT.GetSetMON12 * -1%>]
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

        var echartLine = echarts.init(document.getElementById('echart_line'), theme);

        echartLine.setOption({
            title: {
                text: 'Penutupan Stok & Inventori',
                subtext: 'BY CLOSING DATE'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                x: 220,
                y: 40,
                data: ['Stock Opening Amount', 'Stock Closing Amount']
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
                data: [
                      <% 
                        for(int i=0; i < lsClosingStockValue.Count; i++){
                            MainModel oModStockState = (MainModel)lsClosingStockValue[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModStockState.GetSetclosingdate %>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModStockState.GetSetclosingdate %>'                              
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
                name: 'Stock Opening Amount',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                data: [
                      <% 
                        for(int i=0; i < lsClosingStockValue.Count; i++){
                            MainModel oModStockState = (MainModel)lsClosingStockValue[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModStockState.GetSetstockopeningamount%>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModStockState.GetSetstockopeningamount%>'                              
                      <% 
                                
                            }
                        } 
                      %>
                ]
            }, {
                name: 'Stock Closing Amount',
                type: 'line',
                smooth: true,
                itemStyle: {
                    normal: {
                        areaStyle: {
                            type: 'default'
                        }
                    }
                },
                data: [
                      <% 
                        for(int i=0; i < lsClosingStockValue.Count; i++){
                            MainModel oModStockState = (MainModel)lsClosingStockValue[i];
                            if (i == 0)
                            {
                      %>
                                '<%=oModStockState.GetSetstockclosingamount%>'
                      <% 
                            }
                            else
                            {
                      %>
                                , '<%=oModStockState.GetSetstockclosingamount%>'                              
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

