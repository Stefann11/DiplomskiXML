using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using CampaignMicroservice.DataAccessAdapter;
using CampaignMicroservice.DataAccess.Adapter;

namespace CampaignMicroservice.DataAccessImplementation
{
    public class CampaignRepository : Repository, ICampaignRepository
    {
        private readonly CampaignAdapter _campaignTarget = new CampaignAdapter(new CampaignAdaptee());
        private readonly GetCampaignAdapter _getCampaignTarget = new GetCampaignAdapter(new GetCampaignAdaptee());
        private readonly CampaignUpdateAdapter _campaignUpdateTarget = new CampaignUpdateAdapter(new CampaignUpdateAdaptee());
        private readonly IExposureDateRepository _exposureDateRepository;
        private readonly IAdRepository _adRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISeenByRepository _seenByRepository;

        public CampaignRepository(IConfiguration configuration, IUserRepository userRepository,
            IExposureDateRepository exposureDateRepository, IAdRepository adRepository, ISeenByRepository seenByRepository) :
            base(configuration)
        {
            _userRepository = userRepository;
            _exposureDateRepository = exposureDateRepository;
            _adRepository = adRepository;
            _seenByRepository = seenByRepository;
        }

        public Maybe<Campaign> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT c.id, c.min_date_of_birth, " +
                "c.max_date_of_birth, c.gender, c.agent_id, a.username, a.first_name, a.last_name, " +
                "a.date_of_birth, a.gender, a.profile_image_path, a.is_private, a.is_banned, " +
                "a.website_address, c.likes_count, c.dislikes_count, c.exposure_count, c.click_count, " +
                "c.type, c.start_date, c.end_date, c.date_of_change ");
            queryBuilder.Append("FROM dbo.Campaign AS c, dbo.RegisteredUser AS a ");
            queryBuilder.Append("WHERE c.agent_id = a.id AND c.id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Campaign)_campaignTarget.ConvertSql(
                dataTable.Rows[0], _userRepository.GetBlockedBy(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _userRepository.GetBlocking(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _userRepository.GetFollowing(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _userRepository.GetFollowers(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _userRepository.GetMutedBy(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _userRepository.GetMuted(Guid.Parse(dataTable.Rows[0][4].ToString())),
                        _adRepository.GetAdsForCampaign(id),
                        _exposureDateRepository.GetExposureDatesForCampaign(id)
                );
            }
            return Maybe<Campaign>.None;
        }

        public IEnumerable<Campaign> GetBy(Guid agentId, Guid clientId)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT DISTINCT c.id, c.min_date_of_birth, " +
                "c.max_date_of_birth, c.gender, c.agent_id, " +
                "c.likes_count, c.dislikes_count, c.exposure_count, c.click_count, " +
                "c.type, c.start_date, c.end_date, c.date_of_change ");
            queryBuilder.Append("FROM dbo.Campaign AS c, dbo.RegisteredUser AS a, dbo.ExposureDates AS e ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (agentId != new Guid())
            {
                queryBuilder.Append("WHERE c.agent_id = a.id AND c.agent_id = @Id;");

                SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = agentId };

                parameters.Add(parameterId);
            }
            else if (clientId != new Guid())
            {
                queryBuilder.Append("WHERE e.campaign_id = c.id " +
                                    "AND((a.date_of_birth < c.max_date_of_birth " +
                                    "AND a.date_of_birth > c.min_date_of_birth " +
                                    "AND c.gender = a.gender) " +
                                    "OR(a.id in (select f.followed_by_id from dbo.Follows AS f where f.following_id = c.agent_id))) " +
                                    "AND e.exposure_date < GETDATE() " +
                                    "AND a.id not in (select s2.registered_user_id from dbo.SeenBy AS s2 where s2.exposure_date_id = e.id) " +
                                    "AND a.id = @ClientId");

                SqlParameter parameterId = new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier) { Value = clientId };

                parameters.Add(parameterId);
            }

            string query = queryBuilder.ToString();
            DataTable dataTable = ExecuteQuery(query, parameters);
            return (from DataRow dataRow in dataTable.Rows
                    select (Campaign)_getCampaignTarget.ConvertSql(dataRow,
                        (Agent)_userRepository.GetById(new Guid(dataTable.Rows[0][4].ToString())).Value,
                        _adRepository.GetAdsForCampaign(Guid.Parse(dataRow[0].ToString())),
                        _exposureDateRepository.GetExposureDatesForCampaign(Guid.Parse(dataRow[0].ToString())))).ToList();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Campaign campaign)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Campaign ");
            queryBuilder.Append("(id, agent_id, likes_count, dislikes_count, exposure_count, click_count, type, min_date_of_birth, max_date_of_birth, gender, start_date, end_date, date_of_change) ");
            queryBuilder.Append("VALUES (@Id, @Agent_id, @Likes_count, @Dislikes_count, @Exposure_count, @Click_count, @Type, @MinDateOfBirth, @MaxDateOfBirth, @Gender, @Start_date, @End_date, @Date_of_change);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = campaign.Id },
                 new SqlParameter("@Agent_id", SqlDbType.UniqueIdentifier) { Value = campaign.Agent.Id },
                 new SqlParameter("@Likes_count", SqlDbType.Int) { Value = int.Parse(campaign.CampaignStatistics.LikesCount.ToString()) },
                 new SqlParameter("@Dislikes_count", SqlDbType.Int) { Value = int.Parse(campaign.CampaignStatistics.DislikesCount.ToString()) },
                 new SqlParameter("@Exposure_count", SqlDbType.Int) { Value = int.Parse(campaign.CampaignStatistics.ExposureCount.ToString()) },
                 new SqlParameter("@Click_count", SqlDbType.Int) { Value = int.Parse(campaign.CampaignStatistics.ClickCount.ToString()) },
                 new SqlParameter("@Type", SqlDbType.NVarChar) { Value = campaign.GetType().Name },
                 new SqlParameter("@MinDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MinDateOfBirth },
                 new SqlParameter("@MaxDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MaxDateOfBirth},
                 new SqlParameter("@Gender", SqlDbType.NVarChar) { Value = campaign.TargetAudience.Gender.ToString()},
             };
            if (campaign.GetType().Name.Equals("RecurringPostCampaign"))
            {
                RecurringPostCampaign recurringPostCampaign = (RecurringPostCampaign)campaign;
                parameters.Add(new SqlParameter("@Start_date", SqlDbType.NVarChar) { Value = recurringPostCampaign.StartDate.ToString() });
                parameters.Add(new SqlParameter("@End_date", SqlDbType.NVarChar) { Value = recurringPostCampaign.EndDate.ToString() });
                parameters.Add(new SqlParameter("@Date_of_change", SqlDbType.NVarChar) { Value = DBNull.Value });
            }
            else if (campaign.GetType().Name.Equals("RecurringStoryCampaign"))
            {
                RecurringStoryCampaign recurringStoryCampaign = (RecurringStoryCampaign)campaign;
                parameters.Add(new SqlParameter("@Start_date", SqlDbType.NVarChar) { Value = recurringStoryCampaign.StartDate.ToString() });
                parameters.Add(new SqlParameter("@End_date", SqlDbType.NVarChar) { Value = recurringStoryCampaign.EndDate.ToString() });
                parameters.Add(new SqlParameter("@Date_of_change", SqlDbType.NVarChar) { Value = DBNull.Value });
            }
            else
            {
                parameters.Add(new SqlParameter("@Start_date", SqlDbType.NVarChar) { Value = DBNull.Value });
                parameters.Add(new SqlParameter("@End_date", SqlDbType.NVarChar) { Value = DBNull.Value });
                parameters.Add(new SqlParameter("@Date_of_change", SqlDbType.NVarChar) { Value = DBNull.Value });
            }

            ExecuteQuery(query, parameters);
        }

        public void Update(Campaign campaign)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Campaign ");
            queryBuilder.Append("SET min_date_of_birth = @MinDateOfBirth, max_date_of_birth = @MaxDateOfBirth, gender = @Gender ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = campaign.Id },
                 new SqlParameter("@MinDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MinDateOfBirth.ToString()},
                 new SqlParameter("@MaxDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MaxDateOfBirth.ToString()},
                 new SqlParameter("@Gender", SqlDbType.NVarChar) { Value = campaign.TargetAudience.Gender.ToString()},
             };

            ExecuteQuery(query, parameters);
        }

        public void UpdateWithoutType(CampaignUpdate campaign)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Campaign ");
            queryBuilder.Append("SET min_date_of_birth = @MinDateOfBirth, max_date_of_birth = @MaxDateOfBirth, gender = @Gender ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = campaign.CampaignId },
                 new SqlParameter("@MinDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MinDateOfBirth.ToString()},
                 new SqlParameter("@MaxDateOfBirth", SqlDbType.NVarChar) { Value = campaign.TargetAudience.MaxDateOfBirth.ToString()},
                 new SqlParameter("@Gender", SqlDbType.NVarChar) { Value = campaign.TargetAudience.Gender.ToString()},
             };

            ExecuteQuery(query, parameters);
        }

        public void SaveCampaignUpdate(CampaignUpdate campaignUpdate)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.CampaignUpdates ");
            queryBuilder.Append("(id, campaign_id, min_date_of_birth, max_date_of_birth, gender, date_of_change, is_updated) ");
            queryBuilder.Append("VALUES (@Id, @CampaignId, @MinDateOfBirth, @MaxDateOfBirth, @Gender, @Date_of_change, @IsUpdated);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = campaignUpdate.Id },
                 new SqlParameter("@CampaignId", SqlDbType.UniqueIdentifier) { Value = campaignUpdate.CampaignId },
                 new SqlParameter("@MinDateOfBirth", SqlDbType.NVarChar) { Value = campaignUpdate.TargetAudience.MinDateOfBirth },
                 new SqlParameter("@MaxDateOfBirth", SqlDbType.NVarChar) { Value = campaignUpdate.TargetAudience.MaxDateOfBirth},
                 new SqlParameter("@Gender", SqlDbType.NVarChar) { Value = campaignUpdate.TargetAudience.Gender.ToString()},
                 new SqlParameter("@IsUpdated", SqlDbType.Bit) { Value = campaignUpdate.IsUpdated},
                 new SqlParameter("@Date_of_change", SqlDbType.NVarChar) { Value = campaignUpdate.DateOfChange.ToString()},
             };
            ExecuteQuery(query, parameters);
        }

        public void UpdateCampaignUpdate(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.CampaignUpdates ");
            queryBuilder.Append("SET is_updated = 1 ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        public IEnumerable<CampaignUpdate> GetAllCampaignUpdates()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT * ");
            queryBuilder.Append("FROM dbo.CampaignUpdates AS c ");
            queryBuilder.Append("ORDER BY c.date_of_change ASC");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (CampaignUpdate)_campaignUpdateTarget.ConvertSql(dataRow)).ToList();
        }

        public void Delete(Guid id)
        {
            DeleteAds(id);
            DeleteCampaignUpdates(id);
            DeleteCampaignRequests(id);
            DeleteFromExposureDates(id);
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Campaign ");
            queryBuilder.Append("WHERE id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        private void DeleteFromExposureDates(Guid campaignId)
        {
            List<ExposureDate> exposureDates = (List<ExposureDate>)_exposureDateRepository.GetExposureDatesForCampaign(campaignId);
            foreach (ExposureDate exposureDate in exposureDates)
            {
                DeleteFromSeenBy(exposureDate.Id);
            }
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.ExposureDates ");
            queryBuilder.Append("WHERE campaign_id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = campaignId },
             };

            ExecuteQuery(query, parameters);
        }

        private void DeleteFromSeenBy(Guid exposureDateId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.SeenBy ");
            queryBuilder.Append("WHERE exposure_date_id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = exposureDateId },
             };

            ExecuteQuery(query, parameters);
        }

        private void DeleteCampaignRequests(Guid campaignId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.CampaignRequest ");
            queryBuilder.Append("WHERE campaign_id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = campaignId },
             };

            ExecuteQuery(query, parameters);
        }

        private void DeleteCampaignUpdates(Guid campaignId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.CampaignUpdates ");
            queryBuilder.Append("WHERE campaign_id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = campaignId },
             };

            ExecuteQuery(query, parameters);
        }

        private void DeleteAds(Guid campaignId)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Ad ");
            queryBuilder.Append("WHERE campaign_id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = campaignId },
             };

            ExecuteQuery(query, parameters);
        }
    }
}