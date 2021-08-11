using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Consumes
{
    public class StoryUserRegisteredEventConsumer : IConsumeAsync<StoryUserRegisteredEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public StoryUserRegisteredEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(StoryUserRegisteredEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateRegistrationAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new PostUserRegisteredEvent
                {
                    Id = message.Id,
                    Username = message.Username,
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulPostUserRegistrationEvent
                {
                    Id = message.Id,
                });
            }
        }

        private RegisteredUser Convert(StoryUserRegisteredEvent message)
        {
            return RegisteredUser.Create(message.Id,
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           ProfileImagePath.Create(message.ProfileImagePath).Value,
                                           message.IsPrivate,
                                           message.IsAcceptingTags,
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>()).Value;
        }
    }
}