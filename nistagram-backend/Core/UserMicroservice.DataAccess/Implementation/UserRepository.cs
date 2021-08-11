using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Adapter;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Implementation
{
    public class UserRepository : Repository, IUserRepository
    {
        public RegisteredUserAdapter _registeredUserTarget = new RegisteredUserAdapter(new RegisteredUserAdaptee());
        public UserModelAdapter _userModelTarget = new UserModelAdapter(new UserModelAdaptee());
        public ITarget _followRequestTarget = new FollowRequestAdapter(new FollowRequestAdaptee());

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
                " phone_number = @phone_number, gender = @gender, website_address = @website_address, bio = @bio, is_private = @is_private," +
                " is_accepting_messages = @is_accepting_messages, is_accepting_tags = @is_accepting_tags ");
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
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = registeredUser.IsAcceptingMessages },
                 new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = registeredUser.IsAcceptingTags },
            };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }

        public RegisteredUser EditVerifiedUser(VerifiedUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET username = @username, email = @email, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth," +
                " phone_number = @phone_number, gender = @gender, website_address = @website_address, bio = @bio, is_private = @is_private," +
                " is_accepting_messages = @is_accepting_messages, is_accepting_tags = @is_accepting_tags, type = @type, category = @category ");
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
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = registeredUser.IsAcceptingMessages },
                 new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = registeredUser.IsAcceptingTags },
                 new SqlParameter("@category", SqlDbType.NVarChar) { Value = registeredUser.Category.ToString() },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = registeredUser.GetType().Name },
            };

            ExecuteQuery(query, parameters);

            return registeredUser;
        }

        public RegisteredUser EditAgent(Agent registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET username = @username, email = @email, first_name = @first_name, last_name = @last_name, date_of_birth = @date_of_birth," +
                " phone_number = @phone_number, gender = @gender, website_address = @website_address, bio = @bio, is_private = @is_private," +
                " is_accepting_messages = @is_accepting_messages, is_accepting_tags = @is_accepting_tags, type = @type ");
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
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = registeredUser.IsAcceptingMessages },
                 new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = registeredUser.IsAcceptingTags },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = registeredUser.GetType().Name },
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
            queryBuilder.Append("WHERE id = @Id AND is_banned=0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (RegisteredUser)_registeredUserTarget.ConvertSql(
                dataTable.Rows[0], GetBlocking(id), GetBlockedBy(id),
                GetMuted(id), GetMutedBy(id), GetFollowing(id), GetFollowers(id),
                GetMyCloseFriends(id), GetCloseFriendsTo(id)
                );
            }
            return Maybe<RegisteredUser>.None;
        }

        public Maybe<User> GetByIdWithType(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE id = @Id AND is_banned=0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0][13].Equals("default"))
                {
                    return (RegisteredUser)_userModelTarget.ConvertSql(
                  dataTable.Rows[0], GetBlocking(id), GetBlockedBy(id),
                  GetMuted(id), GetMutedBy(id), GetFollowing(id), GetFollowers(id),
                  GetMyCloseFriends(id), GetCloseFriendsTo(id)
                );
                }
                else if (dataTable.Rows[0][13].Equals("Agent"))
                {
                    return (Agent)_userModelTarget.ConvertSql(
                 dataTable.Rows[0], GetBlocking(id), GetBlockedBy(id),
                 GetMuted(id), GetMutedBy(id), GetFollowing(id), GetFollowers(id),
                 GetMyCloseFriends(id), GetCloseFriendsTo(id)
                 );
                }
                else
                {
                    return (VerifiedUser)_userModelTarget.ConvertSql(
                   dataTable.Rows[0], GetBlocking(id), GetBlockedBy(id),
                   GetMuted(id), GetMutedBy(id), GetFollowing(id), GetFollowers(id),
                   GetMyCloseFriends(id), GetCloseFriendsTo(id)
                );
                }
            }
            return Maybe<User>.None;
        }

        private Boolean IsBlocked(Guid loggedId, Guid userId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Blocks ");
            queryBuilder.Append("WHERE blocking_id = @userId AND blocked_by_id = @loggedId;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@loggedId", SqlDbType.UniqueIdentifier) { Value = loggedId },
                new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId }
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public Maybe<User> GetByIdWithoutBlocked(Guid loggedId, Guid userId)
        {
            if (IsBlocked(loggedId, userId)) return Maybe<User>.None;
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE id = @Id AND is_banned=0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = userId };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0][13].Equals("default"))
                {
                    return (RegisteredUser)_userModelTarget.ConvertSql(
                  dataTable.Rows[0], GetBlocking(userId), GetBlockedBy(userId),
                  GetMuted(userId), GetMutedBy(userId), GetFollowing(userId), GetFollowers(userId),
                  GetMyCloseFriends(userId), GetCloseFriendsTo(userId)
                );
                }
                else if (dataTable.Rows[0][13].Equals("Agent"))
                {
                    return (Agent)_userModelTarget.ConvertSql(
                 dataTable.Rows[0], GetBlocking(userId), GetBlockedBy(userId),
                 GetMuted(userId), GetMutedBy(userId), GetFollowing(userId), GetFollowers(userId),
                 GetMyCloseFriends(userId), GetCloseFriendsTo(userId)
                 );
                }
                else
                {
                    return (VerifiedUser)_userModelTarget.ConvertSql(
                   dataTable.Rows[0], GetBlocking(userId), GetBlockedBy(userId),
                   GetMuted(userId), GetMutedBy(userId), GetFollowing(userId), GetFollowers(userId),
                   GetMyCloseFriends(userId), GetCloseFriendsTo(userId)
                );
                }
            }
            return Maybe<User>.None;
        }

        public Maybe<RegisteredUser> GetByUsername(String username)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE username = @Username AND is_banned=0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterUsername = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterUsername };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (RegisteredUser)_registeredUserTarget.ConvertSql(
                dataTable.Rows[0], GetBlocking((Guid)dataTable.Rows[0][0]), GetBlockedBy((Guid)dataTable.Rows[0][0]),
                GetMuted((Guid)dataTable.Rows[0][0]), GetMutedBy((Guid)dataTable.Rows[0][0]),
                GetFollowing((Guid)dataTable.Rows[0][0]), GetFollowers((Guid)dataTable.Rows[0][0]),
                GetMyCloseFriends((Guid)dataTable.Rows[0][0]), GetCloseFriendsTo((Guid)dataTable.Rows[0][0])
                );
            }
            return Maybe<RegisteredUser>.None;
        }

        public Maybe<User> GetRoleByUsername(String username)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");
            queryBuilder.Append("WHERE username = @Username AND is_banned=0;");

            string query = queryBuilder.ToString();

            SqlParameter parameterUsername = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterUsername };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (User)_userModelTarget.ConvertSql(
                dataTable.Rows[0], new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                new List<RegisteredUser>()
                );
            }
            return Maybe<User>.None;
        }

        public IEnumerable<RegisteredUser> GetBy(string id, string name, string access)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            queryBuilder.Append("WHERE is_banned = 0 AND ");

            if (!String.IsNullOrWhiteSpace(name) || !String.IsNullOrWhiteSpace(access))
            {
                bool needsAnd = false;
                if (!String.IsNullOrWhiteSpace(name))
                {
                    if (needsAnd)
                        queryBuilder.Append("AND ");

                    queryBuilder.Append("(LOWER(username) like LOWER(@Name) OR LOWER(first_name) like LOWER(@Name) OR LOWER(last_name) like LOWER(@Name)) ");
                    SqlParameter parameterName = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = "%" + name + "%" };
                    parameters.Add(parameterName);
                    needsAnd = true;
                }
                if (!String.IsNullOrWhiteSpace(access))
                {
                    if (needsAnd)
                        queryBuilder.Append("AND ");

                    queryBuilder.Append("is_private = @Access ");

                    SqlParameter parameterHashTag = new SqlParameter("@Access", SqlDbType.Bit)
                    { Value = access.Equals("private") ? 1 : 0 };
                    parameters.Add(parameterHashTag);
                    needsAnd = true;
                }
            }

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            List<RegisteredUser> resultUsers = (from DataRow dataRow in dataTable.Rows

                                                select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                                                GetBlocking((Guid)dataRow[0]), GetBlockedBy((Guid)dataRow[0]),
                                                GetMuted((Guid)dataRow[0]), GetMutedBy((Guid)dataRow[0]), GetFollowing((Guid)dataRow[0]),
                                                GetFollowers((Guid)dataRow[0]), GetMyCloseFriends((Guid)dataRow[0]),
                                                GetCloseFriendsTo((Guid)dataRow[0])
                                                )).ToList();
            if (new Guid(id) != new Guid())
            {
                List<RegisteredUser> blockedUsers = GetBlocking(new Guid(id)).ToList();
                foreach (var user in blockedUsers) resultUsers.Remove(user);
                blockedUsers = GetBlockedBy(new Guid(id)).ToList();
                foreach (var user in blockedUsers) resultUsers.Remove(user);
            }

            return resultUsers;
        }

        public IEnumerable<RegisteredUser> GetByWithoutBlocked(Guid loggedId, string name, string access)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.RegisteredUser as r, dbo.RegisteredUser as r2, " +
                "dbo.Blocks as b ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrWhiteSpace(name) || !String.IsNullOrWhiteSpace(access))
            {
                queryBuilder.Append("WHERE r.is_banned=0 AND r2.id = @loggedId AND NOT(r.id = b.blocking_id AND r2.id = b.blocked_by_id) ");
                SqlParameter parameterId = new SqlParameter("@loggedId", SqlDbType.UniqueIdentifier) { Value = loggedId };
                parameters.Add(parameterId);
                bool needsAnd = true;
                if (!String.IsNullOrWhiteSpace(name))
                {
                    if (needsAnd)
                        queryBuilder.Append("AND ");

                    queryBuilder.Append("LOWER(r.username) like LOWER(@Name) OR LOWER(r.first_name) like LOWER(@Name) OR LOWER(r.last_name) like LOWER(@Name) ");
                    SqlParameter parameterName = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = "%" + name + "%" };
                    parameters.Add(parameterName);
                    needsAnd = true;
                }
                if (!String.IsNullOrWhiteSpace(access))
                {
                    if (needsAnd)
                        queryBuilder.Append("AND ");

                    queryBuilder.Append("r.is_private = @Access ");

                    SqlParameter parameterHashTag = new SqlParameter("@Access", SqlDbType.Bit)
                    { Value = access.Equals("private") ? 1 : 0 };
                    parameters.Add(parameterHashTag);
                    needsAnd = true;
                }
            }

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows

                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    GetBlocking((Guid)dataRow[0]), GetBlockedBy((Guid)dataRow[0]),
                    GetMuted((Guid)dataRow[0]), GetMutedBy((Guid)dataRow[0]), GetFollowing((Guid)dataRow[0]),
                    GetFollowers((Guid)dataRow[0]), GetMyCloseFriends((Guid)dataRow[0]),
                    GetCloseFriendsTo((Guid)dataRow[0])
                    )).ToList();
        }

        public RegisteredUser Save(RegisteredUser registeredUser)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.RegisteredUser ");
            queryBuilder.Append("(id, username, email, first_name, last_name, date_of_birth, phone_number, gender, website_address, bio, is_private, is_accepting_messages, is_accepting_tags, type, category, password, is_banned) ");
            queryBuilder.Append("VALUES (@id, @username, @email, @first_name, @last_name, @date_of_birth, @phone_number, @gender, @website_address, @bio, @is_private, @is_accepting_messages, @is_accepting_tags, 'default', '', @password, @is_banned);");

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
                 new SqlParameter("@is_private", SqlDbType.Bit) { Value = registeredUser.IsPrivate },
                 new SqlParameter("@is_accepting_messages", SqlDbType.Bit) { Value = registeredUser.IsAcceptingMessages },
                 new SqlParameter("@is_accepting_tags", SqlDbType.Bit) { Value = registeredUser.IsAcceptingTags },
                 new SqlParameter("@password", SqlDbType.NVarChar) { Value = registeredUser.Password.ToString() },
                 new SqlParameter("@is_banned", SqlDbType.Bit) { Value = registeredUser.IsBanned },
             };

            ExecuteQuery(query, parameters);

            return registeredUser;
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

        public void AddCloseFriend(Guid id, Guid userId, Guid closeFriendId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.CloseFriends ");
            queryBuilder.Append("(id, my_close_friend_id, close_friend_to_id) ");
            queryBuilder.Append("VALUES (@id, @close_friend_id, @user_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@user_id", SqlDbType.UniqueIdentifier) { Value = userId },
                 new SqlParameter("@close_friend_id", SqlDbType.UniqueIdentifier) { Value = closeFriendId }
             };

            ExecuteQuery(query, parameters);
        }

        public void FollowPrivate(Guid id, Guid requests_follow_id, Guid recieves_follow_id)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.FollowRequest ");
            queryBuilder.Append("(id, requests_follow_id, recieves_follow_id, timestamp, type, is_approved) ");
            queryBuilder.Append("VALUES (@id, @followed_by_id, @following_id, @timestamp, @type, @is_approved );");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@followed_by_id", SqlDbType.UniqueIdentifier) { Value = requests_follow_id },
                 new SqlParameter("@following_id", SqlDbType.UniqueIdentifier) { Value = recieves_follow_id },
                 new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = DateTime.Now },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = "unappr" },
                 new SqlParameter("@is_approved", SqlDbType.Bit) { Value = false }
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

        public Boolean AlreadyFollowingPrivate(Guid requests_follow_id, Guid recieves_follow_id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM dbo.FollowRequest ");
            queryBuilder.Append("WHERE @followed_by_id = requests_follow_id AND @following_id = recieves_follow_id AND (@type = type OR @is_approved = is_approved); ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@followed_by_id", SqlDbType.UniqueIdentifier) { Value = requests_follow_id },
                 new SqlParameter("@following_id", SqlDbType.UniqueIdentifier) { Value = recieves_follow_id },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = "unappr" },
                 new SqlParameter("@is_approved", SqlDbType.Bit) { Value = true }
             };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public void HandleFollowRequest(Guid id, String type, Boolean is_approved)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.FollowRequest ");
            queryBuilder.Append("SET type = @type, is_approved = @is_approved ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@type", SqlDbType.NVarChar) { Value = type },
                 new SqlParameter("@is_approved", SqlDbType.Bit) { Value = is_approved }
             };

            ExecuteQuery(query, parameters);
        }

        public IEnumerable<FollowRequest> GetFollowRequestsForUser(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT f.id, f.timestamp, r.id, r.username, r.email, r.first_name, r.last_name, r.date_of_birth, " +
                "r.phone_number, r.gender, r.website_address, r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, " +
                "r.password, r.profilePicturePath, r.is_banned, r2.id, r2.username, r2.email, r2.first_name, r2.last_name, r2.date_of_birth, " +
                "r2.phone_number, r2.gender, r2.website_address, r2.bio, r2.is_private, " +
                "r2.is_accepting_messages, r2.is_accepting_tags, r2.type, r2.category, r2.password, r2.profilePicturePath, r2.is_banned ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.FollowRequest AS f, dbo.RegisteredUser As r2 ");
            queryBuilder.Append("WHERE r.id = f.requests_follow_id AND r.is_banned = 0 AND f.recieves_follow_id = @id  AND f.recieves_follow_id = r2.id " +
                "AND f.type = \'unappr\' ");
            queryBuilder.Append("ORDER BY f.timestamp DESC");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (FollowRequest)_followRequestTarget.ConvertSql(dataRow)).ToList();
        }

        private IEnumerable<RegisteredUser> GetFollowers(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        public IEnumerable<RegisteredUser> GetFollowing(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned  ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private Boolean DoesHaveMuted(Guid userId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Mutes ");
            queryBuilder.Append("WHERE muted_by_id = @userId;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId }
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public IEnumerable<RegisteredUser> GetFollowingWithoutMuted(Guid id)
        {
            if (!DoesHaveMuted(id))
            {
                return GetFollowing(id);
            }
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, MAX(r.username), MAX(r.email), " +
                "MAX(r.first_name), MAX(r.last_name), MAX(r.date_of_birth), MAX(r.phone_number), MAX(r.gender), MAX(r.website_address), " +
                "MAX(r.bio), cast(max(cast(r.is_private as int)) as bit), cast(max(cast(r.is_accepting_messages as int)) as bit), " +
                "cast(max(cast(r.is_accepting_tags as int)) as bit), " +
                "MAX(r.type), MAX(r.category), MAX(r.password), MAX(r.profilePicturePath), cast(max(cast(r.is_banned as int)) as bit)  ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, dbo.Mutes AS m, " +
                "dbo.Follows as f ");
            queryBuilder.Append("WHERE f.following_id=r.id AND r.is_banned = 0 AND f.followed_by_id=r2.id " +
                "AND f.followed_by_id NOT IN (SELECT m2.muted_by_id FROM  dbo.Mutes AS m2 WHERE m2.muting_id = f.following_id) " +
                "AND f.followed_by_id = @Id ");
            queryBuilder.Append("GROUP BY r.id; ");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetBlockedBy(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath,  r.is_banned ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetBlocking(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetMutedBy(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetMuted(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
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
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetCloseFriendsTo(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.CloseFriends as f ");
            queryBuilder.Append("WHERE f.my_close_friend_id = r2.id AND f.close_friend_to_id = r.id AND r.is_banned = 0 " +
                "AND f.my_close_friend_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        private IEnumerable<RegisteredUser> GetMyCloseFriends(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.email, " +
                "r.first_name, r.last_name, r.date_of_birth, r.phone_number, r.gender, r.website_address, " +
                "r.bio, r.is_private, r.is_accepting_messages, r.is_accepting_tags, r.type, r.category, r.password, r.profilePicturePath, r.is_banned ");
            queryBuilder.Append("FROM dbo.RegisteredUser AS r, dbo.RegisteredUser AS r2, " +
                "dbo.CloseFriends as f ");
            queryBuilder.Append("WHERE f.my_close_friend_id = r.id AND r.is_banned = 0 AND f.close_friend_to_id = r2.id " +
                "AND f.close_friend_to_id = @Id");

            List<SqlParameter> parameters = new List<SqlParameter>{
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id }
             };

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow,
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<RegisteredUser>(), new List<RegisteredUser>())).ToList();
        }

        public void AddProfilePicture(Guid id, string image)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET profilePicturePath = @profilePicturePath ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
                 new SqlParameter("@profilePicturePath", SqlDbType.NVarChar) { Value = image },
             };

            ExecuteQuery(query, parameters);
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

        public void DeleteFollowRequests(Guid blockedById, Guid blockingId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.FollowRequest ");
            queryBuilder.Append("WHERE (requests_follow_id = @blocked_by_id AND recieves_follow_id = @blocking_id) OR " +
                "(requests_follow_id = @blocking_id AND recieves_follow_id = @blocked_by_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@blocked_by_id", SqlDbType.UniqueIdentifier) { Value = blockedById },
                 new SqlParameter("@blocking_id", SqlDbType.UniqueIdentifier) { Value = blockingId }
             };

            ExecuteQuery(query, parameters);
        }

        public void BanUser(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.RegisteredUser ");
            queryBuilder.Append("SET is_banned = 1 ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        public void DeleteMute(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Mutes ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
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
    }
}