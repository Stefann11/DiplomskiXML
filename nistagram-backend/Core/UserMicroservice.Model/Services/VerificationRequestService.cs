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
    public class VerificationRequestService
    {
        private readonly IVerificationRequestRepository _verificationRequestRepository;
        private readonly UserService _userService;
        private readonly IBus _bus;

        public VerificationRequestService(IVerificationRequestRepository verificationRequestRepository, UserService userService, IBus bus)
        {
            _verificationRequestRepository = verificationRequestRepository;
            _userService = userService;
            _bus = bus;
        }

        public async Task<Result> VerifyUserAsync(VerificationRequest verificationRequest)
        {
            var result = Edit(VerificationRequest.Create(verificationRequest.Id, verificationRequest.RegisteredUser, verificationRequest.FirstName,
                verificationRequest.LastName, verificationRequest.Category, verificationRequest.DocumentImagePath, true).Value);
            if (result.IsFailure) return Result.Failure(result.Error);
            var verifiedUser = VerifiedUser.Create(verificationRequest.RegisteredUser.Id, verificationRequest.RegisteredUser.Username,
                verificationRequest.RegisteredUser.EmailAddress, verificationRequest.RegisteredUser.FirstName, verificationRequest.RegisteredUser.LastName,
                verificationRequest.RegisteredUser.DateOfBirth, verificationRequest.RegisteredUser.PhoneNumber, verificationRequest.RegisteredUser.Gender,
                verificationRequest.RegisteredUser.WebsiteAddress, verificationRequest.RegisteredUser.Bio, verificationRequest.RegisteredUser.IsPrivate,
                verificationRequest.RegisteredUser.IsAcceptingMessages, verificationRequest.RegisteredUser.IsAcceptingTags,
                verificationRequest.RegisteredUser.Password, verificationRequest.RegisteredUser.ProfileImagePath, verificationRequest.RegisteredUser.BlockedUsers,
                verificationRequest.RegisteredUser.BlockedByUsers, verificationRequest.RegisteredUser.MutedUsers, verificationRequest.RegisteredUser.MutedByUsers,
                verificationRequest.RegisteredUser.Following, verificationRequest.RegisteredUser.Followers, verificationRequest.RegisteredUser.MyCloseFriends,
                verificationRequest.RegisteredUser.CloseFriendTo, verificationRequest.RegisteredUser.IsBanned, Category.Create(verificationRequest.Category.ToString()).Value);
            if (result.IsFailure) return Result.Failure(result.Error);
            result = await _userService.EditVerifiedUserAsync(verifiedUser.Value);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success(verifiedUser);
        }

        public Result Create(VerificationRequest verificationRequest)
        {
            if (_verificationRequestRepository.GetById(verificationRequest.Id).HasValue)
                return Result.Failure("Verification request with that Id already exists");
            return Result.Success(_verificationRequestRepository.Save(verificationRequest));
        }

        public Result Edit(VerificationRequest verificationRequest)
        {
            if (_verificationRequestRepository.GetById(verificationRequest.Id).HasNoValue)
                return Result.Failure("Verification request with that Id does not exists");
            return Result.Success(_verificationRequestRepository.Edit(verificationRequest));
        }

        public Result Delete(Guid id)
        {
            if (_verificationRequestRepository.GetById(id).HasNoValue)
                return Result.Failure("Verification request with that Id does not exists");
            _verificationRequestRepository.Delete(id);
            return Result.Success();
        }
    }
}