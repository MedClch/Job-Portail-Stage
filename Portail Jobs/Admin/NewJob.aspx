<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NewJob.aspx.cs" Inherits="Portail_Jobs.Admin.NewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h3 class="text-center">Add new job</h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobTitle" style="font-weight: 600">Job title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" placeholder="Ex. Web Developer ..." required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblNbPost" style="font-weight: 600">Number of posts</label>
                    <asp:TextBox ID="txtNbPost" runat="server" CssClass="form-control" placeholder="Enter number of posts" TextMode="Number" required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblJobDesc" style="font-weight: 600">Job description</label>
                    <asp:TextBox ID="txtJobDesc" runat="server" CssClass="form-control" placeholder="Enter job description" TextMode="MultiLine" required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblQualiEdu" style="font-weight: 600">Qualification/Education required</label>
                    <asp:TextBox ID="txtlblQualiEdu" runat="server" CssClass="form-control" placeholder="Ex. MCA, MBA ..." required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblExperience" style="font-weight: 600">Experience required</label>
                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex. 2 years, 1.5 years ..." required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSpecialization" style="font-weight: 600">Specialization required</label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Enter specialization" required TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblLastDate" style="font-weight: 600">Last date to apply</label>
                    <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" placeholder="Enter the last date to apply" required TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSalary" style="font-weight: 600">Salary</label>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" placeholder="Ex. 25000/Month" required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblJobType" style="font-weight: 600">Job type</label>
                    <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Select job type</asp:ListItem>
                        <asp:ListItem>Full time</asp:ListItem>
                        <asp:ListItem>Part time</asp:ListItem>
                        <asp:ListItem>Remote</asp:ListItem>
                        <asp:ListItem>Freelance</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Job type is required !" ForeColor="Red" ControlToValidate="ddlJobType" 
                        InitialValue="0" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblCompanyName" style="font-weight: 600">Company/Organisation name</label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Ex. Company/Organisation name" required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblCompanyLogo" style="font-weight: 600">Company/Organisation logo</label>
                    <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" ToolTip=".jpg, .jpeg, .png extensions only !"/>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblWebsite" style="font-weight: 600">Website</label>
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Enter website" required TextMode="Url"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblEmail" style="font-weight: 600">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" required TextMode="Email"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblAddress" style="font-weight: 600">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter job location" TextMode="MultiLine" required></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
