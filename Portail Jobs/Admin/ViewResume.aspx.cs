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
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],AppliedJobId,UserId,CompanyName,jobId,Title,Mobile,Username,Name,Email,Resume from AppliedJobs";
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
            DeleteAppliedJob_EditStatus(appliedjobID);
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
            //        lblMsg.CssClass="alert alert-danger";
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
                DeleteAppliedJob_EditStatus(appliedJobID);
                showApplications();
            }
            else
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int appliedJobId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
                AcceptApplication(appliedJobId);
                showApplications();
            }
        }


        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Delete")
        //    {
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        int appliedJobID = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
        //        DeleteAppliedJob_EditStatus(appliedJobID);
        //        showApplications();
        //    }
        //    if (e.CommandName == "Accept")
        //    {
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);
        //        int appliedJobId = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
        //        AcceptApplication(appliedJobId);
        //        showApplications();
        //    }
        //    //else
        //    //{
        //    //    int rowIndex = Convert.ToInt32(e.CommandArgument);
        //    //    HiddenField jobId = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdnJobId");
        //    //    Response.Redirect("JobList.aspx?id=" + jobId.Value);
        //    //}
        //}

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
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                else
                {
                    lblMsg.Text = "Couldn't delete this job application, please try again later !";
                    lblMsg.CssClass = "alert alert-danger";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
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

        //private void DeleteAppliedJob1(int appliedJobID)
        //{
        //    try
        //    {
        //        conn = new SqlConnection(str);
        //        cmd = new SqlCommand("INSERT INTO JobApplicationResp (AppliedJobId, Response) VALUES (@appliedJobID, 'Rejected')", conn);
        //        cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
        //        conn.Open();
        //        int result = cmd.ExecuteNonQuery();
        //        if (result > 0)
        //        {
        //            cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", conn);
        //            cmd.Parameters.AddWithValue("@id", appliedJobID);
        //            int r = cmd.ExecuteNonQuery();
        //            if (r > 0)
        //            {
        //                lblMsg.Text = "Job application deleted successfully !";
        //                lblMsg.CssClass = "alert alert-success";
        //            }
        //            else
        //            {
        //                lblMsg.Text = "Couldn't delete this job application, please try again later !";
        //                lblMsg.CssClass = "alert alert-danger";
        //            }
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Couldn't delete this job application, please try again later !";
        //            lblMsg.CssClass = "alert alert-danger";
        //        }
        //        showApplications();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('" + ex.Message + "');</script>");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private void DeleteAppliedJob_EditStatus(int appliedJobID)
        {
            try
            {
                conn = new SqlConnection(str);
                conn.Open();

                // Update JobApplicationResp
                cmd = new SqlCommand("Update JobApplicationResp set Response='Rejected' where AppliedJobId=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", appliedJobID);
                int result = cmd.ExecuteNonQuery();

                // Update JobApplicationHistory
                if (result > 0)
                {
                    cmd = new SqlCommand("Update JobApplicationHistory set Response='Rejected',ReplyDate=@rDate where AppliedJobId=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", appliedJobID);
                    cmd.Parameters.AddWithValue("@rDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    int historyResult = cmd.ExecuteNonQuery();

                    if (historyResult > 0)
                    {
                        // Delete from AppliedJobs
                        cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", conn);
                        cmd.Parameters.AddWithValue("@id", appliedJobID);
                        int deleteResult = cmd.ExecuteNonQuery();

                        if (deleteResult > 0)
                        {
                            lblMsg.Text = "Job application deleted successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        }
                        else
                        {
                            lblMsg.Text = "Couldn't delete this job application, please try again later!";
                            lblMsg.CssClass = "alert alert-danger";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Couldn't update the job application history, please try again later!";
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
                else
                {
                    lblMsg.Text = "Couldn't update the job application response, please try again later!";
                    lblMsg.CssClass = "alert alert-danger";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                //conn = new SqlConnection(str);
                //conn.Open();
                //cmd = new SqlCommand("Update JobApplicationResp set Response='Rejected' where AppliedJobId=@Id", conn);
                //cmd.Parameters.AddWithValue("@Id", appliedJobID);
                //int result = cmd.ExecuteNonQuery();
                //if (result > 0)
                //{
                //    cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", conn);
                //    cmd.Parameters.AddWithValue("@id", appliedJobID);
                //    int r = cmd.ExecuteNonQuery();
                //    if (r > 0)
                //    {
                //        lblMsg.Text = "Job application deleted successfully !";
                //        lblMsg.CssClass = "alert alert-success";
                //        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //    }
                //    else
                //    {
                //        lblMsg.Text = "Couldn't delete this job application, please try again later !";
                //        lblMsg.CssClass = "alert alert-danger";
                //        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //    }
                //}
                //else
                //{
                //    lblMsg.Text = "Couldn't delete this job application  please try again later !";
                //    lblMsg.CssClass = "alert alert-danger";
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //}
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

        private void AcceptApplication(int appliedJobID)
        {
            try
            {
                conn = new SqlConnection(str);
                conn.Open();

                // Update JobApplicationResp
                cmd = new SqlCommand("Update JobApplicationResp set Response='Accepted' where AppliedJobId=@appliedJobID", conn);
                cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                int respResult = cmd.ExecuteNonQuery();

                // Update JobApplicationHistory
                if (respResult > 0)
                {
                    cmd = new SqlCommand("Update JobApplicationHistory set Response='Accepted',ReplyDate=@rDate where AppliedJobId=@appliedJobID", conn);
                    cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                    cmd.Parameters.AddWithValue("@rDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    int historyResult = cmd.ExecuteNonQuery();

                    if (historyResult > 0)
                    {
                        lblMsg.Text = "Job application accepted!";
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "alert alert-success";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                    else
                    {
                        lblMsg.Text = "Couldn't update the job application history status, please try again later!";
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
                else
                {
                    lblMsg.Text = "Couldn't update this job application status, please try again later!";
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "alert alert-danger";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }

                //conn = new SqlConnection(str);
                //conn.Open();
                //cmd = new SqlCommand("Update JobApplicationResp set Response='Accepted' where AppliedJobId=@appliedJobID", conn);
                //cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                //int result = cmd.ExecuteNonQuery();
                //if (result > 0)
                //{
                //    lblMsg.Text = "Job application accepted!";
                //    lblMsg.Visible=true;
                //    lblMsg.CssClass = "alert alert-success";
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //}
                //else
                //{
                //    lblMsg.Text = "Couldn't update this job application status, please try again later!";
                //    lblMsg.Visible=true;
                //    lblMsg.CssClass = "alert alert-danger";
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //}
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

        protected void btnAccept_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnAccept = (ImageButton)sender;
            int appliedJobID = Convert.ToInt32(btnAccept.CommandArgument);
            try
            {
                conn = new SqlConnection(str);
                conn.Open();

                // Update JobApplicationResp
                cmd = new SqlCommand("Update JobApplicationResp set Response='Accepted' where AppliedJobId=@appliedJobID", conn);
                cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                int respResult = cmd.ExecuteNonQuery();

                // Update JobApplicationHistory
                if (respResult > 0)
                {
                    cmd = new SqlCommand("Update JobApplicationHistory set Response='Accepted',ReplyDate=@rDate where AppliedJobId=@appliedJobID", conn);
                    cmd.Parameters.AddWithValue("@appliedJobID", appliedJobID);
                    cmd.Parameters.AddWithValue("@rDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    int historyResult = cmd.ExecuteNonQuery();

                    if (historyResult > 0)
                    {
                        lblMsg.Text = "Job application accepted!";
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "alert alert-success";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                    else
                    {
                        lblMsg.Text = "Couldn't update the job application history status, please try again later!";
                        lblMsg.Visible = true;
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
                else
                {
                    lblMsg.Text = "Couldn't update this job application status, please try again later!";
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "alert alert-danger";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }

                //conn = new SqlConnection(str);
                //conn.Open();
                //cmd = new SqlCommand("Update JobApplicationResp set Response='Accepted' where AppliedJobId=@Id", conn);
                //cmd.Parameters.AddWithValue("@Id", appliedJobID);
                //int result = cmd.ExecuteNonQuery();
                //if (result > 0)
                //{
                //    lblMsg.Text = "Job application accepted successfully !";
                //    lblMsg.CssClass = "alert alert-success";
                //    btnAccept.Enabled = false; 
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //}
                //else
                //{
                //    lblMsg.Text = "Couldn't update this job application status, please try again later !";
                //    lblMsg.CssClass = "alert alert-danger";
                //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                //}
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
                    if (GridView1.Columns[i].HeaderText != "Resume" &&
                        GridView1.Columns[i].HeaderText != "Accept application" &&
                        GridView1.Columns[i].HeaderText != "Reject application")
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
                        if (GridView1.Columns[j].HeaderText != "Resume" &&
                            GridView1.Columns[j].HeaderText != "Accept application" &&
                            GridView1.Columns[j].HeaderText != "Reject application")
                        {
                            ws.Cells[i + 2, cellIndex + 1].Value = GridView1.Rows[i].Cells[j].Text;
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
                    Response.AddHeader("content-disposition", "attachment; filename=JobApplicationsList.xlsx");
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filterKeyword = txtFilter.Text.Trim();
            filter(filterKeyword);
        }

        private void filter(string filterKeyword)
        {
            string query = string.Empty;
            conn = new SqlConnection(str);

            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],aj.AppliedJobId,aj.UserId,j.CompanyName,aj.jobId,j.Title,u.Mobile,u.Username,u.Name,u.Email,u.Resume from AppliedJobs aj
                      inner join [User] u on aj.UserId = u.UserId
                      inner join Jobs j on aj.JobId = j.jobId
                      WHERE aj.AppliedJobId LIKE @Keyword OR aj.UserId LIKE @Keyword OR j.CompanyName LIKE @Keyword OR aj.jobId LIKE @Keyword OR j.Title LIKE @Keyword OR u.Mobile LIKE @Keyword
                       OR u.Username LIKE @Keyword OR u.Name LIKE @Keyword OR u.Email LIKE @Keyword";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", "%" + filterKeyword + "%");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}