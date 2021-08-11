using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class LastName : ValueObject
    {
        private readonly string name;

        private LastName(string name)
        {
            this.name = name;
        }

        public static Result<LastName> Create(string name)
        {
            if (name.Length > 50) return Result.Failure<LastName>("Last name cannot contain more than 50 characters");
            return Result.Success(new LastName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(LastName lastName) => lastName.name;
    }
}