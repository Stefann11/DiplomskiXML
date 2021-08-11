using System;

namespace NotificationMicroservice.Core.Model
{
    public class Comment : Content
    {
        public Comment() : base()
        {
        }

        public Comment(Guid id) : base(id, "Comment")
        {
        }
    }
}