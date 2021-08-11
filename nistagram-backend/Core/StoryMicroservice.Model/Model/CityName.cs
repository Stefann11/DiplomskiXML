using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class CityName : ValueObject
    {
        private readonly string name;

        private CityName(string name)
        {
            this.name = name;
        }

        public static Result<CityName> Create(string name)
        {
            if (name.Length > 30) return Result.Failure<CityName>("City name cannot contain more than 30 characters");
            return Result.Success(new CityName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(CityName cityName) => cityName.name;
    }
}