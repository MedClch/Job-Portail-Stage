using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Portail_Jobs.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(str);
                string query = @"Insert into [User] (Username,Password,Name,Address,Mobile,Email,Country) 
                                    values (@Username,@Password,@Name,@Address,@Mobile,@Email,@Country)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtConfirmPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAdress.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                conn.Open();
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Successfully registered !";
                    lblMsg.CssClass="alert alert-success";
                    clear();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Couldn't save your informations, please try again !";
                    lblMsg.CssClass="alert alert-danger";
                }

            }
            catch(SqlException ex)
            {
                if(ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>"+txtUserName.Text.Trim()+"<b> already exists, please try again !";
                    lblMsg.CssClass="alert alert-danger";
                    clear();
                }
                else
                {
                    Response.Write("<script>alert('"+ex.Message+"');</script>");
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

        private void clear()
        {
            txtUserName.Text=string.Empty;
            txtPassword.Text=string.Empty;
            txtFullName.Text=string.Empty;
            txtAdress.Text=string.Empty;
            txtMobile.Text=string.Empty;
            txtEmail.Text=string.Empty;
            ddlCountry.ClearSelection();
        }

        //private int check(string userName)
        //{
        //    SqlDataReader reader;
        //    conn = new SqlConnection(str);
        //    conn.Open();
        //    string query = @"Select * from [User] where Username=@Username";
        //    cmd = new SqlCommand(query, conn);
        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        conn.Close();
        //        return 0;
        //    }
        //    else
        //    {
        //        conn.Close();
        //        return 1;
        //    }
        //}
    }
}