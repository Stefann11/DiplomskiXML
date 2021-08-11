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
    public class CampaignUserFollowedEventConsumer : IConsumeAsync<CampaignUserFollowedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public CampaignUserFollowedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(CampaignUserFollowedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.FollowAsync(message.FollowedById, message.FollowingId);
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new UserFollowCompletedEvent
                {
                    FollowedById = message.FollowedById,
                    FollowingId = message.FollowingId
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulStoryFollowEvent
                {
                    FollowedById = message.FollowedById,
                    FollowingId = message.FollowingId
                });
            }
        }
    }
}