using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Core.Model
{
    public class VerificationRequestFirstName : ValueObject
    {
        private readonly string name;

        private VerificationRequestFirstName(string name)
        {
            this.name = name;
        }

        public static Result<VerificationRequestFirstName> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return Result.Failure<VerificationRequestFirstName>("First name cannot be empty");
            if (name.Length > 50) return Result.Failure<VerificationRequestFirstName>("First name cannot contain more than 50 characters");
            return Result.Success(new VerificationRequestFirstName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(VerificationRequestFirstName firstName) => firstName.name;
    }
}