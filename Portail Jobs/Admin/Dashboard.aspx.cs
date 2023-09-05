using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Portail_Jobs.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlDataAdapter adapter;
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
                Response.Redirect("../User/Default.aspx");
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
            if (Session["admin"]==null && Session["user"]==null)
                Response.Redirect("../User/Login.aspx");
            if (!IsPostBack)
            {
                showUserStats();
                showJobStats();
                showApplicationStats();
                showContactStats();
            }

            //if (Session["user"]!=null)
            //    Response.Redirect("../User/Default.aspx");
            //if (Session["admin"]==null)
            //    Response.Redirect("../User/Login.aspx");

            //if (Session["admin"]==null && Session["user"]==null)
            //    Response.Redirect("../User/Login.aspx");
            //if (Session["user"]!=null)
            //    Response.Redirect("../User/Default.aspx");
        }

        public string GetCombinedDataAsJson()
        {
            // Fetch data from each table
            DataTable dataFromUserTable = FetchDataFromTable("SELECT count(*) FROM [User]");
            DataTable dataFromAppliedJobsTable = FetchDataFromTable("SELECT count(*) FROM AppliedJobs");
            DataTable dataFromContactTable = FetchDataFromTable("SELECT count(*) FROM Contact");
            DataTable dataFromJobsTable = FetchDataFromTable("SELECT count(*) FROM Jobs");
            // Get the counts from each table
            int userCount = Convert.ToInt32(dataFromUserTable.Rows[0][0]);
            int appliedJobsCount = Convert.ToInt32(dataFromAppliedJobsTable.Rows[0][0]);
            int contactCount = Convert.ToInt32(dataFromContactTable.Rows[0][0]);
            int jobsCount = Convert.ToInt32(dataFromJobsTable.Rows[0][0]);
            // Combine the data into a single object
            var combinedData = new
            {
                Labels = new string[] { "Users", "Applied Jobs", "Contacts", "Jobs"},
                Values1 = new int[] { userCount, appliedJobsCount, contactCount, jobsCount}
            };
            // Convert the combinedData object to a JSON string
            string jsonData = JsonConvert.SerializeObject(combinedData);
            return jsonData;
        }

        public string GetCombinedDataAsJson_Chart2()
        {
            // Fetch data from each table
            DataTable dataFromAppliedJobsTable = FetchDataFromTable("SELECT count(*) FROM AppliedJobs");
            DataTable dataFromJobApplicationResp_Declined = FetchDataFromTable("SELECT count(*) FROM JobApplicationHistory where Response='Rejected'");
            DataTable dataFromJobApplicationResp_Accepted = FetchDataFromTable("SELECT count(*) FROM JobApplicationHistory where Response='Accepted'");
            DataTable dataFromJobApplicationResp_Pending = FetchDataFromTable("SELECT count(*) FROM JobApplicationHistory where Response='Pending'");
            // Get the counts from each table
            int appliedJobsCount = Convert.ToInt32(dataFromAppliedJobsTable.Rows[0][0]);
            int pendingJobsCount = Convert.ToInt32(dataFromJobApplicationResp_Pending.Rows[0][0]);
            int declinedAppCount = Convert.ToInt32(dataFromJobApplicationResp_Declined.Rows[0][0]);
            int acceptedAppCount = Convert.ToInt32(dataFromJobApplicationResp_Accepted.Rows[0][0]);
            // Combine the data into a single object
            var combinedData = new
            {
                Labels1 = new string[] { "Applied Jobs", "Pending applications", "Rejected applications", "Accepted applications" },
                Values2 = new int[] { appliedJobsCount, pendingJobsCount, declinedAppCount, acceptedAppCount }
            };
            // Convert the combinedData object to a JSON string
            string jsonData = JsonConvert.SerializeObject(combinedData);
            return jsonData;
        }

        // Fetch data from the database using the provided query
        private DataTable FetchDataFromTable(string query)
        {
            DataTable dataTable = new DataTable();
            using (conn = new SqlConnection(str))
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                conn.Close();
            }
            return dataTable;
        }

        public string GetPieChartDataAsJson()
        {
            int usersWithNullResume = GetCountFromQuery("SELECT COUNT(*) FROM [User] WHERE Resume IS NULL");
            int appliedJobsCount = GetCountFromQuery("SELECT COUNT(a.AppliedJobId) FROM Jobs j LEFT JOIN AppliedJobs a ON j.JobId = a.JobId");
            int pendingApplicationsCount = GetCountFromQuery("SELECT COUNT(*) FROM JobApplicationHistory WHERE Response='Pending'");

            // Combine the pie chart data into an object
            var pieChartData = new
            {
                Labels = new string[] { "Users with Null Resume", "Applied Jobs", "Pending Applications" },
                Values = new int[] { usersWithNullResume, appliedJobsCount, pendingApplicationsCount }
            };

            // Convert the pieChartData object to a JSON string
            string jsonData = JsonConvert.SerializeObject(pieChartData);
            return jsonData;
        }

        // Fetch a count from the database using the provided query
        private int GetCountFromQuery(string query)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }
            return count;
        }


        private void showContactStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from Contact", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count>0)
                Session["Contact"] = dt.Rows[0][0];
            else
                Session["Contact"] = 0;
        }

        private void showApplicationStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from AppliedJobs", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
                Session["Applications"] = dt.Rows[0][0];
            else
                Session["Applications"] = 0;
        }

        private void showJobStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from Jobs", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
                Session["Jobs"] = dt.Rows[0][0];
            else
                Session["Jobs"] = 0;
        }

        private void showUserStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from [User]", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
                Session["Users"] = dt.Rows[0][0];
            else
                Session["Users"] = 0;
        }
    }
}

