using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml;
using System.Drawing;

namespace Portail_Jobs.Admin
{
    public partial class ContactList : System.Web.UI.Page
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
                showContacts();
        }

        private void showContacts()
        {
            string query = string.Empty;
            conn = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],ContactId,Name,Email,Subject,Message from Contact";
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
            showContacts();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int contactID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                conn = new SqlConnection(str);
                cmd = new SqlCommand("Delete from Contact where ContactId = @id", conn);
                cmd.Parameters.AddWithValue("@id", contactID);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text="Contact deleted successfully !";
                    lblMsg.CssClass="alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                else
                {
                    lblMsg.Text="Couldn't delete this contact, please try again later !";
                    lblMsg.CssClass="alert alert-success";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + lblMsg.ClientID + "').style.display = 'none'; }, 4500);", true);
                }
                //conn.Close();
                GridView1.EditIndex = -1;
                showContacts();
            }
            catch (Exception ex)
            {
                //conn.Close();
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            finally
            {
                conn.Close();
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
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No],ContactId,Name,Email,Subject,Message from Contact
                WHERE Name LIKE @Keyword OR Email LIKE @Keyword OR Subject LIKE @Keyword OR Message LIKE @Keyword";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", "%" + filterKeyword + "%");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
                    if (GridView1.Columns[i].HeaderText != "Delete")
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
                        if (allData.Columns[j].ColumnName != "Delete")
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
                    Response.AddHeader("content-disposition", "attachment; filename=ContactList.xlsx");
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
                string query = @"SELECT ContactId,Name,Email,Subject,Message from Contact";
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
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

        protected void btnReply_Click(object sender, EventArgs e)
        {
            ImageButton btnReply = (ImageButton)sender;
            string subject = "Reply to your message about : "+ btnReply.CommandArgument;
            Response.Redirect($"mailto:?subject={subject}");

            //Response.Redirect($"mailto:?subject={Server.UrlEncode(subject)}");

            //Button btnReply = (Button)sender;
            //string subject = btnReply.CommandArgument;

            //// Redirect to your mailbox with the subject as the object of the email
            //// You'll need to implement this part based on your email system.
            //Response.Redirect($"mailto:?subject={Server.UrlEncode(subject)}");
        }

        protected void btnReply_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Reply")
            {
                string[] args = e.CommandArgument.ToString().Split('&');
                string recipientEmail = args[0];
                string subject = args[1];
                string mailtoLink = $"mailto:{recipientEmail}?subject={Uri.EscapeDataString(subject)}";
                Response.Redirect(mailtoLink);
            }
        }
    }
}