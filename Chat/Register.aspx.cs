using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chat
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_button_Click(object sender, EventArgs e)
        {
            var username = username_input.Text;
            var id = Auth.register(username, password_input.Text);

            if (username == null)
                msg_label.Text = "Error! Could not register.";
            else
                if (Auth.login(Response, Session, id, username) == null)
                    msg_label.Text = "Error in login";
        }
    }
}