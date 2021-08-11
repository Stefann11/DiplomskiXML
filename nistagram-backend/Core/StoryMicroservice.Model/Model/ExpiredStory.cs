using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class ExpiredStory : Story
    {
        private ExpiredStory(Guid id, ContentPath contentPath, DateTime timeStamp, Duration duration, Description description,
            RegisteredUser registeredUser, Location location,
            IEnumerable<RegisteredUser> seenByUsers, IEnumerable<RegisteredUser> taggedUsers, IEnumerable<HashTag> hashTags, bool isBanned)
            : base(id, contentPath, timeStamp, duration, description, registeredUser, location,
                seenByUsers, taggedUsers, hashTags, isBanned)
        {
        }

        public static new Result<ExpiredStory> Create(Guid id, ContentPath contentPath, DateTime timeStamp, Duration duration, Description description,
            RegisteredUser registeredUser, Location location,
            IEnumerable<RegisteredUser> seenByUsers, IEnumerable<RegisteredUser> taggedUsers, IEnumerable<HashTag> hashTags, bool isBanned)
        {
            return Result.Success(new ExpiredStory(id, contentPath, timeStamp, duration, description, registeredUser, location,
                seenByUsers, taggedUsers, hashTags, isBanned));
        }
    }
}