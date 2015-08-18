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

    public class LocationRepositorySql : ILocationRepository
    {
        public Location AddLocation(Location location)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@City", location.City);
                param.Add("@Country", location.Country);
                param.Add("@LocationID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("LocationsAdd", param, commandType: CommandType.StoredProcedure);
                location.LocationID = param.Get<int>("@LocationID");
            }
            return location;
        }

        public void DeleteLocation(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationID", id);
                connection.Execute("LocationsDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Location> GetAllLocations()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Location>("LocationsGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Location GetLocationById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LocationID", id);
                return connection.Query<Location>("LocationsGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int? GetLocationIDByCity(string city)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@City", city);
                return connection.Query<int?>("LocationIDGetByCity", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
