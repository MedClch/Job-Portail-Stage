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
    public partial class Job_Info : System.Web.UI.Page
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
                showJobInfo();
        }

        private void showJobInfo()
        {
            if (Request.QueryString["id"]!=null)
            {
                conn = new SqlConnection(str);
                query = "Select * from Jobs where JobId = '"+Request.QueryString["id"]+"'";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtJobTitle.Text = reader["Title"].ToString();
                        txtNbPost.Text = reader["NoOfPost"].ToString();
                        txtJobDesc.Text = reader["Description"].ToString();
                        txtQualiEdu.Text = reader["Qualification"].ToString();
                        txtExperience.Text = reader["Experience"].ToString();
                        txtSpecialization.Text = reader["Specialization"].ToString();
                        txtLastDate.Text = Convert.ToDateTime(reader["LastDateToApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = reader["Salary"].ToString();
                        txtJobType.Text = reader["JobType"].ToString();
                        txtCompanyName.Text = reader["CompanyName"].ToString();
                        txtWebsite.Text = reader["Website"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtCountry.Text = reader["Country"].ToString();
                        txtState.Text = reader["State"].ToString();
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
    }
}