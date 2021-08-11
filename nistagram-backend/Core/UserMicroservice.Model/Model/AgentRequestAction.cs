using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class AgentRequestAction : ValueObject
    {
        private readonly string name;

        private AgentRequestAction(string name)
        {
            this.name = name;
        }

        public static Result<AgentRequestAction> Create(string name)
        {
            if (name.Length > 250) return Result.Failure<AgentRequestAction>("Agent Request Action cannot contain more than 250 characters");
            return Result.Success(new AgentRequestAction(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(AgentRequestAction firstName) => firstName.name;
    }
}