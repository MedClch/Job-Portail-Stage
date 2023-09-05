using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            string recipientEmail = txtEmail.Text.Trim();
            string replyMessage = txtReply.Text.Trim();
            if (!string.IsNullOrEmpty(recipientEmail))
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("chlouchi.med@gmail.com", "moha1234567890");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("chlouchi.med@gmail.com");
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = txtSubject.Text.Trim(); // Customize the subject as needed.
                    mailMessage.Body = replyMessage;

                    smtpClient.Send(mailMessage);
                    lblMsg.Text = "Email sent successfully !";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "An error occurred while sending the email: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            else
            {
                lblMsg.Text = "Recipient's email address is empty. Please provide a valid email address.";
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}