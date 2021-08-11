using CSharpFunctionalExtensions;
using EasyNetQ;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Model.File;
using Shared.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly IBus _bus;

        public UserService(IUserRepository userRepository, ICollectionRepository collectionRepository, IBus bus)
        {
            _userRepository = userRepository;
            _collectionRepository = collectionRepository;
            _bus = bus;
        }

        public RegisteredUser GetById(Guid id)
        {
            return _userRepository.GetById(id).Value;
        }

        public IEnumerable<RegisteredUser> GetTaggable()
        {
            return _userRepository.GetTaggable();
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
            _collectionRepository.Save(Collection.Create(Guid.NewGuid(), CollectionName.Create("favourites").Value,
                new List<Post>(), registeredUser).Value);
            return Result.Success(registeredUser);
        }

        public Task RejectRegistrationAsync(Guid registeredUserId, string reason)
        {
            _userRepository.Delete(registeredUserId);

            return Task.CompletedTask;
        }

        public async Task<Result> CreateEditAsync(RegisteredUser registeredUser)
        {
            return Edit(registeredUser);
        }

        public Result Edit(RegisteredUser registeredUser)
        {
            var test = _userRepository.GetById(registeredUser.Id).Value.Username.ToString();
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

        public void AddProfilePicture(Guid id, string image)
        {
            _userRepository.AddProfilePicture(id, image);
        }
    }
}