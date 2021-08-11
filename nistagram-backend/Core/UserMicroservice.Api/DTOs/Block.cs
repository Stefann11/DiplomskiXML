using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class Block
    {
        public Guid Id { get; set; }
        public Guid BlockedById { get; set; }
        public Guid BlockingId { get; set; }
    }
}