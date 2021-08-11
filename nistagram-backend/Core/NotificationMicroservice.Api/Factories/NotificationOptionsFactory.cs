using NotificationMicroservice.Api.DTOs;

namespace NotificationMicroservice.Api.Factories
{
    public class NotificationOptionsFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public NotificationOptionsFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public NotificationOptions Create(Core.Model.NotificationOptions notificationOptions)
        {
            return new NotificationOptions
            {
                Id = notificationOptions.Id,
                IsNotifiedByFollowRequests = notificationOptions.IsNotifiedByFollowRequests,
                IsNotifiedByMessages = notificationOptions.IsNotifiedByMessages,
                IsNotifiedByPosts = notificationOptions.IsNotifiedByPosts,
                IsNotifiedByStories = notificationOptions.IsNotifiedByStories,
                IsNotifiedByComments = notificationOptions.IsNotifiedByComments,
                LoggedUser = registeredUserFactory.Create(notificationOptions.LoggedUser),
                NotificationByUser = registeredUserFactory.Create(notificationOptions.NotificationByUser),
            };
        }
    }
}