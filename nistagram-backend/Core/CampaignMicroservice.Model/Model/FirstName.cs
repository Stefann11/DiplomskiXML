using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class FirstName : ValueObject
    {
        private readonly string name;

        private FirstName(string name)
        {
            this.name = name;
        }

        public static Result<FirstName> Create(string name)
        {
            if (name.Length > 50) return Result.Failure<FirstName>("First name cannot contain more than 50 characters");
            return Result.Success(new FirstName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(FirstName firstName) => firstName.name;
    }
}