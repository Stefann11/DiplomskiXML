using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class AgentFactory
    {
        public Agent Create(Core.Model.Agent agent)
        {
            return new Agent()
            {
                Id = agent.Id,
                Username = agent.Username,
                EmailAddress = agent.EmailAddress,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                DateOfBirth = agent.DateOfBirth,
                PhoneNumber = agent.PhoneNumber,
                Gender = agent.Gender,
                WebsiteAddress = agent.WebsiteAddress,
                Bio = agent.Bio,
                Password = agent.Password,
                ProfilePicturePath = agent.ProfileImagePath,
                IsPrivate = agent.IsPrivate,
                IsAcceptingMessages = agent.IsAcceptingMessages,
                IsAcceptingTags = agent.IsAcceptingTags,
                BlockedUsers = Convert(agent.BlockedUsers),
                BlockedByUsers = Convert(agent.BlockedByUsers),
                MutedUsers = Convert(agent.MutedUsers),
                MutedByUsers = Convert(agent.MutedByUsers),
                Following = Convert(agent.Following),
                Followers = Convert(agent.Followers),
                MyCloseFriends = Convert(agent.MyCloseFriends),
                CloseFriendTo = Convert(agent.CloseFriendTo),
                IsBanned = agent.IsBanned,
                Type = "Agent"
            };
        }

        public IEnumerable<RegisteredUser> Convert(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            return registeredUsers.Select(registeredUser => new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                EmailAddress = registeredUser.EmailAddress,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                PhoneNumber = registeredUser.PhoneNumber,
                Gender = registeredUser.Gender,
                WebsiteAddress = registeredUser.WebsiteAddress,
                Bio = registeredUser.Bio,
                Password = registeredUser.Password,
                ProfilePicturePath = registeredUser.ProfileImagePath,
                IsPrivate = registeredUser.IsPrivate,
                IsAcceptingMessages = registeredUser.IsAcceptingMessages,
                IsAcceptingTags = registeredUser.IsAcceptingTags,
                IsBanned = registeredUser.IsBanned
            }).ToList();
        }
    }
}