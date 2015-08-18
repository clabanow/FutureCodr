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

    public class LinkRepositorySql : ILinkRepository
    {
        public Link AddLink(Link link)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddLinkParameters(link);
                param.Add("@LinkID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("LinksAdd", param, commandType: CommandType.StoredProcedure);
                link.LinkID = param.Get<int>("@LinkID");
            }
            return link;
        }

        public DynamicParameters AddLinkParameters(Link link)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@URL", link.URL);
            parameters.Add("@LinkText", link.LinkText);
            parameters.Add("@SiteID", link.SiteID);
            parameters.Add("@Date", link.Date);
            parameters.Add("@BootcampID", link.BootcampID);
            return parameters;
        }

        public void DeleteLink(int linkId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LinkID", linkId);
                connection.Execute("LinksDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditLink(Link link)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = AddLinkParameters(link);
                param.Add("@LinkID", link.LinkID);
                connection.Execute("LinksEdit", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Link> GetAllLinks()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<Link>("LinksGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<Link> GetAllLinksByBootcampId(int bootcampId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@BootcampID", bootcampId);
                return connection.Query<Link>("LinksGetAllByBootcampId", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Link GetLinkById(int linkId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@LinkID", linkId);
                return connection.Query<Link>("LinksGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
