<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="css/loginStyle.css" rel="stylesheet" />
    <!-- Bootstrap -->
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- jQuery -->
    <script src="vendors/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>
</head>
<body>
    <div>
        <div class="wrapper col-lg-offset-3 col-md-offset-3">
            <div class="col-xs-6 col-sm-6 col-md-4 col-lg-4">
                <h4 class="text-info"><a href="http://www.bioappsystem.com" class="h3" style="font-weight: 600"><i>"BIOApp System"</i></a> adalah merupakan sebuah sistem yang dibangunkan bagi tujuan membantu komuniti sosial khasnya peniaga-peniaga yang menjalankan perniagaan secara kecil-kecilan dalam merekod maklumat perniagaan mereka.
                </h4>
                <h4 class="text-info">Sila klik
                    <button class="btn-primary" onclick="openregister();">Daftar</button>
                    untuk menggunakan sistem ini.
                </h4>
                <h4 class="text-info">Untuk tujuan proses penambahbaikan & penyelenggaraan sistem. Sila klik
                    <button class="btn-success" onclick="opendonation();">Donation</button>
                    untuk sumbangan derma anda.
                </h4>
                <h5 class="text-warning"><a href="http://www.bioappsystem.com"><i>Halaman Utama <<</i></a></h5>
            </div>
            <div class="form-signin col-xs-5 col-sm-5 col-md-5 col-lg-5">
                <form id="form1" runat="server">
                    <h2 class="form-signin-heading text-info">Sila Log Masuk</h2>
                    <!--<input type="text" class="form-control" name="username" placeholder="Email Address" required="" autofocus="" />-->
                    <input name="txtUserId" type="text" id="txtUser" class="form-control" placeholder="Id Pengguna" required="" autofocus="" />
                    <p></p>
                    <input name="txtPassword" type="password" id="txtPassword" class="form-control" placeholder="Katalaluan" required="" />
                    <!--<input type="password" class="form-control" name="password" placeholder="Password" required=""/>-->
                    <!--<label class="checkbox">
                    <input type="checkbox" value="remember-me" id="rememberMe" name="rememberMe"> Remember me
                    </label>-->
                    <p></p>
                    <asp:Button ID="btnSubmit" runat="server" Text="Log Masuk" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" />
                    <p></p>
                    <p>
                        <span id="errMsg" style="color: Red; font-family: Verdana; font-size: 10pt;"><%=sMessage %></span>
                    </p>
                </form>
            </div>
        </div>
        <!--BEGIN dialog box for new registration-->
        <div class="modal fade modal-user-dashboard" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                        <h4 class="modal-title">Dashboard Pengguna</h4>
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
                                <input type="hidden" name="hidPeopleId" id="hidPeopleId" value="" />
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
        <!--END dialog box for new registration-->

    </div>
</body>
<script type="text/javascript">
    function openregister() {
        var popupWindow = window.open("NewRegistration.aspx", "open_register", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=600,height=600");
        if (popupWindow == null) {
            alert("Error: While Launching Session Expiry screen.\nYour browser maybe blocking up Popup windows.\nPlease check your Popup Blocker Settings");
        } else {
            wleft = (screen.width - 600) / 2;
            wtop = (screen.height - 600) / 2;
            if (wleft < 0) {
                wleft = 0;
            }
            if (wtop < 0) {
                wtop = 0;
            }
            popupWindow.moveTo(wleft, wtop);
        }
    }

    function opendonation() {
        var popupWindow = window.open("https://toyyibpay.com/donation_maintenance", "open_donation", "toolbar=0,location=0,status=1,menubar=0,resizable=1,scrollbars=1,width=1000,height=800");
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

</script>
</html>
