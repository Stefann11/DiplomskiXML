using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using ReportMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMicroservice.Api.Consumers
{
    public class UnsuccessfulNotificationUserRegistrationEventConsumer : IConsumeAsync<UnsuccessfulNotificationUserRegistrationEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulNotificationUserRegistrationEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulNotificationUserRegistrationEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectRegistrationAsync(message.Id, "Unsuccessful registration error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulPostUserRegistrationEvent
            {
                Id = message.Id,
            });
        }
    }
}