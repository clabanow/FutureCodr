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

    public class UserRepositorySql : IUserRepository
    {
        public User AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddUserParameters(user);
                param.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("UsersAdd", param, commandType: CommandType.StoredProcedure);
                user.UserID = param.Get<int>("@UserID");
            }
            return user;
        }

        public DynamicParameters AddUserParameters(User user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            return parameters;
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userId);
                connection.Execute("UsersDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddUserParameters(user);
                param.Add("@UserID", user.UserID);
                connection.Execute("UsersEdit", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<User>("UsersGetAll", commandType: CommandType.StoredProcedure).ToList<User>();
            }
        }

        public User GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserID", userId);
                return connection.Query<User>("UsersGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault<User>();
            }
        }
    }
}
