using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class VerificationRequest
    {
        public Guid Id { get; set; }
        public Guid RegisteredUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
        public string DocumentImagePath { get; set; }
        public bool IsApproved { get; set; }
    }
}