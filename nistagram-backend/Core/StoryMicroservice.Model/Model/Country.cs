using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class Country : ValueObject
    {
        private readonly string name;

        private Country(string name)
        {
            this.name = name;
        }

        public static Result<Country> Create(string name)
        {
            if (name.Length > 50) return Result.Failure<Country>("Country cannot contain more than 50 characters");
            return Result.Success(new Country(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(Country country) => country.name;
    }
}