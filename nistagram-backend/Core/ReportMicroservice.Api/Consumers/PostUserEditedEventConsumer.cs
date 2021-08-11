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
    public class PostUserEditedEventConsumer : IConsumeAsync<PostUserEditedEvent>
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public PostUserEditedEventConsumer(UserService userService, IBus bus, IUserRepository userRepository)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task ConsumeAsync(PostUserEditedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateEditAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new ReportUserEditedEvent
                {
                    Id = message.Id,
                    Username = message.Username,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    ProfilePicturePath = message.ProfilePicturePath,
                    IsPrivate = message.IsPrivate,
                    IsAcceptingTags = message.IsAcceptingTags,
                    Followers = message.Followers,
                    Following = message.Following,
                    BlockedUsers = message.BlockedUsers,
                    BlockedByUsers = message.BlockedByUsers,
                    MyCloseFriends = message.MyCloseFriends,
                    CloseFriendTo = message.CloseFriendTo,

                    OldEmailAddress = message.OldEmailAddress,
                    OldUsername = message.OldUsername,
                    OldFirstName = message.OldFirstName,
                    OldLastName = message.OldLastName,
                    OldDateOfBirth = message.OldDateOfBirth,
                    OldPhoneNumber = message.OldPhoneNumber,
                    OldGender = message.OldGender,
                    OldWebsiteAddress = message.OldWebsiteAddress,
                    OldBio = message.OldBio,
                    OldPassword = message.OldPassword,
                    OldIsPrivate = message.OldIsPrivate,
                    OldIsAcceptingMessages = message.OldIsAcceptingMessages,
                    OldIsAcceptingTags = message.OldIsAcceptingTags,
                    OldProfileImagePath = message.OldProfileImagePath,
                    OldBlockedUsers = message.OldBlockedUsers,
                    OldBlockedByUsers = message.OldBlockedByUsers,
                    OldMutedUsers = message.OldMutedUsers,
                    OldMutedByUsers = message.OldMutedByUsers,
                    OldFollowing = message.OldFollowing,
                    OldFollowers = message.OldFollowers,
                    OldMyCloseFriends = message.OldMyCloseFriends,
                    OldCloseFriendTo = message.OldCloseFriendTo,
                    OldIsBanned = message.OldIsBanned
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulReportUserEditEvent
                {
                    Id = message.Id.ToString(),
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

        private RegisteredUser Convert(PostUserEditedEvent message)
        {
            return RegisteredUser.Create(message.Id,
                                         Username.Create(message.Username).Value).Value;
        }
    }
}