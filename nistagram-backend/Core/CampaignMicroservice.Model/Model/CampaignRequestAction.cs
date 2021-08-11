using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class CampaignRequestAction : ValueObject
    {
        private readonly string action;

        private CampaignRequestAction(string action)
        {
            this.action = action;
        }

        public static Result<CampaignRequestAction> Create(string action)
        {
            if (String.IsNullOrWhiteSpace(action)) return Result.Failure<CampaignRequestAction>("Action cannot be empty, or contain only white spaces");
            if (action.Length > 50) return Result.Failure<CampaignRequestAction>("Action cannot contain more than 50 characters");
            return Result.Success(new CampaignRequestAction(action));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return action;
        }

        public override string ToString()
        {
            return this.action;
        }

        public static implicit operator string(CampaignRequestAction action) => action.action;
    }
}