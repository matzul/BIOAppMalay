<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="ReceiptListing.aspx.cs" Inherits="ReceiptListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Penerimaan Pesanan <small>SENARAI</small></h2>
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
                        <a class="btn btn-app" onclick="openaddnewreceipt();">
                          <i class="fa fa-plus-square green"></i>Daftar
                        </a>
                        <a class="btn btn-app" onclick="openbulkreceipt();">
                          <i class="fa fa-barcode"></i> Nota Terimaan
                        </a>
                        <a class="btn btn-app" onclick="enabledisablesearchbox();">
                          <i class="fa fa-search"></i> Carian
                        </a>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12" id="search-section" style="display:none;">
                        <section class="panel">
                            <div class="x_title">
                                <h2>Carian Penerimaan Pesanan</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="panel-body">
                                <form id="search" runat="server">
                                    <div class="form-group">
                                        <label for="receiptno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Penerimaan:</label>
                                        <input type="text" id="receiptno" class="form-control" name="receiptno" value="<%=sReceiptNo %>" />
                                    </div>
                                    <div class="form-group">
                                        <label for="bpid" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Terima Daripada:</label>
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
                          <th>No. Penerimaan</th>
                          <th>Terima Daripada</th>
                          <th>Alamat</th>
                          <th>Tarikh Penerimaan</th>
                          <th>Kategori</th>
                          <th>Status</th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsReceiptHeader.Count > 0)
                            {
                                for (int i = 0; i < lsReceiptHeader.Count; i++)
                                {
                                    MainModel modHdr = (MainModel)lsReceiptHeader[i];
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditreceipt('<%=modHdr.GetSetcomp %>','<%=modHdr.GetSetreceiptno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditreceipt('<%=modHdr.GetSetcomp %>','<%=modHdr.GetSetreceiptno %>');"><%=modHdr.GetSetreceiptno %></a></td>
                              <td><%=modHdr.GetSetbpdesc %></td>
                              <td><%=modHdr.GetSetbpaddress %></td>
                              <td><%=modHdr.GetSetreceiptdate %></td>
                              <td><%=modHdr.GetSetreceiptcat %></td>
                              <td><%=modHdr.GetSetstatus %></td>
                            </tr>
                        <% 
                                }
                            } else {
                        %>
                            <tr>
                                <td></td>
                                <td>Rekod tiada...</td>
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
            <div class="modal fade" id="myBulkReceive" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            Daftar Nota Terimaan
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
                                            <option value="PURCHASE_ORDER">PESANAN BELIAN</option>
                                            <option value="RECEIVE_ORDER">PESANAN TERIMAAN</option>
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
                                        <th style="display:none;">ReceiptNo</th>
                                        <th style="display:none;">LineNo</th>
                                        <th>OrderNo</th>
                                        <th style="display:none;">ItemNo</th>
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
                                            <button type="button" onclick="createReceive();" class="btn btn-primary">Daftar</button>
                                            <button type="button" onclick="resetSearch();" class="btn btn-warning">Kembali</button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" onclick="closeBulkReceive();">Tutup</button>
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
        var savereceipt = false;
        var receiptno = "";
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
                    }
                ]
            });

            resetSearch();
        });

        function openaddnewreceipt() {
            var popupWindow = window.open("ReceiptDetails.aspx?action=ADD", "add_receipt", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
        function openeditreceipt(comp, receiptno) {
            var popupWindow = window.open("ReceiptDetails.aspx?action=OPEN&comp=" + comp + "&receiptno=" + receiptno, "open_receipt", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
                    $('#txtfindorderno').val(objQRCode.shipmentno);
					$('#selfindordercat').val(objQRCode.shiptmentcat);
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

        function openbulkreceipt() {
            $('#myBulkReceive').modal({ backdrop: "static" });
        }

        function closeBulkReceive() {
            $('#myBulkReceive').modal('hide');
        }

        function searchOrder() {
            var parameters_getFBZM_PendingReceiptList = ["comp", "<%=sCurrComp%>", "bpid", "", "receiveno", $('#txtfindorderno').val(), "ordercat", $('#selfindordercat').val()];
            PageMethod("getFBZM_PendingReceiptList", parameters_getFBZM_PendingReceiptList, succeededAjaxFn_getFBZM_PendingReceiptList, failedAjaxFn_getFBZM_PendingReceiptList, false);
        }

        //notification for succeeded & failed ajax transaction
        succeededAjaxFn_getFBZM_PendingReceiptList = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getFBZM_PendingReceiptList: " + textStatus);

            oTable.clear().draw();

            result_getFBZM_PendingReceiptList = JSON.parse(data.d);

            $.each(result_getFBZM_PendingReceiptList.arraypendreceiptno, function (i, result) {
                oTable.row.add([
                    i + 1,
                    result.GetSetcomp,
                    result.GetSetreceiptno,
                    result.GetSetlineno,
                    result.GetSetorderno,
                    result.GetSetitemno,
                    result.GetSetitemno + '<br/>' + result.GetSetitemdesc + '<br/>Qty Order: ' + result.GetSetorder_quantity,
                    '<input id="qty_receipt_' + i + '" name="qty_receipt' + i + '" type="text" value="' + result.GetSetorder_quantity + '" style="width:30px;"/>',
                    '<select id="location_receipt' + i + '" name="location_receipt' + i + '"><option value="' + result.GetSetordertype + '">' + result.GetSetordertype + '</option></select>'
                ]).draw(false);
            });
            if (result_getFBZM_PendingReceiptList.countpendreceipt > 0) {
                $('#tblSearchOrder').hide();
                $('#tblOrderlist').show(); 
                $('#tblNextAction').show(); 
            } else {
                resetSearch();
                alert("Record not found!");
            }
        }

        failedAjaxFn_getFBZM_PendingReceiptList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getFBZM_PendingReceiptList: " + textStatus);
            resetSearch();
        }

        function createReceive() {
            var proceedprocess = false;
            oTable.rows().every(function (i, element) {
                var data = this.data();
                if ($('#qty_receipt_' + i).val() != "0") {
                    proceedprocess = true;
                }
            });

            if (proceedprocess) {
                //create receipt order
                var parameters_createFBZM_ReceiptOrder = ["comp", "<%=sCurrComp%>", "receiveno", $('#txtfindorderno').val(), "userid", "<%=sUserId%>", "ordercat", $('#selfindordercat').val()];
                PageMethod("createFBZM_ReceiptOrder", parameters_createFBZM_ReceiptOrder, succeededAjaxFn_createFBZM_ReceiptOrder, failedAjaxFn_createFBZM_ReceiptOrder, false);

                if (savereceipt && receiptno != "") {
                    //add line item for receipt order
                    var x = 1;
                    oTable.rows().every(function (i, element) {
                        var data = this.data();
                        //alert(data[1] + ":" + data[3] + ":" + data[4] + ":" + data[5] + ":" + $('#qty_receipt_' + i).val() + ":" + $('#location_receipt' + i).val());

                        var parameters_addFBZM_ReceiptItemOrder = ["comp", "<%=sCurrComp%>", "receiptno", receiptno, "receiptlineno", x, "receiveno", $('#txtfindorderno').val(), "lineno", data[3], "itemno", data[5], "qty", $('#qty_receipt_' + i).val(), "location", $('#location_receipt' + i).val(), "ordercat", $('#selfindordercat').val()];
                        PageMethod("addFBZM_ReceiptItemOrder", parameters_addFBZM_ReceiptItemOrder, succeededAjaxFn_addFBZM_ReceiptItemOrder, failedAjaxFn_addFBZM_ReceiptItemOrder, false);

                        x = x + 1;
                    });

                    //confirm receipt order
                    //var parameters_updateFBZM_ReceiptOrderStatus = ["comp", comp, "receiptno", receiptno, "status", "CONFIRMED", "userid", userid];
                    //PageMethod("updateFBZM_ReceiptOrderStatus", parameters_updateFBZM_ReceiptOrderStatus, succeededAjaxFn_updateFBZM_ReceiptOrderStatus, failedAjaxFn_updateFBZM_ReceiptOrderStatus, false);

                    if (savereceipt) {
                        closeBulkReceive();
                        openeditreceipt("<%=sCurrComp%>", receiptno);
                    }
                }
            } else {
                alert("Terdapat Qty Terima yang bernilai 0!");
            }

        }

        //notification for failed ajax transaction
        succeededAjaxFn_createFBZM_ReceiptOrder = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_createFBZM_ReceiptOrder: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                savereceipt = true;
                receiptno = result.message;
            } else {
                savereceipt = false;
                receiptno = "";
            }
        }

        failedAjaxFn_createFBZM_ReceiptOrder = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_createFBZM_ReceiptOrder: " + textStatus);
            savereceipt = false;
            receiptno = "";
        }

        //notification for failed ajax transaction
        succeededAjaxFn_addFBZM_ReceiptItemOrder = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_addFBZM_ReceiptItemOrder: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                savereceipt = true;
            }
            else {
                savereceipt = false;
            }
        }

        failedAjaxFn_addFBZM_ReceiptItemOrder = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_addFBZM_ReceiptItemOrder: " + textStatus);
            savereceipt = false;
        }

        function resetSearch() {
            $('#tblSearchOrder').show();
            $('#tblOrderlist').hide();
            $('#tblNextAction').hide();
        }

        <%
        if (sAction.Equals("ADD"))
        {
        %>
        openaddnewreceipt();
        <%
        }
        %>
    </script>
</asp:Content>

