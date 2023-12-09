<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ShipmentListing.aspx.cs" Inherits="ShipmentListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penghantaran Pesanan <small>SENARAI</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="x_content">
                    <div class="">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <a class="btn btn-app" onclick="openaddnewshipment();">
                          <i class="fa fa-plus-square green"></i>Daftar
                        </a>
                        <a class="btn btn-app" onclick="openBulkShipment();">
                          <i class="fa fa-barcode"></i> Nota Hantaran
                        </a>
                        <a class="btn btn-app" onclick="enabledisablesearchbox();">
                          <i class="fa fa-search"></i> Carian
                        </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-section" style="display:none;">
                        <section class="panel">
                            <div class="x_title">
                                <h2>Carian Penghantaran Pesanan</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <div class="form-group">
                                        <label for="shipmentno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penghantaran:</label>
                                        <input type="text" id="shipmentno" class="form-control" name="shipmentno" value="<%=sShipmentNo %>" />
                                    </div>
                                    <div class="form-group">
                                        <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Hantar Kepada:</label>
                                        <select id="bpid" class="select2_single form-control" tabindex="-1" name="bpid" style="width: 100%;">
                                                <option></option>
                                                <%
                                                    if (lsBP.Count > 0)
                                                    {
                                                        for (int i = 0; i < lsBP.Count; i++)
                                                        {
                                                            MainModel modBP = (MainModel)lsBP[i];
                                                %>       
                                                            <option value="<%=modBP.GetSetbpid %>" <%=sBPID.Equals(modBP.GetSetbpid)?"selected":"" %>><%=modBP.GetSetbpdesc %></option>
                                                <% 
                                                        }
                                                    }
                                                %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                        <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                        <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                                        <div style="display: none;">
                                            <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                            <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </section>
                    </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th></th>
                          <th>No. Penghantaran</th>
                          <th>Hantar Kepada</th>
                          <th>Alamat</th>
                          <th>Tarikh Penghantaran</th>
                          <th>Kategori</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsShipmentHeader.Count > 0)
                            {
                                for (int i = 0; i < lsShipmentHeader.Count; i++)
                                {
                                    MainModel modHdr = (MainModel)lsShipmentHeader[i];
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditshipment('<%=modHdr.GetSetcomp %>','<%=modHdr.GetSetshipmentno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditshipment('<%=modHdr.GetSetcomp %>','<%=modHdr.GetSetshipmentno %>');"><%=modHdr.GetSetshipmentno %></a></td>
                              <td><%=modHdr.GetSetbpdesc %></td>
                              <td><%=modHdr.GetSetbpaddress %></td>
                              <td><%=modHdr.GetSetshipmentdate %></td>
                              <td><%=modHdr.GetSetshipmentcat %></td>
                              <td><%=modHdr.GetSetstatus %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td></td>
                                <td>Tiada rekod...</td>
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
            <div class="modal fade" id="myBulkShipment" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            Daftar Nota Hantaran
                        </div>
                        <div class="modal-body">
                            <table id="tblSearchOrder" class="display">
                                <tbody>
                                    <tr>
                                        <td>No. Pesanan</td>
                                        <td><input type="text" id="txtfindorderno" name="txtfindorderno" class="form-control" /></td>
                                    </tr>
                                    <tr>
                                        <td>Kategori</td>
                                        <td>
                                          <select id="selfindordercat" name="selfindordercat" class="form-control">
                                            <option value="">-select-</option>
                                            <option value="SALES_ORDER">PESANAN JUALAN</option>
                                            <option value="GIVE_ORDER">PESANAN AGIHAN</option>
                                            <option value="TRANSFER_ORDER">PESANAN PINDAHAN</option>
                                          </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <button type="button" onclick="searchOrder();" class="btn btn-primary">Cari</button>
                                            <button type="button" onclick="openQRShipment();" class="btn btn-info">Scan QR</button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table id="tblOrderlist" class="table table-striped jambo_table">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th style="display:none;">Comp</th>
                                        <th style="display:none;">ShipmentNo</th>
                                        <th style="display:none;">LineNo</th>
                                        <th>OrderNo</th>
                                        <th style="display:none;">ItemNo</th>
                                        <th style="display:none;">OrderQty</th>
                                        <th>No. Item / Produk</th>
                                        <th>Qty</th>
                                        <th>Inventori</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                            <table id="tblNextAction" class="display">
                                <tbody>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <button type="button" onclick="createShipment();" class="btn btn-primary">Daftar</button>
                                            <button type="button" onclick="resetSearch();" class="btn btn-warning">Kembali</button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="closeBulk" class="btn btn-default pull-left" onclick="closeBulkShipment();">Tutup</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="myQRShipment" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            Scan QR Code
                        </div>
                        <div class="modal-body" style="align-content:center;">
                            <div id="qr-reader" style="/*width:500px;*/ text-align: center;align-content:center;"></div>
                            <div id="qr-reader-results"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnCloseQR" class="btn btn-default pull-left" onclick="closeQRShipment();">Tutup</button>
                        </div>
                    </div>
                </div>
            </div>
            
    <script type="text/javascript">

        var oTable;
        var currflag = "0";
        var saveshipment = false;
        var shipmentno = "";
        var urlhandler = "http://www.bioappsystem.com/bioappmalay";
        const html5QrCode = new Html5Qrcode("qr-reader");

        function enabledisablesearchbox() {
            var sb = document.getElementById("search-section");
            var cf = currflag;
            if (cf == "0") {
                sb.style.display = "none";
                currflag = "1";
            } else {
                sb.style.display = "";
                currflag = "0";
            }
        }

        $(document).ready(function () {            
            /*var resultContainer = document.getElementById('qr-reader-results');
            var lastResult, countResults = 0;
    
            function onScanSuccess(decodedText, decodedResult) {
                //if (decodedText !== lastResult)
                {
                    ++countResults;
                    lastResult = decodedText;
                    // Handle on success condition with the decoded message.
                    console.log(`Scan result ${decodedText}`, decodedResult);
                    $('#txtfindorderno').val(decodedText);
                    $('#btnCloseQR').click();

                    //closeQRShipment();
                    //$('#myQRShipment').remove();
                    //$('.modal-backdrop').remove();
                    //$('#myBulkShipment').modal('show').trigger('shown');
                    //closeBulkShipment();
                    //openBulkShipment();
                }
            }
    
            var html5QrcodeScanner = new Html5QrcodeScanner(
                "qr-reader", { fps: 10, qrbox: 250 });
            html5QrcodeScanner.render(onScanSuccess);*/
            

            /*
            const html5QrCode = new Html5Qrcode("qr-reader");
            // File based scanning
            const fileinput = document.getElementById('qr-reader-results');
            fileinput.addEventListener('change', e => {
                if (e.target.files.length == 0) {
                    // No file selected, ignore 
                    return;
                }

                const imageFile = e.target.files[0];
                // Scan QR Code
                html5QrCode.scanFile(imageFile, true)
                    .then(decodedText => {
                        // success, use decodedText
                        console.log(decodedText);
                    })
                    .catch(err => {
                        // failure, handle it.
                        console.log(`Error scanning file. Reason: ${err}`)
                    });
            });
            */


            enabledisablesearchbox();

            oTable = $('#tblOrderlist').DataTable({
                'paging': false,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': false,
                'autoWidth': false,
                "columnDefs": [
                    {
                        "targets": [1],
                        "visible": false,
                        "searchable": false
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
                        "targets": [5],
                        "visible": false
                    },
                    {
                        "targets": [6],
                        "visible": false
                    }
                ]
            });

            resetSearch();

            /*
            $("#myQRShipment").on('hide.bs.modal', function () {
                $('.modal-backdrop').remove()
            });

            $("#myBulkShipment").on('hide.bs.modal', function () {
                $('.modal-backdrop').remove()
            });
            */

        });

        function openaddnewshipment() {
            var popupWindow = window.open("ShipmentDetails.aspx?action=ADD", "add_shipment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
        function openeditshipment(comp, shipmentno) {
            var popupWindow = window.open("ShipmentDetails.aspx?action=OPEN&comp=" + comp + "&shipmentno=" + shipmentno, "open_shipment", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        function openQRShipment() {
            $('#myQRShipment').modal('show');
			const qrCodeSuccessCallback = (decodedText, decodedResult) => {
				/* handle success */
				if (decodedText !== ''){
                    // Handle on success condition with the decoded message.
                    console.log(`Scan result ${decodedText}`, decodedText);
					let objQRCode = JSON.parse(decodedText);
					$('#txtfindorderno').val(objQRCode.orderno);
					$('#selfindordercat').val(objQRCode.ordercat);
                    //$('#txtfindorderno').val(objQRCode.shipmentno);
					//$('#selfindordercat').val(objQRCode.shiptmentcat);
                    closeQRShipment();
				}
			};
			const config = { fps: 10, qrbox: 250 };
			// If you want to prefer back camera
			html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
        }
        function closeQRShipment() {
            $('#myQRShipment').modal('hide');
			html5QrCode.stop().then((ignore) => {
			  // QR Code scanning is stopped.
			  console.log("Stop", ignore)
			}).catch((err) => {
			  // Stop failed, handle it.
			  console.log("Error", err)
			});
			setTimeout(function() {
				$('body').addClass('modal-open');
			}, 400);
        }

        function openBulkShipment() {
            //$('#myBulkShipment').modal({ backdrop: "static" });
			$('#myBulkShipment').modal('show');
			resetSearch();
        }

        function closeBulkShipment() {
            $('#myBulkShipment').modal('hide');
        }

        function searchOrder() {
            var parameters_getFBZM_PendingShipmentList = ["comp", "<%=sCurrComp%>", "bpid", "", "giveno", $('#txtfindorderno').val(), "ordercat", $('#selfindordercat').val()];
            PageMethod("getFBZM_PendingShipmentList", parameters_getFBZM_PendingShipmentList, succeededAjaxFn_getFBZM_PendingShipmentList, failedAjaxFn_getFBZM_PendingShipmentList, false);
            
        }

        //notification for failed ajax transaction
        succeededAjaxFn_getFBZM_PendingShipmentList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getFBZM_PendingShipmentList: " + textStatus);
            oTable.clear().draw();

            result_getFBZM_PendingShipmentList = JSON.parse(data.d);
            $.each(result_getFBZM_PendingShipmentList.arraypendshipmentno, function (i, result) {
                oTable.row.add([
                    i + 1,
                    result.GetSetcomp,
                    result.GetSetshipmentno,
                    result.GetSetlineno,
                    result.GetSetorderno,
                    result.GetSetitemno,
                    result.GetSetorder_quantity,
                    result.GetSetitemno + '<br/>' + result.GetSetitemdesc + '<br/>Qty Order: ' + result.GetSetorder_quantity,
                    '<input id="qty_shipment_' + result.GetSetlineno + '" name="qty_shipment' + result.GetSetlineno + '" type="text" value="' + result.GetSetquantity + '" style="width:30px;"/>',
                    '<select id="location_shipment' + result.GetSetlineno + '" name="location_shipment' + result.GetSetlineno + ' class="select form-control"><option value="">-Select-</option></select><input type="text" id="item_location' + result.GetSetlineno + '" name="item_location' + result.GetSetlineno + '" value ="" readonly class="product_price form-control"/><input type="text" id ="item_datesoh' + result.GetSetlineno + '" name="item_datesoh' + result.GetSetlineno + '" value="" readonly class="product_price form-control"/><input type="text" id="item_qtyavailable' + result.GetSetlineno + '" name="item_qtyavailable' + result.GetSetlineno + '" value="" readonly class="product_price form-control"/>'
                ]).draw(false);
            });

            if (result_getFBZM_PendingShipmentList.countpendshipment > 0) {
                $('#tblSearchOrder').hide();
                $('#tblOrderlist').show();
                $('#tblNextAction').show();

                oTable.rows().every(function (x, element) {
                    var data = this.data();
                    //get location stock availability
                    $.getJSON(urlhandler + "/GeneralHandler.ashx?action=GET_STOCKAVAILABLE&comp=<%=sCurrComp%>&itemno=" + data[5],
                        function (mps) {
                            //reset stockavailable, hidden field of stock information & balance order qty
                            var select = document.getElementById("location_shipment" + data[3]);
                            for (var option in select) {
                                select.remove(option);
                            }
                            document.getElementById("location_shipment" + data[3]).add(new Option("-Select-", ""));

                            var x = 0;
                            for (var i in mps) {
                                //pure javascript
                                var x = x + 1;
                                document.getElementById("location_shipment" + data[3]).add(new Option("STOK " + x, mps[i].item + "|" + mps[i].location + "|" + mps[i].datesoh + "|" + mps[i].qtysoh + "|" + mps[i].qtyavailable));
                                //using jquery
                            }

                            $('#location_shipment' + data[3]).change(function () {
                                if ($(this).val() == '') {
                                    $('#item_location' + data[3]).val('');
                                    $('#item_datesoh' + data[3]).val('');
                                    $('#item_qtyavailable' + data[3]).val('0');
                                    $('#qty_shipment_' + data[3]).val(setQtyToShipment(0, 0));
                                } else {
                                    //get latest information about stockavailability
                                    var stockitem = $(this).val().split('|');
                                    $('#item_location' + data[3]).val(stockitem[1]);
                                    $('#item_datesoh' + data[3]).val(stockitem[2]);
                                    $('#item_qtyavailable' + data[3]).val(stockitem[4]);
                                    $('#qty_shipment_' + data[3]).val(setQtyToShipment(data[6], stockitem[4]));
                                }
                            });

                            //console.log('#location_shipment' + data[3] + ": " + document.getElementById("location_shipment" + data[3]).options.length-1);
                            $('#location_shipment' + data[3]).prop('selectedIndex', document.getElementById("location_shipment" + data[3]).options.length - 1);

                            if ($('#location_shipment' + data[3]).val() == '') {
                                $('#item_location' + data[3]).val('');
                                $('#item_datesoh' + data[3]).val('');
                                $('#item_qtyavailable' + data[3]).val('0');
                                $('#qty_shipment_' + data[3]).val(setQtyToShipment(0, 0));
                            } else {
                                //get latest information about stockavailability
                                var stockitem = $('#location_shipment' + data[3]).val().split('|');
                                $('#item_location' + data[3]).val(stockitem[1]);
                                $('#item_datesoh' + data[3]).val(stockitem[2]);
                                $('#item_qtyavailable' + data[3]).val(stockitem[4]);
                                $('#qty_shipment_' + data[3]).val(setQtyToShipment(data[6], stockitem[4]));
                            }

                            //setSelectedIndexByIndex(document.getElementById("location_shipment" + data[3]), document.getElementById("location_shipment" + data[3]).options.length);

                        });
                });

            } else {
                resetSearch();
                alert("Record not found!");
            }

        }

        failedAjaxFn_getFBZM_PendingShipmentList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getFBZM_PendingShipmentList: " + textStatus);
        }

        function createShipment() {
            var proceedprocess = false;
            oTable.rows().every(function (i, element) {
                var data = this.data();
                if ($('#qty_shipment_' + data[3]).val() != "0") {
                    proceedprocess = true;
                }
            });

            if (proceedprocess) {
                //create shipment order
                var parameters_createFBZM_ShipmentOrder = ["comp", "<%=sCurrComp%>", "giveno", $('#txtfindorderno').val(), "userid", "<%=sUserId%>", "ordercat", $('#selfindordercat').val()];
                PageMethod("createFBZM_ShipmentOrder", parameters_createFBZM_ShipmentOrder, succeededAjaxFn_createFBZM_ShipmentOrder, failedAjaxFn_createFBZM_ShipmentOrder, false);

                if (saveshipment && shipmentno != "") {
                    //add line item for shipment order
                    var x = 1;
                    oTable.rows().every(function (i, element) {
                        var data = this.data();
                        //alert(data[1] + ":" + data[3] + ":" + data[4] + ":" + data[5] + ":" + $('#qty_shipment_' + i).val() + ":" + $('#location_shipment' + i).val());
                        if ($('#qty_shipment_' + data[3]).val() != "0") {
                            var parameters_addFBZM_ShipmentItemOrder = ["comp", "<%=sCurrComp%>", "shipmentno", shipmentno, "shipmentlineno", x, "giveno", $('#txtfindorderno').val(), "lineno", data[3], "itemno", data[5], "qty", $('#qty_shipment_' + data[3]).val(), "location", $('#item_location' + data[3]).val(), "datesoh", $('#item_datesoh' + data[3]).val(), "qtyavailable", $('#item_qtyavailable' + data[3]).val(), "ordercat", $('#selfindordercat').val()];
                            PageMethod("addFBZM_ShipmentItemOrder", parameters_addFBZM_ShipmentItemOrder, succeededAjaxFn_addFBZM_ShipmentItemOrder, failedAjaxFn_addFBZM_ShipmentItemOrder, false);
                        }
                        x = x + 1;
                    });

                    //confirm shipment order
                    //var parameters_updateFBZM_ShipmentOrderStatus = ["comp", comp, "shipmentno", shipmentno, "status", "CONFIRMED", "userid", userid];
                    //PageMethod("updateFBZM_ShipmentOrderStatus", parameters_updateFBZM_ShipmentOrderStatus, succeededAjaxFn_updateFBZM_ShipmentOrderStatus, failedAjaxFn_updateFBZM_ShipmentOrderStatus, false);

                    if (saveshipment) {
                        closeBulkShipment();
                        openeditshipment("<%=sCurrComp%>", shipmentno);
                    }
                }
            } else {
                alert("Terdapat Qty Hantar yang bernilai 0!");
            }

        }

        //notification for failed ajax transaction
        succeededAjaxFn_createFBZM_ShipmentOrder = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createFBZM_ShipmentOrder: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                saveshipment = true;
                shipmentno = result.message;
            } else {
                saveshipment = false;
                shipmentno = "";
            }
        }

        failedAjaxFn_createFBZM_ShipmentOrder = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createFBZM_ShipmentOrder: " + textStatus);
            saveshipment = false;
            shipmentno = "";
        }

        //notification for failed ajax transaction
        succeededAjaxFn_addFBZM_ShipmentItemOrder = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addFBZM_ShipmentItemOrder: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                saveshipment = true;
            }
            else {
                saveshipment = false;
            }
        }

        failedAjaxFn_addFBZM_ShipmentItemOrder = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addFBZM_ShipmentItemOrder: " + textStatus);
            saveshipment = false;
        }


        function resetSearch() {
            $('#tblSearchOrder').show();
            $('#tblOrderlist').hide();
            $('#tblNextAction').hide();
        }

        function setSelectedIndexByName(s, v) {
            for (var i = 0; i < s.options.length; i++) {
                if (s.options[i].text == v) {
                    s.options[i].selected = true;
                    return;
                }
            }
        }

        function setSelectedIndexByValue(s, valsearch) {
            // Loop through all the items in drop down list
            for (i = 0; i < s.options.length; i++) {
                if (s.options[i].value == valsearch) {
                    // Item is found. Set its property and exit
                    s.options[i].selected = true;
                    break;
                }
            }
            return;
        }

        function setSelectedIndexByIndex(s, i) {
            s.options[i - 1].selected = true;
            return;
        }

        function setQtyToShipment(qtyorder, qtyavailable) {
            if (qtyavailable > qtyorder) {
                return qtyorder;
            } else {
                return qtyavailable;
            }
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
        openaddnewshipment();
        <%
        }
        %>
    </script>
</asp:Content>

