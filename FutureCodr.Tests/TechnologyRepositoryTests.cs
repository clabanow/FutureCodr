using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutureCodr.Data;
using FutureCodr.Data.Interfaces;
using FutureCodr.Data.MockRepository;
using FutureCodr.Data.Repositories.Sql;
using NUnit.Framework;

namespace FutureCodr.Tests
{
    [TestFixture]
    class TechnologyRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private ITechnologyRepository repo = new TechnologyRepositorySql();

        [SetUp]
        public void Setup()
        {
            using (var connection = new SqlConnection(Settings.GetConnectionString()))
            {
                var command = new SqlCommand("DBReset", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void GetAllTechnologiesTest()
        {
            Assert.AreEqual(repo.GetAllTechnologies().Count, 16);
        }

        [Test]
        public void GetTechnologyByIdTest()
        {
            Assert.AreEqual(repo.GetTechnologyById(1).Name, "Ruby on Rails");
        }

        [Test]
        public void AddTechnologyTest()
        {
            var beforeCount = repo.GetAllTechnologies().Count;
            repo.AddTechnology(mock.mockTechnology);
            var afterCount = repo.GetAllTechnologies().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void DeleteTechnologyTest()
        {
            var beforeCount = repo.GetAllTechnologies().Count;
            repo.DeleteTechnology(1);
            var afterCount = repo.GetAllTechnologies().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
