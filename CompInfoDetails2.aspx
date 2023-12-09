<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompInfoDetails2.aspx.cs" Inherits="CompInfoDetails2"  ValidateRequest="false" %>

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
            top: 100px;
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
            <form id="form1" runat="server" enctype="multipart/form-data">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form">
                            <label for="compId">ID Comp:</label>
                            <input type="text" id="compCode" class="form-control" name="compCode" value="<%=oModComp.GetSetcomp %>" required="required" readonly/>
                            <label for="compName">Keterangan:</label>
                            <input type="text" id="compName" class="form-control" name="compName" value="<%=oModComp.GetSetcomp_name %>" required="required" />
                            <label for="registerno">No Pendaftaran:</label>
                            <input type="text" id="registerno" class="form-control" name="registerno" value="<%=oModComp.GetSetcomp_registerno %>" required="required"  />
                            <label for="contactname">Nama Pegawai:</label>
                            <input type="text" id="contactname" class="form-control" name="contactname" value="<%=oModComp.GetSetcomp_contact %>" required="required" />
                            <label for="contactno">No Telefon:</label>
                            <input type="text" id="contactno" class="form-control" name="contactno" value="<%=oModComp.GetSetcomp_contactno %>" required="required" />
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form1">
                            <label for="committeeaddress">Alamat:</label>
                            <textarea id="committeeaddress" class="form-control" rows="3" name="committeeaddress" required="required"><%=oModComp.GetSetcomp_address %></textarea>
                            <label for="comparea">Daerah:</label>
                            <select class="form-control" id="comparea" name="comparea" required="required">
                                <option value="">-Sila Pilih-</option>
                                <option value="KS" <%=oModComp.GetSetcomp_daerah.Equals("KS")?"selected":"" %>>Kota Setar</option>
								<option value="LW" <%=oModComp.GetSetcomp_daerah.Equals("LW")?"selected":"" %>>Langkawi</option>
								<option value="KM" <%=oModComp.GetSetcomp_daerah.Equals("KM")?"selected":"" %>>Pendang</option>
								<option value="YN" <%=oModComp.GetSetcomp_daerah.Equals("YN")?"selected":"" %>>Yan</option>
								<option value="SK" <%=oModComp.GetSetcomp_daerah.Equals("SK")?"selected":"" %>>Sik</option>
								<option value="PT" <%=oModComp.GetSetcomp_daerah.Equals("PT")?"selected":"" %>>Padang Terap</option>
								<option value="KP" <%=oModComp.GetSetcomp_daerah.Equals("KP")?"selected":"" %>>Kubang Pasu</option>
								<option value="BL" <%=oModComp.GetSetcomp_daerah.Equals("BL")?"selected":"" %>>Baling</option>
								<option value="PD" <%=oModComp.GetSetcomp_daerah.Equals("PD")?"selected":"" %>>Pendang</option>
								<option value="KL" <%=oModComp.GetSetcomp_daerah.Equals("KL")?"selected":"" %>>Kulim</option>
								<option value="BB" <%=oModComp.GetSetcomp_daerah.Equals("BB")?"selected":"" %>>Bandar Baharu</option>
								<option value="PS" <%=oModComp.GetSetcomp_daerah.Equals("PS")?"selected":"" %>>Pokok Sena</option>
                              </select>
                            <label for="landStatus">Status Organisasi:</label>
                            <select class="form-control" id="landStatus" name="landStatus">
                                <option value="">-select-</option>
                                <option value="BERHAD" <%=oModComp.GetSetcomp_landstatus.Equals("BERHAD")?"selected":"" %>>BERHAD</option>
                                <option value="SDN_BHD" <%=oModComp.GetSetcomp_landstatus.Equals("SDN_BHD")?"selected":"" %>>SENDIRIAN BERHAD</option>
                                <option value="ENTERPRISE" <%=oModComp.GetSetcomp_landstatus.Equals("ENTERPRISE")?"selected":"" %>>ENTERPRISE</option>
                                <option value="OTHER" <%=oModComp.GetSetcomp_landstatus.Equals("OTHER")?"selected":"" %>>LAIN-LAIN</option>
                            </select>
                            <label for="infoStatus">Status:</label>
                            <select class="form-control" id="infoStatus" name="infoStatus">
                                <option value="">-select-</option>
                                <option value="ACTIVE" <%=oModComp.GetSetstatus.Equals("ACTIVE")?"selected":"" %>>ACTIVE</option>
                                <option value="IN-ACTIVE" <%=oModComp.GetSetstatus.Equals("IN-ACTIVE")?"selected":"" %>>IN-ACTIVE</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <section class="panel">
                        <div class="panel-body">
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
                                <button id="btnClose" name="btnClose" type="button" class="btn btn-default" onclick="actionclick('CLOSE');">Tutup</button>
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
                                    <input type="hidden" name="hidCompId" id="hidCompId" value="<%=sCompId %>" />
                                    <input type="hidden" name="hidUserAction" id="hidUserAction" value="<%=sUserAction %>" />
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
                <a id="addimageitem" name="addimageitem" class="btn btn-app" onclick="open_popup('CompInfoImages.aspx?action=OPEN&itemno=SIJIL_<%=oModComp.GetSetcomp %>');">
                    <i class="fa fa-image dark"></i>Sijil Pendaftaran
                </a>
                <a id="addimageitem1" name="addimageitem" class="btn btn-app" onclick="open_popup('CompInfoImages.aspx?action=OPEN&itemno=ORG_<%=oModComp.GetSetcomp %>');">
                    <i class="fa fa-image dark"></i>Gambar Organisasi
                </a>
            </div>
        </div>
    </div>
    <script type="text/javascript"> 
        var maxLength = 1000;
        var filename = "";
        //alert("ayam");
        $(document).ready(function () {
            $('#infoDesc').summernote({
                height: 300,                 // set editor height  
                minHeight: null,             // set minimum height of editor  
                maxHeight: null,             // set maximum height of editor  
                focus: true,
                /*addclass: {
                    debug: false,
                    classTags: [{ title: "Button", "value": "btn btn-success" }, "jumbotron", "lead", "img-rounded", "img-circle", "img-responsive", "btn", "btn btn-success", "btn btn-danger", "text-muted", "text-primary", "text-warning", "text-danger", "text-success", "table-bordered", "table-responsive", "alert", "alert alert-success", "alert alert-info", "alert alert-warning", "alert alert-danger", "visible-sm", "hidden-xs", "hidden-md", "hidden-lg", "hidden-print"]
                },*/
                styleTags: [
                    { title: 'Blockquote', tag: 'blockquote', className: 'blockquote', value: 'blockquote' }
                ],
                toolbar: [
                    // [groupName, [list of button]]
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    /*['font', ['strikethrough', 'superscript', 'subscript']],*/
                    ['style', ['style']],
                    /*['fontsize', ['fontsize']],*/
                    /*['fontname', ['fontname']],*/
                    /*['color', ['color']],*/
                    /*['para', ['ul', 'ol', 'paragraph']],*/
                    /*['height', ['height']],*/
                    /*['insert', ['link', 'picture', 'video']],*/
                    ['misc', ['undo', 'redo', 'help']]
                ],
                hint: {
                    words: ['<%=oModComp.GetSetcomp_name %>', '<%=System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(oModComp.GetSetcomp_name.ToLower()) %>', '<%=oModComp.GetSetcomp_address %>', '<%=System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(oModComp.GetSetcomp_address.ToLower()) %>'],
                    match: /\b(\w{1,})$/,
                    search: function (keyword, callback) {
                        callback($.grep(this.words, function (item) {
                            return item.indexOf(keyword) === 0;
                        }));
                    }
                },
                callbacks: {
                    onImageUpload: function (files) {
                        for (let i = 0; i < files.length; i++) {
                            uploadFile(files[i], i);
                        }
                    }
                } 
            });

            $("p").addClass("text-mute");

            $('#infoDesc').on('summernote.keyup', function (e) {
                /*debugger;*/
                var text = $(this).next('.note-editor').find('.note-editable').text();
                var length = text.length;
                var num = maxLength - length;

                if (length > maxLength) {
                    $('#limite_normal').hide();
                    $('#limite_vermelho').text(maxLength - length + " words remaining").show();
                    alert("You have reacehd a word limit");
                }
                else {
                    $('#limite_vermelho').hide();
                    $('#limite_normal').text(maxLength - length + " words remaining").show();
                }

            });
        });

        function uploadFile(file, i) {
            var pagePath = window.location.hostname;
            //alert(window.location.protocol + "//" + pagePath + getAppPath() + "WebService.asmx/uploadFile");
            var formData = new FormData();
            formData.append('inputitem[0]', '<%=sUserIdComp%>');
            formData.append('inputitem[1]', '<%=sUserId%>');
            //formData.append('inputitem[2]', '<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_MASJID", "ACTIVE")%>');
            formData.append('inputitem[2]', '<%=oModnfo.GetSetinfo_no %>');
            formData.append("inputitem[3]", file);
            formData.append('inputitem[4]', 'Info/' + '<%=sUserIdComp%>' + '/');

            var comp = '<%=sUserIdComp%>';
            //var runningno = '<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_MASJID", "ACTIVE")%>';
            var runningno = '<%=oModnfo.GetSetinfo_no %>';

            if ("<%=sUserId%>" != "") {
                $.ajax({
                    type: 'POST',
                    url: "WebService.asmx/uploadFile2",
                    //url: 'http://localhost:62709/WebService.asmx/uploadFile',
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-upload: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            if (result.filename != "") {
                                var imgNode = document.createElement('img');
                                //imgNode.src = "http://172.16.0.14/eMasjidMS/Attachment/Info/" + comp + "/" + runningno + "_" + file.name;
                                imgNode.src = "http://www.bioappsystem.com/bioappmalay/Attachment/Info/" + comp + "/" + runningno + "_" + file.name;
                                $('#infoDesc').summernote('insertNode', imgNode);
                                filename = result.filename;
                            }
                            else {
                                alert("Internal Server Error!");
                            }
                        }
                        else {
                            alert("Internal Server Error!");
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function () {
                        alert("Internal Server Error!");
                    }
                });
            } else {
                alert("Internal Server Error!");
            }
        }

        function storeFile() {
            var pagePath = window.location.hostname;
            var paramList = "{'comp':'<%=sUserIdComp%>', 'userid':'<%=sUserId%>', 'itemno':'<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_COMP", "ACTIVE")%>','filename':" + filename + "}";
            var formData = new FormData();
            formData.append('inputitem[0]', '<%=sUserIdComp%>');
            formData.append('inputitem[1]', '<%=sUserId%>');
            //formData.append('inputitem[2]', '<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_MASJID", "ACTIVE")%>');
            formData.append('inputitem[2]', '<%=oModnfo.GetSetinfo_no %>');
            formData.append("inputitem[3]", filename);
            formData.append("inputitem[4]", "0");
            formData.append("inputitem[5]", "0");
            formData.append('inputitem[6]', 'Info/' + '<%=sUserIdComp%>' + '/');

            if ("<%=sUserId%>" != "") {
                $.ajax({
                    type: "POST",
                    url: "WebService.asmx/storeFile2?",
                    //url: 'http://localhost:62709/WebService.asmx/storeFile',
                    data: formData,
                    success: function (data, status, xmlData) {
                        console.log("status-store: " + status);
                        message = JSON.stringify(data);
                        result = JSON.parse(message);
                        if (result.status == 'Y') {
                            alert("Proses muat naik gambar berjaya!");
                        }
                        else {
                            alert(result.message);
                        }
                    },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    error: function (status) {
                        alert("Internal Server Error!" + status);
                    }
                });
            }
        }

        function actionclick(action) {
            if (action == 'ADD') {
                $('#compName').removeAttr('required');
                $('#registerno').removeAttr('required');
                $('#contactname').removeAttr('required');
                $('#contactno').removeAttr('required');
                $('#committeeaddress').removeAttr('required');
                $('#comparea').removeAttr('required');
                $('#landStatus').removeAttr('required');
                $('#infoStatus').removeAttr('required');

            } else if (action == 'CLOSE') {
                window.close();
                window.opener.location.reload();
            } else if (action == "CREATE") {
                storeFile();
            } else if(action == "SAVE") {
                //alert($('#infoStatus').val());
            }
            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        function enabledisableinputform(flag) {
            $('#compName').prop('disabled', flag);
            $('#registerno').prop('disabled', flag);
            $('#contactname').prop('disabled', flag);
            $('#contactno').prop('disabled', flag);
            $('#committeeaddress').prop('disabled', flag);
            $('#comparea').prop('disabled', flag);
            $('#landStatus').prop('disabled', flag);
            $('#infoStatus').prop('disabled',flag);

        }
                <%
        if (sAction.Equals("ADD") || sAction.Equals("EDIT"))
        {
                %>
        enabledisableinputform(false);
        //$('#infoDesc').summernote('disable');
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

            //alert(src);
            var idBody = document.getElementById("idBody")
            var fade = document.getElementById("fade_div");
            var popup = document.getElementById("pop_div");
            var popup_content = document.getElementById("pop_content");

            idBody.style.overflow = "hidden";
            popup_content.src = src;
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
