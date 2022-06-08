using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;


namespace Chat
{
    public class User
    {
        public string id { get; }
        public string username { get; }
        public User(string id, string username)
        {
            this.id = id;
            this.username = username;
        }
        public List<Friendship> getFriendships()
        {
            //output data structure
            List<Friendship> filled_friendships = new List<Friendship>();

            //support data structure
            var friends_messages = new Dictionary<string, List<(Message message, Friendship.Source source)>>();
            var reader = DataBaseConnection.ExecuteReader($"call get_friendships_messages('{id}')");
            while (reader.Read())
            {
                var friend_id = reader.GetString(0);
                //handle if not already in the dictionary
                if (!friends_messages.ContainsKey(friend_id))
                    friends_messages[friend_id] = new List<(Message message, Friendship.Source source)>();
                try
                {
                    friends_messages[friend_id].Add(
                        (
                            new Message(
                                reader.GetString(2)
                                , reader.GetMySqlDateTime(3)
                            )
                            , (Friendship.Source)reader.GetUInt16(1)
                        )
                    );
                }
                catch { }
            }
            DataBaseConnection.Close();
            foreach (var friend_messages in friends_messages)
            {
                filled_friendships.Add(
                    new Friendship(
                        id
                        , friend_messages.Key
                        , friend_messages.Value
                    )
                );
            }
            return filled_friendships;
        }

        public bool blockFriendship(Friendship friendship)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call block_friend_ship(" +
                $"'{id}'" +
                $",'{friendship.friend_id}" +
            $"')");
            DataBaseConnection.Close();
            return result > 0;
        }

        public bool deleteFriendship(Friendship friendship)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call delete_friend_ship(" +
                $"'{id}'" +
                $",'{friendship.friend_id}')"
            );
            DataBaseConnection.Close();
            return result > 0;
        }
        public bool sendFriendInviteTo(string proposed_friend_id)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call send_friend_invite(" +
                $"'{id}'" +
                $",'{proposed_friend_id}')"
            );
            DataBaseConnection.Close();
            return result > 0;
        }
        public List<User> getFriendshipInvites()
        {
            var reader = DataBaseConnection.ExecuteReader($"call get_received_friendship_invites('{id}')");
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(
                    new User(
                        reader.GetString(0)
                        , reader.GetString(1)
                    )
                );
            }
            DataBaseConnection.Close();
            return users;
        }
        public bool acceptFriendshipInvite(User user_to_accept)
        {
            var parameters =
                $"'{id}'" +
                $",'{user_to_accept.id}'"
            ;
            var cmd = DataBaseConnection.createTransactionCommand();
            //1
            cmd.CommandText =
                $"call does_this_friendship_invite_exist_and_not_blocked(" +
                     parameters
                + $")"
            ;
            if (
                !Convert.ToBoolean(
                    cmd.ExecuteScalar()
                )
            )
                return false;
            //2
            cmd.CommandText =
                $"call create_friendship(" +
                    parameters
                + $")"
            ;
            if (cmd.ExecuteNonQuery() < 1)
                return false;
            //3
            cmd.CommandText =
                $"call delete_friendship_invite(" +
                    parameters
                + $")"
            ;
            if (cmd.ExecuteNonQuery() < 1)
                return false;
            DataBaseConnection.Commit();
            DataBaseConnection.Close();
            return true;
        }
        public bool blockFriendshipInvite(User user_to_block)
        {
            var parameters =
                $"'{id}'" +
                $",'{user_to_block.id}'"
            ;
            var result = DataBaseConnection.ExecuteNonQuery(
                $"block_friend_ship_invite(" +
                    parameters
                + $")");
            DataBaseConnection.Close();
            return result > 0;
        }
        public List<(Message message, Friendship.Source source)> getFriendShipMessages(string user_id)
        {

            var messages = new List<(Message message, Friendship.Source source)>();
            var reader = DataBaseConnection.ExecuteReader($"call get_friendship_messages('{id}','{user_id}')");
            //query returns the following:
            //source [0]
            //text [1]
            //timestamp [2]
            while (reader.Read())
            {
                var friend_id = reader.GetString(0);
                //handle if not already in the dictionary
                {
                    messages.Add(
                        (
                            new Message(
                                reader.GetString(1)
                                , reader.GetMySqlDateTime(2)
                            )
                            , (Friendship.Source)reader.GetUInt16(0)
                        )
                    );
                }
            }
            DataBaseConnection.Close();
            return messages;
        }
    }
}