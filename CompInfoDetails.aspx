<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="CompInfoDetails.aspx.cs" Inherits="CompInfoDetails"  ValidateRequest="false" %>

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
            <form id="form1" runat="server" enctype="multipart/form-data">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div id="add-form1">
                            <label for="compId">Id Comp:</label>
                            <input type="text" id="compCode" class="form-control" name="compCode" value="<%=oModComp.GetSetcomp %>" required="required" readonly="readonly" />
                            <label for="compName">Keterangan:</label>
                            <input type="text" id="compName" class="form-control" name="compName" value="<%=oModComp.GetSetcomp_name %>" required="required"  readonly="readonly"/>
                            <label for="infoNo">No Info:</label>
                            <input type="text" id="infoNo" class="form-control" name="infoNo" value="<%=oModnfo.GetSetinfo_no %>" required="required" readonly="readonly" />
                            <label for="infoType">Jenis:</label>
                            <select class="form-control" id="infoType" name="infoType">
                                <option value="">-select-</option>
                                <option value="PERUTUSAN" <%=oModnfo.GetSetinfo_type.Equals("PERUTUSAN")?"selected":"" %>>PERUTUSAN</option>
                                <option value="SEJARAH" <%=oModnfo.GetSetinfo_type.Equals("SEJARAH")?"selected":"" %>>SEJARAH</option>
                                <option value="VISI" <%=oModnfo.GetSetinfo_type.Equals("VISI")?"selected":"" %>>VISI</option>
                                <option value="MISI" <%=oModnfo.GetSetinfo_type.Equals("MISI")?"selected":"" %>>MISI</option>
                            </select>
                            <label for="infoStatus">Status:</label>
                            <select class="form-control" id="infoStatus" name="infoStatus">
                                <option value="">-select-</option>
                                <option value="ACTIVE" <%=oModnfo.GetSetinfo_status.Equals("ACTIVE")?"selected":"" %>>ACTIVE</option>
                                <option value="IN-ACTIVE" <%=oModnfo.GetSetinfo_status.Equals("IN-ACTIVE")?"selected":"" %>>IN-ACTIVE</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div id="add-form2">
                            <label for="infoDesc">Huraian:</label>
                            <textarea id="infoDesc" class="form-control" rows="3" name="infoDesc"><%=oModnfo.GetSetinfo_desc %></textarea> 
                            <h5 id="limite_vermelho" style="text-align:right;color:red"></h5>
                            <h5 id="limite_normal" style="text-align:right"></h5>
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
                                <button id="btnDelete" name="btnDelete" type="button" class="btn btn-danger" onclick="actionclick('DELETE');">Hapus</button>
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
        </div>
    </div>
    <script type="text/javascript"> 
        var maxLength = 1000;
        var filename = "";

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
            //formData.append('inputitem[2]', '<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_COMP", "ACTIVE")%>');
            formData.append('inputitem[2]', '<%=oModnfo.GetSetinfo_no %>');
            formData.append("inputitem[3]", file);
            formData.append('inputitem[4]', 'Info/' + '<%=sUserIdComp%>' + '/');

            var comp = '<%=sUserIdComp%>';
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
                                //imgNode.src = "https://websvc.zakatkedah.com.my/eMasjidMS/Attachment/Info/" + comp + "/" + runningno + "_" + file.name;
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
            var paramList = "{'comp':'<%=sUserIdComp%>', 'userid':'<%=sUserId%>', 'itemno':'<%=oMainCon.getNextRunningNo(sUserIdComp, "INFO_MASJID", "ACTIVE")%>','filename':" + filename + "}";
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
                        alert("Internal Server Error!ayam" + status);
                    }
                });
            }
        }

        function actionclick(action) {
            if (action == 'ADD') {
                $('#infoType').removeAttr('required');
                $('#infoDesc').removeAttr('required');
                $('#infoStatus').removeAttr('required');
            } else if (action == 'CLOSE') {
                window.close();
                window.opener.location.reload();
            } else if (action == "CREATE") {
                //storeFile();
            }

            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
        }

        function enabledisableinputform(flag) {
            $('#infoType').prop('disabled', flag);
            $('#infoDesc').prop('disabled', flag);
            $('#infoStatus').prop('disabled', flag);
        }
                <%
        if (sAction.Equals("ADD") || sAction.Equals("EDIT") || sAction.Equals("DELETE"))
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
    </script>
</asp:Content>
