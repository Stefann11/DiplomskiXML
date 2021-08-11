using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        private readonly IBus _bus;

        public UserService(IUserRepository userRepository, IConfiguration config, IBus bus)
        {
            _userRepository = userRepository;
            _config = config;
            _bus = bus;
        }

        public async Task<Result> FollowAsync(Guid id, Guid followedById, Guid followingId)
        {
            var result = Follow(id, followedById, followingId);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success();
        }

        public Task RejectFollowAsync(Guid followedById, Guid followingId, string reson)
        {
            _userRepository.DeleteFollow(followedById, followingId);

            return Task.CompletedTask;
        }

        public Result Follow(Guid id, Guid followedById, Guid followingId)
        {
            if (_userRepository.AlreadyFollowing(followedById, followingId)) return Result.Failure("They are already following");
            _userRepository.Follow(id, followedById, followingId);
            return Result.Success();
        }

        public async Task<Result> CreateEditAsync(RegisteredUser registeredUser)
        {
            return Edit(registeredUser);
        }

        public Task RejectEditAsync(RegisteredUser user, string reason)
        {
            _userRepository.Edit(user);

            return Task.CompletedTask;
        }

        public Result Edit(RegisteredUser registeredUser)
        {
            if (!_userRepository.GetById(registeredUser.Id).Value.Username.Equals(registeredUser.Username))
            {
                if (_userRepository.GetByUsername(registeredUser.Username).HasValue) return Result.Failure("There is already user with that username");
            }
            _userRepository.Edit(registeredUser);
            return Result.Success(registeredUser);
        }

        public async Task<Result> CreateRegistrationAsync(RegisteredUser registeredUser)
        {
            var result = Create(registeredUser);
            return result;
        }

        public Result Create(RegisteredUser registeredUser)
        {
            if (_userRepository.GetById(registeredUser.Id).HasValue) return Result.Failure("User with that id already exist");
            if (_userRepository.GetByUsername(registeredUser.Username).HasValue) return Result.Failure("User with that username already exist");
            _userRepository.Save(registeredUser);
            return Result.Success(registeredUser);
        }

        public Task RejectRegistrationAsync(Guid registeredUserId, string reason)
        {
            _userRepository.Delete(registeredUserId);

            return Task.CompletedTask;
        }

        public async Task<Result> MuteAsync(Guid id, Guid mutedById, Guid mutingId)
        {
            var result = Mute(id, mutedById, mutingId);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success("User is successfully muted");
        }

        public Result Mute(Guid id, Guid mutedById, Guid mutingId)
        {
            var user = _userRepository.GetById(mutedById);
            var mutedUser = _userRepository.GetById(mutingId);
            if ((user.HasNoValue) || (mutedUser.HasNoValue)) return Result.Failure("There is no user with that id");
            if (user.Value.MutedUsers.Contains(mutedUser.Value)) return Result.Failure("User is already a muted");
            _userRepository.Mute(id, mutedById, mutingId);
            return Result.Success("User is successfully muted");
        }

        public async Task<Result> BlockAsync(Guid id, Guid blockedById, Guid blockingId)
        {
            var result = Block(id, blockedById, blockingId);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success();
        }

        public Task RejectBlockAsync(Guid id, string reason)
        {
            _userRepository.DeleteBlock(id);

            return Task.CompletedTask;
        }

        public Result Block(Guid id, Guid blockedById, Guid blockingId)
        {
            var user = _userRepository.GetById(blockedById);
            var blockedUser = _userRepository.GetById(blockingId);
            if ((user.HasNoValue) || (blockedUser.HasNoValue)) return Result.Failure("There is no user with that id");
            if (user.Value.BlockedUsers.Contains(blockedUser.Value)) return Result.Failure("User is already a blocked");
            _userRepository.Block(id, blockedById, blockingId);
            _userRepository.DeleteFollows(blockedById, blockingId);
            return Result.Success("User is successfully blocked");
        }

        public async Task<Result> EditAgentAsync(Agent registeredUser)
        {
            var result = EditAgent(registeredUser);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success(registeredUser);
        }

        public Result EditAgent(Agent agent)
        {
            if (!_userRepository.GetById(agent.Id).Value.Username.Equals(agent.Username))
            {
                if (_userRepository.GetByUsername(agent.Username).HasValue) return Result.Failure("There is already user with that username");
            }
            _userRepository.EditAgent(agent);
            return Result.Success(agent);
        }

        public async Task<Result> EditVerifiedUserAsync(VerifiedUser registeredUser)
        {
            var result = EditVerifiedUser(registeredUser);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success(registeredUser);
        }

        public Result EditVerifiedUser(VerifiedUser verifiedUser)
        {
            if (!_userRepository.GetById(verifiedUser.Id).Value.Username.ToString().Equals(verifiedUser.Username))
            {
                if (_userRepository.GetByUsername(verifiedUser.Username).HasValue) return Result.Failure("User with that username already exist");
            }
            _userRepository.EditVerifiedUser(verifiedUser);
            return Result.Success(verifiedUser);
        }
    }
}