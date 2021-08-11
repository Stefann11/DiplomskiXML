using CSharpFunctionalExtensions;
using EasyNetQ;
using ReportMicroservice.Core.Interface.Repository;
using ReportMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportMicroservice.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;

        public UserService(IUserRepository userRepository, IBus bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task<Result> CreateRegistrationAsync(RegisteredUser registeredUser)
        {
            return Create(registeredUser);
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

        public Task RejectRegistrationAsync(Guid registeredUserId, string reason)
        {
            _userRepository.Delete(registeredUserId);

            return Task.CompletedTask;
        }
    }
}