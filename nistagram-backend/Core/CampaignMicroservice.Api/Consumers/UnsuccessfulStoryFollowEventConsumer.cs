using CampaignMicroservice.Core.Services;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.Consumers
{
    public class UnsuccessfulStoryFollowEventConsumer : IConsumeAsync<UnsuccessfulStoryFollowEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulStoryFollowEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulStoryFollowEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectFollowAsync(message.FollowedById, message.FollowingId, "Unsuccessful follow error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignFollowEvent
            {
                Id = message.Id,
                FollowedById = message.FollowedById,
                FollowingId = message.FollowingId
            });
        }
    }
}