using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UserRegisteredEvent
    {
        public string Id { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsAcceptingTags { get; set; }
        public List<string> BlockedUsers { get; set; }
        public List<string> BlockedByUsers { get; set; }
        public List<string> Following { get; set; }
        public List<string> Followers { get; set; }
        public List<string> MutedUsers { get; set; }
        public List<string> MutedByUsers { get; set; }
        public List<string> MyCloseFriends { get; set; }
        public List<string> CloseFriendTo { get; set; }
        public bool IsBanned { get; set; }
    }
}