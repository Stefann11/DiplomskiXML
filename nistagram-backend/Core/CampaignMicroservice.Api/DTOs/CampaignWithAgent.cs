using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.DTOs
{
    public class CampaignWithAgent
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public TargetAudience TargetAudience { get; set; }
        public Agent Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateOfChange { get; set; }
        public IEnumerable<ExposureDate> ExposureDates { get; set; }
        public IEnumerable<Ad> Ads { get; set; }
    }
}