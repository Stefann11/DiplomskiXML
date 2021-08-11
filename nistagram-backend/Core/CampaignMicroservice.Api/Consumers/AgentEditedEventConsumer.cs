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
    public class AgentEditedEventConsumer : IConsumeAsync<AgentEditedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public AgentEditedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(AgentEditedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.EditAgentAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new AgentEditCompletedEvent
                {
                    Id = new Guid(message.Id),
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignAgentEditEvent
                {
                    Id = message.Id,
                    EmailAddress = message.OldEmailAddress,
                    Username = message.OldUsername,
                    FirstName = message.OldFirstName,
                    LastName = message.OldLastName,
                    DateOfBirth = message.OldDateOfBirth,
                    PhoneNumber = message.OldPhoneNumber,
                    Gender = message.OldGender,
                    WebsiteAddress = message.OldWebsiteAddress,
                    Bio = message.OldBio,
                    Password = message.OldPassword,
                    IsPrivate = message.OldIsPrivate,
                    IsAcceptingMessages = message.OldIsAcceptingMessages,
                    IsAcceptingTags = message.OldIsAcceptingTags,
                    ProfileImagePath = message.OldProfileImagePath,
                    BlockedUsers = message.OldBlockedUsers,
                    BlockedByUsers = message.OldBlockedByUsers,
                    MutedUsers = message.OldMutedUsers,
                    MutedByUsers = message.OldMutedByUsers,
                    Following = message.OldFollowing,
                    Followers = message.OldFollowers,
                    MyCloseFriends = message.OldMyCloseFriends,
                    CloseFriendTo = message.OldCloseFriendTo,
                    IsBanned = message.OldIsBanned,
                });
            }
        }

        /*  private IEnumerable<StoryUserEditedEvent> Convert(IEnumerable<Core.Model.RegisteredUser> users)
          {
              return users.Select(registeredUser => new StoryUserEditedEvent
              {
                  Id = registeredUser.Id,
                  Username = registeredUser.Username,
                  FirstName = registeredUser.FirstName,
                  LastName = registeredUser.LastName,
                  ProfilePicturePath = registeredUser.ProfilePicturePath,
                  IsPrivate = registeredUser.IsPrivate,
                  IsAcceptingTags = registeredUser.IsAcceptingTags,
              }).ToList();
          }*/

        public List<string> CreateIds(IEnumerable<UserRegisteredEvent> registeredUsers)
        {
            var test = registeredUsers.Select(registeredUser => registeredUser.Id.ToString()).ToList();
            return test;
        }

        public List<RegisteredUser> CreateUsers(List<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }

        private Agent Convert(AgentEditedEvent message)
        {
            return Agent.Create(new Guid(message.Id),
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           message.DateOfBirth,
                                           Gender.Create(message.Gender).Value,
                                           ProfileImagePath.Create(message.ProfilePicturePath).Value,
                                           message.IsPrivate,
                                           CreateUsers(message.BlockedUsers),
                                           CreateUsers(message.BlockedByUsers),
                                           CreateUsers(message.Following),
                                           CreateUsers(message.Followers),
                                           CreateUsers(message.MutedByUsers),
                                           CreateUsers(message.MutedUsers),
                                           message.IsBanned,
                                           WebsiteAddress.Create(message.WebsiteAddress).Value).Value;
        }
    }
}