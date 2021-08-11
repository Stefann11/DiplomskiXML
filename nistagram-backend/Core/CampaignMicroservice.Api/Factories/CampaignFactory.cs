using CampaignMicroservice.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampaignMicroservice.Api.Factories
{
    public class CampaignFactory
    {
        public Campaign Create(Core.Model.Campaign campaign)
        {
            if (campaign.GetType().Name.Equals("OneTimePostCampaign"))
            {
                Core.Model.OneTimePostCampaign oneTimePostCampaign = (Core.Model.OneTimePostCampaign)campaign;
                return new Campaign
                {
                    Id = campaign.Id,
                    Type = campaign.GetType().Name,
                    TargetAudience = new TargetAudience
                    {
                        MinDateOfBirth = campaign.TargetAudience.MinDateOfBirth,
                        MaxDateOfBirth = campaign.TargetAudience.MaxDateOfBirth,
                        Gender = campaign.TargetAudience.Gender
                    },
                    AgentId = campaign.Agent.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    DateOfChange = DateTime.Now,
                    ExposureDates = new List<ExposureDate>() { ConvertExposureDate(oneTimePostCampaign.ExposureDate) },
                    Ads = ConvertAds(campaign.Ads)
                };
            }
            else if (campaign.GetType().Name.Equals("OneTimeStoryCampaign"))
            {
                Core.Model.OneTimeStoryCampaign oneTimeStoryCampaign = (Core.Model.OneTimeStoryCampaign)campaign;
                return new Campaign
                {
                    Id = campaign.Id,
                    Type = campaign.GetType().Name,
                    TargetAudience = new TargetAudience
                    {
                        MinDateOfBirth = campaign.TargetAudience.MinDateOfBirth,
                        MaxDateOfBirth = campaign.TargetAudience.MaxDateOfBirth,
                        Gender = campaign.TargetAudience.Gender
                    },
                    AgentId = campaign.Agent.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    DateOfChange = DateTime.Now,
                    ExposureDates = new List<ExposureDate>() { ConvertExposureDate(oneTimeStoryCampaign.ExposureDate) },
                    Ads = ConvertAds(campaign.Ads)
                };
            }
            else if (campaign.GetType().Name.Equals("RecurringPostCampaign"))
            {
                Core.Model.RecurringPostCampaign reccuringPostCampaign = (Core.Model.RecurringPostCampaign)campaign;
                return new Campaign
                {
                    Id = campaign.Id,
                    Type = campaign.GetType().Name,
                    TargetAudience = new TargetAudience
                    {
                        MinDateOfBirth = campaign.TargetAudience.MinDateOfBirth,
                        MaxDateOfBirth = campaign.TargetAudience.MaxDateOfBirth,
                        Gender = campaign.TargetAudience.Gender
                    },
                    AgentId = campaign.Agent.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    DateOfChange = DateTime.Now,
                    ExposureDates = ConvertExposureDates(reccuringPostCampaign.ExposureDates),
                    Ads = ConvertAds(campaign.Ads)
                };
            }
            else
            {
                Core.Model.RecurringStoryCampaign reccuringStoryCampaign = (Core.Model.RecurringStoryCampaign)campaign;
                return new Campaign
                {
                    Id = campaign.Id,
                    Type = campaign.GetType().Name,
                    TargetAudience = new TargetAudience
                    {
                        MinDateOfBirth = campaign.TargetAudience.MinDateOfBirth,
                        MaxDateOfBirth = campaign.TargetAudience.MaxDateOfBirth,
                        Gender = campaign.TargetAudience.Gender
                    },
                    AgentId = campaign.Agent.Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    DateOfChange = DateTime.Now,
                    ExposureDates = ConvertExposureDates(reccuringStoryCampaign.ExposureDates),
                    Ads = ConvertAds(campaign.Ads)
                };
            }
        }

        public IEnumerable<Campaign> CreateCampaigns(IEnumerable<Core.Model.Campaign> campaigns)
        {
            return campaigns.Select(campaign => Create(campaign)).ToList();
        }

        private Ad ConvertAd(Core.Model.Ad ad)
        {
            return new Ad
            {
                Id = ad.Id,
                ContentId = ad.Content.Id,
                Type = ad.Content.GetType().Name,
                Link = ad.Link,
                ClickCount = int.Parse(ad.ClickCount),
                ProfileOwnerId = ad.ProfileOwner.Id
            };
        }

        private IEnumerable<Ad> ConvertAds(IEnumerable<Core.Model.Ad> ads)
        {
            return ads.Select(ad => ConvertAd(ad)).ToList();
        }

        private ExposureDate ConvertExposureDate(Core.Model.ExposureDate exposureDate)
        {
            return exposureDate != null ?
             new ExposureDate
             {
                 Id = exposureDate.Id,
                 Time = exposureDate.Time,
                 SeenByIds = ConvertSeenByIds(exposureDate.SeenBy)
             } : null;
        }

        private IEnumerable<ExposureDate> ConvertExposureDates(IEnumerable<Core.Model.ExposureDate> exposureDates)
        {
            return exposureDates.Select(exposureDate => ConvertExposureDate(exposureDate)).ToList();
        }

        private IEnumerable<Guid> ConvertSeenByIds(IEnumerable<Core.Model.RegisteredUser> seenBy)
        {
            return (from Core.Model.RegisteredUser user in seenBy
                    select user.Id).ToList();
        }
    }
}