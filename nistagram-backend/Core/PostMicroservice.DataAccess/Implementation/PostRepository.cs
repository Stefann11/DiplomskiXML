using Microsoft.Extensions.Configuration;
using PostMicroservice.Core.Model;
using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Adapter;
using PostMicroservice.DataAccess.Target;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using PostMicroservice.Core.Interface.Repository;
using System;
using CSharpFunctionalExtensions;

namespace PostMicroservice.DataAccess.Implementation
{
    public class PostRepository : Repository, IPostRepository
    {
        public PostAdapter _target = new PostAdapter(new PostAdaptee());
        public ITarget _commentTarget = new CommentAdapter(new CommentAdaptee());
        public ITarget _hashTagTarget = new HashTagAdapter(new HashTagAdaptee());
        public ITarget _registeredUserTarget = new RegisteredUserAdapter(new RegisteredUserAdaptee());
        public ITarget _contentPathTarget = new ContentPathAdapter(new ContentPathAdaptee());

        public PostRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Post> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, p.timestamp, p.description, " +
                "p.type, l.id, l.city_name, l.street, l.country, r.id, r.username, r.first_name, " +
                "r.last_name, r.profilePicturePath, r.isPrivate, r.isAcceptingTags, p.is_banned, c.content_path ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public Maybe<Post> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, p.timestamp, p.description, " +
                "p.type, l.id, l.street, l.city_name, l.country, r.id, r.username, r.first_name, " +
                "r.last_name, r.profilePicturePath, r.isPrivate, r.isAcceptingTags, p.is_banned, c.content_path ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id " +
                "AND p.Id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            return (Post)_target.ConvertSqlWithAttributes(ExecuteQuery(query, parameters).Rows[0],
                 GetLikesForPost(id), GetDislikesForPost(id), GetHashTagsForPost(id), GetCommentsForPost(id),
                 GetTaggedPeopleForPost(id), GetContentsPathForPost(id));
        }

        public Post Save(Post obj)
        {
            throw new NotImplementedException();
        }

        public Post SaveSinglePost(PostSingle post)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Post ");
            queryBuilder.Append("(id, timestamp, description, registered_user_id, type, location_id, is_banned) ");
            queryBuilder.Append("VALUES (@id, @timestamp, @description, @registered_user_id, @type, @location_id, @is_banned);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = post.TimeStamp },
                new SqlParameter("@description", SqlDbType.NVarChar) { Value = post.Description.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = post.RegisteredUser.Id },
                new SqlParameter("@type", SqlDbType.NVarChar) { Value = "single" },
                new SqlParameter("@location_id", SqlDbType.UniqueIdentifier) { Value = post.Location.Id },
                new SqlParameter("@is_banned", SqlDbType.Bit) { Value = false }
            };

            ExecuteQuery(query, parameters);

            SaveContentPath(post);
            SaveHashTags(post);
            SaveTaggedUsers(post);

            return post;
        }

        public Post SaveAlbumPost(PostAlbum post)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Post ");
            queryBuilder.Append("(id, timestamp, description, registered_user_id, type, location_id, is_banned) ");
            queryBuilder.Append("VALUES (@id, @timestamp, @description, @registered_user_id, @type, @location_id, @is_banned);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = post.TimeStamp },
                new SqlParameter("@description", SqlDbType.NVarChar) { Value = post.Description.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = post.RegisteredUser.Id },
                new SqlParameter("@type", SqlDbType.NVarChar) { Value = "album" },
                new SqlParameter("@location_id", SqlDbType.UniqueIdentifier) { Value = post.Location.Id },
                new SqlParameter("@is_banned", SqlDbType.Bit) { Value = false }
            };

            ExecuteQuery(query, parameters);

            SaveContentPaths(post);
            SaveHashTags(post);
            SaveTaggedUsers(post);

            return post;
        }

        public Post Edit(Post post)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Post> GetBy(Guid id, string hashTag, string country, string city, string street, string access)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, MAX(p.timestamp), MAX(p.description), " +
                "MAX(p.type), MAX(l.id), MAX(l.street), MAX(l.city_name), MAX(l.country), MAX(r.id), " +
                "MAX(r.username), MAX(r.first_name), MAX(r.last_name), MAX(r.profilePicturePath), " +
                "cast(max(cast(r.isPrivate as int)) as bit), cast(max(cast(r.isAcceptingTags as int)) as bit), cast(max(cast(p.is_banned as int)) as bit), MAX(c.content_path) ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c, dbo.HashTags AS h ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id AND h.post_id = p.id ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (id != Guid.Empty || !String.IsNullOrWhiteSpace(hashTag)
                || !String.IsNullOrWhiteSpace(country) || !String.IsNullOrWhiteSpace(access)
                || !String.IsNullOrWhiteSpace(city) || !String.IsNullOrWhiteSpace(street))
            {
                if (id != Guid.Empty)
                {
                    queryBuilder.Append("AND r.Id = @Id ");

                    SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };
                    parameters.Add(parameterId);
                }
                if (!String.IsNullOrWhiteSpace(hashTag))
                {
                    queryBuilder.Append("AND h.text = @HashTag ");

                    SqlParameter parameterHashTag = new SqlParameter("@HashTag", SqlDbType.NVarChar) { Value = hashTag };
                    parameters.Add(parameterHashTag);
                }
                if (!String.IsNullOrWhiteSpace(country))
                {
                    queryBuilder.Append("AND LOWER(country) = LOWER(@Country) ");
                    SqlParameter parameterCountry = new SqlParameter("@Country", SqlDbType.NVarChar) { Value = country };
                    parameters.Add(parameterCountry);
                }
                if (!String.IsNullOrWhiteSpace(city))
                {
                    queryBuilder.Append("AND LOWER(city_name) = LOWER(@City) ");
                    SqlParameter parameterCity = new SqlParameter("@City", SqlDbType.NVarChar) { Value = city };
                    parameters.Add(parameterCity);
                }
                if (!String.IsNullOrWhiteSpace(street))
                {
                    queryBuilder.Append("AND LOWER(street) = LOWER(@Street) ");
                    SqlParameter parameterStreet = new SqlParameter("@Street", SqlDbType.NVarChar) { Value = street };
                    parameters.Add(parameterStreet);
                }
                if (!String.IsNullOrWhiteSpace(access) && access.Equals("private"))
                {
                    queryBuilder.Append("AND r.isPrivate = @Access ");

                    SqlParameter parameterHashTag = new SqlParameter("@Access", SqlDbType.Bit) { Value = 1 };
                    parameters.Add(parameterHashTag);
                }
                else if (!String.IsNullOrWhiteSpace(access) && access.Equals("public"))
                {
                    queryBuilder.Append("AND r.isPrivate = @Access ");

                    SqlParameter parameterHashTag = new SqlParameter("@Access", SqlDbType.Bit) { Value = 0 };
                    parameters.Add(parameterHashTag);
                }
            }

            queryBuilder.Append("GROUP BY p.id ORDER BY MAX(p.timestamp) DESC; ");

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public IEnumerable<Post> GetByHashTag(string hashTag)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, p.timestamp, p.description, " +
                "p.type, l.id, l.street, l.city_name, l.country, r.id, r.username, r.first_name, " +
                "r.last_name, r.profilePicturePath, r.isPrivate, r.isAcceptingTags, p.is_banned, c.content_path ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c, dbo.HashTags AS h ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id " +
                "AND h.text = @HashTag;");

            string query = queryBuilder.ToString();

            SqlParameter parameterHashTag = new SqlParameter("@HashTag", SqlDbType.UniqueIdentifier) { Value = "%" + hashTag + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterHashTag };

            DataTable dataTable = ExecuteQuery(query, parameters);

            int rowsCount = dataTable.Rows.Count;

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        private List<Comment> GetCommentsForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT c.id, c.timestamp, c.comment_text, " +
                "r.id, r.username, r.first_name, r.last_name, r.isPrivate, r.isAcceptingTags, " +
                "r.profilePicturePath ");
            queryBuilder.Append("FROM dbo.Comment AS c, dbo.RegisteredUser AS r ");
            queryBuilder.Append("WHERE c.registered_user_id = r.id AND c.post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Comment)_commentTarget.ConvertSql(dataRow)).ToList();
        }

        private List<ContentPath> GetContentsPathForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT content_path ");
            queryBuilder.Append("FROM dbo.Content ");
            queryBuilder.Append("WHERE post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (ContentPath)_contentPathTarget.ConvertSql(dataRow)).ToList();
        }

        private List<HashTag> GetHashTagsForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT text ");
            queryBuilder.Append("FROM dbo.HashTags ");
            queryBuilder.Append("WHERE post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (HashTag)_hashTagTarget.ConvertSql(dataRow)).ToList();
        }

        private List<RegisteredUser> GetDislikesForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.first_name, " +
                "r.last_name, r.isPrivate, r.isAcceptingTags, r.profilePicturePath ");
            queryBuilder.Append("FROM dbo.Dislikes AS l, dbo.RegisteredUser AS r ");
            queryBuilder.Append("WHERE l.registered_user_id = r.id AND post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow)).ToList();
        }

        private List<RegisteredUser> GetLikesForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.first_name, " +
                "r.last_name, r.isPrivate, r.isAcceptingTags, r.profilePicturePath ");
            queryBuilder.Append("FROM dbo.Likes AS l, dbo.RegisteredUser AS r ");
            queryBuilder.Append("WHERE l.registered_user_id = r.id AND post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow)).ToList();
        }

        private List<RegisteredUser> GetTaggedPeopleForPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT r.id, r.username, r.first_name, " +
                "r.last_name, r.isPrivate, r.isAcceptingTags, r.profilePicturePath ");
            queryBuilder.Append("FROM dbo.PostProfileTags AS l, dbo.RegisteredUser AS r ");
            queryBuilder.Append("WHERE l.registered_user_id = r.id AND post_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (RegisteredUser)_registeredUserTarget.ConvertSql(dataRow)).ToList();
        }

        private void SaveContentPath(PostSingle post)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Content ");
            queryBuilder.Append("(id, post_id, content_path) ");
            queryBuilder.Append("VALUES (@id, @post_id, @content_path);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@content_path", SqlDbType.NVarChar) { Value = post.ContentPath.ToString() }
            };

            ExecuteQuery(query, parameters);
        }

        private void SaveContentPaths(PostAlbum post)
        {
            foreach (ContentPath contentPath in post.ContentPaths)
            {
                StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Content ");
                queryBuilder.Append("(id, post_id, content_path) ");
                queryBuilder.Append("VALUES (@id, @post_id, @content_path);");

                string query = queryBuilder.ToString();

                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@content_path", SqlDbType.NVarChar) { Value = contentPath.ToString() }
            };

                ExecuteQuery(query, parameters);
            }
        }

        private void SaveHashTags(Post post)
        {
            foreach (HashTag hashTag in post.HashTags)
            {
                StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.HashTags ");
                queryBuilder.Append("(id, post_id, text) ");
                queryBuilder.Append("VALUES (@id, @post_id, @text);");

                string query = queryBuilder.ToString();

                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@text", SqlDbType.NVarChar) { Value = hashTag.HashTagText.ToString() }
            };

                ExecuteQuery(query, parameters);
            }
        }

        private void SaveTaggedUsers(Post post)
        {
            foreach (RegisteredUser registeredUser in post.TaggedUsers)
            {
                StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.PostProfileTags ");
                queryBuilder.Append("(id, post_id, registered_user_id) ");
                queryBuilder.Append("VALUES (@id, @post_id, @registered_user_id);");

                string query = queryBuilder.ToString();

                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = post.Id },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = registeredUser.Id }
            };

                ExecuteQuery(query, parameters);
            }
        }

        public bool Like(Guid id, Guid userId)
        {
            if (AlreadyLiked(id, userId))
            {
                return false;
            }
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Likes ");
            queryBuilder.Append("(id, post_id, registered_user_id) ");
            queryBuilder.Append("VALUES (@id, @post_id, @registered_user_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = id },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = userId }
            };

            ExecuteQuery(query, parameters);
            return true;
        }

        public bool Dislike(Guid id, Guid userId)
        {
            if (AlreadyDisliked(id, userId))
            {
                return false;
            }
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Dislikes ");
            queryBuilder.Append("(id, post_id, registered_user_id) ");
            queryBuilder.Append("VALUES (@id, @post_id, @registered_user_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = id },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = userId }
            };

            ExecuteQuery(query, parameters);
            return true;
        }

        private bool AlreadyLiked(Guid id, Guid userId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Likes ");
            queryBuilder.Append("WHERE post_id = @Id AND registered_user_id = @userId;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id },
                new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId },
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        private bool AlreadyDisliked(Guid id, Guid userId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Dislikes ");
            queryBuilder.Append("WHERE post_id = @Id AND registered_user_id = @userId;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id },
                new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId },
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public void CommentPost(Guid postId, Comment comment)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Comment ");
            queryBuilder.Append("(id, timestamp, comment_text, registered_user_id, post_id) ");
            queryBuilder.Append("VALUES (@id, @timestamp, @comment_text, @registered_user_id, @post_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = comment.TimeStamp },
                new SqlParameter("@comment_text", SqlDbType.NVarChar) { Value = comment.CommentText.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = comment.RegisteredUser.Id },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = postId },
            };

            ExecuteQuery(query, parameters);
        }

        public IEnumerable<Post> GetByUserId(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, MAX(p.timestamp), MAX(p.description), " +
                 "MAX(p.type), MAX(l.id), MAX(l.street), MAX(l.city_name), MAX(l.country), MAX(r.id), " +
                 "MAX(r.username), MAX(r.first_name), MAX(r.last_name), MAX(r.profilePicturePath), " +
                 "cast(max(cast(r.isPrivate as int)) as bit), cast(max(cast(r.isAcceptingTags as int)) as bit), cast(max(cast(p.is_banned as int)) as bit), MAX(c.content_path) ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id " +
                "AND p.registered_user_id = @Id ");
            queryBuilder.Append("GROUP BY p.id " +
                "ORDER BY MAX(p.timestamp) DESC; ");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public IEnumerable<Post> GetByCollectionAndUser(Guid collectionId, Guid userId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, MAX(p.timestamp), MAX(p.description), " +
                "MAX(p.type), MAX(l.id), MAX(l.street), MAX(l.city_name), MAX(l.country), MAX(r.id), " +
                "MAX(r.username), MAX(r.first_name), MAX(r.last_name), MAX(r.profilePicturePath), " +
                "cast(max(cast(r.isPrivate as int)) as bit), cast(max(cast(r.isAcceptingTags as int)) as bit), cast(max(cast(p.is_banned as int)) as bit), MAX(con.content_path) ");
            queryBuilder.Append("FROM dbo.Collection as c, dbo.CollectionContent as cc, dbo.Post as p, " +
                "dbo.Location AS l, dbo.RegisteredUser AS r, dbo.Content AS con ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND c.id = cc.collection_id and cc.post_id = p.id and " +
                "p.location_id = l.id AND p.registered_user_id = r.id AND p.id = con.post_id " +
                "AND c.id = @Id AND c.registered_user_id = @UserId ");
            queryBuilder.Append("GROUP BY p.id; ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>() {
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = collectionId },
            new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userId }
            };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public IEnumerable<Post> GetForFollowing(IEnumerable<RegisteredUser> registeredUsers)
        {
            List<Post> posts = new List<Post>();
            foreach (RegisteredUser registeredUser in registeredUsers)
            {
                posts.AddRange(GetBy(registeredUser.Id, "", "", "", "", ""));
            }
            return posts;
        }

        public IEnumerable<Post> GetLikedByUser(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, MAX(p.timestamp), MAX(p.description), " +
                 "MAX(p.type), MAX(l.id), MAX(l.street), MAX(l.city_name), MAX(l.country), MAX(r.id), " +
                 "MAX(r.username), MAX(r.first_name), MAX(r.last_name), MAX(r.profilePicturePath), " +
                 "cast(max(cast(r.isPrivate as int)) as bit), cast(max(cast(r.isAcceptingTags as int)) as bit), cast(max(cast(p.is_banned as int)) as bit), MAX(c.content_path) ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c, dbo.Likes as likes ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id " +
                "AND p.id = likes.post_id AND likes.registered_user_id = @Id ");
            queryBuilder.Append("GROUP BY p.id; ");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public IEnumerable<Post> GetDislikedByUser(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT p.id, MAX(p.timestamp), MAX(p.description), " +
                 "MAX(p.type), MAX(l.id), MAX(l.street), MAX(l.city_name), MAX(l.country), MAX(r.id), " +
                 "MAX(r.username), MAX(r.first_name), MAX(r.last_name), MAX(r.profilePicturePath), " +
                 "cast(max(cast(r.isPrivate as int)) as bit), cast(max(cast(r.isAcceptingTags as int)) as bit), cast(max(cast(p.is_banned as int)) as bit), MAX(c.content_path) ");
            queryBuilder.Append("FROM dbo.Post AS p, dbo.Location AS l, dbo.RegisteredUser AS r, " +
                "dbo.Content AS c, dbo.Dislikes as dislikes ");
            queryBuilder.Append("WHERE p.is_banned = 0 AND p.location_id=l.id AND p.registered_user_id=r.id AND p.id=c.post_id " +
                "AND p.id = dislikes.post_id AND dislikes.registered_user_id = @Id ");
            queryBuilder.Append("GROUP BY p.id; ");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Post)_target.ConvertSqlWithAttributes(dataRow, GetLikesForPost((Guid)dataRow[0]),
                    GetDislikesForPost((Guid)dataRow[0]), GetHashTagsForPost((Guid)dataRow[0]),
                    GetCommentsForPost((Guid)dataRow[0]), GetTaggedPeopleForPost((Guid)dataRow[0]),
                    GetContentsPathForPost((Guid)dataRow[0]))).ToList();
        }

        public void BanPost(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Post ");
            queryBuilder.Append("SET is_banned = 1 ");
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