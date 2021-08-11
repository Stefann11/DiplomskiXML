using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class WebsiteAddress : ValueObject
    {
        private readonly string address;

        public WebsiteAddress(string address)
        {
            this.address = address;
        }

        public static Result<WebsiteAddress> Create(string address)
        {
            if (address.Length > 0
                && !(Uri.TryCreate(address, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
                return Result.Failure<WebsiteAddress>("Website address is not valid URI");
            return Result.Success(new WebsiteAddress(address));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return address;
        }

        public override string ToString()
        {
            return this.address;
        }

        public static implicit operator string(WebsiteAddress websiteAddress) => websiteAddress.address;
    }
}