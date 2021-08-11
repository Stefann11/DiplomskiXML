using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace ChatMicroservice.Core.Model
{
    public class RegisteredUser
    {
        private Guid id;
        private Username username;
        private readonly FirstName firstName;
        private readonly LastName lastName;
        private readonly bool isPrivate;
        private readonly bool isAcceptingTags;
        private readonly IEnumerable<RegisteredUser> blockedUsers;
        private readonly IEnumerable<RegisteredUser> blockedByUsers;
        private readonly IEnumerable<RegisteredUser> following;
        private readonly IEnumerable<RegisteredUser> followers;

        public RegisteredUser(Guid id, Username username, FirstName firstName, LastName lastName, bool isPrivate, bool isAcceptingTags,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            this.id = id;
            this.username = username;
            this.firstName = firstName;
            this.lastName = lastName;
            this.isPrivate = isPrivate;
            this.isAcceptingTags = isAcceptingTags;
            this.blockedUsers = blockedUsers;
            this.blockedByUsers = blockedByUsers;
            this.following = following;
            this.followers = followers;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username, FirstName firstName, LastName lastName, bool isPrivate, bool isAcceptingTags,
           IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            return Result.Success(new RegisteredUser(id, username, firstName, lastName, isPrivate, isAcceptingTags,
                blockedUsers, blockedByUsers, following, followers));
        }
    }
}