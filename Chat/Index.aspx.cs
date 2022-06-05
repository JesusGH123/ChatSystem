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
        public Button delete { get; }
        public Button block { get; }

        public FriendshipGUIRow(string id, string username, string last_message_preview, Button delete, Button block)
        {
            this.id = id;
            this.username = username;
            this.last_message_preview = last_message_preview;
            this.delete = delete;
            this.block = block;
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
                                , delete_button
                                , block_button
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

        protected void btnLogout_Click(object sender, EventArgs e)
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

        protected void MessageGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void buttonSearch_Click(object sender, EventArgs e)
        {
            var I = SessionInfo.getLoggedInUser(Session);
            I.sendFriendInviteTo(userSearchtxt.Text);
        }
    }
}