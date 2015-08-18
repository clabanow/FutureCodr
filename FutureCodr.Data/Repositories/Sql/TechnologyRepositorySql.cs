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

    public class TechnologyRepositorySql : ITechnologyRepository
    {
        public Technology AddTechnology(Technology technology)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", technology.Name);
                param.Add("@TechnologyID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("TechnologyAdd", param, commandType: CommandType.StoredProcedure);
                technology.TechnologyID = param.Get<int>("@TechnologyID");
            }
            return technology;
        }

        public void DeleteTechnology(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TechnologyID", id);
                connection.Execute("TechnologyDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Technology> GetAllTechnologies()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Technology>("TechnologyGetAll", commandType: CommandType.StoredProcedure).ToList<Technology>();
            }
        }

        public Technology GetTechnologyById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TechnologyID", id);
                return connection.Query<Technology>("TechnologyGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault<Technology>();
            }
        }

        public int? GetTechnologyIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", name);
                return connection.Query<int?>("TechnologyIDGetByName", param, commandType: CommandType.StoredProcedure).FirstOrDefault<int?>();
            }
        }
    }
}
