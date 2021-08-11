using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.DTOs
{
    public class TargetAudience
    {
        public DateTime MinDateOfBirth { get; set; }
        public DateTime MaxDateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}