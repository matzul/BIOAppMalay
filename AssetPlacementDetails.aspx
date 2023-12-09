<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AssetPlacementDetails.aspx.cs" Inherits="AssetPlacementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <input type="text" id="assetno" class="form-control" readonly="readonly" name="assetno" value="<%=oModAsset.GetSetassetno %>" />
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
                                    <label for="datereg">Tarikh Daftar: <i class="glyphicon glyphicon-calendar fa fa-calendar"></i></label>
                                    <input type="text" id="datereg" class="date-picker form-control"  name="datereg" value="<%=oModAsset.GetSetdatereg %>" />
                                </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <div id="add-form2">
                                    <label for="assetdesc">Keterangan:</label>
                                    <textarea id="assetdesc" class="form-control" rows="3" name="assetdesc"><%=oModAsset.GetSetassetdesc %></textarea>
                                    <label for="qtyreg">Kuantiti:</label>
                                    <input type="text" id="qtyreg" class="form-control" name="qtyreg" value="<%=oModAsset.GetSetqtyreg %>" />
                                    <label for="status">Status:</label>
                                    <input type="text" id="status" class="form-control" readonly="readonly" name="status" value="<%=oModAsset.GetSetstatus%>"/>
                                </div>
                    </div>
                  </div>
                  <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <div id="action-form">
                                    <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="window.close();">Tutup</button>
                                    <%
                                        MainModel oAlerMssg = oMainCon.getAlertMessage(sAlertMessage);
                                        if (oAlerMssg.GetSetalertstatus.Equals("SUCCESS")) { 
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

                        <!--BEGIN dialog box for add line placement-->
                        <div id="modalplacement" name="modalplacement" class="modal fade modal-add-line-placement" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">

                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span>
                                        </button>
                                        <h4 class="modal-title">Daftar Penempatan Baharu</h4>
                                    </div>
                                    <div class="modal-body">
                                    <div id="form2" class="form-horizontal form-label-left">

                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">No. Aset</label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addassetno" name="addassetno" type="text" class="form-control" readonly="readonly" value="<%=oModAsset.GetSetassetno %>"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Keterangan Aset </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                            <textarea id="addassetdesc" name="addassetdesc" class="form-control" rows="3" readonly="readonly"><%=oModAsset.GetSetassetdesc %></textarea>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Kuantiti <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtranqty" name="addtranqty" type="text" class="form-control" value="0"/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tarikh Penempatan <i class="glyphicon glyphicon-calendar fa fa-calendar"></i><span class="required"> *</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addtrandate" name="addtrandate" type="text" class="date-picker form-control" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Negara <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addcountry" name="addcountry" class="select2_single form-control" style="width:100%;">
                                      <%
                                          for (int ic = 0; ic < lsCountry.Count; ic++)
                                          {
                                              MainModel oItem = (MainModel)lsCountry[ic];
                                      %>                         
                                            <option value="<%=oItem.GetSetparamid %>"><%=oItem.GetSetparamdesc %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Negeri <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addstate" name="addstate" class="select2_single form-control" style="width:100%;">
                                            <option value="">-Select-</option>
                                      <%
                                          for (int ic = 0; ic < lsState.Count; ic++)
                                          {
                                              MainModel oItem = (MainModel)lsState[ic];
                                      %>                         
                                            <option value="<%=oItem.GetSetparamid %>"><%=oItem.GetSetparamdesc %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Daerah <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="adddistrict" name="adddistrict" class="select2_single form-control" style="width:100%;">
                                            <option value="">-Select-</option>
                                      <%
                                          for (int ic = 0; ic < lsDistrict.Count; ic++)
                                          {
                                              MainModel oItem = (MainModel)lsDistrict[ic];
                                      %>                         
                                            <option value="<%=oItem.GetSetparamid %>"><%=oItem.GetSetparamdesc %></option>
                                      <%
                                          }
                                      %>
                                          </select>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Lokasi <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addlocation" name="addlocation" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Tujuan </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addpurpose" name="addpurpose" type="text" class="form-control" value=""/>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Id Pegawai <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addofficerid" name="addofficerid" type="text" class="form-control" value=""/>
                                          <div id="addofficerid-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Nama Pegawai <span class="required">*</span></label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <input id="addofficername" name="addofficername" type="text" class="form-control" value=""/>
                                          <div id="addofficername-container" style="position: relative; float: left; width: 100%; margin: 0px; background-color: aqua;"></div>
                                        </div>
                                      </div>
                                      <div class="form-group">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Status <span class="required">*</span>
                                        </label>
                                        <div class="col-md-9 col-sm-9 col-xs-12">
                                          <select id="addstatus" name="addstatus" class="select2_single form-control" style="width:100%;">
                                            <option value="ACTIVE">AKTIF</option>
                                            <option value="IN-ACTIVE">TIDAK AKTIF</option>
                                          </select>
                                        </div>
                                      </div>

                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-primary" id="btnAddLinePlacement" onclick="actionclick('INSERT');">Tambah</button>
                                        <button type="button" class="btn btn-primary" id="btnEditLinePlacement" onclick="actionclick('UPDATE');">Simpan</button>
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!--END dialog box for add line item-->
                        
                  </div> 
                  </form>                   
                  <div class="col-md-12 col-sm-12 col-xs-12">
                      <a id="addplacement" name="addplacement" class="btn btn-app" data-toggle="modal" data-target=".modal-add-line-placement" onclick="openaddlineplacement();">
                        <i class="fa fa-plus-square green"></i>Daftar Penempatan
                      </a>
                      <div class="table-responsive">
                        <table id="datatable-buttons" class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th>#</th>
                              <th>No. Aset</th>
                              <th>Lokasi</th>
                              <th>Daerah</th>
                              <th>Negeri</th>
                              <th>Tarikh</th>
                              <th>Tujuan</th>
                              <th>Pegawai</th>
                              <th>Catatan</th>
                            </tr>
                          </thead>

                          <tbody>
                        <%
                            if (lsAssetPlacement.Count > 0)
                            {
                                for (int i = 0; i < lsAssetPlacement.Count; i++)
                                {
                                    MainModel modAsset = (MainModel)lsAssetPlacement[i];
                                    if (modAsset.GetSetstatus.Equals("ACTIVE"))
                                    {
                                        iOccupiedQty = iOccupiedQty + modAsset.GetSettranqty; 
                                    }
                        %>       
                            <tr>
                              <td><a href="#" class="btn-link" onclick="openeditlineplacement('<%=modAsset.GetSetlineno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                              <td><a href="#" class="btn-link" onclick="openeditlineplacement('<%=modAsset.GetSetlineno %>');"><%=modAsset.GetSetassetno %></a></td>
                              <td><%=modAsset.GetSetlocation %></td>
                              <td><%=modAsset.GetSetdistrict_desc %></td>
                              <td><%=modAsset.GetSetstate_desc %></td>
                              <td><%=modAsset.GetSettrandate %></td>
                              <td><%=modAsset.GetSetpurpose %></td>
                              <td><%=modAsset.GetSetofficername %></td>
                              <td><%=modAsset.GetSetremarks %></td>
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

                var officerArray = [];
                var officerArray2 = [];
                var maxlengthdataautocomplete = 20;

                function actionclick(action) {
                    var proceed = false;
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
                        } else if ($('#addtrandate').val() == "") {
                            alert("Sila isi Tarikh Penempatan!");
                            proceed = false;
                        } else if ($('#addcountry').val() == "") {
                            alert("Pilihan Negara adalah Salah!");
                            proceed = false;
                        } else if ($('#addstate').val() == "") {
                            alert("Pilihan Negeri adalah Salah!");
                            proceed = false;
                        } else if ($('#adddistrict').val() == "") {
                            alert("Pilihan Daerah adalah Salah!");
                            proceed = false;
                        } else if ($('#addlocation').val() == "") {
                            alert("Sila isi Lokasi!");
                            proceed = false;
                        } else if ($('#addofficerid').val() == "") {
                            alert("Sila isi Id Pegawai!");
                            proceed = false;
                        } else if ($('#addofficername').val() == "") {
                            alert("Sila isi Nama Pegawai!");
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


                function openaddlineplacement() {
                    $('#btnAddLinePlacement').show();
                    $('#btnEditLinePlacement').hide();

                    $('#hidLineNo').val('');
                    $('#addtranqty').val('<%=oModAsset.GetSetqtyreg - iOccupiedQty%>');
                }

                function openeditlineplacement(lineno) {
                    $('#btnAddLinePlacement').hide();
                    $('#btnEditLinePlacement').show();

                    $('#hidLineNo').val(lineno);
                    <%
                    for (int i = 0; i < lsAssetPlacement.Count; i++)
                    {
                        MainModel modPlaceDet = (MainModel)lsAssetPlacement[i];
                    %>
                    if ($('#hidLineNo').val() == '<%=modPlaceDet.GetSetlineno%>') {
                        $('#addasetno').val('<%=oModAsset.GetSetassetno%>');
                        $('#addassetdesc').text('<%=oModAsset.GetSetassetdesc%>');
                        $('#addtranqty').val('<%=modPlaceDet.GetSettranqty%>');
                        $('#addtrandate').val('<%=modPlaceDet.GetSettrandate%>');
                        $('#addcountry').val('<%=modPlaceDet.GetSetcountry%>').change();
                        $('#addstate').val('<%=modPlaceDet.GetSetstate%>').change();
                        $('#adddistrict').val('<%=modPlaceDet.GetSetdistrict%>').change();
                        $('#addlocation').val('<%=modPlaceDet.GetSetlocation%>');
                        $('#addpurpose').val('<%=modPlaceDet.GetSetpurpose%>');
                        $('#addofficerid').val('<%=modPlaceDet.GetSetofficerid%>');
                        $('#addofficername').val('<%=modPlaceDet.GetSetofficername%>');
                        $('#addstatus').val('<%=modPlaceDet.GetSetstatus%>').change();
                    }
                    <%
                    }
                    %>
                    //$('#modalplacement').modal({ backdrop: "static" });
                    $('#modalplacement').modal("show");
                }

                function enabledisableinputform(flag) {
                    <% if (sAction.Equals("EDIT")){%>
                    $('#assetno').prop('disabled', true);
                    <% }else{%>
                    $('#assetno').prop('disabled', flag);
                    <% }%>
                    $('#assetdesc').prop('disabled', flag);
                    $('#qtyreg').prop('disabled', flag);
                    $('#datereg').prop('disabled', flag);
                    $('#assetcat').prop('disabled', flag);
                    $('#assettyp').prop('disabled', flag);
                }

                $(document).ready(function () {


                    var getOfficerList_parameters = ["comp", "<%=sCurrComp%>"];
                    PageMethod("getOfficerList", getOfficerList_parameters, getOfficerList_succeedAjaxFn, getOfficerList_failedAjaxFn, false);

                    $('#addofficerid').autocomplete({
                        lookup: officerArray,
                        appendTo: '#addofficerid-container',
                        minLength: 0,
                        minChars: 0,
                        width: maxlengthdataautocomplete * 12,
                        onSelect: function (suggestion) {
                            $('#addofficerid').val(suggestion.data);
                            $('#addofficername').val(suggestion.value.split(':')[1]);
                        }
                    });

                    $('#addofficername').autocomplete({
                        lookup: officerArray2,
                        appendTo: '#addofficername-container',
                        minLength: 0,
                        minChars: 0,
                        width: maxlengthdataautocomplete * 12,
                        onSelect: function (suggestion) {
                            $('#addofficername').val(suggestion.data);
                            $('#addofficerid').val(suggestion.value.split(':')[0]);
                        }
                    });

                    $('#addtrandate').daterangepicker({
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

                });

                var getOfficerList_succeedAjaxFn = function (data, textStatus, jqXHR) {
                    console.log("getOfficerList_succeedAjaxFn: " + textStatus);
                    var getOfficerList_result = JSON.parse(data.d);
                    if (getOfficerList_result.result == "Y") {
                        $.each(getOfficerList_result.officerlist, function (i, result) {
                            var objData = {};
                            objData.value = result.officerid + ":" + result.officername;
                            objData.data = result.officerid;
                            officerArray.push(objData);
                            if (objData.value.length > maxlengthdataautocomplete) {
                                maxlengthdataautocomplete = objData.value.length;
                            }
                            var objData2 = {};
                            objData2.value = result.officerid + ":" + result.officername;
                            objData2.data = result.officername;
                            officerArray2.push(objData2);
                        });
                    }
                    else {
                        console.log("getOfficerList_result.result: " + getOfficerList_result.result);
                    }
                }

                var getOfficerList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
                    console.log("getOfficerList_failedAjaxFn: " + textStatus);
                }

            </script>            
</asp:Content>
