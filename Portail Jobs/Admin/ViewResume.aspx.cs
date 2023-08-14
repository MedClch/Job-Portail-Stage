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
    public partial class ViewResume : System.Web.UI.Page
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
                showApplications();
        }

        private void showApplications()
        {
            string query = string.Empty;
            conn = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],aj.AppliedJobId,j.CompanyName,aj.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume from AppliedJobs aj
                            inner join [User] u on aj.UserId = u.UserId
                            inner join Jobs j on aj.JobId = j.jobId";
            cmd = new SqlCommand(query, conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex=e.NewPageIndex;
            showApplications();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int appliedjobID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            DeleteAppliedJob1(appliedjobID);
            //try
            //{
            //    GridViewRow row = GridView1.Rows[e.RowIndex];
            //    int appliedjobID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            //    conn = new SqlConnection(str);
            //    cmd = new SqlCommand("Delete from AppliedJobs where AppliedJobId = @id", conn);
            //    cmd.Parameters.AddWithValue("@id", appliedjobID);
            //    conn.Open();
            //    int r = cmd.ExecuteNonQuery();
            //    if (r > 0)
            //    {
            //        lblMsg.Text="Job application deleted successfully !";
            //        lblMsg.CssClass="alert alert-success";
            //    }
            //    else
            //    {
            //        lblMsg.Text="Couldn't delete this job application, please try again later !";
            //        lblMsg.CssClass="alert alert-success";
            //    }
            //    GridView1.EditIndex = -1;
            //    showApplications();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('"+ex.Message+"');</script>");
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$"+e.Row.RowIndex);
            //e.Row.ToolTip = "Click to view job details";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.Cells[1].ToolTip = "Click to view job details";
                e.Row.Cells[2].ToolTip = "Click to view job details";
            }
        }

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        if (row.RowIndex == GridView1.SelectedIndex)
        //        {
        //            HiddenField jobId = (HiddenField)row.FindControl("hdnJobId");
        //            Response.Redirect("JobList.aspx?id="+jobId.Value);
        //        }
        //        else
        //        {
        //            row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //            row.ToolTip = "Click to select this row";
        //        }
        //    }
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int appliedJobID = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
                DeleteAppliedJob(appliedJobID);
                showApplications();
            }
            if (e.CommandName == "Accept")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int appliedJobId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
                string response = "Accepted";
                InsertIntoApplicationsHistory(appliedJobId, response);
                showApplications();
            }
            if (e.CommandName == "Decline")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int appliedJobId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
                string response = "Rejected";
                InsertIntoApplicationsHistory(appliedJobId, response);
                showApplications();
            }
            //else
            //{
            //    int rowIndex = Convert.ToInt32(e.CommandArgument);
            //    HiddenField jobId = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdnJobId");
            //    Response.Redirect("JobList.aspx?id=" + jobId.Value);
            //}
        }

        private void DeleteAppliedJob(int appliedJobID)
        {
            try
            {
                conn = new SqlConnection(str);
                cmd = new SqlCommand("Delete from AppliedJobs where AppliedJobId = @id", conn);
                cmd.Parameters.AddWithValue("@id", appliedJobID);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Job application deleted successfully !";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Couldn't delete this job application, please try again later !";
                    lblMsg.CssClass = "alert alert-success";
                }
                showApplications();
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

        private void DeleteAppliedJob1(int appliedJobID)
        {
            try
            {
                conn = new SqlConnection(str);
                cmd = new SqlCommand("INSERT INTO JobApplicationResp (AppliedJobId, Response) VALUES (@appliedJobID, 'Rejected')", conn);
                cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", conn);
                    cmd.Parameters.AddWithValue("@id", appliedJobID);
                    int r = cmd.ExecuteNonQuery();
                    if (r > 0)
                    {
                        lblMsg.Text = "Job application deleted successfully !";
                        lblMsg.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lblMsg.Text = "Couldn't delete this job application, please try again later !";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Text = "Couldn't delete this job application, please try again later !";
                    lblMsg.CssClass = "alert alert-danger";
                }
                showApplications();
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

        private void DeleteAppliedJob2(int appliedJobID)
        {
            try
            {
                conn = new SqlConnection(str);
                conn.Open();
                    cmd = new SqlCommand("INSERT INTO JobApplicationResp (AppliedJobId, JobId, UserId, Response) VALUES (@appliedJobID, @jobId, @userId, 'Rejected')", conn);
                    cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                    cmd.Parameters.AddWithValue("@jobId", GetUserIdAndJobId(appliedJobID).Item2);
                    cmd.Parameters.AddWithValue("@userId", GetUserIdAndJobId(appliedJobID).Item1);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", conn);
                        cmd.Parameters.AddWithValue("@id", appliedJobID);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Text = "Job application deleted successfully !";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Text = "Couldn't delete this job application, please try again later !";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Couldn't delete this job application, please try again later !";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    showApplications();
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


        private void InsertIntoApplicationsHistory(int appliedJobId, string response)
        {
            try
            {
                conn = new SqlConnection(str);
                cmd = new SqlCommand("INSERT INTO JobApplicationResp VALUES (@AppliedJobId,@Response)", conn);
                cmd.Parameters.AddWithValue("@AppliedJobId", appliedJobId);
                cmd.Parameters.AddWithValue("@Response", response);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Job application status updated successfully !";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Couldn't update this job application status, please try again later !";
                    lblMsg.CssClass = "alert alert-success";
                }
                showApplications();
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

        private Tuple<int, int> GetUserIdAndJobId(int appliedJobId)
        {
            int userId = -1; // Default value indicating not found
            int jobId = -1;  // Default value indicating not found

            string query = @"Select u.UserId, j.JobId from AppliedJobs aj
                    inner join [User] u on aj.UserId = u.UserId
                    inner join Jobs j on aj.JobId = j.jobId
                    where aj.AppliedJobId = @appliedJobId";
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@appliedJobId", appliedJobId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = Convert.ToInt32(reader["UserId"]);
                            jobId = Convert.ToInt32(reader["JobId"]);
                        }
                    }
                }
            }
            return new Tuple<int, int>(userId, jobId);
        }

        //private void ApplicationResponse(bool response)
        //{
        //    string status = string.Empty;
        //    if (response)
        //    {
        //        status = "Accepted";
        //        try
        //        {
        //            conn = new SqlConnection(str);
        //            string query = @"Insert into JobApplicationResp values (@JobId,@UserId)";
        //            cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
        //            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
        //            cmd.Parameters.AddWithValue("@Response", status);
        //            conn.Open();
        //            int res = cmd.ExecuteNonQuery();
        //            if (res > 0)
        //            {
        //                lblMsg.Visible = true;
        //                lblMsg.Text = "Job application accepted !";
        //                lblMsg.CssClass = "alert alert-success";
        //            }
        //            else
        //            {
        //                lblMsg.Visible = true;
        //                lblMsg.Text = "Error, please try again !";
        //                lblMsg.CssClass = "alert alert-danger";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write("<script>alert('" + ex.Message + "');</script>");
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //    else
        //    {
        //        status = "Declined";
        //    }
        //}
    }
}