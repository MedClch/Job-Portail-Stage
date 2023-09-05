<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ContactResponse.aspx.cs" Inherits="Portail_Jobs.Admin.ContactResponse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">
<%--            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>--%>

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ContactList.aspx" CssClass="btn btn-secondary" Visible="false">< Back</asp:HyperLink>
                </div>
            </div>

            <h3 class="text-center">Message Reply</h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblJobTitle" style="font-weight: 600">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Font-Bold="true" ForeColor="Black" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblNbPost" style="font-weight: 600">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Font-Bold="true" ForeColor="Black" TextMode="Email" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="lblQualiEdu" style="font-weight: 600">Subject</label>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Font-Bold="true" ForeColor="Black" disabled></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="lblExperience" style="font-weight: 600">Message</label>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" Font-Bold="true" ForeColor="Black" disabled></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="lblJobDesc" style="font-weight: 600">Reply</label>
                    <asp:TextBox ID="txtReply" runat="server" CssClass="form-control" placeholder="Enter job description" TextMode="MultiLine" required></asp:TextBox>
                </div>
            </div>


            <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnSend" runat="server" Text="Send" BackColor="#7200cf" CssClass="btn btn-primary btn-block" OnClick="btnSend_Click" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
