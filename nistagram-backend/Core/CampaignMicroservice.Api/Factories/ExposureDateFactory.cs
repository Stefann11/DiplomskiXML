using CampaignMicroservice.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.Factories
{
    public class ExposureDateFactory
    {
        public CampaignMicroservice.Core.Model.ExposureDate Create(ExposureDate exposureDate, IEnumerable<CampaignMicroservice.Core.Model.RegisteredUser> seenBy)
        {
            return CampaignMicroservice.Core.Model.ExposureDate.Create(exposureDate.Id, exposureDate.Time, seenBy).Value;
        }
    }
}