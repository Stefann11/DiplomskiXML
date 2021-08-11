using NotificationMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace NotificationMicroservice.Api.Factories
{
    public class NotificationFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public NotificationFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public Notification Create(Core.Model.Notification notification)
        {
            return new Notification
            {
                Id = notification.Id,
                TimeStamp = notification.TimeStamp,
                Type = notification.Content.Type,
                ContentId = notification.Content.Id,
                RegisteredUser = registeredUserFactory.Create(notification.RegisteredUser)
            };
        }

        public List<Notification> CreateNotifications(IEnumerable<Core.Model.Notification> notifications)
        {
            return (from Core.Model.Notification notification in notifications
                    select Create(notification)).ToList();
        }
    }
}