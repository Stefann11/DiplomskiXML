using CSharpFunctionalExtensions;
using System;

namespace UserMicroservice.Core.Model
{
    public class FollowRequest
    {
        public Guid Id { get; }
        public DateTime Timestamp { get; }
        public RegisteredUser RequestsFollow { get; }
        public RegisteredUser ReceivesFollow { get; }

        protected FollowRequest(Guid id, DateTime timestamp, RegisteredUser requestsFollow, RegisteredUser receivesFollow)
        {
            Id = id;
            Timestamp = timestamp;
            RequestsFollow = requestsFollow;
            ReceivesFollow = receivesFollow;
        }

        public static Result<FollowRequest> Create(Guid id, DateTime timestamp, RegisteredUser requestsFollow, RegisteredUser receivesFollow)
        {
            return Result.Success(new FollowRequest(id, timestamp, requestsFollow, receivesFollow));
        }
    }
}