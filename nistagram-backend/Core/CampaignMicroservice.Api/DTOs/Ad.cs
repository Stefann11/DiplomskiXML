using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.DTOs
{
    public class Ad
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public int ClickCount { get; set; }
        public Guid ProfileOwnerId { get; set; }
    }
}