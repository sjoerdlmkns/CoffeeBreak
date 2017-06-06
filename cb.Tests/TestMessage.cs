using System.Collections.Generic;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cb.Tests
{
    [TestClass]
    public class TestMessage
    {
        [TestMethod]
        public void TestGetAllBadwords()
        {
            MessageSqlContext context = new MessageSqlContext();
            MessageRepository mr = new MessageRepository(context);

            List<string> scheldwoorden = mr.GetAllBadWords();

            

            Assert.AreEqual(1,scheldwoorden.Count);
        }
    }
}