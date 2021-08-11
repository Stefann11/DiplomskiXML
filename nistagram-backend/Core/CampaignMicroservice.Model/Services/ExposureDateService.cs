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
    public class ExposureDateService
    {
        private readonly IExposureDateRepository _exposureDateRepository;

        public ExposureDateService(IExposureDateRepository exposureDateRepository, ICampaignRepository campaignRepository)
        {
            _exposureDateRepository = exposureDateRepository;
        }

        public Result Save(ExposureDate exposureDate, Guid campaignId)
        {
            if (_exposureDateRepository.GetById(exposureDate.Id).HasValue) return Result.Failure("Exposure date with that id already exist");
            _exposureDateRepository.Save(exposureDate, campaignId);
            return Result.Success();
        }
    }
}