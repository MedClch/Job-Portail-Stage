using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
                Response.Redirect("../User/Default.aspx");
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
            if (Session["admin"]==null && Session["user"]==null)
                Response.Redirect("../User/Login.aspx");

            //if(Session["user"]!=null || Session["admin"]==null || (Session["admin"]==null && Session["user"]==null))
            //    Response.Redirect("../User/Default.aspx"); or Response.Redirect("../User/Login.aspx");

            //if (Session["admin"]==null || Session["user"]==null)
            //    Response.Redirect("../User/Login.aspx");
            if (!IsPostBack)
                showJobs();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            showJobs();
        }

        private void showJobs()
        {
            string query = string.Empty;
            conn = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],JobId,Title,NoOfPost,Qualification,Experience,LastDateToApply,CompanyName,Country,State,CreateDate from Jobs";
            cmd = new SqlCommand(query, conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (Request.QueryString["id"]!=null)
            {
                linkBack.Visible = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex=e.NewPageIndex;
            showJobs();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int jobID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                conn = new SqlConnection(str);
                cmd = new SqlCommand("Delete from Jobs where JobId = @id", conn);
                cmd.Parameters.AddWithValue("@id", jobID);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text="Job deleted successfully !";
                    lblMsg.CssClass="alert alert-success";
                }
                else
                {
                    lblMsg.Text="Couldn't delete this job, please try again later !";
                    lblMsg.CssClass="alert alert-success";
                }
                GridView1.EditIndex = -1;
                showJobs();
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName =="EditJob")
                Response.Redirect("NewJob.aspx?id="+e.CommandArgument.ToString());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                e.Row.ID=e.Row.RowIndex.ToString();
                if (Request.QueryString["id"]!=null)
                {
                    int jobID = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0]);
                    if(jobID == Convert.ToInt32(Request.QueryString["id"]))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }
    }
}