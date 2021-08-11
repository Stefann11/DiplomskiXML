using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.DTOs
{
    public class ExposureDate
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<Guid> SeenByIds { get; set; }
    }
}