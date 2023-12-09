<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="CompStaffListing.aspx.cs" Inherits="CompStaffListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="search" runat="server">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
            <h2>Maklumat Kakitangan <small>SENARAI</small></h2>
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
            <a class="btn btn-app" onclick="openaddnewstaff();" data-toggle="modal" data-target=".modal-add-new-staff">
                <i class="fa fa-plus-square green"></i>Daftar 
            </a>
            <a href="#" class="btn btn-app" onclick="opendeptcomp()" data-toggle="modal" data-target=".modal-dept-comp">
                <i class="fa fa-tasks blue"></i>Jabatan 
            </a>
            <a href="#" class="btn btn-app" onclick="opengredcomp()" data-toggle="modal" data-target=".modal-gred-comp">
                <i class="fa fa-sitemap blue" aria-hidden="true"></i>Gred 
            </a>
            <a href="#" class="btn btn-app" onclick="openposcomp()" data-toggle="modal" data-target=".modal-pos-comp">
                <i class="fa fa-newspaper-o blue"></i>Jawatan 
            </a>
            <a href="#" class="btn btn-app" onclick="enabledisablesearchbox();">
                <i class="fa fa-search"></i>Carian
            </a>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12" id="search-box" style="display:none;">
                <section class="panel">

                    <div class="x_title">
                        <h2>Carian Kakitangan</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body">
                            <label for="staffno">No. Pekerja:</label>
                            <input type="text" id="staffno" class="form-control" name="staffno" value="<%=sStaffNo %>" />
                            <label for="staffname">Nama Pekerja:</label>
                            <input type="text" id="staffname" class="form-control" name="staffname" value="<%=sStaffName %>" />
                            <br/>
                            <button type="button" onclick="actionclick('SEARCH');" class="btn btn-primary">Cari</button>
                            <button type="button" onclick="actionclick('RESET');" class="btn btn-warning">Reset</button>
                            <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Batal</button>
                            <div style="display: none;">
                                <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                                <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                            </div>
                    </div>
                </section>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12">
            <table id="datatable-buttons" class="table table-striped jambo_table">
                <thead>
                <tr>
                    <th></th>
                    <th>No. Pekerja</th>
                    <th>Salutasi</th>
                    <th>Nama Pekerja</th>
                    <th>No. K/P</th>
                    <th>Tarikh Lahir</th>
                    <th>Jabatan</th>
                    <th>Gred/ Jawatan</th>
                    <th>No. Tel.</th>
                    <th>Status Login</th>
                    <th>Last Login</th>
                    <th>Status</th>
                </tr>
                </thead>

                <tbody>
                <%
                    if (lsStaff.Count > 0)
                    {
                        for (int i = 0; i < lsStaff.Count; i++)
                        {
                            HRModel modStaff = (HRModel)lsStaff[i];
                %>       
                <tr>
                    <td><a href="#" class="btn-link" onclick="openeditstaff('<%=modStaff.GetSetcomp %>','<%=modStaff.GetSetstaffno %>');"><i class="glyphicon glyphicon-play"></i></a></td>
                    <td><a href="#" class="btn-link" onclick="openeditstaff('<%=modStaff.GetSetcomp %>','<%=modStaff.GetSetstaffno %>');"><%=modStaff.GetSetstaffno %></a></td>
                    <td><%=modStaff.GetSetsalute %></td>
                    <td><%=modStaff.GetSetname %></td>
                    <td><%=modStaff.GetSetnicno %></td>
                    <td><%=modStaff.GetSetstr_dob %></td>
                    <td><%=modStaff.GetSetdept_name %></td>
                    <td><%=modStaff.GetSetgred_name %><br /><%=modStaff.GetSetpos_name %></td>
                    <td><%=modStaff.GetSetmobile1 %></td>
                    <td><%=modStaff.GetSetstatuslogon %></td>
                    <td><%=modStaff.GetSetstr_lastaccess %></td>
                    <td><%=modStaff.GetSetstatus %></td>
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
    <!--BEGIN dialog box for add new user-->
    <div id="myAddStaffComp" class="modal fade modal-add-new-staff" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Tambah Kakitangan</h4>
                </div>
                <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="control-label">No. Pekerja <span class="required">*</span></label>
                        <input id="addstaffno" name="addstaffno" type="text" class="form-control" value=""" />
                        <label class="control-label">Salutasi</label>
                        <select id="addstaffsalute" name="addstaffsalute" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Nama Pekerja <span class="required">*</span></label>
                        <input id="addstaffname" name="addstaffname" type="text" class="form-control" value="" />
                        <label class="control-label">Nick Name</label>
                        <input id="addstaffnickname" name="addstaffnickname" type="text" class="form-control" value="" />
                        <label class="control-label">Warganegara <span class="required">*</span></label>
                        <select id="addstaffnationality" name="addstaffnationality" class="form-control">
                            <option value="WARGANEGARA">WARGANEGARA</option>
                            <option value="BUKAN WARGANEGARA">BUKAN WARGANEGARA</option>
                        </select>
                        <label class="control-label">No. K/P <span class="required">*</span></label>
                        <input id="addstaffnicno" name="addstaffnicno" type="text" maxlength="12" class="form-control" value="" />
                        <label class="control-label">No. Passport</label>
                        <input id="addstaffpassport" name="addstaffpassport" type="text" class="form-control" value="" />
                        <label class="control-label">Tarikh Lahir </label>
                        <input type="text" id="addstaffdob" class="date-picker form-control" name="addstaffdob" value="" />
                        <label class="control-label" style="display:none;">Tempat Lahir </label>
                        <input id="addstaffbirthplace" name="addstaffbirthplace" type="text" class="form-control" value="" style="display:none;"/>
                        <label class="control-label">Jantina <span class="required">*</span></label>
                        <select id="addstaffgender" name="addstaffgender" class="form-control">
                            <option value="LELAKI">LELAKI</option>
                            <option value="PEREMPUAN">PEREMPUAN</option>
                        </select>
                        <label class="control-label">Bangsa <span class="required">*</span></label>
                        <select id="addstaffrace" name="addstaffrace" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Agama <span class="required">*</span></label>
                        <select id="addstaffreligion" name="addstaffreligion" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Status Perkahwinan <span class="required">*</span></label>
                        <select id="addstaffmarital" name="addstaffmarital" class="form-control">
                            <option value="KAHWIN">KAHWIN</option>
                            <option value="BUJANG">BUJANG</option>
                            <option value="DUDA">DUDA</option>
                            <option value="JANDA">JANDA</option>
                        </select>
                        <label class="control-label">User Id <span class="required">*</span></label>
                        <input id="addstaffuserid" name="addstaffuserid" type="text" class="form-control" value="" />
                        <label class="control-label">Password <span class="required">*</span></label>
                        <input id="addstaffpassword" name="addstaffpassword" type="password" class="form-control" value="" />
                        <label class="control-label">Status <span class="required">*</span></label>
                        <select id="addstaffstatus" name="addstaffstatus" class="form-control" tabindex="-1" style="width: 100%;">
                            <option value="ACTIVE">AKTIF</option>
                            <option value="IN-ACTIVE">TIDAK AKTIF</option>
                        </select>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="control-label">Jabatan/ Bahagian <span class="required">*</span></label>
                        <select id="addstaffdeptid" name="addstaffdeptid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Gred Kedudukan <span class="required">*</span></label>
                        <select id="addstaffgredid" name="addstaffgredid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Jawatan <span class="required">*</span></label>
                        <select id="addstaffposid" name="addstaffposid" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Penempatan <span class="required">*</span></label>
                        <select id="addstafftype" name="addstafftype" class="form-control">
                            <option value="PERLANTIKAN">PERLANTIKAN</option>
                            <option value="PROMOSI">PROMOSI</option>
                            <option value="KENAIKAN PANGKAT">KENAIKAN PANGKAT</option>
                            <option value="PENURUNAN PANGKAT">PENURUNAN PANGKAT</option>
                            <option value="PERLETAKAN JAWATAN">PERLETAKAN JAWATAN</option>
                            <option value="PEMINDAHAN">PEMINDAHAN</option>
                            <option value="PEMINJAMAN">PEMINJAMAN</option>
                            <option value="PERSARAAN">PERSARAAN</option>
                            <option value="PENAMATAN">PENAMATAN</option>
                        </select>
                        <label class="control-label">Kategori <span class="required">*</span></label>
                        <select id="addstaffcat" name="addstaffcat" class="form-control">
                            <option value="TETAP">TETAP</option>
                            <option value="KONTRAK">KONTRAK</option>
                            <option value="SEMENTARA">SEMENTARA</option>
                            <option value="SAMBILAN">SAMBILAN</option>
                            <option value="PERCUBAAN">PERCUBAAN</option>
                        </select>
                        <label class="control-label">Tarikh Lantikan <span class="required">*</span></label>
                        <input id="addstafffromdate" name="addstafffromdate" type="text" class="date-picker form-control" value="" />                        
                        <label class="control-label">Alamat Tetap<span class="required">*</span></label>
                        <input id="addstaffpaddress1" name="addstaffpaddress1" type="text" class="form-control" value="" />
                        <input id="addstaffpaddress2" name="addstaffpaddress2" type="text" class="form-control" value="" />
                        <input id="addstaffpaddress3" name="addstaffpaddress3" type="text" class="form-control" value="" />
                        <input id="addstaffpaddress4" name="addstaffpaddress4" type="text" class="form-control" value="" />
                        <label class="control-label">Poskod<span class="required">*</span></label>
                        <input id="addstaffppostcode" name="addstaffppostcode" type="text" class="form-control" value="" />
                        <label class="control-label">Bandar<span class="required">*</span></label>
                        <input id="addstaffpcity" name="addstaffpcity" type="text" class="form-control" value="" />
                        <label class="control-label">Negeri <span class="required">*</span></label>
                        <select id="addstaffpstate" name="addstaffpstate" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Negara <span class="required">*</span></label>
                        <select id="addstaffpcountry" name="addstaffpcountry" class="form-control">
                            <option value="">-Select-</option>
                        </select>
                        <label class="control-label">Telefon</label>
                        <input id="addstaffptelephone" name="addstaffptelephone" type="text" class="form-control" value="" />
                        <label class="control-label">Handphone<span class="required">*</span></label>
                        <input id="addstaffmobile1" name="addstaffmobile1" type="text" class="form-control" value="" />
                        <label class="control-label" style="display:none;">Catatan </label>
                        <textarea id="addstaffremarks" class="form-control" rows="2" name="addstaffremarks" style="display:none;"></textarea>
                    </div>
                </div>
                <div class="modal-footer" style="text-align:left;">
                    <button type="button" class="btn btn-primary" id="btnSaveStaff" onclick="actionclick('CREATE');">Simpan</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>                                            
                </div>
            </div>
                                                           
        </div>
    </div>
    <!--END dialog box for add new user-->

    <!--BEGIN dialog box for dept comp-->
    <div id="myDeptComp" class="modal fade modal-dept-comp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Struktur Jabatan & Organisasi</h4>
                </div>
                <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                    <center>
                        <table>
                            <tr>
                                <td><label for="deptid">Kod : </label></td>
                                <td><input type="text" name="deptid" id="deptid" value="" size="15" /></td>
                            </tr>
                            <tr>
                                <td><label for="deptname">Jabatan/Bahagian : </label></td>
                                <td><input type="text" name="deptname" id="deptname" value="" size="50" /></td>
                            </tr>
                            <tr>
                                <td><label for="deptlevel">Posisi/Level : </label></td>
                                <td>
                                    <select id="deptlevel" name="deptlevel" class="form-control">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="deptreportto">Lapor Kepada : </label></td>
                                <td>
                                    <select id="deptreportto" name="deptreportto" class="form-control">
                                        <option value="">-Select-</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="status">Status : </label></td>
                                <td>
                                    <select id="status" name="status" class="form-control" tabindex="-1" style="width: 100%;">
                                        <option value="ACTIVE">AKTIF</option>
                                        <option value="IN-ACTIVE">TIDAK AKTIF</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                        <br/>
                        <button type="button" class="btn btn-primary" id="btnAddDeptComp" onclick="adddeptcomp();">Tambah</button>
                        <button type="button" class="btn btn-primary" id="btnSaveDeptComp" onclick="savedeptcomp();">Simpan</button>
                        <button type="button" class="btn btn-default" id="btnResetDeptComp" onclick="resetdeptcomp();">Reset</button>
                        <br/>
                        <hr/>
                        <div id="records_table" class="scrollable">
                        <table id="tbl_record" class="table" width="100%" style="border-collapse: collapse">
                        </table>
                        </div>
                    </center>
                </div>
                <div class="modal-footer" style="text-align:left;">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>                                            
                </div>
            </div>
                                                           
        </div>
    </div>
    <!--END dialog box for dept comp-->

    <!--BEGIN dialog box for gred comp-->
    <div id="myGredComp" class="modal fade modal-gred-comp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Gred Kedudukan</h4>
                </div>
                <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                    <center>
                        <table>
                            <tr>
                                <td><label for="gredid">Kod : </label></td>
                                <td><input type="text" name="gredid" id="gredid" value="" size="15" /></td>
                            </tr>
                            <tr>
                                <td><label for="gredname">Gred : </label></td>
                                <td><input type="text" name="gredname" id="gredname" value="" size="50" /></td>
                            </tr>
                            <tr>
                                <td><label for="gredlevel">Posisi/Level : </label></td>
                                <td>
                                    <select id="gredlevel" name="gredlevel" class="form-control">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="gredreportto">Lapor Kepada : </label></td>
                                <td>
                                    <select id="gredreportto" name="gredreportto" class="form-control">
                                        <option value="">-Select-</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="gredstatus">Status : </label></td>
                                <td>
                                    <select id="gredstatus" name="gredstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                        <option value="ACTIVE">AKTIF</option>
                                        <option value="IN-ACTIVE">TIDAK AKTIF</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                        <br/>
                        <button type="button" class="btn btn-primary" id="btnAddGredComp" onclick="addgredcomp();;">Tambah</button>
                        <button type="button" class="btn btn-primary" id="btnSaveGredComp" onclick="savegredcomp();">Simpan</button>
                        <button type="button" class="btn btn-default" id="btnResetGredComp" onclick="resetgredcomp();">Reset</button>
                        <br/>
                        <hr/>
                        <div id="gred_table" class="scrollable">
                        <table id="tbl_gred" class="table" width="100%" style="border-collapse: collapse">
                        </table>
                        </div>
                    </center>
                </div>
                <div class="modal-footer" style="text-align:left;">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>                                            
                </div>
            </div>
                                                           
        </div>
    </div>
    <!--END dialog box for Gred comp-->

    <!--BEGIN dialog box for Post comp-->
    <div id="myPosComp" class="modal fade modal-pos-comp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title">Perjawatan</h4>
                </div>
                <div class="modal-body col-md-12 col-sm-12 col-xs-12">
                    <center>
                        <table>
                            <tr>
                                <td><label for="posid">Kod : </label></td>
                                <td><input type="text" name="posid" id="posid" value="" size="15" /></td>
                            </tr>
                            <tr>
                                <td><label for="posname">Jawatan : </label></td>
                                <td><input type="text" name="posname" id="posname" value="" size="50" /></td>
                            </tr>
                            <tr>
                                <td><label for="poslevel">Posisi/Level : </label></td>
                                <td>
                                    <select id="poslevel" name="poslevel" class="form-control">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="posreportto">Lapor Kepada : </label></td>
                                <td>
                                    <select id="posreportto" name="posreportto" class="form-control">
                                        <option value="">-Select-</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="posstatus">Status : </label></td>
                                <td>
                                    <select id="posstatus" name="posstatus" class="form-control" tabindex="-1" style="width: 100%;">
                                        <option value="ACTIVE">AKTIF</option>
                                        <option value="IN-ACTIVE">TIDAK AKTIF</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                        <br/>
                        <button type="button" class="btn btn-primary" id="btnAddPosComp" onclick="addposcomp();">Tambah</button>
                        <button type="button" class="btn btn-primary" id="btnSavePosComp" onclick="saveposcomp();">Simpan</button>
                        <button type="button" class="btn btn-default" id="btnResetPosComp" onclick="resetposcomp();">Reset</button>
                        <br/>
                        <hr/>
                        <div id="pos_table" class="scrollable">
                        <table id="tbl_pos" class="table" width="100%" style="border-collapse: collapse">
                        </table>
                        </div>
                    </center>
                </div>
                <div class="modal-footer" style="text-align:left;">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Tutup</button>                                            
                </div>
            </div>
                                                           
        </div>
    </div>
    <!--END dialog box for dept comp-->

    </form>

    <script type="text/javascript">
        var currflag = false;

        $(document).ready(function () {
            $('#addstaffdob').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            $('#addstafffromdate').daterangepicker({
                singleDatePicker: true,
                format: 'DD-MM-YYYY',
                calender_style: "picker_1"
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

            var getSaluteList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getSaluteList", getSaluteList_parameters, getSaluteList_succeedAjaxFn, getSaluteList_failedAjaxFn, false);

            var getRaceList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getRaceList", getRaceList_parameters, getRaceList_succeedAjaxFn, getRaceList_failedAjaxFn, false);

            var getReligionList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getReligionList", getReligionList_parameters, getReligionList_succeedAjaxFn, getReligionList_failedAjaxFn, false);

            var getCountryList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCountryList", getCountryList_parameters, getCountryList_succeedAjaxFn, getCountryList_failedAjaxFn, false);

            var getStateList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getStateList", getStateList_parameters, getStateList_succeedAjaxFn, getStateList_failedAjaxFn, false);

            var getDeptList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompDeptList", getDeptList_parameters, getDeptList_succeedAjaxFn, getDeptList_failedAjaxFn, false);

            var getGredList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompGredList", getGredList_parameters, getGredList_succeedAjaxFn, getGredList_failedAjaxFn, false);

            var getPosList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompPosList", getPosList_parameters, getPosList_succeedAjaxFn, getPosList_failedAjaxFn, false);

            enabledisablesearchbox(currflag);

        });

        //BEGIN Response for getSaluteList
        getSaluteList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getSaluteList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getSaluteList = JSON.parse(data.d);
            if (result_getSaluteList.result == "Y") {
                $.each(result_getSaluteList.salutelist, function (i, result) {
                    output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffsalute').html("").append(output);
        };

        getSaluteList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getSaluteList_failedAjaxFn: " + textStatus);
        }
        //END Response for getSaluteList

        //BEGIN Response for getRaceList
        getRaceList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getRaceList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getRaceList = JSON.parse(data.d);
            if (result_getRaceList.result == "Y") {
                $.each(result_getRaceList.racelist, function (i, result) {
                    output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffrace').html("").append(output);
        };

        getRaceList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getRaceList_failedAjaxFn: " + textStatus);
        }
        //END Response for getRaceList

        //BEGIN Response for getReligionList
        getReligionList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getReligionList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getReligionList = JSON.parse(data.d);
            if (result_getReligionList.result == "Y") {
                $.each(result_getReligionList.religionlist, function (i, result) {
                    output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffreligion').html("").append(output);
        };

        getReligionList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getReligionList_failedAjaxFn: " + textStatus);
        }
        //END Response for getReligionList

        //BEGIN Response for getCountryList
        getCountryList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCountryList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCountryList = JSON.parse(data.d);
            if (result_getCountryList.result == "Y") {
                $.each(result_getCountryList.countrylist, function (i, result) {
                    output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffpcountry').html("").append(output);
        };

        getCountryList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCountryList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCountryList

        //BEGIN Response for getStateList
        getStateList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getStateList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getStateList = JSON.parse(data.d);
            if (result_getStateList.result == "Y") {
                $.each(result_getStateList.statelist, function (i, result) {
                    output += "<option value='" + result.GetSetid + "'>" + result.GetSetdesc + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffpstate').html("").append(output);
        };

        getStateList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getStateList_failedAjaxFn: " + textStatus);
        }
        //END Response for getStateList

        //BEGIN Response for getCompDeptList
        getDeptList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getDeptList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompDeptList = JSON.parse(data.d);
            if (result_getCompDeptList.result == "Y") {
                $.each(result_getCompDeptList.deptlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffdeptid').html("").append(output);
        };

        getDeptList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getDeptList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompDeptList

        //BEGIN Response for getCompGredList
        getGredList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getGredList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompGredList = JSON.parse(data.d);
            if (result_getCompGredList.result == "Y") {
                $.each(result_getCompGredList.gredlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffgredid').html("").append(output);
        };

        getGredList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getGredList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompGredList

        //BEGIN Response for getCompPosList
        getPosList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getPosList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompPosList = JSON.parse(data.d);
            if (result_getCompPosList.result == "Y") {
                $.each(result_getCompPosList.poslist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#addstaffposid').html("").append(output);
        };

        getPosList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getPosList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompPosList
        /*
        $("#addstaffnationality").blur(function () {

        });
        */
        $('#addstaffnationality').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            //set disable addstaffpassport
            if ($("#addstaffnationality").val() == "WARGANEGARA") {
                //$("#addstaffpassport").attr('readonly', 'readonly');
                //$("#addstaffpassport").css('background-color', 'gray');
                $('#addstaffnicno').removeAttr('readonly');
                $("#addstaffnicno").css('background-color', '');
            } else {
                //$('#addstaffpassport').removeAttr('readonly');
                //$("#addstaffpassport").css('background-color', '');
                $("#addstaffnicno").attr('readonly', 'readonly');
                $("#addstaffnicno").css('background-color', 'gray');
            }
        });

        $('#addstaffnicno').on('focus', function () {
            //if (this.value == this.defaultValue) this.value = '';

        }).on('blur', function () {
            //get date of birth
            var dob = $(this).val();
            var icno = $(this).val();
            if (icno % 2 == 0) {
                $("#addstaffgender").val("PEREMPUAN");
            }
            else
            {
                $("#addstaffgender").val("LELAKI");

            }
            var yy = dob.substr(0, 2);  // 85
            var mm = dob.substr(2, 2); // 05
            var dd = dob.substr(4, 2);   // 10 
            if (yy > 30) {
                yy = "19" + yy;
            } else {
                yy = "20" + yy;
            }
            $("#addstaffdob").val(dd+"/"+mm+"/"+yy);
        });

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
        function openaddnewstaff() {
            //$('#myAddStaffComp').modal({ backdrop: "static" });
        };

        //BEGIN FOR DEPT COMP************
        function opendeptcomp() {
            //$('#myDeptComp').modal({ backdrop: "static" });
            $('#tbl_record').empty();

            var getCompDeptList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompDeptList", getCompDeptList_parameters, getCompDeptList_succeedAjaxFn, getCompDeptList_failedAjaxFn, false);

            $('#deptid').val("");
            $('#deptname').val("");
            $('#deptlevel').val("");
            $('#deptreportto').val("");
            $('#deptstatus').val("");
            $('#btnAddDeptComp').show();
            $('#btnSaveDeptComp').hide();
            $('#btnResetDeptComp').show();
        }

        //BEGIN Response for getCompDeptList
        getCompDeptList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompDeptList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompDeptList = JSON.parse(data.d);
            if (result_getCompDeptList.result == "Y") {

                var trHTML = '<tr><td class="appbgribbon">#</td>' +
                    '<td align="center"><font><b>Kod</b></font></td>' +
                    '<td align="center"><font><b>Jabatan/ Bahagian</font></b></td>' +
                    '<td align="center"><font><b>Posisi/ Level</b></font></td>' +
                    '<td align="center"><font><b>Lapor Kepada</b></font></td>' +
                    '<td align="center"><font><b>Status</b></font></td>' +
                    '<td align="center"><font><b>&nbsp;</b></font></td>' +
                    '</tr>';
                var idx = 0;
                $.each(result_getCompDeptList.deptlist, function (i, item) {
                    idx += 1;
                    trHTML += '<tr><td valign="top"><font>' + idx + '.</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetsid + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetname + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetlevel + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetreportto + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetstatus + '</font></td>' +
                        '<td align="center" valign="top"><button type="button" onclick="editdeptcomp(\'' + item.GetSetsid + '\',\'' + item.GetSetname + '\',\'' + item.GetSetlevel + '\',\'' + item.GetSetreportto + '\',\'' + item.GetSetstatus + '\');">Edit</button><button type="button" onclick="deletedeptcomp(\'' + item.GetSetsid + '\');">Hapus</button></td>' +
                        '</tr>';
                });

                if (idx > 0) {
                    //$('#records_table').append(trHTML);
                }
                else {
                    trHTML += '<tr><td valign="top" colspan=7 ><font>No record found!</font></td>' +
                        '</tr>';
                }

                $('#tbl_record').append(trHTML);
                //alert(trHTML);
            }
        };

        getCompDeptList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompDeptList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompDeptList

        $('#deptreportto').on('focus', function () {
            if ($("#deptlevel").val()) {
                if ($("#deptlevel").val().length > 0) {
                    getDeptReportTo();
                }
            }
        });

        function getDeptReportTo() {
            var getCompDeptReportTo_parameters = ["currcomp", "<%=sCurrComp%>", "deptlevel", $("#deptlevel").val()];
            PageMethod("getCompDeptReportTo", getCompDeptReportTo_parameters, getCompDeptReportTo_succeedAjaxFn, getCompDeptReportTo_failedAjaxFn, false);
        }

        //BEGIN Response for getDeptReportTo
        getCompDeptReportTo_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompDeptReportTo_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompDeptReportTo = JSON.parse(data.d);
            if (result_getCompDeptReportTo.result == "Y") {
                $.each(result_getCompDeptReportTo.deptlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#deptreportto').html("").append(output);
        };

        getCompDeptReportTo_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompDeptReportTo_failedAjaxFn: " + textStatus);
        }
        //END Response for getDeptReportTo

        function adddeptcomp() {
            //to add
            var proceed = true;
            if ($('#deptid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#deptname').val() == "") {
                proceed = false;
                alert("Sila isi Jabatan/Bahagian!");
            }
            else if ($('#deptlevel').val() == "" || $('#deptlevel').val() == null || $('#deptlevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#deptstatus').val() == null || $('#deptstatus').val() == "" || $('#deptstatus').val() == undefined) {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var insertDeptComp_parameters = ["currcomp", "<%=sCurrComp%>", "deptid", $('#deptid').val(), "deptname", $('#deptname').val(), "deptlevel", $('#deptlevel').val(), "deptreportto", $('#deptreportto').val(), "status", $('#status').val()];
                PageMethod("insertDeptComp", insertDeptComp_parameters, insertDeptComp_succeedAjaxFn, insertDeptComp_failedAjaxFn, false);
            }
        }

        var insertDeptComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertDeptComp_succeedAjaxFn: " + textStatus);
            var insertDeptComp_result = JSON.parse(data.d);
            if (insertDeptComp_result.result == "Y")
            {
                //alert(insertDeptComp_result.message);
                opendeptcomp();
            }
            else {
                alert(insertDeptComp_result.message);
            }
        }

        var insertDeptComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertDeptComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to insert dept comp...");
        }

        function editdeptcomp(deptid, deptname, deptlevel, deptreportto, deptstatus) {
            //to edit
            $('#deptid').val(deptid);
            $('#deptname').val(deptname);
            $('#deptlevel').val(deptlevel);
            $('#deptreportto').val(deptreportto);
            $('#deptstatus').val(deptstatus);
            $('#btnAddDeptComp').hide();
            $('#btnSaveDeptComp').show();
            $('#btnResetDeptComp').show();
        }

        function savedeptcomp() {
            //to save
            var proceed = true;
            if ($('#deptid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#deptname').val() == "") {
                proceed = false;
                alert("Sila isi Jabatan/Bahagian!");
            }
            else if ($('#deptlevel').val() == "" || $('#deptlevel').val() == null || $('#deptlevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#deptstatus').val() == null || $('#deptstatus').val() == "" || $('#deptstatus').val() == undefined) {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var updateDeptComp_parameters = ["currcomp", "<%=sCurrComp%>", "deptid", $('#deptid').val(), "deptname", $('#deptname').val(), "deptlevel", $('#deptlevel').val(), "deptreportto", $('#deptreportto').val(), "status", $('#status').val()];
                PageMethod("updateDeptComp", updateDeptComp_parameters, updateDeptComp_succeedAjaxFn, updateDeptComp_failedAjaxFn, false);
            }
        }

        var updateDeptComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateDeptComp_succeedAjaxFn: " + textStatus);
            var updateDeptComp_result = JSON.parse(data.d);
            if (updateDeptComp_result.result == "Y") {
                //alert(insertDeptComp_result.message);
                opendeptcomp();
            }
            else {
                alert(updateDeptComp_result.message);
            }
        }

        var updateDeptComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateDeptComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to update dept comp...");
        }

        function resetdeptcomp() {
            opendeptcomp();
        }

        function deletedeptcomp(deptid) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteDeptComp_parameters = ["currcomp", "<%=sCurrComp%>", "deptid", deptid];
                PageMethod("deleteDeptComp", deleteDeptComp_parameters, deleteDeptComp_succeedAjaxFn, deleteDeptComp_failedAjaxFn, false);
            }
        }

        var deleteDeptComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteDeptComp_succeedAjaxFn: " + textStatus);
            var deleteDeptComp_result = JSON.parse(data.d);
            if (deleteDeptComp_result.result == "Y") {
                //alert(insertDeptComp_result.message);
                opendeptcomp();
            }
            else {
                alert(deleteDeptComp_result.message);
            }
        }

        var deleteDeptComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteDeptComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete dept comp...");
        }
        //END FOR DEPT COMP************

        //BEGIN FOR GRADE COMP************
        function opengredcomp() {
            //$('#myGredComp').modal({ backdrop: "static" });
            $('#tbl_gred').empty();

            var getCompGredList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompGredList", getCompGredList_parameters, getCompGredList_succeedAjaxFn, getCompGredList_failedAjaxFn, false);

            $('#gredid').val("");
            $('#gredname').val("");
            $('#gredlevel').val("");
            $('#gredreportto').val("");
            $('#gredstatus').val("");
            $('#btnAddGredComp').show();
            $('#btnSaveGredComp').hide();
            $('#btnResetGredComp').show();
        }

        //BEGIN Response for getCompGredList
        getCompGredList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompGredList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompGredList = JSON.parse(data.d);
            if (result_getCompGredList.result == "Y") {

                var trHTML = '<tr><td class="appbgribbon">#</td>' +
                    '<td align="center"><font><b>Kod</b></font></td>' +
                    '<td align="center"><font><b>Gred Kedudukan</font></b></td>' +
                    '<td align="center"><font><b>Posisi/ Level</b></font></td>' +
                    '<td align="center"><font><b>Lapor Kepada</b></font></td>' +
                    '<td align="center"><font><b>Status</b></font></td>' +
                    '<td align="center"><font><b>&nbsp;</b></font></td>' +
                    '</tr>';
                var idx = 0;
                $.each(result_getCompGredList.gredlist, function (i, item) {
                    idx += 1;
                    trHTML += '<tr><td valign="top"><font>' + idx + '.</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetsid + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetname + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetlevel + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetreportto + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetstatus + '</font></td>' +
                        '<td align="center" valign="top"><button type="button" onclick="editgredcomp(\'' + item.GetSetsid + '\',\'' + item.GetSetname + '\',\'' + item.GetSetlevel + '\',\'' + item.GetSetreportto + '\',\'' + item.GetSetstatus + '\');">Edit</button><button type="button" onclick="deletegredcomp(\'' + item.GetSetsid + '\');">Hapus</button></td>' +
                        '</tr>';
                });

                if (idx > 0) {
                    //$('#records_table').append(trHTML);
                }
                else {
                    trHTML += '<tr><td valign="top" colspan=7 ><font>No record found!</font></td>' +
                        '</tr>';
                }

                $('#tbl_gred').append(trHTML);
            }
        };

        getCompGredList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompGredList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompGredList

        $('#gredreportto').on('focus', function () {
            if ($('#gredlevel').val()) {
                if ($("#gredlevel").val().length > 0) {
                    getGredReportTo();
                }
            }
        });

        function getGredReportTo() {
            var getCompGredReportTo_parameters = ["currcomp", "<%=sCurrComp%>", "gredlevel", $("#gredlevel").val()];
            PageMethod("getCompGredReportTo", getCompGredReportTo_parameters, getCompGredReportTo_succeedAjaxFn, getCompGredReportTo_failedAjaxFn, false);
        }

        //BEGIN Response for getGredReportTo
        getCompGredReportTo_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompGredReportTo_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompGredReportTo = JSON.parse(data.d);
            if (result_getCompGredReportTo.result == "Y") {
                $.each(result_getCompGredReportTo.gredlist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#gredreportto').html("").append(output);
        };

        getCompGredReportTo_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompGredReportTo_failedAjaxFn: " + textStatus);
        }
        //END Response for getGredReportTo

        function addgredcomp() {
            //to add
            var proceed = true;
            if ($('#gredid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#gredname').val() == "") {
                proceed = false;
                alert("Sila isi Gred Kedudukan!");
            }
            else if ($('#gredlevel').val() == "" || $('#gredlevel').val() == null || $('#gredlevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#gredstatus').val() == null || $('#gredstatus').val() == "" || $('#gredstatus').val() == undefined) {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var insertGredComp_parameters = ["currcomp", "<%=sCurrComp%>", "gredid", $('#gredid').val(), "gredname", $('#gredname').val(), "gredlevel", $('#gredlevel').val(), "gredreportto", $('#gredreportto').val(), "status", $('#status').val()];
                PageMethod("insertGredComp", insertGredComp_parameters, insertGredComp_succeedAjaxFn, insertGredComp_failedAjaxFn, false);
            }
        }

        var insertGredComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertGredComp_succeedAjaxFn: " + textStatus);
            var insertGredComp_result = JSON.parse(data.d);
            if (insertGredComp_result.result == "Y")
            {
                opengredcomp();
            }
            else {
                alert(insertGredComp_result.message);
            }
        }

        var insertGredComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertGredComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to insert gred comp...");
        }

        function editgredcomp(gredid, gredname, gredlevel, gredreportto, gredstatus) {
            //to edit
            $('#gredid').val(gredid);
            $('#gredname').val(gredname);
            $('#gredlevel').val(gredlevel);
            $('#gredreportto').val(gredreportto);
            $('#gredstatus').val(gredstatus);
            $('#btnAddGredComp').hide();
            $('#btnSaveGredComp').show();
            $('#btnResetGredComp').show();
        }

        function savegredcomp() {
            //to save
            var proceed = true;
            if ($('#gredid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#gredname').val() == "") {
                proceed = false;
                alert("Sila isi Gred Kedudukan!");
            }
            else if ($('#gredlevel').val() == "" || $('#gredlevel').val() == null || $('#gredlevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#gredstatus').val() == null || $('#gredstatus').val() == "" || $('#gredstatus').val() == undefined) {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var updateGredComp_parameters = ["currcomp", "<%=sCurrComp%>", "gredid", $('#gredid').val(), "gredname", $('#gredname').val(), "gredlevel", $('#gredlevel').val(), "gredreportto", $('#gredreportto').val(), "status", $('#status').val()];
                PageMethod("updateGredComp", updateGredComp_parameters, updateGredComp_succeedAjaxFn, updateGredComp_failedAjaxFn, false);
            }
        }

        var updateGredComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updateGredComp_succeedAjaxFn: " + textStatus);
            var updateGredComp_result = JSON.parse(data.d);
            if (updateGredComp_result.result == "Y") {
                //alert(updateGredComp_result.message);
                opengredcomp();
            }
            else {
                alert(updateGredComp_result.message);
            }
        }

        var updateGredComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updateGredComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to update gred comp...");
        }

        function resetgredcomp() {
            opengredcomp();
        }

        function deletegredcomp(gredid) {
            if (confirm("Adakah anda pasti?") == true) {
                var deleteGredComp_parameters = ["currcomp", "<%=sCurrComp%>", "gredid", gredid];
                PageMethod("deleteGredComp", deleteGredComp_parameters, deleteGredComp_succeedAjaxFn, deleteGredComp_failedAjaxFn, false);
            }
        }

        var deleteGredComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deleteGredComp_succeedAjaxFn: " + textStatus);
            var deleteGredComp_result = JSON.parse(data.d);
            if (deleteGredComp_result.result == "Y") {
                //alert(deleteGredComp_result.message);
                opengredcomp();
            }
            else {
                alert(deleteGredComp_result.message);
            }
        }

        var deleteGredComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deleteGredComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete gred comp...");
        }
        //END FOR GRADE COMP************

        //BEGIN FOR POSITION COMP************
        function openposcomp() {
            //$('#myPosComp').modal({ backdrop: "static" });
            $('#tbl_pos').empty();

            var getCompPosList_parameters = ["currcomp", "<%=sCurrComp%>"];
            PageMethod("getCompPosList", getCompPosList_parameters, getCompPosList_succeedAjaxFn, getCompPosList_failedAjaxFn, false);

            $('#posid').val("");
            $('#posname').val("");
            $('#poslevel').val("");
            $('#posreportto').val("");
            $('#posstatus').val("");
            $('#btnAddPosComp').show();
            $('#btnSavePosComp').hide();
            $('#btnResetPosComp').show();
        }

        //BEGIN Response for getCompPosList
        getCompPosList_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompPosList_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompPosList = JSON.parse(data.d);
            if (result_getCompPosList.result == "Y") {

                var trHTML = '<tr><td class="appbgribbon">#</td>' +
                    '<td align="center"><font><b>Kod</b></font></td>' +
                    '<td align="center"><font><b>Jawatan</font></b></td>' +
                    '<td align="center"><font><b>Posisi/ Level</b></font></td>' +
                    '<td align="center"><font><b>Lapor Kepada</b></font></td>' +
                    '<td align="center"><font><b>Status</b></font></td>' +
                    '<td align="center"><font><b>&nbsp;</b></font></td>' +
                    '</tr>';
                var idx = 0;
                $.each(result_getCompPosList.poslist, function (i, item) {
                    idx += 1;
                    trHTML += '<tr><td valign="top"><font>' + idx + '.</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetsid + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetname + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetlevel + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetreportto + '</font></td>' +
                        '<td align="center" valign="top"><font>' + item.GetSetstatus + '</font></td>' +
                        '<td align="center" valign="top"><button type="button" onclick="editposcomp(\'' + item.GetSetsid + '\',\'' + item.GetSetname + '\',\'' + item.GetSetlevel + '\',\'' + item.GetSetreportto + '\',\'' + item.GetSetstatus + '\');">Edit</button><button type="button" onclick="deleteposcomp(\'' + item.GetSetsid + '\');">Hapus</button></td>' +
                        '</tr>';
                });

                if (idx > 0) {
                    //$('#records_table').append(trHTML);
                }
                else {
                    trHTML += '<tr><td valign="top" colspan=7 ><font>No record found!</font></td>' +
                        '</tr>';
                }

                $('#tbl_pos').append(trHTML);
            }
        };

        getCompPosList_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompPosList_failedAjaxFn: " + textStatus);
        }
        //END Response for getCompPosList

        $('#posreportto').on('focus', function () {
            if ($("#poslevel").val()) {
                if ($("#poslevel").val().length > 0) {
                    getPosReportTo();
                }
            }
        });

        function getPosReportTo() {
            var getCompPosReportTo_parameters = ["currcomp", "<%=sCurrComp%>", "poslevel", $("#poslevel").val()];
            PageMethod("getCompPosReportTo", getCompPosReportTo_parameters, getCompPosReportTo_succeedAjaxFn, getCompPosReportTo_failedAjaxFn, false);
        }

        //BEGIN Response for getPosReportTo
        getCompPosReportTo_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("getCompPosReportTo_succeedAjaxFn: " + textStatus);
            var output = "";
            result_getCompPosReportTo = JSON.parse(data.d);
            if (result_getCompPosReportTo.result == "Y") {
                $.each(result_getCompPosReportTo.poslist, function (i, result) {
                    output += "<option value='" + result.GetSetsid + "'>" + result.GetSetname + "</option>";
                });
            } else {
                output += "<option value=''>-Sila Pilih-</option>";
            }
            $('#posreportto').html("").append(output);
        };

        getCompPosReportTo_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("getCompPosReportTo_failedAjaxFn: " + textStatus);
        }
        //END Response for getPosReportTo

        function addposcomp() {
            //to add
            var proceed = true;
            if ($('#posid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#posname').val() == "") {
                proceed = false;
                alert("Sila isi Jawatan!");
            }
            else if ($('#poslevel').val() == "" || $('#poslevel').val() == null || $('#poslevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#posstatus').val() == null || $('#posstatus').val() == undefined || $('#posstatus').val() == "") {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var insertPosComp_parameters = ["currcomp", "<%=sCurrComp%>", "posid", $('#posid').val(), "posname", $('#posname').val(), "poslevel", $('#poslevel').val(), "posreportto", $('#posreportto').val(), "status", $('#status').val()];
                PageMethod("insertPosComp", insertPosComp_parameters, insertPosComp_succeedAjaxFn, insertPosComp_failedAjaxFn, false);
            }
        }

        var insertPosComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("insertPosComp_succeedAjaxFn: " + textStatus);
            var insertPosComp_result = JSON.parse(data.d);
            if (insertPosComp_result.result == "Y")
            {
                //alert(insertPosComp_result.message);
                openposcomp();
            }
            else {
                alert(insertPosComp_result.message);
            }
        }

        var insertPosComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("insertPosComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to insert dept comp...");
        }

        function editposcomp(posid, posname, poslevel, posreportto, posstatus) {
            //to edit
            $('#posid').val(posid);
            $('#posname').val(posname);
            $('#poslevel').val(poslevel);
            $('#posreportto').val(posreportto);
            $('#posstatus').val(posstatus);
            $('#btnAddPosComp').hide();
            $('#btnSavePosComp').show();
            $('#btnResetPosComp').show();
        }

        function saveposcomp() {
            //to save
            var proceed = true;
            if ($('#posid').val() == "") {
                proceed = false;
                alert("Sila isi Kod!");
            }
            else if ($('#posname').val() == "") {
                proceed = false;
                alert("Sila isi Jawatan!");
            }
            else if ($('#poslevel').val() == "" || $('#poslevel').val() == null || $('#poslevel').val() == undefined) {
                proceed = false;
                alert("Sila isi Posisi/Level!");
            }
            else if ($('#posstatus').val() == null || $('#posstatus').val() == undefined || $('#posstatus').val() == "") {
                proceed = false;
                alert("Sila isi Status!");
            }
            if (proceed) {
                var updatePosComp_parameters = ["currcomp", "<%=sCurrComp%>", "posid", $('#posid').val(), "posname", $('#posname').val(), "poslevel", $('#poslevel').val(), "posreportto", $('#posreportto').val(), "status", $('#status').val()];
                PageMethod("updatePosComp", updatePosComp_parameters, updatePosComp_succeedAjaxFn, updatePosComp_failedAjaxFn, false);
            }
        }

        var updatePosComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("updatePosComp_succeedAjaxFn: " + textStatus);
            var updatePosComp_result = JSON.parse(data.d);
            if (updatePosComp_result.result == "Y") {
                openposcomp();
            }
            else {
                alert(updatePosComp_result.message);
            }
        }

        var updatePosComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("updatePosComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to update pos comp...");
        }

        function resetposcomp() {
            openposcomp();
        }

        function deleteposcomp(posid) {
            if (confirm("Adakah anda pasti?") == true) {
                var deletePosComp_parameters = ["currcomp", "<%=sCurrComp%>", "posid", posid];
                PageMethod("deletePosComp", deletePosComp_parameters, deletePosComp_succeedAjaxFn, deletePosComp_failedAjaxFn, false);
            }
        }

        var deletePosComp_succeedAjaxFn = function (data, textStatus, jqXHR) {
            console.log("deletePosComp_succeedAjaxFn: " + textStatus);
            var deletePosComp_result = JSON.parse(data.d);
            if (deletePosComp_result.result == "Y") {
                openposcomp();
            }
            else {
                alert(deletePosComp_result.message);
            }
        }

        var deletePosComp_failedAjaxFn = function (jqXHR, textStatus, errorThrown) {
            console.log("deletePosComp_failedAjaxFn: " + textStatus);
            alert("Error!\nUnable to delete pos comp...");
        }
        //END FOR POSITION COMP************

        function openeditstaff(comp, staffno) {
            var popupWindow = window.open("CompStaffDetails.aspx?action=OPEN&comp=" + comp + "&staffno=" + staffno, "open_staff", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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
            var proceed = true;
            if (action == "CREATE") {
                if ($('#addstaffno').val() == "") {
                    proceed = false;
                    alert("Sila isi No. Pekerja!");
                }
                else if ($('#addstaffname').val() == "") {
                    proceed = false;
                    alert("Sila isi Nama Pekerja!");
                }
                else if ($('#addstaffnicno').val() == "") {
                    proceed = false;
                    alert("Sila isi No. K/P!");
                }
                else if ($('#addstaffrace').val() == "" || $('#addstaffrace').val() == null || $('#addstaffrace').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Bangsa!");
                }
                else if ($('#addstaffreligion').val() == "" || $('#addstaffreligion').val() == null || $('#addstaffreligion').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Agama!");
                }
                else if ($('#addstaffuserid').val() == "") {
                    proceed = false;
                    alert("Sila isi User Id!");
                }
                else if ($('#addstaffpassword').val() == "") {
                    proceed = false;
                    alert("Sila isi Password!");
                }
                else if ($('#addstaffdeptid').val() == "" || $('#addstaffdeptid').val() == null || $('#addstaffdeptid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jabatan/Bahagian!");
                }
                else if ($('#addstaffgredid').val() == "" || $('#addstaffgredid').val() == null || $('#addstaffgredid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Gred Kedudukan!");
                }
                else if ($('#addstaffposid').val() == "" || $('#addstaffposid').val() == null || $('#addstaffposid').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Jawatan!");
                }
                else if ($('#addstafffromdate').val() == "") {
                    proceed = false;
                    alert("Sila isi Tarikh Lantikan!");
                }
                else if ($('#addstaffpaddress1').val() == "") {
                    proceed = false;
                    alert("Sila isi Alamat!");
                }
                else if ($('#addstaffppostcode').val() == "") {
                    proceed = false;
                    alert("Sila isi Poskod!");
                }
                else if ($('#addstaffpcity').val() == "") {
                    proceed = false;
                    alert("Sila isi Bandar!");
                }
                else if ($('#addstaffpstate').val() == "" || $('#addstaffpstate').val() == null || $('#addstaffpstate').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Negeri!");
                }
                else if ($('#addstaffpcountry').val() == null || $('#addstaffpcountry').val() == "" || $('#addstaffpcountry').val() == undefined) {
                    proceed = false;
                    alert("Sila isi Negara!");
                }
                else if ($('#addstaffmobile1').val() == "") {
                    proceed = false;
                    alert("Sila isi Nombor Handphone!");
                }
            }
            else if (action == "RESET")
            {
                $("#staffno").val("");
                $("#staffname").val("");
            } 
            if (proceed) {
                document.getElementById("hidAction").value = action;
                var button = document.getElementById("<%=btnAction.ClientID %>");
                button.click();
            }
        }

    </script>
</asp:Content>

