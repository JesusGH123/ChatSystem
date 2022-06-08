using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat
{
    public class Message
    {
        public string content { get; set; }
        public MySqlDateTime date_time {  get; }

        public Message(string content, MySqlDateTime date_time)
        {
            this.content = content;
            this.date_time = date_time;
        }

        public bool post(string user_id, string friend_id)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call insert_message(" +
                $"'{content}'" +
                $",'{user_id}'" +
                $",'{friend_id}')"
            );
            DataBaseConnection.Close();
            return result > 0;
        }

        public bool edit(string user_id, string friend_id,int ord)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call edit_message('" +
                $"{user_id}'" +
                $",'{friend_id}'" +
                $",'{ord}'" +
                $",'{content}')"
            );
            DataBaseConnection.Close();
            return result > 0;
        }

        public bool deleteForAll(string user_a, string user_b)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call delete_message_for_all('" +
                $"{user_a}'" +
                $",'{user_b}'" +
                $",{date_time})"
            );
            DataBaseConnection.Close();
            return result > 0;
        }
        public bool deleteForUserId(string who_deletes_id, string other_id)
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call delete_message_for_single('" +
                $"{who_deletes_id}'" +
                $",'{other_id}'" +
                $",{date_time}" +
                $")");
            DataBaseConnection.Close();
            return result > 0;
        }
    }
}