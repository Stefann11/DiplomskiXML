using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class Name : ValueObject
    {
        private readonly string name;

        private Name(string name)
        {
            this.name = name;
        }

        public static Result<Name> Create(string name)
        {
            if (name.Length > 50) return Result.Failure<Name>("First name cannot contain more than 50 characters");
            return Result.Success(new Name(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(Name firstName) => firstName.name;
    }
}