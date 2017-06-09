using System.Collections.Generic;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using cb.Enums;
using cb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cb.Tests
{
    [TestClass]
    public class TestPost
    {
        [TestMethod]
        public void TestGetAllPosts()
        {
            var context = new PostSqlContext();
            var pr = new PostRepository(context);


            UserSqlContext usercontext = new UserSqlContext();
            UserRepository ur = new UserRepository(usercontext);

            User user = ur.GetGebruikerById(1);


            //Post p = new Post(user,"Me irl",Category.Classic);
            //pr.CreatePost(p);
        }

        [TestMethod]
        public void TestGetPostById()
        {
            var context = new PostSqlContext();
            var pr = new PostRepository(context);


            var post = pr.GetPostById(2);

            Assert.IsNotNull(post);
        }

        [TestMethod]
        public void TestGetTrending()
        {
            var context = new PostSqlContext();
            var pr = new PostRepository(context);

            List<Post> posts = pr.GetTrendingPosts(0);

            Assert.AreEqual(4, posts[0].Id);
        }
    }
}