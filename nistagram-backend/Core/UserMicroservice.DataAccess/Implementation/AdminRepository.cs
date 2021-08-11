using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Adapter;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Implementation
{
    public class AdminRepository : Repository, IAdminRepository
    {
        public ITarget _adminAdapter = new AdminAdapter(new AdminAdaptee());

        public AdminRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Admin Edit(Admin obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<Admin> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Admin ");
            queryBuilder.Append("WHERE id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Admin)_adminAdapter.ConvertSql(
                dataTable.Rows[0]
                );
            }
            return Maybe<Admin>.None;
        }

        public Maybe<Admin> GetByUsername(string username)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Admin ");
            queryBuilder.Append("WHERE username = @Username;");

            string query = queryBuilder.ToString();

            SqlParameter parameterUsername = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterUsername };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Admin)_adminAdapter.ConvertSql(
                dataTable.Rows[0]
                );
            }
            return Maybe<Admin>.None;
        }

        public Maybe<User> GetRoleByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Admin Save(Admin obj)
        {
            throw new NotImplementedException();
        }
    }
}