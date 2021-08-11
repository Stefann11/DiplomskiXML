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
    public class UserMutedEventConsumer : IConsumeAsync<UserMutedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserMutedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserMutedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.MuteAsync(message.Id, message.MutedById, message.MutingId);
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new UserMuteCompletedEvent
                {
                    Id = message.Id,
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