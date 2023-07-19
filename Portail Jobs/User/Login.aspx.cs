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
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string username, password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoginType.SelectedValue=="Admin")
                {
                    username = ConfigurationManager.AppSettings["username"];
                    password = ConfigurationManager.AppSettings["password"];
                    if (username == txtUserName.Text.Trim() && password == txtPassword.Text.Trim())
                    {
                        Session["admin"]=username;
                        clear();
                        lblMsg.Visible = false;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                    }
                    else
                    {
                        showErrorMessage("Admin");
                        clear();
                    }
                }
                else
                {
                    conn = new SqlConnection(str);
                    string query = @"Select * from [User] where Username=@Username and Password=@Password";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["user"]=reader["Username"].ToString();
                        Session["userId"]=reader["UserId"].ToString();
                        clear();
                        lblMsg.Visible = false;
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        showErrorMessage("User");
                        clear();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex1)
            {
                Response.Write("<script>alert('"+ex1.Message+"');</script>");
                conn.Close();
            }
        }

        private void showErrorMessage(string userT)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "<b>"+userT+"</b> credentials are incorrect, please try again ";
            lblMsg.CssClass="alert alert-danger";
        }

        private void clear()
        {
            txtUserName.Text=string.Empty;
            txtPassword.Text=string.Empty;
            ddlLoginType.ClearSelection();
        }
    }
}