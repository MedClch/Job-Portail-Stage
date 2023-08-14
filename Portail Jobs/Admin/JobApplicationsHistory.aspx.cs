using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Portail_Jobs.Admin
{
    public partial class JobApplicationsHistory : System.Web.UI.Page
    {
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
                showApplicationHisotry();
        }

        private void showApplicationHisotry()
        {
            string query = string.Empty;
            conn = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],jar.AppliedJobId,j.CompanyName,aj.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume from JobApplicationResp jar
                            inner join [User] u on jar.UserId = u.UserId
                            inner join Jobs j on jar.JobId = j.jobId";
            cmd = new SqlCommand(query, conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string responseValue = DataBinder.Eval(e.Row.DataItem, "Response").ToString();
                TableCell contactCell = e.Row.Cells[9];
                if (responseValue == "Accepted")
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Green;
                    contactCell.Enabled = true;
                }
                else if (responseValue == "Rejected")
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    contactCell.Enabled = false;
                }
            }
        }
    }
}