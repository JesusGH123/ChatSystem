using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Chat
{
    class FriendshipGUIRow
    {
        public string id { get; }
        public string username { get; }
        public string last_message_preview { get; }

        public FriendshipGUIRow(string id, string username, string last_message_preview)
        {
            this.id = id;
            this.username = username;
            this.last_message_preview = last_message_preview;
        }
    }
  
    public partial class Index : System.Web.UI.Page
    {
        void getAndRenderMyFriendships()
        {
            MessageLabel.Text = "Loading chats";
            try
            {
                var friendships = new List<Friendship>();
                User user = SessionInfo.getLoggedInUser(Session);
                friendships = user.getFriendships();

                if (friendships.Count == 0)
                {
                    MessageLabel.Text = "You have no chats!";
                }
                else
                {
                    MessageGrid.DataSource = friendships.Select(
                        friendship
                        =>
                        {
                            var delete_button = new Button();
                            var block_button = new Button();
                            var messages = friendship.messages;
                            var last_message = messages.Count == 0 ? "" : messages.Last().message.content;
                            return new FriendshipGUIRow(
                                friendship.friend_id
                                , UserController.searchUsers(friendship.friend_id, "")[0].username
                                , last_message.Substring(0, Math.Min(30, last_message.Length))
                             );
                        }
                    );
                    MessageGrid.DataBind();
                    MessageLabel.Text = "";
                }

            }
            catch
            {
                MessageLabel.Text = "Error getting chats ):";
            }
        }

        void getAndRenderRequests()
        {
            try
            {
                var requests = new List<User>();
                User user = SessionInfo.getLoggedInUser(Session);
                requests = user.getFriendshipInvites();

                if (requests.Count == 0)
                    RequestLabel.Text = "You have no requests";
                else
                {
                    RequestGrid.DataSource = requests;
                }
                RequestGrid.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                RequestLabel.Text = "Error in requests";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var user = SessionInfo.getLoggedInUser(Session);
                if (user == null)
                    Response.Redirect("Login.aspx");
                //username_label.Text = user.username;
                getAndRenderMyFriendships();
                getAndRenderRequests();
            }
        }

        protected void btnLogout_click(object sender, EventArgs e)
        {
            Auth.logout(Response, Session);
        }

        protected void delete_click(object sender, EventArgs e)
        {
            var grid_view_row = (GridViewRow)((Button)sender).NamingContainer;
            if (grid_view_row.RowType == DataControlRowType.DataRow)
            {
                string friend_id = ((Label)grid_view_row.FindControl("id")).Text;
                var I = SessionInfo.getLoggedInUser(Session);
                I.deleteFriendship(new Friendship(I.id, friend_id, null));
                Response.Redirect("Index.aspx");
            }
        }
        protected void block_click(object sender, EventArgs e)
        {
            var grid_view_row = (GridViewRow)((Button)sender).NamingContainer;
            if (grid_view_row.RowType == DataControlRowType.DataRow)
            {
                string friend_id = ((Label)grid_view_row.FindControl("id")).Text;
                var I = SessionInfo.getLoggedInUser(Session);
                I.blockFriendship(new Friendship(I.id, friend_id, null));
                Response.Redirect("Index.aspx");
            }
        }

        protected void search_click(object sender, EventArgs e)
        {
            //Check if the friend is already added
            var I = SessionInfo.getLoggedInUser(Session);

            if (System.Convert.ToBoolean(I.sendFriendInviteTo(userSearchtxt.Text)))
            {
                userSearchtxt.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Request sent successfully')", true);
            }
            else
            {
                userSearchtxt.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User is already your friend')", true);
            }
        }

        protected void RequestGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var I = SessionInfo.getLoggedInUser(Session);
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow grid_view_row = RequestGrid.Rows[index];

            string friend_id;
            //Check if the friend is already added
            if (grid_view_row.RowType == DataControlRowType.DataRow)
            {
                friend_id = ((Label)grid_view_row.FindControl("id")).Text;
                var friend = new User(friend_id, "");

                switch (e.CommandName)
                {
                    case "Accept":
                        I.acceptFriendshipInvite(friend);
                        break;
                    case "Block":
                        I.blockFriendshipInvite(friend);
                        break;
                }   
            }

            Response.Redirect("https://localhost:44313/Index");
        }

    }
}