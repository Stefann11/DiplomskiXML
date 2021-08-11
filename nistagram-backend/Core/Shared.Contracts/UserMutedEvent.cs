using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UserMutedEvent
    {
        public Guid Id { get; set; }
        public Guid MutingId { get; set; }
        public Guid MutedById { get; set; }
    }
}