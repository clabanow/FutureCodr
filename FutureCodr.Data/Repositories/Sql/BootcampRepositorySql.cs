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

    public class BootcampRepositorySql : IBootcampRepository
    {
        public Bootcamp AddBootcamp(Bootcamp bootcamp)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddBootcampParameters(bootcamp);
                param.Add("@BootcampID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("BootcampsAdd", param, commandType: CommandType.StoredProcedure);
                bootcamp.BootcampID = param.Get<int>("@BootcampID");
            }
            return bootcamp;
        }

        public DynamicParameters AddBootcampParameters(Bootcamp bootcamp)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", bootcamp.Name);
            parameters.Add("@PrimaryTechnologyID", bootcamp.PrimaryTechnologyID);
            parameters.Add("@LocationID", bootcamp.LocationID);
            parameters.Add("@Price", bootcamp.Price);
            parameters.Add("@LengthInWeeks", bootcamp.LengthInWeeks);
            parameters.Add("@Website", bootcamp.Website);
            parameters.Add("@LogoLink", bootcamp.LogoLink);
            return parameters;
        }

        public void DeleteBootcamp(int bootcampID)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", bootcampID);
                connection.Execute("BootcampsRemove", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditBootcamp(Bootcamp bootcamp)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddBootcampParameters(bootcamp);
                param.Add("@BootcampID", bootcamp.BootcampID);
                connection.Execute("BootcampsEdit", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Bootcamp> GetAllBootcamps()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Bootcamp>("BootcampsGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Bootcamp GetBootcampByID(int bootcampID)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", bootcampID);
                return connection.Query<Bootcamp>("BootcampGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int? GetBootcampIDByName(string bootcampName)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", bootcampName);
                return connection.Query<int?>("BootcampIDGetByName", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
