using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class DislikesCount : ValueObject
    {
        private readonly int count;

        private DislikesCount(int count)
        {
            this.count = count;
        }

        public static Result<DislikesCount> Create(int count)
        {
            if (count < 0) return Result.Failure<DislikesCount>("Number of dislikes can't be less than 0");
            return Result.Success(new DislikesCount(count));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return count;
        }

        public override string ToString()
        {
            return count.ToString();
        }

        public static implicit operator string(DislikesCount dislikesCount) => dislikesCount.count.ToString();
    }
}