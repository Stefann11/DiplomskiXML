using System;
using System.Collections.Generic;

namespace PostMicroservice.Api.DTOs
{
    public class RegisteredUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImagePath { get; set; }
        public Boolean isPrivate { get; set; }
        public Boolean isAcceptingTags { get; set; }
        public IEnumerable<RegisteredUser> Following { get; set; }
        public IEnumerable<RegisteredUser> Followers { get; set; }
    }
}