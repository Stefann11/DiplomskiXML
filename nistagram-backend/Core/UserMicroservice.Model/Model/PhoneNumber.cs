using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class PhoneNumber : ValueObject
    {
        private readonly string number;

        private PhoneNumber(string number)
        {
            this.number = number;
        }

        public static Result<PhoneNumber> Create(string number)
        {
            if (number.Length > 15) return Result.Failure<PhoneNumber>("Phone number cannot contain more than 15 characters");
            return Result.Success(new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return number;
        }

        public override string ToString()
        {
            return this.number;
        }

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.number;
    }
}