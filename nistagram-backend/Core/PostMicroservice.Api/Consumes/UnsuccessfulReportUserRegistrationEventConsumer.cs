using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using PostMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Consumes
{
    public class UnsuccessfulReportUserRegistrationEventConsumer : IConsumeAsync<UnsuccessfulReportUserRegistrationEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public UnsuccessfulReportUserRegistrationEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulReportUserRegistrationEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectRegistrationAsync(message.Id, "Unsuccessful registration error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulPostUserRegistrationEvent
            {
                Id = message.Id,
            });
        }
    }
}