using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

namespace Chat.Tests
{
    [TestClass()]
    public class FriendshipTests
    {
        [TestMethod()]
        [
            DataRow("3aC34b", "A3a4cC")
            ,DataRow("CasHByUWfeDR6uuVaBD4","4QrCmt9iDhNNnsYNySLI")
        ]
        public void FriendshipTest(string my_id, string friend_id)
        {
            var message_and_source = (new Message("", new MySqlDateTime(DateTime.Now)),Friendship.Source.me);
            var friendship = new Friendship(my_id, friend_id, new List<(Message, Friendship.Source)> { message_and_source });
            Assert.AreEqual(my_id, friendship.my_id);
            Assert.AreEqual(friend_id,friendship.friend_id);
            //Assert.AreEqual(message_and_source, friendship.messages);
        }
    }
}