using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Portail_Jobs.User
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
            {
                lbRegisterOrProfile.Text="Profile";
                lbLoginOrLogout.Text="Logout";
            }
            else
            {
                lbRegisterOrProfile.Text="Register";
                lbLoginOrLogout.Text="Login";
            }
        }

        protected void lbRegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (lbRegisterOrProfile.Text=="Profile")
            {
                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Redirect("Register.aspx");
            }
        }

        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (lbLoginOrLogout.Text=="Login")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Redirect("Login.aspx");
            }
        }

        protected bool hasAcceptedApplications()
        {
            if (Session["user"] != null)
            {
                int idU = Convert.ToInt32(Session["userId"]);
                SqlConnection conn = new SqlConnection(str);
                string query = @"Select j.JobId from Jobs j inner join JobApplicationHistory jah on j.JobId = jah.jobId and Response='Accepted' and jah.UserId=" + idU;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool hasAcceptedApplications = reader.HasRows;
                reader.Close();
                conn.Close();
                return hasAcceptedApplications;
            }
            else
            {
                return false;
            }
        }

        protected bool hasApplied()
        {
            if (Session["user"] != null)
            {
                int idU = Convert.ToInt32(Session["userId"]);
                SqlConnection conn = new SqlConnection(str);
                string query = @"Select j.JobId from Jobs j inner join JobApplicationHistory jah on j.JobId = jah.jobId and jah.UserId=" + idU;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                bool hasAcceptedApplications = reader.HasRows;
                reader.Close();
                conn.Close();
                return hasAcceptedApplications;
            }
            else
            {
                return false;
            }
        }

    }
}