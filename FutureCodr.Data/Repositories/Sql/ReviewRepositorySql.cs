namespace FutureCodr.Data.Repositories.Sql
{
    using Dapper;
    using FutureCodr.Data;
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Data;

    public class ReviewRepositorySql : IReviewRepository
    {
        public Review AddReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddReviewParameters(review);
                param.Add("@ReviewID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("ReviewsAdd", param, commandType: CommandType.StoredProcedure);
                review.ReviewID = param.Get<int>("@ReviewID");
            }
            return review;
        }

        public DynamicParameters AddReviewParameters(Review review)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ReviewText", review.ReviewText);
            parameters.Add("@IsVerified", review.IsVerified);
            parameters.Add("@BootcampID", review.BootcampID);
            parameters.Add("@UserID", review.UserID);
            return parameters;
        }

        public void DeleteReview(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReviewID", reviewId);
                connection.Execute("ReviewsDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddReviewParameters(review);
                param.Add("@ReviewID", review.ReviewID);
                connection.Execute("ReviewsEdit", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Review> GetAllReviews()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Review>("ReviewsGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<Review> GetAllReviewsByBootcampId(int bootcampId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", bootcampId);
                return connection.Query<Review>("ReviewsGetAllByBootcampId", param, commandType: CommandType.StoredProcedure).ToList<Review>();
            }
        }

        public Review GetReviewById(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ReviewID", reviewId);
                return connection.Query<Review>("ReviewsGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault<Review>();
            }
        }
    }
}
