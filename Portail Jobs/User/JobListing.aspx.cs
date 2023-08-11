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
    public partial class JobListing : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;
        SqlDataAdapter adapter;
        public int jobCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        void RBSelectedColorChange()
        {
            if(RadioButtonList1.SelectedItem.Selected == true)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }

        private void showJobList()
        {
            if (dt==null)
            {
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs";
                cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            lbljobCount.Text = JobCount(dt.Rows.Count);
        }

        string JobCount(int count)
        {
            if (count>1)
            {
                return "Total <b> "+count+" </b> jobs found.";
            }
            else if (count==1)
            {
                return "Total <b> "+count+" </b> job found.";
            }
            else
            {
                return "No jobs found !";
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCountry.SelectedValue != "0")
            {
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where Country='"+ddlCountry.SelectedValue+"'";
                cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);  
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";
            if(string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.png";
            }
            else
            {
                url1 = string.Format("~/{0}",url);
            }
            return ResolveUrl(url1);
        }

        public static string RelativeDate(DateTime theDate)
        {
            return Utils.GetRelativeDate(theDate);
        }

        protected void jobTypeCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobType = string.Empty;
            jobType = selectedCheckBox();
            if (jobType!="")
            {
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where JobType IN ("+jobType+")";
                cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }
        }

        string selectedCheckBox()
        {
            string jobType = string.Empty;
            for(int i = 0; i < jobTypeCheckBox.Items.Count; i++)
            {
                if (jobTypeCheckBox.Items[i].Selected)
                {
                    jobType += "'"+jobTypeCheckBox.Items[i].Text+"',";
                }
            }
            return jobType.TrimEnd(',');
            //return jobType = jobType.TrimEnd(',');
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue!="0")
            {
                string postedDate = string.Empty;
                postedDate = selectedRadioButton();
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where Convert(DATE,CreateDate) "+postedDate+" ";
                cmd = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;
            if(RadioButtonList1.SelectedValue == "1")
            {
                postedDate = "= Convert(DATE,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                postedDate = "between Convert(DATE,'"+DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd")+"') and Convert(DATE,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                postedDate = "between Convert(DATE,'"+DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd")+"') and Convert(DATE,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            else if (RadioButtonList1.SelectedValue == "4")
            {
                postedDate = "between Convert(DATE,'"+DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd")+"') and Convert(DATE,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            else
            {
                postedDate = "between Convert(DATE,'"+DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd")+"') and Convert(DATE,'"+date.ToString("yyyy/MM/dd")+"')";
            }
            return postedDate;
        }

        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCondition = false;
                string subQuery = string.Empty;
                string jobType = string.Empty;
                string postedDate = string.Empty;
                string addAnd = string.Empty;
                string query = string.Empty;
                List<string> queryList = new List<string>();
                conn = new SqlConnection(str);
                if(ddlCountry.SelectedValue != "0")
                {
                    queryList.Add(" Country = '"+ddlCountry.SelectedValue+"'");
                    isCondition = true;
                }
                jobType = selectedCheckBox();
                if(jobType != "")
                {
                    queryList.Add(" JobType IN ("+jobType+") ");
                    isCondition = true;
                }
                if (RadioButtonList1.SelectedValue!="0")
                {
                    postedDate = selectedRadioButton();
                    queryList.Add(" Convert(DATE,CreateDate) "+postedDate);
                    isCondition = true;
                }
                if (isCondition)
                {
                    foreach(string s in queryList)
                    {
                        subQuery += s + " and ";
                    }
                    subQuery = subQuery.Remove(subQuery.LastIndexOf("and"), 3);
                    query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where "+subQuery+" ";
                }
                else
                {
                    query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs";
                }
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.ClearSelection();
            jobTypeCheckBox.ClearSelection();
            RadioButtonList1.SelectedValue = "0";
            RBSelectedColorChange();
            showJobList();
        }
    }
}