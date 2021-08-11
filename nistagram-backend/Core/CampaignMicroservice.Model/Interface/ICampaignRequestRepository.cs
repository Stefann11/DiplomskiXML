using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Interface
{
    public interface ICampaignRequestRepository
    {
        public CampaignRequest Save(CampaignRequest campaignRequest);

        public CampaignRequest Update(CampaignRequest campaignRequest);

        public IEnumerable<CampaignRequest> GetBy(Guid userId, string isApproved, string action);
    }
}