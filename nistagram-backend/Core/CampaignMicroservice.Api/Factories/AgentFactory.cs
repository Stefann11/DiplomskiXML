using CampaignMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace CampaignMicroservice.Api.Factories
{
    public class AgentFactory
    {
        public Agent Create(Core.Model.Agent agent)
        {
            return new Agent()
            {
                Id = agent.Id,
                Username = agent.Username,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                DateOfBirth = agent.DateOfBirth,
                Gender = agent.Gender,
                ProfileImagePath = agent.ProfileImagePath,
                IsPrivate = agent.IsPrivate,
                BlockedUsers = Convert(agent.BlockedUsers),
                BlockedByUsers = Convert(agent.BlockedByUsers),
                MutedUsers = Convert(agent.MutedUsers),
                MutedByUsers = Convert(agent.MutedByUsers),
                Following = Convert(agent.Following),
                Followers = Convert(agent.Followers),
                IsBanned = agent.IsBanned,
                WebsiteAddress = agent.WebsiteAddress
            };
        }

        public IEnumerable<RegisteredUser> Convert(IEnumerable<Core.Model.RegisteredUser> registeredUsers)
        {
            return registeredUsers.Select(registeredUser => new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                Gender = registeredUser.Gender,
                ProfileImagePath = registeredUser.ProfileImagePath,
                IsPrivate = registeredUser.IsPrivate,
                IsBanned = registeredUser.IsBanned
            }).ToList();
        }
    }
}