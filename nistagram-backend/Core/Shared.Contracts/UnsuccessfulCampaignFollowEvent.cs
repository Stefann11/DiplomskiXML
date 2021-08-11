using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class UnsuccessfulCampaignFollowEvent
    {
        public Guid Id { get; set; }
        public Guid FollowingId { get; set; }
        public Guid FollowedById { get; set; }
    }
}