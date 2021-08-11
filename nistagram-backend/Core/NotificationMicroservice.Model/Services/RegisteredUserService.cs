using CSharpFunctionalExtensions;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using System.Threading.Tasks;

namespace NotificationMicroservice.Core.Services
{
    public class RegisteredUserService
    {
        private readonly IRegisteredUserRepository _userRepository;

        public RegisteredUserService(IRegisteredUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public async Task<Result> CreateEditAsync(RegisteredUser registeredUser)
        {
            return Edit(registeredUser);
        }

        public Result Edit(RegisteredUser registeredUser)
        {
            if (!_userRepository.GetById(registeredUser.Id).Value.Username.ToString().Equals(registeredUser.Username))
            {
                if (_userRepository.GetByUsername(registeredUser.Username).HasValue) return Result.Failure("There is already user with that username");
            }
            _userRepository.Edit(registeredUser);
            return Result.Success(registeredUser);
        }

        public Task RejectEditAsync(RegisteredUser user, string reason)
        {
            _userRepository.Edit(user);

            return Task.CompletedTask;
        }
    }
}