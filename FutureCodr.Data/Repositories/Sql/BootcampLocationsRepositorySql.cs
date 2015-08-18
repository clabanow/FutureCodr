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

    public class BootcampLocationsRepositorySql : IBootcampLocationsRepository
    {
        public BootcampLocation AddBootcampLocation(BootcampLocation location)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", location.BootcampID);
                param.Add("@LocationID", location.LocationID);
                param.Add("@BootcampLocationID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("BootcampLocationsAdd", param, commandType: CommandType.StoredProcedure);
                location.BootcampLocationID = param.Get<int>("@BootcampLocationID");
            }
            return location;
        }

        public void DeleteBootcampLocation(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampLocationID", id);
                connection.Execute("BootcampLocationsDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<BootcampLocation> GetAllBootcampLocations()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<BootcampLocation>("BootcampLocationsGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<BootcampLocation> GetAllBootcampLocationsByBootcampId(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", id);
                return connection.Query<BootcampLocation>("BootcampLocationsGetByBootcampId", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<BootcampLocation> GetAllBootcampLocationsByLocationId(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationID", id);
                return connection.Query<BootcampLocation>("BootcampLocationsGetByLocationID", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public BootcampLocation GetBootcampLocationById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampLocationID", id);
                return connection.Query<BootcampLocation>("BootcampLocationsGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
