using CSharpFunctionalExtensions;
using System;

namespace CampaignMicroservice.Core.Model
{
    public class CampaignRequest
    {
        public Guid Id { get; }
        public bool IsApproved { get; }
        public Campaign Campaign { get; }
        public VerifiedUser VerifiedUser { get; }
        public CampaignRequestAction CampaignRequestAction { get; }

        private CampaignRequest(Guid id, bool isApproved, Campaign campaign, VerifiedUser verifiedUser,
           CampaignRequestAction campaignRequestAction)
        {
            Id = id;
            IsApproved = isApproved;
            Campaign = campaign;
            VerifiedUser = verifiedUser;
            CampaignRequestAction = campaignRequestAction;
        }

        public static Result<CampaignRequest> Create(Guid id, bool isApproved, Campaign campaign,
            VerifiedUser verifiedUser, CampaignRequestAction campaignRequestAction)
        {
            return Result.Success(new CampaignRequest(id, isApproved, campaign, verifiedUser,
                campaignRequestAction));
        }
    }
}