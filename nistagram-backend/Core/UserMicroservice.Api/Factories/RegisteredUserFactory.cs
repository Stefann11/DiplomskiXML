using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class RegisteredUserFactory
    {
        public RegisteredUser Create(Core.Model.RegisteredUser registeredUser)
        {
            return new RegisteredUser()
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                EmailAddress = registeredUser.EmailAddress,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                PhoneNumber = registeredUser.PhoneNumber,
                Gender = registeredUser.Gender,
                WebsiteAddress = registeredUser.WebsiteAddress,
                Bio = registeredUser.Bio,
                Password = registeredUser.Password,
                ProfilePicturePath = registeredUser.ProfileImagePath,
                IsPrivate = registeredUser.IsPrivate,
                IsAcceptingMessages = registeredUser.IsAcceptingMessages,
                IsAcceptingTags = registeredUser.IsAcceptingTags,
                BlockedUsers = Convert(registeredUser.BlockedUsers),
                BlockedByUsers = Convert(registeredUser.BlockedByUsers),
                MutedUsers = Convert(registeredUser.MutedUsers),
                MutedByUsers = Convert(registeredUser.MutedByUsers),
                Following = Convert(registeredUser.Following),
                Followers = Convert(registeredUser.Followers),
                MyCloseFriends = Convert(registeredUser.MyCloseFriends),
                CloseFriendTo = Convert(registeredUser.CloseFriendTo),
                IsBanned = registeredUser.IsBanned
            };
        }

        public IEnumerable<RegisteredUser> Convert(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            return registeredUsers.Select(registeredUser => new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                EmailAddress = registeredUser.EmailAddress,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                PhoneNumber = registeredUser.PhoneNumber,
                Gender = registeredUser.Gender,
                WebsiteAddress = registeredUser.WebsiteAddress,
                Bio = registeredUser.Bio,
                Password = registeredUser.Password,
                ProfilePicturePath = registeredUser.ProfileImagePath,
                IsPrivate = registeredUser.IsPrivate,
                IsAcceptingMessages = registeredUser.IsAcceptingMessages,
                IsAcceptingTags = registeredUser.IsAcceptingTags,
                IsBanned = registeredUser.IsBanned
            }).ToList();
        }
    }
}