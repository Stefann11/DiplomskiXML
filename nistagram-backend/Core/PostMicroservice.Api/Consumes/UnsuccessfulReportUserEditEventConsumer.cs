using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Consumes
{
    public class UnsuccessfulReportUserEditEventConsumer : IConsumeAsync<UnsuccessfulReportUserEditEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UnsuccessfulReportUserEditEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulReportUserEditEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectEditAsync(Convert(message), "Unsuccessful edit error!");
            await _bus.PubSub.PublishAsync(new UnsuccessfulPostUserEditEvent
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

        private RegisteredUser Convert(UnsuccessfulReportUserEditEvent message)
        {
            return RegisteredUser.Create(new Guid(message.Id),
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           ProfileImagePath.Create(message.ProfileImagePath).Value,
                                           message.IsPrivate,
                                           message.IsAcceptingTags,
                                           CreateUsers(message.BlockedUsers),
                                           CreateUsers(message.BlockedByUsers),
                                           CreateUsers(message.Following),
                                           CreateUsers(message.Followers)).Value;
        }

        public List<RegisteredUser> CreateUsers(IEnumerable<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }
    }
}