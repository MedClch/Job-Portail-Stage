<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="JobApplicationsHistory.aspx.cs" Inherits="Portail_Jobs.Admin.JobApplicationsHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">

            <div class="input-group h-25">
                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary"
                    Visible="true"> < Back </asp:HyperLink>
            </div>

            <h3 class="text-center">Job applications history</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" placeholder="Enter keyword to filter"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" CssClass="btn btn-primary btn-block" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary btn-block" />
                        </div>
                    </div>
                    <hr />
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No informations to display !" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="10" DataKeyNames="AppliedJobId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CompanyName" HeaderText="Company name">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Title" HeaderText="Job title">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="User name">
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
                                    <asp:HiddenField ID="hdnJobId" runat="server" Value='<%# Eval("JobId") %>' Visible="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Response" HeaderText="Application response">
                                <ItemStyle HorizontalAlign="Center" ForeColor="White" Font-Bold="true" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
