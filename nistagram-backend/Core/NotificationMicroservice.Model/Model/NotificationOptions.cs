using System;

namespace NotificationMicroservice.Core.Model
{
    public class NotificationOptions
    {
        public Guid Id { get; set; }
        public bool IsNotifiedByFollowRequests { get; set; }
        public bool IsNotifiedByMessages { get; set; }
        public bool IsNotifiedByPosts { get; set; }
        public bool IsNotifiedByStories { get; set; }
        public bool IsNotifiedByComments { get; set; }
        public Guid? LoggedUserId { get; set; }
        public virtual RegisteredUser LoggedUser { get; set; }
        public Guid? NotificationByUserId { get; set; }
        public virtual RegisteredUser NotificationByUser { get; set; }

        public NotificationOptions()
        {
        }

        public NotificationOptions(Guid id, bool isNotifiedByFollowRequests, bool isNotifiedByMessages,
            bool isNotifiedByPosts, bool isNotifiedByStories, bool isNotifiedByComments,
            Guid loggedUserId, Guid notificationByUserId)
        {
            Id = id;
            IsNotifiedByFollowRequests = isNotifiedByFollowRequests;
            IsNotifiedByMessages = isNotifiedByMessages;
            IsNotifiedByPosts = isNotifiedByPosts;
            IsNotifiedByStories = isNotifiedByStories;
            IsNotifiedByComments = isNotifiedByComments;
            LoggedUserId = loggedUserId;
            NotificationByUserId = notificationByUserId;
        }
    }
}