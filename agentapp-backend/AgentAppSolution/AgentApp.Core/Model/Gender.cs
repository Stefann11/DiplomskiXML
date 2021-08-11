using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class Gender : ValueObject
    {
        private readonly string gender;

        private Gender(string gender)
        {
            this.gender = gender;
        }

        public static Result<Gender> Create(string gender)
        {
            if (gender.ToLower().Equals("male")) return Result.Success(new Gender(gender));
            if (gender.ToLower().Equals("female")) return Result.Success(new Gender(gender));
            return Result.Failure<Gender>("Gender has to be either 'male' or 'female'");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return gender;
        }

        public override string ToString()
        {
            return this.gender;
        }

        public static implicit operator string(Gender gender) => gender.gender;
    }
}