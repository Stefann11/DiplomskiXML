using CampaignMicroservice.Api.DTOs;
using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TargetAudience = CampaignMicroservice.Api.DTOs.TargetAudience;

namespace CampaignMicroservice.Api.Factories
{
    public class TargetAudienceFactory
    {
        public CampaignMicroservice.Core.Model.TargetAudience Create(TargetAudience targetAudience)
        {
            return CampaignMicroservice.Core.Model.TargetAudience
                .Create(targetAudience.MinDateOfBirth, targetAudience.MaxDateOfBirth, Gender.Create(targetAudience.Gender).Value).Value;
        }
    }
}