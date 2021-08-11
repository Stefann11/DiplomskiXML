using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class CampaignRequestAdapter
    {
        private readonly CampaignRequestAdaptee campaignRequestAdaptee;

        public CampaignRequestAdapter(CampaignRequestAdaptee campaignRequestAdaptee)
        {
            this.campaignRequestAdaptee = campaignRequestAdaptee;
        }

        public object ConvertSql(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
                IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
                IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates,
                IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
                IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified,
                IEnumerable<RegisteredUser> mutedByUsersVerified, IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            return campaignRequestAdaptee.ConvertSqlDataReaderToCampaignRequest(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates, blockedByUsersVerified, blockedUsersVerified, followingVerified, followersVerified, mutedByUsersVerified,
                    mutedUsersVerified);
        }
    }
}