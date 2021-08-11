using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext;
using System.Linq;

namespace NotificationMicroservice.DataAccess.Implementation
{
    public class RegisteredUserRepository : Repository<RegisteredUser>, IRegisteredUserRepository
    {
        private AppDbContext dbContext;

        public RegisteredUserRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Maybe<RegisteredUser> GetByUsername(string username)
        {
            RegisteredUser registeredUser = dbContext.RegisteredUsers.SingleOrDefault(
                registeredUser => registeredUser.Username.Equals(username));
            return registeredUser == null ?
                Maybe<RegisteredUser>.None :
                registeredUser;
        }
    }
}