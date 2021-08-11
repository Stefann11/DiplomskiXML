using CSharpFunctionalExtensions;
using StoryMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RegisteredUser = StoryMicroservice.Core.DTOs.RegisteredUser;

namespace StoryMicroservice.DataAccess.Factories
{
    public class RegisteredUserFactory
    {
        public RegisteredUser Create(Core.Model.RegisteredUser registeredUser)
        {
            return new RegisteredUser
            {
                Id = registeredUser.Id.ToString(),
                Username = registeredUser.Username,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                IsPrivate = registeredUser.IsPrivate,
                IsAcceptingTags = registeredUser.IsAcceptingTags,
                ProfilePicturePath = registeredUser.ProfilePicturePath.ToString(),
                BlockedByUsers = CreateIds(registeredUser.BlockedByUsers),
                BlockedUsers = CreateIds(registeredUser.BlockedUsers),
                Following = CreateIds(registeredUser.Following),
                Followers = CreateIds(registeredUser.Followers),
                MyCloseFriends = CreateIds(registeredUser.MyCloseFriends),
                CloseFriendTo = CreateIds(registeredUser.CloseFriendTo),
            };
        }

        public Core.Model.RegisteredUser Create(RegisteredUser registeredUser, IEnumerable<Core.Model.RegisteredUser> blockedUsers,
            IEnumerable<Core.Model.RegisteredUser> blockedByUsers, IEnumerable<Core.Model.RegisteredUser> followers,
            IEnumerable<Core.Model.RegisteredUser> following, IEnumerable<Core.Model.RegisteredUser> closeFriendsTo,
            IEnumerable<Core.Model.RegisteredUser> myCloseFriends)
        {
            return Core.Model.RegisteredUser.Create(new Guid(registeredUser.Id), Username.Create(registeredUser.Username).Value,
                FirstName.Create(registeredUser.FirstName).Value, LastName.Create(registeredUser.LastName).Value, registeredUser.IsPrivate,
                registeredUser.IsAcceptingTags, ContentPath.Create(registeredUser.ProfilePicturePath).Value,
                blockedUsers, blockedByUsers, following, followers, myCloseFriends, closeFriendsTo).Value;
        }

        public List<string> CreateIds(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            var test = registeredUsers.Select(registeredUser => registeredUser.Id.ToString()).ToList();
            return test;
        }

        public List<RegisteredUser> CreateUsers(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            return registeredUsers.Select(registeredUser => Create(registeredUser)).ToList();
        }

        public IEnumerable<Core.Model.RegisteredUser> CreateUsers(List<RegisteredUser> registeredUsers)
        {
            if (registeredUsers == null) return new List<Core.Model.RegisteredUser>();
            return registeredUsers.Select(registeredUser => Create(registeredUser, new List<Core.Model.RegisteredUser>(),
               new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>(),
               new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>())).ToList();
        }
    }
}