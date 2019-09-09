<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemAsset.aspx.cs" Inherits="ItemAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="" role="tabpanel" data-example-id="togglable-tabs">
            <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                <li role="presentation" class="active"><a href="#tab_content1" id="home-tab" role="tab" data-toggle="tab" aria-expanded="true">Senarai Item Aset</a>
                </li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="home-tab">
                    <div class="x_panel">
                        <div class="x_content">
                            <div class="">
                                <form id="Form1" runat="server">
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label for="fitemno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Kod Item:</label>
                                            <input type="text" name="fitemno" id="fitemno" class="form-control" value="<%=sName %>" />
                                        </div>
                                        <div class="form-group">
                                            <label for="fassetcat" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Kategori Aset:</label>
                                            <input type="text" id="fassetcat" class="form-control" name="fassetcat" value="<%=sStartDate %>" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label for="fassetno" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">No. Aset:</label>
                                            <input type="text" name="fassetno" id="fassetno" class="form-control" value="<%=sNoKP %>" />
                                        </div>
                                        <div class="form-group">
                                            <label for="fassetloc" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Lokasi Aset:</label>
                                            <input type="text" id="fassetloc" class="form-control" name="fassetloc" value="<%=sEndDate %>" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label for="fassettyp" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Jenis Aset:</label>
                                            <input type="text" name="fassettyp" id="fassettyp" class="form-control" value="<%=sTelNo %>" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">
                                        <div class="form-group">
                                            <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                                            <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                                            <button type="button" onclick="actionclick('ADD');" class="btn btn-success" data-toggle="modal" data-target=".modal-new-stitch">Senarai Belum Daftar Aset</button>
                                            <div style="display: none;">
                                                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                                <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <!--BEGIN dialog box for add new stitch-->
                            <div class="modal fade modal-new-stitch" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                            <h4 class="modal-title">Tempahan Jahitan</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div id="formdetails" class="form-horizontal form-label-left">
                                                <div class="col-md-5 col-sm-5 col-xs-5">
                                                    <div class="product-image">
                                                        <img src="images/ukuran_baju.jpg" alt="..." />
                                                    </div>
                                                    <div class="product_gallery">
                                                        <div class="product_price">
                                                            <label id="stichno" class="h2 price">...</label>
                                                        </div>
                                                    </div>
                                                    <div class="input-group input-group">
                                                        <label for="stitchdate" class="input-group-addon label-info">Tarikh</label>
                                                        <input id="stitchdate" name="stitchdate" type="text" class="date-picker form-control" value="" />
                                                    </div>
                                                    <div class="input-group input-group">
                                                        <label for="measurement" class="input-group-addon  label-info">Ukuran</label>
                                                        <select id="measurement" class="select2_single form-control" tabindex="-1" name="measurement" style="width: 100%;">
                                                            <option value="cm">cm</option>
                                                            <option value="inch">inch</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-7 col-sm-7 col-xs-7">
                                                    <input type="hidden" name="hidPeopleId" id="hidPeopleId" value="<%=sPeopleId %>" />
                                                    <div class="input-group input-group">
                                                        <label for="name" class="input-group-addon  label-info">Nama</label>
                                                        <input id="name" name="name" type="text" class="form-control" value="" />
                                                    </div>
                                                    <div class="input-group input-group">
                                                        <label for="nokp" class="input-group-addon  label-info">No. K/P</label>
                                                        <input id="nokp" name="nokp" type="text" class="form-control" value="" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="address" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Alamat</label>
                                                        <textarea id="address" class="form-control" rows="2" name="address"></textarea>
                                                    </div>
                                                    <div class="input-group input-group">
                                                        <label for="telno" class="input-group-addon  label-info">No. Telefon</label>
                                                        <input id="telno" name="telno" type="text" class="form-control" value="" />
                                                    </div>
                                                    <div class="input-group input-group">
                                                        <label for="email" class="input-group-addon  label-info">Email</label>
                                                        <input id="email" name="email" type="text" class="form-control" value="" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="remarks" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">Catatan</label>
                                                        <textarea id="remarks" class="form-control" rows="2" name="remarks"></textarea>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <table class="table table-striped jambo_table">
                                                        <thead>
                                                            <tr>
                                                                <th>BAJU</th>
                                                                <th></th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajubahu" class="input-group-addon">Bahu</label>
                                                                        <input id="bajubahu" name="bajubahu" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajulabuhlengan" class="input-group-addon">Labuh Lengan</label>
                                                                        <input id="bajulabuhlengan" name="bajulabuhlengan" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajulabuhbaju" class="input-group-addon">Labuh Baju</label>
                                                                        <input id="bajulabuhbaju" name="bajulabuhbaju" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajudada" class="input-group-addon">Dada</label>
                                                                        <input id="bajudada" name="bajudada" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajupinggang" class="input-group-addon">Pinggang</label>
                                                                        <input id="bajupinggang" name="bajupinggang" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajupunggung" class="input-group-addon">Punggung</label>
                                                                        <input id="bajupunggung" name="bajupunggung" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajulabuhkain" class="input-group-addon">Labuh Kain</label>
                                                                        <input id="bajulabuhkain" name="bajulabuhkain" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajuleher" class="input-group-addon">Leher</label>
                                                                        <input id="bajuleher" name="bajuleher" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajuspan" class="input-group-addon">Span</label>
                                                                        <input id="bajuspan" name="bajuspan" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajubahudada" class="input-group-addon">Bahu Dada</label>
                                                                        <input id="bajubahudada" name="bajubahudada" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="bajubahupinggang" class="input-group-addon">Bahu Pinggang</label>
                                                                        <input id="bajubahupinggang" name="bajubahupinggang" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table class="table table-striped jambo_table">
                                                        <thead>
                                                            <tr>
                                                                <th>SELUAR</th>
                                                                <th></th>
                                                                <th></th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarpinggang" class="input-group-addon">Pinggang</label>
                                                                        <input id="seluarpinggang" name="seluarpinggang" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarpunggung" class="input-group-addon">Punggung</label>
                                                                        <input id="seluarpunggung" name="seluarpunggung" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarcawat" class="input-group-addon">Cawat</label>
                                                                        <input id="seluarcawat" name="seluarcawat" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarpaha" class="input-group-addon">Paha</label>
                                                                        <input id="seluarpaha" name="seluarpaha" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarlutut" class="input-group-addon">Lutut</label>
                                                                        <input id="seluarlutut" name="seluarlutut" type="text" class="form-control" value="" />
                                                                    </div>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarbukaankaki" class="input-group-addon">Bukaan Kaki</label>
                                                                        <input id="seluarbukaankaki" name="seluarbukaankaki" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="input-group input-group">
                                                                        <label for="seluarlabuhseluar" class="input-group-addon">Labuh Seluar</label>
                                                                        <input id="seluarlabuhseluar" name="seluarlabuhseluar" type="text" class="form-control" value="" />
                                                                    </div>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button id="btnEdit" name="btnEdit" type="button" class="btn btn-primary" onclick="actionclick('EDIT');">Kemaskini</button>
                                            <button id="btnSave" name="btnSave" type="button" class="btn btn-success" onclick="actionclick('SAVE');">Simpan</button>
                                            <button id="btnDelete" name="btnDelete" type="button" class="btn btn-danger" onclick="actionclick('DELETE');">Hapus</button>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!--END dialog box for add new stitch-->
                            <div class="col-md-12 col-sm-12 col-xs-12 table-responsive">

                                <table id="datatable-buttons" class="table table-striped jambo_table">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Nama</th>
                                            <th>No. K/P</th>
                                            <th>No. Telefon</th>
                                            <th>Tarikh Ukuran</th>
                                            <th>Catatan</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <%
                                            if (lsStitchDetails.Count > 0)
                                            {
                                                for (int i = 0; i < lsStitchDetails.Count; i++)
                                                {
                                                    MainModel modItem = (MainModel)lsStitchDetails[i];
                                        %>

                                        <tr>
                                            <td><a href="#" class="btn-link" onclick="stitchno='<%=modItem.GetSetstitchno %>';actionclick('OPEN');" data-toggle="modal" data-target=".modal-new-stitch"><i class="glyphicon glyphicon-play"></i></a></td>
                                            <td><a href="#" class="btn-link" onclick="stitchno='<%=modItem.GetSetstitchno %>';actionclick('OPEN');" data-toggle="modal" data-target=".modal-new-stitch"><%=modItem.GetSetname %></a></td>
                                            <td><%=modItem.GetSetnokp %></td>
                                            <td><%=modItem.GetSettelno %></td>
                                            <td><%=modItem.GetSetstitchdate %></td>
                                            <td><%=modItem.GetSetremarks %></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="5">

                                                <table border="1" class="table">
                                                    <tr>
                                                        <th colspan="4">Ukuran Baju</th>
                                                        <th colspan="4">Ukuran Seluar</th>
                                                    </tr>
                                                    <tr>
                                                        <td>Bahu: <%=modItem.GetSetbaju_bahu %></td>
                                                        <td>Dada: <%=modItem.GetSetbaju_dada %></td>
                                                        <td>Labuh Kain: <%=modItem.GetSetbaju_labuh_kain %></td>
                                                        <td>Bahu Dada: <%=modItem.GetSetbaju_bahu_dada %></td>

                                                        <td>Pinggang: <%=modItem.GetSetseluar_pinggang %></td>
                                                        <td>Cawat: <%=modItem.GetSetseluar_cawat %></td>
                                                        <td>Lutut: <%=modItem.GetSetseluar_lutut %></td>
                                                        <td>Labuh Seluar: <%=modItem.GetSetseluar_labuh_seluar %></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Labuh Lengan: <%=modItem.GetSetbaju_labuh_lengan %></td>
                                                        <td>Pinggan: <%=modItem.GetSetbaju_pinggang %></td>
                                                        <td>Leher: <%=modItem.GetSetbaju_leher %></td>
                                                        <td>Bahu Pinggang: <%=modItem.GetSetbaju_bahu_pinggang %></td>

                                                        <td>Punggung: <%=modItem.GetSetseluar_punggung %></td>
                                                        <td>Paha: <%=modItem.GetSetseluar_paha %></td>
                                                        <td>Bukaan Kaki: <%=modItem.GetSetseluar_bukaan_kaki %></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Labuh Baju: <%=modItem.GetSetbaju_labuh_baju %></td>
                                                        <td>Punggung: <%=modItem.GetSetbaju_punggung %></td>
                                                        <td>Span: <%=modItem.GetSetbaju_span %></td>
                                                        <td></td>

                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>

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
            </div>
        </div>
        <!--
        <div class="" role="tabpanel" data-example-id="togglable-tabs">
            <ul id="myTab" class="nav nav-tabs bar_tabs" role="tablist">
                <li role="presentation" class="active"><a href="#tab_content1" id="home-tab" role="tab" data-toggle="tab" aria-expanded="true">Tempahan Jahitan</a>
                </li>
                <li role="presentation" class=""><a href="#tab_content2" role="tab" id="profile-tab" data-toggle="tab" aria-expanded="false">Profile</a>
                </li>
                <li role="presentation" class=""><a href="#tab_content3" role="tab" id="profile-tab2" data-toggle="tab" aria-expanded="false">Profile</a>
                </li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div role="tabpanel" class="tab-pane fade active in" id="tab_content1" aria-labelledby="home-tab">
                    <p>
                        Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                              synth. Cosby sweater eu banh mi, qui irure terr.
                    </p>
                </div>
                <div role="tabpanel" class="tab-pane fade" id="tab_content2" aria-labelledby="profile-tab">
                    <p>
                        Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo
                              booth letterpress, commodo enim craft beer mlkshk aliquip
                    </p>
                </div>
                <div role="tabpanel" class="tab-pane fade" id="tab_content3" aria-labelledby="profile-tab">
                    <p>
                        xxFood truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui
                              photo booth letterpress, commodo enim craft beer mlkshk
                    </p>
                </div>
            </div>
        </div>
        -->
    </div>
    <script type="text/javascript">
        //New method to connect to Application Server
        function PageMethod(fn, paramArray, successFn, errorFn, asyncFn) {
            var pagePath = window.location.pathname;
            var paramList = '';

            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }
            paramList = '{' + paramList + '}';
            //Call the page method
            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: successFn,
                error: errorFn,
                timeout: 600000,
                async: asyncFn
            });
        }




    </script>

    <script type="text/javascript">
        var stitchno;
        var stitchdate;
        var peopleid;
        var name;
        var nokp;
        var address;
        var telno;
        var email;
        var remarks;
        var measurement;
        var baju_bahu, baju_labuh_lengan, baju_labuh_baju, baju_dada, baju_pinggang, baju_punggung, baju_labuh_kain, baju_leher, baju_span, baju_bahu_dada, baju_bahu_pinggang;
        var seluar_pinggang, seluar_punggung, seluar_cawat, seluar_paha, seluar_lutut, seluar_bukaan_kaki, seluar_labuh_seluar;
        var status;
        var datefrom;
        var dateto;
        var peopleAutoComplete;
        var peopleAutoComplete2;
        var peopleAutoComplete3;

        function enabledisableinputform(flag) {
            $("#stichno").prop('disabled', flag);
            $("#stitchdate").prop('disabled', flag);
            $("#hidPeopleId").prop('disabled', flag);
            $("#name").prop('disabled', flag);
            $("#nokp").prop('disabled', flag);
            $("#address").prop('disabled', flag);
            $("#telno").prop('disabled', flag);
            $("#email").prop('disabled', flag);
            $("#remarks").prop('disabled', flag);
            $("#measurement").prop('disabled', flag);

            $("#bajubahu").prop('disabled', flag); $("#bajulabuhlengan").prop('disabled', flag); $("#bajulabuhbaju").prop('disabled', flag);
            $("#bajudada").prop('disabled', flag); $("#bajupinggang").prop('disabled', flag); $("#bajupunggung").prop('disabled', flag);
            $("#bajulabuhkain").prop('disabled', flag); $("#bajuleher").prop('disabled', flag); $("#bajuspan").prop('disabled', flag);
            $("#bajubahudada").prop('disabled', flag); $("#bajubahupinggang").prop('disabled', flag); 

            $("#seluarpinggang").prop('disabled', flag); $("#seluarpunggung").prop('disabled', flag); 
            $("#seluarcawat").prop('disabled', flag); $("#seluarpaha").prop('disabled', flag);
            $("#seluarlutut").prop('disabled', flag); $("#seluarbukaankaki").prop('disabled', flag);
            $("#seluarlabuhseluar").prop('disabled', flag);
        }


        function actionclick(action) {
            if (action == 'ADD') {
                $("#stichno").text("...");
                $("#stitchdate").val("");
                $("#hidPeopleId").val("");
                $("#name").val("");
                $("#nokp").val("");
                $("#address").val("");
                $("#telno").val("");
                $("#email").val("");
                $("#remarks").val("");
                $("#measurement").val("cm").change();

                $("#bajubahu").val(""); $("#bajulabuhlengan").val(""); $("#bajulabuhbaju").val("");
                $("#bajudada").val(""); $("#bajupinggang").val(""); $("#bajupunggung").val("");
                $("#bajulabuhkain").val(""); $("#bajuleher").val(""); $("#bajuspan").val("");
                $("#bajubahudada").val(""); $("#bajubahupinggang").val("");

                $("#seluarpinggang").val(""); $("#seluarpunggung").val("");
                $("#seluarcawat").val(""); $("#seluarpaha").val("");
                $("#seluarlutut").val(""); $("#seluarbukaankaki").val("");
                $("#seluarlabuhseluar").val("");

                enabledisableinputform(false);
                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#btnDelete').hide();
            }
            if (action == 'SAVE') {
                stitchno = $("#stichno").text();
                stitchdate = $("#stitchdate").val();
                peopleid = $("#hidPeopleId").val();
                name = $("#name").val();
                nokp = $("#nokp").val();
                address = $("#address").val();
                telno = $("#telno").val();
                email = $("#email").val();
                remarks = $("#remarks").val();
                measurement = $("#measurement").val();

                baju_bahu = $("#bajubahu").val(); baju_labuh_lengan = $("#bajulabuhlengan").val(); baju_labuh_baju = $("#bajulabuhbaju").val();
                baju_dada = $("#bajudada").val(); baju_pinggang = $("#bajupinggang").val(); baju_punggung = $("#bajupunggung").val();
                baju_labuh_kain = $("#bajulabuhkain").val(); baju_leher = $("#bajuleher").val(); baju_span = $("#bajuspan").val();
                baju_bahu_dada = $("#bajubahudada").val(); baju_bahu_pinggang = $("#bajubahupinggang").val();

                seluar_pinggang = $("#seluarpinggang").val(); seluar_punggung = $("#seluarpunggung").val();
                seluar_cawat = $("#seluarcawat").val(); seluar_paha = $("#seluarpaha").val();
                seluar_lutut = $("#seluarlutut").val(); seluar_bukaan_kaki = $("#seluarbukaankaki").val();
                seluar_labuh_seluar = $("#seluarlabuhseluar").val();

                status = "ACTIVE";
                saveStitch();

                enabledisableinputform(true);
                $('#btnSave').hide();
                $('#btnEdit').show();
                $('#btnDelete').hide();
            }
            if (action == 'DELETE') {
                stitchno = $("#stichno").text();
                stitchdate = $("#stitchdate").val();
                peopleid = $("#hidPeopleId").val();
                name = $("#name").val();
                nokp = $("#nokp").val();
                address = $("#address").val();
                telno = $("#telno").val();
                email = $("#email").val();
                remarks = $("#remarks").val();
                measurement = $("#measurement").val();
                status = "INACTIVE";
                saveStitch();

                setTimeout(function () {
                    location.reload()
                }, 100);

            }
            if (action == 'OPEN') {
                getStitch();

                enabledisableinputform(true);
                $('#btnSave').hide();
                $('#btnEdit').show();
                $('#btnDelete').show();
            }
            if (action == 'EDIT') {
                enabledisableinputform(false);
                $('#btnSave').show();
                $('#btnEdit').hide();
                $('#btnDelete').hide();
            }
            if (action == 'RESET' || action == 'SEARCH') {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

        function saveStitch() {
            var parameters = ["stitchno", stitchno, "stitchdate", stitchdate, "peopleid", peopleid, "name", name, "nokp", nokp, "address", address, "telno", telno, "email", email, "remarks", remarks, "measurement", measurement,
                                "baju_bahu", baju_bahu, "baju_labuh_lengan", baju_labuh_lengan, "baju_labuh_baju", baju_labuh_baju, "baju_dada", baju_dada, "baju_pinggang", baju_pinggang, "baju_punggung", baju_punggung, "baju_labuh_kain", baju_labuh_kain,
                                "baju_leher", baju_leher, "baju_span", baju_span, "baju_bahu_dada", baju_bahu_dada, "baju_bahu_pinggang", baju_bahu_pinggang,
                                "seluar_pinggang", seluar_pinggang, "seluar_punggung", seluar_punggung, "seluar_cawat", seluar_cawat, "seluar_paha", seluar_paha, "seluar_lutut", seluar_lutut, "seluar_bukaan_kaki", seluar_bukaan_kaki, "seluar_labuh_seluar", seluar_labuh_seluar,
                              "status", status];
            PageMethod("saveStitch", parameters, succeededSaveStitch, failedSaveStitch, false);
        }

        succeededSaveStitch = function (data, textStatus, jqXHR) {
            console.log("succeededSaveStitch: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                alert("Proses Simpanan Berjaya!");
            }
        };

        failedSaveStitch = function (jqXHR, textStatus, errorThrown) {
            console.log("failedSaveStitch: " + textStatus);
            alert(textStatus);
        }

        function getStitch() {
            var parameters = ["stitchno", stitchno];
            PageMethod("getStitchObject", parameters, succeededStitch, failedStitch, false);
        }

        succeededStitch = function (data, textStatus, jqXHR) {
            console.log("succeededStitch: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                $("#stichno").text(resultJSON.stitch.stitchno);
                $("#stitchdate").val(resultJSON.stitch.stitchdate);
                $("#hidPeopleId").val(resultJSON.stitch.peopleid);
                $("#name").val(resultJSON.stitch.name);
                $("#nokp").val(resultJSON.stitch.nokp);
                $("#address").val(resultJSON.stitch.address);
                $("#telno").val(resultJSON.stitch.telno);
                $("#email").val(resultJSON.stitch.email);
                $("#remarks").val(resultJSON.stitch.remarks);
                $("#measurement").val(resultJSON.stitch.measurement).change();

                $("#bajubahu").val(resultJSON.stitch.baju_bahu); $("#bajulabuhlengan").val(resultJSON.stitch.baju_labuh_lengan); $("#bajulabuhbaju").val(resultJSON.stitch.baju_labuh_baju);
                $("#bajudada").val(resultJSON.stitch.baju_dada); $("#bajupinggang").val(resultJSON.stitch.baju_pinggang); $("#bajupunggung").val(resultJSON.stitch.baju_punggung);
                $("#bajulabuhkain").val(resultJSON.stitch.baju_labuh_kain); $("#bajuleher").val(resultJSON.stitch.baju_leher); $("#bajuspan").val(resultJSON.stitch.baju_span);
                $("#bajubahudada").val(resultJSON.stitch.baju_bahu_dada); $("#bajubahupinggang").val(resultJSON.stitch.baju_bahu_pinggang);

                $("#seluarpinggang").val(resultJSON.stitch.seluar_pinggang); $("#seluarpunggung").val(resultJSON.stitch.seluar_punggung);
                $("#seluarcawat").val(resultJSON.stitch.seluar_cawat); $("#seluarpaha").val(resultJSON.stitch.seluar_paha);
                $("#seluarlutut").val(resultJSON.stitch.seluar_lutut); $("#seluarbukaankaki").val(resultJSON.stitch.seluar_bukaan_kaki);
                $("#seluarlabuhseluar").val(resultJSON.stitch.seluar_labuh_seluar);
            }
            else {
                alert("Tiada Rekod!");
            }
        };

        failedStitch = function (jqXHR, textStatus, errorThrown) {
            console.log("failedStitch: " + textStatus);
            alert(textStatus);
        }

        function getPeopleAutocompleteList() {
            var parameters = ["id", ""];
            PageMethod("getPeopleAutocompleteList", parameters, succeededPeople, failedPeople, false);
        }

        succeededPeople = function (data, textStatus, jqXHR) {
            console.log("succeededPeople: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                peopleAutoComplete = resultJSON.peoplelist;                
            }
            else
            {
                peopleAutoComplete = null;
            }
        };

        failedPeople = function (jqXHR, textStatus, errorThrown) {
            console.log("failedPeople: " + textStatus);
            peopleAutoComplete = null;
            alert(textStatus);
        }

        function getPeopleAutocompleteList2() {
            var parameters = ["id", ""];
            PageMethod("getPeopleAutocompleteList2", parameters, succeededPeople2, failedPeople2, false);
        }

        succeededPeople2 = function (data, textStatus, jqXHR) {
            console.log("succeededPeople2: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                peopleAutoComplete2 = resultJSON.peoplelist;
            }
            else {
                peopleAutoComplete2 = null;
            }
        };

        failedPeople2 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedPeople2: " + textStatus);
            peopleAutoComplete2 = null;
            alert(textStatus);
        }

        function getPeopleAutocompleteList3() {
            var parameters = ["id", ""];
            PageMethod("getPeopleAutocompleteList3", parameters, succeededPeople3, failedPeople3, false);
        }

        succeededPeople3 = function (data, textStatus, jqXHR) {
            console.log("succeededPeople3: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                peopleAutoComplete3 = resultJSON.peoplelist;
            }
            else {
                peopleAutoComplete3 = null;
            }
        };

        failedPeople3 = function (jqXHR, textStatus, errorThrown) {
            console.log("failedPeople3: " + textStatus);
            peopleAutoComplete3 = null;
            alert(textStatus);
        }

        function getPeopleObject(peopleid, name) {
            var parameters = ["peopleid", peopleid, "name", name];
            PageMethod("getPeopleObject", parameters, succeededPeopleObject, failedPeopleObject, false);
        }

        succeededPeopleObject = function (data, textStatus, jqXHR) {
            console.log("succeededPeopleObject: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {
                //$("#stichno").text("...");
                //$("#stitchdate").val("");
                $("#hidPeopleId").val(resultJSON.people.peopleid);
                //$("#name").val(resultJSON.people.name);
                $("#nokp").val(resultJSON.people.nokp);
                $("#address").val(resultJSON.people.address);
                $("#telno").val(resultJSON.people.telno);
                $("#email").val(resultJSON.people.email);
                //$("#remarks").val(resultJSON.people.remarks);
                //$("#measurement").val("cm").change();
            }
            else
            {
                //$("#stichno").text("...");
                //$("#stitchdate").val("");
                $("#hidPeopleId").val("");
                //$("#name").val("");
                $("#nokp").val("");
                $("#address").val("");
                $("#telno").val("");
                $("#email").val("");
                //$("#remarks").val("");
                //$("#measurement").val("cm").change();
            }
        };

        failedPeopleObject = function (jqXHR, textStatus, errorThrown) {
            console.log("failedPeopleObject: " + textStatus);
            //$("#stichno").text("...");
            //$("#stitchdate").val("");
            $("#hidPeopleId").val("");
            //$("#name").val("");
            $("#nokp").val("");
            $("#address").val("");
            $("#telno").val("");
            $("#email").val("");
            //$("#remarks").val("");
            //$("#measurement").val("cm").change();
            alert(textStatus);
        }

        $(document).ready(function () {

            // initialize autocomplete with custom appendTo
            getPeopleAutocompleteList();
            getPeopleAutocompleteList2();
            getPeopleAutocompleteList3();

            $('#name').autocomplete({
                lookup: peopleAutoComplete,
                onSelect: function (suggestion) {
                    //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    getPeopleObject(suggestion.data,"");
                }
            });
            $('#fname').autocomplete({
                lookup: peopleAutoComplete,
                onSelect: function (suggestion) {
                    //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    //getPeopleObject(suggestion.data, "");
                }
            });
            $('#fnokp').autocomplete({
                lookup: peopleAutoComplete2,
                onSelect: function (suggestion) {
                    //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    //getPeopleObject(suggestion.data, "");
                }
            });
            $('#ftelno').autocomplete({
                lookup: peopleAutoComplete3,
                onSelect: function (suggestion) {
                    //alert('You selected: ' + suggestion.value + ', ' + suggestion.data);
                    //getPeopleObject(suggestion.data, "");
                }
            });

            // defined value of people
            
            $('#name').blur(function () {
                getPeopleObject("",$('#name').val());
            });
            

            $('#datefrom').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });
            $('#dateto').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });
            $('#stitchdate').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            //load list of stitch
            stitchno = "";
            peopleid = "";
            name = $("#fname").val();
            nokp = $("#fnokp").val();
            telno = $("#ftelno").val();
            datefrom = $("#datefrom").val();
            dateto = $("#dateto").val();

            //using single page method
            //getStitchList();
        });

        function getStitchList() {
            var parameters = ["stitchno", stitchno, "peopleid", peopleid, "name", name, "nokp", nokp, "telno", telno, "datefrom", datefrom, "dateto", dateto];
            PageMethod("getStitchListObject", parameters, succeededStitchList, failedStitchList, true);
        }

        succeededStitchList = function (data, textStatus, jqXHR) {
            console.log("succeededStitchList: " + textStatus);
            resultJSON = JSON.parse(data.d);

            if (resultJSON.result == "Y") {

                $.each(resultJSON.stitchlist, function (i, result) {
                    $('<tr><td><a href="#" class="btn-link" onclick=";"><i class="glyphicon glyphicon-play"></i></a></td><td>' + result.name + '</td><td>' + result.peopleid + '</td><td>' + result.telno + '</td><td>' + result.remarks + '</td><td>' + result.name + '</td><td></td></tr>').appendTo('#datatable-buttons');
                });
            }
        };

        failedStitchList = function (jqXHR, textStatus, errorThrown) {
            console.log("failedStitchList: " + textStatus);
            alert(textStatus);
        }

    </script>
</asp:Content>

