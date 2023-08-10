<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Job_Info.aspx.cs" Inherits="Portail_Jobs.Admin.Job_Info" %>
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
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/JobList.aspx" CssClass="btn btn-secondary" Visible="true">< Back</asp:HyperLink>
                </div>
            </div>

            <h3 class="text-center">Job Informations</h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobTitle" style="font-weight: 600">Job title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblNbPost" style="font-weight: 600">Number of posts</label>
                    <asp:TextBox ID="txtNbPost" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblJobDesc" style="font-weight: 600">Job description</label>
                    <asp:TextBox ID="txtJobDesc" runat="server" CssClass="form-control" TextMode="MultiLine" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblQualiEdu" style="font-weight: 600">Qualification/Education required</label>
                    <asp:TextBox ID="txtQualiEdu" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblExperience" style="font-weight: 600">Experience required</label>
                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSpecialization" style="font-weight: 600">Specialization required</label>
                    <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" TextMode="MultiLine" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblLastDate" style="font-weight: 600">Last date to apply</label>
                    <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" TextMode="Date" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSalary" style="font-weight: 600">Salary</label>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblJobType" style="font-weight: 600">Job type</label>
                    <asp:TextBox ID="txtJobType" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblCompanyName" style="font-weight: 600">Company/Organisation name</label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblCompanyLogo" style="font-weight: 600">Company/Organisation logo</label>
                    <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" disabled/>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblWebsite" style="font-weight: 600">Website</label>
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" TextMode="Url" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblEmail" style="font-weight: 600">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblAddress" style="font-weight: 600">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblCountry" style="font-weight: 600">Country</label>
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblState" style="font-weight: 600">State</label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

<%--            <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAddJob" runat="server" Text="Add job" BackColor="#7200cf" CssClass="btn btn-primary btn-block" OnClick="btnAddJob_Click" />
                </div>
            </div>--%>

        </div>
    </div>

</asp:Content>
