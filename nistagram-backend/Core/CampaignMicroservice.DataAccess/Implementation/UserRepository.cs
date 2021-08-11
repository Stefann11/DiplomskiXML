using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessAdapter;
using CampaignMicroservice.DataAccessTarget;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccessImplementation
{
    public class UserRepository : Repository, IUserRepository
    {
        public RegisteredUserAdapter _registeredUserTarget = new RegisteredUserAdapter(new RegisteredUserAdaptee());

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
            queryBuilder.Append("SET username = @username, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth," +
                " gender = @gender, is_banned = @is_banned, is_private = @is_private, profile_image_path = @profile_image_path  ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_banned", SqlDbType.Bit) { Value = registeredUser.IsBanned },
                 new SqlParameter("@profile_image_path", SqlDbType.NVarChar) { Value = registeredUser.ProfileImagePath.ToString() },
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
                dataTable.Rows[0], GetBlocking(id), GetBlockedBy(id),
                GetMuted(id), GetMutedBy(id), GetFollowing(id), GetFollowers(id)
                );
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
                dataTable.Rows[0], GetBlocking((Guid)dataTable.Rows[0][0]), GetBlockedBy((Guid)dataTable.Rows[0][0]),
                GetMuted((Guid)dataTable.Rows[0][0]), GetMutedBy((Guid)dataTable.Rows[0][0]),
                GetFollowing((Guid)dataTable.Rows[0][0]), GetFollowers((Guid)dataTable.Rows[0][0])
                );
            }
            return Maybe<RegisteredUser>.None;
        }

        public RegisteredUser Save(RegisteredUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.RegisteredUser ");
            queryBuilder.Append("(id, username, first_name, last_name, date_of_birth, gender, website_address, is_private, type, category, is_banned, profile_image_path) ");
            queryBuilder.Append("VALUES (@id, @username, @first_name, @last_name, @date_of_birth, @gender, '', @is_private, 'default', '', @is_banned, @profile_image_path);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@profile_image_path", SqlDbType.NVarChar) { Value = registeredUser.ProfileImagePath.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_banned", SqlDbType.Bit) { Value = registeredUser.IsBanned },
             };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }

        public IEnumerable<RegisteredUser> GetBlocking(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Blocks as f ");
            queryBuilder.Append("WHERE f.blocking_id = r.id AND r.is_banned = 0 AND f.blocked_by_id = r2.id " +
                "AND f.blocked_by_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public IEnumerable<RegisteredUser> GetBlockedBy(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Blocks as f ");
            queryBuilder.Append("WHERE f.blocking_id = r2.id AND r.is_banned = 0 AND f.blocked_by_id = r.id " +
                "AND f.blocking_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public IEnumerable<RegisteredUser> GetMutedBy(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Mutes as f ");
            queryBuilder.Append("WHERE f.muting_id = r2.id AND r.is_banned = 0 AND f.muted_by_id = r.id " +
                "AND f.muting_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public IEnumerable<RegisteredUser> GetMuted(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Mutes as f ");
            queryBuilder.Append("WHERE f.muting_id = r.id AND r.is_banned = 0 AND f.muted_by_id = r2.id " +
                "AND f.muted_by_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public IEnumerable<RegisteredUser> GetFollowing(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Follows as f ");
            queryBuilder.Append("WHERE f.following_id=r.id AND r.is_banned = 0 AND f.followed_by_id=r2.id " +
                "AND f.followed_by_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public IEnumerable<RegisteredUser> GetFollowers(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.Follows as f ");
            queryBuilder.Append("WHERE f.following_id = r2.id AND r.is_banned = 0 AND f.followed_by_id = r.id " +
                "AND f.following_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public void Follow(Guid id, Guid followedById, Guid followingId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Follows ");
            queryBuilder.Append("(id, followed_by_id, following_id) ");
            queryBuilder.Append("VALUES (@id, @followed_by_id, @following_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@followed_by_id", SqlDbType.UniqueIdentifier) { Value = followedById },
                 new SqlParameter("@following_id", SqlDbType.UniqueIdentifier) { Value = followingId }
             };

            ExecuteQuery(query, parameters);
        }

        public void DeleteFollow(Guid followedById, Guid followingId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Follows ");
            queryBuilder.Append("WHERE followed_by_id = @followed_by_id AND following_id = @following_id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@followed_by_id", SqlDbType.UniqueIdentifier) { Value = followedById },
                 new SqlParameter("@following_id", SqlDbType.UniqueIdentifier) { Value = followingId }
             };

            ExecuteQuery(query, parameters);
        }

        public Boolean AlreadyFollowing(Guid requests_follow_id, Guid recieves_follow_id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.Follows ");
            queryBuilder.Append("WHERE @followed_by_id = followed_by_id AND @following_id = following_id; ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@followed_by_id", SqlDbType.UniqueIdentifier) { Value = requests_follow_id },
                 new SqlParameter("@following_id", SqlDbType.UniqueIdentifier) { Value = recieves_follow_id }
             };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public void Mute(Guid id, Guid mutedById, Guid mutingId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Mutes ");
            queryBuilder.Append("(id, muted_by_id, muting_id) ");
            queryBuilder.Append("VALUES (@id, @muted_by_id, @muting_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@muted_by_id", SqlDbType.UniqueIdentifier) { Value = mutedById },
                 new SqlParameter("@muting_id", SqlDbType.UniqueIdentifier) { Value = mutingId }
             };

            ExecuteQuery(query, parameters);
        }

        public void Block(Guid id, Guid blockedById, Guid blockingId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Blocks ");
            queryBuilder.Append("(id, blocked_by_id, blocking_id) ");
            queryBuilder.Append("VALUES (@id, @blocked_by_id, @blocking_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@blocked_by_id", SqlDbType.UniqueIdentifier) { Value = blockedById },
                 new SqlParameter("@blocking_id", SqlDbType.UniqueIdentifier) { Value = blockingId }
             };

            ExecuteQuery(query, parameters);
        }

        public void DeleteBlock(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Blocks ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        public void DeleteFollows(Guid blockedById, Guid blockingId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Follows ");
            queryBuilder.Append("WHERE (followed_by_id = @blocked_by_id AND following_id = @blocking_id) OR (followed_by_id = @blocking_id AND following_id = @blocked_by_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@blocked_by_id", SqlDbType.UniqueIdentifier) { Value = blockedById },
                 new SqlParameter("@blocking_id", SqlDbType.UniqueIdentifier) { Value = blockingId }
             };

            ExecuteQuery(query, parameters);
        }

        public IEnumerable<RegisteredUser> GetSeenBy(Guid exposureDateId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.SeenBy AS s ");
            queryBuilder.Append("WHERE s.registered_user_id = r.id AND r.is_banned = 0 AND s.exposure_date_id = @ExposureDateId ");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@ExposureDateId", SqlDbType.UniqueIdentifier) { Value = exposureDateId }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>()
                    )).ToList();
        }

        public RegisteredUser EditVerifiedUser(VerifiedUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET username = @username, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth," +
              " gender = @gender, is_banned = @is_banned, is_private = @is_private, profile_image_path = @profile_image_path, type = @type, category = @category  ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                  new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@profile_image_path", SqlDbType.NVarChar) { Value = registeredUser.ProfileImagePath.ToString() },
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_banned", SqlDbType.Bit) { Value = registeredUser.IsBanned },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = registeredUser.GetType().Name },
                 new SqlParameter("@category", SqlDbType.NVarChar) { Value = registeredUser.Category.ToString() },
            };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }

        public RegisteredUser EditAgent(Agent registeredUser)
        {
            string type = registeredUser.GetType().Name;
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET username = @username, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth, website_address = @website_address," +
                " gender = @gender, is_banned = @is_banned, is_private = @is_private, profile_image_path = @profile_image_path, type = @type ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id },
                 new SqlParameter("@username", SqlDbType.NVarChar) { Value = registeredUser.Username.ToString() },
                 new SqlParameter("@first_name", SqlDbType.NVarChar) { Value = registeredUser.FirstName.ToString() },
                 new SqlParameter("@last_name", SqlDbType.NVarChar) { Value = registeredUser.LastName.ToString() },
                 new SqlParameter("@date_of_birth", SqlDbType.NVarChar) { Value = registeredUser.DateOfBirth.ToString() },
                 new SqlParameter("@gender", SqlDbType.NVarChar) { Value = registeredUser.Gender.ToString() },
                 new SqlParameter("@website_address", SqlDbType.NVarChar) { Value = registeredUser.WebsiteAddress.ToString() },
                 new SqlParameter("@profile_image_path", SqlDbType.NVarChar) { Value = registeredUser.ProfileImagePath.ToString() },
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_banned", SqlDbType.Bit) { Value = registeredUser.IsBanned },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = registeredUser.GetType().Name },
            };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }
    }
}