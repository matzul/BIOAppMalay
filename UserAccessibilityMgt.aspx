<%@ Page Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="UserAccessibilityMgt.aspx.cs" Inherits="UserAccessibilityMgt" %>

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
            <br/>
            <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
			  
              <li class="nav-item active">
				<a class="nav-link" id="role-tab" data-toggle="tab" href="#role" role="tab" aria-controls="roleaccess"
				  aria-selected="false">Role</a>
			  </li>
			  <li class="nav-item ">
				<a class="nav-link" id="module-tab" data-toggle="tab" href="#module" role="tab" aria-controls="roleaccess"
				  aria-selected="false">Module</a>
			  </li>
			  <li class="nav-item ">
				<a class="nav-link" id="submodule-tab" data-toggle="tab" href="#submodule" role="tab" aria-controls="roleaccess"
				  aria-selected="false">Submodule</a>
			  </li>
			  <li class="nav-item">
				<a class="nav-link" id="screen-tab" data-toggle="tab" href="#screen" role="tab" aria-controls="roleaccess"
				  aria-selected="false">Screen</a>
			  </li>
				
				  
				  
			</ul>
			<form id="form1" runat="server">
			<div class="tab-content" id="myTabContent1">
			<%-- Role --%>
            <div class="tab-pane active" id="role" role="tabpanel" aria-labelledby="role-tab">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" data-target="#collapse-role" href="#collapse-role">
								Role
							</a>
						</h4>
					</div>
					<div class="panel-body">
						<div id="collapse-role" class="panel-collapse collapse in">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<a class="btn btn-app" data-toggle="modal" data-target=".modal-role"><i class="fa fa-plus-square green"></i>Daftar Role</a>
							</div>
							<div class="col-md-12 col-sm-12 col-xs-12">
                        <table id="datatable-role" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th>Id/ No. Role</th>
                          <th>Nama Role</th>
                          <th>Keterangan Role</th>
                          <th>Status</th>
						  <th><i class="fa fa-cog"></i></th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsRole.Count > 0)
                            {
                                for (int i = 0; i < lsRole.Count; i++)
                                {
                                    MainModel modRole = (MainModel)lsRole[i];
                        %>       
                        <tr>

                          <td><%=modRole.GetSetroleid %></td>
                          <td><%=modRole.GetSetrolename %></td>
                          <td><%=modRole.GetSetroledesc %></td>
						  <td><%=modRole.GetSetrolestatus %></td>
						  <td><a href="#" class="btn btn-info btn-xs" onclick="openeditrole('<%=modRole.GetSetroleid %>');">Kemaskini</a></td>
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
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    <div class="toolbar"></div>
                    </div>
						</div>
					</div>
				</div>
			</div>
			<%-- End of Role --%>

			<%-- module --%>
            <div class="tab-pane" id="module" role="tabpanel" aria-labelledby="module-tab">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" data-target="#collapse-module" href="#collapse-module">
								Module
							</a>
						</h4>
					</div>
					<div class="panel-body">
						<div id="collapse-module" class="panel-collapse collapse in">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<a class="btn btn-app" data-toggle="modal" data-target=".modal-module"><i class="fa fa-plus-square green"></i>Daftar Module</a>
							</div>
					<div class="col-md-12 col-sm-12 col-xs-12">
                      <table id="datatable-module" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th>ID Modul </th>
                          <th>Nama Modul</th>
                          <th>Keterangan Modul</th>
                          <th>Status Modul</th>
						  <th>Icon Modul</th>
						  <th><i class="fa fa-cog"></i></th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsModule.Count > 0)
                            {
                                for (int i = 0; i < lsModule.Count; i++)
                                {
                                    MainModel modModule = (MainModel)lsModule[i];
                        %>       
                        <tr>
                          <td><%=modModule.GetSetmoduleid %></td>
                          <td><%=modModule.GetSetmodulename %></td>
                          <td><%=modModule.GetSetmoduledesc %></td>
						  <td><%=modModule.GetSetmodulestatus %></td>
						  <td><%=modModule.GetSetmoduleicon %></td>
						  <td><a href="#" class="btn btn-info btn-xs" onclick="openeditmodule('<%=modModule.GetSetmoduleid %>');">Kemaskini</a></td>
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
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    <div class="toolbar"></div>
                    </div>
						</div>
					</div>
				</div>
			</div>
			<%-- End of module --%>

			<%-- submodule --%>
            <div class="tab-pane" id="submodule" role="tabpanel" aria-labelledby="submodule-tab">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" data-target="#collapse-submodule" href="#collapse-submodule">
								Submodule
							</a>
						</h4>
					</div>
					<div class="panel-body">
						<div id="collapse-submodule" class="panel-collapse collapse in">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<a class="btn btn-app" data-toggle="modal" data-target=".modal-submodule"><i class="fa fa-plus-square green"></i>Daftar Submodule</a>
							</div>
							<div class="col-md-12 col-sm-12 col-xs-12">
                      <table id="datatable-submodule" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th>ID Submodul </th>
						  <th>ID Modul </th>
                          <th>Nama Submodul</th>
                          <th>Keterangan Submodul</th>
                          <th>Status Submodul</th>
						  <th><i class="fa fa-cog"></i></th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsSubmodule.Count > 0)
                            {
                                for (int i = 0; i < lsSubmodule.Count; i++)
                                {
                                    MainModel modSubmodule = (MainModel)lsSubmodule[i];
                        %>       
                        <tr>
                          <td><%=modSubmodule.GetSetsubmoduleid %></td>
                          <td><%=modSubmodule.GetSetmoduleid %></td>
                          <td><%=modSubmodule.GetSetsubmodulename %></td>
						  <td><%=modSubmodule.GetSetsubmoduledesc %></td>
						  <td><%=modSubmodule.GetSetsubmodulestatus %></td>
						  <td><a href="#" class="btn btn-info btn-xs" onclick="openeditsubmodule('<%=modSubmodule.GetSetsubmoduleid %>');">Kemaskini</a></td>
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
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    <div class="toolbar"></div>
                    </div>
						</div>
					</div>
				</div>
			</div>
			<%-- End of submodule --%>

			<%-- screen --%>
            <div class="tab-pane" id="screen" role="tabpanel" aria-labelledby="screen-tab">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" data-target="#collapse-screen" href="#collapse-screen">
								Screen
							</a>
						</h4>
					</div>
					<div class="panel-body">
						<div id="collapse-screen" class="panel-collapse collapse in">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<a class="btn btn-app" data-toggle="modal" data-target=".modal-screen"><i class="fa fa-plus-square green"></i>Daftar Screen</a>
							</div>
							<div class="col-md-12 col-sm-12 col-xs-12">
                      <table id="datatable-screen" class="table table-striped jambo_table">
                      <thead>
                        <tr>
                          <th>ID Screen </th>
                          <th>Nama Fail Screen</th>
                          <th>Keterangan Screen</th>
                          <th>Status Screen</th>
						  <th><i class="fa fa-cog"></i></th>
                        </tr>
                      </thead>

                      <tbody>
                        <%
                            if (lsScreen.Count > 0)
                            {
                                for (int i = 0; i < lsScreen.Count; i++)
                                {
                                    MainModel modScreen = (MainModel)lsScreen[i];
                        %>       
                        <tr>
                          <td><%=modScreen.GetSetscreenid %></td>
                          <td><%=modScreen.GetSetscreenfilename %></td>
						  <td><%=modScreen.GetSetscreendesc %></td>
						  <td><%=modScreen.GetSetscreenstatus %></td>
						  <td><a href="#" class="btn btn-info btn-xs" onclick="openeditscreen('<%=modScreen.GetSetscreenid %>');">Kemaskini</a></td>
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
                            </tr>
                        <% 
                            }
                        %>
                      </tbody>
                    </table>   
                    <div class="toolbar"></div>
                    </div>
						</div>
					</div>
				</div>
			</div>
			<%-- End of screen --%>

		    
			<div style="display: none;">
                 <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                 <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                 <input type="hidden" name="hidCompId" id="hidCompId" value="<%=sCompId %>" />
                 <input type="hidden" name="hidUserAction" id="hidUserAction" value="<%=sUserAction %>" />
				 <input type="hidden" name="hidRoleid" id="hidRoleid" value="" />
                 <input type="hidden" name="hidModuleid" id="hidModuleid" value="" />
                 <input type="hidden" name="hidSubmoduleid" id="hidSubmoduleid" value="" />
                 <input type="hidden" name="hidScreenid" id="hidScreenid" value="" />
				
            </div>
			
        </div>
		<!--BEGIN dialog box for role-->
			<div class="modal fade modal-role" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog modal-lg">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
							<h4 class="modal-title">Tambah Role</h4>
						</div>
                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
							<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
								<label for="roleid">Role ID:</label>
								<input type="text" id="roleid" class="form-control"  name="roleid"  value="<%=oModRole.GetSetroleid %>" />
								<label for="rolename">Nama Role:</label>
								<input type="text" id="rolename" class="form-control"  name="rolename"  value="<%=oModRole.GetSetrolename %>" />
								<label for="roledesc">Keterangan Role:</label>
								<input type="text" id="roledesc" class="form-control"  name="roledesc"  value="<%=oModRole.GetSetroledesc %>" />
								<label for="rolestatus">Keterangan Role:</label>
								<select id="rolestatus" class="form-control"  name="rolestatus"  >
									<option value="">-Sila Pilih-</option>
									<option value="A" <%=oModRole.GetSetrolestatus.Equals("A")?"selected":"" %>>AKTIF</option>
									<option value="N" <%=oModRole.GetSetrolestatus.Equals("N")?"selected":"" %>>TIDAK AKTIF</option>
								</select>
							</div>
						</div>
						<div class="modal-footer" style="text-align:left;">
							<button type="button" class="btn btn-primary" id="btnSaveRole" onclick="actionclick('SAVE_ROLE');">Kemaskini</button>
							<button type="button" class="btn btn-danger" id="btnDelRole" onclick="actionclick('DELETE_ROLE');">Hapus</button>
							<button type="button" class="btn btn-primary" id="btnStoreRole" onclick="actionclick('STORE_ROLE');">Simpan</button>
						    <button type="button" class="btn btn-default" data-dismiss="modal";>Tutup</button>
						</div>
					</div>                                                           
				</div>
			</div>
		<!--END dialog box for role-->
        <!--BEGIN dialog box for module-->
			<div class="modal fade modal-module" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog modal-lg">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
							<h4 class="modal-title">Tambah Modul</h4>
						</div>
                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
							<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
								<label for="moduleid">Modul ID:</label>
								<input type="text" id="moduleid" class="form-control"  name="moduleid"  value="<%=oModModule.GetSetmoduleid %>" />
								<label for="modulename">Nama Modul:</label>
								<input type="text" id="modulename" class="form-control"  name="modulename"  value="<%=oModModule.GetSetmodulename %>" />
								<label for="moduledesc">Keterangan Modul:</label>
								<input type="text" id="moduledesc" class="form-control"  name="moduledesc"  value="<%=oModModule.GetSetmoduledesc %>" />
								<label for="modulestatus">Status Modul:</label>
								<select id="modulestatus" class="form-control"  name="modulestatus"  >
									<option value="">-Sila Pilih-</option>
									<option value="A" <%=oModModule.GetSetmodulestatus.Equals("A")?"selected":"" %>>AKTIF</option>
									<option value="N" <%=oModModule.GetSetmodulestatus.Equals("N")?"selected":"" %>>TIDAK AKTIF</option>
								</select>

                                <label for="moduleicon">Icon Modul:</label>
								<input type="text" id="moduleicon" class="form-control"  name="moduleicon"  value="<%=oModModule.GetSetmoduleicon %>" />
							</div>
						</div>
						<div class="modal-footer" style="text-align:left;">
							<button type="button" class="btn btn-primary" id="btnSaveModule" onclick="actionclick('SAVE_MODULE');">Kemaskini</button>
							<button type="button" class="btn btn-danger" id="btnDelModule" onclick="actionclick('DELETE_MODULE');">Hapus</button>
							<button type="button" class="btn btn-primary" id="btnStoreModule" onclick="actionclick('STORE_MODULE');">Simpan</button>
						    <button type="button" class="btn btn-default" data-dismiss="modal";>Tutup</button>
						</div>
					</div>                                                           
				</div>
			</div>
		<!--END dialog box for module-->
        <!--BEGIN dialog box for submodule-->
			<div class="modal fade modal-submodule" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog modal-lg">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
							<h4 class="modal-title">Tambah Submodul</h4>
						</div>
                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
							<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
								<label for="submoduleid">Submodul ID:</label>
								<input type="text" id="submoduleid" class="form-control"  name="submoduleid"  value="<%=oModSubmodule.GetSetsubmoduleid %>" />
                                <label for="submodule-moduleid">Modul ID:</label>
                                <select  id="submodule-moduleid" class="form-control"  name="submodule-moduleid"  >
									<option value="">-Sila Pilih-</option>
                                    <%
                                    if (lsModule.Count > 0)
                                    {
                                        for (int i = 0; i < lsModule.Count; i++)
                                        {
                                            MainModel modModule = (MainModel)lsModule[i];
                                    %>       
									<option value="<%=modModule.GetSetmoduleid %>" <%=oModSubmodule.GetSetmoduleid.Equals(modModule.GetSetmoduleid) ? "selected" : "" %>><%=modModule.GetSetmoduleid +" - "+ modModule.GetSetmodulename %></option>
                                    <% }
                                    } %>
								</select>
								<label for="submodulename">Nama Submodul:</label>
								<input type="text" id="submodulename" class="form-control"  name="submodulename"  value="<%=oModSubmodule.GetSetsubmodulename %>" />
								<label for="submoduledesc">Keterangan Submodul:</label>
								<input type="text" id="submoduledesc" class="form-control"  name="submoduledesc"  value="<%=oModSubmodule.GetSetsubmoduledesc %>" />
								<label for="modulestatus">Status Submodul:</label>
								<select id="submodulestatus" class="form-control"  name="submodulestatus"  >
									<option value="">-Sila Pilih-</option>
									<option value="A" <%=oModSubmodule.GetSetsubmodulestatus.Equals("A")?"selected":"" %>>AKTIF</option>
									<option value="N" <%=oModSubmodule.GetSetsubmodulestatus.Equals("N")?"selected":"" %>>TIDAK AKTIF</option>
								</select>
							</div>
						</div>
						<div class="modal-footer" style="text-align:left;">

							<button type="button" class="btn btn-primary" id="btnSaveSubmodule" onclick="actionclick('SAVE_SUBMODULE');">Kemaskini</button>
							<button type="button" class="btn btn-danger" id="btnDelSubmodule" onclick="actionclick('DELETE_SUBMODULE');">Hapus</button>
							<button type="button" class="btn btn-primary" id="btnStoreSubmodule" onclick="actionclick('STORE_SUBMODULE');">Simpan</button>
						    <button type="button" class="btn btn-default" data-dismiss="modal";>Tutup</button>
						</div>
					</div>                                                           
				</div>
			</div>
		<!--END dialog box for module-->
         <!--BEGIN dialog box for screen-->
			<div class="modal fade modal-screen" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog modal-lg">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
							<h4 class="modal-title">Tambah Screen</h4>
						</div>
                        <div class="modal-body col-md-12 col-sm-12 col-xs-12">
							<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
								
                                <label for="screenid">Screen ID:</label>
                                <select  id="screenid" class="form-control"  name="screenid"  >
									<option value="">-Sila Pilih-</option>
                                    <%
                                    if (lsSubmodule.Count > 0)
                                    {
                                        for (int i = 0; i < lsSubmodule.Count; i++)
                                        {
                                            MainModel modSubmodule = (MainModel)lsSubmodule[i];
                                    %>       
									<option value="<%=modSubmodule.GetSetsubmoduleid %>" <%=oModScreen.GetSetscreenid.Equals(modSubmodule.GetSetsubmoduleid) ? "selected" : "" %>><%=modSubmodule.GetSetsubmoduleid +" - "+ modSubmodule.GetSetsubmodulename %></option>
                                    <% }
                                    } %>
								</select>
								<label for="screenfilename">Nama Fail Screen:</label>
								<input type="text" id="screenfilename" class="form-control"  name="screenfilename"  value="<%=oModScreen.GetSetscreenfilename %>" />
								<label for="screendesc">Keterangan Screen:</label>
								<input type="text" id="screendesc" class="form-control"  name="screendesc"  value="<%=oModScreen.GetSetscreendesc %>" />
								<label for="screenstatus">Status Screen:</label>
								<select id="screenstatus" class="form-control"  name="screenstatus"  >
									<option value="">-Sila Pilih-</option>
									<option value="A" <%=oModScreen.GetSetscreenstatus.Equals("A")?"selected":"" %>>AKTIF</option>
									<option value="N" <%=oModScreen.GetSetscreenstatus.Equals("N")?"selected":"" %>>TIDAK AKTIF</option>
								</select>
							</div>
						</div>
						<div class="modal-footer" style="text-align:left;">

							<button type="button" class="btn btn-primary" id="btnSaveScreen" onclick="actionclick('SAVE_SCREEN');">Kemaskini</button>
							<button type="button" class="btn btn-danger" id="btnDelScreen" onclick="actionclick('DELETE_SCREEN');">Hapus</button>
							<button type="button" class="btn btn-primary" id="btnStoreScreen" onclick="actionclick('STORE_SCREEN');">Simpan</button>
						    <button type="button" class="btn btn-default" data-dismiss="modal";>Tutup</button>
						</div>
					</div>                                                           
				</div>
			</div>
		<!--END dialog box for module-->
        </form>
     </div>
	<script type="text/javascript">

        $(document).ready(function () {
            $("#btnSaveRole").hide();
            $("#btnDelRole").hide();
            $("#btnSaveModule").hide();
            $("#btnDelModule").hide();
            $("#btnSaveSubmodule").hide();
            $("#btnDelSubmodule").hide();
            $("#btnSaveScreen").hide();
            $("#btnDelScreen").hide();

            if ("<%=sAction%>" == "VIEW_ROLE") {
                $(".modal-role").modal({ backdrop: "static" });
                $('.nav-tabs a[href="#role"]').tab('show');
                $("#btnSaveRole").show();
                $("#btnDelRole").show();
                $("#btnStoreRole").hide();
            }
            else if ("<%=sAction%>" == "VIEW_MODULE") {
                $(".modal-module").modal({ backdrop: "static" });
                $('.nav-tabs a[href="#module"]').tab('show');

                $("#btnSaveModule").show();
                $("#btnDelModule").show();
                $("#btnStoreModule").hide();
            }
            else if ("<%=sAction%>" == "VIEW_SUBMODULE") {
                $(".modal-submodule").modal({ backdrop: "static" });
                $('.nav-tabs a[href="#submodule"]').tab('show');

                $("#btnSaveSubmodule").show();
                $("#btnDelSubmodule").show();
                $("#btnStoreSubmodule").hide();
            } else if ("<%=sAction%>" == "VIEW_SCREEN") {
                $(".modal-screen").modal({ backdrop: "static" });
                $('.nav-tabs a[href="#screen"]').tab('show');

                $("#btnSaveScreen").show();
                $("#btnDelScreen").show();
                $("#btnStoreScreen").hide();
            }
            else if ("<%=sAction%>" == "SAVE_SCREEN" || "<%=sAction%>" == "STORE_SCREEN" || "<%=sAction%>" == "DELETE_SCREEN") {
                $('.nav-tabs a[href="#screen"]').tab('show');

                $("#btnSaveScreen").hide();
                $("#btnDelScreen").hide();
                $("#btnStoreScreen").show();
            }
            else if ("<%=sAction%>" == "SAVE_ROLE" || "<%=sAction%>" == "STORE_ROLE" || "<%=sAction%>" == "DELETE_ROLE") {
                $('.nav-tabs a[href="#role"]').tab('show');
                $("#btnSaveRole").hide();
                $("#btnDelRole").hide();
                $("#btnStoreRole").show();
            }
            else if ("<%=sAction%>" == "SAVE_MODULE" || "<%=sAction%>" == "STORE_MODULE" || "<%=sAction%>" == "DELETE_MODULE") {
                $('.nav-tabs a[href="#module"]').tab('show');

                $("#btnSaveModule").hide();
                $("#btnDelModule").hide();
                $("#btnStoreModule").show();
            }
            else if ("<%=sAction%>" == "SAVE_SUBMODULE" || "<%=sAction%>" == "STORE_SUBMODULE" || "<%=sAction%>" == "DELETE_SUBMODULE") {
                $('.nav-tabs a[href="#submodule"]').tab('show');

                $("#btnSaveSubmodule").hide();
                $("#btnDelSubmodule").hide();
                $("#btnStoreSubmodule").show();
            }

            $('.modal-role').on('hidden.bs.modal', function () {
                $("#roleid").val("");
                $("#rolename").val("");
                $("#roledesc").val("");
                $("#rolestatus").val("");

                $("#btnStoreRole").show();
                $("#btnSaveRole").hide();
                $("#btnDelRole").hide();
            });
            $('.modal-module').on('hidden.bs.modal', function () {
                $("#moduleid").val("");
                $("#modulename").val("");
                $("#moduledesc").val("");
                $("#modulestatus").val("");
                $("#moduleicon").val("");

                $("#btnSaveModule").hide();
                $("#btnDelModule").hide();
                $("#btnStoreModule").show();
            });
            $('.modal-submodule').on('hidden.bs.modal', function () {
                $("#submoduleid").val("");
                $("#submodule-moduleid").val("");
                $("#submodulename").val("");
                $("#submoduledesc").val("");
                $("#submodulestatus").val("");

                $("#btnSaveSubmodule").hide();
                $("#btnDelSubmodule").hide();
                $("#btnStoreSubmodule").show();
            });
            $('.modal-screen').on('hidden.bs.modal', function () {
                $("#screenid").val("");
                $("#screenfilename").val("");
                $("#screendesc").val("");
                $("#screenstatus").val("");

                $("#btnSaveScreen").hide();
                $("#btnDelScreen").hide();
                $("#btnStoreScreen").show();
            });


            $('#datatable-role').DataTable();
            $('#datatable-module').DataTable();
            $('#datatable-submodule').DataTable();
            $('#datatable-screen').DataTable();
        });

        function actionclick(action) {
            if (action == 'SAVE_ROLE') {
                $('#roleid').prop('required', true);
                $('#rolename').prop('required', true);
                $('#roledesc').prop('required', true);
                $('#rolestatus').prop('required', true);
            } else if (action == 'SAVE_MODULE') {
                $('#moduleid').prop('required', true);
                $('#modulename').prop('required', true);
                $('#moduledesc').prop('required', true);
                $('#modulestatus').prop('required', true);
                $('#moduleicon').prop('required', true);
            } else if (action == 'SAVE_SUBMODULE') {
                $('#submoduleid').prop('required', true);
                $('#submodule-moduleid').prop('required', true);
                $('#submodulename').prop('required', true);
                $('#submoduledesc').prop('required', true);
                $('#submodulestatus').prop('required', true);
            } else if (action == 'SAVE_SCREEN') {
                $('#screenid').prop('required', true);
                $('#screenfilename').prop('required', true);
                $('#screendesc').prop('required', true);
                $('#screenstatus').prop('required', true);
            } else if (action == 'STORE_ROLE') {
                $('#roleid').prop('required', true);
                $('#rolename').prop('required', true);
                $('#roledesc').prop('required', true);
                $('#rolestatus').prop('required', true);
            } else if (action == 'STORE_MODULE') {
                $('#moduleid').prop('required', true);
                $('#modulename').prop('required', true);
                $('#moduledesc').prop('required', true);
                $('#modulestatus').prop('required', true);
                $('#moduleicon').prop('required', true);
            } else if (action == 'STORE_SUBMODULE') {
                $('#submoduleid').prop('required', true);
                $('#submodule-moduleid').prop('required', true);
                $('#submodulename').prop('required', true);
                $('#submoduledesc').prop('required', true);
                $('#submodulestatus').prop('required', true);
            } else if (action == 'STORE_SCREEN') {
                $('#screenid').prop('required', true);
                $('#screenfilename').prop('required', true);
                $('#screendesc').prop('required', true);
                $('#rscreenstatus').prop('required', true);
            }

            document.getElementById("hidAction").value = action;
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
		}

		function opennewrole() {
            $(".modal-role").modal({ backdrop: "static" });
        }
		function openeditrole(roleid) {
            $("#hidRoleid").val(roleid);
            actionclick("VIEW_ROLE");
        }

        function opennewmodule() {
            $(".modal-module").modal({ backdrop: "static" });
        }
        function openeditmodule(moduleid) {
            $("#hidModuleid").val(moduleid);
            actionclick("VIEW_MODULE");
        }

        function opennewsubmodule() {
            $(".modal-submodule").modal({ backdrop: "static" });
        }
        function openeditsubmodule(submoduleid) {
            $("#hidSubmoduleid").val(submoduleid);
            actionclick("VIEW_SUBMODULE");
        }

        function opennewscreen() {
            $(".modal-screen").modal({ backdrop: "static" });
        }
        function openeditscreen(screenid) {
            $("#hidScreenid").val(screenid);
            actionclick("VIEW_SCREEN");
        }

    </script>
</asp:Content>

