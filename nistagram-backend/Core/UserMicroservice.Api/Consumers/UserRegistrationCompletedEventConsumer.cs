using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserMicroservice.Core.Services;

namespace UserMicroservice.Api.Consumers
{
    public class UserRegistrationCompletedEventConsumer : IConsumeAsync<UserRegistrationCompletedEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UserRegistrationCompletedEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserRegistrationCompletedEvent message, CancellationToken cancellationToken = default)
        {
            await userService.CompleteRegistrationAsync(message.Id);
        }
    }
}