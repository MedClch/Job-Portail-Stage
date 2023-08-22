<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewResume.aspx.cs" Inherits="Portail_Jobs.Admin.ViewResume" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/JobApplicationsHistory.aspx" CssClass="btn btn-success" Visible="true"> View applications history </asp:HyperLink>
                </div>
            </div>

            <div class="input-group h-25">
                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary"
                    Visible="false"> < Back </asp:HyperLink>
            </div>
            <h3 class="text-center">View/Download resumes</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No informations to display !" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="AppliedJobId" OnRowDeleting="GridView1_RowDeleting"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>

                            <%--                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>--%>

                            <asp:BoundField DataField="AppliedJobId" HeaderText="Sr.No" Visible="false" />
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkAppliedJobId" ForeColor="Black" Font-Bold="true" runat="server" NavigateUrl='<%# "Application_Info.aspx?id=" + Eval("AppliedJobId") %>' Text='<%# Eval("AppliedJobId") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Company name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCompanyName" ForeColor="Black" Font-Bold="true" runat="server" Text='<%# Eval("CompanyName") %>' NavigateUrl='<%# "JobList.aspx?id=" + Eval("JobId") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job title">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkJobTitle" ForeColor="Black" Font-Bold="true" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# "JobList.aspx?id=" + Eval("JobId") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Username">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkUser" ForeColor="Black" Font-Bold="true" runat="server" Text='<%# Eval("Username") %>' NavigateUrl='<%# "UserList.aspx?id=" + Eval("UserId") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"/>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Name" HeaderText="Full name">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Email" HeaderText="User email">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Mobile" HeaderText="Phone number">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Resume">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.Resume","../{0}") %>'>
                                        <i class="fa fa-download"></i><b>Download</b></asp:HyperLink>
                                    <%--<asp:HiddenField ID="hdnJobId" runat="server" Value='<%# Eval("JobId") %>' Visible="false" />--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Accept application">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnAccept" runat="server" CommandName="Accept" CommandArgument='<%# Eval("AppliedJobId") %>' ImageUrl="../assets/img/icon/yes1.png" Height="35px" Width="35px" OnClick="btnAccept_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Reject application">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="../assets/img/icon/no1.png" Height="35px" Width="35px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
