using CampaignMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace CampaignMicroservice.Api.Factories
{
    public class DTOAdFactory
    {
        public Ad Create(Core.Model.Ad ad)
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

        public IEnumerable<Ad> CreateAds(IEnumerable<Core.Model.Ad> ads)
        {
            return ads.Select(ad => Create(ad)).ToList();
        }
    }
}