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
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],jah.AppliedJobId,j.CompanyName,jah.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume,jar.Response 
                            from JobApplicationHistory jah
                            inner join JobApplicationResp jar on jar.AppliedJobId = jah.AppliedJobId
                            inner join [User] u on jah.UserId = u.UserId
                            inner join Jobs j on jah.JobId = j.jobId";
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
                // Find the index of the "Response" column
                int responseColumnIndex = GridView1.Columns.Cast<DataControlField>().ToList().FindIndex(field => field.HeaderText == "Application response");

                // Make sure the index is valid
                if (responseColumnIndex >= 0)
                {
                    // Get the cell value in the "Response" column for the current row
                    string responseValue = e.Row.Cells[responseColumnIndex].Text.Trim();

                    // Set the row's background color based on the response value
                    if (responseValue == "Accepted")
                    {
                        e.Row.BackColor = System.Drawing.Color.Green; // Green color for 'Accepted'
                    }
                    else if (responseValue == "Rejected")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red; // Red color for 'Rejected'
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {       
            GridView1.PageIndex=e.NewPageIndex;
            showApplicationHisotry();
        }

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex=e.NewPageIndex;
        //    showApplicationHisotry();
        //}
    }
}