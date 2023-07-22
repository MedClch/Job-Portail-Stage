using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BCrypt.Net;

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
                    if (ddlLoginType.SelectedValue == "Admin")
                    {
                        // Assuming the admin username is stored securely, not in configuration files.
                        string adminUsername = ConfigurationManager.AppSettings["adminUsername"];
                        string adminStoredPasswordHash = ConfigurationManager.AppSettings["adminPasswordHash"];
                        string adminInputPassword = txtPassword.Text.Trim();

                        if (adminUsername == txtUserName.Text.Trim() && BCrypt.Net.BCrypt.Verify(adminInputPassword, adminStoredPasswordHash))
                        {
                            // Successful admin login
                            Session["admin"] = adminUsername;
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
                        string query = @"SELECT * FROM [User] WHERE Username=@Username";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Retrieve the stored hashed password from the database
                            string storedPasswordHash = reader["Password"].ToString();

                            // Compare the input password with the stored hash
                            string inputPassword = txtPassword.Text.Trim();
                            if (BCrypt.Net.BCrypt.Verify(inputPassword, storedPasswordHash))
                            {
                                // Successful user login
                                Session["user"] = reader["Username"].ToString();
                                Session["userId"] = reader["UserId"].ToString();
                                clear();
                                lblMsg.Visible = false;
                                Response.Redirect("Default.aspx", false);
                            }
                            else
                            {
                                showErrorMessage("User");
                                clear();
                            }
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
                    Response.Write("<script>alert('" + ex1.Message + "');</script>");
                    conn.Close();
                }
            }



        // Old function
        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlLoginType.SelectedValue=="Admin")
        //        {
        //            username = ConfigurationManager.AppSettings["username"];
        //            password = ConfigurationManager.AppSettings["password"];
        //            if (username == txtUserName.Text.Trim() && password == txtPassword.Text.Trim())
        //            {
        //                Session["admin"]=username;
        //                clear();
        //                lblMsg.Visible = false;
        //                Response.Redirect("../Admin/Dashboard.aspx", false);
        //            }
        //            else
        //            {
        //                showErrorMessage("Admin");
        //                clear();
        //            }
        //        }
        //        else
        //        {
        //            conn = new SqlConnection(str);
        //            string query = @"Select * from [User] where Username=@Username and Password=@Password";
        //            cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
        //            conn.Open();
        //            reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                Session["user"]=reader["Username"].ToString();
        //                Session["userId"]=reader["UserId"].ToString();
        //                clear();
        //                lblMsg.Visible = false;
        //                Response.Redirect("Default.aspx", false);
        //            }
        //            else
        //            {
        //                showErrorMessage("User");
        //                clear();
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        Response.Write("<script>alert('"+ex1.Message+"');</script>");
        //        conn.Close();
        //    }
        //}

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

        // Function to hash the password using bcrypt
        public static string HashPassword(string password)
        {
            // Generate a random salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        // Function to verify the password against the hashed password
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Compare the password with the hashed password
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}