using CampaignMicroservice.Api.DTOs;
using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ad = CampaignMicroservice.Api.DTOs.Ad;

namespace CampaignMicroservice.Api.Factories
{
    public class AdFactory
    {
        public CampaignMicroservice.Core.Model.Ad Create(Ad ad, CampaignMicroservice.Core.Model.RegisteredUser profileOwner)
        {
            Content content;
            if (ad.Type.Equals("Post")) content = Post.Create(ad.ContentId).Value;
            else content = Story.Create(ad.ContentId).Value;
            return CampaignMicroservice.Core.Model.Ad.Create(ad.Id, content, Link.Create(ad.Link).Value,
                ClickCount.Create(ad.ClickCount).Value, profileOwner).Value;
        }
    }
}