using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using System;
using System.Linq;
using NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext;

namespace NotificationMicroservice.DataAccess.Implementation
{
    public class NotificationOptionsRepository : Repository<NotificationOptions>, INotificationOptionsRepository
    {
        private AppDbContext dbContext;

        public NotificationOptionsRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Maybe<NotificationOptions> GetBy(Guid loggedUserId, Guid notificationByUserId)
        {
            NotificationOptions notificationOptions =
                dbContext.NotificationOptions.SingleOrDefault(
                    notificationOptions => notificationOptions.LoggedUser.Id.Equals(
                        loggedUserId) && notificationOptions.NotificationByUser.Id.Equals(
                            notificationByUserId));
            return notificationOptions == null ?
                Maybe<NotificationOptions>.None :
                notificationOptions;
        }
    }
}