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
    class BootcampLocationsTests
    {
        private MockRepository mockRepo = new MockRepository();
        private IBootcampLocationsRepository _locationsRepo = new BootcampLocationsRepositorySql();

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
        public void GetAllBootcampLocationsTest()
        {
            Assert.AreEqual(_locationsRepo.GetAllBootcampLocations().Count, 3);
        }

        [Test]
        public void GetBootcampLocationByIdTest()
        {
            Assert.AreEqual(_locationsRepo.GetBootcampLocationById(1).LocationID, 1);
        }

        [Test]
        public void AddBootcampTest()
        {
            Assert.AreEqual(_locationsRepo.AddBootcampLocation(mockRepo.MockBootcampLocation).BootcampLocationID, 4);
        }

        [Test]
        public void DeleteBootcampTest()
        {
            var beforeCount = _locationsRepo.GetAllBootcampLocations().Count;
            _locationsRepo.DeleteBootcampLocation(1);
            var afterCount = _locationsRepo.GetAllBootcampLocations().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
