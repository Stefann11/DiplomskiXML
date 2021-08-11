using System;

namespace NotificationMicroservice.Api.DTOs
{
    public class NotificationOptions
    {
        public Guid Id { get; set; }
        public bool IsNotifiedByFollowRequests { get; set; }
        public bool IsNotifiedByMessages { get; set; }
        public bool IsNotifiedByPosts { get; set; }
        public bool IsNotifiedByStories { get; set; }
        public bool IsNotifiedByComments { get; set; }
        public RegisteredUser LoggedUser { get; set; }
        public RegisteredUser NotificationByUser { get; set; }
    }
}