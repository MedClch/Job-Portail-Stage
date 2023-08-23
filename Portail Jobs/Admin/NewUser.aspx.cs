using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class NewUser : System.Web.UI.Page
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
            Session["title"]="Add user";
            if (!IsPostBack)
                fillData();
        }

        private void fillData()
        {
            if (Request.QueryString["id"]!=null)
            {
                conn = new SqlConnection(str);
                query = "Select * from [User] where UserId = '"+Request.QueryString["id"]+"'";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtUsername.Enabled=false;
                        txtUsername.ForeColor = Color.Black;
                        txtFullName.Text = reader["Name"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        ddlCountry.SelectedValue = reader["Country"].ToString();
                        btnAddUser.Text = "Edit user";
                        linkBack.Visible=true;
                        Session["title"]="Edit user";
                    }
                }
                else
                {
                    lblMsg.Text="Job not found !";
                    lblMsg.CssClass="alert alert-danger";
                }
                reader.Close();
                conn.Close();
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"]!=null)
            {
                if(txtPass.Text.Trim() == null)
                {
                    try
                    {
                        conn = new SqlConnection(str);
                        string query = @"Update [User] set Name=@Name,Mobile=@Mobile,Email=@Email,
                                Address=@Address,Country=@Country where UserId=@id";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "User informations updated uccessfully !";
                            lblMsg.CssClass = "alert alert-success";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                            clear();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Couldn't update this user informations, please try again!";
                            lblMsg.CssClass = "alert alert-danger";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    try
                    {
                        conn = new SqlConnection(str);
                        string query = @"Update [User] set Name=@Name,Password=@Password,Mobile=@Mobile,Email=@Email,
                                Address=@Address,Country=@Country where UserId=@id";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                        string hashedPassword = HashPassword(txtConfirmPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "User informations updated uccessfully !";
                            lblMsg.CssClass = "alert alert-success";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                            clear();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Couldn't update this user informations, please try again!";
                            lblMsg.CssClass = "alert alert-danger";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                try
                {
                    conn = new SqlConnection(str);
                    string query = @"Insert into [User] (Username,Password,Name,Address,Mobile,Email,Country) 
                            values (@Username,@Password,@Name,@Address,@Mobile,@Email,@Country)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    // Hash the password using bcrypt before storing it in the database
                    string hashedPassword = HashPassword(txtConfirmPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    conn.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "User added uccessfully !";
                        lblMsg.CssClass = "alert alert-success";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        clear();
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Couldn't save this user informations, please try again!";
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number==2627)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "<b> already exists, please try again!";
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        clear();
                    }
                    else
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                }
                catch (Exception ex1)
                {
                    Response.Write("<script>alert('" + ex1.Message + "');</script>");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void clear()
        {
            txtUsername.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtConfirmPass.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlCountry.ClearSelection();
        }

        public static string HashPassword(string password)
        {
            // Generate a random salt and hash the password
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
}