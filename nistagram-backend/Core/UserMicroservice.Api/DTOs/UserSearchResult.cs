using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class UserSearchResult
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
        public bool IsPrivate { get; set; }
        public bool IsAcceptingMessages { get; set; }
        public bool IsAcceptingTags { get; set; }
    }
}