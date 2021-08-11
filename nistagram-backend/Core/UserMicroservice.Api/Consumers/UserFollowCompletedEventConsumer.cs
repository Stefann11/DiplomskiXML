using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserMicroservice.Core.Services;

namespace UserMicroservice.Api.Consumers
{
    public class UserFollowCompletedEventConsumer : IConsumeAsync<UserFollowCompletedEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UserFollowCompletedEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserFollowCompletedEvent message, CancellationToken cancellationToken = default)
        {
            await userService.CompleteFollowAsync();
        }
    }
}