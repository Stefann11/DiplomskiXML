using System;

namespace NotificationMicroservice.Core.Model
{
    public class Post : Content
    {
        public Post() : base()
        {
        }

        public Post(Guid id) : base(id, "Post")
        {
        }
    }
}