using System;

namespace CampaignMicroservice.Api.DTOs
{
    public class CampaignRequest
    {
        public Guid Id { get; set; }
        public bool IsApproved { get; set; }
        public CampaignWithAgent Campaign { get; set; }
        public VerifiedUser VerifiedUser { get; set; }
        public string CampaignRequestAction { get; set; }
    }
}