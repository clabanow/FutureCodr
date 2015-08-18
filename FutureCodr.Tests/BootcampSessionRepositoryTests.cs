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
    public class BootcampSessionRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private IBootcampSessionRepository repo = new BootcampSessionRepositorySql();

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
        public void GetBootcampSessionbyIdTest()
        {
            var result = repo.GetBootcampSessionById(2).StartDate;
            DateTime date = new DateTime(2014, 6, 1);

            Assert.AreEqual(date, result);
        }

        [Test]
        public void GetAllBootcampSessionsTest()
        {
            var result = repo.GetAllBootcampSessions().Count;

            Assert.AreEqual(result, 13);
        }

        [Test]
        public void GetBootcampSessionsByBootcampIdTest()
        {
            var result = repo.GetBootcampSessionsByBootcampId(1).Count;

            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddBootcampSessionTest()
        {
            var beforeCount = repo.GetAllBootcampSessions().Count;
            repo.AddBootcampSession(mock.mockBootcampSession);
            var afterCount = repo.GetAllBootcampSessions().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void EditBootcampSessionTest()
        {
            repo.EditBootcampSession(mock.mockBootcampSession);
            var result = repo.GetBootcampSessionById(mock.mockBootcampSession.BootcampSessionID);

            Assert.AreEqual(mock.mockBootcampSession, result.TechnologyID);
        }

        [Test]
        public void DeleteBootcampSessionTest()
        {
            int beforeCount = repo.GetAllBootcampSessions().Count;
            repo.DeleteBootcampSession(2);
            int afterCount = repo.GetAllBootcampSessions().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
