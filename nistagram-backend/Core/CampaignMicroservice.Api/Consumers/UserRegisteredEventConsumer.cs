using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
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
    public class UserRegisteredEventConsumer : IConsumeAsync<UserRegisteredEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserRegisteredEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UserRegisteredEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateRegistrationAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new CampaignUserRegisteredEvent
                {
                    Id = message.Id,
                    Username = message.Username,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    IsPrivate = message.IsPrivate,
                    IsAcceptingTags = message.IsAcceptingTags,
                    ProfilePicturePath = message.ProfilePicturePath.ToString(),
                    Following = new List<string>(),
                    Followers = new List<string>(),
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulStoryUserRegistrationEvent
                {
                    Id = new Guid(message.Id),
                });
            }
        }

        /* private IEnumerable<StoryUserRegisteredEvent> Convert(IEnumerable<Core.Model.RegisteredUser> users)
         {
             return users.Select(registeredUser => new StoryUserRegisteredEvent
             {
                 Id = registeredUser.Id,
                 Username = registeredUser.Username,
                 FirstName = registeredUser.FirstName,
                 LastName = registeredUser.LastName,
                 ProfileImagePath = registeredUser.ProfilePicturePath,
                 IsPrivate = registeredUser.IsPrivate,
                 IsAcceptingTags = registeredUser.IsAcceptingTags,
             }).ToList();
         }*/

        public List<RegisteredUser> CreateUsers(List<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }

        private RegisteredUser Convert(UserRegisteredEvent message)
        {
            return RegisteredUser.Create(new Guid(message.Id),
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           message.DateOfBirth,
                                           Gender.Create(message.Gender).Value,
                                           ProfileImagePath.Create(message.ProfilePicturePath).Value,
                                           message.IsPrivate,
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           message.IsBanned).Value;
        }
    }
}