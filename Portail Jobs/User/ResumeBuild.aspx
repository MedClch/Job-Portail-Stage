<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ResumeBuild.aspx.cs" Inherits="Portail_Jobs.User.ResumeBuild" %>

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
                    <h2 class="contact-title text-center">Build resume</h2>
                </div>
                <div class="col-lg-12">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h6>Personal informations</h6>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Full name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Name must be in characters !"
                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$"
                                        ControlToValidate="txtFullName"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter username" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAdress" runat="server" CssClass="form-control" placeholder="Enter adrress" TextMode="MultiLine" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Phone number</label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter phone number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Phone number must have 10 digits !"
                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$"
                                        ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
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

                            <div class="col-12 pt-4">
                                <h6>Education/Resume informations</h6>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>10th grade/percentage</label>
                                    <asp:TextBox ID="txtTenth" runat="server" CssClass="form-control" placeholder="Ex. 90%" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>12th grade/percentage</label>
                                    <asp:TextBox ID="txtTwelfth" runat="server" CssClass="form-control" placeholder="Ex. 90%" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Graduation with pointer/grade</label>
                                    <asp:TextBox ID="txtGraduation" runat="server" CssClass="form-control" placeholder="Ex. BTech with 9.2 pointer" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Post graduation with pointer/grade</label>
                                    <asp:TextBox ID="txtPostGraduation" runat="server" CssClass="form-control" placeholder="Ex. BTech with 9.2 pointer" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>PHD with percentage/grade</label>
                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" placeholder="Ex. PHD with grade" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Job profile/works on</label>
                                    <asp:TextBox ID="txtWork" runat="server" CssClass="form-control" placeholder="Enter current job profile" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Work experience</label>
                                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Enter work experience" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Resume</label>
                                    <asp:FileUpload ID="fuResume" runat="server" CssClass="form-control pt-2" required ToolTip=".doc, .docx, .pdf extensions only !"/>
                                </div>
                            </div>

                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="Udpate" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
