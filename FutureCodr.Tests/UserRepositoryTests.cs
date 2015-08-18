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
    public class UserRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private IUserRepository repo = new UserRepositorySql();

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
        public void GetAllUsersTest()
        {
            var result = repo.GetAllUsers().Count;

            Assert.AreEqual(result, 2);
        }

        [Test]
        public void GetUserByIdTest()
        {
            var result = repo.GetUserById(1).Email;

            Assert.AreEqual("clabanow@gmail.com", result);
        }

        [Test]
        public void AddUserTest()
        {
            var beforeCount = repo.GetAllUsers().Count;
            repo.AddUser(mock.mockUser);
            var afterCount = repo.GetAllUsers().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void EditUserTest()
        {
            repo.EditUser(mock.mockUser);
            var result = repo.GetUserById(mock.mockUser.UserID).Email;

            Assert.AreEqual(result, mock.mockUser.Email);
        }

        [Test]
        public void DeleteUserTest()
        {
            var beforeCount = repo.GetAllUsers().Count;
            repo.DeleteUser(1);
            var afterCount = repo.GetAllUsers().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
