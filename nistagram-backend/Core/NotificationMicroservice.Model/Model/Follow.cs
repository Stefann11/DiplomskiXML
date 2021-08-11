using System;

namespace NotificationMicroservice.Core.Model
{
    public class Follow
    {
        public Guid Id { get; set; }
        public Guid? FollowedByUserId { get; set; }
        public virtual RegisteredUser FollowedByUser { get; set; }
        public Guid? FollowingUserId { get; set; }
        public virtual RegisteredUser FollowingUser { get; set; }

        public Follow()
        {
        }

        public Follow(Guid id, Guid followedByUserId, Guid followingUserId)
        {
            Id = id;
            FollowedByUserId = followedByUserId;
            FollowingUserId = followingUserId;
        }
    }
}