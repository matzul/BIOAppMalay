<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="UserAccessibility.aspx.cs" Inherits="UserAccessibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<link href="css/hummingbird-treeview.css" rel="stylesheet" type="text/css" />
	<script src="js/WebMethodService.Call.Helper.js"></script>
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
            <div class="form-group row">
                <label for="compid" class="col-sm-2 col-form-label">Kod Syarikat</label>
                <div class="col-sm-4">
                  <input type="text" readonly  class="form-control" id="compid" name="compid" placeholder="Id Masjid" value="<%=oModComp.GetSetcomp %>" />
                </div>
            </div>
            <div class="form-group row">
                <label for="compdesc" class="col-sm-2 col-form-label">Nama Syarikat</label>
                <div class="col-sm-4">
                  <input type="text" readonly class="form-control" id="compdesc" placeholder="Nama Masjid" value="<%=oModComp.GetSetcomp_name  %>" />
                </div>
            </div>
            <br/>
            <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
			  
              <li class="nav-item active">
				<a class="nav-link" id="roleaccess-tab" data-toggle="tab" href="#roleaccess" role="tab" aria-controls="roleaccess"
				  aria-selected="false">Role Accessibility</a>
			  </li>
			</ul>
			<form id="form1" runat="server">
			<div class="tab-content" id="myTabContent1">
			<%-- Role Accessibility --%>
            <div class="tab-pane active" id="roleaccess" role="tabpanel" aria-labelledby="roleaccess-tab">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" data-target="#collapse-roleaccess" href="#collapse-roleaccess">
								Role Accessibility
							</a>
						</h4>
					</div>
					<div id="collapse-roleaccess" class="panel-collapse collapse in">
						<div class="col-md-6 col-sm-6 col-xs-12">
						   <label for="userrole">User Role:</label>
						   <select class="form-control" id="userrole" name="userrole" required="required">
							   <option value="">-select-</option>
							   <%
                                  for (int i = 0; i < lsRole.Count; i++)
                                  {
									 MainModel oModRole = (MainModel)lsRole[i];
								%>
									 <option value="<%=oModRole.GetSetroleid %>"><%=oModRole.GetSetrolename %> - <%=oModRole.GetSetroledesc %></option>
                                <%
                                  }
                                %>
						   </select>
					   </div>
					   <br/><br/><br/><br />
					   <div class="çol-md-12">
						<div id="treeview_container" class="hummingbird-treeview">
							<ul id="treeview" class="hummingbird-base"></ul>
						</div>
					   </div>
					   <a class="btn btn-responsive btn-block btn-primary" id="getItems" style="width:100px; margin-left:30px;">Simpan</a>
					</div>
				</div>
			</div>
			<%-- End of Role Accessibility --%>

		    
			<div style="display: none;">
                 <asp:Button ID="btnAction" runat="server" Text="Action" OnClick="btnAction_Click" />
                 <input type="hidden" name="hidAction" id="hidAction" value="<%=sAction %>" />
                 <input type="hidden" name="hidCompId" id="hidCompId" value="<%=sCompId %>" />
                 <input type="hidden" name="hidUserAction" id="hidUserAction" value="<%=sUserAction %>" />
            </div>
			
        </div>
        </form>
     </div>
    </div>
	<script src="js/hummingbird-treeview.js"></script>

	<script type="text/javascript"> 
		var moduleoutput = "";
		var submoduleoutput = "";
		var currcomp = getParameterByName("compid");
		 
		$("#treeview").hummingbird();

		$(document).ready(function () {
            var parameters_getModule = [];
			PageMethod("getModule", parameters_getModule, succeededAjaxFn_getModule, failedAjaxFn_getModule, false);
		});

        //BEGIN: getMobile_YearMonthList
        succeededAjaxFn_getModule = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getModule: " + textStatus);

            result_getModule = JSON.parse(data.d);
            //updyear = result_getModule.GetSetmodulename;

			$.each(result_getModule, function (i, result) {
				//output += result.GetSetmoduleid + " :: " + result.GetSetmodulename + " :: " + i + "\n";
                moduleoutput += "<li><i class='fa fa-plus'></i> <label> <input id='" + result.GetSetmoduleid + "' data-id='custom-0' type='checkbox'>" + result.GetSetmoduledesc +"</label>";

				var parameters_getSubModule = ["moduleid", result.GetSetmoduleid];
				PageMethod("getSubModule", parameters_getSubModule, succeededAjaxFn_getSubModule, failedAjaxFn_getSubModule, false);

				moduleoutput += submoduleoutput;
			});

			moduleoutput += "</li>";

            $(".hummingbird-base").html("").append(moduleoutput);

        }

        failedAjaxFn_getModule = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getModule: " + textStatus);
		}

        //BEGIN: getMobile_YearMonthList
        succeededAjaxFn_getSubModule = function (data, textStatus, jqXHR) {
            //console.log("succeededAjaxFn_getSubModule: " + textStatus);

            result_getSubModule = JSON.parse(data.d);
            //updyear = result_getModule.GetSetmodulename;
            submoduleoutput = "<ul style='display: none;'>";
            $.each(result_getSubModule, function (i, result) {
                //output += result.GetSetmoduleid + " :: " + result.GetSetmodulename + " :: " + i + "\n";
				//console.log(result.GetSetmoduleid);
				submoduleoutput += "<li><label><input class='hummingbird-end-node' id='" + result.GetSetsubmoduleid + "' data-id='" + result.GetSetmoduleid + "' type='checkbox'>" + result.GetSetsubmoduledesc + "</label></li>";
			});
            submoduleoutput += "</ul>";
        }

        failedAjaxFn_getSubModule = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getSubModule: " + textStatus);
        }

        $.getScript('js/hummingbird-treeview.js').done(function () {
            $(document).ready(function () {
                //options
                $.fn.hummingbird.defaults.checkDisabled = true;
                //initialisation
                $("#treeview").hummingbird();
                //methods
               // $("#treeview").hummingbird("checkAll");
            });
		});

        //BEGIN: succeededAjaxFn_getUserRoleSubModule
        succeededAjaxFn_getUserRoleSubModule = function (data, textStatus, jqXHR) {
            console.log("succeededAjaxFn_getUserRoleSubModule: " + textStatus);
			var str = "";
			result_getUserRoleSubModule = JSON.parse(data.d);
            $("#treeview").hummingbird("uncheckAll");
            //alert(result_getUserRoleSubModule.length);
			//console.log(result_getModule);
            //$("#treeview").hummingbird("checkNode", { attr: "id", name: result_getModule.GetSetsubmoduleid, expandParents: false });
			$.each(result_getUserRoleSubModule, function (i, result) {
                $("#treeview").hummingbird("checkNode", { attr: "id", name: "'"+result.GetSetsubmoduleid+"'", expandParents: false });
                //console.log("itik: " + result.GetSetsubmoduleid + "'");
			});

            //$("#treeview").hummingbird("checkNode", { attr: "id", name: "300030", expandParents: false });
            //updyear = result_getModule.GetSetmodulename;
        }

        failedAjaxFn_getUserRoleSubModule = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_getUserRoleSubModule: " + textStatus);
		}

		$("#getItems").on("click", function (e) {

            //measure_start();
			var List = { 'id': [], 'dataid': [], 'text': [] };
            $("#treeview").hummingbird("getChecked", { list: List, onlyEndNodes: true });
			//alert(currcomp + " :: " + List);
			console.log(List.dataid.join(","));
            console.log(List.id.join(","));
			console.log(List.text.join(","));
			var output = List.dataid.join(",") + "|" + List.id.join(",") + "|" + List.text.join(",");

			console.log(output);
            //var myJSON = JSON.stringify({ list: List });
			var parameters_updateUserRoleSubModule = ["comp", currcomp, "roleid", $("#userrole").val(), "list", output];
            PageMethod("updateUserRoleSubModule", parameters_updateUserRoleSubModule, succeededAjaxFn_updateUserRoleSubModule, failedAjaxFn_updateUserRoleSubModule, false);

			//$("#displayItems").html("").val(List.text.join(",") + ":" + List.dataid.join(",") + ":" + List.id.join(","));

		});

        //BEGIN: updateUserRoleSubModule
        succeededAjaxFn_updateUserRoleSubModule = function (data, textStatus, jqXHR) {
			console.log("succeededAjaxFn_updateUserRoleSubModule: " + textStatus);
			alert("Successfully update user accessibility!");
        }

        failedAjaxFn_updateUserRoleSubModule = function (jqXHR, textStatus, errorThrown) {
            console.log("failedAjaxFn_updateUserRoleSubModule: " + textStatus);
        }

		function actionclick(action) {

            if (action == 'ADD') {
                $('#compCode').removeAttr('required');
				$('#compId').removeAttr('required');
				//alert("ayam");
            } 
			document.getElementById("hidAction").value = action;
		//document.getElementById("hidComp").value = currcomp;
			console.log(currcomp);
            var button = document.getElementById("<%=btnAction.ClientID %>");
            button.click();
		}

		$("#userrole").change(function () {
			//alert($(this).val());
			//$("#treeview").hummingbird("checkNode", { attr: "id", name: "020010", expandParents: false });
            var parameters_getUserRoleSubModule = ["comp", currcomp, "roleid", $(this).val()];
			PageMethod("getUserRoleSubModule", parameters_getUserRoleSubModule, succeededAjaxFn_getUserRoleSubModule, failedAjaxFn_getUserRoleSubModule, false);

		});

        function getParameterByName(name) {
            name = name.replace(/[[]/, "\[").replace(/[]]/, "\]");
            var regex = new RegExp("[\?&]" + name + "=([^&#]*)"), results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
</asp:Content>
