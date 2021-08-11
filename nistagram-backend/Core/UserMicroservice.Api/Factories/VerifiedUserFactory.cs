using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class VerifiedUserFactory
    {
        public VerifiedUser Create(Core.Model.VerifiedUser verifiedUser)
        {
            return new VerifiedUser()
            {
                Id = verifiedUser.Id,
                Username = verifiedUser.Username,
                EmailAddress = verifiedUser.EmailAddress,
                FirstName = verifiedUser.FirstName,
                LastName = verifiedUser.LastName,
                DateOfBirth = verifiedUser.DateOfBirth,
                PhoneNumber = verifiedUser.PhoneNumber,
                Gender = verifiedUser.Gender,
                WebsiteAddress = verifiedUser.WebsiteAddress,
                Bio = verifiedUser.Bio,
                Password = verifiedUser.Password,
                ProfilePicturePath = verifiedUser.ProfileImagePath,
                IsPrivate = verifiedUser.IsPrivate,
                IsAcceptingMessages = verifiedUser.IsAcceptingMessages,
                IsAcceptingTags = verifiedUser.IsAcceptingTags,
                BlockedUsers = Convert(verifiedUser.BlockedUsers),
                BlockedByUsers = Convert(verifiedUser.BlockedByUsers),
                MutedUsers = Convert(verifiedUser.MutedUsers),
                MutedByUsers = Convert(verifiedUser.MutedByUsers),
                Following = Convert(verifiedUser.Following),
                Followers = Convert(verifiedUser.Followers),
                MyCloseFriends = Convert(verifiedUser.MyCloseFriends),
                CloseFriendTo = Convert(verifiedUser.CloseFriendTo),
                IsBanned = verifiedUser.IsBanned,
                Category = verifiedUser.Category
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