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
    public class HashTagRepository : Repository, IHashTagRepository
    {
        public ITarget _hashTagTarget = new HashTagAdapter(new HashTagAdaptee());

        public HashTagRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<HashTag> GetByText(string text)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT distinct text ");
            queryBuilder.Append("FROM dbo.HashTags ");
            queryBuilder.Append("WHERE LOWER(text) LIKE LOWER(@Text);");

            string query = queryBuilder.ToString();

            SqlParameter parameterText = new SqlParameter("@Text", SqlDbType.NVarChar) { Value = "%" + text + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterText };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (HashTag)_hashTagTarget.ConvertSql(dataRow));
        }
    }
}