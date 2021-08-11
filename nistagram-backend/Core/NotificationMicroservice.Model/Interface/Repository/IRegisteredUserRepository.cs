using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Model;
using System;

namespace NotificationMicroservice.Core.Interface.Repository
{
    public interface IRegisteredUserRepository : IRepository<RegisteredUser>
    {
        public Maybe<RegisteredUser> GetByUsername(String username);
    }
}