using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class ContactResponse : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
                Response.Redirect("../User/Default.aspx");
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
            if (Session["admin"]==null && Session["user"]==null)
                Response.Redirect("../User/Login.aspx");
            if (!IsPostBack)
                fillData();
        }

        private void fillData()
        {
            if (Request.QueryString["id"]!=null)
            {
                conn = new SqlConnection(str);
                query = "Select * from Contact where ContactId = '"+Request.QueryString["id"]+"'";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtUsername.Text = reader["Name"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtSubject.Text = reader["Subject"].ToString();
                        txtMessage.Text = reader["Message"].ToString();
                        linkBack.Visible=true;
                    }
                }
                else
                {
                    lblMsg.Text="Contact not found !";
                    lblMsg.CssClass="alert alert-danger";
                }
                reader.Close();
                conn.Close();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Create a SendinBlue API request
            var apiKey = "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 "; // Replace with your SendinBlue API key
            var apiUrl = "https://api.brevo.com/v3/emailCampaigns";

            var sendinBlueRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            sendinBlueRequest.Method = "POST";
            sendinBlueRequest.Headers.Add("api-key", apiKey);
            sendinBlueRequest.ContentType = "application/json";

            // Define the email content
            var emailContent = new
            {
                sender = new
                {
                    name = "JobFinder",
                    email = "chlouchi.med@gmail.com"
                },
                to = new[]
                {
                new
                {
                    email = txtEmail.Text.Trim(), // Replace with the recipient's email address
                    name = txtUsername.Text // Replace with the recipient's name (optional)
                }
            },
                subject = txtSubject.Text,
                textContent = txtReply.Text,
                htmlContent = "<p>"+txtReply.Text+"</p>"
            };

            // Serialize the email content to JSON
            var jsonContent = JsonConvert.SerializeObject(emailContent);

            // Send the email using the SendinBlue API
            using (var streamWriter = new StreamWriter(sendinBlueRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonContent);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                // Get the SendinBlue API response
                var sendinBlueResponse = (HttpWebResponse)sendinBlueRequest.GetResponse();

                if (sendinBlueResponse.StatusCode == HttpStatusCode.OK)
                {
                    // Email sent successfully
                    lblMsg.Text = "Email sent successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    // Handle errors or display an error message
                    lblMsg.Text = "Error sending email.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (WebException ex)
            {
                // Handle exceptions
                lblMsg.Text = "Error sending email: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}