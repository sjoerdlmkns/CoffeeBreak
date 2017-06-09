using System.Collections.Generic;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using cb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cb.Tests
{
    [TestClass]
    public class TestComment
    {
        [TestMethod]
        public void TestGetPostById()
        {
            var context = new CommentSqlContext();
            var cr = new CommentRepository(context);


            List<Comment> comments = cr.GetCommentsByPostId(2);

            Assert.AreEqual(0,comments.Count);
        }
    }
}