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
    public class BootcampTechnologyTests
    {
        private MockRepository mock = new MockRepository();
        private IBootcampTechnologyRepository repo = new BootcampTechnologyRepositorySql();

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
        public void GetAllBootcampTechnologiesTest()
        {
            Assert.AreEqual(repo.GetAllBootcampTechnologies().Count, 2);
        }

        [Test]
        public void GetBootcampTechnologyByIdTest()
        {
            Assert.AreEqual(repo.GetBootcampTechnologyById(1).BootcampID, 1);
        }

        [Test]
        public void GetAllBootcampTechnologiesByBootcampIdTest()
        {
            Assert.AreEqual(repo.GetAllBootcampTechnologiesByBootcampId(1).Count, 1);
        }

        [Test]
        public void GetAllBootcampTechnologiesByTechnologyIdTest()
        {
            Assert.AreEqual(repo.GetAllBootcampTechnologiesByTechnologyId(3).Count, 1);
        }

        [Test]
        public void AddBootcampTechnologyTest()
        {
            var beforeCount = repo.GetAllBootcampTechnologies().Count;
            repo.AddBootcampTechnology(mock.mockBootcampTechnology);
            var afterCount = repo.GetAllBootcampTechnologies().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void DeleteBootcampTechnologyTest()
        {
            var beforeCount = repo.GetAllBootcampTechnologies().Count;
            repo.DeleteBootcampTechnology(1);
            var afterCount = repo.GetAllBootcampTechnologies().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
