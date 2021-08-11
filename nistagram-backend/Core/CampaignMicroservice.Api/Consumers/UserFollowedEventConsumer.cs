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
    public class UserFollowedEventConsumer : IConsumeAsync<UserFollowedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserFollowedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserFollowedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.FollowAsync(message.Id, message.FollowedById, message.FollowingId);
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new CampaignUserFollowedEvent
                {
                    Id = message.Id,
                    FollowedById = message.FollowedById,
                    FollowingId = message.FollowingId
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignFollowEvent
                {
                    FollowedById = message.FollowedById,
                    FollowingId = message.FollowingId
                });
            }
        }
    }
}