using FutureCodr.Models;
using System;
using System.Collections.Generic;

namespace FutureCodr.Data.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAllReviews();
        List<Review> GetAllReviewsByBootcampId(int bootcampId);
        Review GetReviewById(int reviewId);
        Review AddReview(Review review);
        void EditReview(Review review);
        void DeleteReview(int reviewId);
    }
}
