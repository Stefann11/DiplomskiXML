using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class Description : ValueObject
    {
        private readonly string description;

        private Description(string description)
        {
            this.description = description;
        }

        public static Result<Description> Create(string description)
        {
            if (description.Length > 200) return Result.Failure<Description>("Description cannot contain more than 200 characters");
            return Result.Success(new Description(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return description;
        }

        public override string ToString()
        {
            return this.description;
        }

        public static implicit operator string(Description description) => description.description;
    }
}