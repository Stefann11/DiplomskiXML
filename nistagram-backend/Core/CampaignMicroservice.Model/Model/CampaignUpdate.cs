using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Model
{
    public class CampaignUpdate
    {
        public Guid Id { get; }
        public Guid CampaignId { get; }
        public TargetAudience TargetAudience { get; }
        public DateTime DateOfChange { get; }
        public bool IsUpdated { get; }

        private CampaignUpdate(Guid id, Guid campaignId, TargetAudience targetAudience, DateTime dateOfChange, bool isUpdated)
        {
            Id = id;
            CampaignId = campaignId;
            TargetAudience = targetAudience;
            DateOfChange = dateOfChange;
            IsUpdated = isUpdated;
        }

        public static Result<CampaignUpdate> Create(Guid id, Guid campaignId, TargetAudience targetAudience, DateTime dateOfChange, bool isUpdated)
        {
            return Result.Success(new CampaignUpdate(id, campaignId, targetAudience, dateOfChange, isUpdated));
        }
    }
}