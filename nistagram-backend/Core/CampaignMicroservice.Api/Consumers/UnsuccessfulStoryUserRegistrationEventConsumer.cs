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
    public class UnsuccessfulStoryUserRegistrationEventConsumer : IConsumeAsync<UnsuccessfulStoryUserRegistrationEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulStoryUserRegistrationEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulStoryUserRegistrationEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectRegistrationAsync(message.Id, "Unsuccessful registration error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignUserRegistrationEvent
            {
                Id = message.Id,
            });
        }
    }
}