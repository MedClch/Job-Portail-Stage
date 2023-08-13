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
    public partial class ResumeBuild : System.Web.UI.Page
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"]!=null)
                Response.Redirect("../Admin/Dashboard.aspx");
            if (Session["user"]==null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                if (Request.QueryString["id"]!=null)
                    showUserInfo();
                else
                    Response.Redirect("Login.aspx");
            }

        }

        private void showUserInfo()
        {
            try
            {
                conn = new SqlConnection(str);
                string query = "Select * from [User] where UserId=@userId";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", Request.QueryString["id"]);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        txtUserName.Text = reader["Username"].ToString();
                        txtFullName.Text = reader["Name"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        txtTenth.Text = reader["TenthGrade"].ToString();
                        txtTwelfth.Text = reader["TwelfthGrade"].ToString();
                        txtGraduation.Text = reader["GraduationGrade"].ToString();
                        txtPostGraduation.Text = reader["PostGraduationGrade"].ToString();
                        txtWork.Text = reader["WorksOn"].ToString();
                        txtPhd.Text = reader["Phd"].ToString();
                        txtExperience.Text = reader["Experience"].ToString();
                        txtAdress.Text = reader["Address"].ToString();
                        ddlCountry.SelectedValue = reader["Country"].ToString();
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User not found !";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"]!=null)
                {
                    string concatQuery = string.Empty;
                    string filePath = string.Empty;
                    //bool isValidToExecute = false;
                    bool isValid = false;
                    conn = new SqlConnection(str);
                    if (fuResume.HasFile)
                    {
                        if (Utils.isValidExtensionResume(fuResume.FileName))
                        {
                            concatQuery = "Resume=@resume";
                            isValid = true;
                        }
                        else
                        {
                            concatQuery = string.Empty;
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }
                    query = @"Update [User] set Username=@Username,Name=@Name,Email=@Email,Mobile=@Mobile,TenthGrade=@TenthGrade,TwelfthGrade=@TwelfthGrade,GraduationGrade=@GraduationGrade,
                             PostGraduationGrade=@PostGraduationGrade,Phd=@Phd,WorksOn=@WorksOn,Experience=@Experience,"+concatQuery+",Address=@Address,Country=@Country";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenthGrade", txtTenth.Text.Trim());
                    cmd.Parameters.AddWithValue("@TwelfthGrade", txtTwelfth.Text.Trim());
                    cmd.Parameters.AddWithValue("@GraduationGrade", txtGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorksOn", txtWork.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAdress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                    if (fuResume.HasFile)
                    {
                        if (Utils.isValidExtensionResume(fuResume.FileName))
                        {
                            Guid guid = Guid.NewGuid();
                            filePath = "Resumes/"+guid.ToString()+fuResume.FileName;
                            fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/")+guid.ToString()+fuResume.FileName);
                            cmd.Parameters.AddWithValue("@resume", filePath);
                            isValid = true;
                        }
                        else
                        {
                            concatQuery = string.Empty;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Please select .doc, .docx, .pdf files for resume !";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        isValid = true;
                    }
                    if (isValid)
                    {
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume uploaded successfully !";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Couldn't update informations, please try again !";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Couldn't update informations, please try <b>reconnecting to your account</b> !";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number==2627) // Check for unique constraint violation (error number 2627)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUserName.Text.Trim() + "<b> already exists, please try again!";
                    lblMsg.CssClass = "alert alert-danger";
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
}