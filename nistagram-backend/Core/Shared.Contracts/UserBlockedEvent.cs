using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UserBlockedEvent
    {
        public Guid Id { get; set; }
        public Guid BlockingId { get; set; }
        public Guid BlockedById { get; set; }
    }
}