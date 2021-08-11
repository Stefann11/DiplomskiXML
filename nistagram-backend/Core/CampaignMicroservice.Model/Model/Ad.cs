using CSharpFunctionalExtensions;
using System;

namespace CampaignMicroservice.Core.Model
{
    public class Ad
    {
        public Guid Id { get; }
        public Content Content { get; }
        public Link Link { get; }
        public ClickCount ClickCount { get; }
        public RegisteredUser ProfileOwner { get; }

        private Ad(Guid id, Content content, Link link, ClickCount clickCount, RegisteredUser profileOwner)
        {
            Id = id;
            Content = content;
            Link = link;
            ClickCount = clickCount;
            ProfileOwner = profileOwner;
        }

        public static Result<Ad> Create(Guid id, Content content, Link link, ClickCount clickCount, RegisteredUser profileOwner)
        {
            return Result.Success(new Ad(id, content, link, clickCount, profileOwner));
        }
    }
}