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
    public class UnsuccessfulPostUserEditEventConsumer : IConsumeAsync<UnsuccessfulPostUserEditEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UnsuccessfulPostUserEditEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulPostUserEditEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectEditAsync(Convert(message), "Unsuccessful edit error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulStoryUserEditEvent
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

        private RegisteredUser Convert(UnsuccessfulPostUserEditEvent message)
        {
            return RegisteredUser.Create(new Guid(message.Id),
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           message.IsPrivate,
                                           message.IsAcceptingTags,
                                           ContentPath.Create(message.ProfileImagePath).Value,
                                           CreateUsers(message.BlockedUsers),
                                           CreateUsers(message.BlockedByUsers),
                                           CreateUsers(message.Following),
                                           CreateUsers(message.Followers),
                                           CreateUsers(message.MyCloseFriends),
                                           CreateUsers(message.CloseFriendTo)).Value;
        }

        public List<RegisteredUser> CreateUsers(IEnumerable<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }
    }
}