using System;

namespace NotificationMicroservice.Core.Model
{
    public class FollowRequest : Content
    {
        public FollowRequest() : base()
        {
        }

        public FollowRequest(Guid id) : base(id, "FollowRequest")
        {
        }
    }
}