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
    class LocationTests
    {
        private MockRepository mock = new MockRepository();
        private ILocationRepository _locationRepo = new LocationRepositorySql();

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
        public void GetAllLocationsTest()
        {
            Assert.AreEqual(_locationRepo.GetAllLocations().Count, 4);
        }

        [Test]
        public void GetLocationByIdTest()
        {
            Assert.AreEqual(_locationRepo.GetLocationById(4).City, "Toronto");
        }

        [Test]
        public void AddLocationTest()
        {
            Assert.AreEqual(_locationRepo.AddLocation(mock.mockLocation).LocationID, 5);
        }

        [Test]
        public void DeleteLocationTest()
        {
            var beforeCount = _locationRepo.GetAllLocations().Count;
            _locationRepo.DeleteLocation(1);
            
            Assert.AreEqual(beforeCount - 1, _locationRepo.GetAllLocations().Count);
        }
    }
}
