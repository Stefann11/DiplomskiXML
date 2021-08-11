using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using NotificationMicroservice.Core.Model;
using NotificationMicroservice.Core.Services;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationMicroservice.Api.Consumers
{
    public class ReportUserEditedEventConsumer : IConsumeAsync<ReportUserEditedEvent>
    {
        private readonly RegisteredUserService userService;
        private readonly IBus _bus;

        public ReportUserEditedEventConsumer(RegisteredUserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(ReportUserEditedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateEditAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new UserEditCompletedEvent
                {
                    Id = message.Id,
                });
            }
            else
            {
                await _bus.PubSub.PublishAsync(new UnsuccessfulNotificationUserEditEvent
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

        private RegisteredUser Convert(ReportUserEditedEvent message)
        {
            return new RegisteredUser(message.Id,
                                           message.Username,
                                           "");
        }
    }
}