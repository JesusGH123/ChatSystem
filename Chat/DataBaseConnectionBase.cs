using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Chat
{
    public class DataBaseConnectionBase
    {
        static MySqlConnection conn;
        static string myConnectionString;
        static MySqlTransaction transaction;

        public DataBaseConnectionBase(string server, string uid, string database, string password)
        {
            var password_pattern = password == null ? "" : $"pwd={password}";
            myConnectionString = $"server={server};uid={uid};database={database};{password_pattern}";
        }

        public MySqlCommand createTransactionCommand()
        {
            OpenIfNotAlready();
            transaction = conn.BeginTransaction();
            var cmd = conn.CreateCommand();
            cmd.Transaction = transaction;
            return cmd;
        }
        public void Commit()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }
        MySqlConnection OpenIfNotAlready()
        {

            conn = new MySqlConnection(myConnectionString);
            conn.ConnectionString = myConnectionString;
            conn.Open();
            return conn;

        }
        MySqlCommand createCommand(string cmd)
        {
            OpenIfNotAlready();
            var cmd_ = conn.CreateCommand();
            cmd_.CommandText = cmd;
            return cmd_;
        }
        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }
        public enum Command {
            NonQuery
            ,Reader
            ,Scalar
        }
        public int ExecuteNonQuery(string cmd)
        {
            var result = createCommand(cmd).ExecuteNonQuery();
            return result;
        }
        public MySqlDataReader ExecuteReader(string cmd)
        {
            var result = createCommand(cmd).ExecuteReader();
            return result;
        }
        public object ExecuteScalar(string cmd)
        {
            var result = createCommand(cmd).ExecuteScalar();
            return result;
        }
        /*public bool ExecuteTransaction(List<(string cmd, Command type,Func<object,bool>handler)> commands)
        {
            conn = new MySqlConnection(myConnectionString);
            conn.Open();
            var command = conn.CreateCommand();
            var trans = conn.BeginTransaction();
            command.Transaction = trans;            
            foreach (var c in commands)
            {
                command.CommandText = c.cmd;
                var type = c.type;
                var handler = c.handler;
                var result = type == Command.NonQuery ? command.ExecuteNonQuery() : type == Command.Reader ? command.ExecuteNonQuery() : command.ExecuteNonQuery();
                if (!handler(result))
                    return false;
            }
            trans.Commit();
            conn.Close();
            return true;
        }*/
    }
}