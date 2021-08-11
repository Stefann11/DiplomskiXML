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
    public class UnsuccessfulStoryUserEditEventConsumer : IConsumeAsync<UnsuccessfulStoryUserEditEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UnsuccessfulStoryUserEditEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulStoryUserEditEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectEditAsync(Convert(message), "Unsuccessful user edit error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulCampaignUserEditEvent
            {
                Id = message.Id,
                EmailAddress = message.EmailAddress,
                Username = message.Username,
                FirstName = message.FirstName,
                LastName = message.LastName,
                DateOfBirth = message.DateOfBirth,
                PhoneNumber = message.PhoneNumber,
                Gender = message.Gender,
                WebsiteAddress = message.WebsiteAddress,
                Bio = message.Bio,
                Password = message.Password,
                IsPrivate = message.IsPrivate,
                IsAcceptingMessages = message.IsAcceptingMessages,
                IsAcceptingTags = message.IsAcceptingTags,
                ProfileImagePath = message.ProfileImagePath,
                BlockedUsers = message.BlockedUsers,
                BlockedByUsers = message.BlockedByUsers,
                MutedUsers = message.MutedUsers,
                MutedByUsers = message.MutedByUsers,
                Following = message.Following,
                Followers = message.Followers,
                MyCloseFriends = message.MyCloseFriends,
                CloseFriendTo = message.CloseFriendTo,
                IsBanned = message.IsBanned,
            });
        }

        private RegisteredUser Convert(UnsuccessfulStoryUserEditEvent user)
        {
            return RegisteredUser.Create(new Guid(user.Id), Username.Create(user.Username).Value, FirstName.Create(user.FirstName).Value,
            LastName.Create(user.LastName).Value, user.DateOfBirth,
            Gender.Create(user.Gender).Value, ProfileImagePath.Create(user.ProfileImagePath).Value, user.IsPrivate,
             CreateUsers(user.BlockedUsers), CreateUsers(user.BlockedByUsers),
            CreateUsers(user.MutedUsers), CreateUsers(user.MutedByUsers),
            CreateUsers(user.Following), CreateUsers(user.Followers),
            user.IsBanned).Value;
        }

        public List<RegisteredUser> CreateUsers(IEnumerable<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }
    }
}