using System;

namespace NotificationMicroservice.Core.Model
{
    public class Message : Content
    {
        public Message() : base()
        {
        }

        public Message(Guid id) : base(id, "Message")
        {
        }
    }
}