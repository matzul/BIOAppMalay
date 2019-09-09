<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="ItemDetails.aspx.cs" Inherits="ItemDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">

        .pop_div {
            box-shadow: 0px 0px 5px 2px #999;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            position: absolute;
            width: 640px;
            height: 520px;
            background-color: #FFF;
            z-index: 9995;
            border: #999 1px solid;
            padding-right: 7px;
            padding-top: 7px;
            margin: auto;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
        }
        /*
        .max_div {
            box-shadow: 0px 0px 5px 2px #888;
            position: absolute;
            left: 0px;
            top: 60px;
            width: 99.7%;
            height: 89.3%;
            background-color: #FFF;
            z-index: 999;
            border: #777 1px solid;
            margin-left: 1px;
        }
        */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2><%=sActionString %></h2>
                <ul class="nav navbar-right panel_toolbox">
                </ul>
                <div class="clearfix"></div>
            </div>
            <form id="form1" runat="server">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form1">
                            <label for="itemno">Kod Item:</label>
                            <input type="text" id="itemno" class="form-control" required="required" name="itemno" value="<%=oModItem.GetSetitemno %>" />
                            <label for="itemcat">Kategori:</label>
                            <select class="form-control" id="itemcat" name="itemcat" required="required">
                                <option value="">-select-</option>
                                <option value="INVENTORY" <%=oModItem.GetSetitemcat.Equals("INVENTORY")?"selected":"" %>>INVENTORI</option>
                                <option value="SERVICE" <%=oModItem.GetSetitemcat.Equals("SERVICE")?"selected":"" %>>SERVIS</option>
                                <!--<option value="ASSET" <%=oModItem.GetSetitemcat.Equals("ASSET")?"selected":"" %>>ASET</option>-->
                            </select>
                            <label for="itemtype">Ukuran Item:</label>
                            <select class="form-control" id="itemtype" name="itemtype" required="required">
                                <option value="">-select-</option>
                                <option value="UNIT" <%=oModItem.GetSetitemtype.Equals("UNIT")?"selected":"" %>>UNIT</option>
                                <option value="PC" <%=oModItem.GetSetitemtype.Equals("PC")?"selected":"" %>>PC</option>
                                <option value="SET" <%=oModItem.GetSetitemtype.Equals("SET")?"selected":"" %>>SET</option>
                                <option value="SLOT" <%=oModItem.GetSetitemtype.Equals("SLOT")?"selected":"" %>>SLOT</option>
                                <option value="JOB" <%=oModItem.GetSetitemtype.Equals("JOB")?"selected":"" %>>PERKHIDMATAN</option>
                            </select>
                            <label for="purchaseprice">Harga Belian:</label>
                            <input type="text" id="purchaseprice" class="form-control" name="purchaseprice" value="<%=oModItem.GetSetpurchaseprice %>" required="required" />
                            <label for="costprice">Harga Kos:</label>
                            <input type="text" id="costprice" class="form-control" name="costprice" value="<%=oModItem.GetSetcostprice %>" required="required" />
                            <label for="salesprice">Harga Jualan:</label>
                            <input type="text" id="salesprice" class="form-control" name="salesprice" value="<%=oModItem.GetSetsalesprice %>" required="required" />
                            <label for="itemstatus">Status:</label>
                            <select class="form-control" id="itemstatus" name="itemstatus" required="required">
                                <option value="">-select-</option>
                                <option value="ACTIVE" <%=oModItem.GetSetitemstatus.Equals("ACTIVE")?"selected":"" %>>AKTIF</option>
                                <option value="IN-ACTIVE" <%=oModItem.GetSetitemstatus.Equals("IN-ACTIVE")?"selected":"" %>>TIDAK AKTIF</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="itemdesc">Keterangan Item:</label>
                            <textarea id="itemdesc" class="form-control" rows="3" name="itemdesc" required="required"><%=oModItem.GetSetitemdesc %></textarea>
                            <label for="qtyorder">Qty Pesanan:</label>
                            <input type="text" id="qtyorder" class="form-control" readonly="readonly" name="qtyorder" value="<%=oModItem.GetSetqtyorder %>" />
                            <label for="qtydemand">Qty Permintaan:</label>
                            <input type="text" id="qtydemand" class="form-control" readonly="readonly" name="qtydemand" value="<%=oModItem.GetSetqtydemand %>" />
                            <label for="qtysoh">Qty SOH:</label>
                            <input type="text" id="qtysoh" class="form-control" readonly="readonly" name="qtysoh" value="<%=oModItem.GetSetqtysoh %>" />
                            <label for="costsoh">KOS SOH:</label>
                            <input type="text" id="costsoh" class="form-control" readonly="readonly" name="costsoh" value="<%=oModItem.GetSetcostsoh %>" />
                            <label for="qtysafetystock">Qty Minimum:</label>
                            <input type="text" id="qtysafetystock" class="form-control" required="required" name="qtysafetystock" value="<%=oModItem.GetSetqtysafetystock %>" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <section class="panel">
                        <div class="panel-body">
                            <div id="action-form">
                                <%
                                    if (sAction.Equals("ADD"))
                                    {
                                %>
                                <button id="btnCreate" name="btnCreate" type="button" class="btn btn-info" onclick="actionclick('CREATE');">Daftar</button>
                                <button id="btnReset" name="btnReset" type="button" class="btn btn-warning" onclick="actionclick('ADD');">Reset</button>
                                <%
                                    }
                                    else if (sAction.Equals("OPEN"))
                                    {
                                %>
                                <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');">Kemaskini</button>
                                <%
                                    }
                                    else if (sAction.Equals("EDIT"))
                                    {
                                %>
                                <button id="btnSave" name="btnSave" type="button" class="btn btn-success" onclick="actionclick('SAVE');">Simpan</button>
                                <%
                                    }
                                %>
                                <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();">Tutup</button>
                                <%
                                    MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
                                    if (oAlerMssg.GetSetalertstatus.Equals("SUCCESS"))
                                    {
                                %>
                                <div class="alert alert-success alert-dismissible fade in" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <strong>Success!</strong> <%=oAlerMssg.GetSetalertmessage %>
                                </div>
                                <%
                                    }
                                    else if (oAlerMssg.GetSetalertstatus.Equals("ERROR"))
                                    {
                                %>
                                <div class="alert alert-danger alert-dismissible fade in" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <strong>Error!</strong> <%=oAlerMssg.GetSetalertmessage %>
                                </div>
                                <%
                                    }
                                    //to reset alertmessage
                                    sAlertMessage = "";
                                %>
                                <div style="display: none;">
                                    <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                    <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                    <input type="hidden" name="hidItemNo" id="hidItemNo" value="<%=sItemNo %>" />
                                    <input type="hidden" name="hidLineNo" id="hidLineNo" value="" />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
                <!--BEGIN dialog box for add discount item-->
                <div class="modal fade modal-add-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Tambah Jadual Harga Item</h4>
                            </div>
                            <div class="modal-body">
                                <div id="form2" class="form-horizontal form-label-left">

                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Bil</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="addlineno" name="addlineno" type="text" class="form-control" readonly="readonly" value="" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kod Item</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="additemno" name="additemno" type="text" class="form-control" readonly="readonly" value="<%=oModItem.GetSetitemno %>" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kategori <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <select id="addordercat" name="addordercat" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                                <option value="SALES_ORDER">PESANAN JUALAN</option>
                                                <option value="PURCHASE_ORDER">PESANAN BELIAN</option>
                                                <option value="TRANSFER_ORDER">PESANAN PINDAHAN</option>
                                                <!--
                                                <option value="GIVE_ORDER">PESANAN AGIHAN</option>
                                                <option value="RECEIVE_ORDER">PESANAN TERIMAAN</option>
                                                <option value="ONLINE_ORDER">JUALAN ON-LINE</option>
                                                -->
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Status ON-LINE</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <select id="addstatus" name="addstatus" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                                <option value="NOT_APPLICABLE">TIDAK BERKENAAN</option>
                                                <option value="PROCESSING">SEDANG DIPROSES</option>
                                                <option value="PUBLISHED">AKTIF</option>
                                                <option value="CANCELLED">TIDAK AKTIF</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jenis Transaksi <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <select id="addordertype" name="addordertype" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                                <option value="NORMAL">NORMAL</option>
                                                <option value="PROMOTION">PROMOSI</option>
                                                <option value="AGENT">AGENT</option>
                                                <option value="STOCKIST">STOCKIST</option>
                                                <!--
                                                <option value="PUSAT_BEKALAN">PUSAT BEKALAN</option>
                                                <option value="JOM_SADAQAH">JOM SADAQAH</option>
                                                -->
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Item </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="addunitprice" name="addunitprice" type="text" class="form-control" readonly="readonly" value="<%=oModItem.GetSetsalesprice %>" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kategori Diskaun <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <select id="adddisccat" name="adddisccat" class="select2_single form-control" tabindex="-1" style="width: 100%;">
                                                <option value="PERCENTAGE">PERATUSAN (%)</option>
                                                <option value="AMOUNT">JUMLAH (RM)</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Nilai Diskaun <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="adddiscvalue" name="adddiscvalue" type="text" class="form-control" value="0" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Jumlah Diskaun </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="adddiscamount" name="adddiscamount" type="text" class="form-control" readonly="readonly" value="0" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Harga Slps Diskaun </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <input id="additemprice" name="additemprice" type="text" class="form-control" readonly="readonly" value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary" id="btnAddLineItem" onclick="actionclick('INSERT');">Tambah</button>
                                <button type="button" class="btn btn-primary" id="btnEditLineItem" onclick="actionclick('UPDATE');">Simpan</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                            </div>

                        </div>
                    </div>
                </div>
                <!--END dialog box for add discount item-->
                <!--BEGIN dialog box for confirm delete-->
                <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Anda pasti untuk keluarkan Jadual Harga Item ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnDeleteLineItem" onclick="actionclick('DELETE');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                        </div>
                    </div>
                </div>
                <!--END dialog box for confirm delete-->
            </form>
            
            <div id='fade_div' onclick="close_popup();" style='position: absolute; top: 0; left: 0; z-index: 9990; width: 100%; height: 100%; background-color: #000; opacity: 0.25; filter: alpha(opacity=25); display: none;'>
            </div>
            
            <!-- popup apps -->
            <div id="pop_div" class="pop_div" style="display: none;">
                <div id="noticebar" style="width: 100%;">
                    <div style="float: right; cursor: pointer;">
                        <img src="images/bdel.png" height="18px" border="0" title="Tutup" onclick="close_popup();" />
                    </div>
                </div>
                <iframe id="pop_content" frameborder="0" style="padding-top:5px; padding-bottom:5px;" width="637px" height="480px" name="pop_content" src="Loading.aspx" allowtransparency="true"></iframe>
            </div>

            <div class="col-md-12 col-sm-12 col-xs-12">
                <a id="addlineitem" name="addlineitem" class="btn btn-app" data-toggle="modal" data-target=".modal-add-line-item" onclick="openaddlineitem();">
                    <i class="fa fa-plus-square green"></i>Tambah Jadual Harga
                </a>
                <a id="addimageitem" name="addimageitem" class="btn btn-app" onclick="open_popup('ItemImageUpload.aspx?action=OPEN&itemno=<%=sItemNo %>');">
                    <i class="fa fa-image dark"></i>Muat Naik Gambar
                </a>
                <div class="table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Kategori</th>
                                <th>Status ON-LINE</th>
                                <th>Jenis Transaksi</th>
                                <th>Harga Item</th>
                                <th>Kategori Diskaun</th>
                                <th>Nilai Diskaun</th>
                                <th>Jumlah Diskaun</th>
                                <th>Harga Slps Diskaun</th>
                                <th></th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsItemDiscount.Count > 0)
                                {
                                    for (int i = 0; i < lsItemDiscount.Count; i++)
                                    {
                                        MainModel modItemDisc = (MainModel)lsItemDiscount[i];
                            %>
                            <tr>
                                <td><%=i+1%></td>
                                <td><%=modItemDisc.GetSetordercat %></td>
                                <td><%=modItemDisc.GetSetstatus %></td>
                                <td><%=modItemDisc.GetSetdiscounttype %></td>
                                <td><%=(modItemDisc.GetSetordercat.Equals("PURCHASE_ORDER")?modItemDisc.GetSetpurchaseprice:(modItemDisc.GetSetordercat.Equals("SALES_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("ONLINE_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("RECEIVE_ORDER")?modItemDisc.GetSetpurchaseprice:(modItemDisc.GetSetordercat.Equals("GIVE_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("TRANSFER_ORDER")?modItemDisc.GetSetcostprice:0)))))) %></td>
                                <td><%=modItemDisc.GetSetdisccat %></td>
                                <td><%=modItemDisc.GetSetdiscvalue %></td>
                                <td><%=modItemDisc.GetSetdiscamount %></td>
                                <td><%=(modItemDisc.GetSetordercat.Equals("PURCHASE_ORDER")?modItemDisc.GetSetpurchaseprice:(modItemDisc.GetSetordercat.Equals("SALES_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("ONLINE_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("RECEIVE_ORDER")?modItemDisc.GetSetpurchaseprice:(modItemDisc.GetSetordercat.Equals("GIVE_ORDER")?modItemDisc.GetSetsalesprice:(modItemDisc.GetSetordercat.Equals("TRANSFER_ORDER")?modItemDisc.GetSetcostprice:0)))))) - modItemDisc.GetSetdiscamount %></td>
                                <td>
                                    <a href="#" class="btn btn-info btn-xs" onclick="openeditlineitem('<%=modItemDisc.GetSetcomp %><%=modItemDisc.GetSetdiscounttype %><%=modItemDisc.GetSetitemno %><%=modItemDisc.GetSetordercat %>');" data-toggle="modal" data-target=".modal-add-line-item"><i class="fa fa-pencil"></i>Edit </a>
                                    <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modItemDisc.GetSetcomp %><%=modItemDisc.GetSetdiscounttype %><%=modItemDisc.GetSetitemno %><%=modItemDisc.GetSetordercat %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i>Delete </a>
                                </td>
                            </tr>
                            <% 
                                    }
                                }
                                else
                                {
                            %>
                            <tr>
                                <td></td>
                                <td>Tiada rekod...</td>
                                <td></td>
                                <td></td>
                                <td></td>
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
    <script type="text/javascript">

        $(document).ready(function () {
            calculateTotalAmount($('#adddisccat').val());

            $('#addordercat').change(function () {
                if ($(this).val() == 'SALES_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetsalesprice %>');
                    $('#addstatus').val('NOT_APPLICABLE').change();
                }
                else if ($(this).val() == 'PURCHASE_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetpurchaseprice %>');
                    $('#addstatus').val('NOT_APPLICABLE').change();
                }
                else if ($(this).val() == 'GIVE_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetsalesprice %>');
                    $('#addstatus').val('NOT_APPLICABLE').change();
                }
                else if ($(this).val() == 'RECEIVE_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetpurchaseprice %>');
                    $('#addstatus').val('NOT_APPLICABLE').change();
                }
                else if ($(this).val() == 'ONLINE_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetsalesprice %>');
                    $('#addstatus').val('PROCESSING').change();
                }
                else if ($(this).val() == 'TRANSFER_ORDER') {
                    $('#addunitprice').val('<%=oModItem.GetSetcostprice %>');
                    $('#addstatus').val('NOT_APPLICABLE').change();
                }
                calculateTotalAmount($('#adddisccat').val());
            });
            $('#adddisccat').change(function () {
                calculateTotalAmount($(this).val());
            });
            $('#adddiscvalue').change(function () {
                calculateTotalAmount($('#adddisccat').val());
            });
        });

    function calculateTotalAmount(type) {
        if (type == 'PERCENTAGE') {
            var adddiscamount = $('#addunitprice').val() * $('#adddiscvalue').val() / 100;
            $('#adddiscamount').val(adddiscamount.toFixed(2));
            var additemprice = parseFloat($('#addunitprice').val()) - parseFloat($('#adddiscamount').val());
            $('#additemprice').val(additemprice.toFixed(2));
        } else if (type == 'AMOUNT') {
            $('#adddiscamount').val($('#adddiscvalue').val());
            var additemprice = parseFloat($('#addunitprice').val()) - parseFloat($('#adddiscamount').val());
            $('#additemprice').val(additemprice.toFixed(2));
        }
    }

    function actionclick(action) {
        var proceed = true;
        if (action == 'ADD') {
            $('#itemno').removeAttr('required');
            $('#itemdesc').removeAttr('required');
            $('#itemcat').removeAttr('required');
            $('#itemtype').removeAttr('required');
            $('#purchaseprice').removeAttr('required');
            $('#costprice').removeAttr('required');
            $('#salesprice').removeAttr('required');
            $('#itemstatus').removeAttr('required');
            $('#qtysafetystock').removeAttr('required');
        }
        if (action == 'INSERT' || action == 'UPDATE') {

            if ($('#hidLineNo').val().length == 0 && action == 'UPDATE') {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'System Error, please contact system admin!',
                    type: 'error',
                    styling: 'bootstrap3'
                });
            }
            else if ($('#addlineno').val().length == 0) {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'Please fill out "Line No" field!',
                    type: 'warning',
                    styling: 'bootstrap3'
                });
            }
            else if ($('#additemno').val().length == 0) {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'Please  fill out "Item No" field!',
                    type: 'warning',
                    styling: 'bootstrap3'
                });
            }
            else if ($('#addunitprice').val().length == 0) {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'Please  fill out "Unit Price" field!',
                    type: 'warning',
                    styling: 'bootstrap3'
                });
            }
            else if (parseFloat($('#addunitprice').val()) == 0) {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'Pernambahan tidak boleh berlaku untuk Harga Unit adalah "0"!',
                    type: 'warning',
                    styling: 'bootstrap3'
                });
            }
        }
        if (action == 'DELETE') {
            if ($('#hidLineNo').val().length == 0) {
                proceed = false;
                new PNotify({
                    title: 'Alert',
                    text: 'System Error, please contact system admin!',
                    type: 'error',
                    styling: 'bootstrap3'
                });
            }
        }
        if (proceed) {
            $('#addordercat').prop('disabled', false);
            $('#addordertype').prop('disabled', false);
            $('#addstatus').prop('disabled', false);

            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }
    }

    function enabledisableinputform(flag) {
        <% if (sAction.Equals("EDIT"))
        {
            %>
        $('#itemno').prop('disabled', true);
                    <% }
        else
        {%>
        $('#itemno').prop('disabled', flag);
                    <% }%>
        $('#itemdesc').prop('disabled', flag);
        $('#itemcat').prop('disabled', flag);
        $('#itemtype').prop('disabled', flag);
        $('#purchaseprice').prop('disabled', flag);
        $('#costprice').prop('disabled', flag);
        $('#salesprice').prop('disabled', flag);
        $('#itemstatus').prop('disabled', flag);
        $('#qtysafetystock').prop('disabled', flag);
    }
                <%
        if (sAction.Equals("ADD") || sAction.Equals("EDIT"))
        {
                %>
        enabledisableinputform(false);
                <%
        }
        else
        {
                %>
        enabledisableinputform(true);
        <%
        }
                %>

        function openaddlineitem() {
            $('#btnAddLineItem').show();
            $('#btnEditLineItem').hide();

            $('#hidLineNo').val('');
            $('#addlineno').val('<%=lsItemDiscount.Count + 1%>');

            //enable related fields
            $('#addordercat').prop('disabled', false);
            $('#addordertype').prop('disabled', false);
            <%
        if (sUSerType.Equals("00"))
        {
            %>
            $('#addstatus').prop('disabled', false);
            <%
        }
        else
        {
            %>
            $('#addstatus').prop('disabled', true);
            <%
        }
            %>
            /*
            $('#addordercat option:not(:selected)').prop('disabled', false);
            $('#addordertype option:not(:selected)').prop('disabled', false);
            */
        }

        function openeditlineitem(lineno) {
            $('#btnAddLineItem').hide();
            $('#btnEditLineItem').show();

            $('#hidLineNo').val(lineno);

            //disable related fields
            $('#addordercat').prop('disabled', true);
            $('#addordertype').prop('disabled', true);
            <%
        if (sUSerType.Equals("00"))
        {
            %>
            $('#addstatus').prop('disabled', false);
            <%
        }
        else
        {
            %>
            $('#addstatus').prop('disabled', true);
            <%
        }
            %>

            /*
            $('#addordercat option:not(:selected)').prop('disabled', true);
            $('#addordertype option:not(:selected)').prop('disabled', true);
            */
                    <%
        for (int i = 0; i < lsItemDiscount.Count; i++)
        {
            MainModel modOrdDet = (MainModel)lsItemDiscount[i];
                    %>
            if ($('#hidLineNo').val() == '<%=modOrdDet.GetSetcomp %><%=modOrdDet.GetSetdiscounttype %><%=modOrdDet.GetSetitemno %><%=modOrdDet.GetSetordercat %>') {
                $('#addlineno').val('<%=i+1%>');
                $('#additemno').val('<%=modOrdDet.GetSetitemno%>');
                $('#addordercat').val('<%=modOrdDet.GetSetordercat%>').change();
                $('#addordertype').val('<%=modOrdDet.GetSetdiscounttype%>').change();
                $('#addstatus').val('<%=modOrdDet.GetSetstatus%>').change();
                $('#addunitprice').val('<%=(modOrdDet.GetSetordercat.Equals("PURCHASE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("SALES_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("ONLINE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("RECEIVE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("GIVE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("TRANSFER_ORDER")?modOrdDet.GetSetcostprice:0))))))%>');
                $('#adddisccat').val('<%=modOrdDet.GetSetdisccat%>').change();
                $('#adddiscvalue').val('<%=modOrdDet.GetSetdiscvalue%>');
                $('#adddiscamount').val('<%=modOrdDet.GetSetdiscamount%>');
                $('#additemprice').val('<%=(modOrdDet.GetSetordercat.Equals("PURCHASE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("SALES_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("ONLINE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("RECEIVE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("GIVE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("TRANSFER_ORDER")?modOrdDet.GetSetcostprice:0)))))) - modOrdDet.GetSetdiscamount%>');
            }
                    <%
        }
                    %>

        }

        function confirmdeletelineitem(lineno) {
            $('#hidLineNo').val(lineno);
            <%
        for (int i = 0; i < lsItemDiscount.Count; i++)
        {
            MainModel modOrdDet = (MainModel)lsItemDiscount[i];
                    %>
            if ($('#hidLineNo').val() == '<%=modOrdDet.GetSetcomp %><%=modOrdDet.GetSetdiscounttype %><%=modOrdDet.GetSetitemno %><%=modOrdDet.GetSetordercat %>') {
                $('#addlineno').val('<%=i+1%>');
                $('#additemno').val('<%=modOrdDet.GetSetitemno%>');
                $('#addordercat').val('<%=modOrdDet.GetSetordercat%>').change();
                $('#addordertype').val('<%=modOrdDet.GetSetdiscounttype%>').change();
                $('#addstatus').val('<%=modOrdDet.GetSetstatus%>').change();
                $('#addunitprice').val('<%=(modOrdDet.GetSetordercat.Equals("PURCHASE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("SALES_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("ONLINE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("RECEIVE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("GIVE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("TRANSFER_ORDER")?modOrdDet.GetSetcostprice:0))))))%>');
                $('#adddisccat').val('<%=modOrdDet.GetSetdisccat%>').change();
                $('#adddiscvalue').val('<%=modOrdDet.GetSetdiscvalue%>');
                $('#adddiscamount').val('<%=modOrdDet.GetSetdiscamount%>');
                $('#additemprice').val('<%=(modOrdDet.GetSetordercat.Equals("PURCHASE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("SALES_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("ONLINE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("RECEIVE_ORDER")?modOrdDet.GetSetpurchaseprice:(modOrdDet.GetSetordercat.Equals("GIVE_ORDER")?modOrdDet.GetSetsalesprice:(modOrdDet.GetSetordercat.Equals("TRANSFER_ORDER")?modOrdDet.GetSetcostprice:0)))))) - modOrdDet.GetSetdiscamount%>');
            }
                    <%
        }
                    %>
        }

        function open_popup(src) {
            var idBody = document.getElementById("idBody")
            var fade = document.getElementById("fade_div");
            var popup = document.getElementById("pop_div");
            var popup_content = document.getElementById("pop_content");

            idBody.style.overflow = "hidden";
            popup_content.src = src
            popup.style.display = "";
            fade.style.display = "";
        }

        function close_popup() {
            var idBody = document.getElementById("idBody")
            var fade = document.getElementById("fade_div");
            var popup = document.getElementById("pop_div");
            var popup_content = document.getElementById("pop_content");

            idBody.style.overflow = "auto";
            popup_content.src = "Loading.aspx";
            popup.style.display = "none";
            fade.style.display = "none";
        }

    </script>
</asp:Content>
