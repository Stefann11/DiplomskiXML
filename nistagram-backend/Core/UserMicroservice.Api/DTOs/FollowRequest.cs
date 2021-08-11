using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class FollowRequest
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public RegisteredUser RequestsFollow { get; set; }
        public RegisteredUser ReceivesFollow { get; set; }
    }
}