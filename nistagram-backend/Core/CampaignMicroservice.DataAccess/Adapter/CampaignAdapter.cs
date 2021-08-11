using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class CampaignAdapter
    {
        private readonly CampaignAdaptee campaignAdaptee;

        public CampaignAdapter(CampaignAdaptee campaignAdaptee)
        {
            this.campaignAdaptee = campaignAdaptee;
        }

        public object ConvertSql(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
                IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
                IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates)
        {
            return campaignAdaptee.ConvertSqlDataReaderToCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates);
        }
    }
}