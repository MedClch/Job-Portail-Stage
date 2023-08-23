using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]!=null)
                Response.Redirect("../User/Default.aspx");
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
            if (Session["admin"]==null && Session["user"]==null)
                Response.Redirect("../User/Login.aspx");
            Session["title"]="Add job";
            if (!IsPostBack)
                fillData();
        }

        private void fillData()
        {
            if (Request.QueryString["id"]!=null)
            {
                conn = new SqlConnection(str);
                query = "Select * from Jobs where JobId = '"+Request.QueryString["id"]+"'";
                cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtJobTitle.Text = reader["Title"].ToString();
                        txtNbPost.Text = reader["NoOfPost"].ToString();
                        txtJobDesc.Text = reader["Description"].ToString();
                        txtQualiEdu.Text = reader["Qualification"].ToString();
                        txtExperience.Text = reader["Experience"].ToString();
                        txtSpecialization.Text = reader["Specialization"].ToString();
                        txtLastDate.Text = Convert.ToDateTime(reader["LastDateToApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = reader["Salary"].ToString();
                        ddlJobType.SelectedValue = reader["JobType"].ToString();
                        txtCompanyName.Text = reader["CompanyName"].ToString();
                        txtWebsite.Text = reader["Website"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        ddlCountry.SelectedValue = reader["Country"].ToString();
                        txtState.Text = reader["State"].ToString();
                        btnAddJob.Text = "Edit job";
                        linkBack.Visible=true;
                        Session["title"]="Edit job";
                    }
                }
                else
                {
                    lblMsg.Text="Job not found !";
                    lblMsg.CssClass="alert alert-danger";
                }
                reader.Close();
                conn.Close();
            }
        }

        protected void btnAddJob_Click(object sender, EventArgs e)
        {
            try
            {
                string concatQuery = string.Empty;
                string imagePath = string.Empty;
                string type = string.Empty;
                bool isValidToExecute = false;
                conn = new SqlConnection(str);
                if (Request.QueryString["id"]!=null)
                {
                    int jobId = Convert.ToInt32(Request.QueryString["id"]);
                    string newTitle = txtJobTitle.Text.Trim();
                    if(IsTitleAvailable(newTitle,jobId))
                    {
                        if (fuCompanyLogo.HasFile)
                        {
                            if (Utils.isValidExtension(fuCompanyLogo.FileName))
                            {
                                concatQuery = "CompanyImage=@CompanyImage,";
                            }
                            else
                            {
                                concatQuery = string.Empty;
                            }
                        }
                        else
                        {
                            concatQuery = string.Empty;
                        }
                        query = @"Update Jobs set Title=@Title,NoOfPost=@NoOfPost,Description=@Description,Qualification=@Qualification,Experience=@Experience,Specialization=@Specialization,
                            LastDateToApply=@LastDateToApply,Salary=@Salary,JobType=@JobType,CompanyName=@CompanyName,"+concatQuery+@"Website=@Website,Email=@Email,Address=@Address,
                            Country=@Country,State=@State where JobId=@id";
                        type = "updated";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoOfPost", txtNbPost.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtJobDesc.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qualification", txtQualiEdu.Text.Trim());
                        cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                        cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                        cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                        cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                        cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                        if (fuCompanyLogo.HasFile)
                        {
                            if (Utils.isValidExtension(fuCompanyLogo.FileName))
                            {
                                Guid obj = Guid.NewGuid();
                                imagePath = "Images/"+obj.ToString()+fuCompanyLogo.FileName;
                                fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/")+obj.ToString()+fuCompanyLogo.FileName);
                                cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                                isValidToExecute = true;
                            }
                            else
                            {
                                lblMsg.Text="Please select .jpg, .jpeg, .png file for logo !";
                                lblMsg.CssClass="alert alert-danger";
                            }
                        }
                        else
                        {
                            isValidToExecute = true;
                        }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtJobTitle.Text.Trim() + "<b> already exists, please try again!";
                        lblMsg.CssClass = "alert alert-danger";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
                else
                {
                    query =@"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateToApply,
                            @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
                    type = "saved";
                    DateTime time = DateTime.Now;
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPost", txtNbPost.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtJobDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@Qualification", txtQualiEdu.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreateDate", time.ToString("dd-MM-yyyy HH:mm:ss"));
                    if (fuCompanyLogo.HasFile)
                    {
                        if (Utils.isValidExtension(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "Images/"+obj.ToString()+fuCompanyLogo.FileName;
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/")+obj.ToString()+fuCompanyLogo.FileName);
                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblMsg.Text="Please select .jpg, .jpeg, .png file for logo !";
                            lblMsg.CssClass="alert alert-danger";
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        isValidToExecute = true;
                    }
                }
                if (isValidToExecute)
                {
                    conn.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Text="Job "+type+" successfully !";
                        lblMsg.CssClass="alert alert-success";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                        if (type.Equals("updated"))
                        {
                            Response.Redirect("../Admin/JobList.aspx");
                        }
                        else
                        {
                            clear();
                        }

                    }
                    else
                    {
                        lblMsg.Text="Couldn't save infromations, please try again later !";
                        lblMsg.CssClass="alert alert-success";
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number==2627)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtJobTitle.Text.Trim() + "<b> already exists, please try again!";
                    lblMsg.CssClass = "alert alert-danger";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            catch (Exception ex1)
            {
                Response.Write("<script>alert('"+ex1.Message+"');</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        private bool IsTitleAvailable(string Jtitle, int currentId)
        {
            bool isAvailable = true;
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Jobs WHERE Title = @title AND JobId != @CurrentId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", Jtitle);
                    cmd.Parameters.AddWithValue("@CurrentId", currentId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        isAvailable = false;
                    }
                }
            }
            return isAvailable;
        }

        //protected void btnAddJob_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string type, concatQuery, imagePath = string.Empty;
        //        bool isValidToExecute = false;
        //        conn = new SqlConnection(str);
        //        if (Request.QueryString["id"]!=null)
        //        {
        //            if (fuCompanyLogo.HasFile)
        //            {
        //                if (Utils.isValidExtension(fuCompanyLogo.FileName))
        //                {
        //                    concatQuery = "CompanyImage=@CompanyImage,";
        //                }
        //                else
        //                {
        //                    concatQuery = string.Empty;
        //                }
        //            }
        //            else
        //            {
        //                concatQuery = string.Empty;
        //            }
        //            query = @"Update Jobs set Title=@Title,NoOfPost=@NoOfPost,Description=@Description,Qualification=@Qualification,Experience=@Experience,Specialization=@Specialization,
        //                    LastDateToApply=@LastDateToApply,Salary=@Salary,JobType=@JobType,CompanyName=@CompanyName,"+concatQuery+@"Website=@Website,Email=@Email,Address=@Address,
        //                    Country=@Country,State=@State where JobId=@id";
        //            type = "updated";
        //            cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NoOfPost", txtNbPost.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Description", txtJobDesc.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Qualification", txtQualiEdu.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
        //            cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
        //            cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
        //            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
        //            cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
        //            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
        //            if (fuCompanyLogo.HasFile)
        //            {
        //                if (Utils.isValidExtension(fuCompanyLogo.FileName))
        //                {
        //                    Guid obj = Guid.NewGuid();
        //                    imagePath = "Images/"+obj.ToString()+fuCompanyLogo.FileName;
        //                    fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/")+obj.ToString()+fuCompanyLogo.FileName);
        //                    cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
        //                    isValidToExecute = true;
        //                }
        //                else
        //                {
        //                    lblMsg.Text="Please select .jpg, .jpeg, .png file for logo !";
        //                    lblMsg.CssClass="alert alert-danger";
        //                }
        //            }
        //            else
        //            {
        //                isValidToExecute = true;
        //            }
        //        }
        //        else
        //        {
        //            query =@"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateToApply,
        //                    @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
        //            type = "saved";
        //            DateTime time = DateTime.Now;
        //            cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NoOfPost", txtNbPost.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Description", txtJobDesc.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Qualification", txtQualiEdu.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
        //            cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
        //            cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
        //            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
        //            cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
        //            cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
        //            cmd.Parameters.AddWithValue("@CreateDate", time.ToString("dd-MM-yyyy HH:mm:ss"));
        //            if (fuCompanyLogo.HasFile)
        //            {
        //                if (Utils.isValidExtension(fuCompanyLogo.FileName))
        //                {
        //                    Guid obj = Guid.NewGuid();
        //                    imagePath = "Images/"+obj.ToString()+fuCompanyLogo.FileName;
        //                    fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/")+obj.ToString()+fuCompanyLogo.FileName);
        //                    cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
        //                    isValidToExecute = true;
        //                }
        //                else
        //                {
        //                    lblMsg.Text="Please select .jpg, .jpeg, .png file for logo !";
        //                    lblMsg.CssClass="alert alert-danger";
        //                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
        //                }
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
        //                isValidToExecute = true;
        //            }
        //        }
        //        if (isValidToExecute)
        //        {
        //            conn.Open();
        //            int res = cmd.ExecuteNonQuery();
        //            if (res > 0)
        //            {
        //                lblMsg.Text="Job "+type+" successfully !";
        //                lblMsg.CssClass="alert alert-success";
        //                ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
        //                if (type.Equals("updated"))
        //                {
        //                    Response.Redirect("../Admin/JobList.aspx");
        //                }
        //                else
        //                {
        //                    clear();
        //                }

        //            }
        //            else
        //            {
        //                lblMsg.Text="Couldn't save infromations, please try again later !";
        //                lblMsg.CssClass="alert alert-success";
        //                ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('"+ex.Message+"');</script>");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private void clear()
        {
            txtJobTitle.Text=string.Empty;
            txtNbPost.Text=string.Empty;
            txtJobDesc.Text=string.Empty;
            txtEmail.Text=string.Empty;
            txtAddress.Text=string.Empty;
            txtCompanyName.Text=string.Empty;
            txtLastDate.Text=string.Empty;
            txtQualiEdu.Text=string.Empty;
            txtSalary.Text=string.Empty;
            txtSpecialization.Text=string.Empty;
            txtWebsite.Text=string.Empty;
            txtSalary.Text = string.Empty;
            txtExperience.Text=string.Empty;
            txtState.Text=string.Empty;
            ddlCountry.ClearSelection();
            ddlJobType.ClearSelection();
        }
    }
}