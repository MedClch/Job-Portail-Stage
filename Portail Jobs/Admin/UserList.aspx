<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Portail_Jobs.Admin.UserList" %>

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
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/NewUser.aspx" CssClass="btn btn-success" Visible="true"> + Add new user </asp:HyperLink>
                </div>
            </div>

            <h3 class="text-center">User list & details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">

                    <div class="row justify-content-center">
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
                        AllowPaging="True" PageSize="5" HeaderStyle-HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                        DataKeyNames="UserId" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Username" HeaderText="Username">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Mobile" HeaderText="Phone number">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Country" HeaderText="Country">
                                <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Bold="true" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <a href='<%# "NewUser.aspx?id=" + Eval("UserId") %>'>
                                        <asp:Image ID="Img" runat="server" ImageUrl="../assets/img/icon/edit.png" Height="25px" />
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

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
