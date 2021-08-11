using System;

namespace NotificationMicroservice.Core.Model
{
    public class Notification
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid? ContentId { get; set; }
        public virtual Content Content { get; set; }
        public Guid? RegisteredUserId { get; set; }
        public virtual RegisteredUser RegisteredUser { get; set; }

        public Notification()
        {
        }

        public Notification(Guid id, DateTime timestamp, Guid contentId, Guid registeredUserId)
        {
            Id = id;
            TimeStamp = timestamp;
            ContentId = contentId;
            RegisteredUserId = registeredUserId;
        }
    }
}