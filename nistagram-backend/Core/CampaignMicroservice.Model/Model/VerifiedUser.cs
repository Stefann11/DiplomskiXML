using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class VerifiedUser : RegisteredUser
    {
        public Category Category { get; }

        private VerifiedUser(Guid id, Username username, FirstName firstName, LastName lastName, DateTime dateOfBirth, Gender gender,
            ProfileImagePath profileImagePath, bool isPrivate, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, bool isBanned, Category category)
            : base(id, username, firstName, lastName, dateOfBirth, gender, profileImagePath, isPrivate, blockedByUsers,
                                                        blockedUsers, following, followers, mutedByUsers, mutedUsers, isBanned)
        {
            Category = category;
        }

        public static Result<VerifiedUser> Create(Guid id, Username username, FirstName firstName, LastName lastName, DateTime dateOfBirth, Gender gender,
            ProfileImagePath profileImagePath, bool isPrivate, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, bool isBanned, Category category)
        {
            return Result.Success(new VerifiedUser(id, username, firstName, lastName, dateOfBirth, gender, profileImagePath, isPrivate, blockedByUsers,
                                                        blockedUsers, following, followers, mutedByUsers, mutedUsers, isBanned, category));
        }
    }
}