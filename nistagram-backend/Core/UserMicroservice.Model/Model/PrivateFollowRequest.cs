using CSharpFunctionalExtensions;
using System;

namespace UserMicroservice.Core.Model
{
    public class PrivateFollowRequest : FollowRequest
    {
        private readonly bool isApproved;

        private PrivateFollowRequest(Guid id, DateTime timestamp, RegisteredUser requestsFollow, RegisteredUser receivesFollow, bool isApproved)
            : base(id, timestamp, requestsFollow, receivesFollow)
        {
            this.isApproved = isApproved;
        }

        public static Result<PrivateFollowRequest> Create(Guid id, DateTime timestamp, RegisteredUser requestsFollow,
            RegisteredUser receivesFollow, bool isApproved)
        {
            return Result.Success(new PrivateFollowRequest(id, timestamp, requestsFollow, receivesFollow, isApproved));
        }
    }
}