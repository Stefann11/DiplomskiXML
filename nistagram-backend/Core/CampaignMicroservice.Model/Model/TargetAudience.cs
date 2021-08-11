using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class TargetAudience : ValueObject
    {
        public DateTime MinDateOfBirth { get; }
        public DateTime MaxDateOfBirth { get; }
        public Gender Gender { get; }

        private TargetAudience(DateTime minDateOfBirth, DateTime maxDateOfBirth, Gender gender)
        {
            MinDateOfBirth = minDateOfBirth;
            MaxDateOfBirth = maxDateOfBirth;
            Gender = gender;
        }

        public static Result<TargetAudience> Create(DateTime minDateOfBirth, DateTime maxDateOfBirth,
            Gender gender)
        {
            return Result.Success(new TargetAudience(minDateOfBirth, maxDateOfBirth, gender));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MinDateOfBirth;
            yield return MaxDateOfBirth;
            yield return Gender;
        }
    }
}