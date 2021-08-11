using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using ReportMicroservice.Core.Interface.Repository;
using ReportMicroservice.Core.Model;
using ReportMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMicroservice.Api.Consumers
{
    public class PostUserRegisteredEventConsumer : IConsumeAsync<PostUserRegisteredEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public PostUserRegisteredEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(PostUserRegisteredEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateRegistrationAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new ReportUserRegisteredEvent
                {
                    Id = message.Id,
                    Username = message.Username
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulReportUserRegistrationEvent
                {
                    Id = message.Id,
                });
            }
        }

        private RegisteredUser Convert(PostUserRegisteredEvent message)
        {
            return RegisteredUser.Create(message.Id,
                                         Username.Create(message.Username).Value).Value;
        }
    }
}