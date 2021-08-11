using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; }
        public Username Username { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public DateTime DateOfBirth { get; }
        public Gender Gender { get; }
        public ProfileImagePath ProfileImagePath { get; }
        public bool IsPrivate { get; }
        public IEnumerable<RegisteredUser> BlockedByUsers { get; }
        public IEnumerable<RegisteredUser> BlockedUsers { get; }
        public IEnumerable<RegisteredUser> Following { get; }
        public IEnumerable<RegisteredUser> Followers { get; }
        public IEnumerable<RegisteredUser> MutedByUsers { get; }
        public IEnumerable<RegisteredUser> MutedUsers { get; }
        public bool IsBanned { get; }

        protected RegisteredUser(Guid id, Username username, FirstName firstName, LastName lastName, DateTime dateOfBirth, Gender gender,
            ProfileImagePath profileImagePath, bool isPrivate, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, bool isBanned)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ProfileImagePath = profileImagePath;
            IsPrivate = isPrivate;
            BlockedByUsers = blockedByUsers;
            BlockedUsers = blockedUsers;
            Following = following;
            Followers = followers;
            MutedByUsers = mutedByUsers;
            MutedUsers = mutedUsers;
            IsBanned = isBanned;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username, FirstName firstName, LastName lastName, DateTime dateOfBirth, Gender gender,
            ProfileImagePath profileImagePath, bool isPrivate, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, bool isBanned)
        {
            return Result.Success(new RegisteredUser(id, username, firstName, lastName, dateOfBirth, gender, profileImagePath, isPrivate, blockedByUsers,
                                                        blockedUsers, following, followers, mutedByUsers, mutedUsers, isBanned));
        }

        public override bool Equals(object obj)
        {
            return obj is RegisteredUser user &&
                   Id.Equals(user.Id);
        }
    }
}