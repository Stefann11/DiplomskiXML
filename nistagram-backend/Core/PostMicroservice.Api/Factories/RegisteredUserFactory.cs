using PostMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Factories
{
    public class RegisteredUserFactory
    {
        public RegisteredUser Create(Core.Model.RegisteredUser registeredUser)
        {
            return new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                ProfileImagePath = registeredUser.ProfileImagePath,
                Following = Convert(registeredUser.Following),
                Followers = Convert(registeredUser.Followers)
            };
        }

        public IEnumerable<RegisteredUser> CreateUsers(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            return registeredUsers.Select(registeredUser => Create(registeredUser)).ToList();
        }

        private IEnumerable<RegisteredUser> Convert(IEnumerable<Core.Model.RegisteredUser> users)
        {
            return users.Select(registeredUser => new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                ProfileImagePath = registeredUser.ProfileImagePath
            }).ToList();
        }
    }
}