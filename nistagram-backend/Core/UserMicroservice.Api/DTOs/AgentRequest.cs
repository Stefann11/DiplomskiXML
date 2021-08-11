using System;

namespace UserMicroservice.Api.DTOs
{
    public class AgentRequest
    {
        public Guid Id { get; set; }
        public bool IsApproved { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public string AgentRequestAction { get; set; }
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