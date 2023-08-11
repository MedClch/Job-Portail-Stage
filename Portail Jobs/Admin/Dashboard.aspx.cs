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
                // Fetch data from the database
                DataTable data = GetDataFromDatabase();
                // Convert DataTable to a list of dictionaries
                List<Dictionary<string, object>> dataList = DataTableToList(data);
                // Serialize the list of dictionaries to JSON
                string chartData = ListToJson(dataList);
                // Register client script to render chart
                Page.ClientScript.RegisterStartupScript(GetType(), "RenderChart", $"renderChart({chartData});", true);
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

        private void showContactStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from Contact", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count>0)
            {
                Session["Contact"] = dt.Rows[0][0];
            }
            else
            {
                Session["Contact"] = 0;
            }
        }

        private void showApplicationStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from AppliedJobs", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
            {
                Session["Applications"] = dt.Rows[0][0];
            }
            else
            {
                Session["Applications"] = 0;
            }
        }

        private void showJobStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from Jobs", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
            {
                Session["Jobs"] = dt.Rows[0][0];
            }
            else
            {
                Session["Jobs"] = 0;
            }
        }

        private void showUserStats()
        {
            conn = new SqlConnection(str);
            adapter = new SqlDataAdapter("Select COUNT(*) from [User]", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count>0)
            {
                Session["Users"] = dt.Rows[0][0];
            }
            else
            {
                Session["Users"] = 0;
            }
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, NoOfPost FROM Jobs"; // Replace with your actual query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            return dataTable;
        }

        private List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        private string ListToJson(List<Dictionary<string, object>> list)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        //private DataTable GetDataFromDatabase()
        //{
        //    DataTable dataTable = new DataTable();
        //    string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT Title, NoOfPost FROM Jobs"; // Replace with your actual query
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                dataTable.Load(reader);
        //            }
        //        }
        //    }

        //    return dataTable;
        //}

        //private string DataTableToJson(DataTable dt)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(dt);
        //}
    }
}

