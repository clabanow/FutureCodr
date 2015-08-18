using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureCodr.Data;
using FutureCodr.Data.Interfaces;
using FutureCodr.Data.MockRepository;
using FutureCodr.Data.Repositories;
using FutureCodr.Data.Repositories.Sql;
using NUnit.Framework;

namespace FutureCodr.Tests
{
    [TestFixture]
    public class LinkRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private ILinkRepository repo = new LinkRepositorySql();

        [SetUp]
        public void SetUpDB()
        {
            using (var connection = new SqlConnection(Settings.GetConnectionString()))
            {
                var command = new SqlCommand("DBReset", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void GetAllLinksTest()
        {
            var result = repo.GetAllLinks().Count;

            Assert.AreEqual(result, 2);
        }

        [Test]
        public void GetAllLinksByBootcampIdTest()
        {
            var result = repo.GetAllLinksByBootcampId(1).Count;

            Assert.AreEqual(1, result);
        }

        [Test]
        public void AddLinkTest()
        {
            var beforeCount = repo.GetAllLinks().Count;
            repo.AddLink(mock.mockLink);
            var afterCount = repo.GetAllLinks().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void DeleteLinkTest()
        {
            var beforeCount = repo.GetAllLinks().Count;
            repo.DeleteLink(1);
            var afterCount = repo.GetAllLinks().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
