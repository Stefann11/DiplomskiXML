using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class Bio : ValueObject
    {
        private readonly string bio;

        private Bio(string bio)
        {
            this.bio = bio;
        }

        public static Result<Bio> Create(string bio)
        {
            if (bio.Length > 100) return Result.Failure<Bio>("Bio cannot contain more than 100 characters");
            return Result.Success(new Bio(bio));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return bio;
        }

        public override string ToString()
        {
            return this.bio;
        }

        public static implicit operator string(Bio bio) => bio.bio;
    }
}