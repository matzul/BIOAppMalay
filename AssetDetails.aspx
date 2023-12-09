<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AssetDetails.aspx.cs" Inherits="AssetDetails" %>

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
                            <label for="assetno">No. Aset:</label>
                            <input type="text" id="assetno" class="form-control" readonly="readonly" name="assetno" required="required" value="<%=oModAsset.GetSetassetno %>" />
                            <label for="assetcat">Kategori:</label>
                            <select class="form-control" id="assetcat" name="assetcat" required="required">
                                <option value="ASET_ALIH" <%=oModAsset.GetSetassetcat.Equals("ASET_ALIH")?"selected":"" %>>ASET ALIH</option>
                                <option value="ASET_TAK_ALIH" <%=oModAsset.GetSetassetcat.Equals("ASET_TAK_ALIH")?"selected":"" %>>ASET TAK ALIH</option>
                            </select>
                            <label for="assettyp">Jenis Aset:</label>
                            <select class="form-control" id="assettyp" name="assettyp" required="required">
                                <%
                                    for (int i = 0; i < lsAssetCategory.Count; i++)
                                    {
                                        MainModel modParam = (MainModel)lsAssetCategory[i];
                                %>
                                <option value="<%=modParam.GetSetparamcode %>" <%=oModAsset.GetSetassettyp.Equals(modParam.GetSetparamcode) ? "selected" : "" %>><%=modParam.GetSetparamdesc %></option>
                                <%
                                    }
                                %>
                            </select>
                            <label for="datemfg">Tarikh Buatan: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="datemfg" class="date-picker form-control" name="datemfg" value="<%=oModAsset.GetSetdatemfg %>" required="required" />
                            <label for="datereg">Tarikh Daftar: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="datereg" class="date-picker form-control" required="required" name="datereg" value="<%=oModAsset.GetSetdatereg %>" />
                            <label for="warranty">Jaminan:</label>
                            <select class="form-control" id="warranty" name="warranty" required="required">
                                <option value="N" <%=oModAsset.GetSetwarranty.Equals("N")?"selected":"" %>>TIADA</option>
                                <option value="Y" <%=oModAsset.GetSetwarranty.Equals("Y")?"selected":"" %>>ADA</option>
                            </select>
                            <label for="datewarend">Tarikh Jaminan: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="datewarend" class="date-picker form-control" name="datewarend" value="<%=oModAsset.GetSetdatewarend %>" required="required"/>
                            <label for="remarks">Catatan:</label>
                            <textarea id="remarks" class="form-control" rows="3" name="remarks" ><%=oModAsset.GetSetremarks %></textarea>
                            <label for="status">Status:</label>
                            <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModAsset.GetSetstatus%>"/>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="assetdesc">Keterangan:</label>
                            <textarea id="assetdesc" class="form-control" rows="3" name="assetdesc" required="required"><%=oModAsset.GetSetassetdesc %></textarea>
                            <label for="assetowner">Jenama/Buatan:</label>
                            <input type="text" id="assetowner" class="form-control" name="assetowner" required="required" value="<%=oModAsset.GetSetassetowner %>"/>
                            <label for="assetrefno">No. Id/Pendaftaran/Rujukan:</label>
                            <input type="text" id="assetrefno" class="form-control" name="assetrefno" required="required" value="<%=oModAsset.GetSetassetrefno %>"/>
                            <label for="qtyreg">Kuantiti:</label>
                            <input type="text" id="qtyreg" class="form-control" name="qtyreg" required="required" value="<%=oModAsset.GetSetqtyreg %>" />
                            <label for="costreg">Nilai Aset:</label>
                            <input type="text" id="costreg" class="form-control" name="costreg" required="required" value="<%=oModAsset.GetSetcostreg %>" />
                            <label for="deprtyp">Kaedah Susut Nilai:</label>
                            <select class="form-control" id="deprtyp" name="deprtyp">
                                <option value="">-Select-</option>
                                <option value="GARIS_LURUS" <%=oModAsset.GetSetdeprtyp.Equals("GARIS_LURUS")?"selected":"" %>>GARIS LURUS</option>
                            </select>
                            <label for="deprrate">Kadar Susut Nilai:</label>
                            <input type="text" id="deprrate" class="form-control" required="required" name="deprrate" value="<%=oModAsset.GetSetdeprrate %>" />
                            <label for="depraccum">Susut Nilai Terkumpul:</label>
                            <input type="text" id="depraccum" class="form-control" required="required" name="depraccum" value="<%=oModAsset.GetSetdepraccum %>" />
                            <label for="assetnbv">Nilai Aset Semasa:</label>
                            <input type="text" id="assetnbv" class="form-control" readonly="readonly" name="assetnbv" value="<%=oModAsset.GetSetassetnbv %>" />
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
                                        if (!oModAsset.GetSetstatus.Equals("CONFIRMED") && !oModAsset.GetSetstatus.Equals("CANCELLED"))
                                        {
                                %>
                                <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');">Kemaskini</button>
                                <%
                                                if (lsAssetValueTrans.Count > 0) { 
                                %>
                                <button id="btnApprove" name="btnApprove" type="button" class="btn btn-success" onclick="actionclick('CONFIRM');" >Confirm</button>
                                <%
                                                }
                                %>
                                <button id="btnCancel" name="btnCancel" type="button" class="btn btn-danger" onclick="" data-toggle="modal" data-target=".modal-confirm-cancel-order">Batal</button>
                                <%
                                        }
                                    }
                                    else if (sAction.Equals("EDIT"))
                                    {
                                %>
                                <button id="btnSave" name="btnSave" type="button" class="btn btn-success" onclick="actionclick('SAVE');">Simpan</button>
                                <button id="btnBack" name="btnBack" type="button" class="btn btn-warning" onclick="actionclick('OPEN');" >Kembali</button>
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
                                    <input type="hidden" name="hidAssetNo" id="hidAssetNo" value="<%=sAssetNo %>" />
                                    <input type="hidden" name="hidLineNo" id="hidLineNo" value="" />
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
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
                <a id="addimageasset" name="addimageasset" class="btn btn-app" onclick="open_popup('AssetImageUpload.aspx?action=OPEN&assetno=<%=sAssetNo %>');">
                    <i class="fa fa-image dark"></i>Muat Naik Gambar
                </a>
                <a id="addlocation" name="addlocation" class="btn btn-app" onclick="openeditasset('<%=sCurrComp %>','<%=sAssetNo %>');">
                    <i class="fa fa-map-pin red"></i>Penempatan
                </a>
                <!--
                <a id="viewvetting" name="viewvetting" class="btn btn-app" onclick="">
                    <i class="fa fa-binoculars blue"></i>Pemeriksaan
                </a>
                -->
                <div class="table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>No. Transaksi</th>
                                <th>Kod Transaksi</th>
                                <th>Tarikh</th>
                                <th>No. Asset</th>
                                <th>Nilai/Jumlah</th>
                                <th>Status</th>
                                <th>Catatan</th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsAssetValueTrans.Count > 0)
                                {
                                    for (int i = 0; i < lsAssetValueTrans.Count; i++)
                                    {
                                        MainModel modAsset = (MainModel)lsAssetValueTrans[i];
                            %>
                            <tr>
                                <td><%=i+1%></td>
                                <td><%=modAsset.GetSettranno %></td>
                                <td><%=modAsset.GetSettrancode %></td>
                                <td><%=modAsset.GetSettrandate %></td>
                                <td><%=modAsset.GetSetassetno %></td>
                                <td><%=modAsset.GetSettranvalue %></td>
                                <td><%=modAsset.GetSetstatus %></td>
                                <td><%=modAsset.GetSetremarks %></td>
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

            <%
            if (sAction.Equals("ADD"))
            {
            %>
                if ($('#assettyp').val() == '') {
                } else {
                    var getAssetNoOnType_parameters = ["comp", "<%=sCurrComp%>", "assettyp", "ASSET_" + $('#assettyp').val(), "year", "<%=sCurrFyr%>"];
                    PageMethod("getAssetNoOnType", getAssetNoOnType_parameters, getAssetNoOnType_succeedAjaxFn, getAssetNoOnType_failedAjaxFn, false);
                }
            <%
            }
            %>

            $('#datemfg').daterangepicker({
                singleDatePicker: true,
                timePicker: false,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#datereg').daterangepicker({
                singleDatePicker: true,
                timePicker: false,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#datewarend').daterangepicker({
                singleDatePicker: true,
                timePicker: false,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                timePickerSeconds: true,
                format: "DD-MM-YYYY",
                drops: "up",
                calender_style: "picker_4"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

        });

        
        <%
        if (sAction.Equals("ADD"))
        {
        %>
        $('#assettyp').change(function () {
            if ($(this).val() == '') {
            } else {
                var getAssetNoOnType_parameters = ["comp", "<%=sCurrComp%>", "assettyp", "ASSET_" + $(this).val(), "year", "<%=sCurrFyr%>"];
                PageMethod("getAssetNoOnType", getAssetNoOnType_parameters, getAssetNoOnType_succeedAjaxFn, getAssetNoOnType_failedAjaxFn, false);
                }
            });
        <%
        }
        %>

        //notification for succeed ajax transaction
        getAssetNoOnType_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getAssetNoOnType_succeedAjaxFn: " + textStatus);

            result = JSON.parse(data.d);
            if (result.status == "Y") {
                $('#assetno').val(result.assetno);
            }
            else {
                $('#assetno').val("");
            }
        }

        //notification for failed ajax transaction
        getAssetNoOnType_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getAssetNoOnType_failedAjaxFn: " + textStatus);
            saveshipment = false;
        }

        function actionclick(action) {
            var proceed = false;
            if (action == 'ADD' || action == 'OPEN') {
                $('#assetcat').removeAttr('required');
                $('#assettyp').removeAttr('required');
                $('#datemfg').removeAttr('required');
                $('#datereg').removeAttr('required');
                $('#warranty').removeAttr('required');
                $('#datewarend').removeAttr('required');
                $('#status').removeAttr('required');
                $('#assetdesc').removeAttr('required');
                $('#assetowner').removeAttr('required');
                $('#assetrefno').removeAttr('required');
                $('#depraccum').removeAttr('required');
                $('#qtyreg').removeAttr('required');
                $('#costreg').removeAttr('required');
                $('#deprtyp').removeAttr('required');
                $('#deprrate').removeAttr('required');
                proceed = true;
            }
            if (action == 'EDIT') {
                proceed = true;
            }
            if (action == 'CREATE') {
                proceed = true;
            }
            if (action == 'SAVE' || action == 'CONFIRM') {
                if ($('#assetno').val() == "") {
                    alert("Error for Asset Number!");
                    proceed = false;
                } else if ($('#qtyreg').val() == "") {
                    alert("Kuantiti Aset adalah Salah!");
                    proceed = false; 
                } else if (parseInt($('#qtyreg').val()) == 0) {
                    alert("Kuantiti Aset adalah Salah!");
                    proceed = false; 
                } else if ($('#costreg').val() == "") {
                    alert("Nilai Aset adalah Salah!");
                    proceed = false;
                } else if (parseFloat($('#costreg').val()) == 0) {
                    alert("Nilai Aset adalah Salah!");
                    proceed = false; 
                } else {
                    proceed = true;
                }
            }
            if (action == 'CANCEL') {
                proceed = true;
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        function enabledisableinputform(flag) {
            <%
        if (sAssetNo.Length > 0)
        {
            %>
            $('#assettyp').prop('disabled', true);
            <%
        }
        else 
        { 
            %>
            $('#assettyp').prop('disabled', flag);
            <%
        }
            %>
            $('#assetcat').prop('disabled', flag);
            $('#datemfg').prop('disabled', flag);
            $('#datereg').prop('disabled', flag);
            $('#warranty').prop('disabled', flag);
            $('#datewarend').prop('disabled', flag);
            $('#remarks').prop('disabled', flag);
            $('#status').prop('disabled', flag);
            $('#assetdesc').prop('disabled', flag);
            $('#assetowner').prop('disabled', flag);
            $('#assetrefno').prop('disabled', flag);
            $('#depraccum').prop('disabled', flag);
            $('#qtyreg').prop('disabled', flag);
            $('#costreg').prop('disabled', flag);
            $('#deprtyp').prop('disabled', flag);
            $('#deprrate').prop('disabled', flag);
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

        function openeditasset(comp, assetno) {
            var popupWindow2 = window.open("AssetPlacementDetails.aspx?action=OPEN&comp=" + comp + "&assetno=" + assetno, "open_asset2", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
            if (popupWindow2 == null) {
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
                popupWindow2.moveTo(wleft, wtop);
            }
        }

    </script>
</asp:Content>
