using System;

namespace NotificationMicroservice.Api.DTOs
{
    public class Notification
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Type { get; set; }
        public Guid ContentId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
    }
}