<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="Portail_Jobs.Admin.JobList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <%--            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>--%>

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/NewJob.aspx" CssClass="btn btn-success" Visible="true"> + Add new job </asp:HyperLink>
                </div>
            </div>

            <div class="input-group h-25">
                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary"
                    Visible="false"> < Back </asp:HyperLink>
            </div>

            <h3 class="text-center">Job list & details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No informations to display !" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="JobId" OnRowDeleting="GridView1_RowDeleting"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Job title">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkJobTitle" ForeColor="Black" runat="server" NavigateUrl='<%# "Job_Info.aspx?id=" + Eval("JobId") %>' Text='<%# Eval("Title") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="NoOfPost" HeaderText="Number of posts">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Qualification" HeaderText="Qualifications">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Experience" HeaderText="Experience">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="LastDateToApply" HeaderText="Valid until" DataFormatString="{0:dd MMMM yyyy}">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CompanyName" HeaderText="Company">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Country" HeaderText="Country">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="State" HeaderText="State">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CreateDate" HeaderText="Post date" DataFormatString="{0:dd MMMM yyyy}">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <a href='<%# "NewJob.aspx?id=" + Eval("JobId") %>'>
                                        <asp:Image ID="Img" runat="server" ImageUrl="../assets/img/icon/edit.png" Height="25px" />
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <%--                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditJob" runat="server" CommandName="EditJob" CommandArgument='<%# Eval("JobId") %>'>
                                        <asp:Image ID="Img" runat="server" ImageUrl="../assets/img/icon/edit.png" Height="25px" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>--%>

                            <asp:CommandField CausesValidation="false" HeaderText="Delete" ShowDeleteButton="true" DeleteImageUrl="../assets/img/icon/delete.png" ButtonType="Image">
                                <ControlStyle Height="25px" Width="25px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
