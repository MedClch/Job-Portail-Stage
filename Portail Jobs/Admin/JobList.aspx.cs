using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int jobID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            DeleteJob(jobID);
            //try
            //{
            //    GridViewRow row = GridView1.Rows[e.RowIndex];
            //    int jobID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            //    conn = new SqlConnection(str);
            //    cmd = new SqlCommand("Delete from Jobs where JobId = @id", conn);
            //    cmd.Parameters.AddWithValue("@id", jobID);
            //    conn.Open();
            //    int r = cmd.ExecuteNonQuery();
            //    if (r > 0)
            //    {
            //        lblMsg.Text="Job deleted successfully !";
            //        lblMsg.CssClass="alert alert-success";
            //    }
            //    else
            //    {
            //        lblMsg.Text="Couldn't delete this job, please try again later !";
            //        lblMsg.CssClass="alert alert-success";
            //    }
            //    GridView1.EditIndex = -1;
            //    showJobs();
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

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName =="EditJob")
        //        Response.Redirect("NewJob.aspx?id="+e.CommandArgument.ToString());
        //    else if (e.CommandName == "Delete")
        //    {
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        int jobId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);
        //        DeleteJob(jobId);
        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //e.Row.ToolTip = "Click to view details";
                //// You can also change the cursor style to make it look clickable
                //e.Row.Style["cursor"] = "pointer";
                e.Row.ID=e.Row.RowIndex.ToString();
                if (Request.QueryString["id"]!=null)
                {
                    int jobID = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0]);
                    if (jobID == Convert.ToInt32(Request.QueryString["id"]))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }

        // Deletes the job and all its applications
        private void DeleteJob_JobApplications(int JobID)
        {
            try
            {
                conn = new SqlConnection(str);
                conn.Open();
                // Delete the applications related to the job
                SqlCommand deleteAppliedJobsCmd = new SqlCommand("DELETE FROM AppliedJobs WHERE JobId = @jobId", conn);
                deleteAppliedJobsCmd.Parameters.AddWithValue("@jobId", JobID);
                deleteAppliedJobsCmd.ExecuteNonQuery();
                // Delete the job
                SqlCommand deleteJobCmd = new SqlCommand("DELETE FROM Jobs WHERE JobId = @jobId", conn);
                deleteJobCmd.Parameters.AddWithValue("@jobId", JobID);
                int r = deleteJobCmd.ExecuteNonQuery();

                if (r > 0)
                {
                    lblMsg.Text = "Job deleted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                else
                {
                    lblMsg.Text = "Couldn't delete this job, please try again later!";
                    lblMsg.CssClass = "alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                showJobs();
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

        // Deletes only the job and keeps its applications
        private void DeleteJob(int JobID)
        {
            try
            {
                conn = new SqlConnection(str);
                cmd = new SqlCommand("Delete from Jobs where JobId = @id", conn);
                cmd.Parameters.AddWithValue("@id", JobID);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Job deleted successfully !";
                    lblMsg.CssClass = "alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                else
                {
                    lblMsg.Text = "Couldn't delete this job, please try again later !";
                    lblMsg.CssClass = "alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                showJobs();
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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Sheet1");
                // Adding headers
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    // Exclude columns by header text
                    if (GridView1.Columns[i].HeaderText != "Edit" && GridView1.Columns[i].HeaderText != "Delete")
                    {
                        ws.Cells[1, i + 1].Value = GridView1.Columns[i].HeaderText;
                    }
                }
                // Adding data
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    int cellIndex = 0; // Keep track of the index for the Excel cell
                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        // Exclude columns by header text
                        if (GridView1.Columns[j].HeaderText != "Edit" && GridView1.Columns[j].HeaderText != "Delete")
                        {
                            // Special handling for date columns
                            if (GridView1.Columns[j].HeaderText == "Valid until" || GridView1.Columns[j].HeaderText == "Post date")
                            {
                                DateTime dateValue;
                                if (DateTime.TryParse(GridView1.Rows[i].Cells[j].Text, out dateValue))
                                {
                                    ws.Cells[i + 2, cellIndex + 1].Value = dateValue.ToString("dd MMMM yyyy");
                                }
                                else
                                {
                                    ws.Cells[i + 2, cellIndex + 1].Value = GridView1.Rows[i].Cells[j].Text;
                                }
                            }
                            else
                            {
                                ws.Cells[i + 2, cellIndex + 1].Value = GridView1.Rows[i].Cells[j].Text;
                            }
                            cellIndex++;
                        }
                    }
                }
                // Save the Excel package to a MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    package.SaveAs(ms);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=JobsList.xlsx");
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }



        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selectedIndex = GridView1.SelectedIndex;
        //    string jobId = GridView1.DataKeys[selectedIndex].Value.ToString();

        //    // Redirect to JobInfo page with the selected job's ID
        //    Response.Redirect("Job_Info.aspx?id=" + jobId);
        //}
    }
}