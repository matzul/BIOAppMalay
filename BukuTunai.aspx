<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="BukuTunai.aspx.cs" Inherits="BukuTunai" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="js/WebMethodService.Call.Helper.js"></script>
    <style>

         .modal {
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row tile_count">
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import orange"></i> BAKI BAWA</span>
              <div class="count orange"><b id="lblCashOpeningAmnt">0</b></div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import blue"></i> WANG MASUK</span>
              <div class="count blue"><b id="lblCashPayRcptAmnt">0</b></div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-export red"></i> WANG KELUAR</span>
              <div class="count red"><b id="lblCashPayPaidAmnt">0</b></div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6 tile_stats_count">
              <span class="count_top dark"><i class="glyphicon glyphicon-import green"></i> BAKI SEMASA</span>
              <div class="count green"><b id="lblCashClosingAmnt">0</b></div>
            </div>
            
            
               <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>INFO <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content table-responsive">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                    <a class="btn btn-app" id="btn_modalwangmasuk">
                      <i class="fa fa-sign-in green"></i>Wang Masuk 
                    </a>
                    <a class="btn btn-app" id="btn_modalwangkeluar">
                      <i class="fa fa-sign-out red"></i>Wang Keluar
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i>Carian
                    </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                        <section class="panel">

                            <div class="x_title">
                                <h2>Carian Transaksi</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">

                                    <label for="selectyear">Tahun:</label>
                                    <select class="form-control" id="selectyear" name="selectyear">
                                        <option >-Tahun-</option>
                                    </select>
                                    <label for="selectmonth">Tahun:</label>
                                    <select class="form-control" id="selectmonth" name="selectmonth">
                                        <option >-Bulan-</option>
                                    </select>
                                    <br/>
                                    <button type="button" onclick="searchbydate();" class="btn btn-primary">Cari</button>
                                    <button type="button" onclick="resetsearchbydate();" class="btn btn-warning">Reset</button>
                                    <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                
                                </form>
                            </div>
                        </section>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                    <table id="CashInOutList" class="table table-striped jambo_table">
                        <thead>
                            <tr>
                                <th style="display:none;">comp</th>
                                <th style="display:none;">type</th>
                                <th style="display:none;">payno</th>
                                <th style="display:none;">resno</th>
                                <th style="display:none;">date</th>
                                <th>Butiran</th>
                                <th>Jumlah</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                    <div class="toolbar"></div>
                    </div>
                  </div>
                </div>
            </div>

    </div>
    <!-- Modal -->
    <div class="modal fade" id="myDIV" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-body">
                    <div class="center-block"><img class="center-block" src="images/loader2.gif" width="32" height="32" alt=""/></div>
                    <p class="text-center"> Loading...</p>
                </div>

            </div>
        </div>
    </div>
    <!--modal cash in-->
    <div class="modal fade" id="modal-wangmasuk">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Duit Masuk</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="date">Tarikh <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                        <input type="text" class="form-control date-picker" id="inc_date" name="inc_date" placeholder="Masukkan Tarikh" readonly/>
                    </div>
                    <div class="form-group">
                        <label for="inc_ic">IC Pelanggan/Pembekal</label>
                        <input type="text" class="form-control" id="inc_ic" name="inc_ic" placeholder="Masukkan Ic"/>
                    </div>
                    <div class="form-group">
                        <label for="inc_desc">Nama Pelanggan/Pembekal</label>
                        <input type="text" class="form-control" id="inc_desc" name="inc_desc" placeholder="Masukkan Nama"/>
                    </div>
                    <div class="form-group">
                        <label for="inc_address">Alamat Pelanggan/Pembekal</label>
                        <input type="text" class="form-control" id="inc_address" name="inc_address" readonly />
                    </div>
                    <div class="form-group">
                        <label for="inc_amount">Jumlah</label>
                        <div class="input-group">
                            <span class="input-group-addon">RM</span>
                            <input type="text" class="form-control" id="inc_amount" name="inc_amount" placeholder="Jumlah"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Kaedah Bayaran</label>
                        <select class="form-control" id="inc_paytype" name="inc_paytype" style=" width: 100%;">
                            <option value="" selected="selected">-Sila Pilih-</option>
                            <option value="CASH">TUNAI</option>
                            <option value="BANKING">BANK (EFT/I-Banking/Kad)</option>
                            <!--<option value="BANK">BANK</option>-->
                            <option value="CEK">CEK</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="inc_amount">Kategori</label>
                        <input type="text" class="form-control" id="inc_cat" name="inc_cat" value='BAUCAR TERIMAAN' placeholder="BAUCAR TERIMAAN" readonly/>
                    </div>
                    <div class="form-group">
                        <label>Kumpulan/Jenis</label>
                        <select class="form-control" id="inc_type" name="inc_type" style=" width: 100%;">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="inc_item">Keterangan</label>
                        <select class="form-control" id="inc_item" name="inc_item" style="width: 100%;">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="inc_item">Catatan</label>
                        <input type="text" class="form-control" id="inc_remarks" name="inc_remarks" value="" style="width: 100%;"/>
                    </div>
                    <div class="form-group">
                        <label for="inc_status">Status</label>
                        <input type="text" class="form-control" id="inc_status" name="inc_status" value='NEW' disabled="disabled"/>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="closeincome" name="closeincome" class="btn btn-default pull-left" data-dismiss="modal">Tutup</button>
                    <button type="button" id="cancelincome" class="btn btn-danger" onclick="cancelinvoice()">Batal</button>
                    <button type="button" id="createincome" class="btn btn-default" onclick="createincome();">Tambah</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--end of modal cash in-->
    <!--modal cash out-->
    <div class="modal fade" id="modal-wangkeluar">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Duit Keluar</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="date">Tarikh <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                        <input type="text" class="form-control date-picker" id="exp_date" name="exp_date" placeholder="Masukkan Tarikh" readonly/>
                    </div>
                    <div class="form-group">
                        <label for="exp_desc">IC Pembekal</label>
                        <input type="text" class="form-control" id="exp_ic" name="exp_ic" placeholder="Masukkan Ic"/>
                    </div>
                    <div class="form-group">
                        <label for="exp_desc">Nama Pembekal</label>
                        <input type="text" class="form-control" id="exp_desc" name="exp_desc" placeholder="Masukkan Nama"/>
                    </div>
                    <div class="form-group">
                        <label for="exp_desc">Alamat Pembekal</label>
                        <input type="text" class="form-control" id="exp_address" name="exp_address" placeholder="" readonly />
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">RM</span>
                        <input type="text" class="form-control" id="exp_amount" name="exp_amount" placeholder="Jumlah"/>
                    </div>
                    <div class="form-group">
                        <label>Kaedah Bayaran</label>
                        <select class="form-control" id="exp_paytype" name="exp_paytype" style=" width: 100%;">
                            <option value="" selected="selected">-Sila Pilih-</option>
                            <option value="CASH">TUNAI</option>
                            <option value="BANKING">BANK (EFT/I-Banking/Kad)</option>
                            <!--<option value="BANK">BANK</option>-->
                            <option value="CEK">CEK</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="exp_amount">Kategori</label>
                        <input type="text" class="form-control" id="exp_cat" name="exp_cat" value='BAUCAR BAYARAN' placeholder="BAUCAR BAYARAN" readonly />
                    </div>
                    <div class="form-group">
                        <label>Kumpulan/Jenis</label>
                        <select class="form-control" id="exp_type" name="exp_type" style=" width: 100%;">
                        </select>
                    </div>
                    <div class="form-group">
                        <label>Keterangan</label>
                        <select class="form-control" id="exp_item" name="exp_item" style="width: 100%;">
                        </select>
                    </div>
                    <div class="form-group">
                        <label>Catatan</label>
                        <input type="text" class="form-control" id="exp_remarks" name="exp_remarks" value="" style="width: 100%;"/>
                    </div>
                    <div class="form-group">
                        <label for="exp_status">Status</label>
                        <input type="text" class="form-control" id="exp_status" name="exp_status" value='NEW' disabled="disabled"/>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="closeexpenses" name="closeexpenses" class="btn btn-default pull-left" data-dismiss="modal">Tutup</button>
                    <button type="button" id="cancelexpenses" class="btn btn-danger" onclick="cancelinvoice()">Batal</button>
                    <button type="button" id="createexpenses" class="btn btn-default" onclick="createexpenses()">Tambah</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--end of modal cash out-->
    <!--modal bplist-->
    <div class="modal fade" id="myBPList" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body" style="width:90%;">
                    <table id="tbBPList" class="table" style="width:100%;">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th style="display:none;">bpid</th>
                                <th style="display:none;">bpdesc</th>
                                <th style="display:none;">bpaddress</th>
                                <th style="display:none;">bptelno</th>
                                <th>Senarai Pelanggan</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Tutup</button>
                </div>
            </div>
        </div>
    </div>
    <!--end of modal bplist-->

    <script type="text/javascript">
        var reqyear;
        var currflag = false;
        var comp = '<%=sCurrComp%>';
        var userid = '<%=sCurrUserid%>';
        var successcreationinvoice = false;
        var successcreationinvoicedetails = false;
        var successcreationpayrcpt = false;
        var successcreationpayrcptdetails = false;
        var successcreationexpenses = false;
        var successcreationexpensesdetails = false;
        var successcreationpaypaid = false;
        var successcreationpaypaiddetails = false;
        var successcancelinvoice = false;
        var successcancelpayrcpt = false;
        var successcancelexpenses = false;
        var successcancelpaypaid = false;
        var cancelpayno = "";
        var canceltype = "";
        var cancelresno = "";
        var invoiceno = "";
        var payrcptno = "";
        var expensesno = "";
        var paypaidno = "";
        var oTable;
        var cashinouttype = "";

        var today = new Date();
        var selyear = today.getFullYear();
        var selmonth = today.getMonth() + 1;
        if (selmonth < 10) {
            selmonth = '0' + selmonth;
        }
        var selday = '01';

        var today = new Date();
        var updyear = today.getFullYear();
        var updmonth = today.getMonth() + 1;
        if (updmonth < 10) {
            updmonth = '0' + updmonth;
        } 

        function enabledisablesearchbox() {
            var sb = document.getElementById("search-box");
            cf = currflag;
            if (cf == false) {
                sb.style.display = "none";
                currflag = true;
            } else {
                sb.style.display = "";
                currflag = false;
            }
        }

        enabledisablesearchbox(currflag);

        function enabledisableinput(flag) {
            $('#inc_address').prop('disabled', flag);
            $('#inc_amount').prop('disabled', flag);
            $('#inc_paytype').prop('disabled', flag);
            $('#inc_cat').prop('disabled', flag);
            $('#inc_type').prop('disabled', flag);
            $('#inc_item').prop('disabled', flag);
            $('#inc_remarks').prop('disabled', flag);

            $('#createincome').prop('disabled', flag);

            $('#exp_address').prop('disabled', flag);
            $('#exp_amount').prop('disabled', flag);
            $('#exp_paytype').prop('disabled', flag);
            $('#exp_cat').prop('disabled', flag);
            $('#exp_type').prop('disabled', flag);
            $('#exp_item').prop('disabled', flag);
            $('#exp_remarks').prop('disabled', flag);

            $('#createexpenses').prop('disabled', flag);
        }

        $(document).ready(function () {

            $('#inc_date').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY HH:mm:ss",
                //format: "DD-MM-YYYY",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                //actionclick('OPEN');
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#exp_date').daterangepicker({
                singleDatePicker: true,
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY HH:mm:ss",
                //format: "DD-MM-YYYY",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                //actionclick('OPEN');
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('.select2').select2();

            
            $('#modal-wangmasuk').on('hidden.bs.modal', function () {
                clear();
            });
            $('#modal-wangkeluar').on('hidden.bs.modal', function () {
                clear();                
            });

            if (comp && comp.length) {

                $("#cancelincome").hide();
                $("#cancelexpenses").hide();

                var parameters_getMobile_YearMonthList = ["comp", comp];
                PageMethod("getMobile_YearMonthList", parameters_getMobile_YearMonthList, succeededAjaxFn_getMobile_YearMonthList, failedAjaxFn_getMobile_YearMonthList, false);

                var parameters_getMobile_PaymentReportSummary = ["comp", comp, "selectyear", $("#selectyear").val(), "selectmonth", $("#selectmonth").val(), "selectday", ""];
                PageMethod("getMobile_PaymentReportSummary2", parameters_getMobile_PaymentReportSummary, succeededAjaxFn_getMobile_PaymentReportSummary, failedAjaxFn_getMobile_PaymentReportSummary, false);

                oTable = $('#CashInOutList').DataTable({
                    'paging': true,
                    'pageLength': 10,
                    'lengthChange': false,
                    'searching': false,
                    'invoiceing': true,
                    'info': false,
                    'autoWidth': true,
                    //"order": [[5, "desc"]],
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false
                        },
                        {
                            "targets": [1],
                            "visible": false,
                        },
                        {
                            "targets": [2],
                            "visible": false
                        },
                        {
                            "targets": [3],
                            "visible": false
                        },
                        {
                            "targets": [4],
                            "visible": false
                        }
                    ]
                });

                bpTable = $('#tbBPList').DataTable({
                    'paging': true,
                    'pageLength': 5,
                    'lengthChange': false,
                    'searching': true,
                    'ordering': true,
                    'info': false,
                    'autoWidth': true,
                    "columnDefs": [
                        {
                            "targets": [1],
                            "visible": false,
                            //"searchable": false
                        },
                        {
                            "targets": [2],
                            "visible": false,
                            //"searchable": false
                        },
                        {
                            "targets": [3],
                            "visible": false
                        },
                        {
                            "targets": [4],
                            "visible": false
                        }
                    ]
                });

                $('#myDIV').modal({ backdrop: "static" });

                //Cash In/Out List
                var parameters_getMobile_ListCashInOut = ["comp", '<%=sCurrComp%>', "selectyear", $("#selectyear").val(), "selectmonth", $("#selectmonth").val(), "selectday", ""];
                PageMethod("getMobile_ListCashInOut2", parameters_getMobile_ListCashInOut, succeededAjaxFn_getMobile_ListCashInOut, failedAjaxFn_getMobile_ListCashInOut, true);

                $('#previouslist').on('click', function () {
                    oTable.page('previous').draw('page');
                });

                $('#nextlist').on('click', function () {
                    oTable.page('next').draw('page');
                });

                $('#CashInOutList tbody').on('click', 'tr', function () {
                    var data = oTable.row(this).data();

                    $('#myDIV').modal('show');
                    bukutunaidetails(data[0], data[1], data[2], data[3]);

                });
            }

            enabledisableinput(true);

        });

        //BEGIN: getMobile_YearMonthList
        succeededAjaxFn_getMobile_YearMonthList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_YearMonthList: " + textStatus);

            result_getMobile_YearMonthList = JSON.parse(data.d);
            updyear = result_getMobile_YearMonthList.currentyear;
            updmonth = result_getMobile_YearMonthList.currentmonth;

            if (reqyear && reqyear.length > 0) {
                updyear = reqyear;
            }

            var select2 = document.getElementById("selectyear");
            for (var option in select2) {
                select2.remove(option);
            }

            $.each(result_getMobile_YearMonthList.arrayyear, function (i, res) {
                //select1.append(new Option(res.yearval, res.yearid));
                select2.append(new Option(res.yearval, res.yearid));
            });
            $('#selectyear').val(updyear).change()

            var select4 = document.getElementById("selectmonth");
            for (var option in select4) {
                select4.remove(option);
            }

            $.each(result_getMobile_YearMonthList.arraymonth, function (i, res) {
                //select3.append(new Option(res.monthval, res.monthid));
                select4.append(new Option(res.monthval, res.monthid));
            });

        }

        failedAjaxFn_getMobile_YearMonthList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_YearMonthList: " + textStatus);
            updyear = "";
            updmonth = "";

            //$('#myDIV').modal('hide');
        }

        //BEGIN: getMobile_PaymentReportSummary
        succeededAjaxFn_getMobile_PaymentReportSummary = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_PaymentReportSummary: " + textStatus);

            result = JSON.parse(data.d);

            $('#lblCashOpeningAmnt').text(formatMoney(result.totalcashopeningamount));
            $('#lblCashPayRcptAmnt').text(formatMoney(result.totalcashpayrcptamount));
            $('#lblCashPayPaidAmnt').text(formatMoney(result.totalcashpaypaidamount));
            $('#lblCashClosingAmnt').text(formatMoney(result.totalcashclosingamount));

        }

        failedAjaxFn_getMobile_PaymentReportSummary = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_PaymentReportSummary: " + textStatus);
        }
        //END: getMobile_PaymentReportSummary

        succeededAjaxFn_getMobile_ListCashInOut = function (data, textStatus, jqXHR) {

            console.log("succeededAjaxFn_getMobile_ListCashInOut: " + textStatus);

            oTable.clear().draw();

            result_getMobile_InvoiceList = JSON.parse(data.d);

            setTimeout(function () {

                var i = 0;
                var rowno = 0;
                var totalamount = 0;
                var status = "";
                $.each(result_getMobile_InvoiceList, function (i, result) {
                    var amount = "";
                    if (result.GetSetCashInOutType == "income") {
                        amount = "<span style='color: green;font-weight: bold;'>+" + result.GetSetCashInOutAmount + "</span>";
                    } else {
                        amount = "<span style='color: red;font-weight: bold;'>-" + result.GetSetCashInOutAmount + "</span>";
                    }

                    if (result.GetSetCashInOutStatus == "CONFIRMED") {
                        status = "CONFIRMED";
                    } else if (result.GetSetCashInOutStatus == "CANCELLED") {
                        status = "<span style='color:red'>CANCELLED</span>";
                    } else {
                        status = "NEW";
                    }

                    oTable.row.add([
                        result.GetSetCashInOutcomp,
                        result.GetSetCashInOutType,
                        result.GetSetCashInOutpayno,
                        result.GetSetCashInOutresno,
                        result.GetSetCashInOutdate,
                        result.GetSetCashInOutdate + "</br>" + result.GetSetCashInOutDesc,
                        amount + "</br>(" + result.GetSetpaytype + ")" + "</br>" + result.GetSetRemarks,
                        status
                    ]).draw(false);
                });

                $('#myDIV').modal('hide');

            }, 750);
        }

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_ListCashInOut = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_ListCashInOut: " + textStatus);
            $('#myDIV').modal('hide');
        }

        //open modal keluar dan masuk duit
        $("#btn_modalwangmasuk").click(function (e) {
            $('#myDIV').modal('show');

            $("#cancelincome").hide();
            $("#createincome").show();
            $('#inc_date').prop('disabled', false);
            $('#inc_ic').prop('disabled', false);
            $('#inc_desc').prop('disabled', false);

            $('#modal-wangmasuk').modal({ backdrop: "static" });

        });

        $('#modal-wangmasuk').on('shown.bs.modal', function (e) {
            $('#inc_date').val(getCurrentDate());

            $("#closeincome").focus();

            var parameters_getMobile_parametertype = ["paramtcategory", "'CAP-INCOME','INCOME'"];
            PageMethod("getMobile_parametertype", parameters_getMobile_parametertype, succeededAjaxFn_getMobile_parametertype, failedAjaxFn_getMobile_parametertype, false);

        });

        succeededAjaxFn_getMobile_parametertype = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_parametertype: " + textStatus);
            $('#myDIV').modal('hide');

            var output = "<option>-Sila Pilih-</option>";
            result_getMobile_parametertype = JSON.parse(data.d);
            $.each(result_getMobile_parametertype, function (i, result) {

                output += "<option value='" + result.GetSetparamttype + "'>" + result.GetSetparamtdesc + "</option>";
            });
            
            $('#inc_type').html("").append(output);
            $('#exp_type').html("").append(output);
        };

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_parametertype = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_parametertype: " + textStatus);
            $('#myDIV').modal('hide');
        }

        $("#inc_type").change(function () {
            var parameters_getMobile_PendingInvoiceList = ["comp", comp, "bpid", "", "invoicecat", "RECEIPT_VOUCHER", "invoicetype", $(this).val()];
            PageMethod("getMobile_PendingInvoiceList", parameters_getMobile_PendingInvoiceList, succeededAjaxFn_getMobile_PendingInvoiceList, failedAjaxFn_getMobile_PendingInvoiceList, false);
        });


        $("#exp_type").change(function () {
            var parameters_getMobile_PendingExpensesList = ["comp", comp, "bpid", '', "expensescat", 'PAYMENT_VOUCHER', "expensestype", $(this).val()];
            PageMethod("getMobile_PendingExpensesList", parameters_getMobile_PendingExpensesList, succeededAjaxFn_getMobile_PendingExpensesList, failedAjaxFn_getMobile_PendingExpensesList, false);
        });

        //Blur on ic
        $("#inc_ic").click(function () {
            $('#myDIV').modal('show');
            var parameters_getMobile_BPList = ["comp", comp, "bpid", "", "bpdesc", "", "bpcat", "", "solidbp", "Y"];
            PageMethod("getMobile_BPList", parameters_getMobile_BPList, succeededAjaxFn_getMobile_BPList, failedAjaxFn_getMobile_BPList, false);
        });
        
        $("#inc_desc").click(function () {
            $('#myDIV').modal('show');
            var parameters_getMobile_BPList = ["comp", comp, "bpid", "", "bpdesc", "", "bpcat", "", "solidbp", "Y"];
            PageMethod("getMobile_BPList", parameters_getMobile_BPList, succeededAjaxFn_getMobile_BPList, failedAjaxFn_getMobile_BPList, false);
        });
        
        $("#exp_ic").click(function () {
            $('#myDIV').modal('show');
            var parameters_getMobile_BPList = ["comp", comp, "bpid", "", "bpdesc", "", "bpcat", "", "solidbp", "Y"];
            PageMethod("getMobile_BPList", parameters_getMobile_BPList, succeededAjaxFn_getMobile_BPList, failedAjaxFn_getMobile_BPList, false);
        });
       
        $("#exp_desc").click(function () {
            $('#myDIV').modal('show');
            var parameters_getMobile_BPList = ["comp", comp, "bpid", "", "bpdesc", "", "bpcat", "", "solidbp", "Y"];
            PageMethod("getMobile_BPList", parameters_getMobile_BPList, succeededAjaxFn_getMobile_BPList, failedAjaxFn_getMobile_BPList, false);
        });        

        /*
        $(document).click(function (event) {
            if (event.target == $("#inc_ic").get(0)) {
                blurflag = false;
                $("#inc_ic").focus();
            } else if (event.target == $("#inc_desc").get(0)) {
                blurflag = false;
                $("#inc_desc").focus();
            } else if (event.target == $("#closeincome").get(0)) {
                blurflag = false;
            } else if (event.target == $("#createincome").get(0)) {
                blurflag = false;
                $("#inc_ic").focus();
            } else if (event.target == $("#exp_ic").get(0)) {
                blurflag = false;
                $("#exp_ic").focus();
            } else if (event.target == $("#exp_desc").get(0)) {
                blurflag = false;
                $("#exp_desc").focus();
            } else if (event.target == $("#closeexpenses").get(0)) {
                blurflag = false;
            } else if (event.target == $("#createexpenses").get(0)) {
                blurflag = false;
                $("#exp_ic").focus();
            } else {
                blurflag = true;
            }
        });
        */

        succeededAjaxFn_getMobile_BPList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_BPList: " + textStatus);
            $('#myDIV').modal('hide');

            $('#myBPList').modal({ backdrop: "static" });

            bpTable.clear().draw();
            bpTable.search('');

            result_getMobile_BPList = JSON.parse(data.d);
            $.each(result_getMobile_BPList, function (i, result) {
                bpTable.row.add([
                    i + 1,
                    result.GetSetbpid,
                    result.GetSetbpdesc,
                    result.GetSetbpaddress,
                    result.GetSetbpcontact,
                    result.GetSetbpid + '<br/>' + result.GetSetbpdesc + '<br/>' + result.GetSetbpaddress
                ]).draw(false);
            });

        };

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_BPList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_BPList: " + textStatus);
            $('#myDIV').modal('hide');
        }

        $('#tbBPList tbody').on('click', 'tr', function () {
            var data = bpTable.row(this).data();

            openbpdetails(data[1], data[2], data[3], data[4]);
        });

        function openbpdetails(bpid, bpdesc, bpaddress, bpcontact) {

            $('#inc_ic').val(bpid);
            $('#inc_desc').val(bpdesc);
            $('#inc_address').val(bpaddress);
            $('#inc_contact').val(bpcontact);

            $('#exp_ic').val(bpid);
            $('#exp_desc').val(bpdesc);
            $('#exp_address').val(bpaddress);
            $('#myBPList').modal('hide');

            if ($("#inc_ic").val() != "") {
                $('#inc_desc').prop('readonly', true);
            }
            if ($("#inc_desc").val() != "") {
                $('#inc_ic').prop('readonly', true);
            }

            if ($("#exp_ic").val() != "") {
                $('#exp_desc').prop('readonly', true);
            }
            if ($("#exp_desc").val() != "") {
                $('#exp_ic').prop('readonly', true);
            }

        }

        $("#myBPList").on('shown.bs.modal', function () {
            setTimeout(function () {
                bpTable.columns.adjust();
            }, 750);
        });

        $("#myBPList").on('hidden.bs.modal', function () {
            enabledisableinput(true);

            if ($("#inc_ic").val().length > 0 && $("#inc_desc").val().length > 0) {
                enabledisableinput(false);
            } else if ($("#inc_ic").val().length > 0) {
                $("#closeincome").focus();
                $("#inc_ic").val("");
                $("#inc_desc").val("");
                $("#inc_remarks").val("");
            } else if ($("#inc_desc").val().length > 0) {
                $("#closeincome").focus();
                $("#inc_ic").val("");
                $("#inc_desc").val("");
                $("#inc_remarks").val("");
            } else {
                $("#closeincome").focus();
                $("#inc_ic").val("");
                $("#inc_desc").val("");
                $("#inc_remarks").val("");
            }

            if ($("#exp_ic").val().length > 0 && $("#exp_desc").val().length > 0) {
                enabledisableinput(false);
            } else if ($("#exp_ic").val().length > 0) {
                $("#closeexpenses").focus();
                $("#exp_ic").val("");
                $("#exp_desc").val("");
                $("#exp_remarks").val("");
            } else if ($("#exp_desc").val().length > 0) {
                $("#closeexpenses").focus();
                $("#exp_ic").val("");
                $("#exp_desc").val("");
                $("#exp_remarks").val("");
            } else {
                $("#closeexpenses").focus();
                $("#exp_ic").val("");
                $("#exp_desc").val("");
                $("#exp_remarks").val("");
            }
        });

        $("#btn_modalwangkeluar").click(function (e) {
            $('#myDIV').modal('show');

            $("#cancelexpenses").hide();
            $("#createexpenses").show();
            $('#exp_date').prop('disabled', false);
            $('#exp_ic').prop('disabled', false);
            $('#exp_desc').prop('disabled', false);

            $('#modal-wangkeluar').modal({ backdrop: "static" });

        });

        $('#modal-wangkeluar').on('shown.bs.modal', function (e) {
            $('#exp_date').val(getCurrentDate());
            $("#closeexpenses").focus();

            var parameters_getMobile_parametertype = ["paramtcategory", "'CAP-EXPENSES','EXPENSES'"];
            PageMethod("getMobile_parametertype", parameters_getMobile_parametertype, succeededAjaxFn_getMobile_parametertype, failedAjaxFn_getMobile_parametertype, false);

        });

        succeededAjaxFn_getMobile_PendingInvoiceList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_PendingInvoiceList: " + textStatus);
            $('#myDIV').modal('hide');
            var output = "";
            result_getMobile_PendingInvoiceList = JSON.parse(data.d);
            output += "<option value=''>-Sila Pilih-</option>";
            $.each(result_getMobile_PendingInvoiceList, function (i, result) {
                output += "<option value='" + result.GetSetparamid + "'>" + result.GetSetparamdesc + "</option>";
            });
            $('#inc_item').html("").append(output);
        };

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_PendingInvoiceList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_PendingInvoiceList: " + textStatus);
            $('#myDIV').modal('hide');
        }

        succeededAjaxFn_getMobile_PendingExpensesList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getMobile_PendingExpensesList: " + textStatus);
            $('#myDIV').modal('hide');

            result_getMobile_PendingExpensesList = JSON.parse(data.d);
            var output = "<option>-Sila Pilih-</option>";
            $.each(result_getMobile_PendingExpensesList, function (i, result) {
                output += "<option value='" + result.GetSetparamid + "'>" + result.GetSetparamdesc + "</option>";
            });
            $('#exp_item').html("").append(output);

        };

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_PendingExpensesList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_PendingExpensesList: " + textStatus);
            $('#myDIV').modal('hide');
        }

        succeededAjaxFn_getMobile_ExpensesDetails2 = function (data, textStatus, jqXHR) {
            setTimeout(function () {
                console.log("succeededAjaxFn_getMobile_ExpensesDetails2: " + textStatus);
                $('#myDIV').modal('hide');

                result = JSON.parse(data.d);
                //new Date(result.GetSetexpensesdate)
                if (result.GetSetexpensesdate != "") {
                    var date = new Date(result.GetSetexpensesdate);
                    //$("#exp_date").val(new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toJSON().slice(0, 19));

                }
                $("#exp_date").val(result.GetSetexpensesdate);
                $("#exp_ic").val(result.GetSetbpid);
                $("#exp_desc").val(result.GetSetbpdesc);
                $("#exp_address").val(result.GetSetbpaddress);
                $("#exp_amount").val(result.GetSetpaypaidamount);
                $("#exp_paytype").val(result.GetSetpaytype);
                $("#exp_type").html("").append("<option>" + result.GetSetexpensescat + "</option>");
                $("#exp_item").html("").append("<option>" + result.GetSetexpensestype + "</option>");
                $("#exp_remarks").val(result.GetSetremarks);
                $("#exp_status").val(result.GetSetstatus);
                if (result.GetSetstatus == "CANCELLED") {
                    $("#cancelexpenses").hide();
                } else if (result.GetSetparamtcategory == "INTERNAL") {
                    $("#cancelexpenses").hide();
                } else if (result.GetSetinvoicecat != "PAYMENT_VOUCHER") {
                    $("#cancelexpenses").hide();
                } else {
                    $("#cancelexpenses").show();
                }

            }, 750);
        };

        //notification for failed ajax transaction
        failedAjaxFn_getMobile_ExpensesDetails2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_ExpensesDetails2: " + textStatus);
            $('#modal-wangkeluar').modal('hide');
        }

        succeededAjaxFn_getMobile_IncomeDetails = function (data, textStatus, jqXHR) {
            setTimeout(function () {
                console.log("succeededAjaxFn_getMobile_IncomeDetails: " + textStatus);
                $('#myDIV').modal('hide');

                result = JSON.parse(data.d);
                if (result.GetSetincomedate != "") {
                    var date = new Date(result.GetSetincomedate);
                    //$("#inc_date").val(new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toJSON().slice(0, 19));
                }
                $("#inc_date").val(result.GetSetincomedate);
                $("#inc_ic").val(result.GetSetbpid);
                $("#inc_desc").val(result.GetSetbpdesc);
                $("#inc_address").val(result.GetSetbpaddress);
                $("#inc_amount").val(result.GetSetpayrcptamount);
                $("#inc_paytype").val(result.GetSetpaytype);
                $("#inc_type").html("").append("<option>" + result.GetSetincomecat + "</option>");
                $("#inc_item").html("").append("<option>" + result.GetSetincometype + "</option>");
                $("#inc_remarks").val(result.GetSetremarks);
                $("#inc_status").val(result.GetSetstatus);
                if (result.GetSetstatus == "CANCELLED") {
                    $("#cancelincome").hide();
                } else if (result.GetSetparamtcategory == "INTERNAL") {
                    $("#cancelincome").hide();
                } else if (result.GetSetinvoicecat != "RECEIPT_VOUCHER") {
                    $("#cancelincome").hide();
                } else {
                    $("#cancelincome").show();
                }
            }, 750);
        };
        //notification for failed ajax transaction
        failedAjaxFn_getMobile_IncomeDetails = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getMobile_IncomeDetails: " + textStatus);
            $('#modal-wangmasuk').modal('hide');
        }

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

        function bukutunaidetails(comp, typ, payno, resno) {
            enabledisableinput(true);

            cancelpayno = payno;
            cancelresno = resno;
            canceltype = typ;

            if (typ == "expenses") {
                $("#cancelexpenses").hide();
                $("#createexpenses").hide();
                $('#exp_date').prop('disabled', true);
                $('#exp_ic').prop('disabled', true);
                $('#exp_desc').prop('disabled', true);

                $("#modal-wangkeluar").modal({ backdrop: "static" });
                // alert(cancelpayno + cancelresno);
                var parameters_getMobile_ExpensesDetails2 = ["comp", comp, "paypaidno", payno, "receiptno", resno];
                PageMethod("getMobile_ExpensesDetails2", parameters_getMobile_ExpensesDetails2, succeededAjaxFn_getMobile_ExpensesDetails2, failedAjaxFn_getMobile_ExpensesDetails2, false);

                //console.log("calling bukutunaidetails 2");
            } else {
                $("#cancelincome").hide();
                $("#createincome").hide();
                $('#inc_date').prop('disabled', true);
                $('#inc_ic').prop('disabled', true);
                $('#inc_desc').prop('disabled', true);

                $("#modal-wangmasuk").modal({ backdrop: "static" });
                // alert(cancelpayno + cancelresno);
                var parameters_getMobile_IncomeDetails = ["comp", comp, "paypaidno", payno, "receiptno", resno];
                PageMethod("getMobile_IncomeDetails", parameters_getMobile_IncomeDetails, succeededAjaxFn_getMobile_IncomeDetails, failedAjaxFn_getMobile_IncomeDetails, false);

                //console.log("calling bukutunaidetails 3");
            }
        }

        function createincome() {
            var date = $("#inc_date").val();
            var bpid = $("#inc_ic").val();
            var bpdesc = $("#inc_desc").val();
            var bpaddress = $("#inc_address").val();
            var bpcontact = $("#inc_contact").val();
            var inc_amount = $("#inc_amount").val();
            var inc_paytype = $("#inc_paytype").val();
            var inc_type = $("#inc_type").val();
            var inc_item = $("#inc_item").val();
            var inc_itemdesc = $("#inc_item option:selected").text();
            var inc_remarks = $("#inc_remarks").val();

            var parameters_createMobile_InvoiceInvoice2 = ["comp", comp, "bpid", bpid, "inc_amount", inc_amount, "invoicecat", 'RECEIPT_VOUCHER', "inc_type", inc_type, "inc_remarks", inc_remarks, "datecreation", date, "userid", userid];
            PageMethod("createMobile_InvoiceInvoice2", parameters_createMobile_InvoiceInvoice2, succeededAjaxFn_createMobile_InvoiceInvoice2, failedAjaxFn_createMobile_InvoiceInvoice2, false);

            if (successcreationinvoice) {
                var parameters_addMobile_InvoiceItemDetails2 = ["comp", comp, "invoiceno", invoiceno, "inc_item", inc_item, "inc_itemdesc", inc_itemdesc, "inc_amount", inc_amount];
                PageMethod("addMobile_InvoiceItemDetails2", parameters_addMobile_InvoiceItemDetails2, succeededAjaxFn_addMobile_InvoiceItemDetails2, failedAjaxFn_addMobile_InvoiceItemDetails2, false);
                //alert(invoiceno);
            }

            if (successcreationinvoicedetails) {
                var parameters_createMobile_PayRcptInvoice2 = ["comp", comp, "invoiceno", invoiceno, "datecreation", date, "inc_amount", inc_amount, "userid", userid];
                PageMethod("createMobile_PayRcptInvoice2", parameters_createMobile_PayRcptInvoice2, succeededAjaxFn_createMobile_PayRcptInvoice2, failedAjaxFn_createMobile_PayRcptInvoice2, false);
            }

            if (successcreationpayrcpt) {
                //alert(payrcptno);
                var parameters_addMobile_PayRcptItemInvoice2 = ["comp", comp, "payrcptno", payrcptno, "invoiceno", invoiceno, "datecreation", date, "inc_amount", inc_amount, "inc_paytype", inc_paytype];
                PageMethod("addMobile_PayRcptItemInvoice2", parameters_addMobile_PayRcptItemInvoice2, succeededAjaxFn_addMobile_PayRcptItemInvoice2, failedAjaxFn_addMobile_PayRcptItemInvoice2, false);
            }

            if (successcreationpayrcptdetails) {
                $('#modal-wangmasuk').modal('hide');
                location.reload();

                //var parameters_getMobile_ListCashInOut = ["comp", comp];
                //PageMethod("getMobile_ListCashInOut", parameters_getMobile_ListCashInOut, succeededAjaxFn_getMobile_ListCashInOut, failedAjaxFn_getMobile_ListCashInOut, false);
            }

        }

        succeededAjaxFn_createMobile_InvoiceInvoice2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createMobile_InvoiceInvoice2: " + textStatus);

            result_createMobile_InvoiceInvoice = JSON.parse(data.d);
            if (result_createMobile_InvoiceInvoice.status == "Y") {
                successcreationinvoice = true;
                invoiceno = result_createMobile_InvoiceInvoice.message;

            }
            else {
                successcreationinvoice = false;
                invoiceno = "";
            }
        };

        //notification for failed ajax transaction
        failedAjaxFn_createMobile_InvoiceInvoice2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createMobile_InvoiceInvoice2: " + textStatus);
            successcreationinvoice = false;
            invoiceno = "";
        }

        succeededAjaxFn_addMobile_InvoiceItemDetails2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addMobile_InvoiceItemDetails2: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationinvoicedetails = true;
                errormessage = "";
            }
            else {
                successcreationinvoicedetails = false;
                errormessage = result.message;
            }
        };

        //notification for failed ajax transaction
        failedAjaxFn_addMobile_InvoiceItemDetails2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addMobile_InvoiceItemDetails2: " + textStatus);
            successcreationinvoicedetails = false;
            errormessage = "Internal Server Error!";
        }

        succeededAjaxFn_createMobile_PayRcptInvoice2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createMobile_PayRcptInvoice: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationpayrcpt = true;
                payrcptno = result.message;
            } else {
                successcreationpayrcpt = false;
                payrcptno = "";
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_createMobile_PayRcptInvoice2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createMobile_PayRcptInvoice2: " + textStatus);
            savepayrcpt = false;
            payrcptno = "";
        }


        succeededAjaxFn_addMobile_PayRcptItemInvoice2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addMobile_PayRcptItemInvoice2: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationpayrcptdetails = true;
                errormessage = "";
            }
            else {
                successcreationpayrcptdetails = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_addMobile_PayRcptItemInvoice2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addMobile_PayRcptItemInvoice2: " + textStatus);
            savepayrcpt = false;
            errormessage = "Internal Server Error!";
        }
        
        function createexpenses() {
            // alert("hi im here");
            var date = $("#exp_date").val();
            var bpid = $("#exp_ic").val();
            var bpdesc = $("#exp_desc").val();
            var bpaddress = $("#exp_address").val();
            var exp_amount = $("#exp_amount").val();
            var exp_paytype = $("#exp_paytype").val();
            var exp_type = $("#exp_type").val();
            var exp_item = $("#exp_item").val();
            var exp_itemdesc = $("#exp_item option:selected").text();
            var exp_remarks = $("#exp_remarks").val();

            var parameters_createMobile_ExpensesExpenses2 = ["comp", comp, "bpid", bpid, "expensescat", "PAYMENT_VOUCHER", "expensestype", exp_type, "exp_amount", exp_amount, "exp_remarks", exp_remarks, "datecreation", date, "userid", userid];
            PageMethod("createMobile_ExpensesHeader2", parameters_createMobile_ExpensesExpenses2, succeededAjaxFn_createMobile_ExpensesExpenses2, failedAjaxFn_createMobile_ExpensesExpenses2, false);

            if (successcreationexpenses) {
                // alert(expensesno);
                var parameters_addMobile_ExpensesReceiptItemDetails2 = ["comp", comp, "expensesno", expensesno, "expensescat", "PURCHASE_INVOICE", "exp_item", exp_item, "exp_itemdesc", exp_itemdesc, "exp_amount", exp_amount, "userid", userid];
                PageMethod("addMobile_ExpensesReceiptItemDetails2", parameters_addMobile_ExpensesReceiptItemDetails2, succeededAjaxFn_addMobile_ExpensesReceiptItemDetails2, failedAjaxFn_addMobile_ExpensesReceiptItemDetails2, false);
            }

            if (successcreationexpensesdetails) {
                var parameters_createMobile_PayPaidExpenses2 = ["comp", comp, "expensesno", expensesno, "exp_amount", exp_amount, "datecreation", date, "userid", userid];
                PageMethod("createMobile_PayPaidExpenses2", parameters_createMobile_PayPaidExpenses2, succeededAjaxFn_createMobile_PayPaidExpenses2, failedAjaxFn_createMobile_PayPaidExpenses2, false);
            }

            if (successcreationpaypaid) {
                //alert(paypaidno);
                var parameters_addMobile_PayPaidItemExpenses2 = ["comp", comp, "paypaidno", paypaidno, "expensesno", expensesno, "exp_paytype", exp_paytype, "exp_amount", exp_amount, "datecreation", date,];
                PageMethod("addMobile_PayPaidItemExpenses2", parameters_addMobile_PayPaidItemExpenses2, succeededAjaxFn_addMobile_PayPaidItemExpenses2, failedAjaxFn_addMobile_PayPaidItemExpenses2, false);
            }

            if (successcreationpaypaiddetails) {
                $('#modal-wangkeluar').modal('hide');
                //var parameters_getMobile_ListCashInOut = ["comp", comp];
                //PageMethod("getMobile_ListCashInOut", parameters_getMobile_ListCashInOut, succeededAjaxFn_getMobile_ListCashInOut, failedAjaxFn_getMobile_ListCashInOut, false);
                location.reload();
            }

            console.log(date + bpid + bpdesc + bpaddress + exp_amount + exp_paytype + exp_type + exp_itemdesc + exp_item);

        }

        succeededAjaxFn_createMobile_ExpensesExpenses2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createMobile_ExpensesExpenses2: " + textStatus);

            result_createMobile_ExpensesExpenses = JSON.parse(data.d);
            if (result_createMobile_ExpensesExpenses.status == "Y") {
                successcreationexpenses = true;
                expensesno = result_createMobile_ExpensesExpenses.message;
            }
            else {
                successcreationexpenses = false;
                expensesno = "";
            }
        };

        //notification for failed ajax transaction
        failedAjaxFn_createMobile_ExpensesExpenses2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createMobile_ExpensesExpenses2: " + textStatus);
            successcreationexpenses = false;
            expensesno = "";
        }

        succeededAjaxFn_addMobile_ExpensesReceiptItemDetails2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addMobile_ExpensesReceiptItemDetails2: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationexpensesdetails = true;
                errormessage = "";
            }
            else {
                successcreationexpensesdetails = false;
                errormessage = result.message;
            }
        };

        //notification for failed ajax transaction
        failedAjaxFn_addMobile_ExpensesReceiptItemDetails2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addMobile_ExpensesReceiptItemDetails2: " + textStatus);
            successcreationexpensesdetails = false;
            errormessage = "Internal Server Error!";
        }

        succeededAjaxFn_createMobile_PayPaidExpenses2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createMobile_PayPaidExpenses2: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationpaypaid = true;
                paypaidno = result.message;
            } else {
                successcreationpaypaid = false;
                paypaidno = "";
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_createMobile_PayPaidExpenses2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createMobile_PayPaidExpenses2: " + textStatus);
            successcreationpaypaid = false;
            paypaidno = "";
        }

        succeededAjaxFn_addMobile_PayPaidItemExpenses2 = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addMobile_PayPaidItemExpenses2: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcreationpaypaiddetails = true;
                errormessage = "";
            }
            else {
                successcreationpaypaiddetails = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_addMobile_PayPaidItemExpenses2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addMobile_PayPaidItemExpenses2: " + textStatus);
            successcreationpaypaiddetails = false;
            errormessage = "Internal Server Error!";
        }

        function clear() {
            //alert("clear modal");
            $("#inc_date").val("");
            $("#inc_ic").val("");
            $('#inc_desc').val('');
            $("#inc_address").val("");
            $("#inc_amount").val("");
            $("#inc_paytype").val('');
            $("#inc_type").val('').trigger('change');
            $("#inc_remarks").val('');
            $("#inc_status").val('NEW');

            $("#exp_date").val("");
            $("#exp_ic").val("");
            $('#exp_desc').val('');
            $("#exp_address").val('');
            $("#exp_amount").val("");
            $("#exp_paytype").val('');
            $("#exp_type").val('').trigger('change');
            $("#exp_remarks").val('');
            $("#exp_status").val('NEW');

            $('#inc_ic').prop('readonly', false);
            $('#inc_desc').prop('readonly', false);
            $('#exp_ic').prop('readonly', false);
            $('#exp_desc').prop('readonly', false);

            enabledisableinput(true); 
        }

        function cancelinvoice() {

            if (canceltype == "income") {

                var parameters_updateMobile_PayRcptInvoiceStatus = ["comp", comp, "payrcptno", cancelpayno, "status", "CANCELLED", "userid", userid];
                PageMethod("updateMobile_PayRcptInvoiceStatus", parameters_updateMobile_PayRcptInvoiceStatus, succeededAjaxFn_updateMobile_PayRcptInvoiceStatus, failedAjaxFn_updateMobile_PayRcptInvoiceStatus, false);

                //alert(canceltype + cancelcode);
                var parameters_updateMobile_InvoiceHeaderStatus = ["comp", comp, "invoiceno", cancelresno, "status", "CANCELLED", "userid", userid];
                PageMethod("updateMobile_InvoiceHeaderStatus", parameters_updateMobile_InvoiceHeaderStatus, succeededAjaxFn_updateMobile_InvoiceHeaderStatus, failedAjaxFn_updateMobile_InvoiceHeaderStatus, false);


                if (successcancelinvoice && successcancelpayrcpt) {
                    window.location.href = "BukuTunai.aspx?userid=" + userid + "&comp=" + comp + "&invoiceno=" + invoiceno;
                }

            } else if (canceltype == "expenses") {
                //alert(canceltype + cancelresno + cancelpayno);

                var parameters_updateMobile_ExpensesHeaderStatus = ["comp", comp, "expensesno", cancelresno, "status", "CANCELLED", "userid", userid];
                PageMethod("updateMobile_ExpensesHeaderStatus", parameters_updateMobile_ExpensesHeaderStatus, succeededAjaxFn_updateMobile_ExpensesHeaderStatus, failedAjaxFn_updateMobile_ExpensesHeaderStatus, false);

                var parameters_updateMobile_PayPaidExpensesStatus = ["comp", comp, "paypaidno", cancelpayno, "status", "CANCELLED", "userid", userid];
                PageMethod("updateMobile_PayPaidExpensesStatus", parameters_updateMobile_PayPaidExpensesStatus, succeededAjaxFn_updateMobile_PayPaidExpensesStatus, failedAjaxFn_updateMobile_PayPaidExpensesStatus, false);

                if (successcancelexpenses && successcancelpaypaid) {
                    window.location.href = "BukuTunai.aspx?userid=" + userid + "&comp=" + comp + "&invoiceno=" + invoiceno;

                }
            }

        }

        succeededAjaxFn_updateMobile_PayRcptInvoiceStatus = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_updateMobile_PayRcptInvoiceStatus: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcancelpayrcpt = true;
                errormessage = "";
            }
            else {
                successcancelpayrcpt = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_updateMobile_PayRcptInvoiceStatus = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_updateMobile_PayRcptInvoiceStatus: " + textStatus);
            successcancelpayrcpt = false;
            errormessage = "Internal Server Error!";
        }

        succeededAjaxFn_updateMobile_InvoiceHeaderStatus = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_updateMobile_InvoiceHeaderStatus: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcancelinvoice = true;
                errormessage = "";
            }
            else {
                successcancelinvoice = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_updateMobile_InvoiceHeaderStatus = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_updateMobile_InvoiceHeaderStatus: " + textStatus);
            successcancelinvoice = false;
            errormessage = "Internal Server Error!";
        }

        succeededAjaxFn_updateMobile_ExpensesHeaderStatus = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_updateMobile_ExpensesHeaderStatus: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcancelexpenses = true;
                errormessage = "";
            }
            else {
                successcancelexpenses = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_updateMobile_ExpensesHeaderStatus = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_updateMobile_ExpensesHeaderStatus: " + textStatus);
            successcancelexpenses = false;
            errormessage = "Internal Server Error!";
        }

        succeededAjaxFn_updateMobile_PayPaidExpensesStatus = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_updateMobile_PayPaidExpensesStatus: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                successcancelpaypaid = true;
                errormessage = "";
            }
            else {
                successcancelpaypaid = false;
                errormessage = result.message;
            }
        }

        //notification for failed ajax transaction
        failedAjaxFn_updateMobile_PayPaidExpensesStatus = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_updateMobile_PayPaidExpensesStatus: " + textStatus);
            successcancelpaypaid = false;
            errormessage = "Internal Server Error!";
        }

        function searchbydate() {
            var parameters_getMobile_PaymentReportSummary = ["comp", comp, "selectyear", $("#selectyear").val(), "selectmonth", $("#selectmonth").val(), "selectday", ""];
            PageMethod("getMobile_PaymentReportSummary2", parameters_getMobile_PaymentReportSummary, succeededAjaxFn_getMobile_PaymentReportSummary, failedAjaxFn_getMobile_PaymentReportSummary, false);

            $('#myDIV').modal({ backdrop: "static" });

            //Cash In/Out List
            var parameters_getMobile_ListCashInOut = ["comp", comp, "selectyear", $("#selectyear").val(), "selectmonth", $("#selectmonth").val(), "selectday", ""];
            PageMethod("getMobile_ListCashInOut2", parameters_getMobile_ListCashInOut, succeededAjaxFn_getMobile_ListCashInOut, failedAjaxFn_getMobile_ListCashInOut, true);
        }

        function resetsearchbydate() {
            $("#selectyear").prop('selectedIndex', 0);
            $("#selectmonth").prop('selectedIndex', 0);
        }

        function getCurrentDate() {
            var todaydate = "";

            var today = new Date();
            var selyear = today.getFullYear();
            var selmonth = today.getMonth() + 1;
            if (selmonth < 10) {
                selmonth = '0' + selmonth;
            }
            var selday = today.getDate();
            if (selday < 10) {
                selday = '0' + selday;
            }
            var seltime = (today.getHours() < 10 ? '0' + today.getHours() : today.getHours()) + ':' + (today.getMinutes() < 10 ? '0' + today.getMinutes() : today.getMinutes()) + ':' + (today.getSeconds() < 10 ? '0' + today.getSeconds() : today.getSeconds());

            todaydate = selday + '-' + selmonth + '-' + selyear + " " + seltime;

            return todaydate;
        }

    </script>


  
</asp:Content>

 