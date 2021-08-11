using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext;

namespace NotificationMicroservice.DataAccess.Implementation
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly INotificationOptionsRepository _notificationOptionsRepository;
        private AppDbContext dbContext;

        public NotificationRepository(AppDbContext context,
            INotificationOptionsRepository notificationOptionsRepository) : base(context)
        {
            _notificationOptionsRepository = notificationOptionsRepository;
            dbContext = context;
        }

        public IEnumerable<Notification> GetNotificationsForFollowing(IEnumerable<RegisteredUser> users,
            RegisteredUser loggedUser)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (RegisteredUser registeredUser in users)
            {
                notifications.AddRange(GetNotificationsForRegisteredUser(registeredUser, loggedUser));
            }
            return notifications;
        }

        private IEnumerable<Notification> GetNotificationsForRegisteredUser(RegisteredUser registeredUser,
            RegisteredUser loggedUser)
        {
            List<Notification> notifications = new List<Notification>();
            if (_notificationOptionsRepository.GetBy(loggedUser.Id, registeredUser.Id).HasNoValue)
            {
                return new List<Notification>();
            }
            NotificationOptions notificationOptions = _notificationOptionsRepository.GetBy(loggedUser.Id, registeredUser.Id).Value;

            if (notificationOptions.IsNotifiedByFollowRequests)
            {
                notifications.AddRange(GetFollowRequestsNotificationsForRegisteredUser(registeredUser));
            }
            if (notificationOptions.IsNotifiedByPosts)
            {
                notifications.AddRange(GetPostsNotificationsForRegisteredUser(registeredUser));
            }
            if (notificationOptions.IsNotifiedByStories)
            {
                notifications.AddRange(GetStoriesNotificationsForRegisteredUser(registeredUser));
            }
            if (notificationOptions.IsNotifiedByComments)
            {
                notifications.AddRange(GetCommentsNotificationsForRegisteredUser(registeredUser));
            }
            if (notificationOptions.IsNotifiedByMessages)
            {
                notifications.AddRange(GetMessagesNotificationsForRegisteredUser(registeredUser));
            }
            return notifications;
        }

        private IEnumerable<Notification> GetFollowRequestsNotificationsForRegisteredUser(RegisteredUser registeredUser)
        {
            return dbContext.Notifications.Where(notification => notification.RegisteredUser.Id.Equals(registeredUser.Id) && notification.Content.Type.Equals("FollowRequest"));
        }

        private IEnumerable<Notification> GetPostsNotificationsForRegisteredUser(RegisteredUser registeredUser)
        {
            return dbContext.Notifications.Where(notification => notification.RegisteredUser.Id.Equals(registeredUser.Id) && notification.Content.Type.Equals("Post"));
        }

        private IEnumerable<Notification> GetStoriesNotificationsForRegisteredUser(RegisteredUser registeredUser)
        {
            return dbContext.Notifications.Where(notification => notification.RegisteredUser.Id.Equals(registeredUser.Id) && notification.Content.Type.Equals("Story"));
        }

        private IEnumerable<Notification> GetCommentsNotificationsForRegisteredUser(RegisteredUser registeredUser)
        {
            return dbContext.Notifications.Where(notification => notification.RegisteredUser.Id.Equals(registeredUser.Id) && notification.Content.Type.Equals("Comment"));
        }

        private IEnumerable<Notification> GetMessagesNotificationsForRegisteredUser(RegisteredUser registeredUser)
        {
            return dbContext.Notifications.Where(notification => notification.RegisteredUser.Id.Equals(registeredUser.Id) && notification.Content.Type.Equals("Message"));
        }
    }
}