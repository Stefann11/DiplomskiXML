using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class VerificationRequestView
    {
        public Guid Id { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public string DocumentImagePath { get; set; }
        public bool IsApproved { get; set; }
    }
}