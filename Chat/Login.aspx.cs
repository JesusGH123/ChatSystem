using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chat
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var id = id_textbox.Text;
            var username = Auth.login(Response, Session, id, password_textbox.Text);
            if (username == null)
                msg_label.Text = "Username or Password are incorrect!";
                
        }
    }
}