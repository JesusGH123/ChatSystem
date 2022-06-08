using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chat
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = SessionInfo.getLoggedInUser(Session);
            if (user == null)
                Response.Redirect("Login.aspx");
            //getAndRenderMessages();
            string friendId = Request.QueryString["friend"];
            if ( friendId != null)
            {
                //Need one friendship
            }
        }
    }
}