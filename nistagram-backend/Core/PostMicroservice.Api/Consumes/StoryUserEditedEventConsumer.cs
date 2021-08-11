using EasyNetQ;
using EasyNetQ.AutoSubscribe;
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
    public class StoryUserEditedEventConsumer : IConsumeAsync<StoryUserEditedEvent>
    {
        private readonly UserService userService;
        private readonly IBus _bus;

        public StoryUserEditedEventConsumer(UserService userService, IBus bus)
        {
            this.userService = userService;
            _bus = bus;
        }

        public async Task ConsumeAsync(StoryUserEditedEvent message, CancellationToken cancellationToken = default)
        {
            var result = await userService.CreateEditAsync(Convert(message));
            if (result.IsSuccess)
            {
                await _bus.PubSub.PublishAsync(new PostUserEditedEvent
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
                await _bus.PubSub.PublishAsync(new UnsuccessfulPostUserEditEvent
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

        private RegisteredUser Convert(StoryUserEditedEvent message)
        {
            return RegisteredUser.Create(message.Id,
                                           Username.Create(message.Username).Value,
                                           FirstName.Create(message.FirstName).Value,
                                           LastName.Create(message.LastName).Value,
                                           ProfileImagePath.Create(message.ProfilePicturePath).Value,
                                           message.IsPrivate,
                                           message.IsAcceptingTags,
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>(),
                                           new List<RegisteredUser>()).Value;
        }
    }
}