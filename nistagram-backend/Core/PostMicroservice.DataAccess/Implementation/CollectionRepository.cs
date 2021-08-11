using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Adapter;
using PostMicroservice.DataAccess.Target;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMicroservice.DataAccess.Implementation
{
    public class CollectionRepository : Repository, ICollectionRepository
    {
        public ITarget _collectionTarget = new CollectionAdapter(new CollectionAdaptee());

        public CollectionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Collection> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<Collection> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Collection> GetByUserId(Guid userid)
        {
            StringBuilder queryBuilder = new StringBuilder("select c.id, c.collection_name, r.id, " +
                "r.username, r.first_name, r.last_name, r.isPrivate, r.isAcceptingTags, r.profilePicturePath ");
            queryBuilder.Append("from dbo.Collection as c, dbo.RegisteredUser as r ");
            queryBuilder.Append("WHERE c.registered_user_id = r.id AND c.registered_user_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = userid };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Collection)_collectionTarget.ConvertSql(dataRow)).ToList();
        }

        public Collection Save(Collection collection)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Collection ");
            queryBuilder.Append("(id, collection_name, registered_user_id) ");
            queryBuilder.Append("VALUES (@id, @collection_name, @registered_user_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@collection_name", SqlDbType.NVarChar) { Value = collection.CollectionName.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = collection.RegisteredUser.Id }
            };

            ExecuteQuery(query, parameters);
            return collection;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Collection Edit(Collection collection)
        {
            throw new NotImplementedException();
        }

        public Result AddPostToCollection(Guid id, Guid postId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.CollectionContent ");
            queryBuilder.Append("(id, collection_id, post_id) ");
            queryBuilder.Append("VALUES (@id, @collection_id, @post_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                new SqlParameter("@collection_id", SqlDbType.UniqueIdentifier) { Value = id },
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier) { Value = postId }
            };

            ExecuteQuery(query, parameters);
            return Result.Success();
        }
    }
}