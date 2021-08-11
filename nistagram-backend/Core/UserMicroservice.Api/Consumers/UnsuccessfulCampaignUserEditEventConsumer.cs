using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;
using UserMicroservice.Core.Services;

namespace UserMicroservice.Api.Consumers
{
    public class UnsuccessfulCampaignUserEditEventConsumer : IConsumeAsync<UnsuccessfulCampaignUserEditEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UnsuccessfulCampaignUserEditEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(UnsuccessfulCampaignUserEditEvent message, CancellationToken cancellationToken = default)
        {
            await userService.RejectEditAsync(Convert(message), "Unsuccessful user edit error!");
        }

        private RegisteredUser Convert(UnsuccessfulCampaignUserEditEvent user)
        {
            return RegisteredUser.Create(new Guid(user.Id), Username.Create(user.Username).Value, EmailAddress.Create(user.EmailAddress).Value, FirstName.Create(user.FirstName).Value,
            LastName.Create(user.LastName).Value, user.DateOfBirth, PhoneNumber.Create(user.PhoneNumber).Value,
            Gender.Create(user.Gender).Value, WebsiteAddress.Create(user.WebsiteAddress).Value, Bio.Create(user.Bio).Value, user.IsPrivate, user.IsAcceptingMessages, user.IsAcceptingTags,
            Password.Create(user.Password).Value, ProfileImagePath.Create(user.ProfileImagePath).Value, CreateUsers(user.BlockedUsers), CreateUsers(user.BlockedByUsers),
            CreateUsers(user.MutedUsers), CreateUsers(user.MutedByUsers),
            CreateUsers(user.Following), CreateUsers(user.Followers),
            CreateUsers(user.MyCloseFriends), CreateUsers(user.CloseFriendTo), user.IsBanned).Value;
        }

        public List<RegisteredUser> CreateUsers(IEnumerable<string> registeredUsersIds)
        {
            var test = registeredUsersIds.Select(registeredUserId => _userRepository.GetById(new Guid(registeredUserId)).Value).ToList();
            return test;
        }
    }
}