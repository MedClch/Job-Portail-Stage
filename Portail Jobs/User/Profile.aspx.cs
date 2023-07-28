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
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        DataTable dt;
        SqlDataAdapter adapter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"]!=null)
                Response.Redirect("../Admin/Dashboard.aspx");
            if (Session["user"]==null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
                showUserProfile();
        }

        private void showUserProfile()
        {
            conn = new SqlConnection(str);
            string query = "Select UserId,Username,Name,Address,Mobile,Email,Country,Resume from [User] where Username=@Username";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", Session["user"]);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dlProfile.DataSource = dt;
            dlProfile.DataBind();
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
                Response.Redirect("ResumeBuild.aspx?id="+e.CommandArgument.ToString());
        }
    }
}