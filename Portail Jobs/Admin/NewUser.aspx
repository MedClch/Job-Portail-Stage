<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="Portail_Jobs.Admin.NewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/JobList.aspx" CssClass="btn btn-secondary" Visible="false">< Back</asp:HyperLink>
                </div>
            </div>

            <h3 class="text-center"><% Response.Write(Session["title"]); %></h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobTitle" style="font-weight: 600">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username" required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblNbPost" style="font-weight: 600">Full name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblQualiEdu" style="font-weight: 600">Password</label>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" placeholder="Enter password" TextMode="Password" required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblExperience" style="font-weight: 600">Confirm password</label>
                    <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="form-control" placeholder="Confirm password" TextMode="Password" required></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and confirm password should match !"
                        ControlToCompare="txtPass" ControlToValidate="txtConfirmPass" ForeColor="Red" Display="Dynamic"
                        SetFocusOnError="true" Font-Size="Small"></asp:CompareValidator>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblJobDesc" style="font-weight: 600">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter adrress" TextMode="MultiLine" required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSpecialization" style="font-weight: 600">Phone number</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter phone number" TextMode="Phone" required></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Phone number must have 10 digits !"
                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$"
                        ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblLastDate" style="font-weight: 600">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" required TextMode="Email"></asp:TextBox>
                </div>
            </div>


            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblCountry" style="font-weight: 600">Country</label>
                    <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-contact w-100"
                        AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                        <asp:ListItem Value="0">Select country</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Country is requiered !"
                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAddUser" runat="server" Text="Add user" BackColor="#7200cf" CssClass="btn btn-primary btn-block" OnClick="btnAddUser_Click" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
