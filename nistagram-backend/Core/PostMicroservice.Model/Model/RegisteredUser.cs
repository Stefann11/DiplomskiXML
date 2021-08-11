using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; }
        public Username Username { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public ProfileImagePath ProfileImagePath { get; }
        public bool IsPrivate { get; }
        public bool IsAcceptingTags { get; }
        public IEnumerable<RegisteredUser> BlockedUsers { get; }
        public IEnumerable<RegisteredUser> BlockedByUsers { get; }
        public IEnumerable<RegisteredUser> Following { get; }
        public IEnumerable<RegisteredUser> Followers { get; }

        private RegisteredUser(Guid id, Username username, FirstName firstName, LastName lastName,
            ProfileImagePath profileImagePath, bool isPrivate, bool isAcceptingTags,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            ProfileImagePath = profileImagePath;
            IsPrivate = isPrivate;
            IsAcceptingTags = isAcceptingTags;
            BlockedUsers = blockedUsers;
            BlockedByUsers = blockedByUsers;
            Following = following;
            Followers = followers;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username, FirstName firstName,
            LastName lastName, ProfileImagePath profileImagePath, bool isPrivate, bool isAcceptingTags,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            return Result.Success(new RegisteredUser(id, username, firstName, lastName,
                profileImagePath, isPrivate, isAcceptingTags, blockedUsers, blockedByUsers, following, followers));
        }
    }
}