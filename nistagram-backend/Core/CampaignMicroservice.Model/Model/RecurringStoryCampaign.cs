using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class RecurringStoryCampaign : Campaign
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public IEnumerable<ExposureDate> ExposureDates { get; }
        public DateTime DateOfChange { get; }

        private RecurringStoryCampaign(Guid id, TargetAudience targetAudience, Agent agent,
            CampaignStatistics campaignStatistics, DateTime startDate, DateTime endDate,
            IEnumerable<ExposureDate> exposureDates, DateTime dateOfChange, IEnumerable<Ad> ads)
            : base(id, targetAudience, agent, campaignStatistics, ads)
        {
            StartDate = startDate;
            EndDate = endDate;
            ExposureDates = exposureDates;
            DateOfChange = dateOfChange;
        }

        public static Result<RecurringStoryCampaign> Create(Guid id, TargetAudience targetAudience, Agent agent,
            CampaignStatistics campaignStatistics, DateTime startDate, DateTime endDate,
            IEnumerable<ExposureDate> exposureDates, DateTime dateOfChange, IEnumerable<Ad> ads)
        {
            return Result.Success(new RecurringStoryCampaign(id, targetAudience, agent, campaignStatistics,
                startDate, endDate, exposureDates, dateOfChange, ads));
        }
    }
}