using NotificationMicroservice.Core.Model;
using System.Collections.Generic;

namespace NotificationMicroservice.Core.Interface.Repository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        public IEnumerable<Notification> GetNotificationsForFollowing(IEnumerable<RegisteredUser> users,
            RegisteredUser loggedUser);
    }
}