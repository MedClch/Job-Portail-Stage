using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.User
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;
        SqlDataAdapter adapter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList();
            }
        }
        private void showJobList()
        {
            if (dt==null)
            {
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs";
                cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.png";
            }
            else
            {
                url1 = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }

        protected static string RelativeDate(DateTime theDate)
        {
            return Utils.GetRelativeDate(theDate);
        }
    }
}