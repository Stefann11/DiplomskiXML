using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class Username : ValueObject
    {
        private readonly string name;

        private Username(string name)
        {
            this.name = name;
        }

        public static Result<Username> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return Result.Failure<Username>("Username cannot be empty, or contain only white spaces");
            if (name.Length > 50) return Result.Failure<Username>("Username cannot contain more than 50 characters");
            return Result.Success(new Username(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(Username username) => username.name;
    }
}