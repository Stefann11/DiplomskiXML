using System;
using System.Collections.Generic;

namespace UserMicroservice.Api.DTOs
{
    public class RegisteredUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string WebsiteAddress { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsAcceptingMessages { get; set; }
        public bool IsAcceptingTags { get; set; }
        public bool IsBanned { get; set; }
        public IEnumerable<RegisteredUser> BlockedUsers { get; set; }
        public IEnumerable<RegisteredUser> BlockedByUsers { get; set; }
        public IEnumerable<RegisteredUser> MutedUsers { get; set; }
        public IEnumerable<RegisteredUser> MutedByUsers { get; set; }
        public IEnumerable<RegisteredUser> Following { get; set; }
        public IEnumerable<RegisteredUser> Followers { get; set; }
        public IEnumerable<RegisteredUser> MyCloseFriends { get; set; }
        public IEnumerable<RegisteredUser> CloseFriendTo { get; set; }
    }
}