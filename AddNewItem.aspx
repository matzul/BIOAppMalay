<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage2.master" AutoEventWireup="true" CodeFile="AddNewItem.aspx.cs" Inherits="AddNewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Item Master ADD NEW</h2>
                    <ul class="nav navbar-right panel_toolbox">
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                  <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <form id="search-form1">
                                    <label for="itemcode1">Item Code:</label>
                                    <input type="text" id="itemcode1" class="form-control" name="itemcode1" />
                                    <label for="itemdesc1">Item Description:</label>
                                    <input type="text" id="itemdesc1" class="form-control" name="itemdesc1" />
                                </form>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                                <form id="search-form2">
                                    <label for="itemcode2">Item Code:</label>
                                    <input type="text" id="itemcode2" class="form-control" name="itemcode2" />
                                    <label for="itemdesc">Item Description:</label>
                                    <input type="text" id="itemdesc2" class="form-control" name="itemdesc2" />
                                </form>
                  </div>

                  <div class="col-md-12 col-sm-12 col-xs-12">
                        <section class="panel">
                            <div class="panel-body">
                                <form id="action-form">
                                    <button type="submit" class="btn btn-primary">Search</button>
                                    <button type="reset" class="btn btn-warning">Reset</button>
                                    <button type="button" class="btn btn-default" onclick="enabledisablesearchbox();">Cancel</button>
                                </form>
                            </div>
                        </section>
                  </div> 
                                     
                  <div class="col-md-12 col-sm-12 col-xs-12">
                    <a class="btn btn-app" onclick="openaddnewitem();">
                      <i class="fa fa-plus-square green"></i> Add
                    </a>
                    <a class="btn btn-app" onclick="enabledisablesearchbox();">
                      <i class="fa fa-search"></i> Search
                    </a>
                      <div class="table-responsive">
                        <table class="table table-striped jambo_table">
                          <thead>
                            <tr>
                              <th>Name</th>
                              <th>Position</th>
                              <th>Office</th>
                              <th>Age</th>
                              <th>Start date</th>
                              <th>Salary</th>
                            </tr>
                          </thead>

                          <tbody>
                            <tr>
                              <td>Tiger Nixon</td>
                              <td>System Architect</td>
                              <td>Edinburgh</td>
                              <td>61</td>
                              <td>2011/04/25</td>
                              <td>$320,800</td>
                            </tr>
                          </tbody>
                        </table>   
                      </div>
                  </div>
                </div>
            </div>

</asp:Content>

