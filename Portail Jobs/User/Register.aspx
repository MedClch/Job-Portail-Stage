<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Portail_Jobs.User.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center">Sign Up</h2>
                </div>
                <div class="col-lg-6 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h6>Login Informations</h6>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter username" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter password " TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label>Confirm password</label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Confirm password" TextMode="Password" required></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and confirm password should match !"
                                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic"
                                        SetFocusOnError="true" Font-Size="Small"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="col-12">
                                <h6>Personal Informations</h6>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Full name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name must be in characters !"
                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$"
                                        ControlToValidate="txtFullName"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Adress</label>
                                    <asp:TextBox ID="txtAdress" runat="server" CssClass="form-control" placeholder="Enter adrress" TextMode="MultiLine" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Mobile number</label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter mobile number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Mobile number must have 10 digits !"
                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$"
                                        ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Country</label>

                                    <%--<div style="height: 150px; overflow-y: auto;">
                                        <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-contact w-100"
                                            AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName" size="10">
                                            <asp:ListItem Value="0">Select country</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>

                                    <asp:DropDownList ID="ddlCountry" runat="server" DataSourceID="SqlDataSource1" CssClass="form-contact w-100"
                                        AppendDataBoundItems="true" DataTextField="CountryName" DataValueField="CountryName">
                                        <asp:ListItem Value="0">Select country</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Country is requiered !"
                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="SELECT [CountryName] FROM [Country]"></asp:SqlDataSource>
                                </div>
                            </div>

                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnRegister_Click" />
                            <span class="clickLink"><a href="../User/Login.aspx">Already registered ? Click here</a></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
