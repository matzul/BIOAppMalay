<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage3.master" AutoEventWireup="true" CodeFile="ExpiredPage.aspx.cs" Inherits="ExpiredPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="col-md-12 col-sm-12 col-xs-12">
          <div class="col-middle">
            <div class="text-center text-center">
              <h2>SYSTEM ERROR</h2>
              <h3  class="error-number">System Expired!</h3>
              <p>You are not allowed to access this resource.</p>
              <div class="mid_center">
                <h3><button id="btnClose" name="btnClose" type="button" class="btn btn-warning" onclick="window.close();">Close</button></h3>
              </div>
            </div>
          </div>
        </div>

</asp:Content>

