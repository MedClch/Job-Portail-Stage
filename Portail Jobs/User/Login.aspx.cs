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
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string username, password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                username = ConfigurationManager.AppSettings["username"];
                password = ConfigurationManager.AppSettings["password"];
                if(username == txtUserName.Text.Trim() && password == txtPassword.Text.Trim()) 
                {
                    Session["admin"]=username;
                    Response.Redirect("../Admin/Dashboard.aspx",false);
                }
                else
                {
                    showErrorMessage("Admin");
                }
            }
            catch (Exception ex1)
            {
                Response.Write("<script>alert('"+ex1.Message+"');</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        private void showErrorMessage(string user)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "<b>"+user+"</b> credentials are incorrect, please try again ";
            lblMsg.CssClass="alert alert-danger";
        }
    }
}