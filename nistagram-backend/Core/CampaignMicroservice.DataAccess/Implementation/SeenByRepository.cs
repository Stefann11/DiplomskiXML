using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.DataAccessImplementation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Implementation
{
    public class SeenByRepository : Repository, ISeenByRepository
    {
        public SeenByRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void Save(Guid exposureDateId, Guid registeredUserId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.SeenBy ");
            queryBuilder.Append("(id, registered_user_id, exposure_date_id) ");
            queryBuilder.Append("VALUES (@Id, @Registered_user_id, @Exposure_date_id);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = Guid.NewGuid() },
                 new SqlParameter("@Registered_user_id", SqlDbType.UniqueIdentifier) { Value = registeredUserId},
                 new SqlParameter("@Exposure_date_id", SqlDbType.UniqueIdentifier) { Value = exposureDateId },
             };

            ExecuteQuery(query, parameters);
        }
    }
}