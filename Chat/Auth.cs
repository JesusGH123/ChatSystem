using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Chat
{
    public static class Auth
    {
        public static string login(HttpResponse Response, HttpSessionState Session, string id, string password)
        {
            string username = null;
            try
            {
                username = (string)DataBaseConnection.ExecuteScalar($"select username from user where id = '{id}' and password = '{password}'");
                if (username != null)
                {
                    Session["logged_user"] = new User(id, username);
                    Response.Redirect("Index.aspx");
                }
            }
            catch
            {
            }
            DataBaseConnection.Close();
            return username;
        }

        public static bool logout(HttpResponse Response, HttpSessionState Session)
        {
            bool was_successful = false;
            try
            {
                Session.Remove("logged_user");
                Response.Redirect("Login.aspx");
                was_successful = true;
            }
            catch
            {

            }
            return was_successful;
        }
        static Random random = new Random();
        static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string register(string username, string password)
        {
            var id = RandomString(20);
            var result = DataBaseConnection.ExecuteNonQuery($"insert into user (" +
                $"username" +
                $",id" +
                $",password" +
            $") values(" +
                $"'{username}'" +
                $",'{id}'" +
                $",'{password}'" +
             $")");
            DataBaseConnection.Close();
            if (result < 1)
                return null;
            return id;
        }
        
    }
}