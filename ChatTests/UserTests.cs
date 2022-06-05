using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;


namespace Chat.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        [DataRow("A3a4cC"
            , new string[]
            {
                "abc3da"
                ,"xXXFya"
                ,"D0TjB5"
                ,"3aC34b"
            }
            , new int[]
            {
                0
                ,0
                ,0
                ,0
            }
        )]
        [DataRow("3aC34b",
            new string[] {
                "CasHByUWfeDR6uuVaBD4"
                ,"vTbGUM8DmXiDkHCfQ1pa"
                ,"jkwKHybp2BTmEsIfrCoS"
                ,"hiJ4gl92lHhExsMbqPxC"
                ,"3aC34b"
            }
            , new int[]
            {
                0
                ,0
                ,0
                ,0
                ,0
            }
        )]
        [DataRow(""
            , new string[] {

            }
            , new int[] {

            })
        ]
        public void getFriendshipsTest(string my_id, string[] friend_ids, int[] message_counts_with_each_friend)
        {
            //pre-condition
            Assert.AreEqual(friend_ids.Length, message_counts_with_each_friend.Length);
            var expected_friendships_count = friend_ids.Length;
            var user = new User(my_id, "");
            var friend_ships = user.getFriendships();
            //pos-condition
            Assert.IsNotNull(friend_ships);
            Assert.AreEqual(expected_friendships_count, friend_ships.Count);
        }

        [TestMethod()]
        [DataRow("3aC34b", "A3a4cC")]
        public void deleteFriendshipTest(string user_a, string user_b)
        {
            var user = new User(user_a, "");
            Assert.IsTrue(
                user.deleteFriendship(new Friendship(user_a, user_b, new List<(Message message, Friendship.Source source)> { }))
            );
        }

        [TestMethod()]
        [DataRow("A3a4cC", "3aC34b")]
        public void sendFriendInviteToTest(string origin_id, string destination_id)
        {
            var user = new User(origin_id, "");
            Assert.IsTrue(
                user.sendFriendInviteTo(destination_id)
            ); ;
        }

        [TestMethod()]
        [DataRow("3aC34b", "A3a4cC")]
        public void blockFriendshipTest(string user_a, string user_b)
        {
            var user = new User(user_a, "");
            Assert.IsTrue(
                user.blockFriendship(new Friendship(user_a, user_b, new List<(Message message, Friendship.Source source)> { }))
            );
        }

        [TestMethod()]
        [DataRow("A3a4cC", new string[]
        {
            "3aC34b"
            ,"xXXFya"
        }
        , new string[]{
            "misa"
            ,"Username"
        }
        )]
        public void getFriendshipInvitesTest(
            string in_id
            , string[] expected_out_ids
            , string[] expected_out_usernames)
        {
            //pre-test
            Assert.AreEqual(expected_out_ids.Length, expected_out_usernames.Length);
            var expected_invite_count = expected_out_ids.Length;
            var out_id_to_expected_out_username = expected_out_ids.Select(
                (id, ind) => new { ind, id }
            ).ToDictionary(
                pair => pair.id
                , pair => expected_out_usernames[pair.ind]
            );
            //test
            int validated_friendship_invites = 0;
            new User(in_id, "").getFriendshipInvites().ForEach(user_that_invited =>
            {
                var id = user_that_invited.id;
                Assert.IsTrue(out_id_to_expected_out_username.ContainsKey(id));
                Assert.IsTrue(
                    out_id_to_expected_out_username[id]
                    ==
                    user_that_invited.username
                );
                validated_friendship_invites++;
            });
            Assert.AreEqual(expected_invite_count, validated_friendship_invites);
        }

        [TestMethod()]
        [DataRow("xXXFya", "A3a4cC", false)] //is_blocked case
        [DataRow("vTbGUM8DmXiDkHCfQ1pa", "jkwKHybp2BTmEsIfrCoS", true)] //friendship created case
        public void acceptFriendshipInviteTest(string user_1, string user_2, bool expected_result)
        {
            Assert.AreEqual(
                expected_result
                , new User(user_1, "").acceptFriendshipInvite(
                    new User(user_2, "")
                )
            );
        }

        [TestMethod()]
        [DataRow("vTbGUM8DmXiDkHCfQ1pa", "Eu3mu47mjYqx5BnLtanC",true)]
        [DataRow("vTbGUM8DmXiDkHCfQ1pa", "jkwKHybp2BTmEsIfrCoS", true)]
        public void blockFriendshipInviteTest(string user_1, string user_2, bool expected_result)
        {
            Assert.AreEqual(
                expected_result
                , new User(user_1, "").blockFriendshipInvite(
                    new User(user_2, "")
                )
            );
        }
    }
}