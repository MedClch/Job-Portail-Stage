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
            if (Session["admin"]==null)
                Response.Redirect("../User/Login.aspx");
        }

        protected void btnAddJob_Click(object sender, EventArgs e)
        {
            try
            {
                string concatQuery, imagePath = string.Empty;
                bool isValidToExecute = false;
                conn = new SqlConnection(str);
                query=@"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateToApply,
                        @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
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
                    if (isValidExtension(fuCompanyLogo.FileName))
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
                    cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                    isValidToExecute = true;
                }
                if(isValidToExecute)
                {
                    conn.Open();
                    int res = cmd.ExecuteNonQuery();
                    if(res > 0)
                    {
                        lblMsg.Text="Job saved successfully !";
                        lblMsg.CssClass="alert alert-success";
                        clear();
                    }
                    else
                    {
                        lblMsg.Text="Couldn't save infromations, please try again later !";
                        lblMsg.CssClass="alert alert-success";
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            finally 
            {
                conn.Close();
            }
        }

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

        private bool isValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".jpg", ".png", ".jpeg" };
            for(int i = 0; i <= fileExtension.Length-1; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
    }
}