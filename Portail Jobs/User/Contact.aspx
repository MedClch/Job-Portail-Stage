<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Portail_Jobs.User.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
    <style>
        #map {
            height: 500px;
        }
    </style>
    <div class="slider-area ">
        <div class="single-slider section-overly slider-height2 d-flex align-items-center" data-background="../assets/img/hero/about.jpg">
            <div class="container">
                <div class="row">
                    <div class="col-xl-12">
                        <div class="hero-cap text-center">
                            <h2>Contact us</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section class="contact-section">
        <div class="container">
            <div class="d-none d-sm-block mb-5 pb-4">
                <div class="col-12">
                    <h2 class="contact-title">Our location </h2>
                </div>
                <div id="map"></div>

                <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
                <script>
                    function initMap() {
                        var grayStyles = {
                            "saturation": -90,
                            "lightness": 50
                        };
                        var map = L.map('map').setView([33.995581, -6.718662], 15);
                        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { style: grayStyles }).addTo(map);
                        var marker = L.marker([33.995581, -6.718662]).addTo(map);
                        marker.bindPopup("Our Location").openPopup();
                    }
                </script>
                <script>
                    initMap();
                </script>
            </div>

            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title">Get in Touch</h2>
                </div>
                <div class="col-lg-8">
                    <%--                        <form class="form-contact contact_form" action="contact_process.php" method="post" id="contactForm" novalidate="novalidate">--%>
                    <div class="form-contact contact_form" id="contactForm">
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <textarea class="form-control w-100" runat="server" name="message" id="message" cols="30" rows="9" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Enter Message'" placeholder=" Enter Message" required></textarea>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <input class="form-control valid" runat="server" name="name" id="name" type="text" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Enter your name'" placeholder="Enter your name" required>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <input class="form-control valid" runat="server" name="email" id="email" type="email" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Enter email address'" placeholder="Email" required>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <input class="form-control" runat="server" name="subject" id="subject" type="text" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Enter Subject'" placeholder="Enter Subject" required>
                                </div>
                            </div>
                        </div>
                        <div class="form-group mt-3">
                            <%--<button type="submit" class="button button-contactForm boxed-btn">Send</button>--%>
                            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button button-contactForm boxed-btn" OnClick="btnSend_Click" />
                        </div>
                        <%--                        </form>--%>
                    </div>
                </div>
                <div class="col-lg-3 offset-lg-1">
                    <div class="media contact-info">
                        <span class="contact-info__icon"><i class="ti-home"></i></span>
                        <div class="media-body">
                            <a href="https://www.google.com/maps/place/DXC+Technology+Maroc/@33.995566,-6.7212323,17z/data=!3m1!4b1!4m6!3m5!1s0xda7404317f483cd:0x52507c33095c743!8m2!3d33.9955616!4d-6.7186574!16s%2Fg%2F1hdzlh5_p?entry=ttu" target="_blank">
                                <h3>Rabat, Morrocco.</h3>
                                <p>10 000</p>
                            </a>
                        </div>
                    </div>
                    <div class="media contact-info">
                        <span class="contact-info__icon"><i class="ti-tablet"></i></span>
                        <div class="media-body">
                            <%--<h3>+212 6 20 30 99 16</h3>--%>
                            <h3><a href="https://wa.me/212620309916" target="_blank">Phone : +212 6 20 30 99 16</a></h3>
                            <p>Monday to Friday 9am to 6pm</p>
                        </div>
                    </div>
                    <div class="media contact-info">
                        <span class="contact-info__icon"><i class="ti-email"></i></span>
                        <div class="media-body">
                            <h3><a href="mailto:chlouchi.med4@gmail.com" target="_blank">chlouchi.med4@gmail.com</a></h3>
                            <p>Send us your query anytime!</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
