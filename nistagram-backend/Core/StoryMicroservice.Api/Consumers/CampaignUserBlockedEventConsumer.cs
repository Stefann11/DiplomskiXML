using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoryMicroservice.Api.Consumers
{
    public class CampaignUserBlockedEventConsumer : IConsumeAsync<CampaignUserBlockedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public CampaignUserBlockedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(CampaignUserBlockedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.BlockAsync(message.Id, message.BlockedById, message.BlockingId);
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new UserBlockCompletedEvent
                {
                    Id = message.Id,
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulStoryUserBlockEvent
                {
                    Id = message.Id,
                });
            }
        }
    }
}