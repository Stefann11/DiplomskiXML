using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class AgentEditedEvent
    {
        public string Id { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public string WebsiteAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsAcceptingTags { get; set; }
        public List<string> BlockedUsers { get; set; }
        public List<string> BlockedByUsers { get; set; }
        public List<string> MutedUsers { get; set; }
        public List<string> MutedByUsers { get; set; }
        public List<string> Following { get; set; }
        public List<string> Followers { get; set; }
        public List<string> MyCloseFriends { get; set; }
        public List<string> CloseFriendTo { get; set; }
        public bool IsBanned { get; set; }

        //old
        public string OldEmailAddress { get; set; }

        public string OldCategory { get; set; }
        public string OldUsername { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public DateTime OldDateOfBirth { get; set; }
        public string OldPhoneNumber { get; set; }
        public string OldGender { get; set; }
        public string OldWebsiteAddress { get; set; }
        public string OldBio { get; set; }
        public string OldPassword { get; set; }
        public bool OldIsPrivate { get; set; }
        public bool OldIsAcceptingMessages { get; set; }
        public bool OldIsAcceptingTags { get; set; }
        public string OldProfileImagePath { get; set; }
        public IEnumerable<string> OldBlockedUsers { get; set; }
        public IEnumerable<string> OldBlockedByUsers { get; set; }
        public IEnumerable<string> OldMutedUsers { get; set; }
        public IEnumerable<string> OldMutedByUsers { get; set; }
        public IEnumerable<string> OldFollowing { get; set; }
        public IEnumerable<string> OldFollowers { get; set; }
        public IEnumerable<string> OldMyCloseFriends { get; set; }
        public IEnumerable<string> OldCloseFriendTo { get; set; }
        public bool OldIsBanned { get; set; }
    }
}