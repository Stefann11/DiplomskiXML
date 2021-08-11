using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class HashTagText : ValueObject
    {
        private readonly string text;

        private HashTagText(string text)
        {
            this.text = text;
        }

        public static Result<HashTagText> Create(string text)
        {
            if (text.Length > 50) return Result.Failure<HashTagText>("Hash tag text cannot contain more than 50 characters");
            if (!string.IsNullOrEmpty(text) && !text.Contains("#")) return Result.Failure<HashTagText>("Hash tag text have to contains #");
            return Result.Success(new HashTagText(text));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return text;
        }

        public override string ToString()
        {
            return this.text;
        }

        public static implicit operator string(HashTagText hashTagText) => hashTagText.text;
    }
}