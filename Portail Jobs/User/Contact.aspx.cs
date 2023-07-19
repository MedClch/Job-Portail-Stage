using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.User
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try {
                conn = new SqlConnection(str);
                string query = @"Insert into Contact values (@Name,@Email,@Subject,@Message)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name",name.Value.Trim());
                cmd.Parameters.AddWithValue("@Email", email.Value.Trim());
                cmd.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                cmd.Parameters.AddWithValue("@Message", message.Value.Trim());
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if (res > 0) {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Thank you for reaching out to us !";
                    lblMsg.CssClass="alert alert-success";
                    clear();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Couldn't save your message, please try again !";
                    lblMsg.CssClass="alert alert-danger";

                }
            }
            catch (Exception ex){ 
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            finally { conn.Close(); }
        }

        private void clear()
        {
            name.Value= string.Empty; 
            email.Value= string.Empty;
            subject.Value=string.Empty; 
            message.Value= string.Empty;
        }
    }
}