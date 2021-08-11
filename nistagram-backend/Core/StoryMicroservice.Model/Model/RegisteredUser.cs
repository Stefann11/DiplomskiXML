using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; }
        public Username Username { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public bool IsPrivate { get; }
        public bool IsAcceptingTags { get; }
        public ContentPath ProfilePicturePath { get; }
        public IEnumerable<RegisteredUser> BlockedUsers { get; }
        public IEnumerable<RegisteredUser> BlockedByUsers { get; }
        public IEnumerable<RegisteredUser> Following { get; }
        public IEnumerable<RegisteredUser> Followers { get; }
        public IEnumerable<RegisteredUser> MyCloseFriends { get; }
        public IEnumerable<RegisteredUser> CloseFriendTo { get; }

        private RegisteredUser(Guid id, Username username, FirstName firstName, LastName lastName,
            bool isPrivate, bool isAcceptingTags, ContentPath profilePicturePath, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> following,
            IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendTo)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            IsPrivate = isPrivate;
            IsAcceptingTags = isAcceptingTags;
            ProfilePicturePath = profilePicturePath;
            BlockedUsers = blockedUsers;
            BlockedByUsers = blockedByUsers;
            Following = following;
            Followers = followers;
            MyCloseFriends = myCloseFriends;
            CloseFriendTo = closeFriendTo;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username, FirstName firstName, LastName lastName,
            bool isPrivate, bool isAcceptingTags, ContentPath profilePicturePath, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> following,
            IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendTo)
        {
            return Result.Success(new RegisteredUser(id, username, firstName, lastName,
            isPrivate, isAcceptingTags, profilePicturePath, blockedUsers,
            blockedByUsers, following,
            followers, myCloseFriends, closeFriendTo));
        }

        public override bool Equals(object obj)
        {
            return obj is RegisteredUser user &&
                   Id.Equals(user.Id);
        }
    }
}