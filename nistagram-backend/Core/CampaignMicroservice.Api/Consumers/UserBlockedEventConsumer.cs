using CampaignMicroservice.Core.Interface;
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
    public class UserBlockedEventConsumer : IConsumeAsync<UserBlockedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserBlockedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserBlockedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.BlockAsync(message.Id, message.BlockedById, message.BlockingId);
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new CampaignUserBlockedEvent
                {
                    Id = message.Id,
                    BlockedById = message.BlockedById,
                    BlockingId = message.BlockingId,
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignUserMuteEvent
                {
                    Id = message.Id,
                });
            }
        }
    }
}