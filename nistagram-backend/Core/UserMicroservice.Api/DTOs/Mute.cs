using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Api.DTOs
{
    public class Mute
    {
        public Guid Id { get; set; }
        public Guid MutedById { get; set; }
        public Guid MutingId { get; set; }
    }
}