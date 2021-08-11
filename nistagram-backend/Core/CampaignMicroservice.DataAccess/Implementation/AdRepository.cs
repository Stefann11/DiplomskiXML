using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessAdapter;
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
    public class AdRepository : Repository, IAdRepository
    {
        private readonly AdAdapter _adAdapter = new AdAdapter(new AdAdaptee());
        private readonly IUserRepository _userRepository;

        public AdRepository(IConfiguration configuration, IUserRepository userRepository) : base(configuration)
        {
            _userRepository = userRepository;
        }

        public Maybe<Ad> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Ad ");
            queryBuilder.Append("WHERE id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Ad)_adAdapter.ConvertSql(
                dataTable.Rows[0], _userRepository.GetById(new Guid(dataTable.Rows[0][5].ToString())).Value
                );
            }
            return Maybe<Ad>.None;
        }

        public void Save(Ad ad, Guid campaignId)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Ad ");
            queryBuilder.Append("(id, content_id, link, click_count, campaign_id, registered_user_id, type) ");
            queryBuilder.Append("VALUES (@Id, @Content_id, @Link, @Click_count, @Campaign_id, @Registered_user_id, @Type);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = ad.Id },
                 new SqlParameter("@Content_id", SqlDbType.UniqueIdentifier) { Value = ad.Content.Id },
                 new SqlParameter("@Link", SqlDbType.NVarChar) { Value = ad.Link.ToString() },
                 new SqlParameter("@Click_count", SqlDbType.Int) { Value = 0 },
                 new SqlParameter("@Campaign_id", SqlDbType.UniqueIdentifier) { Value = campaignId},
                 new SqlParameter("@Registered_user_id", SqlDbType.UniqueIdentifier) { Value = ad.ProfileOwner.Id },
                 new SqlParameter("@Type", SqlDbType.NVarChar) { Value = ad.Content.GetType().Name },
             };

            ExecuteQuery(query, parameters);
        }

        public IEnumerable<Ad> GetAdsForCampaign(Guid campaignId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Ad ");
            queryBuilder.Append("WHERE campaign_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = campaignId };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Ad)_adAdapter.ConvertSql(dataRow, _userRepository.GetById(new Guid(dataRow[5].ToString())).Value)).ToList();
        }

        public Maybe<Ad> GetByContentId(Guid contentId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.Ad ");
            queryBuilder.Append("WHERE content_id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = contentId };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Ad)_adAdapter.ConvertSql(
                dataTable.Rows[0], _userRepository.GetById(new Guid(dataTable.Rows[0][5].ToString())).Value
                );
            }
            return Maybe<Ad>.None;
        }
    }
}