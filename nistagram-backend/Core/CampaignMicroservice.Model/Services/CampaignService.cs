using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Services
{
    public class CampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly ExposureDateService _exposureDateService;
        private readonly AdService _adService;

        public CampaignService(ICampaignRepository campaignRepository, AdService adService,
            ExposureDateService exposureDateService)
        {
            _adService = adService;
            _exposureDateService = exposureDateService;
            _campaignRepository = campaignRepository;
        }

        public Result Save(Campaign campaign)
        {
            //if (_campaignRepository.GetById(campaign.Id).HasValue) return Result.Failure("Campaign with that id already exist");
            _campaignRepository.Save(campaign);
            if (campaign.GetType().Name.Equals("OneTimePostCampaign"))
            {
                OneTimePostCampaign oneTimePostCampaign = (OneTimePostCampaign)campaign;
                if (_exposureDateService.Save(oneTimePostCampaign.ExposureDate, campaign.Id).IsFailure) return Result.Failure("Exposure date with that id already exist");
            }
            else if (campaign.GetType().Name.Equals("OneTimeStoryCampaign"))
            {
                OneTimeStoryCampaign oneTimeStoryCampaign = (OneTimeStoryCampaign)campaign;
                if (_exposureDateService.Save(oneTimeStoryCampaign.ExposureDate, campaign.Id).IsFailure) return Result.Failure("Exposure date with that id already exist");
            }
            else if (campaign.GetType().Name.Equals("RecurringPostCampaign"))
            {
                RecurringPostCampaign recurringPostCampaign = (RecurringPostCampaign)campaign;
                foreach (ExposureDate exposureDate in recurringPostCampaign.ExposureDates)
                {
                    if (_exposureDateService.Save(exposureDate, campaign.Id).IsFailure) return Result.Failure("Exposure date with that id already exist");
                }
            }
            else
            {
                RecurringStoryCampaign recurringStoryCampaign = (RecurringStoryCampaign)campaign;
                foreach (ExposureDate exposureDate in recurringStoryCampaign.ExposureDates)
                {
                    if (_exposureDateService.Save(exposureDate, campaign.Id).IsFailure) return Result.Failure("Exposure date with that id already exist");
                }
            }
            foreach (Ad ad in campaign.Ads)
            {
                if (_adService.Save(ad, campaign.Id).IsFailure) return Result.Failure("Ad with that id already exist");
            }
            return Result.Success();
        }

        public Result Update()
        {
            List<CampaignMicroservice.Core.Model.ExposureDate> exposureDates = new List<CampaignMicroservice.Core.Model.ExposureDate>();
            List<CampaignMicroservice.Core.Model.Ad> ads = new List<CampaignMicroservice.Core.Model.Ad>();
            List<RegisteredUser> users = new List<RegisteredUser>();
            Agent agent = Agent.Create(Guid.NewGuid(), Username.Create("username").Value, FirstName.Create("firstName").Value, LastName.Create("lastName").Value,
                DateTime.Now, Gender.Create("male").Value, ProfileImagePath.Create("").Value, true, users, users, users, users, users, users, false, WebsiteAddress.Create("").Value).Value;
            foreach (CampaignUpdate campaign in _campaignRepository.GetAllCampaignUpdates())
            {
                if ((campaign.DateOfChange.AddDays(1) < DateTime.Now) && !campaign.IsUpdated)
                {
                    _campaignRepository.UpdateWithoutType(campaign);
                    _campaignRepository.UpdateCampaignUpdate(campaign.Id);
                }
            }
            return Result.Success();
        }
    }
}