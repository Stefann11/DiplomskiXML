using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class ClickCount : ValueObject
    {
        private readonly int count;

        private ClickCount(int count)
        {
            this.count = count;
        }

        public static Result<ClickCount> Create(int count)
        {
            if (count < 0) return Result.Failure<ClickCount>("Number of clicks can't be less than 0");
            return Result.Success(new ClickCount(count));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return count;
        }

        public override string ToString()
        {
            return count.ToString();
        }

        public static implicit operator string(ClickCount clickCount) => clickCount.count.ToString();
    }
}