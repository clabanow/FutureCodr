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
    public class ContactFormRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private IContactFormRepository repo = new ContactFormRepositorySql();

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
        public void GetAllContactFormsTest()
        {
            Assert.AreEqual(repo.GetAllContactForms().Count, 3);
        }

        [Test]
        public void GetContactFormByIdTest()
        {
            Assert.AreEqual(repo.GetContactFormById(1).Email, "clabanow@gmail.com");
        }

        [Test]
        public void AddContactFormTest()
        {
            Assert.AreEqual(repo.AddContactForm(mock.MockContactForm).ContactFormID, 4);
        }

        [Test]
        public void DeleteContactFormTest()
        {
            var beforeCount = repo.GetAllContactForms().Count;
            repo.DeleteContactForm(1);
            var afterCount = repo.GetAllContactForms().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
