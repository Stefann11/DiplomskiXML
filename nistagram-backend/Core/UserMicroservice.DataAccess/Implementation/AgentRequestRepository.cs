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
    public class AgentRequestRepository : Repository, IAgentRequestRepository
    {
        public ITarget _agentRequestAdapter = new AgentRequestAdapter(new AgentRequestAdaptee());

        public AgentRequestRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<AgentRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<AgentRequest> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT a.id, a.is_approved, r.id, r.username, r.email, r.first_name, r.last_name," +
                " r.date_of_birth, r.phone_number, r.gender, r.website_address, r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, " +
                " r.profilePicturePath, r.password, r.is_banned, a.action, a.username, a.email, a.first_name, a.last_name," +
                " a.date_of_birth, a.phone_number, a.gender, a.website_address, a.bio, a.is_private, a.is_accepting_messages, a.is_accepting_tags, " +
                " a.password ");
            queryBuilder.Append("FROM dbo.AgentRequest as a, dbo.RegisteredUser as r ");
            queryBuilder.Append("WHERE r.id = a.registered_user_id AND a.id = @Id");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (AgentRequest)_agentRequestAdapter.ConvertSql(dataTable.Rows[0]);
            }
            return Maybe<AgentRequest>.None;
        }

        public AgentRequest Save(AgentRequest agentRequest)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.AgentRequest ");
            queryBuilder.Append("(id, is_approved, registered_user_id, action, username, email, first_name, last_name, date_of_birth, " +
                "phone_number, gender, website_address, bio, is_private, is_accepting_messages, is_accepting_tags, password) ");
            queryBuilder.Append("VALUES (@id, @is_approved, @registered_user_id, @action, @username, @email, @first_name, @last_name, @date_of_birth, " +
                "@phone_number, @gender, @website_address, @bio, @is_private, @is_accepting_messages, @is_accepting_tags, @password);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = agentRequest.Id },
                new SqlParameter("@is_approved", SqlDbType.Bit) { Value = agentRequest.IsApproved },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = agentRequest.RegisteredUser.Id },
                new SqlParameter("@action", SqlDbType.NVarChar) { Value = agentRequest.AgentRequestAction.ToString() },
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = agentRequest.Username.ToString() },
                new SqlParameter("@email", SqlDbType.NVarChar) { Value = agentRequest.EmailAddress.ToString() },
                new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = agentRequest.FirstName.ToString() },
                new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = agentRequest.LastName.ToString() },
                new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = agentRequest.DateOfBirth.ToString() },
                new SqlParameter("@phone_number", SqlDbType.NVarChar) { Value = agentRequest.PhoneNumber.ToString() },
                new SqlParameter("@gender", SqlDbType.NVarChar) { Value = agentRequest.Gender.ToString() },
                new SqlParameter("@website_address", SqlDbType.NVarChar) { Value = agentRequest.WebsiteAddress.ToString() },
                new SqlParameter("@bio", SqlDbType.NVarChar) { Value = agentRequest.Bio.ToString() },
                new SqlParameter("@is_private", SqlDbType.Bit) { Value = agentRequest.IsPrivate },
                new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = agentRequest.IsAcceptingMessages },
                new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = agentRequest.IsAcceptingTags },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = agentRequest.Password.ToString() },
            };

            ExecuteQuery(query, parameters);

            return agentRequest;
        }

        public AgentRequest Edit(AgentRequest agentRequest)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.AgentRequest ");
            queryBuilder.Append("SET id = @id, is_approved = @is_approved, registered_user_id = @registered_user_id, " +
                "action=@action, username=@username, email=@email, first_name=@first_name, last_name=@last_name, " +
                "date_of_birth=@date_of_birth, phone_number=@phone_number, gender=@gender, " +
                "website_address=@website_address, bio=@bio, is_private=@is_private, " +
                "is_accepting_messages=@is_accepting_messages, is_accepting_tags=@is_accepting_tags, password=@password ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = agentRequest.Id },
                new SqlParameter("@is_approved", SqlDbType.Bit) { Value = agentRequest.IsApproved },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = agentRequest.RegisteredUser.Id },
                new SqlParameter("@action", SqlDbType.NVarChar) { Value = agentRequest.AgentRequestAction.ToString() },
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = agentRequest.Username.ToString() },
                new SqlParameter("@email", SqlDbType.NVarChar) { Value = agentRequest.EmailAddress.ToString() },
                new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = agentRequest.FirstName.ToString() },
                new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = agentRequest.LastName.ToString() },
                new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = agentRequest.DateOfBirth.ToString() },
                new SqlParameter("@phone_number", SqlDbType.NVarChar) { Value = agentRequest.PhoneNumber.ToString() },
                new SqlParameter("@gender", SqlDbType.NVarChar) { Value = agentRequest.Gender.ToString() },
                new SqlParameter("@website_address", SqlDbType.NVarChar) { Value = agentRequest.WebsiteAddress.ToString() },
                new SqlParameter("@bio", SqlDbType.NVarChar) { Value = agentRequest.Bio.ToString() },
                new SqlParameter("@is_private", SqlDbType.Bit) { Value = agentRequest.IsPrivate },
                new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = agentRequest.IsAcceptingMessages },
                new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = agentRequest.IsAcceptingTags },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = agentRequest.Password.ToString() },
            };

            ExecuteQuery(query, parameters);

            return agentRequest;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AgentRequest> GetBy(string isApproved)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT a.id, a.is_approved, r.id, r.username, r.email, r.first_name, r.last_name," +
                " r.date_of_birth, r.phone_number, r.gender, r.website_address, r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, " +
                " r.profilePicturePath, r.password, r.is_banned, a.action, a.username, a.email, a.first_name, a.last_name," +
                " a.date_of_birth, a.phone_number, a.gender, a.website_address, a.bio, a.is_private, a.is_accepting_messages, a.is_accepting_tags, " +
                " a.password ");
            queryBuilder.Append("FROM dbo.AgentRequest as a, dbo.RegisteredUser as r ");
            queryBuilder.Append("WHERE r.id = a.registered_user_id AND a.action=\'created\' ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrWhiteSpace(isApproved))
            {
                queryBuilder.Append("AND a.is_approved = @IsApproved");
                SqlParameter parameterIsApproved = new SqlParameter("@IsApproved", SqlDbType.Bit)
                { Value = isApproved.Equals("true") ? 1 : 0 };
                parameters.Add(parameterIsApproved);
            }

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows

                    select (AgentRequest)_agentRequestAdapter.ConvertSql(dataRow)).ToList();
        }
    }
}