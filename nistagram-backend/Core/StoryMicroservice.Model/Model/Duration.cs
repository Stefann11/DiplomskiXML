using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.Model
{
    public class Duration : ValueObject
    {
        private readonly int duration;

        private Duration(int duration)
        {
            this.duration = duration;
        }

        public static Result<Duration> Create(int duration)
        {
            if (duration > 15) return Result.Failure<Duration>("Story cannot be longer than 15 seconds.");
            if (duration < 0) return Result.Failure<Duration>("Story duration cannot be negative number.");
            return Result.Success(new Duration(duration));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return duration;
        }

        public static implicit operator int(Duration duration) => duration.duration;
    }
}