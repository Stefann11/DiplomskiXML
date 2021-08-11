using CSharpFunctionalExtensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public abstract class Campaign
    {
        public Guid Id { get; }
        public TargetAudience TargetAudience { get; }
        public Agent Agent { get; }
        public CampaignStatistics CampaignStatistics { get; }
        public IEnumerable<Ad> Ads { get; }

        protected Campaign(Guid id, TargetAudience targetAudience, Agent agent,
            CampaignStatistics campaignStatistics, IEnumerable<Ad> ads)
        {
            Id = id;
            TargetAudience = targetAudience;
            Agent = agent;
            CampaignStatistics = campaignStatistics;
            Ads = ads;
        }
    }
}