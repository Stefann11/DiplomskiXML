using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Model;
using StoryMicroservice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoryMicroservice.Api.Consumers
{
    public class CampaignUserRegisteredEventConsumer : IConsumeAsync<CampaignUserRegisteredEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public CampaignUserRegisteredEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(CampaignUserRegisteredEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateRegistrationAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new StoryUserRegisteredEvent
                {
                    Id = new Guid(message.Id),
                    Username = message.Username,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    IsPrivate = message.IsPrivate,
                    IsAcceptingTags = message.IsAcceptingTags,
                    ProfileImagePath = message.ProfilePicturePath.ToString(),
                    Following = Convert(_userRepository.GetUsersById(message.Following)),
                    Followers = Convert(_userRepository.GetUsersById(message.Followers)),
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

        private IEnumerable<StoryUserRegisteredEvent> Convert(IEnumerable<Core.Model.RegisteredUser> users)
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
        }

        public List<string> CreateIds(IEnumerable<UserRegisteredEvent> registeredUsers)
        {
            var test = registeredUsers.Select(registeredUser => registeredUser.Id.ToString()).ToList();
            return test;
        }

        private RegisteredUser Convert(CampaignUserRegisteredEvent message)
        {
            return RegisteredUser.Create(new Guid(message.Id),
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           message.IsPrivate,
                                           message.IsAcceptingTags,
                                           ContentPath.Create(message.ProfilePicturePath).Value,
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>()).Value;
        }
    }
}