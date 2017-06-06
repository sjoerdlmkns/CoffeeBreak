using System.Collections.Generic;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using cb.Enums;
using cb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cb.Tests
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void GetUserById()
        {
           UserSqlContext context = new UserSqlContext();
           UserRepository ur = new UserRepository(context);

           User usertje =  ur.GetGebruikerById(1);

           Assert.IsNotNull(usertje);
           Assert.AreEqual("Sjoerd",usertje.Username);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            UserSqlContext context = new UserSqlContext();
            UserRepository ur = new UserRepository(context);


            List<User> users = ur.GetAllUsers();

            Assert.AreEqual(1,users.Count);
        }

        [TestMethod]
        public void AuthenticateUser()
        {
            UserSqlContext context = new UserSqlContext();
            UserRepository ur = new UserRepository(context);


            bool isAuth = ur.AuthUser("Sjoerd", "Fiets123");

            Assert.AreEqual(true, isAuth);
        }

        [TestMethod]
        public void GetUserIdByName()
        {
            UserSqlContext context = new UserSqlContext();
            UserRepository ur = new UserRepository(context);


            int userid = ur.GetUserIdByUsername("Sjoerd");

            Assert.AreEqual(1, userid);
        }

        [TestMethod]
        public void CreateUser()
        {
            UserSqlContext context = new UserSqlContext();
            UserRepository ur = new UserRepository(context);

            var user = new User("pietje12", "Fiets123", Gender.Male);
            
            ur.CreateUser(user);
        }
    }
}