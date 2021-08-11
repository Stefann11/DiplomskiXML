using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Services
{
    public class AdService
    {
        private readonly IAdRepository _adRepository;

        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public Result Save(Ad ad, Guid campaignId)
        {
            if (_adRepository.GetById(ad.Id).HasValue) return Result.Failure("Ad with that id already exist");
            _adRepository.Save(ad, campaignId);
            return Result.Success();
        }
    }
}