using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class AvailableCount : ValueObject
    {
        private readonly int count;

        private AvailableCount(int count)
        {
            this.count = count;
        }

        public static Result<AvailableCount> Create(int count)
        {
            if (count < 0) return Result.Failure<AvailableCount>("Available count can't be less than 0");
            return Result.Success(new AvailableCount(count));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return count;
        }

        public override string ToString()
        {
            return this.count.ToString();
        }

        public static implicit operator string(AvailableCount clickCount) => clickCount.count.ToString();
    }
}