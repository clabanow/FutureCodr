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
    public class ReviewRepositoryTests
    {
        private MockRepository mock = new MockRepository();
        private IReviewRepository repo = new ReviewRepositorySql();

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
        public void GetAllReviewsTest()
        {
            var result = repo.GetAllReviews().Count;

            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetAllReviewsByBootcampIdTest()
        {
            var result = repo.GetAllReviewsByBootcampId(1).Count;

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void GetReviewByIdTest()
        {
            var result = repo.GetReviewById(2).ReviewText;

            Assert.AreEqual("The program sucked.", result);
        }

        [Test]
        public void AddReviewTest()
        {
            var beforeCount = repo.GetAllReviews().Count;
            repo.AddReview(mock.mockReview);
            var afterCount = repo.GetAllReviews().Count;

            Assert.AreEqual(beforeCount + 1, afterCount);
        }

        [Test]
        public void EditReviewTest()
        {
            repo.EditReview(mock.mockReview);
            var result = repo.GetReviewById(mock.mockReview.ReviewID).ReviewText;

            Assert.AreEqual("It was good", result);
        }

        [Test]
        public void DeleteReviewTest()
        {
            var beforeCount = repo.GetAllReviews().Count;
            repo.DeleteReview(1);
            var afterCount = repo.GetAllReviews().Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }
    }
}
