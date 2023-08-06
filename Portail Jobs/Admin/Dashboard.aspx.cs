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
                // Fetch data from the database
                DataTable data = GetDataFromDatabase();

                // Convert DataTable to JSON for chart data
                string chartData = DataTableToJson(data);

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

        private string DataTableToJson(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dt);
        }
    }
}

