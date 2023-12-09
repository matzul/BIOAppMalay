<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AssetDepreciationDetails.aspx.cs" Inherits="AssetDepreciationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <label for="txttranno">No. Transaksi:</label>
                            <input type="text" id="txttranno" class="form-control" readonly="readonly" name="txttranno" value="<%=oModDep.GetSettranno %>" />
                            <label for="txttrancode">Kod Transaksi:</label>
                            <input type="text" id="txttrancode" class="form-control" readonly="readonly" name="txttrancode" value="<%=oModDep.GetSettrancode %>" />
                            <label for="txttrandate">Tarikh Transaksi: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                            <input type="text" id="txttrandate" class="date-picker form-control" name="txttrandate" required="required" value="<%=oModDep.GetSettrandate %>" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form2">
                            <label for="txtremarks">Keterangan:</label>
                            <textarea id="txtremarks" class="form-control" rows="3" name="txtremarks"><%=oModDep.GetSetremarks %></textarea>
                            <label for="txtstatus">Status:</label>
                            <input type="text" id="txtstatus" class="form-control" readonly="readonly" name="txtstatus" value="<%=oModDep.GetSetstatus%>" />
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
                                <button id="btnCreate" name="btnCreate" type="button" class="btn btn-primary" onclick="actionclick('CREATE');">Daftar</button>
                                <button id="btnReset" name="btnReset" type="button" class="btn btn-warning" onclick="actionclick('ADD');">Reset</button>
                                <%
                                    }
                                    else if (sAction.Equals("OPEN")) 
                                    {
                                        if (!oModDep.GetSetstatus.Equals("CONFIRMED") && !oModDep.GetSetstatus.Equals("CANCELLED")) { 
                                %>
                                <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');" >Kemaskini</button>
                                <%
                                            if (lsAssetDepreciation.Count > 0) { 
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
                                <button id="btnSave" name="btnSave" type="button" class="btn btn-primary" onclick="actionclick('SAVE');" >Simpan</button>
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
                                    <input type="hidden" name="hidLineNo" id="hidLineNo" value="" />
                                    <input type="hidden" name="hidAssetNo" id="hidAssetNo" value="" />
                                </div>
                            </div>
                        </div>
                    </section>

                    <!--BEGIN dialog box for add line depreciation-->
                    <div id="modalDepreciation" class="modal fade modal-add-line-depreciation" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                    <h4 class="modal-title">Daftar Item Susut Nilai</h4>
                                </div>
                                <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">

                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Aset</label>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <input id="addassetno" name="addassetno" type="text" class="form-control" value="" />
                                                <div id="addassetno-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan Aset </label>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <textarea id="addassetdesc" name="addassetdesc" class="form-control" rows="3" readonly="readonly"></textarea>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti </label>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <input id="addtranqty" name="addtranqty" type="text" class="form-control" value="0" readonly="readonly"/>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-12">
                                                Nilai Aset Semasa 
                                            </label>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <input id="addnetbookvalue" name="addnetbookvalue" type="text" class="form-control" readonly="readonly" value="0" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-12">
                                                Susut Nilai (RM) <span class="required">*</span>
                                            </label>
                                            <div class="col-md-9 col-sm-9 col-xs-12">
                                                <input id="addtranvalue" name="addtranvalue" type="text" class="form-control" value="0" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" id="btnAddLineDepreciation" onclick="actionclick('INSERT');">Tambah</button>
                                    <button type="button" class="btn btn-primary" id="btnEditLineDepreciation" onclick="actionclick('UPDATE');">Simpan</button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for add line item-->
                    <!--BEGIN dialog box for confirm delete-->
                    <div class="modal fade modal-confirm-delete-line-item" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Anda pasti untuk keluarkan Item ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnDeleteLineItem" onclick="actionclick('DELETE');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm delete-->
                    <!--BEGIN dialog box for confirm cancel order-->
                    <div class="modal fade modal-confirm-cancel-order" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Anda pasti untuk batalkan proses ini?</h4>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-warning" id="btnCancelOrder" onclick="actionclick('CANCEL');">Ya</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Tidak</button>
                            </div>

                            </div>
                        </div>
                    </div>
                    <!--END dialog box for confirm delete-->

                </div>
            </form>
            <div class="col-md-12 col-sm-12 col-xs-12">
                <a id="addlineitem" name="addlineitem" class="btn btn-app" data-toggle="modal" data-target=".modal-add-line-depreciation" onclick="openaddlinedepreciation();">
                    <i class="fa fa-plus-square green"></i>Tambah Aset
                </a>
                <a id="savelineitem" name="savelineitem" class="btn btn-app" onclick="updateAssetDepreciation();">
                    <i class="fa fa-save green"></i>Simpan Susut Nilai
                </a>
                <div class="table-responsive">
                    <table id="datatable-buttons" class="table table-striped jambo_table">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>No. Aset</th>
                                <th>Keterangan Aset</th>
                                <th>Kuantiti</th>
                                <th>Nilai Aset Semasa</th>
                                <th>Susut Nilai (RM)</th>
                                <th></th>
                            </tr>
                        </thead>

                        <tbody>
                            <%
                                if (lsAssetDepreciation.Count > 0)
                                {
                                    for (int i = 0; i < lsAssetDepreciation.Count; i++)
                                    {
                                        MainModel modAsset = (MainModel)lsAssetDepreciation[i];
                            %>
                            <tr data-id="<%=modAsset.GetSetassetno %>" class="tblText1">
                                <td><a href="#" class="btn-link" onclick="openeditasset('<%=modAsset.GetSetcomp %>','<%=modAsset.GetSetassetno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                                <td><a href="#" class="btn-link" onclick="openeditasset('<%=modAsset.GetSetcomp %>','<%=modAsset.GetSetassetno %>');"><%=modAsset.GetSetassetno %></a></td>
                                <td><%=modAsset.GetSetassetdesc %></td>
                                <td><%=modAsset.GetSettranqty %></td>
                                <td><%=modAsset.GetSetassetnbv %></td>
                                <td><input id="modtranvalue_<%=modAsset.GetSetassetno %>" name="modtranvalue_<%=modAsset.GetSetassetno %>" type="text" class="form-control tranval" <%=((!oModDep.GetSetstatus.Equals("CONFIRMED") && !oModDep.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT"))?"":"readonly='readonly'")%> value="<%=modAsset.GetSettranvalue %>" /></td>
                                <td>
                                  <%
                                    if (!oModDep.GetSetstatus.Equals("CONFIRMED") && !oModDep.GetSetstatus.Equals("CANCELLED") && !sAction.Equals("EDIT")) 
                                      { 
                                  %>
                                  <a href="#" class="btn btn-danger btn-xs" onclick="confirmdeletelineitem('<%=modAsset.GetSetassetno %>');" data-toggle="modal" data-target=".modal-confirm-delete-line-item"><i class="fa fa-trash-o"></i> Hapus </a>
                                  <%
                                      }
                                  %>

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
                                <td>Rekod tiada...</td>
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

        var assetArray = [];
        var maxlengthdataautocomplete = 20;

        function actionclick(action) {
            var proceed = true;
            if (action == 'ADD') {
                $('#txttrandate').removeAttr('required');
            }
            if (action == 'INSERT' || action == 'UPDATE') {
                if ($('#addassetno').val() == "") {
                    alert("Error for Asset Number!");
                    proceed = false;
                } else if ($('#addtranqty').val() == "") {
                    alert("Kuantiti Aset adalah Salah!");
                    proceed = false;
                } else if (parseInt($('#addtranqty').val()) == 0) {
                    alert("Kuantiti Aset adalah Salah!");
                    proceed = false;
                } else if ($('#addtranvalue').val() == "") {
                    alert("Susut Nilai (RM) adalah Salah!");
                    proceed = false;
                } else if (parseInt($('#addtranvalue').val()) == 0) {
                    alert("Susut Nilai (RM) adalah Salah!");
                    proceed = false;
                } else {
                    proceed = true;
                }
            }
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        var resultLooping = true;
        function updateAssetDepreciation() {

            var assetdeprupdate = [];

            $("tr.tblText1").each(function (i, tr) {
                var tranval = $("input.tranval", tr).val();
                var assetno = $(tr).data("id");

                assetdeprupdate.push(assetno + '|' + tranval);

            });


            if (resultLooping) {

                var updateAssetDepr_parameters = ["currcomp", "<%=sCurrComp%>", "fyr", "<%=sCurrFyr %>", "tranno", "<%=sTranNo %>", "trancode", "<%=sTranCode%>", "assetdeprupdate", assetdeprupdate];
                //alert(updateAssetDepr_parameters);
                PageMethod("updateAssetDepr", updateAssetDepr_parameters, updateAssetDepr_succeedAjaxFn, updateAssetDepr_failedAjaxFn, true);

            } else {
                alert('Error...Kemaskini Tidak Berjaya!');
            }

        }

        var updateAssetDepr_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateAssetDepr_succeedAjaxFn: " + textStatus);
            var updateAssetDepr_result = JSON.parse(data.d);
            if (updateAssetDepr_result.result == "Y") {
                alert(updateAssetDepr_result.message);
                actionclick('OPEN');
            }
            else {
                resultLooping = false;
                alert(updateAssetDepr_result.message);
            }
        }

        var updateAssetDepr_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateAssetDepr_failedAjaxFn: " + textStatus);
        }

        function openaddlinedepreciation() {
            $('#btnAddLineDepreciation').show();
            $('#btnEditLineDepreciation').hide();

            $('#hidLineNo').val('');
            $('#addtranqty').val('');
        }

        function openeditasset(comp, assetno) {
            var popupWindow2 = window.open("AssetDetails.aspx?action=OPEN&comp=" + comp + "&assetno=" + assetno, "open_asset2", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

        function enabledisableinputform(flag) {
            $('#trandate').prop('disabled', flag);
            $('#remarks').prop('disabled', flag);
        }

        function confirmdeletelineitem(assetno) {
            $('#hidAssetNo').val(assetno);
        }

        $(document).ready(function () {

            $('#txttrandate').daterangepicker({
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

            $('#addlineitem').prop('disabled', true);

            $('#addlineitem').attr('disabled', 'disabled');

            $('#savelineitem').prop('disabled', true);

            $('#savelineitem').attr('disabled', 'disabled');

            <%
                if (sAction.Equals("OPEN"))
                {
                    if (!oModDep.GetSetstatus.Equals("CONFIRMED") && !oModDep.GetSetstatus.Equals("CANCELLED"))
                    {
                        if (lsAssetDepreciation.Count > 0)
                        { 
            %>
                            $('#addlineitem').prop('disabled', false);
                            $('#addlineitem').removeAttr('disabled');
                            $('#savelineitem').prop('disabled', false);
                            $('#savelineitem').removeAttr('disabled');

            <%
                        }
                        else
                        { 
            %>
                            $('#addlineitem').prop('disabled', false);
                            $('#addlineitem').removeAttr('disabled');
                            $('#savelineitem').prop('disabled', false);
                            $('#savelineitem').removeAttr('disabled');

            <%
                        }
                    }
                    else
                    {
                        if (lsAssetDepreciation.Count > 0)
                        {
            %>

            <%
                        }
                    }
                }
            %>

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

            var getAssetList_parameters = ["currcomp", "<%=sCurrComp%>", "status", "CONFIRMED"];
            PageMethod("getAssetList", getAssetList_parameters, getAssetList_succeedAjaxFn, getAssetList_failedAjaxFn, false);

            $('#addassetno').autocomplete({
                lookup: assetArray,
                appendTo: '#addassetno-container',
                minLength: 0,
                minChars: 0,
                width: maxlengthdataautocomplete * 12,
                onSelect: function (suggestion) {
                    //console.log("suggestion: " + JSON.stringify(suggestion));
                    $('#addassetno').val(suggestion.data);

                    var getAssetDetails_parameters = ["currcomp", "<%=sCurrComp%>", "assetno", suggestion.data];
                    PageMethod("getAssetDetails", getAssetDetails_parameters, getAssetDetails_succeedAjaxFn, getAssetDetails_failedAjaxFn, false);

                }
            });

        });

        var getAssetList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getAssetList_succeedAjaxFn: " + textStatus);
            var getAssetList_result = JSON.parse(data.d);
            if (getAssetList_result.result == "Y") {
                $.each(getAssetList_result.assetlist, function (i, result) {
                    var objData = {};
                    objData.value = result.GetSetassetno + '-' + result.GetSetassetdesc;
                    objData.data = result.GetSetassetno;
                    assetArray.push(objData);
                    if (objData.value.length > maxlengthdataautocomplete) {
                        maxlengthdataautocomplete = objData.value.length;
                    }
                });
            }
            else {
                console.log("getAssetList_result.result: " + getAssetList_result.result);
            }
            //console.log("assetArray: " + JSON.stringify(assetArray));
        }

        var getAssetList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getAssetList_failedAjaxFn: " + textStatus);
        }        

        var getAssetDetails_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getAssetDetails_succeedAjaxFn: " + textStatus);
            var getAssetDetails_result = JSON.parse(data.d);
            if (getAssetDetails_result.result == "Y") {
                $('#addassetdesc').val(getAssetDetails_result.assetdetail.GetSetassetdesc);
                $('#addtranqty').val(getAssetDetails_result.assetdetail.GetSetqtyreg);
                $('#addnetbookvalue').val(getAssetDetails_result.assetdetail.GetSetassetnbv);
                $('#addtranvalue').val('0');
            }
            else {
                alert(getAssetDetails_result.message);
            }
        }

        var getAssetDetails_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getAssetDetails_failedAjaxFn: " + textStatus);
        }

    </script>
</asp:Content>
