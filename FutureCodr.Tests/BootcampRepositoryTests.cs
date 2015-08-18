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
    public class BootcampRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private IBootcampRepository repo = new BootcampRepositorySql();

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
        public void GetAllBootcampsTest()
        {
            var result = repo.GetAllBootcamps();

            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void GetBootcampById()
        {
            var result = repo.GetBootcampByID(1).Name;

            Assert.AreEqual(result, "Hack Reactor");
        }

        [Test]
        public void AddBootcampTest()
        {
            var beforeCount = repo.GetAllBootcamps().Count;
            repo.AddBootcamp(mock.mockBootcamp);
            var afterCount = repo.GetAllBootcamps().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void EditBootcampTest()
        {
            repo.EditBootcamp(mock.mockBootcamp);
            var result = repo.GetBootcampByID(1).Name;

            Assert.AreEqual("App Academy", result);
        }

        [Test]
        public void DeleteBootcamp()
        {
            var beforeCount = repo.GetAllBootcamps().Count;
            repo.DeleteBootcamp(2);
            var afterCount = repo.GetAllBootcamps().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
