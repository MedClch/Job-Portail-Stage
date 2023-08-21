using System;
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
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],jah.AppliedJobId,j.CompanyName,jah.jobId,j.Title,u.Mobile,u.Name,u.Email,u.Resume,jar.Response 
                            from JobApplicationHistory jah
                            inner join JobApplicationResp jar on jar.AppliedJobId = jah.AppliedJobId
                            inner join [User] u on jah.UserId = u.UserId
                            inner join Jobs j on jah.JobId = j.jobId";
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
                // Find the index of the "Response" column
                int responseColumnIndex = GridView1.Columns.Cast<DataControlField>().ToList().FindIndex(field => field.HeaderText == "Application response");

                // Make sure the index is valid
                if (responseColumnIndex >= 0)
                {
                    // Get the cell value in the "Response" column for the current row
                    string responseValue = e.Row.Cells[responseColumnIndex].Text.Trim();

                    // Set the row's background color based on the response value
                    if (responseValue == "Accepted")
                    {
                        e.Row.BackColor = System.Drawing.Color.Green; // Green color for 'Accepted'
                    }
                    else if (responseValue == "Rejected")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red; // Red color for 'Rejected'
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
                // Adding headers
                int columnIndex = 1;
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (GridView1.Columns[i].HeaderText != "Resume") // Exclude the "Resume" column
                    {
                        ws.Cells[1, columnIndex].Value = GridView1.Columns[i].HeaderText;
                        columnIndex++;
                    }
                }
                DataTable allData = FetchAllData();

                // Adding data from all pages of the GridView
                for (int i = 0; i < allData.Rows.Count; i++)
                {
                    var row = allData.Rows[i];
                    columnIndex = 1;
                    for (int j = 0; j < allData.Columns.Count; j++)
                    {
                        if (allData.Columns[j].ColumnName != "Resume") // Exclude the "Resume" column
                        {
                            ws.Cells[i + 2, columnIndex].Value = row[j];
                            columnIndex++;
                        }
                    }
                }
                // Save the Excel package to a MemoryStream
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
                string query = @"SELECT jah.AppliedJobId,j.CompanyName,jah.jobId,j.Title,u.Mobile,u.Name,u.Email,jar.Response from JobApplicationHistory jah
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

        //protected void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using (var package = new ExcelPackage())
        //    {
        //        var ws = package.Workbook.Worksheets.Add("Sheet1");

        //        // Adding headers
        //        for (int i = 0; i < GridView1.Columns.Count; i++)
        //        {
        //            ws.Cells[1, i + 1].Value = GridView1.Columns[i].HeaderText;
        //        }

        //        // Adding data
        //        for (int i = 0; i < GridView1.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < GridView1.Columns.Count; j++)
        //            {
        //                ws.Cells[i + 2, j + 1].Value = GridView1.Rows[i].Cells[j].Text;
        //            }
        //        }

        //        // Save the Excel package to a MemoryStream
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            package.SaveAs(ms);

        //            Response.Clear();
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment; filename=JobApplicationsHistory.xlsx");
        //            ms.WriteTo(Response.OutputStream);
        //            Response.End();
        //        }
        //    }
        //}

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex=e.NewPageIndex;
        //    showApplicationHisotry();
        //}
    }
}