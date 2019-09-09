<%@ Page Title="" Language="C#" MasterPageFile="./MasterPage.master" AutoEventWireup="true" CodeFile="NotAuthorizedPage.aspx.cs" Inherits="NotAuthorizedPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="col-md-12">
          <div class="col-middle">
            <div class="text-center text-center">
              <h2>SYSTEM ERROR</h2>
              <h1  class="error-number">Access denied!</h1>
              <p>You are not allowed to access this resource.</p>
              <div class="mid_center">
                <h3>Please consult with your system admin</h3>
              </div>
            </div>
          </div>
        </div>

</asp:Content>

