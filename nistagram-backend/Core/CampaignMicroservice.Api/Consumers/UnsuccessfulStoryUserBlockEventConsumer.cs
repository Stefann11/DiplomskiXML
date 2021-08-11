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
    public class UnsuccessfulStoryUserBlockEventConsumer : IConsumeAsync<UnsuccessfulStoryUserBlockEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulStoryUserBlockEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulStoryUserBlockEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectBlockAsync(message.Id, "Unsuccessful block error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignUserBlockEvent
            {
                Id = message.Id,
            });
        }
    }
}