using System.Collections.Generic;

namespace StoryMicroservice.Core.DTOs
{
    public class RegisteredUser
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
    }
}