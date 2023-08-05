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
    public partial class JobDetails : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt,dt1;
        SqlDataAdapter adapter;
        public string jobTitle = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"]!=null)
            {
                showJobDetails();
                DataBind();
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //showJobDetails();
        }

        private void showJobDetails()
        {
            if (dt==null)
            {
                conn = new SqlConnection(str);
                string query = @"Select * from Jobs where JobId = @id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            jobTitle = dt.Rows[0]["Title"].ToString();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName=="ApplyJob")
            {
                if (Session["user"]!=null)
                {
                    try
                    {
                        conn = new SqlConnection(str);
                        string query = @"Insert into AppliedJobs values (@JobId,@UserId)";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Job application sent successfully !";
                            lblMsg.CssClass = "alert alert-success";
                            showJobDetails();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Couldn't apply for this job, please try again !";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Session["user"]!=null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                if (hasApplied())
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply now";
                }
            }
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

        bool hasApplied()
        {
            conn = new SqlConnection(str);
            string query = @"Select * from AppliedJobs where UserId=@UserId and JobId=@JobId";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
            adapter = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            adapter.Fill(dt1);
            if(dt1.Rows.Count == 1)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}