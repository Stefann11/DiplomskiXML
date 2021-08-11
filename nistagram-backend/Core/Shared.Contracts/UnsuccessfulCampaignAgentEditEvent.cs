using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UnsuccessfulCampaignAgentEditEvent
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string WebsiteAddress { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsAcceptingMessages { get; set; }
        public bool IsAcceptingTags { get; set; }
        public string ProfileImagePath { get; set; }
        public IEnumerable<string> BlockedUsers { get; set; }
        public IEnumerable<string> BlockedByUsers { get; set; }
        public IEnumerable<string> MutedUsers { get; set; }
        public IEnumerable<string> MutedByUsers { get; set; }
        public IEnumerable<string> Following { get; set; }
        public IEnumerable<string> Followers { get; set; }
        public IEnumerable<string> MyCloseFriends { get; set; }
        public IEnumerable<string> CloseFriendTo { get; set; }
        public bool IsBanned { get; set; }
    }
}