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

    public class BootcampSessionRepositorySql : IBootcampSessionRepository
    {
        public BootcampSession AddBootcampSession(BootcampSession session)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddBootcampSessionParameters(session);
                param.Add("@BootcampSessionID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("BootcampSessionAdd", param, commandType: CommandType.StoredProcedure);
                session.BootcampSessionID = param.Get<int>("@BootcampSessionID");
            }
            return session;
        }

        public DynamicParameters AddBootcampSessionParameters(BootcampSession session)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@LocationID", session.LocationID);
            parameters.Add("@BootcampID", session.BootcampID);
            parameters.Add("@TechnologyID", session.TechnologyID);
            parameters.Add("@StartDate", session.StartDate);
            parameters.Add("@EndDate", session.EndDate);
            return parameters;
        }

        public void DeleteBootcampSession(int sessionId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampSessionID", sessionId);
                connection.Execute("BootcampSessionDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditBootcampSession(BootcampSession session)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddBootcampSessionParameters(session);
                param.Add("@BootcampSessionID", session.BootcampSessionID);
                connection.Execute("BootcampSessionEdit", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<BootcampSession> GetAllBootcampSessions()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<BootcampSession>("BootcampSessionsGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public BootcampSession GetBootcampSessionById(int sessionId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampSessionID", sessionId);
                return connection.Query<BootcampSession>("BootcampSessionGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<BootcampSession> GetBootcampSessionsByBootcampId(int bootcampId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", bootcampId);
                return connection.Query<BootcampSession>("BootcampSessionsGetAllByBootcampId", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
