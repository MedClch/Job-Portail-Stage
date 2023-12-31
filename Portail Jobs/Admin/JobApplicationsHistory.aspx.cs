﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using OfficeOpenXml;

namespace Portail_Jobs.Admin
{
    public partial class JobApplicationsHistory : System.Web.UI.Page
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
                showApplicationHisotry();
        }

        private void showApplicationHisotry()
        {
            string query = string.Empty;
            conn = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],AppliedJobId,CompanyName,jobId,Title,Mobile,Name,Email,Resume,Response 
                            from JobApplicationHistory";
            //query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],jah.AppliedJobId,j.CompanyName,jah.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume,jar.Response 
            //                from JobApplicationHistory jah
            //                inner join JobApplicationResp jar on jar.AppliedJobId = jah.AppliedJobId
            //                inner join [User] u on jah.UserId = u.UserId
            //                inner join Jobs j on jah.JobId = j.jobId";
            cmd = new SqlCommand(query, conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null)
                {
                    string response = rowView["Response"].ToString();
                    if (response == "Accepted")
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                    }
                    else if (response == "Rejected")
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    }
                    else if (response == "Pending")
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Gray;
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {       
            GridView1.PageIndex=e.NewPageIndex;
            showApplicationHisotry();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Sheet1");
                int columnIndex = 1;
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (GridView1.Columns[i].HeaderText != "Resume")
                    {
                        ws.Cells[1, columnIndex].Value = GridView1.Columns[i].HeaderText;
                        columnIndex++;
                    }
                }
                DataTable allData = FetchAllData();

                // toutes les pages 
                for (int i = 0; i < allData.Rows.Count; i++)
                {
                    var row = allData.Rows[i];
                    columnIndex = 1;
                    for (int j = 0; j < allData.Columns.Count; j++)
                    {
                        if (allData.Columns[j].ColumnName != "Resume")
                        {
                            ws.Cells[i + 2, columnIndex].Value = row[j];
                            columnIndex++;
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    package.SaveAs(ms);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=JobApplicationsHistory.xlsx");
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }

        private DataTable FetchAllData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                string query = @"SELECT jah.AppliedJobId,j.CompanyName,j.Title,u.Name,u.Email,u.Mobile,jar.Response from JobApplicationHistory jah
                            inner join JobApplicationResp jar on jar.AppliedJobId = jah.AppliedJobId
                            inner join [User] u on jah.UserId = u.UserId
                            inner join Jobs j on jah.JobId = j.jobId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
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
            query = @"SELECT Row_Number() over(Order by (Select 1)) as [Sr.No],jah.AppliedJobId,j.CompanyName,jah.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume,jar.Response 
                FROM JobApplicationHistory jah
                INNER JOIN JobApplicationResp jar ON jar.AppliedJobId = jah.AppliedJobId
                INNER JOIN [User] u ON jah.UserId = u.UserId
                INNER JOIN Jobs j ON jah.JobId = j.jobId
                WHERE j.CompanyName LIKE @Keyword OR j.Title LIKE @Keyword OR u.Name LIKE @Keyword OR u.Email LIKE @Keyword OR u.Mobile LIKE @Keyword OR jar.Response LIKE @Keyword";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", "%" + filterKeyword + "%");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void ExportToExcel() 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Sheet1");
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1].Value = GridView1.Columns[i].HeaderText;
                }
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1].Value = GridView1.Rows[i].Cells[j].Text;
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    package.SaveAs(ms);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=JobApplicationsHistory.xlsx");
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex=e.NewPageIndex;
        //    showApplicationHisotry();
        //}
    }
}