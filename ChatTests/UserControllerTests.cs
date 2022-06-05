using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        [DataRow("A3a4cC","")]
        [DataRow("", "Mike Muñoz")]
        [DataRow("","")]
        [DataRow("    ", "     ")]
        public void searchUsersTest(string id,string username)
        {
            var found_users = UserController.searchUsers(id, username);
            Assert.IsNotNull(found_users);
        }
    }
}