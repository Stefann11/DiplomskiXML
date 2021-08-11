using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class Price : ValueObject
    {
        private readonly float price;

        private Price(float price)
        {
            this.price = price;
        }

        public static Result<Price> Create(float price)
        {
            if (price < 0) return Result.Failure<Price>("Price can't be less than 0");
            return Result.Success(new Price(price));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return price;
        }

        public override string ToString()
        {
            return this.price.ToString();
        }

        public static implicit operator string(Price price) => price.price.ToString();
    }
}