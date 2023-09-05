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
            var apiKey = "apiKey";
            var apiUrl = "https://api.brevo.com/v3/emailCampaigns";

            var sendinBlueRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            sendinBlueRequest.Method = "POST";
            sendinBlueRequest.Headers.Add("api-key", apiKey);
            sendinBlueRequest.ContentType = "application/json";

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
                    email = txtEmail.Text.Trim(), 
                    name = txtUsername.Text 
                }
            },
                subject = txtSubject.Text,
                textContent = txtReply.Text,
                htmlContent = "<p>"+txtReply.Text+"</p>"
            };
            var jsonContent = JsonConvert.SerializeObject(emailContent);
            using (var streamWriter = new StreamWriter(sendinBlueRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonContent);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                var sendinBlueResponse = (HttpWebResponse)sendinBlueRequest.GetResponse();
                if (sendinBlueResponse.StatusCode == HttpStatusCode.OK)
                {
                    lblMsg.Text = "Email sent successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Error sending email.";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (WebException ex)
            {
                lblMsg.Text = "Error sending email: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}