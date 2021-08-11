using System;

namespace CampaignMicroservice.Api.DTOs
{
    public class CreateAdDto
    {
        public Ad Ad { get; set; }
        public Guid CampaignId { get; set; }
    }
}