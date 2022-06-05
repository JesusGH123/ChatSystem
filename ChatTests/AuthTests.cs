using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chat.Tests
{
    [TestClass()]
    public class AuthTests
    {
        [TestMethod()]
        [DataRow("Mike Muñoz", "abc")]
        public void registerTest(string username, string password)
        {
            Console.WriteLine($"username: {username} password: {password}");
            //Test that auto generated id is of length 20
            Assert.AreEqual(Auth.register(username, password).Length, 20);
        }

        [TestMethod()]
        [DataRow("abc3da", "aaa","test")]
        [DataRow("abc3da", "aaaa",null)]
        [DataRow("abc3d", "aaa", null)]
        [DataRow("3aC34b","a",null)]
        public void loginTest(string id, string password, string username)
        {
            Assert.AreEqual(Auth.login(null,null,id, password),username);
        }
    }
}