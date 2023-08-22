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
            if (Save(source, e))
            { 
                if (Session["user"]!=null)
                {
                    try
                    {
                        int idU = Convert.ToInt32(Session["userId"]);
                        conn = new SqlConnection(str);
                        string respQuery = @"Insert into JobApplicationResp values (@AppliedJobId, @JobId, @UserId, 'Pending')";
                        cmd = new SqlCommand(respQuery, conn);
                        cmd.Parameters.AddWithValue("@AppliedJobId", GetLatestInsertedId("AppliedJobs"));
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        conn.Open();
                        int respResult = cmd.ExecuteNonQuery();
                        conn.Close();
                        // Insert into JobApplicationHistory
                        string historyQuery = @"Insert into JobApplicationHistory values (@AppliedJobId, @JobId, @UserId, @Title,@Name,@CompanyName,@Email,@Mobile,@Resume, 'Pending')";
                        cmd = new SqlCommand(historyQuery, conn);
                        cmd.Parameters.AddWithValue("@AppliedJobId", GetLatestInsertedId("AppliedJobs"));
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        cmd.Parameters.AddWithValue("@Title", GetTitleForJob(Convert.ToInt32(Request.QueryString["id"])));
                        cmd.Parameters.AddWithValue("@Name", GetNameForUser(idU));
                        cmd.Parameters.AddWithValue("@CompanyName", GetCompanyNameForJob(Convert.ToInt32(Request.QueryString["id"])));
                        cmd.Parameters.AddWithValue("@Email", GetEmailForUser(idU));
                        cmd.Parameters.AddWithValue("@Mobile",GetMobileForUser(idU));
                        cmd.Parameters.AddWithValue("@Resume",GetResumeForUser(idU));
                        conn.Open();
                        int historyResult = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (respResult > 0 && historyResult > 0)
                        {
                            // Successful insertion
                            lblMsg.Visible = true;
                            lblMsg.Text = "Job application sent successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                            showJobDetails();
                        }
                        //string query = @"Insert into JobApplicationResp values (@AppliedJobId,@JobId,@UserId,'Pending')";
                        //cmd = new SqlCommand(query, conn);
                        //cmd.Parameters.AddWithValue("@AppliedJobId", GetLatestInsertedId("AppliedJobs"));
                        //cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        //cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        //conn.Open();
                        //int res = cmd.ExecuteNonQuery();
                        //if (res > 0)
                        //{
                        //    lblMsg.Visible = true;
                        //    lblMsg.Text = "Job application sent successfully !";
                        //    lblMsg.CssClass = "alert alert-success";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        //    showJobDetails();
                        //}
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Couldn't apply for this job, please try again !";
                            lblMsg.CssClass = "alert alert-danger";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
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
            //if(e.CommandName=="ApplyJob")
            //{
            //    if (Session["user"]!=null)
            //    {
            //        try
            //        {
            //            conn = new SqlConnection(str);
            //            string query = @"Insert into AppliedJobs values (@JobId,@UserId)";
            //            cmd = new SqlCommand(query, conn);
            //            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
            //            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            //            conn.Open();
            //            int res = cmd.ExecuteNonQuery();
            //            if (res > 0)
            //            {
            //                lblMsg.Visible = true;
            //                lblMsg.Text = "Job application sent successfully !";
            //                lblMsg.CssClass = "alert alert-success";
            //                showJobDetails();
            //            }
            //            else
            //            {
            //                lblMsg.Visible = true;
            //                lblMsg.Text = "Couldn't apply for this job, please try again !";
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
            //        Response.Redirect("Login.aspx");
            //    }
            //}
        }

        private bool Save(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName=="ApplyJob")
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                    return false;
                }
            }
            return false;
        }

        public int GetLatestInsertedId(string tableName)
        {
            int latestId = -1; // Initialize with a default value

            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();
                    string query = $"SELECT IDENT_CURRENT('{tableName}') AS LatestId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            latestId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                // For debugging, you can output the exception message
                Console.WriteLine("Error: " + ex.Message);
            }

            return latestId;
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

        private string GetCompanyNameForJob(int jobId)
        {
            string companyName = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT CompanyName FROM Jobs WHERE JobId = @JobId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobId", jobId);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            companyName = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                }
            }
            return companyName;
        }

        private string GetTitleForJob(int jobId)
        {
            string title = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT Title FROM Jobs WHERE JobId = @JobId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JobId", jobId);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            title = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                }
            }
            return title;
        }

        private string GetMobileForUser(int id)
        {
            string title = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT Mobile FROM [User] WHERE UserId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            title = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + " mobile ');</script>");
                    }
                }
            }
            return title;
        }

        private string GetNameForUser(int id)
        {
            string title = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT Name FROM [User] WHERE UserId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            title = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + " name ');</script>");
                    }
                }
            }
            return title;
        }

        private string GetEmailForUser(int id)
        {
            string title = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT Email FROM [User] WHERE UserId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            title = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + " email ');</script>");
                    }
                }
            }
            return title;
        }

        private string GetResumeForUser(int id)
        {
            string title = string.Empty;
            using (SqlConnection connection = new SqlConnection(str))
            {
                string query = "SELECT Resume FROM [User] WHERE UserId = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            title = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + " resume ');</script>");
                    }
                }
            }
            return title;
        }
    }
}