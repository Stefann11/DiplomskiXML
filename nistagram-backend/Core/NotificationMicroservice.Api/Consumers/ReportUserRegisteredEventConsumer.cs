using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using NotificationMicroservice.Core.Model;
using NotificationMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationMicroservice.Api.Consumers
{
    public class ReportUserRegisteredEventConsumer : IConsumeAsync<ReportUserRegisteredEvent>
    {
        private readonly RegisteredUserService userService;
        private readonly IBus _bus;

        public ReportUserRegisteredEventConsumer(RegisteredUserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(ReportUserRegisteredEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateRegistrationAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new UserRegistrationCompletedEvent
                {
                    Id = message.Id,
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulNotificationUserRegistrationEvent
                {
                    Id = message.Id,
                });
            }
        }

        private RegisteredUser Convert(ReportUserRegisteredEvent message)
        {
            return new RegisteredUser(message.Id,
                                           message.Username,
                                           "");
        }
    }
}