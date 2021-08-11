using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Core.Model
{
    public class EmailAddress : ValueObject
    {
        private readonly string address;

        private EmailAddress(string address)
        {
            this.address = address;
        }

        public static Result<EmailAddress> Create(string address)
        {
            if (!new EmailAddressAttribute().IsValid(address)) return Result.Failure<EmailAddress>("Email address is not valid");
            return Result.Success(new EmailAddress(address));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return address;
        }

        public override string ToString()
        {
            return this.address;
        }

        public static implicit operator string(EmailAddress emailAddress) => emailAddress.address;
    }
}