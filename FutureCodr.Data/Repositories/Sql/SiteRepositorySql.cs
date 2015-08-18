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

    public class SiteRepositorySql : ISiteRepository
    {
        public Site AddSite(Site site)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SiteName", site.SiteName);
                param.Add("@SiteLogoURL", site.SiteLogoURL);
                param.Add("@SiteID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SitesAdd", param, commandType: CommandType.StoredProcedure);
                site.SiteID = param.Get<int>("@SiteID");
            }
            return site;
        }

        public void DeleteSite(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SiteID", id);
                connection.Execute("SitesDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Site> GetAllSites()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Site>("SitesGetAll", commandType: CommandType.StoredProcedure).ToList<Site>();
            }
        }

        public Site GetSiteById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SiteID", id);
                return connection.Query<Site>("SitesGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault<Site>();
            }
        }
    }
}
