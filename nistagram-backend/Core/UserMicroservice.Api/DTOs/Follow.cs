using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class Follow
    {
        public Guid Id { get; set; }
        public Guid FollowedById { get; set; }
        public Guid FollowingId { get; set; }
        public String Type { get; set; }
        public Boolean IsApproved { get; set; }
    }
}