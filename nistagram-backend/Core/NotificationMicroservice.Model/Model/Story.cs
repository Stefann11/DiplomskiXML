using System;

namespace NotificationMicroservice.Core.Model
{
    public class Story : Content
    {
        public Story() : base()
        {
        }

        public Story(Guid id) : base(id, "Story")
        {
        }
    }
}