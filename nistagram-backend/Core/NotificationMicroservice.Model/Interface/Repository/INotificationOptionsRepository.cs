using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Model;
using System;

namespace NotificationMicroservice.Core.Interface.Repository
{
    public interface INotificationOptionsRepository : IRepository<NotificationOptions>
    {
        public Maybe<NotificationOptions> GetBy(Guid loggedUserId, Guid notificationByUserId);
    }
}