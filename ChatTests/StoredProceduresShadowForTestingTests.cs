using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Chat.Tests
{

    [TestClass()]
    public class StoredProceduresShadowForTestingTests
    {
        
        void execute_cmd_and_assert(string cmd)
        {
            var reader =  DataBaseConnection.ExecuteReader(cmd);
            Assert.IsNotNull(reader);
            DataBaseConnection.Close();
        }

        [TestMethod()]
        [DataRow("A3a4cC","xXXFya")]
        [DataRow("A3a4cC", "3aC34b")]
        [DataRow("3aC34b", "A3a4cC")]
        public void block_friend_ship_Test(string user_1,string user_2) //DONE
        {
            var result = DataBaseConnection.ExecuteNonQuery($"call block_friend_ship(" +
                $"'{user_1}'" +
                $",'{user_2}" +
            $"')");
            DataBaseConnection.Close();
            Assert.IsTrue(result>0);
        }
        [TestMethod()]
        [
            DataRow("A3a4cC", "k1YWfi0OO2yhJMlXFoeb")
        ]
        public void delete_friend_ship_Test(string user_1, string user_2) //DONE
        {
            execute_cmd_and_assert($"call delete_friend_ship(" +
                $"'{user_1}'" +
                $",'{user_2}" +
            $"')");
        }
        [TestMethod()]
        [

            DataRow("A3a4cC", "3aC34b", "2022-05-25 14:47:34")
        ]
        public void delete_message_for_all_Test(string user_1, string user_2, string timestamp) //DONE
        {
            execute_cmd_and_assert($"call delete_message_for_all(" +
                $"'{user_1}'" +
                $",'{user_2}'" +
                $",'{timestamp}" +
            $"')");
        }
        [TestMethod()]
        [
            DataRow("A3a4cC", "3aC34b", "2022-05-25 14:47:34","mensaje editado")
        ]
        public void edit_message_Test(string user_1, string user_2, string timestamp, string message) //ALMOST DONE
        {
            //Don't allow to edit a message that is already deleted (is_for_origin_deleted=1 and is_for_destination_deleted)
            execute_cmd_and_assert($"call edit_message(" +
                $"'{user_1}" +
                $"','{user_2}" +
                $"','{timestamp}" +
                $"','{message}" +
            $"')");
        }
        [TestMethod()]
        [DataRow("A3a4cC",8)]
        [DataRow("3aC34b", 9)]
        public void get_friendships_messages_Test(string user_id,int expected_messages) //DONE
        {
            var reader = DataBaseConnection.ExecuteReader($"call get_friendships_messages('{user_id}')");
            //1° assert
            Assert.IsNotNull(reader);
            //2° assert
            int actual_messages = 0;
            for (; reader.Read(); actual_messages++) ;
            DataBaseConnection.Close();
            Assert.AreEqual(expected_messages, actual_messages);
 
        }
        [TestMethod()]
        [
            DataRow("A3a4cC")
        ]
        public void get_received_friendship_invites_Test(string user_id) //DONE
        {
            execute_cmd_and_assert($"call get_received_friendship_invites('{user_id}')");
        }
        [TestMethod()]
        [
            DataRow("Hola!", "A3a4cC", "3aC34b")
        ]
        public void insert_message_Test(string message, string from_user, string to_user) //DONE
        {
            execute_cmd_and_assert($"call insert_message('{message}','{from_user}','{to_user}')");
        }
        [TestMethod()]
        [
            DataRow("A3a4cC","chucho")
        ]
        public void search_user_by_id_or_username_Test(string user_id, string username) //DONE
        {
            execute_cmd_and_assert($"call search_user_by_id_or_username('{user_id}','{username}')");
        }
        [TestMethod()]
        [
            DataRow("xXXFya", "3aC34b")
        ]
        public void send_friend_invite_Test(string my_id, string candidate_id) //ALMOST DONE
        {
            //Precondition: DELETE all table contents
            DataBaseConnection.ExecuteNonQuery("delete from friendship_request");
            execute_cmd_and_assert($"call send_friend_invite('{my_id}','{candidate_id}')");
            DataBaseConnection.Close();
            //TO DO: If my_id has blocked the candidate_id in friendship table don't allow the insert to friendship_request
        }
        [TestMethod()]
        [DataRow("3aC34b", "A3a4cC",true)]
        [DataRow("xXXFya", "A3a4cC",false)]
        [DataRow("", "", false)]
        public void does_this_friendship_invite_exist_and_not_blocked_Test(
            string origin_user
            ,string destination_user
            ,bool expected_exists_and_not_blocked
        ){
            Assert.AreEqual(
                expected_exists_and_not_blocked
                , Convert.ToBoolean(
                    DataBaseConnection.ExecuteScalar(
                        $"call does_this_friendship_invite_exist_and_not_blocked(" +
                            $"'{origin_user}'" +
                            $",'{destination_user}'" +
                        $")"
                    )
                )
            );
        }


        [TestMethod()]
        [DataRow("3aC34b", "A3a4cC", false)]
        public void delete_friendship_invite_Test(
            string origin_user
            , string destination_user
            , bool expected_deleted
        )
        {
            Assert.AreEqual(
                expected_deleted
                , Convert.ToBoolean(
                    DataBaseConnection.ExecuteScalar(
                        $"call delete_friendship_invite(" +
                            $"'{origin_user}'" +
                            $",'{destination_user}'" +
                        $")"
                    )
                )
            );
        }
        [TestMethod()]
        [DataRow("vTbGUM8DmXiDkHCfQ1pa","Eu3mu47mjYqx5BnLtanC")]
        public void create_friendship_Test(string user_a, string user_b)
        {
            Assert.IsTrue(
                DataBaseConnection.ExecuteNonQuery(
                $"call create_friendship(" +
                    $"'{user_a}'" +
                    $",'{user_b}'" +
                $")"
                ) > 0
            );
        }
        [TestMethod()]
        [DataRow("Mll66O5uRTXCt6JPKYQE", "Eu3mu47mjYqx5BnLtanC")]
        public void block_friend_ship_invite_Test(string user_a, string user_b)
        {
            Assert.IsTrue(
                DataBaseConnection.ExecuteNonQuery(
                $"call block_friend_ship_invite(" +
                    $"'{user_a}'" +
                    $",'{user_b}'" +
                $")"
                ) > 0
            );
        }
    }
}