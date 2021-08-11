using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using StoryMicroservice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoryMicroservice.Api.Consumers
{
    public class UnsuccessfulPostUserRegistrationEventConsumer : IConsumeAsync<UnsuccessfulPostUserRegistrationEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulPostUserRegistrationEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulPostUserRegistrationEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectRegistrationAsync(message.Id, "Unsuccessful registration error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulStoryUserRegistrationEvent
            {
                Id = message.Id,
            });
        }
    }
}