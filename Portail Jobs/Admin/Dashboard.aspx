<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portail_Jobs.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Add the Chart.js library -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.12.1/css/all.min.css" />

    <style>
        .card {
            background-color: #fff;
            border-radius: 10px;
            border: none;
            position: relative;
            margin-bottom: 30px;
            box-shadow: 0 0.46875rem 2.1875rem rgba(90,97,105,0.1), 0 0.9375rem 1.40625rem rgba(90,97,105,0.1), 0 0.25rem 0.53125rem rgba(90,97,105,0.12), 0 0.125rem 0.1875rem rgba(90,97,105,0.1);
        }

        .l-bg-cherry {
            background: linear-gradient(to right, #493240, #f09) !important;
            color: #fff;
        }

        .l-bg-blue-dark {
            background: linear-gradient(to right, #373b44, #4286f4) !important;
            color: #fff;
        }

        .l-bg-green-dark {
            background: linear-gradient(to right, #0a504a, #38ef7d) !important;
            color: #fff;
        }

        .l-bg-orange-dark {
            background: linear-gradient(to right, #a86008, #ffba56) !important;
            color: #fff;
        }

        .card .card-statistic-3 .card-icon-large .fas, .card .card-statistic-3 .card-icon-large .far, .card .card-statistic-3 .card-icon-large .fab, .card .card-statistic-3 .card-icon-large .fal {
            font-size: 110px;
        }

        .card .card-statistic-3 .card-icon {
            text-align: center;
            line-height: 50px;
            margin-left: 15px;
            color: #000;
            position: absolute;
            right: -5px;
            top: 20px;
            opacity: 0.1;
        }

        .l-bg-cyan {
            background: linear-gradient(135deg, #289cf5, #84c0ec) !important;
            color: #fff;
        }

        .l-bg-green {
            background: linear-gradient(135deg, #23bdb8 0%, #43e794 100%) !important;
            color: #fff;
        }

        .l-bg-orange {
            background: linear-gradient(to right, #f9900e, #ffba56) !important;
            color: #fff;
        }

        .l-bg-cyan {
            background: linear-gradient(135deg, #289cf5, #84c0ec) !important;
            color: #fff;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container pt-4">
        <div class="row">
            <div class="col-12 pb-3">
                <h2 class="text-center">Dashboard</h2>
            </div>

            <div class="col-md-10 mx-auto">
                <div class="row">

                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-cherry">
                        <div class="card-statistic-3 pt-4">
                            <div class="card-icon card-icon-large">
                                <i class="fas fa-users pr-2"></i>
                            </div>
                            <div class="mb-4 text-center">
                                <h5 class="card-title mb-0">Total users</h5>
                            </div>
                            <div class="row-align-items-center mb-2 d-flex align-items-center justify-content-center">
                                <div class="col-8 text-center">
                                    <h2 class="mb-0">
                                        <% Response.Write(Session["Users"]); %>
                                    </h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    
                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-orange ">
                        <div class="card-statistic-3 pt-4">
                            <div class="card-icon card-icon-large text-center">
                                <i class="fas fa-briefcase pr-2"></i>
                            </div>
                            <div class="mb-4 text-center">
                                <h5 class="card-title mb-0">Total jobs</h5>
                            </div>
                            <div class="row-align-items-center mb-2 d-flex align-items-center justify-content-center">
                                <div class="col-8 text-center">
                                    <h2 class="mb-0">
                                        <% Response.Write(Session["Jobs"]); %>
                                    </h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    
                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-green">
                        <div class="card-statistic-3 pt-4">
                            <div class="card-icon card-icon-large text-center">
                                <i class="fas fa-check-square pr-2"></i>
                            </div>
                            <div class="mb-4 text-center">
                                <h5 class="card-title mb-0">Total job applications</h5>
                            </div>
                            <div class="row-align-items-center mb-2 d-flex align-items-center justify-content-center">
                                <div class="col-8 text-center">
                                    <h2 class="mb-0">
                                        <% Response.Write(Session["Applications"]); %>
                                    </h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                    
                <div class="col-xl-3 col-lg-6">
                    <div class="card l-bg-warning ">
                        <div class="card-statistic-3 pt-4">
                            <div class="card-icon card-icon-large text-center">
                                <i class="fas fa-comments pr-2"></i>
                            </div>
                            <div class="mb-4 text-center">
                                <h5 class="card-title mb-0">Total contacts</h5>
                            </div>
                            <div class="row-align-items-center mb-2 d-flex align-items-center justify-content-center">
                                <div class="col-8 text-center">
                                    <h2 class="mb-0">
                                        <% Response.Write(Session["Contact"]); %>
                                    </h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                </div>
            </div>
        </div>

        <div class="col-md-10 mx-auto mt-5">
            <div class="row">
                <div class="col">
                    <canvas id="myChart" width="400" height="200"></canvas>
                </div>
            </div>
        </div>

    </div>

    <!-- Create a canvas element for the chart -->
<%--    <canvas id="myChart" width="400" height="200"></canvas>--%>

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
