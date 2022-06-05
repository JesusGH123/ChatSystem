using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Chat
{
    public static class DataBaseConnection
    {
        public readonly static DataBaseConnectionBase DataBaseConnection_ = new DataBaseConnectionBase("localhost", "root", "chat", null);
        public static void Close()
        {
            DataBaseConnection_.Close();
        }
        public static int ExecuteNonQuery(string cmd)
        {
            var result = DataBaseConnection_.ExecuteNonQuery(cmd);
            return result;
        }
        public static MySqlCommand createTransactionCommand()
        {
            return DataBaseConnection_.createTransactionCommand();
        }
        public static void Commit()
        {
            DataBaseConnection_.Commit();
        }
        public static MySqlDataReader ExecuteReader(string cmd)
        {
            var result = DataBaseConnection_.ExecuteReader(cmd);
            return result;
        }
        public static object ExecuteScalar(string cmd)
        {
            var result = DataBaseConnection_.ExecuteScalar(cmd);
            return result;
        }
        /*public static bool ExecuteTransaction(List<(string cmd, DataBaseConnectionBase.Command type, Func<object,bool> handler)> commands)
        {
            return DataBaseConnection_.ExecuteTransaction(commands);
        }*/
    }
}