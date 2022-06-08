using MySql.Data.Types;
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
            string friendId = (string) Session["friend_id"];
            FriendName.Text = (string)Session["username"];
            if ( friendId != null)
            {
                List<(Message m, Friendship.Source s)> messages = user.getFriendShipMessages(friendId);
                var plain_messages = messages.Select(m=>m.m) ;
                MessageGrid.DataSource = plain_messages;
                MessageGrid.DataBind();
            }
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            var user = SessionInfo.getLoggedInUser(Session);
            var cur_date = new MySqlDateTime(DateTime.Now);
            Message newMessage = new Message(TextBox1.Text, cur_date);
            if(newMessage.post(user.id, (string) Session["friend_id"]))
                Response.Redirect("Chat.aspx");
        }
    }
}