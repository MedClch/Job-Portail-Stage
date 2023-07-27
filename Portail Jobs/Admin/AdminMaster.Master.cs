using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portail_Jobs.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("../User/Default.aspx");
        }

        protected void btnNewJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Admin/NewJob.aspx");
        }

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Admin/Dashboard.aspx");
        }

        protected void btnJobList_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Admin/JobList.aspx");
        }

        protected void btnViewResumes_Click(object sender, EventArgs e)
        {

        }

        protected void btnContactList_Click(object sender, EventArgs e)
        {

        }
    }
}