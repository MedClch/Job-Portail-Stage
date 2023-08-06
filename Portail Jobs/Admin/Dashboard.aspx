<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portail_Jobs.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Add the Chart.js library -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Admin dashboard</h2>

    <!-- Create a canvas element for the chart -->
    <canvas id="myChart" width="400" height="200"></canvas>

    <script>
        // Sample data for the chart
        var chartData = {
            labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5'],
            values: [10, 20, 15, 25, 30]
        };

        // Get the canvas element
        var ctx = document.getElementById('myChart').getContext('2d');

        // Create a bar chart
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: chartData.labels,
                datasets: [{
                    label: 'Chart Data',
                    data: chartData.values,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    </script>
</asp:Content>







<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portail_Jobs.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Admin dashboard</h2>
    <canvas id="myChart" width="400" height="200"></canvas>
    <script>
        // Sample data for the chart
        var chartData = {
            labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5'],
            values: [10, 20, 15, 25, 30]
        };

        // Get the canvas element
        var ctx = document.getElementById('myChart').getContext('2d');

        // Create a bar chart
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: chartData.labels,
                datasets: [{
                    label: 'Chart Data',
                    data: chartData.values,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    </script>
</asp:Content>--%>



<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portail_Jobs.Admin.Dashboard" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <!-- Add the Chart.js library -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Admin dashboard</h2>

    <!-- Create a canvas element for the chart -->
    <canvas id="myChart" width="400" height="200"></canvas>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Add the script to render the chart -->
    <script>
        // Sample data for the chart
        var chartData = {
            labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5'],
            values: [10, 20, 15, 25, 30]
        };

        // Get the canvas element
        var ctx = document.getElementById('myChart').getContext('2d');

        // Create a bar chart
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: chartData.labels,
                datasets: [{
                    label: 'Chart Data',
                    data: chartData.values,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    </script>
</asp:Content>--%>
