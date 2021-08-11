using AgentApp.Core.Interface.Repository;
using AgentApp.Core.Model;
using AgentApp.DataAccess.Adaptee;
using AgentApp.DataAccess.Adapter;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.DataAccess.Implementation
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserAdapter _registeredUserTarget = new UserAdapter(new UserAdaptee());

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void Delete(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        public RegisteredUser Edit(RegisteredUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET username = @username, email = @email, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth," +
                " phone_number = @phone_number, gender = @gender, website_address = @website_address, bio = @bio ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@email", SqlDbType.NVarChar) { Value = registeredUser.EmailAddress.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@phone_number", SqlDbType.NVarChar) { Value = registeredUser.PhoneNumber.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@website_address", SqlDbType.NVarChar) { Value = registeredUser.WebsiteAddress.ToString() },
                 new SqlParameter("@bio", SqlDbType.NVarChar) { Value = registeredUser.Bio.ToString() },
            };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }

        public IEnumerable<RegisteredUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<RegisteredUser> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (RegisteredUser)_registeredUserTarget.ConvertSql(
                dataTable.Rows[0]);
            }
            return Maybe<RegisteredUser>.None;
        }

        public Maybe<RegisteredUser> GetByUsername(string username)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE username = @Username;");

            string query = queryBuilder.ToString();

            SqlParameter parameterUsername = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterUsername };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (RegisteredUser)_registeredUserTarget.ConvertSql(
                dataTable.Rows[0]);
            }
            return Maybe<RegisteredUser>.None;
        }

        public RegisteredUser Save(RegisteredUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.RegisteredUser ");
            queryBuilder.Append("(id, username, email, first_name, last_name, date_of_birth, phone_number, gender, website_address, bio, type, password) ");
            queryBuilder.Append("VALUES (@id, @username, @email, @first_name, @last_name, @date_of_birth, @phone_number, @gender, @website_address, @bio, 'default', @password);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@email", SqlDbType.NVarChar) { Value = registeredUser.EmailAddress.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@phone_number", SqlDbType.NVarChar) { Value = registeredUser.PhoneNumber.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@website_address", SqlDbType.NVarChar) { Value = registeredUser.WebsiteAddress.ToString() },
                 new SqlParameter("@bio", SqlDbType.NVarChar) { Value = registeredUser.Bio.ToString() },
                 new SqlParameter("@password", SqlDbType.NVarChar) { Value = registeredUser.Password.ToString() },
             };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }
    }
}