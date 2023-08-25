﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.User
{
    public partial class UserApplications : System.Web.UI.Page
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
            if (RadioButtonList1.SelectedItem.Selected == true)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }

        private void showJobList()
        {
            if (dt==null)
            {
                int idU = Convert.ToInt32(Session["userId"]);
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,Response,ReplyDate from JobApplicationHistory where UserId="+idU+"";
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

        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCountry.SelectedValue != "0")
        //    {
        //        conn = new SqlConnection(str);
        //        string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate from Jobs where Country='"+ddlCountry.SelectedValue+"'";
        //        cmd = new SqlCommand(query, conn);
        //        adapter = new SqlDataAdapter(cmd);
        //        dt = new DataTable();
        //        adapter.Fill(dt);
        //        showJobList();
        //        RBSelectedColorChange();
        //    }
        //    else
        //    {
        //        showJobList();
        //        RBSelectedColorChange();
        //    }
        //}

        protected void jobTypeCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobType = string.Empty;
            jobType = selectedCheckBox();
            if (jobType!="")
            {
                int idU = Convert.ToInt32(Session["userId"]);
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,ReplyDate,Response from JobApplicationHistory where JobType IN ("+jobType+") and UserId="+idU+"";
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
            for (int i = 0; i < jobTypeCheckBox.Items.Count; i++)
            {
                if (jobTypeCheckBox.Items[i].Selected)
                {
                    jobType += "'"+jobTypeCheckBox.Items[i].Text+"',";
                }
            }
            return jobType.TrimEnd(',');
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue!="0")
            {
                int idU = Convert.ToInt32(Session["userId"]);
                string postedDate = string.Empty;
                postedDate = selectedRadioButton();
                conn = new SqlConnection(str);
                string query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,ReplyDate,Response from JobApplicationHistory where Convert(DATE,ReplyDate) "+postedDate+" and UserId="+idU+"";
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
            if (RadioButtonList1.SelectedValue == "1")
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
                //if (ddlCountry.SelectedValue != "0")
                //{
                //    queryList.Add(" Country = '"+ddlCountry.SelectedValue+"'");
                //    isCondition = true;
                //}
                jobType = selectedCheckBox();
                if (jobType != "")
                {
                    queryList.Add(" JobType IN ("+jobType+") ");
                    isCondition = true;
                }
                if (RadioButtonList1.SelectedValue!="0")
                {
                    postedDate = selectedRadioButton();
                    queryList.Add(" Convert(DATE,ReplyDate) "+postedDate);
                    isCondition = true;
                }
                if (isCondition)
                {
                    foreach (string s in queryList)
                    {
                        subQuery += s + " and ";
                    }
                    subQuery = subQuery.Remove(subQuery.LastIndexOf("and"), 3);
                    query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,ReplyDate,Response from JobApplicationHistory where "+subQuery+" ";
                }
                else
                {
                    query = @"Select JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,ReplyDate,Response from JobApplicationHistory";
                }
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
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

        protected void lbReset_Click(object sender, EventArgs e)
        {
            //ddlCountry.ClearSelection();
            jobTypeCheckBox.ClearSelection();
            RadioButtonList1.SelectedValue = "0";
            AppResCheckBox.ClearSelection();
            RBSelectedColorChange();
            showJobList();
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

        public static string RelativeDate(DateTime theDate)
        {
            return Utils.GetRelativeDate(theDate);
        }

        protected void AppResCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectedResponses = new List<string>();

            for (int i = 0; i < AppResCheckBox.Items.Count; i++)
            {
                if (AppResCheckBox.Items[i].Selected)
                    selectedResponses.Add(AppResCheckBox.Items[i].Text);
            }

            if (selectedResponses.Count > 0)
            {
                int idU = Convert.ToInt32(Session["userId"]);
                conn = new SqlConnection(str);
                string responseParameters = string.Join(",", selectedResponses.Select((response, index) => "@Response" + index));
                string query = $@"SELECT JobId, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, Response, ReplyDate FROM JobApplicationHistory WHERE Response IN ({responseParameters}) AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    for (int i = 0; i < selectedResponses.Count; i++)
                        cmd.Parameters.AddWithValue("@Response" + i, selectedResponses[i]);
                    cmd.Parameters.AddWithValue("@UserId", idU);
                    adapter = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    // Set the DataTable as the DataSource for DataList
                    DataList1.DataSource = dt;
                    DataList1.DataBind();

                    // Modify CSS classes based on "Response" column
                    foreach (DataListItem item in DataList1.Items)
                    {
                        DataRowView rowView = item.DataItem as DataRowView;
                        if (rowView != null)
                        {
                            string response = rowView["Response"].ToString();
                            string cssClass = GetResponseCssClass(response);
                            item.CssClass = cssClass;
                        }
                    }

                    // Call this method to apply color changes to radiobuttons
                    RBSelectedColorChange();
                }
            }
            else
            {
                // No responses selected, show the default job list
                showJobList();
            }
        }

        private string GetResponseCssClass(string response)
        {
            if (response == "Accepted")
                return "accepted";
            else if (response == "Rejected")
                return "rejected";
            else if (response == "Pending")
                return "pending";
            else
                return string.Empty;
        }


        string selectedCheckBox1()
        {
            List<string> selectedResponses = new List<string>();
            for (int i = 0; i < AppResCheckBox.Items.Count; i++)
            {
                if (AppResCheckBox.Items[i].Selected)
                {
                    selectedResponses.Add("@Response" + i);
                }
            }
            return string.Join(",", selectedResponses);
        }


        //protected void AppResCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string resp = string.Empty;
        //    resp = selectedCheckBox1();
        //    if (resp!="")
        //    {
        //        int idU = Convert.ToInt32(Session["userId"]);
        //        conn = new SqlConnection(str);
        //        string query = @"Select j.JobId,j.Title,j.Salary,j.JobType,j.CompanyName,j.CompanyImage,j.Country,j.State,j.CreateDate from Jobs j 
        //                       inner join JobApplicationHistory jah on j.JobId = jah.jobId and Response='"+resp+"' and jah.UserId="+idU+"";
        //        cmd = new SqlCommand(query, conn);
        //        adapter = new SqlDataAdapter(cmd);
        //        dt = new DataTable();
        //        adapter.Fill(dt);
        //        showJobList();
        //        RBSelectedColorChange();
        //    }
        //    else
        //    {
        //        showJobList();
        //    }
        //}

        //string selectedCheckBox1()
        //{
        //    string jobType = string.Empty;
        //    for (int i = 0; i < AppResCheckBox.Items.Count; i++)
        //    {
        //        if (AppResCheckBox.Items[i].Selected)
        //        {
        //            jobType += "'"+AppResCheckBox.Items[i].Text+"',";
        //        }
        //    }
        //    return jobType.TrimEnd(',');
        //}
    }
}