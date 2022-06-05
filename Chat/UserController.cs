using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat
{
    public class UserController
    {

        public static List<User> searchUsers(string id, string username)
        {
            //TO DO: improve to be more intelligent
            var reader = DataBaseConnection.ExecuteReader($"call search_user_by_id_or_username(" +
                $"'{id}'" +
                $",'{username}" +
            $"')");
            var users = new List<User>();
            while (reader.Read()) { 
                //TO DO: wait for User implementation
                users.Add(new User(reader.GetString(0),reader.GetString(1)));
            }
            DataBaseConnection.Close();
            return users;
        }

    }
}