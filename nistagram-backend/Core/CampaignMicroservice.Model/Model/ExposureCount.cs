using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class ExposureCount : ValueObject
    {
        private readonly int count;

        private ExposureCount(int count)
        {
            this.count = count;
        }

        public static Result<ExposureCount> Create(int count)
        {
            if (count < 0) return Result.Failure<ExposureCount>("Number of exposures can't be less than 0");
            return Result.Success(new ExposureCount(count));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return count;
        }

        public override string ToString()
        {
            return count.ToString();
        }

        public static implicit operator string(ExposureCount exposureCount) => exposureCount.count.ToString();
    }
}