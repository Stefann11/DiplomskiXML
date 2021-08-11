using CampaignMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace CampaignMicroservice.Api.Factories
{
    public class VerifiedUserFactory
    {
        public VerifiedUser Create(Core.Model.VerifiedUser verifiedUser)
        {
            return new VerifiedUser()
            {
                Id = verifiedUser.Id,
                Username = verifiedUser.Username,
                FirstName = verifiedUser.FirstName,
                LastName = verifiedUser.LastName,
                DateOfBirth = verifiedUser.DateOfBirth,
                Gender = verifiedUser.Gender,
                ProfileImagePath = verifiedUser.ProfileImagePath,
                IsPrivate = verifiedUser.IsPrivate,
                BlockedUsers = Convert(verifiedUser.BlockedUsers),
                BlockedByUsers = Convert(verifiedUser.BlockedByUsers),
                MutedUsers = Convert(verifiedUser.MutedUsers),
                MutedByUsers = Convert(verifiedUser.MutedByUsers),
                Following = Convert(verifiedUser.Following),
                Followers = Convert(verifiedUser.Followers),
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
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                Gender = registeredUser.Gender,
                ProfileImagePath = registeredUser.ProfileImagePath,
                IsPrivate = registeredUser.IsPrivate,
                IsBanned = registeredUser.IsBanned
            }).ToList();
        }
    }
}