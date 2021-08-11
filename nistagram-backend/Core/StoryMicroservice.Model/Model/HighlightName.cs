using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class HighlightName : ValueObject
    {
        private readonly string name;

        private HighlightName(string name)
        {
            this.name = name;
        }

        public static Result<HighlightName> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return Result.Failure<HighlightName>("Highlight name cannot be empty, or contain only white spaces");
            if (name.Length > 50) return Result.Failure<HighlightName>("Highlight name cannot contain more than 50 characters");
            return Result.Success(new HighlightName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public static implicit operator string(HighlightName HighlightName) => HighlightName.name;
    }
}