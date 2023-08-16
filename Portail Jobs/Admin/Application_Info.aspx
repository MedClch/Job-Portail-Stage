﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Application_Info.aspx.cs" Inherits="Portail_Jobs.Admin.Application_Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </div>

            <div class="input-group h-25">
                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary" Visible="true">
            <i class="fas fa-arrow-left"></i> Back
                </asp:HyperLink>
            </div>

            <h3 class="text-center">Applications Informations</h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobTitle" style="font-weight: 600">Company name</label>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblNbPost" style="font-weight: 600">Job title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>


            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobDesc" style="font-weight: 600">User name</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblQualiEdu" style="font-weight: 600">User email</label>
                    <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblSpecialization" style="font-weight: 600">Phone number</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" disabled></asp:TextBox>
                </div>

                <div class="col-md-6 pt-3">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Resume") %>'>
                    <i class="fa fa-download"></i>Download resume</asp:HyperLink>
                </div>
                <%--                <div class="col-md-6 pt-3">
                    <asp:TemplateField HeaderText="Resume">
                        <Itemtemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.Resume","../{0}") %>'>
                                <i class="fa fa-download"></i>Download resume</asp:HyperLink>
                        </Itemtemplate>
                    </asp:TemplateField>
                </div>--%>
            </div>

            <%--            <div class="row mr-lg-5 ml-lg-5 mb-3">
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
            </div>--%>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                    <div class="col-md-3 col-md-offset-2 mb-3">
                        <asp:Button ID="btnAcceptApp" runat="server" Text="Accept application" BackColor="#7200cf" CssClass="btn btn-primary btn-block" />
                    </div>
                </div>

                <%--                <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                    <div class="col-md-3 col-md-offset-2 mb-3">
                        <asp:Button ID="btnDecline" runat="server" Text="Deline application" BackColor="#7200cf" CssClass="btn btn-primary btn-block" OnClick="btnAddJob_Click" />
                    </div>
                </div>--%>

                <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                    <div class="col-md-3 col-md-offset-2 mb-3">
                        <asp:Button ID="btnContact" runat="server" Text="Contact candidate" BackColor="#7200cf" CssClass="btn btn-primary btn-block" />
                    </div>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
