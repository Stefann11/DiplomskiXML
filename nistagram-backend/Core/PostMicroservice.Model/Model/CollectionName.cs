using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class CollectionName : ValueObject
    {
        private readonly string name;

        private CollectionName(string name)
        {
            this.name = name;
        }

        public static Result<CollectionName> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return Result.Failure<CollectionName>("Collection name cannot be empty, or contain only white spaces");
            if (name.Length > 50) return Result.Failure<CollectionName>("Collection name cannot contain more than 50 characters");
            return Result.Success(new CollectionName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(CollectionName CollectionName) => CollectionName.name;
    }
}