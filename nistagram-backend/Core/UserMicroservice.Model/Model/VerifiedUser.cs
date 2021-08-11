using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class VerifiedUser : RegisteredUser
    {
        public Category Category { get; }

        private VerifiedUser(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, bool isPrivate, bool isAcceptingMessages,
            bool isAcceptingTags, Password password, ProfileImagePath profileImagePath, IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendTo,
            bool isBanned, Category category)
            : base(id, username, emailAddress, firstName, lastName, dateOfBirth, phoneNumber, gender, websiteAddress,
                  bio, isPrivate, isAcceptingMessages, isAcceptingTags, password, profileImagePath, blockedUsers, blockedByUsers,
                  mutedUsers, mutedByUsers, following, followers, myCloseFriends, closeFriendTo, isBanned)
        {
            Category = category;
        }

        public static Result<VerifiedUser> Create(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, bool isPrivate, bool isAcceptingMessages,
            bool isAcceptingTags, Password password, ProfileImagePath profileImagePath, IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendTo,
            bool isBanned, Category category)
        {
            return Result.Success(new VerifiedUser(id, username, emailAddress, firstName,
            lastName, dateOfBirth, phoneNumber, gender, websiteAddress, bio, isPrivate, isAcceptingMessages,
            isAcceptingTags, password, profileImagePath, blockedUsers, blockedByUsers,
            mutedUsers, mutedByUsers, following, followers, myCloseFriends, closeFriendTo, isBanned, category));
        }
    }
}