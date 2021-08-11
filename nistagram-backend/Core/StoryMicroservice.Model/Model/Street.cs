using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class Street : ValueObject
    {
        private readonly string name;

        private Street(string name)
        {
            this.name = name;
        }

        public static Result<Street> Create(string name)
        {
            if (name.Length > 50) return Result.Failure<Street>("Street cannot contain more than 50 characters");
            return Result.Success(new Street(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(Street street) => street.name;
    }
}