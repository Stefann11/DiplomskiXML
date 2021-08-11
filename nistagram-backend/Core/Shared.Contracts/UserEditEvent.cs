using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UserEditEvent
    {
        public string Id { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsAcceptingTags { get; set; }
        public List<string> BlockedUsers { get; set; }
        public List<string> BlockedByUsers { get; set; }
        public List<string> Following { get; set; }
        public List<string> Followers { get; set; }
        public List<string> MyCloseFriends { get; set; }
        public List<string> CloseFriendTo { get; set; }

        //STARO
        public string OldProfilePicturePath { get; set; }

        public string OldUsername { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public bool OldIsPrivate { get; set; }
        public bool OldIsAcceptingTags { get; set; }
        public List<string> OldBlockedUsers { get; set; }
        public List<string> OldBlockedByUsers { get; set; }
        public List<string> OldFollowing { get; set; }
        public List<string> OldFollowers { get; set; }
        public List<string> OldMyCloseFriends { get; set; }
        public List<string> OldCloseFriendTo { get; set; }
    }
}