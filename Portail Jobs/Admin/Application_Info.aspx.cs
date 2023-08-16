using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class Application_Info : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;
        protected string resumeFileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
                Response.Redirect("../User/Default.aspx");
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
            if (Session["admin"]==null && Session["user"]==null)
                Response.Redirect("../User/Login.aspx");
            if (!IsPostBack)
                showApplicationInfo();
        }

        private void showApplicationInfo()
        {
            if (Request.QueryString["id"]!=null)
            {
                conn = new SqlConnection(str);
                query = @"Select j.CompanyName,aj.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume from AppliedJobs aj inner join [User] u on aj.UserId = u.UserId
                            inner join Jobs j on aj.JobId = j.jobId where aj.AppliedJobId = '"+Request.QueryString["id"]+"'";
                //query = "Select * from AppliedJobs where AppliedJobId = '"+Request.QueryString["id"]+"'";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCompanyName.Text = reader["CompanyName"].ToString();
                        txtJobTitle.Text = reader["Title"].ToString();
                        txtUsername.Text = reader["Name"].ToString();
                        txtUserEmail.Text = reader["Email"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        resumeFileName = reader["Resume"].ToString();
                    }
                }
                else
                {
                    lblMsg.Text="Job application not found !";
                    lblMsg.CssClass="alert alert-danger";
                }
                reader.Close();
                conn.Close();
            }
        }
    }
}