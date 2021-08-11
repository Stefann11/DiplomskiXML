using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class AgentRequestFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public AgentRequestFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public AgentRequest Create(Core.Model.AgentRequest agentRequest)
        {
            return new AgentRequest
            {
                Id = agentRequest.Id,
                IsApproved = agentRequest.IsApproved,
                RegisteredUser = registeredUserFactory.Create(agentRequest.RegisteredUser),
                AgentRequestAction = agentRequest.AgentRequestAction,
                Username = agentRequest.Username,
                EmailAddress = agentRequest.EmailAddress,
                FirstName = agentRequest.FirstName,
                LastName = agentRequest.LastName,
                DateOfBirth = agentRequest.DateOfBirth,
                PhoneNumber = agentRequest.PhoneNumber,
                Gender = agentRequest.Gender,
                WebsiteAddress = agentRequest.WebsiteAddress,
                Bio = agentRequest.Bio,
                IsPrivate = agentRequest.IsPrivate,
                IsAcceptingMessages = agentRequest.IsAcceptingMessages,
                IsAcceptingTags = agentRequest.IsAcceptingTags,
                Password = agentRequest.Password
            };
        }

        public IEnumerable<AgentRequest> CreateAgentRequests(IEnumerable<Core.Model.AgentRequest> agentRequests)
        {
            return agentRequests.Select(agentRequest => Create(agentRequest)).ToList();
        }
    }
}