<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewResume.aspx.cs" Inherits="Portail_Jobs.Admin.ViewResume" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h3 class="text-center">View/Download resumes</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No informations to display !" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="AppliedJobId" OnRowDeleting="GridView1_RowDeleting"
                        OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CompanyName" HeaderText="Company name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Title" HeaderText="Job title">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="User name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Email" HeaderText="User email">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Mobile" HeaderText="Phone number">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Resume">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.Resume","../{0}") %>'>
                                        <i class="fa fa-download"></i>Download</asp:HyperLink>
                                    <asp:HiddenField ID="hdnJobId" runat="server" Value='<%# Eval("JobId") %>' Visible="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Application response">
                                <ItemTemplate>
                                    <asp:Button ID="btnAccept" runat="server" CommandName="Accept" Text="Accept" ForeColor="Green" Font-Bold="true"/>
                                    <asp:Button ID="btnDecline" runat="server" CommandName="Decline" Text="Decline" ForeColor="Red" Font-Bold="true"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

<%--                            <asp:TemplateField HeaderText="Decline applicant">
                                <ItemTemplate>
                                    <asp:Button ID="btnDecline" runat="server" CommandName="Decline"  Text="Decline" BackColor="Red" ForeColor="White"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="../assets/img/icon/delete.png" Height="25px" Width="25px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--                            <asp:CommandField CausesValidation="false" HeaderText="Delete" ShowDeleteButton="true" DeleteImageUrl="../assets/img/icon/delete.png" ButtonType="Image">
                                <ControlStyle Height="25px" Width="25px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>--%>
                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
