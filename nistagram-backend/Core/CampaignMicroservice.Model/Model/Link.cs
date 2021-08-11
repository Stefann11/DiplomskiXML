using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class Link : ValueObject
    {
        private readonly string link;

        private Link(string link)
        {
            this.link = link;
        }

        public static Result<Link> Create(string link)
        {
            return Result.Success(new Link(link));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return link;
        }

        public override string ToString()
        {
            return link;
        }

        public static implicit operator string(Link link) => link.link;
    }
}