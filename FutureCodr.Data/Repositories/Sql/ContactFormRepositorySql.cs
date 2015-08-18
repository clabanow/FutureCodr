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

    public class ContactFormRepositorySql : IContactFormRepository
    {
        public ContactForm AddContactForm(ContactForm form)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ContactFormID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@Name", form.Name);
                param.Add("@Email", form.Email);
                param.Add("@Message", form.Message);
                connection.Execute("ContactFormAdd", param, commandType: CommandType.StoredProcedure);
                form.ContactFormID = param.Get<int>("@ContactFormID");
            }
            return form;
        }

        public void DeleteContactForm(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ContactFormID", id);
                connection.Execute("ContactFormDelete", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<ContactForm> GetAllContactForms()
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                return connection.Query<ContactForm>("ContactFormGetAll", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public ContactForm GetContactFormById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.GetConnectionString()))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ContactFormID", id);
                return connection.Query<ContactForm>("ContactFormGetById", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
