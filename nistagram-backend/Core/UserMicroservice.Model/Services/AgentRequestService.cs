using CSharpFunctionalExtensions;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;

namespace UserMicroservice.Core.Services
{
    public class AgentRequestService
    {
        private readonly IAgentRequestRepository _agentRequestRepository;
        private readonly UserService _userService;
        private readonly IBus _bus;

        public AgentRequestService(IAgentRequestRepository agentRequestRepository, UserService userService, IBus bus)
        {
            _agentRequestRepository = agentRequestRepository;
            _userService = userService;
            _bus = bus;
        }

        public async Task<Result> UpdateAgentRequestAsync(AgentRequest agentRequest)
        {
            var result = Edit(agentRequest);
            if (result.IsFailure) return Result.Failure(result.Error);
            if (agentRequest.IsApproved)
            {
                var agent = Agent.Create(agentRequest.RegisteredUser.Id, agentRequest.Username,
                agentRequest.EmailAddress, agentRequest.FirstName, agentRequest.LastName,
                agentRequest.DateOfBirth, agentRequest.PhoneNumber, agentRequest.Gender,
                agentRequest.WebsiteAddress, agentRequest.Bio, agentRequest.IsPrivate,
                agentRequest.IsAcceptingMessages, agentRequest.IsAcceptingTags,
                agentRequest.Password, agentRequest.RegisteredUser.ProfileImagePath, agentRequest.RegisteredUser.BlockedUsers,
                agentRequest.RegisteredUser.BlockedByUsers, agentRequest.RegisteredUser.MutedUsers, agentRequest.RegisteredUser.MutedByUsers,
                agentRequest.RegisteredUser.Following, agentRequest.RegisteredUser.Followers, agentRequest.RegisteredUser.MyCloseFriends,
                agentRequest.RegisteredUser.CloseFriendTo, agentRequest.RegisteredUser.IsBanned);
                result = await _userService.EditAsync(agent.Value);
                if (result.IsFailure) return Result.Failure(result.Error);
                result = await _userService.EditAgentAsync(agent.Value);
                if (result.IsFailure) return Result.Failure(result.Error);
            }

            return Result.Success();
        }

        public Result Create(AgentRequest agentRequest)
        {
            if (_agentRequestRepository.GetById(agentRequest.Id).HasValue)
                return Result.Failure("Agent request with that Id already exists");
            return Result.Success(_agentRequestRepository.Save(agentRequest));
        }

        public Result Edit(AgentRequest agentRequest)
        {
            if (_agentRequestRepository.GetById(agentRequest.Id).HasNoValue)
                return Result.Failure("Agent request with that Id does not exists");
            return Result.Success(_agentRequestRepository.Edit(agentRequest));
        }
    }
}