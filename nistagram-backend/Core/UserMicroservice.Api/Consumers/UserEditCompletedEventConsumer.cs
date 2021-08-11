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
    public class UserEditCompletedEventConsumer : IConsumeAsync<UserEditCompletedEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UserEditCompletedEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserEditCompletedEvent message, CancellationToken cancellationToken = default)
        {
            await userService.CompleteEditAsync(message.Id);
        }
    }
}