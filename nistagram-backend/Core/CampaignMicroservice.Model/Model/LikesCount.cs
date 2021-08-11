using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class LikesCount : ValueObject
    {
        private readonly int count;

        private LikesCount(int count)
        {
            this.count = count;
        }

        public static Result<LikesCount> Create(int count)
        {
            if (count < 0) return Result.Failure<LikesCount>("Number of likes can't be less than 0");
            return Result.Success(new LikesCount(count));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return count;
        }

        public override string ToString()
        {
            return count.ToString();
        }

        public static implicit operator string(LikesCount likesCount) => likesCount.count.ToString();

    }
}