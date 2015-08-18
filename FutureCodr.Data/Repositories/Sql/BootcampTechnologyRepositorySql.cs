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

    public class BootcampTechnologyRepositorySql : IBootcampTechnologyRepository
    {
        public BootcampTechnology AddBootcampTechnology(BootcampTechnology obj)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TechnologyID", obj.TechnologyID);
                param.Add("@BootcampID", obj.BootcampID);
                param.Add("@BootcampTechnologyID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("BootcampTechnologyAdd", commandType: CommandType.StoredProcedure);
                obj.BootcampTechnologyID = param.Get<int>("@BootcampTechnologyID");
            }
            return obj;
        }

        public void DeleteBootcampTechnology(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampTechnologyID", id);
                connection.Execute("BootcampTechnologyDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<BootcampTechnology> GetAllBootcampTechnologies()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<BootcampTechnology>("BootcampTechnologyGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<BootcampTechnology> GetAllBootcampTechnologiesByBootcampId(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", id);
                return connection.Query<BootcampTechnology>("BootcampTechnologyGetAllByBootcampId", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<BootcampTechnology> GetAllBootcampTechnologiesByTechnologyId(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TechnologyID", id);
                return connection.Query<BootcampTechnology>("BootcampTechnologyGetAllByTechnologyId", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public BootcampTechnology GetBootcampTechnologyById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampTechnologyID", id);
                return connection.Query<BootcampTechnology>("BootcampTechnologyGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
